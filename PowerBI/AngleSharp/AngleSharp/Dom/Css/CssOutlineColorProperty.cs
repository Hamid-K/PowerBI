using System;
using AngleSharp.Css;
using AngleSharp.Css.Values;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002CA RID: 714
	internal sealed class CssOutlineColorProperty : CssProperty
	{
		// Token: 0x06001564 RID: 5476 RVA: 0x0004D2B5 File Offset: 0x0004B4B5
		internal CssOutlineColorProperty()
			: base(PropertyNames.OutlineColor, PropertyFlags.Animatable)
		{
		}

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x06001565 RID: 5477 RVA: 0x0004D2C3 File Offset: 0x0004B4C3
		internal override IValueConverter Converter
		{
			get
			{
				return CssOutlineColorProperty.StyleConverter;
			}
		}

		// Token: 0x04000C5D RID: 3165
		private static readonly IValueConverter StyleConverter = Converters.InvertedColorConverter.OrDefault(Color.Transparent);
	}
}
