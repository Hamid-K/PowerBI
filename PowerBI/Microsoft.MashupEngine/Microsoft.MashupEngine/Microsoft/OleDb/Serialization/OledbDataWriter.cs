using System;
using System.IO;
using Microsoft.Data.Serialization;

namespace Microsoft.OleDb.Serialization
{
	// Token: 0x02001FBB RID: 8123
	public class OledbDataWriter : IDisposable
	{
		// Token: 0x0600C62E RID: 50734 RVA: 0x00278354 File Offset: 0x00276554
		public OledbDataWriter(Stream stream, TableSchema schema, int? maxPageRowCount = null)
		{
			this.writer = new OleDbPageWriter(stream, schema, maxPageRowCount ?? SchemaTableHelper.MaxRowCount(schema));
			this.page = new ColumnsPage(schema);
		}

		// Token: 0x0600C62F RID: 50735 RVA: 0x0027839A File Offset: 0x0027659A
		public void Flush()
		{
			if (this.page.RowCount > 0)
			{
				this.WritePage();
			}
			this.writer.Flush();
		}

		// Token: 0x0600C630 RID: 50736 RVA: 0x002783BB File Offset: 0x002765BB
		public void WriteEnd()
		{
			this.Flush();
			this.WritePage();
			this.writer.Flush();
		}

		// Token: 0x0600C631 RID: 50737 RVA: 0x002783D4 File Offset: 0x002765D4
		public void WriteRecord(object[] row)
		{
			this.page.AddRow(row);
			if (this.page.RowCount == this.page.MaxRowCount)
			{
				this.WritePage();
			}
		}

		// Token: 0x0600C632 RID: 50738 RVA: 0x00278400 File Offset: 0x00276600
		private void WritePage()
		{
			this.writer.Write(this.page);
			this.page.Clear(null);
		}

		// Token: 0x0600C633 RID: 50739 RVA: 0x0027841F File Offset: 0x0027661F
		public void Dispose()
		{
			this.page.Dispose();
		}

		// Token: 0x0400654B RID: 25931
		private OleDbPageWriter writer;

		// Token: 0x0400654C RID: 25932
		private ColumnsPage page;
	}
}
