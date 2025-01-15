using System;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Monitoring
{
	// Token: 0x02000091 RID: 145
	public class MonitoringTrace : TraceSourceBase<MonitoringTrace>
	{
		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000414 RID: 1044 RVA: 0x0000EDCF File Offset: 0x0000CFCF
		public override TraceSourceIdentifier ID
		{
			get
			{
				return new TraceSourceIdentifier("P.Monitoring");
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000415 RID: 1045 RVA: 0x000034D8 File Offset: 0x000016D8
		public override TraceVerbosity DefaultVerbosity
		{
			get
			{
				return TraceVerbosity.Info;
			}
		}
	}
}
