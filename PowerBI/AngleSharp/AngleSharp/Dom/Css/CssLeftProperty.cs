using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002A8 RID: 680
	internal sealed class CssLeftProperty : CssProperty
	{
		// Token: 0x060014FF RID: 5375 RVA: 0x0004CB7D File Offset: 0x0004AD7D
		internal CssLeftProperty()
			: base(PropertyNames.Left, PropertyFlags.Unitless | PropertyFlags.Animatable)
		{
		}

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x06001500 RID: 5376 RVA: 0x0004CB8C File Offset: 0x0004AD8C
		internal override IValueConverter Converter
		{
			get
			{
				return CssLeftProperty.StyleConverter;
			}
		}

		// Token: 0x04000C3D RID: 3133
		private static readonly IValueConverter StyleConverter = Converters.AutoLengthOrPercentConverter.OrDefault(Keywords.Auto);
	}
}
