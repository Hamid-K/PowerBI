using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using Microsoft.Data.Serialization;
using Microsoft.Internal;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Engine1.DataSourceReference;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Mdx;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.SapBusinessWarehouse
{
	// Token: 0x020004E7 RID: 1255
	internal abstract class SapBwService : ISapBwService
	{
		// Token: 0x060028F6 RID: 10486 RVA: 0x0007A31C File Offset: 0x0007851C
		protected SapBwService(IEngineHost host, IResource resource, SapBwOptions options, SapBwOlapDataSourceLocation location, SapBwRouterString routerString, string providerId)
		{
			this.host = host;
			this.location = location;
			this.routerString = routerString;
			this.resource = resource;
			this.tracer = new Tracer(host, "Engine/IO/SapBusinessWarehouse/" + providerId + "/", resource, null, null);
			this.providerId = providerId;
			this.authenticationProperties = new Dictionary<string, string>();
			this.options = options;
			ITracingService service = TracingService.GetService(host);
			if (service.Enabled() && service.TracePath != null && service.Options.IsEnabled("SapBwTracing"))
			{
				try
				{
					string text = Path.Combine(service.TracePath, "SapBw");
					Directory.CreateDirectory(text);
					this.driverTracePath = text;
				}
				catch (IOException)
				{
				}
				catch (UnauthorizedAccessException)
				{
				}
			}
		}

		// Token: 0x17000FCE RID: 4046
		// (get) Token: 0x060028F7 RID: 10487
		public abstract bool PreferTablesForMultipleHierarchyNodes { get; }

		// Token: 0x17000FCF RID: 4047
		// (get) Token: 0x060028F8 RID: 10488
		public abstract bool SupportsEnhancedMetadata { get; }

		// Token: 0x17000FD0 RID: 4048
		// (get) Token: 0x060028F9 RID: 10489
		public abstract bool MeasuresAsDbNull { get; }

		// Token: 0x17000FD1 RID: 4049
		// (get) Token: 0x060028FA RID: 10490
		public abstract bool SupportsColumnFolding { get; }

		// Token: 0x17000FD2 RID: 4050
		// (get) Token: 0x060028FB RID: 10491 RVA: 0x0007A3F4 File Offset: 0x000785F4
		public virtual bool ScaleMeasures
		{
			get
			{
				return this.options.ScaleMeasures;
			}
		}

		// Token: 0x17000FD3 RID: 4051
		// (get) Token: 0x060028FC RID: 10492 RVA: 0x0007A401 File Offset: 0x00078601
		public bool EnableStructures
		{
			get
			{
				return this.options.EnableStructures;
			}
		}

		// Token: 0x17000FD4 RID: 4052
		// (get) Token: 0x060028FD RID: 10493 RVA: 0x0007A40E File Offset: 0x0007860E
		public IResource Resource
		{
			get
			{
				return this.resource;
			}
		}

		// Token: 0x17000FD5 RID: 4053
		// (get) Token: 0x060028FE RID: 10494 RVA: 0x0007A416 File Offset: 0x00078616
		public IEngineHost Host
		{
			get
			{
				return this.host;
			}
		}

		// Token: 0x17000FD6 RID: 4054
		// (get) Token: 0x060028FF RID: 10495 RVA: 0x0007A41E File Offset: 0x0007861E
		public DbExceptionHandler ExceptionHandler
		{
			get
			{
				if (this.exceptionHandler == null)
				{
					this.exceptionHandler = new DbExceptionHandler(this.host, this.tracer, "SAP Business Warehouse", this.resource, (DbException e) => new DbExceptionInfo(this.host, e), false);
				}
				return this.exceptionHandler;
			}
		}

		// Token: 0x17000FD7 RID: 4055
		// (get) Token: 0x06002900 RID: 10496
		protected abstract ConnectionPoolingDbProviderFactory ProviderFactory { get; }

		// Token: 0x17000FD8 RID: 4056
		// (get) Token: 0x06002901 RID: 10497 RVA: 0x0007A45D File Offset: 0x0007865D
		private ResourceCredentialCollection Credentials
		{
			get
			{
				if (this.credentials == null)
				{
					this.credentials = HostResourcePermissionService.VerifyPermissionAndGetCredentials(this.host, this.resource, null);
				}
				return this.credentials;
			}
		}

		// Token: 0x17000FD9 RID: 4057
		// (get) Token: 0x06002902 RID: 10498 RVA: 0x0007A485 File Offset: 0x00078685
		public string ConnectionString
		{
			get
			{
				if (this.connectionString == null)
				{
					this.connectionString = this.GetConnectionString(null);
				}
				return this.connectionString;
			}
		}

		// Token: 0x17000FDA RID: 4058
		// (get) Token: 0x06002903 RID: 10499 RVA: 0x0007A4A2 File Offset: 0x000786A2
		protected string ConnectionStringCacheKey
		{
			get
			{
				if (this.connectionString == null)
				{
					this.connectionString = this.GetConnectionString(null);
				}
				return this.connectionStringCacheKey;
			}
		}

		// Token: 0x17000FDB RID: 4059
		// (get) Token: 0x06002904 RID: 10500 RVA: 0x0007A4C0 File Offset: 0x000786C0
		public string Language
		{
			get
			{
				string text;
				if (SapBwService.languageCodes.TryGetValue(this.options.GetSapLanguageCode(), out text))
				{
					return text;
				}
				return "E";
			}
		}

		// Token: 0x17000FDC RID: 4060
		// (get) Token: 0x06002905 RID: 10501 RVA: 0x0007A4ED File Offset: 0x000786ED
		public Tracer Tracer
		{
			get
			{
				return this.tracer;
			}
		}

		// Token: 0x06002906 RID: 10502
		protected abstract IDbConnection CreateAndOpenConnection(bool newConnection, string connectionString);

		// Token: 0x06002907 RID: 10503
		protected abstract DbConnection CreateConnection(bool newConnection, string connectionString);

		// Token: 0x06002908 RID: 10504
		public abstract IDataReaderWithTableSchema ExecuteMdx(string mdx, RowRange range, bool cacheResults, bool newConnection = false, object[][] columnInfos = null, string cubeName = null);

		// Token: 0x06002909 RID: 10505
		public abstract IDataReaderWithTableSchema ExtractMetadata(string bapiName, string bapiReturnTable, SapBwRestrictions restrictions);

		// Token: 0x0600290A RID: 10506
		public abstract bool TryExtractTable(string traceInfo, SapBwMetadataAstCreator astCreator, out IDataReaderWithTableSchema reader);

		// Token: 0x0600290B RID: 10507
		public abstract bool TryGetInfoObjectsDetail(string[] infoObjects, out IDataReaderWithTableSchema reader);

		// Token: 0x0600290C RID: 10508
		public abstract ILookup<string, SapBwVariable> GroupVariablesForAdditionalMetadata(SapBwMdxCube cube, Dictionary<string, SapBwVariable> variables);

		// Token: 0x0600290D RID: 10509 RVA: 0x0007A4F5 File Offset: 0x000786F5
		public virtual IEnumerable<MdxCellPropertyMetadata> GetCellProperties()
		{
			yield break;
		}

		// Token: 0x0600290E RID: 10510 RVA: 0x0007A500 File Offset: 0x00078700
		protected virtual void SetConnectionString(DbConnectionStringBuilder builder)
		{
			string text = ((this.location.LogonGroup == null) ? "ASHOST" : "MessageServer");
			builder[text] = this.location.Server;
			if (this.location.LogonGroup == null)
			{
				builder["SYSNR"] = this.location.SystemNumber;
			}
			else
			{
				builder["SystemID"] = this.location.SystemId;
				builder["LogonGroup"] = this.location.LogonGroup;
			}
			builder["CLIENT"] = this.location.ClientId;
			foreach (KeyValuePair<string, string> keyValuePair in this.authenticationProperties)
			{
				builder[keyValuePair.Key] = keyValuePair.Value;
			}
			if (this.providerId == "2.0")
			{
				builder["ProviderId"] = "2.0";
				if (this.routerString != null)
				{
					builder[text] = this.location.Server.Replace('/', '|');
				}
				this.connectionStringCacheKey = builder.ToString();
				if (this.routerString == null)
				{
					goto IL_0189;
				}
				builder.Remove(text);
				using (IEnumerator<KeyValuePair<string, string>> enumerator2 = this.routerString.GetConnectionStringProperties().GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						KeyValuePair<string, string> keyValuePair2 = enumerator2.Current;
						builder[keyValuePair2.Key] = keyValuePair2.Value;
					}
					goto IL_0189;
				}
			}
			this.connectionStringCacheKey = builder.ToString();
			IL_0189:
			if (this.driverTracePath != null)
			{
				builder["DEBUGDIRECTORY"] = this.driverTracePath;
			}
		}

		// Token: 0x0600290F RID: 10511 RVA: 0x0007A6CC File Offset: 0x000788CC
		private string GetConnectionString(string languageCode = null)
		{
			DbConnectionStringBuilder dbConnectionStringBuilder = this.ProviderFactory.CreateConnectionStringBuilder();
			dbConnectionStringBuilder["LANG"] = languageCode ?? this.options.GetSapLanguageCode();
			this.SetConnectionString(dbConnectionStringBuilder);
			return dbConnectionStringBuilder.ToString();
		}

		// Token: 0x06002910 RID: 10512 RVA: 0x0007A710 File Offset: 0x00078910
		protected Func<IDisposable> ProcessCredentialsAndGetImpersonation()
		{
			IResourceCredential resourceCredential;
			EncryptedConnectionAdornment encryptedConnectionAdornment;
			if (!this.Credentials.TryGetDatabasePattern(out resourceCredential, out encryptedConnectionAdornment) || encryptedConnectionAdornment.RequireEncryption)
			{
				throw DataSourceException.NewInvalidCredentialsError(this.host, this.resource, null, null, null);
			}
			WindowsCredential windowsCredential = this.Credentials.OfType<WindowsCredential>().FirstOrDefault<WindowsCredential>();
			if (windowsCredential != null)
			{
				ConnectionStringPropertiesAdornment connectionStringPropertiesAdornment = this.Credentials.OfType<ConnectionStringPropertiesAdornment>().FirstOrDefault<ConnectionStringPropertiesAdornment>();
				string text;
				if (connectionStringPropertiesAdornment.TryGetAdornment("SNCPartnerName", out text))
				{
					this.authenticationProperties["SNCPartnerName"] = text;
					string environmentVariable;
					if (connectionStringPropertiesAdornment.TryGetAdornment("SNCLibrary", out environmentVariable))
					{
						string text2 = environmentVariable.ToUpperInvariant();
						if (!(text2 == "NTLM"))
						{
							if (!(text2 == "KERBEROS"))
							{
								bool flag = false;
								try
								{
									flag = Path.IsPathRooted(environmentVariable);
								}
								catch (ArgumentException)
								{
								}
								if (!flag || !File.Exists(environmentVariable))
								{
									throw DataSourceException.NewInvalidCredentialsError(this.host, this.resource, Strings.SapBwWindowsAuthInvalidSncLibrary, Strings.SapBwWindowsAuthInvalidSncLibrary, null);
								}
								this.authenticationProperties["SNCLibrary"] = environmentVariable;
							}
							else
							{
								this.authenticationProperties["SSOType"] = "KERBEROS5";
							}
						}
						else
						{
							this.authenticationProperties["SSOType"] = "NTLM";
						}
					}
					else
					{
						string text3 = (X64Helper.Is64BitProcess ? "SNC_LIB_64" : "SNC_LIB");
						environmentVariable = Environment.GetEnvironmentVariable(text3);
						if (string.IsNullOrEmpty(environmentVariable))
						{
							throw DataSourceException.NewInvalidCredentialsError(this.host, this.resource, Strings.SapBwWindowsAuthInvalidSncLibraryEnvironment(text3), Strings.SapBwWindowsAuthInvalidSncLibraryEnvironment(text3), null);
						}
						this.authenticationProperties["SNCLibrary"] = environmentVariable;
					}
					return windowsCredential.GetImpersonationWrapper(this.host, this.resource);
				}
				throw DataSourceException.NewInvalidCredentialsError(this.host, this.resource, Strings.SapBwWindowsAuthInvalidSncPartnerName, Strings.SapBwWindowsAuthInvalidSncPartnerName, null);
			}
			else
			{
				UsernamePasswordCredential usernamePasswordCredential = resourceCredential as UsernamePasswordCredential;
				if (usernamePasswordCredential == null)
				{
					throw DataSourceException.NewInvalidCredentialsError(this.host, this.resource, null, null, null);
				}
				this.authenticationProperties["USER"] = usernamePasswordCredential.Username;
				this.authenticationProperties["PASSWD"] = usernamePasswordCredential.Password;
				return new Func<IDisposable>(CredentialExtensions.NullImpersonationWrapper);
			}
		}

		// Token: 0x06002911 RID: 10513 RVA: 0x0007A964 File Offset: 0x00078B64
		public void TestConnection()
		{
			this.HandleExceptionsWithLanguageCodeRetry(delegate(string languageCode)
			{
				using (DbConnection dbConnection = this.CreateConnection(true, this.GetConnectionString(languageCode)))
				{
					dbConnection.Open();
				}
			});
		}

		// Token: 0x06002912 RID: 10514 RVA: 0x0007A978 File Offset: 0x00078B78
		private void HandleExceptionsWithLanguageCodeRetry(Action<string> action)
		{
			string[] array = new string[]
			{
				this.options.GetSapLanguageCode(),
				SapBwOptions.GetSapLanguageCodeFromCulture(Thread.CurrentThread.CurrentUICulture),
				SapBwOptions.GetSapLanguageCodeFromCulture(Thread.CurrentThread.CurrentCulture)
			};
			DbExceptionInfo dbExceptionInfo = null;
			foreach (string text in array)
			{
				if (text != null)
				{
					try
					{
						action(text);
						return;
					}
					catch (DbException ex)
					{
						dbExceptionInfo = dbExceptionInfo ?? new DbExceptionInfo(this.host, ex);
					}
				}
			}
			throw dbExceptionInfo.GetEngineException("SAP Business Warehouse", this.resource);
		}

		// Token: 0x06002913 RID: 10515 RVA: 0x0007AA18 File Offset: 0x00078C18
		private void HandleExceptions(Action action)
		{
			try
			{
				action();
			}
			catch (DbException ex)
			{
				throw new DbExceptionInfo(this.host, ex).GetEngineException("SAP Business Warehouse", this.resource);
			}
		}

		// Token: 0x06002914 RID: 10516 RVA: 0x0007AA5C File Offset: 0x00078C5C
		private static void SwallowSafeExceptions(Action action)
		{
			try
			{
				action();
			}
			catch (Exception ex)
			{
				if (!Microsoft.Mashup.Common.SafeExceptions.IsSafeException(ex))
				{
					throw;
				}
			}
		}

		// Token: 0x06002915 RID: 10517 RVA: 0x0007AA8C File Offset: 0x00078C8C
		protected IDataReaderWithTableSchema ExecuteMdxCommand(Func<IDbConnection, IDbCommand> createCommand, string commandText, RowRange range, bool cacheResults, bool newConnection = false)
		{
			if (cacheResults)
			{
				IHostProgress hostProgress = ProgressService.GetHostProgress(this.host, this.resource.Kind, this.resource.Path);
				using (new ProgressRequest(hostProgress))
				{
					IPersistentCache persistentCache = this.host.GetPersistentCache();
					string cacheKey = this.GetCacheKey(commandText, range.SkipCount.ToString(), range.TakeCount.ToString());
					Stream stream;
					if (persistentCache.TryGetValue(cacheKey, out stream))
					{
						return new ProgressDbDataReader(DbData.Deserialize(stream), hostProgress);
					}
					return new DbData.CachingDbDataReader(this.host, persistentCache, cacheKey, this.GetDataReader(createCommand, range, newConnection, true).WithTableSchema(), range.TakeCount.IsInfinite ? long.MaxValue : range.TakeCount.Value, persistentCache.MaxEntryLength, new Action<Action>(this.HandleExceptions), true, !range.TakeCount.IsInfinite && range.TakeCount.Value < 4096L, null);
				}
			}
			return this.GetDataReader(createCommand, range, newConnection, true);
		}

		// Token: 0x06002916 RID: 10518 RVA: 0x0007ABEC File Offset: 0x00078DEC
		public bool TryGetDataReaderFromCache(string commandText, out IDataReaderWithTableSchema reader)
		{
			string cacheKey = this.GetCacheKey(commandText, null, null);
			Stream stream;
			if (this.host.GetPersistentCache().TryGetValue(cacheKey, out stream))
			{
				reader = DbData.Deserialize(stream);
				return true;
			}
			reader = null;
			return false;
		}

		// Token: 0x06002917 RID: 10519 RVA: 0x0007AC2C File Offset: 0x00078E2C
		protected IDataReaderWithTableSchema ExecuteCommand(Func<IDbConnection, IDbCommand> createCommand, string commandText = null, bool retry = true, Action<Action> readExceptionHandler = null)
		{
			string cacheKey = this.GetCacheKey(commandText, null, null);
			IPersistentCache metadataCache = this.host.GetMetadataCache();
			Stream stream;
			if (metadataCache.TryGetValue(cacheKey, out stream))
			{
				return DbData.Deserialize(stream);
			}
			IDataReaderWithTableSchema dataReaderWithTableSchema = this.GetDataReader(createCommand, RowRange.All, false, retry);
			dataReaderWithTableSchema = new DbData.CachingDbDataReader(this.host, metadataCache, cacheKey, dataReaderWithTableSchema, long.MaxValue, long.MaxValue, readExceptionHandler ?? new Action<Action>(this.HandleExceptions), true, true, null);
			return new SapBwService.CloseOnDisposeDataReader(dataReaderWithTableSchema);
		}

		// Token: 0x06002918 RID: 10520 RVA: 0x0007ACB8 File Offset: 0x00078EB8
		protected bool TryExecuteCommand(string traceInfo, Func<IDbConnection, IDbCommand> createCommand, string commandText, out IDataReaderWithTableSchema reader)
		{
			reader = this.SwallowAndCacheSafeExceptions<IDataReaderWithTableSchema>(traceInfo, this.GetCacheKey(commandText, null, null), () => this.ExecuteCommand(createCommand, commandText, false, new Action<Action>(SapBwService.SwallowSafeExceptions)));
			return reader != null;
		}

		// Token: 0x06002919 RID: 10521 RVA: 0x0007AD0C File Offset: 0x00078F0C
		protected bool TryGetSchema(string traceInfo, string schemaName, string[] restrictions, out IDataReaderWithTableSchema reader)
		{
			IPersistentCache metadataCache = this.host.GetMetadataCache();
			string text = PersistentCacheKey.SapBw.Qualify(this.ConnectionStringCacheKey, schemaName, DbEnvironment.GetKey(restrictions ?? EmptyArray<string>.Instance));
			Stream stream;
			DataTable dataTable;
			if (!metadataCache.TryGetValue(text, out stream))
			{
				dataTable = this.SwallowAndCacheSafeExceptions<DataTable>(traceInfo, text, delegate
				{
					DataTable dataTable2;
					using (DbConnection unwrappedConnection = DbEnvironment.GetUnwrappedConnection(ConnectionPoolingDbProviderFactory.GetUnwrappedConnection((DbConnection)this.CreateAndOpenConnection(false, this.ConnectionString))))
					{
						dataTable2 = ((restrictions == null) ? unwrappedConnection.GetSchema(schemaName) : unwrappedConnection.GetSchema(schemaName, restrictions));
					}
					return dataTable2;
				});
				if (dataTable != null)
				{
					stream = metadataCache.BeginAdd();
					DbData.WriteTable(stream, dataTable);
					metadataCache.EndAdd(text, stream).Close();
				}
			}
			else
			{
				dataTable = DbData.ReadTable(stream);
				stream.Close();
			}
			if (dataTable != null)
			{
				reader = dataTable.CreateDataReader().WithTableSchema();
				return true;
			}
			reader = null;
			return false;
		}

		// Token: 0x0600291A RID: 10522 RVA: 0x0007ADE4 File Offset: 0x00078FE4
		protected T SwallowAndCacheSafeExceptions<T>(string traceInfo, string cacheKey, Func<T> action)
		{
			try
			{
				return action();
			}
			catch (Exception ex)
			{
				Exception ex3;
				Exception ex2 = ex3;
				Exception ex = ex2;
				using (IDataReaderWithTableSchema dataReaderWithTableSchema = new DbData.CachingDbDataReader(this.host, this.host.GetPersistentCache(), cacheKey, new DataTable
				{
					Locale = CultureInfo.InvariantCulture
				}.CreateDataReader().WithTableSchema(), long.MaxValue, long.MaxValue, new Action<Action>(SapBwService.SwallowSafeExceptions), true, true, null))
				{
					while (dataReaderWithTableSchema.Read())
					{
					}
				}
				this.Tracer.Trace(traceInfo, delegate(IHostTrace trace)
				{
					trace.Add(ex, true);
				});
				if (!Microsoft.Mashup.Common.SafeExceptions.IsSafeException(ex))
				{
					throw;
				}
			}
			return default(T);
		}

		// Token: 0x0600291B RID: 10523 RVA: 0x0007AEC8 File Offset: 0x000790C8
		private IDataReaderWithTableSchema GetDataReader(Func<IDbConnection, IDbCommand> createCommand, RowRange range, bool newConnection = false, bool retry = true)
		{
			Func<IDataReaderWithTableSchema> func = delegate
			{
				IDbConnection dbConnection = null;
				IDbCommand dbCommand = null;
				IDataReaderWithTableSchema dataReaderWithTableSchema = null;
				IDataReaderWithTableSchema dataReaderWithTableSchema2;
				try
				{
					dbConnection = this.CreateAndOpenConnection(newConnection, this.ConnectionString);
					dbCommand = createCommand(dbConnection);
					dataReaderWithTableSchema = dbCommand.ExecuteReader(CommandBehavior.Default).WithTableSchema();
					dataReaderWithTableSchema = new DisposingDataReader(dataReaderWithTableSchema, new CompositeDisposable(new IDisposable[] { dbCommand, dbConnection }));
					if (!range.IsAll)
					{
						dataReaderWithTableSchema = new SkipTakeDataReader(dataReaderWithTableSchema, range);
					}
					dataReaderWithTableSchema2 = dataReaderWithTableSchema;
				}
				catch (Exception)
				{
					if (dataReaderWithTableSchema != null)
					{
						dataReaderWithTableSchema.Dispose();
					}
					if (dbCommand != null)
					{
						dbCommand.Dispose();
					}
					if (dbConnection != null)
					{
						dbConnection.Dispose();
					}
					throw;
				}
				return dataReaderWithTableSchema2;
			};
			if (!retry)
			{
				return this.ExceptionHandler.InvokeWithoutRetry<IDataReaderWithTableSchema>(func);
			}
			return this.ExceptionHandler.InvokeWithRetry<IDataReaderWithTableSchema>(func);
		}

		// Token: 0x0600291C RID: 10524 RVA: 0x0007AF20 File Offset: 0x00079120
		private string GetCacheKey(string commandText, string skipCount = null, string takeCount = null)
		{
			if (skipCount != null || takeCount != null)
			{
				return PersistentCacheKey.SapBw.Qualify(this.ConnectionStringCacheKey, commandText, skipCount, takeCount);
			}
			return PersistentCacheKey.SapBw.Qualify(this.ConnectionStringCacheKey, commandText);
		}

		// Token: 0x0600291D RID: 10525 RVA: 0x0007AF60 File Offset: 0x00079160
		public static string GetBapiCommandText(string bapiName, string bapiReturnTable, SapBwRestrictions arguments)
		{
			string text = string.Join(", ", SapBwService.GetBapiExports(arguments).ToArray<string>());
			string text2 = (string.IsNullOrEmpty(text) ? null : string.Format(CultureInfo.InvariantCulture, " EXPORTS {0}", text));
			return string.Format(CultureInfo.InvariantCulture, "EXECUTE FUNCTION '{0}'{1} TABLES '{2}' INTO RESULTSET;", bapiName, text2, bapiReturnTable);
		}

		// Token: 0x0600291E RID: 10526 RVA: 0x0007AFB1 File Offset: 0x000791B1
		private static IEnumerable<string> GetBapiExports(SapBwRestrictions restrictions)
		{
			if (restrictions != null)
			{
				foreach (KeyValuePair<string, object> keyValuePair in restrictions)
				{
					yield return string.Format(CultureInfo.InvariantCulture, (keyValuePair.Value is string) ? "{0} = '{1}'" : "{0} = {1}", keyValuePair.Key, keyValuePair.Value);
				}
				List<KeyValuePair<string, object>>.Enumerator enumerator = default(List<KeyValuePair<string, object>>.Enumerator);
			}
			yield break;
			yield break;
		}

		// Token: 0x04001193 RID: 4499
		private const string sapBwTracing = "SapBwTracing";

		// Token: 0x04001194 RID: 4500
		private const int pageSize = 4096;

		// Token: 0x04001195 RID: 4501
		private static readonly Dictionary<string, string> languageCodes = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
		{
			{ "SR", "0" },
			{ "ZH", "1" },
			{ "TH", "2" },
			{ "KO", "3" },
			{ "RO", "4" },
			{ "SL", "5" },
			{ "HR", "6" },
			{ "MS", "7" },
			{ "UK", "8" },
			{ "ET", "9" },
			{ "AR", "A" },
			{ "HE", "B" },
			{ "CS", "C" },
			{ "DE", "D" },
			{ "EN", "E" },
			{ "FR", "F" },
			{ "EL", "G" },
			{ "HU", "H" },
			{ "IT", "I" },
			{ "JA", "J" },
			{ "DA", "K" },
			{ "PL", "L" },
			{ "ZF", "M" },
			{ "NL", "N" },
			{ "NO", "O" },
			{ "PT", "P" },
			{ "SK", "Q" },
			{ "RU", "R" },
			{ "ES", "S" },
			{ "TR", "T" },
			{ "FI", "U" },
			{ "SV", "V" },
			{ "BG", "W" },
			{ "LT", "X" },
			{ "LV", "Y" },
			{ "Z1", "Z" },
			{ "AF", "a" },
			{ "IS", "b" },
			{ "CA", "c" },
			{ "SH", "d" },
			{ "ID", "i" }
		};

		// Token: 0x04001196 RID: 4502
		protected readonly IEngineHost host;

		// Token: 0x04001197 RID: 4503
		protected readonly SapBwOptions options;

		// Token: 0x04001198 RID: 4504
		private readonly SapBwOlapDataSourceLocation location;

		// Token: 0x04001199 RID: 4505
		private readonly SapBwRouterString routerString;

		// Token: 0x0400119A RID: 4506
		private readonly IResource resource;

		// Token: 0x0400119B RID: 4507
		private readonly string driverTracePath;

		// Token: 0x0400119C RID: 4508
		private readonly Tracer tracer;

		// Token: 0x0400119D RID: 4509
		private readonly Dictionary<string, string> authenticationProperties;

		// Token: 0x0400119E RID: 4510
		private readonly string providerId;

		// Token: 0x0400119F RID: 4511
		private string connectionString;

		// Token: 0x040011A0 RID: 4512
		private string connectionStringCacheKey;

		// Token: 0x040011A1 RID: 4513
		private ResourceCredentialCollection credentials;

		// Token: 0x040011A2 RID: 4514
		private DbExceptionHandler exceptionHandler;

		// Token: 0x020004E8 RID: 1256
		private class CloseOnDisposeDataReader : DelegatingDataReaderWithTableSchema
		{
			// Token: 0x06002922 RID: 10530 RVA: 0x0006CC48 File Offset: 0x0006AE48
			public CloseOnDisposeDataReader(IDataReaderWithTableSchema reader)
				: base(reader)
			{
			}

			// Token: 0x06002923 RID: 10531 RVA: 0x0007B2C0 File Offset: 0x000794C0
			public override void Dispose()
			{
				this.Close();
				base.Dispose();
			}
		}
	}
}
