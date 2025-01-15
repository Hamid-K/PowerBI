using System;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x02000038 RID: 56
	public abstract class SqlRetryLogicBaseProvider
	{
		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x0600072A RID: 1834 RVA: 0x0000EBB0 File Offset: 0x0000CDB0
		// (set) Token: 0x0600072B RID: 1835 RVA: 0x0000EBB8 File Offset: 0x0000CDB8
		public EventHandler<SqlRetryingEventArgs> Retrying { get; set; }

		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x0600072C RID: 1836 RVA: 0x0000EBC1 File Offset: 0x0000CDC1
		// (set) Token: 0x0600072D RID: 1837 RVA: 0x0000EBC9 File Offset: 0x0000CDC9
		public SqlRetryLogicBase RetryLogic { get; protected set; }

		// Token: 0x0600072E RID: 1838
		public abstract TResult Execute<TResult>(object sender, Func<TResult> function);

		// Token: 0x0600072F RID: 1839
		public abstract Task<TResult> ExecuteAsync<TResult>(object sender, Func<Task<TResult>> function, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x06000730 RID: 1840
		public abstract Task ExecuteAsync(object sender, Func<Task> function, CancellationToken cancellationToken = default(CancellationToken));
	}
}
