using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002CC RID: 716
	internal sealed class CssOutlineStyleProperty : CssProperty
	{
		// Token: 0x0600156A RID: 5482 RVA: 0x0004D37A File Offset: 0x0004B57A
		internal CssOutlineStyleProperty()
			: base(PropertyNames.OutlineStyle, PropertyFlags.None)
		{
		}

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x0600156B RID: 5483 RVA: 0x0004D388 File Offset: 0x0004B588
		internal override IValueConverter Converter
		{
			get
			{
				return CssOutlineStyleProperty.StyleConverter;
			}
		}

		// Token: 0x04000C5F RID: 3167
		private static readonly IValueConverter StyleConverter = Converters.LineStyleConverter.OrDefault(LineStyle.None);
	}
}
