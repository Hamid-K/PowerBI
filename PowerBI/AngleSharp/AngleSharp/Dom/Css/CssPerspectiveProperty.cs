using System;
using AngleSharp.Css;
using AngleSharp.Css.Values;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002E9 RID: 745
	internal sealed class CssPerspectiveProperty : CssProperty
	{
		// Token: 0x060015C1 RID: 5569 RVA: 0x0004D950 File Offset: 0x0004BB50
		internal CssPerspectiveProperty()
			: base(PropertyNames.Perspective, PropertyFlags.Animatable)
		{
		}

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x060015C2 RID: 5570 RVA: 0x0004D95E File Offset: 0x0004BB5E
		internal override IValueConverter Converter
		{
			get
			{
				return CssPerspectiveProperty.StyleConverter;
			}
		}

		// Token: 0x04000C7C RID: 3196
		private static readonly IValueConverter StyleConverter = Converters.LengthConverter.OrNone().OrDefault(Length.Zero);
	}
}
