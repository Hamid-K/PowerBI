using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000260 RID: 608
	internal sealed class CssBackgroundAttachmentProperty : CssProperty
	{
		// Token: 0x06001427 RID: 5159 RVA: 0x0004B7D9 File Offset: 0x000499D9
		internal CssBackgroundAttachmentProperty()
			: base(PropertyNames.BackgroundAttachment, PropertyFlags.None)
		{
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x06001428 RID: 5160 RVA: 0x0004B7E7 File Offset: 0x000499E7
		internal override IValueConverter Converter
		{
			get
			{
				return CssBackgroundAttachmentProperty.AttachmentConverter;
			}
		}

		// Token: 0x04000BEF RID: 3055
		private static readonly IValueConverter AttachmentConverter = Converters.BackgroundAttachmentConverter.FromList().OrDefault(BackgroundAttachment.Scroll);
	}
}
