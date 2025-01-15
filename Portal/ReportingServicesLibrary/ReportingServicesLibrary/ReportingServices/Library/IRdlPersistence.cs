using System;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020002D8 RID: 728
	internal interface IRdlPersistence
	{
		// Token: 0x06001A0D RID: 6669
		void InitializeRdlMapping();

		// Token: 0x06001A0E RID: 6670
		void ResetRdlMapping();

		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x06001A0F RID: 6671
		bool StoreRdlChunks { get; }

		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x06001A10 RID: 6672
		RdlChunkMapper RdlChunkMapper { get; }
	}
}
