using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000270 RID: 624
	internal sealed class CssBorderLeftProperty : CssShorthandProperty
	{
		// Token: 0x06001457 RID: 5207 RVA: 0x0004BD4D File Offset: 0x00049F4D
		internal CssBorderLeftProperty()
			: base(PropertyNames.BorderLeft, PropertyFlags.Animatable)
		{
		}

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x06001458 RID: 5208 RVA: 0x0004BD5B File Offset: 0x00049F5B
		internal override IValueConverter Converter
		{
			get
			{
				return CssBorderLeftProperty.StyleConverter;
			}
		}

		// Token: 0x04000C01 RID: 3073
		private static readonly IValueConverter StyleConverter = Converters.WithAny(new IValueConverter[]
		{
			Converters.LineWidthConverter.Option().For(new string[] { PropertyNames.BorderLeftWidth }),
			Converters.LineStyleConverter.Option().For(new string[] { PropertyNames.BorderLeftStyle }),
			Converters.CurrentColorConverter.Option().For(new string[] { PropertyNames.BorderLeftColor })
		}).OrDefault();
	}
}
