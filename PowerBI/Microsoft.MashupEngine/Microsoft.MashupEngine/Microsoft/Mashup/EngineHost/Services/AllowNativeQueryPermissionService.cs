using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001A45 RID: 6725
	internal sealed class AllowNativeQueryPermissionService : IQueryPermissionService
	{
		// Token: 0x0600AA0A RID: 43530 RVA: 0x002322E9 File Offset: 0x002304E9
		public AllowNativeQueryPermissionService(IQueryPermissionService queryPermissionService)
		{
			this.queryPermissionService = queryPermissionService;
		}

		// Token: 0x0600AA0B RID: 43531 RVA: 0x002322F8 File Offset: 0x002304F8
		public bool IsQueryExecutionPermitted(IResource resource, QueryPermissionChallengeType type, string query, int parameterCount, string[] parameterNames)
		{
			return type == QueryPermissionChallengeType.EvaluateNativeQueryUnpermitted || this.queryPermissionService.IsQueryExecutionPermitted(resource, type, query, parameterCount, parameterNames);
		}

		// Token: 0x0400585D RID: 22621
		private IQueryPermissionService queryPermissionService;
	}
}
