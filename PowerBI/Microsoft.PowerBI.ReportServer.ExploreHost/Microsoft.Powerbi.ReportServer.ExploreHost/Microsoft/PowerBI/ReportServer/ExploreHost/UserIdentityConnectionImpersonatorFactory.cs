using System;
using System.Security.Principal;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav;

namespace Microsoft.PowerBI.ReportServer.ExploreHost
{
	// Token: 0x02000011 RID: 17
	internal static class UserIdentityConnectionImpersonatorFactory
	{
		// Token: 0x0600006B RID: 107 RVA: 0x00002DE4 File Offset: 0x00000FE4
		public static IConnectionUserImpersonator CreateImpersonator(IIdentity identity)
		{
			WindowsIdentity windowsIdentity = identity as WindowsIdentity;
			if (windowsIdentity != null)
			{
				return new WindowsUserIdentityConnectionImpersonator(windowsIdentity);
			}
			throw new NotSupportedException(StringUtil.FormatInvariant("{0} is not supported.", identity.GetType().Name));
		}
	}
}
