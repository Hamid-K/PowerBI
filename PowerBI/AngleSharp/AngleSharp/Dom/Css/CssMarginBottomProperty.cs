using System;
using AngleSharp.Css;
using AngleSharp.Css.Values;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x0200028C RID: 652
	internal sealed class CssMarginBottomProperty : CssProperty
	{
		// Token: 0x060014AB RID: 5291 RVA: 0x0004C5A6 File Offset: 0x0004A7A6
		internal CssMarginBottomProperty()
			: base(PropertyNames.MarginBottom, PropertyFlags.Unitless | PropertyFlags.Animatable)
		{
		}

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x060014AC RID: 5292 RVA: 0x0004C5B5 File Offset: 0x0004A7B5
		internal override IValueConverter Converter
		{
			get
			{
				return CssMarginBottomProperty.StyleConverter;
			}
		}

		// Token: 0x04000C21 RID: 3105
		private static readonly IValueConverter StyleConverter = Converters.AutoLengthOrPercentConverter.OrDefault(Length.Zero);
	}
}
