using System;
using System.Data;
using System.IO;
using System.Runtime.CompilerServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase.Serialization
{
	// Token: 0x020000DD RID: 221
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public class OledbDataWriter : IDisposable
	{
		// Token: 0x06000418 RID: 1048 RVA: 0x0000C742 File Offset: 0x0000A942
		public OledbDataWriter(Stream stream, DataTable schemaTable)
		{
			this.writer = new OleDbPageWriter(stream, schemaTable, false, true);
			this.page = new ColumnsPage(schemaTable);
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0000C765 File Offset: 0x0000A965
		public void Flush()
		{
			if (this.page.RowCount > 0)
			{
				this.WritePage();
			}
			this.writer.Flush();
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x0000C786 File Offset: 0x0000A986
		public void WriteEnd()
		{
			this.Flush();
			this.WritePage();
			this.writer.Flush();
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x0000C79F File Offset: 0x0000A99F
		public void WriteRecord(object[] row)
		{
			this.page.AddRow(row);
			if (this.page.RowCount == this.page.MaxRowCount)
			{
				this.WritePage();
			}
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0000C7CB File Offset: 0x0000A9CB
		private void WritePage()
		{
			this.writer.Write(this.page);
			this.page.Clear();
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x0000C7E9 File Offset: 0x0000A9E9
		public void Dispose()
		{
			this.page.Dispose();
		}

		// Token: 0x040003D1 RID: 977
		private OleDbPageWriter writer;

		// Token: 0x040003D2 RID: 978
		private ColumnsPage page;
	}
}
