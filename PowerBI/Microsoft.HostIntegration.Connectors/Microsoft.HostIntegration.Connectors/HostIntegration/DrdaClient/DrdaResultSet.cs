using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.HostIntegration.Drda.Common;
using Microsoft.HostIntegration.Drda.Requester;

namespace Microsoft.HostIntegration.DrdaClient
{
	// Token: 0x02000A04 RID: 2564
	internal abstract class DrdaResultSet
	{
		// Token: 0x060050AA RID: 20650 RVA: 0x00142D2C File Offset: 0x00140F2C
		public DrdaResultSet(Microsoft.HostIntegration.Drda.Requester.IResultSet resultSet, DrdaConnection connection, ISqlStatement statement)
		{
			this._resultSet = resultSet;
			this._connection = connection;
			this._statement = statement;
		}

		// Token: 0x17001390 RID: 5008
		// (get) Token: 0x060050AB RID: 20651 RVA: 0x00142D49 File Offset: 0x00140F49
		public Microsoft.HostIntegration.Drda.Requester.IResultSet ResultSet
		{
			get
			{
				return this._resultSet;
			}
		}

		// Token: 0x17001391 RID: 5009
		// (get) Token: 0x060050AC RID: 20652 RVA: 0x00142D51 File Offset: 0x00140F51
		// (set) Token: 0x060050AD RID: 20653 RVA: 0x00142D59 File Offset: 0x00140F59
		public DrdaConnection Connection
		{
			get
			{
				return this._connection;
			}
			set
			{
				this._connection = value;
			}
		}

		// Token: 0x17001392 RID: 5010
		// (get) Token: 0x060050AE RID: 20654 RVA: 0x00142D62 File Offset: 0x00140F62
		public IRequester Requester
		{
			get
			{
				return this.Connection.Requester;
			}
		}

		// Token: 0x17001393 RID: 5011
		// (get) Token: 0x060050AF RID: 20655 RVA: 0x00142D6F File Offset: 0x00140F6F
		public ISqlStatement Statement
		{
			get
			{
				return this._statement;
			}
		}

		// Token: 0x060050B0 RID: 20656
		public abstract Task<bool> ReadAsync(QueryScrollOrientation orientation, long number, bool isAsync, CancellationToken cancellationToken);

		// Token: 0x060050B1 RID: 20657
		public abstract bool HasData();

		// Token: 0x17001394 RID: 5012
		// (get) Token: 0x060050B2 RID: 20658
		public abstract bool HasRows { get; }

		// Token: 0x060050B3 RID: 20659
		public abstract bool HasSchema();

		// Token: 0x060050B4 RID: 20660
		public abstract DrdaColumnBinding GetColumn(int i);

		// Token: 0x060050B5 RID: 20661 RVA: 0x000036A9 File Offset: 0x000018A9
		public virtual void Close()
		{
		}

		// Token: 0x17001395 RID: 5013
		// (get) Token: 0x060050B6 RID: 20662 RVA: 0x00006F04 File Offset: 0x00005104
		public virtual int FieldCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x060050B7 RID: 20663 RVA: 0x00142D78 File Offset: 0x00140F78
		public int GetValues(object[] values)
		{
			int num = 0;
			while (num < values.Length && num < this.FieldCount)
			{
				values[num] = this.GetValue(num);
				num++;
			}
			return num;
		}

		// Token: 0x060050B8 RID: 20664 RVA: 0x00142DAC File Offset: 0x00140FAC
		public int GetOrdinal(string name)
		{
			for (int i = 0; i < this.FieldCount; i++)
			{
				if (this.CultureAwareCompare(name, this.GetName(i)) == 0)
				{
					return i;
				}
			}
			throw new IndexOutOfRangeException("Could not find specified column in results");
		}

		// Token: 0x060050B9 RID: 20665 RVA: 0x00142DE8 File Offset: 0x00140FE8
		public bool TryGetOrdinal(string name, out int ordinal)
		{
			ordinal = -1;
			for (int i = 0; i < this.FieldCount; i++)
			{
				if (this.CultureAwareCompare(name, this.GetName(i)) == 0)
				{
					ordinal = i;
					return true;
				}
			}
			return false;
		}

		// Token: 0x060050BA RID: 20666 RVA: 0x00142E1F File Offset: 0x0014101F
		public string GetName(int i)
		{
			if (this.HasSchema())
			{
				return this.GetColumn(i).Name;
			}
			throw DrdaException.DataReaderNoData();
		}

		// Token: 0x060050BB RID: 20667 RVA: 0x00142E3B File Offset: 0x0014103B
		public string GetDataTypeName(int i)
		{
			if (this.HasSchema())
			{
				return this.GetColumn(i).Type.TypeName;
			}
			throw DrdaException.DataReaderNoData();
		}

