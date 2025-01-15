using System;
using System.ComponentModel;
using System.Data.Common;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Infrastructure.Interception
{
	// Token: 0x02000288 RID: 648
	public class DbTransactionInterceptionContext : MutableInterceptionContext
	{
		// Token: 0x0600209F RID: 8351 RVA: 0x0005C7C4 File Offset: 0x0005A9C4
		public DbTransactionInterceptionContext()
		{
		}

		// Token: 0x060020A0 RID: 8352 RVA: 0x0005C7CC File Offset: 0x0005A9CC
		public DbTransactionInterceptionContext(DbInterceptionContext copyFrom)
			: base(copyFrom)
		{
			DbTransactionInterceptionContext dbTransactionInterceptionContext = copyFrom as DbTransactionInterceptionContext;
			if (dbTransactionInterceptionContext != null)
			{
				this._connection = dbTransactionInterceptionContext.Connection;
			}
			Check.NotNull<DbInterceptionContext>(copyFrom, "copyFrom");
		}

		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x060020A1 RID: 8353 RVA: 0x0005C802 File Offset: 0x0005AA02
		public DbConnection Connection
		{
			get
			{
				return this._connection;
			}
		}

		// Token: 0x060020A2 RID: 8354 RVA: 0x0005C80A File Offset: 0x0005AA0A
		public DbTransactionInterceptionContext WithConnection(DbConnection connection)
		{
			Check.NotNull<DbConnection>(connection, "connection");
			DbTransactionInterceptionContext dbTransactionInterceptionContext = this.TypedClone();
			dbTransactionInterceptionContext._connection = connection;
			return dbTransactionInterceptionContext;
		}

		// Token: 0x060020A3 RID: 8355 RVA: 0x0005C825 File Offset: 0x0005AA25
		public new DbTransactionInterceptionContext AsAsync()
		{
			return (DbTransactionInterceptionContext)base.AsAsync();
		}

		// Token: 0x060020A4 RID: 8356 RVA: 0x0005C832 File Offset: 0x0005AA32
		public new DbTransactionInterceptionContext WithDbContext(DbContext context)
		{
			Check.NotNull<DbContext>(context, "context");
			return (DbTransactionInterceptionContext)base.WithDbContext(context);
		}

		// Token: 0x060020A5 RID: 8357 RVA: 0x0005C84C File Offset: 0x0005AA4C
		public new DbTransactionInterceptionContext WithObjectContext(ObjectContext context)
		{
			Check.NotNull<ObjectContext>(context, "context");
			return (DbTransactionInterceptionContext)base.WithObjectContext(context);
		}

		// Token: 0x060020A6 RID: 8358 RVA: 0x0005C866 File Offset: 0x0005AA66
		private DbTransactionInterceptionContext TypedClone()
		{
			return (DbTransactionInterceptionContext)this.Clone();
		}

		// Token: 0x060020A7 RID: 8359 RVA: 0x0005C873 File Offset: 0x0005AA73
		protected override DbInterceptionContext Clone()
		{
			return new DbTransactionInterceptionContext(this);
		}

		// Token: 0x060020A8 RID: 8360 RVA: 0x0005C87B File Offset: 0x0005AA7B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x060020A9 RID: 8361 RVA: 0x0005C883 File Offset: 0x0005AA83
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x060020AA RID: 8362 RVA: 0x0005C88C File Offset: 0x0005AA8C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060020AB RID: 8363 RVA: 0x0005C894 File Offset: 0x0005AA94
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x04000B88 RID: 2952
		private DbConnection _connection;
	}
}
