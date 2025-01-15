using System;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Azure.Core.Pipeline;
using Microsoft.Identity.Client;

namespace Azure.Identity
{
	// Token: 0x02000025 RID: 37
	public class AuthenticationRecord
	{
		// Token: 0x060000AE RID: 174 RVA: 0x00003FF7 File Offset: 0x000021F7
		internal AuthenticationRecord()
		{
		}

		// Token: 0x060000AF RID: 175 RVA: 0x0000400C File Offset: 0x0000220C
		internal AuthenticationRecord(AuthenticationResult authResult, string clientId)
		{
			this.Username = authResult.Account.Username;
			this.Authority = authResult.Account.Environment;
			this.AccountId = authResult.Account.HomeAccountId;
			this.TenantId = authResult.TenantId;
			this.ClientId = clientId;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00004070 File Offset: 0x00002270
		internal AuthenticationRecord(string username, string authority, string homeAccountId, string tenantId, string clientId)
		{
			this.Username = username;
			this.Authority = authority;
			this.AccountId = AuthenticationRecord.BuildAccountIdFromString(homeAccountId);
			this.TenantId = tenantId;
			this.ClientId = clientId;
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x000040AD File Offset: 0x000022AD
		// (set) Token: 0x060000B2 RID: 178 RVA: 0x000040B5 File Offset: 0x000022B5
		public string Username { get; private set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x000040BE File Offset: 0x000022BE
		// (set) Token: 0x060000B4 RID: 180 RVA: 0x000040C6 File Offset: 0x000022C6
		public string Authority { get; private set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x000040CF File Offset: 0x000022CF
		public string HomeAccountId
		{
			get
			{
				return this.AccountId.Identifier;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x000040DC File Offset: 0x000022DC
		// (set) Token: 0x060000B7 RID: 183 RVA: 0x000040E4 File Offset: 0x000022E4
		public string TenantId { get; private set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x000040ED File Offset: 0x000022ED
		// (set) Token: 0x060000B9 RID: 185 RVA: 0x000040F5 File Offset: 0x000022F5
		public string ClientId { get; private set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000BA RID: 186 RVA: 0x000040FE File Offset: 0x000022FE
		// (set) Token: 0x060000BB RID: 187 RVA: 0x00004106 File Offset: 0x00002306
		internal AccountId AccountId { get; private set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000BC RID: 188 RVA: 0x0000410F File Offset: 0x0000230F
		// (set) Token: 0x060000BD RID: 189 RVA: 0x00004117 File Offset: 0x00002317
		internal string Version { get; private set; } = "1.0";

		// Token: 0x060000BE RID: 190 RVA: 0x00004120 File Offset: 0x00002320
		public void Serialize(Stream stream, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			this.SerializeAsync(stream, false, cancellationToken).EnsureCompleted();
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00004140 File Offset: 0x00002340
		public async Task SerializeAsync(Stream stream, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			await this.SerializeAsync(stream, true, cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00004193 File Offset: 0x00002393
		public static AuthenticationRecord Deserialize(Stream stream, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			return AuthenticationRecord.DeserializeAsync(stream, false, cancellationToken).EnsureCompleted<AuthenticationRecord>();
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000041B0 File Offset: 0x000023B0
		public static async Task<AuthenticationRecord> DeserializeAsync(Stream stream, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			return await AuthenticationRecord.DeserializeAsync(stream, true, cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000041FC File Offset: 0x000023FC
		private async Task SerializeAsync(Stream stream, bool async, CancellationToken cancellationToken)
		{
			using (Utf8JsonWriter json = new Utf8JsonWriter(stream, default(JsonWriterOptions)))
			{
				json.WriteStartObject();
				json.WriteString(AuthenticationRecord.s_usernamePropertyNameBytes, this.Username);
				json.WriteString(AuthenticationRecord.s_authorityPropertyNameBytes, this.Authority);
				json.WriteString(AuthenticationRecord.s_homeAccountIdPropertyNameBytes, this.HomeAccountId);
				json.WriteString(AuthenticationRecord.s_tenantIdPropertyNameBytes, this.TenantId);
				json.WriteString(AuthenticationRecord.s_clientIdPropertyNameBytes, this.ClientId);
				json.WriteString(AuthenticationRecord.s_versionPropertyNameBytes, this.Version);
				json.WriteEndObject();
				if (async)
				{
					await json.FlushAsync(cancellationToken).ConfigureAwait(false);
				}
				else
				{
					json.Flush();
				}
			}
			Utf8JsonWriter json = null;
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00004258 File Offset: 0x00002458
		private static async Task<AuthenticationRecord> DeserializeAsync(Stream stream, bool async, CancellationToken cancellationToken)
		{
			AuthenticationRecord authProfile = new AuthenticationRecord();
			JsonDocument jsonDocument;
			if (async)
			{
				jsonDocument = await JsonDocument.ParseAsync(stream, default(JsonDocumentOptions), cancellationToken).ConfigureAwait(false);
			}
			else
			{
				jsonDocument = JsonDocument.Parse(stream, default(JsonDocumentOptions));
			}
			AuthenticationRecord authenticationRecord;
			using (JsonDocument jsonDocument2 = jsonDocument)
			{
				foreach (JsonProperty jsonProperty in jsonDocument2.RootElement.EnumerateObject())
				{
					string name = jsonProperty.Name;
					if (!(name == "username"))
					{
						if (!(name == "authority"))
						{
							if (!(name == "homeAccountId"))
							{
								if (!(name == "tenantId"))
								{
									if (!(name == "clientId"))
									{
										if (name == "version")
										{
											authProfile.Version = jsonProperty.Value.GetString();
											if (authProfile.Version != "1.0")
											{
												throw new InvalidOperationException("Attempted to deserialize an AuthenticationRecord with a version that is not the current version. Expected: '1.0', Actual: '" + authProfile.Version + "'");
											}
										}
									}
									else
									{
										authProfile.ClientId = jsonProperty.Value.GetString();
									}
								}
								else
								{
									authProfile.TenantId = jsonProperty.Value.GetString();
								}
							}
							else
							{
								authProfile.AccountId = AuthenticationRecord.BuildAccountIdFromString(jsonProperty.Value.GetString());
							}
						}
						else
						{
							authProfile.Authority = jsonProperty.Value.GetString();
						}
					}
					else
					{
						authProfile.Username = jsonProperty.Value.GetString();
					}
				}
				authenticationRecord = authProfile;
			}
			return authenticationRecord;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x000042AC File Offset: 0x000024AC
		private static AccountId BuildAccountIdFromString(string homeAccountId)
		{
			string[] array = homeAccountId.Split(new char[] { '.' });
			AccountId accountId;
			if (array.Length == 2)
			{
				accountId = new AccountId(homeAccountId, array[0], array[1]);
			}
			else
			{
				accountId = new AccountId(homeAccountId);
			}
			return accountId;
		}

		// Token: 0x0400005B RID: 91
		internal const string CurrentVersion = "1.0";

		// Token: 0x0400005C RID: 92
		private const string UsernamePropertyName = "username";

		// Token: 0x0400005D RID: 93
		private const string AuthorityPropertyName = "authority";

		// Token: 0x0400005E RID: 94
		private const string HomeAccountIdPropertyName = "homeAccountId";

		// Token: 0x0400005F RID: 95
		private const string TenantIdPropertyName = "tenantId";

		// Token: 0x04000060 RID: 96
		private const string ClientIdPropertyName = "clientId";

		// Token: 0x04000061 RID: 97
		private const string VersionPropertyName = "version";

		// Token: 0x04000062 RID: 98
		private static readonly JsonEncodedText s_usernamePropertyNameBytes = JsonEncodedText.Encode("username", null);

		// Token: 0x04000063 RID: 99
		private static readonly JsonEncodedText s_authorityPropertyNameBytes = JsonEncodedText.Encode("authority", null);

		// Token: 0x04000064 RID: 100
		private static readonly JsonEncodedText s_homeAccountIdPropertyNameBytes = JsonEncodedText.Encode("homeAccountId", null);

		// Token: 0x04000065 RID: 101
		private static readonly JsonEncodedText s_tenantIdPropertyNameBytes = JsonEncodedText.Encode("tenantId", null);

		// Token: 0x04000066 RID: 102
		private static readonly JsonEncodedText s_clientIdPropertyNameBytes = JsonEncodedText.Encode("clientId", null);

		// Token: 0x04000067 RID: 103
		private static readonly JsonEncodedText s_versionPropertyNameBytes = JsonEncodedText.Encode("version", null);
	}
}
