using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase.Serialization;

namespace Microsoft.PowerBI.DataMovement.Pipeline.PowerBIPipeline
{
	// Token: 0x02000007 RID: 7
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public class CommaSeparatedValuesStreamWriter : DataStreamWriter
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000020B3 File Offset: 0x000002B3
		public CommaSeparatedValuesStreamWriter(Stream outputStream)
			: this(new List<Stream> { outputStream })
		{
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020C7 File Offset: 0x000002C7
		public CommaSeparatedValuesStreamWriter(List<Stream> outputStreams)
			: base(outputStreams)
		{
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000020D0 File Offset: 0x000002D0
		internal Task WriteToCsvAsync(IRawDataPageReader pageReader)
		{
			return this.WriteTableAsync(pageReader);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000020DC File Offset: 0x000002DC
		internal override async Task WritePageData(List<IColumn> columns, int rowsCount, StreamWriter outputStreamWriter)
		{
			if (columns == null || columns.Count == 0)
			{
				throw new ArgumentException("columns should contain at least one column", "columns");
			}
			if (rowsCount != 0)
			{
				for (int row = 0; row < rowsCount; row++)
				{
					for (int column = 0; column < columns.Count; column++)
					{
						object value = columns[column].GetObject(row);
						if (column > 0)
						{
							await outputStreamWriter.WriteAsync(',');
						}
						if (value != null)
						{
							string text = CommaSeparatedValuesStreamWriter.EscapeString(value);
							if (!string.IsNullOrEmpty(text))
							{
								await outputStreamWriter.WriteAsync(text);
							}
						}
						value = null;
					}
					await outputStreamWriter.WriteLineAsync();
				}
				await outputStreamWriter.FlushAsync();
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002130 File Offset: 0x00000330
		internal override async Task WriteHeader(DataTable schemaTable, StreamWriter outputStreamWriter)
		{
			if (schemaTable.Rows.Count == 0)
			{
				throw new ArgumentException("schemaTable should contain at least one column", "schemaTable");
			}
			for (int column = 0; column < schemaTable.Rows.Count; column++)
			{
				string columnName = (string)schemaTable.Rows[column]["ColumnName"];
				if (column > 0)
				{
					await outputStreamWriter.WriteAsync(',');
				}
				await outputStreamWriter.WriteAsync(CommaSeparatedValuesStreamWriter.EscapeString(columnName));
				columnName = null;
			}
			await outputStreamWriter.WriteLineAsync();
			await outputStreamWriter.FlushAsync();
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000217B File Offset: 0x0000037B
		protected override void Dispose(bool disposing)
		{
			if (this.m_disposed)
			{
				return;
			}
			this.m_disposed = true;
			base.Dispose(disposing);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002194 File Offset: 0x00000394
		private static string EscapeString(object value)
		{
			if (value == null)
			{
				return string.Empty;
			}
			value = DataStreamWriter.ParseOleDbType(value);
			string text;
			if (value is double)
			{
				text = ((double)value).ToString("G", CultureInfo.InvariantCulture);
			}
			else if (value is decimal)
			{
				text = ((decimal)value).ToString(CultureInfo.InvariantCulture);
			}
			else if (value is DateTime)
			{
				text = ((DateTime)value).ToString("o", CultureInfo.InvariantCulture);
			}
			else
			{
				text = string.Format(CultureInfo.InvariantCulture, "{0}", value);
			}
			if (text.IndexOfAny(CommaSeparatedValuesStreamWriter.CsvEscapeCharacters) >= 0)
			{
				text = text.Replace("\"", "\"\"");
				text = string.Format(CultureInfo.InvariantCulture, "\"{0}\"", text);
			}
			return text;
		}

		// Token: 0x04000011 RID: 17
		private const char CsvDelimiter = ',';

		// Token: 0x04000012 RID: 18
		private static readonly char[] CsvEscapeCharacters = new char[] { ',', '\'', '"', '\n' };

		// Token: 0x04000013 RID: 19
		private bool m_disposed;
	}
}
