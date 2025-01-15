using System;
using AngleSharp.Css;
using AngleSharp.Css.Values;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x0200027F RID: 639
	internal sealed class CssBorderImageOutsetProperty : CssProperty
	{
		// Token: 0x06001484 RID: 5252 RVA: 0x0004C21A File Offset: 0x0004A41A
		internal CssBorderImageOutsetProperty()
			: base(PropertyNames.BorderImageOutset, PropertyFlags.None)
		{
		}

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x06001485 RID: 5253 RVA: 0x0004C228 File Offset: 0x0004A428
		internal override IValueConverter Converter
		{
			get
			{
				return CssBorderImageOutsetProperty.StyleConverter;
			}
		}

		// Token: 0x04000C10 RID: 3088
		internal static readonly IValueConverter TheConverter = Converters.LengthOrPercentConverter.Periodic(new string[0]);

		// Token: 0x04000C11 RID: 3089
		private static readonly IValueConverter StyleConverter = CssBorderImageOutsetProperty.TheConverter.OrDefault(Length.Zero);
	}
}
