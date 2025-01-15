using System;
using System.Runtime.InteropServices;

namespace Microsoft.ASPaaS.AnalysisServer.Interfaces.Common.MInterop
{
	// Token: 0x02000005 RID: 5
	[ComVisible(true)]
	[Guid("2BEAA3B7-3467-48E4-A763-A4BD960F5F92")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[CLSCompliant(true)]
	public interface IJwtTokenHelperMInterop
	{
		// Token: 0x06000001 RID: 1
		bool TryGetClaimFromAccessToken(Guid rootActivityId, Guid parentActivityId, string accessToken, string claimType, out ClaimMInterop claim);
	}
}
