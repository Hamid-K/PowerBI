using System;
using System.IO;

namespace Microsoft.Mashup.Engine.Interface.Tracing
{
	// Token: 0x02000138 RID: 312
	public class TracingStreamSource : IStreamSource, IDisposable
	{
		// Token: 0x06000566 RID: 1382 RVA: 0x0000832C File Offset: 0x0000652C
		public TracingStreamSource(IStreamSource streamSource, IHostTrace trace)
		{
			this.streamSource = streamSource;
			this.trace = trace;
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000567 RID: 1383 RVA: 0x00008344 File Offset: 0x00006544
		public Stream Stream
		{
			get
			{
				Stream stream;
				using (this.trace.NewTimedScope())
				{
					stream = this.streamSource.Stream;
				}
				return stream;
			}
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x0000838C File Offset: 0x0000658C
		public void Dispose()
		{
			using (this.trace.NewTimedScope())
			{
				this.streamSource.Dispose();
			}
		}

		// Token: 0x04000347 RID: 839
		private readonly IStreamSource streamSource;

		// Token: 0x04000348 RID: 840
		private readonly IHostTrace trace;
	}
}
