using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Migrations.Model
{
	// Token: 0x020000CB RID: 203
	public class UpdateDatabaseOperation : MigrationOperation
	{
		// Token: 0x06000FE1 RID: 4065 RVA: 0x000212A8 File Offset: 0x0001F4A8
		public UpdateDatabaseOperation(IList<DbQueryCommandTree> historyQueryTrees)
			: base(null)
		{
			Check.NotNull<IList<DbQueryCommandTree>>(historyQueryTrees, "historyQueryTrees");
			this._historyQueryTrees = historyQueryTrees;
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06000FE2 RID: 4066 RVA: 0x000212CF File Offset: 0x0001F4CF
		public IList<DbQueryCommandTree> HistoryQueryTrees
		{
			get
			{
				return this._historyQueryTrees;
			}
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06000FE3 RID: 4067 RVA: 0x000212D7 File Offset: 0x0001F4D7
		public IList<UpdateDatabaseOperation.Migration> Migrations
		{
			get
			{
				return this._migrations;
			}
		}

		// Token: 0x06000FE4 RID: 4068 RVA: 0x000212DF File Offset: 0x0001F4DF
		public void AddMigration(string migrationId, IList<MigrationOperation> operations)
		{
			Check.NotEmpty(migrationId, "migrationId");
			Check.NotNull<IList<MigrationOperation>>(operations, "operations");
			this._migrations.Add(new UpdateDatabaseOperation.Migration(migrationId, operations));
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x06000FE5 RID: 4069 RVA: 0x0002130B File Offset: 0x0001F50B
		public override bool IsDestructiveChange
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04000896 RID: 2198
		private readonly IList<DbQueryCommandTree> _historyQueryTrees;

		// Token: 0x04000897 RID: 2199
		private readonly IList<UpdateDatabaseOperation.Migration> _migrations = new List<UpdateDatabaseOperation.Migration>();

		// Token: 0x02000757 RID: 1879
		public class Migration
		{
			// Token: 0x060055CA RID: 21962 RVA: 0x0013242C File Offset: 0x0013062C
			internal Migration(string migrationId, IList<MigrationOperation> operations)
			{
				this._migrationId = migrationId;
				this._operations = operations;
			}

			// Token: 0x17001015 RID: 4117
			// (get) Token: 0x060055CB RID: 21963 RVA: 0x00132442 File Offset: 0x00130642
			public string MigrationId
			{
				get
				{
					return this._migrationId;
				}
			}

			// Token: 0x17001016 RID: 4118
			// (get) Token: 0x060055CC RID: 21964 RVA: 0x0013244A File Offset: 0x0013064A
			public IList<MigrationOperation> Operations
			{
				get
				{
					return this._operations;
				}
			}

			// Token: 0x04001EFC RID: 7932
			private readonly string _migrationId;

			// Token: 0x04001EFD RID: 7933
			private readonly IList<MigrationOperation> _operations;
		}
	}
}
