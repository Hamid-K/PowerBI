using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Internal.Linq
{
	// Token: 0x02000145 RID: 325
	internal class InternalDbSet<TEntity> : DbSet, IQueryable<TEntity>, IEnumerable<TEntity>, IEnumerable, IQueryable, IDbAsyncEnumerable<TEntity>, IDbAsyncEnumerable where TEntity : class
	{
		// Token: 0x06001527 RID: 5415 RVA: 0x0003742F File Offset: 0x0003562F
		public InternalDbSet(IInternalSet<TEntity> internalSet)
		{
			this._internalSet = internalSet;
		}

		// Token: 0x06001528 RID: 5416 RVA: 0x0003743E File Offset: 0x0003563E
		public static InternalDbSet<TEntity> Create(InternalContext internalContext, IInternalSet internalSet)
		{
			return new InternalDbSet<TEntity>(((IInternalSet<TEntity>)internalSet) ?? new InternalSet<TEntity>(internalContext));
		}

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x06001529 RID: 5417 RVA: 0x00037455 File Offset: 0x00035655
		internal override IInternalQuery InternalQuery
		{
			get
			{
				return this._internalSet;
			}
		}

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x0600152A RID: 5418 RVA: 0x0003745D File Offset: 0x0003565D
		internal override IInternalSet InternalSet
		{
			get
			{
				return this._internalSet;
			}
		}

		// Token: 0x0600152B RID: 5419 RVA: 0x00037465 File Offset: 0x00035665
		public override DbQuery Include(string path)
		{
			Check.NotEmpty(path, "path");
			return new InternalDbQuery<TEntity>(this._internalSet.Include(path));
		}

		// Token: 0x0600152C RID: 5420 RVA: 0x00037484 File Offset: 0x00035684
		public override DbQuery AsNoTracking()
		{
			return new InternalDbQuery<TEntity>(this._internalSet.AsNoTracking());
		}

		// Token: 0x0600152D RID: 5421 RVA: 0x00037496 File Offset: 0x00035696
		[Obsolete("Queries are now streaming by default unless a retrying ExecutionStrategy is used. Calling this method will have no effect.")]
		public override DbQuery AsStreaming()
		{
			return new InternalDbQuery<TEntity>(this._internalSet.AsStreaming());
		}

		// Token: 0x0600152E RID: 5422 RVA: 0x000374A8 File Offset: 0x000356A8
		internal override DbQuery WithExecutionStrategy(IDbExecutionStrategy executionStrategy)
		{
			return new InternalDbQuery<TEntity>(this._internalSet.WithExecutionStrategy(executionStrategy));
		}

		// Token: 0x0600152F RID: 5423 RVA: 0x000374BB File Offset: 0x000356BB
		public override object Find(params object[] keyValues)
		{
			return this._internalSet.Find(keyValues);
		}

		// Token: 0x06001530 RID: 5424 RVA: 0x000374CE File Offset: 0x000356CE
		internal override IInternalQuery GetInternalQueryWithCheck(string memberName)
		{
			return this._internalSet;
		}

		// Token: 0x06001531 RID: 5425 RVA: 0x000374D6 File Offset: 0x000356D6
		internal override IInternalSet GetInternalSetWithCheck(string memberName)
		{
			return this._internalSet;
		}

		// Token: 0x06001532 RID: 5426 RVA: 0x000374E0 File Offset: 0x000356E0
		public override async Task<object> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
		{
			global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<TEntity> cultureAwaiter = this._internalSet.FindAsync(cancellationToken, keyValues).WithCurrentCulture<TEntity>().GetAwaiter();
			if (!cultureAwaiter.IsCompleted)
			{
				await cultureAwaiter;
				global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<TEntity> cultureAwaiter2;
				cultureAwaiter = cultureAwaiter2;
				cultureAwaiter2 = default(global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<TEntity>);
			}
			return cultureAwaiter.GetResult();
		}

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x06001533 RID: 5427 RVA: 0x00037535 File Offset: 0x00035735
		public override IList Local
		{
			get
			{
				return this._internalSet.Local;
			}
		}

		// Token: 0x06001534 RID: 5428 RVA: 0x00037542 File Offset: 0x00035742
		public override object Create()
		{
			return this._internalSet.Create();
		}

		// Token: 0x06001535 RID: 5429 RVA: 0x00037554 File Offset: 0x00035754
		public override object Create(Type derivedEntityType)
		{
			Check.NotNull<Type>(derivedEntityType, "derivedEntityType");
			return this._internalSet.Create(derivedEntityType);
		}

		// Token: 0x06001536 RID: 5430 RVA: 0x00037573 File Offset: 0x00035773
		public IEnumerator<TEntity> GetEnumerator()
		{
			return this._internalSet.GetEnumerator();
		}

		// Token: 0x06001537 RID: 5431 RVA: 0x00037580 File Offset: 0x00035780
		public IDbAsyncEnumerator<TEntity> GetAsyncEnumerator()
		{
			return this._internalSet.GetAsyncEnumerator();
		}

		// Token: 0x040009CD RID: 2509
		private readonly IInternalSet<TEntity> _internalSet;
	}
}
