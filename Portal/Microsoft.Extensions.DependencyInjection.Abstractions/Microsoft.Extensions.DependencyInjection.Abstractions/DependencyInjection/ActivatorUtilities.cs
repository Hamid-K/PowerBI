using System;
using Microsoft.Extensions.Internal;

namespace Microsoft.Extensions.DependencyInjection
{
	// Token: 0x02000002 RID: 2
	public static class ActivatorUtilities
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public static object CreateInstance(IServiceProvider provider, Type instanceType, params object[] parameters)
		{
			return ActivatorUtilities.CreateInstance(provider, instanceType, parameters);
		}

		// Token: 0x06000002 RID: 2 RVA: 0x0000205A File Offset: 0x0000025A
		public static ObjectFactory CreateFactory(Type instanceType, Type[] argumentTypes)
		{
			return new ObjectFactory(ActivatorUtilities.CreateFactory(instanceType, argumentTypes).Invoke);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x0000206E File Offset: 0x0000026E
		public static T CreateInstance<T>(IServiceProvider provider, params object[] parameters)
		{
			return ActivatorUtilities.CreateInstance<T>(provider, parameters);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002077 File Offset: 0x00000277
		public static T GetServiceOrCreateInstance<T>(IServiceProvider provider)
		{
			return ActivatorUtilities.GetServiceOrCreateInstance<T>(provider);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000207F File Offset: 0x0000027F
		public static object GetServiceOrCreateInstance(IServiceProvider provider, Type type)
		{
			return ActivatorUtilities.GetServiceOrCreateInstance(provider, type);
		}
	}
}
