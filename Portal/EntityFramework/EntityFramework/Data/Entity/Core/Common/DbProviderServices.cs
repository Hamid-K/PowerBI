using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.Resources;
using System.Data.Entity.Spatial;
using System.Data.Entity.Utilities;
using System.IO;
using System.Transactions;
using System.Xml;

namespace System.Data.Entity.Core.Common
{
	// Token: 0x020005EB RID: 1515
	public abstract class DbProviderServices : IDbDependencyResolver
	{
		// Token: 0x060049F6 RID: 18934 RVA: 0x00106537 File Offset: 0x00104737
		protected DbProviderServices()
			: this(() => DbConfiguration.DependencyResolver)
		{
		}

		// Token: 0x060049F7 RID: 18935 RVA: 0x0010655E File Offset: 0x0010475E
		internal DbProviderServices(Func<IDbDependencyResolver> resolver)
			: this(resolver, new Lazy<DbCommandTreeDispatcher>(() => DbInterception.Dispatch.CommandTree))
		{
		}

		// Token: 0x060049F8 RID: 18936 RVA: 0x0010658B File Offset: 0x0010478B
		internal DbProviderServices(Func<IDbDependencyResolver> resolver, Lazy<DbCommandTreeDispatcher> treeDispatcher)
		{
			Check.NotNull<Func<IDbDependencyResolver>>(resolver, "resolver");
			this._resolver = new Lazy<IDbDependencyResolver>(resolver);
			this._treeDispatcher = treeDispatcher;
		}

		// Token: 0x060049F9 RID: 18937 RVA: 0x001065BD File Offset: 0x001047BD
		public virtual void RegisterInfoMessageHandler(DbConnection connection, Action<string> handler)
		{
		}

		// Token: 0x060049FA RID: 18938 RVA: 0x001065BF File Offset: 0x001047BF
		public DbCommandDefinition CreateCommandDefinition(DbCommandTree commandTree)
		{
			Check.NotNull<DbCommandTree>(commandTree, "commandTree");
			return this.CreateCommandDefinition(commandTree, new DbInterceptionContext());
		}

		// Token: 0x060049FB RID: 18939 RVA: 0x001065DC File Offset: 0x001047DC
		internal DbCommandDefinition CreateCommandDefinition(DbCommandTree commandTree, DbInterceptionContext interceptionContext)
		{
			this.ValidateDataSpace(commandTree);
			StoreItemCollection storeItemCollection = (StoreItemCollection)commandTree.MetadataWorkspace.GetItemCollection(DataSpace.SSpace);
			commandTree = this._treeDispatcher.Value.Created(commandTree, interceptionContext);
			return this.CreateDbCommandDefinition(storeItemCollection.ProviderManifest, commandTree, interceptionContext);
		}

		// Token: 0x060049FC RID: 18940 RVA: 0x00106624 File Offset: 0x00104824
		internal virtual DbCommandDefinition CreateDbCommandDefinition(DbProviderManifest providerManifest, DbCommandTree commandTree, DbInterceptionContext interceptionContext)
		{
			return this.CreateDbCommandDefinition(providerManifest, commandTree);
		}

		// Token: 0x060049FD RID: 18941 RVA: 0x00106630 File Offset: 0x00104830
		public DbCommandDefinition CreateCommandDefinition(DbProviderManifest providerManifest, DbCommandTree commandTree)
		{
			Check.NotNull<DbProviderManifest>(providerManifest, "providerManifest");
			Check.NotNull<DbCommandTree>(commandTree, "commandTree");
			DbCommandDefinition dbCommandDefinition;
			try
			{
				dbCommandDefinition = this.CreateDbCommandDefinition(providerManifest, commandTree);
			}
			catch (ProviderIncompatibleException)
			{
				throw;
			}
			catch (Exception ex)
			{
				if (ex.IsCatchableExceptionType())
				{
					throw new ProviderIncompatibleException(Strings.ProviderDidNotCreateACommandDefinition, ex);
				}
				throw;
			}
			return dbCommandDefinition;
		}

		// Token: 0x060049FE RID: 18942
		protected abstract DbCommandDefinition CreateDbCommandDefinition(DbProviderManifest providerManifest, DbCommandTree commandTree);

