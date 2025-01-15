using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000273 RID: 627
	internal sealed class CssBorderProperty : CssShorthandProperty
	{
		// Token: 0x06001460 RID: 5216 RVA: 0x0004BE39 File Offset: 0x0004A039
		internal CssBorderProperty()
			: base(PropertyNames.Border, PropertyFlags.Animatable)
		{
		}

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x06001461 RID: 5217 RVA: 0x0004BE47 File Offset: 0x0004A047
		internal override IValueConverter Converter
		{
			get
			{
				return CssBorderProperty.StyleConverter;
			}
		}

		// Token: 0x04000C04 RID: 3076
		private static readonly IValueConverter StyleConverter = Converters.WithAny(new IValueConverter[]
		{
			Converters.LineWidthConverter.Option().For(new string[]
			{
				PropertyNames.BorderTopWidth,
				PropertyNames.BorderRightWidth,
				PropertyNames.BorderBottomWidth,
				PropertyNames.BorderLeftWidth
			}),
			Converters.LineStyleConverter.Option().For(new string[]
			{
				PropertyNames.BorderTopStyle,
				PropertyNames.BorderRightStyle,
				PropertyNames.BorderBottomStyle,
				PropertyNames.BorderLeftStyle
			}),
			Converters.CurrentColorConverter.Option().For(new string[]
			{
				PropertyNames.BorderTopColor,
				PropertyNames.BorderRightColor,
				PropertyNames.BorderBottomColor,
				PropertyNames.BorderLeftColor
			})
		}).OrDefault();
	}
}
