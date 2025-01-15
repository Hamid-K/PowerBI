using System;
using System.ComponentModel;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Infrastructure.Interception
{
	// Token: 0x02000282 RID: 642
	public class DbConnectionInterceptionContext<TResult> : MutableInterceptionContext<TResult>
	{
		// Token: 0x0600205E RID: 8286 RVA: 0x0005BEB4 File Offset: 0x0005A0B4
		public DbConnectionInterceptionContext()
		{
		}

		// Token: 0x0600205F RID: 8287 RVA: 0x0005BEBC File Offset: 0x0005A0BC
		public DbConnectionInterceptionContext(DbInterceptionContext copyFrom)
			: base(copyFrom)
		{
			Check.NotNull<DbInterceptionContext>(copyFrom, "copyFrom");
		}

		// Token: 0x06002060 RID: 8288 RVA: 0x0005BED1 File Offset: 0x0005A0D1
		public new DbConnectionInterceptionContext<TResult> AsAsync()
		{
			return (DbConnectionInterceptionContext<TResult>)base.AsAsync();
		}

		// Token: 0x06002061 RID: 8289 RVA: 0x0005BEDE File Offset: 0x0005A0DE
		public new DbConnectionInterceptionContext<TResult> WithDbContext(DbContext context)
		{
			Check.NotNull<DbContext>(context, "context");
			return (DbConnectionInterceptionContext<TResult>)base.WithDbContext(context);
		}

		// Token: 0x06002062 RID: 8290 RVA: 0x0005BEF8 File Offset: 0x0005A0F8
		public new DbConnectionInterceptionContext<TResult> WithObjectContext(ObjectContext context)
		{
			Check.NotNull<ObjectContext>(context, "context");
			return (DbConnectionInterceptionContext<TResult>)base.WithObjectContext(context);
		}

		// Token: 0x06002063 RID: 8291 RVA: 0x0005BF12 File Offset: 0x0005A112
		protected override DbInterceptionContext Clone()
		{
			return new DbConnectionInterceptionContext<TResult>(this);
		}

		// Token: 0x06002064 RID: 8292 RVA: 0x0005BF1A File Offset: 0x0005A11A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06002065 RID: 8293 RVA: 0x0005BF22 File Offset: 0x0005A122
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06002066 RID: 8294 RVA: 0x0005BF2B File Offset: 0x0005A12B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06002067 RID: 8295 RVA: 0x0005BF33 File Offset: 0x0005A133
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}
	}
}
