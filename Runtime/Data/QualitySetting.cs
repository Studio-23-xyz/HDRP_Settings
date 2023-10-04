using System;

namespace Studio23.SS2.SettingsManager.Data
{
	[Serializable]
	public class QualitySetting
	{
		public QualityName Names;
		public TextureQuality TextureQuality;
		public ShadowQuality ShadowQuality;
		public bool VSyncCount;
		public bool VolumetricLight;
		public bool Bloom;
		public bool Vignette;
		public bool AmbientOcclusion;
	}
}