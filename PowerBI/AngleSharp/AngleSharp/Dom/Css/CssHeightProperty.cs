using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002A7 RID: 679
	internal sealed class CssHeightProperty : CssProperty
	{
		// Token: 0x060014FC RID: 5372 RVA: 0x0004CB51 File Offset: 0x0004AD51
		internal CssHeightProperty()
			: base(PropertyNames.Height, PropertyFlags.Unitless | PropertyFlags.Animatable)
		{
		}

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x060014FD RID: 5373 RVA: 0x0004CB60 File Offset: 0x0004AD60
		internal override IValueConverter Converter
		{
			get
			{
				return CssHeightProperty.StyleConverter;
			}
		}

		// Token: 0x04000C3C RID: 3132
		private static readonly IValueConverter StyleConverter = Converters.AutoLengthOrPercentConverter.OrDefault(Keywords.Auto);
	}
}
