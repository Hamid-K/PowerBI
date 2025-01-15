using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Globalization;
using SAP.Middleware.Connector;

namespace Microsoft.Mashup.SapBwProvider
{
	// Token: 0x02000007 RID: 7
	internal sealed class BapiDataReader : DbDataReader
	{
		// Token: 0x06000061 RID: 97 RVA: 0x00002E7C File Offset: 0x0000107C
		public BapiDataReader(SapBwConnection connection, IRfcTable outputTable, string outputTableName)
		{
			this.connection = connection;
			this.outputTable = outputTable;
			this.outputTableName = outputTableName;
			this.isClosed = outputTable.RowCount <= 0;
			this.currentIndex = -1;
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00002EB2 File Offset: 0x000010B2
		public string OutputTableName
		{
			get
			{
				return this.outputTableName;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00002EBA File Offset: 0x000010BA
		public IRfcTable OutputTable
		{
			get
			{
				return this.outputTable;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00002EC2 File Offset: 0x000010C2
		public override int Depth
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00002EC5 File Offset: 0x000010C5
		public override bool IsClosed
		{
			get
			{
				return this.isClosed;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00002ECD File Offset: 0x000010CD
		public override int RecordsAffected
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00002ED4 File Offset: 0x000010D4
		public override bool HasRows
		{
			get
			{
				return this.outputTable.RowCount > 0;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00002EE4 File Offset: 0x000010E4
		public override int FieldCount
		{
			get
			{
				return this.outputTable.ElementCount;
			}
		}

		// Token: 0x1700002D RID: 45
		public override object this[string name]
		{
			get
			{
				this.AssertData(null);
				return this.outputTable.CurrentRow[name].GetValue();
			}
		}

		// Token: 0x1700002E RID: 46
		public override object this[int i]
		{
			get
			{
				this.AssertData(new int?(i));
				return this.outputTable.GetValue(i);
			}
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002F40 File Offset: 0x00001140
		public override bool GetBoolean(int i)
		{
			this.AssertData(new int?(i));
			return this.outputTable.GetChar(i) == 'X';
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002F5E File Offset: 0x0000115E
		public override byte GetByte(int i)
		{
			this.AssertData(new int?(i));
			return this.outputTable.GetByte(i);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00002F78 File Offset: 0x00001178
		public override long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
		{
			this.AssertData(new int?(i));
			byte[] byteArray = this.outputTable.GetByteArray(i);
			Buffer.BlockCopy(byteArray, (int)fieldOffset, buffer, bufferoffset, (int)Math.Min((long)length, (long)byteArray.Length - fieldOffset));
			return Math.Min((long)length, (long)byteArray.Length - fieldOffset);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002FC6 File Offset: 0x000011C6
		public override char GetChar(int i)
		{
			this.AssertData(new int?(i));
			return this.outputTable.GetChar(i);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002FE0 File Offset: 0x000011E0
		public override long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002FE8 File Offset: 0x000011E8
		public override string GetDataTypeName(int i)
		{
			this.AssertColumnIndex(i);
			return this.outputTable.GetElementMetadata(i).DataType.ToString();
		}

		// Token: 0x06000071 RID: 113 RVA: 0x0000301B File Offset: 0x0000121B
		public override DateTime GetDateTime(int i)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003022 File Offset: 0x00001222
		public override decimal GetDecimal(int i)
		{
			this.AssertData(new int?(i));
			return this.outputTable.GetDecimal(i);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x0000303C File Offset: 0x0000123C
		public override double GetDouble(int i)
		{
			this.AssertData(new int?(i));
			return this.outputTable.GetDouble(i);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003058 File Offset: 0x00001258
		public override Type GetFieldType(int i)
		{
			this.AssertColumnIndex(i);
			RfcDataType dataType = this.outputTable.GetElementMetadata(i).DataType;
			Type type;
			if (BapiDataReader.rfcTypeToClrType.TryGetValue(dataType, out type))
			{
				return type;
			}
			throw this.connection.Helper.NewDataSourceError(Resources.InvalidTypeMapping(dataType));
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000030AF File Offset: 0x000012AF
		public override float GetFloat(int i)
		{
			this.AssertData(new int?(i));
			return this.outputTable.GetFloat(i);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000030C9 File Offset: 0x000012C9
		public override Guid GetGuid(int i)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000030D0 File Offset: 0x000012D0
		public override short GetInt16(int i)
		{
			this.AssertData(new int?(i));
			return this.outputTable.GetShort(i);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000030EA File Offset: 0x000012EA
		public override int GetInt32(int i)
		{
			this.AssertData(new int?(i));
			return this.outputTable.GetInt(i);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003104 File Offset: 0x00001304
		public override long GetInt64(int i)
		{
			this.AssertData(new int?(i));
			return this.outputTable.GetLong(i);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x0000311E File Offset: 0x0000131E
		public override string GetName(int i)
		{
			this.AssertColumnIndex(i);
			return this.outputTable.GetElementMetadata(i).Name;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003138 File Offset: 0x00001338
		public override int GetOrdinal(string name)
		{
			return this.outputTable.Metadata.TryNameToIndex(name);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x0000314C File Offset: 0x0000134C
		public override string GetString(int i)
		{
			object obj = this[i];
			if (this[i] is string)
			{
				return obj as string;
			}
			return obj.ToString();
		}

		// Token: 0x0600007D RID: 125 RVA: 0x0000317C File Offset: 0x0000137C
		public override object GetValue(int i)
		{
			return this[i];
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003188 File Offset: 0x00001388
		public override int GetValues(object[] values)
		{
			int num = ((values.Length < this.FieldCount) ? values.Length : this.FieldCount);
			for (int i = 0; i < num; i++)
			{
				values[i] = this[i];
			}
			return num;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000031C3 File Offset: 0x000013C3
		public override bool IsDBNull(int i)
		{
			return this[i] == DBNull.Value;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000031D3 File Offset: 0x000013D3
		public override bool NextResult()
		{
			return false;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000031D8 File Offset: 0x000013D8
		private void AssertData(int? i = null)
		{
			if (this.isClosed)
			{
				throw this.connection.Helper.NewDataSourceError(Resources.ReadNotCalled);
			}
			if (this.currentIndex < 0 || this.currentIndex >= this.outputTable.RowCount)
			{
				throw this.connection.Helper.NewDataSourceError(Resources.ReadNotCalled);
			}
			if (i != null)
			{
				this.AssertColumnIndex(i.Value);
			}
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003258 File Offset: 0x00001458
		private void AssertColumnIndex(int i)
		{
			if (i < 0 || i >= this.outputTable.ElementCount)
			{
				throw this.connection.Helper.NewDataSourceError(Resources.TableIndexOutOfRange(this.outputTableName, this.outputTable.ElementCount, i));
			}
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000032B0 File Offset: 0x000014B0
		public override bool Read()
		{
			if (!this.isClosed)
			{
				this.currentIndex++;
				if (this.currentIndex < this.outputTable.RowCount)
				{
					this.outputTable.CurrentIndex = this.currentIndex;
					return true;
				}
			}
			this.isClosed = true;
			return false;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003301 File Offset: 0x00001501
		public override void Close()
		{
			this.isClosed = true;
			this.currentIndex = -1;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003311 File Offset: 0x00001511
		public override IEnumerator GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003318 File Offset: 0x00001518
		public override DataTable GetSchemaTable()
		{
			if (this.schemaTable == null)
			{
				this.schemaTable = new DataTable("SchemaTable")
				{
					Locale = CultureInfo.InvariantCulture,
					MinimumCapacity = this.FieldCount
				};
				this.schemaTable.Columns.Add(SchemaTableColumn.ColumnName, typeof(string));
				this.schemaTable.Columns.Add(SchemaTableColumn.ColumnOrdinal, typeof(int));
				this.schemaTable.Columns.Add(SchemaTableColumn.ColumnSize, typeof(int));
				this.schemaTable.Columns.Add(SchemaTableColumn.NumericPrecision, typeof(short));
				this.schemaTable.Columns.Add(SchemaTableColumn.NumericScale, typeof(short));
				this.schemaTable.Columns.Add(SchemaTableColumn.BaseColumnName, typeof(string));
				this.schemaTable.Columns.Add(SchemaTableColumn.BaseTableName, typeof(string));
				this.schemaTable.Columns.Add(SchemaTableColumn.DataType, typeof(Type));
				this.schemaTable.Columns.Add(SchemaTableColumn.ProviderType, typeof(int));
				this.schemaTable.Columns.Add("DataTypeName", typeof(string));
				this.schemaTable.Columns.Add(SchemaTableColumn.AllowDBNull, typeof(bool));
				for (int i = 0; i < this.FieldCount; i++)
				{
					RfcElementMetadata elementMetadata = this.outputTable.GetElementMetadata(i);
					DataRow dataRow = this.schemaTable.NewRow();
					dataRow[0] = elementMetadata.Name;
					dataRow[1] = i;
					dataRow[2] = elementMetadata.NucLength;
					dataRow[3] = elementMetadata.Decimals;
					dataRow[4] = 0;
					dataRow[5] = elementMetadata.Name;
					dataRow[6] = this.outputTableName;
					Type type;
					if (BapiDataReader.rfcTypeToClrType.TryGetValue(elementMetadata.DataType, out type))
					{
						dataRow[7] = type;
					}
					dataRow[8] = elementMetadata.DataType;
					dataRow[9] = elementMetadata.DataType.ToString();
					dataRow[10] = true;
					this.schemaTable.Rows.Add(dataRow);
					dataRow.AcceptChanges();
				}
				foreach (object obj in this.schemaTable.Columns)
				{
					((DataColumn)obj).ReadOnly = false;
				}
				this.schemaTable.AcceptChanges();
			}
			return this.schemaTable;
		}

		// Token: 0x0400000B RID: 11
		private static readonly Dictionary<RfcDataType, Type> rfcTypeToClrType = new Dictionary<RfcDataType, Type>
		{
			{
				0,
				typeof(string)
			},
			{
				1,
				typeof(byte[])
			},
			{
				2,
				typeof(string)
			},
			{
				3,
				typeof(string)
			},
			{
				4,
				typeof(string)
			},
			{
				5,
				typeof(string)
			},
			{
				6,
				typeof(long)
			},
			{
				7,
				typeof(long)
			},
			{
				8,
				typeof(long)
			},
			{
				9,
				typeof(int)
			},
			{
				10,
				typeof(int)
			},
			{
				11,
				typeof(int)
			},
			{
				12,
				typeof(int)
			},
			{
				13,
				typeof(short)
			},
			{
				14,
				typeof(short)
			},
			{
				15,
				typeof(double)
			},
			{
				16,
				typeof(byte)
			},
			{
				17,
				typeof(short)
			},
			{
				18,
				typeof(int)
			},
			{
				19,
				typeof(long)
			},
			{
				20,
				typeof(string)
			},
			{
				21,
				typeof(string)
			},
			{
				22,
				typeof(string)
			},
			{
				23,
				typeof(byte[])
			},
			{
				24,
				typeof(IRfcStructureView)
			},
			{
				25,
				typeof(IRfcTableView)
			},
			{
				26,
				typeof(IRfcAbapObject)
			}
		};

		// Token: 0x0400000C RID: 12
		private readonly SapBwConnection connection;

		// Token: 0x0400000D RID: 13
		private readonly IRfcTable outputTable;

		// Token: 0x0400000E RID: 14
		private readonly string outputTableName;

		// Token: 0x0400000F RID: 15
		private DataTable schemaTable;

		// Token: 0x04000010 RID: 16
		private bool isClosed;

		// Token: 0x04000011 RID: 17
		private int currentIndex;
	}
}
