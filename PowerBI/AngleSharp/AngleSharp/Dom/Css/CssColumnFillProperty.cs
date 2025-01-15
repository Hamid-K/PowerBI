using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x0200029D RID: 669
	internal sealed class CssColumnFillProperty : CssProperty
	{
		// Token: 0x060014DE RID: 5342 RVA: 0x0004C8ED File Offset: 0x0004AAED
		internal CssColumnFillProperty()
			: base(PropertyNames.ColumnFill, PropertyFlags.None)
		{
		}

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x060014DF RID: 5343 RVA: 0x0004C8FB File Offset: 0x0004AAFB
		internal override IValueConverter Converter
		{
			get
			{
				return CssColumnFillProperty.StyleConverter;
			}
		}

		// Token: 0x04000C32 RID: 3122
		private static readonly IValueConverter StyleConverter = Converters.ColumnFillConverter.OrDefault(true);
	}
}
