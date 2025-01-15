using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Runtime.Serialization;
using Microsoft.DataIntegration.FuzzyMatchingCommon;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Data;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x0200006B RID: 107
	[Serializable]
	internal sealed class MainMemoryRecordStore2 : IRecordStore, IMemoryLimit, IMemoryUsage, ISessionable, IRawSerializable, ISerializable
	{
		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000474 RID: 1140 RVA: 0x00014BEB File Offset: 0x00012DEB
		// (set) Token: 0x06000475 RID: 1141 RVA: 0x00014BF3 File Offset: 0x00012DF3
		bool IRawSerializable.EnableRawSerialization { get; set; }

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000476 RID: 1142 RVA: 0x00014BFC File Offset: 0x00012DFC
		// (set) Token: 0x06000477 RID: 1143 RVA: 0x00014C04 File Offset: 0x00012E04
		int IRawSerializable.RawSerializationID { get; set; }

		// Token: 0x06000478 RID: 1144 RVA: 0x00014C0D File Offset: 0x00012E0D
		public MainMemoryRecordStore2(DataTable schemaTable, string[] keyColumnNames)
		{
			this.SetReferenceTableSchema(schemaTable, keyColumnNames);
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x00014C4C File Offset: 0x00012E4C
		protected MainMemoryRecordStore2(SerializationInfo info, StreamingContext context)
		{
			((IRawSerializable)this).EnableRawSerialization = (bool)info.GetValue("EnableRawSerialization", typeof(bool));
			((IRawSerializable)this).RawSerializationID = (int)info.GetValue("RawSerializationID", typeof(int));
			this.m_schema = (DataTable)info.GetValue("m_schema", typeof(DataTable));
			this.m_memoryLimit = (long)info.GetValue("m_memoryLimit", typeof(long));
			this.SerializeRecords = (bool)info.GetValue("SerializeRecords", typeof(bool));
			this.m_recordCount = (int)info.GetValue("m_recordCount", typeof(int));
			this.m_columnStoreTypes = (MainMemoryRecordStore2.StoreType[])info.GetValue("m_columnStoreTypes", typeof(MainMemoryRecordStore2.StoreType[]));
			this.m_keyColumnComparer = (KeyColumnEqualityComparer)info.GetValue("m_keyColumnComparer", typeof(KeyColumnEqualityComparer));
			if (this.SerializeRecords)
			{
				this.m_columnStores = (object[])info.GetValue("m_columnStores", typeof(object[]));
				return;
			}
			this.SetReferenceTableSchema(this.m_schema, this.m_keyColumns);
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x00014DC8 File Offset: 0x00012FC8
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("EnableRawSerialization", ((IRawSerializable)this).EnableRawSerialization);
			info.AddValue("RawSerializationID", ((IRawSerializable)this).RawSerializationID);
			info.AddValue("m_schema", this.m_schema);
			info.AddValue("m_memoryLimit", this.m_memoryLimit);
			info.AddValue("SerializeRecords", this.SerializeRecords);
			info.AddValue("m_recordCount", this.m_recordCount);
			info.AddValue("m_columnStoreTypes", this.m_columnStoreTypes);
			info.AddValue("m_keyColumnComparer", this.m_keyColumnComparer);
			if (this.SerializeRecords)
			{
				info.AddValue("m_columnStores", this.m_columnStores);
			}
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x00014E76 File Offset: 0x00013076
		public ISession CreateSession()
		{
			return null;
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600047C RID: 1148 RVA: 0x00014E79 File Offset: 0x00013079
		// (set) Token: 0x0600047D RID: 1149 RVA: 0x00014E81 File Offset: 0x00013081
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

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x0600047E RID: 1150 RVA: 0x00014E8C File Offset: 0x0001308C
		public long MemoryUsage
		{
			get
			{
				long num = 0L;
				for (int i = 0; i < this.m_columnStores.Length; i++)
				{
					if (this.m_columnStores[i] is IMemoryUsage)
					{
						num += (this.m_columnStores[i] as IMemoryUsage).MemoryUsage;
					}
				}
				return num;
			}
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x00014ED4 File Offset: 0x000130D4
		public IUpdateContext BeginUpdate(DataTable schemaTable)
		{
			for (int i = 0; i < this.m_columnStores.Length; i++)
			{
				if (this.m_columnStores[i] is CompactStringStore)
				{
					(this.m_columnStores[i] as CompactStringStore).BeginUpdate();
				}
			}
			return null;
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x00014F18 File Offset: 0x00013118
		public void EndUpdate(IUpdateContext updateContext)
		{
			for (int i = 0; i < this.m_columnStores.Length; i++)
			{
				if (this.m_columnStores[i] is CompactStringStore)
				{
					(this.m_columnStores[i] as CompactStringStore).EndUpdate();
				}
			}
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x00014F59 File Offset: 0x00013159
		void IRawSerializable.Serialize(Stream s)
		{
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x00014F5B File Offset: 0x0001315B
		void IRawSerializable.Deserialize(Stream s)
		{
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x00014F5D File Offset: 0x0001315D
		protected bool AllowRemoveRecord(int rid, object[] recordValues)
		{
			return false;
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x00014F60 File Offset: 0x00013160
		public void AddRecord(int rid, object[] values)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x00014F68 File Offset: 0x00013168
		public int AddRecord(IDataRecord record)
		{
			if (record.FieldCount != this.m_schema.Rows.Count)
			{
				throw new ArgumentException("The record must have the same number of columns as the record schema.");
			}
			this.m_recordCount++;
			int i = 0;
			while (i < record.FieldCount)
			{
				switch (this.m_columnStoreTypes[i])
				{
				case MainMemoryRecordStore2.StoreType.Object:
					goto IL_00CB;
				case MainMemoryRecordStore2.StoreType.String:
					if (record.IsDBNull(i))
					{
						string text = null;
						((CompactStringStore)this.m_columnStores[i]).AddString(text);
					}
					else
					{
						((CompactStringStore)this.m_columnStores[i]).AddString(record.GetString(i));
					}
					break;
				case MainMemoryRecordStore2.StoreType.Int32:
					if (record.IsDBNull(i))
					{
						((List<int>)this.m_columnStores[i]).Add(0);
					}
					else
					{
						((List<int>)this.m_columnStores[i]).Add(record.GetInt32(i));
					}
					break;
				default:
					goto IL_00CB;
				}
				IL_00E4:
				i++;
				continue;
				IL_00CB:
				((List<object>)this.m_columnStores[i]).Add(record.GetValue(i));
				goto IL_00E4;
			}
			return this.m_recordCount;
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x00015070 File Offset: 0x00013270
		public bool TryGetRecord(ISession session, int rid, ref object[] record)
		{
			if (record.Length != this.m_schema.Rows.Count)
			{
				throw new Exception("Array length does not match record length.");
			}
			if (rid > this.m_recordCount)
			{
				Array.Clear(record, 0, record.Length);
				return false;
			}
			record = new object[this.m_schema.Rows.Count];
			int i = 0;
			while (i < record.Length)
			{
				bool flag = true;
				switch (this.m_columnStoreTypes[i])
				{
				case MainMemoryRecordStore2.StoreType.Object:
					goto IL_00B0;
				case MainMemoryRecordStore2.StoreType.String:
				{
					string text;
					flag = ((CompactStringStore)this.m_columnStores[i]).TryGetString(rid, out text);
					record[i] = text;
					break;
				}
				case MainMemoryRecordStore2.StoreType.Int32:
					record[i] = ((List<int>)this.m_columnStores[i])[rid];
					break;
				default:
					goto IL_00B0;
				}
				IL_00C7:
				if (!flag)
				{
					throw new Exception("Unexpectedly failed to get column value.");
				}
				i++;
				continue;
				IL_00B0:
				record[i] = ((List<object>)this.m_columnStores[i])[rid];
				goto IL_00C7;
			}
			return true;
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x00015161 File Offset: 0x00013361
		public bool TryRemoveRecord(IDataRecord record, out int rid)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x00015168 File Offset: 0x00013368
		private void SetReferenceTableSchema(DataTable schemaTable, string[] keyColumnNames)
		{
			this.m_schema = schemaTable;
			this.m_keyColumns = keyColumnNames;
			if (!SchemaUtils.SchemaRowOrderMatchesColumnOrdinal(schemaTable))
			{
				throw new ArgumentException("Schema table column ordinal column does not match the schema table row order.");
			}
			this.m_columnStoreTypes = new MainMemoryRecordStore2.StoreType[schemaTable.Rows.Count];
			this.m_columnStores = new object[schemaTable.Rows.Count];
			for (int i = 0; i < schemaTable.Rows.Count; i++)
			{
				if (SchemaUtils.IsStringType((Type)schemaTable.Rows[i][SchemaTableColumn.DataType]))
				{
					this.m_columnStoreTypes[i] = MainMemoryRecordStore2.StoreType.String;
					this.m_columnStores[i] = new CompactStringStore();
				}
				else if (typeof(int) == (Type)schemaTable.Rows[i][SchemaTableColumn.DataType])
				{
					this.m_columnStoreTypes[i] = MainMemoryRecordStore2.StoreType.Int32;
					this.m_columnStores[i] = new List<int>();
					((List<int>)this.m_columnStores[i]).Add(0);
				}
				else
				{
					this.m_columnStoreTypes[i] = MainMemoryRecordStore2.StoreType.Object;
					this.m_columnStores[i] = new List<object>();
					((List<object>)this.m_columnStores[i]).Add(null);
				}
			}
		}

		// Token: 0x0400017B RID: 379
		private int m_recordCount;

		// Token: 0x0400017C RID: 380
		private object[] m_columnStores = new object[0];

		// Token: 0x0400017D RID: 381
		private MainMemoryRecordStore2.StoreType[] m_columnStoreTypes = new MainMemoryRecordStore2.StoreType[0];

		// Token: 0x0400017E RID: 382
		private DataTable m_schema;

		// Token: 0x0400017F RID: 383
		private string[] m_keyColumns;

		// Token: 0x04000180 RID: 384
		private KeyColumnEqualityComparer m_keyColumnComparer;

		// Token: 0x04000183 RID: 387
		public bool SerializeRecords = true;

		// Token: 0x04000184 RID: 388
		private long m_memoryLimit = long.MaxValue;

		// Token: 0x0200015A RID: 346
		private enum StoreType
		{
			// Token: 0x040005AB RID: 1451
			Object,
			// Token: 0x040005AC RID: 1452
			String,
			// Token: 0x040005AD RID: 1453
			Int32
		}
	}
}
