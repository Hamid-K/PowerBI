using System;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.Data.Contracts
{
	// Token: 0x0200008F RID: 143
	public interface IDateTimeProviderFactory
	{
		// Token: 0x06000345 RID: 837
		IDateTimeProvider CreateDateTimeProvider(DateTime? anchorTime);
	}
}
