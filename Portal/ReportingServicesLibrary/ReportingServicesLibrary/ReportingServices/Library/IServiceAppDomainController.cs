using System;
using System.Security.Permissions;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200022C RID: 556
	internal interface IServiceAppDomainController
	{
		// Token: 0x060013F5 RID: 5109
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		bool StartRPCServer(bool firstTime);

		// Token: 0x060013F6 RID: 5110
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		void MarkProcessAsActive();
	}
}
