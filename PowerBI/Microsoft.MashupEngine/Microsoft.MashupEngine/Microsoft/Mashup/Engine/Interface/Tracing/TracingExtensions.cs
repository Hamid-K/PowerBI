using System;

namespace Microsoft.Mashup.Engine.Interface.Tracing
{
	// Token: 0x02000135 RID: 309
	public static class TracingExtensions
	{
		// Token: 0x06000559 RID: 1369 RVA: 0x000080F0 File Offset: 0x000062F0
		public static IDataReaderSource TraceTo(this IDataReaderSource dataReaderSource, IHostTrace trace)
		{
			return new TracingDataReaderSource(dataReaderSource, trace);
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x000080F9 File Offset: 0x000062F9
		public static IStreamSource TraceTo(this IStreamSource streamSource, IHostTrace trace)
		{
			return new TracingStreamSource(streamSource, trace);
		}
	}
}
