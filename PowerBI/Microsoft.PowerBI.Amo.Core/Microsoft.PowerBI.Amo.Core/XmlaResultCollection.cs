using System;
using System.Collections;

namespace Microsoft.AnalysisServices
{
	// Token: 0x02000067 RID: 103
	[Serializable]
	public sealed class XmlaResultCollection : ICollection, IEnumerable
	{
		// Token: 0x0600058D RID: 1421 RVA: 0x00021E0E File Offset: 0x0002000E
		internal XmlaResultCollection()
		{
			this.items = new ArrayList();
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x00021E21 File Offset: 0x00020021
		internal XmlaResultCollection(XmlaResult result)
			: this()
		{
			this.items.Add(result);
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x0600058F RID: 1423 RVA: 0x00021E36 File Offset: 0x00020036
		public int Count
		{
			get
			{
				return this.items.Count;
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000590 RID: 1424 RVA: 0x00021E44 File Offset: 0x00020044
		public bool ContainsErrors
		{
			get
			{
				int i = 0;
				int count = this.items.Count;
				while (i < count)
				{
					if (((XmlaResult)this.items[i]).ContainsErrors)
					{
						return true;
					}
					i++;
				}
				return false;
			}
		}

		// Token: 0x17000138 RID: 312
		public XmlaResult this[int index]
		{
			get
			{
				return (XmlaResult)this.items[index];
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000592 RID: 1426 RVA: 0x00021E98 File Offset: 0x00020098
		internal bool ContainsInvalidSessionError
		{
			get
			{
				int i = 0;
				int count = this.items.Count;
				while (i < count)
				{
					if (((XmlaResult)this.items[i]).ContainsInvalidSessionError)
					{
						return true;
					}
					i++;
				}
				return false;
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000593 RID: 1427 RVA: 0x00021ED8 File Offset: 0x000200D8
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000594 RID: 1428 RVA: 0x00021EDB File Offset: 0x000200DB
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x00021EE0 File Offset: 0x000200E0
		internal static Exception ExceptionOnError(XmlaResultCollection col)
		{
			OperationException ex = new OperationException(col);
			if (col.ContainsInvalidSessionError)
			{
				return new XmlaStreamException(ex, ConnectionExceptionCause.InvalidSessionId);
			}
			return ex;
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x00021F08 File Offset: 0x00020108
		internal static Exception ExceptionOnError(XmlaResult res)
		{
			OperationException ex = new OperationException(res);
			if (res.ContainsInvalidSessionError)
			{
				return new XmlaStreamException(ex, ConnectionExceptionCause.InvalidSessionId);
			}
			return ex;
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x00021F2D File Offset: 0x0002012D
		internal void Add(XmlaResult item)
		{
			this.items.Add(item);
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x00021F3C File Offset: 0x0002013C
		void ICollection.CopyTo(Array array, int index)
		{
			this.items.CopyTo(array, index);
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x00021F4B File Offset: 0x0002014B
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		// Token: 0x040003FD RID: 1021
		private ArrayList items;
	}
}
