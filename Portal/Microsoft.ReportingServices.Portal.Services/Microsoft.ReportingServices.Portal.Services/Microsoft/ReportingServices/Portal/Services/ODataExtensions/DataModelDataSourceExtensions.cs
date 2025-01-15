using System;
using System.Collections.Generic;
using Microsoft.BIServer.HostingEnvironment.Cryptography;
using Microsoft.BIServer.HostingEnvironment.HostingInfo;
using Microsoft.ReportingServices.CatalogAccess.DataAccessObject;
using Model;

namespace Microsoft.ReportingServices.Portal.Services.ODataExtensions
{
	// Token: 0x0200003C RID: 60
	internal static class DataModelDataSourceExtensions
	{
		// Token: 0x06000259 RID: 601 RVA: 0x00010038 File Offset: 0x0000E238
		public static DataSource ToDataSourceWithoutSecret(this DataModelDataSourceEntity dataSourceEntity)
		{
			if (dataSourceEntity == null)
			{
				throw new ArgumentNullException("dataSourceEntity");
			}
			string text;
			string text2;
			if (dataSourceEntity.DSType == DataModelDataSourceEntity.DataModelDataSourceType.Live && dataSourceEntity.DSKind == DataModelDataSourceEntity.DataModelDataSourceKind.AnalysisServices)
			{
				text = DataModelDataSourceExtensions.TruncateTagIfPresent(HostingState.Current.CatalogCrypto.DecryptToString(dataSourceEntity.ConnectionString), CryptoTags.ConnectionString);
				text2 = DataModelDataSourceExtensions.TruncateTagIfPresent(HostingState.Current.CatalogCrypto.DecryptToString(dataSourceEntity.Username), CryptoTags.UserName);
			}
			else
			{
				text = HostingState.Current.CatalogCrypto.DecryptToString(dataSourceEntity.ConnectionString);
				text2 = HostingState.Current.CatalogCrypto.DecryptToString(dataSourceEntity.Username);
			}
			return new DataSource
			{
				Id = dataSourceEntity.DataSourceId,
				CreatedBy = dataSourceEntity.CreatedBy,
				CreatedDate = DataModelDataSourceExtensions.ToDateTimeOffset(dataSourceEntity.CreatedDate),
				ModifiedBy = dataSourceEntity.ModifiedBy,
				ModifiedDate = DataModelDataSourceExtensions.ToDateTimeOffset(dataSourceEntity.ModifiedDate),
				DataSourceSubType = DataModelDataSourceExtensions.DataSourceSubTypeName,
				IsEnabled = true,
				DataModelDataSource = new DataModelDataSource
				{
					Type = (DataModelDataSourceType)dataSourceEntity.DSType,
					Kind = (DataModelDataSourceKind)dataSourceEntity.DSKind,
					AuthType = (DataModelDataSourceAuthType)dataSourceEntity.AuthType,
					SupportedAuthTypes = ((DataModelDataSourceKind)dataSourceEntity.DSKind).GetSupportedAuthKinds((DataModelDataSourceType)dataSourceEntity.DSType),
					Username = text2,
					Secret = string.Empty,
					ModelConnectionName = dataSourceEntity.ModelConnectionName
				},
				ConnectionString = text
			};
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0001019C File Offset: 0x0000E39C
		public static DataModelDataSourceAuthType[] GetSupportedAuthKinds(this DataModelDataSourceKind datasourceKind, DataModelDataSourceType datasourceType)
		{
			List<DataModelDataSourceAuthType> list = (DataModelDataSourceExtensions._supportedAuthTypes.ContainsKey(datasourceKind) ? new List<DataModelDataSourceAuthType>(DataModelDataSourceExtensions._supportedAuthTypes[datasourceKind]) : new List<DataModelDataSourceAuthType>());
			if (datasourceType == DataModelDataSourceType.Import)
			{
				list.Remove(DataModelDataSourceAuthType.Integrated);
				list.Remove(DataModelDataSourceAuthType.Impersonate);
			}
			return list.ToArray();
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00003749 File Offset: 0x00001949
		internal static DateTimeOffset ToDateTimeOffset(DateTime dateTime)
		{
			if (dateTime == DateTime.MinValue)
			{
				return DateTimeOffset.MinValue;
			}
			if (dateTime == DateTime.MaxValue)
			{
				return DateTimeOffset.MaxValue;
			}
			return new DateTimeOffset(dateTime);
		}

		// Token: 0x0600025C RID: 604 RVA: 0x000101E8 File Offset: 0x0000E3E8
		public static DataSource ToDataSourceWithDecryptedSecret(this DataModelDataSourceEntity dataSourceEntity)
		{
			string dataSourceType = DataModelDataSourceExtensions.GetDataSourceType((DataModelDataSourceKind)dataSourceEntity.DSKind);
			return new DataSource
			{
				Id = dataSourceEntity.DataSourceId,
				CreatedBy = dataSourceEntity.CreatedBy,
				CreatedDate = dataSourceEntity.CreatedDate,
				ModifiedBy = dataSourceEntity.ModifiedBy,
				ModifiedDate = dataSourceEntity.ModifiedDate,
				DataSourceSubType = null,
				IsEnabled = true,
				DataModelDataSource = new DataModelDataSource
				{
					Type = (DataModelDataSourceType)dataSourceEntity.DSType,
					Kind = (DataModelDataSourceKind)dataSourceEntity.DSKind,
					AuthType = (DataModelDataSourceAuthType)dataSourceEntity.AuthType,
					Username = HostingState.Current.CatalogCrypto.DecryptToString(dataSourceEntity.Username),
					Secret = HostingState.Current.CatalogCrypto.DecryptToString(dataSourceEntity.Password),
					ModelConnectionName = dataSourceEntity.ModelConnectionName
				},
				ConnectionString = HostingState.Current.CatalogCrypto.DecryptToString(dataSourceEntity.ConnectionString),
				DataSourceType = dataSourceType,
				CredentialRetrieval = ((dataSourceEntity.AuthType == DataModelDataSourceEntity.DataModelDataSourceAuthType.Integrated) ? CredentialRetrievalType.integrated : CredentialRetrievalType.store),
				CredentialsInServer = new CredentialsStoredInServer
				{
					ImpersonateAuthenticatedUser = (dataSourceEntity.AuthType == DataModelDataSourceEntity.DataModelDataSourceAuthType.Impersonate),
					Password = HostingState.Current.CatalogCrypto.DecryptToString(dataSourceEntity.Password),
					UserName = HostingState.Current.CatalogCrypto.DecryptToString(dataSourceEntity.Username),
					UseAsWindowsCredentials = (dataSourceEntity.AuthType == DataModelDataSourceEntity.DataModelDataSourceAuthType.Windows || dataSourceEntity.AuthType == DataModelDataSourceEntity.DataModelDataSourceAuthType.Impersonate)
				},
				IsReference = false,
				IsConnectionStringOverridden = true
			};
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0001037C File Offset: 0x0000E57C
		public static DataSource ToDataSourceWithDecryptedSecret(this DataSource dataSource)
		{
			DataModelDataSource dataModelDataSource = dataSource.DataModelDataSource;
			string text = dataSource.DataSourceType;
			if (string.Equals(dataSource.DataSourceSubType, DataModelDataSourceExtensions.DataSourceSubTypeName, StringComparison.OrdinalIgnoreCase))
			{
				text = DataModelDataSourceExtensions.GetDataSourceType(dataModelDataSource.Kind);
			}
			dataSource.DataModelDataSource = null;
			dataSource.DataSourceType = text;
			dataSource.CredentialRetrieval = ((dataModelDataSource.AuthType == DataModelDataSourceAuthType.Integrated) ? CredentialRetrievalType.integrated : CredentialRetrievalType.store);
			dataSource.CredentialsInServer = new CredentialsStoredInServer
			{
				ImpersonateAuthenticatedUser = (dataModelDataSource.AuthType == DataModelDataSourceAuthType.Impersonate),
				Password = dataModelDataSource.Secret,
				UserName = dataModelDataSource.Username,
				UseAsWindowsCredentials = (dataModelDataSource.AuthType == DataModelDataSourceAuthType.Windows || dataModelDataSource.AuthType == DataModelDataSourceAuthType.Impersonate)
			};
			return dataSource;
		}

		// Token: 0x0600025E RID: 606 RVA: 0x00010425 File Offset: 0x0000E625
		public static bool IsNullOrWhitespaceUsernameOrSecret(this DataSource ds)
		{
			return ds.DataModelDataSource == null || string.IsNullOrWhiteSpace(ds.DataModelDataSource.Username) || string.IsNullOrWhiteSpace(ds.DataModelDataSource.Secret);
		}

		// Token: 0x0600025F RID: 607 RVA: 0x00010454 File Offset: 0x0000E654
		public static bool IsNotConnectionToLocalFile(this DataSource ds)
		{
			if (ds.DataModelDataSource.Kind == DataModelDataSourceKind.File || ds.DataModelDataSource.Kind == DataModelDataSourceKind.Folder)
			{
				string connectionString = ds.ConnectionString;
				return connectionString != null && connectionString.Length >= 2 && (string.Compare(connectionString.Substring(0, 2), "\\\\", StringComparison.Ordinal) == 0 && connectionString.Substring(2).IndexOf("\\", StringComparison.Ordinal) != 0 && connectionString.Substring(2).IndexOf("\\", StringComparison.Ordinal) != -1) && connectionString.Substring(2).IndexOf("\\\\", StringComparison.Ordinal) == -1;
			}
			return true;
		}

		// Token: 0x06000260 RID: 608 RVA: 0x000104EC File Offset: 0x0000E6EC
		private static string GetDataSourceType(DataModelDataSourceKind kind)
		{
			string text;
			if (kind != DataModelDataSourceKind.AnalysisServices)
			{
				if (kind != DataModelDataSourceKind.SapHana)
				{
					text = kind.ToString();
				}
				else
				{
					text = "ODBC";
				}
			}
			else
			{
				text = "OLEDB-MD";
			}
			return text;
		}

		// Token: 0x06000261 RID: 609 RVA: 0x00010523 File Offset: 0x0000E723
		internal static string TruncateTagIfPresent(string data, string tag)
		{
			if (!string.IsNullOrEmpty(data) && data.StartsWith(tag))
			{
				return data.Substring(tag.Length);
			}
			return data;
		}

		// Token: 0x040000B7 RID: 183
		public static string DataSourceSubTypeName = "DataModel";

		// Token: 0x040000B8 RID: 184
		private static readonly IReadOnlyDictionary<DataModelDataSourceKind, DataModelDataSourceAuthType[]> _supportedAuthTypes = new Dictionary<DataModelDataSourceKind, DataModelDataSourceAuthType[]>
		{
			{
				DataModelDataSourceKind.ActiveDirectory,
				new DataModelDataSourceAuthType[]
				{
					DataModelDataSourceAuthType.Integrated,
					DataModelDataSourceAuthType.Windows,
					DataModelDataSourceAuthType.UsernamePassword
				}
			},
			{
				DataModelDataSourceKind.AnalysisServices,
				new DataModelDataSourceAuthType[]
				{
					DataModelDataSourceAuthType.Integrated,
					DataModelDataSourceAuthType.Impersonate,
					DataModelDataSourceAuthType.Windows,
					DataModelDataSourceAuthType.UsernamePassword
				}
			},
			{
				DataModelDataSourceKind.AzureBlobs,
				new DataModelDataSourceAuthType[]
				{
					DataModelDataSourceAuthType.Anonymous,
					DataModelDataSourceAuthType.Key
				}
			},
			{
				DataModelDataSourceKind.AzureTables,
				new DataModelDataSourceAuthType[] { DataModelDataSourceAuthType.Key }
			},
			{
				DataModelDataSourceKind.CurrentWorkbook,
				new DataModelDataSourceAuthType[0]
			},
			{
				DataModelDataSourceKind.DataMarket,
				new DataModelDataSourceAuthType[] { DataModelDataSourceAuthType.Key }
			},
			{
				DataModelDataSourceKind.DB2,
				new DataModelDataSourceAuthType[]
				{
					DataModelDataSourceAuthType.Integrated,
					DataModelDataSourceAuthType.Windows,
					DataModelDataSourceAuthType.UsernamePassword
				}
			},
			{
				DataModelDataSourceKind.Exchange,
				new DataModelDataSourceAuthType[]
				{
					DataModelDataSourceAuthType.Integrated,
					DataModelDataSourceAuthType.Windows,
					DataModelDataSourceAuthType.UsernamePassword
				}
			},
			{
				DataModelDataSourceKind.Facebook,
				new DataModelDataSourceAuthType[0]
			},
			{
				DataModelDataSourceKind.File,
				new DataModelDataSourceAuthType[]
				{
					DataModelDataSourceAuthType.Integrated,
					DataModelDataSourceAuthType.Windows
				}
			},
			{
				DataModelDataSourceKind.Folder,
				new DataModelDataSourceAuthType[]
				{
					DataModelDataSourceAuthType.Integrated,
					DataModelDataSourceAuthType.Windows
				}
			},
			{
				DataModelDataSourceKind.GoogleAnalytics,
				new DataModelDataSourceAuthType[0]
			},
			{
				DataModelDataSourceKind.Hdfs,
				new DataModelDataSourceAuthType[]
				{
					DataModelDataSourceAuthType.Anonymous,
					DataModelDataSourceAuthType.Integrated,
					DataModelDataSourceAuthType.Windows
				}
			},
			{
				DataModelDataSourceKind.HDInsight,
				new DataModelDataSourceAuthType[]
				{
					DataModelDataSourceAuthType.Anonymous,
					DataModelDataSourceAuthType.Key
				}
			},
			{
				DataModelDataSourceKind.Informix,
				new DataModelDataSourceAuthType[]
				{
					DataModelDataSourceAuthType.Integrated,
					DataModelDataSourceAuthType.Windows,
					DataModelDataSourceAuthType.UsernamePassword
				}
			},
			{
				DataModelDataSourceKind.MQ,
				new DataModelDataSourceAuthType[]
				{
					DataModelDataSourceAuthType.UsernamePassword,
					DataModelDataSourceAuthType.Anonymous
				}
			},
			{
				DataModelDataSourceKind.MySql,
				new DataModelDataSourceAuthType[]
				{
					DataModelDataSourceAuthType.Integrated,
					DataModelDataSourceAuthType.Windows,
					DataModelDataSourceAuthType.UsernamePassword
				}
			},
			{
				DataModelDataSourceKind.OData,
				new DataModelDataSourceAuthType[]
				{
					DataModelDataSourceAuthType.Anonymous,
					DataModelDataSourceAuthType.Integrated,
					DataModelDataSourceAuthType.Windows,
					DataModelDataSourceAuthType.UsernamePassword,
					DataModelDataSourceAuthType.Key
				}
			},
			{
				DataModelDataSourceKind.Odbc,
				new DataModelDataSourceAuthType[]
				{
					DataModelDataSourceAuthType.Integrated,
					DataModelDataSourceAuthType.UsernamePassword,
					DataModelDataSourceAuthType.Anonymous,
					DataModelDataSourceAuthType.Windows
				}
			},
			{
				DataModelDataSourceKind.OleDb,
				new DataModelDataSourceAuthType[]
				{
					DataModelDataSourceAuthType.Integrated,
					DataModelDataSourceAuthType.UsernamePassword,
					DataModelDataSourceAuthType.Anonymous,
					DataModelDataSourceAuthType.Windows
				}
			},
			{
				DataModelDataSourceKind.Oracle,
				new DataModelDataSourceAuthType[]
				{
					DataModelDataSourceAuthType.Integrated,
					DataModelDataSourceAuthType.Windows,
					DataModelDataSourceAuthType.UsernamePassword
				}
			},
			{
				DataModelDataSourceKind.PostgreSQL,
				new DataModelDataSourceAuthType[] { DataModelDataSourceAuthType.UsernamePassword }
			},
			{
				DataModelDataSourceKind.Salesforce,
				new DataModelDataSourceAuthType[0]
			},
			{
				DataModelDataSourceKind.SapBusinessObjects,
				new DataModelDataSourceAuthType[0]
			},
			{
				DataModelDataSourceKind.SapBusinessWarehouse,
				new DataModelDataSourceAuthType[]
				{
					DataModelDataSourceAuthType.Integrated,
					DataModelDataSourceAuthType.UsernamePassword
				}
			},
			{
				DataModelDataSourceKind.SapHana,
				new DataModelDataSourceAuthType[]
				{
					DataModelDataSourceAuthType.Integrated,
					DataModelDataSourceAuthType.Windows,
					DataModelDataSourceAuthType.UsernamePassword
				}
			},
			{
				DataModelDataSourceKind.SharePoint,
				new DataModelDataSourceAuthType[]
				{
					DataModelDataSourceAuthType.Anonymous,
					DataModelDataSourceAuthType.Integrated,
					DataModelDataSourceAuthType.Windows
				}
			},
			{
				DataModelDataSourceKind.SQL,
				new DataModelDataSourceAuthType[]
				{
					DataModelDataSourceAuthType.Integrated,
					DataModelDataSourceAuthType.Windows,
					DataModelDataSourceAuthType.UsernamePassword
				}
			},
			{
				DataModelDataSourceKind.Sybase,
				new DataModelDataSourceAuthType[]
				{
					DataModelDataSourceAuthType.Integrated,
					DataModelDataSourceAuthType.Windows,
					DataModelDataSourceAuthType.UsernamePassword
				}
			},
			{
				DataModelDataSourceKind.Teradata,
				new DataModelDataSourceAuthType[]
				{
					DataModelDataSourceAuthType.Integrated,
					DataModelDataSourceAuthType.Windows,
					DataModelDataSourceAuthType.UsernamePassword
				}
			},
			{
				DataModelDataSourceKind.Web,
				new DataModelDataSourceAuthType[]
				{
					DataModelDataSourceAuthType.Anonymous,
					DataModelDataSourceAuthType.Integrated,
					DataModelDataSourceAuthType.Windows,
					DataModelDataSourceAuthType.UsernamePassword
				}
			}
		};
	}
}
