using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace System.Data.Entity
{
	// Token: 0x02000068 RID: 104
	public interface IDbSet<TEntity> : IQueryable<TEntity>, IEnumerable<TEntity>, IEnumerable, IQueryable where TEntity : class
	{
		// Token: 0x06000360 RID: 864
		TEntity Find(params object[] keyValues);

		// Token: 0x06000361 RID: 865
		TEntity Add(TEntity entity);

		// Token: 0x06000362 RID: 866
		TEntity Remove(TEntity entity);

		// Token: 0x06000363 RID: 867
		TEntity Attach(TEntity entity);

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000364 RID: 868
		ObservableCollection<TEntity> Local { get; }

		// Token: 0x06000365 RID: 869
		TEntity Create();

		// Token: 0x06000366 RID: 870
		TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, TEntity;
	}
}
