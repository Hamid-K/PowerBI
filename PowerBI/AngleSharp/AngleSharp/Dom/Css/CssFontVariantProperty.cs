using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002BD RID: 701
	internal sealed class CssFontVariantProperty : CssProperty
	{
		// Token: 0x0600153F RID: 5439 RVA: 0x0004D01D File Offset: 0x0004B21D
		internal CssFontVariantProperty()
			: base(PropertyNames.FontVariant, PropertyFlags.Inherited)
		{
		}

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x06001540 RID: 5440 RVA: 0x0004D02B File Offset: 0x0004B22B
		internal override IValueConverter Converter
		{
			get
			{
				return CssFontVariantProperty.StyleConverter;
			}
		}

		// Token: 0x04000C52 RID: 3154
		private static readonly IValueConverter StyleConverter = Converters.FontVariantConverter.OrDefault(FontVariant.Normal);
	}
}
