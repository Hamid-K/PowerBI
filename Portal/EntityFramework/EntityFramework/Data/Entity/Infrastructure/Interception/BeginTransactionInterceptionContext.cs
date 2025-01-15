using System;
using System.ComponentModel;
using System.Data.Common;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Infrastructure.Interception
{
	// Token: 0x02000273 RID: 627
	public class BeginTransactionInterceptionContext : DbConnectionInterceptionContext<DbTransaction>
	{
		// Token: 0x06001F8B RID: 8075 RVA: 0x0005A013 File Offset: 0x00058213
		public BeginTransactionInterceptionContext()
		{
		}

		// Token: 0x06001F8C RID: 8076 RVA: 0x0005A024 File Offset: 0x00058224
		public BeginTransactionInterceptionContext(DbInterceptionContext copyFrom)
			: base(copyFrom)
		{
			Check.NotNull<DbInterceptionContext>(copyFrom, "copyFrom");
			BeginTransactionInterceptionContext beginTransactionInterceptionContext = copyFrom as BeginTransactionInterceptionContext;
			if (beginTransactionInterceptionContext != null)
			{
				this._isolationLevel = beginTransactionInterceptionContext._isolationLevel;
			}
		}

		// Token: 0x06001F8D RID: 8077 RVA: 0x0005A061 File Offset: 0x00058261
		public new BeginTransactionInterceptionContext AsAsync()
		{
			return (BeginTransactionInterceptionContext)base.AsAsync();
		}

		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x06001F8E RID: 8078 RVA: 0x0005A06E File Offset: 0x0005826E
		public IsolationLevel IsolationLevel
		{
			get
			{
				return this._isolationLevel;
			}
		}

		// Token: 0x06001F8F RID: 8079 RVA: 0x0005A076 File Offset: 0x00058276
		public BeginTransactionInterceptionContext WithIsolationLevel(IsolationLevel isolationLevel)
		{
			BeginTransactionInterceptionContext beginTransactionInterceptionContext = this.TypedClone();
			beginTransactionInterceptionContext._isolationLevel = isolationLevel;
			return beginTransactionInterceptionContext;
		}

		// Token: 0x06001F90 RID: 8080 RVA: 0x0005A085 File Offset: 0x00058285
		private BeginTransactionInterceptionContext TypedClone()
		{
			return (BeginTransactionInterceptionContext)this.Clone();
		}

		// Token: 0x06001F91 RID: 8081 RVA: 0x0005A092 File Offset: 0x00058292
		protected override DbInterceptionContext Clone()
		{
			return new BeginTransactionInterceptionContext(this);
		}

		// Token: 0x06001F92 RID: 8082 RVA: 0x0005A09A File Offset: 0x0005829A
		public new BeginTransactionInterceptionContext WithDbContext(DbContext context)
		{
			Check.NotNull<DbContext>(context, "context");
			return (BeginTransactionInterceptionContext)base.WithDbContext(context);
		}

		// Token: 0x06001F93 RID: 8083 RVA: 0x0005A0B4 File Offset: 0x000582B4
		public new BeginTransactionInterceptionContext WithObjectContext(ObjectContext context)
		{
			Check.NotNull<ObjectContext>(context, "context");
			return (BeginTransactionInterceptionContext)base.WithObjectContext(context);
		}

		// Token: 0x06001F94 RID: 8084 RVA: 0x0005A0CE File Offset: 0x000582CE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06001F95 RID: 8085 RVA: 0x0005A0D6 File Offset: 0x000582D6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06001F96 RID: 8086 RVA: 0x0005A0DF File Offset: 0x000582DF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06001F97 RID: 8087 RVA: 0x0005A0E7 File Offset: 0x000582E7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x04000B6B RID: 2923
		private IsolationLevel _isolationLevel = IsolationLevel.Unspecified;
	}
}
