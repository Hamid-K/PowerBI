using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Utilities;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.Migrations.Infrastructure
{
	// Token: 0x020000D2 RID: 210
	internal class MigrationAssembly
	{
		// Token: 0x06001067 RID: 4199 RVA: 0x000254AE File Offset: 0x000236AE
		public static string CreateMigrationId(string migrationName)
		{
			return UtcNowGenerator.UtcNowAsMigrationIdTimestamp() + "_" + migrationName;
		}

		// Token: 0x06001068 RID: 4200 RVA: 0x000254C0 File Offset: 0x000236C0
		public static string CreateBootstrapMigrationId()
		{
			return new string('0', 15) + "_" + Strings.BootstrapMigration;
		}

		// Token: 0x06001069 RID: 4201 RVA: 0x000254DA File Offset: 0x000236DA
		protected MigrationAssembly()
		{
		}

		// Token: 0x0600106A RID: 4202 RVA: 0x000254E4 File Offset: 0x000236E4
		public MigrationAssembly(Assembly migrationsAssembly, string migrationsNamespace)
		{
			this._migrations = (from t in migrationsAssembly.GetAccessibleTypes()
				where t.IsSubclassOf(typeof(DbMigration)) && typeof(IMigrationMetadata).IsAssignableFrom(t) && t.GetPublicConstructor(new Type[0]) != null && !t.IsAbstract() && !t.IsGenericType() && t.Namespace == migrationsNamespace
				select (IMigrationMetadata)Activator.CreateInstance(t) into mm
				where !string.IsNullOrWhiteSpace(mm.Id) && mm.Id.IsValidMigrationId()
				orderby mm.Id
				select mm).ToList<IMigrationMetadata>();
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x0600106B RID: 4203 RVA: 0x00025592 File Offset: 0x00023792
		public virtual IEnumerable<string> MigrationIds
		{
			get
			{
				return this._migrations.Select((IMigrationMetadata t) => t.Id).ToList<string>();
			}
		}

		// Token: 0x0600106C RID: 4204 RVA: 0x000255C3 File Offset: 0x000237C3
		public virtual string UniquifyName(string migrationName)
		{
			return this._migrations.Select((IMigrationMetadata m) => m.GetType().Name).Uniquify(migrationName);
		}

		// Token: 0x0600106D RID: 4205 RVA: 0x000255F8 File Offset: 0x000237F8
		public virtual DbMigration GetMigration(string migrationId)
		{
			DbMigration dbMigration = (DbMigration)this._migrations.SingleOrDefault((IMigrationMetadata m) => m.Id.StartsWith(migrationId, StringComparison.Ordinal));
			if (dbMigration != null)
			{
				dbMigration.Reset();
			}
			return dbMigration;
		}

		// Token: 0x040008A5 RID: 2213
		private readonly IList<IMigrationMetadata> _migrations;
	}
}
