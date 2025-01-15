using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.Internal;
using System.Data.Entity.Migrations.Design;
using System.Data.Entity.Migrations.Edm;
using System.Data.Entity.Migrations.History;
using System.Data.Entity.Migrations.Infrastructure;
using System.Data.Entity.Migrations.Model;
using System.Data.Entity.Migrations.Sql;
using System.Data.Entity.Migrations.Utilities;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Resources;
using System.Xml.Linq;

namespace System.Data.Entity.Migrations
{
	// Token: 0x020000A0 RID: 160
	public class DbMigrator : MigratorBase
	{
		// Token: 0x06000E83 RID: 3715 RVA: 0x0001D3A8 File Offset: 0x0001B5A8
		internal DbMigrator(DbContext usersContext = null, DbProviderFactory providerFactory = null, MigrationAssembly migrationAssembly = null)
			: base(null)
		{
			this._usersContext = usersContext;
			this._providerFactory = providerFactory;
			this._migrationAssembly = migrationAssembly;
			this._usersContextInfo = new DbContextInfo(typeof(DbContext));
			this._configuration = new DbMigrationsConfiguration();
			this._calledByCreateDatabase = true;
		}

		// Token: 0x06000E84 RID: 3716 RVA: 0x0001D3F8 File Offset: 0x0001B5F8
		public DbMigrator(DbMigrationsConfiguration configuration)
			: this(configuration, null, DatabaseExistenceState.Unknown, false)
		{
			Check.NotNull<DbMigrationsConfiguration>(configuration, "configuration");
			Check.NotNull<Type>(configuration.ContextType, "configuration.ContextType");
		}

		// Token: 0x06000E85 RID: 3717 RVA: 0x0001D421 File Offset: 0x0001B621
		public DbMigrator(DbMigrationsConfiguration configuration, DbContext context)
			: this(configuration, context, DatabaseExistenceState.Unknown, false)
		{
		}

		// Token: 0x06000E86 RID: 3718 RVA: 0x0001D430 File Offset: 0x0001B630
		internal DbMigrator(DbMigrationsConfiguration configuration, DbContext usersContext, DatabaseExistenceState existenceState, bool calledByCreateDatabase)
			: base(null)
		{
			Check.NotNull<DbMigrationsConfiguration>(configuration, "configuration");
			Check.NotNull<Type>(configuration.ContextType, "configuration.ContextType");
			this._configuration = configuration;
			this._calledByCreateDatabase = calledByCreateDatabase;
			this._existenceState = existenceState;
			if (usersContext != null)
			{
				this._usersContextInfo = new DbContextInfo(usersContext, null);
			}
			else
			{
				this._usersContextInfo = ((configuration.TargetDatabase == null) ? new DbContextInfo(configuration.ContextType) : new DbContextInfo(configuration.ContextType, configuration.TargetDatabase));
				if (!this._usersContextInfo.IsConstructible)
				{
					throw Error.ContextNotConstructible(configuration.ContextType);
				}
			}
			this._modelDiffer = this._configuration.ModelDiffer;
			DbContext dbContext = usersContext ?? this._usersContextInfo.CreateInstance();
			this._usersContext = dbContext;
			try
			{
				this._migrationAssembly = new MigrationAssembly(this._configuration.MigrationsAssembly, this._configuration.MigrationsNamespace);
				this._currentModel = dbContext.GetModel();
				this._connection = dbContext.Database.Connection;
				this._providerFactory = DbProviderServices.GetProviderFactory(this._connection);
				this._defaultSchema = dbContext.InternalContext.DefaultSchema ?? "dbo";
				this._historyContextFactory = this._configuration.GetHistoryContextFactory(this._usersContextInfo.ConnectionProviderName);
				this._historyRepository = new HistoryRepository(dbContext.InternalContext, this._usersContextInfo.ConnectionString, this._providerFactory, this._configuration.ContextKey, this._configuration.CommandTimeout, this._historyContextFactory, new string[] { this._defaultSchema }.Concat(this.GetHistorySchemas()), this._usersContext, this._existenceState, (Exception e) => this.SqlGenerator.IsPermissionDeniedError(e));
				this._providerManifestToken = ((dbContext.InternalContext.ModelProviderInfo != null) ? dbContext.InternalContext.ModelProviderInfo.ProviderManifestToken : DbConfiguration.DependencyResolver.GetService<IManifestTokenResolver>().ResolveManifestToken(this._connection));
				DbModelBuilder modelBuilder = dbContext.InternalContext.CodeFirstModel.CachedModelBuilder;
				this._modificationCommandTreeGenerator = new Lazy<ModificationCommandTreeGenerator>(() => new ModificationCommandTreeGenerator(modelBuilder.BuildDynamicUpdateModel(new DbProviderInfo(this._usersContextInfo.ConnectionProviderName, this._providerManifestToken)), this.CreateConnection()));
				DbInterceptionContext dbInterceptionContext = new DbInterceptionContext();
				dbInterceptionContext = dbInterceptionContext.WithDbContext(this._usersContext);
				this._targetDatabase = Strings.LoggingTargetDatabaseFormat(DbInterception.Dispatch.Connection.GetDataSource(this._connection, dbInterceptionContext), DbInterception.Dispatch.Connection.GetDatabase(this._connection, dbInterceptionContext), this._usersContextInfo.ConnectionProviderName, (this._usersContextInfo.ConnectionStringOrigin == DbConnectionStringOrigin.DbContextInfo) ? Strings.LoggingExplicit : this._usersContextInfo.ConnectionStringOrigin.ToString());
				this._legacyContextKey = dbContext.InternalContext.DefaultContextKey;
				this._emptyModel = this.GetEmptyModel();
			}
			finally
			{
				if (usersContext == null)
				{
					this._usersContext = null;
					this._connection = null;
					dbContext.Dispose();
				}
			}
		}

