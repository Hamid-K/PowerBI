using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Threading;
using Microsoft.AnalysisServices.Tabular;
using Microsoft.BIServer.HostingEnvironment;
using Microsoft.Data.Mashup;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.EngineHost;
using Microsoft.Mashup.Storage;
using Microsoft.PowerBI.ReportServer.AsServer.ProviderManager;
using Microsoft.PowerBI.ReportServer.PbixLib.Parsing;

namespace Microsoft.PowerBI.ReportServer.AsServer.Mashup
{
	// Token: 0x02000025 RID: 37
	internal class MashupProviderManager : IProviderManager
	{
		// Token: 0x060000C6 RID: 198 RVA: 0x00004618 File Offset: 0x00002818
		public MashupProviderManager()
		{
			Logger.Info("Initializing a new instance of MashupProviderManager...", Array.Empty<object>());
			int num = 15;
			TimeSpan timeSpan = TimeSpan.FromMinutes((double)num);
			TimeSpan? containerTimeToLive = MashupConnection.ContainerTimeToLive;
			TimeSpan timeSpan2 = timeSpan;
			if (containerTimeToLive == null || (containerTimeToLive != null && containerTimeToLive.GetValueOrDefault() != timeSpan2))
			{
				try
				{
					Logger.Info("Setting Mashup Evaluation Container time to live to {0} minutes...", new object[] { num });
					MashupConnection.TryCleanup();
					MashupConnection.ContainerTimeToLive = new TimeSpan?(timeSpan);
				}
				catch (Exception ex)
				{
					Logger.Warning("Failed to set Mashup Evaluation Container time to live: {0}", new object[] { ex.Message });
				}
			}
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x000046D0 File Offset: 0x000028D0
		public bool TryGetMashupConnectionString(string dbConnectionString, out string mashup)
		{
			mashup = null;
			DbConnectionStringBuilder dbConnectionStringBuilder = new DbConnectionStringBuilder
			{
				ConnectionString = dbConnectionString
			};
			if (dbConnectionStringBuilder.ContainsKey("mashup"))
			{
				mashup = dbConnectionStringBuilder["mashup"].ToString();
			}
			if (dbConnectionStringBuilder.ContainsKey("package"))
			{
				mashup = dbConnectionStringBuilder["package"].ToString();
			}
			return mashup != null;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00004730 File Offset: 0x00002930
		public string GetMashupConnectionString(string dbConnectionString)
		{
			string text = null;
			if (!this.TryGetMashupConnectionString(dbConnectionString, out text))
			{
				throw new MashupStringNotFoundException(dbConnectionString);
			}
			return text;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00004754 File Offset: 0x00002954
		public virtual IEnumerable<Microsoft.Data.Mashup.DataSource> GetDataSources(string mashupConnectionString)
		{
			List<Microsoft.Data.Mashup.DataSource> list = new List<Microsoft.Data.Mashup.DataSource>();
			using (MashupConnection mashupConnection = new MashupConnection(new MashupConnectionStringBuilder
			{
				Package = mashupConnectionString
			}.ConnectionString))
			{
				bool flag;
				bool flag2;
				IEnumerable<DataSourceReference> enumerable = mashupConnection.FindReferencedDataSources(out flag, out flag2);
				list.AddRange(enumerable.Select((DataSourceReference r) => r.DataSource));
			}
			return list;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x000047D4 File Offset: 0x000029D4
		public string UpdateConnectionStringWithCredentials(string connectionString, IEnumerable<PbixDataSource> dataSources)
		{
			Dictionary<DataSourceKey, PbixDataSource> dictionary = dataSources.ToDictionary((PbixDataSource ds) => new DataSourceKey
			{
				Kind = ds.Kind.ToString(),
				ConnectionString = ds.ConnectionString
			});
			string location = this.GetLocation(connectionString);
			string mashupConnectionString = this.GetMashupConnectionString(connectionString);
			string text = this.GenerateDataSourceSettings(mashupConnectionString, dictionary);
			return new MashupConnectionStringBuilder
			{
				Package = mashupConnectionString,
				Location = location,
				FastCombine = true,
				AllowNativeQueries = true,
				DataSourceSettings = text,
				LegacyRedirects = true
			}.ConnectionString;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00004854 File Offset: 0x00002A54
		public ProviderDataSourceCredentials RemoveCredentials(ProviderDataSource providerDataSource)
		{
			if (providerDataSource == null)
			{
				throw new ArgumentNullException("providerDataSource cannot be null");
			}
			return new ProviderDataSourceCredentials
			{
				ConnectionString = this.RemoveCredentialsFromConnectionString(providerDataSource.ConnectionString)
			};
		}

		// Token: 0x060000CC RID: 204 RVA: 0x0000487C File Offset: 0x00002A7C
		internal string RemoveCredentialsFromConnectionString(string connectionString)
		{
			string location = this.GetLocation(connectionString);
			string mashupConnectionString = this.GetMashupConnectionString(connectionString);
			return new MashupConnectionStringBuilder
			{
				Package = mashupConnectionString,
				Location = location,
				FastCombine = true,
				AllowNativeQueries = true,
				LegacyRedirects = true
			}.ConnectionString;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000048C8 File Offset: 0x00002AC8
		internal string GetLocation(string dbConnectionString)
		{
			DbConnectionStringBuilder dbConnectionStringBuilder = new DbConnectionStringBuilder
			{
				ConnectionString = dbConnectionString
			};
			if (dbConnectionStringBuilder.ContainsKey("location"))
			{
				return dbConnectionStringBuilder["location"].ToString();
			}
			return null;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00004904 File Offset: 0x00002B04
		internal string GenerateDataSourceSettings(string mashupConnectionString, Dictionary<DataSourceKey, PbixDataSource> serverDataSourcesDictionary)
		{
			Dictionary<Microsoft.Data.Mashup.DataSource, DataSourceSetting> dictionary = new Dictionary<Microsoft.Data.Mashup.DataSource, DataSourceSetting>();
			foreach (Microsoft.Data.Mashup.DataSource dataSource in this.GetDataSources(mashupConnectionString))
			{
				string text = (dataSource.Kind.Equals("web", StringComparison.InvariantCultureIgnoreCase) ? dataSource.Path : dataSource.NormalizedPath);
				DataSourceKey dataSourceKey = new DataSourceKey
				{
					Kind = dataSource.Kind,
					ConnectionString = text
				};
				if (serverDataSourcesDictionary.ContainsKey(dataSourceKey))
				{
					PbixDataSource pbixDataSource = serverDataSourcesDictionary[dataSourceKey];
					if (!dictionary.ContainsKey(dataSource))
					{
						dictionary.Add(dataSource, this.GenerateDataSourceSetting(pbixDataSource));
					}
				}
			}
			return DataSourceSettings.Create(dictionary);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x000049C0 File Offset: 0x00002BC0
		internal DataSourceSetting GenerateDataSourceSetting(PbixDataSource pbixDataSource)
		{
			switch (pbixDataSource.AuthType)
			{
			case AuthorizationType.Anonymous:
				return DataSourceSetting.CreateAnonymousCredential();
			case AuthorizationType.Integrated:
			{
				DataSourceSetting dataSourceSetting = DataSourceSetting.CreateWindowsCredential();
				dataSourceSetting.AuthenticationProperties.Add("IdentitySource", "Thread");
				return dataSourceSetting;
			}
			case AuthorizationType.Windows:
				return DataSourceSetting.CreateWindowsCredential(pbixDataSource.Username, pbixDataSource.Secret);
			case AuthorizationType.UsernamePassword:
				return DataSourceSetting.CreateUsernamePasswordCredential(pbixDataSource.Username, pbixDataSource.Secret);
			case AuthorizationType.Key:
				return DataSourceSetting.CreateKeyCredential(pbixDataSource.Secret);
			}
			throw new InvalidOperationException(SR.Error_UnsupportedAuthenticationType(pbixDataSource.AuthType.ToString()));
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00004A63 File Offset: 0x00002C63
		public IEnumerable<PbixDataSource> BuildDataModelDataSources(ProviderDataSourceInfo providerDataSourceInfo)
		{
			AccessType dbType = (providerDataSourceInfo.isDirectQuery ? AccessType.DirectQuery : AccessType.Import);
			string text;
			if (this.TryGetMashupConnectionString(providerDataSourceInfo.providerDataSource.ConnectionString, out text))
			{
				foreach (Microsoft.Data.Mashup.DataSource dataSource in this.GetDataSources(text))
				{
					SourceKind sourceKind;
					if (this.TryParseDataSourceKind(dataSource, out sourceKind))
					{
						string text2 = ((sourceKind == SourceKind.Web) ? dataSource.Path : dataSource.NormalizedPath);
						yield return new PbixDataSource
						{
							ConnectionString = text2,
							DataSourceIdentifier = providerDataSourceInfo.providerDataSource.Name,
							Kind = sourceKind,
							Type = dbType,
							AuthType = AuthorizationType.Unknown
						};
					}
				}
				IEnumerator<Microsoft.Data.Mashup.DataSource> enumerator = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00004A7C File Offset: 0x00002C7C
		public ProviderDataSourceCredentials CreateCredentials(ProviderDataSource providerDataSource, IEnumerable<PbixDataSource> dataSources)
		{
			if (dataSources == null)
			{
				throw new ArgumentNullException("dataSources cannot be null");
			}
			if (providerDataSource == null)
			{
				throw new ArgumentNullException("providerDataSource cannot be null");
			}
			return new ProviderDataSourceCredentials
			{
				ConnectionString = this.UpdateConnectionStringWithCredentials(providerDataSource.ConnectionString, dataSources),
				Provider = "Microsoft.Data.Mashup",
				ImpersonationMode = new ImpersonationMode?(ImpersonationMode.ImpersonateServiceAccount)
			};
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00004AD4 File Offset: 0x00002CD4
		public bool CanCreateCredentials(ProviderDataSource providerDataSource)
		{
			string text = null;
			return providerDataSource.Provider.Equals("Microsoft.Data.Mashup", StringComparison.InvariantCultureIgnoreCase) || providerDataSource.Provider.Equals("Microsoft.PowerBI.OleDb", StringComparison.InvariantCultureIgnoreCase) || this.TryGetMashupConnectionString(providerDataSource.ConnectionString, out text);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00004B1C File Offset: 0x00002D1C
		public PbixDataSourceCheckResult TestCredentials(PbixDataSource dataSource)
		{
			if (dataSource == null)
			{
				throw new ArgumentNullException("dataSource cannot be null");
			}
			if (dataSource == null)
			{
				throw new ArgumentNullException("dataSource cannot be null");
			}
			bool flag = false;
			string text = null;
			bool flag2 = this.TestDataSource(dataSource, ref flag, ref text);
			if (!flag2 && flag)
			{
				Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
				flag2 = this.TestDataSource(dataSource, ref flag, ref text);
			}
			return new PbixDataSourceCheckResult
			{
				IsSuccessful = flag2,
				ErrorMessage = text
			};
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00004B90 File Offset: 0x00002D90
		private bool TestDataSource(PbixDataSource dataSource, ref bool languageExceptionOccured, ref string errorMessage)
		{
			languageExceptionOccured = false;
			string text = dataSource.Kind.ToString();
			Resource resource = new Resource(text, dataSource.ConnectionString, dataSource.ConnectionString);
			bool flag;
			try
			{
				IDataSourceLocation dataSourceLocation;
				if (MashupEngines.Version1.TryCreateLocationFromResource(resource, false, out dataSourceLocation))
				{
					DataSourceSetting dataSourceSetting = this.GenerateDataSourceSetting(dataSource);
					new DataSourceReference(dataSourceLocation.ToJson()).TestConnection(dataSourceSetting);
					flag = true;
				}
				else
				{
					errorMessage = SR.Error_UnsupportedDataSourceLocation(text);
					flag = false;
				}
			}
			catch (MashupCredentialException ex)
			{
				string text2 = "select one of the installed languages";
				if (ex.Message.ToLower().Contains(text2))
				{
					errorMessage = string.Format(SR.Error_LanguageError, Thread.CurrentThread.CurrentCulture.DisplayName);
					languageExceptionOccured = true;
				}
				else
				{
					errorMessage = SR.Error_LogonFailed;
				}
				Logger.Verbose("Logon failed for TestCredentials: {0}", new object[] { ex.Message });
				flag = false;
			}
			catch (Exception ex2)
			{
				errorMessage = ex2.Message;
				Logger.Error(ex2.Message, Array.Empty<object>());
				flag = false;
			}
			return flag;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00004CB0 File Offset: 0x00002EB0
		private bool TryParseDataSourceKind(Microsoft.Data.Mashup.DataSource dataSource, out SourceKind dataSourceKind)
		{
			if (!Enum.TryParse<SourceKind>(dataSource.Kind, out dataSourceKind))
			{
				Logger.Verbose(string.Format("Server currently doesn't support this datasource kind: {0}, connection string: {1}", dataSource.Kind, dataSource.NormalizedPath), Array.Empty<object>());
				return false;
			}
			Logger.Verbose("Datasource {0} parsed", new object[] { dataSource.NormalizedPath });
			return true;
		}

		// Token: 0x04000065 RID: 101
		public const string DataProviderName = "Microsoft.Data.Mashup";

		// Token: 0x04000066 RID: 102
		public const string DataProviderOleDbName = "Microsoft.PowerBI.OleDb";
	}
}
