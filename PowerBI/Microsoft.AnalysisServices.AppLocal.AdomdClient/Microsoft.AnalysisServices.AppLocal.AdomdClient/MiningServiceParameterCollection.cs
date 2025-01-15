using System;
using System.Collections;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000CC RID: 204
	public sealed class MiningServiceParameterCollection : ICollection, IEnumerable
	{
		// Token: 0x06000B7E RID: 2942 RVA: 0x0002D247 File Offset: 0x0002B447
		internal MiningServiceParameterCollection(AdomdConnection connection, MiningService parentService)
		{
			this.miningServiceParameterCollectionInternal = new MiningServiceParameterCollectionInternal(connection, parentService);
		}

		// Token: 0x17000433 RID: 1075
		public MiningServiceParameter this[int index]
		{
			get
			{
				return this.miningServiceParameterCollectionInternal[index];
			}
		}

		// Token: 0x17000434 RID: 1076
		public MiningServiceParameter this[string index]
		{
			get
			{
				return this.miningServiceParameterCollectionInternal[index];
			}
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x0002D278 File Offset: 0x0002B478
		public MiningServiceParameter Find(string index)
		{
			return this.miningServiceParameterCollectionInternal.Find(index);
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06000B82 RID: 2946 RVA: 0x0002D286 File Offset: 0x0002B486
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06000B83 RID: 2947 RVA: 0x0002D289 File Offset: 0x0002B489
		public object SyncRoot
		{
			get
			{
				return this.miningServiceParameterCollectionInternal.SyncRoot;
			}
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06000B84 RID: 2948 RVA: 0x0002D296 File Offset: 0x0002B496
		public int Count
		{
			get
			{
				return this.miningServiceParameterCollectionInternal.Count;
			}
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x0002D2A3 File Offset: 0x0002B4A3
		public void CopyTo(MiningServiceParameter[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x0002D2B0 File Offset: 0x0002B4B0
		void ICollection.CopyTo(Array array, int index)
		{
			AdomdUtils.CheckCopyToParameters(array, index, this.Count);
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], index + i);
			}
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x0002D2EB File Offset: 0x0002B4EB
		public MiningServiceParameterCollection.Enumerator GetEnumerator()
		{
			return new MiningServiceParameterCollection.Enumerator(this);
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x0002D2F3 File Offset: 0x0002B4F3
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06000B89 RID: 2953 RVA: 0x0002D300 File Offset: 0x0002B500
		internal MiningServiceParameterCollectionInternal CollectionInternal
		{
			get
			{
				return this.miningServiceParameterCollectionInternal;
			}
		}

		// Token: 0x0400078C RID: 1932
		private MiningServiceParameterCollectionInternal miningServiceParameterCollectionInternal;

		// Token: 0x020001BD RID: 445
		public struct Enumerator : IEnumerator
		{
			// Token: 0x06001373 RID: 4979 RVA: 0x00044900 File Offset: 0x00042B00
			internal Enumerator(MiningServiceParameterCollection miningServiceParameters)
			{
				this.miningServiceParameters = miningServiceParameters.CollectionInternal;
				this.currentIndex = -1;
			}

			// Token: 0x06001374 RID: 4980 RVA: 0x00044915 File Offset: 0x00042B15
			internal Enumerator(MiningServiceParameterCollectionInternal miningServiceParameters)
			{
				this.miningServiceParameters = miningServiceParameters;
				this.currentIndex = -1;
			}

			// Token: 0x170006CB RID: 1739
			// (get) Token: 0x06001375 RID: 4981 RVA: 0x00044928 File Offset: 0x00042B28
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

			// Token: 0x170006CC RID: 1740
			// (get) Token: 0x06001376 RID: 4982 RVA: 0x00044964 File Offset: 0x00042B64
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06001377 RID: 4983 RVA: 0x0004496C File Offset: 0x00042B6C
			public bool MoveNext()
			{
				int num = this.currentIndex + 1;
				this.currentIndex = num;
				return num < this.miningServiceParameters.Count;
			}

			// Token: 0x06001378 RID: 4984 RVA: 0x00044997 File Offset: 0x00042B97
			public void Reset()
			{
				this.currentIndex = -1;
			}

			// Token: 0x04000CE8 RID: 3304
			private MiningServiceParameterCollectionInternal miningServiceParameters;

			// Token: 0x04000CE9 RID: 3305
			private int currentIndex;
		}
	}
}
