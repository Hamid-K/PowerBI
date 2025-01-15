using System;
using System.Collections;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Core.Objects.ELinq;
using System.Data.Entity.Infrastructure;
using System.Linq.Expressions;

namespace System.Data.Entity.Internal.Linq
{
	// Token: 0x0200013E RID: 318
	internal interface IInternalQuery
	{
		// Token: 0x060014FF RID: 5375
		void ResetQuery();

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x06001500 RID: 5376
		InternalContext InternalContext { get; }

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x06001501 RID: 5377
		ObjectQuery ObjectQuery { get; }

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x06001502 RID: 5378
		Type ElementType { get; }

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x06001503 RID: 5379
		Expression Expression { get; }

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x06001504 RID: 5380
		ObjectQueryProvider ObjectQueryProvider { get; }

		// Token: 0x06001505 RID: 5381
		string ToTraceString();

		// Token: 0x06001506 RID: 5382
		IDbAsyncEnumerator GetAsyncEnumerator();

		// Token: 0x06001507 RID: 5383
		IEnumerator GetEnumerator();
	}
}
