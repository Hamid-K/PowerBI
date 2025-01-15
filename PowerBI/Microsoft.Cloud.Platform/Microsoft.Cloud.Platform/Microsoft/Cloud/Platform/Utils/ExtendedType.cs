using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200020D RID: 525
	public static class ExtendedType
	{
		// Token: 0x06000DD7 RID: 3543 RVA: 0x00030970 File Offset: 0x0002EB70
		public static object ConvertValue([NotNull] string value, MemberInfo memberInfo)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<string>(value, "value");
			Type type = null;
			MemberTypes memberType = memberInfo.MemberType;
			if (memberType != MemberTypes.Field)
			{
				if (memberType != MemberTypes.Property)
				{
					ExtendedDiagnostics.EnsureInvalidSwitchValue<MemberTypes>(memberInfo.MemberType);
				}
				else
				{
					type = ((PropertyInfo)memberInfo).PropertyType;
				}
			}
			else
			{
				type = ((FieldInfo)memberInfo).FieldType;
			}
			TypeConverter fieldTypeConverter = ExtendedType.GetFieldTypeConverter(memberInfo);
			return ExtendedType.ConvertValue(value, type, fieldTypeConverter);
		}

		// Token: 0x06000DD8 RID: 3544 RVA: 0x000309D4 File Offset: 0x0002EBD4
		[CanBeNull]
		private static TypeConverter GetFieldTypeConverter(MemberInfo memberInfo)
		{
			TypeConverterAttribute typeConverterAttribute = Attribute.GetCustomAttribute(memberInfo, typeof(TypeConverterAttribute)) as TypeConverterAttribute;
			if (typeConverterAttribute != null)
			{
				return DynamicLoader.Instantiate(Type.GetType(typeConverterAttribute.ConverterTypeName), new Predicate<Type>(DynamicLoader.IsValidType), new object[0]) as TypeConverter;
			}
			return null;
		}

		// Token: 0x06000DD9 RID: 3545 RVA: 0x00030A23 File Offset: 0x0002EC23
		public static T ConvertValue<T>(string value)
		{
			return (T)((object)ExtendedType.ConvertValue(value, typeof(T), null));
		}

		// Token: 0x06000DDA RID: 3546 RVA: 0x00030A3C File Offset: 0x0002EC3C
		public static object ConvertValue([NotNull] string value, [NotNull] Type targetType, TypeConverter converter)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<Type>(targetType, "targetType");
			ExtendedDiagnostics.EnsureArgumentNotNull<string>(value, "value");
			object obj = null;
			try
			{
				converter = ((converter == null) ? TypeDescriptor.GetConverter(targetType) : converter);
				if (targetType.IsEnum)
				{
					obj = Enum.Parse(targetType, value);
				}
				else if (converter != null)
				{
					obj = converter.ConvertFromString(value);
				}
			}
			catch (NotSupportedException ex)
			{
				throw new ChangeTypeException(targetType, string.Format(CultureInfo.InvariantCulture, "Can't convert from string to {0}", new object[] { targetType }), ex);
			}
			return obj;
		}

		// Token: 0x06000DDB RID: 3547 RVA: 0x00030AC4 File Offset: 0x0002ECC4
		public static void SetValue(object obj, MemberInfo member, object value)
		{
			try
			{
				MemberTypes memberType = member.MemberType;
				if (memberType != MemberTypes.Field)
				{
					if (memberType != MemberTypes.Property)
					{
						ExtendedDiagnostics.EnsureInvalidSwitchValue<MemberTypes>(member.MemberType);
					}
					else
					{
						((PropertyInfo)member).SetValue(obj, value, null);
					}
				}
				else
				{
					((FieldInfo)member).SetValue(obj, value);
				}
			}
			catch (ArgumentException ex)
			{
				ExtendedType.ThrowSetValueException(obj, member, value, ex);
			}
			catch (TargetException ex2)
			{
				ExtendedType.ThrowSetValueException(obj, member, value, ex2);
			}
			catch (FieldAccessException ex3)
			{
				ExtendedType.ThrowSetValueException(obj, member, value, ex3);
			}
		}

		// Token: 0x06000DDC RID: 3548 RVA: 0x00030B5C File Offset: 0x0002ED5C
		private static void ThrowSetValueException(object obj, MemberInfo member, object value, Exception ex)
		{
			throw new SetValueException(string.Format(CultureInfo.InvariantCulture, "Can't set the value '{0}' in the member '{1}' of '{2}'", new object[]
			{
				value,
				member.Name,
				obj.GetType().Name
			}), ex);
		}

		// Token: 0x06000DDD RID: 3549 RVA: 0x00030B94 File Offset: 0x0002ED94
		public static object GetValue(object obj, MemberInfo member)
		{
			MemberTypes memberType = member.MemberType;
			if (memberType == MemberTypes.Field)
			{
				return ((FieldInfo)member).GetValue(obj);
			}
			if (memberType != MemberTypes.Property)
			{
				ExtendedDiagnostics.EnsureInvalidSwitchValue<MemberTypes>(member.MemberType);
				return null;
			}
			return ((PropertyInfo)member).GetValue(obj, null);
		}

		// Token: 0x06000DDE RID: 3550 RVA: 0x00030BDC File Offset: 0x0002EDDC
		public static bool IsSubclassOfRawGeneric(Type typeToCheck, Type genericType)
		{
			while (typeToCheck != null && typeToCheck != typeof(object))
			{
				Type type = (typeToCheck.IsGenericType ? typeToCheck.GetGenericTypeDefinition() : typeToCheck);
				if (genericType == type)
				{
					return true;
				}
				typeToCheck = typeToCheck.BaseType;
			}
			return false;
		}

		// Token: 0x06000DDF RID: 3551 RVA: 0x00030C2C File Offset: 0x0002EE2C
		public static IEnumerable<Type> GetBaseTypes(Type type)
		{
			if (type.BaseType == null)
			{
				return type.GetInterfaces();
			}
			return Enumerable.Repeat<Type>(type.BaseType, 1).Concat(type.GetInterfaces()).Concat(type.GetInterfaces().SelectMany(new Func<Type, IEnumerable<Type>>(ExtendedType.GetBaseTypes)))
				.Concat(ExtendedType.GetBaseTypes(type.BaseType));
		}

		// Token: 0x06000DE0 RID: 3552 RVA: 0x00030C91 File Offset: 0x0002EE91
		public static string GetTypeAndAssemblyName(this Type type)
		{
			return "{0}, {1}".FormatWithInvariantCulture(new object[]
			{
				type.FullName,
				type.Assembly.GetName().Name
			});
		}
	}
}
