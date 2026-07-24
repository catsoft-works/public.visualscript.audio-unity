using VisualScript.Icons;

namespace VisualScript.Runtime.AudioUnity
{
    public class AudioUnityAsset : TAsset<AudioUnityRepository>
    {
        public override IIcon Icon => new IconAudioSource(ColorTheme.Type.TextLight);

        public override string Name => "Audio";
    }
}