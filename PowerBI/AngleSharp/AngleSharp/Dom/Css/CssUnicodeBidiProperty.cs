using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000254 RID: 596
	internal sealed class CssUnicodeBidiProperty : CssProperty
	{
		// Token: 0x06001404 RID: 5124 RVA: 0x0004B4C6 File Offset: 0x000496C6
		internal CssUnicodeBidiProperty()
			: base(PropertyNames.UnicodeBidi, PropertyFlags.None)
		{
		}

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x06001405 RID: 5125 RVA: 0x0004B4D4 File Offset: 0x000496D4
		internal override IValueConverter Converter
		{
			get
			{
				return CssUnicodeBidiProperty.StyleConverter;
			}
		}

		// Token: 0x04000BE4 RID: 3044
		private static readonly IValueConverter StyleConverter = Converters.UnicodeModeConverter.OrDefault(UnicodeMode.Normal);
	}
}
