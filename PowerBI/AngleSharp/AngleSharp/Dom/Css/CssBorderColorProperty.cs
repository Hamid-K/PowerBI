using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x0200026E RID: 622
	internal sealed class CssBorderColorProperty : CssShorthandProperty
	{
		// Token: 0x06001451 RID: 5201 RVA: 0x0004BCD0 File Offset: 0x00049ED0
		internal CssBorderColorProperty()
			: base(PropertyNames.BorderColor, PropertyFlags.Hashless | PropertyFlags.Animatable)
		{
		}

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x06001452 RID: 5202 RVA: 0x0004BCDF File Offset: 0x00049EDF
		internal override IValueConverter Converter
		{
			get
			{
				return CssBorderColorProperty.StyleConverter;
			}
		}

		// Token: 0x04000BFF RID: 3071
		private static readonly IValueConverter StyleConverter = Converters.CurrentColorConverter.Periodic(new string[]
		{
			PropertyNames.BorderTopColor,
			PropertyNames.BorderRightColor,
			PropertyNames.BorderBottomColor,
			PropertyNames.BorderLeftColor
		}).OrDefault();
	}
}
