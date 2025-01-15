using System;
using AngleSharp.Css;
using AngleSharp.Css.Values;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000290 RID: 656
	internal sealed class CssMarginTopProperty : CssProperty
	{
		// Token: 0x060014B7 RID: 5303 RVA: 0x0004C68C File Offset: 0x0004A88C
		internal CssMarginTopProperty()
			: base(PropertyNames.MarginTop, PropertyFlags.Unitless | PropertyFlags.Animatable)
		{
		}

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x060014B8 RID: 5304 RVA: 0x0004C69B File Offset: 0x0004A89B
		internal override IValueConverter Converter
		{
			get
			{
				return CssMarginTopProperty.StyleConverter;
			}
		}

		// Token: 0x04000C25 RID: 3109
		private static readonly IValueConverter StyleConverter = Converters.AutoLengthOrPercentConverter.OrDefault(Length.Zero);
	}
}
