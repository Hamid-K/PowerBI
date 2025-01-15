using System;
using AngleSharp.Css;
using AngleSharp.Css.Values;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000295 RID: 661
	internal sealed class CssPaddingTopProperty : CssProperty
	{
		// Token: 0x060014C6 RID: 5318 RVA: 0x0004C7A0 File Offset: 0x0004A9A0
		internal CssPaddingTopProperty()
			: base(PropertyNames.PaddingTop, PropertyFlags.Unitless | PropertyFlags.Animatable)
		{
		}

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x060014C7 RID: 5319 RVA: 0x0004C7AF File Offset: 0x0004A9AF
		internal override IValueConverter Converter
		{
			get
			{
				return CssPaddingTopProperty.StyleConverter;
			}
		}

		// Token: 0x04000C2A RID: 3114
		private static readonly IValueConverter StyleConverter = Converters.LengthOrPercentConverter.OrDefault(Length.Zero);
	}
}
