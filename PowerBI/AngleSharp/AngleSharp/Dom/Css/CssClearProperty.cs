using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002B0 RID: 688
	internal sealed class CssClearProperty : CssProperty
	{
		// Token: 0x06001517 RID: 5399 RVA: 0x0004CCCF File Offset: 0x0004AECF
		internal CssClearProperty()
			: base(PropertyNames.Clear, PropertyFlags.None)
		{
		}

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x06001518 RID: 5400 RVA: 0x0004CCDD File Offset: 0x0004AEDD
		internal override IValueConverter Converter
		{
			get
			{
				return CssClearProperty.StyleConverter;
			}
		}

		// Token: 0x04000C45 RID: 3141
		private static readonly IValueConverter StyleConverter = Converters.ClearModeConverter.OrDefault(ClearMode.None);
	}
}