		// Token: 0x060049FF RID: 18943 RVA: 0x00106698 File Offset: 0x00104898
		internal virtual void ValidateDataSpace(DbCommandTree commandTree)
		{
			if (commandTree.DataSpace != DataSpace.SSpace)
			{
				throw new ProviderIncompatibleException(Strings.ProviderRequiresStoreCommandTree);
			}
		}

		// Token: 0x06004A00 RID: 18944 RVA: 0x001066AE File Offset: 0x001048AE
		internal virtual DbCommand CreateCommand(DbCommandTree commandTree, DbInterceptionContext interceptionContext)
		{
			return this.CreateCommandDefinition(commandTree, interceptionContext).CreateCommand();
		}

		// Token: 0x06004A01 RID: 18945 RVA: 0x001066BD File Offset: 0x001048BD
		public virtual DbCommandDefinition CreateCommandDefinition(DbCommand prototype)
		{
			return new DbCommandDefinition(prototype, new Func<DbCommand, DbCommand>(this.CloneDbCommand));
		}

		// Token: 0x06004A02 RID: 18946 RVA: 0x001066D4 File Offset: 0x001048D4
		protected virtual DbCommand CloneDbCommand(DbCommand fromDbCommand)
		{
			Check.NotNull<DbCommand>(fromDbCommand, "fromDbCommand");
			ICloneable cloneable = fromDbCommand as ICloneable;
			if (cloneable == null)
			{
				throw new ProviderIncompatibleException(Strings.EntityClient_CannotCloneStoreProvider);
			}
			return (DbCommand)cloneable.Clone();
		}

		// Token: 0x06004A03 RID: 18947 RVA: 0x0010670D File Offset: 0x0010490D
		public virtual DbConnection CloneDbConnection(DbConnection connection)
		{
			return this.CloneDbConnection(connection, DbProviderServices.GetProviderFactory(connection));
		}

		// Token: 0x06004A04 RID: 18948 RVA: 0x0010671C File Offset: 0x0010491C
		public virtual DbConnection CloneDbConnection(DbConnection connection, DbProviderFactory factory)
		{
			return factory.CreateConnection();
		}

		// Token: 0x06004A05 RID: 18949 RVA: 0x00106724 File Offset: 0x00104924
		public string GetProviderManifestToken(DbConnection connection)
		{
			Check.NotNull<DbConnection>(connection, "connection");
			string text;
			try
			{
				string dbProviderManifestToken;
				using (new TransactionScope(TransactionScopeOption.Suppress))
				{
					dbProviderManifestToken = this.GetDbProviderManifestToken(connection);
				}
				if (dbProviderManifestToken == null)
				{
					throw new ProviderIncompatibleException(Strings.ProviderDidNotReturnAProviderManifestToken);
				}
				text = dbProviderManifestToken;
			}
			catch (ProviderIncompatibleException)
			{
				throw;
			}
			catch (Exception ex)
			{
				if (ex.IsCatchableExceptionType())
				{
					throw new ProviderIncompatibleException(Strings.ProviderDidNotReturnAProviderManifestToken, ex);
				}
				throw;
			}
			return text;
		}

		// Token: 0x06004A06 RID: 18950
		protected abstract string GetDbProviderManifestToken(DbConnection connection);

		// Token: 0x06004A07 RID: 18951 RVA: 0x001067B0 File Offset: 0x001049B0
		public DbProviderManifest GetProviderManifest(string manifestToken)
		{
			Check.NotNull<string>(manifestToken, "manifestToken");
			DbProviderManifest dbProviderManifest2;
			try
			{
				DbProviderManifest dbProviderManifest = this.GetDbProviderManifest(manifestToken);
				if (dbProviderManifest == null)
				{
					throw new ProviderIncompatibleException(Strings.ProviderDidNotReturnAProviderManifest);
				}
				dbProviderManifest2 = dbProviderManifest;
			}
			catch (ProviderIncompatibleException)
			{
				throw;
			}
			catch (Exception ex)
			{
				if (ex.IsCatchableExceptionType())
				{
					throw new ProviderIncompatibleException(Strings.ProviderDidNotReturnAProviderManifest, ex);
				}
				throw;
			}
			return dbProviderManifest2;
		}

		// Token: 0x06004A08 RID: 18952
		protected abstract DbProviderManifest GetDbProviderManifest(string manifestToken);

