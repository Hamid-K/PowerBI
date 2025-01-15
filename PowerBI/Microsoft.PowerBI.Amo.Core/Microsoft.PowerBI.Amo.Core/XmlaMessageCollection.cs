using System;
using System.Collections;

namespace Microsoft.AnalysisServices
{
	// Token: 0x02000063 RID: 99
	[Serializable]
	public sealed class XmlaMessageCollection : ICollection, IEnumerable
	{
		// Token: 0x06000504 RID: 1284 RVA: 0x0001FEAD File Offset: 0x0001E0AD
		internal XmlaMessageCollection()
		{
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000505 RID: 1285 RVA: 0x0001FEC0 File Offset: 0x0001E0C0
		public int Count
		{
			get
			{
				return this.items.Count;
			}
		}

		// Token: 0x17000104 RID: 260
		public XmlaMessage this[int index]
		{
			get
			{
				return (XmlaMessage)this.items[index];
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000507 RID: 1287 RVA: 0x0001FEE0 File Offset: 0x0001E0E0
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000508 RID: 1288 RVA: 0x0001FEE3 File Offset: 0x0001E0E3
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x0001FEE6 File Offset: 0x0001E0E6
		internal void Add(XmlaMessage item)
		{
			this.items.Add(item);
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x0001FEF5 File Offset: 0x0001E0F5
		internal void Clear()
		{
			this.items.Clear();
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x0001FF02 File Offset: 0x0001E102
		void ICollection.CopyTo(Array array, int index)
		{
			this.items.CopyTo(array, index);
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x0001FF11 File Offset: 0x0001E111
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		// Token: 0x040003E4 RID: 996
		private ArrayList items = new ArrayList();
	}
}
