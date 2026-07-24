using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace VisualScript.Runtime.AudioUnity
{
    [DefaultExecutionOrder(UpdateManager.AFTER_LATER)]

    [AddComponentMenu("")]
    public class AudioManager : Singleton<AudioManager>
    {
        private const int PREALLOCATE_BUFFER_SIZE = 4;

        // MEMBERS: -------------------------------------------------------------------------------

        [NonSerialized] private readonly Queue<Audio> m_Ready = new Queue<Audio>();
        [NonSerialized] private readonly Dictionary<Guid, Audio> m_Active = new Dictionary<Guid, Audio>();
        [NonSerialized] private readonly List<Guid> m_Complete = new List<Guid>();

        // PROPERTIES: ----------------------------------------------------------------------------

        internal static Transform Transform => Instance.transform;

        // INITIALIZERS: --------------------------------------------------------------------------

        protected override void OnCreate()
        {
            base.OnCreate();

            for (int i = 0; i < PREALLOCATE_BUFFER_SIZE; ++i)
            {
                Audio source = new Audio();
                this.m_Ready.Enqueue(source);
            }
        }

        // PUBLIC METHODS: ------------------------------------------------------------------------

        /// <summary>
        /// Returns true if the Audio ID is valid and it's an active audio queue
        /// </summary>
        /// <param name="audioId"></param>
        /// <returns></returns>
        public static bool IsPlaying(Guid audioId)
        {
            return Instance.m_Active.ContainsKey(audioId);
        }

        /// <summary>
        /// Returns true if the Audio Resource is currently being played
        /// </summary>
        /// <param name="audioResource"></param>
        /// <returns></returns>
        public static bool IsPlaying(AudioResource audioResource)
        {
            foreach (KeyValuePair<Guid, Audio> entry in Instance.m_Active)
            {
                if (entry.Value.Resource == audioResource) return true;
            }

            return false;
        }

        /// <summary>
        /// Returns true if a game object is the target of an active audio queue
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool IsPlaying(GameObject target)
        {
            if (target == null) return false;
            foreach (KeyValuePair<Guid, Audio> entry in Instance.m_Active)
            {
                if (entry.Value.HasTarget && entry.Value.Target == target) return true;
            }

            return false;
        }

        /// <summary>
        /// Gets the <code>IAudio</code> interface to modify the currently active Audio
        /// </summary>
        /// <param name="audioId"></param>
        /// <returns>The active <code>IAudio</code> reference. Null if it does not exist</returns>
        public static IAudio GetActive(Guid audioId)
        {
            return Instance.m_Active.GetValueOrDefault(audioId);
        }

        /// <summary>
        /// Plays an audio resource with the specified configuration options
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="config"></param>
        /// <returns>The unique audio ID of the resource played</returns>
        public static Guid Play(AudioResource resource, in AudioConfig config)
        {
            if (!Instance.m_Ready.TryDequeue(out Audio source))
            {
                source = new Audio();
            }

            Guid audioId = Guid.NewGuid();
            while (Instance.m_Active.ContainsKey(audioId))
            {
                audioId = Guid.NewGuid();
            }

            Instance.m_Active.Add(audioId, source);
            return source.Play(audioId, resource, config);
        }

        /// <summary>
        /// Stops an active audio queue by its ID
        /// </summary>
        /// <param name="audioId"></param>
        public static void Stop(Guid audioId)
        {
            if (Instance.m_Active.TryGetValue(audioId, out Audio source))
            {
                source.Stop();
            }
        }

        /// <summary>
        /// Fades and stops an active audio queue by its ID
        /// </summary>
        /// <param name="audioId"></param>
        /// <param name="transition"></param>
        public static async Awaitable Stop(Guid audioId, float transition)
        {
            if (Instance.m_Active.TryGetValue(audioId, out Audio source))
            {
                await source.Stop(transition);
            }
        }

        /// <summary>
        /// Stops all active audio queues that match the specified audio resource
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="transition"></param>
        public static async Awaitable Stop(AudioResource resource, float transition)
        {
            Awaitable awaitable = null;
            foreach (KeyValuePair<Guid, Audio> entry in Instance.m_Active)
            {
                if (entry.Value.Resource != resource) continue;
                awaitable = Stop(entry.Key, transition);
            }

            if (awaitable != null)
            {
                await awaitable;
            }
        }

        /// <summary>
        /// Stops oll active audio queues that are played on the targeted game object
        /// </summary>
        /// <param name="onTarget"></param>
        /// <param name="transition"></param>
        public static async Awaitable Stop(GameObject onTarget, float transition)
        {
            if (onTarget == null) return;

            Awaitable awaitable = null;
            foreach (KeyValuePair<Guid, Audio> entry in Instance.m_Active)
            {
                if (!entry.Value.HasTarget || entry.Value.Target != onTarget) continue;
                awaitable = Stop(entry.Key, transition);
            }

            if (awaitable != null)
            {
                await awaitable;
            }
        }

        /// <summary>
        /// Stops all active audio queues playing at the specified channel
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="transition"></param>
        public static async Awaitable StopChannel(AudioChannel channel, float transition)
        {
            Awaitable awaitable = null;
            foreach (KeyValuePair<Guid, Audio> entry in Instance.m_Active)
            {
                if (entry.Value.Channel != channel) continue;
                awaitable = Stop(entry.Key, transition);
            }

            if (awaitable != null)
            {
                await awaitable;
            }
        }

        // UPDATE METHODS: ------------------------------------------------------------------------

        private void Update()
        {
            m_Complete.Clear();

            foreach (KeyValuePair<Guid, Audio> entry in this.m_Active)
            {
                if (entry.Value.OnUpdate())
                {
                    this.m_Complete.Add(entry.Key);
                }
            }

            foreach (Guid audioId in this.m_Complete)
            {
                if (this.m_Active.Remove(audioId, out Audio source))
                {
                    this.m_Ready.Enqueue(source);
                }
            }
        }
    }
}