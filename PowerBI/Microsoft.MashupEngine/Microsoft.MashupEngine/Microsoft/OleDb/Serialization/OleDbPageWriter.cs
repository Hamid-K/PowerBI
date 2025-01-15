using System;
using System.IO;
using Microsoft.Data.Serialization;

namespace Microsoft.OleDb.Serialization
{
	// Token: 0x02001FBD RID: 8125
	public class OleDbPageWriter : IDisposable
	{
		// Token: 0x0600C63E RID: 50750 RVA: 0x00278509 File Offset: 0x00276709
		public OleDbPageWriter(Stream stream, TableSchema schema, int maxPageRowCount)
			: this(new PageWriter(stream), schema, maxPageRowCount)
		{
		}

		// Token: 0x0600C63F RID: 50751 RVA: 0x00278519 File Offset: 0x00276719
		public OleDbPageWriter(PageWriter writer, TableSchema schema, int maxPageRowCount)
		{
			this.writer = writer;
			this.schema = schema;
			this.writer.WriteSchema(schema);
			this.writer.WriteInt32(maxPageRowCount);
		}

		// Token: 0x17003018 RID: 12312
		// (get) Token: 0x0600C640 RID: 50752 RVA: 0x00278547 File Offset: 0x00276747
		public TableSchema Schema
		{
			get
			{
				return this.schema;
			}
		}

		// Token: 0x0600C641 RID: 50753 RVA: 0x0027854F File Offset: 0x0027674F
		public void Write(IPage page)
		{
			this.writer.WritePage(page);
		}

		// Token: 0x0600C642 RID: 50754 RVA: 0x0027855D File Offset: 0x0027675D
		public void Flush()
		{
			this.writer.Flush();
		}

		// Token: 0x0600C643 RID: 50755 RVA: 0x0027856A File Offset: 0x0027676A
		public void Dispose()
		{
			this.writer.Dispose();
		}

		// Token: 0x04006552 RID: 25938
		private readonly PageWriter writer;

		// Token: 0x04006553 RID: 25939
		private readonly TableSchema schema;
	}
}
