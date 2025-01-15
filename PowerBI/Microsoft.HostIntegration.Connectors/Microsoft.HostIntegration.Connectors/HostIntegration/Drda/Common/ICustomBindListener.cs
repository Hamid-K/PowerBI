using System;
using System.Collections.Generic;
using Microsoft.HostIntegration.StaticSqlUtil;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x0200081D RID: 2077
	public interface ICustomBindListener
	{
		// Token: 0x060041C0 RID: 16832
		void OnPackageBound(StaticSql staticSql, string rdbNam, string collid, out List<string> sqlScripts);
	}
}
