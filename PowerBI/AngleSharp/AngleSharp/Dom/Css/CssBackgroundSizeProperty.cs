using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000268 RID: 616
	internal sealed class CssBackgroundSizeProperty : CssProperty
	{
		// Token: 0x0600143F RID: 5183 RVA: 0x0004BB68 File Offset: 0x00049D68
		internal CssBackgroundSizeProperty()
			: base(PropertyNames.BackgroundSize, PropertyFlags.Animatable)
		{
		}

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x06001440 RID: 5184 RVA: 0x0004BB76 File Offset: 0x00049D76
		internal override IValueConverter Converter
		{
			get
			{
				return CssBackgroundSizeProperty.ListConverter;
			}
		}

		// Token: 0x04000BF9 RID: 3065
		private static readonly IValueConverter ListConverter = Converters.BackgroundSizeConverter.FromList().OrDefault();
	}
}
