using System;
using System.IO;
using System.Linq;
using System.Text;
using AngleSharp.Dom.Collections;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;
using AngleSharp.Extensions;
using AngleSharp.Html;
using AngleSharp.Parser.Css;
using AngleSharp.Services.Styling;

namespace AngleSharp.Dom
{
	// Token: 0x02000152 RID: 338
	internal class Element : Node, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle
	{
		// Token: 0x06000B5E RID: 2910 RVA: 0x00042F63 File Offset: 0x00041163
		public Element(Document owner, string localName, string prefix, string namespaceUri, NodeFlags flags = NodeFlags.None)
			: this(owner, (prefix != null) ? (prefix + ":" + localName) : localName, localName, prefix, namespaceUri, flags)
		{
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x00042F84 File Offset: 0x00041184
		public Element(Document owner, string name, string localName, string prefix, string namespaceUri, NodeFlags flags = NodeFlags.None)
			: base(owner, name, NodeType.Element, flags)
		{
			this._localName = localName;
			this._prefix = prefix;
			this._namespace = namespaceUri;
			this._attributes = new NamedNodeMap(this);
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000B60 RID: 2912 RVA: 0x00042FB4 File Offset: 0x000411B4
		internal NamedNodeMap Attributes
		{
			get
			{
				return this._attributes;
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000B61 RID: 2913 RVA: 0x00042FBC File Offset: 0x000411BC
		public ICssStyleDeclaration Style
		{
			get
			{
				ICssStyleDeclaration cssStyleDeclaration;
				if ((cssStyleDeclaration = this._style) == null)
				{
					cssStyleDeclaration = (this._style = this.CreateStyle());
				}
				return cssStyleDeclaration;
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000B62 RID: 2914 RVA: 0x00042FE2 File Offset: 0x000411E2
		public IElement AssignedSlot
		{
			get
			{
				IElement parentElement = base.ParentElement;
				if (parentElement == null)
				{
					return null;
				}
				IShadowRoot shadowRoot = parentElement.ShadowRoot;
				if (shadowRoot == null)
				{
					return null;
				}
				return shadowRoot.GetAssignedSlot(this.Slot);
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000B63 RID: 2915 RVA: 0x00043006 File Offset: 0x00041206
		// (set) Token: 0x06000B64 RID: 2916 RVA: 0x00043013 File Offset: 0x00041213
		public string Slot
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Slot);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Slot, value, false);
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000B65 RID: 2917 RVA: 0x00043022 File Offset: 0x00041222
		public IShadowRoot ShadowRoot
		{
			get
			{
				return Element.ShadowRootProperty.Get(this);
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000B66 RID: 2918 RVA: 0x0004302F File Offset: 0x0004122F
		public string Prefix
		{
			get
			{
				return this._prefix;
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000B67 RID: 2919 RVA: 0x00043037 File Offset: 0x00041237
		public string LocalName
		{
			get
			{
				return this._localName;
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000B68 RID: 2920 RVA: 0x0004303F File Offset: 0x0004123F
		public string NamespaceUri
		{
			get
			{
				return this._namespace;
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000B69 RID: 2921 RVA: 0x00043048 File Offset: 0x00041248
		// (set) Token: 0x06000B6A RID: 2922 RVA: 0x000430AC File Offset: 0x000412AC
		public override string TextContent
		{
			get
			{
				StringBuilder stringBuilder = Pool.NewStringBuilder();
				foreach (IText text in this.GetDescendants().OfType<IText>())
				{
					stringBuilder.Append(text.Data);
				}
				return stringBuilder.ToPool();
			}
			set
			{
				TextNode textNode = ((!string.IsNullOrEmpty(value)) ? new TextNode(base.Owner, value) : null);
				base.ReplaceAll(textNode, false);
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000B6B RID: 2923 RVA: 0x000430D9 File Offset: 0x000412D9
		public ITokenList ClassList
		{
			get
			{
				if (this._classList == null)
				{
					this._classList = new TokenList(this.GetOwnAttribute(AttributeNames.Class));
					this._classList.Changed += delegate(string value)
					{
						this.UpdateAttribute(AttributeNames.Class, value);
					};
				}
				return this._classList;
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000B6C RID: 2924 RVA: 0x00043116 File Offset: 0x00041316
		// (set) Token: 0x06000B6D RID: 2925 RVA: 0x00043123 File Offset: 0x00041323
		public string ClassName
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Class);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Class, value, false);
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000B6E RID: 2926 RVA: 0x00043132 File Offset: 0x00041332
		// (set) Token: 0x06000B6F RID: 2927 RVA: 0x0004313F File Offset: 0x0004133F
		public string Id
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Id);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Id, value, false);
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000B70 RID: 2928 RVA: 0x00042B73 File Offset: 0x00040D73
		public string TagName
		{
			get
			{
				return base.NodeName;
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000B71 RID: 2929 RVA: 0x00043150 File Offset: 0x00041350
		public IElement PreviousElementSibling
		{
			get
			{
				Node parent = base.Parent;
				if (parent != null)
				{
					bool flag = false;
					for (int i = parent.ChildNodes.Length - 1; i >= 0; i--)
					{
						if (parent.ChildNodes[i] == this)
						{
							flag = true;
						}
						else if (flag && parent.ChildNodes[i] is IElement)
						{
							return (IElement)parent.ChildNodes[i];
						}
					}
				}
				return null;
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000B72 RID: 2930 RVA: 0x000431C0 File Offset: 0x000413C0
		public IElement NextElementSibling
		{
			get
			{
				Node parent = base.Parent;
				if (parent != null)
				{
					int length = parent.ChildNodes.Length;
					bool flag = false;
					for (int i = 0; i < length; i++)
					{
						if (parent.ChildNodes[i] == this)
						{
							flag = true;
						}
						else if (flag && parent.ChildNodes[i] is IElement)
						{
							return (IElement)parent.ChildNodes[i];
						}
					}
				}
				return null;
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000B73 RID: 2931 RVA: 0x00043230 File Offset: 0x00041430
		public int ChildElementCount
		{
			get
			{
				NodeList childNodes = base.ChildNodes;
				int length = childNodes.Length;
				int num = 0;
				for (int i = 0; i < length; i++)
				{
					if (childNodes[i].NodeType == NodeType.Element)
					{
						num++;
					}
				}
				return num;
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000B74 RID: 2932 RVA: 0x00043270 File Offset: 0x00041470
		public IHtmlCollection<IElement> Children
		{
			get
			{
				HtmlCollection<IElement> htmlCollection;
				if ((htmlCollection = this._elements) == null)
				{
					htmlCollection = (this._elements = new HtmlCollection<IElement>(this, false, null));
				}
				return htmlCollection;
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000B75 RID: 2933 RVA: 0x00043298 File Offset: 0x00041498
		public IElement FirstElementChild
		{
			get
			{
				NodeList childNodes = base.ChildNodes;
				int length = childNodes.Length;
				for (int i = 0; i < length; i++)
				{
					IElement element = childNodes[i] as IElement;
					if (element != null)
					{
						return element;
					}
				}
				return null;
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000B76 RID: 2934 RVA: 0x000432D4 File Offset: 0x000414D4
		public IElement LastElementChild
		{
			get
			{
				NodeList childNodes = base.ChildNodes;
				for (int i = childNodes.Length - 1; i >= 0; i--)
				{
					IElement element = childNodes[i] as IElement;
					if (element != null)
					{
						return element;
					}
				}
				return null;
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000B77 RID: 2935 RVA: 0x0004330E File Offset: 0x0004150E
		// (set) Token: 0x06000B78 RID: 2936 RVA: 0x0004331B File Offset: 0x0004151B
		public string InnerHtml
		{
			get
			{
				return base.ChildNodes.ToHtml();
			}
			set
			{
				base.ReplaceAll(new DocumentFragment(this, value), false);
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000B79 RID: 2937 RVA: 0x0004332B File Offset: 0x0004152B
		// (set) Token: 0x06000B7A RID: 2938 RVA: 0x00043334 File Offset: 0x00041534
		public string OuterHtml
		{
			get
			{
				return this.ToHtml();
			}
			set
			{
				Node parent = base.Parent;
				if (parent == null)
				{
					throw new DomException(DomError.NotSupported);
				}
				Document owner = base.Owner;
				if (owner != null && owner.DocumentElement == this)
				{
					throw new DomException(DomError.NoModificationAllowed);
				}
				parent.InsertChild(parent.IndexOf(this), new DocumentFragment(this, value));
				parent.RemoveChild(this);
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000B7B RID: 2939 RVA: 0x00042FB4 File Offset: 0x000411B4
		INamedNodeMap IElement.Attributes
		{
			get
			{
				return this._attributes;
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000B7C RID: 2940 RVA: 0x00043388 File Offset: 0x00041588
		// (set) Token: 0x06000B7D RID: 2941 RVA: 0x000433A0 File Offset: 0x000415A0
		public bool IsFocused
		{
			get
			{
				Document owner = base.Owner;
				return ((owner != null) ? owner.FocusElement : null) == this;
			}
			protected set
			{
				Document owner = base.Owner;
				if (owner != null)
				{
					if (value)
					{
						owner.SetFocus(this);
						this.Fire(delegate(FocusEvent m)
						{
							m.Init(EventNames.Focus, false, false);
						}, null);
						return;
					}
					owner.SetFocus(null);
					this.Fire(delegate(FocusEvent m)
					{
						m.Init(EventNames.Blur, false, false);
					}, null);
				}
			}
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x00043418 File Offset: 0x00041618
		public IShadowRoot AttachShadow(ShadowRootMode mode = ShadowRootMode.Open)
		{
			if (TagNames.AllNoShadowRoot.Contains(this._localName))
			{
				throw new DomException(DomError.NotSupported);
			}
			if (this.ShadowRoot != null)
			{
				throw new DomException(DomError.InvalidState);
			}
			ShadowRoot shadowRoot = new ShadowRoot(this, mode);
			Element.ShadowRootProperty.Set(this, shadowRoot);
			return shadowRoot;
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x0004208E File Offset: 0x0004028E
		public IElement QuerySelector(string selectors)
		{
			return base.ChildNodes.QuerySelector(selectors);
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x0004209C File Offset: 0x0004029C
		public IHtmlCollection<IElement> QuerySelectorAll(string selectors)
		{
			return base.ChildNodes.QuerySelectorAll(selectors);
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x000420AA File Offset: 0x000402AA
		public IHtmlCollection<IElement> GetElementsByClassName(string classNames)
		{
			return base.ChildNodes.GetElementsByClassName(classNames);
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x000420B8 File Offset: 0x000402B8
		public IHtmlCollection<IElement> GetElementsByTagName(string tagName)
		{
			return base.ChildNodes.GetElementsByTagName(tagName);
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x000420C6 File Offset: 0x000402C6
		public IHtmlCollection<IElement> GetElementsByTagNameNS(string namespaceURI, string tagName)
		{
			return base.ChildNodes.GetElementsByTagName(namespaceURI, tagName);
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x00043464 File Offset: 0x00041664
		public bool Matches(string selectors)
		{
			return CssParser.Default.ParseSelector(selectors).Match(this);
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x00043478 File Offset: 0x00041678
		public override INode Clone(bool deep = true)
		{
			Element element = new Element(base.Owner, this.LocalName, this._prefix, this._namespace, base.Flags);
			this.CloneElement(element, deep);
			return element;
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x000434B2 File Offset: 0x000416B2
		public IPseudoElement Pseudo(string pseudoElement)
		{
			return PseudoElement.Create(this, pseudoElement);
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x000434BB File Offset: 0x000416BB
		public bool HasAttribute(string name)
		{
			if (this._namespace.Is(NamespaceNames.HtmlUri))
			{
				name = name.HtmlLower();
			}
			return this._attributes.GetNamedItem(name) != null;
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x000434E6 File Offset: 0x000416E6
		public bool HasAttribute(string namespaceUri, string localName)
		{
			if (string.IsNullOrEmpty(namespaceUri))
			{
				namespaceUri = null;
			}
			return this._attributes.GetNamedItem(namespaceUri, localName) != null;
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x00043503 File Offset: 0x00041703
		public string GetAttribute(string name)
		{
			if (this._namespace.Is(NamespaceNames.HtmlUri))
			{
				name = name.HtmlLower();
			}
			IAttr namedItem = this._attributes.GetNamedItem(name);
			if (namedItem == null)
			{
				return null;
			}
			return namedItem.Value;
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x00043536 File Offset: 0x00041736
		public string GetAttribute(string namespaceUri, string localName)
		{
			if (string.IsNullOrEmpty(namespaceUri))
			{
				namespaceUri = null;
			}
			IAttr namedItem = this._attributes.GetNamedItem(namespaceUri, localName);
			if (namedItem == null)
			{
				return null;
			}
			return namedItem.Value;
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x0004355B File Offset: 0x0004175B
		public void SetAttribute(string name, string value)
		{
			if (value == null)
			{
				this.RemoveAttribute(name);
				return;
			}
			if (!name.IsXmlName())
			{
				throw new DomException(DomError.InvalidCharacter);
			}
			if (this._namespace.Is(NamespaceNames.HtmlUri))
			{
				name = name.HtmlLower();
			}
			this.SetOwnAttribute(name, value, false);
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x0004359C File Offset: 0x0004179C
		public void SetAttribute(string namespaceUri, string name, string value)
		{
			if (value != null)
			{
				string text = null;
				string text2 = null;
				Node.GetPrefixAndLocalName(name, ref namespaceUri, out text, out text2);
				this._attributes.SetNamedItem(new Attr(text, text2, value, namespaceUri));
				return;
			}
			this.RemoveAttribute(namespaceUri, name);
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x000435DB File Offset: 0x000417DB
		public bool RemoveAttribute(string name)
		{
			if (this._namespace.Is(NamespaceNames.HtmlUri))
			{
				name = name.HtmlLower();
			}
			return this._attributes.RemoveNamedItemOrDefault(name) != null;
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x00043606 File Offset: 0x00041806
		public bool RemoveAttribute(string namespaceUri, string localName)
		{
			if (string.IsNullOrEmpty(namespaceUri))
			{
				namespaceUri = null;
			}
			return this._attributes.RemoveNamedItemOrDefault(namespaceUri, localName) != null;
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x00041F5C File Offset: 0x0004015C
		public void Prepend(params INode[] nodes)
		{
			this.PrependNodes(nodes);
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x00041F65 File Offset: 0x00040165
		public void Append(params INode[] nodes)
		{
			this.AppendNodes(nodes);
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x00043624 File Offset: 0x00041824
		public override bool Equals(INode otherNode)
		{
			IElement element = otherNode as IElement;
			return element != null && (this.NamespaceUri.Is(element.NamespaceUri) && this._attributes.AreEqual(element.Attributes)) && base.Equals(otherNode);
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x00040C80 File Offset: 0x0003EE80
		public void Before(params INode[] nodes)
		{
			this.InsertBefore(nodes);
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x00040C89 File Offset: 0x0003EE89
		public void After(params INode[] nodes)
		{
			this.InsertAfter(nodes);
		}

		// Token: 0x06000B94 RID: 2964 RVA: 0x00040C92 File Offset: 0x0003EE92
		public void Replace(params INode[] nodes)
		{
			this.ReplaceWith(nodes);
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x00040C9B File Offset: 0x0003EE9B
		public void Remove()
		{
			this.RemoveFromParent();
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x0004366C File Offset: 0x0004186C
		public void Insert(AdjacentPosition position, string html)
		{
			DocumentFragment documentFragment = new DocumentFragment((position == AdjacentPosition.BeforeBegin || position == AdjacentPosition.AfterEnd) ? this : (base.Parent as Element), html);
			switch (position)
			{
			case AdjacentPosition.BeforeBegin:
				base.Parent.InsertBefore(documentFragment, this);
				return;
			case AdjacentPosition.AfterBegin:
				base.InsertChild(0, documentFragment);
				return;
			case AdjacentPosition.BeforeEnd:
				base.AppendChild(documentFragment);
				return;
			case AdjacentPosition.AfterEnd:
				base.Parent.InsertChild(base.Parent.IndexOf(this) + 1, documentFragment);
				return;
			default:
				return;
			}
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x000436F0 File Offset: 0x000418F0
		public override void ToHtml(TextWriter writer, IMarkupFormatter formatter)
		{
			bool flag = (base.Flags & NodeFlags.SelfClosing) == NodeFlags.SelfClosing;
			writer.Write(formatter.OpenTag(this, flag));
			if (!flag)
			{
				if ((base.Flags & NodeFlags.LineTolerance) == NodeFlags.LineTolerance && base.FirstChild is IText && ((IText)base.FirstChild).Data.Has('\n', 0))
				{
					writer.Write('\n');
				}
				foreach (INode node in base.ChildNodes)
				{
					node.ToHtml(writer, formatter);
				}
			}
			writer.Write(formatter.CloseTag(this, flag));
		}

		// Token: 0x06000B98 RID: 2968 RVA: 0x00003C25 File Offset: 0x00001E25
		internal virtual void SetupElement()
		{
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x000437A4 File Offset: 0x000419A4
		internal void AttributeChanged(string localName, string namespaceUri, string oldValue, string newValue)
		{
			if (namespaceUri == null)
			{
				foreach (IAttributeObserver attributeObserver in base.Owner.Options.GetServices<IAttributeObserver>())
				{
					attributeObserver.NotifyChange(this, localName, newValue);
				}
			}
			base.Owner.QueueMutation(MutationRecord.Attributes(this, localName, namespaceUri, oldValue));
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x00043814 File Offset: 0x00041A14
		internal void UpdateClassList(string value)
		{
			TokenList classList = this._classList;
			if (classList == null)
			{
				return;
			}
			classList.Update(value);
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x00043827 File Offset: 0x00041A27
		internal void UpdateStyle(string value)
		{
			IBindable bindable = this._style as IBindable;
			if (string.IsNullOrEmpty(value))
			{
				this._attributes.RemoveNamedItemOrDefault(AttributeNames.Style, true);
			}
			if (bindable == null)
			{
				return;
			}
			bindable.Update(value);
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x0004385C File Offset: 0x00041A5C
		protected ICssStyleDeclaration CreateStyle()
		{
			if ((base.Flags & (NodeFlags.HtmlMember | NodeFlags.SvgMember)) != NodeFlags.None)
			{
				Document owner = base.Owner;
				IConfiguration options = owner.Options;
				IBrowsingContext context = owner.Context;
				ICssStyleEngine cssStyleEngine = options.GetCssStyleEngine();
				if (cssStyleEngine != null)
				{
					string ownAttribute = this.GetOwnAttribute(AttributeNames.Style);
					StyleOptions styleOptions = new StyleOptions(context)
					{
						Element = this
					};
					ICssStyleDeclaration cssStyleDeclaration = cssStyleEngine.ParseDeclaration(ownAttribute, styleOptions);
					IBindable bindable = cssStyleDeclaration as IBindable;
					if (bindable != null)
					{
						bindable.Changed += delegate(string value)
						{
							this.UpdateAttribute(AttributeNames.Style, value);
						};
					}
					return cssStyleDeclaration;
				}
			}
			return null;
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x000438D9 File Offset: 0x00041AD9
		protected void UpdateAttribute(string name, string value)
		{
			this.SetOwnAttribute(name, value, true);
		}

		// Token: 0x06000B9E RID: 2974 RVA: 0x000438E4 File Offset: 0x00041AE4
		protected sealed override string LocateNamespace(string prefix)
		{
			return this.LocateNamespaceFor(prefix);
		}

		// Token: 0x06000B9F RID: 2975 RVA: 0x000438ED File Offset: 0x00041AED
		protected sealed override string LocatePrefix(string namespaceUri)
		{
			return this.LocatePrefixFor(namespaceUri);
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x000438F8 File Offset: 0x00041AF8
		protected void CloneElement(Element element, bool deep)
		{
			base.CloneNode(element, deep);
			foreach (IAttr attr in this._attributes)
			{
				Attr attr2 = new Attr(attr.Prefix, attr.LocalName, attr.Value, attr.NamespaceUri);
				element._attributes.FastAddItem(attr2);
			}
			element.SetupElement();
		}

		// Token: 0x04000939 RID: 2361
		private static readonly AttachedProperty<Element, IShadowRoot> ShadowRootProperty = new AttachedProperty<Element, IShadowRoot>();

		// Token: 0x0400093A RID: 2362
		private readonly NamedNodeMap _attributes;

		// Token: 0x0400093B RID: 2363
		private readonly string _namespace;

		// Token: 0x0400093C RID: 2364
		private readonly string _prefix;

		// Token: 0x0400093D RID: 2365
		private readonly string _localName;

		// Token: 0x0400093E RID: 2366
		private HtmlCollection<IElement> _elements;

		// Token: 0x0400093F RID: 2367
		private ICssStyleDeclaration _style;

		// Token: 0x04000940 RID: 2368
		private TokenList _classList;
	}
}
