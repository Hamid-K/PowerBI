using System;
using AngleSharp.Css;
using AngleSharp.Css.Values;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000294 RID: 660
	internal sealed class CssPaddingRightProperty : CssProperty
	{
		// Token: 0x060014C3 RID: 5315 RVA: 0x0004C774 File Offset: 0x0004A974
		internal CssPaddingRightProperty()
			: base(PropertyNames.PaddingRight, PropertyFlags.Unitless | PropertyFlags.Animatable)
		{
		}

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x060014C4 RID: 5316 RVA: 0x0004C783 File Offset: 0x0004A983
		internal override IValueConverter Converter
		{
			get
			{
				return CssPaddingRightProperty.StyleConverter;
			}
		}

		// Token: 0x04000C29 RID: 3113
		private static readonly IValueConverter StyleConverter = Converters.LengthOrPercentConverter.OrDefault(Length.Zero);
	}
}
