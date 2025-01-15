using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.Internal;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.Infrastructure.DependencyResolution
{
	// Token: 0x020002B7 RID: 695
	internal class InternalConfiguration
	{
		// Token: 0x060021DD RID: 8669 RVA: 0x0005F30C File Offset: 0x0005D50C
		public InternalConfiguration(ResolverChain appConfigChain = null, ResolverChain normalResolverChain = null, RootDependencyResolver rootResolver = null, AppConfigDependencyResolver appConfigResolver = null, Func<DbDispatchers> dispatchers = null)
		{
			this._rootResolver = rootResolver ?? new RootDependencyResolver();
			this._resolvers = new CompositeResolver<ResolverChain, ResolverChain>(appConfigChain ?? new ResolverChain(), normalResolverChain ?? new ResolverChain());
			this._resolvers.Second.Add(this._rootResolver);
			this._resolvers.First.Add(appConfigResolver ?? new AppConfigDependencyResolver(AppConfig.DefaultInstance, this, null));
			Func<DbDispatchers> func = dispatchers;
			if (dispatchers == null && (func = InternalConfiguration.<>c.<>9__4_0) == null)
			{
				func = (InternalConfiguration.<>c.<>9__4_0 = () => DbInterception.Dispatch);
			}
			this._dispatchers = func;
		}

		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x060021DE RID: 8670 RVA: 0x0005F3B1 File Offset: 0x0005D5B1
		// (set) Token: 0x060021DF RID: 8671 RVA: 0x0005F3BD File Offset: 0x0005D5BD
		public static InternalConfiguration Instance
		{
			get
			{
				return DbConfigurationManager.Instance.GetConfiguration();
			}
			set
			{
				DbConfigurationManager.Instance.SetConfiguration(value);
			}
		}

		// Token: 0x060021E0 RID: 8672 RVA: 0x0005F3CC File Offset: 0x0005D5CC
		public virtual void Lock()
		{
			List<IDbInterceptor> list = this.DependencyResolver.GetServices<IDbInterceptor>().ToList<IDbInterceptor>();
			list.Each(new Action<IDbInterceptor>(this._dispatchers().AddInterceptor));
			DbConfigurationManager.Instance.OnLoaded(this);
			this._isLocked = true;
			this.DependencyResolver.GetServices<IDbInterceptor>().Except(list).Each(new Action<IDbInterceptor>(this._dispatchers().AddInterceptor));
		}

		// Token: 0x060021E1 RID: 8673 RVA: 0x0005F446 File Offset: 0x0005D646
		public void DispatchLoadedInterceptors(DbConfigurationLoadedEventArgs loadedEventArgs)
		{
			this._dispatchers().Configuration.Loaded(loadedEventArgs, new DbInterceptionContext());
		}

		// Token: 0x060021E2 RID: 8674 RVA: 0x0005F463 File Offset: 0x0005D663
		public virtual void AddAppConfigResolver(IDbDependencyResolver resolver)
		{
			this._resolvers.First.Add(resolver);
		}

		// Token: 0x060021E3 RID: 8675 RVA: 0x0005F476 File Offset: 0x0005D676
		public virtual void AddDependencyResolver(IDbDependencyResolver resolver, bool overrideConfigFile = false)
		{
			(overrideConfigFile ? this._resolvers.First : this._resolvers.Second).Add(resolver);
		}

		// Token: 0x060021E4 RID: 8676 RVA: 0x0005F499 File Offset: 0x0005D699
		public virtual void AddDefaultResolver(IDbDependencyResolver resolver)
		{
			this._rootResolver.AddDefaultResolver(resolver);
		}

		// Token: 0x060021E5 RID: 8677 RVA: 0x0005F4A7 File Offset: 0x0005D6A7
		public virtual void SetDefaultProviderServices(DbProviderServices provider, string invariantName)
		{
			this._rootResolver.SetDefaultProviderServices(provider, invariantName);
		}

		// Token: 0x060021E6 RID: 8678 RVA: 0x0005F4B6 File Offset: 0x0005D6B6
		public virtual void RegisterSingleton<TService>(TService instance) where TService : class
		{
			this.AddDependencyResolver(new SingletonDependencyResolver<TService>(instance, null), false);
		}

		// Token: 0x060021E7 RID: 8679 RVA: 0x0005F4C6 File Offset: 0x0005D6C6
		public virtual void RegisterSingleton<TService>(TService instance, object key) where TService : class
		{
			this.AddDependencyResolver(new SingletonDependencyResolver<TService>(instance, key), false);
		}

		// Token: 0x060021E8 RID: 8680 RVA: 0x0005F4D6 File Offset: 0x0005D6D6
		public virtual void RegisterSingleton<TService>(TService instance, Func<object, bool> keyPredicate) where TService : class
		{
			this.AddDependencyResolver(new SingletonDependencyResolver<TService>(instance, keyPredicate), false);
		}

		// Token: 0x060021E9 RID: 8681 RVA: 0x0005F4E6 File Offset: 0x0005D6E6
		public virtual TService GetService<TService>(object key)
		{
			return this._resolvers.GetService(key);
		}

		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x060021EA RID: 8682 RVA: 0x0005F4F4 File Offset: 0x0005D6F4
		public virtual IDbDependencyResolver DependencyResolver
		{
			get
			{
				return this._resolvers;
			}
		}

		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x060021EB RID: 8683 RVA: 0x0005F4FC File Offset: 0x0005D6FC
		public virtual RootDependencyResolver RootResolver
		{
			get
			{
				return this._rootResolver;
			}
		}

		// Token: 0x060021EC RID: 8684 RVA: 0x0005F504 File Offset: 0x0005D704
		public virtual void SwitchInRootResolver(RootDependencyResolver value)
		{
			ResolverChain resolverChain = new ResolverChain();
			resolverChain.Add(value);
			this._resolvers.Second.Resolvers.Skip(1).Each(new Action<IDbDependencyResolver>(resolverChain.Add));
			this._rootResolver = value;
			this._resolvers = new CompositeResolver<ResolverChain, ResolverChain>(this._resolvers.First, resolverChain);
		}

		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x060021ED RID: 8685 RVA: 0x0005F564 File Offset: 0x0005D764
		public virtual IDbDependencyResolver ResolverSnapshot
		{
			get
			{
				ResolverChain resolverChain = new ResolverChain();
				this._resolvers.Second.Resolvers.Each(new Action<IDbDependencyResolver>(resolverChain.Add));
				this._resolvers.First.Resolvers.Each(new Action<IDbDependencyResolver>(resolverChain.Add));
				return resolverChain;
			}
		}

		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x060021EE RID: 8686 RVA: 0x0005F5BC File Offset: 0x0005D7BC
		// (set) Token: 0x060021EF RID: 8687 RVA: 0x0005F5C4 File Offset: 0x0005D7C4
		public virtual DbConfiguration Owner { get; set; }

		// Token: 0x060021F0 RID: 8688 RVA: 0x0005F5CD File Offset: 0x0005D7CD
		public virtual void CheckNotLocked(string memberName)
		{
			if (this._isLocked)
			{
				throw new InvalidOperationException(Strings.ConfigurationLocked(memberName));
			}
		}

		// Token: 0x04000BC7 RID: 3015
		private CompositeResolver<ResolverChain, ResolverChain> _resolvers;

		// Token: 0x04000BC8 RID: 3016
		private RootDependencyResolver _rootResolver;

		// Token: 0x04000BC9 RID: 3017
		private readonly Func<DbDispatchers> _dispatchers;

		// Token: 0x04000BCA RID: 3018
		private bool _isLocked;
	}
}
