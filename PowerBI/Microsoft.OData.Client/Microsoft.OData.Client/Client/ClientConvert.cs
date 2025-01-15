using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;

namespace Microsoft.OData.Client
{
	// Token: 0x020000CB RID: 203
	internal static class ClientConvert
	{
		// Token: 0x06000695 RID: 1685 RVA: 0x0001C19C File Offset: 0x0001A39C
		internal static object ChangeType(string propertyValue, Type propertyType)
		{
			PrimitiveType primitiveType;
			if (PrimitiveType.TryGetPrimitiveType(propertyType, out primitiveType) && primitiveType.TypeConverter != null)
			{
				try
				{
					return primitiveType.TypeConverter.Parse(propertyValue);
				}
				catch (FormatException ex)
				{
					propertyValue = ((propertyValue.Length == 0) ? "String.Empty" : "String");
					throw Error.InvalidOperation(Strings.Deserialize_Current(propertyType.ToString(), propertyValue), ex);
				}
				catch (OverflowException ex2)
				{
					propertyValue = ((propertyValue.Length == 0) ? "String.Empty" : "String");
					throw Error.InvalidOperation(Strings.Deserialize_Current(propertyType.ToString(), propertyValue), ex2);
				}
				return propertyValue;
			}
			return propertyValue;
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x0001C240 File Offset: 0x0001A440
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "binaryValue", Justification = "Method is compiled into desktop and SL assemblies, and the parameter is used in the desktop version.")]
		internal static bool TryConvertBinaryToByteArray(object binaryValue, out byte[] converted)
		{
			Type type = binaryValue.GetType();
			PrimitiveType primitiveType;
			if (PrimitiveType.TryGetPrimitiveType(type, out primitiveType) && type == BinaryTypeConverter.BinaryType)
			{
				converted = (byte[])type.InvokeMember("ToArray", BindingFlags.Instance | BindingFlags.Public | BindingFlags.InvokeMethod, null, binaryValue, null, CultureInfo.InvariantCulture);
				return true;
			}
			converted = null;
			return false;
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x0001C290 File Offset: 0x0001A490
		internal static bool ToNamedType(string typeName, out Type type)
		{
			type = typeof(string);
			if (string.IsNullOrEmpty(typeName))
			{
				return true;
			}
			PrimitiveType primitiveType;
			if (PrimitiveType.TryGetPrimitiveType(typeName, out primitiveType))
			{
				type = primitiveType.ClrType;
				return true;
			}
			return false;
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x0001C2C8 File Offset: 0x0001A4C8
		internal static string ToString(object propertyValue)
		{
			PrimitiveType primitiveType;
			if (PrimitiveType.TryGetPrimitiveType(propertyValue.GetType(), out primitiveType) && primitiveType.TypeConverter != null)
			{
				return primitiveType.TypeConverter.ToString(propertyValue);
			}
			ODataEnumValue odataEnumValue = propertyValue as ODataEnumValue;
			if (odataEnumValue != null)
			{
				return odataEnumValue.Value;
			}
			return propertyValue.ToString();
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x0001C310 File Offset: 0x0001A510
		internal static string GetEdmType(Type propertyType)
		{
			PrimitiveType primitiveType;
			if (!PrimitiveType.TryGetPrimitiveType(propertyType, out primitiveType))
			{
				return null;
			}
			if (primitiveType.ClrType == typeof(DateTime))
			{
				return "Edm.DateTimeOffset";
			}
			if (primitiveType.EdmTypeName != null)
			{
				return primitiveType.EdmTypeName;
			}
			throw new NotSupportedException(Strings.ALinq_CantCastToUnsupportedPrimitive(propertyType.Name));
		}
	}
}
