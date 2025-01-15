using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002B2 RID: 690
	internal sealed class CssFloatProperty : CssProperty
	{
		// Token: 0x0600151D RID: 5405 RVA: 0x0004CD1D File Offset: 0x0004AF1D
		internal CssFloatProperty()
			: base(PropertyNames.Float, PropertyFlags.None)
		{
		}

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x0600151E RID: 5406 RVA: 0x0004CD2B File Offset: 0x0004AF2B
		internal override IValueConverter Converter
		{
			get
			{
				return CssFloatProperty.StyleConverter;
			}
		}

		// Token: 0x04000C47 RID: 3143
		private static readonly IValueConverter StyleConverter = Converters.FloatingConverter.OrDefault(Floating.None);
	}
}
