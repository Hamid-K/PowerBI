using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x0200023D RID: 573
	public class DefaultExecutionStrategy : IDbExecutionStrategy
	{
		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x06001E31 RID: 7729 RVA: 0x000544D2 File Offset: 0x000526D2
		public bool RetriesOnFailure
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001E32 RID: 7730 RVA: 0x000544D5 File Offset: 0x000526D5
		public void Execute(Action operation)
		{
			operation();
		}

		// Token: 0x06001E33 RID: 7731 RVA: 0x000544DD File Offset: 0x000526DD
		public TResult Execute<TResult>(Func<TResult> operation)
		{
			return operation();
		}

		// Token: 0x06001E34 RID: 7732 RVA: 0x000544E5 File Offset: 0x000526E5
		public Task ExecuteAsync(Func<Task> operation, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			return operation();
		}

		// Token: 0x06001E35 RID: 7733 RVA: 0x000544F4 File Offset: 0x000526F4
		public Task<TResult> ExecuteAsync<TResult>(Func<Task<TResult>> operation, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			return operation();
		}
	}
}
