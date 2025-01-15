using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x0200089A RID: 2202
	public sealed class ScalableList<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IList, ICollection, IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable, IDisposable
	{
		// Token: 0x06007890 RID: 30864 RVA: 0x001F0A70 File Offset: 0x001EEC70
		public ScalableList()
		{
		}

		// Token: 0x06007891 RID: 30865 RVA: 0x001F0A78 File Offset: 0x001EEC78
		internal ScalableList(int priority, IScalabilityCache cache)
			: this(priority, cache, 10)
		{
		}

		// Token: 0x06007892 RID: 30866 RVA: 0x001F0A84 File Offset: 0x001EEC84
		internal ScalableList(int priority, IScalabilityCache cache, int segmentSize)
			: this(priority, cache, segmentSize, segmentSize)
		{
		}

		// Token: 0x06007893 RID: 30867 RVA: 0x001F0A90 File Offset: 0x001EEC90
		internal ScalableList(int priority, IScalabilityCache cache, int segmentSize, int capacity)
			: this(priority, cache, segmentSize, capacity, false)
		{
		}

		// Token: 0x06007894 RID: 30868 RVA: 0x001F0AA0 File Offset: 0x001EECA0
		internal ScalableList(int priority, IScalabilityCache cache, int segmentSize, int capacity, bool keepAllBucketsPinned)
		{
			this.m_priority = priority;
			this.m_cache = cache;
			this.m_bucketSize = segmentSize;
			this.m_count = 0;
			if (keepAllBucketsPinned)
			{
				this.m_bucketPinState = ScalableList<T>.BucketPinState.UntilListEnd;
			}
			else if (cache.CacheType == ScalabilityCacheType.GroupTree || cache.CacheType == ScalabilityCacheType.Lookup)
			{
				this.m_bucketPinState = ScalableList<T>.BucketPinState.UntilBucketFull;
			}
			this.EnsureCapacity(capacity);
		}

		// Token: 0x06007895 RID: 30869 RVA: 0x001F0B00 File Offset: 0x001EED00
		public int IndexOf(T item)
		{
			EqualityComparer<T> @default = EqualityComparer<T>.Default;
			if (this.m_array != null)
			{
				object[] array = this.m_array.Value().Array;
				for (int i = 0; i < this.m_count; i++)
				{
					if (@default.Equals(item, (T)((object)array[i])))
					{
						return i;
					}
				}
			}
			else
			{
				int count = this.m_buckets.Count;
				for (int j = 0; j < count; j++)
				{
					IReference<StorableArray> reference = this.m_buckets[j];
					using (reference.PinValue())
					{
						object[] array2 = reference.Value().Array;
						int num = 0;
						if (j == count - 1)
						{
							num = this.m_count % this.m_bucketSize;
						}
						if (num == 0)
						{
							num = this.m_bucketSize;
						}
						for (int k = 0; k < num; k++)
						{
							if (@default.Equals(item, (T)((object)array2[k])))
							{
								return j * this.m_bucketSize + k;
							}
						}
					}
				}
			}
			return -1;
		}

		// Token: 0x06007896 RID: 30870 RVA: 0x001F0C18 File Offset: 0x001EEE18
		public void Insert(int index, T itemToInsert)
		{
			this.CheckReadOnly("Insert");
			this.CheckIndex(index, this.m_count);
			this.EnsureCapacity(this.m_count + 1);
			if (this.m_array != null)
			{
				using (this.m_array.PinValue())
				{
					object[] array = this.m_array.Value().Array;
					Array.Copy(array, index, array, index + 1, this.m_count - index);
					array[index] = itemToInsert;
					goto IL_012F;
				}
			}
			object obj = itemToInsert;
			object obj2 = obj;
			for (int i = this.GetBucketIndex(index); i < this.m_buckets.Count; i++)
			{
				obj = obj2;
				int indexInBucket = this.GetIndexInBucket(index);
				int num;
				if (i == this.m_buckets.Count - 1)
				{
					num = this.GetIndexInBucket(this.m_count);
				}
				else
				{
					num = this.m_bucketSize - 1;
				}
				IReference<StorableArray> reference = this.m_buckets[i];
				using (reference.PinValue())
				{
					object[] array2 = reference.Value().Array;
					obj2 = array2[num];
					Array.Copy(array2, indexInBucket, array2, indexInBucket + 1, num - indexInBucket);
					array2[indexInBucket] = obj;
				}
				index = 0;
			}
			IL_012F:
			this.m_count++;
			this.m_version++;
		}

		// Token: 0x06007897 RID: 30871 RVA: 0x001F0D8C File Offset: 0x001EEF8C
		public void RemoveAt(int index)
		{
			this.RemoveRange(index, 1);
		}

		// Token: 0x17002809 RID: 10249
		public T this[int index]
		{
			get
			{
				this.CheckIndex(index, this.m_count - 1);
				if (this.m_array != null)
				{
					using (this.m_array.PinValue())
					{
						return (T)((object)this.m_array.Value().Array[index]);
					}
				}
				IReference<StorableArray> reference = this.m_buckets[this.GetBucketIndex(index)];
				T valueAt;
				using (reference.PinValue())
				{
					StorableArray storableArray = reference.Value();
					valueAt = this.GetValueAt(index, storableArray);
				}
				return valueAt;
			}
			set
			{
				this.CheckReadOnly("set value");
				this.SetValue(index, value, false);
			}
		}

		// Token: 0x0600789A RID: 30874 RVA: 0x001F0E58 File Offset: 0x001EF058
		private T GetValueAt(int index, StorableArray bucket)
		{
			object obj = bucket.Array[this.GetIndexInBucket(index)];
			if (obj == null)
			{
				return default(T);
			}
			return (T)((object)obj);
		}

		// Token: 0x0600789B RID: 30875 RVA: 0x001F0E88 File Offset: 0x001EF088
		private void SetValue(int index, T value, bool fromAdd)
		{
			this.CheckIndex(index, this.m_count - 1);
			if (this.m_array != null)
			{
				using (this.m_array.PinValue())
				{
					this.m_array.Value().Array[index] = value;
					if (fromAdd)
					{
						this.m_array.UpdateSize(ItemSizes.SizeOfInObjectArray(value));
					}
					goto IL_00B3;
				}
			}
			IReference<StorableArray> reference = this.m_buckets[this.GetBucketIndex(index)];
			using (reference.PinValue())
			{
				reference.Value().Array[this.GetIndexInBucket(index)] = value;
				if (fromAdd)
				{
					reference.UpdateSize(ItemSizes.SizeOfInObjectArray(value));
				}
			}
			IL_00B3:
			this.m_version++;
		}

		// Token: 0x0600789C RID: 30876 RVA: 0x001F0F74 File Offset: 0x001EF174
		public void SetValueWithExtension(int index, T item)
		{
			this.CheckReadOnly("SetValueWithExtension");
			int num = Math.Max(this.m_count, index + 1);
			this.EnsureCapacity(num);
			this.m_count = num;
			this.SetValue(index, item, true);
		}

		// Token: 0x0600789D RID: 30877 RVA: 0x001F0FB4 File Offset: 0x001EF1B4
		public IDisposable GetAndPin(int index, out T item)
		{
			this.CheckIndex(index, this.m_count - 1);
			if (this.m_array != null)
			{
				this.m_array.PinValue();
				item = (T)((object)this.m_array.Value().Array[index]);
				return (IDisposable)this.m_array;
			}
			IReference<StorableArray> reference = this.m_buckets[this.GetBucketIndex(index)];
			reference.PinValue();
			StorableArray storableArray = reference.Value();
			item = this.GetValueAt(index, storableArray);
			return (IDisposable)reference;
		}

		// Token: 0x0600789E RID: 30878 RVA: 0x001F1040 File Offset: 0x001EF240
		public IDisposable AddAndPin(T item)
		{
			this.CheckReadOnly("AddAndPin");
			this.EnsureCapacity(this.m_count + 1);
			this.m_count++;
			this.m_version++;
			IDisposable disposable = this.SetAndPin(this.m_count - 1, item, true);
			this.CheckFilledBucket();
			return disposable;
		}

		// Token: 0x0600789F RID: 30879 RVA: 0x001F1098 File Offset: 0x001EF298
		public IDisposable SetAndPin(int index, T item)
		{
			this.CheckReadOnly("SetAndPin");
			return this.SetAndPin(index, item, false);
		}

		// Token: 0x060078A0 RID: 30880 RVA: 0x001F10B0 File Offset: 0x001EF2B0
		private IDisposable SetAndPin(int index, T item, bool fromAdd)
		{
			this.CheckIndex(index, this.m_count - 1);
			IDisposable disposable;
			if (this.m_array != null)
			{
				disposable = this.m_array.PinValue();
				this.m_array.Value().Array[index] = item;
				if (fromAdd)
				{
					this.m_array.UpdateSize(ItemSizes.SizeOfInObjectArray(item));
				}
			}
			else
			{
				int bucketIndex = this.GetBucketIndex(index);
				IReference<StorableArray> reference = this.m_buckets[bucketIndex];
				UnPinCascadeHolder unPinCascadeHolder = new UnPinCascadeHolder();
				unPinCascadeHolder.AddCleanupRef(reference.PinValue());
				this.m_buckets.PinContainingBucket(bucketIndex, unPinCascadeHolder);
				disposable = unPinCascadeHolder;
				reference.Value().Array[this.GetIndexInBucket(index)] = item;
				if (fromAdd)
				{
					reference.UpdateSize(ItemSizes.SizeOfInObjectArray(item));
				}
			}
			return disposable;
		}

		// Token: 0x060078A1 RID: 30881 RVA: 0x001F1178 File Offset: 0x001EF378
		public void RemoveRange(int index, int count)
		{
			this.CheckReadOnly("RemoveRange");
			if (index < 0)
			{
				Global.Tracer.Assert(false, "ScalableList.RemoveRange: Index may not be less than 0");
			}
			if (count < 0)
			{
				Global.Tracer.Assert(false, "ScalableList.RemoveRange: Count may not be less than 0");
			}
			if (index + count > this.Count)
			{
				Global.Tracer.Assert(false, "ScalableList.RemoveRange: Index + Count may not be larger than the number of elements in the list");
			}
			if (this.m_array != null)
			{
				using (this.m_array.PinValue())
				{
					object[] array = this.m_array.Value().Array;
					int num = array.Length - count - index;
					Array.Copy(array, index + count, array, index, num);
				}
				this.m_count -= count;
			}
			else
			{
				int num2 = index + count;
				int num3 = index;
				for (;;)
				{
					int indexInBucket = this.GetIndexInBucket(num2);
					int indexInBucket2 = this.GetIndexInBucket(num3);
					int num4 = Math.Min(this.m_bucketSize - indexInBucket2, Math.Min(this.m_bucketSize - indexInBucket, this.m_count - num2));
					if (num4 <= 0)
					{
						break;
					}
					IReference<StorableArray> reference = this.m_buckets[this.GetBucketIndex(num3)];
					IReference<StorableArray> reference2 = this.m_buckets[this.GetBucketIndex(num2)];
					using (reference.PinValue())
					{
						using (reference2.PinValue())
						{
							Array array2 = reference2.Value().Array;
							object[] array3 = reference.Value().Array;
							Array.Copy(array2, indexInBucket, array3, indexInBucket2, num4);
						}
					}
					num3 += num4;
					num2 += num4;
				}
				this.m_count -= count;
				int num5 = this.GetBucketIndex(this.m_count);
				if (this.m_count % this.m_bucketSize != 0 || num5 == 0)
				{
					num5++;
				}
				int num6 = this.m_buckets.Count - num5;
				if (num6 > 0)
				{
					this.m_buckets.RemoveRange(num5, num6);
					this.m_capacity -= num6 * this.m_bucketSize;
				}
			}
			this.m_version++;
		}

		// Token: 0x060078A2 RID: 30882 RVA: 0x001F13A4 File Offset: 0x001EF5A4
		public int BinarySearch(T value, IComparer comparer)
		{
			if (comparer == null)
			{
				Global.Tracer.Assert(false, "Cannot pass null comparer to BinarySearch");
			}
			if (this.m_array != null)
			{
				return Array.BinarySearch(this.m_array.Value().Array, 0, this.Count, value, comparer);
			}
			return ArrayList.Adapter(this).BinarySearch(value, comparer);
		}

		// Token: 0x060078A3 RID: 30883 RVA: 0x001F1402 File Offset: 0x001EF602
		int IList.Add(object value)
		{
			int count = this.Count;
			this.Add((T)((object)value));
			return count;
		}

		// Token: 0x060078A4 RID: 30884 RVA: 0x001F1416 File Offset: 0x001EF616
		bool IList.Contains(object value)
		{
			return this.Contains((T)((object)value));
		}

		// Token: 0x060078A5 RID: 30885 RVA: 0x001F1424 File Offset: 0x001EF624
		int IList.IndexOf(object value)
		{
			return this.IndexOf((T)((object)value));
		}

		// Token: 0x060078A6 RID: 30886 RVA: 0x001F1432 File Offset: 0x001EF632
		void IList.Insert(int index, object value)
		{
			this.Insert(index, (T)((object)value));
		}

		// Token: 0x1700280A RID: 10250
		// (get) Token: 0x060078A7 RID: 30887 RVA: 0x001F1441 File Offset: 0x001EF641
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060078A8 RID: 30888 RVA: 0x001F1444 File Offset: 0x001EF644
		void IList.Remove(object value)
		{
			this.Remove((T)((object)value));
		}

		// Token: 0x1700280B RID: 10251
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				this[index] = (T)((object)value);
			}
		}

		// Token: 0x060078AB RID: 30891 RVA: 0x001F1470 File Offset: 0x001EF670
		void ICollection.CopyTo(Array array, int index)
		{
			this.InternalCopyTo(array, index);
		}

		// Token: 0x1700280C RID: 10252
		// (get) Token: 0x060078AC RID: 30892 RVA: 0x001F147A File Offset: 0x001EF67A
		int ICollection.Count
		{
			get
			{
				return this.Count;
			}
		}

		// Token: 0x1700280D RID: 10253
		// (get) Token: 0x060078AD RID: 30893 RVA: 0x001F1482 File Offset: 0x001EF682
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700280E RID: 10254
		// (get) Token: 0x060078AE RID: 30894 RVA: 0x001F1485 File Offset: 0x001EF685
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x060078AF RID: 30895 RVA: 0x001F1488 File Offset: 0x001EF688
		private void CheckIndex(int index, int inclusiveLimit)
		{
			if (index < 0 || index > inclusiveLimit)
			{
				Global.Tracer.Assert(false, "ScalableList: Index {0} outside the allowed range [0::{1}]", new object[] { index, inclusiveLimit });
			}
		}

		// Token: 0x060078B0 RID: 30896 RVA: 0x001F14BA File Offset: 0x001EF6BA
		private void CheckReadOnly(string operation)
		{
			if (this.m_isReadOnly)
			{
				Global.Tracer.Assert(false, "Cannot {0} on a read-only ScalableList", new object[] { operation });
			}
		}

		// Token: 0x060078B1 RID: 30897 RVA: 0x001F14DE File Offset: 0x001EF6DE
		private int GetBucketIndex(int index)
		{
			return index / this.m_bucketSize;
		}

		// Token: 0x060078B2 RID: 30898 RVA: 0x001F14E8 File Offset: 0x001EF6E8
		private int GetIndexInBucket(int index)
		{
			return index % this.m_bucketSize;
		}

		// Token: 0x060078B3 RID: 30899 RVA: 0x001F14F4 File Offset: 0x001EF6F4
		private void EnsureCapacity(int count)
		{
			if (count <= this.m_capacity)
			{
				return;
			}
			if (this.m_array == null && this.m_buckets == null)
			{
				StorableArray storableArray = new StorableArray();
				storableArray.Array = new object[count];
				int emptySize = storableArray.EmptySize;
				if (this.m_bucketPinState == ScalableList<T>.BucketPinState.UntilBucketFull || this.m_bucketPinState == ScalableList<T>.BucketPinState.UntilListEnd)
				{
					this.m_array = this.m_cache.AllocateAndPin<StorableArray>(storableArray, this.m_priority, emptySize);
				}
				else
				{
					this.m_array = this.m_cache.Allocate<StorableArray>(storableArray, this.m_priority, emptySize);
				}
				this.m_capacity = count;
			}
			if (this.m_array != null)
			{
				if (count <= this.m_bucketSize)
				{
					int num = Math.Min(Math.Max(count, this.m_capacity * 2), this.m_bucketSize);
					using (this.m_array.PinValue())
					{
						Array.Resize<object>(ref this.m_array.Value().Array, num);
					}
					this.m_capacity = num;
				}
				else
				{
					if (this.m_capacity < this.m_bucketSize)
					{
						using (this.m_array.PinValue())
						{
							Array.Resize<object>(ref this.m_array.Value().Array, this.m_bucketSize);
						}
						this.m_capacity = this.m_bucketSize;
					}
					this.m_buckets = new ScalableList<IReference<StorableArray>>(this.m_priority, this.m_cache, 100, 10, this.m_bucketPinState == ScalableList<T>.BucketPinState.UntilListEnd);
					this.m_buckets.Add(this.m_array);
					this.m_array = null;
				}
			}
			if (this.m_buckets != null)
			{
				while (this.GetBucketIndex(count - 1) >= this.m_buckets.Count)
				{
					StorableArray storableArray2 = new StorableArray();
					storableArray2.Array = new object[this.m_bucketSize];
					int emptySize2 = storableArray2.EmptySize;
					if (this.m_bucketPinState == ScalableList<T>.BucketPinState.UntilListEnd)
					{
						IReference<StorableArray> reference = this.m_cache.AllocateAndPin<StorableArray>(storableArray2, this.m_priority, emptySize2);
						this.m_buckets.Add(reference);
					}
					else if (this.m_bucketPinState == ScalableList<T>.BucketPinState.UntilBucketFull)
					{
						IReference<StorableArray> reference = this.m_cache.AllocateAndPin<StorableArray>(storableArray2, this.m_priority, emptySize2);
						this.m_buckets.AddAndPin(reference);
					}
					else
					{
						IReference<StorableArray> reference = this.m_cache.Allocate<StorableArray>(storableArray2, this.m_priority, emptySize2);
						this.m_buckets.Add(reference);
					}
					this.m_capacity += this.m_bucketSize;
				}
			}
		}

		// Token: 0x060078B4 RID: 30900 RVA: 0x001F1770 File Offset: 0x001EF970
		private void UnPinContainingBucket(int index)
		{
			if (this.m_array != null)
			{
				this.m_array.UnPinValue();
				return;
			}
			int bucketIndex = this.GetBucketIndex(index);
			this.m_buckets[bucketIndex].UnPinValue();
			this.m_buckets.UnPinContainingBucket(bucketIndex);
		}

		// Token: 0x060078B5 RID: 30901 RVA: 0x001F17B8 File Offset: 0x001EF9B8
		private void PinContainingBucket(int index, UnPinCascadeHolder cascadeHolder)
		{
			if (this.m_array != null)
			{
				cascadeHolder.AddCleanupRef(this.m_array.PinValue());
				return;
			}
			int bucketIndex = this.GetBucketIndex(index);
			cascadeHolder.AddCleanupRef(this.m_buckets[bucketIndex].PinValue());
			this.m_buckets.PinContainingBucket(bucketIndex, cascadeHolder);
		}

		// Token: 0x060078B6 RID: 30902 RVA: 0x001F180C File Offset: 0x001EFA0C
		private void InternalCopyTo(Array array, int arrayIndex)
		{
			if (array == null)
			{
				Global.Tracer.Assert(false, "ScalableList.CopyTo: Dest array cannot be null");
			}
			if (arrayIndex < 0)
			{
				Global.Tracer.Assert(false, "ScalableList.CopyTo: Index must not be less than 0");
			}
			if (arrayIndex != 1)
			{
				Global.Tracer.Assert(false, "ScalableList.CopyTo: Array must be one-dimensional");
			}
			if (arrayIndex > array.Length - 1)
			{
				Global.Tracer.Assert(false, "ScalableList.CopyTo: Start index must be less than the size of the array");
			}
			if (arrayIndex + this.m_count > array.Length)
			{
				Global.Tracer.Assert(false, "ScalableList.CopyTo: Insufficent space in the target array");
			}
			if (this.m_array != null)
			{
				using (this.m_array.PinValue())
				{
					Array.Copy(this.m_array.Value().Array, 0, array, arrayIndex, this.m_count);
					return;
				}
			}
			int num = this.m_buckets.Count - 1;
			IReference<StorableArray> reference;
			for (int i = 0; i < num; i++)
			{
				reference = this.m_buckets[i];
				using (reference.PinValue())
				{
					Array.Copy(reference.Value().Array, 0, array, arrayIndex + i * this.m_bucketSize, this.m_bucketSize);
				}
			}
			int num2 = this.GetIndexInBucket(this.m_count);
			if (num2 == 0)
			{
				num2 = this.m_bucketSize;
			}
			reference = this.m_buckets[num];
			using (reference.PinValue())
			{
				Array.Copy(reference.Value().Array, 0, array, arrayIndex + num * this.m_bucketSize, num2);
			}
		}

		// Token: 0x060078B7 RID: 30903 RVA: 0x001F19B0 File Offset: 0x001EFBB0
		public void Add(T item)
		{
			this.CheckReadOnly("Add");
			this.EnsureCapacity(this.m_count + 1);
			this.m_count++;
			this.m_version++;
			this.SetValue(this.m_count - 1, item, true);
			this.CheckFilledBucket();
		}

		// Token: 0x060078B8 RID: 30904 RVA: 0x001F1A08 File Offset: 0x001EFC08
		private void CheckFilledBucket()
		{
			if (this.m_bucketPinState == ScalableList<T>.BucketPinState.UntilBucketFull && this.m_count % this.m_bucketSize == 0)
			{
				if (this.m_array != null)
				{
					this.m_array.UnPinValue();
					return;
				}
				int bucketIndex = this.GetBucketIndex(this.m_count - 1);
				this.m_buckets[bucketIndex].UnPinValue();
				this.m_buckets.UnPinContainingBucket(bucketIndex);
			}
		}

		// Token: 0x060078B9 RID: 30905 RVA: 0x001F1A70 File Offset: 0x001EFC70
		public void AddRange(IEnumerable<T> collection)
		{
			if (collection == null)
			{
				Global.Tracer.Assert(false, "ScalableList.AddRange: Collection cannot be null");
			}
			foreach (T t in collection)
			{
				this.Add(t);
			}
		}

		// Token: 0x060078BA RID: 30906 RVA: 0x001F1ACC File Offset: 0x001EFCCC
		public void AddRange(IList<T> list)
		{
			this.CheckReadOnly("AddRange");
			if (list == null)
			{
				Global.Tracer.Assert(false, "ScalableList.AddRange(IList<T>): List to add may not be null");
			}
			this.EnsureCapacity(this.m_count + list.Count);
			for (int i = 0; i < list.Count; i++)
			{
				this.Add(list[i]);
			}
		}

		// Token: 0x060078BB RID: 30907 RVA: 0x001F1B28 File Offset: 0x001EFD28
		public void Clear()
		{
			this.CheckReadOnly("Clear");
			this.m_count = 0;
			if (this.m_array != null)
			{
				this.m_array.Free();
				this.m_array = null;
			}
			if (this.m_buckets != null)
			{
				int count = this.m_buckets.Count;
				for (int i = 0; i < count; i++)
				{
					this.m_buckets[i].Free();
				}
				this.m_buckets.Clear();
			}
			this.m_buckets = null;
			this.m_capacity = 0;
			this.m_version++;
		}

		// Token: 0x060078BC RID: 30908 RVA: 0x001F1BBC File Offset: 0x001EFDBC
		public void UnPinAll()
		{
			if (this.m_bucketPinState == ScalableList<T>.BucketPinState.UntilListEnd)
			{
				if (this.m_array != null)
				{
					this.m_array.UnPinValue();
				}
				if (this.m_buckets != null)
				{
					for (int i = 0; i < this.m_buckets.Count; i++)
					{
						this.m_buckets[i].UnPinValue();
					}
					this.m_buckets.UnPinAll();
					return;
				}
			}
			else if (this.m_bucketPinState == ScalableList<T>.BucketPinState.UntilBucketFull)
			{
				if (this.m_count < Math.Max(this.m_capacity, this.m_bucketSize))
				{
					if (this.m_array != null)
					{
						this.m_array.UnPinValue();
					}
					else
					{
						int bucketIndex = this.GetBucketIndex(this.m_count - 1);
						this.m_buckets[bucketIndex].UnPinValue();
						this.m_buckets.UnPinContainingBucket(bucketIndex);
					}
				}
				if (this.m_buckets != null)
				{
					this.m_buckets.UnPinAll();
				}
			}
		}

		// Token: 0x060078BD RID: 30909 RVA: 0x001F1C99 File Offset: 0x001EFE99
		public void TransferTo(IScalabilityCache scaleCache)
		{
			if (this.m_array != null)
			{
				this.m_array = (IReference<StorableArray>)this.m_array.TransferTo(scaleCache);
			}
			else
			{
				this.m_buckets.TransferTo(scaleCache);
			}
			this.m_cache = scaleCache;
		}

		// Token: 0x060078BE RID: 30910 RVA: 0x001F1CCF File Offset: 0x001EFECF
		public bool Contains(T item)
		{
			return this.IndexOf(item) != -1;
		}

		// Token: 0x060078BF RID: 30911 RVA: 0x001F1CDE File Offset: 0x001EFEDE
		public void CopyTo(T[] array, int arrayIndex)
		{
			this.InternalCopyTo(array, arrayIndex);
		}

		// Token: 0x1700280F RID: 10255
		// (get) Token: 0x060078C0 RID: 30912 RVA: 0x001F1CE8 File Offset: 0x001EFEE8
		public int Count
		{
			get
			{
				return this.m_count;
			}
		}

		// Token: 0x17002810 RID: 10256
		// (get) Token: 0x060078C1 RID: 30913 RVA: 0x001F1CF0 File Offset: 0x001EFEF0
		public int Capacity
		{
			get
			{
				return this.m_capacity;
			}
		}

		// Token: 0x17002811 RID: 10257
		// (get) Token: 0x060078C2 RID: 30914 RVA: 0x001F1CF8 File Offset: 0x001EFEF8
		public bool IsReadOnly
		{
			get
			{
				return this.m_isReadOnly;
			}
		}

		// Token: 0x060078C3 RID: 30915 RVA: 0x001F1D00 File Offset: 0x001EFF00
		public void SetReadOnly()
		{
			this.m_isReadOnly = true;
		}

		// Token: 0x060078C4 RID: 30916 RVA: 0x001F1D0C File Offset: 0x001EFF0C
		public bool Remove(T item)
		{
			this.CheckReadOnly("Remove");
			EqualityComparer<T> @default = EqualityComparer<T>.Default;
			if (this.m_array != null)
			{
				using (this.m_array.PinValue())
				{
					object[] array = this.m_array.Value().Array;
					return this.Remove(item, 0, array, this.m_count, @default);
				}
			}
			bool flag = false;
			int count = this.m_buckets.Count;
			int num = 0;
			while (num < count && !flag)
			{
				IReference<StorableArray> reference = this.m_buckets[num];
				using (reference.PinValue())
				{
					int num2;
					if (num == count - 1)
					{
						num2 = this.m_count - num * this.m_bucketSize;
					}
					else
					{
						num2 = this.m_bucketSize;
					}
					object[] array2 = reference.Value().Array;
					flag = this.Remove(item, num * this.m_bucketSize, array2, num2, @default);
				}
				num++;
			}
			return flag;
		}

		// Token: 0x060078C5 RID: 30917 RVA: 0x001F1E20 File Offset: 0x001F0020
		private bool Remove(T item, int baseIndex, object[] array, int limit, IEqualityComparer<T> comparer)
		{
			for (int i = 0; i < limit; i++)
			{
				if (comparer.Equals(item, (T)((object)array[i])))
				{
					this.RemoveAt(baseIndex + i);
					return true;
				}
			}
			return false;
		}

		// Token: 0x060078C6 RID: 30918 RVA: 0x001F1E58 File Offset: 0x001F0058
		public IEnumerator<T> GetEnumerator()
		{
			return new ScalableList<T>.ScalableListEnumerator(this);
		}

		// Token: 0x060078C7 RID: 30919 RVA: 0x001F1E65 File Offset: 0x001F0065
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new ScalableList<T>.ScalableListEnumerator(this);
		}

		// Token: 0x060078C8 RID: 30920 RVA: 0x001F1E72 File Offset: 0x001F0072
		public void Dispose()
		{
			this.Clear();
		}

		// Token: 0x060078C9 RID: 30921 RVA: 0x001F1E7C File Offset: 0x001F007C
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(ScalableList<T>.m_declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.Count)
				{
					if (memberName == MemberName.Capacity)
					{
						writer.Write(this.m_capacity);
						continue;
					}
					if (memberName == MemberName.Count)
					{
						writer.Write(this.m_count);
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.Priority)
					{
						writer.Write(this.m_priority);
						continue;
					}
					switch (memberName)
					{
					case MemberName.Array:
						writer.Write(this.m_array);
						continue;
					case MemberName.BucketSize:
						writer.Write(this.m_bucketSize);
						continue;
					case MemberName.Buckets:
						writer.Write(this.m_buckets);
						continue;
					case MemberName.Version:
						writer.Write(this.m_version);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x060078CA RID: 30922 RVA: 0x001F1F6C File Offset: 0x001F016C
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(ScalableList<T>.m_declaration);
			this.m_cache = reader.PersistenceHelper as IScalabilityCache;
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.Count)
				{
					if (memberName == MemberName.Capacity)
					{
						this.m_capacity = reader.ReadInt32();
						continue;
					}
					if (memberName == MemberName.Count)
					{
						this.m_count = reader.ReadInt32();
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.Priority)
					{
						this.m_priority = reader.ReadInt32();
						continue;
					}
					switch (memberName)
					{
					case MemberName.Array:
						this.m_array = (IReference<StorableArray>)reader.ReadRIFObject();
						continue;
					case MemberName.BucketSize:
						this.m_bucketSize = reader.ReadInt32();
						continue;
					case MemberName.Buckets:
						this.m_buckets = reader.ReadRIFObject<ScalableList<IReference<StorableArray>>>();
						continue;
					case MemberName.Version:
						this.m_version = reader.ReadInt32();
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x060078CB RID: 30923 RVA: 0x001F2075 File Offset: 0x001F0275
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
		}

		// Token: 0x060078CC RID: 30924 RVA: 0x001F2077 File Offset: 0x001F0277
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScalableList;
		}

		// Token: 0x060078CD RID: 30925 RVA: 0x001F207C File Offset: 0x001F027C
		public static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			if (ScalableList<T>.m_declaration == null)
			{
				return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScalableList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
				{
					new MemberInfo(MemberName.BucketSize, Token.Int32),
					new MemberInfo(MemberName.Count, Token.Int32),
					new MemberInfo(MemberName.Capacity, Token.Int32),
					new MemberInfo(MemberName.Buckets, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScalableList),
					new MemberInfo(MemberName.Array, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.StorableArrayReference),
					new MemberInfo(MemberName.Version, Token.Int32),
					new MemberInfo(MemberName.Priority, Token.Int32)
				});
			}
			return ScalableList<T>.m_declaration;
		}

		// Token: 0x17002812 RID: 10258
		// (get) Token: 0x060078CE RID: 30926 RVA: 0x001F2132 File Offset: 0x001F0332
		public int Size
		{
			get
			{
				return 12 + ItemSizes.SizeOf<IReference<StorableArray>>(this.m_buckets) + ItemSizes.SizeOf(this.m_array) + 4 + 4;
			}
		}

		// Token: 0x060078CF RID: 30927 RVA: 0x001F2152 File Offset: 0x001F0352
		public void DisableStorageUpdates()
		{
			this.m_cache.DisableStorageUpdates();
		}

		// Token: 0x04003CA6 RID: 15526
		private IScalabilityCache m_cache;

		// Token: 0x04003CA7 RID: 15527
		private int m_bucketSize;

		// Token: 0x04003CA8 RID: 15528
		private int m_count;

		// Token: 0x04003CA9 RID: 15529
		private int m_capacity;

		// Token: 0x04003CAA RID: 15530
		private int m_priority;

		// Token: 0x04003CAB RID: 15531
		private ScalableList<IReference<StorableArray>> m_buckets;

		// Token: 0x04003CAC RID: 15532
		private IReference<StorableArray> m_array;

		// Token: 0x04003CAD RID: 15533
		private int m_version;

		// Token: 0x04003CAE RID: 15534
		private bool m_isReadOnly;

		// Token: 0x04003CAF RID: 15535
		private ScalableList<T>.BucketPinState m_bucketPinState;

		// Token: 0x04003CB0 RID: 15536
		private static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_declaration = ScalableList<T>.GetDeclaration();

		// Token: 0x02000D10 RID: 3344
		private enum BucketPinState
		{
			// Token: 0x0400503E RID: 20542
			None,
			// Token: 0x0400503F RID: 20543
			UntilBucketFull,
			// Token: 0x04005040 RID: 20544
			UntilListEnd
		}

		// Token: 0x02000D11 RID: 3345
		private struct ScalableListEnumerator : IEnumerator<T>, IDisposable, IEnumerator
		{
			// Token: 0x06008EBF RID: 36543 RVA: 0x00245BE6 File Offset: 0x00243DE6
			internal ScalableListEnumerator(ScalableList<T> list)
			{
				this.m_list = list;
				this.m_version = list.m_version;
				this.m_currentIndex = -1;
				this.Reset();
			}

			// Token: 0x17002BC7 RID: 11207
			// (get) Token: 0x06008EC0 RID: 36544 RVA: 0x00245C08 File Offset: 0x00243E08
			public T Current
			{
				get
				{
					if (this.m_list.m_version != this.m_version)
					{
						Global.Tracer.Assert(false, "ScalableListEnumerator: Cannot use enumerator after modifying the underlying collection");
					}
					if (this.m_currentIndex < 0 || this.m_currentIndex > this.m_list.Count)
					{
						Global.Tracer.Assert(false, "ScalableListEnumerator: Enumerator beyond the bounds of the underlying collection");
					}
					return this.m_list[this.m_currentIndex];
				}
			}

			// Token: 0x06008EC1 RID: 36545 RVA: 0x00245C75 File Offset: 0x00243E75
			public void Dispose()
			{
			}

			// Token: 0x17002BC8 RID: 11208
			// (get) Token: 0x06008EC2 RID: 36546 RVA: 0x00245C77 File Offset: 0x00243E77
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06008EC3 RID: 36547 RVA: 0x00245C84 File Offset: 0x00243E84
			public bool MoveNext()
			{
				if (this.m_list.m_version != this.m_version)
				{
					Global.Tracer.Assert(false, "ScalableListEnumerator: Cannot use enumerator after modifying the underlying collection");
				}
				if (this.m_currentIndex < this.m_list.Count - 1)
				{
					this.m_currentIndex++;
					return true;
				}
				return false;
			}

			// Token: 0x06008EC4 RID: 36548 RVA: 0x00245CDA File Offset: 0x00243EDA
			public void Reset()
			{
				this.m_currentIndex = -1;
				this.m_version = this.m_list.m_version;
			}

			// Token: 0x04005041 RID: 20545
			private int m_currentIndex;

			// Token: 0x04005042 RID: 20546
			private ScalableList<T> m_list;

			// Token: 0x04005043 RID: 20547
			private int m_version;
		}
	}
}
