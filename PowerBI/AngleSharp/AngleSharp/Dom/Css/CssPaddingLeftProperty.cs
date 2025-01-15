using System;
using AngleSharp.Css;
using AngleSharp.Css.Values;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000292 RID: 658
	internal sealed class CssPaddingLeftProperty : CssProperty
	{
		// Token: 0x060014BD RID: 5309 RVA: 0x0004C6E4 File Offset: 0x0004A8E4
		internal CssPaddingLeftProperty()
			: base(PropertyNames.PaddingLeft, PropertyFlags.Unitless | PropertyFlags.Animatable)
		{
		}

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x060014BE RID: 5310 RVA: 0x0004C6F3 File Offset: 0x0004A8F3
		internal override IValueConverter Converter
		{
			get
			{
				return CssPaddingLeftProperty.StyleConverter;
			}
		}

		// Token: 0x04000C27 RID: 3111
		private static readonly IValueConverter StyleConverter = Converters.LengthOrPercentConverter.OrDefault(Length.Zero);
	}
}
