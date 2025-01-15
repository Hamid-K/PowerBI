using System;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Migrations.Design
{
	// Token: 0x020000E4 RID: 228
	public class MigrationScaffolder
	{
		// Token: 0x06001148 RID: 4424 RVA: 0x0002A9C5 File Offset: 0x00028BC5
		public MigrationScaffolder(DbMigrationsConfiguration migrationsConfiguration)
		{
			Check.NotNull<DbMigrationsConfiguration>(migrationsConfiguration, "migrationsConfiguration");
			this._migrator = new DbMigrator(migrationsConfiguration);
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06001149 RID: 4425 RVA: 0x0002A9E5 File Offset: 0x00028BE5
		// (set) Token: 0x0600114A RID: 4426 RVA: 0x0002AA06 File Offset: 0x00028C06
		public string Namespace
		{
			get
			{
				if (!this._namespaceSpecified)
				{
					return this._migrator.Configuration.MigrationsNamespace;
				}
				return this._namespace;
			}
			set
			{
				this._namespaceSpecified = this._migrator.Configuration.MigrationsNamespace != value;
				this._namespace = value;
			}
		}

		// Token: 0x0600114B RID: 4427 RVA: 0x0002AA2B File Offset: 0x00028C2B
		public virtual ScaffoldedMigration Scaffold(string migrationName)
		{
			Check.NotEmpty(migrationName, "migrationName");
			return this._migrator.Scaffold(migrationName, this.Namespace, false);
		}

		// Token: 0x0600114C RID: 4428 RVA: 0x0002AA4C File Offset: 0x00028C4C
		public virtual ScaffoldedMigration Scaffold(string migrationName, bool ignoreChanges)
		{
			Check.NotEmpty(migrationName, "migrationName");
			return this._migrator.Scaffold(migrationName, this.Namespace, ignoreChanges);
		}

		// Token: 0x0600114D RID: 4429 RVA: 0x0002AA6D File Offset: 0x00028C6D
		public virtual ScaffoldedMigration ScaffoldInitialCreate()
		{
			return this._migrator.ScaffoldInitialCreate(this.Namespace);
		}

		// Token: 0x040008D6 RID: 2262
		private readonly DbMigrator _migrator;

		// Token: 0x040008D7 RID: 2263
		private string _namespace;

		// Token: 0x040008D8 RID: 2264
		private bool _namespaceSpecified;
	}
}
