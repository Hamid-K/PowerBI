using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000251 RID: 593
	[DataContract(Name = "EvictionBatch", Namespace = "http://schemas.microsoft.com/velocity/namespace")]
	internal class EvictionReplicationBatch : IBatchOperation, IEnumerable<EvictedElement>, IEnumerable
	{
		// Token: 0x060013D1 RID: 5073 RVA: 0x0003E048 File Offset: 0x0003C248
		public EvictionReplicationBatch(int capacity, int numRegions)
		{
			ReleaseAssert.IsTrue(capacity > 0);
			ReleaseAssert.IsTrue(capacity <= 256);
			this._capacity = capacity;
			this._regionIds = new byte[capacity];
			this._versions = new InternalCacheItemVersion[capacity];
			this._keys = new Key[capacity];
			this._regionNames = new List<string>(numRegions);
			this._cacheItems = new AOMCacheItem[capacity];
			this._ignoreIds = new BitArray(capacity);
		}

		// Token: 0x060013D2 RID: 5074 RVA: 0x0003E0C4 File Offset: 0x0003C2C4
		public bool Add(EvictedElement elem)
		{
			if (this._count < this._capacity)
			{
				int num = this.FindRegionIndex(elem.RegionName);
				if (num < 0)
				{
					this._regionNames.Add(elem.RegionName);
					num = this._regionNames.Count - 1;
					this._lastRegionIndex = num;
				}
				this._regionIds[this._count] = (byte)num;
				this._versions[this._count] = elem.Version;
				this._keys[this._count] = elem.Key;
				this._cacheItems[this._count] = elem.CacheItem;
				this._count++;
				if (elem.CacheItem != null && elem.CacheItem.IsItemExpiredForEviction())
				{
					this._expiredCount++;
				}
				return true;
			}
			return false;
		}

		// Token: 0x060013D3 RID: 5075 RVA: 0x0003E1A8 File Offset: 0x0003C3A8
		public void Prune()
		{
			if (this._count < this._capacity)
			{
				Key[] array = new Key[this._count];
				InternalCacheItemVersion[] array2 = null;
				InternalCacheItemVersion[] versions = this._versions;
				if (versions != null)
				{
					array2 = new InternalCacheItemVersion[this._count];
				}
				AOMCacheItem[] array3 = new AOMCacheItem[this._count];
				byte[] array4 = new byte[this._count];
				for (int i = 0; i < this._count; i++)
				{
					array[i] = this._keys[i];
					if (versions != null)
					{
						array2[i] = versions[i];
					}
					array3[i] = this._cacheItems[i];
					array4[i] = this._regionIds[i];
				}
				this._keys = array;
				this._versions = array2;
				this._cacheItems = array3;
				this._regionIds = array4;
				this._capacity = this._count;
			}
		}

		// Token: 0x060013D4 RID: 5076 RVA: 0x0003E288 File Offset: 0x0003C488
		private int FindRegionIndex(string regionName)
		{
			if (this._lastRegionIndex < this._regionNames.Count && this._regionNames[this._lastRegionIndex].Equals(regionName))
			{
				return this._lastRegionIndex;
			}
			return this._regionNames.IndexOf(regionName);
		}

		// Token: 0x17000429 RID: 1065
		public EvictedElement this[int i]
		{
			get
			{
				EvictedElement evictedElement = default(EvictedElement);
				if (i < this._count && i >= 0)
				{
					evictedElement.index = i;
					evictedElement.RegionName = this.GetRegionName(i);
					evictedElement.Version = this.GetVersion(i);
					evictedElement.Key = this.GetKey(i);
					evictedElement.CacheItem = this.GetCacheItem(i);
				}
				return evictedElement;
			}
		}

		// Token: 0x060013D6 RID: 5078 RVA: 0x0003E337 File Offset: 0x0003C537
		private Key GetKey(int i)
		{
			return this._keys[i];
		}

		// Token: 0x060013D7 RID: 5079 RVA: 0x0003E344 File Offset: 0x0003C544
		private InternalCacheItemVersion GetVersion(int i)
		{
			InternalCacheItemVersion[] versions = this._versions;
			if (versions != null)
			{
				return versions[i];
			}
			return InternalCacheItemVersion.Null;
		}

		// Token: 0x060013D8 RID: 5080 RVA: 0x0003E370 File Offset: 0x0003C570
		private string GetRegionName(int i)
		{
			int num = (int)this._regionIds[i];
			if (num < this._regionNames.Count)
			{
				return this._regionNames[num];
			}
			return null;
		}

		// Token: 0x060013D9 RID: 5081 RVA: 0x0003E3A2 File Offset: 0x0003C5A2
		private AOMCacheItem GetCacheItem(int i)
		{
			if (this._cacheItems != null && this._cacheItems.Length > i)
			{
				return this._cacheItems[i];
			}
			return null;
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x060013DA RID: 5082 RVA: 0x0003E3C1 File Offset: 0x0003C5C1
		// (set) Token: 0x060013DB RID: 5083 RVA: 0x0003E3C9 File Offset: 0x0003C5C9
		public AOMCacheItem[] CacheItems
		{
			get
			{
				return this._cacheItems;
			}
			set
			{
				this._cacheItems = value;
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x060013DC RID: 5084 RVA: 0x0003E3D2 File Offset: 0x0003C5D2
		// (set) Token: 0x060013DD RID: 5085 RVA: 0x0003E3DA File Offset: 0x0003C5DA
		public bool GroupBeforeEnumerating
		{
			get
			{
				return this._groupBeforeEnumerate;
			}
			set
			{
				this._groupBeforeEnumerate = value;
			}
		}

		// Token: 0x060013DE RID: 5086 RVA: 0x0003E3E3 File Offset: 0x0003C5E3
		public bool IsIgnored(int index)
		{
			return this._ignoreIds[index];
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x060013DF RID: 5087 RVA: 0x0003E3F1 File Offset: 0x0003C5F1
		public int Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x060013E0 RID: 5088 RVA: 0x0003E3F9 File Offset: 0x0003C5F9
		public int ExpiredCount
		{
			get
			{
				return this._expiredCount;
			}
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x060013E1 RID: 5089 RVA: 0x0003E401 File Offset: 0x0003C601
		public int Capacity
		{
			get
			{
				return this._capacity;
			}
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x060013E2 RID: 5090 RVA: 0x0003E409 File Offset: 0x0003C609
		internal bool IsGrouped
		{
			get
			{
				return this._isGrouped;
			}
		}

		// Token: 0x060013E3 RID: 5091 RVA: 0x0003E414 File Offset: 0x0003C614
		public IEnumerator<EvictedElement> GetEnumerator()
		{
			if (this._groupBeforeEnumerate)
			{
				ReleaseAssert.IsTrue(this._isGrouped);
			}
			for (int i = 0; i < this.Count; i++)
			{
				if (!this.IsIgnored(i))
				{
					yield return this[i];
				}
			}
			yield break;
		}

		// Token: 0x060013E4 RID: 5092 RVA: 0x0003E430 File Offset: 0x0003C630
		public IEnumerator<EvictedElement> GetEnumerator(string regionName)
		{
			ReleaseAssert.IsTrue(this._isGrouped);
			int regionId = this.RegionNames.IndexOf(regionName);
			if (regionId >= 0)
			{
				int endIndex;
				int startIndex;
				this.GetStartAndEndIndex(regionId, out endIndex, out startIndex);
				for (int i = startIndex; i <= endIndex; i++)
				{
					if (!this.IsIgnored(i))
					{
						yield return this[i];
					}
				}
			}
			yield break;
		}

		// Token: 0x060013E5 RID: 5093 RVA: 0x0003E453 File Offset: 0x0003C653
		private void GetStartAndEndIndex(int regionId, out int endIndex, out int startIndex)
		{
			endIndex = (int)this._regionEndIndices[regionId];
			startIndex = 0;
			if (regionId != 0)
			{
				startIndex = (int)(this._regionEndIndices[regionId - 1] + 1);
			}
		}

		// Token: 0x060013E6 RID: 5094 RVA: 0x0003E473 File Offset: 0x0003C673
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x060013E7 RID: 5095 RVA: 0x0003E47B File Offset: 0x0003C67B
		internal object[] Keys
		{
			get
			{
				return this._keys;
			}
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x060013E8 RID: 5096 RVA: 0x0003E483 File Offset: 0x0003C683
		internal byte[] RegionIds
		{
			get
			{
				return this._regionIds;
			}
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x060013E9 RID: 5097 RVA: 0x0003E48B File Offset: 0x0003C68B
		// (set) Token: 0x060013EA RID: 5098 RVA: 0x0003E493 File Offset: 0x0003C693
		internal InternalCacheItemVersion[] Versions
		{
			get
			{
				return this._versions;
			}
			set
			{
				this._versions = value;
			}
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x060013EB RID: 5099 RVA: 0x0003E49C File Offset: 0x0003C69C
		public List<string> RegionNames
		{
			get
			{
				return this._regionNames;
			}
		}

		// Token: 0x060013EC RID: 5100 RVA: 0x0003E4A4 File Offset: 0x0003C6A4
		internal void Group()
		{
			if (this.Count <= 0)
			{
				this._isGrouped = true;
				return;
			}
			if (!this._isGrouped)
			{
				if (!this.PopulateIndicesIfSorted())
				{
					this.QuickSort(0, this._count - 1);
					this.PopulateIndicesIfSorted();
				}
				this._isGrouped = true;
			}
		}

		// Token: 0x060013ED RID: 5101 RVA: 0x0003E4E4 File Offset: 0x0003C6E4
		private bool PopulateIndicesIfSorted()
		{
			if (this.RegionNames.Count > 0 && this._count > 0)
			{
				this._regionEndIndices = new byte[this.RegionNames.Count];
				int num = (int)this.RegionIds[0];
				for (int i = 1; i < this._count; i++)
				{
					if (num != (int)this.RegionIds[i])
					{
						if (num > (int)this.RegionIds[i])
						{
							return false;
						}
						this._regionEndIndices[num] = (byte)(i - 1);
						num = (int)this.RegionIds[i];
					}
				}
				this._regionEndIndices[num] = (byte)(this._count - 1);
			}
			return true;
		}

		// Token: 0x060013EE RID: 5102 RVA: 0x0003E578 File Offset: 0x0003C778
		private void QuickSort(int p, int r)
		{
			if (r > p)
			{
				int num = this.Partition(p, r);
				this.QuickSort(p, num - 1);
				this.QuickSort(num + 1, r);
			}
		}

		// Token: 0x060013EF RID: 5103 RVA: 0x0003E5A8 File Offset: 0x0003C7A8
		private int Partition(int p, int r)
		{
			int num = p;
			int num2 = r + 1;
			int num3 = (int)this.RegionIds[p];
			for (;;)
			{
				num++;
				if (num >= r || (int)this.RegionIds[num] > num3)
				{
					do
					{
						num2--;
					}
					while ((int)this.RegionIds[num2] > num3);
					if (num < num2)
					{
						this.Exchange(num, num2);
					}
					if (num >= num2)
					{
						break;
					}
				}
			}
			this.Exchange(p, num2);
			return num2;
		}

		// Token: 0x060013F0 RID: 5104 RVA: 0x0003E600 File Offset: 0x0003C800
		private void Exchange(int i, int j)
		{
			if (i != j)
			{
				byte b = this.RegionIds[i];
				this.RegionIds[i] = this.RegionIds[j];
				this.RegionIds[j] = b;
				object obj = this.Keys[i];
				this.Keys[i] = this.Keys[j];
				this.Keys[j] = obj;
				InternalCacheItemVersion[] versions = this.Versions;
				if (versions != null)
				{
					InternalCacheItemVersion internalCacheItemVersion = versions[i];
					versions[i] = versions[j];
					versions[j] = internalCacheItemVersion;
				}
				bool flag = this._ignoreIds[i];
				this._ignoreIds[i] = this._ignoreIds[j];
				this._ignoreIds[j] = flag;
				AOMCacheItem[] cacheItems = this._cacheItems;
				if (cacheItems != null)
				{
					AOMCacheItem aomcacheItem = cacheItems[i];
					cacheItems[i] = cacheItems[j];
					cacheItems[j] = aomcacheItem;
				}
			}
		}

		// Token: 0x060013F1 RID: 5105 RVA: 0x0003E6E8 File Offset: 0x0003C8E8
		public void IgnoreElement(EvictedElement element)
		{
			InternalCacheItemVersion[] versions = this.Versions;
			this._ignoreIds[element.index] = true;
		}

		// Token: 0x060013F2 RID: 5106 RVA: 0x0003E714 File Offset: 0x0003C914
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('{');
			stringBuilder.Append("{ Regions List :");
			for (int i = 0; i < this._regionNames.Count; i++)
			{
				stringBuilder.Append(this._regionNames[i] + "/");
			}
			stringBuilder.AppendLine(" }");
			stringBuilder.Append("[{RegionIndex,Key,Version}] = [");
			InternalCacheItemVersion[] versions = this._versions;
			for (int j = 0; j < this._count; j++)
			{
				if (this._keys[j] != null)
				{
					stringBuilder.Append(string.Concat(new object[]
					{
						"{",
						this._regionIds[j],
						":",
						this._keys[j],
						":"
					}));
					if (versions != null)
					{
						stringBuilder.Append(versions[j]);
					}
					stringBuilder.Append("}");
				}
			}
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x060013F3 RID: 5107 RVA: 0x0003E838 File Offset: 0x0003CA38
		public List<string> GetContainedRegionsList(List<string> regions)
		{
			List<string> list = new List<string>(regions.Count);
			List<string>.Enumerator enumerator = regions.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (this.RegionNames.Contains(enumerator.Current))
				{
					list.Add(enumerator.Current);
				}
			}
			return list;
		}

		// Token: 0x060013F4 RID: 5108 RVA: 0x0003E888 File Offset: 0x0003CA88
		public int GetItemCount(string regionName)
		{
			ReleaseAssert.IsTrue(this._isGrouped);
			int num = this.RegionNames.IndexOf(regionName);
			if (num == -1)
			{
				return 0;
			}
			int num2;
			int num3;
			this.GetStartAndEndIndex(num, out num2, out num3);
			int num4 = 0;
			for (int i = num3; i <= num2; i++)
			{
				if (!this.IsIgnored(i))
				{
					num4++;
				}
			}
			return num4;
		}

		// Token: 0x04000BEF RID: 3055
		[DataMember]
		private List<string> _regionNames;

		// Token: 0x04000BF0 RID: 3056
		[DataMember]
		private byte[] _regionEndIndices;

		// Token: 0x04000BF1 RID: 3057
		private AOMCacheItem[] _cacheItems;

		// Token: 0x04000BF2 RID: 3058
		[DataMember]
		private BitArray _ignoreIds;

		// Token: 0x04000BF3 RID: 3059
		[DataMember]
		private byte[] _regionIds;

		// Token: 0x04000BF4 RID: 3060
		[DataMember]
		private InternalCacheItemVersion[] _versions;

		// Token: 0x04000BF5 RID: 3061
		[DataMember]
		private Key[] _keys;

		// Token: 0x04000BF6 RID: 3062
		[DataMember]
		private int _count;

		// Token: 0x04000BF7 RID: 3063
		private int _expiredCount;

		// Token: 0x04000BF8 RID: 3064
		[DataMember]
		private int _capacity;

		// Token: 0x04000BF9 RID: 3065
		[DataMember]
		private bool _isGrouped;

		// Token: 0x04000BFA RID: 3066
		[DataMember]
		private bool _groupBeforeEnumerate;

		// Token: 0x04000BFB RID: 3067
		private int _lastRegionIndex;
	}
}
