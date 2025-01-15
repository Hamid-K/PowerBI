using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002C9 RID: 713
	internal sealed class CssListStyleTypeProperty : CssProperty
	{
		// Token: 0x06001561 RID: 5473 RVA: 0x0004D28E File Offset: 0x0004B48E
		internal CssListStyleTypeProperty()
			: base(PropertyNames.ListStyleType, PropertyFlags.Inherited)
		{
		}

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x06001562 RID: 5474 RVA: 0x0004D29C File Offset: 0x0004B49C
		internal override IValueConverter Converter
		{
			get
			{
				return CssListStyleTypeProperty.StyleConverter;
			}
		}

		// Token: 0x04000C5C RID: 3164
		private static readonly IValueConverter StyleConverter = Converters.ListStyleConverter.OrDefault(ListStyle.Disc);
	}
}
