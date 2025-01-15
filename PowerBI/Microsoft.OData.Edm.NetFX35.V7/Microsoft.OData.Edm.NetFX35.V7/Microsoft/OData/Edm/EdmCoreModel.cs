using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000050 RID: 80
	public class EdmCoreModel : EdmElement, IEdmModel, IEdmElement, IEdmValidCoreModelElement
	{
		// Token: 0x060002F3 RID: 755 RVA: 0x00009EC0 File Offset: 0x000080C0
		private EdmCoreModel()
		{
			EdmCoreModel.EdmValidCoreModelPrimitiveType edmValidCoreModelPrimitiveType = new EdmCoreModel.EdmValidCoreModelPrimitiveType("Edm", "Double", EdmPrimitiveTypeKind.Double);
			EdmCoreModel.EdmValidCoreModelPrimitiveType edmValidCoreModelPrimitiveType2 = new EdmCoreModel.EdmValidCoreModelPrimitiveType("Edm", "Single", EdmPrimitiveTypeKind.Single);
			EdmCoreModel.EdmValidCoreModelPrimitiveType edmValidCoreModelPrimitiveType3 = new EdmCoreModel.EdmValidCoreModelPrimitiveType("Edm", "Int64", EdmPrimitiveTypeKind.Int64);
			EdmCoreModel.EdmValidCoreModelPrimitiveType edmValidCoreModelPrimitiveType4 = new EdmCoreModel.EdmValidCoreModelPrimitiveType("Edm", "Int32", EdmPrimitiveTypeKind.Int32);
			EdmCoreModel.EdmValidCoreModelPrimitiveType edmValidCoreModelPrimitiveType5 = new EdmCoreModel.EdmValidCoreModelPrimitiveType("Edm", "Int16", EdmPrimitiveTypeKind.Int16);
			EdmCoreModel.EdmValidCoreModelPrimitiveType edmValidCoreModelPrimitiveType6 = new EdmCoreModel.EdmValidCoreModelPrimitiveType("Edm", "SByte", EdmPrimitiveTypeKind.SByte);
			EdmCoreModel.EdmValidCoreModelPrimitiveType edmValidCoreModelPrimitiveType7 = new EdmCoreModel.EdmValidCoreModelPrimitiveType("Edm", "Byte", EdmPrimitiveTypeKind.Byte);
			EdmCoreModel.EdmValidCoreModelPrimitiveType edmValidCoreModelPrimitiveType8 = new EdmCoreModel.EdmValidCoreModelPrimitiveType("Edm", "Boolean", EdmPrimitiveTypeKind.Boolean);
			EdmCoreModel.EdmValidCoreModelPrimitiveType edmValidCoreModelPrimitiveType9 = new EdmCoreModel.EdmValidCoreModelPrimitiveType("Edm", "Guid", EdmPrimitiveTypeKind.Guid);
			EdmCoreModel.EdmValidCoreModelPrimitiveType edmValidCoreModelPrimitiveType10 = new EdmCoreModel.EdmValidCoreModelPrimitiveType("Edm", "Duration", EdmPrimitiveTypeKind.Duration);
			EdmCoreModel.EdmValidCoreModelPrimitiveType edmValidCoreModelPrimitiveType11 = new EdmCoreModel.EdmValidCoreModelPrimitiveType("Edm", "TimeOfDay", EdmPrimitiveTypeKind.TimeOfDay);
			EdmCoreModel.EdmValidCoreModelPrimitiveType edmValidCoreModelPrimitiveType12 = new EdmCoreModel.EdmValidCoreModelPrimitiveType("Edm", "DateTimeOffset", EdmPrimitiveTypeKind.DateTimeOffset);
			EdmCoreModel.EdmValidCoreModelPrimitiveType edmValidCoreModelPrimitiveType13 = new EdmCoreModel.EdmValidCoreModelPrimitiveType("Edm", "Date", EdmPrimitiveTypeKind.Date);
			EdmCoreModel.EdmValidCoreModelPrimitiveType edmValidCoreModelPrimitiveType14 = new EdmCoreModel.EdmValidCoreModelPrimitiveType("Edm", "Decimal", EdmPrimitiveTypeKind.Decimal);
			EdmCoreModel.EdmValidCoreModelPrimitiveType edmValidCoreModelPrimitiveType15 = new EdmCoreModel.EdmValidCoreModelPrimitiveType("Edm", "Binary", EdmPrimitiveTypeKind.Binary);
			EdmCoreModel.EdmValidCoreModelPrimitiveType edmValidCoreModelPrimitiveType16 = new EdmCoreModel.EdmValidCoreModelPrimitiveType("Edm", "String", EdmPrimitiveTypeKind.String);
			EdmCoreModel.EdmValidCoreModelPrimitiveType edmValidCoreModelPrimitiveType17 = new EdmCoreModel.EdmValidCoreModelPrimitiveType("Edm", "Stream", EdmPrimitiveTypeKind.Stream);
			EdmCoreModel.EdmValidCoreModelPrimitiveType edmValidCoreModelPrimitiveType18 = new EdmCoreModel.EdmValidCoreModelPrimitiveType("Edm", "Geography", EdmPrimitiveTypeKind.Geography);
			EdmCoreModel.EdmValidCoreModelPrimitiveType edmValidCoreModelPrimitiveType19 = new EdmCoreModel.EdmValidCoreModelPrimitiveType("Edm", "GeographyPoint", EdmPrimitiveTypeKind.GeographyPoint);
			EdmCoreModel.EdmValidCoreModelPrimitiveType edmValidCoreModelPrimitiveType20 = new EdmCoreModel.EdmValidCoreModelPrimitiveType("Edm", "GeographyLineString", EdmPrimitiveTypeKind.GeographyLineString);
			EdmCoreModel.EdmValidCoreModelPrimitiveType edmValidCoreModelPrimitiveType21 = new EdmCoreModel.EdmValidCoreModelPrimitiveType("Edm", "GeographyPolygon", EdmPrimitiveTypeKind.GeographyPolygon);
			EdmCoreModel.EdmValidCoreModelPrimitiveType edmValidCoreModelPrimitiveType22 = new EdmCoreModel.EdmValidCoreModelPrimitiveType("Edm", "GeographyCollection", EdmPrimitiveTypeKind.GeographyCollection);
			EdmCoreModel.EdmValidCoreModelPrimitiveType edmValidCoreModelPrimitiveType23 = new EdmCoreModel.EdmValidCoreModelPrimitiveType("Edm", "GeographyMultiPolygon", EdmPrimitiveTypeKind.GeographyMultiPolygon);
			EdmCoreModel.EdmValidCoreModelPrimitiveType edmValidCoreModelPrimitiveType24 = new EdmCoreModel.EdmValidCoreModelPrimitiveType("Edm", "GeographyMultiLineString", EdmPrimitiveTypeKind.GeographyMultiLineString);
			EdmCoreModel.EdmValidCoreModelPrimitiveType edmValidCoreModelPrimitiveType25 = new EdmCoreModel.EdmValidCoreModelPrimitiveType("Edm", "GeographyMultiPoint", EdmPrimitiveTypeKind.GeographyMultiPoint);
			EdmCoreModel.EdmValidCoreModelPrimitiveType edmValidCoreModelPrimitiveType26 = new EdmCoreModel.EdmValidCoreModelPrimitiveType("Edm", "Geometry", EdmPrimitiveTypeKind.Geometry);
			EdmCoreModel.EdmValidCoreModelPrimitiveType edmValidCoreModelPrimitiveType27 = new EdmCoreModel.EdmValidCoreModelPrimitiveType("Edm", "GeometryPoint", EdmPrimitiveTypeKind.GeometryPoint);
			EdmCoreModel.EdmValidCoreModelPrimitiveType edmValidCoreModelPrimitiveType28 = new EdmCoreModel.EdmValidCoreModelPrimitiveType("Edm", "GeometryLineString", EdmPrimitiveTypeKind.GeometryLineString);
			EdmCoreModel.EdmValidCoreModelPrimitiveType edmValidCoreModelPrimitiveType29 = new EdmCoreModel.EdmValidCoreModelPrimitiveType("Edm", "GeometryPolygon", EdmPrimitiveTypeKind.GeometryPolygon);
			EdmCoreModel.EdmValidCoreModelPrimitiveType edmValidCoreModelPrimitiveType30 = new EdmCoreModel.EdmValidCoreModelPrimitiveType("Edm", "GeometryCollection", EdmPrimitiveTypeKind.GeometryCollection);
			EdmCoreModel.EdmValidCoreModelPrimitiveType edmValidCoreModelPrimitiveType31 = new EdmCoreModel.EdmValidCoreModelPrimitiveType("Edm", "GeometryMultiPolygon", EdmPrimitiveTypeKind.GeometryMultiPolygon);
			EdmCoreModel.EdmValidCoreModelPrimitiveType edmValidCoreModelPrimitiveType32 = new EdmCoreModel.EdmValidCoreModelPrimitiveType("Edm", "GeometryMultiLineString", EdmPrimitiveTypeKind.GeometryMultiLineString);
			EdmCoreModel.EdmValidCoreModelPrimitiveType edmValidCoreModelPrimitiveType33 = new EdmCoreModel.EdmValidCoreModelPrimitiveType("Edm", "GeometryMultiPoint", EdmPrimitiveTypeKind.GeometryMultiPoint);
			this.primitiveTypes = new EdmCoreModel.EdmValidCoreModelPrimitiveType[]
			{
				edmValidCoreModelPrimitiveType15, edmValidCoreModelPrimitiveType8, edmValidCoreModelPrimitiveType7, edmValidCoreModelPrimitiveType13, edmValidCoreModelPrimitiveType12, edmValidCoreModelPrimitiveType14, edmValidCoreModelPrimitiveType, edmValidCoreModelPrimitiveType9, edmValidCoreModelPrimitiveType5, edmValidCoreModelPrimitiveType4,
				edmValidCoreModelPrimitiveType3, edmValidCoreModelPrimitiveType6, edmValidCoreModelPrimitiveType2, edmValidCoreModelPrimitiveType17, edmValidCoreModelPrimitiveType16, edmValidCoreModelPrimitiveType10, edmValidCoreModelPrimitiveType11, edmValidCoreModelPrimitiveType18, edmValidCoreModelPrimitiveType19, edmValidCoreModelPrimitiveType20,
				edmValidCoreModelPrimitiveType21, edmValidCoreModelPrimitiveType22, edmValidCoreModelPrimitiveType23, edmValidCoreModelPrimitiveType24, edmValidCoreModelPrimitiveType25, edmValidCoreModelPrimitiveType26, edmValidCoreModelPrimitiveType27, edmValidCoreModelPrimitiveType28, edmValidCoreModelPrimitiveType29, edmValidCoreModelPrimitiveType30,
				edmValidCoreModelPrimitiveType31, edmValidCoreModelPrimitiveType32, edmValidCoreModelPrimitiveType33
			};
			foreach (EdmCoreModel.EdmValidCoreModelPrimitiveType edmValidCoreModelPrimitiveType34 in this.primitiveTypes)
			{
				this.primitiveTypeKinds[edmValidCoreModelPrimitiveType34.Name] = edmValidCoreModelPrimitiveType34.PrimitiveKind;
				this.primitiveTypeKinds[edmValidCoreModelPrimitiveType34.Namespace + "." + edmValidCoreModelPrimitiveType34.Name] = edmValidCoreModelPrimitiveType34.PrimitiveKind;
				this.primitiveTypesByKind[edmValidCoreModelPrimitiveType34.PrimitiveKind] = edmValidCoreModelPrimitiveType34;
				this.primitiveTypeByName[edmValidCoreModelPrimitiveType34.Namespace + "." + edmValidCoreModelPrimitiveType34.Name] = edmValidCoreModelPrimitiveType34;
				this.primitiveTypeByName[edmValidCoreModelPrimitiveType34.Name] = edmValidCoreModelPrimitiveType34;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x0000A2EF File Offset: 0x000084EF
		public static string Namespace
		{
			get
			{
				return "Edm";
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x0000A2F6 File Offset: 0x000084F6
		public IEnumerable<IEdmSchemaElement> SchemaElements
		{
			get
			{
				return this.primitiveTypes;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x0000A2FE File Offset: 0x000084FE
		public IEnumerable<string> DeclaredNamespaces
		{
			get
			{
				return Enumerable.Empty<string>();
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x0000A305 File Offset: 0x00008505
		public IEnumerable<IEdmVocabularyAnnotation> VocabularyAnnotations
		{
			get
			{
				return Enumerable.Empty<IEdmVocabularyAnnotation>();
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x0000A30C File Offset: 0x0000850C
		public IEnumerable<IEdmModel> ReferencedModels
		{
			get
			{
				return Enumerable.Empty<IEdmModel>();
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x0000A313 File Offset: 0x00008513
		public IEdmDirectValueAnnotationsManager DirectValueAnnotationsManager
		{
			get
			{
				return this.annotationsManager;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060002FA RID: 762 RVA: 0x00008D69 File Offset: 0x00006F69
		public IEdmEntityContainer EntityContainer
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0000A31B File Offset: 0x0000851B
		public static IEdmCollectionTypeReference GetCollection(IEdmTypeReference elementType)
		{
			return new EdmCollectionTypeReference(new EdmCollectionType(elementType));
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0000A328 File Offset: 0x00008528
		public IEdmSchemaType FindDeclaredType(string qualifiedName)
		{
			EdmCoreModel.EdmValidCoreModelPrimitiveType edmValidCoreModelPrimitiveType;
			if (this.primitiveTypeByName.TryGetValue(qualifiedName, ref edmValidCoreModelPrimitiveType))
			{
				return edmValidCoreModelPrimitiveType;
			}
			if (string.Equals(qualifiedName, "Edm.Untyped", 4) || string.Equals(qualifiedName, "Untyped", 4))
			{
				return this.untypedType;
			}
			return null;
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0000A36B File Offset: 0x0000856B
		public IEnumerable<IEdmOperation> FindDeclaredBoundOperations(IEdmType bindingType)
		{
			return Enumerable.Empty<IEdmOperation>();
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0000A36B File Offset: 0x0000856B
		public IEnumerable<IEdmOperation> FindDeclaredBoundOperations(string qualifiedName, IEdmType bindingType)
		{
			return Enumerable.Empty<IEdmOperation>();
		}

		// Token: 0x060002FF RID: 767 RVA: 0x00008D69 File Offset: 0x00006F69
		public IEdmTerm FindDeclaredTerm(string qualifiedName)
		{
			return null;
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0000A36B File Offset: 0x0000856B
		public IEnumerable<IEdmOperation> FindDeclaredOperations(string qualifiedName)
		{
			return Enumerable.Empty<IEdmOperation>();
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0000A372 File Offset: 0x00008572
		public IEnumerable<IEdmOperationImport> FindOperationImportsByNameNonBindingParameterType(string operationImportName, IEnumerable<string> parameterNames)
		{
			return Enumerable.Empty<IEdmOperationImport>();
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0000A379 File Offset: 0x00008579
		public IEdmPrimitiveType GetPrimitiveType(EdmPrimitiveTypeKind kind)
		{
			return this.GetCoreModelPrimitiveType(kind);
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0000A382 File Offset: 0x00008582
		public IEdmUntypedType GetUntypedType()
		{
			return this.untypedType;
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0000A38C File Offset: 0x0000858C
		public EdmPrimitiveTypeKind GetPrimitiveTypeKind(string typeName)
		{
			EdmPrimitiveTypeKind edmPrimitiveTypeKind;
			if (!this.primitiveTypeKinds.TryGetValue(typeName, ref edmPrimitiveTypeKind))
			{
				return EdmPrimitiveTypeKind.None;
			}
			return edmPrimitiveTypeKind;
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0000A3AC File Offset: 0x000085AC
		public IEdmPrimitiveTypeReference GetPrimitive(EdmPrimitiveTypeKind kind, bool isNullable)
		{
			IEdmPrimitiveType coreModelPrimitiveType = this.GetCoreModelPrimitiveType(kind);
			if (coreModelPrimitiveType != null)
			{
				return coreModelPrimitiveType.GetPrimitiveTypeReference(isNullable);
			}
			throw new InvalidOperationException(Strings.EdmPrimitive_UnexpectedKind);
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0000A3D6 File Offset: 0x000085D6
		public IEdmPrimitiveTypeReference GetInt16(bool isNullable)
		{
			return new EdmPrimitiveTypeReference(this.GetCoreModelPrimitiveType(EdmPrimitiveTypeKind.Int16), isNullable);
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0000A3E5 File Offset: 0x000085E5
		public IEdmPrimitiveTypeReference GetInt32(bool isNullable)
		{
			return new EdmPrimitiveTypeReference(this.GetCoreModelPrimitiveType(EdmPrimitiveTypeKind.Int32), isNullable);
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0000A3F5 File Offset: 0x000085F5
		public IEdmPrimitiveTypeReference GetInt64(bool isNullable)
		{
			return new EdmPrimitiveTypeReference(this.GetCoreModelPrimitiveType(EdmPrimitiveTypeKind.Int64), isNullable);
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0000A405 File Offset: 0x00008605
		public IEdmPrimitiveTypeReference GetBoolean(bool isNullable)
		{
			return new EdmPrimitiveTypeReference(this.GetCoreModelPrimitiveType(EdmPrimitiveTypeKind.Boolean), isNullable);
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0000A414 File Offset: 0x00008614
		public IEdmPrimitiveTypeReference GetByte(bool isNullable)
		{
			return new EdmPrimitiveTypeReference(this.GetCoreModelPrimitiveType(EdmPrimitiveTypeKind.Byte), isNullable);
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0000A423 File Offset: 0x00008623
		public IEdmPrimitiveTypeReference GetSByte(bool isNullable)
		{
			return new EdmPrimitiveTypeReference(this.GetCoreModelPrimitiveType(EdmPrimitiveTypeKind.SByte), isNullable);
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000A433 File Offset: 0x00008633
		public IEdmPrimitiveTypeReference GetGuid(bool isNullable)
		{
			return new EdmPrimitiveTypeReference(this.GetCoreModelPrimitiveType(EdmPrimitiveTypeKind.Guid), isNullable);
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000A442 File Offset: 0x00008642
		public IEdmPrimitiveTypeReference GetDate(bool isNullable)
		{
			return new EdmPrimitiveTypeReference(this.GetCoreModelPrimitiveType(EdmPrimitiveTypeKind.Date), isNullable);
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000A452 File Offset: 0x00008652
		public IEdmTemporalTypeReference GetDateTimeOffset(bool isNullable)
		{
			return new EdmTemporalTypeReference(this.GetCoreModelPrimitiveType(EdmPrimitiveTypeKind.DateTimeOffset), isNullable);
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000A461 File Offset: 0x00008661
		public IEdmTemporalTypeReference GetDuration(bool isNullable)
		{
			return new EdmTemporalTypeReference(this.GetCoreModelPrimitiveType(EdmPrimitiveTypeKind.Duration), isNullable);
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000A471 File Offset: 0x00008671
		public IEdmTemporalTypeReference GetTimeOfDay(bool isNullable)
		{
			return new EdmTemporalTypeReference(this.GetCoreModelPrimitiveType(EdmPrimitiveTypeKind.TimeOfDay), isNullable);
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0000A481 File Offset: 0x00008681
		public IEdmDecimalTypeReference GetDecimal(int? precision, int? scale, bool isNullable)
		{
			if (precision != null || scale != null)
			{
				return new EdmDecimalTypeReference(this.GetCoreModelPrimitiveType(EdmPrimitiveTypeKind.Decimal), isNullable, precision, scale);
			}
			return new EdmDecimalTypeReference(this.GetCoreModelPrimitiveType(EdmPrimitiveTypeKind.Decimal), isNullable);
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0000A4B2 File Offset: 0x000086B2
		public IEdmDecimalTypeReference GetDecimal(bool isNullable)
		{
			return new EdmDecimalTypeReference(this.GetCoreModelPrimitiveType(EdmPrimitiveTypeKind.Decimal), isNullable);
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0000A4C1 File Offset: 0x000086C1
		public IEdmPrimitiveTypeReference GetSingle(bool isNullable)
		{
			return new EdmPrimitiveTypeReference(this.GetCoreModelPrimitiveType(EdmPrimitiveTypeKind.Single), isNullable);
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000A4D1 File Offset: 0x000086D1
		public IEdmPrimitiveTypeReference GetDouble(bool isNullable)
		{
			return new EdmPrimitiveTypeReference(this.GetCoreModelPrimitiveType(EdmPrimitiveTypeKind.Double), isNullable);
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0000A4E0 File Offset: 0x000086E0
		public IEdmPrimitiveTypeReference GetStream(bool isNullable)
		{
			return new EdmPrimitiveTypeReference(this.GetCoreModelPrimitiveType(EdmPrimitiveTypeKind.Stream), isNullable);
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0000A4F0 File Offset: 0x000086F0
		public IEdmTemporalTypeReference GetTemporal(EdmPrimitiveTypeKind kind, int? precision, bool isNullable)
		{
			if (kind == EdmPrimitiveTypeKind.DateTimeOffset || kind == EdmPrimitiveTypeKind.Duration || kind == EdmPrimitiveTypeKind.TimeOfDay)
			{
				return new EdmTemporalTypeReference(this.GetCoreModelPrimitiveType(kind), isNullable, precision);
			}
			throw new InvalidOperationException(Strings.EdmPrimitive_UnexpectedKind);
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0000A519 File Offset: 0x00008719
		public IEdmTemporalTypeReference GetTemporal(EdmPrimitiveTypeKind kind, bool isNullable)
		{
			if (kind == EdmPrimitiveTypeKind.DateTimeOffset || kind == EdmPrimitiveTypeKind.Duration || kind == EdmPrimitiveTypeKind.TimeOfDay)
			{
				return new EdmTemporalTypeReference(this.GetCoreModelPrimitiveType(kind), isNullable);
			}
			throw new InvalidOperationException(Strings.EdmPrimitive_UnexpectedKind);
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0000A541 File Offset: 0x00008741
		public IEdmBinaryTypeReference GetBinary(bool isUnbounded, int? maxLength, bool isNullable)
		{
			return new EdmBinaryTypeReference(this.GetCoreModelPrimitiveType(EdmPrimitiveTypeKind.Binary), isNullable, isUnbounded, maxLength);
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0000A552 File Offset: 0x00008752
		public IEdmBinaryTypeReference GetBinary(bool isNullable)
		{
			return new EdmBinaryTypeReference(this.GetCoreModelPrimitiveType(EdmPrimitiveTypeKind.Binary), isNullable);
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0000A564 File Offset: 0x00008764
		public IEdmSpatialTypeReference GetSpatial(EdmPrimitiveTypeKind kind, int? spatialReferenceIdentifier, bool isNullable)
		{
			switch (kind)
			{
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
				return new EdmSpatialTypeReference(this.GetCoreModelPrimitiveType(kind), isNullable, spatialReferenceIdentifier);
			default:
				throw new InvalidOperationException(Strings.EdmPrimitive_UnexpectedKind);
			}
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0000A5D8 File Offset: 0x000087D8
		public IEdmSpatialTypeReference GetSpatial(EdmPrimitiveTypeKind kind, bool isNullable)
		{
			switch (kind)
			{
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
				return new EdmSpatialTypeReference(this.GetCoreModelPrimitiveType(kind), isNullable);
			default:
				throw new InvalidOperationException(Strings.EdmPrimitive_UnexpectedKind);
			}
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0000A648 File Offset: 0x00008848
		public IEdmStringTypeReference GetString(bool isUnbounded, int? maxLength, bool? isUnicode, bool isNullable)
		{
			return new EdmStringTypeReference(this.GetCoreModelPrimitiveType(EdmPrimitiveTypeKind.String), isNullable, isUnbounded, maxLength, isUnicode);
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0000A65C File Offset: 0x0000885C
		public IEdmStringTypeReference GetString(bool isNullable)
		{
			return new EdmStringTypeReference(this.GetCoreModelPrimitiveType(EdmPrimitiveTypeKind.String), isNullable);
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0000A66C File Offset: 0x0000886C
		public IEdmUntypedTypeReference GetUntyped()
		{
			return new EdmUntypedTypeReference(this.GetUntypedType());
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0000A305 File Offset: 0x00008505
		public IEnumerable<IEdmVocabularyAnnotation> FindDeclaredVocabularyAnnotations(IEdmVocabularyAnnotatable element)
		{
			return Enumerable.Empty<IEdmVocabularyAnnotation>();
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0000A679 File Offset: 0x00008879
		public IEnumerable<IEdmStructuredType> FindDirectlyDerivedTypes(IEdmStructuredType baseType)
		{
			return Enumerable.Empty<IEdmStructuredType>();
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0000A680 File Offset: 0x00008880
		private EdmCoreModel.EdmValidCoreModelPrimitiveType GetCoreModelPrimitiveType(EdmPrimitiveTypeKind kind)
		{
			EdmCoreModel.EdmValidCoreModelPrimitiveType edmValidCoreModelPrimitiveType;
			if (!this.primitiveTypesByKind.TryGetValue(kind, ref edmValidCoreModelPrimitiveType))
			{
				return null;
			}
			return edmValidCoreModelPrimitiveType;
		}

		// Token: 0x0400009E RID: 158
		public static readonly EdmCoreModel Instance = new EdmCoreModel();

		// Token: 0x0400009F RID: 159
		private readonly EdmCoreModel.EdmValidCoreModelPrimitiveType[] primitiveTypes;

		// Token: 0x040000A0 RID: 160
		private const string EdmNamespace = "Edm";

		// Token: 0x040000A1 RID: 161
		private readonly Dictionary<string, EdmPrimitiveTypeKind> primitiveTypeKinds = new Dictionary<string, EdmPrimitiveTypeKind>();

		// Token: 0x040000A2 RID: 162
		private readonly Dictionary<EdmPrimitiveTypeKind, EdmCoreModel.EdmValidCoreModelPrimitiveType> primitiveTypesByKind = new Dictionary<EdmPrimitiveTypeKind, EdmCoreModel.EdmValidCoreModelPrimitiveType>();

		// Token: 0x040000A3 RID: 163
		private readonly Dictionary<string, EdmCoreModel.EdmValidCoreModelPrimitiveType> primitiveTypeByName = new Dictionary<string, EdmCoreModel.EdmValidCoreModelPrimitiveType>();

		// Token: 0x040000A4 RID: 164
		private readonly EdmCoreModel.EdmValidCoreModelUntypedType untypedType = new EdmCoreModel.EdmValidCoreModelUntypedType();

		// Token: 0x040000A5 RID: 165
		private readonly IEdmDirectValueAnnotationsManager annotationsManager = new EdmDirectValueAnnotationsManager();

		// Token: 0x0200021C RID: 540
		internal sealed class EdmValidCoreModelUntypedType : EdmType, IEdmUntypedType, IEdmSchemaType, IEdmSchemaElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable, IEdmType, IEdmValidCoreModelElement
		{
			// Token: 0x17000491 RID: 1169
			// (get) Token: 0x06000DE1 RID: 3553 RVA: 0x0000C876 File Offset: 0x0000AA76
			public override EdmTypeKind TypeKind
			{
				get
				{
					return EdmTypeKind.Untyped;
				}
			}

			// Token: 0x17000492 RID: 1170
			// (get) Token: 0x06000DE2 RID: 3554 RVA: 0x00008D76 File Offset: 0x00006F76
			public EdmSchemaElementKind SchemaElementKind
			{
				get
				{
					return EdmSchemaElementKind.TypeDefinition;
				}
			}

			// Token: 0x17000493 RID: 1171
			// (get) Token: 0x06000DE3 RID: 3555 RVA: 0x00025516 File Offset: 0x00023716
			public string Name
			{
				get
				{
					return "Untyped";
				}
			}

			// Token: 0x17000494 RID: 1172
			// (get) Token: 0x06000DE4 RID: 3556 RVA: 0x0000A2EF File Offset: 0x000084EF
			public string Namespace
			{
				get
				{
					return "Edm";
				}
			}
		}

		// Token: 0x0200021D RID: 541
		internal sealed class EdmValidCoreModelPrimitiveType : EdmType, IEdmPrimitiveType, IEdmSchemaType, IEdmSchemaElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable, IEdmType, IEdmValidCoreModelElement
		{
			// Token: 0x06000DE6 RID: 3558 RVA: 0x00025528 File Offset: 0x00023728
			public EdmValidCoreModelPrimitiveType(string namespaceName, string name, EdmPrimitiveTypeKind primitiveKind)
			{
				this.namespaceName = namespaceName ?? string.Empty;
				this.name = name ?? string.Empty;
				this.primitiveKind = primitiveKind;
				this.fullName = this.namespaceName + "." + this.name;
			}

			// Token: 0x17000495 RID: 1173
			// (get) Token: 0x06000DE7 RID: 3559 RVA: 0x0002557E File Offset: 0x0002377E
			public string Name
			{
				get
				{
					return this.name;
				}
			}

			// Token: 0x17000496 RID: 1174
			// (get) Token: 0x06000DE8 RID: 3560 RVA: 0x00025586 File Offset: 0x00023786
			public string Namespace
			{
				get
				{
					return this.namespaceName;
				}
			}

			// Token: 0x17000497 RID: 1175
			// (get) Token: 0x06000DE9 RID: 3561 RVA: 0x00008D76 File Offset: 0x00006F76
			public override EdmTypeKind TypeKind
			{
				get
				{
					return EdmTypeKind.Primitive;
				}
			}

			// Token: 0x17000498 RID: 1176
			// (get) Token: 0x06000DEA RID: 3562 RVA: 0x0002558E File Offset: 0x0002378E
			public EdmPrimitiveTypeKind PrimitiveKind
			{
				get
				{
					return this.primitiveKind;
				}
			}

			// Token: 0x17000499 RID: 1177
			// (get) Token: 0x06000DEB RID: 3563 RVA: 0x00008D76 File Offset: 0x00006F76
			public EdmSchemaElementKind SchemaElementKind
			{
				get
				{
					return EdmSchemaElementKind.TypeDefinition;
				}
			}

			// Token: 0x1700049A RID: 1178
			// (get) Token: 0x06000DEC RID: 3564 RVA: 0x00025596 File Offset: 0x00023796
			public string FullName
			{
				get
				{
					return this.fullName;
				}
			}

			// Token: 0x04000788 RID: 1928
			private readonly string namespaceName;

			// Token: 0x04000789 RID: 1929
			private readonly string name;

			// Token: 0x0400078A RID: 1930
			private readonly EdmPrimitiveTypeKind primitiveKind;

			// Token: 0x0400078B RID: 1931
			private readonly string fullName;
		}
	}
}
