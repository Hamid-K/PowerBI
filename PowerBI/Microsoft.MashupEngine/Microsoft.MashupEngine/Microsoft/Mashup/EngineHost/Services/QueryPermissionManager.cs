using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Storage;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001A43 RID: 6723
	public class QueryPermissionManager
	{
		// Token: 0x0600AA06 RID: 43526 RVA: 0x00232294 File Offset: 0x00230494
		public QueryPermissionManager(QueryPermission[] queryPermissions)
		{
			this.queryPermissions = new List<QueryPermission>(queryPermissions);
		}

		// Token: 0x0600AA07 RID: 43527 RVA: 0x002322A8 File Offset: 0x002304A8
		public bool TryGetQueryPermission(Resource resource, QueryPermissionChallengeType type, string query, int parameterCount, string[] parameterNames)
		{
			return QueryPermission.Lookup(this.queryPermissions.ToArray(), resource, type, query, parameterCount, parameterNames);
		}

		// Token: 0x0400585B RID: 22619
		private List<QueryPermission> queryPermissions;
	}
}
