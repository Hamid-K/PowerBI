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
	// Token: 0x0200007D RID: 125
	[Serializable]
	public sealed class FastIntHashSet : IRawSerializable, ISerializable, IMemoryUsage, IEnumerable<int>, IEnumerable
	{
		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000536 RID: 1334 RVA: 0x0001EC57 File Offset: 0x0001CE57
		// (set) Token: 0x06000537 RID: 1335 RVA: 0x0001EC5F File Offset: 0x0001CE5F
		bool IRawSerializable.EnableRawSerialization { get; set; }

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000538 RID: 1336 RVA: 0x0001EC68 File Offset: 0x0001CE68
		// (set) Token: 0x06000539 RID: 1337 RVA: 0x0001EC70 File Offset: 0x0001CE70
		int IRawSerializable.RawSerializationID { get; set; }

		// Token: 0x0600053A RID: 1338 RVA: 0x0001EC79 File Offset: 0x0001CE79
		public FastIntHashSet()
			: this(0.75f)
		{
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x0001EC86 File Offset: 0x0001CE86
		public FastIntHashSet(float load)
		{
			this.load = 0.75f;
			base..ctor();
			this.load = Math.Min(load, 0.9f);
			this.buckets = new int[16];
			this.mask = 15;
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x0001ECC0 File Offset: 0x0001CEC0
		private FastIntHashSet(SerializationInfo info, StreamingContext context)
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
				this.buckets = (int[])info.GetValue("buckets", typeof(int[]));
			}
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x0001EDA8 File Offset: 0x0001CFA8
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

		// Token: 0x0600053E RID: 1342 RVA: 0x0001EE24 File Offset: 0x0001D024
		void IRawSerializable.Serialize(Stream s)
		{
			StreamUtilities.WriteInt64(s, s.Position);
			StreamUtilities.WriteInt32(s, this.buckets.Length);
			for (int i = 0; i < this.buckets.Length; i++)
			{
				StreamUtilities.WriteInt32(s, this.buckets[i]);
			}
			StreamUtilities.WriteInt64(s, s.Position);
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x0001EE78 File Offset: 0x0001D078
		void IRawSerializable.Deserialize(Stream s)
		{
			if (s.Position != StreamUtilities.ReadInt64(s))
			{
				throw new SerializationException("Unexpected stream position encountered when deserializing.");
			}
			this.buckets = new int[StreamUtilities.ReadInt32(s)];
			for (int i = 0; i < this.buckets.Length; i++)
			{
				this.buckets[i] = StreamUtilities.ReadInt32(s);
			}
			if (s.Position != StreamUtilities.ReadInt64(s))
			{
				throw new SerializationException("Unexpected stream position encountered when deserializing.");
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000540 RID: 1344 RVA: 0x0001EEE9 File Offset: 0x0001D0E9
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x0001EEF4 File Offset: 0x0001D0F4
		public void Add(int k)
		{
			if (FastIntHashSet.Empty == k)
			{
				throw new ArgumentException("Key may not be zero!");
			}
			if ((float)(this.count + 1) / (float)this.buckets.Length > this.load)
			{
				this.Rehash();
			}
			int num = k & this.mask;
			while (this.buckets[num] != FastIntHashSet.Empty)
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

		// Token: 0x06000542 RID: 1346 RVA: 0x0001EF80 File Offset: 0x0001D180
		public bool Contains(int k)
		{
			if (k == 0)
			{
				throw new ArgumentException("Key may not be zero!");
			}
			int num = k;
			for (;;)
			{
				num &= this.mask;
				if (this.buckets[num] == k)
				{
					break;
				}
				if (this.buckets[num++] == 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x0001EFC4 File Offset: 0x0001D1C4
		internal void Rehash()
		{
			if (this.mask == 2147483647)
			{
				throw new OverflowException("The hash table has reached the maximum size and is unable to grow further.");
			}
			int[] array = this.buckets;
			this.buckets = new int[this.buckets.Length * 2];
			this.mask = (this.mask << 1) | 1;
			int num = this.count;
			this.count = 0;
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] != FastIntHashSet.Empty)
				{
					this.Add(array[i]);
				}
			}
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x0001F044 File Offset: 0x0001D244
		public void Remove(int k)
		{
			if (FastIntHashSet.Empty == k)
			{
				throw new ArgumentException("Key may not be zero!");
			}
			int num = k & this.mask;
			while (this.buckets[num] != k)
			{
				if (this.buckets[num] == 0)
				{
					throw new KeyNotFoundException();
				}
				num = (num + 1) & this.mask;
			}
			this.buckets[num] = 0;
			this.count--;
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x0001F0AE File Offset: 0x0001D2AE
		public void Clear()
		{
			Array.Clear(this.buckets, 0, this.buckets.Length);
			this.count = 0;
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000546 RID: 1350 RVA: 0x0001F0CB File Offset: 0x0001D2CB
		public long MemoryUsage
		{
			get
			{
				return (long)(this.buckets.Length * 4);
			}
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x0001F0D8 File Offset: 0x0001D2D8
		public IEnumerator<int> GetEnumerator()
		{
			return new FastIntHashSet.Enumerator(this);
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x0001F0E0 File Offset: 0x0001D2E0
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new FastIntHashSet.Enumerator(this);
		}

		// Token: 0x040000EC RID: 236
		private static readonly int Empty;

		// Token: 0x040000ED RID: 237
		private readonly float load;

		// Token: 0x040000EE RID: 238
		private int[] buckets;

		// Token: 0x040000EF RID: 239
		private int mask;

		// Token: 0x040000F0 RID: 240
		private int count;

		// Token: 0x02000124 RID: 292
		[Serializable]
		private class Enumerator : IEnumerator<int>, IDisposable, IEnumerator
		{
			// Token: 0x060009DB RID: 2523 RVA: 0x0002E377 File Offset: 0x0002C577
			public Enumerator(FastIntHashSet hashSet)
			{
				this.m_hashSet = hashSet;
			}

			// Token: 0x17000195 RID: 405
			// (get) Token: 0x060009DC RID: 2524 RVA: 0x0002E38D File Offset: 0x0002C58D
			public int Current
			{
				get
				{
					return this.m_hashSet.buckets[this.m_idx];
				}
			}

			// Token: 0x17000196 RID: 406
			// (get) Token: 0x060009DD RID: 2525 RVA: 0x0002E3A1 File Offset: 0x0002C5A1
			object IEnumerator.Current
			{
				get
				{
					return this.m_hashSet.buckets[this.m_idx];
				}
			}

			// Token: 0x060009DE RID: 2526 RVA: 0x0002E3BC File Offset: 0x0002C5BC
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
				while (this.m_hashSet.buckets[this.m_idx] == 0);
				return true;
			}

			// Token: 0x060009DF RID: 2527 RVA: 0x0002E402 File Offset: 0x0002C602
			public void Reset()
			{
				this.m_idx = -1;
			}

			// Token: 0x060009E0 RID: 2528 RVA: 0x0002E40B File Offset: 0x0002C60B
			public void Dispose()
			{
			}

			// Token: 0x040002EC RID: 748
			private FastIntHashSet m_hashSet;

			// Token: 0x040002ED RID: 749
			private int m_idx = -1;
		}
	}
}
