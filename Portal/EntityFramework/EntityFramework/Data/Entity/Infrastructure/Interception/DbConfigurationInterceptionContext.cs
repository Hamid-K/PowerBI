using System;
using System.ComponentModel;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Infrastructure.Interception
{
	// Token: 0x0200027F RID: 639
	public class DbConfigurationInterceptionContext : DbInterceptionContext
	{
		// Token: 0x06002037 RID: 8247 RVA: 0x0005B5DC File Offset: 0x000597DC
		public DbConfigurationInterceptionContext()
		{
		}

		// Token: 0x06002038 RID: 8248 RVA: 0x0005B5E4 File Offset: 0x000597E4
		public DbConfigurationInterceptionContext(DbInterceptionContext copyFrom)
			: base(copyFrom)
		{
			Check.NotNull<DbInterceptionContext>(copyFrom, "copyFrom");
		}

		// Token: 0x06002039 RID: 8249 RVA: 0x0005B5F9 File Offset: 0x000597F9
		protected override DbInterceptionContext Clone()
		{
			return new DbConfigurationInterceptionContext(this);
		}

		// Token: 0x0600203A RID: 8250 RVA: 0x0005B601 File Offset: 0x00059801
		public new DbConfigurationInterceptionContext WithDbContext(DbContext context)
		{
			Check.NotNull<DbContext>(context, "context");
			return (DbConfigurationInterceptionContext)base.WithDbContext(context);
		}

		// Token: 0x0600203B RID: 8251 RVA: 0x0005B61B File Offset: 0x0005981B
		public new DbConfigurationInterceptionContext WithObjectContext(ObjectContext context)
		{
			Check.NotNull<ObjectContext>(context, "context");
			return (DbConfigurationInterceptionContext)base.WithObjectContext(context);
		}

		// Token: 0x0600203C RID: 8252 RVA: 0x0005B635 File Offset: 0x00059835
		public new DbConfigurationInterceptionContext AsAsync()
		{
			return (DbConfigurationInterceptionContext)base.AsAsync();
		}

		// Token: 0x0600203D RID: 8253 RVA: 0x0005B642 File Offset: 0x00059842
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x0600203E RID: 8254 RVA: 0x0005B64A File Offset: 0x0005984A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x0600203F RID: 8255 RVA: 0x0005B653 File Offset: 0x00059853
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06002040 RID: 8256 RVA: 0x0005B65B File Offset: 0x0005985B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}
	}
}
