using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom
{
	// Token: 0x02000191 RID: 401
	[DomName("Node")]
	public interface INode : IEventTarget, IMarkupFormattable
	{
		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000E5E RID: 3678
		[DomName("baseURI")]
		string BaseUri { get; }

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000E5F RID: 3679
		Url BaseUrl { get; }

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06000E60 RID: 3680
		[DomName("nodeName")]
		string NodeName { get; }

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06000E61 RID: 3681
		[DomName("childNodes")]
		INodeList ChildNodes { get; }

		// Token: 0x06000E62 RID: 3682
		[DomName("cloneNode")]
		INode Clone(bool deep = true);

		// Token: 0x06000E63 RID: 3683
		[DomName("isEqualNode")]
		bool Equals(INode otherNode);

		// Token: 0x06000E64 RID: 3684
		[DomName("compareDocumentPosition")]
		DocumentPositions CompareDocumentPosition(INode otherNode);

		// Token: 0x06000E65 RID: 3685
		[DomName("normalize")]
		void Normalize();

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000E66 RID: 3686
		[DomName("ownerDocument")]
		IDocument Owner { get; }

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000E67 RID: 3687
		[DomName("parentElement")]
		IElement ParentElement { get; }

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000E68 RID: 3688
		[DomName("parentNode")]
		INode Parent { get; }

		// Token: 0x06000E69 RID: 3689
		[DomName("contains")]
		bool Contains(INode otherNode);

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000E6A RID: 3690
		[DomName("firstChild")]
		INode FirstChild { get; }

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000E6B RID: 3691
		[DomName("lastChild")]
		INode LastChild { get; }

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000E6C RID: 3692
		[DomName("nextSibling")]
		INode NextSibling { get; }

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000E6D RID: 3693
		[DomName("previousSibling")]
		INode PreviousSibling { get; }

		// Token: 0x06000E6E RID: 3694
		[DomName("isDefaultNamespace")]
		bool IsDefaultNamespace(string namespaceUri);

		// Token: 0x06000E6F RID: 3695
		[DomName("lookupNamespaceURI")]
		string LookupNamespaceUri(string prefix);

		// Token: 0x06000E70 RID: 3696
		[DomName("lookupPrefix")]
		string LookupPrefix(string namespaceUri);

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000E71 RID: 3697
		[DomName("nodeType")]
		NodeType NodeType { get; }

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06000E72 RID: 3698
		// (set) Token: 0x06000E73 RID: 3699
		[DomName("nodeValue")]
		string NodeValue { get; set; }

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06000E74 RID: 3700
		// (set) Token: 0x06000E75 RID: 3701
		[DomName("textContent")]
		string TextContent { get; set; }

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000E76 RID: 3702
		[DomName("hasChildNodes")]
		bool HasChildNodes { get; }

		// Token: 0x06000E77 RID: 3703
		[DomName("appendChild")]
		INode AppendChild(INode child);

		// Token: 0x06000E78 RID: 3704
		[DomName("insertBefore")]
		INode InsertBefore(INode newElement, INode referenceElement);

		// Token: 0x06000E79 RID: 3705
		[DomName("removeChild")]
		INode RemoveChild(INode child);

		// Token: 0x06000E7A RID: 3706
		[DomName("replaceChild")]
		INode ReplaceChild(INode newChild, INode oldChild);
	}
}
