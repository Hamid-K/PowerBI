using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x020000DC RID: 220
	[Guid("83BC7AED-4D6F-4e61-B396-565A491185FB")]
	public sealed class TraceColumnCollection : ICollection, IEnumerable
	{
		// Token: 0x06000E8E RID: 3726 RVA: 0x0007043B File Offset: 0x0006E63B
		internal TraceColumnCollection()
		{
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06000E8F RID: 3727 RVA: 0x00070453 File Offset: 0x0006E653
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x1700039D RID: 925
		public TraceColumn this[int index]
		{
			get
			{
				if (index < 0 || index >= this.count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return this.items[index];
			}
		}

		// Token: 0x06000E91 RID: 3729 RVA: 0x00070480 File Offset: 0x0006E680
		public int Add(TraceColumn item)
		{
			if (this.Contains(item))
			{
				throw new InvalidOperationException(SR.Collections_ItemIsAlreadyInCollectionException(item.ToString(), typeof(TraceColumn).Name, typeof(TraceColumnCollection).Name));
			}
			this.items[this.count] = item;
			int num = this.count;
			this.count = num + 1;
			return num;
		}

		// Token: 0x06000E92 RID: 3730 RVA: 0x000704EB File Offset: 0x0006E6EB
		public void Clear()
		{
			this.count = 0;
		}

		// Token: 0x06000E93 RID: 3731 RVA: 0x000704F4 File Offset: 0x0006E6F4
		public bool Contains(TraceColumn item)
		{
			for (int i = 0; i < this.count; i++)
			{
				if (this.items[i] == item)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000E94 RID: 3732 RVA: 0x00070520 File Offset: 0x0006E720
		public void Remove(TraceColumn item)
		{
			int i = -1;
			for (int j = 0; j < this.count; j++)
			{
				if (this.items[j] == item)
				{
					i = j;
					break;
				}
			}
			if (i == -1)
			{
				throw new InvalidOperationException(SR.Collections_ItemNotInCollectionException(item.ToString()));
			}
			int num = i++;
			while (i < this.count)
			{
				this.items[num] = this.items[i];
				num++;
				i++;
			}
			this.count--;
		}

		// Token: 0x06000E95 RID: 3733 RVA: 0x000705A4 File Offset: 0x0006E7A4
		internal void CopyTo(TraceColumnCollection col)
		{
			if (col == null)
			{
				throw new ArgumentNullException("col");
			}
			col.Clear();
			for (int i = 0; i < this.count; i++)
			{
				col.items[i] = this.items[i];
			}
			col.count = this.count;
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06000E96 RID: 3734 RVA: 0x000705F2 File Offset: 0x0006E7F2
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06000E97 RID: 3735 RVA: 0x000705F5 File Offset: 0x0006E7F5
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06000E98 RID: 3736 RVA: 0x000705F8 File Offset: 0x0006E7F8
		void ICollection.CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (index + this.count > array.Length)
			{
				throw new ArgumentException(SR.Collections_InvalidIndex);
			}
			int i = 0;
			while (i < this.count)
			{
				array.SetValue(this.items[i], index);
				i++;
				index++;
			}
		}

		// Token: 0x06000E99 RID: 3737 RVA: 0x00070667 File Offset: 0x0006E867
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new TraceColumnCollection.TraceColumnEnumerator(this.items.GetEnumerator(), this.count);
		}

		// Token: 0x040001AD RID: 429
		private TraceColumn[] items = new TraceColumn[Trace.TraceColumnCount];

		// Token: 0x040001AE RID: 430
		private int count;

		// Token: 0x020002EF RID: 751
		private sealed class TraceColumnEnumerator : IEnumerator
		{
			// Token: 0x060023BF RID: 9151 RVA: 0x000E2BDB File Offset: 0x000E0DDB
			internal TraceColumnEnumerator(IEnumerator e, int count)
			{
				this.enumerator = e;
				this.count = count;
				this.currentPosition = -1;
			}

			// Token: 0x060023C0 RID: 9152 RVA: 0x000E2BF8 File Offset: 0x000E0DF8
			public bool MoveNext()
			{
				if (this.currentPosition >= this.count)
				{
					return false;
				}
				this.currentPosition++;
				return this.currentPosition < this.count && this.enumerator.MoveNext();
			}

			// Token: 0x060023C1 RID: 9153 RVA: 0x000E2C33 File Offset: 0x000E0E33
			public void Reset()
			{
				this.enumerator.Reset();
				this.count = -1;
			}

			// Token: 0x17000752 RID: 1874
			// (get) Token: 0x060023C2 RID: 9154 RVA: 0x000E2C47 File Offset: 0x000E0E47
			public object Current
			{
				get
				{
					if (this.currentPosition >= this.count)
					{
						throw new InvalidOperationException();
					}
					return this.enumerator.Current;
				}
			}

			// Token: 0x04000AC7 RID: 2759
			private IEnumerator enumerator;

			// Token: 0x04000AC8 RID: 2760
			private int count;

			// Token: 0x04000AC9 RID: 2761
			private int currentPosition;
		}
	}
}
