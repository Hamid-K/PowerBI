using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Resources;
using System.Linq;
using System.Linq.Expressions;

namespace System.Data.Entity.Core.Common.Internal.Materialization
{
	// Token: 0x02000635 RID: 1589
	internal class CompensatingCollection<TElement> : IOrderedQueryable<TElement>, IQueryable<TElement>, IEnumerable<TElement>, IEnumerable, IQueryable, IOrderedQueryable, IOrderedEnumerable<TElement>
	{
		// Token: 0x06004C7C RID: 19580 RVA: 0x0010E5E9 File Offset: 0x0010C7E9
		public CompensatingCollection(IEnumerable<TElement> source)
		{
			this._source = source;
			this._expression = Expression.Constant(source);
		}

		// Token: 0x06004C7D RID: 19581 RVA: 0x0010E604 File Offset: 0x0010C804
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._source.GetEnumerator();
		}

		// Token: 0x06004C7E RID: 19582 RVA: 0x0010E611 File Offset: 0x0010C811
		IEnumerator<TElement> IEnumerable<TElement>.GetEnumerator()
		{
			return this._source.GetEnumerator();
		}

		// Token: 0x06004C7F RID: 19583 RVA: 0x0010E61E File Offset: 0x0010C81E
		IOrderedEnumerable<TElement> IOrderedEnumerable<TElement>.CreateOrderedEnumerable<K>(Func<TElement, K> keySelector, IComparer<K> comparer, bool descending)
		{
			throw new NotSupportedException(Strings.ELinq_CreateOrderedEnumerableNotSupported);
		}

		// Token: 0x17000ECA RID: 3786
		// (get) Token: 0x06004C80 RID: 19584 RVA: 0x0010E62A File Offset: 0x0010C82A
		Type IQueryable.ElementType
		{
			get
			{
				return typeof(TElement);
			}
		}

		// Token: 0x17000ECB RID: 3787
		// (get) Token: 0x06004C81 RID: 19585 RVA: 0x0010E636 File Offset: 0x0010C836
		Expression IQueryable.Expression
		{
			get
			{
				return this._expression;
			}
		}

		// Token: 0x17000ECC RID: 3788
		// (get) Token: 0x06004C82 RID: 19586 RVA: 0x0010E63E File Offset: 0x0010C83E
		IQueryProvider IQueryable.Provider
		{
			get
			{
				throw new NotSupportedException(Strings.ELinq_UnsupportedQueryableMethod);
			}
		}

		// Token: 0x04001B0D RID: 6925
		private readonly IEnumerable<TElement> _source;

		// Token: 0x04001B0E RID: 6926
		private readonly Expression _expression;
	}
}
