using System;
using AngleSharp.Css;
using AngleSharp.Css.Values;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000286 RID: 646
	internal sealed class CssBorderBottomRightRadiusProperty : CssProperty
	{
		// Token: 0x06001499 RID: 5273 RVA: 0x0004C4B2 File Offset: 0x0004A6B2
		internal CssBorderBottomRightRadiusProperty()
			: base(PropertyNames.BorderBottomRightRadius, PropertyFlags.Animatable)
		{
		}

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x0600149A RID: 5274 RVA: 0x0004C4C0 File Offset: 0x0004A6C0
		internal override IValueConverter Converter
		{
			get
			{
				return CssBorderBottomRightRadiusProperty.StyleConverter;
			}
		}

		// Token: 0x04000C1B RID: 3099
		private static readonly IValueConverter StyleConverter = Converters.BorderRadiusConverter.OrDefault(Length.Zero);
	}
}
