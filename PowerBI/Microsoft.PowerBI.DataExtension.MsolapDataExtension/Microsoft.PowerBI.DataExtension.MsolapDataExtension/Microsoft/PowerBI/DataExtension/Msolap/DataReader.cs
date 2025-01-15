using System;
using Microsoft.PowerBI.DataExtension.Contracts.Hosting;
using Microsoft.PowerBI.DataExtension.Contracts.Internal;
using MsolapWrapper;

namespace Microsoft.PowerBI.DataExtension.Msolap
{
	// Token: 0x0200000E RID: 14
	internal sealed class DataReader : IDataReader, IDisposable
	{
		// Token: 0x06000034 RID: 52 RVA: 0x0000281B File Offset: 0x00000A1B
		internal DataReader(DataReader reader, IPrivateInformationService piiService)
		{
			this._reader = reader;
			this._piiService = piiService;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002834 File Offset: 0x00000A34
		public int ColumnCount
		{
			get
			{
				int columnCount;
				try
				{
					columnCount = (int)this._reader.GetColumnCount();
				}
				catch (MsolapWrapperException ex)
				{
					throw DataExtensionErrorUtils.CreateDataExtensionException(ex, this._piiService, "Failed to get the column count from the data reader.", new object[0]);
				}
				return columnCount;
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002878 File Offset: 0x00000A78
		public bool MoveNext()
		{
			bool flag;
			try
			{
				flag = this._reader.MoveNext();
			}
			catch (MsolapWrapperException ex)
			{
				throw DataExtensionErrorUtils.CreateDataExtensionException(ex, this._piiService, "Failed to move the data reader to the next row.", new object[0]);
			}
			return flag;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000028BC File Offset: 0x00000ABC
		public bool NextResult()
		{
			bool flag;
			try
			{
				flag = this._reader.NextResult();
			}
			catch (MsolapWrapperException ex)
			{
				throw DataExtensionErrorUtils.CreateDataExtensionException(ex, this._piiService, "Failed to move the data reader to the next result.", new object[0]);
			}
			return flag;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002900 File Offset: 0x00000B00
		public object GetValue(int index)
		{
			object value;
			try
			{
				value = this._reader.GetValue((uint)index);
			}
			catch (MsolapWrapperException ex)
			{
				throw DataExtensionErrorUtils.CreateDataExtensionException(ex, this._piiService, "Failed to get the value of the column at index {0} from the data reader.", new object[] { index });
			}
			return value;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002950 File Offset: 0x00000B50
		public int GetOrdinal(string name)
		{
			int ordinal;
			try
			{
				ordinal = (int)this._reader.GetOrdinal(name);
			}
			catch (MsolapWrapperException ex)
			{
				throw DataExtensionErrorUtils.CreateDataExtensionException(ex, this._piiService, "Failed to get the ordinal of column '{0}' from the data reader. Column count: {1}. Existing columns: [{2}]", new object[]
				{
					this._piiService.MarkAsPrivate(name),
					this._reader.GetColumnCount(),
					this.GetColumnNamesTrace()
				});
			}
			return ordinal;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000029C0 File Offset: 0x00000BC0
		public string GetColumnName(int index)
		{
			string columnName;
			try
			{
				columnName = this._reader.GetColumnName((uint)index);
			}
			catch (MsolapWrapperException ex)
			{
				throw DataExtensionErrorUtils.CreateDataExtensionException(ex, this._piiService, "Failed to get the column name at index {0} from the data reader. Column count: {1}. Existing columns: [{2}]", new object[]
				{
					index,
					this._reader.GetColumnCount(),
					this.GetColumnNamesTrace()
				});
			}
			return columnName;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002A2C File Offset: 0x00000C2C
		public Type GetColumnType(int index)
		{
			Type columnType;
			try
			{
				columnType = this._reader.GetColumnType((uint)index);
			}
			catch (MsolapWrapperException ex)
			{
				throw DataExtensionErrorUtils.CreateDataExtensionException(ex, this._piiService, "Failed to get the column type at index {0} from the data reader. Column count: {1}. Existing columns: [{2}]", new object[]
				{
					index,
					this._reader.GetColumnCount(),
					this.GetColumnNamesTrace()
				});
			}
			return columnType;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002A98 File Offset: 0x00000C98
		public void GetValues(object[] values)
		{
			int columnCount = this.ColumnCount;
			try
			{
				uint num = 0U;
				while ((ulong)num < (ulong)((long)columnCount))
				{
					values[(int)num] = this._reader.GetValue(num);
					num += 1U;
				}
			}
			catch (MsolapWrapperException ex)
			{
				throw DataExtensionErrorUtils.CreateDataExtensionException(ex, this._piiService, "Failed to get the values of the current row from the data reader.", new object[0]);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002AF4 File Offset: 0x00000CF4
		public bool IsOpen
		{
			get
			{
				return this._reader.IsOpen();
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002B01 File Offset: 0x00000D01
		public void Close()
		{
			this._reader.Close();
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002B0E File Offset: 0x00000D0E
		public void Dispose()
		{
			this._reader.Dispose();
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002B1C File Offset: 0x00000D1C
		private string GetColumnNamesTrace()
		{
			string[] columnNames = this._reader.GetColumnNames();
			return this._piiService.MarkAsPrivate(string.Join(",", columnNames));
		}

		// Token: 0x04000048 RID: 72
		private readonly DataReader _reader;

		// Token: 0x04000049 RID: 73
		private readonly IPrivateInformationService _piiService;
	}
}
