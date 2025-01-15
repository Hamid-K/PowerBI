using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Azure.Core.Extensions
{
	// Token: 0x020000BB RID: 187
	public interface IAzureClientFactoryBuilderWithConfiguration<[Nullable(2)] in TConfiguration> : IAzureClientFactoryBuilder
	{
		// Token: 0x060005C1 RID: 1473
		[NullableContext(1)]
		[RequiresUnreferencedCode("Binding strongly typed objects to configuration values is not supported with trimming. Use the Configuration Binder Source Generator (EnableConfigurationBindingGenerator=true) instead.")]
		[RequiresDynamicCode("Binding strongly typed objects to configuration values requires generating dynamic code at runtime, for example instantiating generic types. Use the Configuration Binder Source Generator (EnableConfigurationBindingGenerator=true) instead.")]
		IAzureClientBuilder<TClient, TOptions> RegisterClientFactory<[Nullable(2), DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TClient, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TOptions>(TConfiguration configuration) where TOptions : class;
	}
}
