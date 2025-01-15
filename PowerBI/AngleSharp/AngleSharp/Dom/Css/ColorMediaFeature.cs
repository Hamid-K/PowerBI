using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x0200023A RID: 570
	internal sealed class ColorMediaFeature : MediaFeature
	{
		// Token: 0x060013AC RID: 5036 RVA: 0x0004AC6D File Offset: 0x00048E6D
		public ColorMediaFeature(string name)
			: base(name)
		{
		}

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x060013AD RID: 5037 RVA: 0x0004AD3E File Offset: 0x00048F3E
		internal override IValueConverter Converter
		{
			get
			{
				if (!base.IsMinimum && !base.IsMaximum)
				{
					return Converters.PositiveIntegerConverter.Option(1);
				}
				return Converters.PositiveIntegerConverter;
			}
		}

		// Token: 0x060013AE RID: 5038 RVA: 0x0004AD64 File Offset: 0x00048F64
		public override bool Validate(RenderDevice device)
		{
			int num = 1;
			double num2 = Math.Pow((double)device.ColorBits, 2.0);
			if (base.IsMaximum)
			{
				return num2 <= (double)num;
			}
			if (base.IsMinimum)
			{
				return num2 >= (double)num;
			}
			return (double)num == num2;
		}
	}
}
