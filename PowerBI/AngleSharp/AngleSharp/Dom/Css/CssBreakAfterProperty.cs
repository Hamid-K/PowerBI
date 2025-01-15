using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000296 RID: 662
	internal sealed class CssBreakAfterProperty : CssProperty
	{
		// Token: 0x060014C9 RID: 5321 RVA: 0x0004C7CC File Offset: 0x0004A9CC
		internal CssBreakAfterProperty()
			: base(PropertyNames.BreakAfter, PropertyFlags.None)
		{
		}

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x060014CA RID: 5322 RVA: 0x0004C7DA File Offset: 0x0004A9DA
		internal override IValueConverter Converter
		{
			get
			{
				return CssBreakAfterProperty.StyleConverter;
			}
		}

		// Token: 0x04000C2B RID: 3115
		private static readonly IValueConverter StyleConverter = Converters.BreakModeConverter.OrDefault(BreakMode.Auto);
	}
}
