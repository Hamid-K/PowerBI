using System;
using System.Collections;
using System.Data.Entity.Infrastructure;

namespace System.Data.Entity.Internal.Linq
{
	// Token: 0x02000141 RID: 321
	internal interface IInternalSet : IInternalQuery
	{
		// Token: 0x0600150F RID: 5391
		void Attach(object entity);

		// Token: 0x06001510 RID: 5392
		void Add(object entity);

		// Token: 0x06001511 RID: 5393
		void AddRange(IEnumerable entities);

		// Token: 0x06001512 RID: 5394
		void RemoveRange(IEnumerable entities);

		// Token: 0x06001513 RID: 5395
		void Remove(object entity);

		// Token: 0x06001514 RID: 5396
		void Initialize();

		// Token: 0x06001515 RID: 5397
		void TryInitialize();

		// Token: 0x06001516 RID: 5398
		IEnumerator ExecuteSqlQuery(string sql, bool asNoTracking, bool? streaming, object[] parameters);

		// Token: 0x06001517 RID: 5399
		IDbAsyncEnumerator ExecuteSqlQueryAsync(string sql, bool asNoTracking, bool? streaming, object[] parameters);
	}
}
