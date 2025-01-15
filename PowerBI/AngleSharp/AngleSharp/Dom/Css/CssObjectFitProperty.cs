using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002CE RID: 718
	internal sealed class CssObjectFitProperty : CssProperty
	{
		// Token: 0x06001570 RID: 5488 RVA: 0x0004D3CC File Offset: 0x0004B5CC
		internal CssObjectFitProperty()
			: base(PropertyNames.ObjectFit, PropertyFlags.None)
		{
		}

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x06001571 RID: 5489 RVA: 0x0004D3DA File Offset: 0x0004B5DA
		internal override IValueConverter Converter
		{
			get
			{
				return CssObjectFitProperty.StyleConverter;
			}
		}

		// Token: 0x04000C61 RID: 3169
		private static readonly IValueConverter StyleConverter = Converters.ObjectFittingConverter.OrDefault(ObjectFitting.Fill);
	}
}
