using System;
using AngleSharp.Css;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002D6 RID: 726
	internal sealed class CssStrokeProperty : CssProperty
	{
		// Token: 0x06001588 RID: 5512 RVA: 0x0004D4F5 File Offset: 0x0004B6F5
		internal CssStrokeProperty()
			: base(PropertyNames.Stroke, PropertyFlags.Animatable)
		{
		}

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x06001589 RID: 5513 RVA: 0x0004D503 File Offset: 0x0004B703
		internal override IValueConverter Converter
		{
			get
			{
				return CssStrokeProperty.StyleConverter;
			}
		}

		// Token: 0x04000C69 RID: 3177
		private static readonly IValueConverter StyleConverter = Converters.PaintConverter;
	}
}
