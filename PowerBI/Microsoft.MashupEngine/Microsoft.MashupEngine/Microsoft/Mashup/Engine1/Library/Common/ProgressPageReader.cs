using System;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x02001108 RID: 4360
	internal class ProgressPageReader : IPageReader, IDisposable
	{
		// Token: 0x06007203 RID: 29187 RVA: 0x00187E4A File Offset: 0x0018604A
		public ProgressPageReader(IPageReader pageReader, IHostProgress hostProgress)
		{
			this.pageReader = pageReader;
			this.hostProgress = hostProgress;
		}

		// Token: 0x17001FF0 RID: 8176
		// (get) Token: 0x06007204 RID: 29188 RVA: 0x00187E60 File Offset: 0x00186060
		public TableSchema Schema
		{
			get
			{
				return this.pageReader.Schema;
			}
		}

		// Token: 0x17001FF1 RID: 8177
		// (get) Token: 0x06007205 RID: 29189 RVA: 0x00187E6D File Offset: 0x0018606D
		public IProgress Progress
		{
			get
			{
				return this.pageReader.Progress;
			}
		}

		// Token: 0x17001FF2 RID: 8178
		// (get) Token: 0x06007206 RID: 29190 RVA: 0x00187E7A File Offset: 0x0018607A
		public int MaxPageRowCount
		{
			get
			{
				return this.pageReader.MaxPageRowCount;
			}
		}

		// Token: 0x06007207 RID: 29191 RVA: 0x00187E87 File Offset: 0x00186087
		public IPage CreatePage()
		{
			return this.pageReader.CreatePage();
		}

		// Token: 0x06007208 RID: 29192 RVA: 0x00187E94 File Offset: 0x00186094
		public void Read(IPage page)
		{
			this.pageReader.Read(page);
			this.hostProgress.RecordRowsRead((long)page.RowCount);
		}

		// Token: 0x06007209 RID: 29193 RVA: 0x00187EB4 File Offset: 0x001860B4
		public IPageReader NextResult()
		{
			IPageReader pageReader = this.pageReader.NextResult();
			if (pageReader != null)
			{
				pageReader = new ProgressPageReader(pageReader, this.hostProgress);
			}
			return pageReader;
		}

		// Token: 0x0600720A RID: 29194 RVA: 0x00187EDE File Offset: 0x001860DE
		public void Dispose()
		{
			if (this.pageReader != null)
			{
				this.pageReader.Dispose();
				this.pageReader = null;
				this.hostProgress = null;
			}
		}

		// Token: 0x04003EFC RID: 16124
		private IPageReader pageReader;

		// Token: 0x04003EFD RID: 16125
		private IHostProgress hostProgress;
	}
}
