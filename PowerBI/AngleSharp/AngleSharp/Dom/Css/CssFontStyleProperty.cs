using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002BC RID: 700
	internal sealed class CssFontStyleProperty : CssProperty
	{
		// Token: 0x0600153C RID: 5436 RVA: 0x0004CFF6 File Offset: 0x0004B1F6
		internal CssFontStyleProperty()
			: base(PropertyNames.FontStyle, PropertyFlags.Inherited)
		{
		}

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x0600153D RID: 5437 RVA: 0x0004D004 File Offset: 0x0004B204
		internal override IValueConverter Converter
		{
			get
			{
				return CssFontStyleProperty.StyleConverter;
			}
		}

		// Token: 0x04000C51 RID: 3153
		private static readonly IValueConverter StyleConverter = Converters.FontStyleConverter.OrDefault(FontStyle.Normal);
	}
}
