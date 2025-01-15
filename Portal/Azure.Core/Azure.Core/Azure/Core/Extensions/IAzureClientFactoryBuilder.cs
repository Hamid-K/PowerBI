using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Azure.Core.Extensions
{
	// Token: 0x020000BA RID: 186
	[NullableContext(1)]
	public interface IAzureClientFactoryBuilder
	{
		// Token: 0x060005C0 RID: 1472
		IAzureClientBuilder<TClient, TOptions> RegisterClientFactory<[Nullable(2)] TClient, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TOptions>(Func<TOptions, TClient> clientFactory) where TOptions : class;
	}
}
