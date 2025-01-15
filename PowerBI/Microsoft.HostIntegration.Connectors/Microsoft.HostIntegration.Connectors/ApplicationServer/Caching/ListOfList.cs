using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200032F RID: 815
	internal sealed class ListOfList<T> : IList<T>, ICollection<T>, IEnumerable<T>, ICollection, IEnumerable
	{
		// Token: 0x06001D5E RID: 7518 RVA: 0x000588E8 File Offset: 0x00056AE8
		public ListOfList()
			: this(0)
		{
		}

		// Token: 0x06001D5F RID: 7519 RVA: 0x000588F4 File Offset: 0x00056AF4
		public ListOfList(int capacity)
		{
			if (capacity == 0)
			{
				this._internalList = new List<IList<T>>();
				return;
			}
			int num = capacity + ListOfList<T>._maxCount - 1 >> ListOfList<T>._sizeNum;
			this._internalList = new List<IList<T>>(num);
			if (num == 1)
			{
				this._childCapacity = capacity;
			}
		}

		// Token: 0x06001D60 RID: 7520 RVA: 0x0005894B File Offset: 0x00056B4B
		public ListOfList(IEnumerable<T> collection)
			: this(1)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this.AddRange(collection);
		}

		// Token: 0x06001D61 RID: 7521 RVA: 0x00058969 File Offset: 0x00056B69
		public ListOfList(ICollection<T> collection)
			: this((collection != null) ? collection.Count : 0)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this.AddRange(collection);
		}

		// Token: 0x06001D62 RID: 7522 RVA: 0x00058994 File Offset: 0x00056B94
		private static int GetSize()
		{
			int num = 16;
			int i = IntPtr.Size;
			while (i > 0)
			{
				i >>= 1;
				num--;
			}
			return num;
		}

		// Token: 0x06001D63 RID: 7523 RVA: 0x000589B9 File Offset: 0x00056BB9
		private static int GetParentListIndex(int index)
		{
			return index >> ListOfList<T>._sizeNum;
		}

		// Token: 0x06001D64 RID: 7524 RVA: 0x000589C5 File Offset: 0x00056BC5
		private static int GetChildListIndex(int index)
		{
			return index & ((1 << ListOfList<T>._sizeNum) - 1);
		}

		// Token: 0x06001D65 RID: 7525 RVA: 0x000589D5 File Offset: 0x00056BD5
		private static int GetCombinedIndex(int parent, int child)
		{
			return (parent << ListOfList<T>._sizeNum) + child;
		}

		// Token: 0x06001D66 RID: 7526 RVA: 0x000589E3 File Offset: 0x00056BE3
		private void ValidateExistingIndex(string paramName, int parent, int child)
		{
			if (parent < 0 || child < 0 || parent >= this._internalList.Count || child >= this._internalList[parent].Count)
			{
				throw new ArgumentOutOfRangeException(paramName);
			}
		}

		// Token: 0x06001D67 RID: 7527 RVA: 0x00058A18 File Offset: 0x00056C18
		public void AddRange(IEnumerable<T> collection)
		{
			foreach (T t in collection)
			{
				this.Add(t);
			}
		}

		// Token: 0x06001D68 RID: 7528 RVA: 0x00058A60 File Offset: 0x00056C60
		public int IndexOf(T item)
		{
			for (int i = 0; i < this._internalList.Count; i++)
			{
				int num = this._internalList[i].IndexOf(item);
				if (num >= 0)
				{
					return ListOfList<T>.GetCombinedIndex(i, num);
				}
			}
			return -1;
		}

		// Token: 0x06001D69 RID: 7529 RVA: 0x00058AA4 File Offset: 0x00056CA4
		public void Insert(int index, T item)
		{
			if (index == this.Count)
			{
				this.Add(item);
				return;
			}
			int parentListIndex = ListOfList<T>.GetParentListIndex(index);
			int num = ListOfList<T>.GetChildListIndex(index);
			this.ValidateExistingIndex("index", parentListIndex, num);
			for (int i = parentListIndex; i < this._internalList.Count; i++)
			{
				if (this._internalList[i].Count != ListOfList<T>._maxCount)
				{
					this._internalList[i].Insert(num, item);
					num = -1;
					break;
				}
				T t = this._internalList[i][this._internalList[i].Count - 1];
				this._internalList[i].RemoveAt(this._internalList[i].Count - 1);
				this._internalList[i].Insert(num, item);
				item = t;
				num = 0;
			}
			if (num == 0)
			{
				List<T> list = new List<T>(this._childCapacity);
				list.Add(item);
				this._internalList.Add(list);
			}
		}

		// Token: 0x06001D6A RID: 7530 RVA: 0x00058BB0 File Offset: 0x00056DB0
		public void RemoveAt(int index)
		{
			int parentListIndex = ListOfList<T>.GetParentListIndex(index);
			int num = ListOfList<T>.GetChildListIndex(index);
			this.ValidateExistingIndex("index", parentListIndex, num);
			for (int i = parentListIndex; i < this._internalList.Count - 1; i++)
			{
				this._internalList[i].RemoveAt(num);
				this._internalList[i].Add(this._internalList[i + 1][0]);
				num = 0;
			}
			if (this._internalList[this._internalList.Count - 1].Count == 1)
			{
				this._internalList.RemoveAt(this._internalList.Count - 1);
				return;
			}
			this._internalList[this._internalList.Count - 1].RemoveAt(num);
		}

		// Token: 0x1700060F RID: 1551
		public T this[int index]
		{
			get
			{
				int parentListIndex = ListOfList<T>.GetParentListIndex(index);
				int childListIndex = ListOfList<T>.GetChildListIndex(index);
				this.ValidateExistingIndex("index", parentListIndex, childListIndex);
				return this._internalList[parentListIndex][childListIndex];
			}
			set
			{
				int parentListIndex = ListOfList<T>.GetParentListIndex(index);
				int childListIndex = ListOfList<T>.GetChildListIndex(index);
				this.ValidateExistingIndex("index", parentListIndex, childListIndex);
				this._internalList[parentListIndex][childListIndex] = value;
			}
		}

		// Token: 0x06001D6D RID: 7533 RVA: 0x00058CF8 File Offset: 0x00056EF8
		public void Add(T item)
		{
			if (this._internalList.Count != 0 && this._internalList[this._internalList.Count - 1].Count < ListOfList<T>._maxCount)
			{
				this._internalList[this._internalList.Count - 1].Add(item);
				return;
			}
			List<T> list = new List<T>(this._childCapacity);
			list.Add(item);
			this._internalList.Add(list);
		}

		// Token: 0x06001D6E RID: 7534 RVA: 0x00058D74 File Offset: 0x00056F74
		public void Clear()
		{
			this._internalList.Clear();
		}

		// Token: 0x06001D6F RID: 7535 RVA: 0x00058D84 File Offset: 0x00056F84
		public bool Contains(T item)
		{
			foreach (IList<T> list in this._internalList)
			{
				List<T> list2 = (List<T>)list;
				if (list2.Contains(item))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001D70 RID: 7536 RVA: 0x00058DE0 File Offset: 0x00056FE0
		public void CopyTo(T[] array, int arrayIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			for (int i = 0; i < this.Count; i++)
			{
				array[arrayIndex + i] = this[i];
			}
		}

		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x06001D71 RID: 7537 RVA: 0x00058E1C File Offset: 0x0005701C
		public int Count
		{
			get
			{
				if (this._internalList.Count == 0)
				{
					return 0;
				}
				int num = this._internalList.Count - 1;
				return ListOfList<T>.GetCombinedIndex(num, this._internalList[num].Count);
			}
		}

		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x06001D72 RID: 7538 RVA: 0x00006F04 File Offset: 0x00005104
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001D73 RID: 7539 RVA: 0x00058E60 File Offset: 0x00057060
		public bool Remove(T item)
		{
			int num = this.IndexOf(item);
			if (num >= 0)
			{
				this.RemoveAt(num);
				return true;
			}
			return false;
		}

		// Token: 0x06001D74 RID: 7540 RVA: 0x00058E83 File Offset: 0x00057083
		public IEnumerator<T> GetEnumerator()
		{
			return this.Items.GetEnumerator();
		}

		// Token: 0x06001D75 RID: 7541 RVA: 0x00058E83 File Offset: 0x00057083
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.Items.GetEnumerator();
		}

		// Token: 0x06001D76 RID: 7542 RVA: 0x00058E90 File Offset: 0x00057090
		void ICollection.CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], index + i);
			}
		}

		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x06001D77 RID: 7543 RVA: 0x00058ED1 File Offset: 0x000570D1
		bool ICollection.IsSynchronized
		{
			get
			{
				return (this._internalList as ICollection).IsSynchronized;
			}
		}

		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x06001D78 RID: 7544 RVA: 0x00058EE3 File Offset: 0x000570E3
		object ICollection.SyncRoot
		{
			get
			{
				return (this._internalList as ICollection).SyncRoot;
			}
		}

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x06001D79 RID: 7545 RVA: 0x00058EF8 File Offset: 0x000570F8
		private IEnumerable<T> Items
		{
			get
			{
				foreach (IList<T> list2 in this._internalList)
				{
					List<T> list = (List<T>)list2;
					foreach (T element in list)
					{
						yield return element;
					}
				}
				yield break;
			}
		}

		// Token: 0x04001041 RID: 4161
		private static readonly int _sizeNum = ListOfList<T>.GetSize();

		// Token: 0x04001042 RID: 4162
		private static readonly int _maxCount = 1 << ListOfList<T>._sizeNum;

		// Token: 0x04001043 RID: 4163
		private readonly IList<IList<T>> _internalList;

		// Token: 0x04001044 RID: 4164
		private readonly int _childCapacity = ListOfList<T>._maxCount;
	}
}
