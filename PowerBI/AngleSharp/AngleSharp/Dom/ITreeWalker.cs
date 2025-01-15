using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom
{
	// Token: 0x020001A2 RID: 418
	[DomName("TreeWalker")]
	public interface ITreeWalker
	{
		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000EC9 RID: 3785
		[DomName("root")]
		INode Root { get; }

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000ECA RID: 3786
		// (set) Token: 0x06000ECB RID: 3787
		[DomName("currentNode")]
		INode Current { get; set; }

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000ECC RID: 3788
		[DomName("whatToShow")]
		FilterSettings Settings { get; }

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000ECD RID: 3789
		[DomName("filter")]
		NodeFilter Filter { get; }

		// Token: 0x06000ECE RID: 3790
		[DomName("nextNode")]
		INode ToNext();

		// Token: 0x06000ECF RID: 3791
		[DomName("previousNode")]
		INode ToPrevious();

		// Token: 0x06000ED0 RID: 3792
		[DomName("parentNode")]
		INode ToParent();

		// Token: 0x06000ED1 RID: 3793
		[DomName("firstChild")]
		INode ToFirst();

		// Token: 0x06000ED2 RID: 3794
		[DomName("lastChild")]
		INode ToLast();

		// Token: 0x06000ED3 RID: 3795
		[DomName("previousSibling")]
		INode ToPreviousSibling();

		// Token: 0x06000ED4 RID: 3796
		[DomName("nextSibling")]
		INode ToNextSibling();
	}
}
