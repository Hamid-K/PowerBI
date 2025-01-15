using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Library;
using Microsoft.Spatial;

namespace Microsoft.Mashup.Engine1.Library.OData.V4
{
	// Token: 0x02000896 RID: 2198
	internal static class ODataTypeServices
	{
		// Token: 0x06003F14 RID: 16148 RVA: 0x000CED3C File Offset: 0x000CCF3C
		public static Microsoft.OData.Edm.IEdmType FindEdmType(Microsoft.OData.Edm.IEdmModel model, string qualifiedEdmName)
		{
			if (qualifiedEdmName.StartsWith("Collection", StringComparison.Ordinal) || qualifiedEdmName.StartsWith("Edm.Collection", StringComparison.Ordinal))
			{
				return ODataTypeServices.FindEdmCollectionType(model, qualifiedEdmName);
			}
			Microsoft.OData.Edm.EdmPrimitiveTypeKind primitiveTypeKind = Microsoft.OData.Edm.Library.EdmCoreModel.Instance.GetPrimitiveTypeKind(qualifiedEdmName);
			if (primitiveTypeKind != Microsoft.OData.Edm.EdmPrimitiveTypeKind.None)
			{
				return Microsoft.OData.Edm.Library.EdmCoreModel.Instance.GetPrimitiveType(primitiveTypeKind);
			}
			return model.FindDeclaredType(qualifiedEdmName);
		}

		// Token: 0x06003F15 RID: 16149 RVA: 0x000CED90 File Offset: 0x000CCF90
		public static Microsoft.OData.Edm.IEdmPrimitiveTypeReference GetEdmPrimitiveTypeReference(TypeValue type, bool isKey)
		{
			Microsoft.OData.Edm.EdmPrimitiveTypeKind edmTypeKind = ODataTypeServices.GetEdmTypeKind(type);
			if (edmTypeKind != Microsoft.OData.Edm.EdmPrimitiveTypeKind.None)
			{
				return Microsoft.OData.Edm.Library.EdmCoreModel.Instance.GetPrimitive(edmTypeKind, !isKey && ODataTypeServices.IsNullablePrimitiveType(edmTypeKind, type.IsNullable));
			}
			return null;
		}

