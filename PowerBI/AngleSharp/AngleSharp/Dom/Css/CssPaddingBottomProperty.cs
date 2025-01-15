using System;
using AngleSharp.Css;
using AngleSharp.Css.Values;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000291 RID: 657
	internal sealed class CssPaddingBottomProperty : CssProperty
	{
		// Token: 0x060014BA RID: 5306 RVA: 0x0004C6B8 File Offset: 0x0004A8B8
		internal CssPaddingBottomProperty()
			: base(PropertyNames.PaddingBottom, PropertyFlags.Unitless | PropertyFlags.Animatable)
		{
		}

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x060014BB RID: 5307 RVA: 0x0004C6C7 File Offset: 0x0004A8C7
		internal override IValueConverter Converter
		{
			get
			{
				return CssPaddingBottomProperty.StyleConverter;
			}
		}

		// Token: 0x04000C26 RID: 3110
		private static readonly IValueConverter StyleConverter = Converters.LengthOrPercentConverter.OrDefault(Length.Zero);
	}
}
