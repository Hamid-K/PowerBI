using System;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Eventing.Base
{
	// Token: 0x020003C5 RID: 965
	public class EventingOnBehalfOfTrace : TraceSourceBase<EventingOnBehalfOfTrace>
	{
		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06001DE8 RID: 7656 RVA: 0x00071504 File Offset: 0x0006F704
		public override TraceSourceIdentifier ID
		{
			get
			{
				return new TraceSourceIdentifier("P.EventingOnBehalfOf");
			}
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06001DE9 RID: 7657 RVA: 0x000034D8 File Offset: 0x000016D8
		public override TraceVerbosity DefaultVerbosity
		{
			get
			{
				return TraceVerbosity.Info;
			}
		}
	}
}
