using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002B7 RID: 695
	internal sealed class CssFontFamilyProperty : CssProperty
	{
		// Token: 0x0600152C RID: 5420 RVA: 0x0004CDE4 File Offset: 0x0004AFE4
		internal CssFontFamilyProperty()
			: base(PropertyNames.FontFamily, PropertyFlags.Inherited)
		{
		}

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x0600152D RID: 5421 RVA: 0x0004CDF2 File Offset: 0x0004AFF2
		internal override IValueConverter Converter
		{
			get
			{
				return CssFontFamilyProperty.StyleConverter;
			}
		}

		// Token: 0x04000C4C RID: 3148
		private static readonly IValueConverter StyleConverter = Converters.FontFamiliesConverter.OrDefault("Times New Roman");

		// Token: 0x02000500 RID: 1280
		private enum SystemFonts : byte
		{
			// Token: 0x04001220 RID: 4640
			Serif,
			// Token: 0x04001221 RID: 4641
			SansSerif,
			// Token: 0x04001222 RID: 4642
			Monospace,
			// Token: 0x04001223 RID: 4643
			Cursive,
			// Token: 0x04001224 RID: 4644
			Fantasy
		}
	}
}
