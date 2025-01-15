using System;
using System.IO;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x020003A0 RID: 928
	internal sealed class HtmlTemplateElement : HtmlElement, IHtmlTemplateElement, IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers
	{
		// Token: 0x06001D1F RID: 7455 RVA: 0x0005546D File Offset: 0x0005366D
		public HtmlTemplateElement(Document owner, string prefix = null)
			: base(owner, TagNames.Template, prefix, NodeFlags.Special | NodeFlags.Scoped | NodeFlags.HtmlTableSectionScoped | NodeFlags.HtmlTableScoped)
		{
			this._content = new DocumentFragment(owner);
		}

		// Token: 0x17000882 RID: 2178
		// (get) Token: 0x06001D20 RID: 7456 RVA: 0x0005548D File Offset: 0x0005368D
		public IDocumentFragment Content
		{
			get
			{
				return this._content;
			}
		}

		// Token: 0x06001D21 RID: 7457 RVA: 0x00055498 File Offset: 0x00053698
		public void PopulateFragment()
		{
			while (base.HasChildNodes)
			{
				Node node = base.ChildNodes[0];
				base.RemoveNode(0, node);
				this._content.AddNode(node);
			}
		}

		// Token: 0x06001D22 RID: 7458 RVA: 0x000554D0 File Offset: 0x000536D0
		public override INode Clone(bool deep = true)
		{
			HtmlTemplateElement htmlTemplateElement = new HtmlTemplateElement(base.Owner, null);
			base.CloneElement(htmlTemplateElement, deep);
			for (int i = 0; i < this._content.ChildNodes.Length; i++)
			{
				Node node = this._content.ChildNodes[i].Clone(deep) as Node;
				if (node != null)
				{
					htmlTemplateElement._content.AddNode(node);
				}
			}
			return htmlTemplateElement;
		}

		// Token: 0x06001D23 RID: 7459 RVA: 0x0005553A File Offset: 0x0005373A
		public override void ToHtml(TextWriter writer, IMarkupFormatter formatter)
		{
			writer.Write(formatter.OpenTag(this, false));
			this._content.ChildNodes.ToHtml(writer, formatter);
			writer.Write(formatter.CloseTag(this, false));
		}

		// Token: 0x06001D24 RID: 7460 RVA: 0x0005556A File Offset: 0x0005376A
		internal override void NodeIsAdopted(Document oldDocument)
		{
			this._content.Owner = oldDocument;
		}

		// Token: 0x04000D01 RID: 3329
		private readonly DocumentFragment _content;
	}
}
