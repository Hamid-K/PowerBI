using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.Identity.Json.Serialization;

namespace Microsoft.Identity.Json.Utilities
{
	// Token: 0x02000061 RID: 97
	internal class LateBoundReflectionDelegateFactory : ReflectionDelegateFactory
	{
		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600055E RID: 1374 RVA: 0x0001721B File Offset: 0x0001541B
		internal static ReflectionDelegateFactory Instance
		{
			get
			{
				return LateBoundReflectionDelegateFactory._instance;
			}
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x00017224 File Offset: 0x00015424
		public override ObjectConstructor<object> CreateParameterizedConstructor(MethodBase method)
		{
			ValidationUtils.ArgumentNotNull(method, "method");
			ConstructorInfo c = method as ConstructorInfo;
			if (c != null)
			{
				return ([Nullable(new byte[] { 0, 2 })] object[] a) => c.Invoke(a);
			}
			return ([Nullable(new byte[] { 0, 2 })] object[] a) => method.Invoke(null, a);
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x00017280 File Offset: 0x00015480
		[return: Nullable(new byte[] { 0, 0, 2 })]
		public override MethodCall<T, object> CreateMethodCall<T>(MethodBase method)
		{
			ValidationUtils.ArgumentNotNull(method, "method");
			ConstructorInfo c = method as ConstructorInfo;
			if (c != null)
			{
				return (T o, [Nullable(new byte[] { 0, 2 })] object[] a) => c.Invoke(a);
			}
			return (T o, [Nullable(new byte[] { 0, 2 })] object[] a) => method.Invoke(o, a);
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x000172DC File Offset: 0x000154DC
		public override Func<T> CreateDefaultConstructor<T>(Type type)
		{
			ValidationUtils.ArgumentNotNull(type, "type");
			if (type.IsValueType())
			{
				return () => (T)((object)Activator.CreateInstance(type));
			}
			ConstructorInfo constructorInfo = ReflectionUtils.GetDefaultConstructor(type, true);
			return () => (T)((object)constructorInfo.Invoke(null));
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x0001733E File Offset: 0x0001553E
		[return: Nullable(new byte[] { 0, 0, 2 })]
		public override Func<T, object> CreateGet<T>(PropertyInfo propertyInfo)
		{
			ValidationUtils.ArgumentNotNull(propertyInfo, "propertyInfo");
			return (T o) => propertyInfo.GetValue(o, null);
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x00017367 File Offset: 0x00015567
		[return: Nullable(new byte[] { 0, 0, 2 })]
		public override Func<T, object> CreateGet<T>(FieldInfo fieldInfo)
		{
			ValidationUtils.ArgumentNotNull(fieldInfo, "fieldInfo");
			return (T o) => fieldInfo.GetValue(o);
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x00017390 File Offset: 0x00015590
		[return: Nullable(new byte[] { 0, 0, 2 })]
		public override Action<T, object> CreateSet<T>(FieldInfo fieldInfo)
		{
			ValidationUtils.ArgumentNotNull(fieldInfo, "fieldInfo");
			return delegate(T o, [Nullable(2)] object v)
			{
				fieldInfo.SetValue(o, v);
			};
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x000173B9 File Offset: 0x000155B9
		[return: Nullable(new byte[] { 0, 0, 2 })]
		public override Action<T, object> CreateSet<T>(PropertyInfo propertyInfo)
		{
			ValidationUtils.ArgumentNotNull(propertyInfo, "propertyInfo");
			return delegate(T o, [Nullable(2)] object v)
			{
				propertyInfo.SetValue(o, v, null);
			};
		}

		// Token: 0x040001FB RID: 507
		private static readonly LateBoundReflectionDelegateFactory _instance = new LateBoundReflectionDelegateFactory();
	}
}
