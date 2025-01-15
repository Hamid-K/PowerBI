using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Storage
{
	// Token: 0x0200208A RID: 8330
	public abstract class QueryPermissionsStorage
	{
		// Token: 0x0600CBDA RID: 52186
		public abstract QueryPermission[] GetQueryPermissions();

		// Token: 0x0600CBDB RID: 52187
		public abstract void SetQueryPermission(Resource resource, string queryPermission, QueryPermissionChallengeType type, int parameterCount, IEnumerable<string> parameterNames);

		// Token: 0x0600CBDC RID: 52188
		public abstract void ClearQueryPermissions(params Resource[] resources);
	}
}
