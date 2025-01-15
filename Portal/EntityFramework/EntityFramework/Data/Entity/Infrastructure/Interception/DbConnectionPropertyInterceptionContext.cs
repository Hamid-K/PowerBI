using System;
using System.ComponentModel;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Infrastructure.Interception
{
	// Token: 0x02000283 RID: 643
	public class DbConnectionPropertyInterceptionContext<TValue> : PropertyInterceptionContext<TValue>
	{
		// Token: 0x06002068 RID: 8296 RVA: 0x0005BF3B File Offset: 0x0005A13B
		public DbConnectionPropertyInterceptionContext()
		{
		}

		// Token: 0x06002069 RID: 8297 RVA: 0x0005BF43 File Offset: 0x0005A143
		public DbConnectionPropertyInterceptionContext(DbInterceptionContext copyFrom)
			: base(copyFrom)
		{
			Check.NotNull<DbInterceptionContext>(copyFrom, "copyFrom");
		}

		// Token: 0x0600206A RID: 8298 RVA: 0x0005BF58 File Offset: 0x0005A158
		public new DbConnectionPropertyInterceptionContext<TValue> WithValue(TValue value)
		{
			return (DbConnectionPropertyInterceptionContext<TValue>)base.WithValue(value);
		}

		// Token: 0x0600206B RID: 8299 RVA: 0x0005BF66 File Offset: 0x0005A166
		protected override DbInterceptionContext Clone()
		{
			return new DbConnectionPropertyInterceptionContext<TValue>(this);
		}

		// Token: 0x0600206C RID: 8300 RVA: 0x0005BF6E File Offset: 0x0005A16E
		public new DbConnectionPropertyInterceptionContext<TValue> AsAsync()
		{
			return (DbConnectionPropertyInterceptionContext<TValue>)base.AsAsync();
		}

		// Token: 0x0600206D RID: 8301 RVA: 0x0005BF7B File Offset: 0x0005A17B
		public new DbConnectionPropertyInterceptionContext<TValue> WithDbContext(DbContext context)
		{
			Check.NotNull<DbContext>(context, "context");
			return (DbConnectionPropertyInterceptionContext<TValue>)base.WithDbContext(context);
		}

		// Token: 0x0600206E RID: 8302 RVA: 0x0005BF95 File Offset: 0x0005A195
		public new DbConnectionPropertyInterceptionContext<TValue> WithObjectContext(ObjectContext context)
		{
			Check.NotNull<ObjectContext>(context, "context");
			return (DbConnectionPropertyInterceptionContext<TValue>)base.WithObjectContext(context);
		}

		// Token: 0x0600206F RID: 8303 RVA: 0x0005BFAF File Offset: 0x0005A1AF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06002070 RID: 8304 RVA: 0x0005BFB7 File Offset: 0x0005A1B7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06002071 RID: 8305 RVA: 0x0005BFC0 File Offset: 0x0005A1C0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06002072 RID: 8306 RVA: 0x0005BFC8 File Offset: 0x0005A1C8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}
	}
}
