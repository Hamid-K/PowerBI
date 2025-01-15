using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Storage.Memory
{
	// Token: 0x020020A0 RID: 8352
	public class MemoryQueryPermissionsStorage : QueryPermissionsStorage
	{
		// Token: 0x0600CC97 RID: 52375 RVA: 0x0028AD98 File Offset: 0x00288F98
		public MemoryQueryPermissionsStorage()
		{
			this.queryPermissions = new List<QueryPermission>();
		}

		// Token: 0x0600CC98 RID: 52376 RVA: 0x0028ADAB File Offset: 0x00288FAB
		public MemoryQueryPermissionsStorage(IEnumerable<QueryPermission> queryPermissions)
		{
			this.queryPermissions = new List<QueryPermission>(queryPermissions);
		}

		// Token: 0x0600CC99 RID: 52377 RVA: 0x0028ADBF File Offset: 0x00288FBF
		public override QueryPermission[] GetQueryPermissions()
		{
			return this.queryPermissions.ToArray<QueryPermission>();
		}

		// Token: 0x0600CC9A RID: 52378 RVA: 0x0028ADCC File Offset: 0x00288FCC
		public override void SetQueryPermission(Resource resource, string query, QueryPermissionChallengeType type, int parameterCount, IEnumerable<string> parameterNames)
		{
			if (type == QueryPermissionChallengeType.EvaluateNativeQueryUnpermitted)
			{
				this.queryPermissions.Add(new NativeQueryXml(resource, query, parameterCount, parameterNames));
				return;
			}
			this.queryPermissions.Add(new QueryPermissionXml(resource, type, query));
		}

		// Token: 0x0600CC9B RID: 52379 RVA: 0x0028ADFB File Offset: 0x00288FFB
		public override void ClearQueryPermissions(params Resource[] resource)
		{
			this.queryPermissions.Clear();
		}

		// Token: 0x04006796 RID: 26518
		private readonly IList<QueryPermission> queryPermissions;
	}
}
