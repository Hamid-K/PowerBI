using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002B1 RID: 689
	internal sealed class CssDisplayProperty : CssProperty
	{
		// Token: 0x0600151A RID: 5402 RVA: 0x0004CCF6 File Offset: 0x0004AEF6
		internal CssDisplayProperty()
			: base(PropertyNames.Display, PropertyFlags.None)
		{
		}

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x0600151B RID: 5403 RVA: 0x0004CD04 File Offset: 0x0004AF04
		internal override IValueConverter Converter
		{
			get
			{
				return CssDisplayProperty.StyleConverter;
			}
		}

		// Token: 0x04000C46 RID: 3142
		private static readonly IValueConverter StyleConverter = Converters.DisplayModeConverter.OrDefault(DisplayMode.Inline);
	}
}
