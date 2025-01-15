using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x0200024C RID: 588
	public interface IDbExecutionStrategy
	{
		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x06001EB2 RID: 7858
		bool RetriesOnFailure { get; }

		// Token: 0x06001EB3 RID: 7859
		void Execute(Action operation);

		// Token: 0x06001EB4 RID: 7860
		TResult Execute<TResult>(Func<TResult> operation);

		// Token: 0x06001EB5 RID: 7861
		Task ExecuteAsync(Func<Task> operation, CancellationToken cancellationToken);

		// Token: 0x06001EB6 RID: 7862
		Task<TResult> ExecuteAsync<TResult>(Func<Task<TResult>> operation, CancellationToken cancellationToken);
	}
}
