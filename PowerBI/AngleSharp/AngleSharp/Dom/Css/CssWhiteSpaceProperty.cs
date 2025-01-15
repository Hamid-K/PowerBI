using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002E6 RID: 742
	internal sealed class CssWhiteSpaceProperty : CssProperty
	{
		// Token: 0x060015B8 RID: 5560 RVA: 0x0004D7C5 File Offset: 0x0004B9C5
		internal CssWhiteSpaceProperty()
			: base(PropertyNames.WhiteSpace, PropertyFlags.Inherited)
		{
		}

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x060015B9 RID: 5561 RVA: 0x0004D7D3 File Offset: 0x0004B9D3
		internal override IValueConverter Converter
		{
			get
			{
				return CssWhiteSpaceProperty.StyleConverter;
			}
		}

		// Token: 0x04000C79 RID: 3193
		private static readonly IValueConverter StyleConverter = Converters.WhitespaceConverter.OrDefault(Whitespace.Normal);
	}
}
