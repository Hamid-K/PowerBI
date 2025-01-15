using System;
using System.ServiceModel;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020002A5 RID: 677
	internal class TestClientIdentityProvider : IEndpointIdentityProvider
	{
		// Token: 0x060018E0 RID: 6368 RVA: 0x0004AECF File Offset: 0x000490CF
		public TestClientIdentityProvider(string sslSubjectIdentity)
		{
			this._sslSubjectIdentity = sslSubjectIdentity;
		}

		// Token: 0x060018E1 RID: 6369 RVA: 0x0004AEDE File Offset: 0x000490DE
		public EndpointIdentity GetEndpointIdentity(string targetHost, int targetPort)
		{
			return EndpointIdentity.CreateDnsIdentity(this._sslSubjectIdentity);
		}

		// Token: 0x04000D89 RID: 3465
		private string _sslSubjectIdentity;
	}
}
