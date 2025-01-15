using System;
using AngleSharp.Css;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000255 RID: 597
	internal sealed class CssUnknownProperty : CssProperty
	{
		// Token: 0x06001407 RID: 5127 RVA: 0x0004B4ED File Offset: 0x000496ED
		internal CssUnknownProperty(string name)
			: base(name, PropertyFlags.None)
		{
		}

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x06001408 RID: 5128 RVA: 0x0004B13B File Offset: 0x0004933B
		internal override IValueConverter Converter
		{
			get
			{
				return Converters.Any;
			}
		}
	}
}
