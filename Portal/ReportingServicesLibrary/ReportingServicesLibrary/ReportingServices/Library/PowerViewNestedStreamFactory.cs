using System;
using System.IO;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.ProgressivePackaging;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200024D RID: 589
	internal sealed class PowerViewNestedStreamFactory : StreamFactoryBase
	{
		// Token: 0x06001594 RID: 5524 RVA: 0x000553EE File Offset: 0x000535EE
		internal PowerViewNestedStreamFactory(IMessageWriter writer)
		{
			this.m_writer = writer;
		}

		// Token: 0x06001595 RID: 5525 RVA: 0x00055400 File Offset: 0x00053600
		protected override Stream InternalCreateStream(bool primaryOutputStream, bool willSeek, bool needCacheableStream)
		{
			Stream stream;
			if (primaryOutputStream)
			{
				RSTrace.BufferedResponseTracer.Assert(!willSeek, "RenderEditResponseStreamFactory.InternalCreateStream: !willSeek");
				RSTrace.BufferedResponseTracer.Assert(!needCacheableStream, "RenderEditResponseStreamFactory.InternalCreateStream: !needCacheableStream");
				stream = this.m_writer.CreateWritableStream("rpds");
			}
			else
			{
				stream = new MemoryThenFileStream();
				base.RegisterStreamForClosing(stream);
			}
			base.RegisterStreamForClosing(stream);
			return stream;
		}

		// Token: 0x040007D7 RID: 2007
		private IMessageWriter m_writer;
	}
}
