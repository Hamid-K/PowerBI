using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom
{
	// Token: 0x0200018F RID: 399
	[DomName("MutationRecord")]
	public interface IMutationRecord
	{
		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000E4C RID: 3660
		[DomName("type")]
		string Type { get; }

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000E4D RID: 3661
		[DomName("target")]
		INode Target { get; }

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000E4E RID: 3662
		[DomName("addedNodes")]
		INodeList Added { get; }

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000E4F RID: 3663
		[DomName("removedNodes")]
		INodeList Removed { get; }

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000E50 RID: 3664
		[DomName("previousSibling")]
		INode PreviousSibling { get; }

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000E51 RID: 3665
		[DomName("nextSibling")]
		INode NextSibling { get; }

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000E52 RID: 3666
		[DomName("attributeName")]
		string AttributeName { get; }

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000E53 RID: 3667
		[DomName("attributeNamespace")]
		string AttributeNamespace { get; }

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000E54 RID: 3668
		[DomName("oldValue")]
		string PreviousValue { get; }
	}
}
