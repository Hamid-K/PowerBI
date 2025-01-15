using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000262 RID: 610
	internal sealed class CssBackgroundColorProperty : CssProperty
	{
		// Token: 0x0600142D RID: 5165 RVA: 0x0004B831 File Offset: 0x00049A31
		internal CssBackgroundColorProperty()
			: base(PropertyNames.BackgroundColor, PropertyFlags.Hashless | PropertyFlags.Animatable)
		{
		}

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x0600142E RID: 5166 RVA: 0x0004B840 File Offset: 0x00049A40
		internal override IValueConverter Converter
		{
			get
			{
				return CssBackgroundColorProperty.StyleConverter;
			}
		}

		// Token: 0x04000BF1 RID: 3057
		private static readonly IValueConverter StyleConverter = Converters.CurrentColorConverter.OrDefault();
	}
}
