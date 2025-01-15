using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x020004F3 RID: 1267
	internal class RouterRequest : IRouterRequest
	{
		// Token: 0x06002683 RID: 9859 RVA: 0x00089780 File Offset: 0x00087980
		public RouterRequest([NotNull] IEnumerable<EndpointIdentifier> initialEndpoints, bool aggregateExceptions, [NotNull] IRouterPolicy routerPolicy, [NotNull] IRetryPolicy retryPolicy)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<IEnumerable<EndpointIdentifier>>(initialEndpoints, "initialEndpoints");
			ExtendedDiagnostics.EnsureArgumentNotNull<IRouterPolicy>(routerPolicy, "routerPolicy");
			ExtendedDiagnostics.EnsureArgumentNotNull<IRetryPolicy>(retryPolicy, "retryPolicy");
			this.m_endpoints = initialEndpoints;
			this.m_aggregateExceptions = aggregateExceptions;
			this.m_routerPolicy = routerPolicy;
			this.m_retryPolicy = retryPolicy;
		}

		// Token: 0x06002684 RID: 9860 RVA: 0x000897D4 File Offset: 0x000879D4
		public IAsyncResult BeginGetNextEndpoints(IEnumerable<EndpointIdentifier> previousEndpoints, IEnumerable<EndpointFault> endpointFaults, AsyncCallback callback, object state)
		{
			if (endpointFaults == null)
			{
				return new CompletedAsyncResult<IEnumerable<EndpointIdentifier>>(callback, state, this.m_routerPolicy.GetNextEndpoints(this.m_endpoints, false));
			}
			List<EndpointIdentifier> list = new List<EndpointIdentifier>();
			foreach (EndpointFault endpointFault in endpointFaults)
			{
				if (this.m_retryPolicy.ShouldRetryToTheSameEndpoint(endpointFault, endpointFault.Endpoint.State))
				{
					list.Add(endpointFault.Endpoint);
				}
				else if (this.m_retryPolicy.ShouldRetryToDifferentEndpoint(endpointFault, endpointFault.Endpoint.State))
				{
					this.m_endpoints = this.m_endpoints.Except(previousEndpoints);
					if (this.m_endpoints.Any<EndpointIdentifier>())
					{
						list.AddRange(this.m_routerPolicy.GetNextEndpoints(this.m_endpoints, true));
					}
				}
			}
			if (list.Count != 0)
			{
				return new CompletedAsyncResult<IEnumerable<EndpointIdentifier>>(callback, state, list);
			}
			if (this.m_aggregateExceptions && endpointFaults.Count<EndpointFault>() > 1)
			{
				EndpointFault endpointFault2 = endpointFaults.First<EndpointFault>();
				IEnumerable<EndpointFault> enumerable = endpointFaults.Where((EndpointFault ep) => ep.Exception is MonitoredException);
				if (enumerable.Any<EndpointFault>())
				{
					endpointFault2 = enumerable.First<EndpointFault>();
				}
				throw new CommunicationFrameworkAggregatedException(endpointFaults.ToArray<EndpointFault>(), string.Empty, endpointFault2.ConvertedException);
			}
			throw endpointFaults.First<EndpointFault>().ConvertedException;
		}

		// Token: 0x06002685 RID: 9861 RVA: 0x00089938 File Offset: 0x00087B38
		public IEnumerable<EndpointIdentifier> EndGetNextEndpoints(IAsyncResult asyncResult)
		{
			return CompletedAsyncResult<IEnumerable<EndpointIdentifier>>.End(asyncResult);
		}

		// Token: 0x06002686 RID: 9862 RVA: 0x00089940 File Offset: 0x00087B40
		public IAsyncResult BeginCompleteRequest(AsyncCallback callback, object state)
		{
			return this.m_routerPolicy.BeginCompleteRequest(callback, state);
		}

		// Token: 0x06002687 RID: 9863 RVA: 0x0008994F File Offset: 0x00087B4F
		public void EndCompleteRequest(IAsyncResult result)
		{
			this.m_routerPolicy.EndCompleteRequest(result);
		}

		// Token: 0x04000DAF RID: 3503
		private IEnumerable<EndpointIdentifier> m_endpoints;

		// Token: 0x04000DB0 RID: 3504
		private bool m_aggregateExceptions;

		// Token: 0x04000DB1 RID: 3505
		private IRouterPolicy m_routerPolicy;

		// Token: 0x04000DB2 RID: 3506
		private IRetryPolicy m_retryPolicy;
	}
}
