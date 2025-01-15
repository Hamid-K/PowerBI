using System;
using System.Collections.Generic;
using System.ServiceModel;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x020004F8 RID: 1272
	public class UrisRoundRobinRouter : Router
	{
		// Token: 0x06002696 RID: 9878 RVA: 0x00089B20 File Offset: 0x00087D20
		public UrisRoundRobinRouter(IEnumerable<Uri> uris, IEnumerable<Type> retryToDifferentEndPointExceptions, IEnumerable<Type> retryToSameEndPointExceptions)
			: this(uris, new RoundRobinRetryPolicy(retryToDifferentEndPointExceptions, retryToSameEndPointExceptions))
		{
		}

		// Token: 0x06002697 RID: 9879 RVA: 0x00089B30 File Offset: 0x00087D30
		public UrisRoundRobinRouter(IEnumerable<Uri> uris)
			: this(uris, new RoundRobinRetryPolicy(new List<Type> { typeof(EndpointNotFoundException) }))
		{
		}

		// Token: 0x06002698 RID: 9880 RVA: 0x00089B53 File Offset: 0x00087D53
		public UrisRoundRobinRouter(IEnumerable<Uri> uris, IRetryPolicy retryPolicy)
			: base(true, retryPolicy)
		{
			this.m_uris = uris;
		}

		// Token: 0x06002699 RID: 9881 RVA: 0x00089AFD File Offset: 0x00087CFD
		public override IAsyncResult BeginGetEndpoints(object[] keys, AsyncCallback callback, object state)
		{
			VoidAsyncResult voidAsyncResult = new VoidAsyncResult(callback, state);
			voidAsyncResult.SignalCompletion(true);
			return voidAsyncResult;
		}

		// Token: 0x0600269A RID: 9882 RVA: 0x00089B64 File Offset: 0x00087D64
		public override IEnumerable<Uri> EndGetEndpoints(IAsyncResult result)
		{
			((VoidAsyncResult)result).End();
			return this.m_uris;
		}

		// Token: 0x04000DB8 RID: 3512
		private IEnumerable<Uri> m_uris;
	}
}
