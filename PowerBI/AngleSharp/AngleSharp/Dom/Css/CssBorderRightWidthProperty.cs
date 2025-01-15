using System;
using AngleSharp.Css;
using AngleSharp.Css.Values;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000277 RID: 631
	internal sealed class CssBorderRightWidthProperty : CssProperty
	{
		// Token: 0x0600146C RID: 5228 RVA: 0x0004C005 File Offset: 0x0004A205
		internal CssBorderRightWidthProperty()
			: base(PropertyNames.BorderRightWidth, PropertyFlags.Unitless | PropertyFlags.Animatable)
		{
		}

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x0600146D RID: 5229 RVA: 0x0004C014 File Offset: 0x0004A214
		internal override IValueConverter Converter
		{
			get
			{
				return CssBorderRightWidthProperty.StyleConverter;
			}
		}

		// Token: 0x04000C08 RID: 3080
		private static readonly IValueConverter StyleConverter = Converters.LineWidthConverter.OrDefault(Length.Medium);
	}
}
