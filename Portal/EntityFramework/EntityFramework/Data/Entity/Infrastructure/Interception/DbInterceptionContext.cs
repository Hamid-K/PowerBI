using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.Infrastructure.Interception
{
	// Token: 0x02000286 RID: 646
	public class DbInterceptionContext
	{
		// Token: 0x06002085 RID: 8325 RVA: 0x0005C1F5 File Offset: 0x0005A3F5
		public DbInterceptionContext()
		{
			this._dbContexts = new List<DbContext>();
			this._objectContexts = new List<ObjectContext>();
		}

		// Token: 0x06002086 RID: 8326 RVA: 0x0005C214 File Offset: 0x0005A414
		protected DbInterceptionContext(DbInterceptionContext copyFrom)
		{
			Check.NotNull<DbInterceptionContext>(copyFrom, "copyFrom");
			this._dbContexts = copyFrom.DbContexts.Where((DbContext c) => c.InternalContext == null || !c.InternalContext.IsDisposed).ToList<DbContext>();
			this._objectContexts = copyFrom.ObjectContexts.Where((ObjectContext c) => !c.IsDisposed).ToList<ObjectContext>();
			this._isAsync = copyFrom._isAsync;
		}

		// Token: 0x06002087 RID: 8327 RVA: 0x0005C2AC File Offset: 0x0005A4AC
		private DbInterceptionContext(IEnumerable<DbInterceptionContext> copyFrom)
		{
			this._dbContexts = (from c in copyFrom.SelectMany((DbInterceptionContext c) => c.DbContexts).Distinct<DbContext>()
				where !c.InternalContext.IsDisposed
				select c).ToList<DbContext>();
			this._objectContexts = (from c in copyFrom.SelectMany((DbInterceptionContext c) => c.ObjectContexts).Distinct<ObjectContext>()
				where !c.IsDisposed
				select c).ToList<ObjectContext>();
			this._isAsync = copyFrom.Any((DbInterceptionContext c) => c.IsAsync);
		}

		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x06002088 RID: 8328 RVA: 0x0005C39C File Offset: 0x0005A59C
		public IEnumerable<DbContext> DbContexts
		{
			get
			{
				return this._dbContexts;
			}
		}

		// Token: 0x06002089 RID: 8329 RVA: 0x0005C3A4 File Offset: 0x0005A5A4
		public DbInterceptionContext WithDbContext(DbContext context)
		{
			Check.NotNull<DbContext>(context, "context");
			DbInterceptionContext dbInterceptionContext = this.Clone();
			if (!dbInterceptionContext._dbContexts.Contains(context, ObjectReferenceEqualityComparer.Default))
			{
				dbInterceptionContext._dbContexts.Add(context);
			}
			return dbInterceptionContext;
		}

		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x0600208A RID: 8330 RVA: 0x0005C3E4 File Offset: 0x0005A5E4
		public IEnumerable<ObjectContext> ObjectContexts
		{
			get
			{
				return this._objectContexts;
			}
		}

		// Token: 0x0600208B RID: 8331 RVA: 0x0005C3EC File Offset: 0x0005A5EC
		public DbInterceptionContext WithObjectContext(ObjectContext context)
		{
			Check.NotNull<ObjectContext>(context, "context");
			DbInterceptionContext dbInterceptionContext = this.Clone();
			if (!dbInterceptionContext._objectContexts.Contains(context, ObjectReferenceEqualityComparer.Default))
			{
				dbInterceptionContext._objectContexts.Add(context);
			}
			return dbInterceptionContext;
		}

		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x0600208C RID: 8332 RVA: 0x0005C42C File Offset: 0x0005A62C
		public bool IsAsync
		{
			get
			{
				return this._isAsync;
			}
		}

		// Token: 0x0600208D RID: 8333 RVA: 0x0005C434 File Offset: 0x0005A634
		public DbInterceptionContext AsAsync()
		{
			DbInterceptionContext dbInterceptionContext = this.Clone();
			dbInterceptionContext._isAsync = true;
			return dbInterceptionContext;
		}

		// Token: 0x0600208E RID: 8334 RVA: 0x0005C443 File Offset: 0x0005A643
		protected virtual DbInterceptionContext Clone()
		{
			return new DbInterceptionContext(this);
		}

		// Token: 0x0600208F RID: 8335 RVA: 0x0005C44B File Offset: 0x0005A64B
		internal static DbInterceptionContext Combine(IEnumerable<DbInterceptionContext> interceptionContexts)
		{
			return new DbInterceptionContext(interceptionContexts);
		}

		// Token: 0x06002090 RID: 8336 RVA: 0x0005C453 File Offset: 0x0005A653
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06002091 RID: 8337 RVA: 0x0005C45B File Offset: 0x0005A65B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06002092 RID: 8338 RVA: 0x0005C464 File Offset: 0x0005A664
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06002093 RID: 8339 RVA: 0x0005C46C File Offset: 0x0005A66C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x04000B84 RID: 2948
		private readonly IList<DbContext> _dbContexts;

		// Token: 0x04000B85 RID: 2949
		private readonly IList<ObjectContext> _objectContexts;

		// Token: 0x04000B86 RID: 2950
		private bool _isAsync;
	}
}
