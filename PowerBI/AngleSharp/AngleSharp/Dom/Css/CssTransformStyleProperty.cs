using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002EC RID: 748
	internal sealed class CssTransformStyleProperty : CssProperty
	{
		// Token: 0x060015CA RID: 5578 RVA: 0x0004DB3F File Offset: 0x0004BD3F
		internal CssTransformStyleProperty()
			: base(PropertyNames.TransformStyle, PropertyFlags.None)
		{
		}

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x060015CB RID: 5579 RVA: 0x0004DB4D File Offset: 0x0004BD4D
		internal override IValueConverter Converter
		{
			get
			{
				return CssTransformStyleProperty.StyleConverter;
			}
		}

		// Token: 0x04000C7F RID: 3199
		private static readonly IValueConverter StyleConverter = Converters.Toggle(Keywords.Flat, Keywords.Preserve3d).OrDefault(true);
	}
}
