using System;
using System.Security.Permissions;

namespace Microsoft.ReportingServices.ReportProcessing.ReportObjectModel
{
	// Token: 0x02000012 RID: 18
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class Parameter : MarshalByRefObject
	{
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000040 RID: 64
		public abstract object Value { get; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000041 RID: 65
		public abstract object Label { get; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000042 RID: 66
		public abstract int Count { get; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000043 RID: 67
		public abstract bool IsMultiValue { get; }
	}
}
