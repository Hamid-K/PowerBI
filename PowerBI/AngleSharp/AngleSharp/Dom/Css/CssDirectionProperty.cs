using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002D8 RID: 728
	internal sealed class CssDirectionProperty : CssProperty
	{
		// Token: 0x0600158E RID: 5518 RVA: 0x0004D537 File Offset: 0x0004B737
		internal CssDirectionProperty()
			: base(PropertyNames.Direction, PropertyFlags.Inherited)
		{
		}

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x0600158F RID: 5519 RVA: 0x0004D545 File Offset: 0x0004B745
		internal override IValueConverter Converter
		{
			get
			{
				return CssDirectionProperty.StyleConverter;
			}
		}

		// Token: 0x04000C6B RID: 3179
		private static readonly IValueConverter StyleConverter = Converters.DirectionModeConverter.OrDefault(DirectionMode.Ltr);
	}
}
