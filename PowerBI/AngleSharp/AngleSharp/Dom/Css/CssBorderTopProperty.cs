using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x0200027B RID: 635
	internal sealed class CssBorderTopProperty : CssShorthandProperty
	{
		// Token: 0x06001478 RID: 5240 RVA: 0x0004C0DF File Offset: 0x0004A2DF
		internal CssBorderTopProperty()
			: base(PropertyNames.BorderTop, PropertyFlags.Animatable)
		{
		}

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x06001479 RID: 5241 RVA: 0x0004C0ED File Offset: 0x0004A2ED
		internal override IValueConverter Converter
		{
			get
			{
				return CssBorderTopProperty.StyleConverter;
			}
		}

		// Token: 0x04000C0C RID: 3084
		private static readonly IValueConverter StyleConverter = Converters.WithAny(new IValueConverter[]
		{
			Converters.LineWidthConverter.Option().For(new string[] { PropertyNames.BorderTopWidth }),
			Converters.LineStyleConverter.Option().For(new string[] { PropertyNames.BorderTopStyle }),
			Converters.CurrentColorConverter.Option().For(new string[] { PropertyNames.BorderTopColor })
		}).OrDefault();
	}
}
