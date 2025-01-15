using System;
using AngleSharp.Css;

namespace AngleSharp.Dom.Css
{
	// Token: 0x0200023D RID: 573
	internal sealed class DevicePixelRatioFeature : MediaFeature
	{
		// Token: 0x060013B5 RID: 5045 RVA: 0x0004AC6D File Offset: 0x00048E6D
		public DevicePixelRatioFeature(string name)
			: base(name)
		{
		}

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x060013B6 RID: 5046 RVA: 0x0004AE5F File Offset: 0x0004905F
		internal override IValueConverter Converter
		{
			get
			{
				return Converters.NaturalNumberConverter;
			}
		}

		// Token: 0x060013B7 RID: 5047 RVA: 0x0004AE68 File Offset: 0x00049068
		public override bool Validate(RenderDevice device)
		{
			float num = 1f;
			float num2 = (float)device.Resolution / 96f;
			if (base.IsMaximum)
			{
				return num2 <= num;
			}
			if (base.IsMinimum)
			{
				return num2 >= num;
			}
			return num == num2;
		}
	}
}
