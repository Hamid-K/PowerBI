using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002E5 RID: 741
	internal sealed class CssVerticalAlignProperty : CssProperty
	{
		// Token: 0x060015B5 RID: 5557 RVA: 0x0004D794 File Offset: 0x0004B994
		internal CssVerticalAlignProperty()
			: base(PropertyNames.VerticalAlign, PropertyFlags.Animatable)
		{
		}

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x060015B6 RID: 5558 RVA: 0x0004D7A2 File Offset: 0x0004B9A2
		internal override IValueConverter Converter
		{
			get
			{
				return CssVerticalAlignProperty.StyleConverter;
			}
		}

		// Token: 0x04000C78 RID: 3192
		private static readonly IValueConverter StyleConverter = Converters.LengthOrPercentConverter.Or(Converters.VerticalAlignmentConverter).OrDefault(VerticalAlignment.Baseline);
	}
}
