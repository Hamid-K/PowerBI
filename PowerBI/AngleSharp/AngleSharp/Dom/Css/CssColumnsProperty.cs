using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002A4 RID: 676
	internal sealed class CssColumnsProperty : CssShorthandProperty
	{
		// Token: 0x060014F3 RID: 5363 RVA: 0x0004CA83 File Offset: 0x0004AC83
		internal CssColumnsProperty()
			: base(PropertyNames.Columns, PropertyFlags.Animatable)
		{
		}

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x060014F4 RID: 5364 RVA: 0x0004CA91 File Offset: 0x0004AC91
		internal override IValueConverter Converter
		{
			get
			{
				return CssColumnsProperty.StyleConverter;
			}
		}

		// Token: 0x04000C39 RID: 3129
		private static readonly IValueConverter StyleConverter = Converters.WithAny(new IValueConverter[]
		{
			Converters.AutoLengthConverter.Option().For(new string[] { PropertyNames.ColumnWidth }),
			Converters.OptionalIntegerConverter.Option().For(new string[] { PropertyNames.ColumnCount })
		}).OrDefault();
	}
}
