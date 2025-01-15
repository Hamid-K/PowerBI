using System;
using AngleSharp.Css;
using AngleSharp.Css.Values;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x0200026F RID: 623
	internal sealed class CssBorderLeftColorProperty : CssProperty
	{
		// Token: 0x06001454 RID: 5204 RVA: 0x0004BD22 File Offset: 0x00049F22
		internal CssBorderLeftColorProperty()
			: base(PropertyNames.BorderLeftColor, PropertyFlags.None)
		{
		}

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x06001455 RID: 5205 RVA: 0x0004BD30 File Offset: 0x00049F30
		internal override IValueConverter Converter
		{
			get
			{
				return CssBorderLeftColorProperty.StyleConverter;
			}
		}

		// Token: 0x04000C00 RID: 3072
		private static readonly IValueConverter StyleConverter = Converters.CurrentColorConverter.OrDefault(Color.Transparent);
	}
}
