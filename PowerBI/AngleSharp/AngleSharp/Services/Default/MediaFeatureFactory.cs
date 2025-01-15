using System;
using System.Collections.Generic;
using AngleSharp.Css;
using AngleSharp.Dom.Css;

namespace AngleSharp.Services.Default
{
	// Token: 0x0200004F RID: 79
	internal sealed class MediaFeatureFactory : IMediaFeatureFactory
	{
		// Token: 0x0600018C RID: 396 RVA: 0x0000BA54 File Offset: 0x00009C54
		public MediaFeature Create(string name)
		{
			MediaFeatureFactory.Creator creator = null;
			if (this.creators.TryGetValue(name, out creator))
			{
				return creator();
			}
			return null;
		}

		// Token: 0x0600018D RID: 397 RVA: 0x0000BA7C File Offset: 0x00009C7C
		public MediaFeatureFactory()
		{
			Dictionary<string, MediaFeatureFactory.Creator> dictionary = new Dictionary<string, MediaFeatureFactory.Creator>(StringComparer.OrdinalIgnoreCase);
			dictionary.Add(FeatureNames.MinWidth, () => new WidthMediaFeature(FeatureNames.MinWidth));
			dictionary.Add(FeatureNames.MaxWidth, () => new WidthMediaFeature(FeatureNames.MaxWidth));
			dictionary.Add(FeatureNames.Width, () => new WidthMediaFeature(FeatureNames.Width));
			dictionary.Add(FeatureNames.MinHeight, () => new HeightMediaFeature(FeatureNames.MinHeight));
			dictionary.Add(FeatureNames.MaxHeight, () => new HeightMediaFeature(FeatureNames.MaxHeight));
			dictionary.Add(FeatureNames.Height, () => new HeightMediaFeature(FeatureNames.Height));
			dictionary.Add(FeatureNames.MinDeviceWidth, () => new DeviceWidthMediaFeature(FeatureNames.MinDeviceWidth));
			dictionary.Add(FeatureNames.MaxDeviceWidth, () => new DeviceWidthMediaFeature(FeatureNames.MaxDeviceWidth));
			dictionary.Add(FeatureNames.DeviceWidth, () => new DeviceWidthMediaFeature(FeatureNames.DeviceWidth));
			dictionary.Add(FeatureNames.MinDevicePixelRatio, () => new DevicePixelRatioFeature(FeatureNames.MinDevicePixelRatio));
			dictionary.Add(FeatureNames.MaxDevicePixelRatio, () => new DevicePixelRatioFeature(FeatureNames.MaxDevicePixelRatio));
			dictionary.Add(FeatureNames.DevicePixelRatio, () => new DevicePixelRatioFeature(FeatureNames.DevicePixelRatio));
			dictionary.Add(FeatureNames.MinDeviceHeight, () => new DeviceHeightMediaFeature(FeatureNames.MinDeviceHeight));
			dictionary.Add(FeatureNames.MaxDeviceHeight, () => new DeviceHeightMediaFeature(FeatureNames.MaxDeviceHeight));
			dictionary.Add(FeatureNames.DeviceHeight, () => new DeviceHeightMediaFeature(FeatureNames.DeviceHeight));
			dictionary.Add(FeatureNames.MinAspectRatio, () => new AspectRatioMediaFeature(FeatureNames.MinAspectRatio));
			dictionary.Add(FeatureNames.MaxAspectRatio, () => new AspectRatioMediaFeature(FeatureNames.MaxAspectRatio));
			dictionary.Add(FeatureNames.AspectRatio, () => new AspectRatioMediaFeature(FeatureNames.AspectRatio));
			dictionary.Add(FeatureNames.MinDeviceAspectRatio, () => new DeviceAspectRatioMediaFeature(FeatureNames.MinDeviceAspectRatio));
			dictionary.Add(FeatureNames.MaxDeviceAspectRatio, () => new DeviceAspectRatioMediaFeature(FeatureNames.MaxDeviceAspectRatio));
			dictionary.Add(FeatureNames.DeviceAspectRatio, () => new DeviceAspectRatioMediaFeature(FeatureNames.DeviceAspectRatio));
			dictionary.Add(FeatureNames.MinColor, () => new ColorMediaFeature(FeatureNames.MinColor));
			dictionary.Add(FeatureNames.MaxColor, () => new ColorMediaFeature(FeatureNames.MaxColor));
			dictionary.Add(FeatureNames.Color, () => new ColorMediaFeature(FeatureNames.Color));
			dictionary.Add(FeatureNames.MinColorIndex, () => new ColorIndexMediaFeature(FeatureNames.MinColorIndex));
			dictionary.Add(FeatureNames.MaxColorIndex, () => new ColorIndexMediaFeature(FeatureNames.MaxColorIndex));
			dictionary.Add(FeatureNames.ColorIndex, () => new ColorIndexMediaFeature(FeatureNames.ColorIndex));
			dictionary.Add(FeatureNames.MinMonochrome, () => new MonochromeMediaFeature(FeatureNames.MinMonochrome));
			dictionary.Add(FeatureNames.MaxMonochrome, () => new MonochromeMediaFeature(FeatureNames.MaxMonochrome));
			dictionary.Add(FeatureNames.Monochrome, () => new MonochromeMediaFeature(FeatureNames.Monochrome));
			dictionary.Add(FeatureNames.MinResolution, () => new ResolutionMediaFeature(FeatureNames.MinResolution));
			dictionary.Add(FeatureNames.MaxResolution, () => new ResolutionMediaFeature(FeatureNames.MaxResolution));
			dictionary.Add(FeatureNames.Resolution, () => new ResolutionMediaFeature(FeatureNames.Resolution));
			dictionary.Add(FeatureNames.Orientation, () => new OrientationMediaFeature());
			dictionary.Add(FeatureNames.Grid, () => new GridMediaFeature());
			dictionary.Add(FeatureNames.Scan, () => new ScanMediaFeature());
			dictionary.Add(FeatureNames.UpdateFrequency, () => new UpdateFrequencyMediaFeature());
			dictionary.Add(FeatureNames.Scripting, () => new ScriptingMediaFeature());
			dictionary.Add(FeatureNames.Pointer, () => new PointerMediaFeature());
			dictionary.Add(FeatureNames.Hover, () => new HoverMediaFeature());
			this.creators = dictionary;
			base..ctor();
		}

		// Token: 0x040001CD RID: 461
		private readonly Dictionary<string, MediaFeatureFactory.Creator> creators;

		// Token: 0x02000432 RID: 1074
		// (Invoke) Token: 0x060022CA RID: 8906
		private delegate MediaFeature Creator();
	}
}
