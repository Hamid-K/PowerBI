using System;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x02000022 RID: 34
	internal class EnclavePublicKey
	{
		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x06000692 RID: 1682 RVA: 0x0000CC3B File Offset: 0x0000AE3B
		// (set) Token: 0x06000693 RID: 1683 RVA: 0x0000CC43 File Offset: 0x0000AE43
		public byte[] PublicKey { get; set; }

		// Token: 0x06000694 RID: 1684 RVA: 0x0000CC4C File Offset: 0x0000AE4C
		public EnclavePublicKey(byte[] payload)
		{
			this.PublicKey = payload;
		}
	}
}