		// Token: 0x060050BC RID: 20668 RVA: 0x00142E5C File Offset: 0x0014105C
		public Type GetFieldType(int i)
		{
			if (this.HasSchema())
			{
				return this.GetColumn(i).Type.ClassType;
			}
			throw DrdaException.DataReaderNoData();
		}

		// Token: 0x060050BD RID: 20669 RVA: 0x00142E7D File Offset: 0x0014107D
		public object GetValue(int i)
		{
			if (this.HasData())
			{
				return this.GetColumn(i).Value;
			}
			throw DrdaException.DataReaderNoData();
		}

		// Token: 0x060050BE RID: 20670 RVA: 0x00142E99 File Offset: 0x00141099
		public bool IsDBNull(int i)
		{
			return DBNull.Value.Equals(this.GetValue(i));
		}

		// Token: 0x060050BF RID: 20671 RVA: 0x00142EAC File Offset: 0x001410AC
		public DataTable GetSchemaTable(bool retrieveKeys)
		{
			DataTable dataTable = new DataTable("SchemaTable");
			dataTable.Locale = CultureInfo.InvariantCulture;
			if (this.HasSchema())
			{
				Dictionary<string, Dictionary<string, bool>> dictionary = new Dictionary<string, Dictionary<string, bool>>();
				dataTable.MinimumCapacity = this.FieldCount;
				DataColumn dataColumn = new DataColumn(SchemaTableColumn.ColumnName, typeof(string));
				DataColumn dataColumn2 = new DataColumn(SchemaTableColumn.ColumnOrdinal, typeof(int));
				DataColumn dataColumn3 = new DataColumn(SchemaTableColumn.ColumnSize, typeof(int));
				DataColumn dataColumn4 = new DataColumn(SchemaTableColumn.NumericPrecision, typeof(short));
				DataColumn dataColumn5 = new DataColumn(SchemaTableColumn.NumericScale, typeof(short));
				DataColumn dataColumn6 = new DataColumn(SchemaTableColumn.DataType, typeof(Type));
				DataColumn dataColumn7 = new DataColumn(SchemaTableColumn.ProviderType, typeof(DrdaClientType));
				DataColumn dataColumn8 = new DataColumn(SchemaTableColumn.IsLong, typeof(bool));
				DataColumn dataColumn9 = new DataColumn(SchemaTableColumn.AllowDBNull, typeof(bool));
				DataColumn dataColumn10 = new DataColumn(SchemaTableOptionalColumn.IsReadOnly, typeof(bool));
				DataColumn dataColumn11 = new DataColumn(SchemaTableOptionalColumn.IsRowVersion, typeof(bool));
				DataColumn dataColumn12 = new DataColumn(SchemaTableColumn.IsUnique, typeof(bool));
				DataColumn dataColumn13 = new DataColumn(SchemaTableColumn.IsKey, typeof(bool));
				DataColumn dataColumn14 = new DataColumn(SchemaTableOptionalColumn.IsAutoIncrement, typeof(bool));
				DataColumn dataColumn15 = new DataColumn(SchemaTableColumn.BaseSchemaName, typeof(string));
				DataColumn dataColumn16 = new DataColumn(SchemaTableOptionalColumn.BaseCatalogName, typeof(string));
				DataColumn dataColumn17 = new DataColumn(SchemaTableColumn.BaseTableName, typeof(string));
				DataColumn dataColumn18 = new DataColumn(SchemaTableColumn.BaseColumnName, typeof(string));
				dataColumn2.DefaultValue = 0;
				dataColumn8.DefaultValue = false;
				DataColumnCollection columns = dataTable.Columns;
				columns.Add(dataColumn);
				columns.Add(dataColumn2);
				columns.Add(dataColumn3);
				columns.Add(dataColumn4);
				columns.Add(dataColumn5);
				columns.Add(dataColumn6);
				columns.Add(dataColumn7);
				columns.Add(dataColumn8);
				columns.Add(dataColumn9);
				columns.Add(dataColumn10);
				columns.Add(dataColumn11);
				columns.Add(dataColumn12);
				columns.Add(dataColumn13);
				columns.Add(dataColumn14);
				columns.Add(dataColumn15);
				columns.Add(dataColumn16);
				columns.Add(dataColumn17);
				columns.Add(dataColumn18);
				for (int i = 0; i < this.FieldCount; i++)
				{
					DrdaColumnBinding column = this.GetColumn(i);
					DataRow dataRow = dataTable.NewRow();
					dataRow[dataColumn] = column.Name;
					dataRow[dataColumn2] = i;
					dataRow[dataColumn3] = column.Size;
					dataRow[dataColumn4] = column.Precision;
					dataRow[dataColumn5] = column.Scale;
					dataRow[dataColumn6] = column.Type.ClassType;
					dataRow[dataColumn7] = column.Type.DrdaType;
					dataRow[dataColumn8] = column.Type.IsLong;
					dataRow[dataColumn9] = column.IsNullable;
					dataRow[dataColumn10] = false;
					dataRow[dataColumn11] = false;
					dataRow[dataColumn12] = false;
					dataRow[dataColumn13] = column.IsKey;
					dataRow[dataColumn14] = column.GeneratedIdType == 2 || column.GeneratedIdType == 4;
					column.Schema = (string.IsNullOrWhiteSpace(column.Schema) ? string.Empty : column.Schema.Trim());
					column.Catalog = (string.IsNullOrWhiteSpace(column.Catalog) ? string.Empty : column.Catalog.Trim());
					column.BaseTable = (string.IsNullOrWhiteSpace(column.BaseTable) ? string.Empty : column.BaseTable.Trim());
					column.Name = (string.IsNullOrWhiteSpace(column.Name) ? string.Empty : column.Name.Trim());
					dataRow[dataColumn15] = column.Schema;
					dataRow[dataColumn16] = column.Catalog;
					dataRow[dataColumn17] = column.BaseTable;
					dataRow[dataColumn18] = column.Name;
					if (retrieveKeys && !string.IsNullOrWhiteSpace(column.Name) && !string.IsNullOrWhiteSpace(column.BaseTable))
					{
						string text = column.Schema + "." + column.BaseTable;
						Dictionary<string, bool> dictionary2 = null;
						if (!dictionary.TryGetValue(text, out dictionary2))
						{
							DataTable dataTable2;
							if (this._connection.Requester.Flavor == DrdaFlavor.Informix)
							{
								DrdaConnection drdaConnection = new DrdaConnection(this._connection);
								drdaConnection.Open();
								dataTable2 = drdaConnection.GetSchema("Indexes", new string[] { null, column.Schema, column.BaseTable });
								drdaConnection.Close();
							}
							else
							{
								dataTable2 = this._connection.GetSchema("Indexes", new string[] { null, column.Schema, column.BaseTable });
							}
							dictionary2 = new Dictionary<string, bool>();
							for (int j = 0; j < dataTable2.Rows.Count; j++)
							{
								DataRow dataRow2 = dataTable2.Rows[j];
								string text2 = dataRow2["ColumnName"] as string;
								if (!string.IsNullOrWhiteSpace(text2))
								{
									object obj = dataRow2["Unique"];
									if (obj is bool)
									{
										dictionary2[text2] = (bool)obj;
									}
									else
									{
										dictionary2[text2] = false;
									}
								}
							}
						}
						bool flag = false;
						if (dictionary2.TryGetValue(column.Name, out flag))
						{
							dataRow[dataColumn12] = flag;
							dataRow[dataColumn13] = true;
						}
					}
					dataTable.Rows.Add(dataRow);
					dataRow.AcceptChanges();
				}
				for (int k = 0; k < columns.Count; k++)
				{
					columns[k].ReadOnly = true;
				}
			}
			return dataTable;
		}

