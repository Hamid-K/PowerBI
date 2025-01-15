using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000264 RID: 612
	internal sealed class CssBackgroundOriginProperty : CssProperty
	{
		// Token: 0x06001433 RID: 5171 RVA: 0x0004B87E File Offset: 0x00049A7E
		internal CssBackgroundOriginProperty()
			: base(PropertyNames.BackgroundOrigin, PropertyFlags.None)
		{
		}

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x06001434 RID: 5172 RVA: 0x0004B88C File Offset: 0x00049A8C
		internal override IValueConverter Converter
		{
			get
			{
				return CssBackgroundOriginProperty.ListConverter;
			}
		}

		// Token: 0x04000BF3 RID: 3059
		private static readonly IValueConverter ListConverter = Converters.BoxModelConverter.FromList().OrDefault(BoxModel.PaddingBox);
	}
}
