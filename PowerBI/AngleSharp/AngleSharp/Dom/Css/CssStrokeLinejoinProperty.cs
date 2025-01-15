using System;
using AngleSharp.Css;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002D3 RID: 723
	internal sealed class CssStrokeLinejoinProperty : CssProperty
	{
		// Token: 0x0600157F RID: 5503 RVA: 0x0004D488 File Offset: 0x0004B688
		public CssStrokeLinejoinProperty()
			: base(PropertyNames.StrokeLinejoin, PropertyFlags.Animatable)
		{
		}

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x06001580 RID: 5504 RVA: 0x0004D496 File Offset: 0x0004B696
		internal override IValueConverter Converter
		{
			get
			{
				return CssStrokeLinejoinProperty.StyleConverter;
			}
		}

		// Token: 0x04000C66 RID: 3174
		private static readonly IValueConverter StyleConverter = Converters.StrokeLinejoinConverter;
	}
}
