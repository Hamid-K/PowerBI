using System;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection.ServiceLookup
{
	// Token: 0x02000013 RID: 19
	internal class GenericService : IGenericService
	{
		// Token: 0x06000092 RID: 146 RVA: 0x0000360D File Offset: 0x0000180D
		public GenericService(ServiceDescriptor descriptor)
		{
			this._descriptor = descriptor;
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000093 RID: 147 RVA: 0x0000361C File Offset: 0x0000181C
		public ServiceLifetime Lifetime
		{
			get
			{
				return this._descriptor.Lifetime;
			}
		}

		// Token: 0x06000094 RID: 148 RVA: 0x0000362C File Offset: 0x0000182C
		public IService GetService(Type closedServiceType)
		{
			Type[] genericTypeArguments = closedServiceType.GetTypeInfo().GenericTypeArguments;
			Type type = this._descriptor.ImplementationType.MakeGenericType(genericTypeArguments);
			return new Service(new ServiceDescriptor(closedServiceType, type, this.Lifetime));
		}

		// Token: 0x04000026 RID: 38
		private readonly ServiceDescriptor _descriptor;
	}
}
