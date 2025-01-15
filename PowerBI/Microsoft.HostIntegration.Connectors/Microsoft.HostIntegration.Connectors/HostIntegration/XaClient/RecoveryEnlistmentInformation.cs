using System;

namespace Microsoft.HostIntegration.XaClient
{
	// Token: 0x02000709 RID: 1801
	internal class RecoveryEnlistmentInformation
	{
		// Token: 0x17000CAF RID: 3247
		// (get) Token: 0x06003911 RID: 14609 RVA: 0x000BF2BB File Offset: 0x000BD4BB
		// (set) Token: 0x06003912 RID: 14610 RVA: 0x000BF2C3 File Offset: 0x000BD4C3
		internal int ResourceManagerId { get; private set; }

		// Token: 0x17000CB0 RID: 3248
		// (get) Token: 0x06003913 RID: 14611 RVA: 0x000BF2CC File Offset: 0x000BD4CC
		// (set) Token: 0x06003914 RID: 14612 RVA: 0x000BF2D4 File Offset: 0x000BD4D4
		internal IXaRecoveryEnlistment IXaRecoveryEnlistment { get; private set; }

		// Token: 0x06003915 RID: 14613 RVA: 0x000BF2DD File Offset: 0x000BD4DD
		internal RecoveryEnlistmentInformation(int resourceManagerId, IXaRecoveryEnlistment iXaRecoveryEnlistment)
		{
			this.ResourceManagerId = resourceManagerId;
			this.IXaRecoveryEnlistment = iXaRecoveryEnlistment;
		}
	}
}
