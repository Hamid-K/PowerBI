using System;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.Migrations;
using System.Data.Entity.Migrations.Infrastructure;
using System.Data.Entity.Migrations.Sql;

namespace System.Data.Entity.Internal
{
	// Token: 0x020000F1 RID: 241
	internal class DatabaseCreator
	{
		// Token: 0x06001216 RID: 4630 RVA: 0x0002EEC8 File Offset: 0x0002D0C8
		public DatabaseCreator()
			: this(DbConfiguration.DependencyResolver)
		{
		}

		// Token: 0x06001217 RID: 4631 RVA: 0x0002EED5 File Offset: 0x0002D0D5
		public DatabaseCreator(IDbDependencyResolver resolver)
		{
			this._resolver = resolver;
		}

		// Token: 0x06001218 RID: 4632 RVA: 0x0002EEE4 File Offset: 0x0002D0E4
		public virtual void CreateDatabase(InternalContext internalContext, Func<DbMigrationsConfiguration, DbContext, MigratorBase> createMigrator, ObjectContext objectContext)
		{
			if (internalContext.CodeFirstModel != null && this._resolver.GetService(internalContext.ProviderName) != null)
			{
				createMigrator(internalContext.MigrationsConfiguration, internalContext.Owner).Update();
			}
			else
			{
				internalContext.DatabaseOperations.Create(objectContext);
				internalContext.SaveMetadataToDatabase();
			}
			internalContext.MarkDatabaseInitialized();
		}

		// Token: 0x04000904 RID: 2308
		private readonly IDbDependencyResolver _resolver;
	}
}