		// Token: 0x06003F16 RID: 16150 RVA: 0x000CEDC8 File Offset: 0x000CCFC8
		public static TypeValue GetTypeValueFromEdm(Microsoft.OData.Edm.EdmPrimitiveTypeKind typeKind)
		{
			switch (typeKind)
			{
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.None:
				return TypeValue.Any;
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.Binary:
				return TypeValue.Binary;
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.Boolean:
				return TypeValue.Logical;
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.Byte:
				return TypeValue.Byte;
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.DateTimeOffset:
				return TypeValue.DateTimeZone;
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.Decimal:
				return TypeValue.Decimal;
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.Double:
				return TypeValue.Double;
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.Guid:
				return TypeValue.Guid;
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.Int16:
				return TypeValue.Int16;
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.Int32:
				return TypeValue.Int32;
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.Int64:
				return TypeValue.Int64;
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.SByte:
				return TypeValue.Int8;
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.Single:
				return TypeValue.Single;
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.String:
				return TypeValue.Text;
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.Stream:
				return TypeValue.Binary;
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.Duration:
				return TypeValue.Duration;
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.Geography:
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.GeographyPoint:
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.GeographyLineString:
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.GeographyPolygon:
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.GeographyCollection:
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.GeographyMultiPolygon:
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.GeographyMultiLineString:
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.GeographyMultiPoint:
				return SerializedTextTypeValue.SerializedGeographyType;
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.Geometry:
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.GeometryPoint:
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.GeometryLineString:
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.GeometryPolygon:
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.GeometryCollection:
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.GeometryMultiPolygon:
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.GeometryMultiLineString:
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.GeometryMultiPoint:
				return SerializedTextTypeValue.SerializedGeometryType;
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.Date:
				return TypeValue.Date;
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.TimeOfDay:
				return TypeValue.Time;
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06003F17 RID: 16151 RVA: 0x000CEEE4 File Offset: 0x000CD0E4
		public static Microsoft.OData.Edm.IEdmType StripCollectionType(Microsoft.OData.Edm.IEdmType type)
		{
			Microsoft.OData.Edm.IEdmCollectionType edmCollectionType = type as Microsoft.OData.Edm.IEdmCollectionType;
			if (edmCollectionType != null)
			{
				return edmCollectionType.ElementType.Definition;
			}
			return type;
		}

		// Token: 0x06003F18 RID: 16152 RVA: 0x000CEF08 File Offset: 0x000CD108
		public static bool IsCompatible(this Microsoft.OData.Edm.IEdmTypeReference typeReference, Microsoft.OData.Edm.IEdmTypeReference typeReference2)
		{
			if (typeReference == null || typeReference2 == null)
			{
				return false;
			}
			Microsoft.OData.Edm.IEdmType definition = typeReference.Definition;
			Microsoft.OData.Edm.IEdmType definition2 = typeReference2.Definition;
			if (definition.TypeKind != definition2.TypeKind)
			{
				return false;
			}
			if (definition.TypeKind == Microsoft.OData.Edm.EdmTypeKind.Primitive)
			{
				Microsoft.OData.Edm.IEdmPrimitiveType edmPrimitiveType = (Microsoft.OData.Edm.IEdmPrimitiveType)definition;
				Microsoft.OData.Edm.IEdmPrimitiveType edmPrimitiveType2 = (Microsoft.OData.Edm.IEdmPrimitiveType)definition2;
				if (edmPrimitiveType.PrimitiveKind == edmPrimitiveType2.PrimitiveKind)
				{
					return true;
				}
				if (edmPrimitiveType.PrimitiveKind.IsNumericKind() && edmPrimitiveType2.PrimitiveKind.IsNumericKind())
				{
					return true;
				}
			}
			else if (definition.TypeKind == Microsoft.OData.Edm.EdmTypeKind.Complex || definition.TypeKind == Microsoft.OData.Edm.EdmTypeKind.Entity)
			{
				Microsoft.OData.Edm.IEdmStructuredType edmStructuredType = (Microsoft.OData.Edm.IEdmStructuredType)definition;
				return ((Microsoft.OData.Edm.IEdmStructuredType)definition).IsOrInheritsFrom(edmStructuredType);
			}
			return definition.IsEquivalentTo(definition2);
		}

		// Token: 0x06003F19 RID: 16153 RVA: 0x000CEFB0 File Offset: 0x000CD1B0
		private static Microsoft.OData.Edm.IEdmCollectionType FindEdmCollectionType(Microsoft.OData.Edm.IEdmModel model, string qualifiedEdmName)
		{
			if (!qualifiedEdmName.StartsWith("Collection", StringComparison.Ordinal) && !qualifiedEdmName.StartsWith("Edm.Collection", StringComparison.Ordinal))
			{
				return null;
			}
			int num = qualifiedEdmName.IndexOf("(", StringComparison.Ordinal);
			string text = qualifiedEdmName.TrimEnd(new char[] { ')' }).Remove(0, num + 1);
			Microsoft.OData.Edm.EdmPrimitiveTypeKind primitiveTypeKind = Microsoft.OData.Edm.Library.EdmCoreModel.Instance.GetPrimitiveTypeKind(text);
			if (primitiveTypeKind != Microsoft.OData.Edm.EdmPrimitiveTypeKind.None)
			{
				return new Microsoft.OData.Edm.Library.EdmCollectionType(Microsoft.OData.Edm.Library.EdmCoreModel.Instance.GetPrimitive(primitiveTypeKind, false));
			}
			Microsoft.OData.Edm.IEdmSchemaType edmSchemaType = model.FindDeclaredType(text);
			if (edmSchemaType == null)
			{
				return null;
			}
			if (edmSchemaType.TypeKind == Microsoft.OData.Edm.EdmTypeKind.Complex)
			{
				return new Microsoft.OData.Edm.Library.EdmCollectionType(new Microsoft.OData.Edm.Library.EdmComplexTypeReference((Microsoft.OData.Edm.IEdmComplexType)edmSchemaType, true));
			}
			return new Microsoft.OData.Edm.Library.EdmCollectionType(new Microsoft.OData.Edm.Library.EdmEntityTypeReference((Microsoft.OData.Edm.IEdmEntityType)edmSchemaType, true));
		}

		// Token: 0x06003F1A RID: 16154 RVA: 0x000CF060 File Offset: 0x000CD260
		private static Microsoft.OData.Edm.EdmPrimitiveTypeKind GetEdmTypeKind(TypeValue type)
		{
			type = TypeServices.StripNullableAndMetadata(type);
			Microsoft.OData.Edm.EdmPrimitiveTypeKind edmPrimitiveTypeKind;
			if (ODataTypeServices.TypeValueToEdmKind.TryGetValue(type, out edmPrimitiveTypeKind))
			{
				return edmPrimitiveTypeKind;
			}
			switch (type.TypeKind)
			{
			case ValueKind.DateTimeZone:
				return Microsoft.OData.Edm.EdmPrimitiveTypeKind.DateTimeOffset;
			case ValueKind.Number:
				if (type.Equals(TypeValue.Byte))
				{
					return Microsoft.OData.Edm.EdmPrimitiveTypeKind.Byte;
				}
				if (type.Equals(TypeValue.Decimal))
				{
					return Microsoft.OData.Edm.EdmPrimitiveTypeKind.Decimal;
				}
				if (type.Equals(TypeValue.Int8))
				{
					return Microsoft.OData.Edm.EdmPrimitiveTypeKind.SByte;
				}
				if (type.Equals(TypeValue.Int16))
				{
					return Microsoft.OData.Edm.EdmPrimitiveTypeKind.Int16;
				}
				if (type.Equals(TypeValue.Int32))
				{
					return Microsoft.OData.Edm.EdmPrimitiveTypeKind.Int32;
				}
				if (type.Equals(TypeValue.Int64))
				{
					return Microsoft.OData.Edm.EdmPrimitiveTypeKind.Int64;
				}
				if (type.Equals(TypeValue.Single))
				{
					return Microsoft.OData.Edm.EdmPrimitiveTypeKind.Single;
				}
				return Microsoft.OData.Edm.EdmPrimitiveTypeKind.Double;
			case ValueKind.Logical:
				return Microsoft.OData.Edm.EdmPrimitiveTypeKind.Boolean;
			case ValueKind.Text:
				if (type.Equals(TypeValue.Guid))
				{
					return Microsoft.OData.Edm.EdmPrimitiveTypeKind.Guid;
				}
				return Microsoft.OData.Edm.EdmPrimitiveTypeKind.String;
			case ValueKind.Binary:
				return Microsoft.OData.Edm.EdmPrimitiveTypeKind.Binary;
			}
			return Microsoft.OData.Edm.EdmPrimitiveTypeKind.None;
		}

		// Token: 0x06003F1B RID: 16155 RVA: 0x000B4D02 File Offset: 0x000B2F02
		private static bool IsNullablePrimitiveType(Microsoft.OData.Edm.EdmPrimitiveTypeKind typeKind, bool forceNullable)
		{
			switch (typeKind)
			{
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.Boolean:
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.Byte:
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.Decimal:
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.Double:
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.Int16:
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.Int32:
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.Int64:
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.SByte:
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.Single:
				return forceNullable;
			}
			return true;
		}

		// Token: 0x06003F1C RID: 16156 RVA: 0x000CF13C File Offset: 0x000CD33C
		private static bool IsNumericKind(this Microsoft.OData.Edm.EdmPrimitiveTypeKind primitiveTypeKind)
		{
			bool flag = false;
			switch (primitiveTypeKind)
			{
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.Byte:
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.Decimal:
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.Double:
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.Int16:
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.Int32:
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.Int64:
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.SByte:
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.Single:
				flag = true;
				break;
			}
			return flag;
		}

		// Token: 0x06003F1D RID: 16157 RVA: 0x000CF180 File Offset: 0x000CD380
		public static Value MarshalFromClr(object instance)
		{
			Microsoft.Spatial.Geometry geometry = instance as Microsoft.Spatial.Geometry;
			if (geometry != null)
			{
				return TextValue.New(SpatialUtilities.ToWellKnownTextV4(geometry));
			}
			Microsoft.Spatial.Geography geography = instance as Microsoft.Spatial.Geography;
			if (geography != null)
			{
				return TextValue.New(SpatialUtilities.ToWellKnownTextV4(geography));
			}
			Microsoft.OData.Edm.Library.Date? date = instance as Microsoft.OData.Edm.Library.Date?;
			if (date != null)
			{
				return DateValue.New(date.Value);
			}
			Microsoft.OData.Edm.Library.TimeOfDay? timeOfDay = instance as Microsoft.OData.Edm.Library.TimeOfDay?;
			if (timeOfDay != null)
			{
				return TimeValue.New(timeOfDay.Value);
			}
			return ValueMarshaller.MarshalFromClr(instance);
		}

		// Token: 0x06003F1E RID: 16158 RVA: 0x000CF210 File Offset: 0x000CD410
		public static object MarshalToClr(Value mValue, Microsoft.OData.Edm.IEdmPrimitiveType primitiveType)
		{
			TypeValue typeValueFromEdm = ODataTypeServices.GetTypeValueFromEdm(primitiveType.PrimitiveKind);
			object obj = ValueMarshaller.MarshalToClr(mValue, typeValueFromEdm);
			switch (primitiveType.PrimitiveKind)
			{
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.Guid:
				obj = new Guid((string)obj);
				break;
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.Geography:
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.GeographyPoint:
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.GeographyLineString:
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.GeographyPolygon:
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.GeographyCollection:
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.GeographyMultiPolygon:
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.GeographyMultiLineString:
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.GeographyMultiPoint:
				if (!ODataLiteral.TryParseGeography((string)obj, out obj))
				{
					throw ValueException.NewDataFormatError<Message0>(Strings.ODataSpatialTypeIncorrectFormat, mValue, null);
				}
				break;
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.Geometry:
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.GeometryPoint:
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.GeometryLineString:
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.GeometryPolygon:
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.GeometryCollection:
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.GeometryMultiPolygon:
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.GeometryMultiLineString:
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.GeometryMultiPoint:
				if (!ODataLiteral.TryParseGeometry((string)obj, out obj))
				{
					throw ValueException.NewDataFormatError<Message0>(Strings.ODataSpatialTypeIncorrectFormat, mValue, null);
				}
				break;
			}
			return obj;
		}

		// Token: 0x0400211F RID: 8479
		private const string EdmNamespace = "Edm";

		// Token: 0x04002120 RID: 8480
		private const string CollectionString = "Collection";

		// Token: 0x04002121 RID: 8481
		private const string EdmCollectionString = "Edm.Collection";

		// Token: 0x04002122 RID: 8482
		private static readonly Dictionary<TypeValue, Microsoft.OData.Edm.EdmPrimitiveTypeKind> TypeValueToEdmKind = new Dictionary<TypeValue, Microsoft.OData.Edm.EdmPrimitiveTypeKind>
		{
			{
				TypeValue.Binary,
				Microsoft.OData.Edm.EdmPrimitiveTypeKind.Binary
			},
			{
				TypeValue.Logical,
				Microsoft.OData.Edm.EdmPrimitiveTypeKind.Boolean
			},
			{
				TypeValue.Byte,
				Microsoft.OData.Edm.EdmPrimitiveTypeKind.Byte
			},
			{
				TypeValue.DateTimeZone,
				Microsoft.OData.Edm.EdmPrimitiveTypeKind.DateTimeOffset
			},
			{
				TypeValue.Decimal,
				Microsoft.OData.Edm.EdmPrimitiveTypeKind.Decimal
			},
			{
				TypeValue.Double,
				Microsoft.OData.Edm.EdmPrimitiveTypeKind.Double
			},
			{
				TypeValue.Duration,
				Microsoft.OData.Edm.EdmPrimitiveTypeKind.Duration
			},
			{
				TypeValue.Guid,
				Microsoft.OData.Edm.EdmPrimitiveTypeKind.Guid
			},
			{
				TypeValue.Int16,
				Microsoft.OData.Edm.EdmPrimitiveTypeKind.Int16
			},
			{
				TypeValue.Int32,
				Microsoft.OData.Edm.EdmPrimitiveTypeKind.Int32
			},
			{
				TypeValue.Int64,
				Microsoft.OData.Edm.EdmPrimitiveTypeKind.Int64
			},
			{
				TypeValue.Int8,
				Microsoft.OData.Edm.EdmPrimitiveTypeKind.SByte
			},
			{
				TypeValue.Single,
				Microsoft.OData.Edm.EdmPrimitiveTypeKind.Single
			},
			{
				TypeValue.Text,
				Microsoft.OData.Edm.EdmPrimitiveTypeKind.String
			}
		};
	}
}
