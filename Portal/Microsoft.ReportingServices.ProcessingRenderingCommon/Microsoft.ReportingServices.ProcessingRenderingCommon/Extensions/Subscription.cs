using System;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.Extensions
{
	// Token: 0x02000012 RID: 18
	internal abstract class Subscription
	{
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600008F RID: 143
		public abstract Guid ID { get; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000090 RID: 144
		public abstract UserContext Owner { get; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000091 RID: 145
		public abstract Guid ItemID { get; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000092 RID: 146
		public abstract string SubscriptionData { get; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000093 RID: 147
		public abstract string EventType { get; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000094 RID: 148
		public abstract string ReportName { get; }
	}
}
