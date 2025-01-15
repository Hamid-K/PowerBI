using System;
using AngleSharp.Css;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002DA RID: 730
	internal sealed class CssTextAlignLastProperty : CssProperty
	{
		// Token: 0x06001594 RID: 5524 RVA: 0x0004D57F File Offset: 0x0004B77F
		public CssTextAlignLastProperty()
			: base(PropertyNames.TextAlignLast, PropertyFlags.None)
		{
		}

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x06001595 RID: 5525 RVA: 0x0004D58D File Offset: 0x0004B78D
		internal override IValueConverter Converter
		{
			get
			{
				return CssTextAlignLastProperty.StyleConverter;
			}
		}

		// Token: 0x04000C6D RID: 3181
		private static readonly IValueConverter StyleConverter = Converters.TextAlignLastConverter;
	}
}
