using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x02000016 RID: 22
	public static class DataSourceProperties
	{
		// Token: 0x0600004F RID: 79 RVA: 0x00002558 File Offset: 0x00000758
		public static CredentialProperty[] GetConnectionProperties(ResourceKindInfo kindInfo)
		{
			List<CredentialProperty> list = new List<CredentialProperty>();
			if (kindInfo.SupportsEncryptedConnection)
			{
				list.Add(new CredentialProperty
				{
					Name = "EncryptConnection",
					PropertyType = typeof(bool)
				});
			}
			if (kindInfo.SupportsConnectionString)
			{
				list.Add(new CredentialProperty
				{
					Name = "ConnectionString",
					PropertyType = typeof(string),
					IsSecret = true
				});
			}
			if (kindInfo.AuthenticationInfo.Any((AuthenticationInfo info) => info.AuthenticationKind == AuthenticationKind.Exchange))
			{
				list.Add(new CredentialProperty
				{
					Name = "EmailAddress",
					PropertyType = typeof(string)
				});
				list.Add(new CredentialProperty
				{
					Name = "EwsUrl",
					PropertyType = typeof(string)
				});
				list.Add(new CredentialProperty
				{
					Name = "EwsSupportedSchema",
					PropertyType = typeof(string)
				});
			}
			foreach (string text in kindInfo.ConnectionStringProperties)
			{
				list.Add(new CredentialProperty
				{
					Name = text,
					PropertyType = typeof(string)
				});
			}
			return list.ToArray();
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000026D0 File Offset: 0x000008D0
		public static CredentialProperty[] GetAuthenticationProperties(AuthenticationInfo info, bool allowGeneric = false)
		{
			CredentialProperty[] array;
			switch (info.AuthenticationKind)
			{
			case AuthenticationKind.Implicit:
				array = DataSourceProperties.anonymousProperties.ReplaceLabels(info);
				goto IL_00D9;
			case AuthenticationKind.UsernamePassword:
				array = DataSourceProperties.usernamePasswordProperties.ReplaceLabels(info);
				goto IL_00D9;
			case AuthenticationKind.Windows:
				array = (((WindowsAuthenticationInfo)info).SupportsAlternateCredentials ? DataSourceProperties.windowsProperties : DataSourceProperties.windowsCurrentUserProperties).ReplaceLabels(info);
				goto IL_00D9;
			case AuthenticationKind.WebApi:
				array = DataSourceProperties.webApiProperties.ReplaceLabels(info);
				goto IL_00D9;
			case AuthenticationKind.OAuth2:
				array = ((info is AadAuthenticationInfo) ? DataSourceProperties.aadProperties.ReplaceLabels(info) : DataSourceProperties.oAuth2Properties.ReplaceLabels(info));
				goto IL_00D9;
			case AuthenticationKind.Exchange:
				array = DataSourceProperties.usernamePasswordProperties.ReplaceLabels(info);
				goto IL_00D9;
			case AuthenticationKind.Key:
				array = DataSourceProperties.keyProperties.ReplaceLabels(info);
				goto IL_00D9;
			case AuthenticationKind.Parameterized:
			{
				ParameterizedAuthenticationInfo parameterizedAuthenticationInfo = (ParameterizedAuthenticationInfo)info;
				array = new CredentialProperty[0];
				goto IL_00D9;
			}
			}
			return null;
			IL_00D9:
			if (allowGeneric && info.Properties != null && info.Properties.Count > 0)
			{
				CredentialProperty[] array2 = new CredentialProperty[array.Length + info.Properties.Count];
				for (int i = 0; i < array.Length; i++)
				{
					array2[i] = array[i];
				}
				for (int j = 0; j < info.Properties.Count; j++)
				{
					array2[j + array.Length] = info.Properties[j];
				}
				return array2;
			}
			return array;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x0000282A File Offset: 0x00000A2A
		public static string FromQueryPermission(QueryPermissionChallengeType queryPermissionChallengeType)
		{
			if (queryPermissionChallengeType == QueryPermissionChallengeType.EvaluateNativeQueryUnpermitted)
			{
				return "NativeQuery";
			}
			if (queryPermissionChallengeType != QueryPermissionChallengeType.EvaluateExchangeRedirectUnpermitted)
			{
				throw new NotImplementedException(queryPermissionChallengeType.ToString());
			}
			return "Redirect";
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002854 File Offset: 0x00000A54
		private static CredentialProperty[] ReplaceLabels(this ICollection<CredentialProperty> properties, AuthenticationInfo info)
		{
			CredentialProperty[] array = new CredentialProperty[properties.Count];
			int num = 0;
			foreach (CredentialProperty credentialProperty in properties)
			{
				array[num++] = credentialProperty.ReplaceLabel(info);
			}
			return array;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000028B4 File Offset: 0x00000AB4
		private static CredentialProperty ReplaceLabel(this CredentialProperty source, AuthenticationInfo info)
		{
			string propertyLabel = info.GetPropertyLabel(source.Name);
			if (propertyLabel == null)
			{
				return source;
			}
			return new CredentialProperty
			{
				Name = source.Name,
				Label = propertyLabel,
				IsRequired = source.IsRequired,
				IsSecret = source.IsSecret,
				PropertyType = source.PropertyType
			};
		}

		// Token: 0x04000067 RID: 103
		public const string NativeQuery = "NativeQuery";

		// Token: 0x04000068 RID: 104
		public const string Redirect = "Redirect";

		// Token: 0x04000069 RID: 105
		private static readonly ICollection<CredentialProperty> emptyCredentialProperties = new List<CredentialProperty>().AsReadOnly();

		// Token: 0x0400006A RID: 106
		private static readonly ICollection<string> emptyPermissionKinds = new List<string>().AsReadOnly();

		// Token: 0x0400006B RID: 107
		private static readonly ICollection<CredentialProperty> oAuth2Properties = Array.AsReadOnly<CredentialProperty>(new CredentialProperty[]
		{
			new CredentialProperty
			{
				Name = "AccessToken",
				PropertyType = typeof(string),
				IsRequired = true,
				IsSecret = true
			},
			new CredentialProperty
			{
				Name = "RefreshToken",
				PropertyType = typeof(string),
				IsRequired = false,
				IsSecret = true,
				AllowNull = true
			},
			new CredentialProperty
			{
				Name = "Expires",
				PropertyType = typeof(string),
				IsRequired = false,
				IsSecret = false,
				AllowNull = true
			},
			new CredentialProperty
			{
				Name = "Properties",
				PropertyType = typeof(IDictionary<string, string>)
			}
		});

		// Token: 0x0400006C RID: 108
		private static readonly ICollection<CredentialProperty> aadProperties = DataSourceProperties.oAuth2Properties;

		// Token: 0x0400006D RID: 109
		private static readonly ICollection<CredentialProperty> anonymousProperties = DataSourceProperties.emptyCredentialProperties;

		// Token: 0x0400006E RID: 110
		private static readonly ICollection<CredentialProperty> usernamePasswordProperties = Array.AsReadOnly<CredentialProperty>(new CredentialProperty[]
		{
			new CredentialProperty
			{
				Name = "Username",
				PropertyType = typeof(string),
				IsRequired = true
			},
			new CredentialProperty
			{
				Name = "Password",
				PropertyType = typeof(string),
				IsRequired = true,
				IsSecret = true
			}
		});

		// Token: 0x0400006F RID: 111
		private static readonly ICollection<CredentialProperty> windowsProperties = Array.AsReadOnly<CredentialProperty>(new CredentialProperty[]
		{
			new CredentialProperty
			{
				Name = "Username",
				PropertyType = typeof(string)
			},
			new CredentialProperty
			{
				Name = "Password",
				PropertyType = typeof(string),
				IsSecret = true
			},
			new CredentialProperty
			{
				Name = "IdentitySource",
				PropertyType = typeof(string)
			}
		});

		// Token: 0x04000070 RID: 112
		private static readonly ICollection<CredentialProperty> windowsCurrentUserProperties = DataSourceProperties.emptyCredentialProperties;

		// Token: 0x04000071 RID: 113
		private static readonly ICollection<CredentialProperty> keyProperties = Array.AsReadOnly<CredentialProperty>(new CredentialProperty[]
		{
			new CredentialProperty
			{
				Name = "Key",
				PropertyType = typeof(string),
				IsRequired = true,
				IsSecret = true
			}
		});

		// Token: 0x04000072 RID: 114
		private static readonly ICollection<CredentialProperty> webApiProperties = Array.AsReadOnly<CredentialProperty>(new CredentialProperty[]
		{
			new CredentialProperty
			{
				Name = "Key",
				PropertyType = typeof(string),
				IsRequired = true,
				IsSecret = true
			}
		});
	}
}
