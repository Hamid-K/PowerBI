using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000275 RID: 629
	internal sealed class CssBorderRightProperty : CssShorthandProperty
	{
		// Token: 0x06001466 RID: 5222 RVA: 0x0004BF45 File Offset: 0x0004A145
		internal CssBorderRightProperty()
			: base(PropertyNames.BorderRight, PropertyFlags.Animatable)
		{
		}

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x06001467 RID: 5223 RVA: 0x0004BF53 File Offset: 0x0004A153
		internal override IValueConverter Converter
		{
			get
			{
				return CssBorderRightProperty.StyleConverter;
			}
		}

		// Token: 0x04000C06 RID: 3078
		private static readonly IValueConverter StyleConverter = Converters.WithAny(new IValueConverter[]
		{
			Converters.LineWidthConverter.Option().For(new string[] { PropertyNames.BorderRightWidth }),
			Converters.LineStyleConverter.Option().For(new string[] { PropertyNames.BorderRightStyle }),
			Converters.CurrentColorConverter.Option().For(new string[] { PropertyNames.BorderRightColor })
		}).OrDefault();
	}
}
