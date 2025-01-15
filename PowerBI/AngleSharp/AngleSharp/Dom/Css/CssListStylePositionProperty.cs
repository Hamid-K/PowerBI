using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002C7 RID: 711
	internal sealed class CssListStylePositionProperty : CssProperty
	{
		// Token: 0x0600155B RID: 5467 RVA: 0x0004D1D0 File Offset: 0x0004B3D0
		internal CssListStylePositionProperty()
			: base(PropertyNames.ListStylePosition, PropertyFlags.Inherited)
		{
		}

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x0600155C RID: 5468 RVA: 0x0004D1DE File Offset: 0x0004B3DE
		internal override IValueConverter Converter
		{
			get
			{
				return CssListStylePositionProperty.StyleConverter;
			}
		}

		// Token: 0x04000C5A RID: 3162
		private static readonly IValueConverter StyleConverter = Converters.ListPositionConverter.OrDefault(ListPosition.Outside);
	}
}
