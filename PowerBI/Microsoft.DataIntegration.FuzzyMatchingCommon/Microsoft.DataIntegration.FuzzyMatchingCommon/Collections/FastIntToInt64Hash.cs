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
	// Token: 0x0200007F RID: 127
	[Serializable]
	public sealed class FastIntToInt64Hash : IRawSerializable, ISerializable, IMemoryUsage, IFastDictionary<int, long>, IEnumerable<KeyValuePair<int, long>>, IEnumerable
	{
		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600055B RID: 1371 RVA: 0x0001F6F8 File Offset: 0x0001D8F8
		// (set) Token: 0x0600055C RID: 1372 RVA: 0x0001F700 File Offset: 0x0001D900
		bool IRawSerializable.EnableRawSerialization { get; set; }

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600055D RID: 1373 RVA: 0x0001F709 File Offset: 0x0001D909
		// (set) Token: 0x0600055E RID: 1374 RVA: 0x0001F711 File Offset: 0x0001D911
		int IRawSerializable.RawSerializationID { get; set; }

		// Token: 0x0600055F RID: 1375 RVA: 0x0001F71A File Offset: 0x0001D91A
		public FastIntToInt64Hash()
			: this(0.75f)
		{
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x0001F727 File Offset: 0x0001D927
		public FastIntToInt64Hash(float load)
		{
			this.load = 0.75f;
			base..ctor();
			this.load = Math.Min(load, 0.9f);
			this.buckets = new FastIntToInt64Hash.Entry[16];
			this.mask = 15;
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x0001F760 File Offset: 0x0001D960
		public FastIntToInt64Hash(SerializationInfo info, StreamingContext context)
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
				this.buckets = (FastIntToInt64Hash.Entry[])info.GetValue("buckets", typeof(FastIntToInt64Hash.Entry[]));
			}
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x0001F848 File Offset: 0x0001DA48
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

		// Token: 0x06000563 RID: 1379 RVA: 0x0001F8C4 File Offset: 0x0001DAC4
		void IRawSerializable.Serialize(Stream s)
		{
			StreamUtilities.WriteInt64(s, s.Position);
			StreamUtilities.WriteInt32(s, this.buckets.Length);
			for (int i = 0; i < this.buckets.Length; i++)
			{
				StreamUtilities.WriteInt32(s, this.buckets[i].k);
				StreamUtilities.WriteInt64(s, this.buckets[i].v);
			}
			StreamUtilities.WriteInt64(s, s.Position);
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x0001F938 File Offset: 0x0001DB38
		void IRawSerializable.Deserialize(Stream s)
		{
			if (s.Position != StreamUtilities.ReadInt64(s))
			{
				throw new SerializationException("Unexpected stream position encountered when deserializing.");
			}
			this.buckets = new FastIntToInt64Hash.Entry[StreamUtilities.ReadInt32(s)];
			for (int i = 0; i < this.buckets.Length; i++)
			{
				this.buckets[i].k = StreamUtilities.ReadInt32(s);
				this.buckets[i].v = StreamUtilities.ReadInt64(s);
			}
			if (s.Position != StreamUtilities.ReadInt64(s))
			{
				throw new SerializationException("Unexpected stream position encountered when deserializing.");
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000565 RID: 1381 RVA: 0x0001F9C9 File Offset: 0x0001DBC9
		public long DefaultValue
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000566 RID: 1382 RVA: 0x0001F9CD File Offset: 0x0001DBCD
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x0001F9D5 File Offset: 0x0001DBD5
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x0001F9DD File Offset: 0x0001DBDD
		public IEnumerator<KeyValuePair<int, long>> GetEnumerator()
		{
			foreach (FastIntToInt64Hash.Entry entry in this.buckets)
			{
				if (entry.k != 0 && entry.v != 0L)
				{
					yield return new KeyValuePair<int, long>(entry.k, entry.v);
				}
			}
			FastIntToInt64Hash.Entry[] array = null;
			yield break;
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x0001F9EC File Offset: 0x0001DBEC
		public void Add(int k, long v)
		{
			if (v == 0L)
			{
				throw new ArgumentException("Value may not be zero.");
			}
			if ((float)(this.count + 1) / (float)this.buckets.Length > this.load)
			{
				this.Rehash();
			}
			int num = k & this.mask;
			while (this.buckets[num].v != 0L)
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

		// Token: 0x0600056A RID: 1386 RVA: 0x0001FAA4 File Offset: 0x0001DCA4
		public bool TryAdd(int k, long v)
		{
			if (v == 0L)
			{
				throw new ArgumentException("Value may not be zero.");
			}
			if ((float)(this.count + 1) / (float)this.buckets.Length > this.load)
			{
				this.Rehash();
			}
			int num = k & this.mask;
			while (this.buckets[num].v != 0L)
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

		// Token: 0x0600056B RID: 1387 RVA: 0x0001FB54 File Offset: 0x0001DD54
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
			while (this.buckets[num].v != 0L)
			{
				checked
				{
					if (this.buckets[num].k == key)
					{
						this.buckets[num].v = this.buckets[num].v + unchecked((long)increment);
						return;
					}
				}
				num = (num + 1) & this.mask;
			}
			this.buckets[num].k = key;
			this.buckets[num].v = (long)increment;
			this.count++;
		}

		// Token: 0x170000C4 RID: 196
		public long this[int k]
		{
			get
			{
				int num = k & this.mask;
				while (this.buckets[num].k != k)
				{
					if (this.buckets[num].v == 0L)
					{
						throw new KeyNotFoundException();
					}
					num = (num + 1) & this.mask;
				}
				if (k == 0 && this.buckets[num].v == 0L)
				{
					throw new KeyNotFoundException();
				}
				return this.buckets[num].v;
			}
			set
			{
				if (value == 0L)
				{
					throw new ArgumentException("Value may not be zero.");
				}
				int num = k & this.mask;
				while (this.buckets[num].k != k)
				{
					if (this.buckets[num].v == 0L)
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
				if (k == 0 && this.buckets[num].v == 0L)
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

		// Token: 0x0600056E RID: 1390 RVA: 0x0001FDBC File Offset: 0x0001DFBC
		internal void Rehash()
		{
			if (this.mask == 2147483647)
			{
				throw new OverflowException("The hash table has reached the maximum size and is unable to grow further.");
			}
			FastIntToInt64Hash.Entry[] array = this.buckets;
			this.buckets = new FastIntToInt64Hash.Entry[this.buckets.Length * 2];
			this.mask = (this.mask << 1) | 1;
			int num = this.count;
			this.count = 0;
			foreach (FastIntToInt64Hash.Entry entry in array)
			{
				if (entry.v != 0L)
				{
					this.Add(entry.k, entry.v);
				}
			}
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x0001FE4C File Offset: 0x0001E04C
		public bool ContainsKey(int k)
		{
			int num = k & this.mask;
			while (this.buckets[num].k != k)
			{
				if (this.buckets[num].v == 0L)
				{
					return false;
				}
				num = (num + 1) & this.mask;
			}
			return k != 0 || this.buckets[num].v != 0L;
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x0001FEB4 File Offset: 0x0001E0B4
		public bool TryGetValue(int k, out long v)
		{
			int num = k & this.mask;
			while (this.buckets[num].k != k)
			{
				if (this.buckets[num].v == 0L)
				{
					v = 0L;
					return false;
				}
				num = (num + 1) & this.mask;
			}
			v = this.buckets[num].v;
			return k != 0 || v != 0L;
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x0001FF24 File Offset: 0x0001E124
		public void Remove(int k)
		{
			int num = k & this.mask;
			while (this.buckets[num].k != k)
			{
				if (this.buckets[num].v == 0L)
				{
					throw new KeyNotFoundException();
				}
				num = (num + 1) & this.mask;
			}
			if (k == 0 && this.buckets[num].v == 0L)
			{
				throw new KeyNotFoundException();
			}
			int num2 = (num + 1) & this.mask;
			int num3 = 1;
			while (this.buckets[num2].v != 0L)
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
			this.buckets[num].v = 0L;
			this.count--;
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x00020064 File Offset: 0x0001E264
		private int GetBucket(int key)
		{
			return key & this.mask;
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x0002006E File Offset: 0x0001E26E
		private int ClockwiseDistance(int start, int end)
		{
			return (this.mask + 1 + end - start) % (this.mask + 1);
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x00020085 File Offset: 0x0001E285
		public void Clear()
		{
			Array.Clear(this.buckets, 0, this.buckets.Length);
			this.count = 0;
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000575 RID: 1397 RVA: 0x000200A2 File Offset: 0x0001E2A2
		public long MemoryUsage
		{
			get
			{
				return (long)(this.buckets.Length * 12);
			}
		}

		// Token: 0x040000F9 RID: 249
		private readonly float load;

		// Token: 0x040000FA RID: 250
		private FastIntToInt64Hash.Entry[] buckets;

		// Token: 0x040000FB RID: 251
		private int mask;

		// Token: 0x040000FC RID: 252
		private int count;

		// Token: 0x02000126 RID: 294
		[DebuggerDisplay("k={k} v={v}")]
		[Serializable]
		public struct Entry
		{
			// Token: 0x040002F1 RID: 753
			public int k;

			// Token: 0x040002F2 RID: 754
			public long v;
		}
	}
}
