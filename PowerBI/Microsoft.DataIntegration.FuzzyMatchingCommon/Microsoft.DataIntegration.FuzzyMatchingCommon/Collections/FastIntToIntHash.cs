using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using Microsoft.DataIntegration.FuzzyMatchingCommon.IO;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections
{
	// Token: 0x02000080 RID: 128
	[Serializable]
	public sealed class FastIntToIntHash : IRawSerializable, ISerializable, IMemoryUsage, IFastDictionary<int, int>, IEnumerable<KeyValuePair<int, int>>, IEnumerable
	{
		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000576 RID: 1398 RVA: 0x000200B0 File Offset: 0x0001E2B0
		// (set) Token: 0x06000577 RID: 1399 RVA: 0x000200B8 File Offset: 0x0001E2B8
		bool IRawSerializable.EnableRawSerialization { get; set; }

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000578 RID: 1400 RVA: 0x000200C1 File Offset: 0x0001E2C1
		// (set) Token: 0x06000579 RID: 1401 RVA: 0x000200C9 File Offset: 0x0001E2C9
		int IRawSerializable.RawSerializationID { get; set; }

		// Token: 0x0600057A RID: 1402 RVA: 0x000200D2 File Offset: 0x0001E2D2
		public FastIntToIntHash()
			: this(0.75f)
		{
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x000200DF File Offset: 0x0001E2DF
		public FastIntToIntHash(float load)
		{
			this.load = 0.75f;
			base..ctor();
			this.load = Math.Min(load, 0.9f);
			this.buckets = new FastIntToIntHash.Entry[16];
			this.mask = 15;
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x00020118 File Offset: 0x0001E318
		public FastIntToIntHash(SerializationInfo info, StreamingContext context)
		{
			this.load = 0.75f;
			base..ctor();
			((IRawSerializable)this).EnableRawSerialization = (bool)info.GetValue("EnableRawSerialization", typeof(bool));
			((IRawSerializable)this).RawSerializationID = (int)info.GetValue("RawSerializationID", typeof(int));
			this.load = (float)info.GetValue("load", typeof(float));
			this.mask = (int)info.GetValue("mask", typeof(int));
			this.count = (int)info.GetValue("count", typeof(int));
			if (!((IRawSerializable)this).EnableRawSerialization)
			{
				this.buckets = (FastIntToIntHash.Entry[])info.GetValue("buckets", typeof(FastIntToIntHash.Entry[]));
			}
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x00020200 File Offset: 0x0001E400
		[SecurityCritical]
		[SecurityPermission(6, Flags = 128)]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("EnableRawSerialization", ((IRawSerializable)this).EnableRawSerialization);
			info.AddValue("RawSerializationID", ((IRawSerializable)this).RawSerializationID);
			info.AddValue("load", this.load);
			info.AddValue("mask", this.mask);
			info.AddValue("count", this.count);
			if (!((IRawSerializable)this).EnableRawSerialization)
			{
				info.AddValue("buckets", this.buckets);
			}
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x0002027C File Offset: 0x0001E47C
		void IRawSerializable.Serialize(Stream s)
		{
			StreamUtilities.WriteInt64(s, s.Position);
			StreamUtilities.WriteInt32(s, this.buckets.Length);
			for (int i = 0; i < this.buckets.Length; i++)
			{
				StreamUtilities.WriteInt32(s, this.buckets[i].k);
				StreamUtilities.WriteInt32(s, this.buckets[i].v);
			}
			StreamUtilities.WriteInt64(s, s.Position);
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x000202F0 File Offset: 0x0001E4F0
		void IRawSerializable.Deserialize(Stream s)
		{
			if (s.Position != StreamUtilities.ReadInt64(s))
			{
				throw new SerializationException("Unexpected stream position encountered when deserializing.");
			}
			this.buckets = new FastIntToIntHash.Entry[StreamUtilities.ReadInt32(s)];
			for (int i = 0; i < this.buckets.Length; i++)
			{
				this.buckets[i].k = StreamUtilities.ReadInt32(s);
				this.buckets[i].v = StreamUtilities.ReadInt32(s);
			}
			if (s.Position != StreamUtilities.ReadInt64(s))
			{
				throw new SerializationException("Unexpected stream position encountered when deserializing.");
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000580 RID: 1408 RVA: 0x00020381 File Offset: 0x0001E581
		public int DefaultValue
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000581 RID: 1409 RVA: 0x00020384 File Offset: 0x0001E584
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x0002038C File Offset: 0x0001E58C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x00020394 File Offset: 0x0001E594
		public IEnumerator<KeyValuePair<int, int>> GetEnumerator()
		{
			foreach (FastIntToIntHash.Entry entry in this.buckets)
			{
				if (entry.k != 0 && entry.v != 0)
				{
					yield return new KeyValuePair<int, int>(entry.k, entry.v);
				}
			}
			FastIntToIntHash.Entry[] array = null;
			yield break;
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x000203A4 File Offset: 0x0001E5A4
		public void Add(int k, int v)
		{
			if (v == 0)
			{
				throw new ArgumentException("Value may not be zero.");
			}
			if ((float)(this.count + 1) / (float)this.buckets.Length > this.load)
			{
				this.Rehash();
			}
			int num = k & this.mask;
			while (this.buckets[num].v != 0)
			{
				if (this.buckets[num].k == k)
				{
					throw new ArgumentException("Key already present!");
				}
				num = (num + 1) & this.mask;
			}
			this.buckets[num].k = k;
			this.buckets[num].v = v;
			this.count++;
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x0002045C File Offset: 0x0001E65C
		public bool TryAdd(int k, int v)
		{
			if (v == 0)
			{
				throw new ArgumentException("Value may not be zero.");
			}
			if ((float)(this.count + 1) / (float)this.buckets.Length > this.load)
			{
				this.Rehash();
			}
			int num = k & this.mask;
			while (this.buckets[num].v != 0)
			{
				if (this.buckets[num].k == k)
				{
					return false;
				}
				num = (num + 1) & this.mask;
			}
			this.buckets[num].k = k;
			this.buckets[num].v = v;
			this.count++;
			return true;
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x0002050C File Offset: 0x0001E70C
		public void Increment(int key, int increment = 1)
		{
			if (increment == 0)
			{
				throw new ArgumentException("Increment may not be zero.");
			}
			if ((float)(this.count + 1) / (float)this.buckets.Length > this.load)
			{
				this.Rehash();
			}
			int num = key & this.mask;
			while (this.buckets[num].v != 0)
			{
				if (this.buckets[num].k == key)
				{
					this.buckets[num].v = checked(this.buckets[num].v + increment);
					return;
				}
				num = (num + 1) & this.mask;
			}
			this.buckets[num].k = key;
			this.buckets[num].v = increment;
			this.count++;
		}

		// Token: 0x170000CA RID: 202
		public int this[int k]
		{
			get
			{
				int num = k & this.mask;
				while (this.buckets[num].k != k)
				{
					if (this.buckets[num].v == 0)
					{
						throw new KeyNotFoundException();
					}
					num = (num + 1) & this.mask;
				}
				if (k == 0 && this.buckets[num].v == 0)
				{
					throw new KeyNotFoundException();
				}
				return this.buckets[num].v;
			}
			set
			{
				if (value == 0)
				{
					throw new ArgumentException("Value may not be zero.");
				}
				int num = k & this.mask;
				while (this.buckets[num].k != k)
				{
					if (this.buckets[num].v == 0)
					{
						this.buckets[num].k = k;
						this.buckets[num].v = value;
						int num2 = this.count + 1;
						this.count = num2;
						if ((float)num2 / (float)this.buckets.Length > this.load)
						{
							this.Rehash();
						}
						return;
					}
					num = (num + 1) & this.mask;
				}
				if (k == 0 && this.buckets[num].v == 0)
				{
					this.buckets[num].v = value;
					int num2 = this.count + 1;
					this.count = num2;
					if ((float)num2 / (float)this.buckets.Length > this.load)
					{
						this.Rehash();
					}
					return;
				}
				this.buckets[num].v = value;
			}
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x00020774 File Offset: 0x0001E974
		internal void Rehash()
		{
			if (this.mask == 2147483647)
			{
				throw new OverflowException("The hash table has reached the maximum size and is unable to grow further.");
			}
			FastIntToIntHash.Entry[] array = this.buckets;
			this.buckets = new FastIntToIntHash.Entry[this.buckets.Length * 2];
			this.mask = (this.mask << 1) | 1;
			int num = this.count;
			this.count = 0;
			foreach (FastIntToIntHash.Entry entry in array)
			{
				if (entry.v != 0)
				{
					this.Add(entry.k, entry.v);
				}
			}
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x00020804 File Offset: 0x0001EA04
		public bool ContainsKey(int k)
		{
			int num = k & this.mask;
			while (this.buckets[num].k != k)
			{
				if (this.buckets[num].v == 0)
				{
					return false;
				}
				num = (num + 1) & this.mask;
			}
			return k != 0 || this.buckets[num].v != 0;
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x0002086C File Offset: 0x0001EA6C
		public bool TryGetValue(int k, out int v)
		{
			int num = k & this.mask;
			while (this.buckets[num].k != k)
			{
				if (this.buckets[num].v == 0)
				{
					v = 0;
					return false;
				}
				num = (num + 1) & this.mask;
			}
			v = this.buckets[num].v;
			return k != 0 || v != 0;
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x000208DC File Offset: 0x0001EADC
		public void Remove(int k)
		{
			int num = k & this.mask;
			while (this.buckets[num].k != k)
			{
				if (this.buckets[num].v == 0)
				{
					throw new KeyNotFoundException();
				}
				num = (num + 1) & this.mask;
			}
			if (k == 0 && this.buckets[num].v == 0)
			{
				throw new KeyNotFoundException();
			}
			int num2 = (num + 1) & this.mask;
			int num3 = 1;
			while (this.buckets[num2].v != 0)
			{
				if (this.ClockwiseDistance(this.GetBucket(this.buckets[num2].k), num2) >= num3)
				{
					this.buckets[num].k = this.buckets[num2].k;
					this.buckets[num].v = this.buckets[num2].v;
					num = num2;
					num3 = 0;
				}
				num2 = (num2 + 1) & this.mask;
				num3++;
			}
			this.buckets[num].k = 0;
			this.buckets[num].v = 0;
			this.count--;
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x00020A1B File Offset: 0x0001EC1B
		private int GetBucket(int key)
		{
			return key & this.mask;
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x00020A25 File Offset: 0x0001EC25
		private int ClockwiseDistance(int start, int end)
		{
			return (this.mask + 1 + end - start) % (this.mask + 1);
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x00020A3C File Offset: 0x0001EC3C
		public void Clear()
		{
			Array.Clear(this.buckets, 0, this.buckets.Length);
			this.count = 0;
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000590 RID: 1424 RVA: 0x00020A59 File Offset: 0x0001EC59
		public long MemoryUsage
		{
			get
			{
				return (long)(this.buckets.Length * 2 * 4);
			}
		}

		// Token: 0x040000FF RID: 255
		private readonly float load;

		// Token: 0x04000100 RID: 256
		private FastIntToIntHash.Entry[] buckets;

		// Token: 0x04000101 RID: 257
		private int mask;

		// Token: 0x04000102 RID: 258
		private int count;

		// Token: 0x02000128 RID: 296
		[DebuggerDisplay("k={k} v={v}")]
		[Serializable]
		internal struct Entry
		{
			// Token: 0x040002F8 RID: 760
			public int k;

			// Token: 0x040002F9 RID: 761
			public int v;
		}
	}
}
