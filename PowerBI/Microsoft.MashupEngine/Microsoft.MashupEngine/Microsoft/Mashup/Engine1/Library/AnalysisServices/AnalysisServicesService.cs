using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using Microsoft.AnalysisServices.AdomdClient;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Http;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.AnalysisServices
{
	// Token: 0x02000F33 RID: 3891
	internal class AnalysisServicesService
	{
		// Token: 0x17001DC9 RID: 7625
		// (get) Token: 0x060066EC RID: 26348 RVA: 0x001622ED File Offset: 0x001604ED
		public static PersistentCacheKey AnalysisServicesCacheKey
		{
			get
			{
				return new PersistentCacheKey("AnalysisServices/4");
			}
		}

		// Token: 0x060066ED RID: 26349 RVA: 0x001622FC File Offset: 0x001604FC
		public AnalysisServicesService(IEngineHost host, OptionsRecord options, string serverName, string catalogName = null)
		{
			this.host = host;
			this.hostProgress = ProgressService.GetHostProgress(host, "AnalysisServices", serverName);
			this.serverName = serverName;
			this.catalogName = catalogName;
			this.options = options;
			this.accessTokenPlaceholder = Guid.NewGuid().ToString();
		}

		// Token: 0x17001DCA RID: 7626
		// (get) Token: 0x060066EE RID: 26350 RVA: 0x00162357 File Offset: 0x00160557
		public string Server
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x17001DCB RID: 7627
		// (get) Token: 0x060066EF RID: 26351 RVA: 0x0016235F File Offset: 0x0016055F
		public string CatalogName
		{
			get
			{
				return this.catalogName;
			}
		}

		// Token: 0x17001DCC RID: 7628
		// (get) Token: 0x060066F0 RID: 26352 RVA: 0x00162367 File Offset: 0x00160567
		public IList<AnalysisServicesCatalog> Catalogs
		{
			get
			{
				if (this.catalogs == null)
				{
					this.catalogs = this.GetCatalogs();
				}
				return this.catalogs;
			}
		}

		// Token: 0x17001DCD RID: 7629
		// (get) Token: 0x060066F1 RID: 26353 RVA: 0x00162384 File Offset: 0x00160584
		public bool IsInTabularMode
		{
			get
			{
				if (this.isInTabularMode == null)
				{
					string text = AnalysisServicesService.AnalysisServicesCacheKey.Qualify("IsTabular", this.ConnectionStringWithoutAccessToken);
					Stream stream;
					if (this.MetadataCache.TryGetValue(text, out stream))
					{
						using (BinaryReader binaryReader = new BinaryReader(stream))
						{
							this.isInTabularMode = new bool?(binaryReader.ReadBoolean());
							goto IL_00B2;
						}
					}
					this.isInTabularMode = new bool?(this.GetIsInTabularMode());
					stream = this.MetadataCache.BeginAdd();
					BinaryWriter binaryWriter = new BinaryWriter(stream);
					binaryWriter.Write(this.isInTabularMode.Value);
					binaryWriter.Flush();
					this.MetadataCache.EndAdd(text, stream).Dispose();
				}
				IL_00B2:
				return this.isInTabularMode.Value;
			}
		}

		// Token: 0x17001DCE RID: 7630
		// (get) Token: 0x060066F2 RID: 26354 RVA: 0x00162460 File Offset: 0x00160660
		public IResource Resource
		{
			get
			{
				if (this.resource == null)
				{
					this.resource = AnalysisServicesService.GetResource(this.serverName, this.catalogName);
				}
				return this.resource;
			}
		}

		// Token: 0x17001DCF RID: 7631
		// (get) Token: 0x060066F3 RID: 26355 RVA: 0x00162487 File Offset: 0x00160687
		public bool SupportsProperties
		{
			get
			{
				return this.host.QueryService<IExtensibilityService>() != null && this.options.GetBool("SupportsProperties", false);
			}
		}

		// Token: 0x17001DD0 RID: 7632
		// (get) Token: 0x060066F4 RID: 26356 RVA: 0x001624A9 File Offset: 0x001606A9
		public IPersistentCache DataCache
		{
			get
			{
				if (this.dataCache == null)
				{
					this.dataCache = this.host.GetPersistentCache();
				}
				return this.dataCache;
			}
		}

		// Token: 0x17001DD1 RID: 7633
		// (get) Token: 0x060066F5 RID: 26357 RVA: 0x001624CA File Offset: 0x001606CA
		public IPersistentCache MetadataCache
		{
			get
			{
				if (this.metadataCache == null)
				{
					this.metadataCache = this.host.GetMetadataCache();
				}
				return this.metadataCache;
			}
		}

		// Token: 0x17001DD2 RID: 7634
		// (get) Token: 0x060066F6 RID: 26358 RVA: 0x001624EB File Offset: 0x001606EB
		public IEngineHost EngineHost
		{
			get
			{
				return this.host;
			}
		}

		// Token: 0x17001DD3 RID: 7635
		// (get) Token: 0x060066F7 RID: 26359 RVA: 0x001624F3 File Offset: 0x001606F3
		private string ConnectionStringWithoutAccessToken
		{
			get
			{
				this.EnsureConnectionString();
				return this.connectionString;
			}
		}

		// Token: 0x060066F8 RID: 26360 RVA: 0x00162504 File Offset: 0x00160704
		private bool GetIsInTabularMode()
		{
			bool flag;
			using (IAnalysisServicesConnection analysisServicesConnection = this.CreateConnection())
			{
				analysisServicesConnection.Open();
				if (new Version(analysisServicesConnection.ServerVersion) < AnalysisServicesService.sqlServer2012)
				{
					flag = false;
				}
				else
				{
					DataSet schemaDataSet = analysisServicesConnection.GetSchemaDataSet("DISCOVER_XML_METADATA", new AdomdRestrictionCollection { { "ObjectExpansion", "ObjectProperties" } });
					if (schemaDataSet.Tables.Count == 0)
					{
						flag = false;
					}
					else
					{
						DataTable dataTable = schemaDataSet.Tables[0];
						if (!dataTable.Columns.Contains("METADATA") || dataTable.Rows.Count == 0)
						{
							flag = false;
						}
						else
						{
							string text = dataTable.Rows[0]["METADATA"] as string;
							if (text == null)
							{
								flag = false;
							}
							else
							{
								using (XmlReader xmlReader = XmlHelperUtility.XmlReaderCreate(new StringReader(text)))
								{
									XmlDocument xmlDocument = XmlHelperUtility.CreateXmlDocument();
									xmlDocument.Load(xmlReader);
									XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(xmlDocument.NameTable);
									xmlNamespaceManager.AddNamespace("ddl", "http://schemas.microsoft.com/analysisservices/2003/engine");
									xmlNamespaceManager.AddNamespace("ddl200_200", "http://schemas.microsoft.com/analysisservices/2010/engine/200/200");
									xmlNamespaceManager.AddNamespace("ddl300", "http://schemas.microsoft.com/analysisservices/2011/engine/300");
									XmlNodeList xmlNodeList = xmlDocument.SelectNodes("descendant::ddl300:ServerMode", xmlNamespaceManager);
									if (xmlNodeList == null || xmlNodeList.Count == 0)
									{
										flag = false;
									}
									else
									{
										XmlNode xmlNode = xmlNodeList[0];
										if (xmlNode == null)
										{
											flag = false;
										}
										else
										{
											flag = "Tabular".Equals(xmlNode.InnerText, StringComparison.Ordinal);
										}
									}
								}
							}
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x060066F9 RID: 26361 RVA: 0x001626C4 File Offset: 0x001608C4
		public static IResource GetResource(string serverName, string catalogName = null)
		{
			string text = serverName;
			if (catalogName != null)
			{
				text = text + ";" + catalogName;
			}
			return Microsoft.Mashup.Engine1.Library.Resource.New("AnalysisServices", text.ToString());
		}

		// Token: 0x060066FA RID: 26362 RVA: 0x001626F3 File Offset: 0x001608F3
		public AnalysisServicesCatalog GetCatalog(string name)
		{
			return new AnalysisServicesCatalog(new AnalysisServicesService(this.host, this.options, this.serverName, name), name);
		}

		// Token: 0x060066FB RID: 26363 RVA: 0x00162714 File Offset: 0x00160914
		public IDataReaderWithTableSchema ExecuteCommand(IPersistentCache cache, CommandBehavior behavior, string commandText, KeyValuePair<string, object>[] parameters, RowRange range, Func<RowRange, string> getRangedCommandText)
		{
			IHostProgress hostProgress = ProgressService.GetHostProgress(this.host, this.Resource.Kind, this.Resource.Path);
			IDataReaderWithTableSchema dataReaderWithTableSchema;
			using (new ProgressRequest(hostProgress))
			{
				string cacheKey = AnalysisServicesService.GetCacheKey(behavior, this.ConnectionStringWithoutAccessToken, commandText, parameters, range);
				Stream stream;
				if (cache.TryGetValue(cacheKey, out stream))
				{
					dataReaderWithTableSchema = new ProgressDbDataReader(DbData.Deserialize(stream), hostProgress);
				}
				else
				{
					dataReaderWithTableSchema = new DbData.CachingDbDataReader(this.host, cache, cacheKey, this.GetDataReader(commandText, parameters, range, getRangedCommandText), range.TakeCount.IsInfinite ? long.MaxValue : range.TakeCount.Value, cache.MaxEntryLength, () => TracingService.CreateTrace(this.host, "Engine/IO/AnalysisServices/TraceExceptions", TraceEventType.Information, this.Resource), true, !range.TakeCount.IsInfinite && range.TakeCount.Value < 4096L);
				}
			}
			return dataReaderWithTableSchema;
		}

		// Token: 0x060066FC RID: 26364 RVA: 0x0016282C File Offset: 0x00160A2C
		public IDataReaderWithTableSchema ExecuteCommand(IPersistentCache cache, CommandBehavior behavior, string commandText, params KeyValuePair<string, object>[] parameters)
		{
			return this.ExecuteCommand(cache, behavior, commandText, parameters, RowRange.All, null);
		}

		// Token: 0x060066FD RID: 26365 RVA: 0x00162840 File Offset: 0x00160A40
		private IAnalysisServicesConnection CreateConnection()
		{
			string text = this.ConnectionStringWithoutAccessToken;
			if (this.oauthCredential != null)
			{
				this.oauthCredential = this.oauthCredential.RefreshTokenAsNeeded(this.host, this.Resource, false);
				text = text.Replace(this.accessTokenPlaceholder, this.oauthCredential.AccessTokenForResource(null));
			}
			return this.connectionFactory.CreateConnection(text);
		}

		// Token: 0x060066FE RID: 26366 RVA: 0x001628A0 File Offset: 0x00160AA0
		private void EnsureConnectionString()
		{
			if (this.connectionString == null)
			{
				Value @null;
				if (!this.options.TryGetValue("Culture", out @null))
				{
					@null = Value.Null;
				}
				CultureInfo cultureInfo = Culture.GetCultureInfo(this.host, @null, Culture.GetDefaultCulture(this.host));
				if (Lcid.IsTransient(cultureInfo.LCID))
				{
					throw AnalysisServicesExceptions.NewUnsupportedCulture(cultureInfo.Name);
				}
				DbConnectionStringBuilder dbConnectionStringBuilder = new DbConnectionStringBuilder();
				try
				{
					dbConnectionStringBuilder["Data Source"] = this.serverName;
				}
				catch (ArgumentException ex)
				{
					throw AnalysisServicesExceptions.NewInvalidServerException(TextValue.New(this.serverName), ex);
				}
				if (this.catalogName != null)
				{
					try
					{
						dbConnectionStringBuilder["Initial Catalog"] = this.catalogName;
					}
					catch (ArgumentException ex2)
					{
						throw AnalysisServicesExceptions.NewInvalidDatabaseNameException(TextValue.New(this.catalogName), ex2);
					}
				}
				ResourceCredentialCollection resourceCredentialCollection = HostResourcePermissionService.VerifyPermissionAndGetCredentials(this.host, this.Resource, null);
				if (resourceCredentialCollection.Count < 1 || resourceCredentialCollection.Count > 2)
				{
					throw DataSourceException.NewInvalidCredentialsError(this.host, this.Resource, null, null, null);
				}
				bool flag = AnalysisServicesResource.IsCloudAnalysisServices(this.Resource);
				string text = string.Empty;
				WindowsCredential windowsCredential = resourceCredentialCollection.OfType<WindowsCredential>().FirstOrDefault<WindowsCredential>();
				Func<IDisposable> func;
				if (windowsCredential != null)
				{
					if (flag)
					{
						throw DataSourceException.NewDataSourceError<Message0>(this.host, Strings.ServerDoesNotSupportWindowsCredentialTryMicrosoftAccount, this.Resource, null, null);
					}
					func = windowsCredential.GetImpersonationWrapper(this.host, this.Resource);
					text = windowsCredential.Username;
				}
				else
				{
					BasicAuthCredential basicAuthCredential = resourceCredentialCollection.OfType<BasicAuthCredential>().FirstOrDefault<BasicAuthCredential>();
					if (basicAuthCredential != null)
					{
						dbConnectionStringBuilder["UID"] = basicAuthCredential.Username;
						dbConnectionStringBuilder["PWD"] = basicAuthCredential.Password;
					}
					else
					{
						this.oauthCredential = resourceCredentialCollection.OfType<OAuthCredential>().FirstOrDefault<OAuthCredential>();
						if (this.oauthCredential == null)
						{
							throw DataSourceException.NewInvalidCredentialsError(this.host, this.Resource, null, null, null);
						}
						dbConnectionStringBuilder["PWD"] = this.accessTokenPlaceholder;
					}
					func = new Func<IDisposable>(CredentialExtensions.NullImpersonationWrapper);
				}
				ConnectionStringPropertiesAdornment connectionStringPropertiesAdornment = resourceCredentialCollection.OfType<ConnectionStringPropertiesAdornment>().FirstOrDefault<ConnectionStringPropertiesAdornment>();
				if (connectionStringPropertiesAdornment != null)
				{
					foreach (KeyValuePair<string, string> keyValuePair in connectionStringPropertiesAdornment.Properties.Where((KeyValuePair<string, string> kvp) => AnalysisServicesService.knownConnectionStringProperties.Contains(kvp.Key)))
					{
						dbConnectionStringBuilder[keyValuePair.Key] = keyValuePair.Value;
					}
				}
				dbConnectionStringBuilder["Mode"] = "Read";
				if (flag && AnalysisServicesResource.ShouldEnforceReadOnlyAccessMode(this.serverName))
				{
					dbConnectionStringBuilder["Access Mode"] = "readonly";
				}
				object obj;
				this.options.TryGetValue("SubQueries", out obj);
				dbConnectionStringBuilder["SubQueries"] = obj;
				dbConnectionStringBuilder["LocaleIdentifier"] = cultureInfo.LCID;
				int num;
				if (this.options.TryGetDurationAsSeconds("ConnectionTimeout", out num))
				{
					dbConnectionStringBuilder["Connect Timeout"] = num;
				}
				this.connectionString = dbConnectionStringBuilder.ToString();
				this.connectionFactory = this.host.Hook(() => new AnalysisServicesConnectionFactory());
				this.connectionFactory = new AnalysisServicesPoolingConnectionFactory(text, this.connectionFactory, AnalysisServicesService.ConnectionPool);
				this.connectionFactory = new AnalysisServicesImpersonatingConnectionFactory(this.host, this.connectionFactory, func, this.Server, this.Resource);
				this.connectionFactory = new AnalysisServicesTracingConnectionFactory(this.host, this.connectionFactory, this.Resource);
			}
		}

		// Token: 0x060066FF RID: 26367 RVA: 0x00162C48 File Offset: 0x00160E48
		private IDataReaderWithTableSchema GetDataReader(string commandText, KeyValuePair<string, object>[] parameters, RowRange range, Func<RowRange, string> getRangedCommandText)
		{
			IAnalysisServicesConnection connection = null;
			IAnalysisServicesCommand command = null;
			IDataReaderWithTableSchema dataReaderWithTableSchema2;
			try
			{
				connection = this.CreateConnection();
				connection.Open();
				command = connection.CreateCommand();
				if (getRangedCommandText != null)
				{
					command.CommandText = getRangedCommandText(range);
				}
				else
				{
					command.CommandText = commandText;
				}
				foreach (KeyValuePair<string, object> keyValuePair in parameters)
				{
					command.AddParameter(keyValuePair.Key, keyValuePair.Value);
				}
				object obj;
				if (this.options.TryGetValue("CommandTimeout", out obj) && obj is TimeSpan)
				{
					command.CommandTimeout = (int)Math.Round(((TimeSpan)obj).TotalSeconds);
				}
				IDataReaderWithTableSchema dataReaderWithTableSchema = command.ExecuteReader().WithTableSchema();
				if (getRangedCommandText == null)
				{
					dataReaderWithTableSchema = new SkipTakeDataReader(dataReaderWithTableSchema, range);
				}
				dataReaderWithTableSchema2 = dataReaderWithTableSchema.AfterDispose(delegate
				{
					command.Dispose();
					connection.Dispose();
				});
			}
			catch (Exception)
			{
				if (connection != null)
				{
					connection.Dispose();
				}
				if (command != null)
				{
					command.Dispose();
				}
				throw;
			}
			return dataReaderWithTableSchema2;
		}

		// Token: 0x06006700 RID: 26368 RVA: 0x00162D98 File Offset: 0x00160F98
		private IList<AnalysisServicesCatalog> GetCatalogs()
		{
			List<AnalysisServicesCatalog> list = new List<AnalysisServicesCatalog>();
			using (IDataReader dataReader = this.ExecuteCommand(this.MetadataCache, CommandBehavior.Default, "select [CATALOG_NAME] from $system.DBSCHEMA_CATALOGS", Array.Empty<KeyValuePair<string, object>>()))
			{
				while (dataReader.Read())
				{
					string text = (string)dataReader["CATALOG_NAME"];
					list.Add(this.GetCatalog(text));
				}
			}
			return list;
		}

		// Token: 0x06006701 RID: 26369 RVA: 0x00162E08 File Offset: 0x00161008
		private static string GetCacheKey(CommandBehavior behavior, string connectionString, string commandText, KeyValuePair<string, object>[] parameters, RowRange range)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(behavior.ToString());
			stringBuilder.Append("/");
			stringBuilder.Append(connectionString);
			stringBuilder.Append("/");
			stringBuilder.Append(commandText);
			foreach (KeyValuePair<string, object> keyValuePair in parameters)
			{
				stringBuilder.Append("/");
				stringBuilder.Append(keyValuePair.Value);
			}
			stringBuilder.Append("/");
			stringBuilder.Append(range.SkipCount.ToString());
			stringBuilder.Append("/");
			stringBuilder.Append(range.TakeCount.ToString());
			return AnalysisServicesService.AnalysisServicesCacheKey.Qualify("ExecuteQuery", stringBuilder.ToString());
		}

		// Token: 0x04003898 RID: 14488
		private const int pageSize = 4096;

		// Token: 0x04003899 RID: 14489
		public static readonly IPool ConnectionPool = new TimeToLivePool(new Pool(), TimeSpan.FromMinutes(1.0));

		// Token: 0x0400389A RID: 14490
		private static readonly Version sqlServer2012 = new Version(11, 0, 0, 0);

		// Token: 0x0400389B RID: 14491
		private static readonly string[] knownConnectionStringProperties = new string[] { "EffectiveUserName", "CustomData" };

		// Token: 0x0400389C RID: 14492
		private readonly IEngineHost host;

		// Token: 0x0400389D RID: 14493
		private string connectionString;

		// Token: 0x0400389E RID: 14494
		private OAuthCredential oauthCredential;

		// Token: 0x0400389F RID: 14495
		private IAnalysisServicesConnectionFactory connectionFactory;

		// Token: 0x040038A0 RID: 14496
		private readonly IHostProgress hostProgress;

		// Token: 0x040038A1 RID: 14497
		private readonly string serverName;

		// Token: 0x040038A2 RID: 14498
		private readonly string catalogName;

		// Token: 0x040038A3 RID: 14499
		private readonly OptionsRecord options;

		// Token: 0x040038A4 RID: 14500
		private readonly string accessTokenPlaceholder;

		// Token: 0x040038A5 RID: 14501
		private IList<AnalysisServicesCatalog> catalogs;

		// Token: 0x040038A6 RID: 14502
		private bool? isInTabularMode;

		// Token: 0x040038A7 RID: 14503
		private IResource resource;

		// Token: 0x040038A8 RID: 14504
		private IPersistentCache dataCache;

		// Token: 0x040038A9 RID: 14505
		private IPersistentCache metadataCache;
	}
}
