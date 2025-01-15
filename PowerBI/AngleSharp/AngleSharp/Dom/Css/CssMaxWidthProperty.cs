using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002AA RID: 682
	internal sealed class CssMaxWidthProperty : CssProperty
	{
		// Token: 0x06001505 RID: 5381 RVA: 0x0004CBCF File Offset: 0x0004ADCF
		internal CssMaxWidthProperty()
			: base(PropertyNames.MaxWidth, PropertyFlags.Animatable)
		{
		}

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x06001506 RID: 5382 RVA: 0x0004CBDD File Offset: 0x0004ADDD
		internal override IValueConverter Converter
		{
			get
			{
				return CssMaxWidthProperty.StyleConverter;
			}
		}

		// Token: 0x04000C3F RID: 3135
		private static readonly IValueConverter StyleConverter = Converters.OptionalLengthOrPercentConverter.OrDefault();
	}
}
