using System;
using AngleSharp.Css;
using AngleSharp.Css.Values;

namespace AngleSharp.Dom.Css
{
	// Token: 0x0200023E RID: 574
	internal sealed class DeviceWidthMediaFeature : MediaFeature
	{
		// Token: 0x060013B8 RID: 5048 RVA: 0x0004AC6D File Offset: 0x00048E6D
		public DeviceWidthMediaFeature(string name)
			: base(name)
		{
		}

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x060013B9 RID: 5049 RVA: 0x0004AE0F File Offset: 0x0004900F
		internal override IValueConverter Converter
		{
			get
			{
				return Converters.LengthConverter;
			}
		}

		// Token: 0x060013BA RID: 5050 RVA: 0x0004AEB0 File Offset: 0x000490B0
		public override bool Validate(RenderDevice device)
		{
			float num = Length.Zero.ToPixel();
			float num2 = (float)device.DeviceWidth;
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
