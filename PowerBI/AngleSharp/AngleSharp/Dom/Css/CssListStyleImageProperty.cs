using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002C6 RID: 710
	internal sealed class CssListStyleImageProperty : CssProperty
	{
		// Token: 0x06001558 RID: 5464 RVA: 0x0004D1AA File Offset: 0x0004B3AA
		internal CssListStyleImageProperty()
			: base(PropertyNames.ListStyleImage, PropertyFlags.Inherited)
		{
		}

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x06001559 RID: 5465 RVA: 0x0004D1B8 File Offset: 0x0004B3B8
		internal override IValueConverter Converter
		{
			get
			{
				return CssListStyleImageProperty.StyleConverter;
			}
		}

		// Token: 0x04000C59 RID: 3161
		private static readonly IValueConverter StyleConverter = Converters.OptionalImageSourceConverter.OrDefault();
	}
}
