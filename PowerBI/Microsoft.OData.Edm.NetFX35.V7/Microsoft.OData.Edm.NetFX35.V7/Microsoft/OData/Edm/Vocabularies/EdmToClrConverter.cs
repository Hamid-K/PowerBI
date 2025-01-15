using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x020000ED RID: 237
	public class EdmToClrConverter
	{
		// Token: 0x060006C6 RID: 1734 RVA: 0x00012B30 File Offset: 0x00010D30
		public EdmToClrConverter()
		{
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x00012B59 File Offset: 0x00010D59
		public EdmToClrConverter(TryCreateObjectInstance tryCreateObjectInstanceDelegate)
		{
			EdmUtil.CheckArgumentNull<TryCreateObjectInstance>(tryCreateObjectInstanceDelegate, "tryCreateObjectInstanceDelegate");
			this.tryCreateObjectInstanceDelegate = tryCreateObjectInstanceDelegate;
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x00012B95 File Offset: 0x00010D95
		public EdmToClrConverter(TryCreateObjectInstance tryCreateObjectInstanceDelegate, TryGetClrPropertyInfo tryGetClrPropertyInfoDelegate, TryGetClrTypeName tryGetClrTypeNameDelegate)
		{
			this.tryCreateObjectInstanceDelegate = tryCreateObjectInstanceDelegate;
			this.tryGetClrPropertyInfoDelegate = tryGetClrPropertyInfoDelegate;
			this.TryGetClrTypeNameDelegate = tryGetClrTypeNameDelegate;
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x060006C9 RID: 1737 RVA: 0x00012BD3 File Offset: 0x00010DD3
		// (set) Token: 0x060006CA RID: 1738 RVA: 0x00012BDB File Offset: 0x00010DDB
		internal TryGetClrTypeName TryGetClrTypeNameDelegate { get; private set; }

		// Token: 0x060006CB RID: 1739 RVA: 0x00012BE4 File Offset: 0x00010DE4
		public T AsClrValue<T>(IEdmValue edmValue)
		{
			EdmUtil.CheckArgumentNull<IEdmValue>(edmValue, "edmValue");
			bool flag = false;
			return (T)((object)this.AsClrValue(edmValue, typeof(T), flag));
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x00012C18 File Offset: 0x00010E18
		public object AsClrValue(IEdmValue edmValue, Type clrType)
		{
			EdmUtil.CheckArgumentNull<IEdmValue>(edmValue, "edmValue");
			EdmUtil.CheckArgumentNull<Type>(clrType, "clrType");
			bool flag = true;
			return this.AsClrValue(edmValue, clrType, flag);
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x00012C48 File Offset: 0x00010E48
		public void RegisterConvertedObject(IEdmStructuredValue edmValue, object clrObject)
		{
			this.convertedObjects.Add(edmValue, clrObject);
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x00012C57 File Offset: 0x00010E57
		internal static byte[] AsClrByteArray(IEdmValue edmValue)
		{
			EdmUtil.CheckArgumentNull<IEdmValue>(edmValue, "edmValue");
			if (edmValue is IEdmNullValue)
			{
				return null;
			}
			return ((IEdmBinaryValue)edmValue).Value;
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x00012C7A File Offset: 0x00010E7A
		internal static string AsClrString(IEdmValue edmValue)
		{
			EdmUtil.CheckArgumentNull<IEdmValue>(edmValue, "edmValue");
			if (edmValue is IEdmNullValue)
			{
				return null;
			}
			return ((IEdmStringValue)edmValue).Value;
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x00012C9D File Offset: 0x00010E9D
		internal static bool AsClrBoolean(IEdmValue edmValue)
		{
			EdmUtil.CheckArgumentNull<IEdmValue>(edmValue, "edmValue");
			return ((IEdmBooleanValue)edmValue).Value;
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x00012CB6 File Offset: 0x00010EB6
		internal static long AsClrInt64(IEdmValue edmValue)
		{
			EdmUtil.CheckArgumentNull<IEdmValue>(edmValue, "edmValue");
			return ((IEdmIntegerValue)edmValue).Value;
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x00012CCF File Offset: 0x00010ECF
		internal static char AsClrChar(IEdmValue edmValue)
		{
			return checked((char)EdmToClrConverter.AsClrInt64(edmValue));
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x00012CD8 File Offset: 0x00010ED8
		internal static byte AsClrByte(IEdmValue edmValue)
		{
			return checked((byte)EdmToClrConverter.AsClrInt64(edmValue));
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x00012CE1 File Offset: 0x00010EE1
		internal static short AsClrInt16(IEdmValue edmValue)
		{
			return checked((short)EdmToClrConverter.AsClrInt64(edmValue));
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x00012CEA File Offset: 0x00010EEA
		internal static int AsClrInt32(IEdmValue edmValue)
		{
			return checked((int)EdmToClrConverter.AsClrInt64(edmValue));
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x00012CF3 File Offset: 0x00010EF3
		internal static double AsClrDouble(IEdmValue edmValue)
		{
			EdmUtil.CheckArgumentNull<IEdmValue>(edmValue, "edmValue");
			return ((IEdmFloatingValue)edmValue).Value;
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x00012D0C File Offset: 0x00010F0C
		internal static float AsClrSingle(IEdmValue edmValue)
		{
			return (float)EdmToClrConverter.AsClrDouble(edmValue);
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x00012D15 File Offset: 0x00010F15
		internal static TimeOfDay AsClrTimeOfDay(IEdmValue edmValue)
		{
			EdmUtil.CheckArgumentNull<IEdmValue>(edmValue, "edmValue");
			return ((IEdmTimeOfDayValue)edmValue).Value;
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x00012D2E File Offset: 0x00010F2E
		internal static Date AsClrDate(IEdmValue edmValue)
		{
			EdmUtil.CheckArgumentNull<IEdmValue>(edmValue, "edmValue");
			return ((IEdmDateValue)edmValue).Value;
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x00012D47 File Offset: 0x00010F47
		internal static decimal AsClrDecimal(IEdmValue edmValue)
		{
			EdmUtil.CheckArgumentNull<IEdmValue>(edmValue, "edmValue");
			return ((IEdmDecimalValue)edmValue).Value;
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x00012D60 File Offset: 0x00010F60
		internal static TimeSpan AsClrDuration(IEdmValue edmValue)
		{
			EdmUtil.CheckArgumentNull<IEdmValue>(edmValue, "edmValue");
			return ((IEdmDurationValue)edmValue).Value;
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x00012D79 File Offset: 0x00010F79
		internal static Guid AsClrGuid(IEdmValue edmValue)
		{
			EdmUtil.CheckArgumentNull<IEdmValue>(edmValue, "edmValue");
			return ((IEdmGuidValue)edmValue).Value;
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x00012D92 File Offset: 0x00010F92
		internal static DateTimeOffset AsClrDateTimeOffset(IEdmValue edmValue)
		{
			EdmUtil.CheckArgumentNull<IEdmValue>(edmValue, "edmValue");
			return ((IEdmDateTimeOffsetValue)edmValue).Value;
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x00012DAC File Offset: 0x00010FAC
		private static bool TryConvertAsNonGuidPrimitiveType(Type clrType, IEdmValue edmValue, out object clrValue)
		{
			if (clrType == typeof(bool))
			{
				clrValue = EdmToClrConverter.AsClrBoolean(edmValue);
				return true;
			}
			if (clrType == typeof(char))
			{
				clrValue = EdmToClrConverter.AsClrChar(edmValue);
				return true;
			}
			checked
			{
				if (clrType == typeof(sbyte))
				{
					clrValue = (sbyte)EdmToClrConverter.AsClrInt64(edmValue);
					return true;
				}
				if (clrType == typeof(byte))
				{
					clrValue = EdmToClrConverter.AsClrByte(edmValue);
					return true;
				}
				if (clrType == typeof(short))
				{
					clrValue = EdmToClrConverter.AsClrInt16(edmValue);
					return true;
				}
				if (clrType == typeof(ushort))
				{
					clrValue = (ushort)EdmToClrConverter.AsClrInt64(edmValue);
					return true;
				}
				if (clrType == typeof(int))
				{
					clrValue = EdmToClrConverter.AsClrInt32(edmValue);
					return true;
				}
				if (clrType == typeof(uint))
				{
					clrValue = (uint)EdmToClrConverter.AsClrInt64(edmValue);
					return true;
				}
				if (clrType == typeof(long))
				{
					clrValue = EdmToClrConverter.AsClrInt64(edmValue);
					return true;
				}
				if (clrType == typeof(ulong))
				{
					clrValue = (ulong)EdmToClrConverter.AsClrInt64(edmValue);
					return true;
				}
				if (clrType == typeof(float))
				{
					clrValue = EdmToClrConverter.AsClrSingle(edmValue);
					return true;
				}
				if (clrType == typeof(double))
				{
					clrValue = EdmToClrConverter.AsClrDouble(edmValue);
					return true;
				}
				if (clrType == typeof(decimal))
				{
					clrValue = EdmToClrConverter.AsClrDecimal(edmValue);
					return true;
				}
				if (clrType == typeof(string))
				{
					clrValue = EdmToClrConverter.AsClrString(edmValue);
					return true;
				}
				clrValue = null;
				return false;
			}
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x00012F44 File Offset: 0x00011144
		private static MethodInfo FindICollectionOfElementTypeAddMethod(Type collectionType, Type elementType)
		{
			Type type = typeof(ICollection).MakeGenericType(new Type[] { elementType });
			return type.GetMethod("Add");
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x00012F78 File Offset: 0x00011178
		private PropertyInfo FindProperty(Type clrObjectType, string propertyName)
		{
			if (this.tryGetClrPropertyInfoDelegate != null)
			{
				PropertyInfo propertyInfo = null;
				if (this.tryGetClrPropertyInfoDelegate(clrObjectType, propertyName, out propertyInfo))
				{
					return propertyInfo;
				}
				return null;
			}
			else
			{
				List<PropertyInfo> list = Enumerable.ToList<PropertyInfo>(Enumerable.Where<PropertyInfo>(clrObjectType.GetProperties(), (PropertyInfo p) => p.Name == propertyName));
				int count = list.Count;
				if (count == 0)
				{
					return null;
				}
				if (count != 1)
				{
					PropertyInfo propertyInfo2 = list[0];
					for (int i = 1; i < list.Count; i++)
					{
						PropertyInfo propertyInfo3 = list[i];
						if (propertyInfo2.DeclaringType.IsAssignableFrom(propertyInfo3.DeclaringType))
						{
							propertyInfo2 = propertyInfo3;
						}
					}
					return propertyInfo2;
				}
				return list[0];
			}
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x00013030 File Offset: 0x00011230
		private static string GetEdmValueInterfaceName(IEdmValue edmValue)
		{
			Type type = typeof(IEdmValue);
			foreach (Type type2 in Enumerable.OrderBy<Type, string>(edmValue.GetType().GetInterfaces(), (Type i) => i.FullName))
			{
				if (type.IsAssignableFrom(type2) && type != type2)
				{
					type = type2;
				}
			}
			return type.Name;
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x000130C0 File Offset: 0x000112C0
		private static bool IsBuiltInOrEnumType(Type type)
		{
			return type.IsPrimitive() || type == typeof(string) || type == typeof(decimal) || type.IsEnum();
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x000130EC File Offset: 0x000112EC
		private object AsClrValue(IEdmValue edmValue, Type clrType, bool convertEnumValues)
		{
			if (!EdmToClrConverter.IsBuiltInOrEnumType(clrType))
			{
				if (clrType.IsGenericType() && clrType.GetGenericTypeDefinition() == EdmToClrConverter.TypeNullableOfT)
				{
					if (edmValue is IEdmNullValue)
					{
						return null;
					}
					return this.AsClrValue(edmValue, Enumerable.Single<Type>(clrType.GetGenericArguments()));
				}
				else
				{
					if (clrType == typeof(Guid))
					{
						return EdmToClrConverter.AsClrGuid(edmValue);
					}
					if (clrType == typeof(Date))
					{
						return EdmToClrConverter.AsClrDate(edmValue);
					}
					if (clrType == typeof(DateTimeOffset))
					{
						return EdmToClrConverter.AsClrDateTimeOffset(edmValue);
					}
					if (clrType == typeof(TimeOfDay))
					{
						return EdmToClrConverter.AsClrTimeOfDay(edmValue);
					}
					if (clrType == typeof(TimeSpan))
					{
						return EdmToClrConverter.AsClrDuration(edmValue);
					}
					if (clrType == typeof(byte[]))
					{
						return EdmToClrConverter.AsClrByteArray(edmValue);
					}
					if (clrType.IsGenericType() && clrType.IsInterface() && (clrType.GetGenericTypeDefinition() == EdmToClrConverter.TypeICollectionOfT || clrType.GetGenericTypeDefinition() == EdmToClrConverter.TypeIListOfT || clrType.GetGenericTypeDefinition() == EdmToClrConverter.TypeIEnumerableOfT))
					{
						return this.AsListOfT(edmValue, clrType);
					}
					return this.AsClrObject(edmValue, clrType);
				}
			}
			else
			{
				bool flag = clrType.IsEnum();
				if (flag)
				{
					Type underlyingType = Enum.GetUnderlyingType(clrType);
					IEdmEnumValue edmEnumValue = edmValue as IEdmEnumValue;
					object obj = null;
					if (edmEnumValue != null)
					{
						EdmEnumMemberValue edmEnumMemberValue = edmEnumValue.Value as EdmEnumMemberValue;
						if (edmEnumMemberValue != null && !EdmToClrConverter.TryConvertEnumType(underlyingType, edmEnumMemberValue.Value, out obj))
						{
							throw new InvalidCastException(Strings.EdmToClr_UnsupportedType(underlyingType));
						}
					}
					else if (!EdmToClrConverter.TryConvertAsNonGuidPrimitiveType(underlyingType, edmValue, out obj))
					{
						throw new InvalidCastException(Strings.EdmToClr_UnsupportedType(underlyingType));
					}
					if (!convertEnumValues)
					{
						return obj;
					}
					return this.GetEnumValue(obj, clrType);
				}
				else
				{
					object obj2 = null;
					if (!EdmToClrConverter.TryConvertAsNonGuidPrimitiveType(clrType, edmValue, out obj2))
					{
						throw new InvalidCastException(Strings.EdmToClr_UnsupportedType(clrType));
					}
					return obj2;
				}
			}
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x000132A0 File Offset: 0x000114A0
		private static bool TryConvertEnumType(Type type, long enumValue, out object clrValue)
		{
			if (type == typeof(sbyte))
			{
				clrValue = (sbyte)enumValue;
				return true;
			}
			if (type == typeof(byte))
			{
				clrValue = (byte)enumValue;
				return true;
			}
			if (type == typeof(short))
			{
				clrValue = (short)enumValue;
				return true;
			}
			if (type == typeof(ushort))
			{
				clrValue = (ushort)enumValue;
				return true;
			}
			if (type == typeof(int))
			{
				clrValue = (int)enumValue;
				return true;
			}
			if (type == typeof(uint))
			{
				clrValue = (uint)enumValue;
				return true;
			}
			if (type == typeof(long))
			{
				clrValue = enumValue;
				return true;
			}
			clrValue = null;
			return false;
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x00013358 File Offset: 0x00011558
		private object AsListOfT(IEdmValue edmValue, Type clrType)
		{
			if (edmValue is IEdmNullValue)
			{
				return null;
			}
			Type type = Enumerable.Single<Type>(clrType.GetGenericArguments());
			MethodInfo methodInfo;
			if (!this.enumerableConverters.TryGetValue(type, ref methodInfo))
			{
				methodInfo = EdmToClrConverter.EnumerableToListOfTMethodInfo.MakeGenericMethod(new Type[] { type });
				this.enumerableConverters.Add(type, methodInfo);
			}
			object obj;
			try
			{
				obj = methodInfo.Invoke(null, new object[] { this.AsIEnumerable(edmValue, type) });
			}
			catch (TargetInvocationException ex)
			{
				if (ex.InnerException != null && ex.InnerException is InvalidCastException)
				{
					throw ex.InnerException;
				}
				throw;
			}
			return obj;
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x000133FC File Offset: 0x000115FC
		private object GetEnumValue(object clrValue, Type clrType)
		{
			MethodInfo methodInfo;
			if (!this.enumTypeConverters.TryGetValue(clrType, ref methodInfo))
			{
				methodInfo = EdmToClrConverter.CastToClrTypeMethodInfo.MakeGenericMethod(new Type[] { clrType });
				this.enumTypeConverters.Add(clrType, methodInfo);
			}
			object obj;
			try
			{
				obj = methodInfo.Invoke(null, new object[] { clrValue });
			}
			catch (TargetInvocationException ex)
			{
				if (ex.InnerException != null && ex.InnerException is InvalidCastException)
				{
					throw ex.InnerException;
				}
				throw;
			}
			return obj;
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x00013480 File Offset: 0x00011680
		private object AsClrObject(IEdmValue edmValue, Type clrObjectType)
		{
			EdmUtil.CheckArgumentNull<IEdmValue>(edmValue, "edmValue");
			EdmUtil.CheckArgumentNull<Type>(clrObjectType, "clrObjectType");
			if (edmValue is IEdmNullValue)
			{
				return null;
			}
			IEdmStructuredValue edmStructuredValue = edmValue as IEdmStructuredValue;
			if (edmStructuredValue == null)
			{
				if (edmValue is IEdmCollectionValue)
				{
					throw new InvalidCastException(Strings.EdmToClr_CannotConvertEdmCollectionValueToClrType(clrObjectType.FullName));
				}
				throw new InvalidCastException(Strings.EdmToClr_CannotConvertEdmValueToClrType(EdmToClrConverter.GetEdmValueInterfaceName(edmValue), clrObjectType.FullName));
			}
			else
			{
				object obj;
				if (this.convertedObjects.TryGetValue(edmStructuredValue, ref obj))
				{
					return obj;
				}
				if (!clrObjectType.IsClass())
				{
					throw new InvalidCastException(Strings.EdmToClr_StructuredValueMappedToNonClass);
				}
				bool flag;
				if (this.tryCreateObjectInstanceDelegate != null && this.tryCreateObjectInstanceDelegate(edmStructuredValue, clrObjectType, this, out obj, out flag))
				{
					if (obj != null)
					{
						Type type = obj.GetType();
						if (!clrObjectType.IsAssignableFrom(type))
						{
							throw new InvalidCastException(Strings.EdmToClr_TryCreateObjectInstanceReturnedWrongObject(type.FullName, clrObjectType.FullName));
						}
						clrObjectType = type;
					}
				}
				else
				{
					obj = Activator.CreateInstance(clrObjectType);
					flag = false;
				}
				this.convertedObjects[edmStructuredValue] = obj;
				if (!flag && obj != null)
				{
					this.PopulateObjectProperties(edmStructuredValue, obj, clrObjectType);
				}
				return obj;
			}
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x00013580 File Offset: 0x00011780
		private void PopulateObjectProperties(IEdmStructuredValue edmValue, object clrObject, Type clrObjectType)
		{
			HashSetInternal<string> hashSetInternal = new HashSetInternal<string>();
			foreach (IEdmPropertyValue edmPropertyValue in edmValue.PropertyValues)
			{
				PropertyInfo propertyInfo = this.FindProperty(clrObjectType, edmPropertyValue.Name);
				if (propertyInfo != null)
				{
					if (hashSetInternal.Contains(edmPropertyValue.Name))
					{
						throw new InvalidCastException(Strings.EdmToClr_StructuredPropertyDuplicateValue(edmPropertyValue.Name));
					}
					if (!this.TrySetCollectionProperty(propertyInfo, clrObject, edmPropertyValue))
					{
						object obj = this.AsClrValue(edmPropertyValue.Value, propertyInfo.PropertyType);
						propertyInfo.SetValue(clrObject, obj, null);
					}
					hashSetInternal.Add(edmPropertyValue.Name);
				}
			}
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x00013634 File Offset: 0x00011834
		private bool TrySetCollectionProperty(PropertyInfo clrProperty, object clrObject, IEdmPropertyValue propertyValue)
		{
			if (propertyValue.Value is IEdmNullValue)
			{
				return false;
			}
			Type propertyType = clrProperty.PropertyType;
			if (propertyType.IsGenericType())
			{
				Type genericTypeDefinition = propertyType.GetGenericTypeDefinition();
				bool flag = genericTypeDefinition == EdmToClrConverter.TypeIEnumerableOfT;
				IEnumerable<Type> interfaces = propertyType.GetInterfaces();
				if (!flag)
				{
					if (!Enumerable.Any<Type>(interfaces, (Type t) => t.GetGenericTypeDefinition() == EdmToClrConverter.TypeIEnumerableOfT))
					{
						return false;
					}
				}
				object obj = clrProperty.GetValue(clrObject, null);
				Type type = Enumerable.Single<Type>(propertyType.GetGenericArguments());
				Type type2;
				if (obj == null)
				{
					type2 = EdmToClrConverter.TypeListOfT.MakeGenericType(new Type[] { type });
					obj = Activator.CreateInstance(type2);
					clrProperty.SetValue(clrObject, obj, null);
				}
				else
				{
					if (flag)
					{
						throw new InvalidCastException(Strings.EdmToClr_IEnumerableOfTPropertyAlreadyHasValue(clrProperty.Name, clrProperty.DeclaringType.FullName));
					}
					type2 = obj.GetType();
				}
				MethodInfo methodInfo = EdmToClrConverter.FindICollectionOfElementTypeAddMethod(type2, type);
				foreach (object obj2 in this.AsIEnumerable(propertyValue.Value, type))
				{
					try
					{
						methodInfo.Invoke(obj, new object[] { obj2 });
					}
					catch (TargetInvocationException ex)
					{
						if (ex.InnerException != null && ex.InnerException is InvalidCastException)
						{
							throw ex.InnerException;
						}
						throw;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x000137BC File Offset: 0x000119BC
		private IEnumerable AsIEnumerable(IEdmValue edmValue, Type elementType)
		{
			foreach (IEdmDelayedValue edmDelayedValue in ((IEdmCollectionValue)edmValue).Elements)
			{
				if (edmDelayedValue.Value != null || (elementType.IsGenericType() && elementType.GetGenericTypeDefinition() == EdmToClrConverter.TypeNullableOfT))
				{
					yield return this.AsClrValue(edmDelayedValue.Value, elementType);
				}
			}
			IEnumerator<IEdmDelayedValue> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x04000401 RID: 1025
		private static readonly Type TypeICollectionOfT = typeof(ICollection);

		// Token: 0x04000402 RID: 1026
		private static readonly Type TypeIListOfT = typeof(IList);

		// Token: 0x04000403 RID: 1027
		private static readonly Type TypeListOfT = typeof(List);

		// Token: 0x04000404 RID: 1028
		private static readonly Type TypeIEnumerableOfT = typeof(IEnumerable);

		// Token: 0x04000405 RID: 1029
		private static readonly Type TypeNullableOfT = typeof(Nullable);

		// Token: 0x04000406 RID: 1030
		private static readonly MethodInfo CastToClrTypeMethodInfo = typeof(EdmToClrConverter.CastHelper).GetMethod("CastToClrType");

		// Token: 0x04000407 RID: 1031
		private static readonly MethodInfo EnumerableToListOfTMethodInfo = typeof(EdmToClrConverter.CastHelper).GetMethod("EnumerableToListOfT");

		// Token: 0x04000408 RID: 1032
		private readonly Dictionary<IEdmStructuredValue, object> convertedObjects = new Dictionary<IEdmStructuredValue, object>();

		// Token: 0x04000409 RID: 1033
		private readonly Dictionary<Type, MethodInfo> enumerableConverters = new Dictionary<Type, MethodInfo>();

		// Token: 0x0400040A RID: 1034
		private readonly Dictionary<Type, MethodInfo> enumTypeConverters = new Dictionary<Type, MethodInfo>();

		// Token: 0x0400040B RID: 1035
		private readonly TryCreateObjectInstance tryCreateObjectInstanceDelegate;

		// Token: 0x0400040C RID: 1036
		private readonly TryGetClrPropertyInfo tryGetClrPropertyInfoDelegate;

		// Token: 0x02000286 RID: 646
		private static class CastHelper
		{
			// Token: 0x06000F4C RID: 3916 RVA: 0x0002A7FD File Offset: 0x000289FD
			public static T CastToClrType<T>(object obj)
			{
				return (T)((object)obj);
			}

			// Token: 0x06000F4D RID: 3917 RVA: 0x0002A805 File Offset: 0x00028A05
			public static List<T> EnumerableToListOfT<T>(IEnumerable enumerable)
			{
				return Enumerable.ToList<T>(Enumerable.Cast<T>(enumerable));
			}
		}
	}
}
