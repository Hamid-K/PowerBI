using System;
using AngleSharp.Attributes;
using AngleSharp.Dom.Css;

namespace AngleSharp.Dom
{
	// Token: 0x02000185 RID: 389
	[DomName("Element")]
	public interface IElement : INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle
	{
		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000E10 RID: 3600
		[DomName("prefix")]
		string Prefix { get; }

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000E11 RID: 3601
		[DomName("localName")]
		string LocalName { get; }

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000E12 RID: 3602
		[DomName("namespaceURI")]
		string NamespaceUri { get; }

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000E13 RID: 3603
		[DomName("attributes")]
		INamedNodeMap Attributes { get; }

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000E14 RID: 3604
		[DomName("classList")]
		ITokenList ClassList { get; }

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000E15 RID: 3605
		// (set) Token: 0x06000E16 RID: 3606
		[DomName("className")]
		string ClassName { get; set; }

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000E17 RID: 3607
		// (set) Token: 0x06000E18 RID: 3608
		[DomName("id")]
		string Id { get; set; }

		// Token: 0x06000E19 RID: 3609
		[DomName("insertAdjacentHTML")]
		void Insert(AdjacentPosition position, string html);

		// Token: 0x06000E1A RID: 3610
		[DomName("hasAttribute")]
		bool HasAttribute(string name);

		// Token: 0x06000E1B RID: 3611
		[DomName("hasAttributeNS")]
		bool HasAttribute(string namespaceUri, string localName);

		// Token: 0x06000E1C RID: 3612
		[DomName("getAttribute")]
		string GetAttribute(string name);

		// Token: 0x06000E1D RID: 3613
		[DomName("getAttributeNS")]
		string GetAttribute(string namespaceUri, string localName);

		// Token: 0x06000E1E RID: 3614
		[DomName("setAttribute")]
		void SetAttribute(string name, string value);

		// Token: 0x06000E1F RID: 3615
		[DomName("setAttributeNS")]
		void SetAttribute(string namespaceUri, string name, string value);

		// Token: 0x06000E20 RID: 3616
		[DomName("removeAttribute")]
		bool RemoveAttribute(string name);

		// Token: 0x06000E21 RID: 3617
		[DomName("removeAttributeNS")]
		bool RemoveAttribute(string namespaceUri, string localName);

		// Token: 0x06000E22 RID: 3618
		[DomName("getElementsByClassName")]
		IHtmlCollection<IElement> GetElementsByClassName(string classNames);

		// Token: 0x06000E23 RID: 3619
		[DomName("getElementsByTagName")]
		IHtmlCollection<IElement> GetElementsByTagName(string tagName);

		// Token: 0x06000E24 RID: 3620
		[DomName("getElementsByTagNameNS")]
		IHtmlCollection<IElement> GetElementsByTagNameNS(string namespaceUri, string tagName);

		// Token: 0x06000E25 RID: 3621
		[DomName("matches")]
		bool Matches(string selectors);

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000E26 RID: 3622
		// (set) Token: 0x06000E27 RID: 3623
		[DomName("innerHTML")]
		string InnerHtml { get; set; }

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000E28 RID: 3624
		// (set) Token: 0x06000E29 RID: 3625
		[DomName("outerHTML")]
		string OuterHtml { get; set; }

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000E2A RID: 3626
		[DomName("tagName")]
		string TagName { get; }

		// Token: 0x06000E2B RID: 3627
		[DomName("pseudo")]
		IPseudoElement Pseudo(string pseudoElement);

		// Token: 0x06000E2C RID: 3628
		[DomName("attachShadow")]
		[DomInitDict(0, false)]
		IShadowRoot AttachShadow(ShadowRootMode mode = ShadowRootMode.Open);

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000E2D RID: 3629
		[DomName("assignedSlot")]
		IElement AssignedSlot { get; }

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000E2E RID: 3630
		// (set) Token: 0x06000E2F RID: 3631
		[DomName("slot")]
		string Slot { get; set; }

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000E30 RID: 3632
		[DomName("shadowRoot")]
		IShadowRoot ShadowRoot { get; }

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000E31 RID: 3633
		bool IsFocused { get; }
	}
}