		// Token: 0x060050C0 RID: 20672 RVA: 0x0014352D File Offset: 0x0014172D
		public bool GetBoolean(int i)
		{
			return Convert.ToBoolean(this.GetValue(i));
		}

		// Token: 0x060050C1 RID: 20673 RVA: 0x0014353B File Offset: 0x0014173B
		public byte GetByte(int i)
		{
			return Convert.ToByte(this.GetValue(i));
		}

		// Token: 0x060050C2 RID: 20674 RVA: 0x0014354C File Offset: 0x0014174C
		public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			object value = this.GetValue(i);
			if (!(value is byte[]))
			{
				throw new InvalidCastException();
			}
			byte[] array = (byte[])value;
			if (buffer == null)
			{
				return (long)array.Length;
			}
			int num = buffer.Length - bufferOffset;
			if (bufferOffset + 1 > buffer.Length)
			{
				throw new InvalidCastException();
			}
			if (length <= 0 || num <= 0)
			{
				return 0L;
			}
			BinaryReader binaryReader = new BinaryReader(new MemoryStream(array));
			binaryReader.BaseStream.Seek(fieldOffset, SeekOrigin.Begin);
			byte[] array2 = binaryReader.ReadBytes(length);
			if (num > array2.Length)
			{
				array2.CopyTo(buffer, bufferOffset);
			}
			else
			{
				for (int j = 0; j < num; j++)
				{
					buffer[bufferOffset + j] = array2[j];
				}
			}
			return (long)Math.Min(num, array2.Length);
		}

