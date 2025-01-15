using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace Microsoft.AnalysisServices
{
	// Token: 0x020000D7 RID: 215
	[Guid("357DB182-675C-42B4-87E4-AC47FB8D4D5A")]
	[Serializable]
	public class XmlaWarningCollection : ICollection, IEnumerable
	{
		// Token: 0x1700024E RID: 590
		// (get) Token: 0x060009A0 RID: 2464 RVA: 0x0002AF9A File Offset: 0x0002919A
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x060009A1 RID: 2465 RVA: 0x0002AF9D File Offset: 0x0002919D
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x0002AFA0 File Offset: 0x000291A0
		void ICollection.CopyTo(Array array, int index)
		{
			this.items.CopyTo(array, index);
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x0002AFAF File Offset: 0x000291AF
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x060009A5 RID: 2469 RVA: 0x0002AFCF File Offset: 0x000291CF
		public int Count
		{
			get
			{
				return this.items.Count;
			}
		}

		// Token: 0x17000251 RID: 593
		public XmlaWarning this[int index]
		{
			get
			{
				return (XmlaWarning)this.items[index];
			}
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x0002AFEF File Offset: 0x000291EF
		internal void Add(XmlaWarning item)
		{
			this.items.Add(item);
		}

		// Token: 0x040007AD RID: 1965
		private ArrayList items = new ArrayList();
	}
}
