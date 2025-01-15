using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x0200003F RID: 63
	public interface IQueryPermissionService
	{
		// Token: 0x0600013A RID: 314
		bool IsQueryExecutionPermitted(IResource resource, QueryPermissionChallengeType type, string query, int parameterCount, string[] parameterNames);
	}
}
