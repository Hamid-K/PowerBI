using System;
using System.Configuration;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x02000033 RID: 51
	internal sealed class SqlConfigurableRetryCommandSection : SqlConfigurableRetryConnectionSection, ISqlConfigurableRetryCommandSection, ISqlConfigurableRetryConnectionSection
	{
		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x060006F7 RID: 1783 RVA: 0x0000E6C0 File Offset: 0x0000C8C0
		// (set) Token: 0x060006F8 RID: 1784 RVA: 0x0000E6D2 File Offset: 0x0000C8D2
		[ConfigurationProperty("authorizedSqlCondition", IsRequired = false)]
		public string AuthorizedSqlCondition
		{
			get
			{
				return base["authorizedSqlCondition"] as string;
			}
			set
			{
				base["authorizedSqlCondition"] = value;
			}
		}

		// Token: 0x040000B2 RID: 178
		public new const string Name = "SqlConfigurableRetryLogicCommand";
	}
}
