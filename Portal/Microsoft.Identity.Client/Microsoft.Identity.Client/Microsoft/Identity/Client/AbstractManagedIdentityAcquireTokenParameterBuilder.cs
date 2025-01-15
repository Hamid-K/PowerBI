using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Identity.Client.ApiConfig.Executors;

namespace Microsoft.Identity.Client
{
	// Token: 0x0200011A RID: 282
	public abstract class AbstractManagedIdentityAcquireTokenParameterBuilder<T> : BaseAbstractAcquireTokenParameterBuilder<T> where T : BaseAbstractAcquireTokenParameterBuilder<T>
	{
		// Token: 0x06000E06 RID: 3590 RVA: 0x00037337 File Offset: 0x00035537
		protected AbstractManagedIdentityAcquireTokenParameterBuilder()
		{
		}

		// Token: 0x06000E07 RID: 3591 RVA: 0x0003733F File Offset: 0x0003553F
		internal AbstractManagedIdentityAcquireTokenParameterBuilder(IManagedIdentityApplicationExecutor managedIdentityApplicationExecutor)
			: base(managedIdentityApplicationExecutor.ServiceBundle)
		{
			ApplicationBase.GuardMobileFrameworks();
			this.ManagedIdentityApplicationExecutor = managedIdentityApplicationExecutor;
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000E08 RID: 3592 RVA: 0x00037359 File Offset: 0x00035559
		internal IManagedIdentityApplicationExecutor ManagedIdentityApplicationExecutor { get; }

		// Token: 0x06000E09 RID: 3593
		internal abstract Task<AuthenticationResult> ExecuteInternalAsync(CancellationToken cancellationToken);

		// Token: 0x06000E0A RID: 3594 RVA: 0x00037361 File Offset: 0x00035561
		public override Task<AuthenticationResult> ExecuteAsync(CancellationToken cancellationToken)
		{
			ApplicationBase.GuardMobileFrameworks();
			base.ValidateAndCalculateApiId();
			return this.ExecuteInternalAsync(cancellationToken);
		}
	}
}