		// Token: 0x060050C3 RID: 20675 RVA: 0x001435FA File Offset: 0x001417FA
		public char GetChar(int i)
		{
			return Convert.ToChar(this.GetValue(i));
		}

		// Token: 0x060050C4 RID: 20676 RVA: 0x00143608 File Offset: 0x00141808
		public long GetChars(int i, long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			object value = this.GetValue(i);
			if (!(value is string))
			{
				throw new InvalidCastException();
			}
			int num = buffer.Length - bufferOffset;
			if (bufferOffset + 1 > buffer.Length)
			{
				throw new InvalidCastException();
			}
			if (length <= 0 || num <= 0)
			{
				return 0L;
			}
			BinaryReader binaryReader = new BinaryReader(new MemoryStream((byte[])value));
			binaryReader.BaseStream.Seek(fieldOffset, SeekOrigin.Begin);
			char[] array = binaryReader.ReadChars(length);
			if (num > array.Length)
			{
				array.CopyTo(buffer, bufferOffset);
			}
			else
			{
				for (int j = 0; j < num; j++)
				{
					buffer[bufferOffset + j] = array[j];
				}
			}
			return (long)Math.Min(num, array.Length);
		}

		// Token: 0x060050C5 RID: 20677 RVA: 0x001436A4 File Offset: 0x001418A4
		public Guid GetGuid(int i)
		{
			object value = this.GetValue(i);
			if (value is string)
			{
				try
				{
					return new Guid((string)value);
				}
				catch (Exception)
				{
					throw new InvalidCastException();
				}
			}
			if (value is byte[])
			{
				try
				{
					return new Guid((byte[])value);
				}
				catch (Exception)
				{
					throw new InvalidCastException();
				}
			}
			throw new InvalidCastException();
		}

		// Token: 0x060050C6 RID: 20678 RVA: 0x00143718 File Offset: 0x00141918
		public short GetInt16(int i)
		{
			return Convert.ToInt16(this.GetValue(i));
		}

		// Token: 0x060050C7 RID: 20679 RVA: 0x00143726 File Offset: 0x00141926
		public int GetInt32(int i)
		{
			return Convert.ToInt32(this.GetValue(i));
		}

		// Token: 0x060050C8 RID: 20680 RVA: 0x00143734 File Offset: 0x00141934
		public long GetInt64(int i)
		{
			return Convert.ToInt64(this.GetValue(i));
		}

		// Token: 0x060050C9 RID: 20681 RVA: 0x00143742 File Offset: 0x00141942
		public float GetFloat(int i)
		{
			return Convert.ToSingle(this.GetValue(i));
		}

		// Token: 0x060050CA RID: 20682 RVA: 0x00143750 File Offset: 0x00141950
		public double GetDouble(int i)
		{
			return Convert.ToDouble(this.GetValue(i));
		}

		// Token: 0x060050CB RID: 20683 RVA: 0x00143760 File Offset: 0x00141960
		public string GetString(int i)
		{
			object value = this.GetValue(i);
			if (value == null || DBNull.Value.Equals(value))
			{
				throw new InvalidCastException();
			}
			return Convert.ToString(value);
		}

		// Token: 0x060050CC RID: 20684 RVA: 0x00143791 File Offset: 0x00141991
		public decimal GetDecimal(int i)
		{
			return Convert.ToDecimal(this.GetValue(i));
		}

		// Token: 0x060050CD RID: 20685 RVA: 0x0014379F File Offset: 0x0014199F
		public DateTime GetDateTime(int i)
		{
			return Convert.ToDateTime(this.GetValue(i));
		}

		// Token: 0x060050CE RID: 20686 RVA: 0x001437B0 File Offset: 0x001419B0
		public TimeSpan GetTimeSpan(int i)
		{
			object value = this.GetValue(i);
			if (value is TimeSpan)
			{
				return (TimeSpan)value;
			}
			throw new InvalidCastException();
		}

		// Token: 0x060050CF RID: 20687 RVA: 0x001437DD File Offset: 0x001419DD
		protected int CultureAwareCompare(string strA, string strB)
		{
			return CultureInfo.CurrentCulture.CompareInfo.Compare(strA, strB, CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth);
		}

		// Token: 0x04003F65 RID: 16229
		private DrdaConnection _connection;

		// Token: 0x04003F66 RID: 16230
		private ISqlStatement _statement;

		// Token: 0x04003F67 RID: 16231
		private Microsoft.HostIntegration.Drda.Requester.IResultSet _resultSet;
	}
}
