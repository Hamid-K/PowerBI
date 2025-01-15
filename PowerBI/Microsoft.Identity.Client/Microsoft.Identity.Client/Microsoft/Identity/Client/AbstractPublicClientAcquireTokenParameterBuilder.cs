using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Identity.Client.ApiConfig.Executors;

namespace Microsoft.Identity.Client
{
	// Token: 0x0200011B RID: 283
	public abstract class AbstractPublicClientAcquireTokenParameterBuilder<T> : AbstractAcquireTokenParameterBuilder<T> where T : AbstractAcquireTokenParameterBuilder<T>
	{
		// Token: 0x06000E0B RID: 3595 RVA: 0x00037375 File Offset: 0x00035575
		internal AbstractPublicClientAcquireTokenParameterBuilder(IPublicClientApplicationExecutor publicClientApplicationExecutor)
			: base(publicClientApplicationExecutor.ServiceBundle)
		{
			this.PublicClientApplicationExecutor = publicClientApplicationExecutor;
		}

		// Token: 0x06000E0C RID: 3596
		internal abstract Task<AuthenticationResult> ExecuteInternalAsync(CancellationToken cancellationToken);

		// Token: 0x06000E0D RID: 3597 RVA: 0x0003738A File Offset: 0x0003558A
		public override Task<AuthenticationResult> ExecuteAsync(CancellationToken cancellationToken)
		{
			base.ValidateAndCalculateApiId();
			return this.ExecuteInternalAsync(cancellationToken);
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000E0E RID: 3598 RVA: 0x00037399 File Offset: 0x00035599
		internal IPublicClientApplicationExecutor PublicClientApplicationExecutor { get; }
	}
}
