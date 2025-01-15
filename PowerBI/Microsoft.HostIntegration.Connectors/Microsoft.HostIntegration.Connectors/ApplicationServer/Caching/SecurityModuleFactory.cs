using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200019A RID: 410
	internal static class SecurityModuleFactory
	{
		// Token: 0x06000D63 RID: 3427 RVA: 0x0002DB50 File Offset: 0x0002BD50
		internal static IChannelSecurityModule GetChannelSecurityModule(DataCacheSecurity securityProperties, string logPrefix)
		{
			IChannelSecurityModule channelSecurityModule;
			if (securityProperties.SslEnabled)
			{
				channelSecurityModule = new SslModule(logPrefix);
			}
			else
			{
				channelSecurityModule = new WindowsAuthenticationModule();
			}
			return channelSecurityModule;
		}

		// Token: 0x06000D64 RID: 3428 RVA: 0x0002DB78 File Offset: 0x0002BD78
		internal static IChannelSecurityModule GetServerChannelSecurityModule(DataCacheSecurity securityProperties, string logPrefix)
		{
			if (securityProperties.SslEnabled)
			{
				SslModule sslModule = new SslModule(logPrefix);
				sslModule.Initialize(securityProperties.SslSubjectIdentity);
				return sslModule;
			}
			return new WindowsAuthenticationModule();
		}
	}
}
