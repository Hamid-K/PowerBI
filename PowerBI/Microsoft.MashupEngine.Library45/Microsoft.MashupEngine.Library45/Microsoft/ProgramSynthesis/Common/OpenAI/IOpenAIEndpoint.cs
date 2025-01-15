using System;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.ProgramSynthesis.Common.OpenAI
{
	// Token: 0x020006B1 RID: 1713
	public interface IOpenAIEndpoint<TRequest, TResponse> : IOpenAIEndpoint
	{
		// Token: 0x06002520 RID: 9504
		Task<TResponse> GetResponseAsync(TRequest request, CancellationToken cancellationToken);
	}
}
