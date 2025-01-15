using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002C4 RID: 708
	internal sealed class CssCounterIncrementProperty : CssProperty
	{
		// Token: 0x06001552 RID: 5458 RVA: 0x0004D112 File Offset: 0x0004B312
		internal CssCounterIncrementProperty()
			: base(PropertyNames.CounterIncrement, PropertyFlags.None)
		{
		}

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x06001553 RID: 5459 RVA: 0x0004D120 File Offset: 0x0004B320
		internal override IValueConverter Converter
		{
			get
			{
				return CssCounterIncrementProperty.StyleConverter;
			}
		}

		// Token: 0x04000C57 RID: 3159
		private static readonly IValueConverter StyleConverter = Converters.Continuous(Converters.WithOrder(new IValueConverter[]
		{
			Converters.IdentifierConverter.Required(),
			Converters.IntegerConverter.Option(1)
		})).OrDefault();
	}
}
