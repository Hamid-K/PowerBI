using System;
using System.Data;
using System.IO;
using System.Text;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x02001058 RID: 4184
	internal class DbDataWriter : IDisposable
	{
		// Token: 0x06006D45 RID: 27973 RVA: 0x001786FC File Offset: 0x001768FC
		public DbDataWriter(Stream stream)
		{
			this.writer = new BinaryWriter(stream, new UTF8Encoding(false, false));
		}

		// Token: 0x06006D46 RID: 27974 RVA: 0x00178718 File Offset: 0x00176918
		public void WriteStartTable(IDataReader reader)
		{
			this.writer.WriteObjectTag(ObjectTag.Table);
			int fieldCount = reader.FieldCount;
			this.writer.Write(fieldCount);
			for (int i = 0; i < fieldCount; i++)
			{
				this.writer.Write(reader.GetName(i));
			}
			this.WriteSchemaTable(reader.GetSchemaTable());
		}

		// Token: 0x06006D47 RID: 27975 RVA: 0x0017876E File Offset: 0x0017696E
		public void WriteValue(object value)
		{
			this.writer.WriteObject(value);
		}

		// Token: 0x06006D48 RID: 27976 RVA: 0x0017877C File Offset: 0x0017697C
		public void WriteRow(IDataReader reader)
		{
			int fieldCount = reader.FieldCount;
			for (int i = 0; i < fieldCount; i++)
			{
				object value;
				try
				{
					value = DbData.GetValue(reader, i, null);
				}
				catch (ValueException value)
				{
				}
				this.writer.WriteObject(value);
			}
		}

		// Token: 0x06006D49 RID: 27977 RVA: 0x0000336E File Offset: 0x0000156E
		public void WriteEndTable()
		{
		}

		// Token: 0x06006D4A RID: 27978 RVA: 0x001787C8 File Offset: 0x001769C8
		public void Dispose()
		{
			this.writer.WriteObjectTag(ObjectTag.Eof);
		}

		// Token: 0x06006D4B RID: 27979 RVA: 0x001787D8 File Offset: 0x001769D8
		private void WriteSchemaTable(DataTable table)
		{
			DataColumnCollection columns = table.Columns;
			this.writer.Write(columns.Count);
			foreach (object obj in columns)
			{
				DataColumn dataColumn = (DataColumn)obj;
				this.writer.Write(dataColumn.ColumnName);
				this.writer.WriteObject(dataColumn.DataType);
			}
			DataRowCollection rows = table.Rows;
			this.writer.Write(rows.Count);
			foreach (object obj2 in table.Rows)
			{
				DataRow dataRow = (DataRow)obj2;
				for (int i = 0; i < columns.Count; i++)
				{
					this.writer.WriteObject(dataRow[i]);
				}
			}
		}

		// Token: 0x04003C9D RID: 15517
		private BinaryWriter writer;
	}
}
