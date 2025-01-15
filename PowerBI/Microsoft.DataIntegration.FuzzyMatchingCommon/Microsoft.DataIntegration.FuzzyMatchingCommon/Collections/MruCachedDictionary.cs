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
	// Token: 0x0200008F RID: 143
	[Serializable]
	public sealed class MruCachedDictionary<TKey, TValue> : IMemoryUsage, IMemoryLimit, IRawSerializable, ISerializable
	{
		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000632 RID: 1586 RVA: 0x00022795 File Offset: 0x00020995
		// (set) Token: 0x06000633 RID: 1587 RVA: 0x0002279D File Offset: 0x0002099D
		bool IRawSerializable.EnableRawSerialization { get; set; }

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000634 RID: 1588 RVA: 0x000227A6 File Offset: 0x000209A6
		// (set) Token: 0x06000635 RID: 1589 RVA: 0x000227AE File Offset: 0x000209AE
		int IRawSerializable.RawSerializationID { get; set; }

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000636 RID: 1590 RVA: 0x000227B7 File Offset: 0x000209B7
		// (set) Token: 0x06000637 RID: 1591 RVA: 0x000227BF File Offset: 0x000209BF
		public Action<Stream, TKey, TValue> RawSerializationDelegate { get; set; }

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000638 RID: 1592 RVA: 0x000227C8 File Offset: 0x000209C8
		// (set) Token: 0x06000639 RID: 1593 RVA: 0x000227D0 File Offset: 0x000209D0
		public MruCachedDictionary<TKey, TValue>.DeserializationDelegate RawDeserializationDelegate { get; set; }

		// Token: 0x0600063A RID: 1594 RVA: 0x000227D9 File Offset: 0x000209D9
		public MruCachedDictionary(Func<TKey, TValue, long> memoryUsageDelegate)
			: this(memoryUsageDelegate, null, null, 1)
		{
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x000227E5 File Offset: 0x000209E5
		public MruCachedDictionary(Func<TKey, TValue, long> memoryUsageDelegate, Func<TKey, TValue, bool> allowRemoveDelegate, Func<bool> memoryLimitExceededDelegate, int capacity)
			: this(null, memoryUsageDelegate, allowRemoveDelegate, memoryLimitExceededDelegate, capacity)
		{
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x000227F4 File Offset: 0x000209F4
		public MruCachedDictionary(IEqualityComparer<TKey> keyComparer, Func<TKey, TValue, long> memoryUsageDelegate, Func<TKey, TValue, bool> allowRemoveDelegate, Func<bool> memoryLimitExceededDelegate, int capacity)
		{
			this.m_memoryLimit = long.MaxValue;
			base..ctor();
			if (memoryUsageDelegate == null)
			{
				throw new ArgumentNullException("memoryUsageDelegate may not be null.");
			}
			this.m_keyComparer = keyComparer;
			this.GetMemoryUsage = memoryUsageDelegate;
			this.AllowRemove = ((allowRemoveDelegate != null) ? allowRemoveDelegate : new Func<TKey, TValue, bool>(MruCachedDictionary<TKey, TValue>.AllowRemoveDefault));
			this.MemoryLimitExceeded = ((memoryLimitExceededDelegate != null) ? memoryLimitExceededDelegate : new Func<bool>(this.MemoryLimitExceededDefault));
			this.m_dictionary = ((keyComparer != null) ? new Dictionary<TKey, MruCachedDictionary<TKey, TValue>.Node>(capacity, keyComparer) : new Dictionary<TKey, MruCachedDictionary<TKey, TValue>.Node>(capacity));
			this.m_head = new MruCachedDictionary<TKey, TValue>.Node(default(TKey), default(TValue), null, null);
			this.m_head.Next = (this.m_head.Prev = this.m_head);
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x000228BC File Offset: 0x00020ABC
		protected MruCachedDictionary(SerializationInfo info, StreamingContext context)
		{
			this.m_memoryLimit = long.MaxValue;
			base..ctor();
			((IRawSerializable)this).EnableRawSerialization = (bool)info.GetValue("EnableRawSerialization", typeof(bool));
			((IRawSerializable)this).RawSerializationID = (int)info.GetValue("RawSerializationID", typeof(int));
			this.m_memoryUsage = (long)info.GetValue("m_memoryUsage", typeof(long));
			this.m_memoryLimit = (long)info.GetValue("m_memoryLimit", typeof(long));
			this.GetMemoryUsage = (Func<TKey, TValue, long>)info.GetValue("GetMemoryUsage", typeof(Func<TKey, TValue, long>));
			this.AllowRemove = (Func<TKey, TValue, bool>)info.GetValue("AllowRemove", typeof(Func<TKey, TValue, bool>));
			this.OnRemoved = (Action<TKey, TValue>)info.GetValue("OnRemoved", typeof(Action<TKey, TValue>));
			this.MemoryLimitExceeded = (Func<bool>)info.GetValue("MemoryLimitExceeded", typeof(Func<bool>));
			if (this.MemoryLimitExceeded == null)
			{
				this.MemoryLimitExceeded = new Func<bool>(this.MemoryLimitExceededDefault);
			}
			this.RawSerializationDelegate = (Action<Stream, TKey, TValue>)info.GetValue("RawSerializationDelegate", typeof(Action<Stream, TKey, TValue>));
			this.RawDeserializationDelegate = (MruCachedDictionary<TKey, TValue>.DeserializationDelegate)info.GetValue("RawDeserializationDelegate", typeof(MruCachedDictionary<TKey, TValue>.DeserializationDelegate));
			this.m_keyComparer = (IEqualityComparer<TKey>)info.GetValue("m_keyComparer", typeof(IEqualityComparer<TKey>));
			if (!((IRawSerializable)this).EnableRawSerialization || this.RawSerializationDelegate == null || this.RawDeserializationDelegate == null)
			{
				this.m_head = (MruCachedDictionary<TKey, TValue>.Node)info.GetValue("m_head", typeof(MruCachedDictionary<TKey, TValue>.Node));
				this.m_dictionary = (Dictionary<TKey, MruCachedDictionary<TKey, TValue>.Node>)info.GetValue("m_dictionary", typeof(Dictionary<TKey, MruCachedDictionary<TKey, TValue>.Node>));
				return;
			}
			this.m_head = new MruCachedDictionary<TKey, TValue>.Node(default(TKey), default(TValue), null, null);
			this.m_head.Next = (this.m_head.Prev = this.m_head);
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x00022AEF File Offset: 0x00020CEF
		[SecurityCritical]
		[SecurityPermission(6, Flags = 128)]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			this.GetObjectData(info, context);
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x00022AFC File Offset: 0x00020CFC
		protected void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("EnableRawSerialization", ((IRawSerializable)this).EnableRawSerialization);
			info.AddValue("RawSerializationID", ((IRawSerializable)this).RawSerializationID);
			info.AddValue("m_memoryUsage", this.m_memoryUsage);
			info.AddValue("m_memoryLimit", this.m_memoryLimit);
			info.AddValue("GetMemoryUsage", this.GetMemoryUsage);
			info.AddValue("AllowRemove", this.AllowRemove);
			info.AddValue("OnRemoved", this.OnRemoved);
			info.AddValue("MemoryLimitExceeded", (new Func<bool>(this.MemoryLimitExceededDefault) == this.MemoryLimitExceeded) ? null : this.MemoryLimitExceeded);
			info.AddValue("RawSerializationDelegate", this.RawSerializationDelegate);
			info.AddValue("RawDeserializationDelegate", this.RawDeserializationDelegate);
			info.AddValue("m_keyComparer", this.m_keyComparer);
			if (!((IRawSerializable)this).EnableRawSerialization || this.RawSerializationDelegate == null || this.RawDeserializationDelegate == null)
			{
				info.AddValue("m_head", this.m_head);
				info.AddValue("m_dictionary", this.m_dictionary);
			}
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x00022C1C File Offset: 0x00020E1C
		void IRawSerializable.Serialize(Stream s)
		{
			StreamUtilities.WriteInt64(s, s.Position);
			StreamUtilities.WriteInt32(s, this.m_dictionary.Count);
			foreach (KeyValuePair<TKey, MruCachedDictionary<TKey, TValue>.Node> keyValuePair in this.m_dictionary)
			{
				this.RawSerializationDelegate.Invoke(s, keyValuePair.Key, keyValuePair.Value.Value);
			}
			StreamUtilities.WriteInt64(s, s.Position);
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x00022CB0 File Offset: 0x00020EB0
		void IRawSerializable.Deserialize(Stream s)
		{
			if (s.Position != StreamUtilities.ReadInt64(s))
			{
				throw new SerializationException("Unexpected stream position encountered when deserializing.");
			}
			int num = StreamUtilities.ReadInt32(s);
			this.m_dictionary = ((this.m_keyComparer != null) ? new Dictionary<TKey, MruCachedDictionary<TKey, TValue>.Node>(num, this.m_keyComparer) : new Dictionary<TKey, MruCachedDictionary<TKey, TValue>.Node>(num));
			for (int i = 0; i < num; i++)
			{
				TKey tkey;
				TValue tvalue;
				this.RawDeserializationDelegate(s, out tkey, out tvalue);
				this.Add(tkey, tvalue);
			}
			if (s.Position != StreamUtilities.ReadInt64(s))
			{
				throw new SerializationException("Unexpected stream position encountered when deserializing.");
			}
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x00022D3C File Offset: 0x00020F3C
		public bool MemoryLimitExceededDefault()
		{
			return this.m_memoryLimit < long.MaxValue && this.MemoryUsage > this.m_memoryLimit;
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x00022D5F File Offset: 0x00020F5F
		public static bool AllowRemoveDefault(TKey key, TValue value)
		{
			return true;
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000644 RID: 1604 RVA: 0x00022D62 File Offset: 0x00020F62
		// (set) Token: 0x06000645 RID: 1605 RVA: 0x00022D6A File Offset: 0x00020F6A
		public long MemoryLimit
		{
			get
			{
				return this.m_memoryLimit;
			}
			set
			{
				this.m_memoryLimit = value;
				this.Compact();
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000646 RID: 1606 RVA: 0x00022D79 File Offset: 0x00020F79
		public long MemoryUsage
		{
			get
			{
				return this.m_memoryUsage + (long)(this.m_dictionary.Count / 2 * 16);
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000647 RID: 1607 RVA: 0x00022D93 File Offset: 0x00020F93
		public int Count
		{
			get
			{
				return this.m_dictionary.Count;
			}
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x00022DA0 File Offset: 0x00020FA0
		public void Clear()
		{
			this.m_dictionary.Clear();
			this.m_head = new MruCachedDictionary<TKey, TValue>.Node(default(TKey), default(TValue), null, null);
			this.m_head.Next = (this.m_head.Prev = this.m_head);
			this.m_memoryUsage = 0L;
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x00022E00 File Offset: 0x00021000
		private void Compact()
		{
			MruCachedDictionary<TKey, TValue>.Node node = this.m_head.Next;
			while (node != this.m_head && this.MemoryLimitExceeded.Invoke())
			{
				this.Remove(node);
				node = this.m_head.Next;
			}
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x00022E48 File Offset: 0x00021048
		private bool Remove(MruCachedDictionary<TKey, TValue>.Node n)
		{
			bool flag = false;
			if (this.AllowRemove.Invoke(n.Key, n.Value))
			{
				n.Prev.Next = n.Next;
				n.Next.Prev = n.Prev;
				flag = this.m_dictionary.Remove(n.Key);
				this.m_memoryUsage -= MruCachedDictionary<TKey, TValue>.Node.MemoryUsage + this.GetMemoryUsage.Invoke(n.Key, n.Value);
				if (this.OnRemoved != null)
				{
					this.OnRemoved.Invoke(n.Key, n.Value);
				}
			}
			return flag;
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x00022EF0 File Offset: 0x000210F0
		public bool Remove(TKey k)
		{
			MruCachedDictionary<TKey, TValue>.Node node;
			return this.m_dictionary.TryGetValue(k, ref node) && this.Remove(node);
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x00022F18 File Offset: 0x00021118
		public void Add(TKey k, TValue v)
		{
			MruCachedDictionary<TKey, TValue>.Node node = new MruCachedDictionary<TKey, TValue>.Node(k, v, this.m_head, this.m_head.Prev);
			this.m_dictionary.Add(k, node);
			this.m_head.Prev.Next = node;
			this.m_head.Prev = node;
			this.m_memoryUsage += MruCachedDictionary<TKey, TValue>.Node.MemoryUsage + this.GetMemoryUsage.Invoke(k, v);
			this.Compact();
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x00022F8E File Offset: 0x0002118E
		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			foreach (KeyValuePair<TKey, MruCachedDictionary<TKey, TValue>.Node> keyValuePair in this.m_dictionary)
			{
				yield return new KeyValuePair<TKey, TValue>(keyValuePair.Key, keyValuePair.Value.Value);
			}
			Dictionary<TKey, MruCachedDictionary<TKey, TValue>.Node>.Enumerator enumerator = default(Dictionary<TKey, MruCachedDictionary<TKey, TValue>.Node>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x170000F1 RID: 241
		public TValue this[TKey key]
		{
			get
			{
				MruCachedDictionary<TKey, TValue>.Node node = this.m_dictionary[key];
				this.Touch(node);
				return node.Value;
			}
			set
			{
				MruCachedDictionary<TKey, TValue>.Node node;
				if (this.m_dictionary.TryGetValue(key, ref node))
				{
					node.Value = value;
					this.Touch(node);
					return;
				}
				this.Add(key, value);
			}
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x00022FFC File Offset: 0x000211FC
		public bool TryGetValue(TKey k, out TValue v)
		{
			MruCachedDictionary<TKey, TValue>.Node node;
			if (this.m_dictionary.TryGetValue(k, ref node))
			{
				this.Touch(node);
				v = node.Value;
				return true;
			}
			v = default(TValue);
			return false;
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x00023038 File Offset: 0x00021238
		private void Touch(MruCachedDictionary<TKey, TValue>.Node n)
		{
			if (n != this.m_head)
			{
				n.Prev.Next = n.Next;
				n.Next.Prev = n.Prev;
				n.Next = this.m_head;
				n.Prev = this.m_head.Prev;
				this.m_head.Prev.Next = n;
				this.m_head.Prev = n;
			}
		}

		// Token: 0x04000134 RID: 308
		private Dictionary<TKey, MruCachedDictionary<TKey, TValue>.Node> m_dictionary;

		// Token: 0x04000135 RID: 309
		private MruCachedDictionary<TKey, TValue>.Node m_head;

		// Token: 0x04000136 RID: 310
		private long m_memoryLimit;

		// Token: 0x04000137 RID: 311
		private long m_memoryUsage;

		// Token: 0x0400013C RID: 316
		public Func<TKey, TValue, long> GetMemoryUsage;

		// Token: 0x0400013D RID: 317
		public Func<TKey, TValue, bool> AllowRemove;

		// Token: 0x0400013E RID: 318
		public Action<TKey, TValue> OnRemoved;

		// Token: 0x0400013F RID: 319
		public Func<bool> MemoryLimitExceeded;

		// Token: 0x04000140 RID: 320
		private IEqualityComparer<TKey> m_keyComparer;

		// Token: 0x02000131 RID: 305
		// (Invoke) Token: 0x06000A02 RID: 2562
		public delegate void DeserializationDelegate(Stream s, out TKey key, out TValue value);

		// Token: 0x02000132 RID: 306
		[DebuggerDisplay("Key={Key} Value={Value} Next={Next} Prev={Prev} PinCount={PinCount}")]
		[Serializable]
		private sealed class Node
		{
			// Token: 0x06000A05 RID: 2565 RVA: 0x0002E776 File Offset: 0x0002C976
			public Node(TKey key, TValue value, MruCachedDictionary<TKey, TValue>.Node next, MruCachedDictionary<TKey, TValue>.Node prev)
			{
				this.Key = key;
				this.Value = value;
				this.Next = next;
				this.Prev = prev;
			}

			// Token: 0x1700019F RID: 415
			// (get) Token: 0x06000A06 RID: 2566 RVA: 0x0002E79B File Offset: 0x0002C99B
			public static long MemoryUsage
			{
				get
				{
					return 48L;
				}
			}

			// Token: 0x06000A07 RID: 2567 RVA: 0x0002E7A0 File Offset: 0x0002C9A0
			public void Reset()
			{
				this.Key = default(TKey);
				this.Value = default(TValue);
				this.Next = (this.Prev = null);
			}

			// Token: 0x04000314 RID: 788
			public TKey Key;

			// Token: 0x04000315 RID: 789
			public TValue Value;

			// Token: 0x04000316 RID: 790
			public MruCachedDictionary<TKey, TValue>.Node Next;

			// Token: 0x04000317 RID: 791
			public MruCachedDictionary<TKey, TValue>.Node Prev;
		}
	}
}
