using System;
using AngleSharp.Css;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002C1 RID: 705
	internal sealed class CssSrcProperty : CssProperty
	{
		// Token: 0x0600154B RID: 5451 RVA: 0x0004D0CF File Offset: 0x0004B2CF
		public CssSrcProperty()
			: base(PropertyNames.Src, PropertyFlags.None)
		{
		}

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x0600154C RID: 5452 RVA: 0x0004B13B File Offset: 0x0004933B
		internal override IValueConverter Converter
		{
			get
			{
				return Converters.Any;
			}
		}
	}
}
