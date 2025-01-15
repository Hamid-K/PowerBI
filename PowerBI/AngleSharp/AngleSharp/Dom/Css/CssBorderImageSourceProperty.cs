using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000283 RID: 643
	internal sealed class CssBorderImageSourceProperty : CssProperty
	{
		// Token: 0x06001490 RID: 5264 RVA: 0x0004C421 File Offset: 0x0004A621
		internal CssBorderImageSourceProperty()
			: base(PropertyNames.BorderImageSource, PropertyFlags.None)
		{
		}

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x06001491 RID: 5265 RVA: 0x0004C42F File Offset: 0x0004A62F
		internal override IValueConverter Converter
		{
			get
			{
				return CssBorderImageSourceProperty.StyleConverter;
			}
		}

		// Token: 0x04000C17 RID: 3095
		private static readonly IValueConverter StyleConverter = Converters.OptionalImageSourceConverter.OrDefault();
	}
}
