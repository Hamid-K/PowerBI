using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002F2 RID: 754
	internal sealed class CssBackfaceVisibilityProperty : CssProperty
	{
		// Token: 0x060015DC RID: 5596 RVA: 0x0004DCFE File Offset: 0x0004BEFE
		internal CssBackfaceVisibilityProperty()
			: base(PropertyNames.BackfaceVisibility, PropertyFlags.None)
		{
		}

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x060015DD RID: 5597 RVA: 0x0004DD0C File Offset: 0x0004BF0C
		internal override IValueConverter Converter
		{
			get
			{
				return CssBackfaceVisibilityProperty.StyleConverter;
			}
		}

		// Token: 0x04000C85 RID: 3205
		private static readonly IValueConverter StyleConverter = Converters.BackfaceVisibilityConverter.OrDefault(true);
	}
}
