using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Text.Json.Serialization;

namespace System.Text.Json.Reflection
{
	// Token: 0x02000065 RID: 101
	internal static class ReflectionExtensions
	{
		// Token: 0x060007BF RID: 1983 RVA: 0x0002403C File Offset: 0x0002223C
		public static Type GetCompatibleGenericBaseClass(this Type type, Type baseType)
		{
			if (baseType == null)
			{
				return null;
			}
			Type type2 = type;
			while (type2 != null && type2 != typeof(object))
			{
				if (type2.IsGenericType)
				{
					Type genericTypeDefinition = type2.GetGenericTypeDefinition();
					if (genericTypeDefinition == baseType)
					{
						return type2;
					}
				}
				type2 = type2.BaseType;
			}
			return null;
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x00024090 File Offset: 0x00022290
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2070:UnrecognizedReflectionPattern", Justification = "The 'interfaceType' must exist and so trimmer kept it. In which case It also kept it on any type which implements it. The below call to GetInterfaces may return fewer results when trimmed but it will return the 'interfaceType' if the type implemented it, even after trimming.")]
		public static Type GetCompatibleGenericInterface(this Type type, Type interfaceType)
		{
			if (interfaceType == null)
			{
				return null;
			}
			Type type2 = type;
			if (type2.IsGenericType)
			{
				type2 = type2.GetGenericTypeDefinition();
			}
			if (type2 == interfaceType)
			{
				return type;
			}
			foreach (Type type3 in type.GetInterfaces())
			{
				if (type3.IsGenericType)
				{
					Type genericTypeDefinition = type3.GetGenericTypeDefinition();
					if (genericTypeDefinition == interfaceType)
					{
						return type3;
					}
				}
			}
			return null;
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x000240F4 File Offset: 0x000222F4
		public static bool IsImmutableDictionaryType(this Type type)
		{
			if (!type.IsGenericType || !type.Assembly.FullName.StartsWith("System.Collections.Immutable", StringComparison.Ordinal))
			{
				return false;
			}
			string baseNameFromGenericType = ReflectionExtensions.GetBaseNameFromGenericType(type);
			return baseNameFromGenericType == "System.Collections.Immutable.ImmutableDictionary`2" || baseNameFromGenericType == "System.Collections.Immutable.IImmutableDictionary`2" || baseNameFromGenericType == "System.Collections.Immutable.ImmutableSortedDictionary`2";
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x00024154 File Offset: 0x00022354
		public static bool IsImmutableEnumerableType(this Type type)
		{
			if (!type.IsGenericType || !type.Assembly.FullName.StartsWith("System.Collections.Immutable", StringComparison.Ordinal))
			{
				return false;
			}
			string baseNameFromGenericType = ReflectionExtensions.GetBaseNameFromGenericType(type);
			if (baseNameFromGenericType != null)
			{
				switch (baseNameFromGenericType.Length)
				{
				case 44:
				{
					char c = baseNameFromGenericType[30];
					if (c != 'I')
					{
						if (c != 'm')
						{
							return false;
						}
						if (!(baseNameFromGenericType == "System.Collections.Immutable.ImmutableList`1"))
						{
							return false;
						}
					}
					else if (!(baseNameFromGenericType == "System.Collections.Immutable.IImmutableSet`1"))
					{
						return false;
					}
					break;
				}
				case 45:
				{
					char c = baseNameFromGenericType[38];
					if (c <= 'Q')
					{
						if (c != 'A')
						{
							if (c != 'Q')
							{
								return false;
							}
							if (!(baseNameFromGenericType == "System.Collections.Immutable.ImmutableQueue`1"))
							{
								return false;
							}
						}
						else if (!(baseNameFromGenericType == "System.Collections.Immutable.ImmutableArray`1"))
						{
							return false;
						}
					}
					else if (c != 'S')
					{
						if (c != 'e')
						{
							return false;
						}
						if (!(baseNameFromGenericType == "System.Collections.Immutable.IImmutableList`1"))
						{
							return false;
						}
					}
					else if (!(baseNameFromGenericType == "System.Collections.Immutable.ImmutableStack`1"))
					{
						return false;
					}
					break;
				}
				case 46:
				{
					char c = baseNameFromGenericType[39];
					if (c != 'Q')
					{
						if (c != 'S')
						{
							return false;
						}
						if (!(baseNameFromGenericType == "System.Collections.Immutable.IImmutableStack`1"))
						{
							return false;
						}
					}
					else if (!(baseNameFromGenericType == "System.Collections.Immutable.IImmutableQueue`1"))
					{
						return false;
					}
					break;
				}
				case 47:
					if (!(baseNameFromGenericType == "System.Collections.Immutable.ImmutableHashSet`1"))
					{
						return false;
					}
					break;
				case 48:
					return false;
				case 49:
					if (!(baseNameFromGenericType == "System.Collections.Immutable.ImmutableSortedSet`1"))
					{
						return false;
					}
					break;
				default:
					return false;
				}
				return true;
			}
			return false;
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x000242B8 File Offset: 0x000224B8
		public static string GetImmutableDictionaryConstructingTypeName(this Type type)
		{
			string baseNameFromGenericType = ReflectionExtensions.GetBaseNameFromGenericType(type);
			if (baseNameFromGenericType == "System.Collections.Immutable.ImmutableDictionary`2" || baseNameFromGenericType == "System.Collections.Immutable.IImmutableDictionary`2")
			{
				return "System.Collections.Immutable.ImmutableDictionary";
			}
			if (!(baseNameFromGenericType == "System.Collections.Immutable.ImmutableSortedDictionary`2"))
			{
				return null;
			}
			return "System.Collections.Immutable.ImmutableSortedDictionary";
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x00024304 File Offset: 0x00022504
		public static string GetImmutableEnumerableConstructingTypeName(this Type type)
		{
			string baseNameFromGenericType = ReflectionExtensions.GetBaseNameFromGenericType(type);
			if (baseNameFromGenericType != null)
			{
				switch (baseNameFromGenericType.Length)
				{
				case 44:
				{
					char c = baseNameFromGenericType[30];
					if (c != 'I')
					{
						if (c != 'm')
						{
							goto IL_0165;
						}
						if (!(baseNameFromGenericType == "System.Collections.Immutable.ImmutableList`1"))
						{
							goto IL_0165;
						}
					}
					else
					{
						if (!(baseNameFromGenericType == "System.Collections.Immutable.IImmutableSet`1"))
						{
							goto IL_0165;
						}
						goto IL_015F;
					}
					break;
				}
				case 45:
				{
					char c = baseNameFromGenericType[38];
					if (c <= 'Q')
					{
						if (c != 'A')
						{
							if (c != 'Q')
							{
								goto IL_0165;
							}
							if (!(baseNameFromGenericType == "System.Collections.Immutable.ImmutableQueue`1"))
							{
								goto IL_0165;
							}
							goto IL_0153;
						}
						else
						{
							if (!(baseNameFromGenericType == "System.Collections.Immutable.ImmutableArray`1"))
							{
								goto IL_0165;
							}
							return "System.Collections.Immutable.ImmutableArray";
						}
					}
					else if (c != 'S')
					{
						if (c != 'e')
						{
							goto IL_0165;
						}
						if (!(baseNameFromGenericType == "System.Collections.Immutable.IImmutableList`1"))
						{
							goto IL_0165;
						}
					}
					else
					{
						if (!(baseNameFromGenericType == "System.Collections.Immutable.ImmutableStack`1"))
						{
							goto IL_0165;
						}
						goto IL_014D;
					}
					break;
				}
				case 46:
				{
					char c = baseNameFromGenericType[39];
					if (c != 'Q')
					{
						if (c != 'S')
						{
							goto IL_0165;
						}
						if (!(baseNameFromGenericType == "System.Collections.Immutable.IImmutableStack`1"))
						{
							goto IL_0165;
						}
						goto IL_014D;
					}
					else
					{
						if (!(baseNameFromGenericType == "System.Collections.Immutable.IImmutableQueue`1"))
						{
							goto IL_0165;
						}
						goto IL_0153;
					}
					break;
				}
				case 47:
					if (!(baseNameFromGenericType == "System.Collections.Immutable.ImmutableHashSet`1"))
					{
						goto IL_0165;
					}
					goto IL_015F;
				case 48:
					goto IL_0165;
				case 49:
					if (!(baseNameFromGenericType == "System.Collections.Immutable.ImmutableSortedSet`1"))
					{
						goto IL_0165;
					}
					return "System.Collections.Immutable.ImmutableSortedSet";
				default:
					goto IL_0165;
				}
				return "System.Collections.Immutable.ImmutableList";
				IL_014D:
				return "System.Collections.Immutable.ImmutableStack";
				IL_0153:
				return "System.Collections.Immutable.ImmutableQueue";
				IL_015F:
				return "System.Collections.Immutable.ImmutableHashSet";
			}
			IL_0165:
			return null;
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x00024478 File Offset: 0x00022678
		private static string GetBaseNameFromGenericType(Type genericType)
		{
			Type genericTypeDefinition = genericType.GetGenericTypeDefinition();
			return genericTypeDefinition.FullName;
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x00024492 File Offset: 0x00022692
		public static bool IsVirtual(this PropertyInfo propertyInfo)
		{
			MethodInfo getMethod = propertyInfo.GetMethod;
			if (getMethod == null || !getMethod.IsVirtual)
			{
				MethodInfo setMethod = propertyInfo.SetMethod;
				return setMethod != null && setMethod.IsVirtual;
			}
			return true;
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x000244BB File Offset: 0x000226BB
		public static bool IsKeyValuePair(this Type type)
		{
			return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(KeyValuePair<, >);
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x000244DC File Offset: 0x000226DC
		public static bool TryGetDeserializationConstructor([DynamicallyAccessedMembers((DynamicallyAccessedMemberTypes)7)] this Type type, bool useDefaultCtorInAnnotatedStructs, out ConstructorInfo deserializationCtor)
		{
			ConstructorInfo constructorInfo = null;
			ConstructorInfo constructorInfo2 = null;
			ConstructorInfo constructorInfo3 = null;
			ConstructorInfo[] constructors = type.GetConstructors(BindingFlags.Instance | BindingFlags.Public);
			if (constructors.Length == 1)
			{
				constructorInfo3 = constructors[0];
			}
			foreach (ConstructorInfo constructorInfo4 in constructors)
			{
				if (ReflectionExtensions.HasJsonConstructorAttribute(constructorInfo4))
				{
					if (constructorInfo != null)
					{
						deserializationCtor = null;
						return false;
					}
					constructorInfo = constructorInfo4;
				}
				else if (constructorInfo4.GetParameters().Length == 0)
				{
					constructorInfo2 = constructorInfo4;
				}
			}
			foreach (ConstructorInfo constructorInfo5 in type.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic))
			{
				if (ReflectionExtensions.HasJsonConstructorAttribute(constructorInfo5))
				{
					if (constructorInfo != null)
					{
						deserializationCtor = null;
						return false;
					}
					constructorInfo = constructorInfo5;
				}
			}
			if (useDefaultCtorInAnnotatedStructs && type.IsValueType && constructorInfo == null)
			{
				deserializationCtor = null;
				return true;
			}
			ConstructorInfo constructorInfo6;
			if ((constructorInfo6 = constructorInfo) == null)
			{
				constructorInfo6 = constructorInfo2 ?? constructorInfo3;
			}
			deserializationCtor = constructorInfo6;
			return true;
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x000245B0 File Offset: 0x000227B0
		public static object GetDefaultValue(this ParameterInfo parameterInfo)
		{
			Type parameterType = parameterInfo.ParameterType;
			object defaultValue = parameterInfo.DefaultValue;
			if (defaultValue == null)
			{
				return null;
			}
			if (defaultValue == DBNull.Value && parameterType != typeof(DBNull))
			{
				return null;
			}
			if (parameterType.IsEnum)
			{
				return Enum.ToObject(parameterType, defaultValue);
			}
			Type underlyingType = Nullable.GetUnderlyingType(parameterType);
			if (underlyingType != null && underlyingType.IsEnum)
			{
				return Enum.ToObject(underlyingType, defaultValue);
			}
			return defaultValue;
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x00024618 File Offset: 0x00022818
		[RequiresUnreferencedCode("Should only be used by the reflection-based serializer.")]
		public static Type[] GetSortedTypeHierarchy(this Type type)
		{
			if (!type.IsInterface)
			{
				List<Type> list = new List<Type>();
				Type type2 = type;
				while (type2 != null)
				{
					list.Add(type2);
					type2 = type2.BaseType;
				}
				return list.ToArray();
			}
			return JsonHelpers.TraverseGraphWithTopologicalSort<Type>(type, (Type t) => t.GetInterfaces(), null);
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x0002467B File Offset: 0x0002287B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsNullableOfT(this Type type)
		{
			return type.IsGenericType && type.GetGenericTypeDefinition() == ReflectionExtensions.s_nullableType;
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x00024697 File Offset: 0x00022897
		public static bool IsAssignableFromInternal(this Type type, Type from)
		{
			if (from.IsNullableOfT() && type.IsInterface)
			{
				return type.IsAssignableFrom(from.GetGenericArguments()[0]);
			}
			return type.IsAssignableFrom(from);
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x000246BF File Offset: 0x000228BF
		public static bool IsInSubtypeRelationshipWith(this Type type, Type other)
		{
			return type.IsAssignableFromInternal(other) || other.IsAssignableFromInternal(type);
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x000246D3 File Offset: 0x000228D3
		private static bool HasJsonConstructorAttribute(ConstructorInfo constructorInfo)
		{
			return constructorInfo.GetCustomAttribute<JsonConstructorAttribute>() != null;
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x000246DE File Offset: 0x000228DE
		public static bool HasRequiredMemberAttribute(this MemberInfo memberInfo)
		{
			return memberInfo.HasCustomAttributeWithName("System.Runtime.CompilerServices.RequiredMemberAttribute", false);
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x000246EC File Offset: 0x000228EC
		public static bool HasSetsRequiredMembersAttribute(this MemberInfo memberInfo)
		{
			return memberInfo.HasCustomAttributeWithName("System.Diagnostics.CodeAnalysis.SetsRequiredMembersAttribute", false);
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x000246FC File Offset: 0x000228FC
		private static bool HasCustomAttributeWithName(this MemberInfo memberInfo, string fullName, bool inherit)
		{
			foreach (object obj in memberInfo.GetCustomAttributes(inherit))
			{
				if (obj.GetType().FullName == fullName)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x0002473C File Offset: 0x0002293C
		public static TAttribute GetUniqueCustomAttribute<TAttribute>(this MemberInfo memberInfo, bool inherit) where TAttribute : Attribute
		{
			object[] customAttributes = memberInfo.GetCustomAttributes(typeof(TAttribute), inherit);
			if (customAttributes.Length == 0)
			{
				return default(TAttribute);
			}
			if (customAttributes.Length == 1)
			{
				return (TAttribute)((object)customAttributes[0]);
			}
			ThrowHelper.ThrowInvalidOperationException_SerializationDuplicateAttribute(typeof(TAttribute), memberInfo);
			return default(TAttribute);
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x00024794 File Offset: 0x00022994
		public static object CreateInstanceNoWrapExceptions([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.NonPublicConstructors)] this Type type, Type[] parameterTypes, object[] parameters)
		{
			ConstructorInfo constructor = type.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, parameterTypes, null);
			object obj = null;
			try
			{
				obj = constructor.Invoke(parameters);
			}
			catch (TargetInvocationException ex)
			{
				ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
			}
			return obj;
		}

		// Token: 0x0400028D RID: 653
		private const string ImmutableArrayGenericTypeName = "System.Collections.Immutable.ImmutableArray`1";

		// Token: 0x0400028E RID: 654
		private const string ImmutableListGenericTypeName = "System.Collections.Immutable.ImmutableList`1";

		// Token: 0x0400028F RID: 655
		private const string ImmutableListGenericInterfaceTypeName = "System.Collections.Immutable.IImmutableList`1";

		// Token: 0x04000290 RID: 656
		private const string ImmutableStackGenericTypeName = "System.Collections.Immutable.ImmutableStack`1";

		// Token: 0x04000291 RID: 657
		private const string ImmutableStackGenericInterfaceTypeName = "System.Collections.Immutable.IImmutableStack`1";

		// Token: 0x04000292 RID: 658
		private const string ImmutableQueueGenericTypeName = "System.Collections.Immutable.ImmutableQueue`1";

		// Token: 0x04000293 RID: 659
		private const string ImmutableQueueGenericInterfaceTypeName = "System.Collections.Immutable.IImmutableQueue`1";

		// Token: 0x04000294 RID: 660
		private const string ImmutableSortedSetGenericTypeName = "System.Collections.Immutable.ImmutableSortedSet`1";

		// Token: 0x04000295 RID: 661
		private const string ImmutableHashSetGenericTypeName = "System.Collections.Immutable.ImmutableHashSet`1";

		// Token: 0x04000296 RID: 662
		private const string ImmutableSetGenericInterfaceTypeName = "System.Collections.Immutable.IImmutableSet`1";

		// Token: 0x04000297 RID: 663
		private const string ImmutableDictionaryGenericTypeName = "System.Collections.Immutable.ImmutableDictionary`2";

		// Token: 0x04000298 RID: 664
		private const string ImmutableDictionaryGenericInterfaceTypeName = "System.Collections.Immutable.IImmutableDictionary`2";

		// Token: 0x04000299 RID: 665
		private const string ImmutableSortedDictionaryGenericTypeName = "System.Collections.Immutable.ImmutableSortedDictionary`2";

		// Token: 0x0400029A RID: 666
		private const string ImmutableArrayTypeName = "System.Collections.Immutable.ImmutableArray";

		// Token: 0x0400029B RID: 667
		private const string ImmutableListTypeName = "System.Collections.Immutable.ImmutableList";

		// Token: 0x0400029C RID: 668
		private const string ImmutableStackTypeName = "System.Collections.Immutable.ImmutableStack";

		// Token: 0x0400029D RID: 669
		private const string ImmutableQueueTypeName = "System.Collections.Immutable.ImmutableQueue";

		// Token: 0x0400029E RID: 670
		private const string ImmutableSortedSetTypeName = "System.Collections.Immutable.ImmutableSortedSet";

		// Token: 0x0400029F RID: 671
		private const string ImmutableHashSetTypeName = "System.Collections.Immutable.ImmutableHashSet";

		// Token: 0x040002A0 RID: 672
		private const string ImmutableDictionaryTypeName = "System.Collections.Immutable.ImmutableDictionary";

		// Token: 0x040002A1 RID: 673
		private const string ImmutableSortedDictionaryTypeName = "System.Collections.Immutable.ImmutableSortedDictionary";

		// Token: 0x040002A2 RID: 674
		public const string CreateRangeMethodName = "CreateRange";

		// Token: 0x040002A3 RID: 675
		private static readonly Type s_nullableType = typeof(Nullable<>);
	}
}
