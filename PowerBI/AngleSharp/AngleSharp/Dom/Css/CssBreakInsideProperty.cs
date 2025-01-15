using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000298 RID: 664
	internal sealed class CssBreakInsideProperty : CssProperty
	{
		// Token: 0x060014CF RID: 5327 RVA: 0x0004C81A File Offset: 0x0004AA1A
		internal CssBreakInsideProperty()
			: base(PropertyNames.BreakInside, PropertyFlags.None)
		{
		}

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x060014D0 RID: 5328 RVA: 0x0004C828 File Offset: 0x0004AA28
		internal override IValueConverter Converter
		{
			get
			{
				return CssBreakInsideProperty.StyleConverter;
			}
		}

		// Token: 0x04000C2D RID: 3117
		private static readonly IValueConverter StyleConverter = Converters.BreakInsideModeConverter.OrDefault(BreakMode.Auto);
	}
}
