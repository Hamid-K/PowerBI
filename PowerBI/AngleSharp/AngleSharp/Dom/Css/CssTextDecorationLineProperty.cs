using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002DE RID: 734
	internal sealed class CssTextDecorationLineProperty : CssProperty
	{
		// Token: 0x060015A0 RID: 5536 RVA: 0x0004D613 File Offset: 0x0004B813
		internal CssTextDecorationLineProperty()
			: base(PropertyNames.TextDecorationLine, PropertyFlags.None)
		{
		}

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x060015A1 RID: 5537 RVA: 0x0004D621 File Offset: 0x0004B821
		internal override IValueConverter Converter
		{
			get
			{
				return CssTextDecorationLineProperty.ListConverter;
			}
		}

		// Token: 0x04000C71 RID: 3185
		private static readonly IValueConverter ListConverter = Converters.TextDecorationLinesConverter.OrDefault();
	}
}
