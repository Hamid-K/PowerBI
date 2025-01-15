using System;
using System.IO;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000244 RID: 580
	internal sealed class MemoryThenFileStreamFactory : StreamFactoryBase
	{
		// Token: 0x06001521 RID: 5409 RVA: 0x00054104 File Offset: 0x00052304
		protected override Stream InternalCreateStream(bool primaryOutputStream, bool willSeek, bool needCacheableStream)
		{
			MemoryThenFileStream memoryThenFileStream = new MemoryThenFileStream();
			base.RegisterStreamForClosing(memoryThenFileStream);
			return memoryThenFileStream;
		}

		// Token: 0x06001522 RID: 5410 RVA: 0x0005411F File Offset: 0x0005231F
		public void RegisterExternalStreamForClosing(Stream stream)
		{
			base.RegisterStreamForClosing(stream);
		}
	}
}
