using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002BB RID: 699
	internal sealed class CssFontStretchProperty : CssProperty
	{
		// Token: 0x06001539 RID: 5433 RVA: 0x0004CFCE File Offset: 0x0004B1CE
		internal CssFontStretchProperty()
			: base(PropertyNames.FontStretch, PropertyFlags.Inherited | PropertyFlags.Animatable)
		{
		}

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x0600153A RID: 5434 RVA: 0x0004CFDD File Offset: 0x0004B1DD
		internal override IValueConverter Converter
		{
			get
			{
				return CssFontStretchProperty.StyleConverter;
			}
		}

		// Token: 0x04000C50 RID: 3152
		private static readonly IValueConverter StyleConverter = Converters.FontStretchConverter.OrDefault(FontStretch.Normal);
	}
}
