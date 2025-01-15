using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020001A1 RID: 417
	internal class TcpConnectOperationResult
	{
		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000DAB RID: 3499 RVA: 0x0002EAD8 File Offset: 0x0002CCD8
		internal bool OperationSucceded
		{
			get
			{
				return this.ConnectionFailureException == null;
			}
		}

		// Token: 0x0400096D RID: 2413
		internal ITcpChannel Channel;

		// Token: 0x0400096E RID: 2414
		internal Exception ConnectionFailureException;
	}
}
