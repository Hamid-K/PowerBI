using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom
{
	// Token: 0x02000194 RID: 404
	[DomName("NonDocumentTypeChildNode")]
	[DomNoInterfaceObject]
	public interface INonDocumentTypeChildNode
	{
		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000E84 RID: 3716
		[DomName("nextElementSibling")]
		IElement NextElementSibling { get; }

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000E85 RID: 3717
		[DomName("previousElementSibling")]
		IElement PreviousElementSibling { get; }
	}
}
