using System;
using AngleSharp.Css;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002E7 RID: 743
	internal sealed class CssWordBreakProperty : CssProperty
	{
		// Token: 0x060015BB RID: 5563 RVA: 0x0004D7EC File Offset: 0x0004B9EC
		public CssWordBreakProperty()
			: base(PropertyNames.WordBreak, PropertyFlags.None)
		{
		}

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x060015BC RID: 5564 RVA: 0x0004D7FA File Offset: 0x0004B9FA
		internal override IValueConverter Converter
		{
			get
			{
				return CssWordBreakProperty.StyleConverter;
			}
		}

		// Token: 0x04000C7A RID: 3194
		private static readonly IValueConverter StyleConverter = Converters.WordBreakConverter;
	}
}
