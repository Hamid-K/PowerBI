using System;
using System.Net.Http;
using System.Threading;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Hosting;

namespace System.Web.Http.Owin
{
	// Token: 0x0200000D RID: 13
	public class HttpMessageHandlerOptions
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00002BA1 File Offset: 0x00000DA1
		// (set) Token: 0x06000070 RID: 112 RVA: 0x00002BA9 File Offset: 0x00000DA9
		public HttpMessageHandler MessageHandler { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00002BB2 File Offset: 0x00000DB2
		// (set) Token: 0x06000072 RID: 114 RVA: 0x00002BBA File Offset: 0x00000DBA
		public IHostBufferPolicySelector BufferPolicySelector { get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00002BC3 File Offset: 0x00000DC3
		// (set) Token: 0x06000074 RID: 116 RVA: 0x00002BCB File Offset: 0x00000DCB
		public IExceptionLogger ExceptionLogger { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00002BD4 File Offset: 0x00000DD4
		// (set) Token: 0x06000076 RID: 118 RVA: 0x00002BDC File Offset: 0x00000DDC
		public IExceptionHandler ExceptionHandler { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00002BE5 File Offset: 0x00000DE5
		// (set) Token: 0x06000078 RID: 120 RVA: 0x00002BED File Offset: 0x00000DED
		public CancellationToken AppDisposing { get; set; }
	}
}
