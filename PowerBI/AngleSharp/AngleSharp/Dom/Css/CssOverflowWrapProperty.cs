using System;
using AngleSharp.Css;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002D9 RID: 729
	internal sealed class CssOverflowWrapProperty : CssProperty
	{
		// Token: 0x06001591 RID: 5521 RVA: 0x0004D55E File Offset: 0x0004B75E
		public CssOverflowWrapProperty()
			: base(PropertyNames.OverflowWrap, PropertyFlags.None)
		{
		}

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x06001592 RID: 5522 RVA: 0x0004D56C File Offset: 0x0004B76C
		internal override IValueConverter Converter
		{
			get
			{
				return CssOverflowWrapProperty.StyleConverter;
			}
		}

		// Token: 0x04000C6C RID: 3180
		private static readonly IValueConverter StyleConverter = Converters.OverflowWrapConverter;
	}
}
