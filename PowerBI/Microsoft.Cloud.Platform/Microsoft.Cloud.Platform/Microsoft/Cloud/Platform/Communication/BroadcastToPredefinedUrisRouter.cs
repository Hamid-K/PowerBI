using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x020004F7 RID: 1271
	public class BroadcastToPredefinedUrisRouter : Router
	{
		// Token: 0x06002693 RID: 9875 RVA: 0x00089ACE File Offset: 0x00087CCE
		public BroadcastToPredefinedUrisRouter(IEnumerable<Uri> uris)
			: base(false, new RoundRobinRetryPolicy(new List<Type> { typeof(EndpointNotFoundException) }))
		{
			this.m_uris = uris.ToArray<Uri>();
		}

		// Token: 0x06002694 RID: 9876 RVA: 0x00089AFD File Offset: 0x00087CFD
		public override IAsyncResult BeginGetEndpoints(object[] keys, AsyncCallback callback, object state)
		{
			VoidAsyncResult voidAsyncResult = new VoidAsyncResult(callback, state);
			voidAsyncResult.SignalCompletion(true);
			return voidAsyncResult;
		}

		// Token: 0x06002695 RID: 9877 RVA: 0x00089B0D File Offset: 0x00087D0D
		public override IEnumerable<Uri> EndGetEndpoints(IAsyncResult result)
		{
			((VoidAsyncResult)result).End();
			return this.m_uris;
		}

		// Token: 0x04000DB7 RID: 3511
		private Uri[] m_uris;
	}
}
