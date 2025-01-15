using System;
using AngleSharp.Css;
using AngleSharp.Css.Values;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002E1 RID: 737
	internal sealed class CssTextIndentProperty : CssProperty
	{
		// Token: 0x060015A9 RID: 5545 RVA: 0x0004D6F9 File Offset: 0x0004B8F9
		internal CssTextIndentProperty()
			: base(PropertyNames.TextIndent, PropertyFlags.Inherited | PropertyFlags.Animatable)
		{
		}

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x060015AA RID: 5546 RVA: 0x0004D708 File Offset: 0x0004B908
		internal override IValueConverter Converter
		{
			get
			{
				return CssTextIndentProperty.StyleConverter;
			}
		}

		// Token: 0x04000C74 RID: 3188
		private static readonly IValueConverter StyleConverter = Converters.LengthOrPercentConverter.OrDefault(Length.Zero);
	}
}
