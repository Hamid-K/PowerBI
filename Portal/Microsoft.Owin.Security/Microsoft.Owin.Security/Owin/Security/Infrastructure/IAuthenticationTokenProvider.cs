using System;
using System.Threading.Tasks;

namespace Microsoft.Owin.Security.Infrastructure
{
	// Token: 0x02000022 RID: 34
	public interface IAuthenticationTokenProvider
	{
		// Token: 0x060000A1 RID: 161
		void Create(AuthenticationTokenCreateContext context);

		// Token: 0x060000A2 RID: 162
		Task CreateAsync(AuthenticationTokenCreateContext context);

		// Token: 0x060000A3 RID: 163
		void Receive(AuthenticationTokenReceiveContext context);

		// Token: 0x060000A4 RID: 164
		Task ReceiveAsync(AuthenticationTokenReceiveContext context);
	}
}
