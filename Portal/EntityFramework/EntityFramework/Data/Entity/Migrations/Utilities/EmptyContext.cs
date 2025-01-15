using System;
using System.Data.Common;

namespace System.Data.Entity.Migrations.Utilities
{
	// Token: 0x020000A4 RID: 164
	internal class EmptyContext : DbContext
	{
		// Token: 0x06000EC0 RID: 3776 RVA: 0x0001F3B0 File Offset: 0x0001D5B0
		public EmptyContext(DbConnection existingConnection)
			: base(existingConnection, false)
		{
			this.InternalContext.InitializerDisabled = true;
		}
	}
}
