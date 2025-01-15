using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using Microsoft.DataIntegration.FuzzyMatchingCommon.IO;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections
{
	// Token: 0x0200007E RID: 126
	[Serializable]
	public sealed class FastIntPairToIntHash : IRawSerializable, ISerializable, IMemoryUsage
	{
		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600054A RID: 1354 RVA: 0x0001F0EA File Offset: 0x0001D2EA
		// (set) Token: 0x0600054B RID: 1355 RVA: 0x0001F0F2 File Offset: 0x0001D2F2
		bool IRawSerializable.EnableRawSerialization { get; set; }

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x0600054C RID: 1356 RVA: 0x0001F0FB File Offset: 0x0001D2FB
		// (set) Token: 0x0600054D RID: 1357 RVA: 0x0001F103 File Offset: 0x0001D303
		int IRawSerializable.RawSerializationID { get; set; }

		// Token: 0x0600054E RID: 1358 RVA: 0x0001F10C File Offset: 0x0001D30C
		public FastIntPairToIntHash()
			: this(0.75f)
		{
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x0001F119 File Offset: 0x0001D319
		public FastIntPairToIntHash(float load)
		{
			this.load = 0.75f;
			base..ctor();
			this.load = Math.Min(load, 0.9f);
			this.buckets = new FastIntPairToIntHash.Entry[16];
			this.mask = 15;
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x0001F154 File Offset: 0x0001D354
		private FastIntPairToIntHash(SerializationInfo info, StreamingContext context)
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
				this.buckets = (FastIntPairToIntHash.Entry[])info.GetValue("buckets", typeof(FastIntPairToIntHash.Entry[]));
			}
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x0001F23C File Offset: 0x0001D43C
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

		// Token: 0x06000552 RID: 1362 RVA: 0x0001F2B8 File Offset: 0x0001D4B8
		void IRawSerializable.Serialize(Stream s)
		{
			StreamUtilities.WriteInt64(s, s.Position);
			StreamUtilities.WriteInt32(s, this.buckets.Length);
			for (int i = 0; i < this.buckets.Length; i++)
			{
				StreamUtilities.WriteInt32(s, this.buckets[i].k1);
				StreamUtilities.WriteInt32(s, this.buckets[i].k2);
				StreamUtilities.WriteInt32(s, this.buckets[i].v);
			}
			StreamUtilities.WriteInt64(s, s.Position);
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x0001F344 File Offset: 0x0001D544
		void IRawSerializable.Deserialize(Stream s)
		{
			if (s.Position != StreamUtilities.ReadInt64(s))
			{
				throw new SerializationException("Unexpected stream position encountered when deserializing.");
			}
			this.buckets = new FastIntPairToIntHash.Entry[StreamUtilities.ReadInt32(s)];
			for (int i = 0; i < this.buckets.Length; i++)
			{
				this.buckets[i].k1 = StreamUtilities.ReadInt32(s);
				this.buckets[i].k2 = StreamUtilities.ReadInt32(s);
				this.buckets[i].v = StreamUtilities.ReadInt32(s);
			}
			if (s.Position != StreamUtilities.ReadInt64(s))
			{
				throw new SerializationException("Unexpected stream position encountered when deserializing.");
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000554 RID: 1364 RVA: 0x0001F3EC File Offset: 0x0001D5EC
		public long MemoryUsage
		{
			get
			{
				return (long)(this.buckets.Length * 3 * 4);
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000555 RID: 1365 RVA: 0x0001F3FB File Offset: 0x0001D5FB
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x0001F404 File Offset: 0x0001D604
		public void Add(int k1, int k2, int v)
		{
			if (v == 0)
			{
				throw new ArgumentException("Value may not be zero.");
			}
			if ((float)(this.count + 1) / (float)this.buckets.Length > this.load)
			{
				this.Rehash();
			}
			int num = Utilities.GetHashCode(k1, k2) & this.mask;
			while (this.buckets[num].v != 0)
			{
				if (this.buckets[num].k1 == k1 && this.buckets[num].k2 == k2)
				{
					throw new ArgumentException("Key already present!");
				}
				num = (num + 1) & this.mask;
			}
			this.buckets[num].k1 = k1;
			this.buckets[num].k2 = k2;
			this.buckets[num].v = v;
			this.count++;
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x0001F4E8 File Offset: 0x0001D6E8
		internal void Rehash()
		{
			if (this.mask == 2147483647)
			{
				throw new OverflowException("The hash table has reached the maximum size and is unable to grow further.");
			}
			FastIntPairToIntHash.Entry[] array = this.buckets;
			this.buckets = new FastIntPairToIntHash.Entry[this.buckets.Length * 2];
			this.mask = (this.mask << 1) | 1;
			int num = this.count;
			this.count = 0;
			foreach (FastIntPairToIntHash.Entry entry in array)
			{
				if (entry.v != 0)
				{
					this.Add(entry.k1, entry.k2, entry.v);
				}
			}
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x0001F580 File Offset: 0x0001D780
		public bool TryGetValue(int k1, int k2, out int v)
		{
			int num = Utilities.GetHashCode(k1, k2) & this.mask;
			while (this.buckets[num].k1 != k1 || this.buckets[num].k2 != k2)
			{
				if (this.buckets[num].v == 0)
				{
					v = 0;
					return false;
				}
				num = (num + 1) & this.mask;
			}
			v = this.buckets[num].v;
			return k1 != 0 || k2 != 0 || v != 0;
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x0001F60C File Offset: 0x0001D80C
		public void Remove(int k1, int k2)
		{
			int num = Utilities.GetHashCode(k1, k2) & this.mask;
			while (this.buckets[num].k1 != k1 || this.buckets[num].k2 != k2)
			{
				if (this.buckets[num].v == 0)
				{
					throw new KeyNotFoundException();
				}
				num = (num + 1) & this.mask;
			}
			if (k1 == 0 && k2 == 0 && this.buckets[num].v == 0)
			{
				throw new KeyNotFoundException();
			}
			this.buckets[num].k1 = 0;
			this.buckets[num].k2 = 0;
			this.buckets[num].v = 0;
			this.count--;
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x0001F6DB File Offset: 0x0001D8DB
		public void Clear()
		{
			Array.Clear(this.buckets, 0, this.buckets.Length);
			this.count = 0;
		}

		// Token: 0x040000F3 RID: 243
		private readonly float load;

		// Token: 0x040000F4 RID: 244
		internal FastIntPairToIntHash.Entry[] buckets;

		// Token: 0x040000F5 RID: 245
		private int mask;

		// Token: 0x040000F6 RID: 246
		private int count;

		// Token: 0x02000125 RID: 293
		[DebuggerDisplay("k1={k1} k2={k2} v={v}")]
		[Serializable]
		public struct Entry
		{
			// Token: 0x040002EE RID: 750
			public int k1;

			// Token: 0x040002EF RID: 751
			public int k2;

			// Token: 0x040002F0 RID: 752
			public int v;
		}
	}
}
