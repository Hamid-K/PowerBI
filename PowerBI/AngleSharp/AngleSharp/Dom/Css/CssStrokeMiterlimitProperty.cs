using System;
using AngleSharp.Css;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002D4 RID: 724
	internal sealed class CssStrokeMiterlimitProperty : CssProperty
	{
		// Token: 0x06001582 RID: 5506 RVA: 0x0004D4A9 File Offset: 0x0004B6A9
		public CssStrokeMiterlimitProperty()
			: base(PropertyNames.StrokeMiterlimit, PropertyFlags.Animatable)
		{
		}

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x06001583 RID: 5507 RVA: 0x0004D4B7 File Offset: 0x0004B6B7
		internal override IValueConverter Converter
		{
			get
			{
				return CssStrokeMiterlimitProperty.StyleConverter;
			}
		}

		// Token: 0x04000C67 RID: 3175
		private static readonly IValueConverter StyleConverter = Converters.StrokeMiterlimitConverter;
	}
}
