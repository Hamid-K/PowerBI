using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200012D RID: 301
	internal sealed class ReportOptionSecurityCheck : SecurityCheck
	{
		// Token: 0x06000C1D RID: 3101 RVA: 0x0002D7E8 File Offset: 0x0002B9E8
		private ReportOptionSecurityCheck(ReportOperation operation)
		{
			this.m_operation = operation;
		}

		// Token: 0x06000C1E RID: 3102 RVA: 0x0002D7F7 File Offset: 0x0002B9F7
		public override bool Check(Security security, ItemType itemType, byte[] securityDescriptor, ExternalItemPath itemPath)
		{
			return security.CheckAccess(itemType, securityDescriptor, this.m_operation, itemPath);
		}

		// Token: 0x040004EC RID: 1260
		private ReportOperation m_operation;

		// Token: 0x040004ED RID: 1261
		public static ReportOptionSecurityCheck ReadProperties = new ReportOptionSecurityCheck(ReportOperation.ReadProperties);

		// Token: 0x040004EE RID: 1262
		public static ReportOptionSecurityCheck ExecuteAndView = new ReportOptionSecurityCheck(ReportOperation.ExecuteAndView);
	}
}
