using System;
using System.ServiceModel;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020002A4 RID: 676
	internal class ClientIdentityProvider : IEndpointIdentityProvider
	{
		// Token: 0x060018DE RID: 6366 RVA: 0x0004AE48 File Offset: 0x00049048
		internal ClientIdentityProvider(DataCacheSecurity cacheSecurity, DataCacheServiceAccountType serviceAccountType)
		{
			this._cacheSecurity = cacheSecurity;
			this._dataCacheServiceAccountType = serviceAccountType;
		}

		// Token: 0x060018DF RID: 6367 RVA: 0x0004AE60 File Offset: 0x00049060
		public EndpointIdentity GetEndpointIdentity(string targetHostName, int targetPort)
		{
			if (this._cacheSecurity.SecurityMode != DataCacheSecurityMode.Transport)
			{
				if (this._cacheSecurity.SslEnabled)
				{
					return EndpointIdentity.CreateDnsIdentity(Utility.GetCertSubjectIdentity(targetHostName));
				}
				return null;
			}
			else
			{
				if (this._dataCacheServiceAccountType == DataCacheServiceAccountType.SystemAccount)
				{
					return null;
				}
				return EndpointIdentity.CreateSpnIdentity(string.Concat(new object[] { "AppFabricCachingService/", targetHostName, ":", targetPort }));
			}
		}

		// Token: 0x04000D87 RID: 3463
		private readonly DataCacheSecurity _cacheSecurity;

		// Token: 0x04000D88 RID: 3464
		private readonly DataCacheServiceAccountType _dataCacheServiceAccountType;
	}
}
