using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002E4 RID: 740
	internal sealed class CssTextTransformProperty : CssProperty
	{
		// Token: 0x060015B2 RID: 5554 RVA: 0x0004D76D File Offset: 0x0004B96D
		internal CssTextTransformProperty()
			: base(PropertyNames.TextTransform, PropertyFlags.Inherited)
		{
		}

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x060015B3 RID: 5555 RVA: 0x0004D77B File Offset: 0x0004B97B
		internal override IValueConverter Converter
		{
			get
			{
				return CssTextTransformProperty.StyleConverter;
			}
		}

		// Token: 0x04000C77 RID: 3191
		private static readonly IValueConverter StyleConverter = Converters.TextTransformConverter.OrDefault(TextTransform.None);
	}
}
