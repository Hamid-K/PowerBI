using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Collections
{
	// Token: 0x020003FA RID: 1018
	internal sealed class HtmlCollection<T> : IHtmlCollection<T>, IEnumerable<T>, IEnumerable where T : class, IElement
	{
		// Token: 0x0600204B RID: 8267 RVA: 0x0005612B File Offset: 0x0005432B
		public HtmlCollection(IEnumerable<T> elements)
		{
			this._elements = elements;
		}

		// Token: 0x0600204C RID: 8268 RVA: 0x0005613A File Offset: 0x0005433A
		public HtmlCollection(INode parent, bool deep = true, Predicate<T> predicate = null)
		{
			this._elements = parent.GetElements(deep, predicate);
		}

		// Token: 0x17000A27 RID: 2599
		public T this[int index]
		{
			get
			{
				return this._elements.GetItemByIndex(index);
			}
		}

		// Token: 0x17000A28 RID: 2600
		public T this[string id]
		{
			get
			{
				return this._elements.GetElementById(id);
			}
		}

		// Token: 0x17000A29 RID: 2601
		// (get) Token: 0x0600204F RID: 8271 RVA: 0x0005616C File Offset: 0x0005436C
		public int Length
		{
			get
			{
				return this._elements.Count<T>();
			}
		}

		// Token: 0x06002050 RID: 8272 RVA: 0x00056179 File Offset: 0x00054379
		public IEnumerator<T> GetEnumerator()
		{
			return this._elements.GetEnumerator();
		}

		// Token: 0x06002051 RID: 8273 RVA: 0x00056179 File Offset: 0x00054379
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._elements.GetEnumerator();
		}

		// Token: 0x04000D0F RID: 3343
		private readonly IEnumerable<T> _elements;
	}
}
