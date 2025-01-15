using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Owin.Hosting.Services
{
	// Token: 0x0200001A RID: 26
	public class ServiceProvider : IServiceProvider
	{
		// Token: 0x06000075 RID: 117 RVA: 0x00003879 File Offset: 0x00001A79
		public ServiceProvider()
		{
			this._services[typeof(IServiceProvider)] = () => this;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000038B8 File Offset: 0x00001AB8
		public virtual object GetService(Type serviceType)
		{
			return this.GetSingleService(serviceType) ?? this.GetMultiService(serviceType);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000038CC File Offset: 0x00001ACC
		private object GetSingleService(Type serviceType)
		{
			Func<object> serviceFactory;
			if (!this._services.TryGetValue(serviceType, out serviceFactory))
			{
				return null;
			}
			return serviceFactory();
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000038F4 File Offset: 0x00001AF4
		private object GetMultiService(Type collectionType)
		{
			if (collectionType.IsGenericType && collectionType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
			{
				Type serviceType = collectionType.GetGenericArguments().Single<Type>();
				Type listType = typeof(List<>).MakeGenericType(new Type[] { serviceType });
				IList services = (IList)Activator.CreateInstance(listType);
				Func<object> serviceFactory;
				if (this._services.TryGetValue(serviceType, out serviceFactory))
				{
					services.Add(serviceFactory());
					List<Func<object>> prior;
					if (this._priorServices.TryGetValue(serviceType, out prior))
					{
						foreach (Func<object> factory in prior)
						{
							services.Add(factory());
						}
					}
				}
				return services;
			}
			return null;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000039D4 File Offset: 0x00001BD4
		public virtual ServiceProvider RemoveAll<T>()
		{
			return this.RemoveAll(typeof(T));
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000039E6 File Offset: 0x00001BE6
		public virtual ServiceProvider RemoveAll(Type type)
		{
			this._services.Remove(type);
			this._priorServices.Remove(type);
			return this;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003A03 File Offset: 0x00001C03
		public virtual ServiceProvider AddInstance<TService>(object instance)
		{
			return this.AddInstance(typeof(TService), instance);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003A18 File Offset: 0x00001C18
		public virtual ServiceProvider AddInstance(Type service, object instance)
		{
			return this.Add(service, () => instance);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003A45 File Offset: 0x00001C45
		public virtual ServiceProvider Add<TService, TImplementation>()
		{
			return this.Add(typeof(TService), typeof(TImplementation));
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003A64 File Offset: 0x00001C64
		public virtual ServiceProvider Add(Type serviceType, Type implementationType)
		{
			Func<IServiceProvider, object> factory = ActivatorUtilities.CreateFactory(implementationType);
			return this.Add(serviceType, () => factory(this));
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003AA0 File Offset: 0x00001CA0
		public virtual ServiceProvider Add(Type serviceType, Func<object> serviceFactory)
		{
			Func<object> existing;
			if (this._services.TryGetValue(serviceType, out existing))
			{
				List<Func<object>> prior;
				if (this._priorServices.TryGetValue(serviceType, out prior))
				{
					prior.Add(existing);
				}
				else
				{
					prior = new List<Func<object>> { existing };
					this._priorServices.Add(serviceType, prior);
				}
			}
			this._services[serviceType] = serviceFactory;
			return this;
		}

		// Token: 0x04000034 RID: 52
		private readonly IDictionary<Type, Func<object>> _services = new Dictionary<Type, Func<object>>();

		// Token: 0x04000035 RID: 53
		private readonly IDictionary<Type, List<Func<object>>> _priorServices = new Dictionary<Type, List<Func<object>>>();
	}
}
