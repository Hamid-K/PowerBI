using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Data.Serialization;
using Microsoft.Internal;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Language.Typeflow;
using Microsoft.Mashup.Engine1.Library.Action;
using Microsoft.Mashup.Engine1.Library.AdoDotNet;
using Microsoft.Mashup.Engine1.Library.Common.Creators;
using Microsoft.Mashup.Engine1.Library.Common.Navigation;
using Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;
using Microsoft.OleDb;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x02001059 RID: 4185
	internal abstract class DbEnvironment : EnvironmentBase, IDbService
	{
		// Token: 0x06006D4C RID: 27980 RVA: 0x001788E8 File Offset: 0x00176AE8
		protected DbEnvironment(IEngineHost host, IResource resource, string dataSourceName, string server, string database, Value options, IDataSourceLocation location = null, DbTransactionInfo transactionInfo = null)
			: base(host, dataSourceName + "/FoldingWarning")
		{
			this.dataSourceName = dataSourceName;
			this.server = server;
			this.database = database;
			this.options = options;
			this.resource = resource;
			this.dbService = base.Host.Hook(() => this);
			this.metadataCache = base.Host.QueryService<ICacheSets>().Metadata.PersistentObjectCache;
			this.knownExceptionService = base.Host.QueryService<IKnownExceptionService>();
			this.location = location;
			this.transactionInfo = transactionInfo;
			if (this.transactionInfo != null)
			{
				this.transactionInfo.SetGetConnection(new Func<DbConnection>(this.CreateConnectionInternal));
			}
		}

		// Token: 0x17001F00 RID: 7936
		// (get) Token: 0x06006D4D RID: 27981 RVA: 0x001789A5 File Offset: 0x00176BA5
		protected virtual IDictionary<string, TableType> TableTypes
		{
			get
			{
				return DbEnvironment.defaultTableTypes;
			}
		}

		// Token: 0x17001F01 RID: 7937
		// (get) Token: 0x06006D4E RID: 27982 RVA: 0x001789AC File Offset: 0x00176BAC
		public OptionsRecord UserOptions
		{
			get
			{
				if (this.userOptions == null)
				{
					this.userOptions = this.ValidOptions.CreateOptions(this.dataSourceName, this.options);
				}
				return this.userOptions;
			}
		}

		// Token: 0x17001F02 RID: 7938
		// (get) Token: 0x06006D4F RID: 27983 RVA: 0x001789D9 File Offset: 0x00176BD9
		public RecordValue OptionsRecord
		{
			get
			{
				if (!this.options.IsRecord)
				{
					return RecordValue.Empty;
				}
				return this.options.AsRecord;
			}
		}

		// Token: 0x17001F03 RID: 7939
		// (get) Token: 0x06006D50 RID: 27984 RVA: 0x001789FC File Offset: 0x00176BFC
		public NavigationPropertiesHelper.NameGenerator NameGenerator
		{
			get
			{
				if (this.nameGenerator == null)
				{
					this.nameGenerator = new NavigationPropertiesHelper.NameGenerator(NavigationPropertiesHelper.DefaultNameGenerator);
					object obj;
					if (this.UserOptions.TryGetValue("NavigationPropertyNameGenerator", out obj))
					{
						this.nameGenerator = (NavigationPropertiesHelper.NameGenerator)obj;
					}
				}
				return this.nameGenerator;
			}
		}

		// Token: 0x06006D51 RID: 27985 RVA: 0x00178A4C File Offset: 0x00176C4C
		public NavigationPropertiesHelper.NavigationPropertiesRecord GetNavigationPropertiesRecord(SchemaItem? itemFilter)
		{
			if (this.TransactionInfo != null && itemFilter != null)
			{
				return new NavigationPropertiesHelper.NavigationPropertiesRecord(this.GetItemCatalog(itemFilter), this, this.NameGenerator);
			}
			SchemaItem schemaItem;
			Value value;
			if (!string.IsNullOrEmpty((itemFilter != null) ? itemFilter.GetValueOrDefault().Item : null) && this.TryLookupDatabaseItem(itemFilter.Value, out schemaItem, out value))
			{
				Dictionary<SchemaItem, Value> dictionary = new Dictionary<SchemaItem, Value>(1);
				if (!value.IsNull)
				{
					dictionary.Add(schemaItem, value);
				}
				return new NavigationPropertiesHelper.NavigationPropertiesRecord(dictionary, this, this.NameGenerator);
			}
			return this.NavigationPropertiesRecord;
		}

		// Token: 0x17001F04 RID: 7940
		// (get) Token: 0x06006D52 RID: 27986 RVA: 0x00178ADE File Offset: 0x00176CDE
		public NavigationPropertiesHelper.NavigationPropertiesRecord NavigationPropertiesRecord
		{
			get
			{
				if (this.navigationPropertiesRecord == null)
				{
					this.navigationPropertiesRecord = new NavigationPropertiesHelper.NavigationPropertiesRecord(this.ItemCatalog, this, this.NameGenerator);
				}
				return this.navigationPropertiesRecord;
			}
		}

		// Token: 0x17001F05 RID: 7941
		// (get) Token: 0x06006D53 RID: 27987 RVA: 0x00178B06 File Offset: 0x00176D06
		public virtual IDataSourceCapabilities DataSourceCapabilities
		{
			get
			{
				return this.SqlSettings;
			}
		}

		// Token: 0x17001F06 RID: 7942
		// (get) Token: 0x06006D54 RID: 27988 RVA: 0x00178B0E File Offset: 0x00176D0E
		public SqlSettings SqlSettings
		{
			get
			{
				if (this.sqlSettings == null)
				{
					this.sqlSettings = this.LoadSqlSettings();
				}
				return this.sqlSettings;
			}
		}

		// Token: 0x06006D55 RID: 27989 RVA: 0x00178B2C File Offset: 0x00176D2C
		public TableValue CreateTable()
		{
			string text;
			if (this.UserOptions.TryGetString("Query", out text) && text != null)
			{
				return new NativeQueryTableValue(base.Host, this, this.CreateCatalogTableValue(base.Host, null), text);
			}
			if (!this.UserOptions.GetBool("HierarchicalNavigation", false))
			{
				return this.CreateCatalogTableValue(base.Host, null);
			}
			return this.CreateHierarchicalNavigationTableValue();
		}

		// Token: 0x17001F07 RID: 7943
		// (get) Token: 0x06006D56 RID: 27990 RVA: 0x00178B92 File Offset: 0x00176D92
		public override IResource Resource
		{
			get
			{
				return this.resource;
			}
		}

		// Token: 0x17001F08 RID: 7944
		// (get) Token: 0x06006D57 RID: 27991 RVA: 0x00178B9C File Offset: 0x00176D9C
		public string CacheKey
		{
			get
			{
				return this.ConnectionInfo.CacheKey;
			}
		}

		// Token: 0x17001F09 RID: 7945
		// (get) Token: 0x06006D58 RID: 27992 RVA: 0x00178BB7 File Offset: 0x00176DB7
		public string Server
		{
			get
			{
				return this.server;
			}
		}

		// Token: 0x17001F0A RID: 7946
		// (get) Token: 0x06006D59 RID: 27993 RVA: 0x00178BBF File Offset: 0x00176DBF
		public string Database
		{
			get
			{
				return this.database;
			}
		}

		// Token: 0x17001F0B RID: 7947
		// (get) Token: 0x06006D5A RID: 27994 RVA: 0x00178BC7 File Offset: 0x00176DC7
		public IDataSourceLocation Location
		{
			get
			{
				return this.location;
			}
		}

		// Token: 0x17001F0C RID: 7948
		// (get) Token: 0x06006D5B RID: 27995 RVA: 0x00178BCF File Offset: 0x00176DCF
		public DbTransactionInfo TransactionInfo
		{
			get
			{
				return this.transactionInfo;
			}
		}

		// Token: 0x06006D5C RID: 27996 RVA: 0x00178BD7 File Offset: 0x00176DD7
		public IDictionary<SchemaItem, Value> GetItemCatalog(SchemaItem? itemFilter)
		{
			if (this.TransactionInfo != null && itemFilter != null)
			{
				return this.LoadCatalog(itemFilter);
			}
			return this.ItemCatalog;
		}

		// Token: 0x17001F0D RID: 7949
		// (get) Token: 0x06006D5D RID: 27997 RVA: 0x00178BF8 File Offset: 0x00176DF8
		public IDictionary<SchemaItem, Value> ItemCatalog
		{
			get
			{
				if (this.itemCatalog == null)
				{
					this.itemCatalog = this.LoadCatalog(null);
				}
				return this.itemCatalog;
			}
		}

		// Token: 0x17001F0E RID: 7950
		// (get) Token: 0x06006D5E RID: 27998 RVA: 0x00178C28 File Offset: 0x00176E28
		public bool IsItemCatalogLoaded
		{
			get
			{
				return this.itemCatalog != null;
			}
		}

		// Token: 0x17001F0F RID: 7951
		// (get) Token: 0x06006D5F RID: 27999 RVA: 0x00178C34 File Offset: 0x00176E34
		public IEnumerable<string> AllSchemas
		{
			get
			{
				if (this.allSchemas == null)
				{
					this.allSchemas = from DataRow row in this.GetSchemas().Rows
						select (string)row["SCHEMA_NAME"];
				}
				return this.allSchemas;
			}
		}

		// Token: 0x17001F10 RID: 7952
		// (get) Token: 0x06006D60 RID: 28000 RVA: 0x00178C89 File Offset: 0x00176E89
		public virtual DbCommandTypeMap ParameterTypeMap
		{
			get
			{
				return DbCommandTypeMap.Default;
			}
		}

		// Token: 0x17001F11 RID: 7953
		// (get) Token: 0x06006D61 RID: 28001 RVA: 0x00002105 File Offset: 0x00000305
		public virtual bool SupportsNativeQueryFolding
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001F12 RID: 7954
		// (get) Token: 0x06006D62 RID: 28002 RVA: 0x00002105 File Offset: 0x00000305
		public virtual bool SupportsNativeQueryTypePreservation
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001F13 RID: 7955
		// (get) Token: 0x06006D63 RID: 28003
		public abstract OptionRecordDefinition ValidOptions { get; }

		// Token: 0x17001F14 RID: 7956
		// (get) Token: 0x06006D64 RID: 28004 RVA: 0x00178C90 File Offset: 0x00176E90
		private OptionRecordDefinition NativeQueryOptions
		{
			get
			{
				ArrayBuilder<OptionItem> arrayBuilder = new ArrayBuilder<OptionItem>(2);
				if (this.SupportsNativeQueryFolding)
				{
					arrayBuilder.Add(new OptionItem("EnableFolding", NullableTypeValue.Logical));
				}
				if (this.SupportsNativeQueryTypePreservation)
				{
					arrayBuilder.Add(new OptionItem("PreserveTypes", NullableTypeValue.Logical));
				}
				return new OptionRecordDefinition(arrayBuilder.ToArray());
			}
		}

		// Token: 0x17001F15 RID: 7957
		// (get) Token: 0x06006D65 RID: 28005 RVA: 0x00002105 File Offset: 0x00000305
		public virtual bool SuppressNativeQueryPermissionChallenge
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001F16 RID: 7958
		// (get) Token: 0x06006D66 RID: 28006 RVA: 0x00178CF0 File Offset: 0x00176EF0
		public int? CommandTimeout
		{
			get
			{
				if (!this.commandTimeoutSet)
				{
					int num;
					if (this.UserOptions.TryGetDurationAsSeconds("CommandTimeout", out num))
					{
						if (num > this.MaxTimeoutSeconds)
						{
							throw ValueException.NewExpressionError<Message2>(Strings.DurationIsTooLarge("CommandTimeout", this.MaxTimeoutSeconds), DurationValue.New(TimeSpan.FromSeconds((double)num)), null);
						}
						if (num > 0)
						{
							this.commandTimeout = new int?(num);
						}
					}
					this.commandTimeoutSet = true;
				}
				return this.commandTimeout;
			}
		}

		// Token: 0x17001F17 RID: 7959
		// (get) Token: 0x06006D67 RID: 28007 RVA: 0x00178D68 File Offset: 0x00176F68
		public int? ConnectionTimeout
		{
			get
			{
				if (!this.connectionTimeoutSet)
				{
					object obj;
					if (this.UserOptions.TryGetValue("ConnectionTimeout", out obj) && obj is TimeSpan)
					{
						int num = (int)Math.Round(((TimeSpan)obj).TotalSeconds);
						if (num > 0)
						{
							this.connectionTimeout = new int?(num);
						}
					}
					this.connectionTimeoutSet = true;
				}
				return this.connectionTimeout;
			}
		}

		// Token: 0x17001F18 RID: 7960
		// (get) Token: 0x06006D68 RID: 28008 RVA: 0x00178DCB File Offset: 0x00176FCB
		protected ConnectionInfo ConnectionInfo
		{
			get
			{
				if (this.connectionInfo == null)
				{
					this.connectionInfo = new ConnectionInfo?(this.CreateConnectionInfo());
				}
				return this.connectionInfo.Value;
			}
		}

		// Token: 0x06006D69 RID: 28009 RVA: 0x00178DF6 File Offset: 0x00176FF6
		protected void ClearConnectionInfo()
		{
			this.connectionInfo = null;
		}

		// Token: 0x17001F19 RID: 7961
		// (get) Token: 0x06006D6A RID: 28010 RVA: 0x00178E04 File Offset: 0x00177004
		protected IDbService DbService
		{
			get
			{
				return this.dbService;
			}
		}

		// Token: 0x17001F1A RID: 7962
		// (get) Token: 0x06006D6B RID: 28011 RVA: 0x00178E0C File Offset: 0x0017700C
		public Tracer Tracer
		{
			get
			{
				if (this.tracer == null)
				{
					Dictionary<string, object> dictionary = new Dictionary<string, object> { { "Server", this.Server } };
					if (!string.IsNullOrEmpty(this.Database))
					{
						dictionary.Add("Database", this.Database);
					}
					this.tracer = new Tracer(base.Host, "Engine/IO/Db/" + this.DataSourceNameString + "/", this.Resource, dictionary, new Func<IHostTrace, Exception, bool>(this.TraceException));
				}
				return this.tracer;
			}
		}

		// Token: 0x17001F1B RID: 7963
		// (get) Token: 0x06006D6C RID: 28012 RVA: 0x00178E95 File Offset: 0x00177095
		public IHostProgress HostProgressService
		{
			get
			{
				if (this.hostProgressService == null)
				{
					this.hostProgressService = ProgressService.GetHostProgress(base.Host, this.Resource.Kind, this.resource.Path);
				}
				return this.hostProgressService;
			}
		}

		// Token: 0x17001F1C RID: 7964
		// (get) Token: 0x06006D6D RID: 28013
		protected abstract string ProviderName { get; }

		// Token: 0x17001F1D RID: 7965
		// (get) Token: 0x06006D6E RID: 28014 RVA: 0x000020FA File Offset: 0x000002FA
		protected virtual string ProviderDownloadLink
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001F1E RID: 7966
		// (get) Token: 0x06006D6F RID: 28015 RVA: 0x000020FA File Offset: 0x000002FA
		protected virtual string ProviderLibraryName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001F1F RID: 7967
		// (get) Token: 0x06006D70 RID: 28016 RVA: 0x00178ECC File Offset: 0x001770CC
		protected virtual int MaxTimeoutSeconds
		{
			get
			{
				return int.MaxValue;
			}
		}

		// Token: 0x17001F20 RID: 7968
		// (get) Token: 0x06006D71 RID: 28017 RVA: 0x00178ED3 File Offset: 0x001770D3
		protected IPersistentObjectCache MetadataCache
		{
			get
			{
				return this.metadataCache;
			}
		}

		// Token: 0x17001F21 RID: 7969
		// (get) Token: 0x06006D72 RID: 28018 RVA: 0x00178EDB File Offset: 0x001770DB
		protected ResourceCredentialCollection Credentials
		{
			get
			{
				this.EnsureCredentials();
				return this.credentials;
			}
		}

		// Token: 0x17001F22 RID: 7970
		// (get) Token: 0x06006D73 RID: 28019 RVA: 0x00178EE9 File Offset: 0x001770E9
		public virtual bool CreateRelationships
		{
			get
			{
				return this.UserOptions.GetBool("CreateNavigationProperties", true);
			}
		}

		// Token: 0x17001F23 RID: 7971
		// (get) Token: 0x06006D74 RID: 28020 RVA: 0x00178EFC File Offset: 0x001770FC
		public virtual bool PrefetchMetadata
		{
			get
			{
				return this.CreateRelationships;
			}
		}

		// Token: 0x17001F24 RID: 7972
		// (get) Token: 0x06006D75 RID: 28021 RVA: 0x00178F04 File Offset: 0x00177104
		public virtual bool UnsafeTypeConversions
		{
			get
			{
				return this.UserOptions.GetBool("UnsafeTypeConversions", false);
			}
		}

		// Token: 0x06006D76 RID: 28022
		protected abstract SqlSettings LoadSqlSettings();

		// Token: 0x06006D77 RID: 28023 RVA: 0x00178F17 File Offset: 0x00177117
		public void EnsureCredentials()
		{
			if (this.credentials == null)
			{
				this.credentials = HostResourcePermissionService.VerifyPermissionAndGetCredentials(base.Host, this.resource, this.location);
			}
		}

		// Token: 0x06006D78 RID: 28024 RVA: 0x00178F3E File Offset: 0x0017713E
		public override EnvironmentStatementBuilder NewStatementBuilder(TableValue table, ValueBuilderBase valueBuilder, IExpression expression)
		{
			return new DbStatementBuilder(table, this, (DbValueBuilder)valueBuilder, expression);
		}

		// Token: 0x06006D79 RID: 28025 RVA: 0x00178F50 File Offset: 0x00177150
		public bool TryGetOleDbClient(out OleDbClient client)
		{
			bool flag;
			using (IHostTrace hostTrace = this.Tracer.CreateTrace("TryGetOleDbClient", TraceEventType.Information))
			{
				try
				{
					if (this.TryGetOleDbClientCore(out client))
					{
						client = new DbEnvironment.WrappedOleDbClient(this, client);
						hostTrace.Add("Result", true, false);
						return true;
					}
				}
				catch (Exception ex)
				{
					if (!Microsoft.Mashup.Common.SafeExceptions.TraceIsSafeException(hostTrace, ex))
					{
						throw;
					}
				}
				hostTrace.Add("Result", false, false);
				client = null;
				flag = false;
			}
			return flag;
		}

		// Token: 0x06006D7A RID: 28026 RVA: 0x000E6755 File Offset: 0x000E4955
		protected virtual bool TryGetOleDbClientCore(out OleDbClient client)
		{
			client = null;
			return false;
		}

		// Token: 0x06006D7B RID: 28027 RVA: 0x000912D6 File Offset: 0x0008F4D6
		public virtual bool TryGetBulkCopy(string schema, string table, out IBulkCopy bulkCopy)
		{
			bulkCopy = null;
			return false;
		}

		// Token: 0x06006D7C RID: 28028 RVA: 0x00178FE8 File Offset: 0x001771E8
		public bool TryGetBulkCopy(TableValue targetTable, out IBulkCopy bulkCopy)
		{
			Value value = targetTable.Type.MetaValue["Sql.Schema"];
			Value value2 = targetTable.Type.MetaValue["Sql.Table"];
			string text = (value.IsNull ? null : value.AsString);
			string asString = value2.AsString;
			return this.dbService.TryGetBulkCopy(text, asString, out bulkCopy);
		}

		// Token: 0x06006D7D RID: 28029 RVA: 0x00179046 File Offset: 0x00177246
		public virtual bool TryGetProviderMissingException(ArgumentException e, out Exception providerMissingException)
		{
			if (this.ProviderDownloadLink == null || this.ProviderLibraryName == null)
			{
				providerMissingException = null;
				return false;
			}
			providerMissingException = this.DownloadLinkException(Strings.DatabaseProviderMissingExceptionMessage(this.ProviderName), e);
			return true;
		}

		// Token: 0x06006D7E RID: 28030 RVA: 0x00179078 File Offset: 0x00177278
		private ValueException DownloadLinkException(string message, Exception innerException)
		{
			string text = message + Environment.NewLine + DbEnvironment.GetClientSoftwareNotFoundExceptionMessage(this.ProviderLibraryName, this.ProviderDownloadLink);
			return DataSourceException.NewMissingClientLibraryError<Message2>(base.Host, DataSourceException.DataSourceMessage(this.dataSourceName, text), this.resource, this.ProviderLibraryName, this.ProviderDownloadLink, innerException);
		}

		// Token: 0x06006D7F RID: 28031
		public abstract DbAstCreator NewAstCreator(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor);

		// Token: 0x06006D80 RID: 28032 RVA: 0x001790D1 File Offset: 0x001772D1
		protected override ValueBuilderBase Compile(Query originalQuery, IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor)
		{
			return new DbValueBuilder(this, this.NewAstCreator(expression, cursor).Create(expression), null);
		}

		// Token: 0x06006D81 RID: 28033 RVA: 0x001790E8 File Offset: 0x001772E8
		protected override ActionValue CompileStatement(Query targetQuery, IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor, string statementType)
		{
			return DbStatementActionValue.New(this, targetQuery, this.NewAstCreator(expression, cursor).CreateStatementPlan(), statementType, null);
		}

		// Token: 0x06006D82 RID: 28034
		protected abstract ConnectionStringResourceCredentialDispatcher CreateConnectionStringDispatcher();

		// Token: 0x06006D83 RID: 28035 RVA: 0x00179101 File Offset: 0x00177301
		protected virtual ConnectionInfo CreateConnectionInfo()
		{
			return this.CreateConnectionStringDispatcher().ConstructConnectionString(this.server, this.database, this.Credentials, this.ConnectionTimeout);
		}

		// Token: 0x06006D84 RID: 28036 RVA: 0x00179128 File Offset: 0x00177328
		public virtual DbConnection CreateDbConnection()
		{
			DbProviderFactory dbProviderFactory;
			try
			{
				dbProviderFactory = this.CreateDbProviderFactory();
			}
			catch (ArgumentException ex)
			{
				Exception ex2;
				if (this.TryGetProviderMissingException(ex, out ex2))
				{
					throw ex2;
				}
				throw;
			}
			return dbProviderFactory.CreateConnection();
		}

		// Token: 0x06006D85 RID: 28037 RVA: 0x00179164 File Offset: 0x00177364
		protected virtual DbProviderFactory CreateDbProviderFactory()
		{
			DbProviderFactory factory;
			try
			{
				factory = DbProviderFactories.GetFactory(this.ProviderName);
			}
			catch (ConfigurationErrorsException ex)
			{
				throw DataSourceException.NewDataSourceError<Message2>(base.Host, Strings.ConfigurationErrorsExceptionMessage(ex.Message, DbEnvironment.MachineConfigPath), this.resource, null, ex);
			}
			return factory;
		}

		// Token: 0x06006D86 RID: 28038 RVA: 0x001791B8 File Offset: 0x001773B8
		protected bool IsProviderInstalled(string providerName)
		{
			try
			{
				DbProviderFactories.GetFactory(providerName);
				return true;
			}
			catch (ArgumentException)
			{
			}
			catch (ConfigurationErrorsException ex)
			{
				throw DataSourceException.NewDataSourceError<Message2>(base.Host, Strings.ConfigurationErrorsExceptionMessage(ex.Message, DbEnvironment.MachineConfigPath), this.Resource, null, ex);
			}
			catch (TargetInvocationException)
			{
			}
			return false;
		}

		// Token: 0x06006D87 RID: 28039 RVA: 0x00179228 File Offset: 0x00177428
		protected void EnsureServerMetadata()
		{
			this.serverMetadata = this.ServerMetadata;
		}

		// Token: 0x06006D88 RID: 28040 RVA: 0x00179238 File Offset: 0x00177438
		protected virtual DbConnection WrapConnection(DbConnection baseConnection)
		{
			return new WrappedDbConnection(this, baseConnection, this.ConnectionInfo.Impersonate);
		}

		// Token: 0x06006D89 RID: 28041 RVA: 0x0017925A File Offset: 0x0017745A
		public DbConnection CreateConnection()
		{
			this.EnsureServerMetadata();
			if (this.TransactionInfo != null)
			{
				return this.TransactionInfo.GetConnection();
			}
			return this.CreateConnectionInternal();
		}

		// Token: 0x06006D8A RID: 28042 RVA: 0x0017927C File Offset: 0x0017747C
		private DbConnection CreateConnectionInternal()
		{
			DbConnection dbConnection2;
			try
			{
				DbConnection dbConnection = this.dbService.CreateDbConnection();
				if (base.Host.QueryService<IConnectionGovernanceService>() != null)
				{
					dbConnection = new GovernedDbConnection(base.Host, this.Resource, dbConnection);
				}
				dbConnection = this.TraceConnection(dbConnection);
				dbConnection = this.WrapConnection(dbConnection);
				dbConnection.ConnectionString = this.ConnectionInfo.ConnectionString;
				dbConnection2 = dbConnection;
			}
			catch (ArgumentException ex)
			{
				throw ValueException.NewExpressionError(ex.Message, null, ex);
			}
			return dbConnection2;
		}

		// Token: 0x06006D8B RID: 28043 RVA: 0x00179300 File Offset: 0x00177500
		protected virtual TracingDbConnection TraceConnection(DbConnection connection)
		{
			return new TracingDbConnection(this.Tracer, connection, new Action<IHostTrace>(this.AdditionalCommandTraces), this.ConnectionInfo.RequireEncryption);
		}

		// Token: 0x06006D8C RID: 28044 RVA: 0x00179334 File Offset: 0x00177534
		public bool TraceException(IHostTrace trace, Exception exception)
		{
			return this.TraceException(trace, exception, TraceEventType.Error);
		}

		// Token: 0x06006D8D RID: 28045 RVA: 0x0017933F File Offset: 0x0017753F
		public virtual bool TraceException(IHostTrace trace, Exception exception, TraceEventType severity)
		{
			trace.Add(exception, severity, true);
			return false;
		}

		// Token: 0x06006D8E RID: 28046 RVA: 0x0000336E File Offset: 0x0000156E
		protected virtual void AdditionalCommandTraces(IHostTrace trace)
		{
		}

		// Token: 0x06006D8F RID: 28047 RVA: 0x0017934C File Offset: 0x0017754C
		public virtual void AbortCommand(DbCommand command, DbDataReader reader)
		{
			if (!reader.IsClosed)
			{
				try
				{
					command.Cancel();
				}
				catch (Exception ex)
				{
					using (IHostTrace hostTrace = this.Tracer.CreateTrace("AbortCommand", TraceEventType.Information))
					{
						this.TraceException(hostTrace, ex);
					}
					if (!(ex is DbException) && !(ex is IOException))
					{
						throw;
					}
				}
			}
			reader.Dispose();
		}

		// Token: 0x06006D90 RID: 28048 RVA: 0x001793C8 File Offset: 0x001775C8
		public virtual void TestConnection()
		{
			this.ConvertDbExceptions<int>(delegate
			{
				using (DbConnection dbConnection = this.CreateConnection())
				{
					dbConnection.Open();
				}
				return 0;
			});
		}

		// Token: 0x06006D91 RID: 28049 RVA: 0x001793E0 File Offset: 0x001775E0
		public override bool SupportsSkip(TableTypeValue type)
		{
			string text;
			return !this.UserOptions.TryGetString("Query", out text) && base.SupportsSkip(type);
		}

		// Token: 0x06006D92 RID: 28050 RVA: 0x0017940C File Offset: 0x0017760C
		public override bool SupportsTake(TableTypeValue type)
		{
			string text;
			return !this.UserOptions.TryGetString("Query", out text) && base.SupportsTake(type);
		}

		// Token: 0x06006D93 RID: 28051 RVA: 0x00179436 File Offset: 0x00177636
		public virtual bool SupportsPaging(TableTypeValue type)
		{
			return type.GetPrimaryKey() != null;
		}

		// Token: 0x06006D94 RID: 28052 RVA: 0x00179441 File Offset: 0x00177641
		public sealed override TableValue CreateCatalogTableValue(IEngineHost host)
		{
			return this.CreateCatalogTableValue(host, null);
		}

		// Token: 0x06006D95 RID: 28053 RVA: 0x0017944C File Offset: 0x0017764C
		public virtual TableValue CreateCatalogTableValue(IEngineHost host, string schema)
		{
			if (schema == null)
			{
				if (this.dbCatalog == null)
				{
					this.dbCatalog = new DbCatalogTableValue(this, null, null);
				}
				return this.dbCatalog;
			}
			return new DbCatalogTableValue(this, schema, null);
		}

		// Token: 0x06006D96 RID: 28054 RVA: 0x00179491 File Offset: 0x00177691
		protected TableValue CreateUpdatableCatalogTableValue(TableValue catalogTable, string schema)
		{
			return new QueryTableValue(new DbEnvironment.UpdatableCatalogQuery(this, catalogTable, schema), catalogTable.Type);
		}

		// Token: 0x06006D97 RID: 28055 RVA: 0x001794A6 File Offset: 0x001776A6
		protected virtual TableValue CreateHierarchicalNavigationTableValue()
		{
			return new DbHierarchicalNavigationTableValue(this, (string schema) => this.CreateCatalogTableValue(base.Host, schema));
		}

		// Token: 0x06006D98 RID: 28056 RVA: 0x001794BA File Offset: 0x001776BA
		protected TableValue CreateUpdatableHierarchicalNavigationTableValue(TableValue hierarchicalTable)
		{
			return new QueryTableValue(new DbEnvironment.UpdatableHierarchicalNavigationQuery(this, hierarchicalTable), hierarchicalTable.Type);
		}

		// Token: 0x06006D99 RID: 28057 RVA: 0x001794D0 File Offset: 0x001776D0
		protected TableValue SelectSchema(TableValue catalogTable, string schema)
		{
			if (schema != null)
			{
				RecordValue recordValue = RecordValue.New(Keys.New("Schema"), new Value[] { TextValue.New(schema) });
				catalogTable = catalogTable.SelectRows(new TableValue.RowSelectorFunctionValue(recordValue));
			}
			return catalogTable;
		}

		// Token: 0x06006D9A RID: 28058 RVA: 0x0017950E File Offset: 0x0017770E
		protected virtual bool TryLookupDatabaseItem(SchemaItem item, out SchemaItem adjustedItem, out Value data)
		{
			data = null;
			adjustedItem = item;
			return false;
		}

		// Token: 0x06006D9B RID: 28059 RVA: 0x0017951B File Offset: 0x0017771B
		public Value NativeQuery(Value target, TextValue query, Value parameters, Value options)
		{
			options = this.NativeQueryOptions.ValidateOptions(options, "Value.NativeQuery", false, true);
			return new NativeQueryTableValue(base.Host, this, target, query.AsString, parameters, options);
		}

		// Token: 0x06006D9C RID: 28060 RVA: 0x0017954C File Offset: 0x0017774C
		public ActionValue NativeStatement(Value target, TextValue statement, Value parameters, Value options)
		{
			if (!options.IsNull && options.AsRecord.Count > 0)
			{
				throw ValueException.NewExpressionError<Message1>(Strings.InvalidOptionWithNoValidOptions(options.AsRecord.Keys[0]), options, null);
			}
			this.EnsureCredentials();
			base.VerifyActionPermitted();
			return CountOnlyTableBindingActionValue.New((bool countOnlyTable) => DbStatementActionValue.New(this, target, statement.String, countOnlyTable, parameters, "NativeStatement")).ClearCache(base.Host);
		}

		// Token: 0x06006D9D RID: 28061 RVA: 0x001795DC File Offset: 0x001777DC
		protected TableValue AddColumnIdentity(SchemaItem identifier, TableValue table)
		{
			string text;
			if (this.TryGetRelationshipIdentity(identifier, out text))
			{
				table = table.ReplaceRelationshipIdentity(text);
			}
			return table;
		}

		// Token: 0x06006D9E RID: 28062 RVA: 0x001795FE File Offset: 0x001777FE
		protected virtual RetryBehavior RetryAfterSqlError(DbException error)
		{
			return new RetryBehavior(false, DbEnvironment.RetryDelay);
		}

		// Token: 0x06006D9F RID: 28063 RVA: 0x0000336E File Offset: 0x0000156E
		protected virtual void FinalizeOnRetry()
		{
		}

		// Token: 0x06006DA0 RID: 28064 RVA: 0x000912D6 File Offset: 0x0008F4D6
		public virtual bool TryExecuteCommand(TableTypeValue resultType, Func<string> commandTextCtor, out TableValue result)
		{
			result = null;
			return false;
		}

		// Token: 0x06006DA1 RID: 28065 RVA: 0x000BF254 File Offset: 0x000BD454
		public virtual bool TryExecuteNativeQuery(string query, Value parameters, Value options, out TableValue result)
		{
			result = null;
			return false;
		}

		// Token: 0x06006DA2 RID: 28066 RVA: 0x0017960C File Offset: 0x0017780C
		public virtual T ConvertDbExceptions<T>(Func<T> action)
		{
			T t;
			try
			{
				t = this.RunWithRetryGuard<T>(action);
			}
			catch (Exception ex)
			{
				Exception ex2;
				if (this.TryConvertException(ex, out ex2))
				{
					throw ex2;
				}
				throw;
			}
			return t;
		}

		// Token: 0x06006DA3 RID: 28067 RVA: 0x00179644 File Offset: 0x00177844
		public virtual bool IsTypeSupported(object type)
		{
			return Column.GetColumnType((Type)type) != ColumnType.Unsupported;
		}

		// Token: 0x06006DA4 RID: 28068 RVA: 0x00179658 File Offset: 0x00177858
		private T RunWithRetryGuard<T>(Func<T> action)
		{
			int num = 4;
			for (int i = 0; i < num; i++)
			{
				try
				{
					T t = action();
					this.TraceRetriesSucceeded(3, i);
					return t;
				}
				catch (DbException ex) when (this.ShouldRetry(3, i, ex))
				{
					if (i == 2)
					{
						this.FinalizeOnRetry();
					}
				}
				catch (Exception ex2) when (!(ex2 is DbException))
				{
					this.TraceRetriesFailed(3, ex2);
					throw;
				}
			}
			throw new InvalidOperationException(Strings.UnreachableCodePath);
		}

		// Token: 0x06006DA5 RID: 28069 RVA: 0x00179708 File Offset: 0x00177908
		private void TraceRetriesFailed(int maxRetry, Exception e)
		{
			using (IHostTrace hostTrace = this.Tracer.CreateTrace("RunWithRetry/Failure", TraceEventType.Error))
			{
				hostTrace.Add("MaxAttempt", maxRetry, false);
				this.TraceException(hostTrace, e);
			}
		}

		// Token: 0x06006DA6 RID: 28070 RVA: 0x00179760 File Offset: 0x00177960
		private void TraceRetriesSucceeded(int maxRetryAttempts, int attempt)
		{
			if (attempt > 0)
			{
				using (IHostTrace hostTrace = this.Tracer.CreateTrace("RunWithRetry/Success", TraceEventType.Information))
				{
					hostTrace.Add("Attempt", attempt, false);
					hostTrace.Add("MaxAttempt", maxRetryAttempts, false);
				}
			}
		}

		// Token: 0x06006DA7 RID: 28071 RVA: 0x001797C4 File Offset: 0x001779C4
		private bool ShouldRetry(int maxRetryAttempts, int attempt, DbException e)
		{
			int num;
			using (IHostTrace hostTrace = this.Tracer.CreateTrace("RunWithRetry/Exception", TraceEventType.Information))
			{
				hostTrace.Add("Attempt", attempt, false);
				hostTrace.Add("MaxAttempt", maxRetryAttempts, false);
				RetryBehavior retryBehavior = this.RetryAfterSqlError(e);
				hostTrace.Add("IsRetryable", retryBehavior.Retry, false);
				if (retryBehavior.RetryErrorCode != null)
				{
					hostTrace.Add("RetryErrorCode", retryBehavior.RetryErrorCode, false);
				}
				bool flag = attempt < 3 && retryBehavior.Retry;
				this.TraceException(hostTrace, e, flag ? TraceEventType.Warning : TraceEventType.Error);
				if (!flag)
				{
					return false;
				}
				num = (int)(retryBehavior.BaseDelay.TotalMilliseconds * (double)(attempt + 1));
				hostTrace.Add("RetryWaitTimeMs", num, false);
			}
			Thread.Sleep(num);
			return true;
		}

		// Token: 0x06006DA8 RID: 28072 RVA: 0x001798C8 File Offset: 0x00177AC8
		public ISerializedException GetPageReaderExceptionProperties(Exception e)
		{
			ISerializedException ex;
			if (!this.TryGetPageReaderExceptionProperties(e, out ex))
			{
				throw e;
			}
			return ex;
		}

		// Token: 0x06006DA9 RID: 28073 RVA: 0x001798E4 File Offset: 0x00177AE4
		public bool TryGetPageReaderExceptionProperties(Exception e, out ISerializedException properties)
		{
			properties = null;
			Exception ex;
			return this.TryConvertException(e, out ex) && PageExceptionSerializer.TryGetPropertiesFromException(ex, out properties);
		}

		// Token: 0x06006DAA RID: 28074 RVA: 0x00179908 File Offset: 0x00177B08
		protected bool TryConvertException(Exception e, out Exception newException)
		{
			if (!Microsoft.Mashup.Common.SafeExceptions.IsSafeException(e) || this.knownExceptionService.IsKnownException(e))
			{
				newException = null;
				return false;
			}
			FileLoadException ex = e as FileLoadException;
			if (ex != null)
			{
				newException = this.ProcessFileLoadException(ex);
				return true;
			}
			DbException ex2 = e as DbException;
			if (ex2 != null)
			{
				newException = this.ProcessDbException(ex2);
				return true;
			}
			newException = this.ProcessException(e);
			return true;
		}

		// Token: 0x06006DAB RID: 28075 RVA: 0x00179964 File Offset: 0x00177B64
		public static DbConnection GetUnwrappedConnection(DbConnection connection)
		{
			for (;;)
			{
				DelegatingDbConnection delegatingDbConnection = connection as DelegatingDbConnection;
				if (delegatingDbConnection == null)
				{
					break;
				}
				connection = delegatingDbConnection.InnerConnection;
			}
			return connection;
		}

		// Token: 0x06006DAC RID: 28076 RVA: 0x00179988 File Offset: 0x00177B88
		public static DbTransaction GetUnwrappedTransaction(DbTransaction transaction)
		{
			for (;;)
			{
				DelegatingDbTransaction delegatingDbTransaction = transaction as DelegatingDbTransaction;
				if (delegatingDbTransaction == null)
				{
					break;
				}
				transaction = delegatingDbTransaction.InnerTransaction;
			}
			return transaction;
		}

		// Token: 0x06006DAD RID: 28077 RVA: 0x001799AA File Offset: 0x00177BAA
		public static Message2 GetClientSoftwareNotFoundExceptionMessage(string libraryName, string downloadLink)
		{
			if (X64Helper.Is64BitProcess)
			{
				return Strings.DatabaseClientMissingExceptionMessage64bit(libraryName, downloadLink);
			}
			return Strings.DatabaseClientMissingExceptionMessage32bit(libraryName, downloadLink);
		}

		// Token: 0x06006DAE RID: 28078 RVA: 0x001799C4 File Offset: 0x00177BC4
		protected virtual Exception ProcessFileLoadException(FileLoadException e)
		{
			Exception ex;
			using (IHostTrace hostTrace = this.Tracer.CreateTrace("ProcessFileLoadException", TraceEventType.Error))
			{
				hostTrace.Add("FileName", e.FileName, false);
				hostTrace.Add("ExceptionMessage", e.Message, false);
				ex = DataSourceException.NewFileLoadException<Message2>(base.Host, Strings.FileLoadingException(TextValue.New(e.FileName).NewMeta(ValueException.NonPii), TextValue.New(this.Resource.Kind).NewMeta(ValueException.NonPii)), this.Resource, e);
			}
			return ex;
		}

		// Token: 0x06006DAF RID: 28079 RVA: 0x00179A6C File Offset: 0x00177C6C
		public virtual Exception ProcessDbException(DbException exception)
		{
			ResourceExceptionKind resourceExceptionKind = this.GetResourceExceptionKind(exception);
			if (resourceExceptionKind == ResourceExceptionKind.SecureConnectionFailed && !this.ConnectionInfo.RequireEncryption)
			{
				using (IHostTrace hostTrace = this.Tracer.CreateTrace("ProcessDbException", TraceEventType.Warning))
				{
					hostTrace.Add("Message", "Exception kind was SecureConnectionFailed when the encryption wasn't required", false);
				}
				resourceExceptionKind = ResourceExceptionKind.None;
			}
			switch (resourceExceptionKind)
			{
			case ResourceExceptionKind.None:
				return this.CreateValueException(exception);
			case ResourceExceptionKind.InvalidCredentials:
				return DataSourceException.NewAccessAuthorizationError(base.Host, this.Resource, exception.Message, null, exception);
			case ResourceExceptionKind.SecureConnectionFailed:
				return DataSourceException.NewEncryptedConnectionError(base.Host, this.Resource, exception.Message, null, exception);
			case ResourceExceptionKind.ServerNameMismatch:
				return DataSourceException.NewEncryptionPrincipalNameMismatch(base.Host, this.Resource, exception.Message, null, exception);
			default:
				throw new InvalidOperationException(Strings.UnreachableCodePath);
			}
		}

		// Token: 0x06006DB0 RID: 28080 RVA: 0x00179B54 File Offset: 0x00177D54
		protected virtual Exception ProcessException(Exception e)
		{
			if (string.IsNullOrEmpty(e.Message))
			{
				return DataSourceException.NewDataSourceError<Message1>(base.Host, Strings.ReadingFromProviderError(e.GetType().FullName), this.resource, "Source", TextValue.NewOrNull(e.Source), TypeValue.Text.Nullable, e);
			}
			return DataSourceException.NewDataSourceError<Message1>(base.Host, Strings.ReadingFromProviderError(e.Message), this.resource, null, e);
		}

		// Token: 0x06006DB1 RID: 28081
		protected abstract ResourceExceptionKind GetResourceExceptionKind(DbException exception);

		// Token: 0x06006DB2 RID: 28082 RVA: 0x00179BC9 File Offset: 0x00177DC9
		public ValueException CreateValueException(DbException dbException)
		{
			return DataSourceException.NewDataSourceError<Message2>(base.Host, DataSourceException.DataSourceMessage(this.DataSourceNameString, dbException.Message), this.resource, this.GetDbExceptionDetails(dbException), null);
		}

		// Token: 0x06006DB3 RID: 28083 RVA: 0x00179BF5 File Offset: 0x00177DF5
		public virtual IList<RecordKeyDefinition> GetDbExceptionDetails(DbException dbException)
		{
			return DbExceptionInfo.GetDetails(dbException);
		}

		// Token: 0x17001F25 RID: 7973
		// (get) Token: 0x06006DB4 RID: 28084 RVA: 0x00179BFD File Offset: 0x00177DFD
		public string DataSourceNameString
		{
			get
			{
				return this.dataSourceName;
			}
		}

		// Token: 0x17001F26 RID: 7974
		// (get) Token: 0x06006DB5 RID: 28085 RVA: 0x00179C05 File Offset: 0x00177E05
		public string ServerVersion
		{
			get
			{
				return this.ServerMetadata.Version;
			}
		}

		// Token: 0x17001F27 RID: 7975
		// (get) Token: 0x06006DB6 RID: 28086 RVA: 0x00179C14 File Offset: 0x00177E14
		// (set) Token: 0x06006DB7 RID: 28087 RVA: 0x00179C64 File Offset: 0x00177E64
		protected DbEnvironment.DbServerMetadata ServerMetadata
		{
			get
			{
				if (!this.currentlyRetrievingServerMetadata && this.serverMetadata == null)
				{
					try
					{
						this.currentlyRetrievingServerMetadata = true;
						this.serverMetadata = this.LoadCachedServerMetadata();
					}
					finally
					{
						this.currentlyRetrievingServerMetadata = false;
					}
				}
				return this.serverMetadata;
			}
			set
			{
				this.serverMetadata = value;
			}
		}

		// Token: 0x17001F28 RID: 7976
		// (get) Token: 0x06006DB8 RID: 28088 RVA: 0x00179C6D File Offset: 0x00177E6D
		protected bool IsServerMetadataSet
		{
			get
			{
				return this.serverMetadata != null;
			}
		}

		// Token: 0x06006DB9 RID: 28089 RVA: 0x00179C78 File Offset: 0x00177E78
		private DbEnvironment.DbServerMetadata LoadCachedServerMetadata()
		{
			string text = PersistentCacheKey.ServerCatalog.Qualify(this.ConnectionInfo.CacheKey, "ServerMetadata");
			object obj;
			DbEnvironment.DbServerMetadata dbServerMetadata;
			if (this.metadataCache.TryGetValue(text, DateTime.MinValue, null, (Stream s) => this.LoadServerMetadataFromStream(s), out obj))
			{
				dbServerMetadata = (DbEnvironment.DbServerMetadata)obj;
			}
			else
			{
				dbServerMetadata = this.LoadServerMetadata();
				try
				{
					this.metadataCache.CommitValue(text, delegate(Stream s, object o)
					{
						((DbEnvironment.DbServerMetadata)o).Serialize(s);
					}, dbServerMetadata);
				}
				catch (PersistentCacheException)
				{
				}
			}
			return dbServerMetadata;
		}

		// Token: 0x06006DBA RID: 28090 RVA: 0x00179D20 File Offset: 0x00177F20
		protected virtual DbEnvironment.DbServerMetadata LoadServerMetadataFromStream(Stream s)
		{
			DbEnvironment.DbServerMetadata dbServerMetadata = new DbEnvironment.DbServerMetadata();
			dbServerMetadata.Deserialize(s);
			return dbServerMetadata;
		}

		// Token: 0x06006DBB RID: 28091 RVA: 0x00179D2E File Offset: 0x00177F2E
		protected virtual DbEnvironment.DbServerMetadata LoadServerMetadata()
		{
			return this.ConvertDbExceptions<DbEnvironment.DbServerMetadata>(delegate
			{
				DbEnvironment.DbServerMetadata dbServerMetadata;
				using (DbConnection dbConnection = this.CreateConnection())
				{
					dbConnection.Open(this);
					dbServerMetadata = new DbEnvironment.DbServerMetadata
					{
						Version = dbConnection.ServerVersion
					};
				}
				return dbServerMetadata;
			});
		}

		// Token: 0x06006DBC RID: 28092 RVA: 0x00179D44 File Offset: 0x00177F44
		public virtual Number GetNumeric(IDataReader reader, int index)
		{
			Number number;
			try
			{
				number = new Number(reader.GetDecimal(index));
			}
			catch (OverflowException)
			{
				DbDataReader dbDataReader = reader as DbDataReader;
				if (dbDataReader != null)
				{
					string text = dbDataReader.GetProviderSpecificValue(index).ToString();
					decimal num;
					if (decimal.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out num))
					{
						return new Number(num);
					}
					double num2;
					if (double.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out num2))
					{
						return new Number(num2);
					}
				}
				throw;
			}
			return number;
		}

		// Token: 0x06006DBD RID: 28093 RVA: 0x00179DC8 File Offset: 0x00177FC8
		protected IEnumerable<KeyValuePair<HierarchyTableItem, TypeValue>> GetTableTypes(SchemaItem? itemFilter)
		{
			DataTable tables = this.GetTables(itemFilter);
			foreach (object obj in tables.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				string schemaColumn = DbEnvironment.GetSchemaColumn<string>(dataRow, "TABLE_TYPE");
				string schemaColumn2 = DbEnvironment.GetSchemaColumn<string>(dataRow, "TABLE_SCHEMA");
				string schemaColumn3 = DbEnvironment.GetSchemaColumn<string>(dataRow, "TABLE_NAME");
				string stringSchemaColumnOrNull = DbEnvironment.GetStringSchemaColumnOrNull(dataRow.Table.Columns, dataRow, "DESCRIPTION");
				TableType tableType;
				if (this.TableTypes.TryGetValue(schemaColumn, out tableType))
				{
					HierarchyTableItem hierarchyTableItem = new HierarchyTableItem(this.Database, schemaColumn2, schemaColumn3, tableType, TextValue.NewOrNull(stringSchemaColumnOrNull));
					yield return new KeyValuePair<HierarchyTableItem, TypeValue>(hierarchyTableItem, this.ReadTableType(dataRow));
				}
			}
			IEnumerator enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06006DBE RID: 28094 RVA: 0x00179DE0 File Offset: 0x00177FE0
		private IDictionary<SchemaItem, TypeValue> LoadTypeCatalog(SchemaItem? itemFilter)
		{
			IDictionary<SchemaItem, TypeValue> dictionary = new SortedDictionary<SchemaItem, TypeValue>(SchemaItem.Comparer);
			foreach (KeyValuePair<HierarchyTableItem, TypeValue> keyValuePair in this.GetTableTypes(itemFilter))
			{
				SchemaItem key = new SchemaItem(keyValuePair.Key.SchemaName, keyValuePair.Key.Name, keyValuePair.Key.Kind.AsString);
				try
				{
					dictionary.Add(key, keyValuePair.Value);
				}
				catch (ArgumentException)
				{
					SchemaItem key2 = dictionary.First((KeyValuePair<SchemaItem, TypeValue> pair) => pair.Key.Equals(key)).Key;
					using (IHostTrace hostTrace = this.Tracer.CreateTrace("LoadTypeCatalog", TraceEventType.Warning))
					{
						hostTrace.Add("Key1", key.ToString(), true);
						hostTrace.Add("Key2", key2.ToString(), true);
					}
					if (key.Kind != key2.Kind)
					{
						throw;
					}
				}
			}
			bool flag = this.DataSourceCapabilities.SupportsStoredProcedures && base.Host.AreActionsAvailable();
			if (this.DataSourceCapabilities.SupportsStoredFunctions || flag)
			{
				DataTable procedures = this.GetProcedures(itemFilter);
				if (this.DataSourceCapabilities.SupportsStoredFunctions)
				{
					this.AddFunctionsToCatalog(procedures, "FUNCTION", "Function", dictionary);
				}
				if (flag)
				{
					this.AddFunctionsToCatalog(procedures, "PROCEDURE", "ParameterizedAction", dictionary);
				}
			}
			return dictionary;
		}

		// Token: 0x06006DBF RID: 28095 RVA: 0x00179FCC File Offset: 0x001781CC
		protected virtual IDictionary<SchemaItem, Value> LoadCatalog(SchemaItem? itemFilter)
		{
			return (from kvp in this.LoadTypeCatalog(itemFilter)
				select new KeyValuePair<SchemaItem, Value>(kvp.Key, this.CreateItem(kvp.Key, kvp.Value))).ToDictionary((KeyValuePair<SchemaItem, Value> kvp) => kvp.Key, (KeyValuePair<SchemaItem, Value> kvp) => kvp.Value);
		}

		// Token: 0x06006DC0 RID: 28096 RVA: 0x0017A034 File Offset: 0x00178234
		protected Value CreateItem(SchemaItem item, TypeValue type)
		{
			string kind = item.Kind;
			if (kind == "Function")
			{
				return QueryResultFunctionValue.CreateFunction(this, item.Identifier, base.Host, type.AsFunctionType);
			}
			if (!(kind == "ParameterizedAction"))
			{
				return this.WrapDatabaseTable(this.AddColumnIdentity(item, new QueryResultTableValue(this, item.Identifier, base.Host, type.AsTableType)));
			}
			return QueryResultFunctionValue.CreateProcedure(this, item.Identifier, base.Host, type.AsFunctionType);
		}

		// Token: 0x06006DC1 RID: 28097 RVA: 0x0000A6A5 File Offset: 0x000088A5
		public virtual TableValue WrapDatabaseTable(TableValue tableValue)
		{
			return tableValue;
		}

		// Token: 0x06006DC2 RID: 28098 RVA: 0x0017A0D0 File Offset: 0x001782D0
		private void AddFunctionsToCatalog(DataTable functionsTable, string functionType, string linkKind, IDictionary<SchemaItem, TypeValue> typeCatalog)
		{
			foreach (DataRow dataRow in functionsTable.Select(string.Format(CultureInfo.InvariantCulture, "{0} = '{1}'", "ROUTINE_TYPE", functionType)))
			{
				string schemaColumn = DbEnvironment.GetSchemaColumn<string>(dataRow, "ROUTINE_SCHEMA");
				string schemaColumn2 = DbEnvironment.GetSchemaColumn<string>(dataRow, "ROUTINE_NAME");
				DateTime? dateTimeSchemaColumnOrNull = DbEnvironment.GetDateTimeSchemaColumnOrNull(functionsTable.Columns, dataRow, "CREATED_DATE");
				DateTime? dateTimeSchemaColumnOrNull2 = DbEnvironment.GetDateTimeSchemaColumnOrNull(functionsTable.Columns, dataRow, "MODIFIED_DATE");
				string stringSchemaColumnOrNull = DbEnvironment.GetStringSchemaColumnOrNull(functionsTable.Columns, dataRow, "DESCRIPTION");
				typeCatalog.Add(new SchemaItem(schemaColumn, schemaColumn2, linkKind), this.CreateFunctionType(schemaColumn, schemaColumn2, dateTimeSchemaColumnOrNull, dateTimeSchemaColumnOrNull2, stringSchemaColumnOrNull));
			}
		}

		// Token: 0x17001F29 RID: 7977
		// (get) Token: 0x06006DC3 RID: 28099
		public abstract HashSet<string> SearchableTypes { get; }

		// Token: 0x17001F2A RID: 7978
		// (get) Token: 0x06006DC4 RID: 28100
		public abstract Dictionary<string, TypeValue> NativeToClrTypeMapping { get; }

		// Token: 0x06006DC5 RID: 28101 RVA: 0x0017A17C File Offset: 0x0017837C
		public virtual bool? IsVariableLengthType(string dataType)
		{
			return null;
		}

		// Token: 0x06006DC6 RID: 28102 RVA: 0x0017A192 File Offset: 0x00178392
		public bool TryGetClrTypeValue(string clrType, out TypeValue typeValue)
		{
			return this.NativeToClrTypeMapping.TryGetValue(clrType, out typeValue);
		}

		// Token: 0x06006DC7 RID: 28103 RVA: 0x0017A1A4 File Offset: 0x001783A4
		public override bool OtherCanFoldToThis(EnvironmentBase other)
		{
			DbEnvironment dbEnvironment = other as DbEnvironment;
			return dbEnvironment != null && this.resource.Equals(dbEnvironment.resource) && (this.TransactionInfo.NullableEquals(dbEnvironment.TransactionInfo) || dbEnvironment.TransactionInfo == null);
		}

		// Token: 0x06006DC8 RID: 28104 RVA: 0x0017A1F0 File Offset: 0x001783F0
		public override bool TryGetRelationshipIdentity(SchemaItem schemaItem, out string relationshipIdentity)
		{
			relationshipIdentity = PersistentCacheKey.ServerDatabase.Qualify(this.resource.Kind, this.resource.Path, schemaItem.Schema, schemaItem.Identifier);
			return true;
		}

		// Token: 0x06006DC9 RID: 28105 RVA: 0x0017A231 File Offset: 0x00178431
		public virtual IEnumerable<DbTransactionInfo> GetRegisteredTransactions()
		{
			return DbEnvironment.transactions.Values;
		}

		// Token: 0x06006DCA RID: 28106 RVA: 0x0017A23D File Offset: 0x0017843D
		public virtual bool IsRegisteredTransaction(DbTransactionInfo transactionInfo)
		{
			return DbEnvironment.transactions.ContainsKey(transactionInfo.Identity);
		}

		// Token: 0x06006DCB RID: 28107 RVA: 0x0017A24F File Offset: 0x0017844F
		public virtual void RegisterTransaction(DbTransactionInfo transactionInfo)
		{
			base.Host.QueryService<ILifetimeService>().Register(transactionInfo);
			DbEnvironment.transactions.Add(transactionInfo.Identity, transactionInfo);
		}

		// Token: 0x06006DCC RID: 28108 RVA: 0x0017A273 File Offset: 0x00178473
		public virtual void UnregisterTransaction(DbTransactionInfo transactionInfo)
		{
			if (DbEnvironment.transactions.Remove(transactionInfo.Identity))
			{
				base.Host.QueryService<ILifetimeService>().Unregister(transactionInfo);
			}
		}

		// Token: 0x06006DCD RID: 28109 RVA: 0x0017A298 File Offset: 0x00178498
		protected TypeValue CreateTableType(string schemaName, string tableName, bool fromFunction, DateTime? createdDate, DateTime? modifiedDate, string description)
		{
			TypeValue typeValue = new QueryResultTableTypeValue(this, schemaName, tableName, fromFunction);
			KeysBuilder keysBuilder = default(KeysBuilder);
			ArrayBuilder<Value> arrayBuilder = default(ArrayBuilder<Value>);
			ArrayBuilder<IValueReference> arrayBuilder2 = default(ArrayBuilder<IValueReference>);
			keysBuilder.Add("Sql.Schema");
			arrayBuilder.Add(DbEnvironment.NullableTextTypeField);
			arrayBuilder2.Add(TextValue.NewOrNull(schemaName));
			keysBuilder.Add("Sql.Table");
			arrayBuilder.Add(DbEnvironment.NullableTextTypeField);
			arrayBuilder2.Add(TextValue.New(tableName));
			if (createdDate != null)
			{
				keysBuilder.Add("Documentation.CreatedDate");
				arrayBuilder.Add(DbEnvironment.NullableDateTimeTypeField);
				arrayBuilder2.Add(DateTimeValue.New(createdDate.Value));
			}
			if (modifiedDate != null)
			{
				keysBuilder.Add("Documentation.ModifiedDate");
				arrayBuilder.Add(DbEnvironment.NullableDateTimeTypeField);
				arrayBuilder2.Add(DateTimeValue.New(modifiedDate.Value));
			}
			if (!string.IsNullOrEmpty(description))
			{
				keysBuilder.Add("Documentation.Description");
				arrayBuilder.Add(DbEnvironment.NullableTextTypeField);
				arrayBuilder2.Add(TextValue.New(description));
			}
			if (!fromFunction)
			{
				keysBuilder.Add("Documentation.NativeSize");
				arrayBuilder.Add(DbEnvironment.DelayedNullableNumberTypeField);
				arrayBuilder2.Add(new DelayedValue(() => this.GetSize(schemaName, tableName)));
			}
			RecordValue recordValue = RecordValue.New(RecordTypeValue.New(RecordValue.New(keysBuilder.ToKeys(), arrayBuilder.ToArray()), false), arrayBuilder2.ToArray());
			return BinaryOperator.AddMeta.Invoke(typeValue, recordValue).AsType;
		}

		// Token: 0x06006DCE RID: 28110 RVA: 0x0017A448 File Offset: 0x00178648
		private TypeValue ReadTableType(DataRow tableRow)
		{
			string schemaColumn = DbEnvironment.GetSchemaColumn<string>(tableRow, "TABLE_SCHEMA");
			string schemaColumn2 = DbEnvironment.GetSchemaColumn<string>(tableRow, "TABLE_NAME");
			DateTime? dateTimeSchemaColumnOrNull = DbEnvironment.GetDateTimeSchemaColumnOrNull(tableRow.Table.Columns, tableRow, "CREATED_DATE");
			DateTime? dateTimeSchemaColumnOrNull2 = DbEnvironment.GetDateTimeSchemaColumnOrNull(tableRow.Table.Columns, tableRow, "MODIFIED_DATE");
			string stringSchemaColumnOrNull = DbEnvironment.GetStringSchemaColumnOrNull(tableRow.Table.Columns, tableRow, "DESCRIPTION");
			return this.CreateTableType(schemaColumn, schemaColumn2, false, dateTimeSchemaColumnOrNull, dateTimeSchemaColumnOrNull2, stringSchemaColumnOrNull);
		}

		// Token: 0x06006DCF RID: 28111 RVA: 0x0017A4C0 File Offset: 0x001786C0
		protected TypeValue CreateFunctionType(string schemaName, string tableName, DateTime? createdDate, DateTime? modifiedDate, string description)
		{
			TypeValue typeValue = new QueryResultFunctionTypeValue(this, schemaName, tableName);
			List<NamedValue> list = new List<NamedValue>();
			list.Add(new NamedValue("Sql.Schema", TextValue.NewOrNull(schemaName)));
			list.Add(new NamedValue("Sql.Table", TextValue.New(tableName)));
			if (createdDate != null)
			{
				list.Add(new NamedValue("Documentation.CreatedDate", DateTimeValue.New(createdDate.Value)));
			}
			if (modifiedDate != null)
			{
				list.Add(new NamedValue("Documentation.ModifiedDate", DateTimeValue.New(modifiedDate.Value)));
			}
			if (!string.IsNullOrEmpty(description))
			{
				list.Add(new NamedValue("Documentation.Description", TextValue.New(description)));
			}
			return BinaryOperator.AddMeta.Invoke(typeValue, RecordValue.New(list.ToArray())).AsType;
		}

		// Token: 0x06006DD0 RID: 28112 RVA: 0x0017A590 File Offset: 0x00178790
		private Value GetSize(string schemaName, string tableName)
		{
			Value value;
			try
			{
				long? num = null;
				DataTable resourceInformation = this.GetResourceInformation(schemaName, tableName);
				using (IEnumerator enumerator = resourceInformation.Rows.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						DataRow dataRow = (DataRow)enumerator.Current;
						num = DbEnvironment.GetLongSchemaColumnOrNull(resourceInformation.Columns, dataRow, "TOTAL_BYTES");
					}
				}
				value = ((num != null) ? NumberValue.New(num.Value) : Value.Null);
			}
			catch (Exception ex)
			{
				if (!Microsoft.Mashup.Common.SafeExceptions.IsSafeException(ex))
				{
					throw;
				}
				using (IHostTrace hostTrace = this.Tracer.CreateTrace("GetSize", TraceEventType.Information))
				{
					this.TraceException(hostTrace, ex);
				}
				value = Value.Null;
			}
			return value;
		}

		// Token: 0x06006DD1 RID: 28113 RVA: 0x0017A688 File Offset: 0x00178888
		public static string CreateIdentifierName(string schemaName, string tableName)
		{
			if (schemaName == "dbo" || string.IsNullOrEmpty(schemaName))
			{
				return tableName;
			}
			return schemaName + "." + tableName;
		}

		// Token: 0x06006DD2 RID: 28114 RVA: 0x0017A6AD File Offset: 0x001788AD
		public virtual bool TryGetSchemaItemFromName(string name, out SchemaItem item)
		{
			item = default(SchemaItem);
			return false;
		}

		// Token: 0x06006DD3 RID: 28115 RVA: 0x0017A6B8 File Offset: 0x001788B8
		public static T GetSchemaColumn<T>(DataRow row, string columnName)
		{
			object obj = row[columnName];
			if (obj != null && !obj.Equals(DBNull.Value))
			{
				return (T)((object)obj);
			}
			return default(T);
		}

		// Token: 0x06006DD4 RID: 28116 RVA: 0x0017A6F0 File Offset: 0x001788F0
		public static bool? GetNullableBooleanSchemaColumn(DataRow row, string columnName)
		{
			object obj = row[columnName];
			if (obj != null)
			{
				TypeCode typeCode = Type.GetTypeCode(obj.GetType());
				if (typeCode == TypeCode.Boolean)
				{
					return new bool?((bool)obj);
				}
				if (typeCode == TypeCode.Int32)
				{
					return new bool?((int)obj != 0);
				}
				if (typeCode == TypeCode.String)
				{
					char c = ((string)obj)[0];
					return new bool?(c == 'Y' || c == 'T');
				}
			}
			return null;
		}

		// Token: 0x06006DD5 RID: 28117 RVA: 0x0017A768 File Offset: 0x00178968
		public static bool? GetBooleanSchemaColumnOrNull(DataColumnCollection columns, DataRow schemaRow, string columnName)
		{
			if (columns.Contains(columnName) && !schemaRow.IsNull(columnName))
			{
				return DbEnvironment.GetNullableBooleanSchemaColumn(schemaRow, columnName);
			}
			return null;
		}

		// Token: 0x06006DD6 RID: 28118 RVA: 0x0017A798 File Offset: 0x00178998
		public static long GetLongSchemaColumn(DataRow row, string columnName)
		{
			return DbEnvironment.GetLongSchemaColumnOrNull(row, columnName).Value;
		}

		// Token: 0x06006DD7 RID: 28119 RVA: 0x0017A7B4 File Offset: 0x001789B4
		private static long? GetLongSchemaColumnOrNull(DataRow row, string columnName)
		{
			long num;
			if (!long.TryParse(row[columnName].ToString(), NumberStyles.Integer, CultureInfo.InvariantCulture, out num))
			{
				return null;
			}
			return new long?(num);
		}

		// Token: 0x06006DD8 RID: 28120 RVA: 0x0017A7EC File Offset: 0x001789EC
		public static long? GetLongSchemaColumnOrNull(DataColumnCollection columns, DataRow schemaRow, string columnName)
		{
			if (columns.Contains(columnName) && !schemaRow.IsNull(columnName))
			{
				return DbEnvironment.GetLongSchemaColumnOrNull(schemaRow, columnName);
			}
			return null;
		}

		// Token: 0x06006DD9 RID: 28121 RVA: 0x0017A81C File Offset: 0x00178A1C
		public static int? GetIntSchemaColumnOrNull(DataColumnCollection columns, DataRow schemaRow, string columnName)
		{
			if (!columns.Contains(columnName) || schemaRow.IsNull(columnName))
			{
				return null;
			}
			long? longSchemaColumnOrNull = DbEnvironment.GetLongSchemaColumnOrNull(schemaRow, columnName);
			if (longSchemaColumnOrNull == null)
			{
				return null;
			}
			return new int?((int)longSchemaColumnOrNull.GetValueOrDefault());
		}

		// Token: 0x06006DDA RID: 28122 RVA: 0x0017A870 File Offset: 0x00178A70
		public static string GetStringSchemaColumn(DataRow row, string columnName)
		{
			object obj = row[columnName];
			if (obj != null && !obj.Equals(DBNull.Value))
			{
				return obj.ToString();
			}
			return null;
		}

		// Token: 0x06006DDB RID: 28123 RVA: 0x0017A89D File Offset: 0x00178A9D
		public static string GetStringSchemaColumnOrNull(DataColumnCollection columns, DataRow schemaRow, string columnName)
		{
			if (columns.Contains(columnName))
			{
				return DbEnvironment.GetStringSchemaColumn(schemaRow, columnName);
			}
			return null;
		}

		// Token: 0x06006DDC RID: 28124 RVA: 0x0017A8B4 File Offset: 0x00178AB4
		public static DateTime? GetDateTimeSchemaColumn(DataRow row, string columnName)
		{
			object obj = row[columnName];
			if (obj != null && !obj.Equals(DBNull.Value))
			{
				return new DateTime?((DateTime)obj);
			}
			return null;
		}

		// Token: 0x06006DDD RID: 28125 RVA: 0x0017A8F0 File Offset: 0x00178AF0
		public static DateTime? GetDateTimeSchemaColumnOrNull(DataColumnCollection columns, DataRow schemaRow, string columnName)
		{
			if (columns.Contains(columnName) && !schemaRow.IsNull(columnName))
			{
				return DbEnvironment.GetDateTimeSchemaColumn(schemaRow, columnName);
			}
			return null;
		}

		// Token: 0x06006DDE RID: 28126 RVA: 0x0017A920 File Offset: 0x00178B20
		protected static string RemoveParentheticals(string input)
		{
			int num = input.IndexOf('(');
			int num2 = input.IndexOf(')');
			while (num >= 0 && num2 >= 0)
			{
				input = input.Substring(0, num) + input.Substring(num2 + 1);
				num = input.IndexOf('(');
				num2 = input.IndexOf(')');
			}
			return input;
		}

		// Token: 0x06006DDF RID: 28127 RVA: 0x0017A974 File Offset: 0x00178B74
		private long GetOrdinalColumn(DataRow row, bool usesLong)
		{
			if (!usesLong)
			{
				return DbEnvironment.GetLongSchemaColumn(row, "ORDINAL_POSITION");
			}
			return DbEnvironment.GetSchemaColumn<long>(row, "ORDINAL_POSITION");
		}

		// Token: 0x06006DE0 RID: 28128 RVA: 0x0017A990 File Offset: 0x00178B90
		public RecordTypeValue RetrieveRowTypeForTable(string schemaName, string tableName, bool fromFunction)
		{
			IDictionary<long, NamedValue> dictionary = new SortedDictionary<long, NamedValue>();
			DataTable dataTable;
			try
			{
				dataTable = (fromFunction ? this.GetProcedureColumns(schemaName, tableName) : this.GetColumns(schemaName, tableName));
			}
			catch (DbException ex)
			{
				throw this.ProcessDbException(ex);
			}
			bool flag = dataTable.Columns["ORDINAL_POSITION"].DataType.Equals(typeof(long));
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				string schemaColumn = DbEnvironment.GetSchemaColumn<string>(dataRow, "COLUMN_NAME");
				bool valueOrDefault = DbEnvironment.GetNullableBooleanSchemaColumn(dataRow, "IS_NULLABLE").GetValueOrDefault(true);
				long ordinalColumn = this.GetOrdinalColumn(dataRow, flag);
				TypeValue typeValue;
				bool flag2;
				if (this.TryGetDataTypeValue(dataTable.Columns, dataRow, out typeValue, out flag2))
				{
					NamedValue namedValue = new NamedValue(schemaColumn, RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
					{
						DbTypeServices.CreateTypeValue(typeValue, valueOrDefault, flag2),
						LogicalValue.False
					}));
					try
					{
						dictionary.Add(ordinalColumn, namedValue);
					}
					catch (ArgumentException)
					{
						string text = "Table {0}.{1} has columns with duplicate ordinal values. upernumerary columns will be ignored.";
						using (IHostTrace hostTrace = this.Tracer.CreateTrace("RetrieveRowTypeForTable", TraceEventType.Error))
						{
							DataTable dataTable2 = dataRow.Table.Clone();
							foreach (object obj2 in dataTable.Rows)
							{
								DataRow dataRow2 = (DataRow)obj2;
								if (this.GetOrdinalColumn(dataRow2, flag) == ordinalColumn)
								{
									dataTable2.Rows.Add(dataRow2.ItemArray);
								}
							}
							hostTrace.AddDataTable(string.Format(CultureInfo.InvariantCulture, text, schemaName, tableName), dataTable2, true);
						}
					}
				}
			}
			RecordValue recordValue;
			if (dictionary.Count <= 0)
			{
				recordValue = RecordValue.Empty;
			}
			else
			{
				recordValue = RecordValue.New(dictionary.Select((KeyValuePair<long, NamedValue> c) => c.Value).ToArray<NamedValue>());
			}
			return RecordTypeValue.New(recordValue);
		}

		// Token: 0x06006DE1 RID: 28129 RVA: 0x0017AC24 File Offset: 0x00178E24
		protected virtual string NormalizeDataType(string dataType)
		{
			return DbEnvironment.RemoveParentheticals(dataType.ToLowerInvariant());
		}

		// Token: 0x06006DE2 RID: 28130 RVA: 0x0017AC34 File Offset: 0x00178E34
		protected virtual bool TryGetDataTypeValue(DataColumnCollection columns, DataRow schemaRow, out TypeValue clrDataType, out bool isSearchable)
		{
			string text = DbEnvironment.GetStringSchemaColumn(schemaRow, "DATA_TYPE");
			int? intSchemaColumnOrNull = DbEnvironment.GetIntSchemaColumnOrNull(columns, schemaRow, "NUMERIC_PRECISION_RADIX");
			int? num = DbEnvironment.GetIntSchemaColumnOrNull(columns, schemaRow, "NUMERIC_PRECISION");
			int? num2 = DbEnvironment.GetIntSchemaColumnOrNull(columns, schemaRow, "NUMERIC_SCALE");
			int? intSchemaColumnOrNull2 = DbEnvironment.GetIntSchemaColumnOrNull(columns, schemaRow, "DATETIME_PRECISION");
			long? num3 = DbEnvironment.GetLongSchemaColumnOrNull(columns, schemaRow, "CHARACTER_MAXIMUM_LENGTH");
			bool? booleanSchemaColumnOrNull = DbEnvironment.GetBooleanSchemaColumnOrNull(columns, schemaRow, "IS_WRITABLE");
			string stringSchemaColumnOrNull = DbEnvironment.GetStringSchemaColumnOrNull(columns, schemaRow, "COLUMN_DEFAULT");
			string stringSchemaColumnOrNull2 = DbEnvironment.GetStringSchemaColumnOrNull(columns, schemaRow, "COLUMN_EXPRESSION");
			string stringSchemaColumnOrNull3 = DbEnvironment.GetStringSchemaColumnOrNull(columns, schemaRow, "DESCRIPTION");
			if (text == null)
			{
				clrDataType = TypeValue.Any;
				isSearchable = false;
			}
			else
			{
				text = this.NormalizeDataType(text);
				if (!this.NativeToClrTypeMapping.TryGetValue(text, out clrDataType))
				{
					clrDataType = TypeValue.Any;
				}
				isSearchable = this.SearchableTypes.Contains(text);
				if (intSchemaColumnOrNull == null)
				{
					num = null;
					num2 = null;
				}
				long? num4 = num3;
				long num5 = 0L;
				if ((num4.GetValueOrDefault() <= num5) & (num4 != null))
				{
					num3 = null;
				}
				TypeFacets typeFacets = TypeFacets.None;
				switch (clrDataType.TypeKind)
				{
				case ValueKind.Time:
				case ValueKind.DateTime:
				case ValueKind.DateTimeZone:
					typeFacets = TypeFacets.NewDateTime(intSchemaColumnOrNull2, null);
					break;
				case ValueKind.Number:
					typeFacets = TypeFacets.NewNumeric(intSchemaColumnOrNull, num, num2, null);
					break;
				case ValueKind.Text:
					typeFacets = TypeFacets.NewText(num3, this.IsVariableLengthType(text), null);
					break;
				case ValueKind.Binary:
					typeFacets = TypeFacets.NewBinary(num3, this.IsVariableLengthType(text), null);
					break;
				}
				string nativeTypeName = this.GetNativeTypeName(text);
				typeFacets = typeFacets.AddNative(nativeTypeName, stringSchemaColumnOrNull, stringSchemaColumnOrNull2);
				clrDataType = clrDataType.NewFacets(typeFacets);
			}
			KeysBuilder keysBuilder = default(KeysBuilder);
			ArrayBuilder<IValueReference> arrayBuilder = default(ArrayBuilder<IValueReference>);
			if (booleanSchemaColumnOrNull != null)
			{
				keysBuilder.Add("Documentation.IsWritable");
				arrayBuilder.Add(LogicalValue.New(booleanSchemaColumnOrNull.Value));
			}
			if (!string.IsNullOrEmpty(stringSchemaColumnOrNull3))
			{
				keysBuilder.Add("Documentation.FieldDescription");
				arrayBuilder.Add(TextValue.New(stringSchemaColumnOrNull3));
			}
			RecordValue recordValue = RecordValue.New(keysBuilder.ToKeys(), arrayBuilder.ToArray());
			clrDataType = clrDataType.NewMeta(recordValue.Concatenate(clrDataType.MetaValue).AsRecord).AsType;
			return true;
		}

		// Token: 0x06006DE3 RID: 28131 RVA: 0x0000A6A5 File Offset: 0x000088A5
		public virtual string GetNativeTypeName(string dataType)
		{
			return dataType;
		}

		// Token: 0x06006DE4 RID: 28132 RVA: 0x0017AE7A File Offset: 0x0017907A
		public DataTable GetDatabaseInformation()
		{
			return this.GetSchemaTable((DbConnection connection) => this.dbService.LoadDatabaseInformation(connection), true, "DatabaseInformation", Array.Empty<string>());
		}

		// Token: 0x06006DE5 RID: 28133 RVA: 0x0017AE99 File Offset: 0x00179099
		public virtual DataTable LoadDatabaseInformation(DbConnection connection)
		{
			return new DataTable
			{
				Locale = CultureInfo.InvariantCulture,
				Columns = { 
				{
					"DESCRIPTION",
					typeof(string)
				} }
			};
		}

		// Token: 0x06006DE6 RID: 28134 RVA: 0x0017AEC8 File Offset: 0x001790C8
		protected virtual DataTable GetTables(SchemaItem? itemFilter)
		{
			DbEnvironment.<>c__DisplayClass287_0 CS$<>8__locals1 = new DbEnvironment.<>c__DisplayClass287_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.itemFilter = itemFilter;
			Func<DbConnection, DataTable> func = (DbConnection connection) => CS$<>8__locals1.<>4__this.dbService.LoadTables(connection, (CS$<>8__locals1.itemFilter != null) ? CS$<>8__locals1.itemFilter.GetValueOrDefault().Schema : null, (CS$<>8__locals1.itemFilter != null) ? CS$<>8__locals1.itemFilter.GetValueOrDefault().Item : null);
			bool flag = true;
			string text = "Tables";
			string[] array = new string[2];
			int num = 0;
			DbEnvironment.<>c__DisplayClass287_0 CS$<>8__locals2 = CS$<>8__locals1;
			array[num] = ((CS$<>8__locals2.itemFilter != null) ? CS$<>8__locals2.itemFilter.GetValueOrDefault().Schema : null);
			int num2 = 1;
			DbEnvironment.<>c__DisplayClass287_0 CS$<>8__locals3 = CS$<>8__locals1;
			array[num2] = ((CS$<>8__locals3.itemFilter != null) ? CS$<>8__locals3.itemFilter.GetValueOrDefault().Item : null);
			return this.GetSchemaTable(func, flag, text, array);
		}

		// Token: 0x06006DE7 RID: 28135 RVA: 0x0017AF4B File Offset: 0x0017914B
		public virtual DataTable LoadTables(DbConnection connection, string schemaFilter, string tableFilter)
		{
			return connection.GetSchema("Tables", new string[0]);
		}

		// Token: 0x06006DE8 RID: 28136 RVA: 0x0017AF5E File Offset: 0x0017915E
		protected virtual DataTable GetSchemas()
		{
			return this.GetSchemaTable((DbConnection connection) => this.dbService.LoadSchemas(connection), true, "Schemas", Array.Empty<string>());
		}

		// Token: 0x06006DE9 RID: 28137
		public abstract DataTable LoadSchemas(DbConnection connection);

		// Token: 0x06006DEA RID: 28138 RVA: 0x0017AF80 File Offset: 0x00179180
		protected virtual DataTable GetProcedures(SchemaItem? itemFilter)
		{
			DbEnvironment.<>c__DisplayClass291_0 CS$<>8__locals1 = new DbEnvironment.<>c__DisplayClass291_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.itemFilter = itemFilter;
			Func<DbConnection, DataTable> func = (DbConnection connection) => CS$<>8__locals1.<>4__this.dbService.LoadProcedures(connection, (CS$<>8__locals1.itemFilter != null) ? CS$<>8__locals1.itemFilter.GetValueOrDefault().Schema : null, (CS$<>8__locals1.itemFilter != null) ? CS$<>8__locals1.itemFilter.GetValueOrDefault().Item : null);
			bool flag = true;
			string text = "Procedures";
			string[] array = new string[2];
			int num = 0;
			DbEnvironment.<>c__DisplayClass291_0 CS$<>8__locals2 = CS$<>8__locals1;
			array[num] = ((CS$<>8__locals2.itemFilter != null) ? CS$<>8__locals2.itemFilter.GetValueOrDefault().Schema : null);
			int num2 = 1;
			DbEnvironment.<>c__DisplayClass291_0 CS$<>8__locals3 = CS$<>8__locals1;
			array[num2] = ((CS$<>8__locals3.itemFilter != null) ? CS$<>8__locals3.itemFilter.GetValueOrDefault().Item : null);
			return this.GetSchemaTable(func, flag, text, array);
		}

		// Token: 0x06006DEB RID: 28139 RVA: 0x0017B003 File Offset: 0x00179203
		public virtual DataTable LoadProcedures(DbConnection connection, string schemaFilter, string tableFilter)
		{
			return connection.GetSchema("Procedures", new string[0]);
		}

		// Token: 0x06006DEC RID: 28140 RVA: 0x0017B018 File Offset: 0x00179218
		protected virtual DataTable GetColumns(string schema, string table)
		{
			return this.GetSchemaTable((DbConnection connection) => this.dbService.LoadColumns(connection, schema, table), true, "Columns", new string[] { schema, table });
		}

		// Token: 0x06006DED RID: 28141 RVA: 0x0017B070 File Offset: 0x00179270
		public virtual DataTable LoadColumns(DbConnection connection, string schema, string table)
		{
			string[] array = new string[4];
			array[1] = schema;
			array[2] = table;
			string[] array2 = array;
			return connection.GetSchema("Columns", array2);
		}

		// Token: 0x06006DEE RID: 28142 RVA: 0x0017B098 File Offset: 0x00179298
		protected DataTable GetProcedureColumns(string schema, string procedure)
		{
			return this.GetSchemaTable((DbConnection connection) => this.dbService.LoadProcedureColumns(connection, schema, procedure), true, "ProcedureColumns", new string[] { schema, procedure });
		}

		// Token: 0x06006DEF RID: 28143 RVA: 0x0017B0F0 File Offset: 0x001792F0
		public virtual DataTable LoadProcedureColumns(DbConnection connection, string schema, string procedure)
		{
			string[] array = new string[4];
			array[1] = schema;
			array[2] = procedure;
			string[] array2 = array;
			return connection.GetSchema("ProcedureColumns", array2);
		}

		// Token: 0x06006DF0 RID: 28144 RVA: 0x0017B118 File Offset: 0x00179318
		protected virtual DataTable GetIndexes(string schema, string table)
		{
			return this.GetSchemaTable((DbConnection connection) => this.dbService.LoadIndexes(connection, schema, table), true, "Indexes", new string[] { schema, table });
		}

		// Token: 0x06006DF1 RID: 28145 RVA: 0x0017B170 File Offset: 0x00179370
		public virtual DataTable LoadIndexes(DbConnection connection, string schema, string table)
		{
			string[] array = new string[] { null, schema, null, null, table };
			return connection.GetSchema("Indexes", array);
		}

		// Token: 0x06006DF2 RID: 28146 RVA: 0x0017B198 File Offset: 0x00179398
		protected virtual DataTable GetForeignKeysParent(string schema, string table)
		{
			return this.GetSchemaTable((DbConnection connection) => this.dbService.LoadForeignKeysParent(connection, schema, table), true, "ForeignKeysParent", new string[] { schema, table });
		}

		// Token: 0x06006DF3 RID: 28147 RVA: 0x0017B1F0 File Offset: 0x001793F0
		public virtual DataTable LoadForeignKeysParent(DbConnection connection, string schema, string table)
		{
			string[] array = new string[] { schema, table };
			return connection.GetSchema("ForeignKeysParent", array);
		}

		// Token: 0x06006DF4 RID: 28148 RVA: 0x0017B218 File Offset: 0x00179418
		protected virtual DataTable GetForeignKeysChild(string schema, string table)
		{
			return this.GetSchemaTable((DbConnection connection) => this.dbService.LoadForeignKeysChild(connection, schema, table), true, "ForeignKeysChild", new string[] { schema, table });
		}

		// Token: 0x06006DF5 RID: 28149 RVA: 0x0017B270 File Offset: 0x00179470
		public virtual DataTable LoadForeignKeysChild(DbConnection connection, string schema, string table)
		{
			string[] array = new string[] { schema, table };
			return connection.GetSchema("ForeignKeysChild", array);
		}

		// Token: 0x06006DF6 RID: 28150 RVA: 0x0017B298 File Offset: 0x00179498
		protected DataTable GetProcedureParameters(string schema, string function)
		{
			return this.GetSchemaTable((DbConnection connection) => this.dbService.LoadProcedureParameters(connection, schema, function), true, "ProcedureParameters", new string[] { schema, function });
		}

		// Token: 0x06006DF7 RID: 28151 RVA: 0x0017B2F0 File Offset: 0x001794F0
		public virtual DataTable LoadProcedureParameters(DbConnection connection, string schema, string function)
		{
			string[] array = new string[4];
			array[1] = schema;
			array[2] = function;
			string[] array2 = array;
			return connection.GetSchema("ProcedureParameters", array2);
		}

		// Token: 0x06006DF8 RID: 28152 RVA: 0x0017B318 File Offset: 0x00179518
		protected DataTable GetIdentityColumns(string schema, string table)
		{
			return this.GetSchemaTable((DbConnection connection) => this.dbService.LoadIdentityColumns(connection, schema, table), true, "IdentityColumns", new string[] { schema, table });
		}

		// Token: 0x06006DF9 RID: 28153 RVA: 0x0017B370 File Offset: 0x00179570
		public virtual DataTable LoadIdentityColumns(DbConnection connection, string schema, string table)
		{
			return new DataTable
			{
				Locale = CultureInfo.InvariantCulture,
				Columns = 
				{
					{
						"COLUMN_NAME",
						typeof(string)
					},
					{
						"ORDINAL_POSITION",
						typeof(int)
					}
				}
			};
		}

		// Token: 0x06006DFA RID: 28154 RVA: 0x0017B3C4 File Offset: 0x001795C4
		protected DataTable GetResourceInformation(string schema, string table)
		{
			return this.GetSchemaTable((DbConnection connection) => this.dbService.LoadResourceInformation(connection, schema, table), true, "ResourceInformation", new string[] { schema, table });
		}

		// Token: 0x06006DFB RID: 28155 RVA: 0x0017B41C File Offset: 0x0017961C
		public virtual DataTable LoadResourceInformation(DbConnection connection, string schema, string table)
		{
			return new DataTable
			{
				Locale = CultureInfo.InvariantCulture,
				Columns = { 
				{
					"TOTAL_BYTES",
					typeof(long)
				} }
			};
		}

		// Token: 0x06006DFC RID: 28156 RVA: 0x0017B44C File Offset: 0x0017964C
		protected DataTable LoadData(string queryName, DbConnection connection, string query, params string[] arguments)
		{
			arguments = arguments.Select((string s) => this.SqlSettings.QuoteNationalStringLiteral(s)).ToArray<string>();
			IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
			string text = query;
			object[] array = arguments;
			query = string.Format(invariantCulture, text, array);
			return this.LoadData(queryName, connection, query);
		}

		// Token: 0x06006DFD RID: 28157 RVA: 0x0017B490 File Offset: 0x00179690
		public DataTable LoadData(string queryName, DbConnection connection, string query)
		{
			DataTable dataTable2;
			using (DbCommand dbCommand = connection.CreateCommand())
			{
				TracingDbCommand.TryAddTracer(dbCommand, delegate(IHostTrace trace)
				{
					trace.Add("QueryName", queryName, false);
				});
				dbCommand.CommandText = query;
				DataTable dataTable = new DataTable
				{
					Locale = CultureInfo.InvariantCulture
				};
				using (DbDataReader dbDataReader = dbCommand.ExecuteReader())
				{
					dataTable.Load(dbDataReader);
				}
				dataTable2 = dataTable;
			}
			return dataTable2;
		}

		// Token: 0x06006DFE RID: 28158 RVA: 0x0017B524 File Offset: 0x00179724
		public virtual SqlDataType GetSqlScalarType(TypeValue type)
		{
			switch (type.TypeKind)
			{
			case ValueKind.Time:
				return SqlDataType.Time;
			case ValueKind.Date:
				return SqlDataType.DateTime2;
			case ValueKind.DateTime:
				return SqlDataType.DateTime2;
			case ValueKind.DateTimeZone:
				return SqlDataType.DateTimeOffset;
			case ValueKind.Duration:
				return SqlDataType.BigInt;
			case ValueKind.Number:
				return SqlDataType.Float;
			case ValueKind.Logical:
				return SqlDataType.Bit;
			case ValueKind.Text:
				return new SqlDataType(type, SqlLanguageStrings.NVarCharSqlString);
			case ValueKind.Binary:
				return SqlDataType.VarBinary;
			default:
				return null;
			}
		}

		// Token: 0x06006DFF RID: 28159 RVA: 0x0017B5A4 File Offset: 0x001797A4
		public Keys GetIdentityColumnNames(string schema, string table)
		{
			KeysBuilder keysBuilder = default(KeysBuilder);
			foreach (object obj in this.GetIdentityColumns(schema, table).Rows)
			{
				DataRow dataRow = (DataRow)obj;
				keysBuilder.Add((string)dataRow["COLUMN_NAME"]);
			}
			return keysBuilder.ToKeys();
		}

		// Token: 0x06006E00 RID: 28160 RVA: 0x0017B624 File Offset: 0x00179824
		public static string GetKey(string[] restrictions)
		{
			PersistentCacheKeyBuilder persistentCacheKeyBuilder = new PersistentCacheKeyBuilder();
			for (int i = 0; i < restrictions.Length; i++)
			{
				persistentCacheKeyBuilder.Add(restrictions[i]);
			}
			return persistentCacheKeyBuilder.ToString();
		}

		// Token: 0x06006E01 RID: 28161 RVA: 0x0017B654 File Offset: 0x00179854
		protected bool TryGetCachedSchemaTable(string collectionName, string[] restrictions, out DataTable schemaTable)
		{
			string text = PersistentCacheKey.ServerCatalog.Qualify(this.ConnectionInfo.CacheKey, collectionName, DbEnvironment.GetKey(restrictions));
			object obj;
			if (this.metadataCache.TryGetValue(text, (Stream s) => DbData.ReadTable(s), out obj))
			{
				schemaTable = (DataTable)obj;
				return true;
			}
			schemaTable = null;
			return false;
		}

		// Token: 0x06006E02 RID: 28162 RVA: 0x0017B6C8 File Offset: 0x001798C8
		protected DataTable GetSchemaTable(Func<DbConnection, DataTable> loader, bool openConnection, string collectionName, params string[] restrictions)
		{
			string text = PersistentCacheKey.ServerCatalog.Qualify(this.ConnectionInfo.CacheKey, collectionName, DbEnvironment.GetKey(restrictions));
			object obj;
			if (this.metadataCache.TryGetValue(text, (Stream s) => DbData.ReadTable(s), out obj))
			{
				return (DataTable)obj;
			}
			DataTable table = this.ConvertDbExceptions<DataTable>(delegate
			{
				DataTable table2;
				using (DbConnection dbConnection = this.CreateConnection())
				{
					if (openConnection)
					{
						dbConnection.Open(this);
					}
					table = loader(dbConnection);
					if (dbConnection.State == ConnectionState.Open)
					{
						dbConnection.Close();
					}
					table2 = table;
				}
				return table2;
			});
			this.metadataCache.CommitValue(text, delegate(Stream s, object o)
			{
				DbData.WriteTable(s, (DataTable)o);
			}, table);
			return table;
		}

		// Token: 0x06006E03 RID: 28163 RVA: 0x0017B7A4 File Offset: 0x001799A4
		public IList<TableKey> RetrieveKeysForTable(string schemaName, string tableName, Keys columnNames)
		{
			Dictionary<string, Dictionary<long, string>> dictionary = new Dictionary<string, Dictionary<long, string>>();
			Dictionary<string, bool> dictionary2 = new Dictionary<string, bool>();
			Dictionary<long, string> dictionary3 = null;
			DataTable indexes = this.GetIndexes(schemaName, tableName);
			IEnumerable enumerable;
			if (indexes.Columns.Contains("UNIQUE"))
			{
				enumerable = indexes.Select(string.Format(CultureInfo.InvariantCulture, "{0} = true or {1} = true", "PRIMARY_KEY", "UNIQUE"), "INDEX_NAME");
			}
			else
			{
				enumerable = indexes.Rows;
			}
			bool flag = indexes.Columns["PRIMARY_KEY"].DataType.Equals(typeof(bool));
			bool flag2 = indexes.Columns["ORDINAL_POSITION"].DataType.Equals(typeof(long));
			string text = null;
			foreach (object obj in enumerable)
			{
				DataRow dataRow = (DataRow)obj;
				string schemaColumn = DbEnvironment.GetSchemaColumn<string>(dataRow, "INDEX_NAME");
				if (text != schemaColumn)
				{
					text = schemaColumn;
					dictionary3 = new Dictionary<long, string>();
					DictionaryTracing.AddWithTracing<string, Dictionary<long, string>>(dictionary, text, dictionary3, base.Host, true, true);
					dictionary2.Add(text, flag ? DbEnvironment.GetSchemaColumn<bool>(dataRow, "PRIMARY_KEY") : DbEnvironment.GetNullableBooleanSchemaColumn(dataRow, "PRIMARY_KEY").Value);
				}
				string schemaColumn2 = DbEnvironment.GetSchemaColumn<string>(dataRow, "COLUMN_NAME");
				long num = (flag2 ? DbEnvironment.GetSchemaColumn<long>(dataRow, "ORDINAL_POSITION") : DbEnvironment.GetLongSchemaColumn(dataRow, "ORDINAL_POSITION"));
				DictionaryTracing.AddWithTracing<long, string>(dictionary3, num, schemaColumn2, base.Host, false, true);
			}
			Dictionary<string, int> reverseColumnLookup = new Dictionary<string, int>(columnNames.Length);
			for (int i = 0; i < columnNames.Length; i++)
			{
				DictionaryTracing.AddWithTracing<string, int>(reverseColumnLookup, columnNames[i], i, base.Host, true, false);
			}
			IList<TableKey> list = new List<TableKey>(dictionary.Count);
			Func<KeyValuePair<long, string>, int> <>9__1;
			foreach (KeyValuePair<string, Dictionary<long, string>> keyValuePair in dictionary)
			{
				try
				{
					IEnumerable<KeyValuePair<long, string>> enumerable2 = keyValuePair.Value.OrderBy((KeyValuePair<long, string> c) => c.Key);
					Func<KeyValuePair<long, string>, int> func;
					if ((func = <>9__1) == null)
					{
						func = (<>9__1 = (KeyValuePair<long, string> kc) => reverseColumnLookup[kc.Value]);
					}
					int[] array = enumerable2.Select(func).ToArray<int>();
					list.Add(new TableKey(array, dictionary2[keyValuePair.Key]));
				}
				catch (KeyNotFoundException)
				{
				}
			}
			return list;
		}

		// Token: 0x06006E04 RID: 28164 RVA: 0x0017BA6C File Offset: 0x00179C6C
		public IList<RelationshipInfo> RetrieveIncomingRelationshipsForTable(string schemaName, string tableName)
		{
			return this.RetrieveRelationshipsForTable(schemaName, tableName, true);
		}

		// Token: 0x06006E05 RID: 28165 RVA: 0x0017BA77 File Offset: 0x00179C77
		public IList<RelationshipInfo> RetrieveOutgoingRelationshipsForTable(string schemaName, string tableName)
		{
			return this.RetrieveRelationshipsForTable(schemaName, tableName, false);
		}

		// Token: 0x06006E06 RID: 28166 RVA: 0x0017BA84 File Offset: 0x00179C84
		private IList<RelationshipInfo> RetrieveRelationshipsForTable(string schemaName, string tableName, bool getIncomingRelationships)
		{
			IList<RelationshipInfo> list = new List<RelationshipInfo>();
			if (!this.DataSourceCapabilities.SupportsForeignKeys || !this.CreateRelationships)
			{
				return list;
			}
			DataTable dataTable = (getIncomingRelationships ? this.GetForeignKeysParent(schemaName, tableName) : this.GetForeignKeysChild(schemaName, tableName));
			bool flag = dataTable.Columns["ORDINAL"].DataType.Equals(typeof(long));
			Dictionary<DbEnvironment.FullyQualifiedKeyName, DbEnvironment.RelatedColumnList> dictionary = new Dictionary<DbEnvironment.FullyQualifiedKeyName, DbEnvironment.RelatedColumnList>();
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				DbEnvironment.FullyQualifiedKeyName fullyQualifiedKeyName = (getIncomingRelationships ? new DbEnvironment.FullyQualifiedKeyName(DbEnvironment.GetSchemaColumn<string>(dataRow, "FK_TABLE_SCHEMA"), DbEnvironment.GetSchemaColumn<string>(dataRow, "FK_TABLE_NAME"), DbEnvironment.GetSchemaColumn<string>(dataRow, "FK_NAME")) : new DbEnvironment.FullyQualifiedKeyName(DbEnvironment.GetSchemaColumn<string>(dataRow, "PK_TABLE_SCHEMA"), DbEnvironment.GetSchemaColumn<string>(dataRow, "PK_TABLE_NAME"), DbEnvironment.GetSchemaColumn<string>(dataRow, "FK_NAME")));
				DbEnvironment.RelatedColumns relatedColumns = new DbEnvironment.RelatedColumns(DbEnvironment.GetSchemaColumn<string>(dataRow, "PK_COLUMN_NAME"), DbEnvironment.GetSchemaColumn<string>(dataRow, "FK_COLUMN_NAME"), flag ? DbEnvironment.GetSchemaColumn<long>(dataRow, "ORDINAL") : DbEnvironment.GetLongSchemaColumn(dataRow, "ORDINAL"));
				DbEnvironment.RelatedColumnList relatedColumnList;
				if (!dictionary.TryGetValue(fullyQualifiedKeyName, out relatedColumnList))
				{
					relatedColumnList = new DbEnvironment.RelatedColumnList();
					dictionary.Add(fullyQualifiedKeyName, relatedColumnList);
				}
				relatedColumnList.Add(relatedColumns);
			}
			foreach (KeyValuePair<DbEnvironment.FullyQualifiedKeyName, DbEnvironment.RelatedColumnList> keyValuePair in dictionary)
			{
				RelationshipInfo relationshipInfo = new RelationshipInfo();
				SchemaItem schemaItem = new SchemaItem(schemaName, tableName, null);
				SchemaItem schemaItem2 = new SchemaItem(keyValuePair.Key.Schema, keyValuePair.Key.Table, null);
				relationshipInfo.Primary = (getIncomingRelationships ? schemaItem : schemaItem2);
				relationshipInfo.Foreign = (getIncomingRelationships ? schemaItem2 : schemaItem);
				foreach (DbEnvironment.RelatedColumns relatedColumns2 in keyValuePair.Value)
				{
					DictionaryTracing.AddWithTracing<long, string>(relationshipInfo.ReferringColumns, relatedColumns2.Ordinal, relatedColumns2.ForeignKeyColumn, base.Host, false, true);
					DictionaryTracing.AddWithTracing<long, string>(relationshipInfo.TargetColumns, relatedColumns2.Ordinal, relatedColumns2.PrimaryKeyColumn, base.Host, false, true);
				}
				list.Add(relationshipInfo);
			}
			return list;
		}

		// Token: 0x06006E07 RID: 28167 RVA: 0x0017BD14 File Offset: 0x00179F14
		public RecordValue RetrieveTypeInformationForFunction(string schemaName, string functionName, out TypeValue returnType)
		{
			returnType = null;
			IDictionary<long, NamedValue> dictionary = new SortedDictionary<long, NamedValue>();
			DataTable procedureParameters = this.GetProcedureParameters(schemaName, functionName);
			bool flag = procedureParameters.Columns["ORDINAL_POSITION"].DataType.Equals(typeof(long));
			foreach (object obj in procedureParameters.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				string schemaColumn = DbEnvironment.GetSchemaColumn<string>(dataRow, "PARAMETER_NAME");
				TypeValue typeValue;
				bool flag2;
				if (this.TryGetDataTypeValue(procedureParameters.Columns, dataRow, out typeValue, out flag2))
				{
					long num = (flag ? DbEnvironment.GetSchemaColumn<long>(dataRow, "ORDINAL_POSITION") : DbEnvironment.GetLongSchemaColumn(dataRow, "ORDINAL_POSITION"));
					TypeValue typeValue2 = DbTypeServices.CreateTypeValue(typeValue, true, flag2);
					if (num != 0L)
					{
						dictionary.Add(num, new NamedValue(schemaColumn, typeValue2));
					}
					else
					{
						returnType = typeValue2;
					}
				}
			}
			if (returnType == null)
			{
				returnType = this.CreateTableType(schemaName, functionName, true, null, null, null);
			}
			return RecordValue.New(dictionary.Values.ToArray<NamedValue>());
		}

		// Token: 0x06006E08 RID: 28168 RVA: 0x0000A6A5 File Offset: 0x000088A5
		public virtual DbDataReaderWithTableSchema WrapDbDataReader(DbDataReaderWithTableSchema reader)
		{
			return reader;
		}

		// Token: 0x06006E09 RID: 28169 RVA: 0x0017BE40 File Offset: 0x0017A040
		public virtual DbEnvironment.DbReaderWrapper CreateReaderWrapper(string cacheKey, bool forNativeQuery)
		{
			return DbEnvironment.DbReaderWrapper.Instance;
		}

		// Token: 0x06006E0A RID: 28170 RVA: 0x0017BE47 File Offset: 0x0017A047
		public static string BracketQuoteIdentifier(string identifier)
		{
			return "[" + identifier.Replace("]", "]]") + "]";
		}

		// Token: 0x06006E0B RID: 28171 RVA: 0x0017BE68 File Offset: 0x0017A068
		public override void ReportFoldingFailure()
		{
			using (this.Tracer.CreatePerformanceTrace("ReportFoldingFailure", TraceEventType.Information))
			{
				if (base.Host.QueryService<IFoldingFailureService>().ThrowOnFoldingFailure)
				{
					throw ValueException.NewExpressionError<Message0>(Strings.FoldingFailure, null, null);
				}
			}
		}

		// Token: 0x06006E0C RID: 28172 RVA: 0x0017BEC4 File Offset: 0x0017A0C4
		protected static bool TryGetDbEnvironment(Value value, out DbEnvironment environment)
		{
			environment = null;
			if (value.IsTable)
			{
				QueryResultQuery queryResultQuery = value.AsTable.Query as QueryResultQuery;
				DbEnvironment.UpdatableCatalogQuery updatableCatalogQuery = value.AsTable.Query as DbEnvironment.UpdatableCatalogQuery;
				if (queryResultQuery != null)
				{
					QueryResultTableValue queryResultTableValue = queryResultQuery.Table as QueryResultTableValue;
					if (queryResultTableValue != null)
					{
						environment = queryResultTableValue.Environment as DbEnvironment;
					}
				}
				else if (updatableCatalogQuery != null)
				{
					environment = updatableCatalogQuery.Environment;
				}
			}
			return environment != null;
		}

		// Token: 0x06006E0D RID: 28173 RVA: 0x0007D355 File Offset: 0x0007B555
		public virtual bool TryCreateTransactedEnvironment(DbTransactionInfo transaction, out DbEnvironment environment)
		{
			environment = null;
			return false;
		}

		// Token: 0x06006E0E RID: 28174 RVA: 0x0017BF30 File Offset: 0x0017A130
		public bool TryCreateVersion(string version)
		{
			if (version != null)
			{
				DbTransactionInfo dbTransactionInfo = new DbTransactionInfo(this, version);
				this.RegisterTransaction(dbTransactionInfo);
				return true;
			}
			return false;
		}

		// Token: 0x06006E0F RID: 28175 RVA: 0x0017BF54 File Offset: 0x0017A154
		public bool TryCommitVersion()
		{
			DbTransactionInfo dbTransactionInfo = this.TransactionInfo;
			if (this.IsRegisteredTransaction(dbTransactionInfo))
			{
				dbTransactionInfo.Commit();
				dbTransactionInfo.Dispose();
				return true;
			}
			return false;
		}

		// Token: 0x06006E10 RID: 28176 RVA: 0x000E6755 File Offset: 0x000E4955
		protected virtual bool TryGetExpression(out IExpression expression)
		{
			expression = null;
			return false;
		}

		// Token: 0x04003C9E RID: 15518
		private const int MaxRetryAttempts = 3;

		// Token: 0x04003C9F RID: 15519
		protected static readonly TimeSpan RetryDelay = TimeSpan.FromSeconds(0.1);

		// Token: 0x04003CA0 RID: 15520
		public static readonly string MachineConfigPath = Path.Combine(Path.Combine(RuntimeEnvironment.GetRuntimeDirectory(), "Config"), "machine.config");

		// Token: 0x04003CA1 RID: 15521
		public static readonly SqlDataType NumericBitType = new SqlDataType(TypeValue.Logical.NewFacets(TypeFacets.NewNumeric(new int?(10), new int?(1), new int?(0), SqlLanguageStrings.NumericSqlString.String)));

		// Token: 0x04003CA2 RID: 15522
		public static readonly SqlDataType Int8Type = new SqlDataType(TypeValue.Int8.NewFacets(TypeFacets.NewNumeric(new int?(10), new int?(3), new int?(0), SqlLanguageStrings.NumericSqlString.String)));

		// Token: 0x04003CA3 RID: 15523
		public static readonly SqlDataType Int16Type = new SqlDataType(TypeValue.Int16.NewFacets(TypeFacets.NewNumeric(new int?(10), new int?(5), new int?(0), SqlLanguageStrings.NumericSqlString.String)));

		// Token: 0x04003CA4 RID: 15524
		public static readonly SqlDataType Int32Type = new SqlDataType(TypeValue.Int32.NewFacets(TypeFacets.NewNumeric(new int?(10), new int?(10), new int?(0), SqlLanguageStrings.NumericSqlString.String)));

		// Token: 0x04003CA5 RID: 15525
		public static readonly SqlDataType Int64Type = new SqlDataType(TypeValue.Int64.NewFacets(TypeFacets.NewNumeric(new int?(10), new int?(19), new int?(0), SqlLanguageStrings.NumericSqlString.String)));

		// Token: 0x04003CA6 RID: 15526
		public static readonly SqlDataType DecimalType = new SqlDataType(TypeValue.Decimal.NewFacets(TypeFacets.NewNumeric(new int?(10), new int?(38), new int?(6), SqlLanguageStrings.DecimalSqlString.String)));

		// Token: 0x04003CA7 RID: 15527
		public static readonly SqlDataType CurrencyType = new SqlDataType(TypeValue.Currency.NewFacets(TypeFacets.NewNumeric(new int?(10), new int?(19), new int?(4), SqlLanguageStrings.NumericSqlString.String)));

		// Token: 0x04003CA8 RID: 15528
		private static readonly Dictionary<string, DbTransactionInfo> transactions = new Dictionary<string, DbTransactionInfo>();

		// Token: 0x04003CA9 RID: 15529
		protected readonly IDbService dbService;

		// Token: 0x04003CAA RID: 15530
		private readonly DbTransactionInfo transactionInfo;

		// Token: 0x04003CAB RID: 15531
		private readonly IPersistentObjectCache metadataCache;

		// Token: 0x04003CAC RID: 15532
		private readonly IKnownExceptionService knownExceptionService;

		// Token: 0x04003CAD RID: 15533
		private readonly string dataSourceName;

		// Token: 0x04003CAE RID: 15534
		private readonly string server;

		// Token: 0x04003CAF RID: 15535
		private readonly string database;

		// Token: 0x04003CB0 RID: 15536
		private readonly IResource resource;

		// Token: 0x04003CB1 RID: 15537
		private readonly IDataSourceLocation location;

		// Token: 0x04003CB2 RID: 15538
		private readonly Value options;

		// Token: 0x04003CB3 RID: 15539
		private SqlSettings sqlSettings;

		// Token: 0x04003CB4 RID: 15540
		private Tracer tracer;

		// Token: 0x04003CB5 RID: 15541
		private OptionsRecord userOptions;

		// Token: 0x04003CB6 RID: 15542
		private ConnectionInfo? connectionInfo;

		// Token: 0x04003CB7 RID: 15543
		private DbEnvironment.DbServerMetadata serverMetadata;

		// Token: 0x04003CB8 RID: 15544
		private bool currentlyRetrievingServerMetadata;

		// Token: 0x04003CB9 RID: 15545
		private IDictionary<SchemaItem, Value> itemCatalog;

		// Token: 0x04003CBA RID: 15546
		private IEnumerable<string> allSchemas;

		// Token: 0x04003CBB RID: 15547
		private bool commandTimeoutSet;

		// Token: 0x04003CBC RID: 15548
		private int? commandTimeout;

		// Token: 0x04003CBD RID: 15549
		private bool connectionTimeoutSet;

		// Token: 0x04003CBE RID: 15550
		private int? connectionTimeout;

		// Token: 0x04003CBF RID: 15551
		private ResourceCredentialCollection credentials;

		// Token: 0x04003CC0 RID: 15552
		private NavigationPropertiesHelper.NameGenerator nameGenerator;

		// Token: 0x04003CC1 RID: 15553
		private NavigationPropertiesHelper.NavigationPropertiesRecord navigationPropertiesRecord;

		// Token: 0x04003CC2 RID: 15554
		private DbCatalogTableValue dbCatalog;

		// Token: 0x04003CC3 RID: 15555
		private IHostProgress hostProgressService;

		// Token: 0x04003CC4 RID: 15556
		protected static readonly IDictionary<string, TableType> defaultTableTypes = new Dictionary<string, TableType>(StringComparer.OrdinalIgnoreCase)
		{
			{
				"BASE TABLE",
				new TableType("BASE TABLE", "Table")
			},
			{
				"BASE",
				new TableType("BASE", "Table")
			},
			{
				"TABLE",
				new TableType("TABLE", "Table")
			},
			{
				"LINK",
				new TableType("LINK", "Table")
			},
			{
				"VIEW",
				new TableType("VIEW", "View")
			}
		};

		// Token: 0x04003CC5 RID: 15557
		protected const string IndexesSchemaIndexName = "INDEX_NAME";

		// Token: 0x04003CC6 RID: 15558
		protected const string IndexesSchemaPrimaryKey = "PRIMARY_KEY";

		// Token: 0x04003CC7 RID: 15559
		protected const string IndexesSchemaUnique = "UNIQUE";

		// Token: 0x04003CC8 RID: 15560
		public const string DatabaseInformationSchemaDescriptionName = "DESCRIPTION";

		// Token: 0x04003CC9 RID: 15561
		protected const string TablesSchemaTableType = "TABLE_TYPE";

		// Token: 0x04003CCA RID: 15562
		protected const string TablesSchemaSchemaName = "TABLE_SCHEMA";

		// Token: 0x04003CCB RID: 15563
		protected const string TablesSchemaTableName = "TABLE_NAME";

		// Token: 0x04003CCC RID: 15564
		protected const string TablesSchemaCreatedDateName = "CREATED_DATE";

		// Token: 0x04003CCD RID: 15565
		protected const string TablesSchemaModifiedDateName = "MODIFIED_DATE";

		// Token: 0x04003CCE RID: 15566
		protected const string TablesSchemaDescriptionName = "DESCRIPTION";

		// Token: 0x04003CCF RID: 15567
		protected const string SchemasSchemaSchemaName = "SCHEMA_NAME";

		// Token: 0x04003CD0 RID: 15568
		protected const string ProceduresSchemaSchemaName = "ROUTINE_SCHEMA";

		// Token: 0x04003CD1 RID: 15569
		protected const string ProceduresSchemaFunctionName = "ROUTINE_NAME";

		// Token: 0x04003CD2 RID: 15570
		protected const string ProceduresSchemaFunctionType = "ROUTINE_TYPE";

		// Token: 0x04003CD3 RID: 15571
		protected const string ProceduresSchemaFunctionValue = "FUNCTION";

		// Token: 0x04003CD4 RID: 15572
		protected const string ProceduresSchemaParameterName = "PARAMETER_NAME";

		// Token: 0x04003CD5 RID: 15573
		private const string ProceduresSchemaCreatedDateName = "CREATED_DATE";

		// Token: 0x04003CD6 RID: 15574
		private const string ProceduresSchemaModifiedDateName = "MODIFIED_DATE";

		// Token: 0x04003CD7 RID: 15575
		private const string ProceduresSchemaDescriptionName = "DESCRIPTION";

		// Token: 0x04003CD8 RID: 15576
		private const string ProceduresSchemaProcedureValue = "PROCEDURE";

		// Token: 0x04003CD9 RID: 15577
		protected const string ColumnsSchemaColumnName = "COLUMN_NAME";

		// Token: 0x04003CDA RID: 15578
		protected const string ColumnsSchemaNullableName = "IS_NULLABLE";

		// Token: 0x04003CDB RID: 15579
		protected const string ColumnsSchemaDataTypeName = "DATA_TYPE";

		// Token: 0x04003CDC RID: 15580
		protected const string ColumnsSchemaOrdinalName = "ORDINAL_POSITION";

		// Token: 0x04003CDD RID: 15581
		protected const string ColumnsSchemaNumericPrecisionRadixName = "NUMERIC_PRECISION_RADIX";

		// Token: 0x04003CDE RID: 15582
		protected const string ColumnsSchemaNumericPrecisionName = "NUMERIC_PRECISION";

		// Token: 0x04003CDF RID: 15583
		protected const string ColumnsSchemaNumericScaleName = "NUMERIC_SCALE";

		// Token: 0x04003CE0 RID: 15584
		protected const string ColumnsSchemaDateTimePrecisionName = "DATETIME_PRECISION";

		// Token: 0x04003CE1 RID: 15585
		protected const string ColumnsSchemaCharMaxLengthName = "CHARACTER_MAXIMUM_LENGTH";

		// Token: 0x04003CE2 RID: 15586
		protected const string ColumnsSchemaIsWritableName = "IS_WRITABLE";

		// Token: 0x04003CE3 RID: 15587
		protected const string ColumnsSchemaColumnDefaultName = "COLUMN_DEFAULT";

		// Token: 0x04003CE4 RID: 15588
		protected const string ColumnsSchemaColumnExpressionName = "COLUMN_EXPRESSION";

		// Token: 0x04003CE5 RID: 15589
		protected const string ColumnsSchemaDescriptionName = "DESCRIPTION";

		// Token: 0x04003CE6 RID: 15590
		protected const string SchemaForeignKeysPrimaryTableSchema = "PK_TABLE_SCHEMA";

		// Token: 0x04003CE7 RID: 15591
		protected const string SchemaForeignKeysPrimaryTableName = "PK_TABLE_NAME";

		// Token: 0x04003CE8 RID: 15592
		protected const string SchemaForeignKeysPrimaryColumnName = "PK_COLUMN_NAME";

		// Token: 0x04003CE9 RID: 15593
		protected const string SchemaForeignKeysForeignTableSchema = "FK_TABLE_SCHEMA";

		// Token: 0x04003CEA RID: 15594
		protected const string SchemaForeignKeysForeignTableName = "FK_TABLE_NAME";

		// Token: 0x04003CEB RID: 15595
		protected const string SchemaForeignKeysForeignColumnName = "FK_COLUMN_NAME";

		// Token: 0x04003CEC RID: 15596
		protected const string SchemaForeignKeysOrdinal = "ORDINAL";

		// Token: 0x04003CED RID: 15597
		protected const string SchemaForeignKeysName = "FK_NAME";

		// Token: 0x04003CEE RID: 15598
		protected const string ResourceInformationSchemaTotalBytes = "TOTAL_BYTES";

		// Token: 0x04003CEF RID: 15599
		protected const string dbo = "dbo";

		// Token: 0x04003CF0 RID: 15600
		public const string SqlSchemaKey = "Sql.Schema";

		// Token: 0x04003CF1 RID: 15601
		public const string SqlTableKey = "Sql.Table";

		// Token: 0x04003CF2 RID: 15602
		private static readonly TypeValue DelayedNullableTextType = PreviewServices.ConvertToDelayedValue(TypeValue.Text.Nullable, "Value");

		// Token: 0x04003CF3 RID: 15603
		private static readonly TypeValue DelayedNullableNumberType = PreviewServices.ConvertToDelayedValue(TypeValue.Number.Nullable, "Value");

		// Token: 0x04003CF4 RID: 15604
		private static readonly Value NullableTextTypeField = RecordTypeAlgebra.NewField(TypeValue.Text.Nullable, false);

		// Token: 0x04003CF5 RID: 15605
		private static readonly Value NullableDateTimeTypeField = RecordTypeAlgebra.NewField(TypeValue.DateTime.Nullable, false);

		// Token: 0x04003CF6 RID: 15606
		private static readonly Value DelayedNullableNumberTypeField = RecordTypeAlgebra.NewField(DbEnvironment.DelayedNullableNumberType, false);

		// Token: 0x04003CF7 RID: 15607
		public static readonly Value DelayedNullableTextTypeField = RecordTypeAlgebra.NewField(DbEnvironment.DelayedNullableTextType, false);

		// Token: 0x04003CF8 RID: 15608
		protected static readonly Lazy<PrivateAdoDotNetProviderManager> privateProviderManager = new Lazy<PrivateAdoDotNetProviderManager>(() => new PrivateAdoDotNetProviderManager());

		// Token: 0x0200105A RID: 4186
		private struct FullyQualifiedKeyName
		{
			// Token: 0x06006E1B RID: 28187 RVA: 0x0017C363 File Offset: 0x0017A563
			public FullyQualifiedKeyName(string schema, string table, string keyname)
			{
				this.Schema = schema;
				this.Table = table;
				this.KeyName = keyname;
			}

			// Token: 0x04003CF9 RID: 15609
			public readonly string Schema;

			// Token: 0x04003CFA RID: 15610
			public readonly string Table;

			// Token: 0x04003CFB RID: 15611
			public readonly string KeyName;
		}

		// Token: 0x0200105B RID: 4187
		private struct RelatedColumns
		{
			// Token: 0x06006E1C RID: 28188 RVA: 0x0017C37A File Offset: 0x0017A57A
			public RelatedColumns(string pkcolumn, string fkcolumn, long ordinal)
			{
				this.PrimaryKeyColumn = pkcolumn;
				this.ForeignKeyColumn = fkcolumn;
				this.Ordinal = ordinal;
			}

			// Token: 0x04003CFC RID: 15612
			public readonly string PrimaryKeyColumn;

			// Token: 0x04003CFD RID: 15613
			public readonly string ForeignKeyColumn;

			// Token: 0x04003CFE RID: 15614
			public readonly long Ordinal;
		}

		// Token: 0x0200105C RID: 4188
		private class RelatedColumnList : List<DbEnvironment.RelatedColumns>
		{
		}

		// Token: 0x0200105D RID: 4189
		protected class DbServerMetadata
		{
			// Token: 0x17001F2B RID: 7979
			// (get) Token: 0x06006E1E RID: 28190 RVA: 0x0017C399 File Offset: 0x0017A599
			// (set) Token: 0x06006E1F RID: 28191 RVA: 0x0017C3A1 File Offset: 0x0017A5A1
			public string Version { get; set; }

			// Token: 0x06006E20 RID: 28192 RVA: 0x0017C3AA File Offset: 0x0017A5AA
			public void Serialize(Stream s)
			{
				this.Serialize(new BinaryWriter(s));
			}

			// Token: 0x06006E21 RID: 28193 RVA: 0x0017C3B8 File Offset: 0x0017A5B8
			public void Deserialize(Stream s)
			{
				this.Deserialize(new BinaryReader(s));
			}

			// Token: 0x06006E22 RID: 28194 RVA: 0x0017C3C6 File Offset: 0x0017A5C6
			protected virtual void Serialize(BinaryWriter writer)
			{
				writer.WriteNullableString(this.Version);
			}

			// Token: 0x06006E23 RID: 28195 RVA: 0x0017C3D4 File Offset: 0x0017A5D4
			protected virtual void Deserialize(BinaryReader reader)
			{
				this.Version = reader.ReadNullableString();
			}
		}

		// Token: 0x0200105E RID: 4190
		public class DbReaderWrapper
		{
			// Token: 0x06006E25 RID: 28197 RVA: 0x0000A6A5 File Offset: 0x000088A5
			public virtual DbDataReaderWithTableSchema Wrap(DbDataReaderWithTableSchema reader)
			{
				return reader;
			}

			// Token: 0x04003D00 RID: 15616
			public static readonly DbEnvironment.DbReaderWrapper Instance = new DbEnvironment.DbReaderWrapper();
		}

		// Token: 0x0200105F RID: 4191
		protected sealed class UpdatableCatalogQuery : FilteredTableQuery
		{
			// Token: 0x06006E28 RID: 28200 RVA: 0x0017C3EE File Offset: 0x0017A5EE
			public UpdatableCatalogQuery(DbEnvironment environment, TableValue table, string schema)
				: base(table, environment.Host)
			{
				this.environment = environment;
				this.schema = schema;
			}

			// Token: 0x17001F2C RID: 7980
			// (get) Token: 0x06006E29 RID: 28201 RVA: 0x0017C40B File Offset: 0x0017A60B
			public DbEnvironment Environment
			{
				get
				{
					return this.environment;
				}
			}

			// Token: 0x06006E2A RID: 28202 RVA: 0x0017C413 File Offset: 0x0017A613
			public override bool TryGetExpression(out IExpression expression)
			{
				return this.environment.TryGetExpression(out expression);
			}

			// Token: 0x06006E2B RID: 28203 RVA: 0x0017C424 File Offset: 0x0017A624
			public override bool TrySelectRows(FunctionValue condition, out Query query)
			{
				DbCatalogTableValue dbCatalogTableValue = base.Table as DbCatalogTableValue;
				TableValue tableValue;
				if (dbCatalogTableValue != null && dbCatalogTableValue.TrySelectRows(condition, out tableValue))
				{
					query = tableValue.Query;
					return true;
				}
				return base.TrySelectRows(condition, out query);
			}

			// Token: 0x06006E2C RID: 28204 RVA: 0x0017C460 File Offset: 0x0017A660
			public override bool TryInvokeAsArgument(FunctionValue function, Value[] arguments, int index, out Value result)
			{
				if (function.Equals(Library._Value.VersionIdentity) && this.environment.TransactionInfo != null)
				{
					result = TextValue.New(this.environment.TransactionInfo.Identity);
					return true;
				}
				TableValue tableValue;
				if (function.Equals(Library._Value.Versions) && new DbValueVersions(this.environment, (DbEnvironment versionEnv) => versionEnv.CreateCatalogTableValue(versionEnv.Host, this.schema)).TryCreateTable(out tableValue))
				{
					result = tableValue;
					return true;
				}
				result = null;
				return false;
			}

			// Token: 0x06006E2D RID: 28205 RVA: 0x0017C4D9 File Offset: 0x0017A6D9
			public override Value NativeQuery(TextValue query, Value parameters, Value options)
			{
				return this.environment.NativeQuery(base.Table, query, parameters, options);
			}

			// Token: 0x06006E2E RID: 28206 RVA: 0x0017C4EF File Offset: 0x0017A6EF
			public override ActionValue InsertRows(Query rowsToInsert)
			{
				this.environment.VerifyActionPermitted();
				return CountOnlyTableBindingActionValue.New((bool countOnlyTable) => this.InsertRows(rowsToInsert, countOnlyTable));
			}

			// Token: 0x06006E2F RID: 28207 RVA: 0x0017C51F File Offset: 0x0017A71F
			public override void TestConnection()
			{
				this.environment.TestConnection();
			}

			// Token: 0x06006E30 RID: 28208 RVA: 0x0017C52C File Offset: 0x0017A72C
			private ActionValue InsertRows(Query rowsToInsert, bool countOnlyTable)
			{
				List<IValueReference> list = new List<IValueReference>();
				list.Add(ActionModule.Action.Return.Invoke(TableValue.Empty));
				foreach (IValueReference valueReference in new QueryTableValue(rowsToInsert))
				{
					RecordValue asRecord = valueReference.Value.AsRecord;
					Alias alias;
					Alias alias2;
					if (this.schema != null)
					{
						alias = Alias.NewNativeAlias(this.schema);
						alias2 = Alias.NewNativeAlias(asRecord["Name"].AsString);
					}
					else
					{
						alias = Alias.NewNativeAlias("dbo");
						Value value;
						if (asRecord.TryGetValue("Schema", out value))
						{
							alias = Alias.NewNativeAlias(value.AsString);
						}
						alias2 = Alias.NewNativeAlias(asRecord["Item"].AsString);
						Value value2;
						if (asRecord.TryGetValue("Name", out value2) && DbEnvironment.CreateIdentifierName(alias.Name, alias2.Name) != value2.AsString)
						{
							throw ValueException.NewDataSourceError<Message0>(Strings.Catalog_InsertNameMustMatchSchemaItem, asRecord, null);
						}
					}
					string text = "Table";
					Value value3;
					if (asRecord.TryGetValue("Kind", out value3))
					{
						text = value3.AsString;
						if (!(text == "Table"))
						{
							if (!(text == "View"))
							{
								throw ValueException.NewDataSourceError<Message0>(Strings.Catalog_InsertKindMustBeTableOrView, asRecord, null);
							}
							if (!this.environment.SqlSettings.SupportsViewCreation)
							{
								throw ValueException.NewDataSourceError<Message0>(Strings.Catalog_InsertKindMustBeTable, asRecord, null);
							}
							if (this.environment.UserOptions.GetBool("EnableCrossDatabaseFolding", false))
							{
								throw ValueException.NewDataSourceError<Message0>(Strings.Catalog_CreateViewNoCrossDatabaseFolding, asRecord, null);
							}
						}
					}
					SchemaItem schemaItem = new SchemaItem(alias.Name, alias2.Name, text);
					TableValue asTable = asRecord["Data"].AsTable;
					this.VerifyTableToInsertIsValid(asTable);
					List<SqlColumnDefinition> list2 = new List<SqlColumnDefinition>();
					for (int i = 0; i < asTable.Columns.Length; i++)
					{
						TypeValue columnType = asTable.GetColumnType(i);
						SqlDataType sqlScalarType = this.environment.GetSqlScalarType(columnType.NonNullable);
						if (sqlScalarType == null)
						{
							throw ValueException.NewDataSourceError<Message1>(Strings.Catalog_UnsupportedColumnType(asTable.Columns[i]), asTable.GetColumnType(i), null);
						}
						List<SqlColumnConstraint> list3 = new List<SqlColumnConstraint>();
						if (!columnType.IsNullable)
						{
							list3.Add(NotNullConstraint.Instance);
						}
						list2.Add(new SqlColumnDefinition(new ColumnReference(Alias.NewNativeAlias(asTable.Columns[i])), sqlScalarType, list3));
					}
					TableTypeValue asTableType = asTable.Type.NewMeta(RecordValue.New(Keys.New("Sql.Schema", "Sql.Table"), new Value[]
					{
						TextValue.New(alias.Name),
						TextValue.New(alias2.Name)
					})).AsType.AsTableType;
					ActionValue actionValue = null;
					ActionValue actionValue2 = null;
					if (text == "View")
					{
						QueryResultQuery queryResultQuery = asTable.Query.QueryDomain.Optimize(asTable.Query) as QueryResultQuery;
						if (queryResultQuery != null && queryResultQuery.QueryResultTable.Environment.IsSameDataSourceEnvironment(this.environment))
						{
							DbQueryPlan completeDbQueryPlan = ((DbValueBuilder)queryResultQuery.QueryResultTable.ValueBuilder).CompleteDbQueryPlan;
							actionValue = DbStatementActionValue.New(this.environment, this, new DbStatementPlan(new SqlCreateViewStatement(new TableReference(alias, alias2), list2, completeDbQueryPlan.Query), DbAstCreator.CreateOptions(this.environment), this.environment.SqlSettings, countOnlyTable), "CreateView", null);
							TableValue tableValue = NavigationPropertiesHelper.AddNavigationPropertiesToDatabase(new Dictionary<SchemaItem, Value> { 
							{
								schemaItem,
								Value.Null
							} }, this.environment, this.environment.NameGenerator);
							actionValue = ActionValue.New(ListValue.New(new Value[]
							{
								actionValue.ClearCache(this.environment.Host),
								ActionModule.Action.Return.Invoke(tableValue.Item0)
							}));
							TableValue tableValue2 = new QueryResultTableValue(this.environment, DbEnvironment.CreateIdentifierName(alias.Name, alias2.Name), this.environment.Host, asTableType);
							actionValue2 = ActionModule.Action.Return.Invoke(tableValue2).AsAction;
						}
					}
					else
					{
						actionValue = DbStatementActionValue.New(this.environment, this, new DbStatementPlan(new SqlCreateTableStatement(new TableReference(alias, alias2, this.environment.UserOptions.GetBool("EnableCrossDatabaseFolding", false) ? Alias.NewNativeAlias(this.environment.database) : null), list2), DbAstCreator.CreateOptions(this.environment), this.environment.SqlSettings, countOnlyTable), "CreateTable", null);
						TableValue tableValue3 = NavigationPropertiesHelper.AddNavigationPropertiesToDatabase(new Dictionary<SchemaItem, Value> { 
						{
							schemaItem,
							Value.Null
						} }, this.environment, this.environment.NameGenerator);
						actionValue = ActionValue.New(ListValue.New(new Value[]
						{
							actionValue.ClearCache(this.environment.Host),
							ActionModule.Action.Return.Invoke(tableValue3.Item0)
						}));
						actionValue2 = new QueryResultTableValue(this.environment, DbEnvironment.CreateIdentifierName(alias.Name, alias2.Name), this.environment.Host, asTableType).InsertRows(asTable);
					}
					if (actionValue == null)
					{
						throw ValueException.NewDataSourceError<Message0>(Strings.Value_UpdateNotSupported, asRecord, null);
					}
					if (countOnlyTable)
					{
						actionValue2 = actionValue2.Bind(new ReturnConstantFunctionValue(NumberValue.Zero));
						actionValue2 = actionValue2.Bind(new ReturnTypedTableFromCountFunctionValue(asTableType));
					}
					ActionValue actionValue3 = ActionHelpers.NewSequenceResultsAction(new ActionValue[] { actionValue, actionValue2 });
					actionValue3 = actionValue3.Bind(ActionHelpers.NewCombineParentChildResultsFunction(0, 1, "Data").ToBinding());
					actionValue3 = actionValue3.Bind(ActionHelpers.TableFromRecord.ToBinding());
					list.Add(ActionHelpers.NewCombineActionResultsFunction(actionValue3));
				}
				return ActionValue.New(ListValue.New(list)).ClearCache(this.environment.Host);
			}

			// Token: 0x06006E31 RID: 28209 RVA: 0x0017CB1C File Offset: 0x0017AD1C
			public override ActionValue UpdateRows(ColumnUpdates columnUpdates, FunctionValue selector)
			{
				this.environment.VerifyActionPermitted();
				throw ValueException.NewDataSourceError<Message0>(Strings.Catalog_OnlyInsertAndDeleteSupported, new QueryTableValue(this), null);
			}

			// Token: 0x06006E32 RID: 28210 RVA: 0x0017CB3A File Offset: 0x0017AD3A
			public override ActionValue DeleteRows(FunctionValue selector)
			{
				this.environment.VerifyActionPermitted();
				return CountOnlyTableBindingActionValue.New((bool countOnlyTable) => this.DeleteRows(selector, countOnlyTable));
			}

			// Token: 0x06006E33 RID: 28211 RVA: 0x0017CB6C File Offset: 0x0017AD6C
			private ActionValue DeleteRows(FunctionValue selector, bool countOnlyTable)
			{
				List<IValueReference> list = new List<IValueReference>();
				list.Add(ActionModule.Action.Return.Invoke(TableValue.Empty));
				foreach (IValueReference valueReference in base.Table.SelectRows(selector))
				{
					RecordValue asRecord = valueReference.Value.AsRecord;
					Alias alias;
					Alias alias2;
					if (this.schema != null)
					{
						alias = Alias.NewNativeAlias(this.schema);
						alias2 = Alias.NewNativeAlias(asRecord["Name"].AsString);
					}
					else
					{
						alias = Alias.NewNativeAlias(asRecord["Schema"].AsString);
						alias2 = Alias.NewNativeAlias(asRecord["Item"].AsString);
					}
					string asString = asRecord["Kind"].AsString;
					SchemaItem schemaItem = new SchemaItem(alias.Name, alias2.Name, asString);
					ActionValue actionValue = DbStatementActionValue.New(this.environment, this, new DbStatementPlan(this.GetDropStatement(asString, new TableReference(alias, alias2, (this.environment.UserOptions.GetBool("EnableCrossDatabaseFolding", false) && asString == "Table") ? Alias.NewNativeAlias(this.environment.database) : null), asRecord), DbAstCreator.CreateOptions(this.environment), this.environment.SqlSettings, countOnlyTable), "Drop" + asString, null);
					TableValue tableValue = NavigationPropertiesHelper.AddNavigationPropertiesToDatabase(new Dictionary<SchemaItem, Value> { 
					{
						schemaItem,
						Value.Null
					} }, this.environment, this.environment.NameGenerator);
					actionValue = ActionValue.New(ListValue.New(new Value[]
					{
						actionValue,
						ActionModule.Action.Return.Invoke(tableValue.Item0)
					}));
					ActionValue actionValue2;
					if (countOnlyTable)
					{
						actionValue2 = actionValue;
					}
					else
					{
						ActionValue actionValue3 = asRecord["Data"].AsTable.DeleteRows();
						actionValue2 = ActionHelpers.NewSequenceResultsAction(new ActionValue[] { actionValue3, actionValue });
						actionValue2 = actionValue2.Bind(ActionHelpers.NewCombineParentChildResultsFunction(1, 0, "Data").ToBinding());
					}
					actionValue2 = actionValue2.Bind(ActionHelpers.TableFromRecord.ToBinding());
					list.Add(ActionHelpers.NewCombineActionResultsFunction(actionValue2));
				}
				return ActionValue.New(ListValue.New(list)).ClearCache(this.environment.Host);
			}

			// Token: 0x06006E34 RID: 28212 RVA: 0x0017CDD0 File Offset: 0x0017AFD0
			private SqlStatement GetDropStatement(string kind, TableReference tableReference, RecordValue row)
			{
				if (!(kind == "View"))
				{
					if (!(kind == "Table"))
					{
						throw ValueException.NewDataSourceError<Message0>(Strings.Value_UpdateNotSupported, row, null);
					}
					return new SqlDropTableStatement(tableReference);
				}
				else
				{
					if (this.environment.UserOptions.GetBool("EnableCrossDatabaseFolding", false))
					{
						throw ValueException.NewDataSourceError<Message0>(Strings.Catalog_DropViewNoCrossDatabaseFolding, new QueryTableValue(this), null);
					}
					return new SqlDropViewStatement(tableReference);
				}
			}

			// Token: 0x06006E35 RID: 28213 RVA: 0x0017CE3D File Offset: 0x0017B03D
			public override ActionValue NativeStatement(TextValue statement, Value parameters, Value options)
			{
				return this.environment.NativeStatement(base.Table, statement, parameters, options);
			}

			// Token: 0x06006E36 RID: 28214 RVA: 0x0017CE53 File Offset: 0x0017B053
			private void VerifyTableToInsertIsValid(TableValue row)
			{
				if (row.Columns.Length < 1)
				{
					throw ValueException.NewDataSourceError<Message0>(Strings.Catalog_InsertKindMustHaveOneColumn, row, null);
				}
			}

			// Token: 0x04003D01 RID: 15617
			private readonly DbEnvironment environment;

			// Token: 0x04003D02 RID: 15618
			private readonly string schema;
		}

		// Token: 0x02001062 RID: 4194
		protected sealed class UpdatableHierarchicalNavigationQuery : FilteredTableQuery
		{
			// Token: 0x06006E3C RID: 28220 RVA: 0x0017CEAC File Offset: 0x0017B0AC
			public UpdatableHierarchicalNavigationQuery(DbEnvironment environment, TableValue table)
				: base(table, environment.Host)
			{
				this.environment = environment;
			}

			// Token: 0x17001F2D RID: 7981
			// (get) Token: 0x06006E3D RID: 28221 RVA: 0x0017CEC2 File Offset: 0x0017B0C2
			private new DbHierarchicalNavigationTableValue Table
			{
				get
				{
					return (DbHierarchicalNavigationTableValue)base.Table;
				}
			}

			// Token: 0x06006E3E RID: 28222 RVA: 0x0017CECF File Offset: 0x0017B0CF
			public override void TestConnection()
			{
				this.environment.TestConnection();
			}

			// Token: 0x06006E3F RID: 28223 RVA: 0x0017CEDC File Offset: 0x0017B0DC
			public override bool TrySelectRows(FunctionValue condition, out Query query)
			{
				DbHierarchicalNavigationTableValue table = this.Table;
				TableValue tableValue;
				if (table != null && table.TrySelectRows(condition, out tableValue))
				{
					query = tableValue.Query;
					return true;
				}
				return base.TrySelectRows(condition, out query);
			}

			// Token: 0x06006E40 RID: 28224 RVA: 0x0017CF10 File Offset: 0x0017B110
			public override Value NativeQuery(TextValue query, Value parameters, Value options)
			{
				return this.environment.NativeQuery(this.Table, query, parameters, options);
			}

			// Token: 0x06006E41 RID: 28225 RVA: 0x0017CF26 File Offset: 0x0017B126
			public override ActionValue NativeStatement(TextValue statement, Value parameters, Value options)
			{
				return this.environment.NativeStatement(this.Table, statement, parameters, options);
			}

			// Token: 0x06006E42 RID: 28226 RVA: 0x0017CF3C File Offset: 0x0017B13C
			public override ActionValue InsertRows(Query rowsToInsert)
			{
				this.environment.VerifyActionPermitted();
				return CountOnlyTableBindingActionValue.New((bool countOnlyTable) => this.InsertRows(rowsToInsert, countOnlyTable));
			}

			// Token: 0x06006E43 RID: 28227 RVA: 0x0017CF6C File Offset: 0x0017B16C
			private ActionValue InsertRows(Query rowsToInsert, bool countOnlyTable)
			{
				if (!countOnlyTable)
				{
					throw ValueException.NewDataSourceError<Message0>(Strings.Value_UpdateNotSupported, new QueryTableValue(this), null);
				}
				List<IValueReference> list = new List<IValueReference>();
				list.Add(ActionModule.Action.Return.Invoke(TableValue.Empty));
				foreach (IValueReference valueReference in new QueryTableValue(rowsToInsert))
				{
					RecordValue asRecord = valueReference.Value.AsRecord;
					Alias alias = Alias.NewNativeAlias(asRecord["Schema"].AsString);
					Value value;
					if (asRecord.TryGetValue("Data", out value))
					{
						throw ValueException.NewDataSourceError<Message0>(Strings.Catalog_CreateSchemaNoDataAllowed, new QueryTableValue(this), null);
					}
					if (this.environment.UserOptions.GetBool("EnableCrossDatabaseFolding", false))
					{
						throw ValueException.NewDataSourceError<Message0>(Strings.Catalog_CreateSchemaNoCrossDatabaseFolding, new QueryTableValue(this), null);
					}
					ActionValue actionValue = DbStatementActionValue.New(this.environment, this, new DbStatementPlan(new SqlCreateSchemaStatement(alias), DbAstCreator.CreateOptions(this.environment), this.environment.SqlSettings, countOnlyTable), "CreateSchema", null);
					list.Add(actionValue);
				}
				list.Add(ActionModule.Action.Return.Invoke(new CountTableValue((long)(list.Count - 1))));
				return ActionValue.New(ListValue.New(list)).ClearCache(this.environment.Host);
			}

			// Token: 0x06006E44 RID: 28228 RVA: 0x0017D0C8 File Offset: 0x0017B2C8
			public override ActionValue UpdateRows(ColumnUpdates columnUpdates, FunctionValue selector)
			{
				this.environment.VerifyActionPermitted();
				throw ValueException.NewDataSourceError<Message0>(Strings.Catalog_OnlyInsertAndDeleteSupported, new QueryTableValue(this), null);
			}

			// Token: 0x06006E45 RID: 28229 RVA: 0x0017D0E6 File Offset: 0x0017B2E6
			public override ActionValue DeleteRows(FunctionValue selector)
			{
				this.environment.VerifyActionPermitted();
				return CountOnlyTableBindingActionValue.New((bool countOnlyTable) => this.DeleteRows(selector, countOnlyTable));
			}

			// Token: 0x06006E46 RID: 28230 RVA: 0x0017D118 File Offset: 0x0017B318
			private ActionValue DeleteRows(FunctionValue selector, bool countOnlyTable)
			{
				if (!countOnlyTable)
				{
					throw ValueException.NewDataSourceError<Message0>(Strings.Value_UpdateNotSupported, new QueryTableValue(this), null);
				}
				List<IValueReference> list = new List<IValueReference>();
				list.Add(ActionModule.Action.Return.Invoke(TableValue.Empty));
				foreach (IValueReference valueReference in this.Table.SelectRows(selector))
				{
					Value asRecord = valueReference.Value.AsRecord;
					if (this.environment.UserOptions.GetBool("EnableCrossDatabaseFolding", false))
					{
						throw ValueException.NewDataSourceError<Message0>(Strings.Catalog_DropSchemaNoCrossDatabaseFolding, new QueryTableValue(this), null);
					}
					Alias alias = Alias.NewNativeAlias(asRecord["Schema"].AsString);
					ActionValue actionValue = DbStatementActionValue.New(this.environment, this, new DbStatementPlan(new SqlDropSchemaStatement(alias), DbAstCreator.CreateOptions(this.environment), this.environment.SqlSettings, countOnlyTable), "DropSchema", null);
					list.Add(actionValue);
				}
				list.Add(ActionModule.Action.Return.Invoke(new CountTableValue((long)(list.Count - 1))));
				return ActionValue.New(ListValue.New(list)).ClearCache(this.environment.Host);
			}

			// Token: 0x04003D07 RID: 15623
			private readonly DbEnvironment environment;
		}

		// Token: 0x02001065 RID: 4197
		private class WrappedOleDbClient : OleDbClient
		{
			// Token: 0x06006E4B RID: 28235 RVA: 0x0017D280 File Offset: 0x0017B480
			public WrappedOleDbClient(DbEnvironment environment, OleDbClient oleDbClient)
			{
				this.environment = environment;
				this.oleDbClient = oleDbClient;
			}

			// Token: 0x06006E4C RID: 28236 RVA: 0x0017D298 File Offset: 0x0017B498
			public override IPageReader ExecuteCommand(IList<Type> columnTypes, string text)
			{
				return this.environment.ConvertDbExceptions<IPageReader>(delegate
				{
					IHostTrace hostTrace = this.environment.Tracer.CreateTrace("OleDbClient/ExecuteCommand", TraceEventType.Information);
					IPageReader pageReader2;
					try
					{
						hostTrace.Add("CommandText", text, true);
						IPageReader pageReader = this.oleDbClient.ExecuteCommand(columnTypes, text);
						hostTrace.Suspend();
						pageReader = new TracingPageReader(pageReader, hostTrace, 1).AfterDispose(new Action(hostTrace.Dispose));
						pageReader = new DbEnvironment.WrappedPageReader(this.environment, pageReader);
						pageReader2 = pageReader;
					}
					catch (Exception ex)
					{
						this.environment.TraceException(hostTrace, ex);
						hostTrace.Dispose();
						throw;
					}
					return pageReader2;
				});
			}

			// Token: 0x06006E4D RID: 28237 RVA: 0x0017D2D7 File Offset: 0x0017B4D7
			public override void Dispose()
			{
				this.oleDbClient.Dispose();
			}

			// Token: 0x04003D0C RID: 15628
			private readonly DbEnvironment environment;

			// Token: 0x04003D0D RID: 15629
			private readonly OleDbClient oleDbClient;
		}

		// Token: 0x02001067 RID: 4199
		protected class WrappedPageReader : DelegatingPageReader
		{
			// Token: 0x06006E50 RID: 28240 RVA: 0x0017D3A0 File Offset: 0x0017B5A0
			public WrappedPageReader(DbEnvironment environment, IPageReader reader)
				: base(reader)
			{
				this.environment = environment;
			}

			// Token: 0x06006E51 RID: 28241 RVA: 0x0017D3B0 File Offset: 0x0017B5B0
			public override void Read(IPage page)
			{
				try
				{
					base.Read(page);
				}
				catch (DbException ex)
				{
					using (IHostTrace hostTrace = this.environment.Tracer.CreateTrace("PageReader/Read", TraceEventType.Information))
					{
						Exception ex2;
						if (this.environment.TryConvertException(ex, out ex2))
						{
							this.environment.TraceException(hostTrace, ex2);
							throw ex2;
						}
						this.environment.TraceException(hostTrace, ex);
						throw;
					}
				}
			}

			// Token: 0x04003D11 RID: 15633
			private readonly DbEnvironment environment;
		}

		// Token: 0x02001068 RID: 4200
		public static class Capabilities
		{
			// Token: 0x04003D12 RID: 15634
			public static readonly string[] StringFunctions = new string[] { "Text.End", "Text.Length", "Text.Lower", "Text.Middle", "Text.PositionOf", "Text.Replace", "Text.Start", "Text.TrimEnd", "Text.TrimStart", "Text.Upper" };

			// Token: 0x04003D13 RID: 15635
			public static readonly string[] NumericFunctions = new string[]
			{
				"Number.Abs", "Number.Acos", "Number.Asin", "Number.Atan", "Number.Atan2", "Number.Cos", "Number.Exp", "Number.Log", "Number.Log10", "Number.Mod",
				"Number.Power", "Number.Round", "Number.RoundDown", "Number.RoundUp", "Number.Sign", "Number.Sin", "Number.Sqrt", "Number.Tan"
			};

			// Token: 0x04003D14 RID: 15636
			public static readonly string[] DateFunctions = new string[]
			{
				"Date.AddDays", "Date.AddMonths", "Date.AddQuarters", "Date.AddWeeks", "Date.AddYears", "Date.Day", "Date.DayOfWeek", "Date.DayOfYear", "Date.EndOfDay", "Date.EndOfMonth",
				"Date.EndOfQuarter", "Date.EndOfWeek", "Date.EndOfYear", "Date.Month", "Date.QuarterOfYear", "Date.StartOfDay", "Date.StartOfMonth", "Date.StartOfQuarter", "Date.StartOfWeek", "Date.StartOfYear",
				"Date.WeekOfYear", "Date.Year", "Duration.Days", "Duration.Hours", "Duration.Minutes", "Duration.Seconds", "Duration.TotalDays", "Duration.TotalHours", "Duration.TotalMinutes", "Duration.TotalSeconds",
				"Time.EndOfHour", "Time.EndOfMinute", "Time.Hour", "Time.Minute", "Time.Second", "Time.StartOfHour", "Time.StartOfMinute"
			};

			// Token: 0x04003D15 RID: 15637
			public static readonly string[] TableFunctions = new string[] { "Table.FirstN", "Table.RowCount", "Table.Skip", "Table.Sort" };

			// Token: 0x04003D16 RID: 15638
			public static readonly string[] ListFunctions = new string[] { "List.Max", "List.Min", "List.Sum" };
		}
	}
}
