using System;
using System.Text;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200016C RID: 364
	public static class ToTraceStringExtensionMethods
	{
		// Token: 0x060006F3 RID: 1779 RVA: 0x00011135 File Offset: 0x0000F335
		public static string ToTraceString(this IEdmSchemaType schemaType)
		{
			return schemaType.ToTraceString();
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x0001113D File Offset: 0x0000F33D
		public static string ToTraceString(this IEdmSchemaElement schemaElement)
		{
			return schemaElement.FullName();
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x00011148 File Offset: 0x0000F348
		public static string ToTraceString(this IEdmType type)
		{
			EdmUtil.CheckArgumentNull<IEdmType>(type, "type");
			switch (type.TypeKind)
			{
			case EdmTypeKind.Collection:
				return ((IEdmCollectionType)type).ToTraceString();
			case EdmTypeKind.EntityReference:
				return ((IEdmEntityReferenceType)type).ToTraceString();
			default:
			{
				IEdmSchemaType edmSchemaType = type as IEdmSchemaType;
				if (edmSchemaType == null)
				{
					return "UnknownType";
				}
				return edmSchemaType.ToTraceString();
			}
			}
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x000111A8 File Offset: 0x0000F3A8
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

		// Token: 0x060006F7 RID: 1783 RVA: 0x00011228 File Offset: 0x0000F428
		public static string ToTraceString(this IEdmProperty property)
		{
			EdmUtil.CheckArgumentNull<IEdmProperty>(property, "property");
			return ((property.Name != null) ? property.Name : "") + ":" + ((property.Type != null) ? property.Type.ToTraceString() : "");
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x0001127C File Offset: 0x0000F47C
		private static string ToTraceString(this IEdmEntityReferenceType type)
		{
			return string.Concat(new object[]
			{
				EdmTypeKind.EntityReference.ToString(),
				'(',
				(type.EntityType != null) ? type.EntityType.ToTraceString() : "",
				')'
			});
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x000112D8 File Offset: 0x0000F4D8
		private static string ToTraceString(this IEdmCollectionType type)
		{
			return string.Concat(new object[]
			{
				EdmTypeKind.Collection.ToString(),
				'(',
				(type.ElementType != null) ? type.ElementType.ToTraceString() : "",
				')'
			});
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x00011334 File Offset: 0x0000F534
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

		// Token: 0x060006FB RID: 1787 RVA: 0x00011410 File Offset: 0x0000F610
		private static void AppendBinaryFacets(this StringBuilder sb, IEdmBinaryTypeReference type)
		{
			if (type.IsUnbounded || type.MaxLength != null)
			{
				sb.AppendKeyValue("MaxLength", type.IsUnbounded ? "max" : type.MaxLength.ToString());
			}
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x00011464 File Offset: 0x0000F664
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

		// Token: 0x060006FD RID: 1789 RVA: 0x000114E8 File Offset: 0x0000F6E8
		private static void AppendTemporalFacets(this StringBuilder sb, IEdmTemporalTypeReference type)
		{
			if (type.Precision != null)
			{
				sb.AppendKeyValue("Precision", type.Precision.ToString());
			}
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x00011524 File Offset: 0x0000F724
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

		// Token: 0x060006FF RID: 1791 RVA: 0x00011590 File Offset: 0x0000F790
		private static void AppendSpatialFacets(this StringBuilder sb, IEdmSpatialTypeReference type)
		{
			sb.AppendKeyValue("SRID", (type.SpatialReferenceIdentifier != null) ? type.SpatialReferenceIdentifier.ToString() : "Variable");
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x000115D3 File Offset: 0x0000F7D3
		private static void AppendKeyValue(this StringBuilder sb, string key, string value)
		{
			sb.Append(' ');
			sb.Append(key);
			sb.Append('=');
			sb.Append(value);
		}
	}
}
