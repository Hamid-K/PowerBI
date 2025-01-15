using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Identity.Client.ApiConfig.Executors;

namespace Microsoft.Identity.Client
{
	// Token: 0x02000118 RID: 280
	public abstract class AbstractClientAppBaseAcquireTokenParameterBuilder<T> : AbstractAcquireTokenParameterBuilder<T> where T : AbstractAcquireTokenParameterBuilder<T>
	{
		// Token: 0x06000DFC RID: 3580 RVA: 0x00037211 File Offset: 0x00035411
		internal AbstractClientAppBaseAcquireTokenParameterBuilder(IClientApplicationBaseExecutor clientApplicationBaseExecutor)
			: base(clientApplicationBaseExecutor.ServiceBundle)
		{
			this.ClientApplicationBaseExecutor = clientApplicationBaseExecutor;
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000DFD RID: 3581 RVA: 0x00037226 File Offset: 0x00035426
		internal IClientApplicationBaseExecutor ClientApplicationBaseExecutor { get; }

		// Token: 0x06000DFE RID: 3582
		internal abstract Task<AuthenticationResult> ExecuteInternalAsync(CancellationToken cancellationToken);

		// Token: 0x06000DFF RID: 3583 RVA: 0x0003722E File Offset: 0x0003542E
		public override Task<AuthenticationResult> ExecuteAsync(CancellationToken cancellationToken)
		{
			base.ValidateAndCalculateApiId();
			return this.ExecuteInternalAsync(cancellationToken);
		}
	}
}
