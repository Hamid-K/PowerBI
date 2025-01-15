using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.DataSourceReference;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Resources;
using Microsoft.Mashup.Engine1.Runtime.Extensibility;
using Microsoft.Mashup.OAuth;

namespace Microsoft.Mashup.Engine1.Library.Sql
{
	// Token: 0x020003D5 RID: 981
	internal class SqlResource
	{
		// Token: 0x17000E6F RID: 3695
		// (get) Token: 0x0600221D RID: 8733 RVA: 0x0005E5E3 File Offset: 0x0005C7E3
		public static ResourceKindInfo ResourceKindInfo
		{
			get
			{
				return SqlResource.resourceKindInfo;
			}
		}

		// Token: 0x0600221E RID: 8734 RVA: 0x0005E5EC File Offset: 0x0005C7EC
		private static OAuthResource AadForSqlAzure(OAuthServices services, string resourcePath)
		{
			if (SqlResource.supportsDiscovery == null)
			{
				object obj = SqlResource.lockObject;
				lock (obj)
				{
					if (SqlResource.supportsDiscovery == null)
					{
						try
						{
							SqlResource.supportsDiscovery = new bool?(SqlEnvironment.SqlClient.InitializeAadDiscovery());
						}
						catch (Exception ex)
						{
							if (!SafeExceptions.IsSafeException(ex))
							{
								throw;
							}
							SqlResource.supportsDiscovery = new bool?(false);
						}
					}
				}
			}
			OAuthResource oauthResource;
			if (SqlResource.supportsDiscovery.Value && SqlResource.TryGetAadResource(resourcePath, services, out oauthResource))
			{
				return oauthResource;
			}
			return SqlResource.AadForSqlAzureFallback(services, resourcePath);
		}

		// Token: 0x0600221F RID: 8735 RVA: 0x0005E698 File Offset: 0x0005C898
		private static OAuthResource AadForSqlAzureFallback(OAuthServices services, string resourcePath)
		{
			string text = "https://database.windows.net/";
			OAuthSettings oauthSettings = AadSettings.CommonSettings;
			string text2;
			string text3;
			if (DatabaseResource.TryParsePath(resourcePath, out text2, out text3))
			{
				if (text2.StartsWith("tcp:", StringComparison.OrdinalIgnoreCase))
				{
					text2 = text2.Substring(4);
				}
				int num = text2.IndexOf(':');
				if (num > 0)
				{
					text2 = text2.Substring(0, num);
				}
				foreach (KeyValuePair<string, OAuthSettings> keyValuePair in SqlResource.sqlAzureResources)
				{
					if (text2.EndsWith(keyValuePair.Key, StringComparison.OrdinalIgnoreCase))
					{
						text = "https://" + keyValuePair.Key.Substring(1) + "/";
						oauthSettings = keyValuePair.Value;
						break;
					}
				}
			}
			return AadOAuthProvider.CreateResourceForId(services, text, oauthSettings);
		}

		// Token: 0x06002220 RID: 8736 RVA: 0x0005E770 File Offset: 0x0005C970
		private static bool TryGetAadResource(string resourcePath, OAuthServices trace, out OAuthResource resource)
		{
			string text;
			string text2;
			if (!DatabaseResource.TryParsePath(resourcePath, out text, out text2))
			{
				resource = null;
				return false;
			}
			bool flag;
			try
			{
				DbConnectionStringBuilder dbConnectionStringBuilder = SqlEnvironment.SqlClient.ProviderFactory.CreateConnectionStringBuilder();
				dbConnectionStringBuilder["UID"] = "anonymous";
				dbConnectionStringBuilder["Data Source"] = text;
				dbConnectionStringBuilder["Authentication"] = "Active Directory Interactive";
				using (DbConnection dbConnection = SqlEnvironment.SqlClient.ProviderFactory.CreateConnection())
				{
					dbConnection.ConnectionString = dbConnectionStringBuilder.ConnectionString;
					dbConnection.Open();
				}
				resource = null;
				flag = false;
			}
			catch (Exception innerException) when (SafeExceptions.IsSafeException(innerException))
			{
				while (innerException.InnerException != null)
				{
					innerException = innerException.InnerException;
				}
				SqlAadAuthException ex = innerException as SqlAadAuthException;
				if (ex == null)
				{
					Dictionary<string, object> dictionary = new Dictionary<string, object>();
					dictionary["Exception"] = innerException;
					trace.WriteTrace("SqlResource/TryGetAadResource", TraceEventType.Error, dictionary, true);
					resource = null;
					flag = false;
				}
				else
				{
					resource = AadOAuthProvider.CreateResourceForAuthorizationUrl(trace, ex.Authority, ex.Resource, null);
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x04000D3B RID: 3387
		private static object lockObject = new object();

		// Token: 0x04000D3C RID: 3388
		private static bool? supportsDiscovery;

		// Token: 0x04000D3D RID: 3389
		private static ResourceKindInfo resourceKindInfo = new DatabaseResourceKindInfo("SQL", null, true, false, true, new AuthenticationInfo[]
		{
			new WindowsAuthenticationInfo
			{
				SupportsAlternateCredentials = true
			},
			new UsernamePasswordAuthenticationInfo(),
			new AadAuthenticationInfo
			{
				ClientApplicationType = OAuthClientApplicationType.Required,
				ProviderFactory = new OAuthFactory(new Func<OAuthServices, string, OAuthResource>(SqlResource.AadForSqlAzure), (OAuthServices services, OAuthClientApplication app, string url) => new AadOAuthProvider(services, app, SqlResource.AadForSqlAzure(services, url)))
			}
		}, null, null, new DataSourceLocationFactory[] { SqlDataSourceLocation.Factory });

		// Token: 0x04000D3E RID: 3390
		private static readonly Dictionary<string, OAuthSettings> sqlAzureResources = new Dictionary<string, OAuthSettings>
		{
			{
				".database.cloudapi.de",
				AadSettings.CommonDeSettings
			},
			{
				".database.usgovcloudapi.net",
				AadSettings.CommonUsGovSettings
			},
			{
				".database.chinacloudapi.cn",
				AadSettings.CommonCnSettings
			}
		};
	}
}
