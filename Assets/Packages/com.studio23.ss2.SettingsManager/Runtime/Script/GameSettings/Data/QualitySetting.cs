using Studio23.SS2.SettingsManager.Data;
using System;

[Serializable]
public class QualitySetting
{
	public QualityName names;
	public TextureQuality textureQuality;
	public ShadowQuality shadowQuality;
	public bool vSyncCount;
	public bool volumetricLight;
	public bool bloom;
	public bool vignette;
	public bool ambientOcclusion;
}