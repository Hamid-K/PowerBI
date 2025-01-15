using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.OData.Edm.Library;
using Microsoft.OData.Edm.Values;

namespace Microsoft.OData.Edm.EdmToClrConversion
{
	// Token: 0x020000DC RID: 220
	public class EdmToClrConverter
	{
		// Token: 0x06000459 RID: 1113 RVA: 0x0000A921 File Offset: 0x00008B21
		public EdmToClrConverter()
		{
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x0000A94A File Offset: 0x00008B4A
		public EdmToClrConverter(TryCreateObjectInstance tryCreateObjectInstanceDelegate)
		{
			EdmUtil.CheckArgumentNull<TryCreateObjectInstance>(tryCreateObjectInstanceDelegate, "tryCreateObjectInstanceDelegate");
			this.tryCreateObjectInstanceDelegate = tryCreateObjectInstanceDelegate;
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x0000A986 File Offset: 0x00008B86
		public EdmToClrConverter(TryCreateObjectInstance tryCreateObjectInstanceDelegate, TryGetClrPropertyInfo tryGetClrPropertyInfoDelegate, TryGetClrTypeName tryGetClrTypeNameDelegate)
		{
			this.tryCreateObjectInstanceDelegate = tryCreateObjectInstanceDelegate;
			this.tryGetClrPropertyInfoDelegate = tryGetClrPropertyInfoDelegate;
			this.TryGetClrTypeNameDelegate = tryGetClrTypeNameDelegate;
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x0600045C RID: 1116 RVA: 0x0000A9C4 File Offset: 0x00008BC4
		// (set) Token: 0x0600045D RID: 1117 RVA: 0x0000A9CC File Offset: 0x00008BCC
		internal TryGetClrTypeName TryGetClrTypeNameDelegate { get; private set; }

		// Token: 0x0600045E RID: 1118 RVA: 0x0000A9D8 File Offset: 0x00008BD8
		public T AsClrValue<T>(IEdmValue edmValue)
		{
			EdmUtil.CheckArgumentNull<IEdmValue>(edmValue, "edmValue");
			bool flag = false;
			return (T)((object)this.AsClrValue(edmValue, typeof(T), flag));
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x0000AA0C File Offset: 0x00008C0C
		public object AsClrValue(IEdmValue edmValue, Type clrType)
		{
			EdmUtil.CheckArgumentNull<IEdmValue>(edmValue, "edmValue");
			EdmUtil.CheckArgumentNull<Type>(clrType, "clrType");
			bool flag = true;
			return this.AsClrValue(edmValue, clrType, flag);
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x0000AA3C File Offset: 0x00008C3C
		public void RegisterConvertedObject(IEdmStructuredValue edmValue, object clrObject)
		{
			this.convertedObjects.Add(edmValue, clrObject);
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x0000AA4B File Offset: 0x00008C4B
		internal static byte[] AsClrByteArray(IEdmValue edmValue)
		{
			EdmUtil.CheckArgumentNull<IEdmValue>(edmValue, "edmValue");
			if (edmValue is IEdmNullValue)
			{
				return null;
			}
			return ((IEdmBinaryValue)edmValue).Value;
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x0000AA6E File Offset: 0x00008C6E
		internal static string AsClrString(IEdmValue edmValue)
		{
			EdmUtil.CheckArgumentNull<IEdmValue>(edmValue, "edmValue");
			if (edmValue is IEdmNullValue)
			{
				return null;
			}
			return ((IEdmStringValue)edmValue).Value;
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x0000AA91 File Offset: 0x00008C91
		internal static bool AsClrBoolean(IEdmValue edmValue)
		{
			EdmUtil.CheckArgumentNull<IEdmValue>(edmValue, "edmValue");
			return ((IEdmBooleanValue)edmValue).Value;
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x0000AAAA File Offset: 0x00008CAA
		internal static long AsClrInt64(IEdmValue edmValue)
		{
			EdmUtil.CheckArgumentNull<IEdmValue>(edmValue, "edmValue");
			return ((IEdmIntegerValue)edmValue).Value;
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x0000AAC3 File Offset: 0x00008CC3
		internal static char AsClrChar(IEdmValue edmValue)
		{
			return checked((char)EdmToClrConverter.AsClrInt64(edmValue));
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x0000AACC File Offset: 0x00008CCC
		internal static byte AsClrByte(IEdmValue edmValue)
		{
			return checked((byte)EdmToClrConverter.AsClrInt64(edmValue));
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x0000AAD5 File Offset: 0x00008CD5
		internal static short AsClrInt16(IEdmValue edmValue)
		{
			return checked((short)EdmToClrConverter.AsClrInt64(edmValue));
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x0000AADE File Offset: 0x00008CDE
		internal static int AsClrInt32(IEdmValue edmValue)
		{
			return checked((int)EdmToClrConverter.AsClrInt64(edmValue));
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x0000AAE7 File Offset: 0x00008CE7
		internal static double AsClrDouble(IEdmValue edmValue)
		{
			EdmUtil.CheckArgumentNull<IEdmValue>(edmValue, "edmValue");
			return ((IEdmFloatingValue)edmValue).Value;
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x0000AB00 File Offset: 0x00008D00
		internal static float AsClrSingle(IEdmValue edmValue)
		{
			return (float)EdmToClrConverter.AsClrDouble(edmValue);
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x0000AB09 File Offset: 0x00008D09
		internal static TimeOfDay AsClrTimeOfDay(IEdmValue edmValue)
		{
			EdmUtil.CheckArgumentNull<IEdmValue>(edmValue, "edmValue");
			return ((IEdmTimeOfDayValue)edmValue).Value;
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x0000AB22 File Offset: 0x00008D22
		internal static Date AsClrDate(IEdmValue edmValue)
		{
			EdmUtil.CheckArgumentNull<IEdmValue>(edmValue, "edmValue");
			return ((IEdmDateValue)edmValue).Value;
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x0000AB3B File Offset: 0x00008D3B
		internal static decimal AsClrDecimal(IEdmValue edmValue)
		{
			EdmUtil.CheckArgumentNull<IEdmValue>(edmValue, "edmValue");
			return ((IEdmDecimalValue)edmValue).Value;
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x0000AB54 File Offset: 0x00008D54
		internal static TimeSpan AsClrDuration(IEdmValue edmValue)
		{
			EdmUtil.CheckArgumentNull<IEdmValue>(edmValue, "edmValue");
			return ((IEdmDurationValue)edmValue).Value;
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x0000AB6D File Offset: 0x00008D6D
		internal static Guid AsClrGuid(IEdmValue edmValue)
		{
			EdmUtil.CheckArgumentNull<IEdmValue>(edmValue, "edmValue");
			return ((IEdmGuidValue)edmValue).Value;
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x0000AB86 File Offset: 0x00008D86
		internal static DateTimeOffset AsClrDateTimeOffset(IEdmValue edmValue)
		{
			EdmUtil.CheckArgumentNull<IEdmValue>(edmValue, "edmValue");
			return ((IEdmDateTimeOffsetValue)edmValue).Value;
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x0000ABA0 File Offset: 0x00008DA0
		private static bool TryConvertAsNonGuidPrimitiveType(TypeCode typeCode, IEdmValue edmValue, out object clrValue)
		{
			checked
			{
				switch (typeCode)
				{
				case 3:
					clrValue = EdmToClrConverter.AsClrBoolean(edmValue);
					return true;
				case 4:
					clrValue = EdmToClrConverter.AsClrChar(edmValue);
					return true;
				case 5:
					clrValue = (sbyte)EdmToClrConverter.AsClrInt64(edmValue);
					return true;
				case 6:
					clrValue = EdmToClrConverter.AsClrByte(edmValue);
					return true;
				case 7:
					clrValue = EdmToClrConverter.AsClrInt16(edmValue);
					return true;
				case 8:
					clrValue = (ushort)EdmToClrConverter.AsClrInt64(edmValue);
					return true;
				case 9:
					clrValue = EdmToClrConverter.AsClrInt32(edmValue);
					return true;
				case 10:
					clrValue = (uint)EdmToClrConverter.AsClrInt64(edmValue);
					return true;
				case 11:
					clrValue = EdmToClrConverter.AsClrInt64(edmValue);
					return true;
				case 12:
					clrValue = (ulong)EdmToClrConverter.AsClrInt64(edmValue);
					return true;
				case 13:
					clrValue = EdmToClrConverter.AsClrSingle(edmValue);
					return true;
				case 14:
					clrValue = EdmToClrConverter.AsClrDouble(edmValue);
					return true;
				case 15:
					clrValue = EdmToClrConverter.AsClrDecimal(edmValue);
					return true;
				case 18:
					clrValue = EdmToClrConverter.AsClrString(edmValue);
					return true;
				}
				clrValue = null;
				return false;
			}
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x0000ACD4 File Offset: 0x00008ED4
		private static MethodInfo FindICollectionOfElementTypeAddMethod(Type collectionType, Type elementType)
		{
			Type type = typeof(ICollection).MakeGenericType(new Type[] { elementType });
			return type.GetMethod("Add");
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x0000AD24 File Offset: 0x00008F24
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
				switch (list.Count)
				{
				case 0:
					return null;
				case 1:
					return list[0];
				default:
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
				}
			}
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x0000ADF4 File Offset: 0x00008FF4
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

		// Token: 0x06000475 RID: 1141 RVA: 0x0000AE84 File Offset: 0x00009084
		private object AsClrValue(IEdmValue edmValue, Type clrType, bool convertEnumValues)
		{
			TypeCode typeCode = PlatformHelper.GetTypeCode(clrType);
			if (typeCode == 1)
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
					IEdmEnumValue edmEnumValue = edmValue as IEdmEnumValue;
					if (edmEnumValue != null)
					{
						edmValue = edmEnumValue.Value;
					}
				}
				object obj;
				if (!EdmToClrConverter.TryConvertAsNonGuidPrimitiveType(PlatformHelper.GetTypeCode(clrType), edmValue, out obj))
				{
					throw new InvalidCastException(Strings.EdmToClr_UnsupportedTypeCode(typeCode));
				}
				if (!flag || !convertEnumValues)
				{
					return obj;
				}
				return this.GetEnumValue(obj, clrType);
			}
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x0000AFF8 File Offset: 0x000091F8
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

		// Token: 0x06000477 RID: 1143 RVA: 0x0000B0A4 File Offset: 0x000092A4
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

		// Token: 0x06000478 RID: 1144 RVA: 0x0000B130 File Offset: 0x00009330
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

		// Token: 0x06000479 RID: 1145 RVA: 0x0000B230 File Offset: 0x00009430
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

		// Token: 0x0600047A RID: 1146 RVA: 0x0000B2F8 File Offset: 0x000094F8
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

		// Token: 0x0600047B RID: 1147 RVA: 0x0000B67C File Offset: 0x0000987C
		private IEnumerable AsIEnumerable(IEdmValue edmValue, Type elementType)
		{
			foreach (IEdmDelayedValue element in ((IEdmCollectionValue)edmValue).Elements)
			{
				if (element.Value != null || (elementType.IsGenericType() && elementType.GetGenericTypeDefinition() == EdmToClrConverter.TypeNullableOfT))
				{
					yield return this.AsClrValue(element.Value, elementType);
				}
			}
			yield break;
		}

		// Token: 0x040001A7 RID: 423
		private static readonly Type TypeICollectionOfT = typeof(ICollection);

		// Token: 0x040001A8 RID: 424
		private static readonly Type TypeIListOfT = typeof(IList);

		// Token: 0x040001A9 RID: 425
		private static readonly Type TypeListOfT = typeof(List);

		// Token: 0x040001AA RID: 426
		private static readonly Type TypeIEnumerableOfT = typeof(IEnumerable);

		// Token: 0x040001AB RID: 427
		private static readonly Type TypeNullableOfT = typeof(Nullable);

		// Token: 0x040001AC RID: 428
		private static readonly MethodInfo CastToClrTypeMethodInfo = typeof(EdmToClrConverter.CastHelper).GetMethod("CastToClrType");

		// Token: 0x040001AD RID: 429
		private static readonly MethodInfo EnumerableToListOfTMethodInfo = typeof(EdmToClrConverter.CastHelper).GetMethod("EnumerableToListOfT");

		// Token: 0x040001AE RID: 430
		private readonly Dictionary<IEdmStructuredValue, object> convertedObjects = new Dictionary<IEdmStructuredValue, object>();

		// Token: 0x040001AF RID: 431
		private readonly Dictionary<Type, MethodInfo> enumerableConverters = new Dictionary<Type, MethodInfo>();

		// Token: 0x040001B0 RID: 432
		private readonly Dictionary<Type, MethodInfo> enumTypeConverters = new Dictionary<Type, MethodInfo>();

		// Token: 0x040001B1 RID: 433
		private readonly TryCreateObjectInstance tryCreateObjectInstanceDelegate;

		// Token: 0x040001B2 RID: 434
		private readonly TryGetClrPropertyInfo tryGetClrPropertyInfoDelegate;

		// Token: 0x020000DD RID: 221
		private static class CastHelper
		{
			// Token: 0x0600047F RID: 1151 RVA: 0x0000B732 File Offset: 0x00009932
			public static T CastToClrType<T>(object obj)
			{
				return (T)((object)obj);
			}

			// Token: 0x06000480 RID: 1152 RVA: 0x0000B73A File Offset: 0x0000993A
			public static List<T> EnumerableToListOfT<T>(IEnumerable enumerable)
			{
				return Enumerable.ToList<T>(Enumerable.Cast<T>(enumerable));
			}
		}
	}
}
