using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom
{
	// Token: 0x0200018C RID: 396
	[DomName("LinkStyle")]
	[DomNoInterfaceObject]
	public interface ILinkStyle
	{
		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000E47 RID: 3655
		[DomName("sheet")]
		IStyleSheet Sheet { get; }
	}
}
