using System;
using AngleSharp.Css;
using AngleSharp.Css.Values;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x0200028E RID: 654
	internal sealed class CssMarginProperty : CssShorthandProperty
	{
		// Token: 0x060014B1 RID: 5297 RVA: 0x0004C5FE File Offset: 0x0004A7FE
		internal CssMarginProperty()
			: base(PropertyNames.Margin, PropertyFlags.None)
		{
		}

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x060014B2 RID: 5298 RVA: 0x0004C60C File Offset: 0x0004A80C
		internal override IValueConverter Converter
		{
			get
			{
				return CssMarginProperty.StyleConverter;
			}
		}

		// Token: 0x04000C23 RID: 3107
		private static readonly IValueConverter StyleConverter = Converters.AutoLengthOrPercentConverter.Periodic(new string[]
		{
			PropertyNames.MarginTop,
			PropertyNames.MarginRight,
			PropertyNames.MarginBottom,
			PropertyNames.MarginLeft
		}).OrDefault(Length.Zero);
	}
}
