using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002A3 RID: 675
	internal sealed class CssColumnSpanProperty : CssProperty
	{
		// Token: 0x060014F0 RID: 5360 RVA: 0x0004CA5C File Offset: 0x0004AC5C
		internal CssColumnSpanProperty()
			: base(PropertyNames.ColumnSpan, PropertyFlags.None)
		{
		}

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x060014F1 RID: 5361 RVA: 0x0004CA6A File Offset: 0x0004AC6A
		internal override IValueConverter Converter
		{
			get
			{
				return CssColumnSpanProperty.StyleConverter;
			}
		}

		// Token: 0x04000C38 RID: 3128
		private static readonly IValueConverter StyleConverter = Converters.ColumnSpanConverter.OrDefault(false);
	}
}
