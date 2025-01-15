using System;
using System.Text;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000072 RID: 114
	public static class ToTraceStringExtensionMethods
	{
		// Token: 0x0600024B RID: 587 RVA: 0x00005AC9 File Offset: 0x00003CC9
		public static string ToTraceString(this IEdmSchemaType schemaType)
		{
			return schemaType.ToTraceString();
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00005AD1 File Offset: 0x00003CD1
		public static string ToTraceString(this IEdmSchemaElement schemaElement)
		{
			return schemaElement.FullName();
		}

		// Token: 0x0600024D RID: 589 RVA: 0x00005ADC File Offset: 0x00003CDC
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

		// Token: 0x0600024E RID: 590 RVA: 0x00005B34 File Offset: 0x00003D34
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

		// Token: 0x0600024F RID: 591 RVA: 0x00005BB4 File Offset: 0x00003DB4
		public static string ToTraceString(this IEdmProperty property)
		{
			EdmUtil.CheckArgumentNull<IEdmProperty>(property, "property");
			return ((property.Name != null) ? property.Name : "") + ":" + ((property.Type != null) ? property.Type.ToTraceString() : "");
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00005C08 File Offset: 0x00003E08
		private static string ToTraceString(this IEdmEntityReferenceType type)
		{
			return EdmTypeKind.EntityReference.ToString() + "(" + ((type.EntityType != null) ? type.EntityType.ToTraceString() : "") + ")";
		}

		// Token: 0x06000251 RID: 593 RVA: 0x00005C50 File Offset: 0x00003E50
		private static string ToTraceString(this IEdmCollectionType type)
		{
			return EdmTypeKind.Collection.ToString() + "(" + ((type.ElementType != null) ? type.ElementType.ToTraceString() : "") + ")";
		}

		// Token: 0x06000252 RID: 594 RVA: 0x00005C98 File Offset: 0x00003E98
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

		// Token: 0x06000253 RID: 595 RVA: 0x00005D74 File Offset: 0x00003F74
		private static void AppendBinaryFacets(this StringBuilder sb, IEdmBinaryTypeReference type)
		{
			if (type.IsUnbounded || type.MaxLength != null)
			{
				sb.AppendKeyValue("MaxLength", type.IsUnbounded ? "max" : type.MaxLength.ToString());
			}
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00005DC8 File Offset: 0x00003FC8
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

		// Token: 0x06000255 RID: 597 RVA: 0x00005E4C File Offset: 0x0000404C
		private static void AppendTemporalFacets(this StringBuilder sb, IEdmTemporalTypeReference type)
		{
			if (type.Precision != null)
			{
				sb.AppendKeyValue("Precision", type.Precision.ToString());
			}
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00005E88 File Offset: 0x00004088
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

		// Token: 0x06000257 RID: 599 RVA: 0x00005EF4 File Offset: 0x000040F4
		private static void AppendSpatialFacets(this StringBuilder sb, IEdmSpatialTypeReference type)
		{
			sb.AppendKeyValue("SRID", (type.SpatialReferenceIdentifier != null) ? type.SpatialReferenceIdentifier.ToString() : "Variable");
		}

		// Token: 0x06000258 RID: 600 RVA: 0x00005F37 File Offset: 0x00004137
		private static void AppendKeyValue(this StringBuilder sb, string key, string value)
		{
			sb.Append(' ');
			sb.Append(key);
			sb.Append('=');
			sb.Append(value);
		}
	}
}
