using System;
using AngleSharp.Css;
using AngleSharp.Css.Values;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000293 RID: 659
	internal sealed class CssPaddingProperty : CssShorthandProperty
	{
		// Token: 0x060014C0 RID: 5312 RVA: 0x0004C710 File Offset: 0x0004A910
		internal CssPaddingProperty()
			: base(PropertyNames.Padding, PropertyFlags.None)
		{
		}

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x060014C1 RID: 5313 RVA: 0x0004C71E File Offset: 0x0004A91E
		internal override IValueConverter Converter
		{
			get
			{
				return CssPaddingProperty.StyleConverter;
			}
		}

		// Token: 0x04000C28 RID: 3112
		private static readonly IValueConverter StyleConverter = Converters.LengthOrPercentConverter.Periodic(new string[]
		{
			PropertyNames.PaddingTop,
			PropertyNames.PaddingRight,
			PropertyNames.PaddingBottom,
			PropertyNames.PaddingLeft
		}).OrDefault(Length.Zero);
	}
}
