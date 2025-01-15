using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000239 RID: 569
	internal sealed class ColorIndexMediaFeature : MediaFeature
	{
		// Token: 0x060013A9 RID: 5033 RVA: 0x0004AC6D File Offset: 0x00048E6D
		public ColorIndexMediaFeature(string name)
			: base(name)
		{
		}

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x060013AA RID: 5034 RVA: 0x0004ACDF File Offset: 0x00048EDF
		internal override IValueConverter Converter
		{
			get
			{
				if (!base.IsMinimum && !base.IsMaximum)
				{
					return Converters.NaturalIntegerConverter.Option(1);
				}
				return Converters.NaturalIntegerConverter;
			}
		}

		// Token: 0x060013AB RID: 5035 RVA: 0x0004AD04 File Offset: 0x00048F04
		public override bool Validate(RenderDevice device)
		{
			int num = 0;
			int colorBits = device.ColorBits;
			if (base.IsMaximum)
			{
				return colorBits <= num;
			}
			if (base.IsMinimum)
			{
				return colorBits >= num;
			}
			return num == colorBits;
		}
	}
}
