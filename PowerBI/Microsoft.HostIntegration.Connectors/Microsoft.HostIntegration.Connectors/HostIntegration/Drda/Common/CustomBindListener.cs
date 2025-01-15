using System;
using System.Collections.Generic;
using Microsoft.HostIntegration.StaticSqlUtil;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x02000855 RID: 2133
	public class CustomBindListener : ICustomBindListener
	{
		// Token: 0x06004409 RID: 17417 RVA: 0x000E5214 File Offset: 0x000E3414
		public virtual void OnPackageBound(StaticSql staticSql, string rdbNam, string collid, out List<string> sqlScripts)
		{
			sqlScripts = null;
		}
	}
}
