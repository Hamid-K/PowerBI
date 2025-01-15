using System;
using AngleSharp.Css;
using AngleSharp.Css.Values;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000272 RID: 626
	internal sealed class CssBorderLeftWidthProperty : CssProperty
	{
		// Token: 0x0600145D RID: 5213 RVA: 0x0004BE0D File Offset: 0x0004A00D
		internal CssBorderLeftWidthProperty()
			: base(PropertyNames.BorderLeftWidth, PropertyFlags.Unitless | PropertyFlags.Animatable)
		{
		}

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x0600145E RID: 5214 RVA: 0x0004BE1C File Offset: 0x0004A01C
		internal override IValueConverter Converter
		{
			get
			{
				return CssBorderLeftWidthProperty.StyleConverter;
			}
		}

		// Token: 0x04000C03 RID: 3075
		private static readonly IValueConverter StyleConverter = Converters.LineWidthConverter.OrDefault(Length.Medium);
	}
}
