using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000279 RID: 633
	internal sealed class CssBorderStyleProperty : CssShorthandProperty
	{
		// Token: 0x06001472 RID: 5234 RVA: 0x0004C063 File Offset: 0x0004A263
		internal CssBorderStyleProperty()
			: base(PropertyNames.BorderStyle, PropertyFlags.None)
		{
		}

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x06001473 RID: 5235 RVA: 0x0004C071 File Offset: 0x0004A271
		internal override IValueConverter Converter
		{
			get
			{
				return CssBorderStyleProperty.StyleConverter;
			}
		}

		// Token: 0x04000C0A RID: 3082
		private static readonly IValueConverter StyleConverter = Converters.LineStyleConverter.Periodic(new string[]
		{
			PropertyNames.BorderTopStyle,
			PropertyNames.BorderRightStyle,
			PropertyNames.BorderBottomStyle,
			PropertyNames.BorderLeftStyle
		}).OrDefault();
	}
}
