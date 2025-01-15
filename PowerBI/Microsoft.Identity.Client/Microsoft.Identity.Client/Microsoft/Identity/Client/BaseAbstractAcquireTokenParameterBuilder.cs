using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Identity.Client.ApiConfig.Parameters;
using Microsoft.Identity.Client.Internal;
using Microsoft.Identity.Client.TelemetryCore.Internal.Events;

namespace Microsoft.Identity.Client
{
	// Token: 0x02000126 RID: 294
	public abstract class BaseAbstractAcquireTokenParameterBuilder<T> where T : BaseAbstractAcquireTokenParameterBuilder<T>
	{
		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000E7B RID: 3707 RVA: 0x00038108 File Offset: 0x00036308
		internal IServiceBundle ServiceBundle { get; }

		// Token: 0x06000E7C RID: 3708 RVA: 0x00038110 File Offset: 0x00036310
		protected BaseAbstractAcquireTokenParameterBuilder()
		{
		}

		// Token: 0x06000E7D RID: 3709 RVA: 0x00038123 File Offset: 0x00036323
		internal BaseAbstractAcquireTokenParameterBuilder(IServiceBundle serviceBundle)
		{
			this.ServiceBundle = serviceBundle;
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000E7E RID: 3710 RVA: 0x0003813D File Offset: 0x0003633D
		internal AcquireTokenCommonParameters CommonParameters { get; } = new AcquireTokenCommonParameters();

		// Token: 0x06000E7F RID: 3711
		public abstract Task<AuthenticationResult> ExecuteAsync(CancellationToken cancellationToken);

		// Token: 0x06000E80 RID: 3712
		internal abstract ApiEvent.ApiIds CalculateApiEventId();

		// Token: 0x06000E81 RID: 3713 RVA: 0x00038145 File Offset: 0x00036345
		public Task<AuthenticationResult> ExecuteAsync()
		{
			return this.ExecuteAsync(CancellationToken.None);
		}

		// Token: 0x06000E82 RID: 3714 RVA: 0x00038152 File Offset: 0x00036352
		public T WithCorrelationId(Guid correlationId)
		{
			this.CommonParameters.UserProvidedCorrelationId = correlationId;
			this.CommonParameters.UseCorrelationIdFromUser = true;
			return (T)((object)this);
		}

		// Token: 0x06000E83 RID: 3715 RVA: 0x00038172 File Offset: 0x00036372
		protected virtual void Validate()
		{
		}

		// Token: 0x06000E84 RID: 3716 RVA: 0x00038174 File Offset: 0x00036374
		internal void ValidateAndCalculateApiId()
		{
			this.Validate();
			this.CommonParameters.ApiId = this.CalculateApiEventId();
			this.CommonParameters.CorrelationId = (this.CommonParameters.UseCorrelationIdFromUser ? this.CommonParameters.UserProvidedCorrelationId : Guid.NewGuid());
		}

		// Token: 0x06000E85 RID: 3717 RVA: 0x000381C2 File Offset: 0x000363C2
		internal void ValidateUseOfExperimentalFeature([CallerMemberName] string memberName = "")
		{
			if (!this.ServiceBundle.Config.ExperimentalFeaturesEnabled)
			{
				throw new MsalClientException("experimental_feature", MsalErrorMessage.ExperimentalFeature(memberName));
			}
		}
	}
}
