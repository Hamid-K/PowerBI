using System;
using System.Collections;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000C7 RID: 199
	public sealed class MiningServiceCollection : ICollection, IEnumerable
	{
		// Token: 0x06000B4B RID: 2891 RVA: 0x0002CDCB File Offset: 0x0002AFCB
		internal MiningServiceCollection(AdomdConnection connection)
		{
			this.miningServiceCollectionInternal = new MiningServiceCollectionInternal(connection);
		}

		// Token: 0x17000415 RID: 1045
		public MiningService this[int index]
		{
			get
			{
				return this.miningServiceCollectionInternal[index];
			}
		}

		// Token: 0x17000416 RID: 1046
		public MiningService this[string index]
		{
			get
			{
				return this.miningServiceCollectionInternal[index];
			}
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x0002CDFB File Offset: 0x0002AFFB
		public MiningService Find(string index)
		{
			return this.miningServiceCollectionInternal.Find(index);
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06000B4F RID: 2895 RVA: 0x0002CE09 File Offset: 0x0002B009
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06000B50 RID: 2896 RVA: 0x0002CE0C File Offset: 0x0002B00C
		public object SyncRoot
		{
			get
			{
				return this.miningServiceCollectionInternal.SyncRoot;
			}
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06000B51 RID: 2897 RVA: 0x0002CE19 File Offset: 0x0002B019
		public int Count
		{
			get
			{
				return this.miningServiceCollectionInternal.Count;
			}
		}

		// Token: 0x06000B52 RID: 2898 RVA: 0x0002CE26 File Offset: 0x0002B026
		public void CopyTo(MiningService[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x06000B53 RID: 2899 RVA: 0x0002CE30 File Offset: 0x0002B030
		void ICollection.CopyTo(Array array, int index)
		{
			AdomdUtils.CheckCopyToParameters(array, index, this.Count);
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], index + i);
			}
		}

		// Token: 0x06000B54 RID: 2900 RVA: 0x0002CE6B File Offset: 0x0002B06B
		public MiningServiceCollection.Enumerator GetEnumerator()
		{
			return new MiningServiceCollection.Enumerator(this);
		}

		// Token: 0x06000B55 RID: 2901 RVA: 0x0002CE73 File Offset: 0x0002B073
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06000B56 RID: 2902 RVA: 0x0002CE80 File Offset: 0x0002B080
		internal MiningServiceCollectionInternal CollectionInternal
		{
			get
			{
				return this.miningServiceCollectionInternal;
			}
		}

		// Token: 0x04000775 RID: 1909
		private MiningServiceCollectionInternal miningServiceCollectionInternal;

		// Token: 0x020001BC RID: 444
		public struct Enumerator : IEnumerator
		{
			// Token: 0x0600136D RID: 4973 RVA: 0x00044860 File Offset: 0x00042A60
			internal Enumerator(MiningServiceCollection miningServices)
			{
				this.miningServices = miningServices.CollectionInternal;
				this.currentIndex = -1;
			}

			// Token: 0x0600136E RID: 4974 RVA: 0x00044875 File Offset: 0x00042A75
			internal Enumerator(MiningServiceCollectionInternal miningServices)
			{
				this.miningServices = miningServices;
				this.currentIndex = -1;
			}

			// Token: 0x170006C9 RID: 1737
			// (get) Token: 0x0600136F RID: 4975 RVA: 0x00044888 File Offset: 0x00042A88
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

			// Token: 0x170006CA RID: 1738
			// (get) Token: 0x06001370 RID: 4976 RVA: 0x000448C4 File Offset: 0x00042AC4
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06001371 RID: 4977 RVA: 0x000448CC File Offset: 0x00042ACC
			public bool MoveNext()
			{
				int num = this.currentIndex + 1;
				this.currentIndex = num;
				return num < this.miningServices.Count;
			}

			// Token: 0x06001372 RID: 4978 RVA: 0x000448F7 File Offset: 0x00042AF7
			public void Reset()
			{
				this.currentIndex = -1;
			}

			// Token: 0x04000CE6 RID: 3302
			private MiningServiceCollectionInternal miningServices;

			// Token: 0x04000CE7 RID: 3303
			private int currentIndex;
		}
	}
}
