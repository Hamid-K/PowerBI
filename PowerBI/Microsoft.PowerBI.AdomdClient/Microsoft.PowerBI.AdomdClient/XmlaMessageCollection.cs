using System;
using System.Collections;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000046 RID: 70
	internal sealed class XmlaMessageCollection : ICollection, IEnumerable
	{
		// Token: 0x0600043F RID: 1087 RVA: 0x0001C1C5 File Offset: 0x0001A3C5
		internal XmlaMessageCollection()
		{
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000440 RID: 1088 RVA: 0x0001C1D8 File Offset: 0x0001A3D8
		public int Count
		{
			get
			{
				return this.items.Count;
			}
		}

		// Token: 0x170000F0 RID: 240
		public XmlaMessage this[int index]
		{
			get
			{
				return (XmlaMessage)this.items[index];
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000442 RID: 1090 RVA: 0x0001C1F8 File Offset: 0x0001A3F8
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000443 RID: 1091 RVA: 0x0001C1FB File Offset: 0x0001A3FB
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x0001C1FE File Offset: 0x0001A3FE
		internal void Add(XmlaMessage item)
		{
			this.items.Add(item);
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x0001C20D File Offset: 0x0001A40D
		void ICollection.CopyTo(Array array, int index)
		{
			this.items.CopyTo(array, index);
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x0001C21C File Offset: 0x0001A41C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		// Token: 0x040003A8 RID: 936
		private ArrayList items = new ArrayList();
	}
}
