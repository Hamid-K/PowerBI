using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;

namespace System.Web.Http.Internal
{
	// Token: 0x02000183 RID: 387
	internal static class TypeHelper
	{
		// Token: 0x06000A00 RID: 2560 RVA: 0x00019CE8 File Offset: 0x00017EE8
		internal static Type GetTaskInnerTypeOrNull(Type type)
		{
			if (type.IsGenericType && !type.IsGenericTypeDefinition)
			{
				Type genericTypeDefinition = type.GetGenericTypeDefinition();
				if (TypeHelper.TaskGenericType == genericTypeDefinition)
				{
					return type.GetGenericArguments()[0];
				}
			}
			return null;
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x00019D24 File Offset: 0x00017F24
		internal static Type[] GetTypeArgumentsIfMatch(Type closedType, Type matchingOpenType)
		{
			if (!closedType.IsGenericType)
			{
				return null;
			}
			Type genericTypeDefinition = closedType.GetGenericTypeDefinition();
			if (!(matchingOpenType == genericTypeDefinition))
			{
				return null;
			}
			return closedType.GetGenericArguments();
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x00019D53 File Offset: 0x00017F53
		internal static bool IsCompatibleObject(Type type, object value)
		{
			return (value == null && TypeHelper.TypeAllowsNullValue(type)) || type.IsInstanceOfType(value);
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x00019D69 File Offset: 0x00017F69
		internal static bool IsNullableValueType(Type type)
		{
			return Nullable.GetUnderlyingType(type) != null;
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x00019D77 File Offset: 0x00017F77
		internal static bool TypeAllowsNullValue(Type type)
		{
			return !type.IsValueType || TypeHelper.IsNullableValueType(type);
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x00019D8C File Offset: 0x00017F8C
		internal static bool IsSimpleType(Type type)
		{
			return type.IsPrimitive || type.Equals(typeof(string)) || type.Equals(typeof(DateTime)) || type.Equals(typeof(decimal)) || type.Equals(typeof(Guid)) || type.Equals(typeof(DateTimeOffset)) || type.Equals(typeof(TimeSpan));
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x00019E10 File Offset: 0x00018010
		internal static bool IsSimpleUnderlyingType(Type type)
		{
			Type underlyingType = Nullable.GetUnderlyingType(type);
			if (underlyingType != null)
			{
				type = underlyingType;
			}
			return TypeHelper.IsSimpleType(type);
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x00019E36 File Offset: 0x00018036
		internal static bool CanConvertFromString(Type type)
		{
			return TypeHelper.IsSimpleUnderlyingType(type) || TypeHelper.HasStringConverter(type);
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x00019E48 File Offset: 0x00018048
		internal static bool HasStringConverter(Type type)
		{
			return TypeDescriptor.GetConverter(type).CanConvertFrom(typeof(string));
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x00019E60 File Offset: 0x00018060
		internal static ReadOnlyCollection<T> OfType<T>(object[] objects) where T : class
		{
			int num = objects.Length;
			List<T> list = new List<T>(num);
			int num2 = 0;
			for (int i = 0; i < num; i++)
			{
				T t = objects[i] as T;
				if (t != null)
				{
					list.Add(t);
					num2++;
				}
			}
			list.Capacity = num2;
			return new ReadOnlyCollection<T>(list);
		}

		// Token: 0x040002B0 RID: 688
		private static readonly Type TaskGenericType = typeof(Task<>);

		// Token: 0x040002B1 RID: 689
		internal static readonly Type ApiControllerType = typeof(ApiController);
	}
}
