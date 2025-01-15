using System;
using System.ComponentModel;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Migrations
{
	// Token: 0x0200009F RID: 159
	public class DbMigrationsConfiguration<TContext> : DbMigrationsConfiguration where TContext : DbContext
	{
		// Token: 0x06000E7A RID: 3706 RVA: 0x0001D313 File Offset: 0x0001B513
		static DbMigrationsConfiguration()
		{
			DbConfigurationManager.Instance.EnsureLoadedForContext(typeof(TContext));
		}

		// Token: 0x06000E7B RID: 3707 RVA: 0x0001D329 File Offset: 0x0001B529
		public DbMigrationsConfiguration()
		{
			base.ContextType = typeof(TContext);
			base.MigrationsAssembly = this.GetType().Assembly();
			base.MigrationsNamespace = this.GetType().Namespace;
		}

		// Token: 0x06000E7C RID: 3708 RVA: 0x0001D363 File Offset: 0x0001B563
		protected virtual void Seed(TContext context)
		{
			Check.NotNull<TContext>(context, "context");
		}

		// Token: 0x06000E7D RID: 3709 RVA: 0x0001D371 File Offset: 0x0001B571
		internal override void OnSeed(DbContext context)
		{
			this.Seed((TContext)((object)context));
		}

		// Token: 0x06000E7E RID: 3710 RVA: 0x0001D37F File Offset: 0x0001B57F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06000E7F RID: 3711 RVA: 0x0001D387 File Offset: 0x0001B587
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06000E80 RID: 3712 RVA: 0x0001D390 File Offset: 0x0001B590
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000E81 RID: 3713 RVA: 0x0001D398 File Offset: 0x0001B598
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x06000E82 RID: 3714 RVA: 0x0001D3A0 File Offset: 0x0001B5A0
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected new object MemberwiseClone()
		{
			return base.MemberwiseClone();
		}
	}
}
