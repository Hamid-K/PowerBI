using System;
using System.ComponentModel;

namespace System.Net.Http.Handlers
{
	// Token: 0x02000028 RID: 40
	public class HttpProgressEventArgs : ProgressChangedEventArgs
	{
		// Token: 0x06000185 RID: 389 RVA: 0x000059D1 File Offset: 0x00003BD1
		public HttpProgressEventArgs(int progressPercentage, object userToken, long bytesTransferred, long? totalBytes)
			: base(progressPercentage, userToken)
		{
			this.BytesTransferred = bytesTransferred;
			this.TotalBytes = totalBytes;
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000186 RID: 390 RVA: 0x000059EA File Offset: 0x00003BEA
		// (set) Token: 0x06000187 RID: 391 RVA: 0x000059F2 File Offset: 0x00003BF2
		public long BytesTransferred { get; private set; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000188 RID: 392 RVA: 0x000059FB File Offset: 0x00003BFB
		// (set) Token: 0x06000189 RID: 393 RVA: 0x00005A03 File Offset: 0x00003C03
		public long? TotalBytes { get; private set; }
	}
}
