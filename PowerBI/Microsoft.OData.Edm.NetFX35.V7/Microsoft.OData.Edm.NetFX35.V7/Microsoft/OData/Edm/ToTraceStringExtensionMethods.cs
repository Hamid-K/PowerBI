using System;
using System.Text;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200000F RID: 15
	public static class ToTraceStringExtensionMethods
	{
		// Token: 0x06000042 RID: 66 RVA: 0x00003025 File Offset: 0x00001225
		public static string ToTraceString(this IEdmSchemaType schemaType)
		{
			return schemaType.ToTraceString();
		}

		// Token: 0x06000043 RID: 67 RVA: 0x0000302D File Offset: 0x0000122D
		public static string ToTraceString(this IEdmSchemaElement schemaElement)
		{
			return schemaElement.FullName();
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00003038 File Offset: 0x00001238
		public static string ToTraceString(this IEdmType type)
		{
			EdmUtil.CheckArgumentNull<IEdmType>(type, "type");
			EdmTypeKind typeKind = type.TypeKind;
			if (typeKind == EdmTypeKind.Collection)
			{
				return ((IEdmCollectionType)type).ToTraceString();
			}
			if (typeKind == EdmTypeKind.EntityReference)
			{
				return ((IEdmEntityReferenceType)type).ToTraceString();
			}
			IEdmSchemaType edmSchemaType = type as IEdmSchemaType;
			if (edmSchemaType == null)
			{
				return "UnknownType";
			}
			return edmSchemaType.ToTraceString();
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00003090 File Offset: 0x00001290
		public static string ToTraceString(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('[');
			if (type.Definition != null)
			{
				stringBuilder.Append(type.Definition.ToTraceString());
				stringBuilder.AppendKeyValue("Nullable", type.IsNullable.ToString());
				if (type.IsPrimitive())
				{
					stringBuilder.AppendFacets(type.AsPrimitive());
				}
			}
			stringBuilder.Append(']');
			return stringBuilder.ToString();
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00003110 File Offset: 0x00001310
		public static string ToTraceString(this IEdmProperty property)
		{
			EdmUtil.CheckArgumentNull<IEdmProperty>(property, "property");
			return ((property.Name != null) ? property.Name : "") + ":" + ((property.Type != null) ? property.Type.ToTraceString() : "");
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00003164 File Offset: 0x00001364
		private static string ToTraceString(this IEdmEntityReferenceType type)
		{
			return EdmTypeKind.EntityReference.ToString() + "(" + ((type.EntityType != null) ? type.EntityType.ToTraceString() : "") + ")";
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000031AC File Offset: 0x000013AC
		private static string ToTraceString(this IEdmCollectionType type)
		{
			return EdmTypeKind.Collection.ToString() + "(" + ((type.ElementType != null) ? type.ElementType.ToTraceString() : "") + ")";
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000031F4 File Offset: 0x000013F4
		private static void AppendFacets(this StringBuilder sb, IEdmPrimitiveTypeReference type)
		{
			switch (type.PrimitiveKind())
			{
			case EdmPrimitiveTypeKind.Binary:
				sb.AppendBinaryFacets(type.AsBinary());
				return;
			case EdmPrimitiveTypeKind.Boolean:
			case EdmPrimitiveTypeKind.Byte:
			case EdmPrimitiveTypeKind.Double:
			case EdmPrimitiveTypeKind.Guid:
			case EdmPrimitiveTypeKind.Int16:
			case EdmPrimitiveTypeKind.Int32:
			case EdmPrimitiveTypeKind.Int64:
			case EdmPrimitiveTypeKind.SByte:
			case EdmPrimitiveTypeKind.Single:
			case EdmPrimitiveTypeKind.Stream:
				break;
			case EdmPrimitiveTypeKind.DateTimeOffset:
			case EdmPrimitiveTypeKind.Duration:
				sb.AppendTemporalFacets(type.AsTemporal());
				return;
			case EdmPrimitiveTypeKind.Decimal:
				sb.AppendDecimalFacets(type.AsDecimal());
				return;
			case EdmPrimitiveTypeKind.String:
				sb.AppendStringFacets(type.AsString());
				return;
			case EdmPrimitiveTypeKind.Geography:
			case EdmPrimitiveTypeKind.GeographyPoint:
			case EdmPrimitiveTypeKind.GeographyLineString:
			case EdmPrimitiveTypeKind.GeographyPolygon:
			case EdmPrimitiveTypeKind.GeographyCollection:
			case EdmPrimitiveTypeKind.GeographyMultiPolygon:
			case EdmPrimitiveTypeKind.GeographyMultiLineString:
			case EdmPrimitiveTypeKind.GeographyMultiPoint:
			case EdmPrimitiveTypeKind.Geometry:
			case EdmPrimitiveTypeKind.GeometryPoint:
			case EdmPrimitiveTypeKind.GeometryLineString:
			case EdmPrimitiveTypeKind.GeometryPolygon:
			case EdmPrimitiveTypeKind.GeometryCollection:
			case EdmPrimitiveTypeKind.GeometryMultiPolygon:
			case EdmPrimitiveTypeKind.GeometryMultiLineString:
			case EdmPrimitiveTypeKind.GeometryMultiPoint:
				sb.AppendSpatialFacets(type.AsSpatial());
				break;
			default:
				return;
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000032D0 File Offset: 0x000014D0
		private static void AppendBinaryFacets(this StringBuilder sb, IEdmBinaryTypeReference type)
		{
			if (type.IsUnbounded || type.MaxLength != null)
			{
				sb.AppendKeyValue("MaxLength", type.IsUnbounded ? "max" : type.MaxLength.ToString());
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00003324 File Offset: 0x00001524
		private static void AppendStringFacets(this StringBuilder sb, IEdmStringTypeReference type)
		{
			if (type.IsUnbounded || type.MaxLength != null)
			{
				sb.AppendKeyValue("MaxLength", type.IsUnbounded ? "max" : type.MaxLength.ToString());
			}
			if (type.IsUnicode != null)
			{
				sb.AppendKeyValue("Unicode", type.IsUnicode.ToString());
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000033A8 File Offset: 0x000015A8
		private static void AppendTemporalFacets(this StringBuilder sb, IEdmTemporalTypeReference type)
		{
			if (type.Precision != null)
			{
				sb.AppendKeyValue("Precision", type.Precision.ToString());
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000033E4 File Offset: 0x000015E4
		private static void AppendDecimalFacets(this StringBuilder sb, IEdmDecimalTypeReference type)
		{
			if (type.Precision != null)
			{
				sb.AppendKeyValue("Precision", type.Precision.ToString());
			}
			if (type.Scale != null)
			{
				sb.AppendKeyValue("Scale", type.Scale.ToString());
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003450 File Offset: 0x00001650
		private static void AppendSpatialFacets(this StringBuilder sb, IEdmSpatialTypeReference type)
		{
			sb.AppendKeyValue("SRID", (type.SpatialReferenceIdentifier != null) ? type.SpatialReferenceIdentifier.ToString() : "Variable");
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003493 File Offset: 0x00001693
		private static void AppendKeyValue(this StringBuilder sb, string key, string value)
		{
			sb.Append(' ');
			sb.Append(key);
			sb.Append('=');
			sb.Append(value);
		}
	}
}
