using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using Microsoft.DataIntegration.FuzzyMatchingCommon.IO;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections
{
	// Token: 0x02000084 RID: 132
	[Serializable]
	public class FastIntToIntHash2<TKey, TValue, TKeyAdapter, TValueAdapter> : IRawSerializable, ISerializable, IMemoryUsage where TKey : struct where TValue : struct where TKeyAdapter : struct, IHashAdapter<TKeyAdapter, TKey> where TValueAdapter : struct, IHashAdapter<TValueAdapter, TValue>
	{
		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600059F RID: 1439 RVA: 0x00020CBD File Offset: 0x0001EEBD
		// (set) Token: 0x060005A0 RID: 1440 RVA: 0x00020CC5 File Offset: 0x0001EEC5
		bool IRawSerializable.EnableRawSerialization { get; set; }

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060005A1 RID: 1441 RVA: 0x00020CCE File Offset: 0x0001EECE
		// (set) Token: 0x060005A2 RID: 1442 RVA: 0x00020CD6 File Offset: 0x0001EED6
		int IRawSerializable.RawSerializationID { get; set; }

		// Token: 0x060005A3 RID: 1443 RVA: 0x00020CDF File Offset: 0x0001EEDF
		public FastIntToIntHash2()
			: this(0.75f)
		{
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x00020CEC File Offset: 0x0001EEEC
		public FastIntToIntHash2(float load)
		{
			this.load = 0.75f;
			base..ctor();
			this.load = Math.Min(load, 0.9f);
			this.buckets = new FastIntToIntHash2<TKey, TValue, TKeyAdapter, TValueAdapter>.Entry[16];
			this.mask = 15;
			this.m_loadMark = (int)(load * (float)this.buckets.Length);
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x00020D44 File Offset: 0x0001EF44
		public FastIntToIntHash2(SerializationInfo info, StreamingContext context)
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
				this.buckets = (FastIntToIntHash2<TKey, TValue, TKeyAdapter, TValueAdapter>.Entry[])info.GetValue("buckets", typeof(FastIntToIntHash2<TKey, TValue, TKeyAdapter, TValueAdapter>.Entry[]));
			}
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x00020E2C File Offset: 0x0001F02C
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

		// Token: 0x060005A7 RID: 1447 RVA: 0x00020EA8 File Offset: 0x0001F0A8
		void IRawSerializable.Serialize(Stream s)
		{
			StreamUtilities.WriteInt64(s, s.Position);
			StreamUtilities.WriteInt32(s, this.buckets.Length);
			for (int i = 0; i < this.buckets.Length; i++)
			{
			}
			StreamUtilities.WriteInt64(s, s.Position);
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x00020EF0 File Offset: 0x0001F0F0
		void IRawSerializable.Deserialize(Stream s)
		{
			if (s.Position != StreamUtilities.ReadInt64(s))
			{
				throw new SerializationException("Unexpected stream position encountered when deserializing.");
			}
			this.buckets = new FastIntToIntHash2<TKey, TValue, TKeyAdapter, TValueAdapter>.Entry[StreamUtilities.ReadInt32(s)];
			for (int i = 0; i < this.buckets.Length; i++)
			{
			}
			if (s.Position != StreamUtilities.ReadInt64(s))
			{
				throw new SerializationException("Unexpected stream position encountered when deserializing.");
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060005A9 RID: 1449 RVA: 0x00020F53 File Offset: 0x0001F153
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x00020F5C File Offset: 0x0001F15C
		public virtual void Add(TKey k, TValue v)
		{
			TValueAdapter tvalueAdapter = FastIntToIntHash2<TKey, TValue, TKeyAdapter, TValueAdapter>.valueAdapterDefault;
			if (tvalueAdapter.IsDefault2(v))
			{
				throw new ArgumentException("Value may not be zero.");
			}
			if (this.count > this.m_loadMark)
			{
				this.Rehash();
			}
			TKeyAdapter tkeyAdapter = FastIntToIntHash2<TKey, TValue, TKeyAdapter, TValueAdapter>.keyAdapaterDefault;
			int num = tkeyAdapter.GetBucket2(k, this.mask);
			for (;;)
			{
				tvalueAdapter = FastIntToIntHash2<TKey, TValue, TKeyAdapter, TValueAdapter>.valueAdapterDefault;
				if (tvalueAdapter.IsDefault2(this.buckets[num].v))
				{
					goto Block_4;
				}
				tkeyAdapter = FastIntToIntHash2<TKey, TValue, TKeyAdapter, TValueAdapter>.keyAdapaterDefault;
				if (tkeyAdapter.Equals3(k, this.buckets[num].k))
				{
					break;
				}
				num = (num + 1) & this.mask;
			}
			throw new ArgumentException("Key already present!");
			Block_4:
			this.buckets[num].k = k;
			this.buckets[num].v = v;
			this.count++;
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x00021054 File Offset: 0x0001F254
		public bool TryAdd(TKey k, TValue v)
		{
			if (this.ValueIsDefault.Invoke(v))
			{
				throw new ArgumentException("Value may not be zero.");
			}
			if (this.count > this.m_loadMark)
			{
				this.Rehash();
			}
			int num = this.GetBucket.Invoke(k, this.mask);
			while (!this.ValueIsDefault.Invoke(this.buckets[num].v))
			{
				if (this.KeyEqual.Invoke(k, this.buckets[num].k))
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

		// Token: 0x060005AC RID: 1452 RVA: 0x00021124 File Offset: 0x0001F324
		public bool TryAdd4(TKey k, TValue v)
		{
			TValueAdapter tvalueAdapter = default(TValueAdapter);
			if (tvalueAdapter.IsDefault2(v))
			{
				throw new ArgumentException("Value may not be zero.");
			}
			if (this.count > this.m_loadMark)
			{
				this.Rehash();
			}
			TKeyAdapter tkeyAdapter = default(TKeyAdapter);
			int num = tkeyAdapter.GetBucket2(k, this.mask);
			for (;;)
			{
				tvalueAdapter = default(TValueAdapter);
				if (tvalueAdapter.IsDefault2(this.buckets[num].v))
				{
					goto Block_4;
				}
				tkeyAdapter = default(TKeyAdapter);
				if (tkeyAdapter.Equals3(k, this.buckets[num].k))
				{
					break;
				}
				num = (num + 1) & this.mask;
			}
			return false;
			Block_4:
			this.buckets[num].k = k;
			this.buckets[num].v = v;
			this.count++;
			return true;
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x00021218 File Offset: 0x0001F418
		internal void Rehash()
		{
			if (this.inRehash)
			{
				throw new InvalidOperationException();
			}
			this.inRehash = true;
			if (this.mask == 2147483647)
			{
				throw new OverflowException("The hash table has reached the maximum size and is unable to grow further.");
			}
			FastIntToIntHash2<TKey, TValue, TKeyAdapter, TValueAdapter>.Entry[] array = this.buckets;
			this.buckets = new FastIntToIntHash2<TKey, TValue, TKeyAdapter, TValueAdapter>.Entry[this.buckets.Length * 2];
			this.mask = (this.mask << 1) | 1;
			int num = this.count;
			this.count = 0;
			this.m_loadMark = (int)(this.load * (float)this.buckets.Length);
			foreach (FastIntToIntHash2<TKey, TValue, TKeyAdapter, TValueAdapter>.Entry entry in array)
			{
				TValueAdapter tvalueAdapter = default(TValueAdapter);
				if (!tvalueAdapter.IsDefault2(entry.v))
				{
					this.Add(entry.k, entry.v);
				}
			}
			this.inRehash = false;
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x000212F0 File Offset: 0x0001F4F0
		public bool TryGetValue(TKey k, out TValue v)
		{
			TKeyAdapter tkeyAdapter = FastIntToIntHash2<TKey, TValue, TKeyAdapter, TValueAdapter>.keyAdapaterDefault;
			int num = tkeyAdapter.GetBucket2(k, this.mask);
			for (;;)
			{
				tkeyAdapter = FastIntToIntHash2<TKey, TValue, TKeyAdapter, TValueAdapter>.keyAdapaterDefault;
				if (tkeyAdapter.Equals3(k, this.buckets[num].k))
				{
					break;
				}
				TValueAdapter tvalueAdapter = FastIntToIntHash2<TKey, TValue, TKeyAdapter, TValueAdapter>.valueAdapterDefault;
				if (tvalueAdapter.IsDefault2(this.buckets[num].v))
				{
					goto Block_3;
				}
				num = (num + 1) & this.mask;
			}
			v = this.buckets[num].v;
			tkeyAdapter = FastIntToIntHash2<TKey, TValue, TKeyAdapter, TValueAdapter>.keyAdapaterDefault;
			if (tkeyAdapter.IsDefault2(k))
			{
				TValueAdapter tvalueAdapter = FastIntToIntHash2<TKey, TValue, TKeyAdapter, TValueAdapter>.valueAdapterDefault;
				return !tvalueAdapter.IsDefault2(v);
			}
			return true;
			Block_3:
			v = default(TValue);
			return false;
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x000213CB File Offset: 0x0001F5CB
		public void Clear()
		{
			Array.Clear(this.buckets, 0, this.buckets.Length);
			this.count = 0;
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060005B0 RID: 1456 RVA: 0x000213E8 File Offset: 0x0001F5E8
		public long MemoryUsage
		{
			get
			{
				return (long)(this.buckets.Length * 2 * 4);
			}
		}

		// Token: 0x04000105 RID: 261
		private readonly float load;

		// Token: 0x04000106 RID: 262
		protected FastIntToIntHash2<TKey, TValue, TKeyAdapter, TValueAdapter>.Entry[] buckets;

		// Token: 0x04000107 RID: 263
		protected int mask;

		// Token: 0x04000108 RID: 264
		protected int count;

		// Token: 0x04000109 RID: 265
		protected int m_loadMark;

		// Token: 0x0400010C RID: 268
		protected Func<TKey, int, int> GetBucket;

		// Token: 0x0400010D RID: 269
		protected Func<TKey, TKey, bool> KeyEqual;

		// Token: 0x0400010E RID: 270
		protected Func<TKey, bool> KeyIsDefault;

		// Token: 0x0400010F RID: 271
		protected Func<TValue, TKey, bool> ValueEqual;

		// Token: 0x04000110 RID: 272
		protected Func<TValue, bool> ValueIsDefault;

		// Token: 0x04000111 RID: 273
		private bool inRehash;

		// Token: 0x04000112 RID: 274
		private static readonly TKey keyDefault;

		// Token: 0x04000113 RID: 275
		private static readonly TKeyAdapter keyAdapaterDefault;

		// Token: 0x04000114 RID: 276
		private static readonly TValue valueDefault;

		// Token: 0x04000115 RID: 277
		private static readonly TValueAdapter valueAdapterDefault;

		// Token: 0x0200012B RID: 299
		[DebuggerDisplay("k={k} v={v}")]
		[Serializable]
		public struct Entry
		{
			// Token: 0x04000305 RID: 773
			public TKey k;

			// Token: 0x04000306 RID: 774
			public TValue v;
		}
	}
}
