using System;
using AngleSharp.Css;
using AngleSharp.Css.Values;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x0200027A RID: 634
	internal sealed class CssBorderTopColorProperty : CssProperty
	{
		// Token: 0x06001475 RID: 5237 RVA: 0x0004C0B4 File Offset: 0x0004A2B4
		internal CssBorderTopColorProperty()
			: base(PropertyNames.BorderTopColor, PropertyFlags.None)
		{
		}

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x06001476 RID: 5238 RVA: 0x0004C0C2 File Offset: 0x0004A2C2
		internal override IValueConverter Converter
		{
			get
			{
				return CssBorderTopColorProperty.StyleConverter;
			}
		}

		// Token: 0x04000C0B RID: 3083
		private static readonly IValueConverter StyleConverter = Converters.CurrentColorConverter.OrDefault(Color.Transparent);
	}
}
