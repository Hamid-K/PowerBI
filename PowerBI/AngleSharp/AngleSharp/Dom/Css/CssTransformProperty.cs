using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002EB RID: 747
	internal sealed class CssTransformProperty : CssProperty
	{
		// Token: 0x060015C7 RID: 5575 RVA: 0x0004DB09 File Offset: 0x0004BD09
		internal CssTransformProperty()
			: base(PropertyNames.Transform, PropertyFlags.Animatable)
		{
		}

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x060015C8 RID: 5576 RVA: 0x0004DB17 File Offset: 0x0004BD17
		internal override IValueConverter Converter
		{
			get
			{
				return CssTransformProperty.StyleConverter;
			}
		}

		// Token: 0x04000C7E RID: 3198
		private static readonly IValueConverter StyleConverter = Converters.TransformConverter.Many(1, 65535).OrNone().OrDefault();
	}
}
