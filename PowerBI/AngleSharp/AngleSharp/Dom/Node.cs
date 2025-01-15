using System;
using System.IO;
using System.Text;
using AngleSharp.Dom.Collections;
using AngleSharp.Extensions;
using AngleSharp.Html;

namespace AngleSharp.Dom
{
	// Token: 0x0200015C RID: 348
	internal abstract class Node : EventTarget, INode, IEventTarget, IMarkupFormattable, IEquatable<INode>
	{
		// Token: 0x06000C11 RID: 3089 RVA: 0x000447B7 File Offset: 0x000429B7
		internal Node(Document owner, string name, NodeType type = NodeType.Element, NodeFlags flags = NodeFlags.None)
		{
			this._owner = owner;
			this._name = name ?? string.Empty;
			this._type = type;
			this._children = this.CreateChildren();
			this._flags = flags;
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000C12 RID: 3090 RVA: 0x000447F1 File Offset: 0x000429F1
		public bool HasChildNodes
		{
			get
			{
				return this._children.Length != 0;
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000C13 RID: 3091 RVA: 0x00044801 File Offset: 0x00042A01
		public string BaseUri
		{
			get
			{
				Url baseUrl = this.BaseUrl;
				return ((baseUrl != null) ? baseUrl.Href : null) ?? string.Empty;
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000C14 RID: 3092 RVA: 0x00044820 File Offset: 0x00042A20
		// (set) Token: 0x06000C15 RID: 3093 RVA: 0x000448C4 File Offset: 0x00042AC4
		public Url BaseUrl
		{
			get
			{
				if (this._baseUri != null)
				{
					return this._baseUri;
				}
				if (this._parent != null)
				{
					foreach (Node node in this.Ancestors<Node>())
					{
						if (node._baseUri != null)
						{
							return node._baseUri;
						}
					}
				}
				Document document = this.Owner;
				if (document != null)
				{
					return document._baseUri ?? document.DocumentUrl;
				}
				if (this._type == NodeType.Document)
				{
					document = (Document)this;
					return document.DocumentUrl;
				}
				return null;
			}
			set
			{
				this._baseUri = value;
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000C16 RID: 3094 RVA: 0x000448CD File Offset: 0x00042ACD
		public NodeType NodeType
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000C17 RID: 3095 RVA: 0x0000C295 File Offset: 0x0000A495
		// (set) Token: 0x06000C18 RID: 3096 RVA: 0x00003C25 File Offset: 0x00001E25
		public virtual string NodeValue
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000C19 RID: 3097 RVA: 0x0000C295 File Offset: 0x0000A495
		// (set) Token: 0x06000C1A RID: 3098 RVA: 0x00003C25 File Offset: 0x00001E25
		public virtual string TextContent
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000C1B RID: 3099 RVA: 0x000448D5 File Offset: 0x00042AD5
		INode INode.PreviousSibling
		{
			get
			{
				return this.PreviousSibling;
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000C1C RID: 3100 RVA: 0x000448DD File Offset: 0x00042ADD
		INode INode.NextSibling
		{
			get
			{
				return this.NextSibling;
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000C1D RID: 3101 RVA: 0x000448E5 File Offset: 0x00042AE5
		INode INode.FirstChild
		{
			get
			{
				return this.FirstChild;
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000C1E RID: 3102 RVA: 0x000448ED File Offset: 0x00042AED
		INode INode.LastChild
		{
			get
			{
				return this.LastChild;
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000C1F RID: 3103 RVA: 0x000448F5 File Offset: 0x00042AF5
		IDocument INode.Owner
		{
			get
			{
				return this.Owner;
			}
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000C20 RID: 3104 RVA: 0x000448FD File Offset: 0x00042AFD
		INode INode.Parent
		{
			get
			{
				return this._parent;
			}
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000C21 RID: 3105 RVA: 0x00044905 File Offset: 0x00042B05
		public IElement ParentElement
		{
			get
			{
				return this._parent as IElement;
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000C22 RID: 3106 RVA: 0x00044912 File Offset: 0x00042B12
		INodeList INode.ChildNodes
		{
			get
			{
				return this._children;
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000C23 RID: 3107 RVA: 0x0004491A File Offset: 0x00042B1A
		public string NodeName
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000C24 RID: 3108 RVA: 0x00044924 File Offset: 0x00042B24
		internal Node PreviousSibling
		{
			get
			{
				if (this._parent != null)
				{
					int length = this._parent._children.Length;
					for (int i = 1; i < length; i++)
					{
						if (this._parent._children[i] == this)
						{
							return this._parent._children[i - 1];
						}
					}
				}
				return null;
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000C25 RID: 3109 RVA: 0x00044980 File Offset: 0x00042B80
		internal Node NextSibling
		{
			get
			{
				if (this._parent != null)
				{
					int num = this._parent._children.Length - 1;
					for (int i = 0; i < num; i++)
					{
						if (this._parent._children[i] == this)
						{
							return this._parent._children[i + 1];
						}
					}
				}
				return null;
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000C26 RID: 3110 RVA: 0x000449DD File Offset: 0x00042BDD
		internal Node FirstChild
		{
			get
			{
				if (this._children.Length <= 0)
				{
					return null;
				}
				return this._children[0];
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000C27 RID: 3111 RVA: 0x000449FB File Offset: 0x00042BFB
		internal Node LastChild
		{
			get
			{
				if (this._children.Length <= 0)
				{
					return null;
				}
				return this._children[this._children.Length - 1];
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000C28 RID: 3112 RVA: 0x00044A25 File Offset: 0x00042C25
		internal NodeFlags Flags
		{
			get
			{
				return this._flags;
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000C29 RID: 3113 RVA: 0x00044912 File Offset: 0x00042B12
		// (set) Token: 0x06000C2A RID: 3114 RVA: 0x00044A2D File Offset: 0x00042C2D
		internal NodeList ChildNodes
		{
			get
			{
				return this._children;
			}
			set
			{
				this._children = value;
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000C2B RID: 3115 RVA: 0x000448FD File Offset: 0x00042AFD
		// (set) Token: 0x06000C2C RID: 3116 RVA: 0x00044A36 File Offset: 0x00042C36
		internal Node Parent
		{
			get
			{
				return this._parent;
			}
			set
			{
				this._parent = value;
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000C2D RID: 3117 RVA: 0x00044A3F File Offset: 0x00042C3F
		// (set) Token: 0x06000C2E RID: 3118 RVA: 0x00044A54 File Offset: 0x00042C54
		internal Document Owner
		{
			get
			{
				if (this._type == NodeType.Document)
				{
					return null;
				}
				return this._owner;
			}
			set
			{
				foreach (Node node in this.DescendentsAndSelf<Node>())
				{
					Document owner = node.Owner;
					if (owner != value)
					{
						node._owner = value;
						if (owner != null)
						{
							this.NodeIsAdopted(owner);
						}
					}
				}
			}
		}

		// Token: 0x06000C2F RID: 3119 RVA: 0x00044AB8 File Offset: 0x00042CB8
		internal void AppendText(string s)
		{
			TextNode textNode = this.LastChild as TextNode;
			if (textNode == null)
			{
				this.AddNode(new TextNode(this.Owner, s));
				return;
			}
			textNode.Append(s);
		}

		// Token: 0x06000C30 RID: 3120 RVA: 0x00044AF0 File Offset: 0x00042CF0
		internal void InsertText(int index, string s)
		{
			if (index > 0 && index <= this._children.Length && this._children[index - 1].NodeType == NodeType.Text)
			{
				((IText)this._children[index - 1]).Append(s);
				return;
			}
			if (index >= 0 && index < this._children.Length && this._children[index].NodeType == NodeType.Text)
			{
				((IText)this._children[index]).Insert(0, s);
				return;
			}
			TextNode textNode = new TextNode(this.Owner, s);
			this.InsertNode(index, textNode);
		}

		// Token: 0x06000C31 RID: 3121 RVA: 0x00044B93 File Offset: 0x00042D93
		public virtual void ToHtml(TextWriter writer, IMarkupFormatter formatter)
		{
			writer.Write(this.TextContent);
		}

		// Token: 0x06000C32 RID: 3122 RVA: 0x00044BA1 File Offset: 0x00042DA1
		public INode AppendChild(INode child)
		{
			return this.PreInsert(child, null);
		}

		// Token: 0x06000C33 RID: 3123 RVA: 0x00044BAC File Offset: 0x00042DAC
		public INode InsertChild(int index, INode child)
		{
			Node node = ((index < this._children.Length) ? this._children[index] : null);
			return this.PreInsert(child, node);
		}

		// Token: 0x06000C34 RID: 3124 RVA: 0x00044BDF File Offset: 0x00042DDF
		public INode InsertBefore(INode newElement, INode referenceElement)
		{
			return this.PreInsert(newElement, referenceElement);
		}

		// Token: 0x06000C35 RID: 3125 RVA: 0x00044BE9 File Offset: 0x00042DE9
		public INode ReplaceChild(INode newChild, INode oldChild)
		{
			return this.ReplaceChild(newChild as Node, oldChild as Node, false);
		}

		// Token: 0x06000C36 RID: 3126 RVA: 0x00044BFE File Offset: 0x00042DFE
		public INode RemoveChild(INode child)
		{
			return this.PreRemove(child);
		}

		// Token: 0x06000C37 RID: 3127
		public abstract INode Clone(bool deep = true);

		// Token: 0x06000C38 RID: 3128 RVA: 0x00044C08 File Offset: 0x00042E08
		public DocumentPositions CompareDocumentPosition(INode otherNode)
		{
			if (this == otherNode)
			{
				return DocumentPositions.Same;
			}
			if (this.Owner != otherNode.Owner)
			{
				DocumentPositions documentPositions = ((otherNode.GetHashCode() > this.GetHashCode()) ? DocumentPositions.Following : DocumentPositions.Preceding);
				return DocumentPositions.Disconnected | DocumentPositions.ImplementationSpecific | documentPositions;
			}
			if (otherNode.IsAncestorOf(this))
			{
				return DocumentPositions.Preceding | DocumentPositions.Contains;
			}
			if (otherNode.IsDescendantOf(this))
			{
				return DocumentPositions.Following | DocumentPositions.ContainedBy;
			}
			if (otherNode.IsPreceding(this))
			{
				return DocumentPositions.Preceding;
			}
			return DocumentPositions.Following;
		}

		// Token: 0x06000C39 RID: 3129 RVA: 0x00044C65 File Offset: 0x00042E65
		public bool Contains(INode otherNode)
		{
			return this.IsInclusiveAncestorOf(otherNode);
		}

		// Token: 0x06000C3A RID: 3130 RVA: 0x00044C70 File Offset: 0x00042E70
		public void Normalize()
		{
			for (int i = 0; i < this._children.Length; i++)
			{
				TextNode text = this._children[i] as TextNode;
				if (text != null)
				{
					int length = text.Length;
					if (length == 0)
					{
						this.RemoveChild(text, false);
						i--;
					}
					else
					{
						StringBuilder stringBuilder = Pool.NewStringBuilder();
						TextNode sibling = text;
						int end = i;
						Document owner = this.Owner;
						Action<Range> <>9__1;
						Action<Range> <>9__3;
						Action<Range> <>9__5;
						Action<Range> <>9__7;
						while ((sibling = sibling.NextSibling as TextNode) != null)
						{
							stringBuilder.Append(sibling.Data);
							int end2 = end;
							end = end2 + 1;
							Document document = owner;
							Predicate<Range> predicate = (Range m) => m.Head == sibling;
							Action<Range> action;
							if ((action = <>9__1) == null)
							{
								action = (<>9__1 = delegate(Range m)
								{
									m.StartWith(text, length);
								});
							}
							document.ForEachRange(predicate, action);
							Document document2 = owner;
							Predicate<Range> predicate2 = (Range m) => m.Tail == sibling;
							Action<Range> action2;
							if ((action2 = <>9__3) == null)
							{
								action2 = (<>9__3 = delegate(Range m)
								{
									m.EndWith(text, length);
								});
							}
							document2.ForEachRange(predicate2, action2);
							Document document3 = owner;
							Predicate<Range> predicate3 = (Range m) => m.Head == sibling.Parent && m.Start == end;
							Action<Range> action3;
							if ((action3 = <>9__5) == null)
							{
								action3 = (<>9__5 = delegate(Range m)
								{
									m.StartWith(text, length);
								});
							}
							document3.ForEachRange(predicate3, action3);
							Document document4 = owner;
							Predicate<Range> predicate4 = (Range m) => m.Tail == sibling.Parent && m.End == end;
							Action<Range> action4;
							if ((action4 = <>9__7) == null)
							{
								action4 = (<>9__7 = delegate(Range m)
								{
									m.EndWith(text, length);
								});
							}
							document4.ForEachRange(predicate4, action4);
							length += sibling.Length;
						}
						text.Replace(text.Length, 0, stringBuilder.ToPool());
						for (int j = end; j > i; j--)
						{
							this.RemoveChild(this._children[j], false);
						}
					}
				}
				else if (this._children[i].HasChildNodes)
				{
					this._children[i].Normalize();
				}
			}
		}

		// Token: 0x06000C3B RID: 3131 RVA: 0x00044EDD File Offset: 0x000430DD
		public string LookupNamespaceUri(string prefix)
		{
			if (string.IsNullOrEmpty(prefix))
			{
				prefix = null;
			}
			return this.LocateNamespace(prefix);
		}

		// Token: 0x06000C3C RID: 3132 RVA: 0x00044EF1 File Offset: 0x000430F1
		public string LookupPrefix(string namespaceUri)
		{
			if (string.IsNullOrEmpty(namespaceUri))
			{
				return null;
			}
			return this.LocatePrefix(namespaceUri);
		}

		// Token: 0x06000C3D RID: 3133 RVA: 0x00044F04 File Offset: 0x00043104
		public bool IsDefaultNamespace(string namespaceUri)
		{
			if (string.IsNullOrEmpty(namespaceUri))
			{
				namespaceUri = null;
			}
			string text = this.LocateNamespace(null);
			return namespaceUri.Is(text);
		}

		// Token: 0x06000C3E RID: 3134 RVA: 0x00044F2C File Offset: 0x0004312C
		public virtual bool Equals(INode otherNode)
		{
			if (this.BaseUri.Is(otherNode.BaseUri) && this.NodeName.Is(otherNode.NodeName) && this.ChildNodes.Length == otherNode.ChildNodes.Length)
			{
				for (int i = 0; i < this._children.Length; i++)
				{
					if (!this._children[i].Equals(otherNode.ChildNodes[i]))
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000C3F RID: 3135 RVA: 0x00044FB4 File Offset: 0x000431B4
		protected static void GetPrefixAndLocalName(string qualifiedName, ref string namespaceUri, out string prefix, out string localName)
		{
			if (!qualifiedName.IsXmlName())
			{
				throw new DomException(DomError.InvalidCharacter);
			}
			if (!qualifiedName.IsQualifiedName())
			{
				throw new DomException(DomError.Namespace);
			}
			if (string.IsNullOrEmpty(namespaceUri))
			{
				namespaceUri = null;
			}
			int num = qualifiedName.IndexOf(':');
			if (num > 0)
			{
				prefix = qualifiedName.Substring(0, num);
				localName = qualifiedName.Substring(num + 1);
			}
			else
			{
				prefix = null;
				localName = qualifiedName;
			}
			if (Node.IsNamespaceError(prefix, namespaceUri, qualifiedName))
			{
				throw new DomException(DomError.Namespace);
			}
		}

		// Token: 0x06000C40 RID: 3136 RVA: 0x0004502C File Offset: 0x0004322C
		protected static bool IsNamespaceError(string prefix, string namespaceUri, string qualifiedName)
		{
			return (prefix != null && namespaceUri == null) || (prefix.Is(NamespaceNames.XmlPrefix) && !namespaceUri.Is(NamespaceNames.XmlUri)) || ((qualifiedName.Is(NamespaceNames.XmlNsPrefix) || prefix.Is(NamespaceNames.XmlNsPrefix)) && !namespaceUri.Is(NamespaceNames.XmlNsUri)) || (namespaceUri.Is(NamespaceNames.XmlNsUri) && !qualifiedName.Is(NamespaceNames.XmlNsPrefix) && !prefix.Is(NamespaceNames.XmlNsPrefix));
		}

		// Token: 0x06000C41 RID: 3137 RVA: 0x000450AE File Offset: 0x000432AE
		protected virtual string LocateNamespace(string prefix)
		{
			Node parent = this._parent;
			if (parent == null)
			{
				return null;
			}
			return parent.LocateNamespace(prefix);
		}

		// Token: 0x06000C42 RID: 3138 RVA: 0x000450C2 File Offset: 0x000432C2
		protected virtual string LocatePrefix(string namespaceUri)
		{
			Node parent = this._parent;
			if (parent == null)
			{
				return null;
			}
			return parent.LocatePrefix(namespaceUri);
		}

		// Token: 0x06000C43 RID: 3139 RVA: 0x000450D8 File Offset: 0x000432D8
		internal void ChangeOwner(Document document)
		{
			Document owner = this.Owner;
			Node parent = this._parent;
			if (parent != null)
			{
				parent.RemoveChild(this, false);
			}
			this.Owner = document;
			this.NodeIsAdopted(owner);
		}

		// Token: 0x06000C44 RID: 3140 RVA: 0x0004510D File Offset: 0x0004330D
		internal void InsertNode(int index, Node node)
		{
			node.Parent = this;
			this._children.Insert(index, node);
		}

		// Token: 0x06000C45 RID: 3141 RVA: 0x00045123 File Offset: 0x00043323
		internal void AddNode(Node node)
		{
			node.Parent = this;
			this._children.Add(node);
		}

		// Token: 0x06000C46 RID: 3142 RVA: 0x00045138 File Offset: 0x00043338
		internal void RemoveNode(int index, Node node)
		{
			node.Parent = null;
			this._children.RemoveAt(index);
		}

		// Token: 0x06000C47 RID: 3143 RVA: 0x00045150 File Offset: 0x00043350
		internal void ReplaceAll(Node node, bool suppressObservers)
		{
			Document owner = this.Owner;
			if (node != null)
			{
				owner.AdoptNode(node);
			}
			NodeList nodeList = new NodeList();
			NodeList nodeList2 = new NodeList();
			nodeList.AddRange(this._children);
			if (node != null)
			{
				if (node.NodeType == NodeType.DocumentFragment)
				{
					nodeList2.AddRange(node._children);
				}
				else
				{
					nodeList2.Add(node);
				}
			}
			for (int i = 0; i < nodeList.Length; i++)
			{
				this.RemoveChild(nodeList[i], true);
			}
			for (int j = 0; j < nodeList2.Length; j++)
			{
				this.InsertBefore(nodeList2[j], null, true);
			}
			if (!suppressObservers)
			{
				owner.QueueMutation(MutationRecord.ChildList(this, nodeList2, nodeList, null, null));
			}
		}

		// Token: 0x06000C48 RID: 3144 RVA: 0x00045200 File Offset: 0x00043400
		internal INode InsertBefore(Node newElement, Node referenceElement, bool suppressObservers)
		{
			Document owner = this.Owner;
			int count = ((newElement.NodeType == NodeType.DocumentFragment) ? newElement.ChildNodes.Length : 1);
			if (referenceElement != null && owner != null)
			{
				int childIndex = referenceElement.Index();
				owner.ForEachRange((Range m) => m.Head == this && m.Start > childIndex, delegate(Range m)
				{
					m.StartWith(this, m.Start + count);
				});
				owner.ForEachRange((Range m) => m.Tail == this && m.End > childIndex, delegate(Range m)
				{
					m.EndWith(this, m.End + count);
				});
			}
			if (newElement.NodeType == NodeType.Document || newElement.Contains(this))
			{
				throw new DomException(DomError.HierarchyRequest);
			}
			NodeList nodeList = new NodeList();
			int num = this._children.Index(referenceElement);
			if (num == -1)
			{
				num = this._children.Length;
			}
			if (newElement._type == NodeType.DocumentFragment)
			{
				int num2 = num;
				int i = num;
				while (newElement.HasChildNodes)
				{
					Node node = newElement.ChildNodes[0];
					newElement.RemoveChild(node, true);
					this.InsertNode(num2, node);
					num2++;
				}
				while (i < num2)
				{
					Node node2 = this._children[i];
					nodeList.Add(node2);
					this.NodeIsInserted(node2);
					i++;
				}
			}
			else
			{
				nodeList.Add(newElement);
				this.InsertNode(num, newElement);
				this.NodeIsInserted(newElement);
			}
			if (!suppressObservers && owner != null)
			{
				owner.QueueMutation(MutationRecord.ChildList(this, nodeList, null, (num > 0) ? this._children[num - 1] : null, referenceElement));
			}
			return newElement;
		}

		// Token: 0x06000C49 RID: 3145 RVA: 0x0004539C File Offset: 0x0004359C
		internal void RemoveChild(Node node, bool suppressObservers)
		{
			Document owner = this.Owner;
			int index = this._children.Index(node);
			if (owner != null)
			{
				owner.ForEachRange((Range m) => m.Head.IsInclusiveDescendantOf(node), delegate(Range m)
				{
					m.StartWith(this, index);
				});
				owner.ForEachRange((Range m) => m.Tail.IsInclusiveDescendantOf(node), delegate(Range m)
				{
					m.EndWith(this, index);
				});
				owner.ForEachRange((Range m) => m.Head == this && m.Start > index, delegate(Range m)
				{
					m.StartWith(this, m.Start - 1);
				});
				owner.ForEachRange((Range m) => m.Tail == this && m.End > index, delegate(Range m)
				{
					m.EndWith(this, m.End - 1);
				});
			}
			Node node2 = ((index > 0) ? this._children[index - 1] : null);
			if (!suppressObservers && owner != null)
			{
				NodeList nodeList = new NodeList { node };
				owner.QueueMutation(MutationRecord.ChildList(this, null, nodeList, node2, node.NextSibling));
				owner.AddTransientObserver(node);
			}
			this.RemoveNode(index, node);
			this.NodeIsRemoved(node, node2);
		}

		// Token: 0x06000C4A RID: 3146 RVA: 0x000454D4 File Offset: 0x000436D4
		internal INode ReplaceChild(Node node, Node child, bool suppressObservers)
		{
			if (this.IsEndPoint() || node.IsHostIncludingInclusiveAncestor(this))
			{
				throw new DomException(DomError.HierarchyRequest);
			}
			if (child.Parent != this)
			{
				throw new DomException(DomError.NotFound);
			}
			if (node.IsInsertable())
			{
				IDocument document = this._parent as IDocument;
				Node node2 = child.NextSibling;
				Document owner = this.Owner;
				NodeList nodeList = new NodeList();
				NodeList nodeList2 = new NodeList();
				if (document != null)
				{
					bool flag = false;
					NodeType type = node._type;
					if (type != NodeType.Element)
					{
						if (type != NodeType.DocumentType)
						{
							if (type == NodeType.DocumentFragment)
							{
								int elementCount = node.GetElementCount();
								flag = elementCount > 1 || node.HasTextNodes() || (elementCount == 1 && (document.DocumentElement != child || child.IsFollowedByDoctype()));
							}
						}
						else
						{
							flag = document.Doctype != child || child.IsPrecededByElement();
						}
					}
					else
					{
						flag = document.DocumentElement != child || child.IsFollowedByDoctype();
					}
					if (flag)
					{
						throw new DomException(DomError.HierarchyRequest);
					}
				}
				if (node2 == node)
				{
					node2 = node.NextSibling;
				}
				if (owner != null)
				{
					owner.AdoptNode(node);
				}
				this.RemoveChild(child, true);
				this.InsertBefore(node, node2, true);
				nodeList2.Add(child);
				if (node._type == NodeType.DocumentFragment)
				{
					nodeList.AddRange(node._children);
				}
				else
				{
					nodeList.Add(node);
				}
				if (!suppressObservers && owner != null)
				{
					owner.QueueMutation(MutationRecord.ChildList(this, nodeList, nodeList2, child.PreviousSibling, node2));
				}
				return child;
			}
			throw new DomException(DomError.HierarchyRequest);
		}

		// Token: 0x06000C4B RID: 3147 RVA: 0x00003C25 File Offset: 0x00001E25
		internal virtual void NodeIsAdopted(Document oldDocument)
		{
		}

		// Token: 0x06000C4C RID: 3148 RVA: 0x0004563C File Offset: 0x0004383C
		internal virtual void NodeIsInserted(Node newNode)
		{
			newNode.OnParentChanged();
		}

		// Token: 0x06000C4D RID: 3149 RVA: 0x0004563C File Offset: 0x0004383C
		internal virtual void NodeIsRemoved(Node removedNode, Node oldPreviousSibling)
		{
			removedNode.OnParentChanged();
		}

		// Token: 0x06000C4E RID: 3150 RVA: 0x00003C25 File Offset: 0x00001E25
		protected virtual void OnParentChanged()
		{
		}

		// Token: 0x06000C4F RID: 3151 RVA: 0x00045644 File Offset: 0x00043844
		protected void CloneNode(Node target, bool deep)
		{
			target._baseUri = this._baseUri;
			if (deep)
			{
				foreach (INode node in this._children)
				{
					target.AddNode((Node)node.Clone(true));
				}
			}
		}

		// Token: 0x0400095D RID: 2397
		private readonly NodeType _type;

		// Token: 0x0400095E RID: 2398
		private readonly string _name;

		// Token: 0x0400095F RID: 2399
		private readonly NodeFlags _flags;

		// Token: 0x04000960 RID: 2400
		private Url _baseUri;

		// Token: 0x04000961 RID: 2401
		private Node _parent;

		// Token: 0x04000962 RID: 2402
		private NodeList _children;

		// Token: 0x04000963 RID: 2403
		private Document _owner;
	}
}
