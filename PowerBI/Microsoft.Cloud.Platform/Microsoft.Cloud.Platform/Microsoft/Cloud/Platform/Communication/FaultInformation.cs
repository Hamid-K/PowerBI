using System;
using System.ServiceModel;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x020004CF RID: 1231
	public class FaultInformation
	{
		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x0600257D RID: 9597 RVA: 0x000856C7 File Offset: 0x000838C7
		// (set) Token: 0x0600257E RID: 9598 RVA: 0x000856CF File Offset: 0x000838CF
		public FaultReason FaultString { get; private set; }

		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x0600257F RID: 9599 RVA: 0x000856D8 File Offset: 0x000838D8
		// (set) Token: 0x06002580 RID: 9600 RVA: 0x000856E0 File Offset: 0x000838E0
		public FaultCode FaultErrorCode { get; private set; }

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x06002581 RID: 9601 RVA: 0x000856E9 File Offset: 0x000838E9
		// (set) Token: 0x06002582 RID: 9602 RVA: 0x000856F1 File Offset: 0x000838F1
		public object FaultDetail { get; private set; }

		// Token: 0x06002583 RID: 9603 RVA: 0x000856FA File Offset: 0x000838FA
		public FaultInformation(FaultCode faultCode, FaultReason faultString, object faultDeatil)
		{
			this.FaultString = faultString;
			this.FaultErrorCode = faultCode;
			this.FaultDetail = faultDeatil;
		}
	}
}
