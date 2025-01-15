using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x0200027E RID: 638
	internal sealed class CssBorderWidthProperty : CssShorthandProperty
	{
		// Token: 0x06001481 RID: 5249 RVA: 0x0004C1C9 File Offset: 0x0004A3C9
		internal CssBorderWidthProperty()
			: base(PropertyNames.BorderWidth, PropertyFlags.Animatable)
		{
		}

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x06001482 RID: 5250 RVA: 0x0004C1D7 File Offset: 0x0004A3D7
		internal override IValueConverter Converter
		{
			get
			{
				return CssBorderWidthProperty.StyleConverter;
			}
		}

		// Token: 0x04000C0F RID: 3087
		private static readonly IValueConverter StyleConverter = Converters.LineWidthConverter.Periodic(new string[]
		{
			PropertyNames.BorderTopWidth,
			PropertyNames.BorderRightWidth,
			PropertyNames.BorderBottomWidth,
			PropertyNames.BorderLeftWidth
		}).OrDefault();
	}
}
