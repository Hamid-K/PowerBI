using System;
using System.Collections;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000A4 RID: 164
	public sealed class MeasureCollection : ICollection, IEnumerable
	{
		// Token: 0x06000973 RID: 2419 RVA: 0x00028B6F File Offset: 0x00026D6F
		internal MeasureCollection(AdomdConnection connection, CubeDef parentCube)
		{
			this.measureCollectionInternal = new MeasureCollectionInternal(connection, parentCube);
		}

		// Token: 0x17000308 RID: 776
		public Measure this[int index]
		{
			get
			{
				return this.measureCollectionInternal[index];
			}
		}

		// Token: 0x17000309 RID: 777
		public Measure this[string index]
		{
			get
			{
				return this.measureCollectionInternal[index];
			}
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x00028BA0 File Offset: 0x00026DA0
		public Measure Find(string index)
		{
			return this.measureCollectionInternal.Find(index);
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000977 RID: 2423 RVA: 0x00028BAE File Offset: 0x00026DAE
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000978 RID: 2424 RVA: 0x00028BB1 File Offset: 0x00026DB1
		public object SyncRoot
		{
			get
			{
				return this.measureCollectionInternal.SyncRoot;
			}
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000979 RID: 2425 RVA: 0x00028BBE File Offset: 0x00026DBE
		public int Count
		{
			get
			{
				return this.measureCollectionInternal.Count;
			}
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x00028BCB File Offset: 0x00026DCB
		public void CopyTo(Measure[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x00028BD8 File Offset: 0x00026DD8
		void ICollection.CopyTo(Array array, int index)
		{
			AdomdUtils.CheckCopyToParameters(array, index, this.Count);
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], index + i);
			}
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x00028C13 File Offset: 0x00026E13
		public MeasureCollection.Enumerator GetEnumerator()
		{
			return new MeasureCollection.Enumerator(this);
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x00028C1B File Offset: 0x00026E1B
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x0600097E RID: 2430 RVA: 0x00028C28 File Offset: 0x00026E28
		internal MeasureCollectionInternal CollectionInternal
		{
			get
			{
				return this.measureCollectionInternal;
			}
		}

		// Token: 0x04000648 RID: 1608
		private MeasureCollectionInternal measureCollectionInternal;

		// Token: 0x020001B3 RID: 435
		public struct Enumerator : IEnumerator
		{
			// Token: 0x0600133A RID: 4922 RVA: 0x000442F0 File Offset: 0x000424F0
			internal Enumerator(MeasureCollection measures)
			{
				this.measures = measures.CollectionInternal;
				this.currentIndex = -1;
			}

			// Token: 0x0600133B RID: 4923 RVA: 0x00044305 File Offset: 0x00042505
			internal Enumerator(MeasureCollectionInternal measures)
			{
				this.measures = measures;
				this.currentIndex = -1;
			}

			// Token: 0x170006B7 RID: 1719
			// (get) Token: 0x0600133C RID: 4924 RVA: 0x00044318 File Offset: 0x00042518
			public Measure Current
			{
				get
				{
					Measure measure;
					try
					{
						measure = this.measures[this.currentIndex];
					}
					catch (ArgumentException)
					{
						throw new InvalidOperationException();
					}
					return measure;
				}
			}

			// Token: 0x170006B8 RID: 1720
			// (get) Token: 0x0600133D RID: 4925 RVA: 0x00044354 File Offset: 0x00042554
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x0600133E RID: 4926 RVA: 0x0004435C File Offset: 0x0004255C
			public bool MoveNext()
			{
				int num = this.currentIndex + 1;
				this.currentIndex = num;
				return num < this.measures.Count;
			}

			// Token: 0x0600133F RID: 4927 RVA: 0x00044387 File Offset: 0x00042587
			public void Reset()
			{
				this.currentIndex = -1;
			}

			// Token: 0x04000CD4 RID: 3284
			private MeasureCollectionInternal measures;

			// Token: 0x04000CD5 RID: 3285
			private int currentIndex;
		}
	}
}
