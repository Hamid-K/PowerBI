using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Collections
{
	// Token: 0x020003F9 RID: 1017
	internal sealed class HtmlAllCollection : IHtmlAllCollection, IHtmlCollection<IElement>, IEnumerable<IElement>, IEnumerable
	{
		// Token: 0x06002045 RID: 8261 RVA: 0x000560DF File Offset: 0x000542DF
		public HtmlAllCollection(IDocument document)
		{
			this._elements = document.GetElements(true, null);
		}

		// Token: 0x17000A24 RID: 2596
		public IElement this[int index]
		{
			get
			{
				return this._elements.GetItemByIndex(index);
			}
		}

		// Token: 0x17000A25 RID: 2597
		public IElement this[string id]
		{
			get
			{
				return this._elements.GetElementById(id);
			}
		}

		// Token: 0x17000A26 RID: 2598
		// (get) Token: 0x06002048 RID: 8264 RVA: 0x00056111 File Offset: 0x00054311
		public int Length
		{
			get
			{
				return this._elements.Count<IElement>();
			}
		}

		// Token: 0x06002049 RID: 8265 RVA: 0x0005611E File Offset: 0x0005431E
		public IEnumerator<IElement> GetEnumerator()
		{
			return this._elements.GetEnumerator();
		}

		// Token: 0x0600204A RID: 8266 RVA: 0x0005611E File Offset: 0x0005431E
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._elements.GetEnumerator();
		}

		// Token: 0x04000D0E RID: 3342
		private readonly IEnumerable<IElement> _elements;
	}
}
