using System;
using System.Collections;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000C7 RID: 199
	public sealed class MiningServiceCollection : ICollection, IEnumerable
	{
		// Token: 0x06000B3E RID: 2878 RVA: 0x0002CA9B File Offset: 0x0002AC9B
		internal MiningServiceCollection(AdomdConnection connection)
		{
			this.miningServiceCollectionInternal = new MiningServiceCollectionInternal(connection);
		}

		// Token: 0x1700040F RID: 1039
		public MiningService this[int index]
		{
			get
			{
				return this.miningServiceCollectionInternal[index];
			}
		}

		// Token: 0x17000410 RID: 1040
		public MiningService this[string index]
		{
			get
			{
				return this.miningServiceCollectionInternal[index];
			}
		}

		// Token: 0x06000B41 RID: 2881 RVA: 0x0002CACB File Offset: 0x0002ACCB
		public MiningService Find(string index)
		{
			return this.miningServiceCollectionInternal.Find(index);
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06000B42 RID: 2882 RVA: 0x0002CAD9 File Offset: 0x0002ACD9
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06000B43 RID: 2883 RVA: 0x0002CADC File Offset: 0x0002ACDC
		public object SyncRoot
		{
			get
			{
				return this.miningServiceCollectionInternal.SyncRoot;
			}
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06000B44 RID: 2884 RVA: 0x0002CAE9 File Offset: 0x0002ACE9
		public int Count
		{
			get
			{
				return this.miningServiceCollectionInternal.Count;
			}
		}

		// Token: 0x06000B45 RID: 2885 RVA: 0x0002CAF6 File Offset: 0x0002ACF6
		public void CopyTo(MiningService[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x0002CB00 File Offset: 0x0002AD00
		void ICollection.CopyTo(Array array, int index)
		{
			AdomdUtils.CheckCopyToParameters(array, index, this.Count);
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], index + i);
			}
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x0002CB3B File Offset: 0x0002AD3B
		public MiningServiceCollection.Enumerator GetEnumerator()
		{
			return new MiningServiceCollection.Enumerator(this);
		}

		// Token: 0x06000B48 RID: 2888 RVA: 0x0002CB43 File Offset: 0x0002AD43
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06000B49 RID: 2889 RVA: 0x0002CB50 File Offset: 0x0002AD50
		internal MiningServiceCollectionInternal CollectionInternal
		{
			get
			{
				return this.miningServiceCollectionInternal;
			}
		}

		// Token: 0x04000768 RID: 1896
		private MiningServiceCollectionInternal miningServiceCollectionInternal;

		// Token: 0x020001BC RID: 444
		public struct Enumerator : IEnumerator
		{
			// Token: 0x06001360 RID: 4960 RVA: 0x00044324 File Offset: 0x00042524
			internal Enumerator(MiningServiceCollection miningServices)
			{
				this.miningServices = miningServices.CollectionInternal;
				this.currentIndex = -1;
			}

			// Token: 0x06001361 RID: 4961 RVA: 0x00044339 File Offset: 0x00042539
			internal Enumerator(MiningServiceCollectionInternal miningServices)
			{
				this.miningServices = miningServices;
				this.currentIndex = -1;
			}

			// Token: 0x170006C3 RID: 1731
			// (get) Token: 0x06001362 RID: 4962 RVA: 0x0004434C File Offset: 0x0004254C
			public MiningService Current
			{
				get
				{
					MiningService miningService;
					try
					{
						miningService = this.miningServices[this.currentIndex];
					}
					catch (ArgumentException)
					{
						throw new InvalidOperationException();
					}
					return miningService;
				}
			}

			// Token: 0x170006C4 RID: 1732
			// (get) Token: 0x06001363 RID: 4963 RVA: 0x00044388 File Offset: 0x00042588
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06001364 RID: 4964 RVA: 0x00044390 File Offset: 0x00042590
			public bool MoveNext()
			{
				int num = this.currentIndex + 1;
				this.currentIndex = num;
				return num < this.miningServices.Count;
			}

			// Token: 0x06001365 RID: 4965 RVA: 0x000443BB File Offset: 0x000425BB
			public void Reset()
			{
				this.currentIndex = -1;
			}

			// Token: 0x04000CD5 RID: 3285
			private MiningServiceCollectionInternal miningServices;

			// Token: 0x04000CD6 RID: 3286
			private int currentIndex;
		}
	}
}
