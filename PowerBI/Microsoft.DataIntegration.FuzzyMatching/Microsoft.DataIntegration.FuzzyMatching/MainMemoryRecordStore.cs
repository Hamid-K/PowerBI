using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Runtime.Serialization;
using Microsoft.DataIntegration.FuzzyMatchingCommon;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Data;
using Microsoft.DataIntegration.FuzzyMatchingCommon.IO;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000068 RID: 104
	[Serializable]
	internal sealed class MainMemoryRecordStore : IRecordStore, IMemoryLimit, IMemoryUsage, ISessionable, IRawSerializable, ISerializable
	{
		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000445 RID: 1093 RVA: 0x00013944 File Offset: 0x00011B44
		// (set) Token: 0x06000446 RID: 1094 RVA: 0x0001394C File Offset: 0x00011B4C
		bool IRawSerializable.EnableRawSerialization { get; set; }

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000447 RID: 1095 RVA: 0x00013955 File Offset: 0x00011B55
		// (set) Token: 0x06000448 RID: 1096 RVA: 0x0001395D File Offset: 0x00011B5D
		int IRawSerializable.RawSerializationID { get; set; }

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000449 RID: 1097 RVA: 0x00013966 File Offset: 0x00011B66
		// (set) Token: 0x0600044A RID: 1098 RVA: 0x0001396E File Offset: 0x00011B6E
		public long MemoryLimit
		{
			get
			{
				return this.m_memoryLimit;
			}
			set
			{
				this.m_memoryLimit = value;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x0600044B RID: 1099 RVA: 0x00013977 File Offset: 0x00011B77
		public long MemoryUsage
		{
			get
			{
				return this.m_refTableRecords.MemoryUsage + (long)(this.m_freeRecordIds.Count * 4) + (long)(2 * (this.m_recordToRid.Count * 16)) + this.m_distinctAttributeValues.MemoryUsage;
			}
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x000139B4 File Offset: 0x00011BB4
		public MainMemoryRecordStore(FuzzyLookupDefinition indexDefinition)
		{
			this.m_indexDefinition = indexDefinition;
			this.MemoryLimit = long.MaxValue;
			this.m_refTableRecords = new MruCachedDictionary<int, object[]>(new Func<int, object[], long>(this.GetRecordMemory), new Func<int, object[], bool>(this.AllowRemoveRecord), new Func<bool>(this.MemoryLimitExceeded), 101);
			this.m_schemaRowsByOrdinal = new int[0];
			this.m_keyColumnComparer = new KeyColumnEqualityComparer(new int[0]);
			this.m_recordToRid = new Dictionary<object[], int[]>(this.m_keyColumnComparer);
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x00013A76 File Offset: 0x00011C76
		public IUpdateContext BeginUpdate(DataTable schemaTable)
		{
			this.m_schemaRowsByOrdinal = SchemaUtils.GetRowsByOrdinal(schemaTable);
			this.SetReferenceTableSchema(schemaTable, this.m_indexDefinition.KeyColumns);
			return null;
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x00013A97 File Offset: 0x00011C97
		public void EndUpdate(IUpdateContext _updateContext)
		{
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x00013A9C File Offset: 0x00011C9C
		protected MainMemoryRecordStore(SerializationInfo info, StreamingContext context)
		{
			((IRawSerializable)this).EnableRawSerialization = (bool)info.GetValue("EnableRawSerialization", typeof(bool));
			((IRawSerializable)this).RawSerializationID = (int)info.GetValue("RawSerializationID", typeof(int));
			this.m_indexDefinition = (FuzzyLookupDefinition)info.GetValue("m_indexDefinition", typeof(FuzzyLookupDefinition));
			this.m_memoryLimit = (long)info.GetValue("m_memoryLimit", typeof(long));
			this.SerializeRecords = (bool)info.GetValue("SerializeRecords", typeof(bool));
			this.m_nextRecordId = (int)info.GetValue("m_nextRecordId", typeof(int));
			this.m_freeRecordIds = (Stack<int>)info.GetValue("m_freeRecordIds", typeof(Stack<int>));
			this.m_isSimpleValueType = (BitArray)info.GetValue("m_isSimpleValueType", typeof(BitArray));
			this.ThrowOnMemoryLimitExceeded = (bool)info.GetValue("ThrowOnMemoryLimitExceeded", typeof(bool));
			this.m_keyColumnComparer = (KeyColumnEqualityComparer)info.GetValue("m_keyColumnComparer", typeof(KeyColumnEqualityComparer));
			this.m_schemaRowsByOrdinal = (int[])info.GetValue("m_schemaRowsByOrdinal", typeof(int[]));
			this.m_tmpRemoveRecordValues = (object[])info.GetValue("m_tmpRemoveRecordValues", typeof(object[]));
			if (this.SerializeRecords)
			{
				this.m_distinctAttributeValues = (DistinctValueManager)info.GetValue("m_distinctAttributeValues", typeof(DistinctValueManager));
				if (!((IRawSerializable)this).EnableRawSerialization)
				{
					this.m_refTableRecords = (MruCachedDictionary<int, object[]>)info.GetValue("m_refTableRecords", typeof(MruCachedDictionary<int, object[]>));
					this.m_recordToRid = (Dictionary<object[], int[]>)info.GetValue("m_recordToRid", typeof(Dictionary<object[], int[]>));
					return;
				}
			}
			else
			{
				this.m_refTableRecords = new MruCachedDictionary<int, object[]>(new Func<int, object[], long>(this.GetRecordMemory), new Func<int, object[], bool>(this.AllowRemoveRecord), new Func<bool>(this.MemoryLimitExceeded), 101);
				this.m_recordToRid = new Dictionary<object[], int[]>(this.m_keyColumnComparer);
				this.m_distinctAttributeValues = new DistinctValueManager();
			}
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x00013D2C File Offset: 0x00011F2C
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("EnableRawSerialization", ((IRawSerializable)this).EnableRawSerialization);
			info.AddValue("RawSerializationID", ((IRawSerializable)this).RawSerializationID);
			info.AddValue("m_indexDefinition", this.m_indexDefinition);
			info.AddValue("m_memoryLimit", this.m_memoryLimit);
			info.AddValue("SerializeRecords", this.SerializeRecords);
			info.AddValue("m_nextRecordId", this.m_nextRecordId);
			info.AddValue("m_freeRecordIds", this.m_freeRecordIds);
			info.AddValue("m_isSimpleValueType", this.m_isSimpleValueType);
			info.AddValue("ThrowOnMemoryLimitExceeded", this.ThrowOnMemoryLimitExceeded);
			info.AddValue("m_keyColumnComparer", this.m_keyColumnComparer);
			info.AddValue("m_schemaRowsByOrdinal", this.m_schemaRowsByOrdinal);
			info.AddValue("m_tmpRemoveRecordValues", this.m_tmpRemoveRecordValues);
			if (this.SerializeRecords)
			{
				info.AddValue("m_distinctAttributeValues", this.m_distinctAttributeValues);
				if (!((IRawSerializable)this).EnableRawSerialization)
				{
					info.AddValue("m_refTableRecords", this.m_refTableRecords);
					info.AddValue("m_recordToRid", this.m_recordToRid);
				}
			}
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x00013E48 File Offset: 0x00012048
		void IRawSerializable.Serialize(Stream s)
		{
			if (this.SerializeRecords)
			{
				StreamUtilities.WriteInt64(s, s.Position);
				StreamUtilities.WriteInt32(s, this.m_refTableRecords.Count);
				foreach (KeyValuePair<int, object[]> keyValuePair in this.m_refTableRecords)
				{
					object[] value = keyValuePair.Value;
					StreamUtilities.WriteInt32(s, keyValuePair.Key);
					for (int i = 0; i < value.Length; i++)
					{
						if (!this.IsSimpleType(i))
						{
							StreamUtilities.WriteInt32(s, (value[i] == null) ? 0 : this.m_distinctAttributeValues.GetSerializationKey(value[i]));
						}
						else
						{
							StreamUtilities.WriteSimpleValueType(s, value[i]);
						}
					}
				}
				StreamUtilities.WriteInt64(s, s.Position);
			}
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x00013F18 File Offset: 0x00012118
		private bool IsSimpleType(int ordinal)
		{
			return this.m_isSimpleValueType[ordinal];
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x00013F26 File Offset: 0x00012126
		private Type GetType(int ordinal)
		{
			return (Type)this.m_indexDefinition.RecordBinding.Schema.Rows[this.m_schemaRowsByOrdinal[ordinal]][SchemaTableColumn.DataType];
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x00013F59 File Offset: 0x00012159
		private int GetColumnSize(int ordinal)
		{
			return (int)this.m_indexDefinition.RecordBinding.Schema.Rows[this.m_schemaRowsByOrdinal[ordinal]][SchemaTableColumn.ColumnSize];
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x00013F8C File Offset: 0x0001218C
		void IRawSerializable.Deserialize(Stream s)
		{
			if (this.SerializeRecords)
			{
				if (s.Position != StreamUtilities.ReadInt64(s))
				{
					throw new SerializationException("Unexpected stream position encountered when deserializing.");
				}
				this.m_distinctAttributeValues.BuildSerializationKeyToValueIndex();
				int num = StreamUtilities.ReadInt32(s);
				this.m_refTableRecords = new MruCachedDictionary<int, object[]>(new Func<int, object[], long>(this.GetRecordMemory), new Func<int, object[], bool>(this.AllowRemoveRecord), new Func<bool>(this.MemoryLimitExceeded), num);
				this.m_recordToRid = new Dictionary<object[], int[]>(num, this.m_keyColumnComparer);
				for (int i = 0; i < num; i++)
				{
					int num2 = StreamUtilities.ReadInt32(s);
					object[] array = new object[this.m_schemaRowsByOrdinal.Length];
					for (int j = 0; j < array.Length; j++)
					{
						if (this.IsSimpleType(j))
						{
							array[j] = StreamUtilities.ReadSimpleValueType(s, this.GetType(j));
						}
						else
						{
							int num3 = StreamUtilities.ReadInt32(s);
							array[j] = ((num3 == 0) ? null : this.m_distinctAttributeValues.GetObjectBySerializationKey(num3));
						}
					}
					this.AddRecord(num2, array);
				}
				this.m_distinctAttributeValues.DropSerializationKeyToValueIndex();
				if (s.Position != StreamUtilities.ReadInt64(s))
				{
					throw new SerializationException("Unexpected stream position encountered when deserializing.");
				}
			}
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x000140B0 File Offset: 0x000122B0
		private long GetRecordMemory(int rid, object[] recordValues)
		{
			long num = (long)(20 + recordValues.Length * 8);
			for (int i = 0; i < recordValues.Length; i++)
			{
				if (this.IsSimpleType(i))
				{
					if (this.GetColumnSize(i) > 0)
					{
						num += (long)(16 + this.GetColumnSize(i));
					}
					else
					{
						num += Utilities.GetMemoryUsage(recordValues[i]);
					}
				}
			}
			return num;
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x00014104 File Offset: 0x00012304
		protected bool AllowRemoveRecord(int rid, object[] recordValues)
		{
			if (this.ThrowOnMemoryLimitExceeded)
			{
				throw new OutOfMemoryException("FuzzyLookup state manager has reached the memory limit and cannot continue.  Increase the FuzzyLookup.MemoryLimit property or provide a SQL connection string in the FuzzyLookup constructor to allow spillover onto disk.");
			}
			foreach (object obj in recordValues)
			{
				this.m_distinctAttributeValues.Remove(obj);
			}
			int[] array;
			this.m_recordToRid.TryGetValue(recordValues, ref array);
			for (int j = 0; j < array.Length; j++)
			{
				if (array[j] == rid)
				{
					if (array.Length == 1)
					{
						this.m_recordToRid.Remove(recordValues);
					}
					else
					{
						if (j < array.Length - 1)
						{
							for (int k = j; k < array.Length - 1; k++)
							{
								array[k] = array[k + 1];
							}
						}
						int[] array2 = array;
						Array.Resize<int>(ref array, array.Length - 1);
						if (array2 != array)
						{
							this.m_recordToRid[recordValues] = array;
						}
					}
				}
			}
			return true;
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x000141C8 File Offset: 0x000123C8
		protected bool MemoryLimitExceeded()
		{
			return this.MemoryUsage > this.MemoryLimit;
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x000141D8 File Offset: 0x000123D8
		public void AddRecord(int rid, object[] values)
		{
			for (int i = 0; i < values.Length; i++)
			{
				if (values[i] != null && values[i] != DBNull.Value && !this.IsSimpleType(i))
				{
					this.m_distinctAttributeValues.Add(values[i], out values[i]);
				}
			}
			int[] array;
			if (this.m_recordToRid.TryGetValue(values, ref array))
			{
				int[] array2 = array;
				Array.Resize<int>(ref array, array.Length + 1);
				array[array.Length - 1] = rid;
				if (array != array2)
				{
					this.m_recordToRid[values] = array;
				}
			}
			else
			{
				this.m_recordToRid.Add(values, new int[] { rid });
			}
			this.m_refTableRecords.Add(rid, values);
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x0001427C File Offset: 0x0001247C
		public int AddRecord(IDataRecord record)
		{
			if (record.FieldCount != this.m_schemaRowsByOrdinal.Length)
			{
				throw new ArgumentException("The record must have the same number of columns as the reference schema used in the RecordBinding of the FuzzyLookupDefinition.");
			}
			int num;
			if (this.m_freeRecordIds.Count <= 0)
			{
				int nextRecordId = this.m_nextRecordId;
				this.m_nextRecordId = nextRecordId + 1;
				num = nextRecordId;
			}
			else
			{
				num = this.m_freeRecordIds.Pop();
			}
			int num2 = num;
			if (this.MemoryLimit > 0L)
			{
				object[] array = new object[record.FieldCount];
				record.GetValues(array);
				this.AddRecord(num2, array);
			}
			return num2;
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x000142F8 File Offset: 0x000124F8
		public ISession CreateSession()
		{
			return new MainMemoryRecordStore.Session();
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x00014300 File Offset: 0x00012500
		public bool TryGetRecord(ISession session, int rid, ref object[] record)
		{
			object[] array;
			if (this.m_refTableRecords.TryGetValue(rid, out array))
			{
				for (int i = 0; i < record.Length; i++)
				{
					record[i] = array[i];
				}
				return true;
			}
			Array.Clear(record, 0, record.Length);
			return false;
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x00014344 File Offset: 0x00012544
		private static bool Equal(object[] x, object[] y)
		{
			for (int i = 0; i < x.Length; i++)
			{
				if (x[i] == null)
				{
					if (y[i] != null)
					{
						return false;
					}
				}
				else if (!x[i].Equals(y[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x0001437C File Offset: 0x0001257C
		public bool TryRemoveRecord(IDataRecord record, out int rid)
		{
			if (record.FieldCount != this.m_schemaRowsByOrdinal.Length)
			{
				throw new ArgumentException("The field count of this record does not match the field count of the reference table.");
			}
			record.GetValues(this.m_tmpRemoveRecordValues);
			int[] array;
			if (this.m_recordToRid.TryGetValue(this.m_tmpRemoveRecordValues, ref array))
			{
				int i = 0;
				while (i < array.Length)
				{
					rid = array[i];
					object[] array2;
					if (!this.m_refTableRecords.TryGetValue(rid, out array2))
					{
						throw new Exception("RecordId was unexpectedly not found!");
					}
					if (MainMemoryRecordStore.Equal(this.m_tmpRemoveRecordValues, array2))
					{
						if (!this.m_refTableRecords.Remove(rid))
						{
							throw new Exception("RecordId was unexpectedly unable to remove the record!");
						}
						this.m_freeRecordIds.Push(rid);
						if (array.Length == 1)
						{
							this.m_recordToRid.Remove(this.m_tmpRemoveRecordValues);
						}
						else
						{
							if (i < array.Length - 1)
							{
								for (int j = i; j < array.Length - 1; j++)
								{
									array[j] = array[j + 1];
								}
							}
							int[] array3 = array;
							Array.Resize<int>(ref array, array.Length - 1);
							if (array3 != array)
							{
								this.m_recordToRid[this.m_tmpRemoveRecordValues] = array;
							}
						}
						return true;
					}
					else
					{
						i++;
					}
				}
			}
			rid = 0;
			return false;
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x00014498 File Offset: 0x00012698
		private void SetReferenceTableSchema(DataTable schemaTable, List<Column> keyColumns)
		{
			this.m_isSimpleValueType = StreamUtilities.CreateIsSimpleValueBitArray(schemaTable);
			this.m_tmpRemoveRecordValues = new object[schemaTable.Rows.Count];
			int num = ((keyColumns != null) ? keyColumns.Count : 0);
			int[] array = new int[num];
			for (int i = 0; i < num; i++)
			{
				bool flag = false;
				foreach (object obj in schemaTable.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					if (keyColumns[i].Name.Equals(dataRow[SchemaTableColumn.ColumnName] as string))
					{
						array[i] = (int)dataRow[SchemaTableColumn.ColumnOrdinal];
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					throw new ArgumentException(string.Format("The specified key column '{0}' was not found in the reference table schema.  Note that naming is case-sensitive.", keyColumns[i].Name));
				}
			}
			this.m_keyColumnComparer = new KeyColumnEqualityComparer(array);
			this.m_recordToRid = new Dictionary<object[], int[]>(this.m_keyColumnComparer);
		}

		// Token: 0x04000164 RID: 356
		private int m_nextRecordId = 1;

		// Token: 0x04000165 RID: 357
		private MruCachedDictionary<int, object[]> m_refTableRecords;

		// Token: 0x04000166 RID: 358
		private Stack<int> m_freeRecordIds = new Stack<int>();

		// Token: 0x04000167 RID: 359
		private Dictionary<object[], int[]> m_recordToRid;

		// Token: 0x04000168 RID: 360
		private DistinctValueManager m_distinctAttributeValues = new DistinctValueManager();

		// Token: 0x04000169 RID: 361
		private KeyColumnEqualityComparer m_keyColumnComparer;

		// Token: 0x0400016A RID: 362
		private FuzzyLookupDefinition m_indexDefinition;

		// Token: 0x0400016B RID: 363
		private BitArray m_isSimpleValueType;

		// Token: 0x0400016C RID: 364
		private int[] m_schemaRowsByOrdinal;

		// Token: 0x0400016D RID: 365
		private object[] m_tmpRemoveRecordValues;

		// Token: 0x0400016E RID: 366
		public bool ThrowOnMemoryLimitExceeded = true;

		// Token: 0x04000171 RID: 369
		public bool SerializeRecords = true;

		// Token: 0x04000172 RID: 370
		private long m_memoryLimit = long.MaxValue;

		// Token: 0x02000158 RID: 344
		private class Session : ISession
		{
			// Token: 0x06000CC7 RID: 3271 RVA: 0x000371C7 File Offset: 0x000353C7
			public void Reset()
			{
			}
		}
	}
}
