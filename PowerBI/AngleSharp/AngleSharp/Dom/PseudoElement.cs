using System;
using System.Collections.Generic;
using System.IO;
using AngleSharp.Css;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;
using AngleSharp.Extensions;

namespace AngleSharp.Dom
{
	// Token: 0x02000160 RID: 352
	internal abstract class PseudoElement : IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IPseudoElement, IStyleUtilities
	{
		// Token: 0x06000C60 RID: 3168 RVA: 0x00045784 File Offset: 0x00043984
		public static PseudoElement Create(IElement host, string pseudoSelector)
		{
			Func<IElement, PseudoElement> func;
			if (!string.IsNullOrEmpty(pseudoSelector) && PseudoElement.creators.TryGetValue(pseudoSelector, out func))
			{
				return func(host);
			}
			return null;
		}

		// Token: 0x06000C61 RID: 3169 RVA: 0x000457B1 File Offset: 0x000439B1
		public PseudoElement(IElement host)
		{
			this._host = host;
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000C62 RID: 3170 RVA: 0x000457C0 File Offset: 0x000439C0
		public ICssStyleDeclaration Style
		{
			get
			{
				return this._host.Style;
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000C63 RID: 3171 RVA: 0x000457CD File Offset: 0x000439CD
		public IElement AssignedSlot
		{
			get
			{
				return this._host.AssignedSlot;
			}
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000C64 RID: 3172 RVA: 0x000457DA File Offset: 0x000439DA
		// (set) Token: 0x06000C65 RID: 3173 RVA: 0x00003C25 File Offset: 0x00001E25
		public string Slot
		{
			get
			{
				return this._host.Slot;
			}
			set
			{
			}
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000C66 RID: 3174 RVA: 0x000457E7 File Offset: 0x000439E7
		public IShadowRoot ShadowRoot
		{
			get
			{
				return this._host.ShadowRoot;
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000C67 RID: 3175 RVA: 0x000457F4 File Offset: 0x000439F4
		public ICssStyleDeclaration CascadedStyle
		{
			get
			{
				return this.Owner.DefaultView.GetStyleCollection().ComputeCascadedStyle(this);
			}
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000C68 RID: 3176 RVA: 0x0004580C File Offset: 0x00043A0C
		public ICssStyleDeclaration DefaultStyle
		{
			get
			{
				return this.Owner.DefaultView.ComputeDefaultStyle(this);
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000C69 RID: 3177 RVA: 0x0004581F File Offset: 0x00043A1F
		public ICssStyleDeclaration RawComputedStyle
		{
			get
			{
				return this.Owner.DefaultView.ComputeRawStyle(this);
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000C6A RID: 3178 RVA: 0x00045832 File Offset: 0x00043A32
		public ICssStyleDeclaration UsedStyle
		{
			get
			{
				return this.Owner.DefaultView.ComputeUsedStyle(this);
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000C6B RID: 3179 RVA: 0x00045845 File Offset: 0x00043A45
		public string Prefix
		{
			get
			{
				return this._host.Prefix;
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000C6C RID: 3180
		public abstract string PseudoName { get; }

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000C6D RID: 3181 RVA: 0x00045852 File Offset: 0x00043A52
		public string LocalName
		{
			get
			{
				return this._host.LocalName;
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000C6E RID: 3182 RVA: 0x0004585F File Offset: 0x00043A5F
		public string NamespaceUri
		{
			get
			{
				return this._host.NamespaceUri;
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000C6F RID: 3183 RVA: 0x0004586C File Offset: 0x00043A6C
		public INamedNodeMap Attributes
		{
			get
			{
				return this._host.Attributes;
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000C70 RID: 3184 RVA: 0x00045879 File Offset: 0x00043A79
		public ITokenList ClassList
		{
			get
			{
				return this._host.ClassList;
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000C71 RID: 3185 RVA: 0x00045886 File Offset: 0x00043A86
		// (set) Token: 0x06000C72 RID: 3186 RVA: 0x00003C25 File Offset: 0x00001E25
		public string ClassName
		{
			get
			{
				return this._host.ClassName;
			}
			set
			{
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000C73 RID: 3187 RVA: 0x00045893 File Offset: 0x00043A93
		// (set) Token: 0x06000C74 RID: 3188 RVA: 0x00003C25 File Offset: 0x00001E25
		public string Id
		{
			get
			{
				return this._host.Id;
			}
			set
			{
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000C75 RID: 3189 RVA: 0x0004280F File Offset: 0x00040A0F
		// (set) Token: 0x06000C76 RID: 3190 RVA: 0x00003C25 File Offset: 0x00001E25
		public string InnerHtml
		{
			get
			{
				return string.Empty;
			}
			set
			{
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000C77 RID: 3191 RVA: 0x0004280F File Offset: 0x00040A0F
		// (set) Token: 0x06000C78 RID: 3192 RVA: 0x00003C25 File Offset: 0x00001E25
		public string OuterHtml
		{
			get
			{
				return string.Empty;
			}
			set
			{
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000C79 RID: 3193 RVA: 0x000458A0 File Offset: 0x00043AA0
		public string TagName
		{
			get
			{
				return this._host.TagName;
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000C7A RID: 3194 RVA: 0x000458AD File Offset: 0x00043AAD
		public bool IsFocused
		{
			get
			{
				return this._host.IsFocused;
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000C7B RID: 3195 RVA: 0x000458BA File Offset: 0x00043ABA
		public string BaseUri
		{
			get
			{
				return this._host.BaseUri;
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000C7C RID: 3196 RVA: 0x000458C7 File Offset: 0x00043AC7
		public Url BaseUrl
		{
			get
			{
				return this._host.BaseUrl;
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000C7D RID: 3197 RVA: 0x000458D4 File Offset: 0x00043AD4
		public string NodeName
		{
			get
			{
				return this._host.NodeName;
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000C7E RID: 3198 RVA: 0x000458E1 File Offset: 0x00043AE1
		public INodeList ChildNodes
		{
			get
			{
				return this._host.ChildNodes;
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000C7F RID: 3199 RVA: 0x000458EE File Offset: 0x00043AEE
		public IDocument Owner
		{
			get
			{
				return this._host.Owner;
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000C80 RID: 3200 RVA: 0x000458FB File Offset: 0x00043AFB
		public IElement ParentElement
		{
			get
			{
				return this._host.ParentElement;
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000C81 RID: 3201 RVA: 0x00045908 File Offset: 0x00043B08
		public INode Parent
		{
			get
			{
				return this._host.Parent;
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000C82 RID: 3202 RVA: 0x00045915 File Offset: 0x00043B15
		public INode FirstChild
		{
			get
			{
				return this._host.FirstChild;
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000C83 RID: 3203 RVA: 0x00045922 File Offset: 0x00043B22
		public INode LastChild
		{
			get
			{
				return this._host.LastChild;
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000C84 RID: 3204 RVA: 0x0004592F File Offset: 0x00043B2F
		public INode NextSibling
		{
			get
			{
				return this._host.NextSibling;
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000C85 RID: 3205 RVA: 0x0004593C File Offset: 0x00043B3C
		public INode PreviousSibling
		{
			get
			{
				return this._host.PreviousSibling;
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000C86 RID: 3206 RVA: 0x0002F0AA File Offset: 0x0002D2AA
		public NodeType NodeType
		{
			get
			{
				return NodeType.Element;
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000C87 RID: 3207 RVA: 0x00045949 File Offset: 0x00043B49
		// (set) Token: 0x06000C88 RID: 3208 RVA: 0x00003C25 File Offset: 0x00001E25
		public string NodeValue
		{
			get
			{
				return this._host.NodeValue;
			}
			set
			{
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000C89 RID: 3209 RVA: 0x0004280F File Offset: 0x00040A0F
		// (set) Token: 0x06000C8A RID: 3210 RVA: 0x00003C25 File Offset: 0x00001E25
		public string TextContent
		{
			get
			{
				return string.Empty;
			}
			set
			{
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000C8B RID: 3211 RVA: 0x00045956 File Offset: 0x00043B56
		public bool HasChildNodes
		{
			get
			{
				return this._host.HasChildNodes;
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000C8C RID: 3212 RVA: 0x00045963 File Offset: 0x00043B63
		public IHtmlCollection<IElement> Children
		{
			get
			{
				return this._host.Children;
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000C8D RID: 3213 RVA: 0x00045970 File Offset: 0x00043B70
		public IElement FirstElementChild
		{
			get
			{
				return this._host.FirstElementChild;
			}
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000C8E RID: 3214 RVA: 0x0004597D File Offset: 0x00043B7D
		public IElement LastElementChild
		{
			get
			{
				return this._host.LastElementChild;
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000C8F RID: 3215 RVA: 0x0004598A File Offset: 0x00043B8A
		public int ChildElementCount
		{
			get
			{
				return this._host.ChildElementCount;
			}
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000C90 RID: 3216 RVA: 0x00045997 File Offset: 0x00043B97
		public IElement NextElementSibling
		{
			get
			{
				return this._host.NextElementSibling;
			}
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000C91 RID: 3217 RVA: 0x000459A4 File Offset: 0x00043BA4
		public IElement PreviousElementSibling
		{
			get
			{
				return this._host.PreviousElementSibling;
			}
		}

		// Token: 0x06000C92 RID: 3218 RVA: 0x000459B1 File Offset: 0x00043BB1
		public IShadowRoot AttachShadow(ShadowRootMode mode = ShadowRootMode.Open)
		{
			return this._host.AttachShadow(mode);
		}

		// Token: 0x06000C93 RID: 3219 RVA: 0x00003C25 File Offset: 0x00001E25
		public void Insert(AdjacentPosition position, string html)
		{
		}

		// Token: 0x06000C94 RID: 3220 RVA: 0x000459BF File Offset: 0x00043BBF
		public bool HasAttribute(string name)
		{
			return this._host.HasAttribute(name);
		}

		// Token: 0x06000C95 RID: 3221 RVA: 0x000459CD File Offset: 0x00043BCD
		public bool HasAttribute(string namespaceUri, string localName)
		{
			return this._host.HasAttribute(namespaceUri, localName);
		}

		// Token: 0x06000C96 RID: 3222 RVA: 0x000459DC File Offset: 0x00043BDC
		public string GetAttribute(string name)
		{
			return this._host.GetAttribute(name);
		}

		// Token: 0x06000C97 RID: 3223 RVA: 0x000459EA File Offset: 0x00043BEA
		public string GetAttribute(string namespaceUri, string localName)
		{
			return this._host.GetAttribute(namespaceUri, localName);
		}

		// Token: 0x06000C98 RID: 3224 RVA: 0x000459F9 File Offset: 0x00043BF9
		public void SetAttribute(string name, string value)
		{
			this._host.SetAttribute(name, value);
		}

		// Token: 0x06000C99 RID: 3225 RVA: 0x00045A08 File Offset: 0x00043C08
		public void SetAttribute(string namespaceUri, string name, string value)
		{
			this._host.SetAttribute(namespaceUri, name, value);
		}

		// Token: 0x06000C9A RID: 3226 RVA: 0x00045A18 File Offset: 0x00043C18
		public bool RemoveAttribute(string name)
		{
			return this._host.RemoveAttribute(name);
		}

		// Token: 0x06000C9B RID: 3227 RVA: 0x00045A26 File Offset: 0x00043C26
		public bool RemoveAttribute(string namespaceUri, string localName)
		{
			return this._host.RemoveAttribute(namespaceUri, localName);
		}

		// Token: 0x06000C9C RID: 3228 RVA: 0x00045A35 File Offset: 0x00043C35
		public IHtmlCollection<IElement> GetElementsByClassName(string classNames)
		{
			return this._host.GetElementsByClassName(classNames);
		}

		// Token: 0x06000C9D RID: 3229 RVA: 0x00045A43 File Offset: 0x00043C43
		public IHtmlCollection<IElement> GetElementsByTagName(string tagName)
		{
			return this._host.GetElementsByTagName(tagName);
		}

		// Token: 0x06000C9E RID: 3230 RVA: 0x00045A51 File Offset: 0x00043C51
		public IHtmlCollection<IElement> GetElementsByTagNameNS(string namespaceUri, string tagName)
		{
			return this._host.GetElementsByTagNameNS(namespaceUri, tagName);
		}

		// Token: 0x06000C9F RID: 3231 RVA: 0x00045A60 File Offset: 0x00043C60
		public bool Matches(string selectors)
		{
			return this._host.Matches(selectors);
		}

		// Token: 0x06000CA0 RID: 3232 RVA: 0x00045A6E File Offset: 0x00043C6E
		public INode Clone(bool deep = true)
		{
			return this._host.Clone(deep);
		}

		// Token: 0x06000CA1 RID: 3233 RVA: 0x0000C295 File Offset: 0x0000A495
		public IPseudoElement Pseudo(string pseudoElement)
		{
			return null;
		}

		// Token: 0x06000CA2 RID: 3234 RVA: 0x00045A7C File Offset: 0x00043C7C
		public bool Equals(INode otherNode)
		{
			return this._host.Equals(otherNode);
		}

		// Token: 0x06000CA3 RID: 3235 RVA: 0x00045A8A File Offset: 0x00043C8A
		public DocumentPositions CompareDocumentPosition(INode otherNode)
		{
			return this._host.CompareDocumentPosition(otherNode);
		}

		// Token: 0x06000CA4 RID: 3236 RVA: 0x00045A98 File Offset: 0x00043C98
		public void Normalize()
		{
			this._host.Normalize();
		}

		// Token: 0x06000CA5 RID: 3237 RVA: 0x00045AA5 File Offset: 0x00043CA5
		public bool Contains(INode otherNode)
		{
			return this._host.Contains(otherNode);
		}

		// Token: 0x06000CA6 RID: 3238 RVA: 0x00045AB3 File Offset: 0x00043CB3
		public bool IsDefaultNamespace(string namespaceUri)
		{
			return this._host.IsDefaultNamespace(namespaceUri);
		}

		// Token: 0x06000CA7 RID: 3239 RVA: 0x00045AC1 File Offset: 0x00043CC1
		public string LookupNamespaceUri(string prefix)
		{
			return this._host.LookupNamespaceUri(prefix);
		}

		// Token: 0x06000CA8 RID: 3240 RVA: 0x00045ACF File Offset: 0x00043CCF
		public string LookupPrefix(string namespaceUri)
		{
			return this._host.LookupPrefix(namespaceUri);
		}

		// Token: 0x06000CA9 RID: 3241 RVA: 0x00045ADD File Offset: 0x00043CDD
		public INode AppendChild(INode child)
		{
			return this._host.AppendChild(child);
		}

		// Token: 0x06000CAA RID: 3242 RVA: 0x00045AEB File Offset: 0x00043CEB
		public INode InsertBefore(INode newElement, INode referenceElement)
		{
			return this._host.InsertBefore(newElement, referenceElement);
		}

		// Token: 0x06000CAB RID: 3243 RVA: 0x00045AFA File Offset: 0x00043CFA
		public INode RemoveChild(INode child)
		{
			return this._host.RemoveChild(child);
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x00045B08 File Offset: 0x00043D08
		public INode ReplaceChild(INode newChild, INode oldChild)
		{
			return this._host.ReplaceChild(newChild, oldChild);
		}

		// Token: 0x06000CAD RID: 3245 RVA: 0x00045B17 File Offset: 0x00043D17
		public void AddEventListener(string type, DomEventHandler callback = null, bool capture = false)
		{
			this._host.AddEventListener(type, callback, capture);
		}

		// Token: 0x06000CAE RID: 3246 RVA: 0x00045B27 File Offset: 0x00043D27
		public void RemoveEventListener(string type, DomEventHandler callback = null, bool capture = false)
		{
			this._host.RemoveEventListener(type, callback, capture);
		}

		// Token: 0x06000CAF RID: 3247 RVA: 0x00045B37 File Offset: 0x00043D37
		public void InvokeEventListener(Event ev)
		{
			this._host.InvokeEventListener(ev);
		}

		// Token: 0x06000CB0 RID: 3248 RVA: 0x00045B45 File Offset: 0x00043D45
		public bool Dispatch(Event ev)
		{
			return this._host.Dispatch(ev);
		}

		// Token: 0x06000CB1 RID: 3249 RVA: 0x00045B53 File Offset: 0x00043D53
		public void Append(params INode[] nodes)
		{
			this._host.Append(nodes);
		}

		// Token: 0x06000CB2 RID: 3250 RVA: 0x00045B61 File Offset: 0x00043D61
		public void Prepend(params INode[] nodes)
		{
			this._host.Prepend(nodes);
		}

		// Token: 0x06000CB3 RID: 3251 RVA: 0x00045B6F File Offset: 0x00043D6F
		public IElement QuerySelector(string selectors)
		{
			return this._host.QuerySelector(selectors);
		}

		// Token: 0x06000CB4 RID: 3252 RVA: 0x00045B7D File Offset: 0x00043D7D
		public IHtmlCollection<IElement> QuerySelectorAll(string selectors)
		{
			return this._host.QuerySelectorAll(selectors);
		}

		// Token: 0x06000CB5 RID: 3253 RVA: 0x00045B8B File Offset: 0x00043D8B
		public void Before(params INode[] nodes)
		{
			this._host.Before(nodes);
		}

		// Token: 0x06000CB6 RID: 3254 RVA: 0x00045B99 File Offset: 0x00043D99
		public void After(params INode[] nodes)
		{
			this._host.After(nodes);
		}

		// Token: 0x06000CB7 RID: 3255 RVA: 0x00045BA7 File Offset: 0x00043DA7
		public void Replace(params INode[] nodes)
		{
			this._host.Replace(nodes);
		}

		// Token: 0x06000CB8 RID: 3256 RVA: 0x00045BB5 File Offset: 0x00043DB5
		public void Remove()
		{
			this._host.Remove();
		}

		// Token: 0x06000CB9 RID: 3257 RVA: 0x00045BC2 File Offset: 0x00043DC2
		public void ToHtml(TextWriter writer, IMarkupFormatter formatter)
		{
			this._host.ToHtml(writer, formatter);
		}

		// Token: 0x04000966 RID: 2406
		private static readonly Dictionary<string, Func<IElement, PseudoElement>> creators = new Dictionary<string, Func<IElement, PseudoElement>>(StringComparer.OrdinalIgnoreCase)
		{
			{
				":" + PseudoElementNames.Before,
				(IElement element) => new PseudoElement.BeforePseudoElement(element)
			},
			{
				"::" + PseudoElementNames.Before,
				(IElement element) => new PseudoElement.BeforePseudoElement(element)
			},
			{
				":" + PseudoElementNames.After,
				(IElement element) => new PseudoElement.AfterPseudoElement(element)
			},
			{
				"::" + PseudoElementNames.After,
				(IElement element) => new PseudoElement.AfterPseudoElement(element)
			}
		};

		// Token: 0x04000967 RID: 2407
		private readonly IElement _host;

		// Token: 0x020004DF RID: 1247
		private sealed class BeforePseudoElement : PseudoElement
		{
			// Token: 0x060025BB RID: 9659 RVA: 0x00062119 File Offset: 0x00060319
			public BeforePseudoElement(IElement host)
				: base(host)
			{
			}

			// Token: 0x17000ACA RID: 2762
			// (get) Token: 0x060025BC RID: 9660 RVA: 0x00062122 File Offset: 0x00060322
			public override string PseudoName
			{
				get
				{
					return "::" + PseudoElementNames.Before;
				}
			}
		}

		// Token: 0x020004E0 RID: 1248
		private sealed class AfterPseudoElement : PseudoElement
		{
			// Token: 0x060025BD RID: 9661 RVA: 0x00062119 File Offset: 0x00060319
			public AfterPseudoElement(IElement host)
				: base(host)
			{
			}

			// Token: 0x17000ACB RID: 2763
			// (get) Token: 0x060025BE RID: 9662 RVA: 0x00062133 File Offset: 0x00060333
			public override string PseudoName
			{
				get
				{
					return "::" + PseudoElementNames.After;
				}
			}
		}
	}
}
