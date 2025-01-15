using System;
using System.Globalization;
using Microsoft.Data.Serialization;

namespace Microsoft.Mashup.Engine.Interface.Tracing
{
	// Token: 0x02000137 RID: 311
	public class TracingPageReader : IPageReader, IDisposable
	{
		// Token: 0x0600055E RID: 1374 RVA: 0x00008111 File Offset: 0x00006311
		public TracingPageReader(IPageReader pageReader, IHostTrace trace, int resultIndex = 1)
		{
			this.pageReader = pageReader;
			this.trace = trace;
			this.resultIndex = resultIndex;
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x0600055F RID: 1375 RVA: 0x00008130 File Offset: 0x00006330
		public TableSchema Schema
		{
			get
			{
				TableSchema schema;
				using (this.trace.NewTimedScope())
				{
					schema = this.pageReader.Schema;
				}
				return schema;
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000560 RID: 1376 RVA: 0x00008178 File Offset: 0x00006378
		public IProgress Progress
		{
			get
			{
				return this.pageReader.Progress;
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000561 RID: 1377 RVA: 0x00008185 File Offset: 0x00006385
		public int MaxPageRowCount
		{
			get
			{
				return this.pageReader.MaxPageRowCount;
			}
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x00008194 File Offset: 0x00006394
		public IPage CreatePage()
		{
			IPage page;
			using (this.trace.NewTimedScope())
			{
				page = this.pageReader.CreatePage();
			}
			return page;
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x000081DC File Offset: 0x000063DC
		public void Read(IPage page)
		{
			using (this.trace.NewTimedScope())
			{
				this.pageReader.Read(page);
				this.rowCount += (long)page.RowCount;
			}
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x00008238 File Offset: 0x00006438
		public IPageReader NextResult()
		{
			IPageReader pageReader2;
			using (this.trace.NewTimedScope())
			{
				IPageReader pageReader = this.pageReader.NextResult();
				if (pageReader != null)
				{
					pageReader = new TracingPageReader(pageReader, this.trace, this.resultIndex + 1);
				}
				pageReader2 = pageReader;
			}
			return pageReader2;
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x00008298 File Offset: 0x00006498
		public void Dispose()
		{
			if (this.rowCount != -1L)
			{
				using (this.trace.NewTimedScope())
				{
					this.pageReader.Dispose();
				}
				string text = "PageRowCount";
				if (this.resultIndex >= 1)
				{
					text += this.resultIndex.ToString(CultureInfo.InvariantCulture);
				}
				this.trace.Add(text, this.rowCount, false);
				this.rowCount = -1L;
			}
		}

		// Token: 0x04000342 RID: 834
		private const int disposedSentinel = -1;

		// Token: 0x04000343 RID: 835
		private readonly IPageReader pageReader;

		// Token: 0x04000344 RID: 836
		private readonly IHostTrace trace;

		// Token: 0x04000345 RID: 837
		private int resultIndex;

		// Token: 0x04000346 RID: 838
		private long rowCount;
	}
}
