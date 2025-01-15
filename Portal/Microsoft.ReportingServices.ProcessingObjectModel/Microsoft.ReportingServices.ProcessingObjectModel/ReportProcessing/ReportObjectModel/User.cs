using System;
using System.Security.Permissions;

namespace Microsoft.ReportingServices.ReportProcessing.ReportObjectModel
{
	// Token: 0x02000014 RID: 20
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class User : MarshalByRefObject
	{
		// Token: 0x17000038 RID: 56
		public abstract object this[string key] { get; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000052 RID: 82
		public abstract string UserID { get; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000053 RID: 83
		public abstract string Language { get; }
	}
}
