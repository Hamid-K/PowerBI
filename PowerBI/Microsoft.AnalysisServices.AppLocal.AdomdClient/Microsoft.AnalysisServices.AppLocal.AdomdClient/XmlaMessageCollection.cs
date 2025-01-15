using System;
using System.Collections;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000046 RID: 70
	internal sealed class XmlaMessageCollection : ICollection, IEnumerable
	{
		// Token: 0x0600044C RID: 1100 RVA: 0x0001C4F5 File Offset: 0x0001A6F5
		internal XmlaMessageCollection()
		{
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x0600044D RID: 1101 RVA: 0x0001C508 File Offset: 0x0001A708
		public int Count
		{
			get
			{
				return this.items.Count;
			}
		}

		// Token: 0x170000F6 RID: 246
		public XmlaMessage this[int index]
		{
			get
			{
				return (XmlaMessage)this.items[index];
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x0600044F RID: 1103 RVA: 0x0001C528 File Offset: 0x0001A728
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000450 RID: 1104 RVA: 0x0001C52B File Offset: 0x0001A72B
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x0001C52E File Offset: 0x0001A72E
		internal void Add(XmlaMessage item)
		{
			this.items.Add(item);
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x0001C53D File Offset: 0x0001A73D
		void ICollection.CopyTo(Array array, int index)
		{
			this.items.CopyTo(array, index);
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x0001C54C File Offset: 0x0001A74C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		// Token: 0x040003B5 RID: 949
		private ArrayList items = new ArrayList();
	}
}
