using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002B4 RID: 692
	internal sealed class CssPositionProperty : CssProperty
	{
		// Token: 0x06001523 RID: 5411 RVA: 0x0004CD6B File Offset: 0x0004AF6B
		internal CssPositionProperty()
			: base(PropertyNames.Position, PropertyFlags.None)
		{
		}

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x06001524 RID: 5412 RVA: 0x0004CD79 File Offset: 0x0004AF79
		internal override IValueConverter Converter
		{
			get
			{
				return CssPositionProperty.StyleConverter;
			}
		}

		// Token: 0x04000C49 RID: 3145
		private static readonly IValueConverter StyleConverter = Converters.PositionModeConverter.OrDefault(PositionMode.Static);
	}
}
