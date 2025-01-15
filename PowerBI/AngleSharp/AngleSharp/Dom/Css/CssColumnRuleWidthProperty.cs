using System;
using AngleSharp.Css;
using AngleSharp.Css.Values;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002A2 RID: 674
	internal sealed class CssColumnRuleWidthProperty : CssProperty
	{
		// Token: 0x060014ED RID: 5357 RVA: 0x0004CA31 File Offset: 0x0004AC31
		internal CssColumnRuleWidthProperty()
			: base(PropertyNames.ColumnRuleWidth, PropertyFlags.Animatable)
		{
		}

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x060014EE RID: 5358 RVA: 0x0004CA3F File Offset: 0x0004AC3F
		internal override IValueConverter Converter
		{
			get
			{
				return CssColumnRuleWidthProperty.StyleConverter;
			}
		}

		// Token: 0x04000C37 RID: 3127
		private static readonly IValueConverter StyleConverter = Converters.LineWidthConverter.OrDefault(Length.Medium);
	}
}
