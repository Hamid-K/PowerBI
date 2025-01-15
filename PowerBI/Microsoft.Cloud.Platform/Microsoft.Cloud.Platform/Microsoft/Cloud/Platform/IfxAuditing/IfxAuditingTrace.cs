using System;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.IfxAuditing
{
	// Token: 0x0200032C RID: 812
	public class IfxAuditingTrace : TraceSourceBase<IfxAuditingTrace>
	{
		// Token: 0x17000338 RID: 824
		// (get) Token: 0x060017EE RID: 6126 RVA: 0x00058326 File Offset: 0x00056526
		public override TraceSourceIdentifier ID
		{
			get
			{
				return new TraceSourceIdentifier("IfxAuditing");
			}
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x060017EF RID: 6127 RVA: 0x000034D8 File Offset: 0x000016D8
		public override TraceVerbosity DefaultVerbosity
		{
			get
			{
				return TraceVerbosity.Info;
			}
		}
	}
}
