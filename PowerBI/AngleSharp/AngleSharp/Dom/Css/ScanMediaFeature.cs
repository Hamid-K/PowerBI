using System;
using AngleSharp.Css;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000246 RID: 582
	internal sealed class ScanMediaFeature : MediaFeature
	{
		// Token: 0x060013D3 RID: 5075 RVA: 0x0004B09F File Offset: 0x0004929F
		public ScanMediaFeature()
			: base(FeatureNames.Scan)
		{
		}

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x060013D4 RID: 5076 RVA: 0x0004B0AC File Offset: 0x000492AC
		internal override IValueConverter Converter
		{
			get
			{
				return ScanMediaFeature.TheConverter;
			}
		}

		// Token: 0x060013D5 RID: 5077 RVA: 0x0004B0B4 File Offset: 0x000492B4
		public override bool Validate(RenderDevice device)
		{
			bool flag = false;
			bool isInterlaced = device.IsInterlaced;
			return flag == isInterlaced;
		}

		// Token: 0x04000BD7 RID: 3031
		private static readonly IValueConverter TheConverter = Converters.Toggle(Keywords.Interlace, Keywords.Progressive);
	}
}
