using System;
using System.IO;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000243 RID: 579
	internal sealed class MemoryStreamFactory : StreamFactoryBase
	{
		// Token: 0x0600151F RID: 5407 RVA: 0x000540F0 File Offset: 0x000522F0
		protected override Stream InternalCreateStream(bool primaryOutputStream, bool willSeek, bool needCacheableStream)
		{
			return new MemoryStream(Global.BufferInitialSize);
		}
	}
}
