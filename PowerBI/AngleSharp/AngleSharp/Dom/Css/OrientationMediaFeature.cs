using System;
using AngleSharp.Css;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000243 RID: 579
	internal sealed class OrientationMediaFeature : MediaFeature
	{
		// Token: 0x060013C8 RID: 5064 RVA: 0x0004AFD2 File Offset: 0x000491D2
		public OrientationMediaFeature()
			: base(FeatureNames.Orientation)
		{
		}

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x060013C9 RID: 5065 RVA: 0x0004AFDF File Offset: 0x000491DF
		internal override IValueConverter Converter
		{
			get
			{
				return OrientationMediaFeature.TheConverter;
			}
		}

		// Token: 0x060013CA RID: 5066 RVA: 0x0004AFE8 File Offset: 0x000491E8
		public override bool Validate(RenderDevice device)
		{
			bool flag = false;
			bool flag2 = device.DeviceHeight >= device.DeviceWidth;
			return flag == flag2;
		}

		// Token: 0x04000BD5 RID: 3029
		private static readonly IValueConverter TheConverter = Converters.Toggle(Keywords.Portrait, Keywords.Landscape);
	}
}
