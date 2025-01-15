using System;
using System.IO;
using System.Linq;
using System.Text;
using AngleSharp.Dom.Collections;
using AngleSharp.Dom.Html;
using AngleSharp.Extensions;
using AngleSharp.Html;
using AngleSharp.Parser.Html;

namespace AngleSharp.Dom
{
	// Token: 0x0200014D RID: 333
	internal sealed class DocumentFragment : Node, IDocumentFragment, INode, IEventTarget, IMarkupFormattable, IParentNode, INonElementParentNode
	{
		// Token: 0x06000B29 RID: 2857 RVA: 0x00042840 File Offset: 0x00040A40
		internal DocumentFragment(Document owner)
			: base(owner, "#document-fragment", NodeType.DocumentFragment, NodeFlags.None)
		{
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x00042854 File Offset: 0x00040A54
		internal DocumentFragment(Element context, string html)
			: this(context.Owner)
		{
			TextSource textSource = new TextSource(html);
			HtmlDomBuilder htmlDomBuilder = new HtmlDomBuilder(new HtmlDocument(base.Owner.Context, textSource));
			HtmlParserOptions htmlParserOptions = new HtmlParserOptions
			{
				IsEmbedded = false,
				IsStrictMode = false,
				IsScripting = base.Owner.Options.IsScripting()
			};
			IElement documentElement = htmlDomBuilder.ParseFragment(htmlParserOptions, context).DocumentElement;
			while (documentElement.HasChildNodes)
			{
				INode firstChild = documentElement.FirstChild;
				documentElement.RemoveChild(firstChild);
				if (firstChild is Node)
				{
					base.Owner.AdoptNode(firstChild);
					base.InsertBefore((Node)firstChild, null, false);
				}
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000B2B RID: 2859 RVA: 0x00041583 File Offset: 0x0003F783
		public int ChildElementCount
		{
			get
			{
				return base.ChildNodes.OfType<Element>().Count<Element>();
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000B2C RID: 2860 RVA: 0x0004290C File Offset: 0x00040B0C
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

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000B2D RID: 2861 RVA: 0x00042934 File Offset: 0x00040B34
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

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000B2E RID: 2862 RVA: 0x00042970 File Offset: 0x00040B70
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

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000B2F RID: 2863 RVA: 0x000429AC File Offset: 0x00040BAC
		// (set) Token: 0x06000B30 RID: 2864 RVA: 0x00042A10 File Offset: 0x00040C10
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

		// Token: 0x06000B31 RID: 2865 RVA: 0x00042A40 File Offset: 0x00040C40
		public override INode Clone(bool deep = true)
		{
			DocumentFragment documentFragment = new DocumentFragment(base.Owner);
			base.CloneNode(documentFragment, deep);
			return documentFragment;
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x00041F5C File Offset: 0x0004015C
		public void Prepend(params INode[] nodes)
		{
			this.PrependNodes(nodes);
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x00041F65 File Offset: 0x00040165
		public void Append(params INode[] nodes)
		{
			this.AppendNodes(nodes);
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x0004208E File Offset: 0x0004028E
		public IElement QuerySelector(string selectors)
		{
			return base.ChildNodes.QuerySelector(selectors);
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x0004209C File Offset: 0x0004029C
		public IHtmlCollection<IElement> QuerySelectorAll(string selectors)
		{
			return base.ChildNodes.QuerySelectorAll(selectors);
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x000420AA File Offset: 0x000402AA
		public IHtmlCollection<IElement> GetElementsByClassName(string classNames)
		{
			return base.ChildNodes.GetElementsByClassName(classNames);
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x000420B8 File Offset: 0x000402B8
		public IHtmlCollection<IElement> GetElementsByTagName(string tagName)
		{
			return base.ChildNodes.GetElementsByTagName(tagName);
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x000420C6 File Offset: 0x000402C6
		public IHtmlCollection<IElement> GetElementsByTagNameNS(string namespaceURI, string tagName)
		{
			return base.ChildNodes.GetElementsByTagName(namespaceURI, tagName);
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x00042080 File Offset: 0x00040280
		public IElement GetElementById(string elementId)
		{
			return base.ChildNodes.GetElementById(elementId);
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x000420D5 File Offset: 0x000402D5
		public override void ToHtml(TextWriter writer, IMarkupFormatter formatter)
		{
			base.ChildNodes.ToHtml(writer, formatter);
		}

		// Token: 0x04000931 RID: 2353
		private HtmlCollection<IElement> _elements;
	}
}
