using System;
using AngleSharp.Css;
using AngleSharp.Css.Values;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002DD RID: 733
	internal sealed class CssTextDecorationColorProperty : CssProperty
	{
		// Token: 0x0600159D RID: 5533 RVA: 0x0004D5E8 File Offset: 0x0004B7E8
		internal CssTextDecorationColorProperty()
			: base(PropertyNames.TextDecorationColor, PropertyFlags.Animatable)
		{
		}

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x0600159E RID: 5534 RVA: 0x0004D5F6 File Offset: 0x0004B7F6
		internal override IValueConverter Converter
		{
			get
			{
				return CssTextDecorationColorProperty.StyleConverter;
			}
		}

		// Token: 0x04000C70 RID: 3184
		private static readonly IValueConverter StyleConverter = Converters.ColorConverter.OrDefault(Color.Black);
	}
}
