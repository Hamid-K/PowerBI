using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200015E RID: 350
	internal interface IServerSocketProtocolFactory
	{
		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000AC7 RID: 2759
		int BytesNeededForAnalysis { get; }

		// Token: 0x06000AC8 RID: 2760
		IServerSocketProtocol GetProtocol(byte[] magicBytes);
	}
}
