using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x0200029C RID: 668
	internal sealed class CssColumnCountProperty : CssProperty
	{
		// Token: 0x060014DB RID: 5339 RVA: 0x0004C8C7 File Offset: 0x0004AAC7
		internal CssColumnCountProperty()
			: base(PropertyNames.ColumnCount, PropertyFlags.Animatable)
		{
		}

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x060014DC RID: 5340 RVA: 0x0004C8D5 File Offset: 0x0004AAD5
		internal override IValueConverter Converter
		{
			get
			{
				return CssColumnCountProperty.StyleConverter;
			}
		}

		// Token: 0x04000C31 RID: 3121
		private static readonly IValueConverter StyleConverter = Converters.OptionalIntegerConverter.OrDefault();
	}
}
