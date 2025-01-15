using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Azure.Core.Extensions
{
	// Token: 0x020000BC RID: 188
	[NullableContext(1)]
	public interface IAzureClientFactoryBuilderWithCredential
	{
		// Token: 0x060005C2 RID: 1474
		IAzureClientBuilder<TClient, TOptions> RegisterClientFactory<[Nullable(2)] TClient, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TOptions>(Func<TOptions, TokenCredential, TClient> clientFactory, bool requiresCredential = true) where TOptions : class;
	}
}
