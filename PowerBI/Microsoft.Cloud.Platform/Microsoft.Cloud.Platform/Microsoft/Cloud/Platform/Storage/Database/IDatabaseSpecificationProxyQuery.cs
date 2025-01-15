using System;

namespace Microsoft.Cloud.Platform.Storage.Database
{
	// Token: 0x0200003B RID: 59
	internal interface IDatabaseSpecificationProxyQuery
	{
		// Token: 0x06000170 RID: 368
		IDatabaseSpecification GetSpecification(string key);

		// Token: 0x06000171 RID: 369
		IDatabaseSpecification GetSpecificationPreferSecondary(string key);
	}
}
