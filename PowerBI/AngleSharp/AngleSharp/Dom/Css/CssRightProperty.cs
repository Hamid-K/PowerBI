using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002AD RID: 685
	internal sealed class CssRightProperty : CssProperty
	{
		// Token: 0x0600150E RID: 5390 RVA: 0x0004CC4B File Offset: 0x0004AE4B
		internal CssRightProperty()
			: base(PropertyNames.Right, PropertyFlags.Unitless | PropertyFlags.Animatable)
		{
		}

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x0600150F RID: 5391 RVA: 0x0004CC5A File Offset: 0x0004AE5A
		internal override IValueConverter Converter
		{
			get
			{
				return CssRightProperty.StyleConverter;
			}
		}

		// Token: 0x04000C42 RID: 3138
		private static readonly IValueConverter StyleConverter = Converters.AutoLengthOrPercentConverter.OrDefault(Keywords.Auto);
	}
}
