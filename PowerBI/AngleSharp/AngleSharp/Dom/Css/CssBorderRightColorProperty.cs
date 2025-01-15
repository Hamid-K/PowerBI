using System;
using AngleSharp.Css;
using AngleSharp.Css.Values;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000274 RID: 628
	internal sealed class CssBorderRightColorProperty : CssProperty
	{
		// Token: 0x06001463 RID: 5219 RVA: 0x0004BF1A File Offset: 0x0004A11A
		internal CssBorderRightColorProperty()
			: base(PropertyNames.BorderRightColor, PropertyFlags.None)
		{
		}

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x06001464 RID: 5220 RVA: 0x0004BF28 File Offset: 0x0004A128
		internal override IValueConverter Converter
		{
			get
			{
				return CssBorderRightColorProperty.StyleConverter;
			}
		}

		// Token: 0x04000C05 RID: 3077
		private static readonly IValueConverter StyleConverter = Converters.CurrentColorConverter.OrDefault(Color.Transparent);
	}
}
