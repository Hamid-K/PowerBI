using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x0200025C RID: 604
	internal sealed class CssAnimationNameProperty : CssProperty
	{
		// Token: 0x0600141B RID: 5147 RVA: 0x0004B606 File Offset: 0x00049806
		internal CssAnimationNameProperty()
			: base(PropertyNames.AnimationName, PropertyFlags.None)
		{
		}

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x0600141C RID: 5148 RVA: 0x0004B614 File Offset: 0x00049814
		internal override IValueConverter Converter
		{
			get
			{
				return CssAnimationNameProperty.ListConverter;
			}
		}

		// Token: 0x04000BEB RID: 3051
		private static readonly IValueConverter ListConverter = Converters.IdentifierConverter.FromList().OrNone().OrDefault();
	}
}
