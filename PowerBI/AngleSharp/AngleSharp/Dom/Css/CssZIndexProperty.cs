using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002B5 RID: 693
	internal sealed class CssZIndexProperty : CssProperty
	{
		// Token: 0x06001526 RID: 5414 RVA: 0x0004CD92 File Offset: 0x0004AF92
		internal CssZIndexProperty()
			: base(PropertyNames.ZIndex, PropertyFlags.Animatable)
		{
		}

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x06001527 RID: 5415 RVA: 0x0004CDA0 File Offset: 0x0004AFA0
		internal override IValueConverter Converter
		{
			get
			{
				return CssZIndexProperty.StyleConverter;
			}
		}

		// Token: 0x04000C4A RID: 3146
		private static readonly IValueConverter StyleConverter = Converters.OptionalIntegerConverter.OrDefault();
	}
}
