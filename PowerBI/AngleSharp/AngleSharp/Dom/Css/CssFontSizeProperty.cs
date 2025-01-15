using System;
using AngleSharp.Css;
using AngleSharp.Css.Values;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002BA RID: 698
	internal sealed class CssFontSizeProperty : CssProperty
	{
		// Token: 0x06001536 RID: 5430 RVA: 0x0004CFA1 File Offset: 0x0004B1A1
		internal CssFontSizeProperty()
			: base(PropertyNames.FontSize, PropertyFlags.Inherited | PropertyFlags.Unitless | PropertyFlags.Animatable)
		{
		}

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x06001537 RID: 5431 RVA: 0x0004CFB0 File Offset: 0x0004B1B0
		internal override IValueConverter Converter
		{
			get
			{
				return CssFontSizeProperty.StyleConverter;
			}
		}

		// Token: 0x04000C4F RID: 3151
		private static readonly IValueConverter StyleConverter = Converters.FontSizeConverter.OrDefault(FontSize.Medium.ToLength());
	}
}
