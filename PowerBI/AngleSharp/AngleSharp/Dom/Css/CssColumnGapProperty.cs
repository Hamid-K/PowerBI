using System;
using AngleSharp.Css;
using AngleSharp.Css.Values;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x0200029E RID: 670
	internal sealed class CssColumnGapProperty : CssProperty
	{
		// Token: 0x060014E1 RID: 5345 RVA: 0x0004C914 File Offset: 0x0004AB14
		internal CssColumnGapProperty()
			: base(PropertyNames.ColumnGap, PropertyFlags.Animatable)
		{
		}

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x060014E2 RID: 5346 RVA: 0x0004C922 File Offset: 0x0004AB22
		internal override IValueConverter Converter
		{
			get
			{
				return CssColumnGapProperty.StyleConverter;
			}
		}

		// Token: 0x04000C33 RID: 3123
		private static readonly IValueConverter StyleConverter = Converters.LengthOrNormalConverter.OrDefault(new Length(1f, Length.Unit.Em));
	}
}
