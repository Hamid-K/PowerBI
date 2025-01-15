using System;
using System.Collections;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000CC RID: 204
	public sealed class MiningServiceParameterCollection : ICollection, IEnumerable
	{
		// Token: 0x06000B71 RID: 2929 RVA: 0x0002CF17 File Offset: 0x0002B117
		internal MiningServiceParameterCollection(AdomdConnection connection, MiningService parentService)
		{
			this.miningServiceParameterCollectionInternal = new MiningServiceParameterCollectionInternal(connection, parentService);
		}

		// Token: 0x1700042D RID: 1069
		public MiningServiceParameter this[int index]
		{
			get
			{
				return this.miningServiceParameterCollectionInternal[index];
			}
		}

		// Token: 0x1700042E RID: 1070
		public MiningServiceParameter this[string index]
		{
			get
			{
				return this.miningServiceParameterCollectionInternal[index];
			}
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x0002CF48 File Offset: 0x0002B148
		public MiningServiceParameter Find(string index)
		{
			return this.miningServiceParameterCollectionInternal.Find(index);
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06000B75 RID: 2933 RVA: 0x0002CF56 File Offset: 0x0002B156
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06000B76 RID: 2934 RVA: 0x0002CF59 File Offset: 0x0002B159
		public object SyncRoot
		{
			get
			{
				return this.miningServiceParameterCollectionInternal.SyncRoot;
			}
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06000B77 RID: 2935 RVA: 0x0002CF66 File Offset: 0x0002B166
		public int Count
		{
			get
			{
				return this.miningServiceParameterCollectionInternal.Count;
			}
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x0002CF73 File Offset: 0x0002B173
		public void CopyTo(MiningServiceParameter[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x0002CF80 File Offset: 0x0002B180
		void ICollection.CopyTo(Array array, int index)
		{
			AdomdUtils.CheckCopyToParameters(array, index, this.Count);
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], index + i);
			}
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x0002CFBB File Offset: 0x0002B1BB
		public MiningServiceParameterCollection.Enumerator GetEnumerator()
		{
			return new MiningServiceParameterCollection.Enumerator(this);
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x0002CFC3 File Offset: 0x0002B1C3
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06000B7C RID: 2940 RVA: 0x0002CFD0 File Offset: 0x0002B1D0
		internal MiningServiceParameterCollectionInternal CollectionInternal
		{
			get
			{
				return this.miningServiceParameterCollectionInternal;
			}
		}

		// Token: 0x0400077F RID: 1919
		private MiningServiceParameterCollectionInternal miningServiceParameterCollectionInternal;

		// Token: 0x020001BD RID: 445
		public struct Enumerator : IEnumerator
		{
			// Token: 0x06001366 RID: 4966 RVA: 0x000443C4 File Offset: 0x000425C4
			internal Enumerator(MiningServiceParameterCollection miningServiceParameters)
			{
				this.miningServiceParameters = miningServiceParameters.CollectionInternal;
				this.currentIndex = -1;
			}

			// Token: 0x06001367 RID: 4967 RVA: 0x000443D9 File Offset: 0x000425D9
			internal Enumerator(MiningServiceParameterCollectionInternal miningServiceParameters)
			{
				this.miningServiceParameters = miningServiceParameters;
				this.currentIndex = -1;
			}

			// Token: 0x170006C5 RID: 1733
			// (get) Token: 0x06001368 RID: 4968 RVA: 0x000443EC File Offset: 0x000425EC
			public MiningServiceParameter Current
			{
				get
				{
					MiningServiceParameter miningServiceParameter;
					try
					{
						miningServiceParameter = this.miningServiceParameters[this.currentIndex];
					}
					catch (ArgumentException)
					{
						throw new InvalidOperationException();
					}
					return miningServiceParameter;
				}
			}

			// Token: 0x170006C6 RID: 1734
			// (get) Token: 0x06001369 RID: 4969 RVA: 0x00044428 File Offset: 0x00042628
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x0600136A RID: 4970 RVA: 0x00044430 File Offset: 0x00042630
			public bool MoveNext()
			{
				int num = this.currentIndex + 1;
				this.currentIndex = num;
				return num < this.miningServiceParameters.Count;
			}

			// Token: 0x0600136B RID: 4971 RVA: 0x0004445B File Offset: 0x0004265B
			public void Reset()
			{
				this.currentIndex = -1;
			}

			// Token: 0x04000CD7 RID: 3287
			private MiningServiceParameterCollectionInternal miningServiceParameters;

			// Token: 0x04000CD8 RID: 3288
			private int currentIndex;
		}
	}
}
