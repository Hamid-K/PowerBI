using System;
using System.Data;
using System.Data.Common;
using System.IO;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Data;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000013 RID: 19
	[Serializable]
	public class CsvFileDataReader : SimpleDataReader, IDisposable
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000071 RID: 113 RVA: 0x0000381C File Offset: 0x00001A1C
		// (set) Token: 0x06000072 RID: 114 RVA: 0x00003824 File Offset: 0x00001A24
		public new DataTable SchemaTable { get; protected set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000073 RID: 115 RVA: 0x0000382D File Offset: 0x00001A2D
		// (set) Token: 0x06000074 RID: 116 RVA: 0x00003835 File Offset: 0x00001A35
		public char Delimiter { get; protected set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000075 RID: 117 RVA: 0x0000383E File Offset: 0x00001A3E
		// (set) Token: 0x06000076 RID: 118 RVA: 0x00003846 File Offset: 0x00001A46
		public bool FirstRowContainsHeader { get; protected set; }

		// Token: 0x06000077 RID: 119 RVA: 0x0000384F File Offset: 0x00001A4F
		public CsvFileDataReader(string filename, char delimiter, bool firstRowContainsHeader = false)
			: base(null)
		{
			base.SchemaTable = CsvFileDataReader.LoadSchemaTable(filename, delimiter);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003868 File Offset: 0x00001A68
		public CsvFileDataReader(string filename, DataTable schemaTable, char delimiter, bool firstRowContainsHeader = false)
			: base(schemaTable)
		{
			this.SchemaTable = schemaTable;
			this.FirstRowContainsHeader = firstRowContainsHeader;
			this.Delimiter = delimiter;
			this.m_fileStream = File.OpenRead(filename);
			this.m_streamReader = new StreamReader(this.m_fileStream);
			this.m_isOpen = true;
			this.m_record = new SimpleDataRecord(schemaTable, new object[schemaTable.Rows.Count]);
			if (firstRowContainsHeader)
			{
				try
				{
					this.m_streamReader.ReadLine();
				}
				catch (EndOfStreamException)
				{
				}
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000079 RID: 121 RVA: 0x000038F8 File Offset: 0x00001AF8
		protected override IDataRecord Current
		{
			get
			{
				return this.m_record;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00003900 File Offset: 0x00001B00
		public override bool IsClosed
		{
			get
			{
				return !this.m_isOpen;
			}
		}

		// Token: 0x0600007B RID: 123 RVA: 0x0000390B File Offset: 0x00001B0B
		public override void Close()
		{
			if (this.m_isOpen)
			{
				this.m_fileStream.Close();
				this.m_isOpen = false;
			}
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003928 File Offset: 0x00001B28
		public static DataTable LoadSchemaTable(string filename, char delimiter)
		{
			DataTable dataTable2;
			using (FileStream fileStream = File.Open(filename, 3, 1, 1))
			{
				using (StreamReader streamReader = new StreamReader(fileStream))
				{
					DataTable dataTable = CsvFileDataReader.LoadSchemaTable(streamReader, delimiter);
					dataTable.TableName = filename;
					dataTable2 = dataTable;
				}
			}
			return dataTable2;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003988 File Offset: 0x00001B88
		public static DataTable LoadSchemaTable(TextReader s, char delimiter)
		{
			DataTable dataTable = SchemaUtils.CreateSchemaTable("Schema");
			try
			{
				int num = 0;
				foreach (string text in s.ReadLine().Split(new char[] { delimiter }))
				{
					DataRow dataRow = dataTable.NewRow();
					dataRow[SchemaTableColumn.ColumnName] = text;
					dataRow[SchemaTableColumn.ColumnOrdinal] = num++;
					dataRow[SchemaTableColumn.DataType] = typeof(string);
					dataTable.Rows.Add(dataRow);
				}
			}
			catch (EndOfStreamException)
			{
			}
			return dataTable;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003A30 File Offset: 0x00001C30
		public override DataTable GetSchemaTable()
		{
			return this.SchemaTable;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003A38 File Offset: 0x00001C38
		public override bool NextResult()
		{
			return false;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003A3C File Offset: 0x00001C3C
		public override bool Read()
		{
			string text = this.m_streamReader.ReadLine();
			if (text != null)
			{
				string[] array = text.Split(new char[] { this.Delimiter });
				if (array.Length != this.m_record.Values.Length)
				{
					throw new Exception(string.Format("Invalid row encountered. Expected {0} columns, but found {1}", this.m_record.Values.Length, array.Length));
				}
				for (int i = 0; i < array.Length; i++)
				{
					this.m_record.Values[i] = array[i];
				}
			}
			return text != null;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003ACC File Offset: 0x00001CCC
		private void Dispose()
		{
			this.Close();
		}

		// Token: 0x04000026 RID: 38
		private StreamReader m_streamReader;

		// Token: 0x04000027 RID: 39
		private FileStream m_fileStream;

		// Token: 0x04000028 RID: 40
		private SimpleDataRecord m_record;

		// Token: 0x04000029 RID: 41
		private bool m_isOpen;
	}
}
