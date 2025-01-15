using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002F3 RID: 755
	internal sealed class CssClipProperty : CssProperty
	{
		// Token: 0x060015DF RID: 5599 RVA: 0x0004DD25 File Offset: 0x0004BF25
		internal CssClipProperty()
			: base(PropertyNames.Clip, PropertyFlags.Animatable)
		{
		}

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x060015E0 RID: 5600 RVA: 0x0004DD33 File Offset: 0x0004BF33
		internal override IValueConverter Converter
		{
			get
			{
				return CssClipProperty.StyleConverter;
			}
		}

		// Token: 0x04000C86 RID: 3206
		private static readonly IValueConverter StyleConverter = Converters.ShapeConverter.OrDefault();
	}
}
