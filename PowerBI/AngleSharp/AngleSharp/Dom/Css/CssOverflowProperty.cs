using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002B3 RID: 691
	internal sealed class CssOverflowProperty : CssProperty
	{
		// Token: 0x06001520 RID: 5408 RVA: 0x0004CD44 File Offset: 0x0004AF44
		internal CssOverflowProperty()
			: base(PropertyNames.Overflow, PropertyFlags.None)
		{
		}

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x06001521 RID: 5409 RVA: 0x0004CD52 File Offset: 0x0004AF52
		internal override IValueConverter Converter
		{
			get
			{
				return CssOverflowProperty.StyleConverter;
			}
		}

		// Token: 0x04000C48 RID: 3144
		private static readonly IValueConverter StyleConverter = Converters.OverflowModeConverter.OrDefault(OverflowMode.Visible);
	}
}
