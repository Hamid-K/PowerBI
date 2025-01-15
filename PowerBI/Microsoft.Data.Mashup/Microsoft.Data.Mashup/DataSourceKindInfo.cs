using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Mashup.ProviderCommon;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.EngineHost;

namespace Microsoft.Data.Mashup
{
	// Token: 0x0200000B RID: 11
	public sealed class DataSourceKindInfo
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00002F54 File Offset: 0x00001154
		// (set) Token: 0x06000048 RID: 72 RVA: 0x00002F5B File Offset: 0x0000115B
		internal static bool AllowAllDataSources { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00002F63 File Offset: 0x00001163
		public ICollection<CredentialProperty> Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00002F6B File Offset: 0x0000116B
		public ICollection<CredentialProperty> ApplicationProperties
		{
			get
			{
				return this.applicationProperties;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00002F73 File Offset: 0x00001173
		public ICollection<AuthenticationInfo> AuthenticationInfos
		{
			get
			{
				return this.authenticationInfos;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00002F7B File Offset: 0x0000117B
		public ICollection<string> PermissionKinds
		{
			get
			{
				return this.permissionKinds;
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002F83 File Offset: 0x00001183
		internal DataSourceKindInfo(ICollection<CredentialProperty> properties, ICollection<CredentialProperty> applicationProperties, ICollection<AuthenticationInfo> authenticationInfos, ICollection<string> permissionKinds)
		{
			this.properties = properties;
			this.authenticationInfos = authenticationInfos;
			this.permissionKinds = permissionKinds;
			this.applicationProperties = applicationProperties;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002FA8 File Offset: 0x000011A8
		internal static DataSourceKindInfo From(ResourceKindInfo kindInfo, bool exposeAad, bool exposeCustom)
		{
			List<CredentialProperty> list = DataSourceProperties.GetConnectionProperties(kindInfo).Select(new Func<CredentialProperty, CredentialProperty>(DataSourceKindInfo.CreateProperty)).ToList<CredentialProperty>();
			List<CredentialProperty> list2 = kindInfo.ApplicationProperties.Select(new Func<CredentialProperty, CredentialProperty>(DataSourceKindInfo.CreateProperty)).ToList<CredentialProperty>();
			List<AuthenticationInfo> list3 = new List<AuthenticationInfo>(kindInfo.AuthenticationInfo.Count);
			foreach (AuthenticationInfo authenticationInfo in kindInfo.AuthenticationInfo)
			{
				string text;
				if (!DataSourceKindInfo.authenticationKindMap.TryGetValue(authenticationInfo.AuthenticationKind, out text))
				{
					if (authenticationInfo.AuthenticationKind == AuthenticationKind.SapBasic || !exposeCustom)
					{
						continue;
					}
					text = ((ParameterizedAuthenticationInfo)authenticationInfo).Name;
				}
				CredentialProperty[] authenticationProperties = DataSourceProperties.GetAuthenticationProperties(authenticationInfo, exposeCustom);
				AuthenticationKind authenticationKind = authenticationInfo.AuthenticationKind;
				if (authenticationKind != AuthenticationKind.OAuth2)
				{
					if (authenticationKind == AuthenticationKind.Exchange)
					{
						list3.Add(DataSourceKindInfo.windowsCurrentUserAuthenticationInfo.SetLabelsAndProperties(authenticationInfo));
					}
				}
				else if (exposeAad && authenticationInfo is AadAuthenticationInfo)
				{
					text = "AAD";
				}
				IList<CredentialProperty> list4 = authenticationInfo.ApplicationProperties ?? EmptyArray<CredentialProperty>.Instance;
				list3.Add(new AuthenticationInfo(text, authenticationProperties.Select(new Func<CredentialProperty, CredentialProperty>(DataSourceKindInfo.CreateProperty)).ToList<CredentialProperty>().AsReadOnly(), list4.Select(new Func<CredentialProperty, CredentialProperty>(DataSourceKindInfo.CreateProperty)).ToList<CredentialProperty>().AsReadOnly(), authenticationInfo.Label));
			}
			return new DataSourceKindInfo(list.AsReadOnly(), list2.AsReadOnly(), list3.AsReadOnly(), kindInfo.PermissionKinds.Select((QueryPermissionChallengeType p) => MashupPermissionKind.From(p)).ToList<string>().AsReadOnly());
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003168 File Offset: 0x00001368
		public static DataSourceKindInfo FromKind(string dataSourceKind)
		{
			return DataSourceKindInfo.FromKind(dataSourceKind, DataSourceKindInfoOptions.None);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00003174 File Offset: 0x00001374
		public static DataSourceKindInfo FromKind(string dataSourceKind, DataSourceKindInfoOptions options)
		{
			if (string.IsNullOrEmpty(dataSourceKind))
			{
				throw new ArgumentNullException("dataSourceKind");
			}
			if (options != (options & DataSourceKindInfoOptions.All))
			{
				throw new ArgumentException(ProviderErrorStrings.InvalidOption, "options");
			}
			if (!DataSourceKindInfo.AllowAllDataSources && DataSourceKindInfo.SupportedForDiscoveryOnly(dataSourceKind))
			{
				return DataSourceKindInfo.None;
			}
			ResourceKindInfo resourceKindInfo;
			if (!MashupEngines.Version1.TryLookupResourceKind(dataSourceKind, out resourceKindInfo))
			{
				throw new NotSupportedException(ProviderErrorStrings.UnsupportedDataSourceKind(dataSourceKind));
			}
			return DataSourceKindInfo.From(resourceKindInfo, (options & DataSourceKindInfoOptions.ReturnAad) > DataSourceKindInfoOptions.None, (options & DataSourceKindInfoOptions.ReturnCustom) > DataSourceKindInfoOptions.None);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000031EC File Offset: 0x000013EC
		private static bool SupportedForDiscoveryOnly(string dataSourceKind)
		{
			return dataSourceKind == "CurrentWorkbook" && MashupConnection.AllowCurrentWorkbook;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003204 File Offset: 0x00001404
		private static CredentialProperty CreateProperty(CredentialProperty property)
		{
			return new CredentialProperty
			{
				Name = property.Name,
				Label = property.Label,
				IsRequired = property.IsRequired,
				IsSecret = property.IsSecret,
				PropertyType = property.PropertyType,
				AllowNull = property.AllowNull
			};
		}

		// Token: 0x04000036 RID: 54
		private static readonly ICollection<CredentialProperty> emptyCredentialProperties = new List<CredentialProperty>().AsReadOnly();

		// Token: 0x04000037 RID: 55
		private static readonly ICollection<string> emptyPermissionKinds = new List<string>().AsReadOnly();

		// Token: 0x04000038 RID: 56
		private static readonly Dictionary<AuthenticationKind, string> authenticationKindMap = new Dictionary<AuthenticationKind, string>
		{
			{
				AuthenticationKind.Implicit,
				"Anonymous"
			},
			{
				AuthenticationKind.Key,
				"Key"
			},
			{
				AuthenticationKind.OAuth2,
				"OAuth2"
			},
			{
				AuthenticationKind.UsernamePassword,
				"UsernamePassword"
			},
			{
				AuthenticationKind.WebApi,
				"WebApi"
			},
			{
				AuthenticationKind.Windows,
				"Windows"
			},
			{
				AuthenticationKind.Exchange,
				"UsernamePassword"
			}
		};

		// Token: 0x04000039 RID: 57
		private static readonly AuthenticationInfo windowsCurrentUserAuthenticationInfo = new AuthenticationInfo("Windows", new List<CredentialProperty>().AsReadOnly(), null, null);

		// Token: 0x0400003A RID: 58
		internal static readonly DataSourceKindInfo None = new DataSourceKindInfo(DataSourceKindInfo.emptyCredentialProperties, DataSourceKindInfo.emptyCredentialProperties, new List<AuthenticationInfo>().AsReadOnly(), DataSourceKindInfo.emptyPermissionKinds);

		// Token: 0x0400003B RID: 59
		private readonly ICollection<CredentialProperty> properties;

		// Token: 0x0400003C RID: 60
		private readonly ICollection<CredentialProperty> applicationProperties;

		// Token: 0x0400003D RID: 61
		private readonly ICollection<AuthenticationInfo> authenticationInfos;

		// Token: 0x0400003E RID: 62
		private readonly ICollection<string> permissionKinds;
	}
}
