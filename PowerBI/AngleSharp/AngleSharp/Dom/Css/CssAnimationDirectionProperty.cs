using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000258 RID: 600
	internal sealed class CssAnimationDirectionProperty : CssProperty
	{
		// Token: 0x0600140F RID: 5135 RVA: 0x0004B54E File Offset: 0x0004974E
		internal CssAnimationDirectionProperty()
			: base(PropertyNames.AnimationDirection, PropertyFlags.None)
		{
		}

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x06001410 RID: 5136 RVA: 0x0004B55C File Offset: 0x0004975C
		internal override IValueConverter Converter
		{
			get
			{
				return CssAnimationDirectionProperty.ListConverter;
			}
		}

		// Token: 0x04000BE7 RID: 3047
		private static readonly IValueConverter ListConverter = Converters.AnimationDirectionConverter.FromList().OrDefault(AnimationDirection.Normal);
	}
}
