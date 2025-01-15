using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.Migrations.Design;
using System.Data.Entity.Migrations.History;
using System.Data.Entity.Migrations.Infrastructure;
using System.Data.Entity.Migrations.Sql;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.IO;
using System.Reflection;

namespace System.Data.Entity.Migrations
{
	// Token: 0x0200009E RID: 158
	public class DbMigrationsConfiguration
	{
		// Token: 0x06000E5D RID: 3677 RVA: 0x0001D00C File Offset: 0x0001B20C
		public DbMigrationsConfiguration()
			: this(new Lazy<IDbDependencyResolver>(() => DbConfiguration.DependencyResolver))
		{
			this.CodeGenerator = new CSharpMigrationCodeGenerator();
			this.ContextKey = base.GetType().ToString();
		}

		// Token: 0x06000E5E RID: 3678 RVA: 0x0001D05F File Offset: 0x0001B25F
		internal DbMigrationsConfiguration(Lazy<IDbDependencyResolver> resolver)
		{
			this._resolver = resolver;
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06000E5F RID: 3679 RVA: 0x0001D09A File Offset: 0x0001B29A
		// (set) Token: 0x06000E60 RID: 3680 RVA: 0x0001D0A2 File Offset: 0x0001B2A2
		public bool AutomaticMigrationsEnabled { get; set; }

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06000E61 RID: 3681 RVA: 0x0001D0AB File Offset: 0x0001B2AB
		// (set) Token: 0x06000E62 RID: 3682 RVA: 0x0001D0B3 File Offset: 0x0001B2B3
		public string ContextKey
		{
			get
			{
				return this._contextKey;
			}
			set
			{
				Check.NotEmpty(value, "value");
				this._contextKey = value;
			}
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06000E63 RID: 3683 RVA: 0x0001D0C8 File Offset: 0x0001B2C8
		// (set) Token: 0x06000E64 RID: 3684 RVA: 0x0001D0D0 File Offset: 0x0001B2D0
		public bool AutomaticMigrationDataLossAllowed { get; set; }

		// Token: 0x06000E65 RID: 3685 RVA: 0x0001D0D9 File Offset: 0x0001B2D9
		public void SetSqlGenerator(string providerInvariantName, MigrationSqlGenerator migrationSqlGenerator)
		{
			Check.NotEmpty(providerInvariantName, "providerInvariantName");
			Check.NotNull<MigrationSqlGenerator>(migrationSqlGenerator, "migrationSqlGenerator");
			this._sqlGenerators[providerInvariantName] = migrationSqlGenerator;
		}

		// Token: 0x06000E66 RID: 3686 RVA: 0x0001D100 File Offset: 0x0001B300
		public MigrationSqlGenerator GetSqlGenerator(string providerInvariantName)
		{
			Check.NotEmpty(providerInvariantName, "providerInvariantName");
			MigrationSqlGenerator migrationSqlGenerator;
			if (!this._sqlGenerators.TryGetValue(providerInvariantName, out migrationSqlGenerator))
			{
				Func<MigrationSqlGenerator> service = this._resolver.Value.GetService(providerInvariantName);
				if (service == null)
				{
					throw Error.NoSqlGeneratorForProvider(providerInvariantName);
				}
				migrationSqlGenerator = service();
			}
			return migrationSqlGenerator;
		}

		// Token: 0x06000E67 RID: 3687 RVA: 0x0001D14B File Offset: 0x0001B34B
		public void SetHistoryContextFactory(string providerInvariantName, Func<DbConnection, string, HistoryContext> factory)
		{
			Check.NotEmpty(providerInvariantName, "providerInvariantName");
			Check.NotNull<Func<DbConnection, string, HistoryContext>>(factory, "factory");
			this._historyContextFactories[providerInvariantName] = factory;
		}

		// Token: 0x06000E68 RID: 3688 RVA: 0x0001D174 File Offset: 0x0001B374
		public Func<DbConnection, string, HistoryContext> GetHistoryContextFactory(string providerInvariantName)
		{
			Check.NotEmpty(providerInvariantName, "providerInvariantName");
			Func<DbConnection, string, HistoryContext> func;
			if (!this._historyContextFactories.TryGetValue(providerInvariantName, out func))
			{
				return this._resolver.Value.GetService(providerInvariantName) ?? this._resolver.Value.GetService<Func<DbConnection, string, HistoryContext>>();
			}
			return func;
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06000E69 RID: 3689 RVA: 0x0001D1C4 File Offset: 0x0001B3C4
		// (set) Token: 0x06000E6A RID: 3690 RVA: 0x0001D1CC File Offset: 0x0001B3CC
		public Type ContextType
		{
			get
			{
				return this._contextType;
			}
			set
			{
				Check.NotNull<Type>(value, "value");
				if (!typeof(DbContext).IsAssignableFrom(value))
				{
					throw new ArgumentException(Strings.DbMigrationsConfiguration_ContextType(value.Name));
				}
				this._contextType = value;
				DbConfigurationManager.Instance.EnsureLoadedForContext(this._contextType);
			}
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06000E6B RID: 3691 RVA: 0x0001D21F File Offset: 0x0001B41F
		// (set) Token: 0x06000E6C RID: 3692 RVA: 0x0001D227 File Offset: 0x0001B427
		public string MigrationsNamespace { get; set; }

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06000E6D RID: 3693 RVA: 0x0001D230 File Offset: 0x0001B430
		// (set) Token: 0x06000E6E RID: 3694 RVA: 0x0001D238 File Offset: 0x0001B438
		public string MigrationsDirectory
		{
			get
			{
				return this._migrationsDirectory;
			}
			set
			{
				Check.NotEmpty(value, "value");
				if (Path.IsPathRooted(value))
				{
					throw new MigrationsException(Strings.DbMigrationsConfiguration_RootedPath(value));
				}
				this._migrationsDirectory = value;
			}
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06000E6F RID: 3695 RVA: 0x0001D261 File Offset: 0x0001B461
		// (set) Token: 0x06000E70 RID: 3696 RVA: 0x0001D269 File Offset: 0x0001B469
		public MigrationCodeGenerator CodeGenerator
		{
			get
			{
				return this._codeGenerator;
			}
			set
			{
				Check.NotNull<MigrationCodeGenerator>(value, "value");
				this._codeGenerator = value;
			}
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06000E71 RID: 3697 RVA: 0x0001D27E File Offset: 0x0001B47E
		// (set) Token: 0x06000E72 RID: 3698 RVA: 0x0001D286 File Offset: 0x0001B486
		public Assembly MigrationsAssembly
		{
			get
			{
				return this._migrationsAssembly;
			}
			set
			{
				Check.NotNull<Assembly>(value, "value");
				this._migrationsAssembly = value;
			}
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06000E73 RID: 3699 RVA: 0x0001D29B File Offset: 0x0001B49B
		// (set) Token: 0x06000E74 RID: 3700 RVA: 0x0001D2A3 File Offset: 0x0001B4A3
		public DbConnectionInfo TargetDatabase
		{
			get
			{
				return this._connectionInfo;
			}
			set
			{
				Check.NotNull<DbConnectionInfo>(value, "value");
				this._connectionInfo = value;
			}
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06000E75 RID: 3701 RVA: 0x0001D2B8 File Offset: 0x0001B4B8
		// (set) Token: 0x06000E76 RID: 3702 RVA: 0x0001D2C0 File Offset: 0x0001B4C0
		public int? CommandTimeout
		{
			get
			{
				return this._commandTimeout;
			}
			set
			{
				if (value != null)
				{
					int? num = value;
					int num2 = 0;
					if ((num.GetValueOrDefault() < num2) & (num != null))
					{
						throw new ArgumentException(Strings.ObjectContext_InvalidCommandTimeout);
					}
				}
				this._commandTimeout = value;
			}
		}

		// Token: 0x06000E77 RID: 3703 RVA: 0x0001D300 File Offset: 0x0001B500
		internal virtual void OnSeed(DbContext context)
		{
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06000E78 RID: 3704 RVA: 0x0001D302 File Offset: 0x0001B502
		// (set) Token: 0x06000E79 RID: 3705 RVA: 0x0001D30A File Offset: 0x0001B50A
		internal EdmModelDiffer ModelDiffer
		{
			get
			{
				return this._modelDiffer;
			}
			set
			{
				this._modelDiffer = value;
			}
		}

		// Token: 0x0400080B RID: 2059
		public const string DefaultMigrationsDirectory = "Migrations";

		// Token: 0x0400080C RID: 2060
		private readonly Dictionary<string, MigrationSqlGenerator> _sqlGenerators = new Dictionary<string, MigrationSqlGenerator>();

		// Token: 0x0400080D RID: 2061
		private readonly Dictionary<string, Func<DbConnection, string, HistoryContext>> _historyContextFactories = new Dictionary<string, Func<DbConnection, string, HistoryContext>>();

		// Token: 0x0400080E RID: 2062
		private MigrationCodeGenerator _codeGenerator;

		// Token: 0x0400080F RID: 2063
		private Type _contextType;

		// Token: 0x04000810 RID: 2064
		private Assembly _migrationsAssembly;

		// Token: 0x04000811 RID: 2065
		private EdmModelDiffer _modelDiffer = new EdmModelDiffer();

		// Token: 0x04000812 RID: 2066
		private DbConnectionInfo _connectionInfo;

		// Token: 0x04000813 RID: 2067
		private string _migrationsDirectory = "Migrations";

		// Token: 0x04000814 RID: 2068
		private readonly Lazy<IDbDependencyResolver> _resolver;

		// Token: 0x04000815 RID: 2069
		private string _contextKey;

		// Token: 0x04000816 RID: 2070
		private int? _commandTimeout;
	}
}
