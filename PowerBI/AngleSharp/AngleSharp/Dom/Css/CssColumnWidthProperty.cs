using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002A5 RID: 677
	internal sealed class CssColumnWidthProperty : CssProperty
	{
		// Token: 0x060014F6 RID: 5366 RVA: 0x0004CAFA File Offset: 0x0004ACFA
		internal CssColumnWidthProperty()
			: base(PropertyNames.ColumnWidth, PropertyFlags.Animatable)
		{
		}

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x060014F7 RID: 5367 RVA: 0x0004CB08 File Offset: 0x0004AD08
		internal override IValueConverter Converter
		{
			get
			{
				return CssColumnWidthProperty.StyleConverter;
			}
		}

		// Token: 0x04000C3A RID: 3130
		private static readonly IValueConverter StyleConverter = Converters.AutoLengthConverter.OrDefault(Keywords.Auto);
	}
}
