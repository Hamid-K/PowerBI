using System;
using AngleSharp.Css;
using AngleSharp.Css.Values;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000288 RID: 648
	internal sealed class CssBorderTopLeftRadiusProperty : CssProperty
	{
		// Token: 0x0600149F RID: 5279 RVA: 0x0004C503 File Offset: 0x0004A703
		internal CssBorderTopLeftRadiusProperty()
			: base(PropertyNames.BorderTopLeftRadius, PropertyFlags.Animatable)
		{
		}

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x060014A0 RID: 5280 RVA: 0x0004C511 File Offset: 0x0004A711
		internal override IValueConverter Converter
		{
			get
			{
				return CssBorderTopLeftRadiusProperty.StyleConverter;
			}
		}

		// Token: 0x04000C1D RID: 3101
		private static readonly IValueConverter StyleConverter = Converters.BorderRadiusConverter.OrDefault(Length.Zero);
	}
}
