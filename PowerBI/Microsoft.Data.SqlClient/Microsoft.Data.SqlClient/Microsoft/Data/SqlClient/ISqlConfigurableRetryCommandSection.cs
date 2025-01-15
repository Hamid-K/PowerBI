using System;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x02000040 RID: 64
	internal interface ISqlConfigurableRetryCommandSection : ISqlConfigurableRetryConnectionSection
	{
		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x06000770 RID: 1904
		// (set) Token: 0x06000771 RID: 1905
		string AuthorizedSqlCondition { get; set; }
	}
}
