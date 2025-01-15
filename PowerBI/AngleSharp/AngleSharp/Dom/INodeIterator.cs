using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom
{
	// Token: 0x02000192 RID: 402
	[DomName("NodeIterator")]
	public interface INodeIterator
	{
		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000E7B RID: 3707
		[DomName("root")]
		INode Root { get; }

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000E7C RID: 3708
		[DomName("referenceNode")]
		INode Reference { get; }

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000E7D RID: 3709
		[DomName("pointerBeforeReferenceNode")]
		bool IsBeforeReference { get; }

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000E7E RID: 3710
		[DomName("whatToShow")]
		FilterSettings Settings { get; }

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000E7F RID: 3711
		[DomName("filter")]
		NodeFilter Filter { get; }

		// Token: 0x06000E80 RID: 3712
		[DomName("nextNode")]
		INode Next();

		// Token: 0x06000E81 RID: 3713
		[DomName("previousNode")]
		INode Previous();
	}
}
