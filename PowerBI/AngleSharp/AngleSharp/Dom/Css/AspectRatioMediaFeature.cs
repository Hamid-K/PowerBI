using System;
using AngleSharp.Css;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000238 RID: 568
	internal sealed class AspectRatioMediaFeature : MediaFeature
	{
		// Token: 0x060013A6 RID: 5030 RVA: 0x0004AC6D File Offset: 0x00048E6D
		public AspectRatioMediaFeature(string name)
			: base(name)
		{
		}

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x060013A7 RID: 5031 RVA: 0x0004AC76 File Offset: 0x00048E76
		internal override IValueConverter Converter
		{
			get
			{
				return Converters.RatioConverter;
			}
		}

		// Token: 0x060013A8 RID: 5032 RVA: 0x0004AC80 File Offset: 0x00048E80
		public override bool Validate(RenderDevice device)
		{
			Tuple<float, float> tuple = Tuple.Create<float, float>(1f, 1f);
			float num = tuple.Item1 / tuple.Item2;
			float num2 = (float)device.ViewPortWidth / (float)device.ViewPortHeight;
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
