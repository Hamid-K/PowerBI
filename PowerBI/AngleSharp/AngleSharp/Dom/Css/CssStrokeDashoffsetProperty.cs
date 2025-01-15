using System;
using AngleSharp.Css;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002D1 RID: 721
	internal sealed class CssStrokeDashoffsetProperty : CssProperty
	{
		// Token: 0x06001579 RID: 5497 RVA: 0x0004D440 File Offset: 0x0004B640
		public CssStrokeDashoffsetProperty()
			: base(PropertyNames.StrokeDashoffset, PropertyFlags.Animatable)
		{
		}

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x0600157A RID: 5498 RVA: 0x0004D44E File Offset: 0x0004B64E
		internal override IValueConverter Converter
		{
			get
			{
				return CssStrokeDashoffsetProperty.StyleConverter;
			}
		}

		// Token: 0x04000C64 RID: 3172
		private static readonly IValueConverter StyleConverter = Converters.LengthOrPercentConverter;
	}
}
