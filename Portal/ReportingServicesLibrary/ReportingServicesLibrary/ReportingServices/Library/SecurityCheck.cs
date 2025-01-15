using System;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200012C RID: 300
	internal abstract class SecurityCheck
	{
		// Token: 0x06000C1B RID: 3099
		public abstract bool Check(Security security, ItemType itemType, byte[] securityDescriptor, ExternalItemPath itemPath);
	}
}
