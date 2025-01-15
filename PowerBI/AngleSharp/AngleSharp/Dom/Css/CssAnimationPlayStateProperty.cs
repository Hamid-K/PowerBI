using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x0200025D RID: 605
	internal sealed class CssAnimationPlayStateProperty : CssProperty
	{
		// Token: 0x0600141E RID: 5150 RVA: 0x0004B636 File Offset: 0x00049836
		internal CssAnimationPlayStateProperty()
			: base(PropertyNames.AnimationPlayState, PropertyFlags.None)
		{
		}

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x0600141F RID: 5151 RVA: 0x0004B644 File Offset: 0x00049844
		internal override IValueConverter Converter
		{
			get
			{
				return CssAnimationPlayStateProperty.ListConverter;
			}
		}

		// Token: 0x04000BEC RID: 3052
		private static readonly IValueConverter ListConverter = Converters.PlayStateConverter.FromList().OrDefault(PlayState.Running);
	}
}
