using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom
{
	// Token: 0x02000199 RID: 409
	[DomName("Range")]
	public interface IRange
	{
		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000E90 RID: 3728
		[DomName("startContainer")]
		INode Head { get; }

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000E91 RID: 3729
		[DomName("startOffset")]
		int Start { get; }

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000E92 RID: 3730
		[DomName("endContainer")]
		INode Tail { get; }

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06000E93 RID: 3731
		[DomName("endOffset")]
		int End { get; }

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06000E94 RID: 3732
		[DomName("collapsed")]
		bool IsCollapsed { get; }

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06000E95 RID: 3733
		[DomName("commonAncestorContainer")]
		INode CommonAncestor { get; }

		// Token: 0x06000E96 RID: 3734
		[DomName("setStart")]
		void StartWith(INode refNode, int offset);

		// Token: 0x06000E97 RID: 3735
		[DomName("setEnd")]
		void EndWith(INode refNode, int offset);

		// Token: 0x06000E98 RID: 3736
		[DomName("setStartBefore")]
		void StartBefore(INode refNode);

		// Token: 0x06000E99 RID: 3737
		[DomName("setEndBefore")]
		void EndBefore(INode refNode);

		// Token: 0x06000E9A RID: 3738
		[DomName("setStartAfter")]
		void StartAfter(INode refNode);

		// Token: 0x06000E9B RID: 3739
		[DomName("setEndAfter")]
		void EndAfter(INode refNode);

		// Token: 0x06000E9C RID: 3740
		[DomName("collapse")]
		void Collapse(bool toStart);

		// Token: 0x06000E9D RID: 3741
		[DomName("selectNode")]
		void Select(INode refNode);

		// Token: 0x06000E9E RID: 3742
		[DomName("selectNodeContents")]
		void SelectContent(INode refNode);

		// Token: 0x06000E9F RID: 3743
		[DomName("deleteContents")]
		void ClearContent();

		// Token: 0x06000EA0 RID: 3744
		[DomName("extractContents")]
		IDocumentFragment ExtractContent();

		// Token: 0x06000EA1 RID: 3745
		[DomName("cloneContents")]
		IDocumentFragment CopyContent();

		// Token: 0x06000EA2 RID: 3746
		[DomName("insertNode")]
		void Insert(INode node);

		// Token: 0x06000EA3 RID: 3747
		[DomName("surroundContents")]
		void Surround(INode newParent);

		// Token: 0x06000EA4 RID: 3748
		[DomName("cloneRange")]
		IRange Clone();

		// Token: 0x06000EA5 RID: 3749
		[DomName("detach")]
		void Detach();

		// Token: 0x06000EA6 RID: 3750
		[DomName("isPointInRange")]
		bool Contains(INode node, int offset);

		// Token: 0x06000EA7 RID: 3751
		[DomName("compareBoundaryPoints")]
		RangePosition CompareBoundaryTo(RangeType how, IRange sourceRange);

		// Token: 0x06000EA8 RID: 3752
		[DomName("comparePoint")]
		RangePosition CompareTo(INode node, int offset);

		// Token: 0x06000EA9 RID: 3753
		[DomName("intersectsNode")]
		bool Intersects(INode node);
	}
}
