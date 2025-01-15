using System;
using System.IO;

namespace Microsoft.ReportingServices.ReportPublishing
{
	// Token: 0x02000397 RID: 919
	internal abstract class ReportUpgradeStrategy
	{
		// Token: 0x0600258A RID: 9610
		internal abstract Stream Upgrade(Stream definitionStream);
	}
}
