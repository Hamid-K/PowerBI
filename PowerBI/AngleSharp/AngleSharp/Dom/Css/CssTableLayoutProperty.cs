using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000253 RID: 595
	internal sealed class CssTableLayoutProperty : CssProperty
	{
		// Token: 0x06001401 RID: 5121 RVA: 0x0004B49F File Offset: 0x0004969F
		internal CssTableLayoutProperty()
			: base(PropertyNames.TableLayout, PropertyFlags.None)
		{
		}

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x06001402 RID: 5122 RVA: 0x0004B4AD File Offset: 0x000496AD
		internal override IValueConverter Converter
		{
			get
			{
				return CssTableLayoutProperty.StyleConverter;
			}
		}

		// Token: 0x04000BE3 RID: 3043
		private static readonly IValueConverter StyleConverter = Converters.TableLayoutConverter.OrDefault(false);
	}
}
