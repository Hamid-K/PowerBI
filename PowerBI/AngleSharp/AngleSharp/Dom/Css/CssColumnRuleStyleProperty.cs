using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002A1 RID: 673
	internal sealed class CssColumnRuleStyleProperty : CssProperty
	{
		// Token: 0x060014EA RID: 5354 RVA: 0x0004CA0A File Offset: 0x0004AC0A
		internal CssColumnRuleStyleProperty()
			: base(PropertyNames.ColumnRuleStyle, PropertyFlags.None)
		{
		}

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x060014EB RID: 5355 RVA: 0x0004CA18 File Offset: 0x0004AC18
		internal override IValueConverter Converter
		{
			get
			{
				return CssColumnRuleStyleProperty.StyleConverter;
			}
		}

		// Token: 0x04000C36 RID: 3126
		private static readonly IValueConverter StyleConverter = Converters.LineStyleConverter.OrDefault(LineStyle.None);
	}
}
