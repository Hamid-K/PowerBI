using System;
using AngleSharp.Css;
using AngleSharp.Css.Values;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002AC RID: 684
	internal sealed class CssMinWidthProperty : CssProperty
	{
		// Token: 0x0600150B RID: 5387 RVA: 0x0004CC20 File Offset: 0x0004AE20
		internal CssMinWidthProperty()
			: base(PropertyNames.MinWidth, PropertyFlags.Animatable)
		{
		}

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x0600150C RID: 5388 RVA: 0x0004CC2E File Offset: 0x0004AE2E
		internal override IValueConverter Converter
		{
			get
			{
				return CssMinWidthProperty.StyleConverter;
			}
		}

		// Token: 0x04000C41 RID: 3137
		private static readonly IValueConverter StyleConverter = Converters.LengthOrPercentConverter.OrDefault(Length.Zero);
	}
}
