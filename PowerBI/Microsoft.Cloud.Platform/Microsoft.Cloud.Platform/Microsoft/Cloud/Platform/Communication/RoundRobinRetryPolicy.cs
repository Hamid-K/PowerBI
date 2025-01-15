using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x020004F1 RID: 1265
	public class RoundRobinRetryPolicy : IRetryPolicy
	{
		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x06002668 RID: 9832 RVA: 0x000893C8 File Offset: 0x000875C8
		// (set) Token: 0x06002669 RID: 9833 RVA: 0x000893D0 File Offset: 0x000875D0
		private protected IEnumerable<Type> RetryToDifferentEndPointExceptions { protected get; private set; }

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x0600266A RID: 9834 RVA: 0x000893D9 File Offset: 0x000875D9
		// (set) Token: 0x0600266B RID: 9835 RVA: 0x000893E1 File Offset: 0x000875E1
		private protected IEnumerable<Type> RetryToSameEndPointExceptions { protected get; private set; }

		// Token: 0x0600266C RID: 9836 RVA: 0x000893EA File Offset: 0x000875EA
		public RoundRobinRetryPolicy(IEnumerable<Type> retryExceptions)
			: this(retryExceptions, Enumerable.Empty<Type>())
		{
		}

		// Token: 0x0600266D RID: 9837 RVA: 0x000893F8 File Offset: 0x000875F8
		public RoundRobinRetryPolicy([NotNull] IEnumerable<Type> retryToDifferentEndPointExceptions, [NotNull] IEnumerable<Type> retryToSameEndPointExceptions)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<IEnumerable<Type>>(retryToDifferentEndPointExceptions, "retryToDifferentEndPointExceptions");
			ExtendedDiagnostics.EnsureArgumentNotNull<IEnumerable<Type>>(retryToSameEndPointExceptions, "retryToSameEndPointExceptions");
			this.RetryToDifferentEndPointExceptions = retryToDifferentEndPointExceptions;
			this.RetryToSameEndPointExceptions = retryToSameEndPointExceptions;
		}

		// Token: 0x0600266E RID: 9838 RVA: 0x00089424 File Offset: 0x00087624
		public object CreateInitialState()
		{
			return new RetryState();
		}

		// Token: 0x0600266F RID: 9839 RVA: 0x0008942C File Offset: 0x0008762C
		public virtual bool ShouldRetryToTheSameEndpoint(EndpointFault exceptionInformation, object state)
		{
			Exception ex = exceptionInformation.Exception;
			RetryState retryState = (RetryState)state;
			if (retryState.RetryCount > 0)
			{
				return false;
			}
			if (this.RetryToSameEndPointExceptions.Any((Type e) => e.IsAssignableFrom(ex.GetType())))
			{
				RetryState retryState2 = retryState;
				int num = retryState2.RetryCount;
				retryState2.RetryCount = num + 1;
				TraceSourceBase<CommunicationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Info, "Performing retry to the same element '{0}' after receiving exception '{1}'", new object[]
				{
					exceptionInformation.Endpoint.Uri,
					ex
				});
				return true;
			}
			if (ex is CommunicationException && !(ex is ActionNotSupportedException) && !(ex is FaultException) && !(ex is EndpointNotFoundException) && !(ex is ProtocolException))
			{
				RetryState retryState3 = retryState;
				int num = retryState3.RetryCount;
				retryState3.RetryCount = num + 1;
				TraceSourceBase<CommunicationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Info, "Performing retry to the same element '{0}' after receiving exception '{1}'", new object[]
				{
					exceptionInformation.Endpoint.Uri,
					ex
				});
				return true;
			}
			return false;
		}

		// Token: 0x06002670 RID: 9840 RVA: 0x00089538 File Offset: 0x00087738
		public virtual bool ShouldRetryToDifferentEndpoint(EndpointFault exceptionInformation, object state)
		{
			Exception ex = exceptionInformation.Exception;
			((RetryState)state).RetryCount = 0;
			return this.RetryToDifferentEndPointExceptions.Any((Type e) => e.IsAssignableFrom(ex.GetType()));
		}
	}
}
