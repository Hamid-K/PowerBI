using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Cloud.Platform.Communication;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.CommunicationFramework.Routers
{
	// Token: 0x02000478 RID: 1144
	public sealed class TrivialSingleUriRouter : Router
	{
		// Token: 0x06002394 RID: 9108 RVA: 0x00080953 File Offset: 0x0007EB53
		public TrivialSingleUriRouter(IEnumerable<Type> retryToSameEndpointExceptions)
			: base(true, new RoundRobinRetryPolicy(Enumerable.Empty<Type>(), retryToSameEndpointExceptions))
		{
		}

		// Token: 0x06002395 RID: 9109 RVA: 0x00080968 File Offset: 0x0007EB68
		public override IAsyncResult BeginGetEndpoints(object[] keys, AsyncCallback callback, object state)
		{
			ExtendedDiagnostics.EnsureOperation(keys != null && keys.Length == 1, "keys length must be of size 1 when this router is being used");
			ExtendedDiagnostics.EnsureArgumentIsOfType(keys.First<object>(), typeof(Uri), "key argument must be of type Uri");
			return new CompletedAsyncResult<IEnumerable<Uri>>(callback, state, new List<Uri> { (Uri)keys.First<object>() });
		}

		// Token: 0x06002396 RID: 9110 RVA: 0x00080761 File Offset: 0x0007E961
		public override IEnumerable<Uri> EndGetEndpoints(IAsyncResult result)
		{
			return CompletedAsyncResult<IEnumerable<Uri>>.End(result);
		}
	}
}
