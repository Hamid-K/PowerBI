using System;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x020004F4 RID: 1268
	public class PredicateRetryPolicy : IRetryPolicy
	{
		// Token: 0x06002688 RID: 9864 RVA: 0x0008995D File Offset: 0x00087B5D
		public PredicateRetryPolicy([NotNull] Predicate<Exception> shouldRetryToSameEndpoint, [NotNull] Predicate<Exception> shouldRetryToDifferentEndpoints, int numRetries)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<Predicate<Exception>>(shouldRetryToSameEndpoint, "shouldRetryToSameEndpoint");
			ExtendedDiagnostics.EnsureArgumentNotNull<Predicate<Exception>>(shouldRetryToDifferentEndpoints, "shouldRetryToDifferentEndpoints");
			ExtendedDiagnostics.EnsureArgumentIsNotNegative(numRetries, "numRetries");
			this.m_shouldRetryToSameEndpoint = shouldRetryToSameEndpoint;
			this.m_shouldRetryToDifferentEndpoints = shouldRetryToDifferentEndpoints;
			this.m_numRetries = numRetries;
		}

		// Token: 0x06002689 RID: 9865 RVA: 0x00089424 File Offset: 0x00087624
		public object CreateInitialState()
		{
			return new RetryState();
		}

		// Token: 0x0600268A RID: 9866 RVA: 0x0008999C File Offset: 0x00087B9C
		public bool ShouldRetryToTheSameEndpoint(EndpointFault endpointFault, object state)
		{
			RetryState retryState = (RetryState)state;
			if (retryState.RetryCount >= this.m_numRetries)
			{
				return false;
			}
			RetryState retryState2 = retryState;
			int retryCount = retryState2.RetryCount;
			retryState2.RetryCount = retryCount + 1;
			return this.m_shouldRetryToSameEndpoint(endpointFault.Exception);
		}

		// Token: 0x0600268B RID: 9867 RVA: 0x000899E4 File Offset: 0x00087BE4
		public bool ShouldRetryToDifferentEndpoint(EndpointFault endpointFault, object state)
		{
			RetryState retryState = (RetryState)state;
			if (retryState.RetryCount >= this.m_numRetries)
			{
				return false;
			}
			RetryState retryState2 = retryState;
			int retryCount = retryState2.RetryCount;
			retryState2.RetryCount = retryCount + 1;
			return this.m_shouldRetryToDifferentEndpoints(endpointFault.Exception);
		}

		// Token: 0x04000DB3 RID: 3507
		private Predicate<Exception> m_shouldRetryToSameEndpoint;

		// Token: 0x04000DB4 RID: 3508
		private Predicate<Exception> m_shouldRetryToDifferentEndpoints;

		// Token: 0x04000DB5 RID: 3509
		private int m_numRetries;

		// Token: 0x04000DB6 RID: 3510
		public static Predicate<Exception> DoNotRetry = (Exception ex) => false;
	}
}
