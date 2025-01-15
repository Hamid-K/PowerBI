using System;
using AngleSharp.Css;
using AngleSharp.Css.Values;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000284 RID: 644
	internal sealed class CssBorderImageWidthProperty : CssProperty
	{
		// Token: 0x06001493 RID: 5267 RVA: 0x0004C447 File Offset: 0x0004A647
		internal CssBorderImageWidthProperty()
			: base(PropertyNames.BorderImageWidth, PropertyFlags.None)
		{
		}

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x06001494 RID: 5268 RVA: 0x0004C455 File Offset: 0x0004A655
		internal override IValueConverter Converter
		{
			get
			{
				return CssBorderImageWidthProperty.StyleConverter;
			}
		}

		// Token: 0x04000C18 RID: 3096
		internal static readonly IValueConverter TheConverter = Converters.ImageBorderWidthConverter.Periodic(new string[0]);

		// Token: 0x04000C19 RID: 3097
		private static readonly IValueConverter StyleConverter = CssBorderImageWidthProperty.TheConverter.OrDefault(Length.Full);
	}
}
