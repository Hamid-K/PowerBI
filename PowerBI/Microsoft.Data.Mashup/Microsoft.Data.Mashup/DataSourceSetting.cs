using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Data.Mashup.ProviderCommon;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.EngineHost;
using Microsoft.Mashup.OAuth;
using Microsoft.Mashup.Storage;

namespace Microsoft.Data.Mashup
{
	// Token: 0x0200000D RID: 13
	public sealed class DataSourceSetting : IEquatable<DataSourceSetting>
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00003327 File Offset: 0x00001527
		// (set) Token: 0x06000055 RID: 85 RVA: 0x0000332F File Offset: 0x0000152F
		public string AuthenticationKind
		{
			get
			{
				return this.authenticationKind;
			}
			set
			{
				this.authenticationKind = value;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00003338 File Offset: 0x00001538
		// (set) Token: 0x06000057 RID: 87 RVA: 0x00003353 File Offset: 0x00001553
		public IDictionary<string, object> AuthenticationProperties
		{
			get
			{
				if (this.authenticationProperties == null)
				{
					this.authenticationProperties = new Dictionary<string, object>();
				}
				return this.authenticationProperties;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.authenticationProperties = new Dictionary<string, object>(value);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000058 RID: 88 RVA: 0x0000336F File Offset: 0x0000156F
		// (set) Token: 0x06000059 RID: 89 RVA: 0x00003377 File Offset: 0x00001577
		public bool? IsTrusted
		{
			get
			{
				return this.isTrusted;
			}
			set
			{
				this.isTrusted = value;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00003380 File Offset: 0x00001580
		// (set) Token: 0x0600005B RID: 91 RVA: 0x00003388 File Offset: 0x00001588
		public string PrivacySetting
		{
			get
			{
				return this.privacySetting;
			}
			set
			{
				this.privacySetting = value;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00003391 File Offset: 0x00001591
		// (set) Token: 0x0600005D RID: 93 RVA: 0x00003399 File Offset: 0x00001599
		public string PrivateGroupName
		{
			get
			{
				return this.privateGroupName;
			}
			set
			{
				this.privateGroupName = value;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600005E RID: 94 RVA: 0x000033A2 File Offset: 0x000015A2
		// (set) Token: 0x0600005F RID: 95 RVA: 0x000033BD File Offset: 0x000015BD
		public MashupPermissionSet Permissions
		{
			get
			{
				if (this.permissions == null)
				{
					this.permissions = new MashupPermissionSet();
				}
				return this.permissions;
			}
			internal set
			{
				this.permissions = value;
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x000033C6 File Offset: 0x000015C6
		public DataSourceSetting()
		{
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000033CE File Offset: 0x000015CE
		public DataSourceSetting(string authenticationKind)
		{
			this.authenticationKind = authenticationKind;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000033DD File Offset: 0x000015DD
		public DataSourceSetting(string authenticationKind, IDictionary<string, object> authenticationProperties)
			: this(authenticationKind)
		{
			if (authenticationProperties == null)
			{
				throw new ArgumentNullException("authenticationProperties");
			}
			this.authenticationProperties = new Dictionary<string, object>(authenticationProperties);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003400 File Offset: 0x00001600
		public DataSourceSetting(string authenticationKind, IDictionary<string, object> authenticationProperties, string privacySetting)
			: this(authenticationKind, authenticationProperties)
		{
			this.privacySetting = privacySetting;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003411 File Offset: 0x00001611
		public DataSourceSetting EncryptConnection(bool encryptConnection)
		{
			this.AuthenticationProperties["EncryptConnection"] = encryptConnection;
			return this;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x0000342A File Offset: 0x0000162A
		public DataSourceSetting AddConnectionString(string connectionString)
		{
			this.AuthenticationProperties.Add("ConnectionString", connectionString);
			return this;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x0000343E File Offset: 0x0000163E
		public DataSourceSetting SetPrivacySetting(string privacyGroup)
		{
			this.PrivacySetting = privacyGroup;
			return this;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003448 File Offset: 0x00001648
		public DataSourceSetting SetPrivateGroupName(string privateGroupName)
		{
			this.PrivateGroupName = privateGroupName;
			return this;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003452 File Offset: 0x00001652
		public DataSourceSetting AddNativeQuery(string query)
		{
			this.Permissions.Add(new MashupPermission("NativeQuery", query));
			return this;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x0000346B File Offset: 0x0000166B
		public DataSourceSetting AddNativeQuery(string query, int parameterCount)
		{
			this.Permissions.Add(new MashupPermission("NativeQuery", query, new Dictionary<string, object> { 
			{
				MashupPermission.Parameters,
				parameterCount
			} }));
			return this;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x0000349A File Offset: 0x0000169A
		public DataSourceSetting AddNativeQuery(string query, params string[] parameterNames)
		{
			this.Permissions.Add(new MashupPermission("NativeQuery", query, new Dictionary<string, object> { 
			{
				MashupPermission.Parameters,
				parameterNames
			} }));
			return this;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000034C4 File Offset: 0x000016C4
		public DataSourceSetting AddRedirect(string redirect)
		{
			this.Permissions.Add(new MashupPermission("Redirect", redirect));
			return this;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000034E0 File Offset: 0x000016E0
		public DataSourceSetting AddProperty(string propertyName, object propertyValue)
		{
			TypeCode typeCode = ((propertyValue == null) ? TypeCode.Empty : Type.GetTypeCode(propertyValue.GetType()));
			if (typeCode <= TypeCode.Boolean)
			{
				if (typeCode != TypeCode.Empty && typeCode != TypeCode.Boolean)
				{
					goto IL_0032;
				}
			}
			else if (typeCode != TypeCode.Int32 && typeCode != TypeCode.Double && typeCode != TypeCode.String)
			{
				goto IL_0032;
			}
			bool flag = true;
			goto IL_0034;
			IL_0032:
			flag = false;
			IL_0034:
			if (!flag)
			{
				throw new ArgumentException(ProviderErrorStrings.TypeNotSupported(propertyValue.GetType().FullName));
			}
			this.AuthenticationProperties.Add(propertyName, propertyValue);
			return this;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003548 File Offset: 0x00001748
		public DataSourceSetting AddAccessToken(string resource, string accessToken)
		{
			return this.AddProperty(TokenCredential.EncodeAccessTokenKey(resource), accessToken);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003557 File Offset: 0x00001757
		public static DataSourceSetting CreateAnonymousCredential()
		{
			return new DataSourceSetting
			{
				AuthenticationKind = "Anonymous"
			};
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003569 File Offset: 0x00001769
		public static DataSourceSetting CreateWindowsCredential()
		{
			return new DataSourceSetting
			{
				AuthenticationKind = "Windows"
			};
		}

		// Token: 0x06000070 RID: 112 RVA: 0x0000357C File Offset: 0x0000177C
		public static DataSourceSetting CreateWindowsCredential(string username, string password)
		{
			DataSourceSetting dataSourceSetting = DataSourceSetting.CreateWindowsCredential();
			if (username != null || password != null)
			{
				dataSourceSetting.AuthenticationProperties.Add("Username", username);
				dataSourceSetting.AuthenticationProperties.Add("Password", password);
			}
			return dataSourceSetting;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000035B8 File Offset: 0x000017B8
		public static DataSourceSetting CreateWindowsCredential(string identitySource, string username, string password)
		{
			DataSourceSetting dataSourceSetting = DataSourceSetting.CreateWindowsCredential();
			if (identitySource != null)
			{
				dataSourceSetting.AuthenticationProperties.Add("IdentitySource", identitySource);
			}
			if (username != null)
			{
				dataSourceSetting.AuthenticationProperties.Add("Username", username);
			}
			if (password != null)
			{
				dataSourceSetting.AuthenticationProperties.Add("Password", password);
			}
			return dataSourceSetting;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003608 File Offset: 0x00001808
		public static DataSourceSetting CreateUsernamePasswordCredential(string username, string password)
		{
			return new DataSourceSetting
			{
				AuthenticationKind = "UsernamePassword",
				AuthenticationProperties = 
				{
					{ "Username", username },
					{ "Password", password }
				}
			};
		}

		// Token: 0x06000073 RID: 115 RVA: 0x0000363C File Offset: 0x0000183C
		public static DataSourceSetting CreateKeyCredential(string key)
		{
			return new DataSourceSetting
			{
				AuthenticationKind = "Key",
				AuthenticationProperties = { { "Key", key } }
			};
		}

		// Token: 0x06000074 RID: 116 RVA: 0x0000365F File Offset: 0x0000185F
		public static DataSourceSetting CreateWebApiCredential(string key)
		{
			return new DataSourceSetting
			{
				AuthenticationKind = "WebApi",
				AuthenticationProperties = { { "Key", key } }
			};
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003682 File Offset: 0x00001882
		public static DataSourceSetting CreateOAuth2Credential(string accessToken)
		{
			return new DataSourceSetting
			{
				AuthenticationKind = "OAuth2",
				AuthenticationProperties = { { "AccessToken", accessToken } }
			};
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000036A8 File Offset: 0x000018A8
		public static DataSourceSetting CreateParameterizedCredential(string credentialName, params KeyValuePair<string, string>[] parameters)
		{
			DataSourceSetting dataSourceSetting = new DataSourceSetting
			{
				AuthenticationKind = credentialName
			};
			foreach (KeyValuePair<string, string> keyValuePair in parameters)
			{
				dataSourceSetting.AuthenticationProperties.Add(keyValuePair.Key, keyValuePair.Value);
			}
			return dataSourceSetting;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000036F4 File Offset: 0x000018F4
		public static DataSourceSetting CreateMacSandboxFileAccessCredential(string filePath)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000036FC File Offset: 0x000018FC
		public bool Equals(DataSourceSetting other)
		{
			if (other != null && this.AuthenticationKind == other.AuthenticationKind && this.PrivacySetting == other.PrivacySetting)
			{
				bool? flag = this.IsTrusted;
				bool? flag2 = other.IsTrusted;
				if (((flag.GetValueOrDefault() == flag2.GetValueOrDefault()) & (flag != null == (flag2 != null))) && Util.AreEqual<string, object>(this.AuthenticationProperties, other.AuthenticationProperties) && this.PrivateGroupName == other.PrivateGroupName)
				{
					return (this.permissions == null && other.permissions == null) || (this.permissions != null && other.permissions != null && this.Permissions.SetEquals(other.Permissions));
				}
			}
			return false;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000037C9 File Offset: 0x000019C9
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DataSourceSetting);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000037D8 File Offset: 0x000019D8
		public override int GetHashCode()
		{
			int num = ((this.AuthenticationKind != null) ? this.AuthenticationKind.GetHashCode() : 0);
			int num2 = ((this.PrivacySetting != null) ? this.PrivacySetting.GetHashCode() : 0);
			int num3 = ((this.PrivateGroupName != null) ? this.PrivateGroupName.GetHashCode() : 0);
			int hashCode = this.IsTrusted.GetHashCode();
			return this.AuthenticationProperties.Select((KeyValuePair<string, object> pair) => pair.Key.GetHashCode() ^ ((pair.Value != null) ? pair.Value.GetHashCode() : 0)).Aggregate(num ^ num2 ^ num3 ^ hashCode, (int next, int r) => next ^ r);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003895 File Offset: 0x00001A95
		internal bool HasPermissions()
		{
			return this.permissions != null && this.permissions.Count > 0;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x000038AF File Offset: 0x00001AAF
		internal static DataSourceSetting CreateOAuth2Credential(string accessToken, IDictionary<string, string> properties)
		{
			return DataSourceSetting.CreateRefreshableOAuth2Credential(accessToken, null, null, properties);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000038BC File Offset: 0x00001ABC
		public static DataSourceSetting CreateRefreshableOAuth2Credential(string accessToken, string expires, string refreshToken, IDictionary<string, string> properties)
		{
			DataSourceSetting dataSourceSetting = DataSourceSetting.CreateOAuth2Credential(accessToken);
			dataSourceSetting.AuthenticationProperties["Expires"] = expires;
			dataSourceSetting.AuthenticationProperties["RefreshToken"] = refreshToken;
			foreach (KeyValuePair<string, string> keyValuePair in properties)
			{
				dataSourceSetting.AuthenticationProperties[keyValuePair.Key] = keyValuePair.Value;
			}
			return dataSourceSetting;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003940 File Offset: 0x00001B40
		internal static void ValidateSupported(DataSource dataSource, DataSourceSetting dataSourceSetting)
		{
			DataSourceKindInfo kindInfo = DataSourceKindInfo.FromKind(dataSource.Kind, DataSourceKindInfoOptions.ReturnCustom);
			AuthenticationInfo[] array = kindInfo.AuthenticationInfos.Where((AuthenticationInfo ai) => ai.Kind == dataSourceSetting.AuthenticationKind).ToArray<AuthenticationInfo>();
			if (array.Length == 0)
			{
				throw new MashupException(ProviderErrorStrings.UnsupportedDataSourceKindCredentialType(dataSource.Kind, dataSourceSetting.AuthenticationKind));
			}
			DataSourceSetting.CheckProperties(dataSource, kindInfo.Properties.Concat(kindInfo.ApplicationProperties), dataSourceSetting, true);
			if (array.Length == 1)
			{
				DataSourceSetting.CheckProperties(dataSource, array.First<AuthenticationInfo>().Properties.Concat(array.First<AuthenticationInfo>().ApplicationProperties), dataSourceSetting, true);
			}
			else if (!array.Any((AuthenticationInfo authenticationInfo) => DataSourceSetting.CheckProperties(dataSource, authenticationInfo.Properties.Concat(authenticationInfo.ApplicationProperties), dataSourceSetting, false)))
			{
				throw new MashupException(ProviderErrorStrings.UnsupportedDataSourceKindCredential(dataSource.Kind, dataSourceSetting.AuthenticationKind));
			}
			if (dataSourceSetting.HasPermissions())
			{
				string text = dataSourceSetting.Permissions.Select((MashupPermission p) => p.Kind).FirstOrDefault((string p) => !kindInfo.PermissionKinds.Contains(p));
				if (text != null)
				{
					throw new MashupException(ProviderErrorStrings.UnsupportedDataSourceKindPermissionKind(dataSource.Kind, text));
				}
				foreach (MashupPermission mashupPermission in dataSourceSetting.Permissions)
				{
					DataSourceSetting.ValidatePermission(dataSource, mashupPermission);
				}
			}
			if (dataSource.Kind == "Exchange")
			{
				object obj;
				dataSourceSetting.authenticationProperties.TryGetValue("EwsUrl", out obj);
				object obj2;
				dataSourceSetting.authenticationProperties.TryGetValue("EwsSupportedSchema", out obj2);
				if ((obj != null && obj2 == null) || (obj == null && obj2 != null))
				{
					throw new MashupException(ProviderErrorStrings.UnsupportedCredentialPropertiesCombination("EwsUrl", "EwsSupportedSchema"));
				}
			}
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003B7C File Offset: 0x00001D7C
		private static bool CheckProperties(DataSource dataSource, IEnumerable<CredentialProperty> credentialProperties, DataSourceSetting dataSourceSetting, bool throwOnNotMaching)
		{
			string kind = dataSource.Kind;
			foreach (CredentialProperty credentialProperty in credentialProperties)
			{
				object obj;
				if (!dataSourceSetting.AuthenticationProperties.TryGetValue(credentialProperty.Name, out obj))
				{
					if (credentialProperty.IsRequired)
					{
						if (throwOnNotMaching)
						{
							throw new MashupException(ProviderErrorStrings.CredentialMissingProperty(kind, dataSourceSetting.AuthenticationKind, credentialProperty.Name));
						}
						return false;
					}
				}
				else
				{
					if (obj == null && !credentialProperty.AllowNull)
					{
						throw new MashupException(ProviderErrorStrings.CredentialPropertyNullValue(kind, credentialProperty.Name));
					}
					if (obj != null && obj.GetType() != credentialProperty.PropertyType)
					{
						if (throwOnNotMaching)
						{
							throw new MashupException(ProviderErrorStrings.CredentialPropertyType(kind, dataSourceSetting.AuthenticationKind, credentialProperty.Name, obj.GetType().FullName, credentialProperty.PropertyType.FullName));
						}
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003C7C File Offset: 0x00001E7C
		private static void ValidatePermission(DataSource dataSource, MashupPermission permission)
		{
			string kind = permission.Kind;
			if (!(kind == "NativeQuery"))
			{
				if (!(kind == "Redirect"))
				{
					return;
				}
			}
			else
			{
				using (IEnumerator<string> enumerator = permission.Properties.Keys.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string text = enumerator.Current;
						if (!(text == MashupPermission.Parameters))
						{
							throw new MashupPermissionException(ProviderErrorStrings.UnknownPermissionProperty(text, "NativeQuery"), dataSource, permission);
						}
						object obj = permission.Properties[text];
						if (obj == null)
						{
							throw new MashupPermissionException(ProviderErrorStrings.ParameterSignatureCountOrNames, dataSource, permission);
						}
						if (!(obj is int))
						{
							if (obj is IList)
							{
								using (IEnumerator enumerator2 = ((IList)obj).GetEnumerator())
								{
									while (enumerator2.MoveNext())
									{
										if (!(enumerator2.Current is string))
										{
											throw new MashupPermissionException(ProviderErrorStrings.ParameterSignatureCountOrNames, dataSource, permission);
										}
									}
									continue;
								}
							}
							throw new MashupPermissionException(ProviderErrorStrings.ParameterSignatureCountOrNames, dataSource, permission);
						}
					}
					return;
				}
			}
			if (permission.Properties.Count > 0)
			{
				throw new MashupPermissionException(ProviderErrorStrings.UnknownPermissionProperty(permission.Properties.Keys.First<string>(), "Redirect"), dataSource, permission);
			}
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003DD8 File Offset: 0x00001FD8
		internal static DataSourceSetting FromCredential(Credential credential, out DataSource dataSource)
		{
			DataSourceSetting dataSourceSetting = DataSourceSetting.FromCredentialWithoutValidation(credential, out dataSource);
			DataSourceSetting.ValidateSupported(dataSource, dataSourceSetting);
			return dataSourceSetting;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003DF8 File Offset: 0x00001FF8
		internal static DataSourceSetting FromCredentialWithoutValidation(Credential credential, out DataSource dataSource)
		{
			dataSource = new DataSource(credential.Resource.Kind, credential.Resource.Path);
			CredentialDataCollection credentialDataCollection;
			if (!CredentialDataSerializer.TryDeserialize(credential.CredentialData, out credentialDataCollection))
			{
				throw new MashupException(ProviderErrorStrings.CredentialNotValid(credential.Resource));
			}
			List<CredentialData> credentials = credentialDataCollection.Credentials;
			DataSourceSetting dataSourceSetting = DataSourceSetting.FromCredentialWithoutAdornment(credentials.Where((CredentialData data) => data.Kind != CredentialDataKind.Adornment));
			if (MashupEngines.Version1.SupportsEncryptedConnection(credential.Resource.Kind))
			{
				EncryptionAdornmentCredentialData encryptionAdornmentCredentialData = credentials.OfType<EncryptionAdornmentCredentialData>().SingleOrDefault<EncryptionAdornmentCredentialData>();
				if (encryptionAdornmentCredentialData != null)
				{
					dataSourceSetting.AuthenticationProperties["EncryptConnection"] = encryptionAdornmentCredentialData.RequireEncryption;
				}
			}
			if (MashupEngines.Version1.SupportsConnectionString(credential.Resource.Kind))
			{
				ConnectionStringAdornmentCredentialData connectionStringAdornmentCredentialData = credentials.OfType<ConnectionStringAdornmentCredentialData>().SingleOrDefault<ConnectionStringAdornmentCredentialData>();
				if (connectionStringAdornmentCredentialData != null)
				{
					dataSourceSetting.AuthenticationProperties["ConnectionString"] = connectionStringAdornmentCredentialData.ConnectionString;
				}
			}
			if (MashupEngines.Version1.SupportsConnectionStringProperty(credential.Resource.Kind, null))
			{
				ConnectionStringPropertiesAdornmentCredentialData connectionStringPropertiesAdornmentCredentialData = credentials.OfType<ConnectionStringPropertiesAdornmentCredentialData>().SingleOrDefault<ConnectionStringPropertiesAdornmentCredentialData>();
				if (connectionStringPropertiesAdornmentCredentialData != null)
				{
					foreach (KeyValuePair<string, string> keyValuePair in connectionStringPropertiesAdornmentCredentialData.Properties.Where((KeyValuePair<string, string> kvp) => CredentialProperty.KnownCredentialProperties.Contains(kvp.Key)))
					{
						dataSourceSetting.AuthenticationProperties[keyValuePair.Key] = keyValuePair.Value;
					}
				}
			}
			new Dictionary<string, object>();
			ApplicationCredentialPropertiesAdornmentCredentialData applicationCredentialPropertiesAdornmentCredentialData = credentials.OfType<ApplicationCredentialPropertiesAdornmentCredentialData>().SingleOrDefault<ApplicationCredentialPropertiesAdornmentCredentialData>();
			if (applicationCredentialPropertiesAdornmentCredentialData != null)
			{
				foreach (KeyValuePair<string, object> keyValuePair2 in applicationCredentialPropertiesAdornmentCredentialData.Properties)
				{
					dataSourceSetting.AuthenticationProperties[keyValuePair2.Key] = keyValuePair2.Value;
				}
			}
			return dataSourceSetting;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x0000400C File Offset: 0x0000220C
		internal ResourceCredentialCollection CreateCredentialCollection(IResource resource, DataSource dataSource)
		{
			return this.CreateDataCollection(resource, dataSource).ToResourceCredentials(resource, null, true);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00004020 File Offset: 0x00002220
		private static DataSourceSetting FromCredentialWithoutAdornment(IEnumerable<CredentialData> credentialData)
		{
			if (!credentialData.Any<CredentialData>())
			{
				return DataSourceSetting.CreateAnonymousCredential();
			}
			WindowsAuthCredentialData windowsAuthCredentialData = credentialData.OfType<WindowsAuthCredentialData>().SingleOrDefault<WindowsAuthCredentialData>();
			if (windowsAuthCredentialData != null)
			{
				return DataSourceSetting.CreateWindowsCredential(windowsAuthCredentialData.IdentitySource, windowsAuthCredentialData.Username, windowsAuthCredentialData.Password);
			}
			SqlAuthCredentialData sqlAuthCredentialData = credentialData.OfType<SqlAuthCredentialData>().SingleOrDefault<SqlAuthCredentialData>();
			if (sqlAuthCredentialData != null)
			{
				return DataSourceSetting.CreateUsernamePasswordCredential(sqlAuthCredentialData.Username, sqlAuthCredentialData.Password);
			}
			BasicAuthCredentialData basicAuthCredentialData = credentialData.OfType<BasicAuthCredentialData>().SingleOrDefault<BasicAuthCredentialData>();
			if (basicAuthCredentialData != null)
			{
				return DataSourceSetting.CreateUsernamePasswordCredential(basicAuthCredentialData.Username, basicAuthCredentialData.Password);
			}
			FeedKeyCredentialData feedKeyCredentialData = credentialData.OfType<FeedKeyCredentialData>().SingleOrDefault<FeedKeyCredentialData>();
			if (feedKeyCredentialData != null)
			{
				return DataSourceSetting.CreateKeyCredential(feedKeyCredentialData.Key);
			}
			OAuthCredentialData oauthCredentialData = credentialData.OfType<OAuthCredentialData>().SingleOrDefault<OAuthCredentialData>();
			if (oauthCredentialData != null)
			{
				return DataSourceSetting.CreateOAuth2Credential(oauthCredentialData.AccessToken, oauthCredentialData.Properties);
			}
			WebApiKeyCredentialData webApiKeyCredentialData = credentialData.OfType<WebApiKeyCredentialData>().SingleOrDefault<WebApiKeyCredentialData>();
			if (webApiKeyCredentialData != null)
			{
				return DataSourceSetting.CreateWebApiCredential(webApiKeyCredentialData.ApiKeyValue);
			}
			throw new NotSupportedException(ProviderErrorStrings.UnsupportedCredentialType(credentialData.GetType().GetGenericArguments().FirstOrDefault<Type>()));
		}

		// Token: 0x06000085 RID: 133 RVA: 0x0000411C File Offset: 0x0000231C
		private CredentialDataCollection CreateDataCollection(IResource resource, DataSource dataSource)
		{
			CredentialDataCollection credentialDataCollection = new CredentialDataCollection();
			Dictionary<string, object> dictionary = new Dictionary<string, object>(this.AuthenticationProperties);
			DataSourceKindInfo dataSourceKindInfo = DataSourceKindInfo.FromKind(dataSource.Kind, DataSourceKindInfoOptions.ReturnCustom);
			AuthenticationInfo authenticationInfo = dataSourceKindInfo.AuthenticationInfos.FirstOrDefault((AuthenticationInfo a) => a.Kind == this.AuthenticationKind) ?? dataSourceKindInfo.AuthenticationInfos.First<AuthenticationInfo>();
			string text = this.AuthenticationKind;
			if (text != null)
			{
				int length = text.Length;
				switch (length)
				{
				case 3:
					if (text == "Key")
					{
						object obj;
						DataSourceSetting.TryGetProperty(dictionary, "Key", out obj);
						credentialDataCollection.Credentials.Add((resource.Kind == "AzureTables") ? new SharedKeyAuthCredentialData((string)obj) : new FeedKeyCredentialData((string)obj));
						goto IL_044B;
					}
					break;
				case 4:
				case 5:
				case 8:
					break;
				case 6:
				{
					char c = text[0];
					if (c != 'O')
					{
						if (c == 'W')
						{
							if (text == "WebApi")
							{
								object obj;
								DataSourceSetting.TryGetProperty(dictionary, "Key", out obj);
								credentialDataCollection.Credentials.Add(new WebApiKeyCredentialData((string)obj));
								goto IL_044B;
							}
						}
					}
					else if (text == "OAuth2")
					{
						TokenCredential tokenCredential = this.AsTokenCredential();
						credentialDataCollection.Credentials.Add(new OAuthCredentialData(tokenCredential.AccessToken, tokenCredential.Expires, tokenCredential.RefreshToken, tokenCredential.Properties));
						dictionary.Remove("AccessToken");
						dictionary.Remove("RefreshToken");
						dictionary.Remove("Expires");
						goto IL_044B;
					}
					break;
				}
				case 7:
					if (text == "Windows")
					{
						if (resource.Kind == "Exchange")
						{
							object obj2;
							object obj3;
							object obj4;
							DataSourceSetting.GetExchangeProperties(dictionary, out obj2, out obj3, out obj4);
							credentialDataCollection.Credentials.Add(new ExchangeCredentialData((string)obj2, null, null, (string)obj3, (string)obj4));
							goto IL_044B;
						}
						object obj5;
						DataSourceSetting.TryGetProperty(dictionary, "IdentitySource", out obj5);
						object obj6;
						DataSourceSetting.TryGetProperty(dictionary, "Username", out obj6);
						object obj7;
						DataSourceSetting.TryGetProperty(dictionary, "Password", out obj7);
						WindowsAuthCredentialData windowsAuthCredentialData = new WindowsAuthCredentialData((string)obj5, (string)obj6, (string)obj7);
						try
						{
							windowsAuthCredentialData.Validate();
						}
						catch (CredentialValidationException ex)
						{
							throw new MashupException(ex.Message);
						}
						credentialDataCollection.Credentials.Add(windowsAuthCredentialData);
						goto IL_044B;
					}
					break;
				case 9:
					if (text == "Anonymous")
					{
						goto IL_044B;
					}
					break;
				default:
					if (length != 16)
					{
						if (length == 20)
						{
							if (text == "MacSandboxFileAccess")
							{
								throw new PlatformNotSupportedException();
							}
						}
					}
					else if (text == "UsernamePassword")
					{
						object obj8;
						DataSourceSetting.TryGetProperty(dictionary, "Username", out obj8);
						object obj9;
						DataSourceSetting.TryGetProperty(dictionary, "Password", out obj9);
						if (resource.Kind == "Exchange")
						{
							object obj10;
							object obj11;
							object obj12;
							DataSourceSetting.GetExchangeProperties(dictionary, out obj10, out obj11, out obj12);
							credentialDataCollection.Credentials.Add(new ExchangeCredentialData((string)obj10, (string)obj8, (string)obj9, (string)obj11, (string)obj12));
							goto IL_044B;
						}
						if (MashupEngines.Version1.IsDatabaseKind(resource.Kind) && resource.Kind != "AnalysisServices")
						{
							credentialDataCollection.Credentials.Add(new SqlAuthCredentialData((string)obj8, (string)obj9));
							goto IL_044B;
						}
						credentialDataCollection.Credentials.Add(new BasicAuthCredentialData((string)obj8, (string)obj9));
						goto IL_044B;
					}
					break;
				}
			}
			SerializableDictionary<string, string> serializableDictionary = new SerializableDictionary<string, string>();
			foreach (CredentialProperty credentialProperty in authenticationInfo.Properties.Concat(authenticationInfo.ApplicationProperties ?? EmptyArray<CredentialProperty>.Instance))
			{
				object obj13;
				if (dictionary.TryGetValue(credentialProperty.Name, out obj13))
				{
					serializableDictionary[credentialProperty.Name] = (string)obj13;
					dictionary.Remove(credentialProperty.Name);
				}
			}
			credentialDataCollection.Credentials.Add(new ParameterizedAuthCredentialData(this.AuthenticationKind, serializableDictionary));
			IL_044B:
			object obj14;
			if (DataSourceSetting.TryGetProperty(dictionary, "EncryptConnection", out obj14))
			{
				if (!MashupEngines.Version1.SupportsEncryptedConnection(resource.Kind))
				{
					throw new MashupException(ProviderErrorStrings.EncryptConnectionNotSupported(resource.Kind));
				}
				credentialDataCollection.Credentials.Add(new EncryptionAdornmentCredentialData((bool)obj14));
			}
			object obj15;
			if (DataSourceSetting.TryGetProperty(dictionary, "ConnectionString", out obj15) && MashupEngines.Version1.SupportsConnectionString(resource.Kind))
			{
				credentialDataCollection.Credentials.Add(new ConnectionStringAdornmentCredentialData((string)obj15));
			}
			IDictionary<string, string> dictionary2 = new Dictionary<string, string>();
			foreach (CredentialProperty credentialProperty2 in dataSourceKindInfo.Properties.Concat(authenticationInfo.Properties ?? EmptyArray<CredentialProperty>.Instance))
			{
				object obj16;
				if (DataSourceSetting.TryGetProperty(dictionary, credentialProperty2.Name, out obj16))
				{
					dictionary2[credentialProperty2.Name] = (string)obj16;
				}
			}
			if (dictionary2.Count > 0)
			{
				credentialDataCollection.Credentials.Add(new ConnectionStringPropertiesAdornmentCredentialData(dictionary2));
			}
			Dictionary<string, object> dictionary3 = new Dictionary<string, object>();
			foreach (CredentialProperty credentialProperty3 in dataSourceKindInfo.ApplicationProperties.Concat(authenticationInfo.ApplicationProperties ?? EmptyArray<CredentialProperty>.Instance))
			{
				object obj17;
				if (DataSourceSetting.TryGetProperty(dictionary, credentialProperty3.Name, out obj17))
				{
					dictionary3.Add(credentialProperty3.Name, obj17);
				}
			}
			if (dictionary3.Count > 0)
			{
				credentialDataCollection.Credentials.Add(new ApplicationCredentialPropertiesAdornmentCredentialData(dictionary3));
			}
			return credentialDataCollection;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00004740 File Offset: 0x00002940
		internal TokenCredential AsTokenCredential()
		{
			if (this.AuthenticationKind != "OAuth2")
			{
				return null;
			}
			object obj;
			this.AuthenticationProperties.TryGetValue("AccessToken", out obj);
			object obj2;
			this.AuthenticationProperties.TryGetValue("RefreshToken", out obj2);
			object obj3;
			this.AuthenticationProperties.TryGetValue("Expires", out obj3);
			Dictionary<string, string> dictionary = null;
			foreach (KeyValuePair<string, object> keyValuePair in this.AuthenticationProperties)
			{
				if (!(keyValuePair.Key == "AccessToken") && !(keyValuePair.Key == "RefreshToken") && !(keyValuePair.Key == "Expires"))
				{
					if (dictionary == null)
					{
						dictionary = new Dictionary<string, string>();
					}
					dictionary[keyValuePair.Key] = Convert.ToString(keyValuePair.Value, CultureInfo.InvariantCulture);
				}
			}
			return new TokenCredential(obj as string, obj3 as string, obj2 as string, dictionary);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00004854 File Offset: 0x00002A54
		internal Credential MakeCredential(Resource resource, DataSource dataSource)
		{
			CredentialDataCollection credentialDataCollection = this.CreateDataCollection(resource, dataSource);
			byte[] array;
			try
			{
				array = CredentialDataSerializer.Serialize(credentialDataCollection);
			}
			catch (InvalidOperationException ex)
			{
				throw new MashupException(ProviderErrorStrings.CredentialInvalidString, ex);
			}
			return new Credential(resource, array);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00004898 File Offset: 0x00002A98
		private static void GetExchangeProperties(Dictionary<string, object> properties, out object emailAddress, out object ewsUrl, out object ewsSupportedSchema)
		{
			DataSourceSetting.TryGetProperty(properties, "EmailAddress", out emailAddress);
			DataSourceSetting.TryGetProperty(properties, "EwsUrl", out ewsUrl);
			DataSourceSetting.TryGetProperty(properties, "EwsSupportedSchema", out ewsSupportedSchema);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x000048C1 File Offset: 0x00002AC1
		private static bool TryGetProperty(Dictionary<string, object> properties, string propertyName, out object propertyValue)
		{
			bool flag = properties.TryGetValue(propertyName, out propertyValue);
			if (flag)
			{
				properties.Remove(propertyName);
			}
			return flag;
		}

		// Token: 0x04000044 RID: 68
		private string authenticationKind;

		// Token: 0x04000045 RID: 69
		private IDictionary<string, object> authenticationProperties;

		// Token: 0x04000046 RID: 70
		private string privacySetting;

		// Token: 0x04000047 RID: 71
		private string privateGroupName;

		// Token: 0x04000048 RID: 72
		private bool? isTrusted;

		// Token: 0x04000049 RID: 73
		private MashupPermissionSet permissions;
	}
}
