using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Text;
using NLog.Common;
using NLog.Conditions;
using NLog.Config;
using NLog.Layouts;

namespace NLog.Internal
{
	// Token: 0x02000136 RID: 310
	internal static class PropertyHelper
	{
		// Token: 0x06000F68 RID: 3944 RVA: 0x00026D88 File Offset: 0x00024F88
		internal static void SetPropertyFromString(object obj, string propertyName, string value, ConfigurationItemFactory configurationItemFactory)
		{
			InternalLogger.Debug<string, string, string>("Setting '{0}.{1}' to '{2}'", obj.GetType().Name, propertyName, value);
			PropertyInfo propertyInfo;
			if (!PropertyHelper.TryGetPropertyInfo(obj, propertyName, out propertyInfo))
			{
				throw new NotSupportedException("Parameter " + propertyName + " not supported on " + obj.GetType().Name);
			}
			try
			{
				if (propertyInfo.IsDefined(PropertyHelper._arrayParameterAttribute.GetType(), false))
				{
					throw new NotSupportedException(string.Concat(new string[]
					{
						"Parameter ",
						propertyName,
						" of ",
						obj.GetType().Name,
						" is an array and cannot be assigned a scalar value."
					}));
				}
				Type type = propertyInfo.PropertyType;
				type = Nullable.GetUnderlyingType(type) ?? type;
				object obj2;
				if (!PropertyHelper.TryNLogSpecificConversion(type, value, out obj2, configurationItemFactory) && !PropertyHelper.TryGetEnumValue(type, value, out obj2, true) && !PropertyHelper.TrySpecialConversion(type, value, out obj2) && !PropertyHelper.TryImplicitConversion(type, value, out obj2) && !PropertyHelper.TryFlatListConversion(type, value, out obj2) && !PropertyHelper.TryTypeConverterConversion(type, value, out obj2))
				{
					obj2 = Convert.ChangeType(value, type, CultureInfo.InvariantCulture);
				}
				propertyInfo.SetValue(obj, obj2, null);
			}
			catch (TargetInvocationException ex)
			{
				throw new NLogConfigurationException(string.Format("Error when setting property '{0}' on {1}", propertyInfo.Name, obj), ex.InnerException);
			}
			catch (Exception ex2)
			{
				InternalLogger.Warn(ex2, "Error when setting property '{0}' on '{1}'", new object[] { propertyInfo.Name, obj });
				if (ex2.MustBeRethrownImmediately())
				{
					throw;
				}
				throw new NLogConfigurationException(string.Format("Error when setting property '{0}' on {1}", propertyInfo.Name, obj), ex2);
			}
		}

		// Token: 0x06000F69 RID: 3945 RVA: 0x00026F18 File Offset: 0x00025118
		internal static bool IsArrayProperty(Type t, string propertyName)
		{
			PropertyInfo propertyInfo;
			if (!PropertyHelper.TryGetPropertyInfo(t, propertyName, out propertyInfo))
			{
				throw new NotSupportedException("Parameter " + propertyName + " not supported on " + t.Name);
			}
			return propertyInfo.IsDefined(PropertyHelper._arrayParameterAttribute.GetType(), false);
		}

		// Token: 0x06000F6A RID: 3946 RVA: 0x00026F60 File Offset: 0x00025160
		internal static bool TryGetPropertyInfo(object obj, string propertyName, out PropertyInfo result)
		{
			PropertyInfo property = obj.GetType().GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);
			if (property != null)
			{
				result = property;
				return true;
			}
			Dictionary<Type, Dictionary<string, PropertyInfo>> dictionary = PropertyHelper.parameterInfoCache;
			bool flag2;
			lock (dictionary)
			{
				Type type = obj.GetType();
				Dictionary<string, PropertyInfo> dictionary2;
				if (!PropertyHelper.parameterInfoCache.TryGetValue(type, out dictionary2))
				{
					dictionary2 = PropertyHelper.BuildPropertyInfoDictionary(type);
					PropertyHelper.parameterInfoCache[type] = dictionary2;
				}
				flag2 = dictionary2.TryGetValue(propertyName, out result);
			}
			return flag2;
		}

		// Token: 0x06000F6B RID: 3947 RVA: 0x00026FF0 File Offset: 0x000251F0
		internal static Type GetArrayItemType(PropertyInfo propInfo)
		{
			ArrayParameterAttribute customAttribute = propInfo.GetCustomAttribute<ArrayParameterAttribute>();
			if (customAttribute == null)
			{
				return null;
			}
			return customAttribute.ItemType;
		}

		// Token: 0x06000F6C RID: 3948 RVA: 0x00027003 File Offset: 0x00025203
		internal static PropertyInfo[] GetAllReadableProperties(Type type)
		{
			return type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
		}

		// Token: 0x06000F6D RID: 3949 RVA: 0x00027010 File Offset: 0x00025210
		internal static void CheckRequiredParameters(object o)
		{
			foreach (PropertyInfo propertyInfo in PropertyHelper.GetAllReadableProperties(o.GetType()))
			{
				if (propertyInfo.IsDefined(PropertyHelper._requiredParameterAttribute.GetType(), false) && propertyInfo.GetValue(o, null) == null)
				{
					throw new NLogConfigurationException(string.Format("Required parameter '{0}' on '{1}' was not specified.", propertyInfo.Name, o));
				}
			}
		}

