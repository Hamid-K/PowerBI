using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AnalysisServices;
using Microsoft.AnalysisServices.AdomdClient;
using Microsoft.AnalysisServices.Azure.Common;
using Microsoft.AnalysisServices.Tabular;
using Microsoft.BIServer.HostingEnvironment;
using Microsoft.Data.Mashup;
using Microsoft.Mashup.Engine.Host;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.PowerBI.ReportServer.AsServer.Artifacts;
using Microsoft.PowerBI.ReportServer.AsServer.ProviderManager;
using Microsoft.PowerBI.ReportServer.PbixLib.Parsing;

namespace Microsoft.PowerBI.ReportServer.AsServer
{
	// Token: 0x02000005 RID: 5
	public sealed class AnalysisServicesServer : IAnalysisServicesServer
	{
		// Token: 0x06000008 RID: 8 RVA: 0x000024A6 File Offset: 0x000006A6
		public AnalysisServicesServer(AnalysisServicesSettings settings)
			: this(new AnalysisServicesFactory(), settings)
		{
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000024B4 File Offset: 0x000006B4
		internal AnalysisServicesServer(IAnalysisServicesFactory analysisServicesServerFactory, AnalysisServicesSettings settings)
		{
			this._serverFactory = analysisServicesServerFactory;
			this._settings = settings;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000024E0 File Offset: 0x000006E0
		public async Task<LoadDatabaseResult> LoadDatabaseAsync(string databaseName, Lazy<Stream> dataModel, string requestId, string clientSessionId)
		{
			LoadDatabaseResult loadDatabaseResult;
			using (ScopeMeter.Use(new string[] { "AS", "Load" }))
			{
				using (TOMWrapper asWrapper = this._serverFactory.CreateASWrapper(this._settings))
				{
					loadDatabaseResult = await this.QueueLoadDatabaseAsync(databaseName, dataModel, requestId, clientSessionId, asWrapper);
				}
			}
			return loadDatabaseResult;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002548 File Offset: 0x00000748
		public async Task<LoadDatabaseResult> LoadDatabaseForRefreshAsync(string databaseName, Stream dataModel, string requestId, string clientSessionId)
		{
			Lazy<Stream> lazy = new Lazy<Stream>(() => dataModel);
			LoadDatabaseResult loadDatabaseResult = await this.LoadDatabaseAsync(databaseName, lazy, requestId, clientSessionId);
			using (ScopeMeter.Use(new string[] { "AS", "Refresh" }))
			{
				AnalysisServicesServer.ActiveModel activeModel;
				if (this._activeModels.TryGetValue(loadDatabaseResult.DatabaseId, out activeModel))
				{
					activeModel.IsRefreshInProgress = true;
				}
			}
			return loadDatabaseResult;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000025B0 File Offset: 0x000007B0
		public async Task<LoadDatabaseResult> LoadDatabaseForExecutionAsync(IPrincipal readerUser, string databaseName, Lazy<Stream> dataModel, string requestId, string clientSessionId, IEnumerable<PbixDataSource> dataSources, IEnumerable<PbixModelParameter> parameters)
		{
			LoadDatabaseResult loadDatabaseResult2;
			using (TOMWrapper asWrapper = this._serverFactory.CreateASWrapper(this._settings))
			{
				LoadDatabaseResult loadDatabaseResult = await this.QueueLoadDatabaseAsync(databaseName, dataModel, requestId, clientSessionId, asWrapper);
				using (ScopeMeter.Use(new string[] { "AS", "Load", "Execution" }))
				{
					bool flag;
					if (dataSources == null)
					{
						flag = false;
					}
					else
					{
						flag = dataSources.All((PbixDataSource p) => p.Type == AccessType.DirectQuery);
					}
					bool flag2 = flag;
					if (parameters != null && (flag2 || !loadDatabaseResult.Loaded))
					{
						this.SetModelParametersInternal(asWrapper, databaseName, parameters.ToArray<PbixModelParameter>());
					}
					if (flag2)
					{
						this.SetCredentials(asWrapper, databaseName, dataSources);
					}
					if (readerUser != null && this._settings.isWinUser && this.IsDatabaseDirectQuery(databaseName))
					{
						this.SafeAddUserToRole(databaseName, asWrapper, readerUser.Identity.Name, "RSReaderRole");
					}
				}
				loadDatabaseResult2 = loadDatabaseResult;
			}
			return loadDatabaseResult2;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002634 File Offset: 0x00000834
		private void SafeAddUserToRole(string databaseName, TOMWrapper asWrapper, string identityName, string roleName)
		{
			try
			{
				asWrapper.AddUserToRole(roleName, identityName, databaseName);
			}
			catch (Exception)
			{
				asWrapper.Refresh(true);
				if (!asWrapper.UserExistInRole(roleName, identityName, databaseName))
				{
					throw;
				}
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002678 File Offset: 0x00000878
		private void SetCredentials(TOMWrapper asWrapper, string databaseName, IEnumerable<PbixDataSource> dataSources)
		{
			using (ScopeMeter.Use(new string[] { "AS", "SetCredentials" }))
			{
				foreach (ProviderDataSource providerDataSource in asWrapper.GetProviderDataSources(databaseName))
				{
					IProviderManager providerManager = ProviderResolver.ResolveProvider(providerDataSource);
					ProviderDataSourceCredentials providerDataSourceCredentials = ((providerManager == null) ? null : providerManager.CreateCredentials(providerDataSource, dataSources));
					if (providerDataSourceCredentials != null)
					{
						this.UpdateProviderCredentials(providerDataSource, providerDataSourceCredentials);
						asWrapper.SaveChanges(databaseName);
					}
				}
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000271C File Offset: 0x0000091C
		public void RemoveCredentials(string databaseName)
		{
			using (ScopeMeter.Use(new string[] { "AS", "RemoveCredentials" }))
			{
				using (TOMWrapper tomwrapper = this._serverFactory.CreateASWrapper(this._settings))
				{
					foreach (ProviderDataSource providerDataSource in tomwrapper.GetProviderDataSources(databaseName))
					{
						IProviderManager providerManager = ProviderResolver.ResolveProvider(providerDataSource);
						ProviderDataSourceCredentials providerDataSourceCredentials = ((providerManager == null) ? null : providerManager.RemoveCredentials(providerDataSource));
						this.UpdateProviderCredentials(providerDataSource, providerDataSourceCredentials);
					}
					tomwrapper.SaveChanges(databaseName);
				}
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000027E8 File Offset: 0x000009E8
		private void UpdateProviderCredentials(ProviderDataSource providerDataSource, ProviderDataSourceCredentials credentials)
		{
			if (credentials == null)
			{
				return;
			}
			if (credentials.ConnectionString != null)
			{
				providerDataSource.ConnectionString = credentials.ConnectionString;
			}
			if (credentials.Account != null)
			{
				providerDataSource.Account = credentials.Account;
			}
			if (credentials.Password != null)
			{
				providerDataSource.Password = credentials.Password;
			}
			if (credentials.Provider != null)
			{
				providerDataSource.Provider = credentials.Provider;
			}
			if (credentials.ImpersonationMode != null)
			{
				providerDataSource.ImpersonationMode = credentials.ImpersonationMode.Value;
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002870 File Offset: 0x00000A70
		public string GetConnectionString(long modelId, Guid catalogId)
		{
			string text = string.Empty;
			if (!this._activeModels.ContainsKey(modelId))
			{
				text = this.GetMostRecentDatabaseNameFromAS(catalogId, DateTime.MinValue);
				if (string.IsNullOrEmpty(text))
				{
					throw new AsConnectionException(string.Format("Failed to get connection string, databaseName is null or empty. modelId:{0}, catalogId: {1}", modelId, catalogId), AsConnectionExceptionErrorCode.LostConnection);
				}
				this._activeModels[modelId] = new AnalysisServicesServer.ActiveModel(text);
			}
			else
			{
				text = this._activeModels[modelId].DatabaseName;
			}
			return string.Format("Data Source={0}:{1};initial Catalog={2}", this._settings.ServerAddress, this._settings.Port, text);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002910 File Offset: 0x00000B10
		public string ResolveDatabaseName(long databaseId)
		{
			string text;
			using (TOMWrapper tomwrapper = this._serverFactory.CreateASWrapper(this._settings))
			{
				Database database = tomwrapper.GetDatabases().Find(databaseId.ToString());
				if (database != null)
				{
					text = database.Name;
				}
				else
				{
					AnalysisServicesServer.ActiveModel activeModel;
					this._activeModels.TryRemove(databaseId, out activeModel);
					text = string.Empty;
				}
			}
			return text;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002980 File Offset: 0x00000B80
		public string BuildDatabaseName(Guid catalogId, DateTime lastModified)
		{
			return catalogId.ToString() + "_" + lastModified.GetHashCode();
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000029A8 File Offset: 0x00000BA8
		public string GetMostRecentDatabaseName(Guid catalogId, DateTime lastModified, out bool wasInMemory)
		{
			wasInMemory = true;
			string text = this.GetMostRecentDatabaseNameFromAS(catalogId, lastModified);
			if (string.IsNullOrEmpty(text))
			{
				text = this.BuildDatabaseName(catalogId, lastModified);
				wasInMemory = false;
			}
			return text;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000029D8 File Offset: 0x00000BD8
		private string GetMostRecentDatabaseNameFromAS(Guid catalogId, DateTime lastModified)
		{
			string text = string.Empty;
			DateTime dateTime = DateTime.MaxValue;
			Database mostRecentDatabaseWithPrefix = this.GetMostRecentDatabaseWithPrefix(catalogId.ToString());
			if (mostRecentDatabaseWithPrefix != null)
			{
				text = mostRecentDatabaseWithPrefix.Name;
				dateTime = this.Max<DateTime>(mostRecentDatabaseWithPrefix.LastUpdate, mostRecentDatabaseWithPrefix.LastSchemaUpdate);
			}
			if (lastModified > dateTime)
			{
				text = string.Empty;
			}
			return text;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002A34 File Offset: 0x00000C34
		public bool IsDatabaseDirectQuery(string databaseName)
		{
			AnalysisServicesServer.<>c__DisplayClass27_0 CS$<>8__locals1 = new AnalysisServicesServer.<>c__DisplayClass27_0();
			CS$<>8__locals1.databaseName = databaseName;
			bool flag;
			using (TOMWrapper asWrapper = this._serverFactory.CreateASWrapper(this._settings))
			{
				IReadOnlyDictionary<string, IEnumerable<Partition>> tablePartitions = asWrapper.GetTablePartitions(CS$<>8__locals1.databaseName);
				if (tablePartitions.Values.Any<IEnumerable<Partition>>())
				{
					if (tablePartitions.Values.All((IEnumerable<Partition> p) => p.Count<Partition>() == 0))
					{
						return true;
					}
				}
				flag = (from p in tablePartitions.Values.SelectMany((IEnumerable<Partition> p) => p)
					select p.Mode).Any((ModeType p) => p == ModeType.DirectQuery || (p == ModeType.Default && asWrapper.GetDefaultDatabaseMode(CS$<>8__locals1.databaseName) == ModeType.DirectQuery));
			}
			return flag;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002B50 File Offset: 0x00000D50
		private List<ASConnectionInfo> CollectDataSourcesForPostV1FormatModels(IDataSourceReader server, string databaseName, string databaseEngineFriendlyFullName)
		{
			List<ASConnectionInfo> list = new List<ASConnectionInfo>();
			string text = "<Batch Transaction=\"false\" xmlns=\"http://schemas.microsoft.com/analysisservices/2003/engine\">\r\n                    <Discover xmlns=\"urn:schemas-microsoft-com:xml-analysis\">\r\n                      <RequestType>DISCOVER_POWERBI_DATASOURCES</RequestType>\r\n                      <Restrictions>\r\n                        <RestrictionList />\r\n                      </Restrictions>\r\n                      <Properties>\r\n                        <PropertyList>\r\n                        <ImpactAnalysis>{{FOR_MODEL_PUBLISHING_FLAG}}</ImpactAnalysis>\r\n                        </PropertyList>\r\n                      </Properties>\r\n                    </Discover>\r\n                  </Batch>".Replace("{{FOR_MODEL_PUBLISHING_FLAG}}", "0");
			using (IDataResultReader dataResultReader = server.ExecuteReader(text))
			{
				DataSet dataSet = dataResultReader.Evaluate();
				bool flag;
				if (dataSet == null)
				{
					flag = true;
				}
				else
				{
					DataTableCollection tables = dataSet.Tables;
					flag = ((tables != null) ? new int?(tables.Count) : null) != 1;
				}
				if (flag)
				{
					throw new DataSourceDiscoveryException(databaseEngineFriendlyFullName, "Connection string discovery: table collection unexpected.");
				}
				DataTable dataTable = dataSet.Tables[0];
				if (dataTable.Columns[AnalysisServicesServer.ColumnIndexKeyID] == null || dataTable.Columns[AnalysisServicesServer.ColumnIndexKeyConnectionString] == null || dataTable.Columns[AnalysisServicesServer.ColumnIndexKeyProvider] == null || dataTable.Columns[AnalysisServicesServer.ColumnIndexKeyName] == null)
				{
					throw new DataSourceDiscoveryException(databaseEngineFriendlyFullName, "Connection string discovery: table schema unexpected.");
				}
				int ordinal = dataTable.Columns[AnalysisServicesServer.ColumnIndexKeyID].Ordinal;
				int ordinal2 = dataTable.Columns[AnalysisServicesServer.ColumnIndexKeyConnectionString].Ordinal;
				int ordinal3 = dataTable.Columns[AnalysisServicesServer.ColumnIndexKeyProvider].Ordinal;
				int ordinal4 = dataTable.Columns[AnalysisServicesServer.ColumnIndexKeyName].Ordinal;
				foreach (object obj in dataTable.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					list.Add(this.AccumulateConnectionString((string)dataRow.ItemArray[ordinal], (string)dataRow.ItemArray[ordinal4], (string)dataRow.ItemArray[ordinal2], (string)dataRow.ItemArray[ordinal3]));
				}
			}
			return list;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002D58 File Offset: 0x00000F58
		private ASConnectionInfo AccumulateConnectionString(string dataSourceID, string dataSourceName, string connectionString, string provider)
		{
			DbConnectionStringBuilder dbConnectionStringBuilder = new DbConnectionStringBuilder();
			dbConnectionStringBuilder.ConnectionString = connectionString;
			if (string.IsNullOrEmpty(provider) && this.IsMashupDirectQueryConnection(dbConnectionStringBuilder[AnalysisServicesServer.ColumnIndexKeyProvider].ToString(), dbConnectionStringBuilder.ConnectionString) && "Microsoft.PowerBI.OleDb".Equals(dbConnectionStringBuilder[AnalysisServicesServer.ColumnIndexKeyProvider].ToString(), StringComparison.OrdinalIgnoreCase))
			{
				provider = "Microsoft.Mashup.OleDb.1";
				dbConnectionStringBuilder[AnalysisServicesServer.ColumnIndexKeyProvider] = "Microsoft.Mashup.OleDb.1";
			}
			return new ASConnectionInfo
			{
				DataSourceID = dataSourceID,
				DataSourceName = dataSourceName,
				ConnectionString = connectionString,
				ConnectionProvider = provider
			};
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002DF0 File Offset: 0x00000FF0
		private bool IsMashupDirectQueryConnection(string provider, string connectionString)
		{
			bool flag = false;
			if ("Microsoft.PowerBI.OleDb".Equals(provider, StringComparison.OrdinalIgnoreCase))
			{
				flag = !new DbConnectionStringBuilder
				{
					ConnectionString = connectionString
				}.ContainsKey("Location");
			}
			return flag;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002E28 File Offset: 0x00001028
		public IEnumerable<PbixDataSource> GetDataSources(string databaseName)
		{
			IEnumerable<PbixDataSource> dataSourcesInternal;
			using (TOMWrapper tomwrapper = this._serverFactory.CreateASWrapper(this._settings))
			{
				dataSourcesInternal = this.GetDataSourcesInternal(databaseName, tomwrapper);
			}
			return dataSourcesInternal;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002E70 File Offset: 0x00001070
		public IEnumerable<PbixModelParameter> GetModelParameters(string databaseName)
		{
			using (TOMWrapper asWrapper = this._serverFactory.CreateASWrapper(this._settings))
			{
				IEnumerable<KeyValuePair<string, string>> modelParameters = asWrapper.GetModelParameters(databaseName);
				foreach (KeyValuePair<string, string> keyValuePair in modelParameters)
				{
					string text;
					if (this.TryGetParameterValue(keyValuePair.Value, out text))
					{
						yield return new PbixModelParameter
						{
							Name = keyValuePair.Key,
							Value = text
						};
					}
				}
				IEnumerator<KeyValuePair<string, string>> enumerator = null;
			}
			TOMWrapper asWrapper = null;
			yield break;
			yield break;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002E88 File Offset: 0x00001088
		public bool TryGetParameterValue(string expression, out string result)
		{
			result = string.Empty;
			StringSegment literalToken = this.GetLiteralToken(expression);
			return Engines.Version1.TryParseString(literalToken.ToString(), out result);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002EBC File Offset: 0x000010BC
		public string ReplaceExpressionLiteral(string expression, string newLiteral)
		{
			return expression.Remove(0, this.GetLiteralToken(expression).Length).Insert(0, Engines.Version1.EscapeString(newLiteral));
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002EF0 File Offset: 0x000010F0
		private StringSegment GetLiteralToken(string expression)
		{
			ITokens tokens = Engines.Version1.Tokenize(expression);
			return tokens.GetText(tokens.GetToken(1));
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002F0C File Offset: 0x0000110C
		public IEnumerable<PbixDataSource> SetModelParameters(string databaseName, IEnumerable<PbixModelParameter> parameters)
		{
			IEnumerable<PbixDataSource> enumerable;
			using (TOMWrapper tomwrapper = this._serverFactory.CreateASWrapper(this._settings))
			{
				enumerable = this.SetModelParametersInternal(tomwrapper, databaseName, parameters);
			}
			return enumerable;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002F54 File Offset: 0x00001154
		private IEnumerable<PbixDataSource> SetModelParametersInternal(TOMWrapper asWrapper, string databaseName, IEnumerable<PbixModelParameter> parameters)
		{
			if (parameters != null && parameters.Any<PbixModelParameter>())
			{
				Dictionary<string, string> oldParametersFromAs = asWrapper.GetModelParameters(databaseName).ToDictionary((KeyValuePair<string, string> p) => p.Key, (KeyValuePair<string, string> p) => p.Value);
				foreach (PbixModelParameter pbixModelParameter in parameters)
				{
					if (!oldParametersFromAs.ContainsKey(pbixModelParameter.Name))
					{
						throw new InvalidModelParameterException(pbixModelParameter.Name);
					}
				}
				Dictionary<string, string> dictionary = parameters.ToDictionary((PbixModelParameter p) => p.Name, (PbixModelParameter q) => this.ReplaceExpressionLiteral(oldParametersFromAs[q.Name], q.Value));
				if (dictionary.Any((KeyValuePair<string, string> n) => oldParametersFromAs[n.Key] != n.Value))
				{
					asWrapper.SetModelParameters(databaseName, dictionary);
					asWrapper.SaveChanges(databaseName);
					string text = global::System.Diagnostics.Trace.CorrelationManager.ActivityId.ToString();
					Logger.Info(string.Format("SetModelParametersInteral: Successfully set and saved parameters for databaseName={0} (sessionId={1})", databaseName, text), Array.Empty<object>());
				}
				else
				{
					Logger.Info(string.Format("SetModelParametersInteral: Parameters for databaseName={0} have not changed", databaseName), Array.Empty<object>());
				}
			}
			return this.GetDataSourcesInternal(databaseName, asWrapper);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000030C8 File Offset: 0x000012C8
		private IEnumerable<PbixDataSource> GetDataSourcesInternal(string databaseName, TOMWrapper asWrapper)
		{
			List<PbixDataSource> list = new List<PbixDataSource>();
			bool flag = this.IsDatabaseDirectQuery(databaseName);
			foreach (ProviderDataSource providerDataSource in this.GetDataSourcesCollection(databaseName, asWrapper))
			{
				IProviderManager providerManager = ProviderResolver.ResolveProvider(providerDataSource);
				if (providerManager == null && flag)
				{
					this.ThrowUnsupportedProviderException(providerDataSource);
				}
				if (providerManager != null)
				{
					ProviderDataSourceInfo providerDataSourceInfo = new ProviderDataSourceInfo(providerDataSource, flag);
					IEnumerable<PbixDataSource> enumerable = providerManager.BuildDataModelDataSources(providerDataSourceInfo);
					if (!enumerable.Any<PbixDataSource>() && flag)
					{
						this.ThrowUnsupportedProviderException(providerDataSource);
					}
					list.AddRange(enumerable);
				}
			}
			return list.Distinct(new DataModelArtifactsProvider.PbixDataSourceModelComparer());
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00003178 File Offset: 0x00001378
		public string GetModelVersionName(string databaseName)
		{
			string text;
			using (TOMWrapper tomwrapper = this._serverFactory.CreateASWrapper(this._settings))
			{
				text = tomwrapper.GetPowerBIDataSourceVersion(databaseName).ToString();
			}
			return text;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000031CC File Offset: 0x000013CC
		private IEnumerable<ProviderDataSource> GetDataSourcesCollection(string databaseName, TOMWrapper asWrapper)
		{
			PowerBIDataSourceVersion powerBIDataSourceVersion = asWrapper.GetPowerBIDataSourceVersion(databaseName);
			string text = global::System.Diagnostics.Trace.CorrelationManager.ActivityId.ToString();
			Logger.Info(string.Format("GetDataSourcesCollection: sessionId={0}, databaseName={1}, PowerBIDataSourceVersion={2}", text, databaseName, powerBIDataSourceVersion.ToString()), Array.Empty<object>());
			if (powerBIDataSourceVersion != PowerBIDataSourceVersion.PowerBI_V1)
			{
				IDataSourceReader dataSourceReader = this._serverFactory.CreateASDatabaseWrapper(this._settings, databaseName);
				List<ASConnectionInfo> list = this.CollectDataSourcesForPostV1FormatModels(dataSourceReader, databaseName, null);
				List<ProviderDataSource> list2 = new List<ProviderDataSource>();
				foreach (ASConnectionInfo asconnectionInfo in list)
				{
					try
					{
						DataSourceReference dataSourceReference = new DataSourceReference(asconnectionInfo.DataSourceName);
						ProviderDataSource providerDataSource = new ProviderDataSource
						{
							Name = Sha256Hasher.GetSHA256Hash(dataSourceReference.DataSource.Path),
							ConnectionString = asconnectionInfo.ConnectionString,
							Provider = asconnectionInfo.ConnectionProvider
						};
						list2.Add(providerDataSource);
					}
					catch (NotSupportedException ex)
					{
						Logger.Warning(string.Format("Database name {0} have unsupported datasources, error: {1}. The report will not be refresheable", databaseName, ex.Message), Array.Empty<object>());
					}
				}
				return list2;
			}
			return asWrapper.GetProviderDataSources(databaseName);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000330C File Offset: 0x0000150C
		public IEnumerable<PbixModelRole> GetModelRoles(string databaseName)
		{
			using (TOMWrapper asWrapper = this._serverFactory.CreateASWrapper(this._settings))
			{
				foreach (ModelRole modelRole in asWrapper.GetModelRoles(databaseName))
				{
					Microsoft.AnalysisServices.Tabular.Annotation annotation = modelRole.Annotations.Find("PBI_Id");
					if (annotation != null)
					{
						string value = annotation.Value;
						yield return new PbixModelRole
						{
							ModelRoleId = Guid.Parse(value),
							ModelRoleName = modelRole.Name
						};
					}
				}
				IEnumerator<ModelRole> enumerator = null;
			}
			TOMWrapper asWrapper = null;
			yield break;
			yield break;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00003323 File Offset: 0x00001523
		private void ThrowUnsupportedProviderException(ProviderDataSource providerDataSource)
		{
			throw new UnsupportedProviderException(string.Format("This datasource contains an unsupported provider: DataSource: {0}, Provider: {1}, ConnectionString: {2}", providerDataSource.Name, providerDataSource.Provider ?? "(null)", providerDataSource.ConnectionString ?? "(null"));
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00003358 File Offset: 0x00001558
		private Task<LoadDatabaseResult> QueueLoadDatabaseAsync(string databaseName, Lazy<Stream> dataModel, string requestId, string clientSessionId, TOMWrapper asWrapper)
		{
			Task<LoadDatabaseResult> orAdd = this._loadInProgress.GetOrAdd(databaseName, (string s) => this.LoadDatabaseInAsAsync(databaseName, dataModel, requestId, clientSessionId, asWrapper));
			orAdd.ContinueWith<LoadDatabaseResult>(delegate(Task<LoadDatabaseResult> finishedJob)
			{
				Task<LoadDatabaseResult> task;
				this._loadInProgress.TryRemove(databaseName, out task);
				return finishedJob.Result;
			});
			return orAdd;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000033C7 File Offset: 0x000015C7
		private Task<LoadDatabaseResult> LoadDatabaseInAsAsync(string databaseName, Lazy<Stream> dataModel, string requestId, string clientSessionId, TOMWrapper asWrapper)
		{
			return Task.Run<LoadDatabaseResult>(delegate
			{
				if (!string.IsNullOrEmpty(clientSessionId))
				{
					global::System.Diagnostics.Trace.CorrelationManager.ActivityId = new Guid(clientSessionId);
					Logger.Verbose(string.Format("New clientSessionId generated: clientSessionId={0}", clientSessionId), Array.Empty<object>());
				}
				asWrapper.Refresh(false);
				bool flag = asWrapper.GetDatabases().ContainsName(databaseName);
				LoadDatabaseResult loadDatabaseResult;
				try
				{
					long num;
					if (!flag)
					{
						num = this.GetNumericalModelId(databaseName);
						asWrapper.ImageLoad(databaseName, num.ToString(), dataModel.Value);
					}
					else
					{
						num = long.Parse(asWrapper.GetDatabases().FindByName(databaseName).ID);
					}
					this.EnsureDbLoaded(databaseName, asWrapper);
					Logger.Info(string.Format("LoadDatabaseAsync: Successfully EnsureDbLoaded clientSessionId={0}, databaseName={1}, modelId={2}, alreadyLoaded={3}", new object[] { clientSessionId, databaseName, num, flag }), Array.Empty<object>());
					if (!flag)
					{
						asWrapper.RemoveRoleIfExists("RSReaderRole", databaseName);
					}
					asWrapper.AddRole("RSReaderRole", databaseName);
					loadDatabaseResult = new LoadDatabaseResult
					{
						Loaded = flag,
						DatabaseId = num
					};
				}
				catch (Exception ex)
				{
					AsConnectionExceptionErrorCode asConnectionExceptionErrorCode = AsConnectionExceptionErrorCode.Unknown;
					if (ex is OperationException)
					{
						if (ex.Message.ToLowerInvariant().Contains("not enough memory"))
						{
							asConnectionExceptionErrorCode = AsConnectionExceptionErrorCode.OutOfMemory;
						}
					}
					else if (ex is AdomdConnectionException || ex is ConnectionException)
					{
						asConnectionExceptionErrorCode = AsConnectionExceptionErrorCode.LostConnection;
					}
					throw new AsConnectionException(string.Format("Failed to publish model for {0}", databaseName), asConnectionExceptionErrorCode, ex);
				}
				return loadDatabaseResult;
			});
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00003404 File Offset: 0x00001604
		private void EnsureDbLoaded(string databaseName, TOMWrapper wrapper)
		{
			try
			{
				wrapper.Refresh(false);
			}
			catch (AdomdException ex)
			{
				throw new AsConnectionException(string.Format("Failed to refresh models for {0}", databaseName), AsConnectionExceptionErrorCode.LostConnection, ex);
			}
			if (wrapper.GetDatabases().ContainsName(databaseName))
			{
				Database database = wrapper.GetDatabases().FindByName(databaseName);
				this._activeModels.TryAdd(long.Parse(database.ID), new AnalysisServicesServer.ActiveModel(databaseName));
				return;
			}
			throw new AsConnectionException(string.Format("Failed to publish data model for modelId:{0}", databaseName), AsConnectionExceptionErrorCode.Unknown);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00003488 File Offset: 0x00001688
		public void DeleteDatabase(long databaseId)
		{
			using (TOMWrapper tomwrapper = this._serverFactory.CreateASWrapper(this._settings))
			{
				try
				{
					tomwrapper.DeleteDatabase(databaseId.ToString());
					Logger.Info(string.Format("DeleteDatabase: Successfully deleted model with databaseId={0}", databaseId.ToString()), Array.Empty<object>());
				}
				catch (AdomdException ex)
				{
					throw new AsConnectionException(string.Format("Failed to delete model for {0}", databaseId), AsConnectionExceptionErrorCode.LostConnection, ex);
				}
				catch (Exception ex2)
				{
					throw new AsConnectionException(string.Format("Failed to delete model for {0}", databaseId), AsConnectionExceptionErrorCode.Unknown, ex2);
				}
				finally
				{
					AnalysisServicesServer.ActiveModel activeModel;
					bool flag = this._activeModels.TryRemove(databaseId, out activeModel);
					Logger.Info(string.Format("RemoveActiveDatabase: DeleteDatabase attempt to remove cached DB from activeModels success={0}, databaseId={1}", flag, databaseId.ToString()), Array.Empty<object>());
				}
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00003574 File Offset: 0x00001774
		public void RefreshDatabase(string databaseName, IEnumerable<PbixDataSource> dataSources, IEnumerable<PbixModelParameter> parameters, string clientSessionId)
		{
			using (TOMWrapper tomwrapper = this._serverFactory.CreateASWrapperUsingTimeout(this._settings))
			{
				if (!string.IsNullOrEmpty(clientSessionId))
				{
					global::System.Diagnostics.Trace.CorrelationManager.ActivityId = new Guid(clientSessionId);
				}
				Database database = tomwrapper.GetDatabases().FindByName(databaseName);
				if (parameters != null)
				{
					this.SetModelParametersInternal(tomwrapper, databaseName, parameters.ToArray<PbixModelParameter>());
				}
				if (dataSources != null)
				{
					Model model = database.Model;
					if (!dataSources.All((PbixDataSource p) => p.Type == AccessType.Import))
					{
						throw new InvalidDataSourceException("All datasources must be import and have credentials set for data refresh.");
					}
					if (model == null || model.DefaultPowerBIDataSourceVersion != PowerBIDataSourceVersion.PowerBI_V3)
					{
						this.SetCredentials(tomwrapper, database.Name, dataSources);
					}
				}
				try
				{
					this.SetRefreshIsInProgress(databaseName, true);
					tomwrapper.RefreshModel(database);
				}
				finally
				{
					this.SetRefreshIsInProgress(databaseName, false);
				}
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00003668 File Offset: 0x00001868
		private void SetRefreshIsInProgress(string databaseName, bool isRefreshActive)
		{
			long numericalModelId = this.GetNumericalModelId(databaseName);
			AnalysisServicesServer.ActiveModel activeModel;
			if (this._activeModels.TryGetValue(numericalModelId, out activeModel))
			{
				activeModel.IsRefreshInProgress = isRefreshActive;
				string text = global::System.Diagnostics.Trace.CorrelationManager.ActivityId.ToString();
				Logger.Info(string.Format("Model Refresh: sessionId={0}, isRefreshActive={1}, incoming databaseName={2}, databaseId={3}, activeModel.DatabaseName={4}", new object[]
				{
					text,
					isRefreshActive,
					databaseName,
					numericalModelId.ToString(),
					activeModel.DatabaseName
				}), Array.Empty<object>());
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000036EB File Offset: 0x000018EB
		private long GetNumericalModelId(string databaseName)
		{
			return (long)Math.Abs(databaseName.GetHashCode());
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600002D RID: 45 RVA: 0x000036F9 File Offset: 0x000018F9
		public AnalysisServicesSettings Settings
		{
			get
			{
				return this._settings;
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00003704 File Offset: 0x00001904
		public bool IsDatabaseLoaded(string databaseName)
		{
			bool flag;
			using (TOMWrapper tomwrapper = this._serverFactory.CreateASWrapper(this._settings))
			{
				tomwrapper.Refresh(false);
				flag = tomwrapper.GetDatabases().ContainsName(databaseName);
			}
			return flag;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00003754 File Offset: 0x00001954
		public void SaveDatabaseToStream(string databaseName, Stream targetDbStream)
		{
			using (TOMWrapper tomwrapper = this._serverFactory.CreateASWrapper(this._settings))
			{
				string id = tomwrapper.GetDatabases().FindByName(databaseName).ID;
				tomwrapper.ImageSave(id, targetDbStream);
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000037AC File Offset: 0x000019AC
		public DateTime? GetDatabaseLastProcessed(string databaseName)
		{
			Database mostRecentDatabaseWithPrefix = this.GetMostRecentDatabaseWithPrefix(databaseName);
			if (mostRecentDatabaseWithPrefix != null)
			{
				return new DateTime?(mostRecentDatabaseWithPrefix.LastProcessed);
			}
			return null;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000037DC File Offset: 0x000019DC
		private Database GetMostRecentDatabaseWithPrefix(string databaseNamePrefix)
		{
			Database database;
			using (TOMWrapper tomwrapper = this._serverFactory.CreateASWrapper(this._settings))
			{
				DatabaseCollection databases = tomwrapper.GetDatabases();
				if (databases.Count == 0)
				{
					database = null;
				}
				else
				{
					database = (from db in databases.OfType<Database>()
						where db.Name.StartsWith(databaseNamePrefix, StringComparison.OrdinalIgnoreCase)
						orderby db.LastSchemaUpdate descending, db.LastProcessed descending
						select db).FirstOrDefault<Database>();
				}
			}
			return database;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000038A0 File Offset: 0x00001AA0
		private T Max<T>(T first, T second) where T : IComparable
		{
			if (first != null && first.CompareTo(second) > 0)
			{
				return first;
			}
			return second;
		}

		// Token: 0x04000030 RID: 48
		private const string MashupProvider1 = "Microsoft.PowerBI.OleDb";

		// Token: 0x04000031 RID: 49
		private const string MashupProvider2 = "Microsoft.Mashup.OleDb.1";

		// Token: 0x04000032 RID: 50
		private const string ConnectionStringPQParamLocation = "Location";

		// Token: 0x04000033 RID: 51
		private static readonly string ColumnIndexKeyID = "ID".ToUpperInvariant();

		// Token: 0x04000034 RID: 52
		private static readonly string ColumnIndexKeyName = "Name".ToUpperInvariant();

		// Token: 0x04000035 RID: 53
		private static readonly string ColumnIndexKeyProvider = "Provider".ToUpperInvariant();

		// Token: 0x04000036 RID: 54
		private static readonly string ColumnIndexKeyConnectionString = "ConnectionString".ToUpperInvariant();

		// Token: 0x04000037 RID: 55
		private const string PBIRoleIdAnnotationName = "PBI_Id";

		// Token: 0x04000038 RID: 56
		private readonly IAnalysisServicesFactory _serverFactory;

		// Token: 0x04000039 RID: 57
		private readonly ConcurrentDictionary<long, AnalysisServicesServer.ActiveModel> _activeModels = new ConcurrentDictionary<long, AnalysisServicesServer.ActiveModel>();

		// Token: 0x0400003A RID: 58
		private readonly ConcurrentDictionary<string, Task<LoadDatabaseResult>> _loadInProgress = new ConcurrentDictionary<string, Task<LoadDatabaseResult>>();

		// Token: 0x0400003B RID: 59
		private readonly AnalysisServicesSettings _settings;

		// Token: 0x0400003C RID: 60
		public const string DiscoverPostV1DataSourceXmlaTemplate = "<Batch Transaction=\"false\" xmlns=\"http://schemas.microsoft.com/analysisservices/2003/engine\">\r\n                    <Discover xmlns=\"urn:schemas-microsoft-com:xml-analysis\">\r\n                      <RequestType>DISCOVER_POWERBI_DATASOURCES</RequestType>\r\n                      <Restrictions>\r\n                        <RestrictionList />\r\n                      </Restrictions>\r\n                      <Properties>\r\n                        <PropertyList>\r\n                        <ImpactAnalysis>{{FOR_MODEL_PUBLISHING_FLAG}}</ImpactAnalysis>\r\n                        </PropertyList>\r\n                      </Properties>\r\n                    </Discover>\r\n                  </Batch>";

		// Token: 0x0400003D RID: 61
		public const string FOR_MODEL_PUBLISHING_FLAG = "{{FOR_MODEL_PUBLISHING_FLAG}}";

		// Token: 0x02000038 RID: 56
		private class ActiveModel
		{
			// Token: 0x06000122 RID: 290 RVA: 0x000056C6 File Offset: 0x000038C6
			public ActiveModel(string databaseName)
			{
				this.DatabaseName = databaseName;
				this.IsRefreshInProgress = false;
			}

			// Token: 0x1700002D RID: 45
			// (get) Token: 0x06000123 RID: 291 RVA: 0x000056DC File Offset: 0x000038DC
			// (set) Token: 0x06000124 RID: 292 RVA: 0x000056E4 File Offset: 0x000038E4
			public string DatabaseName { get; set; }

			// Token: 0x1700002E RID: 46
			// (get) Token: 0x06000125 RID: 293 RVA: 0x000056ED File Offset: 0x000038ED
			// (set) Token: 0x06000126 RID: 294 RVA: 0x000056F5 File Offset: 0x000038F5
			public bool IsRefreshInProgress { get; set; }
		}
	}
}
