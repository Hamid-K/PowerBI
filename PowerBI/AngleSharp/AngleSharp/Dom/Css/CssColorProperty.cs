using System;
using AngleSharp.Css;
using AngleSharp.Css.Values;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002B6 RID: 694
	internal sealed class CssColorProperty : CssProperty
	{
		// Token: 0x06001529 RID: 5417 RVA: 0x0004CDB8 File Offset: 0x0004AFB8
		internal CssColorProperty()
			: base(PropertyNames.Color, PropertyFlags.Inherited | PropertyFlags.Hashless | PropertyFlags.Animatable)
		{
		}

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x0600152A RID: 5418 RVA: 0x0004CDC7 File Offset: 0x0004AFC7
		internal override IValueConverter Converter
		{
			get
			{
				return CssColorProperty.StyleConverter;
			}
		}

		// Token: 0x04000C4B RID: 3147
		private static readonly IValueConverter StyleConverter = Converters.ColorConverter.OrDefault(Color.Black);
	}
}
