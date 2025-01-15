using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002BE RID: 702
	internal sealed class CssFontWeightProperty : CssProperty
	{
		// Token: 0x06001542 RID: 5442 RVA: 0x0004D044 File Offset: 0x0004B244
		internal CssFontWeightProperty()
			: base(PropertyNames.FontWeight, PropertyFlags.Inherited | PropertyFlags.Animatable)
		{
		}

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x06001543 RID: 5443 RVA: 0x0004D053 File Offset: 0x0004B253
		internal override IValueConverter Converter
		{
			get
			{
				return CssFontWeightProperty.StyleConverter;
			}
		}

		// Token: 0x04000C53 RID: 3155
		private static readonly IValueConverter StyleConverter = Converters.FontWeightConverter.Or(Converters.WeightIntegerConverter).OrDefault(FontWeight.Normal);
	}
}
