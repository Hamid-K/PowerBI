using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000263 RID: 611
	internal sealed class CssBackgroundImageProperty : CssProperty
	{
		// Token: 0x06001430 RID: 5168 RVA: 0x0004B858 File Offset: 0x00049A58
		internal CssBackgroundImageProperty()
			: base(PropertyNames.BackgroundImage, PropertyFlags.None)
		{
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x06001431 RID: 5169 RVA: 0x0004B866 File Offset: 0x00049A66
		internal override IValueConverter Converter
		{
			get
			{
				return CssBackgroundImageProperty.StyleConverter;
			}
		}

		// Token: 0x04000BF2 RID: 3058
		private static readonly IValueConverter StyleConverter = Converters.MultipleImageSourceConverter.OrDefault();
	}
}
