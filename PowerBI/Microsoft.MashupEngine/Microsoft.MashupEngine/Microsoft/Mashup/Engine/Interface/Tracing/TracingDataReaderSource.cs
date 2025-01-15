using System;
using Microsoft.Data.Serialization;

namespace Microsoft.Mashup.Engine.Interface.Tracing
{
	// Token: 0x02000134 RID: 308
	public class TracingDataReaderSource : IDataReaderSource, IDisposable
	{
		// Token: 0x06000555 RID: 1365 RVA: 0x00007FFA File Offset: 0x000061FA
		public TracingDataReaderSource(IDataReaderSource dataReaderSource, IHostTrace trace)
		{
			this.dataReaderSource = dataReaderSource;
			this.trace = trace;
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000556 RID: 1366 RVA: 0x00008010 File Offset: 0x00006210
		public ITableSource TableSource
		{
			get
			{
				ITableSource tableSource;
				using (this.trace.NewTimedScope())
				{
					tableSource = this.dataReaderSource.TableSource;
				}
				return tableSource;
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000557 RID: 1367 RVA: 0x00008058 File Offset: 0x00006258
		public IPageReader PageReader
		{
			get
			{
				IPageReader pageReader;
				using (this.trace.NewTimedScope())
				{
					pageReader = new TracingPageReader(this.dataReaderSource.PageReader, this.trace, 1);
				}
				return pageReader;
			}
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x000080AC File Offset: 0x000062AC
		public void Dispose()
		{
			using (this.trace.NewTimedScope())
			{
				this.dataReaderSource.Dispose();
			}
		}

		// Token: 0x0400033F RID: 831
		private readonly IDataReaderSource dataReaderSource;

		// Token: 0x04000340 RID: 832
		private readonly IHostTrace trace;
	}
}
