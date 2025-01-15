using System;

namespace Microsoft.Data.Mashup
{
	// Token: 0x0200000A RID: 10
	public sealed class CredentialProperty
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00002E20 File Offset: 0x00001020
		// (set) Token: 0x0600003A RID: 58 RVA: 0x00002E28 File Offset: 0x00001028
		public string Name { get; internal set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002E31 File Offset: 0x00001031
		// (set) Token: 0x0600003C RID: 60 RVA: 0x00002E39 File Offset: 0x00001039
		public string Label { get; internal set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002E42 File Offset: 0x00001042
		// (set) Token: 0x0600003E RID: 62 RVA: 0x00002E4A File Offset: 0x0000104A
		public bool IsRequired { get; internal set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002E53 File Offset: 0x00001053
		// (set) Token: 0x06000040 RID: 64 RVA: 0x00002E5B File Offset: 0x0000105B
		public bool IsSecret { get; internal set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002E64 File Offset: 0x00001064
		// (set) Token: 0x06000042 RID: 66 RVA: 0x00002E6C File Offset: 0x0000106C
		public Type PropertyType { get; internal set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00002E75 File Offset: 0x00001075
		// (set) Token: 0x06000044 RID: 68 RVA: 0x00002E7D File Offset: 0x0000107D
		internal bool AllowNull { get; set; }

		// Token: 0x04000019 RID: 25
		public const string ConnectionStringPropertyName = "ConnectionString";

		// Token: 0x0400001A RID: 26
		public const string EncryptConnectionPropertyName = "EncryptConnection";

		// Token: 0x0400001B RID: 27
		public const string UsernamePropertyName = "Username";

		// Token: 0x0400001C RID: 28
		public const string PasswordPropertyName = "Password";

		// Token: 0x0400001D RID: 29
		public const string KeyPropertyName = "Key";

		// Token: 0x0400001E RID: 30
		public const string AccessTokenPropertyName = "AccessToken";

		// Token: 0x0400001F RID: 31
		public const string ExpiresPropertyName = "Expires";

		// Token: 0x04000020 RID: 32
		public const string RefreshTokenPropertyName = "RefreshToken";

		// Token: 0x04000021 RID: 33
		public const string PropertiesPropertyName = "Properties";

		// Token: 0x04000022 RID: 34
		public const string EmailAddress = "EmailAddress";

		// Token: 0x04000023 RID: 35
		public const string EwsUrl = "EwsUrl";

		// Token: 0x04000024 RID: 36
		public const string EwsSupportedSchema = "EwsSupportedSchema";

		// Token: 0x04000025 RID: 37
		public const string EffectiveUsername = "EffectiveUserName";

		// Token: 0x04000026 RID: 38
		public const string CustomData = "CustomData";

		// Token: 0x04000027 RID: 39
		public const string SncPartnerName = "SNCPartnerName";

		// Token: 0x04000028 RID: 40
		public const string SncLibrary = "SNCLibrary";

		// Token: 0x04000029 RID: 41
		public const string SslCryptoProvider = "SSLCryptoProvider";

		// Token: 0x0400002A RID: 42
		public const string SslKeyStore = "SSLKeyStore";

		// Token: 0x0400002B RID: 43
		public const string SslTrustStore = "SSLTrustStore";

		// Token: 0x0400002C RID: 44
		public const string SslValidateServerCertificate = "SSLValidateServerCertificate";

		// Token: 0x0400002D RID: 45
		public const string IdentitySource = "IdentitySource";

		// Token: 0x04000034 RID: 52
		internal static readonly string[] KnownCredentialProperties = new string[]
		{
			"ConnectionString", "EncryptConnection", "Username", "Password", "Key", "AccessToken", "RefreshToken", "Expires", "Properties", "EmailAddress",
			"EwsUrl", "EwsSupportedSchema", "EffectiveUserName", "CustomData", "SNCPartnerName", "SNCLibrary", "SSLCryptoProvider", "SSLKeyStore", "SSLTrustStore", "SSLValidateServerCertificate"
		};
	}
}
