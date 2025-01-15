using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x020000E6 RID: 230
	public class EdmToClrConverter
	{
		// Token: 0x060006F2 RID: 1778 RVA: 0x00010FE5 File Offset: 0x0000F1E5
		public EdmToClrConverter()
		{
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x0001100E File Offset: 0x0000F20E
		public EdmToClrConverter(TryCreateObjectInstance tryCreateObjectInstanceDelegate)
		{
			EdmUtil.CheckArgumentNull<TryCreateObjectInstance>(tryCreateObjectInstanceDelegate, "tryCreateObjectInstanceDelegate");
			this.tryCreateObjectInstanceDelegate = tryCreateObjectInstanceDelegate;
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x0001104A File Offset: 0x0000F24A
		public EdmToClrConverter(TryCreateObjectInstance tryCreateObjectInstanceDelegate, TryGetClrPropertyInfo tryGetClrPropertyInfoDelegate, TryGetClrTypeName tryGetClrTypeNameDelegate)
		{
			this.tryCreateObjectInstanceDelegate = tryCreateObjectInstanceDelegate;
			this.tryGetClrPropertyInfoDelegate = tryGetClrPropertyInfoDelegate;
			this.TryGetClrTypeNameDelegate = tryGetClrTypeNameDelegate;
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x060006F5 RID: 1781 RVA: 0x00011088 File Offset: 0x0000F288
		// (set) Token: 0x060006F6 RID: 1782 RVA: 0x00011090 File Offset: 0x0000F290
		internal TryGetClrTypeName TryGetClrTypeNameDelegate { get; private set; }

		// Token: 0x060006F7 RID: 1783 RVA: 0x0001109C File Offset: 0x0000F29C
		public T AsClrValue<T>(IEdmValue edmValue)
		{
			EdmUtil.CheckArgumentNull<IEdmValue>(edmValue, "edmValue");
			bool flag = false;
			return (T)((object)this.AsClrValue(edmValue, typeof(T), flag));
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x000110D0 File Offset: 0x0000F2D0
		public object AsClrValue(IEdmValue edmValue, Type clrType)
		{
			EdmUtil.CheckArgumentNull<IEdmValue>(edmValue, "edmValue");
			EdmUtil.CheckArgumentNull<Type>(clrType, "clrType");
			bool flag = true;
			return this.AsClrValue(edmValue, clrType, flag);
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x00011100 File Offset: 0x0000F300
		public void RegisterConvertedObject(IEdmStructuredValue edmValue, object clrObject)
		{
			this.convertedObjects.Add(edmValue, clrObject);
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x0001110F File Offset: 0x0000F30F
		internal static byte[] AsClrByteArray(IEdmValue edmValue)
		{
			EdmUtil.CheckArgumentNull<IEdmValue>(edmValue, "edmValue");
			if (edmValue is IEdmNullValue)
			{
				return null;
			}
			return ((IEdmBinaryValue)edmValue).Value;
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x00011132 File Offset: 0x0000F332
		internal static string AsClrString(IEdmValue edmValue)
		{
			EdmUtil.CheckArgumentNull<IEdmValue>(edmValue, "edmValue");
			if (edmValue is IEdmNullValue)
			{
				return null;
			}
			return ((IEdmStringValue)edmValue).Value;
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x00011155 File Offset: 0x0000F355
		internal static bool AsClrBoolean(IEdmValue edmValue)
		{
			EdmUtil.CheckArgumentNull<IEdmValue>(edmValue, "edmValue");
			return ((IEdmBooleanValue)edmValue).Value;
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x0001116E File Offset: 0x0000F36E
		internal static long AsClrInt64(IEdmValue edmValue)
		{
			EdmUtil.CheckArgumentNull<IEdmValue>(edmValue, "edmValue");
			return ((IEdmIntegerValue)edmValue).Value;
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x00011187 File Offset: 0x0000F387
		internal static char AsClrChar(IEdmValue edmValue)
		{
			return checked((char)EdmToClrConverter.AsClrInt64(edmValue));
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x00011190 File Offset: 0x0000F390
		internal static byte AsClrByte(IEdmValue edmValue)
		{
			return checked((byte)EdmToClrConverter.AsClrInt64(edmValue));
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x00011199 File Offset: 0x0000F399
		internal static short AsClrInt16(IEdmValue edmValue)
		{
			return checked((short)EdmToClrConverter.AsClrInt64(edmValue));
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x000111A2 File Offset: 0x0000F3A2
		internal static int AsClrInt32(IEdmValue edmValue)
		{
			return checked((int)EdmToClrConverter.AsClrInt64(edmValue));
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x000111AB File Offset: 0x0000F3AB
		internal static double AsClrDouble(IEdmValue edmValue)
		{
			EdmUtil.CheckArgumentNull<IEdmValue>(edmValue, "edmValue");
			return ((IEdmFloatingValue)edmValue).Value;
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x000111C4 File Offset: 0x0000F3C4
		internal static float AsClrSingle(IEdmValue edmValue)
		{
			return (float)EdmToClrConverter.AsClrDouble(edmValue);
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x000111CD File Offset: 0x0000F3CD
		internal static TimeOfDay AsClrTimeOfDay(IEdmValue edmValue)
		{
			EdmUtil.CheckArgumentNull<IEdmValue>(edmValue, "edmValue");
			return ((IEdmTimeOfDayValue)edmValue).Value;
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x000111E6 File Offset: 0x0000F3E6
		internal static Date AsClrDate(IEdmValue edmValue)
		{
			EdmUtil.CheckArgumentNull<IEdmValue>(edmValue, "edmValue");
			return ((IEdmDateValue)edmValue).Value;
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x000111FF File Offset: 0x0000F3FF
		internal static decimal AsClrDecimal(IEdmValue edmValue)
		{
			EdmUtil.CheckArgumentNull<IEdmValue>(edmValue, "edmValue");
			return ((IEdmDecimalValue)edmValue).Value;
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x00011218 File Offset: 0x0000F418
		internal static TimeSpan AsClrDuration(IEdmValue edmValue)
		{
			EdmUtil.CheckArgumentNull<IEdmValue>(edmValue, "edmValue");
			return ((IEdmDurationValue)edmValue).Value;
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x00011231 File Offset: 0x0000F431
		internal static Guid AsClrGuid(IEdmValue edmValue)
		{
			EdmUtil.CheckArgumentNull<IEdmValue>(edmValue, "edmValue");
			return ((IEdmGuidValue)edmValue).Value;
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x0001124A File Offset: 0x0000F44A
		internal static DateTimeOffset AsClrDateTimeOffset(IEdmValue edmValue)
		{
			EdmUtil.CheckArgumentNull<IEdmValue>(edmValue, "edmValue");
			return ((IEdmDateTimeOffsetValue)edmValue).Value;
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x00011264 File Offset: 0x0000F464
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

		// Token: 0x0600070B RID: 1803 RVA: 0x000113FC File Offset: 0x0000F5FC
		private static MethodInfo FindICollectionOfElementTypeAddMethod(Type collectionType, Type elementType)
		{
			Type type = typeof(ICollection<>).MakeGenericType(new Type[] { elementType });
			return type.GetMethod("Add");
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x00011430 File Offset: 0x0000F630
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
				List<PropertyInfo> list = (from p in clrObjectType.GetProperties()
					where p.Name == propertyName
					select p).ToList<PropertyInfo>();
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

		// Token: 0x0600070D RID: 1805 RVA: 0x000114E8 File Offset: 0x0000F6E8
		private static string GetEdmValueInterfaceName(IEdmValue edmValue)
		{
			Type type = typeof(IEdmValue);
			foreach (Type type2 in from i in edmValue.GetType().GetInterfaces()
				orderby i.FullName
				select i)
			{
				if (type.IsAssignableFrom(type2) && type != type2)
				{
					type = type2;
				}
			}
			return type.Name;
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x00011578 File Offset: 0x0000F778
		private static bool IsBuiltInOrEnumType(Type type)
		{
			return type.IsPrimitive() || type == typeof(string) || type == typeof(decimal) || type.IsEnum();
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x000115A4 File Offset: 0x0000F7A4
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
					return this.AsClrValue(edmValue, clrType.GetGenericArguments().Single<Type>());
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

		// Token: 0x06000710 RID: 1808 RVA: 0x00011758 File Offset: 0x0000F958
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

		// Token: 0x06000711 RID: 1809 RVA: 0x00011810 File Offset: 0x0000FA10
		private object AsListOfT(IEdmValue edmValue, Type clrType)
		{
			if (edmValue is IEdmNullValue)
			{
				return null;
			}
			Type type = clrType.GetGenericArguments().Single<Type>();
			MethodInfo methodInfo;
			if (!this.enumerableConverters.TryGetValue(type, out methodInfo))
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

		// Token: 0x06000712 RID: 1810 RVA: 0x000118B4 File Offset: 0x0000FAB4
		private object GetEnumValue(object clrValue, Type clrType)
		{
			MethodInfo methodInfo;
			if (!this.enumTypeConverters.TryGetValue(clrType, out methodInfo))
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

		// Token: 0x06000713 RID: 1811 RVA: 0x00011938 File Offset: 0x0000FB38
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
				if (this.convertedObjects.TryGetValue(edmStructuredValue, out obj))
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

		// Token: 0x06000714 RID: 1812 RVA: 0x00011A38 File Offset: 0x0000FC38
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

		// Token: 0x06000715 RID: 1813 RVA: 0x00011AEC File Offset: 0x0000FCEC
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
					if (!interfaces.Any((Type t) => t.GetGenericTypeDefinition() == EdmToClrConverter.TypeIEnumerableOfT))
					{
						return false;
					}
				}
				object obj = clrProperty.GetValue(clrObject, null);
				Type type = propertyType.GetGenericArguments().Single<Type>();
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

		// Token: 0x06000716 RID: 1814 RVA: 0x00011C74 File Offset: 0x0000FE74
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

		// Token: 0x040002F5 RID: 757
		private static readonly Type TypeICollectionOfT = typeof(ICollection<>);

		// Token: 0x040002F6 RID: 758
		private static readonly Type TypeIListOfT = typeof(IList<>);

		// Token: 0x040002F7 RID: 759
		private static readonly Type TypeListOfT = typeof(List<>);

		// Token: 0x040002F8 RID: 760
		private static readonly Type TypeIEnumerableOfT = typeof(IEnumerable<>);

		// Token: 0x040002F9 RID: 761
		private static readonly Type TypeNullableOfT = typeof(Nullable<>);

		// Token: 0x040002FA RID: 762
		private static readonly MethodInfo CastToClrTypeMethodInfo = typeof(EdmToClrConverter.CastHelper).GetMethod("CastToClrType");

		// Token: 0x040002FB RID: 763
		private static readonly MethodInfo EnumerableToListOfTMethodInfo = typeof(EdmToClrConverter.CastHelper).GetMethod("EnumerableToListOfT");

		// Token: 0x040002FC RID: 764
		private readonly Dictionary<IEdmStructuredValue, object> convertedObjects = new Dictionary<IEdmStructuredValue, object>();

		// Token: 0x040002FD RID: 765
		private readonly Dictionary<Type, MethodInfo> enumerableConverters = new Dictionary<Type, MethodInfo>();

		// Token: 0x040002FE RID: 766
		private readonly Dictionary<Type, MethodInfo> enumTypeConverters = new Dictionary<Type, MethodInfo>();

		// Token: 0x040002FF RID: 767
		private readonly TryCreateObjectInstance tryCreateObjectInstanceDelegate;

		// Token: 0x04000300 RID: 768
		private readonly TryGetClrPropertyInfo tryGetClrPropertyInfoDelegate;

		// Token: 0x02000247 RID: 583
		private static class CastHelper
		{
			// Token: 0x06000EFC RID: 3836 RVA: 0x0002809C File Offset: 0x0002629C
			public static T CastToClrType<T>(object obj)
			{
				return (T)((object)obj);
			}

			// Token: 0x06000EFD RID: 3837 RVA: 0x000280A4 File Offset: 0x000262A4
			public static List<T> EnumerableToListOfT<T>(IEnumerable enumerable)
			{
				return enumerable.Cast<T>().ToList<T>();
			}
		}
	}
}
