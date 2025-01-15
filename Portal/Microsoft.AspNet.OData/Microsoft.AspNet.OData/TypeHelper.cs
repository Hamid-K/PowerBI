using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Formatter;
using Microsoft.AspNet.OData.Interfaces;

namespace Microsoft.AspNet.OData
{
	// Token: 0x02000058 RID: 88
	internal static class TypeHelper
	{
		// Token: 0x0600025A RID: 602 RVA: 0x0000AA0D File Offset: 0x00008C0D
		public static MemberInfo AsMemberInfo(Type clrType)
		{
			return clrType;
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000AA10 File Offset: 0x00008C10
		public static Type AsType(MemberInfo memberInfo)
		{
			return memberInfo as Type;
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000AA18 File Offset: 0x00008C18
		public static Assembly GetAssembly(Type clrType)
		{
			return clrType.Assembly;
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000AA20 File Offset: 0x00008C20
		public static Type GetBaseType(Type clrType)
		{
			return clrType.BaseType;
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000AA28 File Offset: 0x00008C28
		public static string GetQualifiedName(MemberInfo memberInfo)
		{
			Type type = memberInfo as Type;
			if (!(type != null))
			{
				return memberInfo.Name;
			}
			return type.Namespace + "." + type.Name;
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000AA62 File Offset: 0x00008C62
		public static Type GetReflectedType(MemberInfo memberInfo)
		{
			return memberInfo.ReflectedType;
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000AA6A File Offset: 0x00008C6A
		public static bool IsAbstract(Type clrType)
		{
			return clrType.IsAbstract;
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000AA72 File Offset: 0x00008C72
		public static bool IsClass(Type clrType)
		{
			return clrType.IsClass;
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000AA7A File Offset: 0x00008C7A
		public static bool IsGenericType(this Type clrType)
		{
			return clrType.IsGenericType;
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000AA82 File Offset: 0x00008C82
		public static bool IsGenericTypeDefinition(this Type clrType)
		{
			return clrType.IsGenericTypeDefinition;
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000AA8A File Offset: 0x00008C8A
		public static bool IsInterface(Type clrType)
		{
			return clrType.IsInterface;
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000AA92 File Offset: 0x00008C92
		public static bool IsNullable(Type clrType)
		{
			return !TypeHelper.IsValueType(clrType) || (clrType.IsGenericType() && clrType.GetGenericTypeDefinition() == typeof(Nullable<>));
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000AABD File Offset: 0x00008CBD
		public static bool IsPublic(Type clrType)
		{
			return clrType.IsPublic;
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000AAC5 File Offset: 0x00008CC5
		public static bool IsPrimitive(Type clrType)
		{
			return clrType.IsPrimitive;
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000AACD File Offset: 0x00008CCD
		public static bool IsTypeAssignableFrom(Type clrType, Type fromType)
		{
			return clrType.IsAssignableFrom(fromType);
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000AAD6 File Offset: 0x00008CD6
		public static bool IsValueType(Type clrType)
		{
			return clrType.IsValueType;
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000AADE File Offset: 0x00008CDE
		public static bool IsVisible(Type clrType)
		{
			return clrType.IsVisible;
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000AAE6 File Offset: 0x00008CE6
		public static Type ToNullable(Type clrType)
		{
			if (TypeHelper.IsNullable(clrType))
			{
				return clrType;
			}
			return typeof(Nullable<>).MakeGenericType(new Type[] { clrType });
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0000AB0C File Offset: 0x00008D0C
		public static Type GetInnerElementType(Type clrType)
		{
			Type type;
			TypeHelper.IsCollection(clrType, out type);
			return type;
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0000AB24 File Offset: 0x00008D24
		public static bool IsCollection(Type clrType)
		{
			Type type;
			return TypeHelper.IsCollection(clrType, out type);
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0000AB3C File Offset: 0x00008D3C
		public static bool IsCollection(Type clrType, out Type elementType)
		{
			if (clrType == null)
			{
				throw Error.ArgumentNull("clrType");
			}
			elementType = clrType;
			if (clrType == typeof(string))
			{
				return false;
			}
			Type type = clrType.GetInterfaces().Union(new Type[] { clrType }).FirstOrDefault((Type t) => t.IsGenericType() && t.GetGenericTypeDefinition() == typeof(IEnumerable<>));
			if (type != null)
			{
				elementType = type.GetGenericArguments().Single<Type>();
				return true;
			}
			return false;
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000ABC7 File Offset: 0x00008DC7
		public static Type GetUnderlyingTypeOrSelf(Type type)
		{
			return Nullable.GetUnderlyingType(type) ?? type;
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000ABD4 File Offset: 0x00008DD4
		public static bool IsEnum(Type clrType)
		{
			return TypeHelper.GetUnderlyingTypeOrSelf(clrType).IsEnum;
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000ABE1 File Offset: 0x00008DE1
		public static bool IsDateTime(Type clrType)
		{
			return Type.GetTypeCode(TypeHelper.GetUnderlyingTypeOrSelf(clrType)) == TypeCode.DateTime;
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000ABF2 File Offset: 0x00008DF2
		public static bool IsTimeSpan(Type clrType)
		{
			return TypeHelper.GetUnderlyingTypeOrSelf(clrType) == typeof(TimeSpan);
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000AC09 File Offset: 0x00008E09
		internal static bool IsIQueryable(Type type)
		{
			return type == typeof(IQueryable) || (type != null && type.IsGenericType() && type.GetGenericTypeDefinition() == typeof(IQueryable<>));
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000AC47 File Offset: 0x00008E47
		internal static bool IsQueryPrimitiveType(Type type)
		{
			type = TypeHelper.GetInnerMostElementType(type);
			return TypeHelper.IsEnum(type) || TypeHelper.IsPrimitive(type) || type == typeof(Uri) || EdmLibHelpers.GetEdmPrimitiveTypeOrNull(type) != null;
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000AC80 File Offset: 0x00008E80
		internal static Type GetInnerMostElementType(Type type)
		{
			for (;;)
			{
				Type underlyingType = Nullable.GetUnderlyingType(type);
				if (underlyingType != null)
				{
					type = underlyingType;
				}
				else
				{
					if (!type.HasElementType)
					{
						break;
					}
					type = type.GetElementType();
				}
			}
			return type;
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000ACB8 File Offset: 0x00008EB8
		internal static Type GetImplementedIEnumerableType(Type type)
		{
			if (type.IsGenericType() && type.GetGenericTypeDefinition() == typeof(Task<>))
			{
				type = type.GetGenericArguments().First<Type>();
			}
			if (type.IsGenericType() && TypeHelper.IsInterface(type) && (type.GetGenericTypeDefinition() == typeof(IEnumerable<>) || type.GetGenericTypeDefinition() == typeof(IQueryable<>)))
			{
				return TypeHelper.GetInnerGenericType(type);
			}
			foreach (Type type2 in type.GetInterfaces())
			{
				if (type2.IsGenericType() && (type2.GetGenericTypeDefinition() == typeof(IEnumerable<>) || type2.GetGenericTypeDefinition() == typeof(IQueryable<>)))
				{
					return TypeHelper.GetInnerGenericType(type2);
				}
			}
			return null;
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000AD90 File Offset: 0x00008F90
		internal static IEnumerable<Type> GetLoadedTypes(IWebApiAssembliesResolver assembliesResolver)
		{
			List<Type> list = new List<Type>();
			if (assembliesResolver == null)
			{
				return list;
			}
			foreach (Assembly assembly in assembliesResolver.Assemblies)
			{
				Type[] array = null;
				if (!(assembly == null) && !assembly.IsDynamic)
				{
					try
					{
						array = assembly.GetTypes();
					}
					catch (ReflectionTypeLoadException ex)
					{
						array = ex.Types;
					}
					catch
					{
						continue;
					}
					if (array != null)
					{
						list.AddRange(array.Where((Type t) => t != null && TypeHelper.IsVisible(t)));
					}
				}
			}
			return list;
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000AE50 File Offset: 0x00009050
		internal static Type GetTaskInnerTypeOrSelf(Type type)
		{
			if (type.IsGenericType() && type.GetGenericTypeDefinition() == typeof(Task<>))
			{
				return type.GetGenericArguments().First<Type>();
			}
			return type;
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000AE80 File Offset: 0x00009080
		private static Type GetInnerGenericType(Type interfaceType)
		{
			Type[] genericArguments = interfaceType.GetGenericArguments();
			if (genericArguments.Length == 1)
			{
				return genericArguments[0];
			}
			return null;
		}
	}
}
