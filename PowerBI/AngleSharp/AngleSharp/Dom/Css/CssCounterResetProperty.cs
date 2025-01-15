using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002C5 RID: 709
	internal sealed class CssCounterResetProperty : CssProperty
	{
		// Token: 0x06001555 RID: 5461 RVA: 0x0004D15E File Offset: 0x0004B35E
		internal CssCounterResetProperty()
			: base(PropertyNames.CounterReset, PropertyFlags.None)
		{
		}

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x06001556 RID: 5462 RVA: 0x0004D16C File Offset: 0x0004B36C
		internal override IValueConverter Converter
		{
			get
			{
				return CssCounterResetProperty.StyleConverter;
			}
		}

		// Token: 0x04000C58 RID: 3160
		private static readonly IValueConverter StyleConverter = Converters.Continuous(Converters.WithOrder(new IValueConverter[]
		{
			Converters.IdentifierConverter.Required(),
			Converters.IntegerConverter.Option(0)
		})).OrDefault();
	}
}
