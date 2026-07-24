using System;
using System.Collections.Generic;
using UnityEngine;

namespace VisualScript.Runtime.AudioUnity
{
    [Serializable]
    public class Volume
    {
        private readonly struct Subscription : IDisposable
        {
            [NonSerialized] private readonly Action<Args> m_Callback;
            [NonSerialized] private readonly Args m_Args;

            public Subscription(Action<Args> callback, Args args)
            {
                this.m_Callback = callback;
                this.m_Args = args;
            }

            public void Invoke() => this.m_Callback?.Invoke(this.m_Args);

            public void Dispose() => this.m_Args?.Dispose();
        }

        // EXPOSED MEMBERS: -----------------------------------------------------------------------

        [SerializeField] private float m_Value;

        // MEMBERS: -------------------------------------------------------------------------------

        [NonSerialized]
        private readonly Dictionary<Guid, Subscription> m_Subscriptions = new Dictionary<Guid, Subscription>();

        // EVENTS: --------------------------------------------------------------------------------

        public event Action<float> EventChange;

        // PROPERTIES: ----------------------------------------------------------------------------

        public float Value
        {
            get => this.m_Value;
            set
            {
                float newValue = Mathf.Clamp01(value);
                if (Mathf.Approximately(this.m_Value, newValue)) return;

                this.m_Value = newValue;
                this.EventChange?.Invoke(newValue);

                foreach (KeyValuePair<Guid, Subscription> entry in this.m_Subscriptions)
                {
                    entry.Value.Invoke();
                }
            }
        }

        // SUBSCRIPTION METHODS: ------------------------------------------------------------------

        public Guid Subscribe(Action<Args> callback, Args args)
        {
            if (AppManager.IsExiting) return Guid.Empty;

            Guid subscriptionId = Guid.NewGuid();
            while (this.m_Subscriptions.ContainsKey(subscriptionId))
            {
                subscriptionId = Guid.NewGuid();
            }

            Subscription subscription = new Subscription(callback, args);
            this.m_Subscriptions.Add(subscriptionId, subscription);

            return subscriptionId;
        }

        public void Unsubscribe(Guid subscriptionId)
        {
            if (AppManager.IsExiting) return;

            this.m_Subscriptions.Remove(subscriptionId, out Subscription subscription);
            subscription.Dispose();
        }

        // CONSTRUCTORS: --------------------------------------------------------------------------

        public Volume()
        {
            this.m_Value = 1f;
        }

        public Volume(float value)
        {
            this.m_Value = value;
        }
    }
}