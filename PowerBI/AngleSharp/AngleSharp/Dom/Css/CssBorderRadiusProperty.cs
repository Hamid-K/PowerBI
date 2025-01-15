using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000287 RID: 647
	internal sealed class CssBorderRadiusProperty : CssShorthandProperty
	{
		// Token: 0x0600149C RID: 5276 RVA: 0x0004C4DD File Offset: 0x0004A6DD
		internal CssBorderRadiusProperty()
			: base(PropertyNames.BorderRadius, PropertyFlags.Animatable)
		{
		}

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x0600149D RID: 5277 RVA: 0x0004C4EB File Offset: 0x0004A6EB
		internal override IValueConverter Converter
		{
			get
			{
				return CssBorderRadiusProperty.StyleConverter;
			}
		}

		// Token: 0x04000C1C RID: 3100
		private static readonly IValueConverter StyleConverter = Converters.BorderRadiusShorthandConverter.OrDefault();
	}
}
