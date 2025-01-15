using System;
using System.ComponentModel;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Infrastructure.Interception
{
	// Token: 0x02000281 RID: 641
	public class DbConnectionInterceptionContext : MutableInterceptionContext
	{
		// Token: 0x06002054 RID: 8276 RVA: 0x0005BE2D File Offset: 0x0005A02D
		public DbConnectionInterceptionContext()
		{
		}

		// Token: 0x06002055 RID: 8277 RVA: 0x0005BE35 File Offset: 0x0005A035
		public DbConnectionInterceptionContext(DbInterceptionContext copyFrom)
			: base(copyFrom)
		{
			Check.NotNull<DbInterceptionContext>(copyFrom, "copyFrom");
		}

		// Token: 0x06002056 RID: 8278 RVA: 0x0005BE4A File Offset: 0x0005A04A
		public new DbConnectionInterceptionContext AsAsync()
		{
			return (DbConnectionInterceptionContext)base.AsAsync();
		}

		// Token: 0x06002057 RID: 8279 RVA: 0x0005BE57 File Offset: 0x0005A057
		public new DbConnectionInterceptionContext WithDbContext(DbContext context)
		{
			Check.NotNull<DbContext>(context, "context");
			return (DbConnectionInterceptionContext)base.WithDbContext(context);
		}

		// Token: 0x06002058 RID: 8280 RVA: 0x0005BE71 File Offset: 0x0005A071
		public new DbConnectionInterceptionContext WithObjectContext(ObjectContext context)
		{
			Check.NotNull<ObjectContext>(context, "context");
			return (DbConnectionInterceptionContext)base.WithObjectContext(context);
		}

		// Token: 0x06002059 RID: 8281 RVA: 0x0005BE8B File Offset: 0x0005A08B
		protected override DbInterceptionContext Clone()
		{
			return new DbConnectionInterceptionContext(this);
		}

		// Token: 0x0600205A RID: 8282 RVA: 0x0005BE93 File Offset: 0x0005A093
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x0600205B RID: 8283 RVA: 0x0005BE9B File Offset: 0x0005A09B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x0600205C RID: 8284 RVA: 0x0005BEA4 File Offset: 0x0005A0A4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600205D RID: 8285 RVA: 0x0005BEAC File Offset: 0x0005A0AC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}
	}
}
