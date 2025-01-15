using System;
using System.ComponentModel;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Utilities;
using System.Transactions;

namespace System.Data.Entity.Infrastructure.Interception
{
	// Token: 0x0200028A RID: 650
	public class EnlistTransactionInterceptionContext : DbConnectionInterceptionContext
	{
		// Token: 0x060020B6 RID: 8374 RVA: 0x0005C923 File Offset: 0x0005AB23
		public EnlistTransactionInterceptionContext()
		{
		}

		// Token: 0x060020B7 RID: 8375 RVA: 0x0005C92C File Offset: 0x0005AB2C
		public EnlistTransactionInterceptionContext(DbInterceptionContext copyFrom)
			: base(copyFrom)
		{
			Check.NotNull<DbInterceptionContext>(copyFrom, "copyFrom");
			EnlistTransactionInterceptionContext enlistTransactionInterceptionContext = copyFrom as EnlistTransactionInterceptionContext;
			if (enlistTransactionInterceptionContext != null)
			{
				this._transaction = enlistTransactionInterceptionContext._transaction;
			}
		}

		// Token: 0x060020B8 RID: 8376 RVA: 0x0005C962 File Offset: 0x0005AB62
		public new EnlistTransactionInterceptionContext AsAsync()
		{
			return (EnlistTransactionInterceptionContext)base.AsAsync();
		}

		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x060020B9 RID: 8377 RVA: 0x0005C96F File Offset: 0x0005AB6F
		public Transaction Transaction
		{
			get
			{
				return this._transaction;
			}
		}

		// Token: 0x060020BA RID: 8378 RVA: 0x0005C977 File Offset: 0x0005AB77
		public EnlistTransactionInterceptionContext WithTransaction(Transaction transaction)
		{
			EnlistTransactionInterceptionContext enlistTransactionInterceptionContext = this.TypedClone();
			enlistTransactionInterceptionContext._transaction = transaction;
			return enlistTransactionInterceptionContext;
		}

		// Token: 0x060020BB RID: 8379 RVA: 0x0005C986 File Offset: 0x0005AB86
		private EnlistTransactionInterceptionContext TypedClone()
		{
			return (EnlistTransactionInterceptionContext)this.Clone();
		}

		// Token: 0x060020BC RID: 8380 RVA: 0x0005C993 File Offset: 0x0005AB93
		protected override DbInterceptionContext Clone()
		{
			return new EnlistTransactionInterceptionContext(this);
		}

		// Token: 0x060020BD RID: 8381 RVA: 0x0005C99B File Offset: 0x0005AB9B
		public new EnlistTransactionInterceptionContext WithDbContext(DbContext context)
		{
			Check.NotNull<DbContext>(context, "context");
			return (EnlistTransactionInterceptionContext)base.WithDbContext(context);
		}

		// Token: 0x060020BE RID: 8382 RVA: 0x0005C9B5 File Offset: 0x0005ABB5
		public new EnlistTransactionInterceptionContext WithObjectContext(ObjectContext context)
		{
			Check.NotNull<ObjectContext>(context, "context");
			return (EnlistTransactionInterceptionContext)base.WithObjectContext(context);
		}

		// Token: 0x060020BF RID: 8383 RVA: 0x0005C9CF File Offset: 0x0005ABCF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x060020C0 RID: 8384 RVA: 0x0005C9D7 File Offset: 0x0005ABD7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x060020C1 RID: 8385 RVA: 0x0005C9E0 File Offset: 0x0005ABE0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060020C2 RID: 8386 RVA: 0x0005C9E8 File Offset: 0x0005ABE8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x04000B89 RID: 2953
		private Transaction _transaction;
	}
}
