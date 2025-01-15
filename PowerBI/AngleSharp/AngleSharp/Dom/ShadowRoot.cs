using System;
using System.IO;
using System.Linq;
using System.Text;
using AngleSharp.Dom.Collections;
using AngleSharp.Extensions;
using AngleSharp.Html;

namespace AngleSharp.Dom
{
	// Token: 0x02000161 RID: 353
	internal sealed class ShadowRoot : Node, IShadowRoot, IDocumentFragment, INode, IEventTarget, IMarkupFormattable, IParentNode, INonElementParentNode
	{
		// Token: 0x06000CBB RID: 3259 RVA: 0x00045C84 File Offset: 0x00043E84
		internal ShadowRoot(Element host, ShadowRootMode mode)
			: base(host.Owner, "#shadow-root", NodeType.DocumentFragment, NodeFlags.None)
		{
			this._host = host;
			this._styleSheets = this.CreateStyleSheets();
			this._mode = mode;
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000CBC RID: 3260 RVA: 0x00045CB4 File Offset: 0x00043EB4
		public IElement ActiveElement
		{
			get
			{
				return (from m in this.GetDescendants().OfType<Element>()
					where m.IsFocused
					select m).FirstOrDefault<Element>();
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000CBD RID: 3261 RVA: 0x00045CEA File Offset: 0x00043EEA
		public IElement Host
		{
			get
			{
				return this._host;
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000CBE RID: 3262 RVA: 0x00045CF2 File Offset: 0x00043EF2
		// (set) Token: 0x06000CBF RID: 3263 RVA: 0x00045D04 File Offset: 0x00043F04
		public string InnerHtml
		{
			get
			{
				return base.ChildNodes.ToHtml(HtmlMarkupFormatter.Instance);
			}
			set
			{
				base.ReplaceAll(new DocumentFragment(this._host, value), false);
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000CC0 RID: 3264 RVA: 0x00045D19 File Offset: 0x00043F19
		public IStyleSheetList StyleSheets
		{
			get
			{
				return this._styleSheets;
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000CC1 RID: 3265 RVA: 0x00041583 File Offset: 0x0003F783
		public int ChildElementCount
		{
			get
			{
				return base.ChildNodes.OfType<Element>().Count<Element>();
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000CC2 RID: 3266 RVA: 0x00045D24 File Offset: 0x00043F24
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

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000CC3 RID: 3267 RVA: 0x00045D4C File Offset: 0x00043F4C
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

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000CC4 RID: 3268 RVA: 0x00045D88 File Offset: 0x00043F88
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

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000CC5 RID: 3269 RVA: 0x00045DC4 File Offset: 0x00043FC4
		// (set) Token: 0x06000CC6 RID: 3270 RVA: 0x00045E28 File Offset: 0x00044028
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

		// Token: 0x06000CC7 RID: 3271 RVA: 0x00045E58 File Offset: 0x00044058
		public override INode Clone(bool deep = true)
		{
			ShadowRoot shadowRoot = new ShadowRoot(this._host, this._mode);
			base.CloneNode(shadowRoot, deep);
			return shadowRoot;
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x00041F5C File Offset: 0x0004015C
		public void Prepend(params INode[] nodes)
		{
			this.PrependNodes(nodes);
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x00041F65 File Offset: 0x00040165
		public void Append(params INode[] nodes)
		{
			this.AppendNodes(nodes);
		}

		// Token: 0x06000CCA RID: 3274 RVA: 0x0004208E File Offset: 0x0004028E
		public IElement QuerySelector(string selectors)
		{
			return base.ChildNodes.QuerySelector(selectors);
		}

		// Token: 0x06000CCB RID: 3275 RVA: 0x0004209C File Offset: 0x0004029C
		public IHtmlCollection<IElement> QuerySelectorAll(string selectors)
		{
			return base.ChildNodes.QuerySelectorAll(selectors);
		}

		// Token: 0x06000CCC RID: 3276 RVA: 0x000420AA File Offset: 0x000402AA
		public IHtmlCollection<IElement> GetElementsByClassName(string classNames)
		{
			return base.ChildNodes.GetElementsByClassName(classNames);
		}

		// Token: 0x06000CCD RID: 3277 RVA: 0x000420B8 File Offset: 0x000402B8
		public IHtmlCollection<IElement> GetElementsByTagName(string tagName)
		{
			return base.ChildNodes.GetElementsByTagName(tagName);
		}

		// Token: 0x06000CCE RID: 3278 RVA: 0x000420C6 File Offset: 0x000402C6
		public IHtmlCollection<IElement> GetElementsByTagNameNS(string namespaceURI, string tagName)
		{
			return base.ChildNodes.GetElementsByTagName(namespaceURI, tagName);
		}

		// Token: 0x06000CCF RID: 3279 RVA: 0x00042080 File Offset: 0x00040280
		public IElement GetElementById(string elementId)
		{
			return base.ChildNodes.GetElementById(elementId);
		}

		// Token: 0x06000CD0 RID: 3280 RVA: 0x000420D5 File Offset: 0x000402D5
		public override void ToHtml(TextWriter writer, IMarkupFormatter formatter)
		{
			base.ChildNodes.ToHtml(writer, formatter);
		}

		// Token: 0x04000968 RID: 2408
		private readonly Element _host;

		// Token: 0x04000969 RID: 2409
		private readonly IStyleSheetList _styleSheets;

		// Token: 0x0400096A RID: 2410
		private readonly ShadowRootMode _mode;

		// Token: 0x0400096B RID: 2411
		private HtmlCollection<IElement> _elements;
	}
}
