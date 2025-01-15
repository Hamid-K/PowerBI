using System;
using Microsoft.SqlServer.ReportingServices2010;

namespace Microsoft.ReportingServices.Portal.Interfaces.SoapProxy
{
	// Token: 0x0200008A RID: 138
	public class SubscriptionProperties
	{
		// Token: 0x040002CF RID: 719
		public ExtensionSettings ExtensionSettings;

		// Token: 0x040002D0 RID: 720
		public string Id;

		// Token: 0x040002D1 RID: 721
		public string Owner;

		// Token: 0x040002D2 RID: 722
		public string Description;

		// Token: 0x040002D3 RID: 723
		public ActiveState Active;

		// Token: 0x040002D4 RID: 724
		public string Status;

		// Token: 0x040002D5 RID: 725
		public string EventType;

		// Token: 0x040002D6 RID: 726
		public string MatchData;

		// Token: 0x040002D7 RID: 727
		public ParameterValue[] Parameters;
	}
}
