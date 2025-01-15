using System;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.MonitoredUtils
{
	// Token: 0x02000149 RID: 329
	public class MonitoredUtilsTrace : TraceSourceBase<MonitoredUtilsTrace>
	{
		// Token: 0x17000159 RID: 345
		// (get) Token: 0x0600089A RID: 2202 RVA: 0x0001E052 File Offset: 0x0001C252
		public override TraceSourceIdentifier ID
		{
			get
			{
				return new TraceSourceIdentifier("P.MonitoredUtils");
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x0600089B RID: 2203 RVA: 0x000034D8 File Offset: 0x000016D8
		public override TraceVerbosity DefaultVerbosity
		{
			get
			{
				return TraceVerbosity.Info;
			}
		}
	}
}
