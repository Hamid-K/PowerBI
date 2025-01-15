using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x0200026A RID: 618
	internal sealed class CssBorderBottomProperty : CssShorthandProperty
	{
		// Token: 0x06001445 RID: 5189 RVA: 0x0004BBBE File Offset: 0x00049DBE
		internal CssBorderBottomProperty()
			: base(PropertyNames.BorderBottom, PropertyFlags.Animatable)
		{
		}

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x06001446 RID: 5190 RVA: 0x0004BBCC File Offset: 0x00049DCC
		internal override IValueConverter Converter
		{
			get
			{
				return CssBorderBottomProperty.StyleConverter;
			}
		}

		// Token: 0x04000BFB RID: 3067
		private static readonly IValueConverter StyleConverter = Converters.WithAny(new IValueConverter[]
		{
			Converters.LineWidthConverter.Option().For(new string[] { PropertyNames.BorderBottomWidth }),
			Converters.LineStyleConverter.Option().For(new string[] { PropertyNames.BorderBottomStyle }),
			Converters.CurrentColorConverter.Option().For(new string[] { PropertyNames.BorderBottomColor })
		}).OrDefault();
	}
}