		// Token: 0x06004A09 RID: 18953 RVA: 0x00106818 File Offset: 0x00104A18
		public static IDbExecutionStrategy GetExecutionStrategy(DbConnection connection)
		{
			return DbProviderServices.GetExecutionStrategy(connection, DbProviderServices.GetProviderFactory(connection), null);
		}

		// Token: 0x06004A0A RID: 18954 RVA: 0x00106828 File Offset: 0x00104A28
		internal static IDbExecutionStrategy GetExecutionStrategy(DbConnection connection, MetadataWorkspace metadataWorkspace)
		{
			StoreItemCollection storeItemCollection = (StoreItemCollection)metadataWorkspace.GetItemCollection(DataSpace.SSpace);
			return DbProviderServices.GetExecutionStrategy(connection, storeItemCollection.ProviderFactory, null);
		}

		// Token: 0x06004A0B RID: 18955 RVA: 0x0010684F File Offset: 0x00104A4F
		protected static IDbExecutionStrategy GetExecutionStrategy(DbConnection connection, string providerInvariantName)
		{
			return DbProviderServices.GetExecutionStrategy(connection, DbProviderServices.GetProviderFactory(connection), providerInvariantName);
		}

		// Token: 0x06004A0C RID: 18956 RVA: 0x00106860 File Offset: 0x00104A60
		private static IDbExecutionStrategy GetExecutionStrategy(DbConnection connection, DbProviderFactory providerFactory, string providerInvariantName = null)
		{
			EntityConnection entityConnection = connection as EntityConnection;
			if (entityConnection != null)
			{
				connection = entityConnection.StoreConnection;
			}
			string dataSource = DbInterception.Dispatch.Connection.GetDataSource(connection, new DbInterceptionContext());
			ExecutionStrategyKey executionStrategyKey = new ExecutionStrategyKey(providerFactory.GetType().FullName, dataSource);
			return DbProviderServices._executionStrategyFactories.GetOrAdd(executionStrategyKey, (ExecutionStrategyKey k) => DbConfiguration.DependencyResolver.GetService(new ExecutionStrategyKey(providerInvariantName ?? DbConfiguration.DependencyResolver.GetService(providerFactory).Name, dataSource)))();
		}

		// Token: 0x06004A0D RID: 18957 RVA: 0x001068E8 File Offset: 0x00104AE8
		public DbSpatialDataReader GetSpatialDataReader(DbDataReader fromReader, string manifestToken)
		{
			DbSpatialDataReader dbSpatialDataReader;
			try
			{
				dbSpatialDataReader = this.GetDbSpatialDataReader(fromReader, manifestToken);
			}
			catch (ProviderIncompatibleException)
			{
				throw;
			}
			catch (Exception ex)
			{
				if (ex.IsCatchableExceptionType())
				{
					throw new ProviderIncompatibleException(Strings.ProviderDidNotReturnSpatialServices, ex);
				}
				throw;
			}
			return dbSpatialDataReader;
		}

		// Token: 0x06004A0E RID: 18958 RVA: 0x00106938 File Offset: 0x00104B38
		[Obsolete("Use GetSpatialServices(DbProviderInfo) or DbConfiguration to ensure the configured spatial services are used. See http://go.microsoft.com/fwlink/?LinkId=260882 for more information.")]
		public DbSpatialServices GetSpatialServices(string manifestToken)
		{
			DbSpatialServices dbSpatialServices;
			try
			{
				dbSpatialServices = this.DbGetSpatialServices(manifestToken);
			}
			catch (ProviderIncompatibleException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new ProviderIncompatibleException(Strings.ProviderDidNotReturnSpatialServices, ex);
			}
			return dbSpatialServices;
		}

		// Token: 0x06004A0F RID: 18959 RVA: 0x0010697C File Offset: 0x00104B7C
		internal static DbSpatialServices GetSpatialServices(IDbDependencyResolver resolver, EntityConnection connection)
		{
			StoreItemCollection storeItemCollection = (StoreItemCollection)connection.GetMetadataWorkspace().GetItemCollection(DataSpace.SSpace);
			DbProviderInfo dbProviderInfo = new DbProviderInfo(storeItemCollection.ProviderInvariantName, storeItemCollection.ProviderManifestToken);
			return DbProviderServices.GetSpatialServices(resolver, dbProviderInfo, () => DbProviderServices.GetProviderServices(connection.StoreConnection));
		}

