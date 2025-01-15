using System;
using System.Collections;
using System.Security.Permissions;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000627 RID: 1575
	[Serializable]
	public sealed class ProcessingMessageList : ArrayList
	{
		// Token: 0x060056D1 RID: 22225 RVA: 0x0016E808 File Offset: 0x0016CA08
		[StrongNameIdentityPermission(SecurityAction.LinkDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
		public ProcessingMessageList()
		{
		}

		// Token: 0x060056D2 RID: 22226 RVA: 0x0016E810 File Offset: 0x0016CA10
		internal ProcessingMessageList(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x17001FA4 RID: 8100
		public ProcessingMessage this[int index]
		{
			get
			{
				return (ProcessingMessage)base[index];
			}
		}
	}
}
