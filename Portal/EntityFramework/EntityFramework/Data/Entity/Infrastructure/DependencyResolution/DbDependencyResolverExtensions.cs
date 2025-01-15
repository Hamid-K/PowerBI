using System;
using System.Collections.Generic;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.Infrastructure.DependencyResolution
{
	// Token: 0x020002B6 RID: 694
	public static class DbDependencyResolverExtensions
	{
		// Token: 0x060021D6 RID: 8662 RVA: 0x0005F20A File Offset: 0x0005D40A
		public static T GetService<T>(this IDbDependencyResolver resolver, object key)
		{
			Check.NotNull<IDbDependencyResolver>(resolver, "resolver");
			return (T)((object)resolver.GetService(typeof(T), key));
		}

		// Token: 0x060021D7 RID: 8663 RVA: 0x0005F22E File Offset: 0x0005D42E
		public static T GetService<T>(this IDbDependencyResolver resolver)
		{
			Check.NotNull<IDbDependencyResolver>(resolver, "resolver");
			return (T)((object)resolver.GetService(typeof(T), null));
		}

		// Token: 0x060021D8 RID: 8664 RVA: 0x0005F252 File Offset: 0x0005D452
		public static object GetService(this IDbDependencyResolver resolver, Type type)
		{
			Check.NotNull<IDbDependencyResolver>(resolver, "resolver");
			Check.NotNull<Type>(type, "type");
			return resolver.GetService(type, null);
		}

		// Token: 0x060021D9 RID: 8665 RVA: 0x0005F274 File Offset: 0x0005D474
		public static IEnumerable<T> GetServices<T>(this IDbDependencyResolver resolver, object key)
		{
			Check.NotNull<IDbDependencyResolver>(resolver, "resolver");
			return resolver.GetServices(typeof(T), key).OfType<T>();
		}

		// Token: 0x060021DA RID: 8666 RVA: 0x0005F298 File Offset: 0x0005D498
		public static IEnumerable<T> GetServices<T>(this IDbDependencyResolver resolver)
		{
			Check.NotNull<IDbDependencyResolver>(resolver, "resolver");
			return resolver.GetServices(typeof(T), null).OfType<T>();
		}

		// Token: 0x060021DB RID: 8667 RVA: 0x0005F2BC File Offset: 0x0005D4BC
		public static IEnumerable<object> GetServices(this IDbDependencyResolver resolver, Type type)
		{
			Check.NotNull<IDbDependencyResolver>(resolver, "resolver");
			Check.NotNull<Type>(type, "type");
			return resolver.GetServices(type, null);
		}

		// Token: 0x060021DC RID: 8668 RVA: 0x0005F2E0 File Offset: 0x0005D4E0
		internal static IEnumerable<object> GetServiceAsServices(this IDbDependencyResolver resolver, Type type, object key)
		{
			object service = resolver.GetService(type, key);
			if (service != null)
			{
				return new object[] { service };
			}
			return Enumerable.Empty<object>();
		}
	}
}
