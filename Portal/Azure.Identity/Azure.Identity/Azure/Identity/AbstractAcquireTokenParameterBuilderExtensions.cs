using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Identity.Client;

namespace Azure.Identity
{
	// Token: 0x0200001F RID: 31
	internal static class AbstractAcquireTokenParameterBuilderExtensions
	{
		// Token: 0x06000094 RID: 148 RVA: 0x00003D2C File Offset: 0x00001F2C
		public static async ValueTask<AuthenticationResult> ExecuteAsync<T>(this AbstractAcquireTokenParameterBuilder<T> builder, bool async, CancellationToken cancellationToken) where T : AbstractAcquireTokenParameterBuilder<T>
		{
			AuthenticationResult authenticationResult;
			if (async)
			{
				authenticationResult = await builder.ExecuteAsync(cancellationToken).ConfigureAwait(false);
			}
			else
			{
				authenticationResult = builder.ExecuteAsync(cancellationToken).GetAwaiter().GetResult();
			}
			return authenticationResult;
		}
	}
}
