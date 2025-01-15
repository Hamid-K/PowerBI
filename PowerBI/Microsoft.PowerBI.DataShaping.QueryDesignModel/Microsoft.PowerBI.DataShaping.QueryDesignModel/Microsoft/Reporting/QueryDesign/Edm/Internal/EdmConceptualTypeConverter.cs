using System;
using Microsoft.Data.Metadata.Edm;
using Microsoft.DataShaping;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;
using Microsoft.Reporting.Common.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.ExpressionBuilder.Internal;

namespace Microsoft.Reporting.QueryDesign.Edm.Internal
{
	// Token: 0x020001EB RID: 491
	internal static class EdmConceptualTypeConverter
	{
		// Token: 0x06001763 RID: 5987 RVA: 0x0003FD9C File Offset: 0x0003DF9C
		internal static ConceptualDataCategory ConvertFieldContentTypeToConceptualDataCategory(FieldContentType? type)
		{
			if (type == null)
			{
				return ConceptualDataCategory.None;
			}
			switch (type.Value)
			{
			case FieldContentType.Image:
				return ConceptualDataCategory.Image;
			case FieldContentType.ImageUrl:
				return ConceptualDataCategory.ImageUrl;
			case FieldContentType.WebUrl:
				return ConceptualDataCategory.WebUrl;
			case FieldContentType.Continent:
				return ConceptualDataCategory.Continent;
			case FieldContentType.Country:
				return ConceptualDataCategory.Country;
			case FieldContentType.StateOrProvince:
				return ConceptualDataCategory.StateOrProvince;
			case FieldContentType.County:
				return ConceptualDataCategory.County;
			case FieldContentType.City:
				return ConceptualDataCategory.City;
			case FieldContentType.PostalCode:
				return ConceptualDataCategory.PostalCode;
			case FieldContentType.Place:
				return ConceptualDataCategory.Place;
			case FieldContentType.Address:
				return ConceptualDataCategory.Address;
			case FieldContentType.Longitude:
				return ConceptualDataCategory.Longitude;
			case FieldContentType.Latitude:
				return ConceptualDataCategory.Latitude;
			case FieldContentType.Date:
				return ConceptualDataCategory.Date;
			case FieldContentType.Color:
				return ConceptualDataCategory.Color;
			}
			return ConceptualDataCategory.None;
		}

		// Token: 0x06001764 RID: 5988 RVA: 0x0003FE2C File Offset: 0x0003E02C
		internal static PrimitiveTypeKind? ConvertConceptualDataType(ConceptualPrimitiveType conceptualType)
		{
			switch (conceptualType)
			{
			case ConceptualPrimitiveType.Null:
				return null;
			case ConceptualPrimitiveType.Text:
				return new PrimitiveTypeKind?(PrimitiveTypeKind.String);
			case ConceptualPrimitiveType.Decimal:
				return new PrimitiveTypeKind?(PrimitiveTypeKind.Decimal);
			case ConceptualPrimitiveType.Double:
				return new PrimitiveTypeKind?(PrimitiveTypeKind.Double);
			case ConceptualPrimitiveType.Integer:
				return new PrimitiveTypeKind?(PrimitiveTypeKind.Int64);
			case ConceptualPrimitiveType.Boolean:
				return new PrimitiveTypeKind?(PrimitiveTypeKind.Boolean);
			case ConceptualPrimitiveType.DateTime:
				return new PrimitiveTypeKind?(PrimitiveTypeKind.DateTime);
			case ConceptualPrimitiveType.DateTimeZone:
				return new PrimitiveTypeKind?(PrimitiveTypeKind.DateTimeOffset);
			case ConceptualPrimitiveType.Time:
				return new PrimitiveTypeKind?(PrimitiveTypeKind.Time);
			case ConceptualPrimitiveType.Binary:
				return new PrimitiveTypeKind?(PrimitiveTypeKind.Binary);
			}
			throw new InvalidOperationException(Microsoft.Reporting.StringUtil.FormatInvariant("Unknown ConceptualPrimitiveType: {0}", new object[] { conceptualType }));
		}

		// Token: 0x06001765 RID: 5989 RVA: 0x0003FEDC File Offset: 0x0003E0DC
		internal static TypeUsage ConvertToTypeUsage(this ConceptualPrimitiveType primitiveType)
		{
			PrimitiveTypeKind? primitiveTypeKind = EdmConceptualTypeConverter.ConvertConceptualDataType(primitiveType);
			Microsoft.DataShaping.Contract.RetailAssert(primitiveTypeKind != null, "edmPrimitiveKind");
			return TypeUsage.CreateDefaultTypeUsage(PrimitiveType.GetPrimitiveType(primitiveTypeKind.Value).InternalEdmType);
		}

