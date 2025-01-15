using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002DB RID: 731
	internal sealed class CssTextAlignProperty : CssProperty
	{
		// Token: 0x06001597 RID: 5527 RVA: 0x0004D5A0 File Offset: 0x0004B7A0
		internal CssTextAlignProperty()
			: base(PropertyNames.TextAlign, PropertyFlags.Inherited)
		{
		}

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x06001598 RID: 5528 RVA: 0x0004D5AE File Offset: 0x0004B7AE
		internal override IValueConverter Converter
		{
			get
			{
				return CssTextAlignProperty.StyleConverter;
			}
		}

		// Token: 0x04000C6E RID: 3182
		private static readonly IValueConverter StyleConverter = Converters.HorizontalAlignmentConverter.OrDefault(HorizontalAlignment.Left);
	}
}