		// Token: 0x06000F6E RID: 3950 RVA: 0x00027070 File Offset: 0x00025270
		private static bool TryImplicitConversion(Type resultType, string value, out object result)
		{
			try
			{
				if (Type.GetTypeCode(resultType) != TypeCode.Object)
				{
					result = null;
					return false;
				}
				MethodInfo method = resultType.GetMethod("op_Implicit", BindingFlags.Static | BindingFlags.Public, null, new Type[] { value.GetType() }, null);
				if (method == null || !resultType.IsAssignableFrom(method.ReturnType))
				{
					result = null;
					return false;
				}
				result = method.Invoke(null, new object[] { value });
				return true;
			}
			catch (Exception ex)
			{
				InternalLogger.Warn(ex, "Implicit Conversion Failed of {0} to {1}", new object[] { value, resultType });
			}
			result = null;
			return false;
		}

		// Token: 0x06000F6F RID: 3951 RVA: 0x00027114 File Offset: 0x00025314
		private static bool TryNLogSpecificConversion(Type propertyType, string value, out object newValue, ConfigurationItemFactory configurationItemFactory)
		{
			if (propertyType == typeof(Layout) || propertyType == typeof(SimpleLayout))
			{
				newValue = new SimpleLayout(value, configurationItemFactory);
				return true;
			}
			if (propertyType == typeof(ConditionExpression))
			{
				newValue = ConditionParser.ParseExpression(value, configurationItemFactory);
				return true;
			}
			newValue = null;
			return false;
		}

		// Token: 0x06000F70 RID: 3952 RVA: 0x00027174 File Offset: 0x00025374
		private static bool TryGetEnumValue(Type resultType, string value, out object result, bool flagsEnumAllowed)
		{
			if (!resultType.IsEnum())
			{
				result = null;
				return false;
			}
			if (flagsEnumAllowed && resultType.IsDefined(PropertyHelper._flagsAttribute.GetType(), false))
			{
				ulong num = 0UL;
				foreach (string text in value.SplitAndTrimTokens(','))
				{
					FieldInfo field = resultType.GetField(text, BindingFlags.IgnoreCase | BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy);
					if (field == null)
					{
						throw new NLogConfigurationException("Invalid enumeration value '" + value + "'.");
					}
					num |= Convert.ToUInt64(field.GetValue(null), CultureInfo.InvariantCulture);
				}
				result = Convert.ChangeType(num, Enum.GetUnderlyingType(resultType), CultureInfo.InvariantCulture);
				result = Enum.ToObject(resultType, result);
				return true;
			}
			FieldInfo field2 = resultType.GetField(value, BindingFlags.IgnoreCase | BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy);
			if (field2 == null)
			{
				throw new NLogConfigurationException("Invalid enumeration value '" + value + "'.");
			}
			result = field2.GetValue(null);
			return true;
		}

		// Token: 0x06000F71 RID: 3953 RVA: 0x00027264 File Offset: 0x00025464
		private static bool TrySpecialConversion(Type type, string value, out object newValue)
		{
			if (type == typeof(Encoding))
			{
				value = value.Trim();
				if (string.Equals(value, "UTF8", StringComparison.OrdinalIgnoreCase))
				{
					value = Encoding.UTF8.WebName;
				}
				newValue = Encoding.GetEncoding(value);
				return true;
			}
			if (type == typeof(CultureInfo))
			{
				value = value.Trim();
				newValue = new CultureInfo(value);
				return true;
			}
			if (type == typeof(Type))
			{
				value = value.Trim();
				newValue = Type.GetType(value, true);
				return true;
			}
			newValue = null;
			return false;
		}

		// Token: 0x06000F72 RID: 3954 RVA: 0x000272FC File Offset: 0x000254FC
		private static bool TryFlatListConversion(Type type, string valueRaw, out object newValue)
		{
			object obj;
			MethodInfo methodInfo;
			Type type2;
			if (type.IsGenericType() && PropertyHelper.TryCreateCollectionObject(type, valueRaw, out obj, out methodInfo, out type2))
			{
				foreach (string text in valueRaw.SplitQuoted(',', '\'', '\\'))
				{
					if (!PropertyHelper.TryGetEnumValue(type2, text, out newValue, false) && !PropertyHelper.TrySpecialConversion(type2, text, out newValue) && !PropertyHelper.TryImplicitConversion(type2, text, out newValue) && !PropertyHelper.TryTypeConverterConversion(type2, text, out newValue))
					{
						newValue = Convert.ChangeType(text, type2, CultureInfo.InvariantCulture);
					}
					methodInfo.Invoke(obj, new object[] { newValue });
				}
				newValue = obj;
				return true;
			}
			newValue = null;
			return false;
		}

