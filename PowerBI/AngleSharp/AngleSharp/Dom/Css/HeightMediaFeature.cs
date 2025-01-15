using System;
using AngleSharp.Css;
using AngleSharp.Css.Values;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000240 RID: 576
	internal sealed class HeightMediaFeature : MediaFeature
	{
		// Token: 0x060013BE RID: 5054 RVA: 0x0004AC6D File Offset: 0x00048E6D
		public HeightMediaFeature(string name)
			: base(name)
		{
		}

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x060013BF RID: 5055 RVA: 0x0004AE0F File Offset: 0x0004900F
		internal override IValueConverter Converter
		{
			get
			{
				return Converters.LengthConverter;
			}
		}

		// Token: 0x060013C0 RID: 5056 RVA: 0x0004AF24 File Offset: 0x00049124
		public override bool Validate(RenderDevice device)
		{
			float num = Length.Zero.ToPixel();
			float num2 = (float)device.ViewPortHeight;
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
