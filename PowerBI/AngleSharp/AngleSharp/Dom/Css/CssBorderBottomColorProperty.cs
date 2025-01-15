using System;
using AngleSharp.Css;
using AngleSharp.Css.Values;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000269 RID: 617
	internal sealed class CssBorderBottomColorProperty : CssProperty
	{
		// Token: 0x06001442 RID: 5186 RVA: 0x0004BB93 File Offset: 0x00049D93
		internal CssBorderBottomColorProperty()
			: base(PropertyNames.BorderBottomColor, PropertyFlags.None)
		{
		}

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x06001443 RID: 5187 RVA: 0x0004BBA1 File Offset: 0x00049DA1
		internal override IValueConverter Converter
		{
			get
			{
				return CssBorderBottomColorProperty.StyleConverter;
			}
		}

		// Token: 0x04000BFA RID: 3066
		private static readonly IValueConverter StyleConverter = Converters.CurrentColorConverter.OrDefault(Color.Transparent);
	}
}
