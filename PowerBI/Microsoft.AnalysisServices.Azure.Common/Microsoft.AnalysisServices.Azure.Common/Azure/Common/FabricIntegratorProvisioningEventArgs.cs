using System;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x0200007C RID: 124
	public class FabricIntegratorProvisioningEventArgs : EventArgs
	{
		// Token: 0x060004DB RID: 1243 RVA: 0x00010A36 File Offset: 0x0000EC36
		public FabricIntegratorProvisioningEventArgs(FabricIntegratorProvisioningEvent fabricIntegratorProvisioningEvent, DatabaseEntity databaseEntity)
		{
			this.FabricIntegratorProvisioningEvent = fabricIntegratorProvisioningEvent;
			this.DatabaseEntity = databaseEntity;
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060004DC RID: 1244 RVA: 0x00010A4C File Offset: 0x0000EC4C
		// (set) Token: 0x060004DD RID: 1245 RVA: 0x00010A54 File Offset: 0x0000EC54
		public FabricIntegratorProvisioningEvent FabricIntegratorProvisioningEvent { get; private set; }

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060004DE RID: 1246 RVA: 0x00010A5D File Offset: 0x0000EC5D
		// (set) Token: 0x060004DF RID: 1247 RVA: 0x00010A65 File Offset: 0x0000EC65
		public DatabaseEntity DatabaseEntity { get; private set; }
	}
}
