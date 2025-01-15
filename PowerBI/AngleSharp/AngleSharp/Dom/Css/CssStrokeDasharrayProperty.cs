using System;
using AngleSharp.Css;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002D0 RID: 720
	internal sealed class CssStrokeDasharrayProperty : CssProperty
	{
		// Token: 0x06001576 RID: 5494 RVA: 0x0004D41E File Offset: 0x0004B61E
		public CssStrokeDasharrayProperty()
			: base(PropertyNames.StrokeDasharray, PropertyFlags.Unitless | PropertyFlags.Animatable)
		{
		}

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x06001577 RID: 5495 RVA: 0x0004D42D File Offset: 0x0004B62D
		internal override IValueConverter Converter
		{
			get
			{
				return CssStrokeDasharrayProperty.StyleConverter;
			}
		}

		// Token: 0x04000C63 RID: 3171
		private static readonly IValueConverter StyleConverter = Converters.StrokeDasharrayConverter;
	}
}
