using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x0200024D RID: 589
	internal sealed class CssCursorProperty : CssProperty
	{
		// Token: 0x060013EF RID: 5103 RVA: 0x0004B30B File Offset: 0x0004950B
		internal CssCursorProperty()
			: base(PropertyNames.Cursor, PropertyFlags.Inherited)
		{
		}

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x060013F0 RID: 5104 RVA: 0x0004B319 File Offset: 0x00049519
		internal override IValueConverter Converter
		{
			get
			{
				return CssCursorProperty.StyleConverter;
			}
		}

		// Token: 0x04000BDE RID: 3038
		private static readonly IValueConverter StyleConverter = Converters.ImageSourceConverter.Or(Converters.WithOrder(new IValueConverter[]
		{
			Converters.ImageSourceConverter.Required(),
			Converters.NumberConverter.Required(),
			Converters.NumberConverter.Required()
		})).RequiresEnd(Map.Cursors.ToConverter<SystemCursor>()).OrDefault(SystemCursor.Auto);
	}
}
