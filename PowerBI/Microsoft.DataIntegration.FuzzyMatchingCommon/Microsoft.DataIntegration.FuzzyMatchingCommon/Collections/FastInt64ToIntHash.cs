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
	// Token: 0x0200007C RID: 124
	[Serializable]
	public sealed class FastInt64ToIntHash : IRawSerializable, ISerializable, IMemoryUsage, IFastDictionary<long, int>, IEnumerable<KeyValuePair<long, int>>, IEnumerable
	{
		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000515 RID: 1301 RVA: 0x0001E212 File Offset: 0x0001C412
		// (set) Token: 0x06000516 RID: 1302 RVA: 0x0001E21A File Offset: 0x0001C41A
		bool IRawSerializable.EnableRawSerialization { get; set; }

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000517 RID: 1303 RVA: 0x0001E223 File Offset: 0x0001C423
		// (set) Token: 0x06000518 RID: 1304 RVA: 0x0001E22B File Offset: 0x0001C42B
		int IRawSerializable.RawSerializationID { get; set; }

		// Token: 0x06000519 RID: 1305 RVA: 0x0001E234 File Offset: 0x0001C434
		public FastInt64ToIntHash()
			: this(0.75f)
		{
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x0001E241 File Offset: 0x0001C441
		public FastInt64ToIntHash(float load)
		{
			this.load = 0.75f;
			base..ctor();
			this.load = Math.Min(load, 0.9f);
			this.buckets = new FastInt64ToIntHash.Entry[16];
			this.mask = 15;
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x0001E27C File Offset: 0x0001C47C
		private FastInt64ToIntHash(SerializationInfo info, StreamingContext context)
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
				this.buckets = (FastInt64ToIntHash.Entry[])info.GetValue("buckets", typeof(FastInt64ToIntHash.Entry[]));
			}
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x0001E364 File Offset: 0x0001C564
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

		// Token: 0x0600051D RID: 1309 RVA: 0x0001E3E0 File Offset: 0x0001C5E0
		void IRawSerializable.Serialize(Stream s)
		{
			StreamUtilities.WriteInt64(s, s.Position);
			StreamUtilities.WriteInt32(s, this.buckets.Length);
			for (int i = 0; i < this.buckets.Length; i++)
			{
				StreamUtilities.WriteInt64(s, this.buckets[i].k);
				StreamUtilities.WriteInt32(s, this.buckets[i].v);
			}
			StreamUtilities.WriteInt64(s, s.Position);
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x0001E454 File Offset: 0x0001C654
		void IRawSerializable.Deserialize(Stream s)
		{
			if (s.Position != StreamUtilities.ReadInt64(s))
			{
				throw new SerializationException("Unexpected stream position encountered when deserializing.");
			}
			this.buckets = new FastInt64ToIntHash.Entry[StreamUtilities.ReadInt32(s)];
			for (int i = 0; i < this.buckets.Length; i++)
			{
				this.buckets[i].k = StreamUtilities.ReadInt64(s);
				this.buckets[i].v = StreamUtilities.ReadInt32(s);
			}
			if (s.Position != StreamUtilities.ReadInt64(s))
			{
				throw new SerializationException("Unexpected stream position encountered when deserializing.");
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600051F RID: 1311 RVA: 0x0001E4E5 File Offset: 0x0001C6E5
		public int DefaultValue
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000520 RID: 1312 RVA: 0x0001E4E8 File Offset: 0x0001C6E8
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x0001E4F0 File Offset: 0x0001C6F0
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x0001E4F8 File Offset: 0x0001C6F8
		public IEnumerator<KeyValuePair<long, int>> GetEnumerator()
		{
			foreach (FastInt64ToIntHash.Entry entry in this.buckets)
			{
				if (entry.k != 0L && entry.v != 0)
				{
					yield return new KeyValuePair<long, int>(entry.k, entry.v);
				}
			}
			FastInt64ToIntHash.Entry[] array = null;
			yield break;
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x0001E507 File Offset: 0x0001C707
		public void Add(int k1, int k2, int v)
		{
			this.Add(Utilities.Int32ToInt64(k1, k2), v);
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x0001E517 File Offset: 0x0001C717
		private static int HashCode(long k)
		{
			return (int)k ^ (int)((ulong)k >> 32);
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x0001E524 File Offset: 0x0001C724
		public void Add(long k, int v)
		{
			if (v == 0)
			{
				throw new ArgumentException("Value may not be zero.");
			}
			if ((float)(this.count + 1) / (float)this.buckets.Length > this.load)
			{
				this.Rehash();
			}
			int num = FastInt64ToIntHash.HashCode(k) & this.mask;
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

		// Token: 0x06000526 RID: 1318 RVA: 0x0001E5E4 File Offset: 0x0001C7E4
		public bool TryAdd(long k, int v)
		{
			if (v == 0)
			{
				throw new ArgumentException("Value may not be zero.");
			}
			if ((float)(this.count + 1) / (float)this.buckets.Length > this.load)
			{
				this.Rehash();
			}
			int num = FastInt64ToIntHash.HashCode(k) & this.mask;
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

		// Token: 0x06000527 RID: 1319 RVA: 0x0001E69C File Offset: 0x0001C89C
		public void Increment(long key, int increment = 1)
		{
			if (increment == 0)
			{
				throw new ArgumentException("Increment may not be zero.");
			}
			if ((float)(this.count + 1) / (float)this.buckets.Length > this.load)
			{
				this.Rehash();
			}
			int num = FastInt64ToIntHash.HashCode(key) & this.mask;
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

		// Token: 0x170000B5 RID: 181
		public int this[int k1, int k2]
		{
			get
			{
				return this[Utilities.Int32ToInt64(k1, k2)];
			}
			set
			{
				this[Utilities.Int32ToInt64(k1, k2)] = value;
			}
		}

		// Token: 0x170000B6 RID: 182
		public int this[long k]
		{
			get
			{
				int num = FastInt64ToIntHash.HashCode(k) & this.mask;
				while (this.buckets[num].k != k)
				{
					if (this.buckets[num].v == 0)
					{
						throw new KeyNotFoundException();
					}
					num = (num + 1) & this.mask;
				}
				if (k == 0L && this.buckets[num].v == 0)
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
				int num = FastInt64ToIntHash.HashCode(k) & this.mask;
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
				if (k == 0L && this.buckets[num].v == 0)
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

		// Token: 0x0600052C RID: 1324 RVA: 0x0001E930 File Offset: 0x0001CB30
		internal void Rehash()
		{
			if (this.mask == 2147483647)
			{
				throw new OverflowException("The hash table has reached the maximum size and is unable to grow further.");
			}
			FastInt64ToIntHash.Entry[] array = this.buckets;
			this.buckets = new FastInt64ToIntHash.Entry[this.buckets.Length * 2];
			this.mask = (this.mask << 1) | 1;
			int num = this.count;
			this.count = 0;
			foreach (FastInt64ToIntHash.Entry entry in array)
			{
				if (entry.v != 0)
				{
					this.Add(entry.k, entry.v);
				}
			}
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x0001E9C0 File Offset: 0x0001CBC0
		public bool ContainsKey(long k)
		{
			int num = FastInt64ToIntHash.HashCode(k) & this.mask;
			while (this.buckets[num].k != k)
			{
				if (this.buckets[num].v == 0)
				{
					return false;
				}
				num = (num + 1) & this.mask;
			}
			return k != 0L || this.buckets[num].v != 0;
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x0001EA2C File Offset: 0x0001CC2C
		public bool TryGetValue(int k1, int k2, out int v)
		{
			return this.TryGetValue(Utilities.Int32ToInt64(k1, k2), out v);
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x0001EA3C File Offset: 0x0001CC3C
		public bool TryGetValue(long k, out int v)
		{
			int num = FastInt64ToIntHash.HashCode(k) & this.mask;
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
			return k != 0L || v != 0;
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x0001EAAF File Offset: 0x0001CCAF
		public void Remove(int k1, int k2)
		{
			this.Remove(Utilities.Int32ToInt64(k1, k2));
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x0001EAC0 File Offset: 0x0001CCC0
		public void Remove(long k)
		{
			int num = FastInt64ToIntHash.HashCode(k) & this.mask;
			while (this.buckets[num].k != k)
			{
				if (this.buckets[num].v == 0)
				{
					throw new KeyNotFoundException();
				}
				num = (num + 1) & this.mask;
			}
			if (k == 0L && this.buckets[num].v == 0)
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
			this.buckets[num].k = 0L;
			this.buckets[num].v = 0;
			this.count--;
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x0001EC05 File Offset: 0x0001CE05
		private int GetBucket(long key)
		{
			return FastInt64ToIntHash.HashCode(key) & this.mask;
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x0001EC14 File Offset: 0x0001CE14
		private int ClockwiseDistance(int start, int end)
		{
			return (this.mask + 1 + end - start) % (this.mask + 1);
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x0001EC2B File Offset: 0x0001CE2B
		public void Clear()
		{
			Array.Clear(this.buckets, 0, this.buckets.Length);
			this.count = 0;
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000535 RID: 1333 RVA: 0x0001EC48 File Offset: 0x0001CE48
		public long MemoryUsage
		{
			get
			{
				return (long)(this.buckets.Length * 2 * 4);
			}
		}

		// Token: 0x040000E6 RID: 230
		private readonly float load;

		// Token: 0x040000E7 RID: 231
		internal FastInt64ToIntHash.Entry[] buckets;

		// Token: 0x040000E8 RID: 232
		private int mask;

		// Token: 0x040000E9 RID: 233
		private int count;

		// Token: 0x02000122 RID: 290
		[DebuggerDisplay("k={k} v={v}")]
		[Serializable]
		public struct Entry
		{
			// Token: 0x040002E5 RID: 741
			public long k;

			// Token: 0x040002E6 RID: 742
			public int v;
		}
	}
}
