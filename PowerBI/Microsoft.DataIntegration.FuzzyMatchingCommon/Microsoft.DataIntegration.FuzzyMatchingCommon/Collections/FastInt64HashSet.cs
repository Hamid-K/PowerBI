using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using Microsoft.DataIntegration.FuzzyMatchingCommon.IO;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections
{
	// Token: 0x0200007B RID: 123
	[Serializable]
	public sealed class FastInt64HashSet : IRawSerializable, ISerializable, IMemoryUsage, IEnumerable<long>, IEnumerable
	{
		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000500 RID: 1280 RVA: 0x0001DD56 File Offset: 0x0001BF56
		// (set) Token: 0x06000501 RID: 1281 RVA: 0x0001DD5E File Offset: 0x0001BF5E
		bool IRawSerializable.EnableRawSerialization { get; set; }

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000502 RID: 1282 RVA: 0x0001DD67 File Offset: 0x0001BF67
		// (set) Token: 0x06000503 RID: 1283 RVA: 0x0001DD6F File Offset: 0x0001BF6F
		int IRawSerializable.RawSerializationID { get; set; }

		// Token: 0x06000504 RID: 1284 RVA: 0x0001DD78 File Offset: 0x0001BF78
		public FastInt64HashSet()
			: this(0.75f)
		{
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x0001DD85 File Offset: 0x0001BF85
		public FastInt64HashSet(float load)
		{
			this.load = 0.75f;
			base..ctor();
			this.load = Math.Min(load, 0.9f);
			this.buckets = new long[16];
			this.mask = 15;
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x0001DDC0 File Offset: 0x0001BFC0
		private FastInt64HashSet(SerializationInfo info, StreamingContext context)
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
				this.buckets = (long[])info.GetValue("buckets", typeof(int[]));
			}
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x0001DEA8 File Offset: 0x0001C0A8
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

		// Token: 0x06000508 RID: 1288 RVA: 0x0001DF24 File Offset: 0x0001C124
		void IRawSerializable.Serialize(Stream s)
		{
			StreamUtilities.WriteInt64(s, s.Position);
			StreamUtilities.WriteInt32(s, this.buckets.Length);
			for (int i = 0; i < this.buckets.Length; i++)
			{
				StreamUtilities.WriteInt64(s, this.buckets[i]);
			}
			StreamUtilities.WriteInt64(s, s.Position);
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x0001DF78 File Offset: 0x0001C178
		void IRawSerializable.Deserialize(Stream s)
		{
			if (s.Position != StreamUtilities.ReadInt64(s))
			{
				throw new SerializationException("Unexpected stream position encountered when deserializing.");
			}
			this.buckets = new long[StreamUtilities.ReadInt32(s)];
			for (int i = 0; i < this.buckets.Length; i++)
			{
				this.buckets[i] = StreamUtilities.ReadInt64(s);
			}
			if (s.Position != StreamUtilities.ReadInt64(s))
			{
				throw new SerializationException("Unexpected stream position encountered when deserializing.");
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600050A RID: 1290 RVA: 0x0001DFE9 File Offset: 0x0001C1E9
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x0001DFF4 File Offset: 0x0001C1F4
		public void Add(long k)
		{
			if (FastInt64HashSet.Empty == k)
			{
				throw new ArgumentException("Key may not be zero!");
			}
			if ((float)(this.count + 1) / (float)this.buckets.Length > this.load)
			{
				this.Rehash();
			}
			int num = Utilities.GetHashCode(k) & this.mask;
			while (this.buckets[num] != FastInt64HashSet.Empty)
			{
				if (this.buckets[num] == k)
				{
					return;
				}
				num = (num + 1) & this.mask;
			}
			this.buckets[num] = k;
			this.count++;
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x0001E084 File Offset: 0x0001C284
		public bool Contains(long k)
		{
			int num = Utilities.GetHashCode(k) & this.mask;
			long num2 = this.buckets[num];
			return num2 != 0L && (num2 == k || this.Contains(num, k));
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x0001E0BB File Offset: 0x0001C2BB
		private bool Contains(int bucket, long k)
		{
			for (;;)
			{
				bucket = ++bucket & this.mask;
				if (this.buckets[bucket] == 0L)
				{
					break;
				}
				if (this.buckets[bucket] == k)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x0001E0E4 File Offset: 0x0001C2E4
		internal void Rehash()
		{
			if (this.mask == 2147483647)
			{
				throw new OverflowException("The hash table has reached the maximum size and is unable to grow further.");
			}
			long[] array = this.buckets;
			this.buckets = new long[this.buckets.Length * 2];
			this.mask = (this.mask << 1) | 1;
			int num = this.count;
			this.count = 0;
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] != FastInt64HashSet.Empty)
				{
					this.Add(array[i]);
				}
			}
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x0001E164 File Offset: 0x0001C364
		public void Remove(int k)
		{
			if (FastInt64HashSet.Empty == (long)k)
			{
				throw new ArgumentException("Key may not be zero!");
			}
			int num = Utilities.GetHashCode(k) & this.mask;
			while (this.buckets[num] != (long)k)
			{
				if (this.buckets[num] == 0L)
				{
					throw new KeyNotFoundException();
				}
				num = (num + 1) & this.mask;
			}
			this.buckets[num] = 0L;
			this.count--;
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x0001E1D6 File Offset: 0x0001C3D6
		public void Clear()
		{
			Array.Clear(this.buckets, 0, this.buckets.Length);
			this.count = 0;
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000511 RID: 1297 RVA: 0x0001E1F3 File Offset: 0x0001C3F3
		public long MemoryUsage
		{
			get
			{
				return (long)(this.buckets.Length * 8);
			}
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x0001E200 File Offset: 0x0001C400
		public IEnumerator<long> GetEnumerator()
		{
			return new FastInt64HashSet.Enumerator(this);
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x0001E208 File Offset: 0x0001C408
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new FastInt64HashSet.Enumerator(this);
		}

		// Token: 0x040000DF RID: 223
		private static readonly long Empty;

		// Token: 0x040000E0 RID: 224
		private readonly float load;

		// Token: 0x040000E1 RID: 225
		internal long[] buckets;

		// Token: 0x040000E2 RID: 226
		private int mask;

		// Token: 0x040000E3 RID: 227
		private int count;

		// Token: 0x02000121 RID: 289
		[Serializable]
		private class Enumerator : IEnumerator<long>, IDisposable, IEnumerator
		{
			// Token: 0x060009CF RID: 2511 RVA: 0x0002E203 File Offset: 0x0002C403
			public Enumerator(FastInt64HashSet hashSet)
			{
				this.m_hashSet = hashSet;
			}

			// Token: 0x17000191 RID: 401
			// (get) Token: 0x060009D0 RID: 2512 RVA: 0x0002E219 File Offset: 0x0002C419
			public long Current
			{
				get
				{
					return this.m_hashSet.buckets[this.m_idx];
				}
			}

			// Token: 0x17000192 RID: 402
			// (get) Token: 0x060009D1 RID: 2513 RVA: 0x0002E22D File Offset: 0x0002C42D
			object IEnumerator.Current
			{
				get
				{
					return this.m_hashSet.buckets[this.m_idx];
				}
			}

			// Token: 0x060009D2 RID: 2514 RVA: 0x0002E248 File Offset: 0x0002C448
			public bool MoveNext()
			{
				do
				{
					int num = this.m_idx + 1;
					this.m_idx = num;
					if (num >= this.m_hashSet.buckets.Length)
					{
						return false;
					}
				}
				while (this.m_hashSet.buckets[this.m_idx] == 0L);
				return true;
			}

			// Token: 0x060009D3 RID: 2515 RVA: 0x0002E28E File Offset: 0x0002C48E
			public void Reset()
			{
				this.m_idx = -1;
			}

			// Token: 0x060009D4 RID: 2516 RVA: 0x0002E297 File Offset: 0x0002C497
			public void Dispose()
			{
			}

			// Token: 0x040002E3 RID: 739
			private FastInt64HashSet m_hashSet;

			// Token: 0x040002E4 RID: 740
			private int m_idx = -1;
		}
	}
}