		// Token: 0x06004A10 RID: 18960 RVA: 0x001069D2 File Offset: 0x00104BD2
		public DbSpatialServices GetSpatialServices(DbProviderInfo key)
		{
			return DbProviderServices.GetSpatialServices(this._resolver.Value, key, () => this);
		}

		// Token: 0x06004A11 RID: 18961 RVA: 0x001069F4 File Offset: 0x00104BF4
		private static DbSpatialServices GetSpatialServices(IDbDependencyResolver resolver, DbProviderInfo key, Func<DbProviderServices> providerServices)
		{
			DbSpatialServices orAdd = DbProviderServices._spatialServices.GetOrAdd(key, delegate(DbProviderInfo k)
			{
				DbSpatialServices dbSpatialServices;
				if ((dbSpatialServices = resolver.GetService(k)) == null)
				{
					dbSpatialServices = providerServices().GetSpatialServices(k.ProviderManifestToken) ?? resolver.GetService<DbSpatialServices>();
				}
				return dbSpatialServices;
			});
			if (orAdd == null)
			{
				throw new ProviderIncompatibleException(Strings.ProviderDidNotReturnSpatialServices);
			}
			return orAdd;
		}

		// Token: 0x06004A12 RID: 18962 RVA: 0x00106A3A File Offset: 0x00104C3A
		protected virtual DbSpatialDataReader GetDbSpatialDataReader(DbDataReader fromReader, string manifestToken)
		{
			Check.NotNull<DbDataReader>(fromReader, "fromReader");
			return null;
		}

		// Token: 0x06004A13 RID: 18963 RVA: 0x00106A49 File Offset: 0x00104C49
		[Obsolete("Return DbSpatialServices from the GetService method. See http://go.microsoft.com/fwlink/?LinkId=260882 for more information.")]
		protected virtual DbSpatialServices DbGetSpatialServices(string manifestToken)
		{
			return null;
		}

		// Token: 0x06004A14 RID: 18964 RVA: 0x00106A4C File Offset: 0x00104C4C
		public void SetParameterValue(DbParameter parameter, TypeUsage parameterType, object value)
		{
			Check.NotNull<DbParameter>(parameter, "parameter");
			Check.NotNull<TypeUsage>(parameterType, "parameterType");
			this.SetDbParameterValue(parameter, parameterType, value);
		}

		// Token: 0x06004A15 RID: 18965 RVA: 0x00106A6F File Offset: 0x00104C6F
		protected virtual void SetDbParameterValue(DbParameter parameter, TypeUsage parameterType, object value)
		{
			Check.NotNull<DbParameter>(parameter, "parameter");
			Check.NotNull<TypeUsage>(parameterType, "parameterType");
			parameter.Value = value;
		}

		// Token: 0x06004A16 RID: 18966 RVA: 0x00106A90 File Offset: 0x00104C90
		public static DbProviderServices GetProviderServices(DbConnection connection)
		{
			return DbProviderServices.GetProviderFactory(connection).GetProviderServices();
		}

		// Token: 0x06004A17 RID: 18967 RVA: 0x00106A9D File Offset: 0x00104C9D
		public static DbProviderFactory GetProviderFactory(DbConnection connection)
		{
			Check.NotNull<DbConnection>(connection, "connection");
			DbProviderFactory providerFactory = connection.GetProviderFactory();
			if (providerFactory == null)
			{
				throw new ProviderIncompatibleException(Strings.EntityClient_ReturnedNullOnProviderMethod("get_ProviderFactory", connection.GetType().ToString()));
			}
			return providerFactory;
		}

		// Token: 0x06004A18 RID: 18968 RVA: 0x00106ACF File Offset: 0x00104CCF
		public static XmlReader GetConceptualSchemaDefinition(string csdlName)
		{
			Check.NotEmpty(csdlName, "csdlName");
			return DbProviderServices.GetXmlResource("System.Data.Resources.DbProviderServices." + csdlName + ".csdl");
		}

