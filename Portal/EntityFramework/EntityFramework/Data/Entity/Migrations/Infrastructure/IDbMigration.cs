using System;
using System.Data.Entity.Migrations.Model;

namespace System.Data.Entity.Migrations.Infrastructure
{
	// Token: 0x020000D0 RID: 208
	public interface IDbMigration
	{
		// Token: 0x06001063 RID: 4195
		void AddOperation(MigrationOperation migrationOperation);
	}
}
