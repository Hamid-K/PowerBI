using System;
using Microsoft.PowerBI.ReportServer.PbixLib.Parsing;

namespace Microsoft.PowerBI.ReportServer.AsServer
{
	// Token: 0x02000010 RID: 16
	public interface ICheckConnectionServices
	{
		// Token: 0x06000058 RID: 88
		PbixDataSourceCheckResult TestCredentials(PbixDataSource dataSource);
	}
}
