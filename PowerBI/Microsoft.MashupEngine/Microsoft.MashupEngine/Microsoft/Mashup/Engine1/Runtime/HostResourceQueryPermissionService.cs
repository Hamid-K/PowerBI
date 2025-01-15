using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001331 RID: 4913
	public static class HostResourceQueryPermissionService
	{
		// Token: 0x060081C9 RID: 33225 RVA: 0x001B9094 File Offset: 0x001B7294
		public static bool IsQueryExecutionPermitted(IEngineHost hostEnvironment, IResource resource, QueryPermissionChallengeType type, string query, int parameterCount, string[] parameterNames)
		{
			if (resource.Kind == "SQL" && query == "select d.name as Name from sys.databases d where d.name not in ('master', 'model', 'msdb', 'tempdb') and d.is_distributor = 0 and d.state = 0" && parameterCount == 0)
			{
				return true;
			}
			IQueryPermissionService queryPermissionService = hostEnvironment.QueryService<IQueryPermissionService>();
			return queryPermissionService != null && queryPermissionService.IsQueryExecutionPermitted(resource, type, query, parameterCount, parameterNames);
		}

		// Token: 0x060081CA RID: 33226 RVA: 0x001B90DF File Offset: 0x001B72DF
		public static void VerifyQueryPermission(IEngineHost hostEnvironment, IResource resource, QueryPermissionChallengeType type, string query)
		{
			HostResourceQueryPermissionService.VerifyQueryPermission(hostEnvironment, resource, type, query, 0, null);
		}

		// Token: 0x060081CB RID: 33227 RVA: 0x001B90EC File Offset: 0x001B72EC
		public static void VerifyQueryPermission(IEngineHost hostEnvironment, IResource resource, QueryPermissionChallengeType type, string query, int parameterCount, string[] parameterNames)
		{
			if (!HostResourceQueryPermissionService.IsQueryExecutionPermitted(hostEnvironment, resource, type, query, parameterCount, parameterNames))
			{
				throw DataSourceException.NewQueryPermissionError(hostEnvironment, resource, type, query, parameterCount, parameterNames);
			}
		}
	}
}
