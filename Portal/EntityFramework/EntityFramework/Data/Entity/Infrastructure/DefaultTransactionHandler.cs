using System;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x02000263 RID: 611
	internal class DefaultTransactionHandler : TransactionHandler
	{
		// Token: 0x06001F11 RID: 7953 RVA: 0x00056702 File Offset: 0x00054902
		public override string BuildDatabaseInitializationScript()
		{
			return string.Empty;
		}

		// Token: 0x06001F12 RID: 7954 RVA: 0x00056709 File Offset: 0x00054909
		public override void Committed(DbTransaction transaction, DbTransactionInterceptionContext interceptionContext)
		{
			if (interceptionContext.Exception != null && interceptionContext.Connection != null && this.MatchesParentContext(interceptionContext.Connection, interceptionContext))
			{
				interceptionContext.Exception = new CommitFailedException(Strings.CommitFailed, interceptionContext.Exception);
			}
		}
	}
}
