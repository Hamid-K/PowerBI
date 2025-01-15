using System;
using System.Security.Permissions;

namespace Microsoft.ReportingServices.ReportProcessing.ReportObjectModel
{
	// Token: 0x02000013 RID: 19
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class Globals : MarshalByRefObject
	{
		// Token: 0x1700002D RID: 45
		public abstract object this[string key] { get; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000046 RID: 70
		public abstract string ReportName { get; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000047 RID: 71
		public abstract int PageNumber { get; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000048 RID: 72
		public abstract int TotalPages { get; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000049 RID: 73
		public abstract int OverallPageNumber { get; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600004A RID: 74
		public abstract int OverallTotalPages { get; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600004B RID: 75
		public abstract DateTime ExecutionTime { get; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600004C RID: 76
		public abstract string ReportServerUrl { get; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600004D RID: 77
		public abstract string ReportFolder { get; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600004E RID: 78
		public abstract string PageName { get; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600004F RID: 79
		public abstract RenderFormat RenderFormat { get; }
	}
}
