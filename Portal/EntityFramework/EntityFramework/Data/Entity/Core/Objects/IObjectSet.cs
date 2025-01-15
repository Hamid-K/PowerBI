using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x0200040D RID: 1037
	public interface IObjectSet<TEntity> : IQueryable<TEntity>, IEnumerable<TEntity>, IEnumerable, IQueryable where TEntity : class
	{
		// Token: 0x06003119 RID: 12569
		void AddObject(TEntity entity);

		// Token: 0x0600311A RID: 12570
		void Attach(TEntity entity);

		// Token: 0x0600311B RID: 12571
		void DeleteObject(TEntity entity);

		// Token: 0x0600311C RID: 12572
		void Detach(TEntity entity);
	}
}
