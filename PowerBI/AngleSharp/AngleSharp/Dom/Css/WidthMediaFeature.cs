using System;
using AngleSharp.Css;
using AngleSharp.Css.Values;

namespace AngleSharp.Dom.Css
{
	// Token: 0x0200024A RID: 586
	internal sealed class WidthMediaFeature : MediaFeature
	{
		// Token: 0x060013E2 RID: 5090 RVA: 0x0004AC6D File Offset: 0x00048E6D
		public WidthMediaFeature(string name)
			: base(name)
		{
		}

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x060013E3 RID: 5091 RVA: 0x0004AE0F File Offset: 0x0004900F
		internal override IValueConverter Converter
		{
			get
			{
				return Converters.LengthConverter;
			}
		}

		// Token: 0x060013E4 RID: 5092 RVA: 0x0004B198 File Offset: 0x00049398
		public override bool Validate(RenderDevice device)
		{
			float num = Length.Zero.ToPixel();
			float num2 = (float)device.ViewPortWidth;
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
