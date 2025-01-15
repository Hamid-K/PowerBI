using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Storage;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001A44 RID: 6724
	internal sealed class QueryPermissionService : IQueryPermissionService
	{
		// Token: 0x0600AA08 RID: 43528 RVA: 0x002322C1 File Offset: 0x002304C1
		public QueryPermissionService(QueryPermissionManager queryPermissionManager)
		{
			this.queryPermissionManager = queryPermissionManager;
		}

		// Token: 0x0600AA09 RID: 43529 RVA: 0x002322D0 File Offset: 0x002304D0
		public bool IsQueryExecutionPermitted(IResource resource, QueryPermissionChallengeType type, string query, int parameterCount, string[] parameterNames)
		{
			return this.queryPermissionManager.TryGetQueryPermission(new Resource(resource), type, query, parameterCount, parameterNames);
		}

		// Token: 0x0400585C RID: 22620
		private readonly QueryPermissionManager queryPermissionManager;
	}
}
