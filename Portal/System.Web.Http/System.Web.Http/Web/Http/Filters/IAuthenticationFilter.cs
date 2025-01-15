using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.Web.Http.Filters
{
	// Token: 0x020000C1 RID: 193
	public interface IAuthenticationFilter : IFilter
	{
		// Token: 0x06000543 RID: 1347
		Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken);

		// Token: 0x06000544 RID: 1348
		Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken);
	}
}
