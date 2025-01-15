using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AngleSharp.Dom.Html;
using AngleSharp.Extensions;
using AngleSharp.Html;

namespace AngleSharp.Dom.Collections
{
	// Token: 0x020003FB RID: 1019
	internal sealed class HtmlFormControlsCollection : IHtmlFormControlsCollection, IHtmlCollection<IHtmlElement>, IEnumerable<IHtmlElement>, IEnumerable
	{
		// Token: 0x06002052 RID: 8274 RVA: 0x00056188 File Offset: 0x00054388
		public HtmlFormControlsCollection(IElement form, IElement root = null)
		{
			if (root == null)
			{
				root = form.Owner.DocumentElement;
			}
			this._elements = root.GetElements(true, null).Where(delegate(HtmlFormControlElement m)
			{
				if (m.Form == form)
				{
					IHtmlInputElement htmlInputElement = m as IHtmlInputElement;
					if (htmlInputElement == null || !htmlInputElement.Type.Is(InputTypeNames.Image))
					{
						return true;
					}
				}
				return false;
			});
		}

		// Token: 0x17000A2A RID: 2602
		// (get) Token: 0x06002053 RID: 8275 RVA: 0x000561DC File Offset: 0x000543DC
		public int Length
		{
			get
			{
				return this._elements.Count<HtmlFormControlElement>();
			}
		}

		// Token: 0x17000A2B RID: 2603
		public HtmlFormControlElement this[int index]
		{
			get
			{
				return this._elements.GetItemByIndex(index);
			}
		}

		// Token: 0x17000A2C RID: 2604
		public HtmlFormControlElement this[string id]
		{
			get
			{
				return this._elements.GetElementById(id);
			}
		}

		// Token: 0x06002056 RID: 8278 RVA: 0x00056205 File Offset: 0x00054405
		public IEnumerator<HtmlFormControlElement> GetEnumerator()
		{
			return this._elements.GetEnumerator();
		}

		// Token: 0x06002057 RID: 8279 RVA: 0x00056205 File Offset: 0x00054405
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._elements.GetEnumerator();
		}

		// Token: 0x17000A2D RID: 2605
		IHtmlElement IHtmlCollection<IHtmlElement>.this[int index]
		{
			get
			{
				return this[index];
			}
		}

		// Token: 0x17000A2E RID: 2606
		IHtmlElement IHtmlCollection<IHtmlElement>.this[string id]
		{
			get
			{
				return this[id];
			}
		}

		// Token: 0x0600205A RID: 8282 RVA: 0x00056205 File Offset: 0x00054405
		IEnumerator<IHtmlElement> IEnumerable<IHtmlElement>.GetEnumerator()
		{
			return this._elements.GetEnumerator();
		}

		// Token: 0x04000D10 RID: 3344
		private readonly IEnumerable<HtmlFormControlElement> _elements;
	}
}
