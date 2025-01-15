using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000271 RID: 625
	internal sealed class CssBorderLeftStyleProperty : CssProperty
	{
		// Token: 0x0600145A RID: 5210 RVA: 0x0004BDE6 File Offset: 0x00049FE6
		internal CssBorderLeftStyleProperty()
			: base(PropertyNames.BorderLeftStyle, PropertyFlags.None)
		{
		}

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x0600145B RID: 5211 RVA: 0x0004BDF4 File Offset: 0x00049FF4
		internal override IValueConverter Converter
		{
			get
			{
				return CssBorderLeftStyleProperty.StyleConverter;
			}
		}

		// Token: 0x04000C02 RID: 3074
		private static readonly IValueConverter StyleConverter = Converters.LineStyleConverter.OrDefault(LineStyle.None);
	}
}
