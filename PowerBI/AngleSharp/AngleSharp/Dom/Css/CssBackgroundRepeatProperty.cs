using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000267 RID: 615
	internal sealed class CssBackgroundRepeatProperty : CssProperty
	{
		// Token: 0x0600143C RID: 5180 RVA: 0x0004BB3C File Offset: 0x00049D3C
		internal CssBackgroundRepeatProperty()
			: base(PropertyNames.BackgroundRepeat, PropertyFlags.None)
		{
		}

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x0600143D RID: 5181 RVA: 0x0004BB4A File Offset: 0x00049D4A
		internal override IValueConverter Converter
		{
			get
			{
				return CssBackgroundRepeatProperty.ListConverter;
			}
		}

		// Token: 0x04000BF8 RID: 3064
		private static readonly IValueConverter ListConverter = Converters.BackgroundRepeatsConverter.FromList().OrDefault(BackgroundRepeat.Repeat);
	}
}
