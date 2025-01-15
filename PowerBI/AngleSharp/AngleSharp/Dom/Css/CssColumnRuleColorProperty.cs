using System;
using AngleSharp.Css;
using AngleSharp.Css.Values;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x0200029F RID: 671
	internal sealed class CssColumnRuleColorProperty : CssProperty
	{
		// Token: 0x060014E4 RID: 5348 RVA: 0x0004C945 File Offset: 0x0004AB45
		internal CssColumnRuleColorProperty()
			: base(PropertyNames.ColumnRuleColor, PropertyFlags.Animatable)
		{
		}

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x060014E5 RID: 5349 RVA: 0x0004C953 File Offset: 0x0004AB53
		internal override IValueConverter Converter
		{
			get
			{
				return CssColumnRuleColorProperty.StyleConverter;
			}
		}

		// Token: 0x04000C34 RID: 3124
		private static readonly IValueConverter StyleConverter = Converters.ColorConverter.OrDefault(Color.Transparent);
	}
}
