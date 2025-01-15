using System;
using System.Collections;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x0200005D RID: 93
	[Serializable]
	public sealed class AdomdErrorCollection : ICollection, IEnumerable
	{
		// Token: 0x06000627 RID: 1575 RVA: 0x00021FFB File Offset: 0x000201FB
		internal AdomdErrorCollection()
		{
		}

		// Token: 0x17000186 RID: 390
		public AdomdError this[int index]
		{
			get
			{
				return (AdomdError)this.errors[index];
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000629 RID: 1577 RVA: 0x00022021 File Offset: 0x00020221
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x0600062A RID: 1578 RVA: 0x00022024 File Offset: 0x00020224
		public object SyncRoot
		{
			get
			{
				return this.errors.SyncRoot;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x0600062B RID: 1579 RVA: 0x00022031 File Offset: 0x00020231
		public int Count
		{
			get
			{
				return this.errors.Count;
			}
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x0002203E File Offset: 0x0002023E
		public void CopyTo(AdomdError[] array, int index)
		{
			this.errors.CopyTo(array, index);
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x0002204D File Offset: 0x0002024D
		void ICollection.CopyTo(Array array, int index)
		{
			this.errors.CopyTo(array, index);
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x0002205C File Offset: 0x0002025C
		public AdomdErrorCollection.Enumerator GetEnumerator()
		{
			return new AdomdErrorCollection.Enumerator(this);
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x00022064 File Offset: 0x00020264
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x00022071 File Offset: 0x00020271
		internal void Add(AdomdError error)
		{
			this.errors.Add(error);
		}

		// Token: 0x04000448 RID: 1096
		private ArrayList errors = new ArrayList();

		// Token: 0x020001A6 RID: 422
		public struct Enumerator : IEnumerator
		{
			// Token: 0x060012F4 RID: 4852 RVA: 0x00043B1A File Offset: 0x00041D1A
			internal Enumerator(AdomdErrorCollection errors)
			{
				this.enumer = errors.errors.GetEnumerator();
			}

			// Token: 0x1700069F RID: 1695
			// (get) Token: 0x060012F5 RID: 4853 RVA: 0x00043B2D File Offset: 0x00041D2D
			public AdomdError Current
			{
				get
				{
					return (AdomdError)this.enumer.Current;
				}
			}

			// Token: 0x170006A0 RID: 1696
			// (get) Token: 0x060012F6 RID: 4854 RVA: 0x00043B3F File Offset: 0x00041D3F
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060012F7 RID: 4855 RVA: 0x00043B47 File Offset: 0x00041D47
			public bool MoveNext()
			{
				return this.enumer.MoveNext();
			}

			// Token: 0x060012F8 RID: 4856 RVA: 0x00043B54 File Offset: 0x00041D54
			public void Reset()
			{
				this.enumer.Reset();
			}

			// Token: 0x04000CB9 RID: 3257
			private IEnumerator enumer;
		}
	}
}
