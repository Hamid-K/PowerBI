using System;
using AngleSharp.Css;
using AngleSharp.Css.Values;

namespace AngleSharp.Dom.Css
{
	// Token: 0x0200023C RID: 572
	internal sealed class DeviceHeightMediaFeature : MediaFeature
	{
		// Token: 0x060013B2 RID: 5042 RVA: 0x0004AC6D File Offset: 0x00048E6D
		public DeviceHeightMediaFeature(string name)
			: base(name)
		{
		}

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x060013B3 RID: 5043 RVA: 0x0004AE0F File Offset: 0x0004900F
		internal override IValueConverter Converter
		{
			get
			{
				return Converters.LengthConverter;
			}
		}

		// Token: 0x060013B4 RID: 5044 RVA: 0x0004AE18 File Offset: 0x00049018
		public override bool Validate(RenderDevice device)
		{
			float num = Length.Zero.ToPixel();
			float num2 = (float)device.DeviceHeight;
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
