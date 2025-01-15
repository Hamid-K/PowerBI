using System;
using AngleSharp.Css;
using AngleSharp.Css.Values;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002C0 RID: 704
	internal sealed class CssLineHeightProperty : CssProperty
	{
		// Token: 0x06001548 RID: 5448 RVA: 0x0004D09C File Offset: 0x0004B29C
		internal CssLineHeightProperty()
			: base(PropertyNames.LineHeight, PropertyFlags.Inherited | PropertyFlags.Animatable)
		{
		}

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x06001549 RID: 5449 RVA: 0x0004D0AB File Offset: 0x0004B2AB
		internal override IValueConverter Converter
		{
			get
			{
				return CssLineHeightProperty.StyleConverter;
			}
		}

		// Token: 0x04000C55 RID: 3157
		private static readonly IValueConverter StyleConverter = Converters.LineHeightConverter.OrDefault(new Length(120f, Length.Unit.Percent));
	}
}
