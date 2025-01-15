using System;
using AngleSharp.Css;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002DC RID: 732
	internal sealed class CssTextAnchorProperty : CssProperty
	{
		// Token: 0x0600159A RID: 5530 RVA: 0x0004D5C7 File Offset: 0x0004B7C7
		public CssTextAnchorProperty()
			: base(PropertyNames.TextAnchor, PropertyFlags.None)
		{
		}

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x0600159B RID: 5531 RVA: 0x0004D5D5 File Offset: 0x0004B7D5
		internal override IValueConverter Converter
		{
			get
			{
				return CssTextAnchorProperty.StyleConverter;
			}
		}

		// Token: 0x04000C6F RID: 3183
		private static readonly IValueConverter StyleConverter = Converters.TextAnchorConverter;
	}
}
