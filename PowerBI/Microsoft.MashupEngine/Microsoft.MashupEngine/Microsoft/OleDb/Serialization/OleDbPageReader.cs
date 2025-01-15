using System;
using System.IO;
using Microsoft.Data.Serialization;

namespace Microsoft.OleDb.Serialization
{
	// Token: 0x02001FBC RID: 8124
	public class OleDbPageReader : IPageReader, IDisposable
	{
		// Token: 0x0600C634 RID: 50740 RVA: 0x0027842C File Offset: 0x0027662C
		public OleDbPageReader(Stream stream)
			: this(new PageReader(stream))
		{
		}

		// Token: 0x0600C635 RID: 50741 RVA: 0x0027843A File Offset: 0x0027663A
		public OleDbPageReader(PageReader reader)
		{
			this.reader = reader;
			this.schema = this.reader.ReadSchema();
			this.maxPageRowCount = this.reader.ReadInt32();
			this.progress = new ReaderWriterProgress();
		}

		// Token: 0x17003015 RID: 12309
		// (get) Token: 0x0600C636 RID: 50742 RVA: 0x00278476 File Offset: 0x00276676
		public TableSchema Schema
		{
			get
			{
				return this.schema;
			}
		}

		// Token: 0x17003016 RID: 12310
		// (get) Token: 0x0600C637 RID: 50743 RVA: 0x0027847E File Offset: 0x0027667E
		public IProgress Progress
		{
			get
			{
				return this.progress;
			}
		}

		// Token: 0x17003017 RID: 12311
		// (get) Token: 0x0600C638 RID: 50744 RVA: 0x00278486 File Offset: 0x00276686
		public int MaxPageRowCount
		{
			get
			{
				return this.maxPageRowCount;
			}
		}

		// Token: 0x0600C639 RID: 50745 RVA: 0x0027848E File Offset: 0x0027668E
		public IPage CreatePage()
		{
			return new ColumnsPage(this.schema, this.maxPageRowCount);
		}

		// Token: 0x0600C63A RID: 50746 RVA: 0x002784A1 File Offset: 0x002766A1
		public void Read(IPage page)
		{
			this.Read((ColumnsPage)page);
		}

		// Token: 0x0600C63B RID: 50747 RVA: 0x002784B0 File Offset: 0x002766B0
		private void Read(ColumnsPage page)
		{
			if (!this.eof)
			{
				page.Deserialize(this.reader);
				this.progress.OnRows(page.RowCount, page.ExceptionRows.Count);
				this.eof = page.RowCount == 0;
			}
		}

		// Token: 0x0600C63C RID: 50748 RVA: 0x000020FA File Offset: 0x000002FA
		public IPageReader NextResult()
		{
			return null;
		}

		// Token: 0x0600C63D RID: 50749 RVA: 0x002784FC File Offset: 0x002766FC
		public void Dispose()
		{
			this.reader.Dispose();
		}

		// Token: 0x0400654D RID: 25933
		private readonly PageReader reader;

		// Token: 0x0400654E RID: 25934
		private readonly TableSchema schema;

		// Token: 0x0400654F RID: 25935
		private readonly int maxPageRowCount;

		// Token: 0x04006550 RID: 25936
		private readonly ReaderWriterProgress progress;

		// Token: 0x04006551 RID: 25937
		private bool eof;
	}
}
