using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.DataIntegration.FuzzyMatchingCommon;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;
using Microsoft.DataIntegration.FuzzyMatchingCommon.IO;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x0200006A RID: 106
	[Serializable]
	internal class DistinctValueManager : IMemoryUsage, IRawSerializable, ISerializable
	{
		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x000146BA File Offset: 0x000128BA
		// (set) Token: 0x06000464 RID: 1124 RVA: 0x000146C2 File Offset: 0x000128C2
		bool IRawSerializable.EnableRawSerialization { get; set; }

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000465 RID: 1125 RVA: 0x000146CB File Offset: 0x000128CB
		// (set) Token: 0x06000466 RID: 1126 RVA: 0x000146D3 File Offset: 0x000128D3
		int IRawSerializable.RawSerializationID { get; set; }

		// Token: 0x06000467 RID: 1127 RVA: 0x000146DC File Offset: 0x000128DC
		public DistinctValueManager()
		{
			object obj;
			this.Add(DBNull.Value, out obj);
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x00014714 File Offset: 0x00012914
		protected DistinctValueManager(SerializationInfo info, StreamingContext context)
		{
			((IRawSerializable)this).EnableRawSerialization = (bool)info.GetValue("EnableRawSerialization", typeof(bool));
			((IRawSerializable)this).RawSerializationID = (int)info.GetValue("RawSerializationID", typeof(int));
			this.m_nextKey = (int)info.GetValue("m_nextKey", typeof(int));
			this.m_memoryUsage = (long)info.GetValue("m_memoryUsage", typeof(long));
			this.m_stringCount = (int)info.GetValue("m_stringCount", typeof(int));
			if (!((IRawSerializable)this).EnableRawSerialization)
			{
				this.m_distinctValues = (HashSetWithTryGetValue<object>)info.GetValue("m_distinctValues", typeof(HashSetWithTryGetValue<object>));
			}
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x00014808 File Offset: 0x00012A08
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("EnableRawSerialization", ((IRawSerializable)this).EnableRawSerialization);
			((IRawSerializable)this).RawSerializationID = -((IRawSerializable)this).RawSerializationID;
			info.AddValue("RawSerializationID", ((IRawSerializable)this).RawSerializationID);
			info.AddValue("m_nextKey", this.m_nextKey);
			info.AddValue("m_memoryUsage", this.m_memoryUsage);
			info.AddValue("m_stringCount", this.m_stringCount);
			if (!((IRawSerializable)this).EnableRawSerialization)
			{
				info.AddValue("m_distinctValues", this.m_distinctValues);
			}
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x00014890 File Offset: 0x00012A90
		void IRawSerializable.Serialize(Stream s)
		{
			StreamUtilities.WriteInt64(s, s.Position);
			List<object> list = new List<object>();
			int num = 0;
			foreach (object obj in this.m_distinctValues)
			{
				DistinctValueManager.DistinctAttributeValue distinctAttributeValue = obj as DistinctValueManager.DistinctAttributeValue;
				if (distinctAttributeValue.m_value is string)
				{
					StreamUtilities.WriteString(s, distinctAttributeValue.m_value as string);
					StreamUtilities.WriteInt32(s, distinctAttributeValue.m_count);
					StreamUtilities.WriteInt32(s, distinctAttributeValue.m_key);
					num++;
				}
				else
				{
					list.Add(distinctAttributeValue);
				}
			}
			new BinaryFormatter().Serialize(s, list);
			StreamUtilities.WriteInt64(s, s.Position);
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x0001494C File Offset: 0x00012B4C
		void IRawSerializable.Deserialize(Stream s)
		{
			if (s.Position != StreamUtilities.ReadInt64(s))
			{
				throw new SerializationException("Unexpected stream position encountered when deserializing.");
			}
			this.m_distinctValues = new HashSetWithTryGetValue<object>(DistinctValueManager.DistinctAttributeValue.EqualityComparer);
			for (int i = 0; i < this.m_stringCount; i++)
			{
				this.m_distinctValues.Add(new DistinctValueManager.DistinctAttributeValue(StreamUtilities.ReadString(s), StreamUtilities.ReadInt32(s), StreamUtilities.ReadInt32(s)));
			}
			List<object> list = (List<object>)new BinaryFormatter().Deserialize(s);
			this.m_distinctValues.AddRange(list);
			if (s.Position != StreamUtilities.ReadInt64(s))
			{
				throw new SerializationException("Unexpected stream position encountered when deserializing.");
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x0600046C RID: 1132 RVA: 0x000149EB File Offset: 0x00012BEB
		public long MemoryUsage
		{
			get
			{
				return (long)(2 * this.m_distinctValues.Count * 16) + this.m_memoryUsage;
			}
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x00014A08 File Offset: 0x00012C08
		public void BuildSerializationKeyToValueIndex()
		{
			this.m_serializationKeyToValue = new Dictionary<int, object>(this.m_distinctValues.Count);
			foreach (object obj in this.m_distinctValues)
			{
				DistinctValueManager.DistinctAttributeValue distinctAttributeValue = (DistinctValueManager.DistinctAttributeValue)obj;
				this.m_serializationKeyToValue.Add(distinctAttributeValue.m_key, distinctAttributeValue.m_value);
			}
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x00014A80 File Offset: 0x00012C80
		public void DropSerializationKeyToValueIndex()
		{
			this.m_serializationKeyToValue = null;
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x00014A89 File Offset: 0x00012C89
		public object GetObjectBySerializationKey(int key)
		{
			return this.m_serializationKeyToValue[key];
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x00014A98 File Offset: 0x00012C98
		public int GetSerializationKey(object value)
		{
			object obj;
			if (!this.m_distinctValues.TryGetValue(value, out obj))
			{
				throw new Exception("Value was unexpectedly not found!");
			}
			return (obj as DistinctValueManager.DistinctAttributeValue).m_key;
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x00014ACC File Offset: 0x00012CCC
		public void Add(object value, out object distinctValue)
		{
			object obj;
			if (!this.m_distinctValues.TryGetValue(value, out obj))
			{
				int num = 1;
				int nextKey = this.m_nextKey;
				this.m_nextKey = nextKey + 1;
				obj = new DistinctValueManager.DistinctAttributeValue(value, num, nextKey);
				this.m_distinctValues.Add(obj);
				distinctValue = value;
				if (value is string)
				{
					this.m_stringCount++;
				}
				this.m_memoryUsage += Utilities.GetMemoryUsage(value) + 16L + 8L + 4L;
				return;
			}
			DistinctValueManager.DistinctAttributeValue distinctAttributeValue = obj as DistinctValueManager.DistinctAttributeValue;
			distinctValue = distinctAttributeValue.m_value;
			distinctAttributeValue.m_count++;
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x00014B64 File Offset: 0x00012D64
		public bool Remove(object value)
		{
			object obj;
			if (this.m_distinctValues.TryGetValue(value, out obj))
			{
				DistinctValueManager.DistinctAttributeValue distinctAttributeValue = obj as DistinctValueManager.DistinctAttributeValue;
				DistinctValueManager.DistinctAttributeValue distinctAttributeValue2 = distinctAttributeValue;
				int num = distinctAttributeValue2.m_count - 1;
				distinctAttributeValue2.m_count = num;
				if (num == 0)
				{
					this.m_distinctValues.Remove(distinctAttributeValue);
					if (value is string)
					{
						this.m_stringCount--;
					}
					this.m_memoryUsage -= Utilities.GetMemoryUsage(value) + 16L + 8L + 4L;
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x00014BDE File Offset: 0x00012DDE
		public IEnumerator<object> GetEnumerator()
		{
			return this.m_distinctValues.GetEnumerator();
		}

		// Token: 0x04000174 RID: 372
		private int m_nextKey = 1;

		// Token: 0x04000175 RID: 373
		private long m_memoryUsage;

		// Token: 0x04000176 RID: 374
		private HashSetWithTryGetValue<object> m_distinctValues = new HashSetWithTryGetValue<object>(DistinctValueManager.DistinctAttributeValue.EqualityComparer);

		// Token: 0x04000177 RID: 375
		private int m_stringCount;

		// Token: 0x04000178 RID: 376
		[NonSerialized]
		private Dictionary<int, object> m_serializationKeyToValue;

		// Token: 0x02000159 RID: 345
		[Serializable]
		private class DistinctAttributeValue
		{
			// Token: 0x06000CC9 RID: 3273 RVA: 0x000371D1 File Offset: 0x000353D1
			public DistinctAttributeValue(object value, int count, int key)
			{
				this.m_value = value;
				this.m_count = count;
				this.m_key = key;
			}

			// Token: 0x040005A6 RID: 1446
			public static readonly IEqualityComparer<object> EqualityComparer = new DistinctValueManager.DistinctAttributeValue.DavEqualityComparer();

			// Token: 0x040005A7 RID: 1447
			public object m_value;

			// Token: 0x040005A8 RID: 1448
			public int m_count;

			// Token: 0x040005A9 RID: 1449
			public int m_key;

			// Token: 0x020001BE RID: 446
			private class DavEqualityComparer : IEqualityComparer<object>
			{
				// Token: 0x06000E52 RID: 3666 RVA: 0x0003C9C4 File Offset: 0x0003ABC4
				public bool Equals(object x, object y)
				{
					if (x == null)
					{
						return y == null;
					}
					if (x is DistinctValueManager.DistinctAttributeValue)
					{
						x = (x as DistinctValueManager.DistinctAttributeValue).m_value;
					}
					if (y is DistinctValueManager.DistinctAttributeValue)
					{
						y = (y as DistinctValueManager.DistinctAttributeValue).m_value;
					}
					return x.Equals(y);
				}

				// Token: 0x06000E53 RID: 3667 RVA: 0x0003C9FF File Offset: 0x0003ABFF
				public int GetHashCode(object x)
				{
					if (x is DistinctValueManager.DistinctAttributeValue)
					{
						x = (x as DistinctValueManager.DistinctAttributeValue).m_value;
					}
					return x.GetHashCode();
				}
			}
		}
	}
}
