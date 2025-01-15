using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x0200024B RID: 587
	internal sealed class CssCaptionSideProperty : CssProperty
	{
		// Token: 0x060013E5 RID: 5093 RVA: 0x0004B1DF File Offset: 0x000493DF
		internal CssCaptionSideProperty()
			: base(PropertyNames.CaptionSide, PropertyFlags.None)
		{
		}

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x060013E6 RID: 5094 RVA: 0x0004B1ED File Offset: 0x000493ED
		internal override IValueConverter Converter
		{
			get
			{
				return CssCaptionSideProperty.StyleConverter;
			}
		}

		// Token: 0x04000BDA RID: 3034
		private static readonly IValueConverter StyleConverter = Converters.CaptionSideConverter.OrDefault(true);
	}
}
