using System;
using System.Globalization;
using System.Security;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000380 RID: 896
	public class DataCacheSecurity
	{
		// Token: 0x06001F8A RID: 8074 RVA: 0x00060345 File Offset: 0x0005E545
		public DataCacheSecurity()
			: this(DataCacheSecurityMode.Transport, DataCacheProtectionLevel.EncryptAndSign)
		{
		}

		// Token: 0x06001F8B RID: 8075 RVA: 0x0006034F File Offset: 0x0005E54F
		internal DataCacheSecurity(bool sslEnabled)
		{
			this._securityMode = DataCacheSecurityMode.None;
			this._protectionLevel = DataCacheProtectionLevel.None;
			this._sslEnabled = sslEnabled;
		}

		// Token: 0x06001F8C RID: 8076 RVA: 0x0006036C File Offset: 0x0005E56C
		public DataCacheSecurity(DataCacheSecurityMode securityMode, DataCacheProtectionLevel protectionLevel)
		{
			this._securityMode = securityMode;
			this._protectionLevel = protectionLevel;
		}

		// Token: 0x06001F8D RID: 8077 RVA: 0x00060382 File Offset: 0x0005E582
		public DataCacheSecurity(SecureString authorizationToken)
			: this(authorizationToken, false)
		{
		}

		// Token: 0x06001F8E RID: 8078 RVA: 0x0006038C File Offset: 0x0005E58C
		public DataCacheSecurity(string authorizationToken)
			: this(authorizationToken, false)
		{
		}

		// Token: 0x06001F8F RID: 8079 RVA: 0x00060396 File Offset: 0x0005E596
		public DataCacheSecurity(SecureString authorizationToken, bool sslEnabled)
			: this(DataCacheSecurityMode.Message, DataCacheProtectionLevel.None, AuthorizationType.Token, SecureStringHelper.GetUnSecureString(authorizationToken), sslEnabled)
		{
		}

		// Token: 0x06001F90 RID: 8080 RVA: 0x000603A8 File Offset: 0x0005E5A8
		public DataCacheSecurity(string authorizationToken, bool sslEnabled)
			: this(DataCacheSecurityMode.Message, DataCacheProtectionLevel.None, AuthorizationType.Token, authorizationToken, sslEnabled)
		{
		}

		// Token: 0x06001F91 RID: 8081 RVA: 0x000603B8 File Offset: 0x0005E5B8
		internal DataCacheSecurity(DataCacheSecurityMode securityMode, DataCacheProtectionLevel protectionLevel, AuthorizationType type, string authorization, bool sslEnabled)
			: this(securityMode, protectionLevel)
		{
			this._authorizationType = type;
			this._sslEnabled = sslEnabled;
			switch (this._authorizationType)
			{
			case AuthorizationType.Token:
				this._acsTokenManager = AcsTokenManager.Parse(authorization);
				if (this._acsTokenManager == null)
				{
					throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "AuthTokenInvalid"));
				}
				return;
			case AuthorizationType.SharedKey:
				this._sharedKey = authorization;
				return;
			}
			throw new NotSupportedException();
		}

		// Token: 0x06001F92 RID: 8082 RVA: 0x00060430 File Offset: 0x0005E630
		internal DataCacheSecurity(ServerSecurityProperties serverSecurityProperties)
			: this(serverSecurityProperties.DataCacheSecurityMode, serverSecurityProperties.DataCacheProtectionLevel)
		{
			if (serverSecurityProperties.SharedKeyAuth.IsEnabled)
			{
				this._authorizationType |= AuthorizationType.SharedKey;
				this._sharedKey = serverSecurityProperties.SharedKeyAuth.AuthorizationKey;
			}
			if (serverSecurityProperties.UseAcsForClient)
			{
				this._authorizationType |= AuthorizationType.Token;
			}
			if (serverSecurityProperties.SslEnabled)
			{
				this._sslEnabled = serverSecurityProperties.SslEnabled;
				this._sslSubjectIdentity = serverSecurityProperties.SslProperties.SslCertIdentity;
			}
		}

		// Token: 0x06001F93 RID: 8083 RVA: 0x000604B8 File Offset: 0x0005E6B8
		internal DataCacheSecurity(ClientSecurityProperties securityProperties)
			: this(securityProperties.DataCacheSecurityMode, securityProperties.DataCacheProtectionLevel)
		{
			if (securityProperties.DataCacheSecurityMode == DataCacheSecurityMode.Message)
			{
				this._acsTokenManager = AcsTokenManager.Parse(securityProperties.MessageSecurity.AuthorizationInfo);
				if (this._acsTokenManager == null)
				{
					throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "AuthTokenInvalid"));
				}
				this._authorizationType = AuthorizationType.Token;
			}
			this._sslEnabled = securityProperties.SslEnabled;
			this._sslSubjectIdentity = securityProperties.SslSubjectIdentity;
		}

		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x06001F94 RID: 8084 RVA: 0x00060532 File Offset: 0x0005E732
		public DataCacheSecurityMode SecurityMode
		{
			get
			{
				return this._securityMode;
			}
		}

		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x06001F95 RID: 8085 RVA: 0x0006053A File Offset: 0x0005E73A
		public DataCacheProtectionLevel ProtectionLevel
		{
			get
			{
				return this._protectionLevel;
			}
		}

		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x06001F96 RID: 8086 RVA: 0x00060542 File Offset: 0x0005E742
		internal AuthorizationType AuthorizationType
		{
			get
			{
				return this._authorizationType;
			}
		}

		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x06001F97 RID: 8087 RVA: 0x0006054A File Offset: 0x0005E74A
		internal string SharedKey
		{
			get
			{
				return this._sharedKey;
			}
		}

		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x06001F98 RID: 8088 RVA: 0x00060552 File Offset: 0x0005E752
		internal AcsTokenManager AcsTokenManager
		{
			get
			{
				return this._acsTokenManager;
			}
		}

		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x06001F99 RID: 8089 RVA: 0x0006055A File Offset: 0x0005E75A
		internal bool SslEnabled
		{
			get
			{
				return this._sslEnabled;
			}
		}

		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x06001F9A RID: 8090 RVA: 0x00060562 File Offset: 0x0005E762
		// (set) Token: 0x06001F9B RID: 8091 RVA: 0x0006056A File Offset: 0x0005E76A
		internal string SslSubjectIdentity
		{
			get
			{
				return this._sslSubjectIdentity;
			}
			set
			{
				this._sslSubjectIdentity = value;
			}
		}

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x06001F9C RID: 8092 RVA: 0x0006055A File Offset: 0x0005E75A
		internal bool IsChannelSecurityNeeded
		{
			get
			{
				return this._sslEnabled;
			}
		}

		// Token: 0x040012CE RID: 4814
		private DataCacheSecurityMode _securityMode;

		// Token: 0x040012CF RID: 4815
		private DataCacheProtectionLevel _protectionLevel;

		// Token: 0x040012D0 RID: 4816
		private AcsTokenManager _acsTokenManager;

		// Token: 0x040012D1 RID: 4817
		private string _sharedKey;

		// Token: 0x040012D2 RID: 4818
		private AuthorizationType _authorizationType;

		// Token: 0x040012D3 RID: 4819
		private bool _sslEnabled;

		// Token: 0x040012D4 RID: 4820
		private string _sslSubjectIdentity;
	}
}