		// Token: 0x06000F73 RID: 3955 RVA: 0x000273C0 File Offset: 0x000255C0
		private static bool TryCreateCollectionObject(Type collectionType, string valueRaw, out object collectionObject, out MethodInfo collectionAddMethod, out Type collectionItemType)
		{
			Type genericTypeDefinition = collectionType.GetGenericTypeDefinition();
			bool flag = genericTypeDefinition == typeof(ISet<>) || genericTypeDefinition == typeof(HashSet<>);
			if (!flag && !(genericTypeDefinition == typeof(List<>)) && !(genericTypeDefinition == typeof(IList<>)) && !(genericTypeDefinition == typeof(IEnumerable<>)))
			{
				collectionObject = null;
				collectionAddMethod = null;
				collectionItemType = null;
				return false;
			}
			collectionItemType = collectionType.GetGenericArguments()[0];
			Type type = (flag ? typeof(HashSet<>) : typeof(List<>));
			Type type2 = collectionItemType;
			Type type3 = type.MakeGenericType(new Type[] { type2 });
			collectionObject = Activator.CreateInstance(type3);
			if (collectionObject == null)
			{
				throw new NLogConfigurationException("Cannot create instance of {0} for value {1}", new object[]
				{
					collectionType.ToString(),
					valueRaw
				});
			}
			collectionAddMethod = type3.GetMethod("Add", BindingFlags.Instance | BindingFlags.Public);
			if (collectionAddMethod == null)
			{
				throw new NLogConfigurationException("Add method on type {0} for value {1} not found", new object[]
				{
					collectionType.ToString(),
					valueRaw
				});
			}
			return true;
		}

		// Token: 0x06000F74 RID: 3956 RVA: 0x000274DC File Offset: 0x000256DC
		private static bool TryTypeConverterConversion(Type type, string value, out object newValue)
		{
			TypeConverter converter = TypeDescriptor.GetConverter(type);
			if (converter.CanConvertFrom(typeof(string)))
			{
				newValue = converter.ConvertFromInvariantString(value);
				return true;
			}
			newValue = null;
			return false;
		}

		// Token: 0x06000F75 RID: 3957 RVA: 0x00027514 File Offset: 0x00025714
		private static bool TryGetPropertyInfo(Type targetType, string propertyName, out PropertyInfo result)
		{
			if (!string.IsNullOrEmpty(propertyName))
			{
				PropertyInfo property = targetType.GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);
				if (property != null)
				{
					result = property;
					return true;
				}
			}
			Dictionary<Type, Dictionary<string, PropertyInfo>> dictionary = PropertyHelper.parameterInfoCache;
			bool flag2;
			lock (dictionary)
			{
				Dictionary<string, PropertyInfo> dictionary2;
				if (!PropertyHelper.parameterInfoCache.TryGetValue(targetType, out dictionary2))
				{
					dictionary2 = PropertyHelper.BuildPropertyInfoDictionary(targetType);
					PropertyHelper.parameterInfoCache[targetType] = dictionary2;
				}
				flag2 = dictionary2.TryGetValue(propertyName, out result);
			}
			return flag2;
		}

		// Token: 0x06000F76 RID: 3958 RVA: 0x0002759C File Offset: 0x0002579C
		private static Dictionary<string, PropertyInfo> BuildPropertyInfoDictionary(Type t)
		{
			Dictionary<string, PropertyInfo> dictionary = new Dictionary<string, PropertyInfo>(StringComparer.OrdinalIgnoreCase);
			foreach (PropertyInfo propertyInfo in PropertyHelper.GetAllReadableProperties(t))
			{
				ArrayParameterAttribute customAttribute = propertyInfo.GetCustomAttribute<ArrayParameterAttribute>();
				if (customAttribute != null)
				{
					dictionary[customAttribute.ElementName] = propertyInfo;
				}
				else
				{
					dictionary[propertyInfo.Name] = propertyInfo;
				}
				if (propertyInfo.IsDefined(PropertyHelper._defaultParameterAttribute.GetType(), false))
				{
					dictionary[string.Empty] = propertyInfo;
				}
			}
			return dictionary;
		}

		// Token: 0x0400041D RID: 1053
		private static Dictionary<Type, Dictionary<string, PropertyInfo>> parameterInfoCache = new Dictionary<Type, Dictionary<string, PropertyInfo>>();

		// Token: 0x0400041E RID: 1054
		private static readonly RequiredParameterAttribute _requiredParameterAttribute = new RequiredParameterAttribute();

		// Token: 0x0400041F RID: 1055
		private static readonly ArrayParameterAttribute _arrayParameterAttribute = new ArrayParameterAttribute(null, string.Empty);

		// Token: 0x04000420 RID: 1056
		private static readonly DefaultValueAttribute _defaultValueAttribute = new DefaultValueAttribute(string.Empty);

		// Token: 0x04000421 RID: 1057
		private static readonly AdvancedAttribute _advancedAttribute = new AdvancedAttribute();

		// Token: 0x04000422 RID: 1058
		private static readonly DefaultParameterAttribute _defaultParameterAttribute = new DefaultParameterAttribute();

		// Token: 0x04000423 RID: 1059
		private static readonly FlagsAttribute _flagsAttribute = new FlagsAttribute();
	}
}
