using System;
using System.Collections.Generic;
using System.Globalization;
using System.Spatial;
using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Library;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.OData.V3
{
	// Token: 0x020008DE RID: 2270
	internal static class ODataTypeServices
	{
		// Token: 0x060040C4 RID: 16580 RVA: 0x000D82E3 File Offset: 0x000D64E3
		public static string GetEdmCollectionFullTypeName(IEdmCollectionType edmCollectionType)
		{
			return string.Format(CultureInfo.InvariantCulture, "Collection({0})", edmCollectionType.ElementType.FullName());
		}

		// Token: 0x060040C5 RID: 16581 RVA: 0x000D8300 File Offset: 0x000D6500
		public static IEdmCollectionType FindEdmCollectionType(IEdmModel model, string qualifiedEdmName)
		{
			if (!qualifiedEdmName.StartsWith("Collection", StringComparison.Ordinal) && !qualifiedEdmName.StartsWith("Edm.Collection", StringComparison.Ordinal))
			{
				return null;
			}
			int num = qualifiedEdmName.IndexOf("(", StringComparison.Ordinal);
			string text = qualifiedEdmName.TrimEnd(new char[] { ')' }).Remove(0, num + 1);
			EdmPrimitiveTypeKind primitiveTypeKind = EdmCoreModel.Instance.GetPrimitiveTypeKind(text);
			if (primitiveTypeKind != EdmPrimitiveTypeKind.None)
			{
				return new EdmCollectionType(EdmCoreModel.Instance.GetPrimitive(primitiveTypeKind, false));
			}
			IEdmSchemaType edmSchemaType = model.FindDeclaredType(text);
			if (edmSchemaType == null)
			{
				return null;
			}
			if (edmSchemaType.TypeKind == EdmTypeKind.Complex)
			{
				return new EdmCollectionType(new EdmComplexTypeReference((IEdmComplexType)edmSchemaType, true));
			}
			return new EdmCollectionType(new EdmEntityTypeReference((IEdmEntityType)edmSchemaType, true));
		}

		// Token: 0x060040C6 RID: 16582 RVA: 0x000D83B0 File Offset: 0x000D65B0
		public static EdmPrimitiveTypeKind GetEdmTypeKind(TypeValue type)
		{
			type = TypeServices.StripNullableAndMetadata(type);
			EdmPrimitiveTypeKind edmPrimitiveTypeKind;
			if (ODataTypeServices.TypeValueToEdmKind.TryGetValue(type, out edmPrimitiveTypeKind))
			{
				return edmPrimitiveTypeKind;
			}
			switch (type.TypeKind)
			{
			case ValueKind.Time:
				return EdmPrimitiveTypeKind.Time;
			case ValueKind.DateTime:
				return EdmPrimitiveTypeKind.DateTime;
			case ValueKind.DateTimeZone:
				return EdmPrimitiveTypeKind.DateTimeOffset;
			case ValueKind.Number:
				if (type.Equals(TypeValue.Byte))
				{
					return EdmPrimitiveTypeKind.Byte;
				}
				if (type.Equals(TypeValue.Decimal))
				{
					return EdmPrimitiveTypeKind.Decimal;
				}
				if (type.Equals(TypeValue.Int8))
				{
					return EdmPrimitiveTypeKind.SByte;
				}
				if (type.Equals(TypeValue.Int16))
				{
					return EdmPrimitiveTypeKind.Int16;
				}
				if (type.Equals(TypeValue.Int32))
				{
					return EdmPrimitiveTypeKind.Int32;
				}
				if (type.Equals(TypeValue.Int64))
				{
					return EdmPrimitiveTypeKind.Int64;
				}
				if (type.Equals(TypeValue.Single))
				{
					return EdmPrimitiveTypeKind.Single;
				}
				return EdmPrimitiveTypeKind.Double;
			case ValueKind.Logical:
				return EdmPrimitiveTypeKind.Boolean;
			case ValueKind.Text:
				if (type.Equals(TypeValue.Guid))
				{
					return EdmPrimitiveTypeKind.Guid;
				}
				return EdmPrimitiveTypeKind.String;
			case ValueKind.Binary:
				return EdmPrimitiveTypeKind.Binary;
			}
			return EdmPrimitiveTypeKind.None;
		}

		// Token: 0x060040C7 RID: 16583 RVA: 0x000D849C File Offset: 0x000D669C
		public static IEdmPrimitiveTypeReference GetEdmPrimitiveTypeReference(TypeValue type, bool isKey)
		{
			EdmPrimitiveTypeKind edmTypeKind = ODataTypeServices.GetEdmTypeKind(type);
			if (edmTypeKind != EdmPrimitiveTypeKind.None)
			{
				return EdmCoreModel.Instance.GetPrimitive(edmTypeKind, !isKey && ODataTypeServices.IsNullablePrimitiveType(edmTypeKind, type.IsNullable));
			}
			return null;
		}

		// Token: 0x060040C8 RID: 16584 RVA: 0x000D84D4 File Offset: 0x000D66D4
		public static TypeValue GetTypeValueFromEdm(EdmPrimitiveTypeKind typeKind)
		{
			switch (typeKind)
			{
			case EdmPrimitiveTypeKind.None:
				return TypeValue.Any;
			case EdmPrimitiveTypeKind.Binary:
				return TypeValue.Binary;
			case EdmPrimitiveTypeKind.Boolean:
				return TypeValue.Logical;
			case EdmPrimitiveTypeKind.Byte:
				return TypeValue.Byte;
			case EdmPrimitiveTypeKind.DateTime:
				return TypeValue.DateTime;
			case EdmPrimitiveTypeKind.DateTimeOffset:
				return TypeValue.DateTimeZone;
			case EdmPrimitiveTypeKind.Decimal:
				return TypeValue.Decimal;
			case EdmPrimitiveTypeKind.Double:
				return TypeValue.Double;
			case EdmPrimitiveTypeKind.Guid:
				return TypeValue.Guid;
			case EdmPrimitiveTypeKind.Int16:
				return TypeValue.Int16;
			case EdmPrimitiveTypeKind.Int32:
				return TypeValue.Int32;
			case EdmPrimitiveTypeKind.Int64:
				return TypeValue.Int64;
			case EdmPrimitiveTypeKind.SByte:
				return TypeValue.Int8;
			case EdmPrimitiveTypeKind.Single:
				return TypeValue.Single;
			case EdmPrimitiveTypeKind.String:
				return TypeValue.Text;
			case EdmPrimitiveTypeKind.Stream:
				return TypeValue.Binary;
			case EdmPrimitiveTypeKind.Time:
				return TypeValue.Time;
			case EdmPrimitiveTypeKind.Geography:
			case EdmPrimitiveTypeKind.GeographyPoint:
			case EdmPrimitiveTypeKind.GeographyLineString:
			case EdmPrimitiveTypeKind.GeographyPolygon:
			case EdmPrimitiveTypeKind.GeographyCollection:
			case EdmPrimitiveTypeKind.GeographyMultiPolygon:
			case EdmPrimitiveTypeKind.GeographyMultiLineString:
			case EdmPrimitiveTypeKind.GeographyMultiPoint:
				return SerializedTextTypeValue.SerializedGeographyType;
			case EdmPrimitiveTypeKind.Geometry:
			case EdmPrimitiveTypeKind.GeometryPoint:
			case EdmPrimitiveTypeKind.GeometryLineString:
			case EdmPrimitiveTypeKind.GeometryPolygon:
			case EdmPrimitiveTypeKind.GeometryCollection:
			case EdmPrimitiveTypeKind.GeometryMultiPolygon:
			case EdmPrimitiveTypeKind.GeometryMultiLineString:
			case EdmPrimitiveTypeKind.GeometryMultiPoint:
				return SerializedTextTypeValue.SerializedGeometryType;
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x060040C9 RID: 16585 RVA: 0x000D85E4 File Offset: 0x000D67E4
		public static bool IsNullablePrimitiveType(EdmPrimitiveTypeKind typeKind, bool forceNullable)
		{
			switch (typeKind)
			{
			case EdmPrimitiveTypeKind.Boolean:
			case EdmPrimitiveTypeKind.Byte:
			case EdmPrimitiveTypeKind.Decimal:
			case EdmPrimitiveTypeKind.Double:
			case EdmPrimitiveTypeKind.Int16:
			case EdmPrimitiveTypeKind.Int32:
			case EdmPrimitiveTypeKind.Int64:
			case EdmPrimitiveTypeKind.SByte:
			case EdmPrimitiveTypeKind.Single:
				return forceNullable;
			}
			return true;
		}

		// Token: 0x060040CA RID: 16586 RVA: 0x000D8624 File Offset: 0x000D6824
		public static IEdmType StripCollectionType(IEdmType type)
		{
			IEdmCollectionType edmCollectionType = type as IEdmCollectionType;
			if (edmCollectionType != null)
			{
				return edmCollectionType.ElementType.Definition;
			}
			return type;
		}

		// Token: 0x060040CB RID: 16587 RVA: 0x000D8648 File Offset: 0x000D6848
		public static Value MarshalFromClr(object instance)
		{
			Geometry geometry = instance as Geometry;
			if (geometry != null)
			{
				return TextValue.New(SpatialUtilities.ToWellKnownTextV3(geometry));
			}
			Geography geography = instance as Geography;
			if (geography != null)
			{
				return TextValue.New(SpatialUtilities.ToWellKnownTextV3(geography));
			}
			return ValueMarshaller.MarshalFromClr(instance);
		}

		// Token: 0x0400220C RID: 8716
		public const string EdmNamespace = "Edm";

		// Token: 0x0400220D RID: 8717
		public const string CollectionString = "Collection";

		// Token: 0x0400220E RID: 8718
		public const string EdmCollectionString = "Edm.Collection";

		// Token: 0x0400220F RID: 8719
		public const string EdmCollectionFormatString = "Collection({0})";

		// Token: 0x04002210 RID: 8720
		public static readonly Dictionary<TypeValue, EdmPrimitiveTypeKind> TypeValueToEdmKind = new Dictionary<TypeValue, EdmPrimitiveTypeKind>
		{
			{
				TypeValue.Binary,
				EdmPrimitiveTypeKind.Binary
			},
			{
				TypeValue.Logical,
				EdmPrimitiveTypeKind.Boolean
			},
			{
				TypeValue.Byte,
				EdmPrimitiveTypeKind.Byte
			},
			{
				TypeValue.Date,
				EdmPrimitiveTypeKind.DateTime
			},
			{
				TypeValue.DateTimeZone,
				EdmPrimitiveTypeKind.DateTimeOffset
			},
			{
				TypeValue.DateTime,
				EdmPrimitiveTypeKind.DateTime
			},
			{
				TypeValue.Decimal,
				EdmPrimitiveTypeKind.Decimal
			},
			{
				TypeValue.Double,
				EdmPrimitiveTypeKind.Double
			},
			{
				TypeValue.Duration,
				EdmPrimitiveTypeKind.Time
			},
			{
				TypeValue.Guid,
				EdmPrimitiveTypeKind.Guid
			},
			{
				TypeValue.Int16,
				EdmPrimitiveTypeKind.Int16
			},
			{
				TypeValue.Int32,
				EdmPrimitiveTypeKind.Int32
			},
			{
				TypeValue.Int64,
				EdmPrimitiveTypeKind.Int64
			},
			{
				TypeValue.Int8,
				EdmPrimitiveTypeKind.SByte
			},
			{
				TypeValue.Single,
				EdmPrimitiveTypeKind.Single
			},
			{
				TypeValue.Text,
				EdmPrimitiveTypeKind.String
			},
			{
				TypeValue.Time,
				EdmPrimitiveTypeKind.Time
			}
		};
	}
}
