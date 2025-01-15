using System;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Tracing
{
	// Token: 0x02000015 RID: 21
	public class TracingTrace : TraceSourceBase<TracingTrace>
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600006C RID: 108 RVA: 0x000034CC File Offset: 0x000016CC
		public override TraceSourceIdentifier ID
		{
			get
			{
				return new TraceSourceIdentifier("P.Tracing");
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600006D RID: 109 RVA: 0x000034D8 File Offset: 0x000016D8
		public override TraceVerbosity DefaultVerbosity
		{
			get
			{
				return TraceVerbosity.Info;
			}
		}
	}
}
