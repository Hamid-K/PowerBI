using System;
using System.Data;
using System.IO;
using System.Runtime.CompilerServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase.Serialization
{
	// Token: 0x020000DE RID: 222
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public class OleDbPageReader : IPageReader, IDisposable
	{
		// Token: 0x0600041E RID: 1054 RVA: 0x0000C7F6 File Offset: 0x0000A9F6
		public OleDbPageReader(Stream stream, bool readColumnOrdinals)
		{
			this.reader = new PageReader(stream);
			this.schemaTable = this.reader.ReadSchema(readColumnOrdinals);
			this.progress = new ReaderWriterProgress();
			this.cancelIssued = false;
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x0600041F RID: 1055 RVA: 0x0000C830 File Offset: 0x0000AA30
		public DataTable SchemaTable
		{
			get
			{
				return this.schemaTable;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000420 RID: 1056 RVA: 0x0000C838 File Offset: 0x0000AA38
		// (set) Token: 0x06000421 RID: 1057 RVA: 0x0000C842 File Offset: 0x0000AA42
		public bool CancelIssued
		{
			get
			{
				return this.cancelIssued;
			}
			set
			{
				this.cancelIssued = value;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000422 RID: 1058 RVA: 0x0000C84D File Offset: 0x0000AA4D
		public IProgress Progress
		{
			get
			{
				return this.progress;
			}
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x0000C855 File Offset: 0x0000AA55
		public IPage CreatePage()
		{
			return new ColumnsPage(this.schemaTable);
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0000C862 File Offset: 0x0000AA62
		public void Read(IPage page)
		{
			this.Read((ColumnsPage)page);
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0000C870 File Offset: 0x0000AA70
		private void Read(ColumnsPage page)
		{
			if (!this.eof)
			{
				page.Deserialize(this.reader);
				this.progress.OnRows(page.RowCount, page.ExceptionRows.Count);
				this.eof = page.RowCount == 0;
			}
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x0000C8BC File Offset: 0x0000AABC
		public void Dispose()
		{
			this.reader.Dispose();
		}

		// Token: 0x040003D3 RID: 979
		private readonly PageReader reader;

		// Token: 0x040003D4 RID: 980
		private readonly DataTable schemaTable;

		// Token: 0x040003D5 RID: 981
		private readonly ReaderWriterProgress progress;

		// Token: 0x040003D6 RID: 982
		private bool eof;

		// Token: 0x040003D7 RID: 983
		private volatile bool cancelIssued;
	}
}
