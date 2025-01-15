using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002E0 RID: 736
	internal sealed class CssTextDecorationStyleProperty : CssProperty
	{
		// Token: 0x060015A6 RID: 5542 RVA: 0x0004D6D2 File Offset: 0x0004B8D2
		internal CssTextDecorationStyleProperty()
			: base(PropertyNames.TextDecorationStyle, PropertyFlags.None)
		{
		}

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x060015A7 RID: 5543 RVA: 0x0004D6E0 File Offset: 0x0004B8E0
		internal override IValueConverter Converter
		{
			get
			{
				return CssTextDecorationStyleProperty.StyleConverter;
			}
		}

		// Token: 0x04000C73 RID: 3187
		private static readonly IValueConverter StyleConverter = Converters.TextDecorationStyleConverter.OrDefault(TextDecorationStyle.Solid);
	}
}
