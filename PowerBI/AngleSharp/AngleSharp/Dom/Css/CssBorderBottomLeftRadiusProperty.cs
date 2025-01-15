using System;
using AngleSharp.Css;
using AngleSharp.Css.Values;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000285 RID: 645
	internal sealed class CssBorderBottomLeftRadiusProperty : CssProperty
	{
		// Token: 0x06001496 RID: 5270 RVA: 0x0004C487 File Offset: 0x0004A687
		internal CssBorderBottomLeftRadiusProperty()
			: base(PropertyNames.BorderBottomLeftRadius, PropertyFlags.Animatable)
		{
		}

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x06001497 RID: 5271 RVA: 0x0004C495 File Offset: 0x0004A695
		internal override IValueConverter Converter
		{
			get
			{
				return CssBorderBottomLeftRadiusProperty.StyleConverter;
			}
		}

		// Token: 0x04000C1A RID: 3098
		private static readonly IValueConverter StyleConverter = Converters.BorderRadiusConverter.OrDefault(Length.Zero);
	}
}
