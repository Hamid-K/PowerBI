using System;
using AngleSharp.Css;
using AngleSharp.Css.Values;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002AB RID: 683
	internal sealed class CssMinHeightProperty : CssProperty
	{
		// Token: 0x06001508 RID: 5384 RVA: 0x0004CBF5 File Offset: 0x0004ADF5
		internal CssMinHeightProperty()
			: base(PropertyNames.MinHeight, PropertyFlags.Animatable)
		{
		}

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x06001509 RID: 5385 RVA: 0x0004CC03 File Offset: 0x0004AE03
		internal override IValueConverter Converter
		{
			get
			{
				return CssMinHeightProperty.StyleConverter;
			}
		}

		// Token: 0x04000C40 RID: 3136
		private static readonly IValueConverter StyleConverter = Converters.LengthOrPercentConverter.OrDefault(Length.Zero);
	}
}
