using System;
using System.Collections;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x0200004A RID: 74
	internal sealed class XmlaResultCollection : ICollection, IEnumerable
	{
		// Token: 0x060004D4 RID: 1236 RVA: 0x0001E44A File Offset: 0x0001C64A
		internal XmlaResultCollection()
		{
			this.items = new ArrayList();
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x0001E45D File Offset: 0x0001C65D
		internal XmlaResultCollection(XmlaResult result)
			: this()
		{
			this.items.Add(result);
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060004D6 RID: 1238 RVA: 0x0001E472 File Offset: 0x0001C672
		public int Count
		{
			get
			{
				return this.items.Count;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060004D7 RID: 1239 RVA: 0x0001E480 File Offset: 0x0001C680
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

		// Token: 0x1700012A RID: 298
		public XmlaResult this[int index]
		{
			get
			{
				return (XmlaResult)this.items[index];
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060004D9 RID: 1241 RVA: 0x0001E4D4 File Offset: 0x0001C6D4
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

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060004DA RID: 1242 RVA: 0x0001E514 File Offset: 0x0001C714
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060004DB RID: 1243 RVA: 0x0001E517 File Offset: 0x0001C717
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x0001E51C File Offset: 0x0001C71C
		internal static Exception ExceptionOnError(XmlaResultCollection col)
		{
			AdomdErrorResponseException ex = new AdomdErrorResponseException(col);
			if (col.ContainsInvalidSessionError)
			{
				return new XmlaStreamException(ex, ConnectionExceptionCause.InvalidSessionId);
			}
			return ex;
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x0001E544 File Offset: 0x0001C744
		internal static Exception ExceptionOnError(XmlaResult res)
		{
			AdomdErrorResponseException ex = new AdomdErrorResponseException(res);
			if (res.ContainsInvalidSessionError)
			{
				return new XmlaStreamException(ex, ConnectionExceptionCause.InvalidSessionId);
			}
			return ex;
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x0001E569 File Offset: 0x0001C769
		internal void Add(XmlaResult item)
		{
			this.items.Add(item);
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x0001E578 File Offset: 0x0001C778
		void ICollection.CopyTo(Array array, int index)
		{
			this.items.CopyTo(array, index);
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x0001E587 File Offset: 0x0001C787
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		// Token: 0x040003CE RID: 974
		private ArrayList items;
	}
}
