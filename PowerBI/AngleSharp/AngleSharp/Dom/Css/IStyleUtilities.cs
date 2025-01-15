using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000338 RID: 824
	[DomName("GetStyleUtils")]
	[DomNoInterfaceObject]
	public interface IStyleUtilities
	{
		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x0600191A RID: 6426
		[DomName("cascadedStyle")]
		ICssStyleDeclaration CascadedStyle { get; }

		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x0600191B RID: 6427
		[DomName("defaultStyle")]
		ICssStyleDeclaration DefaultStyle { get; }

		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x0600191C RID: 6428
		[DomName("rawComputedStyle")]
		ICssStyleDeclaration RawComputedStyle { get; }

		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x0600191D RID: 6429
		[DomName("usedStyle")]
		ICssStyleDeclaration UsedStyle { get; }
	}
}
