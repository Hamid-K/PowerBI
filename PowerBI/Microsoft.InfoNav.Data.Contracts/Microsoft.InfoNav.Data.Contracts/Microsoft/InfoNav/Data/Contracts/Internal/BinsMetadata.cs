using System;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x0200014C RID: 332
	public sealed class BinsMetadata
	{
		// Token: 0x0600087E RID: 2174 RVA: 0x00011BA6 File Offset: 0x0000FDA6
		internal BinsMetadata(ClientConceptualBinningMetadata binningDefinition)
		{
			this.BinningDefinition = binningDefinition;
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x0600087F RID: 2175 RVA: 0x00011BB5 File Offset: 0x0000FDB5
		// (set) Token: 0x06000880 RID: 2176 RVA: 0x00011BBD File Offset: 0x0000FDBD
		public ClientConceptualBinningMetadata BinningDefinition { get; private set; }
	}
}
