using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000251 RID: 593
	internal sealed class CssQuotesProperty : CssProperty
	{
		// Token: 0x060013FB RID: 5115 RVA: 0x0004B3FC File Offset: 0x000495FC
		internal CssQuotesProperty()
			: base(PropertyNames.Quotes, PropertyFlags.Inherited)
		{
		}

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x060013FC RID: 5116 RVA: 0x0004B40A File Offset: 0x0004960A
		internal override IValueConverter Converter
		{
			get
			{
				return CssQuotesProperty.StyleConverter;
			}
		}

		// Token: 0x04000BE2 RID: 3042
		private static readonly IValueConverter StyleConverter = Converters.EvenStringsConverter.OrNone().OrDefault(new string[] { "«", "»" });
	}
}
