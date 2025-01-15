using System;
using Microsoft.ReportingServices.Common;

namespace Microsoft.ReportingServices.Library.ProgressiveReport
{
	// Token: 0x02000350 RID: 848
	internal interface IDataSourceRewriter
	{
		// Token: 0x06001C0F RID: 7183
		bool NeedsRewrite(string dataSourceName);

		// Token: 0x06001C10 RID: 7184
		string RewriteConnectionString(string dataSourceName, DataSourceDefinition2 definition);
	}
}
