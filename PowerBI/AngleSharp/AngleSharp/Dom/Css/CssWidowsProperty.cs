using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000256 RID: 598
	internal sealed class CssWidowsProperty : CssProperty
	{
		// Token: 0x06001409 RID: 5129 RVA: 0x0004B4F7 File Offset: 0x000496F7
		internal CssWidowsProperty()
			: base(PropertyNames.Widows, PropertyFlags.Inherited)
		{
		}

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x0600140A RID: 5130 RVA: 0x0004B505 File Offset: 0x00049705
		internal override IValueConverter Converter
		{
			get
			{
				return CssWidowsProperty.StyleConverter;
			}
		}

		// Token: 0x04000BE5 RID: 3045
		private static readonly IValueConverter StyleConverter = Converters.IntegerConverter.OrDefault(2);
	}
}
