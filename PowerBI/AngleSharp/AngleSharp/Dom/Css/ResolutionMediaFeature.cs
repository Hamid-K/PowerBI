using System;
using AngleSharp.Css;
using AngleSharp.Css.Values;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000245 RID: 581
	internal sealed class ResolutionMediaFeature : MediaFeature
	{
		// Token: 0x060013D0 RID: 5072 RVA: 0x0004AC6D File Offset: 0x00048E6D
		public ResolutionMediaFeature(string name)
			: base(name)
		{
		}

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x060013D1 RID: 5073 RVA: 0x0004B046 File Offset: 0x00049246
		internal override IValueConverter Converter
		{
			get
			{
				return Converters.ResolutionConverter;
			}
		}

		// Token: 0x060013D2 RID: 5074 RVA: 0x0004B050 File Offset: 0x00049250
		public override bool Validate(RenderDevice device)
		{
			Resolution resolution = new Resolution(72f, Resolution.Unit.Dpi);
			float num = resolution.To(Resolution.Unit.Dpi);
			float num2 = (float)device.Resolution;
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
