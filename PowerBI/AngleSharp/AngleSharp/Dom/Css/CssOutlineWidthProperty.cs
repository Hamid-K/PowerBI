using System;
using AngleSharp.Css;
using AngleSharp.Css.Values;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002CD RID: 717
	internal sealed class CssOutlineWidthProperty : CssProperty
	{
		// Token: 0x0600156D RID: 5485 RVA: 0x0004D3A1 File Offset: 0x0004B5A1
		internal CssOutlineWidthProperty()
			: base(PropertyNames.OutlineWidth, PropertyFlags.Animatable)
		{
		}

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x0600156E RID: 5486 RVA: 0x0004D3AF File Offset: 0x0004B5AF
		internal override IValueConverter Converter
		{
			get
			{
				return CssOutlineWidthProperty.StyleConverter;
			}
		}

		// Token: 0x04000C60 RID: 3168
		private static readonly IValueConverter StyleConverter = Converters.LineWidthConverter.OrDefault(Length.Medium);
	}
}
