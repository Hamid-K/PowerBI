using System;
using Microsoft.SqlServer.ReportingServices2010;

namespace Microsoft.ReportingServices.Portal.Interfaces.SoapProxy
{
	// Token: 0x02000085 RID: 133
	public class DataDrivenSubscriptionProperties
	{
		// Token: 0x040002C0 RID: 704
		public ExtensionSettings ExtensionSettings;

		// Token: 0x040002C1 RID: 705
		public string Id;

		// Token: 0x040002C2 RID: 706
		public string Owner;

		// Token: 0x040002C3 RID: 707
		public string Description;

		// Token: 0x040002C4 RID: 708
		public ActiveState Active;

		// Token: 0x040002C5 RID: 709
		public DataRetrievalPlan DataSettings;

		// Token: 0x040002C6 RID: 710
		public string Status;

		// Token: 0x040002C7 RID: 711
		public string EventType;

		// Token: 0x040002C8 RID: 712
		public string MatchData;

		// Token: 0x040002C9 RID: 713
		public ParameterValueOrFieldReference[] Parameters;
	}
}
