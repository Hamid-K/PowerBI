using System;
using System.Data.Common;
using System.Data.Entity.Infrastructure;

namespace System.Data.Entity.Migrations.Utilities
{
	// Token: 0x020000A3 RID: 163
	internal class DatabaseCreator
	{
		// Token: 0x06000EBC RID: 3772 RVA: 0x0001F2B8 File Offset: 0x0001D4B8
		public DatabaseCreator(int? commandTimeout)
		{
			this._commandTimeout = commandTimeout;
		}

		// Token: 0x06000EBD RID: 3773 RVA: 0x0001F2C8 File Offset: 0x0001D4C8
		public virtual bool Exists(DbConnection connection)
		{
			bool flag;
			using (EmptyContext emptyContext = new EmptyContext(connection))
			{
				emptyContext.Database.CommandTimeout = this._commandTimeout;
				flag = ((IObjectContextAdapter)emptyContext).ObjectContext.DatabaseExists();
			}
			return flag;
		}

		// Token: 0x06000EBE RID: 3774 RVA: 0x0001F318 File Offset: 0x0001D518
		public virtual void Create(DbConnection connection)
		{
			using (EmptyContext emptyContext = new EmptyContext(connection))
			{
				emptyContext.Database.CommandTimeout = this._commandTimeout;
				((IObjectContextAdapter)emptyContext).ObjectContext.CreateDatabase();
			}
		}

		// Token: 0x06000EBF RID: 3775 RVA: 0x0001F364 File Offset: 0x0001D564
		public virtual void Delete(DbConnection connection)
		{
			using (EmptyContext emptyContext = new EmptyContext(connection))
			{
				emptyContext.Database.CommandTimeout = this._commandTimeout;
				((IObjectContextAdapter)emptyContext).ObjectContext.DeleteDatabase();
			}
		}

		// Token: 0x04000833 RID: 2099
		private readonly int? _commandTimeout;
	}
}
