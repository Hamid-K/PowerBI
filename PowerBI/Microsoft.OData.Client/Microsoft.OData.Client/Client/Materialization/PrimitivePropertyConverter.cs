using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Linq;
using Microsoft.Spatial;

namespace Microsoft.OData.Client.Materialization
{
	// Token: 0x020000FE RID: 254
	internal class PrimitivePropertyConverter
	{
		// Token: 0x06000ABD RID: 2749 RVA: 0x0002897C File Offset: 0x00026B7C
		internal object ConvertPrimitiveValue(object value, Type propertyType)
		{
			if (propertyType != null && value != null)
			{
				if (!PrimitiveType.IsKnownNullableType(propertyType))
				{
					throw new InvalidOperationException(Strings.ClientType_UnsupportedType(propertyType));
				}
				Type type = Nullable.GetUnderlyingType(propertyType) ?? propertyType;
				if (PrimitivePropertyConverter.IsSupportedODataPrimitiveType(type))
				{
					return this.ConvertValueIfNeeded(value, propertyType);
				}
				if (PrimitivePropertyConverter.CanMapToODataPrimitiveType(type))
				{
					PrimitiveType primitiveType;
					PrimitiveType.TryGetPrimitiveType(propertyType, out primitiveType);
					string text = (string)this.ConvertValueIfNeeded(value, typeof(string));
					return primitiveType.TypeConverter.Parse(text);
				}
				if (propertyType == BinaryTypeConverter.BinaryType)
				{
					byte[] array = (byte[])this.ConvertValueIfNeeded(value, typeof(byte[]));
					return Activator.CreateInstance(BinaryTypeConverter.BinaryType, new object[] { array });
				}
			}
			return this.ConvertValueIfNeeded(value, propertyType);
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x00028A44 File Offset: 0x00026C44
		private static bool IsSupportedODataPrimitiveType(Type type)
		{
			return type == typeof(bool) || type == typeof(byte) || type == typeof(decimal) || type == typeof(double) || type == typeof(short) || type == typeof(int) || type == typeof(long) || type == typeof(sbyte) || type == typeof(float) || type == typeof(string);
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x00028B0C File Offset: 0x00026D0C
		private static bool CanMapToODataPrimitiveType(Type type)
		{
			return type == typeof(char) || type == typeof(ushort) || type == typeof(uint) || type == typeof(ulong) || type == typeof(char[]) || type == typeof(Type) || type == typeof(Uri) || type == typeof(XDocument) || type == typeof(XElement);
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x00028BC0 File Offset: 0x00026DC0
		private static object ConvertNonSpatialValue(object value, Type targetType)
		{
			if (PrimitivePropertyConverter.CanSafelyConvertTo(targetType))
			{
				return Convert.ChangeType(value, targetType, CultureInfo.InvariantCulture);
			}
			string text = ClientConvert.ToString(value);
			return ClientConvert.ChangeType(text, targetType);
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x00028BF0 File Offset: 0x00026DF0
		private static bool CanSafelyConvertTo(Type targetType)
		{
			return targetType == typeof(bool) || targetType == typeof(byte) || targetType == typeof(sbyte) || targetType == typeof(short) || targetType == typeof(int) || targetType == typeof(long);
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x00028C6C File Offset: 0x00026E6C
		private object ConvertValueIfNeeded(object value, Type targetType)
		{
			if (value == null || targetType.IsInstanceOfType(value))
			{
				return value;
			}
			if (typeof(ISpatial).IsAssignableFrom(targetType) || value is ISpatial)
			{
				return this.ConvertSpatialValue(value, targetType);
			}
			Type underlyingType = Nullable.GetUnderlyingType(targetType);
			if (underlyingType != null)
			{
				targetType = underlyingType;
			}
			return PrimitivePropertyConverter.ConvertNonSpatialValue(value, targetType);
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x00028CC4 File Offset: 0x00026EC4
		private object ConvertSpatialValue(object value, Type targetType)
		{
			if (typeof(Geometry).IsAssignableFrom(targetType))
			{
				Geography geography = value as Geography;
				if (geography == null)
				{
					return value;
				}
				return this.ConvertSpatialValue<Geography, Geometry>(geography);
			}
			else
			{
				Geometry geometry = value as Geometry;
				if (geometry == null)
				{
					return value;
				}
				return this.ConvertSpatialValue<Geometry, Geography>(geometry);
			}
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x00028D0C File Offset: 0x00026F0C
		private TOut ConvertSpatialValue<TIn, TOut>(TIn valueToConvert) where TIn : ISpatial where TOut : class, ISpatial
		{
			IDictionary<string, object> dictionary = this.lazyGeoJsonFormatter.Value.Write(valueToConvert);
			return this.lazyGeoJsonFormatter.Value.Read<TOut>(dictionary);
		}

		// Token: 0x04000619 RID: 1561
		private readonly SimpleLazy<GeoJsonObjectFormatter> lazyGeoJsonFormatter = new SimpleLazy<GeoJsonObjectFormatter>(new Func<GeoJsonObjectFormatter>(GeoJsonObjectFormatter.Create));
	}
}