		// Token: 0x06004A19 RID: 18969 RVA: 0x00106AF2 File Offset: 0x00104CF2
		internal static XmlReader GetXmlResource(string resourceName)
		{
			Stream manifestResourceStream = typeof(DbProviderServices).Assembly().GetManifestResourceStream(resourceName);
			if (manifestResourceStream == null)
			{
				throw Error.InvalidResourceName(resourceName);
			}
			return XmlReader.Create(manifestResourceStream);
		}

		// Token: 0x06004A1A RID: 18970 RVA: 0x00106B18 File Offset: 0x00104D18
		public string CreateDatabaseScript(string providerManifestToken, StoreItemCollection storeItemCollection)
		{
			Check.NotNull<string>(providerManifestToken, "providerManifestToken");
			Check.NotNull<StoreItemCollection>(storeItemCollection, "storeItemCollection");
			return this.DbCreateDatabaseScript(providerManifestToken, storeItemCollection);
		}

		// Token: 0x06004A1B RID: 18971 RVA: 0x00106B3A File Offset: 0x00104D3A
		protected virtual string DbCreateDatabaseScript(string providerManifestToken, StoreItemCollection storeItemCollection)
		{
			Check.NotNull<string>(providerManifestToken, "providerManifestToken");
			Check.NotNull<StoreItemCollection>(storeItemCollection, "storeItemCollection");
			throw new ProviderIncompatibleException(Strings.ProviderDoesNotSupportCreateDatabaseScript);
		}

		// Token: 0x06004A1C RID: 18972 RVA: 0x00106B5E File Offset: 0x00104D5E
		public void CreateDatabase(DbConnection connection, int? commandTimeout, StoreItemCollection storeItemCollection)
		{
			Check.NotNull<DbConnection>(connection, "connection");
			Check.NotNull<StoreItemCollection>(storeItemCollection, "storeItemCollection");
			this.DbCreateDatabase(connection, commandTimeout, storeItemCollection);
		}

		// Token: 0x06004A1D RID: 18973 RVA: 0x00106B81 File Offset: 0x00104D81
		protected virtual void DbCreateDatabase(DbConnection connection, int? commandTimeout, StoreItemCollection storeItemCollection)
		{
			Check.NotNull<DbConnection>(connection, "connection");
			Check.NotNull<StoreItemCollection>(storeItemCollection, "storeItemCollection");
			throw new ProviderIncompatibleException(Strings.ProviderDoesNotSupportCreateDatabase);
		}

		// Token: 0x06004A1E RID: 18974 RVA: 0x00106BA8 File Offset: 0x00104DA8
		public bool DatabaseExists(DbConnection connection, int? commandTimeout, StoreItemCollection storeItemCollection)
		{
			Check.NotNull<DbConnection>(connection, "connection");
			Check.NotNull<StoreItemCollection>(storeItemCollection, "storeItemCollection");
			bool flag;
			using (new TransactionScope(TransactionScopeOption.Suppress))
			{
				flag = this.DbDatabaseExists(connection, commandTimeout, storeItemCollection);
			}
			return flag;
		}

		// Token: 0x06004A1F RID: 18975 RVA: 0x00106BFC File Offset: 0x00104DFC
		public bool DatabaseExists(DbConnection connection, int? commandTimeout, Lazy<StoreItemCollection> storeItemCollection)
		{
			Check.NotNull<DbConnection>(connection, "connection");
			Check.NotNull<Lazy<StoreItemCollection>>(storeItemCollection, "storeItemCollection");
			bool flag;
			using (new TransactionScope(TransactionScopeOption.Suppress))
			{
				flag = this.DbDatabaseExists(connection, commandTimeout, storeItemCollection);
			}
			return flag;
		}

		// Token: 0x06004A20 RID: 18976 RVA: 0x00106C50 File Offset: 0x00104E50
		protected virtual bool DbDatabaseExists(DbConnection connection, int? commandTimeout, StoreItemCollection storeItemCollection)
		{
			Check.NotNull<DbConnection>(connection, "connection");
			Check.NotNull<StoreItemCollection>(storeItemCollection, "storeItemCollection");
			throw new ProviderIncompatibleException(Strings.ProviderDoesNotSupportDatabaseExists);
		}

