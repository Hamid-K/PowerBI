using System;
using AngleSharp.Dom.Collections;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;
using AngleSharp.Extensions;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x0200036E RID: 878
	internal sealed class HtmlMapElement : HtmlElement, IHtmlMapElement, IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers
	{
		// Token: 0x06001B7A RID: 7034 RVA: 0x000534D0 File Offset: 0x000516D0
		public HtmlMapElement(Document owner, string prefix = null)
			: base(owner, TagNames.Map, prefix, NodeFlags.None)
		{
		}

		// Token: 0x170007CF RID: 1999
		// (get) Token: 0x06001B7B RID: 7035 RVA: 0x0004FCAB File Offset: 0x0004DEAB
		// (set) Token: 0x06001B7C RID: 7036 RVA: 0x0004FCB8 File Offset: 0x0004DEB8
		public string Name
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Name);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Name, value, false);
			}
		}

		// Token: 0x170007D0 RID: 2000
		// (get) Token: 0x06001B7D RID: 7037 RVA: 0x000534E0 File Offset: 0x000516E0
		public IHtmlCollection<IHtmlAreaElement> Areas
		{
			get
			{
				HtmlCollection<IHtmlAreaElement> htmlCollection;
				if ((htmlCollection = this._areas) == null)
				{
					htmlCollection = (this._areas = new HtmlCollection<IHtmlAreaElement>(this, false, null));
				}
				return htmlCollection;
			}
		}

		// Token: 0x170007D1 RID: 2001
		// (get) Token: 0x06001B7E RID: 7038 RVA: 0x00053508 File Offset: 0x00051708
		public IHtmlCollection<IHtmlImageElement> Images
		{
			get
			{
				HtmlCollection<IHtmlImageElement> htmlCollection;
				if ((htmlCollection = this._images) == null)
				{
					htmlCollection = (this._images = new HtmlCollection<IHtmlImageElement>(base.Owner.DocumentElement, true, new Predicate<IHtmlImageElement>(this.IsAssociatedImage)));
				}
				return htmlCollection;
			}
		}

		// Token: 0x06001B7F RID: 7039 RVA: 0x00053548 File Offset: 0x00051748
		private bool IsAssociatedImage(IHtmlImageElement image)
		{
			string useMap = image.UseMap;
			if (!string.IsNullOrEmpty(useMap))
			{
				string text = (useMap.Has('#', 0) ? ("#" + this.Name) : this.Name);
				return useMap.Is(text);
			}
			return false;
		}

		// Token: 0x04000CE2 RID: 3298
		private HtmlCollection<IHtmlAreaElement> _areas;

		// Token: 0x04000CE3 RID: 3299
		private HtmlCollection<IHtmlImageElement> _images;
	}
}
