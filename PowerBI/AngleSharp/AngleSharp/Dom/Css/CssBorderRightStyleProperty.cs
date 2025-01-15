using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000276 RID: 630
	internal sealed class CssBorderRightStyleProperty : CssProperty
	{
		// Token: 0x06001469 RID: 5225 RVA: 0x0004BFDE File Offset: 0x0004A1DE
		internal CssBorderRightStyleProperty()
			: base(PropertyNames.BorderRightStyle, PropertyFlags.None)
		{
		}

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x0600146A RID: 5226 RVA: 0x0004BFEC File Offset: 0x0004A1EC
		internal override IValueConverter Converter
		{
			get
			{
				return CssBorderRightStyleProperty.StyleConverter;
			}
		}

		// Token: 0x04000C07 RID: 3079
		private static readonly IValueConverter StyleConverter = Converters.LineStyleConverter.OrDefault(LineStyle.None);
	}
}