		// Token: 0x06004A21 RID: 18977 RVA: 0x00106C74 File Offset: 0x00104E74
		protected virtual bool DbDatabaseExists(DbConnection connection, int? commandTimeout, Lazy<StoreItemCollection> storeItemCollection)
		{
			Check.NotNull<DbConnection>(connection, "connection");
			Check.NotNull<Lazy<StoreItemCollection>>(storeItemCollection, "storeItemCollection");
			return this.DbDatabaseExists(connection, commandTimeout, storeItemCollection.Value);
		}

		// Token: 0x06004A22 RID: 18978 RVA: 0x00106C9C File Offset: 0x00104E9C
		public void DeleteDatabase(DbConnection connection, int? commandTimeout, StoreItemCollection storeItemCollection)
		{
			Check.NotNull<DbConnection>(connection, "connection");
			Check.NotNull<StoreItemCollection>(storeItemCollection, "storeItemCollection");
			this.DbDeleteDatabase(connection, commandTimeout, storeItemCollection);
		}

		// Token: 0x06004A23 RID: 18979 RVA: 0x00106CBF File Offset: 0x00104EBF
		protected virtual void DbDeleteDatabase(DbConnection connection, int? commandTimeout, StoreItemCollection storeItemCollection)
		{
			Check.NotNull<DbConnection>(connection, "connection");
			Check.NotNull<StoreItemCollection>(storeItemCollection, "storeItemCollection");
			throw new ProviderIncompatibleException(Strings.ProviderDoesNotSupportDeleteDatabase);
		}

		// Token: 0x06004A24 RID: 18980 RVA: 0x00106CE4 File Offset: 0x00104EE4
		public static string ExpandDataDirectory(string path)
		{
			if (string.IsNullOrEmpty(path) || !path.StartsWith("|datadirectory|", StringComparison.OrdinalIgnoreCase))
			{
				return path;
			}
			object data = AppDomain.CurrentDomain.GetData("DataDirectory");
			string text = data as string;
			if (data != null && text == null)
			{
				throw new InvalidOperationException(Strings.ADP_InvalidDataDirectory);
			}
			if (text == string.Empty)
			{
				text = AppDomain.CurrentDomain.BaseDirectory;
			}
			if (text == null)
			{
				text = string.Empty;
			}
			path = path.Substring("|datadirectory|".Length);
			if (path.StartsWith("\\", StringComparison.Ordinal))
			{
				path = path.Substring(1);
			}
			path = (text.EndsWith("\\", StringComparison.Ordinal) ? text : (text + "\\")) + path;
			if (text.Contains(".."))
			{
				throw new ArgumentException(Strings.ExpandingDataDirectoryFailed);
			}
			return path;
		}

		// Token: 0x06004A25 RID: 18981 RVA: 0x00106DB8 File Offset: 0x00104FB8
		protected void AddDependencyResolver(IDbDependencyResolver resolver)
		{
			Check.NotNull<IDbDependencyResolver>(resolver, "resolver");
			this._resolvers.Add(resolver);
		}

		// Token: 0x06004A26 RID: 18982 RVA: 0x00106DD2 File Offset: 0x00104FD2
		public virtual object GetService(Type type, object key)
		{
			return this._resolvers.GetService(type, key);
		}

		// Token: 0x06004A27 RID: 18983 RVA: 0x00106DE1 File Offset: 0x00104FE1
		public virtual IEnumerable<object> GetServices(Type type, object key)
		{
			return this._resolvers.GetServices(type, key);
		}

		// Token: 0x04001A20 RID: 6688
		private readonly Lazy<IDbDependencyResolver> _resolver;

		// Token: 0x04001A21 RID: 6689
		private readonly Lazy<DbCommandTreeDispatcher> _treeDispatcher;

		// Token: 0x04001A22 RID: 6690
		private static readonly ConcurrentDictionary<DbProviderInfo, DbSpatialServices> _spatialServices = new ConcurrentDictionary<DbProviderInfo, DbSpatialServices>();

		// Token: 0x04001A23 RID: 6691
		private static readonly ConcurrentDictionary<ExecutionStrategyKey, Func<IDbExecutionStrategy>> _executionStrategyFactories = new ConcurrentDictionary<ExecutionStrategyKey, Func<IDbExecutionStrategy>>();

		// Token: 0x04001A24 RID: 6692
		private readonly ResolverChain _resolvers = new ResolverChain();
	}
}
