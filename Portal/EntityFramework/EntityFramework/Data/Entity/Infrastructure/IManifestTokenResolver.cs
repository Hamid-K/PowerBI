using System;
using System.Data.Common;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x02000250 RID: 592
	public interface IManifestTokenResolver
	{
		// Token: 0x06001EBB RID: 7867
		string ResolveManifestToken(DbConnection connection);
	}
}
