using System;
using System.Threading.Tasks;
using Azure.Core;
using Azure.Core.Pipeline;

namespace Azure.Identity
{
	// Token: 0x02000078 RID: 120
	internal class DefaultAzureCredentialImdsRetryPolicy : RetryPolicy
	{
		// Token: 0x06000421 RID: 1057 RVA: 0x0000CAFF File Offset: 0x0000ACFF
		public DefaultAzureCredentialImdsRetryPolicy(RetryOptions retryOptions, DelayStrategy delayStrategy = null)
			: base(retryOptions.MaxRetries, delayStrategy ?? DelayStrategy.CreateExponentialDelayStrategy(new TimeSpan?(retryOptions.Delay), new TimeSpan?(retryOptions.MaxDelay)))
		{
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x0000CB2D File Offset: 0x0000AD2D
		protected override bool ShouldRetry(HttpMessage message, Exception exception)
		{
			return !ImdsManagedIdentitySource.IsProbRequest(message) && base.ShouldRetry(message, exception);
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x0000CB44 File Offset: 0x0000AD44
		protected override ValueTask<bool> ShouldRetryAsync(HttpMessage message, Exception exception)
		{
			if (ImdsManagedIdentitySource.IsProbRequest(message))
			{
				return default(ValueTask<bool>);
			}
			return base.ShouldRetryAsync(message, exception);
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0000CB6B File Offset: 0x0000AD6B
		public override void Process(HttpMessage message, ReadOnlyMemory<HttpPipelinePolicy> pipeline)
		{
			base.Process(message, pipeline);
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0000CB75 File Offset: 0x0000AD75
		public override ValueTask ProcessAsync(HttpMessage message, ReadOnlyMemory<HttpPipelinePolicy> pipeline)
		{
			return base.ProcessAsync(message, pipeline);
		}
	}
}