		// Token: 0x06000E87 RID: 3719 RVA: 0x0001D738 File Offset: 0x0001B938
		private Lazy<XDocument> GetEmptyModel()
		{
			return new Lazy<XDocument>(() => new DbModelBuilder().Build(new DbProviderInfo(this._usersContextInfo.ConnectionProviderName, this._providerManifestToken)).GetModel());
		}

		// Token: 0x06000E88 RID: 3720 RVA: 0x0001D74C File Offset: 0x0001B94C
		private XDocument GetHistoryModel(string defaultSchema)
		{
			DbConnection dbConnection = null;
			XDocument model;
			try
			{
				dbConnection = this.CreateConnection();
				using (HistoryContext historyContext = this._historyContextFactory(dbConnection, defaultSchema))
				{
					model = historyContext.GetModel();
				}
			}
			finally
			{
				if (dbConnection != null)
				{
					DbInterception.Dispatch.Connection.Dispose(dbConnection, new DbInterceptionContext());
				}
			}
			return model;
		}

		// Token: 0x06000E89 RID: 3721 RVA: 0x0001D7BC File Offset: 0x0001B9BC
		private IEnumerable<string> GetHistorySchemas()
		{
			return from migrationId in this._migrationAssembly.MigrationIds
				let migration = this._migrationAssembly.GetMigration(migrationId)
				select DbMigrator.GetDefaultSchema(migration);
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06000E8A RID: 3722 RVA: 0x0001D809 File Offset: 0x0001BA09
		public override DbMigrationsConfiguration Configuration
		{
			get
			{
				return this._configuration;
			}
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06000E8B RID: 3723 RVA: 0x0001D811 File Offset: 0x0001BA11
		internal override string TargetDatabase
		{
			get
			{
				return this._targetDatabase;
			}
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06000E8C RID: 3724 RVA: 0x0001D81C File Offset: 0x0001BA1C
		private MigrationSqlGenerator SqlGenerator
		{
			get
			{
				MigrationSqlGenerator migrationSqlGenerator;
				if ((migrationSqlGenerator = this._sqlGenerator) == null)
				{
					migrationSqlGenerator = (this._sqlGenerator = this._configuration.GetSqlGenerator(this._usersContextInfo.ConnectionProviderName));
				}
				return migrationSqlGenerator;
			}
		}

		// Token: 0x06000E8D RID: 3725 RVA: 0x0001D852 File Offset: 0x0001BA52
		public override IEnumerable<string> GetLocalMigrations()
		{
			return this._migrationAssembly.MigrationIds;
		}

		// Token: 0x06000E8E RID: 3726 RVA: 0x0001D85F File Offset: 0x0001BA5F
		public override IEnumerable<string> GetDatabaseMigrations()
		{
			return this._historyRepository.GetMigrationsSince("0");
		}

		// Token: 0x06000E8F RID: 3727 RVA: 0x0001D871 File Offset: 0x0001BA71
		public override IEnumerable<string> GetPendingMigrations()
		{
			return this._historyRepository.GetPendingMigrations(this._migrationAssembly.MigrationIds);
		}

		// Token: 0x06000E90 RID: 3728 RVA: 0x0001D88C File Offset: 0x0001BA8C
		internal ScaffoldedMigration ScaffoldInitialCreate(string @namespace)
		{
			string text;
			string text2;
			XDocument lastModel = this._historyRepository.GetLastModel(out text, out text2, this._legacyContextKey);
			if (lastModel == null || !text.MigrationName().Equals(Strings.InitialCreate))
			{
				return null;
			}
			List<MigrationOperation> list = this._modelDiffer.Diff(this._emptyModel.Value, lastModel, this._modificationCommandTreeGenerator, this.SqlGenerator, null, null).ToList<MigrationOperation>();
			ScaffoldedMigration scaffoldedMigration = this._configuration.CodeGenerator.Generate(text, list, null, Convert.ToBase64String(new ModelCompressor().Compress(this._currentModel)), @namespace, Strings.InitialCreate);
			scaffoldedMigration.MigrationId = text;
			scaffoldedMigration.Directory = this._configuration.MigrationsDirectory;
			scaffoldedMigration.Resources.Add("DefaultSchema", this._defaultSchema);
			return scaffoldedMigration;
		}

		// Token: 0x06000E91 RID: 3729 RVA: 0x0001D950 File Offset: 0x0001BB50
		internal ScaffoldedMigration Scaffold(string migrationName, string @namespace, bool ignoreChanges)
		{
			DbMigrator.<>c__DisplayClass40_0 CS$<>8__locals1 = new DbMigrator.<>c__DisplayClass40_0();
			CS$<>8__locals1.<>4__this = this;
			string text = null;
			bool flag = false;
			List<string> list = this.GetPendingMigrations().ToList<string>();
			if (list.Any<string>())
			{
				string text2 = list.Last<string>();
				if (!text2.EqualsIgnoreCase(migrationName) && !text2.MigrationName().EqualsIgnoreCase(migrationName))
				{
					throw Error.MigrationsPendingException(list.Join(null, ", "));
				}
				flag = true;
				text = text2;
				migrationName = text2.MigrationName();
			}
			CS$<>8__locals1.sourceModel = null;
			this.CheckLegacyCompatibility(delegate
			{
				CS$<>8__locals1.sourceModel = CS$<>8__locals1.<>4__this._currentModel;
			});
			string text3 = null;
			string text4 = null;
			DbMigrator.<>c__DisplayClass40_0 CS$<>8__locals2 = CS$<>8__locals1;
			XDocument xdocument;
			if ((xdocument = CS$<>8__locals1.sourceModel) == null)
			{
				xdocument = this._historyRepository.GetLastModel(out text3, out text4, null) ?? this._emptyModel.Value;
			}
			CS$<>8__locals2.sourceModel = xdocument;
			IEnumerable<MigrationOperation> enumerable2;
			if (!ignoreChanges)
			{
				IEnumerable<MigrationOperation> enumerable = this._modelDiffer.Diff(CS$<>8__locals1.sourceModel, this._currentModel, this._modificationCommandTreeGenerator, this.SqlGenerator, text4, null).ToList<MigrationOperation>();
				enumerable2 = enumerable;
			}
			else
			{
				enumerable2 = Enumerable.Empty<MigrationOperation>();
			}
			IEnumerable<MigrationOperation> enumerable3 = enumerable2;
			if (!flag)
			{
				migrationName = this._migrationAssembly.UniquifyName(migrationName);
				text = MigrationAssembly.CreateMigrationId(migrationName);
			}
			ModelCompressor modelCompressor = new ModelCompressor();
			ScaffoldedMigration scaffoldedMigration = this._configuration.CodeGenerator.Generate(text, enumerable3, (CS$<>8__locals1.sourceModel == this._emptyModel.Value || CS$<>8__locals1.sourceModel == this._currentModel || !text3.IsAutomaticMigration()) ? null : Convert.ToBase64String(modelCompressor.Compress(CS$<>8__locals1.sourceModel)), Convert.ToBase64String(modelCompressor.Compress(this._currentModel)), @namespace, migrationName);
			scaffoldedMigration.MigrationId = text;
			scaffoldedMigration.Directory = this._configuration.MigrationsDirectory;
			scaffoldedMigration.IsRescaffold = flag;
			scaffoldedMigration.Resources.Add("DefaultSchema", this._defaultSchema);
			return scaffoldedMigration;
		}

		// Token: 0x06000E92 RID: 3730 RVA: 0x0001DB08 File Offset: 0x0001BD08
		private void CheckLegacyCompatibility(Action onCompatible)
		{
			if (!this._calledByCreateDatabase && !this._historyRepository.Exists(null))
			{
				DbContext dbContext = this._usersContext ?? this._usersContextInfo.CreateInstance();
				try
				{
					bool flag;
					try
					{
						flag = dbContext.Database.CompatibleWithModel(true);
					}
					catch
					{
						return;
					}
					if (!flag)
					{
						throw Error.MetadataOutOfDate();
					}
					onCompatible();
				}
				finally
				{
					if (this._usersContext == null)
					{
						dbContext.Dispose();
					}
				}
			}
		}

		// Token: 0x06000E93 RID: 3731 RVA: 0x0001DB90 File Offset: 0x0001BD90
		public override void Update(string targetMigration)
		{
			base.EnsureDatabaseExists(delegate
			{
				this.UpdateInternal(targetMigration);
			});
		}

		// Token: 0x06000E94 RID: 3732 RVA: 0x0001DBC4 File Offset: 0x0001BDC4
		private void UpdateInternal(string targetMigration)
		{
			IEnumerable<MigrationOperation> upgradeOperations = this._historyRepository.GetUpgradeOperations();
			if (upgradeOperations.Any<MigrationOperation>())
			{
				base.UpgradeHistory(upgradeOperations);
			}
			IEnumerable<string> enumerable = this.GetPendingMigrations();
			if (!enumerable.Any<string>())
			{
				this.CheckLegacyCompatibility(delegate
				{
					this.ExecuteOperations(MigrationAssembly.CreateBootstrapMigrationId(), new VersionedModel(this._currentModel, null), Enumerable.Empty<MigrationOperation>(), this._modelDiffer.Diff(this._emptyModel.Value, this.GetHistoryModel(this._defaultSchema), this._modificationCommandTreeGenerator, this.SqlGenerator, null, null), false, false);
				});
			}
			string targetMigrationId = targetMigration;
			if (!string.IsNullOrWhiteSpace(targetMigrationId))
			{
				if (!targetMigrationId.IsValidMigrationId())
				{
					if (targetMigrationId == Strings.AutomaticMigration)
					{
						throw Error.AutoNotValidTarget(Strings.AutomaticMigration);
					}
					targetMigrationId = this.GetMigrationId(targetMigration);
				}
				if (enumerable.Any((string m) => m.EqualsIgnoreCase(targetMigrationId)))
				{
					enumerable = enumerable.Where((string m) => string.CompareOrdinal(m.ToLowerInvariant(), targetMigrationId.ToLowerInvariant()) <= 0);
				}
				else
				{
					enumerable = this._historyRepository.GetMigrationsSince(targetMigrationId);
					if (enumerable.Any<string>())
					{
						base.Downgrade(enumerable.Concat(new string[] { targetMigrationId }));
						return;
					}
				}
			}
			base.Upgrade(enumerable, targetMigrationId, null);
		}

		// Token: 0x06000E95 RID: 3733 RVA: 0x0001DCD8 File Offset: 0x0001BED8
		internal override void UpgradeHistory(IEnumerable<MigrationOperation> upgradeOperations)
		{
			IEnumerable<MigrationStatement> enumerable = this.SqlGenerator.Generate(upgradeOperations, this._providerManifestToken);
			base.ExecuteStatements(enumerable);
		}

		// Token: 0x06000E96 RID: 3734 RVA: 0x0001DD00 File Offset: 0x0001BF00
		internal override string GetMigrationId(string migration)
		{
			if (migration.IsValidMigrationId())
			{
				return migration;
			}
			string text = this.GetPendingMigrations().SingleOrDefault((string m) => m.MigrationName().EqualsIgnoreCase(migration)) ?? this._historyRepository.GetMigrationId(migration);
			if (text == null)
			{
				throw Error.MigrationNotFound(migration);
			}
			return text;
		}

		// Token: 0x06000E97 RID: 3735 RVA: 0x0001DD6C File Offset: 0x0001BF6C
		internal override void Upgrade(IEnumerable<string> pendingMigrations, string targetMigrationId, string lastMigrationId)
		{
			DbMigration dbMigration = null;
			if (lastMigrationId != null)
			{
				dbMigration = this._migrationAssembly.GetMigration(lastMigrationId);
			}
			foreach (string text in pendingMigrations)
			{
				DbMigration migration = this._migrationAssembly.GetMigration(text);
				base.ApplyMigration(migration, dbMigration);
				dbMigration = migration;
				this._emptyMigrationNeeded = false;
				if (text.EqualsIgnoreCase(targetMigrationId))
				{
					break;
				}
			}
			if (string.IsNullOrWhiteSpace(targetMigrationId) && ((this._emptyMigrationNeeded && this._configuration.AutomaticMigrationsEnabled) || this.IsModelOutOfDate(this._currentModel, dbMigration)))
			{
				if (!this._configuration.AutomaticMigrationsEnabled)
				{
					throw Error.AutomaticDisabledException();
				}
				base.AutoMigrate(MigrationAssembly.CreateMigrationId(this._calledByCreateDatabase ? Strings.InitialCreate : Strings.AutomaticMigration), this._calledByCreateDatabase ? new VersionedModel(this._emptyModel.Value, null) : this.GetLastModel(dbMigration, null), new VersionedModel(this._currentModel, null), false);
			}
			if (!this._calledByCreateDatabase && !this.IsModelOutOfDate(this._currentModel, dbMigration))
			{
				base.SeedDatabase();
			}
		}

		// Token: 0x06000E98 RID: 3736 RVA: 0x0001DE98 File Offset: 0x0001C098
		internal override void SeedDatabase()
		{
			DbContext dbContext = this._usersContext ?? this._usersContextInfo.CreateInstance();
			if (this._usersContext != null)
			{
				dbContext.InternalContext.UseTempObjectContext();
			}
			try
			{
				this._configuration.OnSeed(dbContext);
				dbContext.SaveChanges();
			}
			finally
			{
				if (this._usersContext == null)
				{
					dbContext.Dispose();
				}
				else
				{
					dbContext.InternalContext.DisposeTempObjectContext();
				}
			}
		}

		// Token: 0x06000E99 RID: 3737 RVA: 0x0001DF10 File Offset: 0x0001C110
		internal virtual bool IsModelOutOfDate(XDocument model, DbMigration lastMigration)
		{
			VersionedModel lastModel = this.GetLastModel(lastMigration, null);
			return this._modelDiffer.Diff(lastModel.Model, model, null, null, lastModel.Version, null).Any<MigrationOperation>();
		}

		// Token: 0x06000E9A RID: 3738 RVA: 0x0001DF48 File Offset: 0x0001C148
		private VersionedModel GetLastModel(DbMigration lastMigration, string currentMigrationId = null)
		{
			if (lastMigration != null)
			{
				return lastMigration.GetTargetModel();
			}
			string text;
			string text2;
			XDocument lastModel = this._historyRepository.GetLastModel(out text, out text2, null);
			if (lastModel != null && (currentMigrationId == null || string.CompareOrdinal(text, currentMigrationId) < 0))
			{
				return new VersionedModel(lastModel, text2);
			}
			return new VersionedModel(this._emptyModel.Value, null);
		}

		// Token: 0x06000E9B RID: 3739 RVA: 0x0001DF9C File Offset: 0x0001C19C
		internal override void Downgrade(IEnumerable<string> pendingMigrations)
		{
			for (int i = 0; i < pendingMigrations.Count<string>() - 1; i++)
			{
				string text = pendingMigrations.ElementAt(i);
				DbMigration migration = this._migrationAssembly.GetMigration(text);
				string text2 = pendingMigrations.ElementAt(i + 1);
				string text3 = null;
				XDocument xdocument = ((text2 != "0") ? this._historyRepository.GetModel(text2, out text3) : this._emptyModel.Value);
				string text4;
				XDocument model = this._historyRepository.GetModel(text, out text4);
				if (migration == null)
				{
					base.AutoMigrate(text, new VersionedModel(model, null), new VersionedModel(xdocument, text3), true);
				}
				else
				{
					base.RevertMigration(text, migration, xdocument);
				}
			}
		}

		// Token: 0x06000E9C RID: 3740 RVA: 0x0001E048 File Offset: 0x0001C248
		internal override void RevertMigration(string migrationId, DbMigration migration, XDocument targetModel)
		{
			IEnumerable<MigrationOperation> enumerable = Enumerable.Empty<MigrationOperation>();
			string defaultSchema = DbMigrator.GetDefaultSchema(migration);
			XDocument historyModel = this.GetHistoryModel(defaultSchema);
			if (targetModel == this._emptyModel.Value && !this._historyRepository.IsShared())
			{
				enumerable = this._modelDiffer.Diff(historyModel, this._emptyModel.Value, null, null, null, null);
			}
			else
			{
				string lastDefaultSchema = this.GetLastDefaultSchema(migrationId);
				if (!string.Equals(lastDefaultSchema, defaultSchema, StringComparison.Ordinal))
				{
					XDocument historyModel2 = this.GetHistoryModel(lastDefaultSchema);
					enumerable = this._modelDiffer.Diff(historyModel, historyModel2, null, null, null, null);
				}
			}
			migration.Down();
			this.ExecuteOperations(migrationId, new VersionedModel(targetModel, null), migration.Operations, enumerable, true, false);
		}

		// Token: 0x06000E9D RID: 3741 RVA: 0x0001E0F0 File Offset: 0x0001C2F0
		internal override void ApplyMigration(DbMigration migration, DbMigration lastMigration)
		{
			IMigrationMetadata migrationMetadata = (IMigrationMetadata)migration;
			VersionedModel versionedModel = this.GetLastModel(lastMigration, migrationMetadata.Id);
			VersionedModel sourceModel = migration.GetSourceModel();
			VersionedModel targetModel = migration.GetTargetModel();
			if (sourceModel != null && this.IsModelOutOfDate(sourceModel.Model, lastMigration))
			{
				base.AutoMigrate(migrationMetadata.Id.ToAutomaticMigrationId(), versionedModel, sourceModel, false);
				versionedModel = sourceModel;
			}
			string defaultSchema = DbMigrator.GetDefaultSchema(migration);
			XDocument historyModel = this.GetHistoryModel(defaultSchema);
			IEnumerable<MigrationOperation> enumerable = Enumerable.Empty<MigrationOperation>();
			if (versionedModel.Model == this._emptyModel.Value && !base.HistoryExists())
			{
				enumerable = this._modelDiffer.Diff(this._emptyModel.Value, historyModel, null, null, null, null);
			}
			else
			{
				string lastDefaultSchema = this.GetLastDefaultSchema(migrationMetadata.Id);
				if (!string.Equals(lastDefaultSchema, defaultSchema, StringComparison.Ordinal))
				{
					XDocument historyModel2 = this.GetHistoryModel(lastDefaultSchema);
					enumerable = this._modelDiffer.Diff(historyModel2, historyModel, null, null, null, null);
				}
			}
			migration.Up();
			this.ExecuteOperations(migrationMetadata.Id, targetModel, migration.Operations, enumerable, false, false);
		}

		// Token: 0x06000E9E RID: 3742 RVA: 0x0001E1F4 File Offset: 0x0001C3F4
		private static string GetDefaultSchema(DbMigration migration)
		{
			string text;
			try
			{
				string @string = new ResourceManager(migration.GetType()).GetString("DefaultSchema");
				text = ((!string.IsNullOrWhiteSpace(@string)) ? @string : "dbo");
			}
			catch (MissingManifestResourceException)
			{
				text = "dbo";
			}
			return text;
		}

		// Token: 0x06000E9F RID: 3743 RVA: 0x0001E244 File Offset: 0x0001C444
		private string GetLastDefaultSchema(string migrationId)
		{
			string text = this._migrationAssembly.MigrationIds.LastOrDefault((string m) => string.CompareOrdinal(m, migrationId) < 0);
			if (text != null)
			{
				return DbMigrator.GetDefaultSchema(this._migrationAssembly.GetMigration(text));
			}
			return "dbo";
		}

		// Token: 0x06000EA0 RID: 3744 RVA: 0x0001E295 File Offset: 0x0001C495
		internal override bool HistoryExists()
		{
			return this._historyRepository.Exists(null);
		}

		// Token: 0x06000EA1 RID: 3745 RVA: 0x0001E2A4 File Offset: 0x0001C4A4
		internal override void AutoMigrate(string migrationId, VersionedModel sourceModel, VersionedModel targetModel, bool downgrading)
		{
			IEnumerable<MigrationOperation> enumerable = Enumerable.Empty<MigrationOperation>();
			if (!this._historyRepository.IsShared())
			{
				if (targetModel.Model == this._emptyModel.Value)
				{
					enumerable = this._modelDiffer.Diff(this.GetHistoryModel("dbo"), this._emptyModel.Value, null, null, null, null);
				}
				else if (sourceModel.Model == this._emptyModel.Value)
				{
					enumerable = this._modelDiffer.Diff(this._emptyModel.Value, this._calledByCreateDatabase ? this.GetHistoryModel(this._defaultSchema) : this.GetHistoryModel("dbo"), null, null, null, null);
				}
			}
			List<MigrationOperation> list = this._modelDiffer.Diff(sourceModel.Model, targetModel.Model, (targetModel.Model == this._currentModel) ? this._modificationCommandTreeGenerator : null, this.SqlGenerator, sourceModel.Version, targetModel.Version).ToList<MigrationOperation>();
			if (!this._calledByCreateDatabase && targetModel.Model == this._currentModel && !string.Equals(this.GetLastDefaultSchema(migrationId), this._defaultSchema, StringComparison.Ordinal))
			{
				throw Error.UnableToMoveHistoryTableWithAuto();
			}
			if (!this._configuration.AutomaticMigrationDataLossAllowed)
			{
				if (list.Any((MigrationOperation o) => o.IsDestructiveChange))
				{
					throw Error.AutomaticDataLoss();
				}
			}
			if (targetModel.Model != this._currentModel)
			{
				if (list.Any((MigrationOperation o) => o is ProcedureOperation))
				{
					throw Error.AutomaticStaleFunctions(migrationId);
				}
			}
			this.ExecuteOperations(migrationId, targetModel, list, enumerable, downgrading, true);
		}

		// Token: 0x06000EA2 RID: 3746 RVA: 0x0001E44C File Offset: 0x0001C64C
		private void ExecuteOperations(string migrationId, VersionedModel targetModel, IEnumerable<MigrationOperation> operations, IEnumerable<MigrationOperation> systemOperations, bool downgrading, bool auto = false)
		{
			this.FillInForeignKeyOperations(operations, targetModel.Model);
			List<AddForeignKeyOperation> list = (from ct in operations.OfType<CreateTableOperation>()
				from afk in operations.OfType<AddForeignKeyOperation>()
				where ct.Name.EqualsIgnoreCase(afk.DependentTable)
				select afk).ToList<AddForeignKeyOperation>();
			List<MigrationOperation> list2 = operations.Except(list).Concat(list).Concat(systemOperations)
				.ToList<MigrationOperation>();
			CreateTableOperation createTableOperation = systemOperations.OfType<CreateTableOperation>().FirstOrDefault<CreateTableOperation>();
			if (createTableOperation != null)
			{
				this._historyRepository.CurrentSchema = DatabaseName.Parse(createTableOperation.Name).Schema;
			}
			MoveTableOperation moveTableOperation = systemOperations.OfType<MoveTableOperation>().FirstOrDefault<MoveTableOperation>();
			if (moveTableOperation != null)
			{
				this._historyRepository.CurrentSchema = moveTableOperation.NewSchema;
				moveTableOperation.ContextKey = this._configuration.ContextKey;
				moveTableOperation.IsSystem = true;
			}
			if (!downgrading)
			{
				list2.Add(this._historyRepository.CreateInsertOperation(migrationId, targetModel));
			}
			else if (!systemOperations.Any((MigrationOperation o) => o is DropTableOperation))
			{
				list2.Add(this._historyRepository.CreateDeleteOperation(migrationId));
			}
			IEnumerable<MigrationStatement> enumerable = base.GenerateStatements(list2, migrationId);
			if (auto)
			{
				enumerable = enumerable.Distinct((MigrationStatement m1, MigrationStatement m2) => string.Equals(m1.Sql, m2.Sql, StringComparison.Ordinal));
			}
			base.ExecuteStatements(enumerable);
			this._historyRepository.ResetExists();
		}

		// Token: 0x06000EA3 RID: 3747 RVA: 0x0001E625 File Offset: 0x0001C825
		internal override IEnumerable<DbQueryCommandTree> CreateDiscoveryQueryTrees()
		{
			return this._historyRepository.CreateDiscoveryQueryTrees();
		}

		// Token: 0x06000EA4 RID: 3748 RVA: 0x0001E632 File Offset: 0x0001C832
		internal override IEnumerable<MigrationStatement> GenerateStatements(IList<MigrationOperation> operations, string migrationId)
		{
			return this.SqlGenerator.Generate(operations, this._providerManifestToken);
		}

		// Token: 0x06000EA5 RID: 3749 RVA: 0x0001E646 File Offset: 0x0001C846
		internal override void ExecuteStatements(IEnumerable<MigrationStatement> migrationStatements)
		{
			this.ExecuteStatements(migrationStatements, null);
		}

		// Token: 0x06000EA6 RID: 3750 RVA: 0x0001E650 File Offset: 0x0001C850
		internal void ExecuteStatements(IEnumerable<MigrationStatement> migrationStatements, DbTransaction existingTransaction)
		{
			DbConnection connection = null;
			try
			{
				if (existingTransaction != null)
				{
					DbInterceptionContext dbInterceptionContext = new DbInterceptionContext();
					dbInterceptionContext = dbInterceptionContext.WithDbContext(this._usersContext);
					this.ExecuteStatementsWithinTransaction(migrationStatements, existingTransaction, dbInterceptionContext);
				}
				else
				{
					connection = this.CreateConnection();
					DbProviderServices.GetExecutionStrategy(connection).Execute(delegate
					{
						this.ExecuteStatementsInternal(migrationStatements, connection);
					});
				}
			}
			finally
			{
				if (connection != null)
				{
					DbInterception.Dispatch.Connection.Dispose(connection, new DbInterceptionContext());
				}
			}
		}

		// Token: 0x06000EA7 RID: 3751 RVA: 0x0001E6FC File Offset: 0x0001C8FC
		private void ExecuteStatementsInternal(IEnumerable<MigrationStatement> migrationStatements, DbConnection connection)
		{
			DbContext dbContext = this._usersContext ?? this._usersContextInfo.CreateInstance();
			DbInterceptionContext dbInterceptionContext = new DbInterceptionContext();
			dbInterceptionContext = dbInterceptionContext.WithDbContext(dbContext);
			TransactionHandler transactionHandler = null;
			try
			{
				if (DbInterception.Dispatch.Connection.GetState(connection, dbInterceptionContext) == ConnectionState.Broken)
				{
					DbInterception.Dispatch.Connection.Close(connection, dbInterceptionContext);
				}
				if (DbInterception.Dispatch.Connection.GetState(connection, dbInterceptionContext) == ConnectionState.Closed)
				{
					DbInterception.Dispatch.Connection.Open(connection, dbInterceptionContext);
				}
				if (!(dbContext is TransactionContext))
				{
					string name = DbConfiguration.DependencyResolver.GetService(DbProviderServices.GetProviderFactory(connection)).Name;
					string dataSource = DbInterception.Dispatch.Connection.GetDataSource(connection, dbInterceptionContext);
					Func<TransactionHandler> service = DbConfiguration.DependencyResolver.GetService(new ExecutionStrategyKey(name, dataSource));
					if (service != null)
					{
						transactionHandler = service();
						transactionHandler.Initialize(dbContext, connection);
					}
				}
				this.ExecuteStatementsInternal(migrationStatements, connection, dbInterceptionContext);
				this._committedStatements = true;
			}
			finally
			{
				if (transactionHandler != null)
				{
					transactionHandler.Dispose();
				}
				if (this._usersContext == null)
				{
					dbContext.Dispose();
				}
			}
		}

		// Token: 0x06000EA8 RID: 3752 RVA: 0x0001E80C File Offset: 0x0001CA0C
		private void ExecuteStatementsInternal(IEnumerable<MigrationStatement> migrationStatements, DbConnection connection, DbTransaction transaction, DbInterceptionContext interceptionContext)
		{
			foreach (MigrationStatement migrationStatement in migrationStatements)
			{
				base.ExecuteSql(migrationStatement, connection, transaction, interceptionContext);
			}
		}

		// Token: 0x06000EA9 RID: 3753 RVA: 0x0001E858 File Offset: 0x0001CA58
		private void ExecuteStatementsInternal(IEnumerable<MigrationStatement> migrationStatements, DbConnection connection, DbInterceptionContext interceptionContext)
		{
			List<MigrationStatement> list = new List<MigrationStatement>();
			foreach (MigrationStatement migrationStatement in migrationStatements.Where((MigrationStatement s) => !string.IsNullOrEmpty(s.Sql)))
			{
				if (!migrationStatement.SuppressTransaction)
				{
					list.Add(migrationStatement);
				}
				else
				{
					if (list.Any<MigrationStatement>())
					{
						this.ExecuteStatementsWithinNewTransaction(list, connection, interceptionContext);
						list.Clear();
					}
					base.ExecuteSql(migrationStatement, connection, null, interceptionContext);
				}
			}
			if (list.Any<MigrationStatement>())
			{
				this.ExecuteStatementsWithinNewTransaction(list, connection, interceptionContext);
			}
		}

		// Token: 0x06000EAA RID: 3754 RVA: 0x0001E908 File Offset: 0x0001CB08
		private void ExecuteStatementsWithinTransaction(IEnumerable<MigrationStatement> migrationStatements, DbTransaction transaction, DbInterceptionContext interceptionContext)
		{
			DbConnection connection = DbInterception.Dispatch.Transaction.GetConnection(transaction, interceptionContext);
			this.ExecuteStatementsInternal(migrationStatements, connection, transaction, interceptionContext);
		}

		// Token: 0x06000EAB RID: 3755 RVA: 0x0001E934 File Offset: 0x0001CB34
		private void ExecuteStatementsWithinNewTransaction(IEnumerable<MigrationStatement> migrationStatements, DbConnection connection, DbInterceptionContext interceptionContext)
		{
			BeginTransactionInterceptionContext beginTransactionInterceptionContext = new BeginTransactionInterceptionContext(interceptionContext).WithIsolationLevel(IsolationLevel.Serializable);
			DbTransaction dbTransaction = null;
			try
			{
				dbTransaction = DbInterception.Dispatch.Connection.BeginTransaction(connection, beginTransactionInterceptionContext);
				this.ExecuteStatementsWithinTransaction(migrationStatements, dbTransaction, interceptionContext);
				DbInterception.Dispatch.Transaction.Commit(dbTransaction, interceptionContext);
			}
			finally
			{
				if (dbTransaction != null)
				{
					DbInterception.Dispatch.Transaction.Dispose(dbTransaction, interceptionContext);
				}
			}
		}

		// Token: 0x06000EAC RID: 3756 RVA: 0x0001E9A8 File Offset: 0x0001CBA8
		internal override void ExecuteSql(MigrationStatement migrationStatement, DbConnection connection, DbTransaction transaction, DbInterceptionContext interceptionContext)
		{
			if (string.IsNullOrWhiteSpace(migrationStatement.Sql))
			{
				return;
			}
			DbCommand dbCommand = connection.CreateCommand();
			using (InterceptableDbCommand interceptableDbCommand = this.ConfigureCommand(dbCommand, migrationStatement.Sql, interceptionContext))
			{
				if (transaction != null)
				{
					interceptableDbCommand.Transaction = transaction;
				}
				interceptableDbCommand.ExecuteNonQuery();
			}
		}

		// Token: 0x06000EAD RID: 3757 RVA: 0x0001EA08 File Offset: 0x0001CC08
		private InterceptableDbCommand ConfigureCommand(DbCommand command, string commandText, DbInterceptionContext interceptionContext)
		{
			command.CommandText = commandText;
			if (this._configuration.CommandTimeout != null)
			{
				command.CommandTimeout = this._configuration.CommandTimeout.Value;
			}
			return new InterceptableDbCommand(command, interceptionContext, null);
		}

		// Token: 0x06000EAE RID: 3758 RVA: 0x0001EA54 File Offset: 0x0001CC54
		private void FillInForeignKeyOperations(IEnumerable<MigrationOperation> operations, XDocument targetModel)
		{
			using (IEnumerator<AddForeignKeyOperation> enumerator = (from fk in operations.OfType<AddForeignKeyOperation>()
				where fk.PrincipalTable != null && !fk.PrincipalColumns.Any<string>()
				select fk).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					DbMigrator.<>c__DisplayClass69_0 CS$<>8__locals1 = new DbMigrator.<>c__DisplayClass69_0();
					CS$<>8__locals1.<>4__this = this;
					CS$<>8__locals1.foreignKeyOperation = enumerator.Current;
					string principalTable = this.GetStandardizedTableName(CS$<>8__locals1.foreignKeyOperation.PrincipalTable);
					string entitySetName = (from es in targetModel.Descendants(EdmXNames.Ssdl.EntitySetNames)
						where new DatabaseName(es.TableAttribute(), es.SchemaAttribute()).ToString().EqualsIgnoreCase(principalTable)
						select es.NameAttribute()).SingleOrDefault<string>();
					if (entitySetName != null)
					{
						targetModel.Descendants(EdmXNames.Ssdl.EntityTypeNames).Single((XElement et) => et.NameAttribute().EqualsIgnoreCase(entitySetName)).Descendants(EdmXNames.Ssdl.PropertyRefNames)
							.Each(delegate(XElement pr)
							{
								CS$<>8__locals1.foreignKeyOperation.PrincipalColumns.Add(pr.NameAttribute());
							});
					}
					else
					{
						CreateTableOperation createTableOperation = operations.OfType<CreateTableOperation>().SingleOrDefault((CreateTableOperation ct) => CS$<>8__locals1.<>4__this.GetStandardizedTableName(ct.Name).EqualsIgnoreCase(principalTable));
						if (createTableOperation == null || createTableOperation.PrimaryKey == null)
						{
							throw Error.PartialFkOperation(CS$<>8__locals1.foreignKeyOperation.DependentTable, CS$<>8__locals1.foreignKeyOperation.DependentColumns.Join(null, ", "));
						}
						createTableOperation.PrimaryKey.Columns.Each(delegate(string c)
						{
							CS$<>8__locals1.foreignKeyOperation.PrincipalColumns.Add(c);
						});
					}
				}
			}
		}

		// Token: 0x06000EAF RID: 3759 RVA: 0x0001EC14 File Offset: 0x0001CE14
		private string GetStandardizedTableName(string tableName)
		{
			if (!string.IsNullOrWhiteSpace(DatabaseName.Parse(tableName).Schema))
			{
				return tableName;
			}
			return new DatabaseName(tableName, this._defaultSchema).ToString();
		}

		// Token: 0x06000EB0 RID: 3760 RVA: 0x0001EC3C File Offset: 0x0001CE3C
		internal override void EnsureDatabaseExists(Action mustSucceedToKeepDatabase)
		{
			bool flag = false;
			global::System.Data.Entity.Migrations.Utilities.DatabaseCreator databaseCreator = new global::System.Data.Entity.Migrations.Utilities.DatabaseCreator(this._configuration.CommandTimeout);
			DbConnection dbConnection = null;
			try
			{
				dbConnection = this.CreateConnection();
				if (this._existenceState == DatabaseExistenceState.DoesNotExist || (this._existenceState == DatabaseExistenceState.Unknown && !databaseCreator.Exists(dbConnection)))
				{
					databaseCreator.Create(dbConnection);
					flag = true;
				}
			}
			finally
			{
				if (dbConnection != null)
				{
					DbInterception.Dispatch.Connection.Dispose(dbConnection, new DbInterceptionContext());
				}
			}
			this._emptyMigrationNeeded = flag;
			try
			{
				this._committedStatements = false;
				mustSucceedToKeepDatabase();
			}
			catch
			{
				if (flag && !this._committedStatements)
				{
					DbConnection dbConnection2 = null;
					try
					{
						dbConnection2 = this.CreateConnection();
						databaseCreator.Delete(dbConnection2);
					}
					catch
					{
					}
					finally
					{
						if (dbConnection2 != null)
						{
							DbInterception.Dispatch.Connection.Dispose(dbConnection2, new DbInterceptionContext());
						}
					}
				}
				throw;
			}
		}

		// Token: 0x06000EB1 RID: 3761 RVA: 0x0001ED2C File Offset: 0x0001CF2C
		private DbConnection CreateConnection()
		{
			DbConnection dbConnection = ((this._connection == null) ? this._providerFactory.CreateConnection() : DbProviderServices.GetProviderServices(this._connection).CloneDbConnection(this._connection, this._providerFactory));
			DbConnectionPropertyInterceptionContext<string> dbConnectionPropertyInterceptionContext = new DbConnectionPropertyInterceptionContext<string>().WithValue(this._usersContextInfo.ConnectionString);
			if (this._usersContext != null)
			{
				dbConnectionPropertyInterceptionContext = dbConnectionPropertyInterceptionContext.WithDbContext(this._usersContext);
			}
			DbInterception.Dispatch.Connection.SetConnectionString(dbConnection, dbConnectionPropertyInterceptionContext);
			return dbConnection;
		}

		// Token: 0x0400081A RID: 2074
		public const string InitialDatabase = "0";

		// Token: 0x0400081B RID: 2075
		private const string DefaultSchemaResourceKey = "DefaultSchema";

		// Token: 0x0400081C RID: 2076
		private readonly Lazy<XDocument> _emptyModel;

		// Token: 0x0400081D RID: 2077
		private readonly DbMigrationsConfiguration _configuration;

		// Token: 0x0400081E RID: 2078
		private readonly XDocument _currentModel;

		// Token: 0x0400081F RID: 2079
		private readonly DbProviderFactory _providerFactory;

		// Token: 0x04000820 RID: 2080
		private readonly HistoryRepository _historyRepository;

		// Token: 0x04000821 RID: 2081
		private readonly MigrationAssembly _migrationAssembly;

		// Token: 0x04000822 RID: 2082
		private readonly DbContextInfo _usersContextInfo;

		// Token: 0x04000823 RID: 2083
		private readonly EdmModelDiffer _modelDiffer;

		// Token: 0x04000824 RID: 2084
		private readonly Lazy<ModificationCommandTreeGenerator> _modificationCommandTreeGenerator;

		// Token: 0x04000825 RID: 2085
		private readonly DbContext _usersContext;

		// Token: 0x04000826 RID: 2086
		private readonly Func<DbConnection, string, HistoryContext> _historyContextFactory;

		// Token: 0x04000827 RID: 2087
		private readonly DbConnection _connection;

		// Token: 0x04000828 RID: 2088
		private readonly bool _calledByCreateDatabase;

		// Token: 0x04000829 RID: 2089
		private readonly DatabaseExistenceState _existenceState;

		// Token: 0x0400082A RID: 2090
		private readonly string _providerManifestToken;

		// Token: 0x0400082B RID: 2091
		private readonly string _targetDatabase;

		// Token: 0x0400082C RID: 2092
		private readonly string _legacyContextKey;

		// Token: 0x0400082D RID: 2093
		private readonly string _defaultSchema;

		// Token: 0x0400082E RID: 2094
		private MigrationSqlGenerator _sqlGenerator;

		// Token: 0x0400082F RID: 2095
		private bool _emptyMigrationNeeded;

		// Token: 0x04000830 RID: 2096
		private bool _committedStatements;
	}
}
