using System;
using System.ComponentModel;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Infrastructure.Interception
{
	// Token: 0x02000279 RID: 633
	public class DbCommandInterceptionContext : DbInterceptionContext
	{
		// Token: 0x06001FEE RID: 8174 RVA: 0x0005B1A7 File Offset: 0x000593A7
		public DbCommandInterceptionContext()
		{
		}

		// Token: 0x06001FEF RID: 8175 RVA: 0x0005B1B0 File Offset: 0x000593B0
		public DbCommandInterceptionContext(DbInterceptionContext copyFrom)
			: base(copyFrom)
		{
			Check.NotNull<DbInterceptionContext>(copyFrom, "copyFrom");
			DbCommandInterceptionContext dbCommandInterceptionContext = copyFrom as DbCommandInterceptionContext;
			if (dbCommandInterceptionContext != null)
			{
				this._commandBehavior = dbCommandInterceptionContext._commandBehavior;
			}
		}

		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x06001FF0 RID: 8176 RVA: 0x0005B1E6 File Offset: 0x000593E6
		public CommandBehavior CommandBehavior
		{
			get
			{
				return this._commandBehavior;
			}
		}

		// Token: 0x06001FF1 RID: 8177 RVA: 0x0005B1EE File Offset: 0x000593EE
		public DbCommandInterceptionContext WithCommandBehavior(CommandBehavior commandBehavior)
		{
			DbCommandInterceptionContext dbCommandInterceptionContext = this.TypedClone();
			dbCommandInterceptionContext._commandBehavior = commandBehavior;
			return dbCommandInterceptionContext;
		}

		// Token: 0x06001FF2 RID: 8178 RVA: 0x0005B1FD File Offset: 0x000593FD
		private DbCommandInterceptionContext TypedClone()
		{
			return (DbCommandInterceptionContext)this.Clone();
		}

		// Token: 0x06001FF3 RID: 8179 RVA: 0x0005B20A File Offset: 0x0005940A
		protected override DbInterceptionContext Clone()
		{
			return new DbCommandInterceptionContext(this);
		}

		// Token: 0x06001FF4 RID: 8180 RVA: 0x0005B212 File Offset: 0x00059412
		public new DbCommandInterceptionContext WithDbContext(DbContext context)
		{
			Check.NotNull<DbContext>(context, "context");
			return (DbCommandInterceptionContext)base.WithDbContext(context);
		}

		// Token: 0x06001FF5 RID: 8181 RVA: 0x0005B22C File Offset: 0x0005942C
		public new DbCommandInterceptionContext WithObjectContext(ObjectContext context)
		{
			Check.NotNull<ObjectContext>(context, "context");
			return (DbCommandInterceptionContext)base.WithObjectContext(context);
		}

		// Token: 0x06001FF6 RID: 8182 RVA: 0x0005B246 File Offset: 0x00059446
		public new DbCommandInterceptionContext AsAsync()
		{
			return (DbCommandInterceptionContext)base.AsAsync();
		}

		// Token: 0x06001FF7 RID: 8183 RVA: 0x0005B253 File Offset: 0x00059453
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06001FF8 RID: 8184 RVA: 0x0005B25B File Offset: 0x0005945B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06001FF9 RID: 8185 RVA: 0x0005B264 File Offset: 0x00059464
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06001FFA RID: 8186 RVA: 0x0005B26C File Offset: 0x0005946C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x04000B76 RID: 2934
		private CommandBehavior _commandBehavior;
	}
}
