using System;
using AngleSharp.Css;
using AngleSharp.Css.Values;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x0200028D RID: 653
	internal sealed class CssMarginLeftProperty : CssProperty
	{
		// Token: 0x060014AE RID: 5294 RVA: 0x0004C5D2 File Offset: 0x0004A7D2
		internal CssMarginLeftProperty()
			: base(PropertyNames.MarginLeft, PropertyFlags.Unitless | PropertyFlags.Animatable)
		{
		}

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x060014AF RID: 5295 RVA: 0x0004C5E1 File Offset: 0x0004A7E1
		internal override IValueConverter Converter
		{
			get
			{
				return CssMarginLeftProperty.StyleConverter;
			}
		}

		// Token: 0x04000C22 RID: 3106
		private static readonly IValueConverter StyleConverter = Converters.AutoLengthOrPercentConverter.OrDefault(Length.Zero);
	}
}
