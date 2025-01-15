using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Internal.Linq
{
	// Token: 0x02000143 RID: 323
	internal interface IInternalSet<TEntity> : IInternalSet, IInternalQuery, IInternalQuery<TEntity> where TEntity : class
	{
		// Token: 0x06001519 RID: 5401
		TEntity Find(params object[] keyValues);

		// Token: 0x0600151A RID: 5402
		Task<TEntity> FindAsync(CancellationToken cancellationToken, params object[] keyValues);

		// Token: 0x0600151B RID: 5403
		TEntity Create();

		// Token: 0x0600151C RID: 5404
		TEntity Create(Type derivedEntityType);

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x0600151D RID: 5405
		ObservableCollection<TEntity> Local { get; }
	}
}
