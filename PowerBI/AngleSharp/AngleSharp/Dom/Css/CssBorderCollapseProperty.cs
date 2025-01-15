using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x0200026D RID: 621
	internal sealed class CssBorderCollapseProperty : CssProperty
	{
		// Token: 0x0600144E RID: 5198 RVA: 0x0004BCA9 File Offset: 0x00049EA9
		internal CssBorderCollapseProperty()
			: base(PropertyNames.BorderCollapse, PropertyFlags.Inherited)
		{
		}

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x0600144F RID: 5199 RVA: 0x0004BCB7 File Offset: 0x00049EB7
		internal override IValueConverter Converter
		{
			get
			{
				return CssBorderCollapseProperty.StyleConverter;
			}
		}

		// Token: 0x04000BFE RID: 3070
		private static readonly IValueConverter StyleConverter = Converters.BorderCollapseConverter.OrDefault(true);
	}
}
