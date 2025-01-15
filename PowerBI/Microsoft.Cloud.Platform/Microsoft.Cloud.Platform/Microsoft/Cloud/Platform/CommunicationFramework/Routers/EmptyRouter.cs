using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using Microsoft.Cloud.Platform.Communication;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.CommunicationFramework.Routers
{
	// Token: 0x02000476 RID: 1142
	public sealed class EmptyRouter : Router
	{
		// Token: 0x06002389 RID: 9097 RVA: 0x00080845 File Offset: 0x0007EA45
		public EmptyRouter()
			: base(true, new RoundRobinRetryPolicy(new List<Type> { typeof(EndpointNotFoundException) }))
		{
		}

		// Token: 0x0600238A RID: 9098 RVA: 0x00080868 File Offset: 0x0007EA68
		public override IAsyncResult BeginGetEndpoints(object[] keys, AsyncCallback callback, object state)
		{
			return new CompletedAsyncResult<IEnumerable<Uri>>(callback, state, Enumerable.Empty<Uri>());
		}

		// Token: 0x0600238B RID: 9099 RVA: 0x00080876 File Offset: 0x0007EA76
		public override IEnumerable<Uri> EndGetEndpoints(IAsyncResult result)
		{
			return ((CompletedAsyncResult<IEnumerable<Uri>>)result).End();
		}
	}
}
