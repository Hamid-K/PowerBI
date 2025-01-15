using System;
using Microsoft.HostIntegration.Drda.Requester;

namespace Microsoft.HostIntegration.DrdaClient
{
	// Token: 0x020009A3 RID: 2467
	internal class DataTypeConverter
	{
		// Token: 0x06004C75 RID: 19573 RVA: 0x00008948 File Offset: 0x00006B48
		public static DrdaType ToDrdaType(DrdaClientType drdaClientType)
		{
			return (DrdaType)drdaClientType;
		}

		// Token: 0x06004C76 RID: 19574 RVA: 0x00008948 File Offset: 0x00006B48
		public static DrdaClientType ToDrdaClientType(DrdaType drdaType)
		{
			return (DrdaClientType)drdaType;
		}
	}
}
