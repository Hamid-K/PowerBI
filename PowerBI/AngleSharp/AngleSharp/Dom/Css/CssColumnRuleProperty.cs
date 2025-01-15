using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002A0 RID: 672
	internal sealed class CssColumnRuleProperty : CssShorthandProperty
	{
		// Token: 0x060014E7 RID: 5351 RVA: 0x0004C970 File Offset: 0x0004AB70
		internal CssColumnRuleProperty()
			: base(PropertyNames.ColumnRule, PropertyFlags.Animatable)
		{
		}

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x060014E8 RID: 5352 RVA: 0x0004C97E File Offset: 0x0004AB7E
		internal override IValueConverter Converter
		{
			get
			{
				return CssColumnRuleProperty.StyleConverter;
			}
		}

		// Token: 0x04000C35 RID: 3125
		private static readonly IValueConverter StyleConverter = Converters.WithAny(new IValueConverter[]
		{
			Converters.ColorConverter.Option().For(new string[] { PropertyNames.ColumnRuleColor }),
			Converters.LineWidthConverter.Option().For(new string[] { PropertyNames.ColumnRuleWidth }),
			Converters.LineStyleConverter.Option().For(new string[] { PropertyNames.ColumnRuleStyle })
		}).OrDefault();
	}
}
