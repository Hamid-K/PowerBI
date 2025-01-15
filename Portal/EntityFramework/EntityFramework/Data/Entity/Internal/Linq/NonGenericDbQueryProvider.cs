using System;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Data.Entity.Internal.Linq
{
	// Token: 0x02000148 RID: 328
	internal class NonGenericDbQueryProvider : DbQueryProvider
	{
		// Token: 0x06001576 RID: 5494 RVA: 0x000381C2 File Offset: 0x000363C2
		public NonGenericDbQueryProvider(InternalContext internalContext, IInternalQuery internalQuery)
			: base(internalContext, internalQuery)
		{
		}

		// Token: 0x06001577 RID: 5495 RVA: 0x000381CC File Offset: 0x000363CC
		public override IQueryable<TElement> CreateQuery<TElement>(Expression expression)
		{
			Check.NotNull<Expression>(expression, "expression");
			ObjectQuery objectQuery = base.CreateObjectQuery(expression);
			if (typeof(TElement) != ((IQueryable)objectQuery).ElementType)
			{
				return (IQueryable<TElement>)this.CreateQuery(objectQuery);
			}
			return new InternalDbQuery<TElement>(new InternalQuery<TElement>(base.InternalContext, objectQuery));
		}

		// Token: 0x06001578 RID: 5496 RVA: 0x00038222 File Offset: 0x00036422
		public override IQueryable CreateQuery(Expression expression)
		{
			Check.NotNull<Expression>(expression, "expression");
			return this.CreateQuery(base.CreateObjectQuery(expression));
		}

		// Token: 0x06001579 RID: 5497 RVA: 0x00038240 File Offset: 0x00036440
		private IQueryable CreateQuery(ObjectQuery objectQuery)
		{
			IInternalQuery internalQuery = base.CreateInternalQuery(objectQuery);
			return (IQueryable)typeof(InternalDbQuery<>).MakeGenericType(new Type[] { internalQuery.ElementType }).GetConstructors(BindingFlags.Instance | BindingFlags.Public).Single<ConstructorInfo>()
				.Invoke(new object[] { internalQuery });
		}
	}
}
