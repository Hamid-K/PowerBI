using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Internal;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x02000234 RID: 564
	public class DbRawSqlQuery<TElement> : IEnumerable<TElement>, IEnumerable, IListSource, IDbAsyncEnumerable<TElement>, IDbAsyncEnumerable
	{
		// Token: 0x06001DAE RID: 7598 RVA: 0x00053B0C File Offset: 0x00051D0C
		internal DbRawSqlQuery(InternalSqlQuery internalQuery)
		{
			this._internalQuery = internalQuery;
		}

		// Token: 0x06001DAF RID: 7599 RVA: 0x00053B1B File Offset: 0x00051D1B
		[Obsolete("Queries are now streaming by default unless a retrying ExecutionStrategy is used. Calling this method will have no effect.")]
		public virtual DbRawSqlQuery<TElement> AsStreaming()
		{
			if (this._internalQuery != null)
			{
				return new DbRawSqlQuery<TElement>(this._internalQuery.AsStreaming());
			}
			return this;
		}

		// Token: 0x06001DB0 RID: 7600 RVA: 0x00053B37 File Offset: 0x00051D37
		public virtual IEnumerator<TElement> GetEnumerator()
		{
			return (IEnumerator<TElement>)this.GetInternalQueryWithCheck("GetEnumerator").GetEnumerator();
		}

		// Token: 0x06001DB1 RID: 7601 RVA: 0x00053B4E File Offset: 0x00051D4E
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06001DB2 RID: 7602 RVA: 0x00053B56 File Offset: 0x00051D56
		IDbAsyncEnumerator<TElement> IDbAsyncEnumerable<TElement>.GetAsyncEnumerator()
		{
			return (IDbAsyncEnumerator<TElement>)this.GetInternalQueryWithCheck("IDbAsyncEnumerable<TElement>.GetAsyncEnumerator").GetAsyncEnumerator();
		}

		// Token: 0x06001DB3 RID: 7603 RVA: 0x00053B6D File Offset: 0x00051D6D
		IDbAsyncEnumerator IDbAsyncEnumerable.GetAsyncEnumerator()
		{
			return this._internalQuery.GetAsyncEnumerator();
		}

		// Token: 0x06001DB4 RID: 7604 RVA: 0x00053B7A File Offset: 0x00051D7A
		public Task ForEachAsync(Action<TElement> action)
		{
			Check.NotNull<Action<TElement>>(action, "action");
			return this.ForEachAsync(action, CancellationToken.None);
		}

		// Token: 0x06001DB5 RID: 7605 RVA: 0x00053B94 File Offset: 0x00051D94
		public Task ForEachAsync(Action<TElement> action, CancellationToken cancellationToken)
		{
			Check.NotNull<Action<TElement>>(action, "action");
			return this.ForEachAsync(action, cancellationToken);
		}

		// Token: 0x06001DB6 RID: 7606 RVA: 0x00053BAA File Offset: 0x00051DAA
		public Task<List<TElement>> ToListAsync()
		{
			return this.ToListAsync<TElement>();
		}

		// Token: 0x06001DB7 RID: 7607 RVA: 0x00053BB2 File Offset: 0x00051DB2
		public Task<List<TElement>> ToListAsync(CancellationToken cancellationToken)
		{
			return this.ToListAsync(cancellationToken);
		}

		// Token: 0x06001DB8 RID: 7608 RVA: 0x00053BBB File Offset: 0x00051DBB
		public Task<TElement[]> ToArrayAsync()
		{
			return this.ToArrayAsync<TElement>();
		}

		// Token: 0x06001DB9 RID: 7609 RVA: 0x00053BC3 File Offset: 0x00051DC3
		public Task<TElement[]> ToArrayAsync(CancellationToken cancellationToken)
		{
			return this.ToArrayAsync(cancellationToken);
		}

		// Token: 0x06001DBA RID: 7610 RVA: 0x00053BCC File Offset: 0x00051DCC
		public Task<Dictionary<TKey, TElement>> ToDictionaryAsync<TKey>(Func<TElement, TKey> keySelector)
		{
			Check.NotNull<Func<TElement, TKey>>(keySelector, "keySelector");
			return this.ToDictionaryAsync(keySelector);
		}

		// Token: 0x06001DBB RID: 7611 RVA: 0x00053BE1 File Offset: 0x00051DE1
		public Task<Dictionary<TKey, TElement>> ToDictionaryAsync<TKey>(Func<TElement, TKey> keySelector, CancellationToken cancellationToken)
		{
			Check.NotNull<Func<TElement, TKey>>(keySelector, "keySelector");
			return this.ToDictionaryAsync(keySelector, cancellationToken);
		}

		// Token: 0x06001DBC RID: 7612 RVA: 0x00053BF7 File Offset: 0x00051DF7
		public Task<Dictionary<TKey, TElement>> ToDictionaryAsync<TKey>(Func<TElement, TKey> keySelector, IEqualityComparer<TKey> comparer)
		{
			Check.NotNull<Func<TElement, TKey>>(keySelector, "keySelector");
			return this.ToDictionaryAsync(keySelector, comparer);
		}

		// Token: 0x06001DBD RID: 7613 RVA: 0x00053C0D File Offset: 0x00051E0D
		public Task<Dictionary<TKey, TElement>> ToDictionaryAsync<TKey>(Func<TElement, TKey> keySelector, IEqualityComparer<TKey> comparer, CancellationToken cancellationToken)
		{
			Check.NotNull<Func<TElement, TKey>>(keySelector, "keySelector");
			return this.ToDictionaryAsync(keySelector, comparer, cancellationToken);
		}

		// Token: 0x06001DBE RID: 7614 RVA: 0x00053C24 File Offset: 0x00051E24
		public Task<Dictionary<TKey, TResult>> ToDictionaryAsync<TKey, TResult>(Func<TElement, TKey> keySelector, Func<TElement, TResult> elementSelector)
		{
			Check.NotNull<Func<TElement, TKey>>(keySelector, "keySelector");
			Check.NotNull<Func<TElement, TResult>>(elementSelector, "elementSelector");
			return this.ToDictionaryAsync(keySelector, elementSelector);
		}

		// Token: 0x06001DBF RID: 7615 RVA: 0x00053C46 File Offset: 0x00051E46
		public Task<Dictionary<TKey, TResult>> ToDictionaryAsync<TKey, TResult>(Func<TElement, TKey> keySelector, Func<TElement, TResult> elementSelector, CancellationToken cancellationToken)
		{
			Check.NotNull<Func<TElement, TKey>>(keySelector, "keySelector");
			Check.NotNull<Func<TElement, TResult>>(elementSelector, "elementSelector");
			return this.ToDictionaryAsync(keySelector, elementSelector, cancellationToken);
		}

		// Token: 0x06001DC0 RID: 7616 RVA: 0x00053C69 File Offset: 0x00051E69
		public Task<Dictionary<TKey, TResult>> ToDictionaryAsync<TKey, TResult>(Func<TElement, TKey> keySelector, Func<TElement, TResult> elementSelector, IEqualityComparer<TKey> comparer)
		{
			Check.NotNull<Func<TElement, TKey>>(keySelector, "keySelector");
			Check.NotNull<Func<TElement, TResult>>(elementSelector, "elementSelector");
			return this.ToDictionaryAsync(keySelector, elementSelector, comparer);
		}

		// Token: 0x06001DC1 RID: 7617 RVA: 0x00053C8C File Offset: 0x00051E8C
		public Task<Dictionary<TKey, TResult>> ToDictionaryAsync<TKey, TResult>(Func<TElement, TKey> keySelector, Func<TElement, TResult> elementSelector, IEqualityComparer<TKey> comparer, CancellationToken cancellationToken)
		{
			Check.NotNull<Func<TElement, TKey>>(keySelector, "keySelector");
			Check.NotNull<Func<TElement, TResult>>(elementSelector, "elementSelector");
			return this.ToDictionaryAsync(keySelector, elementSelector, comparer, cancellationToken);
		}

		// Token: 0x06001DC2 RID: 7618 RVA: 0x00053CB1 File Offset: 0x00051EB1
		public Task<TElement> FirstAsync()
		{
			return this.FirstAsync<TElement>();
		}

		// Token: 0x06001DC3 RID: 7619 RVA: 0x00053CB9 File Offset: 0x00051EB9
		public Task<TElement> FirstAsync(CancellationToken cancellationToken)
		{
			return this.FirstAsync(cancellationToken);
		}

		// Token: 0x06001DC4 RID: 7620 RVA: 0x00053CC2 File Offset: 0x00051EC2
		public Task<TElement> FirstAsync(Func<TElement, bool> predicate)
		{
			Check.NotNull<Func<TElement, bool>>(predicate, "predicate");
			return this.FirstAsync(predicate);
		}

		// Token: 0x06001DC5 RID: 7621 RVA: 0x00053CD7 File Offset: 0x00051ED7
		public Task<TElement> FirstAsync(Func<TElement, bool> predicate, CancellationToken cancellationToken)
		{
			Check.NotNull<Func<TElement, bool>>(predicate, "predicate");
			return this.FirstAsync(predicate, cancellationToken);
		}

		// Token: 0x06001DC6 RID: 7622 RVA: 0x00053CED File Offset: 0x00051EED
		public Task<TElement> FirstOrDefaultAsync()
		{
			return this.FirstOrDefaultAsync<TElement>();
		}

		// Token: 0x06001DC7 RID: 7623 RVA: 0x00053CF5 File Offset: 0x00051EF5
		public Task<TElement> FirstOrDefaultAsync(CancellationToken cancellationToken)
		{
			return this.FirstOrDefaultAsync(cancellationToken);
		}

		// Token: 0x06001DC8 RID: 7624 RVA: 0x00053CFE File Offset: 0x00051EFE
		public Task<TElement> FirstOrDefaultAsync(Func<TElement, bool> predicate)
		{
			Check.NotNull<Func<TElement, bool>>(predicate, "predicate");
			return this.FirstOrDefaultAsync(predicate);
		}

		// Token: 0x06001DC9 RID: 7625 RVA: 0x00053D13 File Offset: 0x00051F13
		public Task<TElement> FirstOrDefaultAsync(Func<TElement, bool> predicate, CancellationToken cancellationToken)
		{
			Check.NotNull<Func<TElement, bool>>(predicate, "predicate");
			return this.FirstOrDefaultAsync(predicate, cancellationToken);
		}

		// Token: 0x06001DCA RID: 7626 RVA: 0x00053D29 File Offset: 0x00051F29
		public Task<TElement> SingleAsync()
		{
			return this.SingleAsync<TElement>();
		}

		// Token: 0x06001DCB RID: 7627 RVA: 0x00053D31 File Offset: 0x00051F31
		public Task<TElement> SingleAsync(CancellationToken cancellationToken)
		{
			return this.SingleAsync(cancellationToken);
		}

		// Token: 0x06001DCC RID: 7628 RVA: 0x00053D3A File Offset: 0x00051F3A
		public Task<TElement> SingleAsync(Func<TElement, bool> predicate)
		{
			Check.NotNull<Func<TElement, bool>>(predicate, "predicate");
			return this.SingleAsync(predicate);
		}

		// Token: 0x06001DCD RID: 7629 RVA: 0x00053D4F File Offset: 0x00051F4F
		public Task<TElement> SingleAsync(Func<TElement, bool> predicate, CancellationToken cancellationToken)
		{
			Check.NotNull<Func<TElement, bool>>(predicate, "predicate");
			return this.SingleAsync(predicate, cancellationToken);
		}

		// Token: 0x06001DCE RID: 7630 RVA: 0x00053D65 File Offset: 0x00051F65
		public Task<TElement> SingleOrDefaultAsync()
		{
			return this.SingleOrDefaultAsync<TElement>();
		}

		// Token: 0x06001DCF RID: 7631 RVA: 0x00053D6D File Offset: 0x00051F6D
		public Task<TElement> SingleOrDefaultAsync(CancellationToken cancellationToken)
		{
			return this.SingleOrDefaultAsync(cancellationToken);
		}

		// Token: 0x06001DD0 RID: 7632 RVA: 0x00053D76 File Offset: 0x00051F76
		public Task<TElement> SingleOrDefaultAsync(Func<TElement, bool> predicate)
		{
			Check.NotNull<Func<TElement, bool>>(predicate, "predicate");
			return this.SingleOrDefaultAsync(predicate);
		}

		// Token: 0x06001DD1 RID: 7633 RVA: 0x00053D8B File Offset: 0x00051F8B
		public Task<TElement> SingleOrDefaultAsync(Func<TElement, bool> predicate, CancellationToken cancellationToken)
		{
			Check.NotNull<Func<TElement, bool>>(predicate, "predicate");
			return this.SingleOrDefaultAsync(predicate, cancellationToken);
		}

		// Token: 0x06001DD2 RID: 7634 RVA: 0x00053DA1 File Offset: 0x00051FA1
		public Task<bool> ContainsAsync(TElement value)
		{
			return this.ContainsAsync(value);
		}

		// Token: 0x06001DD3 RID: 7635 RVA: 0x00053DAA File Offset: 0x00051FAA
		public Task<bool> ContainsAsync(TElement value, CancellationToken cancellationToken)
		{
			return this.ContainsAsync(value, cancellationToken);
		}

		// Token: 0x06001DD4 RID: 7636 RVA: 0x00053DB4 File Offset: 0x00051FB4
		public Task<bool> AnyAsync()
		{
			return this.AnyAsync<TElement>();
		}

		// Token: 0x06001DD5 RID: 7637 RVA: 0x00053DBC File Offset: 0x00051FBC
		public Task<bool> AnyAsync(CancellationToken cancellationToken)
		{
			return this.AnyAsync(cancellationToken);
		}

		// Token: 0x06001DD6 RID: 7638 RVA: 0x00053DC5 File Offset: 0x00051FC5
		public Task<bool> AnyAsync(Func<TElement, bool> predicate)
		{
			Check.NotNull<Func<TElement, bool>>(predicate, "predicate");
			return this.AnyAsync(predicate);
		}

		// Token: 0x06001DD7 RID: 7639 RVA: 0x00053DDA File Offset: 0x00051FDA
		public Task<bool> AnyAsync(Func<TElement, bool> predicate, CancellationToken cancellationToken)
		{
			Check.NotNull<Func<TElement, bool>>(predicate, "predicate");
			return this.AnyAsync(predicate, cancellationToken);
		}

		// Token: 0x06001DD8 RID: 7640 RVA: 0x00053DF0 File Offset: 0x00051FF0
		public Task<bool> AllAsync(Func<TElement, bool> predicate)
		{
			Check.NotNull<Func<TElement, bool>>(predicate, "predicate");
			return this.AllAsync(predicate);
		}

		// Token: 0x06001DD9 RID: 7641 RVA: 0x00053E05 File Offset: 0x00052005
		public Task<bool> AllAsync(Func<TElement, bool> predicate, CancellationToken cancellationToken)
		{
			Check.NotNull<Func<TElement, bool>>(predicate, "predicate");
			return this.AllAsync(predicate, cancellationToken);
		}

		// Token: 0x06001DDA RID: 7642 RVA: 0x00053E1B File Offset: 0x0005201B
		public Task<int> CountAsync()
		{
			return this.CountAsync<TElement>();
		}

		// Token: 0x06001DDB RID: 7643 RVA: 0x00053E23 File Offset: 0x00052023
		public Task<int> CountAsync(CancellationToken cancellationToken)
		{
			return this.CountAsync(cancellationToken);
		}

		// Token: 0x06001DDC RID: 7644 RVA: 0x00053E2C File Offset: 0x0005202C
		public Task<int> CountAsync(Func<TElement, bool> predicate)
		{
			Check.NotNull<Func<TElement, bool>>(predicate, "predicate");
			return this.CountAsync(predicate);
		}

		// Token: 0x06001DDD RID: 7645 RVA: 0x00053E41 File Offset: 0x00052041
		public Task<int> CountAsync(Func<TElement, bool> predicate, CancellationToken cancellationToken)
		{
			Check.NotNull<Func<TElement, bool>>(predicate, "predicate");
			return this.CountAsync(predicate, cancellationToken);
		}

		// Token: 0x06001DDE RID: 7646 RVA: 0x00053E57 File Offset: 0x00052057
		public Task<long> LongCountAsync()
		{
			return this.LongCountAsync<TElement>();
		}

		// Token: 0x06001DDF RID: 7647 RVA: 0x00053E5F File Offset: 0x0005205F
		public Task<long> LongCountAsync(CancellationToken cancellationToken)
		{
			return this.LongCountAsync(cancellationToken);
		}

		// Token: 0x06001DE0 RID: 7648 RVA: 0x00053E68 File Offset: 0x00052068
		public Task<long> LongCountAsync(Func<TElement, bool> predicate)
		{
			Check.NotNull<Func<TElement, bool>>(predicate, "predicate");
			return this.LongCountAsync(predicate);
		}

		// Token: 0x06001DE1 RID: 7649 RVA: 0x00053E7D File Offset: 0x0005207D
		public Task<long> LongCountAsync(Func<TElement, bool> predicate, CancellationToken cancellationToken)
		{
			Check.NotNull<Func<TElement, bool>>(predicate, "predicate");
			return this.LongCountAsync(predicate, cancellationToken);
		}

		// Token: 0x06001DE2 RID: 7650 RVA: 0x00053E93 File Offset: 0x00052093
		public Task<TElement> MinAsync()
		{
			return this.MinAsync<TElement>();
		}

		// Token: 0x06001DE3 RID: 7651 RVA: 0x00053E9B File Offset: 0x0005209B
		public Task<TElement> MinAsync(CancellationToken cancellationToken)
		{
			return this.MinAsync(cancellationToken);
		}

		// Token: 0x06001DE4 RID: 7652 RVA: 0x00053EA4 File Offset: 0x000520A4
		public Task<TElement> MaxAsync()
		{
			return this.MaxAsync<TElement>();
		}

		// Token: 0x06001DE5 RID: 7653 RVA: 0x00053EAC File Offset: 0x000520AC
		public Task<TElement> MaxAsync(CancellationToken cancellationToken)
		{
			return this.MaxAsync(cancellationToken);
		}

		// Token: 0x06001DE6 RID: 7654 RVA: 0x00053EB5 File Offset: 0x000520B5
		public override string ToString()
		{
			if (this._internalQuery != null)
			{
				return this._internalQuery.ToString();
			}
			return base.ToString();
		}

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x06001DE7 RID: 7655 RVA: 0x00053ED1 File Offset: 0x000520D1
		internal InternalSqlQuery InternalQuery
		{
			get
			{
				return this._internalQuery;
			}
		}

		// Token: 0x06001DE8 RID: 7656 RVA: 0x00053ED9 File Offset: 0x000520D9
		private InternalSqlQuery GetInternalQueryWithCheck(string memberName)
		{
			if (this._internalQuery == null)
			{
				throw new NotImplementedException(Strings.TestDoubleNotImplemented(memberName, this.GetType().Name, typeof(DbSqlQuery<>).Name));
			}
			return this._internalQuery;
		}

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x06001DE9 RID: 7657 RVA: 0x00053F0F File Offset: 0x0005210F
		bool IListSource.ContainsListCollection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001DEA RID: 7658 RVA: 0x00053F12 File Offset: 0x00052112
		IList IListSource.GetList()
		{
			throw Error.DbQuery_BindingToDbQueryNotSupported();
		}

		// Token: 0x06001DEB RID: 7659 RVA: 0x00053F19 File Offset: 0x00052119
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06001DEC RID: 7660 RVA: 0x00053F22 File Offset: 0x00052122
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06001DED RID: 7661 RVA: 0x00053F2A File Offset: 0x0005212A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x04000B27 RID: 2855
		private readonly InternalSqlQuery _internalQuery;
	}
}
