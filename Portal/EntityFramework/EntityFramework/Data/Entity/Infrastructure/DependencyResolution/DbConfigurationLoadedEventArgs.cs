using System;
using System.ComponentModel;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Infrastructure.DependencyResolution
{
	// Token: 0x020002AD RID: 685
	public class DbConfigurationLoadedEventArgs : EventArgs
	{
		// Token: 0x060021A8 RID: 8616 RVA: 0x0005E77F File Offset: 0x0005C97F
		internal DbConfigurationLoadedEventArgs(InternalConfiguration configuration)
		{
			this._internalConfiguration = configuration;
		}

		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x060021A9 RID: 8617 RVA: 0x0005E78E File Offset: 0x0005C98E
		public IDbDependencyResolver DependencyResolver
		{
			get
			{
				return this._internalConfiguration.ResolverSnapshot;
			}
		}

		// Token: 0x060021AA RID: 8618 RVA: 0x0005E79B File Offset: 0x0005C99B
		public void AddDependencyResolver(IDbDependencyResolver resolver, bool overrideConfigFile)
		{
			Check.NotNull<IDbDependencyResolver>(resolver, "resolver");
			this._internalConfiguration.CheckNotLocked("AddDependencyResolver");
			this._internalConfiguration.AddDependencyResolver(resolver, overrideConfigFile);
		}

		// Token: 0x060021AB RID: 8619 RVA: 0x0005E7C6 File Offset: 0x0005C9C6
		public void AddDefaultResolver(IDbDependencyResolver resolver)
		{
			Check.NotNull<IDbDependencyResolver>(resolver, "resolver");
			this._internalConfiguration.CheckNotLocked("AddDefaultResolver");
			this._internalConfiguration.AddDefaultResolver(resolver);
		}

		// Token: 0x060021AC RID: 8620 RVA: 0x0005E7F0 File Offset: 0x0005C9F0
		public void ReplaceService<TService>(Func<TService, object, TService> serviceInterceptor)
		{
			Check.NotNull<Func<TService, object, TService>>(serviceInterceptor, "serviceInterceptor");
			this.AddDependencyResolver(new WrappingDependencyResolver<TService>(this.DependencyResolver, serviceInterceptor), true);
		}

		// Token: 0x060021AD RID: 8621 RVA: 0x0005E811 File Offset: 0x0005CA11
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x060021AE RID: 8622 RVA: 0x0005E819 File Offset: 0x0005CA19
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x060021AF RID: 8623 RVA: 0x0005E822 File Offset: 0x0005CA22
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060021B0 RID: 8624 RVA: 0x0005E82A File Offset: 0x0005CA2A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x04000BB9 RID: 3001
		private readonly InternalConfiguration _internalConfiguration;
	}
}
