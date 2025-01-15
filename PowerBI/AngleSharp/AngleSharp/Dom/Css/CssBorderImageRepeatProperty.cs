using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000281 RID: 641
	internal sealed class CssBorderImageRepeatProperty : CssProperty
	{
		// Token: 0x0600148A RID: 5258 RVA: 0x0004C34A File Offset: 0x0004A54A
		internal CssBorderImageRepeatProperty()
			: base(PropertyNames.BorderImageRepeat, PropertyFlags.None)
		{
		}

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x0600148B RID: 5259 RVA: 0x0004C358 File Offset: 0x0004A558
		internal override IValueConverter Converter
		{
			get
			{
				return CssBorderImageRepeatProperty.StyleConverter;
			}
		}

		// Token: 0x04000C13 RID: 3091
		internal static readonly IValueConverter TheConverter = Map.BorderRepeatModes.ToConverter<BorderRepeat>().Many(1, 2);

		// Token: 0x04000C14 RID: 3092
		private static readonly IValueConverter StyleConverter = CssBorderImageRepeatProperty.TheConverter.OrDefault(BorderRepeat.Stretch);
	}
}
