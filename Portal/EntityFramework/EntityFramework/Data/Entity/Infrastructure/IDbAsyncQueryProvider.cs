using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x02000249 RID: 585
	public interface IDbAsyncQueryProvider : IQueryProvider
	{
		// Token: 0x06001EAE RID: 7854
		Task<object> ExecuteAsync(Expression expression, CancellationToken cancellationToken);

		// Token: 0x06001EAF RID: 7855
		Task<TResult> ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken);
	}
}
