using System;
using AngleSharp.Css;
using AngleSharp.Css.Values;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x0200028F RID: 655
	internal sealed class CssMarginRightProperty : CssProperty
	{
		// Token: 0x060014B4 RID: 5300 RVA: 0x0004C660 File Offset: 0x0004A860
		internal CssMarginRightProperty()
			: base(PropertyNames.MarginRight, PropertyFlags.Unitless | PropertyFlags.Animatable)
		{
		}

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x060014B5 RID: 5301 RVA: 0x0004C66F File Offset: 0x0004A86F
		internal override IValueConverter Converter
		{
			get
			{
				return CssMarginRightProperty.StyleConverter;
			}
		}

		// Token: 0x04000C24 RID: 3108
		private static readonly IValueConverter StyleConverter = Converters.AutoLengthOrPercentConverter.OrDefault(Length.Zero);
	}
}
