using System;
using System.Collections;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000C4 RID: 196
	public sealed class MiningParameterCollection : ICollection, IEnumerable
	{
		// Token: 0x06000AFB RID: 2811 RVA: 0x0002C255 File Offset: 0x0002A455
		internal MiningParameterCollection(string parameters)
		{
			this.miningParameterCollectionInternal = new MiningParameterCollectionInternal(parameters);
		}

		// Token: 0x170003DF RID: 991
		public MiningParameter this[int index]
		{
			get
			{
				return this.miningParameterCollectionInternal[index];
			}
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x0002C278 File Offset: 0x0002A478
		public MiningParameter Find(string name)
		{
			foreach (MiningParameter miningParameter in this)
			{
				if (string.Compare(miningParameter.Name, name, StringComparison.Ordinal) == 0)
				{
					return miningParameter;
				}
			}
			return null;
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06000AFE RID: 2814 RVA: 0x0002C2B1 File Offset: 0x0002A4B1
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06000AFF RID: 2815 RVA: 0x0002C2B4 File Offset: 0x0002A4B4
		public object SyncRoot
		{
			get
			{
				return this.miningParameterCollectionInternal.SyncRoot;
			}
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06000B00 RID: 2816 RVA: 0x0002C2C1 File Offset: 0x0002A4C1
		public int Count
		{
			get
			{
				return this.miningParameterCollectionInternal.Count;
			}
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x0002C2CE File Offset: 0x0002A4CE
		public void CopyTo(MiningParameter[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x0002C2D8 File Offset: 0x0002A4D8
		void ICollection.CopyTo(Array array, int index)
		{
			AdomdUtils.CheckCopyToParameters(array, index, this.Count);
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], index + i);
			}
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x0002C313 File Offset: 0x0002A513
		public MiningParameterCollection.Enumerator GetEnumerator()
		{
			return new MiningParameterCollection.Enumerator(this);
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x0002C31B File Offset: 0x0002A51B
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06000B05 RID: 2821 RVA: 0x0002C328 File Offset: 0x0002A528
		internal MiningParameterCollectionInternal CollectionInternal
		{
			get
			{
				return this.miningParameterCollectionInternal;
			}
		}

		// Token: 0x0400074B RID: 1867
		private MiningParameterCollectionInternal miningParameterCollectionInternal;

		// Token: 0x020001BB RID: 443
		public struct Enumerator : IEnumerator
		{
			// Token: 0x0600135A RID: 4954 RVA: 0x00044284 File Offset: 0x00042484
			internal Enumerator(MiningParameterCollection miningParameters)
			{
				this.miningParameters = miningParameters.CollectionInternal;
				this.currentIndex = -1;
			}

			// Token: 0x0600135B RID: 4955 RVA: 0x00044299 File Offset: 0x00042499
			internal Enumerator(MiningParameterCollectionInternal miningParameters)
			{
				this.miningParameters = miningParameters;
				this.currentIndex = -1;
			}

			// Token: 0x170006C1 RID: 1729
			// (get) Token: 0x0600135C RID: 4956 RVA: 0x000442AC File Offset: 0x000424AC
			public MiningParameter Current
			{
				get
				{
					MiningParameter miningParameter;
					try
					{
						miningParameter = this.miningParameters[this.currentIndex];
					}
					catch (ArgumentException)
					{
						throw new InvalidOperationException();
					}
					return miningParameter;
				}
			}

			// Token: 0x170006C2 RID: 1730
			// (get) Token: 0x0600135D RID: 4957 RVA: 0x000442E8 File Offset: 0x000424E8
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x0600135E RID: 4958 RVA: 0x000442F0 File Offset: 0x000424F0
			public bool MoveNext()
			{
				int num = this.currentIndex + 1;
				this.currentIndex = num;
				return num < this.miningParameters.Count;
			}

			// Token: 0x0600135F RID: 4959 RVA: 0x0004431B File Offset: 0x0004251B
			public void Reset()
			{
				this.currentIndex = -1;
			}

			// Token: 0x04000CD3 RID: 3283
			private MiningParameterCollectionInternal miningParameters;

			// Token: 0x04000CD4 RID: 3284
			private int currentIndex;
		}
	}
}
