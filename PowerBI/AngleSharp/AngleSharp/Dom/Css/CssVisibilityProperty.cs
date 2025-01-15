using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002F5 RID: 757
	internal sealed class CssVisibilityProperty : CssProperty
	{
		// Token: 0x060015E5 RID: 5605 RVA: 0x0004DD76 File Offset: 0x0004BF76
		internal CssVisibilityProperty()
			: base(PropertyNames.Visibility, PropertyFlags.Inherited | PropertyFlags.Animatable)
		{
		}

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x060015E6 RID: 5606 RVA: 0x0004DD85 File Offset: 0x0004BF85
		internal override IValueConverter Converter
		{
			get
			{
				return CssVisibilityProperty.StyleConverter;
			}
		}

		// Token: 0x04000C88 RID: 3208
		private static readonly IValueConverter StyleConverter = Converters.VisibilityConverter.OrDefault(Visibility.Visible);
	}
}
