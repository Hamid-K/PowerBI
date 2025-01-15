using System;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Eventing.Base
{
	// Token: 0x020003C4 RID: 964
	public class EventingTrace : TraceSourceBase<EventingTrace>
	{
		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06001DE5 RID: 7653 RVA: 0x000714F0 File Offset: 0x0006F6F0
		public override TraceSourceIdentifier ID
		{
			get
			{
				return new TraceSourceIdentifier("P.Eventing");
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06001DE6 RID: 7654 RVA: 0x000034D8 File Offset: 0x000016D8
		public override TraceVerbosity DefaultVerbosity
		{
			get
			{
				return TraceVerbosity.Info;
			}
		}
	}
}
