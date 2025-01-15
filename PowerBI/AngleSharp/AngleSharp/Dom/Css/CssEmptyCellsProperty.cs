using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x0200024E RID: 590
	internal sealed class CssEmptyCellsProperty : CssProperty
	{
		// Token: 0x060013F2 RID: 5106 RVA: 0x0004B383 File Offset: 0x00049583
		internal CssEmptyCellsProperty()
			: base(PropertyNames.EmptyCells, PropertyFlags.Inherited)
		{
		}

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x060013F3 RID: 5107 RVA: 0x0004B391 File Offset: 0x00049591
		internal override IValueConverter Converter
		{
			get
			{
				return CssEmptyCellsProperty.StyleConverter;
			}
		}

		// Token: 0x04000BDF RID: 3039
		private static readonly IValueConverter StyleConverter = Converters.EmptyCellsConverter.OrDefault(true);
	}
}
