using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000297 RID: 663
	internal sealed class CssBreakBeforeProperty : CssProperty
	{
		// Token: 0x060014CC RID: 5324 RVA: 0x0004C7F3 File Offset: 0x0004A9F3
		internal CssBreakBeforeProperty()
			: base(PropertyNames.BreakBefore, PropertyFlags.None)
		{
		}

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x060014CD RID: 5325 RVA: 0x0004C801 File Offset: 0x0004AA01
		internal override IValueConverter Converter
		{
			get
			{
				return CssBreakBeforeProperty.StyleConverter;
			}
		}

		// Token: 0x04000C2C RID: 3116
		private static readonly IValueConverter StyleConverter = Converters.BreakModeConverter.OrDefault(BreakMode.Auto);
	}
}
