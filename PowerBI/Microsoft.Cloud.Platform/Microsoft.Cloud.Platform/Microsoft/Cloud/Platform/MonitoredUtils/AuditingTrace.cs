using System;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.MonitoredUtils
{
	// Token: 0x02000112 RID: 274
	public class AuditingTrace : TraceSourceBase<AuditingTrace>
	{
		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000764 RID: 1892 RVA: 0x0001A04A File Offset: 0x0001824A
		public override TraceSourceIdentifier ID
		{
			get
			{
				return new TraceSourceIdentifier("AuditingTrace");
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000765 RID: 1893 RVA: 0x000034D8 File Offset: 0x000016D8
		public override TraceVerbosity DefaultVerbosity
		{
			get
			{
				return TraceVerbosity.Info;
			}
		}
	}
}
