using System;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x02000037 RID: 55
	public abstract class SqlRetryLogicBase : ICloneable
	{
		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x0600071D RID: 1821 RVA: 0x0000EB69 File Offset: 0x0000CD69
		// (set) Token: 0x0600071E RID: 1822 RVA: 0x0000EB71 File Offset: 0x0000CD71
		public int NumberOfTries { get; protected set; }

		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x0600071F RID: 1823 RVA: 0x0000EB7A File Offset: 0x0000CD7A
		// (set) Token: 0x06000720 RID: 1824 RVA: 0x0000EB82 File Offset: 0x0000CD82
		public int Current { get; protected set; }

		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x06000721 RID: 1825 RVA: 0x0000EB8B File Offset: 0x0000CD8B
		// (set) Token: 0x06000722 RID: 1826 RVA: 0x0000EB93 File Offset: 0x0000CD93
		public SqlRetryIntervalBaseEnumerator RetryIntervalEnumerator { get; protected set; }

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x06000723 RID: 1827 RVA: 0x0000EB9C File Offset: 0x0000CD9C
		// (set) Token: 0x06000724 RID: 1828 RVA: 0x0000EBA4 File Offset: 0x0000CDA4
		public Predicate<Exception> TransientPredicate { get; protected set; }

		// Token: 0x06000725 RID: 1829 RVA: 0x0000EBAD File Offset: 0x0000CDAD
		public virtual bool RetryCondition(object sender)
		{
			return true;
		}

		// Token: 0x06000726 RID: 1830
		public abstract bool TryNextInterval(out TimeSpan intervalTime);

		// Token: 0x06000727 RID: 1831
		public abstract void Reset();

		// Token: 0x06000728 RID: 1832 RVA: 0x0000E96E File Offset: 0x0000CB6E
		public virtual object Clone()
		{
			throw new NotImplementedException();
		}
	}
}
