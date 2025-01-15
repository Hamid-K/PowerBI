using System;

namespace Microsoft.HostIntegration.DrdaClient
{
	// Token: 0x020009D6 RID: 2518
	internal class TransactionStateEventArgs : EventArgs
	{
		// Token: 0x170012B6 RID: 4790
		// (get) Token: 0x06004DB4 RID: 19892 RVA: 0x00138826 File Offset: 0x00136A26
		// (set) Token: 0x06004DB5 RID: 19893 RVA: 0x0013882E File Offset: 0x00136A2E
		public TransactionState State { get; set; }
	}
}
