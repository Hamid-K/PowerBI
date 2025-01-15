using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x0200028A RID: 650
	internal sealed class CssBoxDecorationBreak : CssProperty
	{
		// Token: 0x060014A5 RID: 5285 RVA: 0x0004C559 File Offset: 0x0004A759
		internal CssBoxDecorationBreak()
			: base(PropertyNames.BoxDecorationBreak, PropertyFlags.None)
		{
		}

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x060014A6 RID: 5286 RVA: 0x0004C567 File Offset: 0x0004A767
		internal override IValueConverter Converter
		{
			get
			{
				return CssBoxDecorationBreak.StyleConverter;
			}
		}

		// Token: 0x04000C1F RID: 3103
		private static readonly IValueConverter StyleConverter = Converters.BoxDecorationConverter.OrDefault(false);
	}
}
