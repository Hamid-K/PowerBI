using System;
using System.IO;

namespace Microsoft.ReportingServices.ReportPublishing
{
	// Token: 0x02000383 RID: 899
	internal sealed class NoOpUpgradeStrategy : ReportUpgradeStrategy
	{
		// Token: 0x0600225C RID: 8796 RVA: 0x00084082 File Offset: 0x00082282
		internal override Stream Upgrade(Stream definitionStream)
		{
			return definitionStream;
		}
	}
}
