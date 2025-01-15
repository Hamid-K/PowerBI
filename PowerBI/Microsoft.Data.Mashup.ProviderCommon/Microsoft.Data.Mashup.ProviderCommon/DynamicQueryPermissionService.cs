using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Data.Mashup.ProviderCommon
{
	// Token: 0x02000009 RID: 9
	internal class DynamicQueryPermissionService : IQueryPermissionService
	{
		// Token: 0x06000028 RID: 40 RVA: 0x00002914 File Offset: 0x00000B14
		public DynamicQueryPermissionService(IQueryPermissionService queryPermissionService, ConnectionContext connection, IEvaluationConstants evaluationConstants = null)
		{
			this.queryPermissionService = queryPermissionService;
			this.connectionContext = connection;
			this.evaluationConstants = evaluationConstants;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002934 File Offset: 0x00000B34
		public bool IsQueryExecutionPermitted(IResource resource, QueryPermissionChallengeType type, string query, int parameterCount, string[] parameterNames)
		{
			if (this.queryPermissionService.IsQueryExecutionPermitted(resource, type, query, parameterCount, parameterNames))
			{
				return true;
			}
			bool flag;
			try
			{
				flag = this.connectionContext.IsQueryExecutionPermitted(resource, type, query, parameterCount, parameterNames);
			}
			catch (Exception ex) when (ProviderTracing.TraceIsSafeException("DynamicQueryPermissionService/IsQueryExecutionPermitted", ex, this.evaluationConstants, resource))
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x04000016 RID: 22
		private readonly ConnectionContext connectionContext;

		// Token: 0x04000017 RID: 23
		private readonly IQueryPermissionService queryPermissionService;

		// Token: 0x04000018 RID: 24
		private readonly IEvaluationConstants evaluationConstants;
	}
}
