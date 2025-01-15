using System;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Text;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Data.Sql
{
	// Token: 0x0200005A RID: 90
	public class BulkInsertStreamWriter : IDisposable
	{
		// Token: 0x06000309 RID: 777 RVA: 0x000156E4 File Offset: 0x000138E4
		public BulkInsertStreamWriter(Stream stream, DataTable schema)
		{
			this.m_encoding = Encoding.Unicode;
			this.m_binaryWriter = new BinaryWriter(stream, this.m_encoding);
			schema = SchemaUtils.CloneAndSortSchema(schema);
			this.m_schemaInfo = new BulkInsertStreamWriter.SchemaInfo[schema.Rows.Count];
			for (int i = 0; i < schema.Rows.Count; i++)
			{
				this.m_schemaInfo[i] = new BulkInsertStreamWriter.SchemaInfo
				{
					AllowNulls = (bool)schema.Rows[i][SchemaTableColumn.AllowDBNull],
					DataType = (Type)schema.Rows[i][SchemaTableColumn.DataType],
					ProviderType = (int)schema.Rows[i][SchemaTableColumn.ProviderType],
					ColumnSize = (int)schema.Rows[i][SchemaTableColumn.ColumnSize]
				};
			}
		}

		// Token: 0x0600030A RID: 778 RVA: 0x000157F7 File Offset: 0x000139F7
		public void Flush()
		{
			this.m_binaryWriter.Flush();
		}

		// Token: 0x0600030B RID: 779 RVA: 0x00015804 File Offset: 0x00013A04
		public void Dispose()
		{
			this.m_binaryWriter.Dispose();
		}

		// Token: 0x0600030C RID: 780 RVA: 0x00015814 File Offset: 0x00013A14
		private void WriteLength(int length, int numBytes)
		{
			switch (numBytes)
			{
			case 1:
				this.m_binaryWriter.Write((byte)length);
				return;
			case 2:
				this.m_binaryWriter.Write((ushort)length);
				return;
			case 3:
				break;
			case 4:
				this.m_binaryWriter.Write((uint)length);
				return;
			default:
				if (numBytes == 8)
				{
					this.m_binaryWriter.Write((ulong)((long)length));
					return;
				}
				break;
			}
			throw new InvalidOperationException();
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0001587C File Offset: 0x00013A7C
		public void Write(IDataRecord record)
		{
			for (int i = 0; i < record.FieldCount; i++)
			{
				object obj = record[i];
				int num;
				if (obj == null || obj is DBNull)
				{
					num = -1;
				}
				else if (obj is string)
				{
					num = 2 * (obj as string).Length;
				}
				else if (obj is byte[])
				{
					num = (obj as byte[]).Length;
				}
				else
				{
					num = this.m_schemaInfo[i].ColumnSize;
				}
				int num2 = (this.m_schemaInfo[i].AllowNulls ? BulkInsertStreamWriter.s_typeInfo[this.m_schemaInfo[i].ProviderType].NativeNull : BulkInsertStreamWriter.s_typeInfo[this.m_schemaInfo[i].ProviderType].NativeNotNull);
				if (this.m_schemaInfo[i].ColumnSize == 2147483647)
				{
					num2 = 8;
				}
				if (num2 > 0)
				{
					this.WriteLength(num, num2);
				}
				if (obj != null && !(obj is DBNull))
				{
					if (obj is string)
					{
						string text = obj as string;
						int byteCount = this.m_encoding.GetByteCount(text);
						if (this.m_buffer.Length < byteCount)
						{
							Array.Resize<byte>(ref this.m_buffer, byteCount);
						}
						this.m_encoding.GetBytes(text, 0, text.Length, this.m_buffer, 0);
						this.m_binaryWriter.BaseStream.Write(this.m_buffer, 0, byteCount);
					}
					else if (obj is int)
					{
						this.m_binaryWriter.Write((int)obj);
					}
					else if (obj is uint)
					{
						this.m_binaryWriter.Write((uint)obj);
					}
					else if (obj is long)
					{
						this.m_binaryWriter.Write((long)obj);
					}
					else if (obj is ulong)
					{
						this.m_binaryWriter.Write((ulong)obj);
					}
					else if (obj is short)
					{
						this.m_binaryWriter.Write((short)obj);
					}
					else if (obj is ushort)
					{
						this.m_binaryWriter.Write((ushort)obj);
					}
					else if (obj is byte[])
					{
						this.m_binaryWriter.BaseStream.Write((byte[])obj, 0, num);
					}
					else if (obj is bool)
					{
						this.m_binaryWriter.Write((bool)obj);
					}
					else if (obj is double)
					{
						this.m_binaryWriter.Write((double)obj);
					}
					else if (obj is float)
					{
						this.m_binaryWriter.Write((float)obj);
					}
					else
					{
						if (!(obj is byte))
						{
							throw new NotSupportedException(string.Format("Data type {0} is not supported.", obj.GetType().ToString()));
						}
						this.m_binaryWriter.Write((byte)obj);
					}
				}
			}
		}

		// Token: 0x0400007B RID: 123
		private Encoding m_encoding;

		// Token: 0x0400007C RID: 124
		private BinaryWriter m_binaryWriter;

		// Token: 0x0400007D RID: 125
		private byte[] m_buffer = new byte[0];

		// Token: 0x0400007E RID: 126
		private BulkInsertStreamWriter.SchemaInfo[] m_schemaInfo;

		// Token: 0x0400007F RID: 127
		private static readonly BulkInsertStreamWriter.SqlTypePrefixLengthInfo[] s_typeInfo = new BulkInsertStreamWriter.SqlTypePrefixLengthInfo[]
		{
			new BulkInsertStreamWriter.SqlTypePrefixLengthInfo(0, "bigint", 0, 1, 1, 1),
			new BulkInsertStreamWriter.SqlTypePrefixLengthInfo(1, "binary", 1, 1, 2, 2),
			new BulkInsertStreamWriter.SqlTypePrefixLengthInfo(2, "bit", 0, 1, 0, 1),
			new BulkInsertStreamWriter.SqlTypePrefixLengthInfo(3, "char", 2, 2, 2, 2),
			new BulkInsertStreamWriter.SqlTypePrefixLengthInfo(4, "datetime", 0, 1, 1, 1),
			new BulkInsertStreamWriter.SqlTypePrefixLengthInfo(5, "decimal", 1, 1, 1, 1),
			new BulkInsertStreamWriter.SqlTypePrefixLengthInfo(6, "float", 0, 1, 1, 1),
			new BulkInsertStreamWriter.SqlTypePrefixLengthInfo(7, "image", 4, 4, 4, 4),
			new BulkInsertStreamWriter.SqlTypePrefixLengthInfo(8, "int", 0, 1, 1, 1),
			new BulkInsertStreamWriter.SqlTypePrefixLengthInfo(9, "money", 0, 1, 1, 1),
			new BulkInsertStreamWriter.SqlTypePrefixLengthInfo(10, "nchar", 2, 2, 2, 2),
			new BulkInsertStreamWriter.SqlTypePrefixLengthInfo(11, "ntext", 4, 4, 1, 1),
			new BulkInsertStreamWriter.SqlTypePrefixLengthInfo(12, "nvarchar", 2, 2, 2, 2),
			new BulkInsertStreamWriter.SqlTypePrefixLengthInfo(13, "real", 0, 1, 1, 1),
			new BulkInsertStreamWriter.SqlTypePrefixLengthInfo(14, "uniqueidentifier", 1, 1, 1, 1),
			new BulkInsertStreamWriter.SqlTypePrefixLengthInfo(15, "smalldatetime", 0, 1, 1, 1),
			new BulkInsertStreamWriter.SqlTypePrefixLengthInfo(16, "smallint", 0, 1, 1, 1),
			new BulkInsertStreamWriter.SqlTypePrefixLengthInfo(17, "smallmoney", 0, 1, 1, 1),
			new BulkInsertStreamWriter.SqlTypePrefixLengthInfo(18, "text", 4, 4, 4, 4),
			new BulkInsertStreamWriter.SqlTypePrefixLengthInfo(19, "timestamp", 1, 1, 2, 2),
			new BulkInsertStreamWriter.SqlTypePrefixLengthInfo(20, "tinyint", 0, 1, 1, 1),
			new BulkInsertStreamWriter.SqlTypePrefixLengthInfo(21, "varbinary", 1, 1, 2, 2),
			new BulkInsertStreamWriter.SqlTypePrefixLengthInfo(22, "varchar", 2, 2, 2, 2)
		};

		// Token: 0x020000DF RID: 223
		private struct SchemaInfo
		{
			// Token: 0x0400022B RID: 555
			public bool AllowNulls;

			// Token: 0x0400022C RID: 556
			public Type DataType;

			// Token: 0x0400022D RID: 557
			public int ProviderType;

			// Token: 0x0400022E RID: 558
			public int ColumnSize;
		}

		// Token: 0x020000E0 RID: 224
		private struct SqlTypePrefixLengthInfo
		{
			// Token: 0x060008D7 RID: 2263 RVA: 0x0002CA5C File Offset: 0x0002AC5C
			public SqlTypePrefixLengthInfo(int SqlDbType, string TypeName, int NativeNotNull, int NativeNull, int CharNotNull, int CharNull)
			{
				this.SqlDbType = SqlDbType;
				this.TypeName = TypeName;
				this.NativeNotNull = NativeNotNull;
				this.NativeNull = NativeNull;
				this.CharNotNull = CharNotNull;
				this.CharNull = CharNull;
			}

			// Token: 0x060008D8 RID: 2264 RVA: 0x0002CA8B File Offset: 0x0002AC8B
			private bool IsVariableLength(SqlDbType type)
			{
				return type == 12 || type == 22 || type == 21 || type == 18 || type == 29 || type == 25 || type == 23 || type == 11 || type == 7 || type == 1;
			}

			// Token: 0x0400022F RID: 559
			public SqlDbType SqlDbType;

			// Token: 0x04000230 RID: 560
			public string TypeName;

			// Token: 0x04000231 RID: 561
			public int NativeNotNull;

			// Token: 0x04000232 RID: 562
			public int NativeNull;

			// Token: 0x04000233 RID: 563
			public int CharNotNull;

			// Token: 0x04000234 RID: 564
			public int CharNull;
		}
	}
}