		// Token: 0x06001766 RID: 5990 RVA: 0x0003FF18 File Offset: 0x0003E118
		internal static ConceptualResultType ConvertTypeForFunction(TypeUsage typeUsage)
		{
			if (typeUsage.EdmType.BuiltInTypeKind == BuiltInTypeKind.PrimitiveType)
			{
				return new PrimitiveTypeKind?(((PrimitiveType)typeUsage.EdmType).PrimitiveTypeKind).ToConceptualPrimitiveType();
			}
			if (typeUsage.EdmType.BuiltInTypeKind == BuiltInTypeKind.CollectionType)
			{
				return EdmConceptualTypeConverter.ConvertConceptualCollectionType((CollectionType)typeUsage.EdmType);
			}
			if (EdmConceptualTypeConverter.IsVariantType(typeUsage))
			{
				return ConceptualPrimitiveResultType.Variant;
			}
			throw new QueryFunctionInvocationException("Unexpected TypeUsage " + ((typeUsage != null) ? typeUsage.ToString() : null));
		}

		// Token: 0x06001767 RID: 5991 RVA: 0x0003FF98 File Offset: 0x0003E198
		private static ConceptualCollectionType ConvertConceptualCollectionType(CollectionType edmCollection)
		{
			TypeUsage typeUsage = edmCollection.TypeUsage;
			if (typeUsage.EdmType.BuiltInTypeKind == BuiltInTypeKind.PrimitiveType)
			{
				return ConceptualCollectionType.FromPrimitive(new PrimitiveTypeKind?(((PrimitiveType)typeUsage.EdmType).PrimitiveTypeKind).ToConceptualPrimitiveType());
			}
			if (EdmConceptualTypeConverter.IsVariantType(typeUsage))
			{
				return ConceptualCollectionType.Variant;
			}
			string text = "Unexpected Collection TypeUsage ";
			TypeUsage typeUsage2 = typeUsage;
			throw new QueryFunctionInvocationException(text + ((typeUsage2 != null) ? typeUsage2.ToString() : null));
		}

		// Token: 0x06001768 RID: 5992 RVA: 0x00040005 File Offset: 0x0003E205
		private static bool IsVariantType(TypeUsage edmType)
		{
			return edmType.EdmType.BuiltInTypeKind == BuiltInTypeKind.ComplexType && edmType.EdmType.FullName == "Core.Variant";
		}

		// Token: 0x06001769 RID: 5993 RVA: 0x0004002C File Offset: 0x0003E22C
		internal static ConceptualPrimitiveResultType ConvertTypeForPrimitive(TypeUsage edmResultType)
		{
			if (edmResultType.EdmType.BuiltInTypeKind == BuiltInTypeKind.PrimitiveType)
			{
				return new PrimitiveTypeKind?(((PrimitiveType)edmResultType.EdmType).PrimitiveTypeKind).ToConceptualPrimitiveType();
			}
			throw new QueryFunctionInvocationException("Unexpected Primitive TypeUsage " + ((edmResultType != null) ? edmResultType.ToString() : null));
		}

		// Token: 0x0600176A RID: 5994 RVA: 0x00040080 File Offset: 0x0003E280
		private static ConceptualPrimitiveResultType ToConceptualPrimitiveType(this PrimitiveTypeKind? typeNullable)
		{
			if (typeNullable != null)
			{
				switch (typeNullable.Value)
				{
				case PrimitiveTypeKind.Binary:
					return ConceptualPrimitiveResultType.Binary;
				case PrimitiveTypeKind.Boolean:
					return ConceptualPrimitiveResultType.Boolean;
				case PrimitiveTypeKind.Byte:
				case PrimitiveTypeKind.Int16:
				case PrimitiveTypeKind.Int32:
				case PrimitiveTypeKind.Int64:
					return ConceptualPrimitiveResultType.Integer;
				case PrimitiveTypeKind.DateTime:
					return ConceptualPrimitiveResultType.DateTime;
				case PrimitiveTypeKind.Decimal:
					return ConceptualPrimitiveResultType.Decimal;
				case PrimitiveTypeKind.Double:
				case PrimitiveTypeKind.Single:
					return ConceptualPrimitiveResultType.Double;
				case PrimitiveTypeKind.String:
					return ConceptualPrimitiveResultType.Text;
				case PrimitiveTypeKind.Time:
					return ConceptualPrimitiveResultType.Time;
				case PrimitiveTypeKind.DateTimeOffset:
					return ConceptualPrimitiveResultType.DateTimeZone;
				}
			}
			return ConceptualPrimitiveResultType.Null;
		}
	}
}
