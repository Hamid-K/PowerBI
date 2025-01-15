using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection.ServiceLookup
{
	// Token: 0x02000020 RID: 32
	internal class ServiceTable
	{
		// Token: 0x060000C8 RID: 200 RVA: 0x00003AD4 File Offset: 0x00001CD4
		public ServiceTable(IEnumerable<ServiceDescriptor> descriptors)
		{
			this._services = new Dictionary<Type, ServiceEntry>();
			this._genericServices = new Dictionary<Type, List<IGenericService>>();
			foreach (ServiceDescriptor serviceDescriptor in descriptors)
			{
				if (serviceDescriptor.ServiceType.GetTypeInfo().IsGenericTypeDefinition)
				{
					Type implementationType = serviceDescriptor.ImplementationType;
					TypeInfo typeInfo = ((implementationType != null) ? implementationType.GetTypeInfo() : null);
					if (typeInfo == null || !typeInfo.IsGenericTypeDefinition)
					{
						throw new ArgumentException(Resources.FormatOpenGenericServiceRequiresOpenGenericImplementation(serviceDescriptor.ServiceType), "descriptors");
					}
					if (typeInfo.IsAbstract || typeInfo.IsInterface)
					{
						throw new ArgumentException(Resources.FormatTypeCannotBeActivated(serviceDescriptor.ImplementationType, serviceDescriptor.ServiceType));
					}
					this.Add(serviceDescriptor.ServiceType, new GenericService(serviceDescriptor));
				}
				else if (serviceDescriptor.ImplementationInstance != null)
				{
					this.Add(serviceDescriptor.ServiceType, new InstanceService(serviceDescriptor));
				}
				else if (serviceDescriptor.ImplementationFactory != null)
				{
					this.Add(serviceDescriptor.ServiceType, new FactoryService(serviceDescriptor));
				}
				else
				{
					TypeInfo typeInfo2 = serviceDescriptor.ImplementationType.GetTypeInfo();
					if (typeInfo2.IsGenericTypeDefinition || typeInfo2.IsAbstract || typeInfo2.IsInterface)
					{
						throw new ArgumentException(Resources.FormatTypeCannotBeActivated(serviceDescriptor.ImplementationType, serviceDescriptor.ServiceType));
					}
					this.Add(serviceDescriptor.ServiceType, new Service(serviceDescriptor));
				}
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00003C64 File Offset: 0x00001E64
		public ConcurrentDictionary<Type, Func<ServiceProvider, object>> RealizedServices
		{
			get
			{
				return this._realizedServices;
			}
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00003C6C File Offset: 0x00001E6C
		public bool TryGetEntry(Type serviceType, out ServiceEntry entry)
		{
			object sync = this._sync;
			lock (sync)
			{
				if (this._services.TryGetValue(serviceType, out entry))
				{
					return true;
				}
				if (serviceType.GetTypeInfo().IsGenericType)
				{
					Type genericTypeDefinition = serviceType.GetGenericTypeDefinition();
					List<IGenericService> list;
					if (this._genericServices.TryGetValue(genericTypeDefinition, out list))
					{
						foreach (IGenericService genericService in list)
						{
							IService service = genericService.GetService(serviceType);
							if (service != null)
							{
								this.Add(serviceType, service);
							}
						}
						return this._services.TryGetValue(serviceType, out entry);
					}
				}
			}
			return false;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00003D40 File Offset: 0x00001F40
		public void Add(Type serviceType, IService service)
		{
			object sync = this._sync;
			lock (sync)
			{
				ServiceEntry serviceEntry;
				if (this._services.TryGetValue(serviceType, out serviceEntry))
				{
					serviceEntry.Add(service);
				}
				else
				{
					this._services[serviceType] = new ServiceEntry(service);
				}
			}
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00003DA8 File Offset: 0x00001FA8
		public void Add(Type serviceType, IGenericService genericService)
		{
			object sync = this._sync;
			lock (sync)
			{
				List<IGenericService> list;
				if (!this._genericServices.TryGetValue(serviceType, out list))
				{
					list = new List<IGenericService>();
					this._genericServices[serviceType] = list;
				}
				list.Add(genericService);
			}
		}

		// Token: 0x04000035 RID: 53
		private readonly object _sync = new object();

		// Token: 0x04000036 RID: 54
		private readonly Dictionary<Type, ServiceEntry> _services;

		// Token: 0x04000037 RID: 55
		private readonly Dictionary<Type, List<IGenericService>> _genericServices;

		// Token: 0x04000038 RID: 56
		private readonly ConcurrentDictionary<Type, Func<ServiceProvider, object>> _realizedServices = new ConcurrentDictionary<Type, Func<ServiceProvider, object>>();
	}
}
