using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000261 RID: 609
	internal sealed class CssBackgroundClipProperty : CssProperty
	{
		// Token: 0x0600142A RID: 5162 RVA: 0x0004B805 File Offset: 0x00049A05
		internal CssBackgroundClipProperty()
			: base(PropertyNames.BackgroundClip, PropertyFlags.None)
		{
		}

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x0600142B RID: 5163 RVA: 0x0004B813 File Offset: 0x00049A13
		internal override IValueConverter Converter
		{
			get
			{
				return CssBackgroundClipProperty.ListConverter;
			}
		}

		// Token: 0x04000BF0 RID: 3056
		private static readonly IValueConverter ListConverter = Converters.BoxModelConverter.FromList().OrDefault(BoxModel.BorderBox);
	}
}
