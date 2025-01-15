using System;
using System.Linq;
using System.Reflection;

namespace Microsoft.Owin.Hosting.Services
{
	// Token: 0x02000019 RID: 25
	public static class ActivatorUtilities
	{
		// Token: 0x06000070 RID: 112 RVA: 0x00003780 File Offset: 0x00001980
		public static object GetServiceOrCreateInstance(IServiceProvider services, Type type)
		{
			return ActivatorUtilities.GetServiceNoExceptions(services, type) ?? ActivatorUtilities.CreateInstance(services, type);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003794 File Offset: 0x00001994
		private static object GetServiceNoExceptions(IServiceProvider services, Type type)
		{
			object obj;
			try
			{
				obj = services.GetService(type);
			}
			catch
			{
				obj = null;
			}
			return obj;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000037C4 File Offset: 0x000019C4
		public static object CreateInstance(IServiceProvider services, Type type)
		{
			return ActivatorUtilities.CreateFactory(type)(services);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000037D4 File Offset: 0x000019D4
		public static Func<IServiceProvider, object> CreateFactory(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			ConstructorInfo[] constructors = type.GetConstructors().Where(new Func<ConstructorInfo, bool>(ActivatorUtilities.IsInjectable)).ToArray<ConstructorInfo>();
			if (constructors.Length == 1)
			{
				ParameterInfo[] parameters = constructors[0].GetParameters();
				return delegate(IServiceProvider services)
				{
					object[] args = new object[parameters.Length];
					for (int index = 0; index != parameters.Length; index++)
					{
						args[index] = services.GetService(parameters[index].ParameterType);
					}
					return Activator.CreateInstance(type, args);
				};
			}
			return (IServiceProvider _) => Activator.CreateInstance(type);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003863 File Offset: 0x00001A63
		private static bool IsInjectable(ConstructorInfo constructor)
		{
			return constructor.IsPublic && constructor.GetParameters().Length != 0;
		}
	}
}
