using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002C8 RID: 712
	internal sealed class CssListStyleProperty : CssShorthandProperty
	{
		// Token: 0x0600155E RID: 5470 RVA: 0x0004D1F7 File Offset: 0x0004B3F7
		internal CssListStyleProperty()
			: base(PropertyNames.ListStyle, PropertyFlags.Inherited)
		{
		}

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x0600155F RID: 5471 RVA: 0x0004D205 File Offset: 0x0004B405
		internal override IValueConverter Converter
		{
			get
			{
				return CssListStyleProperty.StyleConverter;
			}
		}

		// Token: 0x04000C5B RID: 3163
		private static readonly IValueConverter StyleConverter = Converters.WithAny(new IValueConverter[]
		{
			Converters.ListStyleConverter.Option().For(new string[] { PropertyNames.ListStyleType }),
			Converters.ListPositionConverter.Option().For(new string[] { PropertyNames.ListStylePosition }),
			Converters.OptionalImageSourceConverter.Option().For(new string[] { PropertyNames.ListStyleImage })
		}).OrDefault();
	}
}
