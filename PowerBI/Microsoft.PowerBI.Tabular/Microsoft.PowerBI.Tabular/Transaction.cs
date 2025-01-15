using System;
using Microsoft.AnalysisServices.Core;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x020000E4 RID: 228
	internal sealed class Transaction : ITransaction, IDisposable
	{
		// Token: 0x06000EF4 RID: 3828 RVA: 0x00073CAA File Offset: 0x00071EAA
		internal Transaction(Server owner)
		{
			this.owner = owner;
			this.isPending = true;
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06000EF5 RID: 3829 RVA: 0x00073CC0 File Offset: 0x00071EC0
		// (set) Token: 0x06000EF6 RID: 3830 RVA: 0x00073CC8 File Offset: 0x00071EC8
		public Database ModifiedDatabase { get; set; }

		// Token: 0x06000EF7 RID: 3831 RVA: 0x00073CD4 File Offset: 0x00071ED4
		public static void Cleanup(ITransaction tx, bool rollbackLocalChanges)
		{
			if (tx == null)
			{
				return;
			}
			lock (tx)
			{
				if (!((Transaction)tx).isPending)
				{
					return;
				}
				((Transaction)tx).isPending = false;
			}
			try
			{
				if (rollbackLocalChanges && tx.ModifiedDatabase != null)
				{
					((Database)tx.ModifiedDatabase).TryRollbackTrasaction();
				}
				GC.SuppressFinalize(tx);
			}
			finally
			{
				object txCleanupLock = ((Transaction)tx).owner.txCleanupLock;
				lock (txCleanupLock)
				{
					if (((Transaction)tx).owner.CurrentTransaction == tx)
					{
						((Transaction)tx).owner.transactionsCount = 0;
						((Transaction)tx).owner.CurrentTransaction = null;
					}
				}
			}
		}

		// Token: 0x06000EF8 RID: 3832 RVA: 0x00073DC8 File Offset: 0x00071FC8
		public void Dispose()
		{
			GC.SuppressFinalize(this);
		}

		// Token: 0x040001C7 RID: 455
		private readonly Server owner;

		// Token: 0x040001C8 RID: 456
		private bool isPending;
	}
}
