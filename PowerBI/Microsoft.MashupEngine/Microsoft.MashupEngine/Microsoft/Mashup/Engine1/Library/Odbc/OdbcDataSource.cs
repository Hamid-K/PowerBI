using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql;
using Microsoft.Mashup.Engine1.Library.Odbc.Interop;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.OleDb.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Odbc
{
	// Token: 0x020005DB RID: 1499
	internal class OdbcDataSource
	{
		// Token: 0x06002EA5 RID: 11941 RVA: 0x0008E2CC File Offset: 0x0008C4CC
		public OdbcDataSource(IEngineHost host, IOdbcService service, ConnectionStringHandler connectionStringHandler, Value sourceConnectionValue, OdbcOptions options, OdbcExceptionHandler odbcExceptionHandler, Action<IHostTrace> additionalTraces = null)
		{
			this.host = host;
			this.connectionStringHandler = connectionStringHandler;
			this.odbcService = service;
			this.sourceConnectionValue = sourceConnectionValue;
			this.sourceConnectionString = connectionStringHandler.GetValidatedString(sourceConnectionValue, "ODBC", host);
			this.tables = new Dictionary<OdbcIdentifier, OdbcTableInfo>();
			this.options = options;
			this.resource = OdbcModule.CreateResource(this.host, sourceConnectionValue);
			this.types = new OdbcTypeInfoCollection(this, OdbcImplicitTypeConversions.New(options.ImplicitTypeConversions));
			this.odbcExceptionHandler = odbcExceptionHandler;
			this.statementRegistrar = OdbcStatementRegistrar.New(this.options, this.host);
			this.additionalTraces = additionalTraces;
			this.tracer = new Tracer(host, "Engine/IO/Odbc/", this.resource, null, null);
		}

		// Token: 0x17001109 RID: 4361
		// (get) Token: 0x06002EA6 RID: 11942 RVA: 0x0008E38E File Offset: 0x0008C58E
		public OdbcOptions Options
		{
			get
			{
				return this.options;
			}
		}

		// Token: 0x06002EA7 RID: 11943 RVA: 0x0008E396 File Offset: 0x0008C596
		public bool TryGetVersion(out string version)
		{
			version = this.version;
			return version != null;
		}

		// Token: 0x06002EA8 RID: 11944 RVA: 0x0008E3A8 File Offset: 0x0008C5A8
		private void Initialize()
		{
			IExtensibilityService extensibilityService = this.host.QueryService<IExtensibilityService>();
			bool flag = extensibilityService != null;
			ResourceCredentialCollection resourceCredentialCollection;
			if (flag && extensibilityService.CurrentCredentials != null)
			{
				resourceCredentialCollection = extensibilityService.CurrentCredentials;
			}
			else
			{
				resourceCredentialCollection = HostResourcePermissionService.VerifyPermissionAndGetCredentials(this.host, this.resource, null);
			}
			ConnectionStringAdornment connectionStringAdornment;
			if (this.options.TryGetConnectionStringAdornment(this.connectionStringHandler, out connectionStringAdornment))
			{
				ArrayBuilder<IResourceCredential> arrayBuilder = new ArrayBuilder<IResourceCredential>(2);
				foreach (IResourceCredential resourceCredential in resourceCredentialCollection)
				{
					if (resourceCredential is WindowsCredential)
					{
						arrayBuilder.Add(resourceCredential);
						break;
					}
				}
				arrayBuilder.Add(connectionStringAdornment);
				this.credentials = new ResourceCredentialCollection(this.Resource, arrayBuilder.ToArray());
			}
			else
			{
				this.credentials = resourceCredentialCollection;
			}
			if (!this.options.CredentialHandler.IsNull)
			{
				this.refreshConnectionString = true;
			}
			OdbcConnectionInfo info = this.CreateConnectionInfo();
			bool enableGovernance = this.Host.QueryService<IConnectionGovernanceService>() != null;
			if (enableGovernance)
			{
				this.odbcService = new OdbcConnectionGoverningService(this.Host, this.Resource, this.odbcService);
			}
			this.connectionString = info.ConnectionString;
			this.currentCacheContext = null;
			this.connectionAttributes = info.ConnectionAttributes;
			if (info.RequiresImpersonation)
			{
				this.odbcService = new OdbcImpersonatingService(this.odbcService, info.Impersonate);
			}
			bool enablePooling = this.options.ClientConnectionPooling && !enableGovernance;
			if (enablePooling)
			{
				this.odbcService = new OdbcConnectionPoolingService(this.host, this.odbcService, OdbcDataSource.ConnectionPool, this.resource, this.statementRegistrar);
			}
			if ((this.options.SetUserQuery != null || this.options.ClearUserQuery != null) && !enablePooling)
			{
				Value value = TextValue.New(this.options.SetUserQuery ?? this.options.ClearUserQuery);
				throw ValueException.NewExpressionError<Message0>(Strings.Odbc_SetQueryNoPooling, value, null);
			}
			this.odbcService = new OdbcCachingService(this.host, OdbcTracingService.New(this.host, this.tracer, this.odbcService, delegate(IHostTrace trace)
			{
				string text = "RequireEncryption";
				EncryptedConnectionAdornment encryptedConnectionAdornment = this.credentials.OfType<EncryptedConnectionAdornment>().FirstOrDefault<EncryptedConnectionAdornment>();
				trace.Add(text, (encryptedConnectionAdornment != null) ? new bool?(encryptedConnectionAdornment.RequireEncryption) : null, false);
				trace.Add("Governance", enableGovernance, false);
				trace.Add("Pooling", enablePooling, false);
				trace.Add("Impersonation", info.RequiresImpersonation, false);
				Action<IHostTrace> action = this.additionalTraces;
				if (action == null)
				{
					return;
				}
				action(trace);
			}));
			this.odbcService = new UserOverrideOdbcService(flag, this.odbcService, this.host.QueryService<ILifetimeService>(), this.options.SQLGetInfo, this.options.SQLGetTypeInfo, this.options.SQLGetFunctions, this.options.SQLColumns, this.options.SQLTables);
			this.initialized = true;
		}

		// Token: 0x06002EA9 RID: 11945 RVA: 0x0008E670 File Offset: 0x0008C870
		private OdbcConnectionInfo CreateConnectionInfo()
		{
			if (this.refreshConnectionString)
			{
				Dictionary<int, object> dictionary;
				return new OdbcConnectionInfo(this.OverrideConnectionString(out dictionary), dictionary);
			}
			GenericConnectionStringBuilder genericConnectionStringBuilder = new GenericConnectionStringBuilder(this.host, "ODBC", this.connectionStringHandler, this.sourceConnectionString, this.resource, this.options.SqlCompatibleWindowsAuth);
			ConnectionInfo connectionInfo;
			try
			{
				connectionInfo = genericConnectionStringBuilder.ConstructConnectionString(null, null, this.credentials, null);
			}
			catch (ValueException ex)
			{
				RuntimeException ex2;
				if (this.odbcExceptionHandler.TryHandle(ex, out ex2))
				{
					throw ex2;
				}
				throw;
			}
			return new OdbcConnectionInfo(connectionInfo);
		}

		// Token: 0x1700110A RID: 4362
		// (get) Token: 0x06002EAA RID: 11946 RVA: 0x0008E70C File Offset: 0x0008C90C
		public IEngineHost Host
		{
			get
			{
				return this.host;
			}
		}

		// Token: 0x1700110B RID: 4363
		// (get) Token: 0x06002EAB RID: 11947 RVA: 0x0008E714 File Offset: 0x0008C914
		public IResource Resource
		{
			get
			{
				return this.resource;
			}
		}

		// Token: 0x1700110C RID: 4364
		// (get) Token: 0x06002EAC RID: 11948 RVA: 0x0008E71C File Offset: 0x0008C91C
		public OdbcDataSourceInfo Info
		{
			get
			{
				if (this.info == null)
				{
					this.info = this.ConnectForMetadata<OdbcDataSourceInfo>((IOdbcConnection connection) => OdbcDataSourceInfo.Load(connection));
					this.info = this.info.OverrideWithOptions(this.options);
				}
				return this.info;
			}
		}

		// Token: 0x1700110D RID: 4365
		// (get) Token: 0x06002EAD RID: 11949 RVA: 0x0008E779 File Offset: 0x0008C979
		public SqlSettings SqlSettings
		{
			get
			{
				if (this.sqlSettings == null)
				{
					this.sqlSettings = OdbcSqlSettings.From(this.Info);
				}
				return this.sqlSettings;
			}
		}

		// Token: 0x1700110E RID: 4366
		// (get) Token: 0x06002EAE RID: 11950 RVA: 0x0008E79A File Offset: 0x0008C99A
		public OdbcTableTypes TableTypes
		{
			get
			{
				return this.options.TableTypes;
			}
		}

		// Token: 0x06002EAF RID: 11951 RVA: 0x0008E7A8 File Offset: 0x0008C9A8
		public OdbcTableInfo GetOrCreateTableInfo(OdbcIdentifier tableReference, TableType tableType = null)
		{
			OdbcTableInfo odbcTableInfo;
			if (!this.tables.TryGetValue(tableReference, out odbcTableInfo))
			{
				odbcTableInfo = new OdbcTableInfo(this.host, this, tableReference, tableType);
				this.tables[tableReference] = odbcTableInfo;
			}
			return odbcTableInfo;
		}

		// Token: 0x1700110F RID: 4367
		// (get) Token: 0x06002EB0 RID: 11952 RVA: 0x0008E7E2 File Offset: 0x0008C9E2
		public OdbcTypeInfoCollection Types
		{
			get
			{
				return this.types;
			}
		}

		// Token: 0x17001110 RID: 4368
		// (get) Token: 0x06002EB1 RID: 11953 RVA: 0x0008E7EA File Offset: 0x0008C9EA
		public OdbcCacheContext CurrentCacheContext
		{
			get
			{
				if (this.currentCacheContext == null)
				{
					if (this.credentials == null)
					{
						this.Initialize();
					}
					this.currentCacheContext = new OdbcCacheContext(this.sourceConnectionString, null, this.credentials);
				}
				return this.currentCacheContext;
			}
		}

		// Token: 0x06002EB2 RID: 11954 RVA: 0x0008E820 File Offset: 0x0008CA20
		protected virtual OdbcFetchPlanFactory GetFetchPlanFactory()
		{
			Value value;
			if (!this.options.SQLGetFunctions.TryGetValue("SQL_API_SQLBINDCOL", out value) || value.IsNull || !value.AsBoolean)
			{
				return OdbcFetchPlanFactory.BindColumnNotSupportedInstance;
			}
			return OdbcFetchPlanFactory.BindColumnSupportedInstance;
		}

		// Token: 0x06002EB3 RID: 11955 RVA: 0x0008E864 File Offset: 0x0008CA64
		public IList<string> GetInstalledDrivers()
		{
			return this.odbcService.GetInstalledDrivers();
		}

		// Token: 0x06002EB4 RID: 11956 RVA: 0x0008E874 File Offset: 0x0008CA74
		public string TestConnectionAndGetVersion(string catalog)
		{
			Action<IOdbcConnection> <>9__1;
			this.ExceptionHandler.InvokeWithRetry<int>(delegate
			{
				OdbcDataSource <>4__this = this;
				string catalog2 = catalog;
				Action<IOdbcConnection> action;
				if ((action = <>9__1) == null)
				{
					action = (<>9__1 = delegate(IOdbcConnection c)
					{
						this.version = c.GetInfoString(Odbc32.SQL_INFO.SQL_DBMS_VER);
					});
				}
				<>4__this.ConnectForMetadata(catalog2, action);
				return 0;
			});
			return this.version;
		}

		// Token: 0x17001111 RID: 4369
		// (get) Token: 0x06002EB5 RID: 11957 RVA: 0x0008E8B3 File Offset: 0x0008CAB3
		private DbExceptionHandler ExceptionHandler
		{
			get
			{
				if (this.exceptionHandler == null)
				{
					this.exceptionHandler = new DbExceptionHandler(this.host, this.tracer, "ODBC", this.resource, (DbException e) => new OdbcExceptionInfo(this.host, e, this.odbcExceptionHandler), false);
				}
				return this.exceptionHandler;
			}
		}

		// Token: 0x17001112 RID: 4370
		// (get) Token: 0x06002EB6 RID: 11958 RVA: 0x0008E8F2 File Offset: 0x0008CAF2
		public string UniqueIdentifier
		{
			get
			{
				return this.resource.Path;
			}
		}

		// Token: 0x17001113 RID: 4371
		// (get) Token: 0x06002EB7 RID: 11959 RVA: 0x0008E8FF File Offset: 0x0008CAFF
		private string ConnectionString
		{
			get
			{
				if (!this.initialized || this.refreshConnectionString)
				{
					this.Initialize();
				}
				return this.connectionString;
			}
		}

		// Token: 0x17001114 RID: 4372
		// (get) Token: 0x06002EB8 RID: 11960 RVA: 0x0008E91D File Offset: 0x0008CB1D
		private IOdbcService OdbcService
		{
			get
			{
				if (!this.initialized)
				{
					this.Initialize();
				}
				return this.odbcService;
			}
		}

		// Token: 0x06002EB9 RID: 11961 RVA: 0x0008E934 File Offset: 0x0008CB34
		public T ConnectForMetadata<T>(string catalog, Func<IOdbcConnection, T> func)
		{
			return this.ExceptionHandler.InvokeWithRetry<T>(delegate
			{
				T t;
				using (IOdbcConnection odbcConnection = this.CreateConnection(true, catalog))
				{
					using (new ProgressRequest(this.CreateHostProgress()))
					{
						odbcConnection.Open();
						t = func(odbcConnection);
					}
				}
				return t;
			});
		}

		// Token: 0x06002EBA RID: 11962 RVA: 0x0008E973 File Offset: 0x0008CB73
		public T ConnectForMetadata<T>(Func<IOdbcConnection, T> func)
		{
			return this.ConnectForMetadata<T>(null, func);
		}

		// Token: 0x06002EBB RID: 11963 RVA: 0x0008E980 File Offset: 0x0008CB80
		public void ConnectForMetadata(string catalog, Action<IOdbcConnection> action)
		{
			this.ConnectForMetadata<int>(catalog, delegate(IOdbcConnection connection)
			{
				action(connection);
				return 0;
			});
		}

		// Token: 0x06002EBC RID: 11964 RVA: 0x0008E9B0 File Offset: 0x0008CBB0
		public void ConnectForMetadata(Action<IOdbcConnection> action)
		{
			this.ConnectForMetadata<int>(delegate(IOdbcConnection connection)
			{
				action(connection);
				return 0;
			});
		}

		// Token: 0x06002EBD RID: 11965 RVA: 0x0008E9E0 File Offset: 0x0008CBE0
		public IPageReader ExecutePageReader(string commandText, string catalog, IList<OdbcParameter> parameters, RowRange rowRange, string[] columnNames, ColumnConversion[] columnConversions)
		{
			IPageReader pageReader = this.ExecuteCore(commandText, catalog, parameters, rowRange, columnNames, columnConversions);
			return new DbExceptionHandlingPageReader(this.exceptionHandler, pageReader);
		}

		// Token: 0x06002EBE RID: 11966 RVA: 0x0008EA09 File Offset: 0x0008CC09
		public IDataReaderWithTableSchema Execute(IPersistentCache cache, string commandText, string catalog, IList<OdbcParameter> parameters, RowRange rowRange, string[] columnNames, bool isStable, ColumnConversion[] columnConversions = null)
		{
			if (isStable)
			{
				return this.ExecuteWithPaging(cache, commandText, catalog, parameters, rowRange, columnNames, columnConversions);
			}
			return this.ExecuteWithoutPaging(cache, commandText, catalog, parameters, rowRange, columnNames, columnConversions);
		}

		// Token: 0x06002EBF RID: 11967 RVA: 0x0008EA34 File Offset: 0x0008CC34
		public long ExecuteNonQuery(string commandText, string catalog, IList<OdbcParameter> parameters)
		{
			long num;
			using (IOdbcConnection odbcConnection = this.CreateConnection(false, catalog))
			{
				odbcConnection.Open();
				num = odbcConnection.ExecuteNonQueryDirect(commandText, parameters, this.statementRegistrar);
			}
			return num;
		}

		// Token: 0x06002EC0 RID: 11968 RVA: 0x0008EA7C File Offset: 0x0008CC7C
		public override bool Equals(object obj)
		{
			return this.Equals(obj as OdbcDataSource);
		}

		// Token: 0x06002EC1 RID: 11969 RVA: 0x0008EA8A File Offset: 0x0008CC8A
		public bool Equals(OdbcDataSource other)
		{
			return other != null && other.UniqueIdentifier == this.UniqueIdentifier;
		}

		// Token: 0x06002EC2 RID: 11970 RVA: 0x0008EAA2 File Offset: 0x0008CCA2
		public override int GetHashCode()
		{
			return this.UniqueIdentifier.GetHashCode();
		}

		// Token: 0x06002EC3 RID: 11971 RVA: 0x0008EAB0 File Offset: 0x0008CCB0
		private string OverrideConnectionString(out Dictionary<int, object> connectionAttributes)
		{
			RecordValue recordValue;
			if (!CredentialConversion.TryConvertToRecord(this.host.QueryService<IEngine>(), this.host, this.resource, this.credentials, out recordValue))
			{
				throw new UnpermittedResourceAccessException(this.resource, Strings.Extensibility_InvalidCredentialType, null, null);
			}
			Value value = this.options.CredentialHandler.AsFunction.Invoke(this.sourceConnectionValue, recordValue);
			string validatedString = this.connectionStringHandler.GetValidatedString(value, "ODBC", this.host);
			Value value2;
			if (value.TryGetMetaField("ConnectionAttributes", out value2))
			{
				if (!value2.IsTable || !OdbcDataSource.attributeKeys.Equals(value2.AsTable.Columns))
				{
					throw ValueException.NewExpressionError<Message0>(Strings.Odbc_InvalidConnectionAttributes, value2, null);
				}
				connectionAttributes = this.GetConnectionAttributes(value2.AsTable);
			}
			else
			{
				connectionAttributes = null;
			}
			return this.connectionStringHandler.ResourcePathNormalize(validatedString);
		}

		// Token: 0x06002EC4 RID: 11972 RVA: 0x0008EB8A File Offset: 0x0008CD8A
		private IHostProgress CreateHostProgress()
		{
			return ProgressService.GetHostProgress(this.host, this.resource.Kind, this.resource.Path);
		}

		// Token: 0x06002EC5 RID: 11973 RVA: 0x0008EBB0 File Offset: 0x0008CDB0
		private IOdbcConnection CreateConnection(bool isMetadataConnection, string catalog = null)
		{
			return this.OdbcService.CreateConnection(new OdbcConnectionProperties
			{
				ConnectionString = this.ConnectionString,
				Catalog = catalog,
				ConnectionTimeout = this.options.ConnectionTimeout,
				CommandTimeout = this.options.CommandTimeout,
				IsMetadataConnection = isMetadataConnection,
				ConnectionAttributes = this.connectionAttributes,
				SetUserQuery = this.options.SetUserQuery,
				ClearUserQuery = this.options.ClearUserQuery,
				UserContextCredentialProperties = this.options.UserContextCredentialProperties,
				FetchPlanFactory = this.GetFetchPlanFactory(),
				CacheContext = this.CurrentCacheContext.WithCatalog(catalog)
			});
		}

		// Token: 0x06002EC6 RID: 11974 RVA: 0x0008EC68 File Offset: 0x0008CE68
		private Dictionary<int, object> GetConnectionAttributes(TableValue attributes)
		{
			Dictionary<int, object> dictionary = new Dictionary<int, object>(attributes.Count);
			foreach (IValueReference valueReference in attributes)
			{
				int num = 0;
				object obj = null;
				if (valueReference.Value[0].IsNumber && valueReference.Value[0].AsNumber.TryGetInt32(out num))
				{
					switch (valueReference.Value[1].Kind)
					{
					case ValueKind.Number:
					{
						int num2;
						if (valueReference.Value[1].AsNumber.TryGetInt32(out num2))
						{
							obj = num2;
							valueReference.Value[1].ToSource();
						}
						break;
					}
					case ValueKind.Text:
						obj = valueReference.Value[1].AsString;
						valueReference.Value[1].ToSource();
						break;
					case ValueKind.Binary:
						Convert.ToBase64String(obj = valueReference.Value[1].AsBinary.AsBytes);
						break;
					}
				}
				if (obj == null)
				{
					throw ValueException.NewExpressionError<Message0>(Strings.Odbc_InvalidConnectionAttributes, attributes, null);
				}
				dictionary.Add(num, obj);
			}
			return dictionary;
		}

		// Token: 0x06002EC7 RID: 11975 RVA: 0x0008EDC8 File Offset: 0x0008CFC8
		private IDataReaderWithTableSchema ExecuteWithPaging(IPersistentCache cache, string commandText, string catalog, IList<OdbcParameter> parameters, RowRange range, string[] columnNames, ColumnConversion[] columnConversions)
		{
			return PageCachingDataReader.New(this.host, cache, this.OdbcService.PageSize, this.GetCacheKey(commandText, catalog, parameters, RowRange.All), range, (RowRange r) => this.ExecuteCoreDataReader(commandText, catalog, parameters, r, columnNames, columnConversions), this.resource);
		}

		// Token: 0x06002EC8 RID: 11976 RVA: 0x0008EE58 File Offset: 0x0008D058
		private IDataReaderWithTableSchema ExecuteWithoutPaging(IPersistentCache cache, string commandText, string catalog, IList<OdbcParameter> parameters, RowRange range, string[] columnNames, ColumnConversion[] columnConversions)
		{
			IHostProgress hostProgress = this.CreateHostProgress();
			IDataReaderWithTableSchema dataReaderWithTableSchema;
			using (new ProgressRequest(hostProgress))
			{
				StructuredCacheKey cacheKey = this.GetCacheKey(commandText, catalog, parameters, range);
				Stream stream;
				if (cache.TryGetValue(cacheKey, out stream))
				{
					dataReaderWithTableSchema = new ProgressDbDataReader(DbData.Deserialize(stream), hostProgress);
				}
				else
				{
					dataReaderWithTableSchema = new DbData.CachingDbDataReader(this.host, cache, cacheKey, this.ExecuteCoreDataReader(commandText, catalog, parameters, range, columnNames, columnConversions), range.TakeCount.IsInfinite ? long.MaxValue : range.TakeCount.Value, cache.MaxEntryLength, () => TracingService.CreateTrace(this.Host, "Engine/IO/Db/ODBC/TraceValueExceptions", TraceEventType.Information, this.Resource), true, !range.TakeCount.IsInfinite && range.TakeCount.Value < (long)this.OdbcService.PageSize);
				}
			}
			return dataReaderWithTableSchema;
		}

		// Token: 0x06002EC9 RID: 11977 RVA: 0x0008EF58 File Offset: 0x0008D158
		private IDataReaderWithTableSchema ExecuteCoreDataReader(string commandText, string catalog, IList<OdbcParameter> parameters, RowRange range, string[] columnNames, ColumnConversion[] columnConversions)
		{
			return new PageReaderDataReader(this.ExecuteCore(commandText, catalog, parameters, range, columnNames, columnConversions), new Func<ISerializedException, Exception>(PageExceptionSerializer.GetExceptionFromProperties), new Func<ISerializedException, Exception>(PageExceptionSerializer.GetExceptionFromProperties));
		}

		// Token: 0x06002ECA RID: 11978 RVA: 0x0008EF88 File Offset: 0x0008D188
		private IPageReader ExecuteCore(string commandText, string catalog, IList<OdbcParameter> parameters, RowRange rowRange, string[] columnNames, ColumnConversion[] columnConversions)
		{
			return this.ExceptionHandler.InvokeWithRetry<IPageReader>(delegate
			{
				IHostProgress hostProgress = this.CreateHostProgress();
				this.host.QueryService<ICancellationService>();
				IPageReader pageReader2;
				using (new ProgressRequest(hostProgress))
				{
					IOdbcConnection odbcConnection = this.CreateConnection(false, catalog);
					IPageReader pageReader = null;
					OdbcDataSource.OdbcDiagnosticEventArgs odbcDiagnosticEventArgs = null;
					try
					{
						odbcConnection.Open();
						if (this.Info.PrepareStatements)
						{
							OdbcStatementRegistration odbcStatementRegistration = odbcConnection.Prepare(commandText, this.statementRegistrar);
							odbcDiagnosticEventArgs = this.SendQueryStartDiagnosticEvent(commandText, parameters, rowRange, odbcStatementRegistration.Statement);
							pageReader = odbcConnection.Execute(odbcStatementRegistration.Statement, parameters, rowRange);
							pageReader = pageReader.AfterDispose(new Action(odbcStatementRegistration.Unregister));
						}
						else
						{
							odbcDiagnosticEventArgs = this.SendQueryStartDiagnosticEvent(commandText, parameters, rowRange, null);
							pageReader = odbcConnection.ExecuteDirect(commandText, parameters, rowRange, this.statementRegistrar);
						}
						pageReader = pageReader.AfterDispose(new Action(odbcConnection.Dispose));
						pageReader = new OdbcDataSource.ExceptionHandlingPageReader(pageReader, new Action<Action>(this.ExceptionHandler.InvokeWithoutRetry));
						if (columnConversions != null)
						{
							pageReader = ColumnConvertingPageReader.New(pageReader, (from a in columnConversions.Select((ColumnConversion value, int index) => new { index, value })
								where a.value != null
								select a).ToDictionary(pair => pair.index, pair => pair.value));
						}
						if (columnNames != null)
						{
							pageReader = new ColumnRenamePageReader(pageReader, columnNames);
						}
						pageReader = new ProgressPageReader(pageReader, hostProgress);
						if (odbcDiagnosticEventArgs != null)
						{
							pageReader = pageReader.AfterDispose(delegate
							{
								this.SendQueryFinishDiagnosticEvent(odbcDiagnosticEventArgs);
							});
						}
						pageReader2 = pageReader;
					}
					catch (Exception ex)
					{
						if (odbcDiagnosticEventArgs != null)
						{
							odbcDiagnosticEventArgs.ExceptionMessage = ex.Message;
							pageReader = ((pageReader != null) ? pageReader.AfterDispose(delegate
							{
								this.SendQueryFinishDiagnosticEvent(odbcDiagnosticEventArgs);
							}) : null);
						}
						if (pageReader != null)
						{
							pageReader.Dispose();
						}
						odbcConnection.Dispose();
						throw;
					}
				}
				return pageReader2;
			});
		}

		// Token: 0x06002ECB RID: 11979 RVA: 0x0008EFE8 File Offset: 0x0008D1E8
		public StructuredCacheKey GetCacheKey(string commandText, string catalog, IList<OdbcParameter> parameters, RowRange range)
		{
			string[] array = new string[3 * parameters.Count + 4];
			int num = 0;
			array[num++] = "SQLFetch/7";
			array[num++] = commandText;
			array[num++] = range.SkipCount.ToString();
			array[num++] = range.TakeCount.ToString();
			foreach (OdbcParameter odbcParameter in parameters)
			{
				array[num++] = odbcParameter.BindType.CType.ToString();
				array[num++] = odbcParameter.BindType.SqlType.ToString();
				array[num++] = this.GetStringValue(odbcParameter.BindType, odbcParameter.Value);
			}
			return this.CurrentCacheContext.WithCatalog(catalog).GetStructuredCacheKey(array);
		}

		// Token: 0x06002ECC RID: 11980 RVA: 0x0008F0FC File Offset: 0x0008D2FC
		private string GetStringValue(OdbcTypeMap map, object value)
		{
			if (value == null)
			{
				return "null";
			}
			if (value == DBNull.Value)
			{
				return "DBNull";
			}
			Odbc32.SQL_C ctype = map.CType;
			if (ctype <= Odbc32.SQL_C.BIT)
			{
				if (ctype - Odbc32.SQL_C.UTINYINT > 3 && ctype - Odbc32.SQL_C.ULONG > 3 && ctype - Odbc32.SQL_C.WCHAR > 1)
				{
					goto IL_0077;
				}
			}
			else if (ctype <= Odbc32.SQL_C.NUMERIC)
			{
				if (ctype == Odbc32.SQL_C.BINARY)
				{
					return Convert.ToBase64String((byte[])value);
				}
				if (ctype - Odbc32.SQL_C.CHAR > 1)
				{
					goto IL_0077;
				}
			}
			else if (ctype - Odbc32.SQL_C.FLOAT > 1 && ctype - Odbc32.SQL_C.TYPE_DATE > 2)
			{
				goto IL_0077;
			}
			return string.Format(CultureInfo.InvariantCulture, "~{0}", value);
			IL_0077:
			throw new InvalidOperationException(map.CType.ToString());
		}

		// Token: 0x06002ECD RID: 11981 RVA: 0x0008F19C File Offset: 0x0008D39C
		private OdbcDataSource.OdbcDiagnosticEventArgs SendQueryStartDiagnosticEvent(string commandText, IList<OdbcParameter> parameters, RowRange rowRange, OdbcStatementHandle statement)
		{
			IDiagnosticsService diagnosticsService = this.host.QueryService<IDiagnosticsService>();
			if (diagnosticsService != null && diagnosticsService.EnabledChannels.Contains("Odbc"))
			{
				OdbcDataSource.OdbcDiagnosticEventArgs odbcDiagnosticEventArgs = new OdbcDataSource.OdbcDiagnosticEventArgs
				{
					CommandIdentifier = Guid.NewGuid(),
					StartTime = DateTime.UtcNow
				};
				if (statement != null)
				{
					odbcDiagnosticEventArgs.Statement = (long)statement.DangerousGetHandle();
				}
				Dictionary<string, object> dictionary = new Dictionary<string, object>
				{
					{ "Identifier", odbcDiagnosticEventArgs.CommandIdentifier },
					{ "Statement", odbcDiagnosticEventArgs.Statement },
					{ "Command", commandText },
					{
						"MaxRows",
						rowRange.TakeCount.IsInfinite ? (-1L) : rowRange.TakeCount.Value
					},
					{ "ParameterCount", parameters.Count }
				};
				diagnosticsService.Emit("Odbc", "QueryStart", odbcDiagnosticEventArgs.StartTime, this.resource, dictionary);
				return odbcDiagnosticEventArgs;
			}
			return null;
		}

		// Token: 0x06002ECE RID: 11982 RVA: 0x0008F2AC File Offset: 0x0008D4AC
		private void SendQueryFinishDiagnosticEvent(OdbcDataSource.OdbcDiagnosticEventArgs odbcDiagnosticEventArgs)
		{
			IDiagnosticsService diagnosticsService = this.host.QueryService<IDiagnosticsService>();
			TimeSpan timeSpan = DateTime.UtcNow.Subtract(odbcDiagnosticEventArgs.StartTime);
			Dictionary<string, object> dictionary = new Dictionary<string, object>
			{
				{ "Identifier", odbcDiagnosticEventArgs.CommandIdentifier },
				{ "Statement", odbcDiagnosticEventArgs.Statement },
				{ "ExecutionDuration", timeSpan },
				{ "RowCount", null },
				{ "ExceptionMessage", odbcDiagnosticEventArgs.ExceptionMessage }
			};
			IGetDataSourceProgress getDataSourceProgress = this.host.QueryService<IGetDataSourceProgress>();
			DataSourceProgress2 dataSourceProgress = ((getDataSourceProgress != null) ? getDataSourceProgress.GetDataSourceProgress().SingleOrDefault((DataSourceProgress2 dsp) => dsp.DataSource == this.resource.Path && dsp.DataSourceType == this.resource.Kind) : null);
			dictionary["RowCount"] = ((dataSourceProgress != null) ? new long?(dataSourceProgress.RowsRead) : null);
			diagnosticsService.Emit("Odbc", "QueryFinish", DateTime.UtcNow, this.resource, dictionary);
		}

		// Token: 0x04001498 RID: 5272
		public static readonly IPool ConnectionPool = new TimeToLivePool(new Pool(), TimeSpan.FromMinutes(1.0));

		// Token: 0x04001499 RID: 5273
		private static readonly Keys attributeKeys = Keys.New("Attribute", "Value");

		// Token: 0x0400149A RID: 5274
		private const string diagnosticChannelName = "Odbc";

		// Token: 0x0400149B RID: 5275
		private readonly IEngineHost host;

		// Token: 0x0400149C RID: 5276
		private readonly ConnectionStringHandler connectionStringHandler;

		// Token: 0x0400149D RID: 5277
		private readonly string sourceConnectionString;

		// Token: 0x0400149E RID: 5278
		private readonly Value sourceConnectionValue;

		// Token: 0x0400149F RID: 5279
		private readonly Dictionary<OdbcIdentifier, OdbcTableInfo> tables;

		// Token: 0x040014A0 RID: 5280
		private readonly IResource resource;

		// Token: 0x040014A1 RID: 5281
		private readonly OdbcTypeInfoCollection types;

		// Token: 0x040014A2 RID: 5282
		private readonly OdbcOptions options;

		// Token: 0x040014A3 RID: 5283
		private readonly OdbcExceptionHandler odbcExceptionHandler;

		// Token: 0x040014A4 RID: 5284
		private readonly IOdbcStatementRegistrar statementRegistrar;

		// Token: 0x040014A5 RID: 5285
		private readonly Action<IHostTrace> additionalTraces;

		// Token: 0x040014A6 RID: 5286
		private readonly Tracer tracer;

		// Token: 0x040014A7 RID: 5287
		private bool initialized;

		// Token: 0x040014A8 RID: 5288
		private IOdbcService odbcService;

		// Token: 0x040014A9 RID: 5289
		private ResourceCredentialCollection credentials;

		// Token: 0x040014AA RID: 5290
		private DbExceptionHandler exceptionHandler;

		// Token: 0x040014AB RID: 5291
		private SqlSettings sqlSettings;

		// Token: 0x040014AC RID: 5292
		private OdbcDataSourceInfo info;

		// Token: 0x040014AD RID: 5293
		private bool refreshConnectionString;

		// Token: 0x040014AE RID: 5294
		private string connectionString;

		// Token: 0x040014AF RID: 5295
		private OdbcCacheContext currentCacheContext;

		// Token: 0x040014B0 RID: 5296
		private Dictionary<int, object> connectionAttributes;

		// Token: 0x040014B1 RID: 5297
		private string version;

		// Token: 0x020005DC RID: 1500
		private sealed class ExceptionHandlingPageReader : DelegatingPageReader
		{
			// Token: 0x06002ED3 RID: 11987 RVA: 0x0008F438 File Offset: 0x0008D638
			public ExceptionHandlingPageReader(IPageReader reader, Action<Action> exceptionHandler)
				: base(reader)
			{
				this.exceptionHandler = exceptionHandler;
			}

			// Token: 0x06002ED4 RID: 11988 RVA: 0x0008F448 File Offset: 0x0008D648
			public override void Read(IPage page)
			{
				this.exceptionHandler(delegate
				{
					this.<>n__0(page);
				});
			}

			// Token: 0x040014B2 RID: 5298
			private readonly Action<Action> exceptionHandler;
		}

		// Token: 0x020005DE RID: 1502
		private class OdbcDiagnosticEventArgs
		{
			// Token: 0x17001115 RID: 4373
			// (get) Token: 0x06002ED8 RID: 11992 RVA: 0x0008F49C File Offset: 0x0008D69C
			// (set) Token: 0x06002ED9 RID: 11993 RVA: 0x0008F4A4 File Offset: 0x0008D6A4
			public Guid CommandIdentifier { get; set; }

			// Token: 0x17001116 RID: 4374
			// (get) Token: 0x06002EDA RID: 11994 RVA: 0x0008F4AD File Offset: 0x0008D6AD
			// (set) Token: 0x06002EDB RID: 11995 RVA: 0x0008F4B5 File Offset: 0x0008D6B5
			public DateTime StartTime { get; set; }

			// Token: 0x17001117 RID: 4375
			// (get) Token: 0x06002EDC RID: 11996 RVA: 0x0008F4BE File Offset: 0x0008D6BE
			// (set) Token: 0x06002EDD RID: 11997 RVA: 0x0008F4C6 File Offset: 0x0008D6C6
			public long Statement { get; set; }

			// Token: 0x17001118 RID: 4376
			// (get) Token: 0x06002EDE RID: 11998 RVA: 0x0008F4CF File Offset: 0x0008D6CF
			// (set) Token: 0x06002EDF RID: 11999 RVA: 0x0008F4D7 File Offset: 0x0008D6D7
			public string ExceptionMessage { get; set; }
		}
	}
}
