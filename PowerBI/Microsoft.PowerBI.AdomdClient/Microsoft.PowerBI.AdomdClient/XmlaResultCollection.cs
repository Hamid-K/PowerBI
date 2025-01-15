using System;
using System.Collections;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x0200004A RID: 74
	internal sealed class XmlaResultCollection : ICollection, IEnumerable
	{
		// Token: 0x060004C7 RID: 1223 RVA: 0x0001E11A File Offset: 0x0001C31A
		internal XmlaResultCollection()
		{
			this.items = new ArrayList();
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x0001E12D File Offset: 0x0001C32D
		internal XmlaResultCollection(XmlaResult result)
			: this()
		{
			this.items.Add(result);
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060004C9 RID: 1225 RVA: 0x0001E142 File Offset: 0x0001C342
		public int Count
		{
			get
			{
				return this.items.Count;
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060004CA RID: 1226 RVA: 0x0001E150 File Offset: 0x0001C350
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

		// Token: 0x17000124 RID: 292
		public XmlaResult this[int index]
		{
			get
			{
				return (XmlaResult)this.items[index];
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060004CC RID: 1228 RVA: 0x0001E1A4 File Offset: 0x0001C3A4
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

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060004CD RID: 1229 RVA: 0x0001E1E4 File Offset: 0x0001C3E4
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060004CE RID: 1230 RVA: 0x0001E1E7 File Offset: 0x0001C3E7
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x0001E1EC File Offset: 0x0001C3EC
		internal static Exception ExceptionOnError(XmlaResultCollection col)
		{
			AdomdErrorResponseException ex = new AdomdErrorResponseException(col);
			if (col.ContainsInvalidSessionError)
			{
				return new XmlaStreamException(ex, ConnectionExceptionCause.InvalidSessionId);
			}
			return ex;
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x0001E214 File Offset: 0x0001C414
		internal static Exception ExceptionOnError(XmlaResult res)
		{
			AdomdErrorResponseException ex = new AdomdErrorResponseException(res);
			if (res.ContainsInvalidSessionError)
			{
				return new XmlaStreamException(ex, ConnectionExceptionCause.InvalidSessionId);
			}
			return ex;
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x0001E239 File Offset: 0x0001C439
		internal void Add(XmlaResult item)
		{
			this.items.Add(item);
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x0001E248 File Offset: 0x0001C448
		void ICollection.CopyTo(Array array, int index)
		{
			this.items.CopyTo(array, index);
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x0001E257 File Offset: 0x0001C457
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		// Token: 0x040003C1 RID: 961
		private ArrayList items;
	}
}
