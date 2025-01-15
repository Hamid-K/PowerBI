using System;
using System.Collections;
using System.Collections.Generic;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000313 RID: 787
	internal abstract class Selectors : CssNode, IEnumerable<ISelector>, IEnumerable
	{
		// Token: 0x060016A6 RID: 5798 RVA: 0x0004F547 File Offset: 0x0004D747
		public Selectors()
		{
			this._selectors = new List<ISelector>();
		}

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x060016A7 RID: 5799 RVA: 0x0004F55C File Offset: 0x0004D75C
		public Priority Specifity
		{
			get
			{
				Priority priority = default(Priority);
				for (int i = 0; i < this._selectors.Count; i++)
				{
					priority += this._selectors[i].Specifity;
				}
				return priority;
			}
		}

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x060016A8 RID: 5800 RVA: 0x0004810B File Offset: 0x0004630B
		public string Text
		{
			get
			{
				return this.ToCss();
			}
		}

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x060016A9 RID: 5801 RVA: 0x0004F5A0 File Offset: 0x0004D7A0
		public int Length
		{
			get
			{
				return this._selectors.Count;
			}
		}

		// Token: 0x170005DF RID: 1503
		public ISelector this[int index]
		{
			get
			{
				return this._selectors[index];
			}
			set
			{
				this._selectors[index] = value;
			}
		}

		// Token: 0x060016AC RID: 5804 RVA: 0x0004F5CA File Offset: 0x0004D7CA
		public void Add(ISelector selector)
		{
			this._selectors.Add(selector);
		}

		// Token: 0x060016AD RID: 5805 RVA: 0x0004F5D8 File Offset: 0x0004D7D8
		public void Remove(ISelector selector)
		{
			this._selectors.Remove(selector);
		}

		// Token: 0x060016AE RID: 5806 RVA: 0x0004F5E7 File Offset: 0x0004D7E7
		public IEnumerator<ISelector> GetEnumerator()
		{
			return this._selectors.GetEnumerator();
		}

		// Token: 0x060016AF RID: 5807 RVA: 0x0004F5F9 File Offset: 0x0004D7F9
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000CA0 RID: 3232
		protected readonly List<ISelector> _selectors;
	}
}
