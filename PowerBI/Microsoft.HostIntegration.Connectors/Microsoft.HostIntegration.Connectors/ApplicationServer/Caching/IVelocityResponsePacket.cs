using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000161 RID: 353
	internal interface IVelocityResponsePacket : IVelocityPacket
	{
		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000AEB RID: 2795
		// (set) Token: 0x06000AEC RID: 2796
		bool IsEmptyPacket { get; set; }

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000AED RID: 2797
		// (set) Token: 0x06000AEE RID: 2798
		Exception Exception { get; set; }
	}
}
