using System;
using System.ComponentModel;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Infrastructure.Interception
{
	// Token: 0x02000289 RID: 649
	public class DbTransactionInterceptionContext<TResult> : MutableInterceptionContext<TResult>
	{
		// Token: 0x060020AC RID: 8364 RVA: 0x0005C89C File Offset: 0x0005AA9C
		public DbTransactionInterceptionContext()
		{
		}

		// Token: 0x060020AD RID: 8365 RVA: 0x0005C8A4 File Offset: 0x0005AAA4
		public DbTransactionInterceptionContext(DbInterceptionContext copyFrom)
			: base(copyFrom)
		{
			Check.NotNull<DbInterceptionContext>(copyFrom, "copyFrom");
		}

		// Token: 0x060020AE RID: 8366 RVA: 0x0005C8B9 File Offset: 0x0005AAB9
		public new DbTransactionInterceptionContext<TResult> AsAsync()
		{
			return (DbTransactionInterceptionContext<TResult>)base.AsAsync();
		}

		// Token: 0x060020AF RID: 8367 RVA: 0x0005C8C6 File Offset: 0x0005AAC6
		public new DbTransactionInterceptionContext<TResult> WithDbContext(DbContext context)
		{
			Check.NotNull<DbContext>(context, "context");
			return (DbTransactionInterceptionContext<TResult>)base.WithDbContext(context);
		}

		// Token: 0x060020B0 RID: 8368 RVA: 0x0005C8E0 File Offset: 0x0005AAE0
		public new DbTransactionInterceptionContext<TResult> WithObjectContext(ObjectContext context)
		{
			Check.NotNull<ObjectContext>(context, "context");
			return (DbTransactionInterceptionContext<TResult>)base.WithObjectContext(context);
		}

		// Token: 0x060020B1 RID: 8369 RVA: 0x0005C8FA File Offset: 0x0005AAFA
		protected override DbInterceptionContext Clone()
		{
			return new DbTransactionInterceptionContext<TResult>(this);
		}

		// Token: 0x060020B2 RID: 8370 RVA: 0x0005C902 File Offset: 0x0005AB02
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x060020B3 RID: 8371 RVA: 0x0005C90A File Offset: 0x0005AB0A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x060020B4 RID: 8372 RVA: 0x0005C913 File Offset: 0x0005AB13
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060020B5 RID: 8373 RVA: 0x0005C91B File Offset: 0x0005AB1B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}
	}
}
