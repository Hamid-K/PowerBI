using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Annotations;
using Microsoft.OData.Edm.Library.Annotations;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x02000217 RID: 535
	public class EdmCoreModel : EdmElement, IEdmModel, IEdmElement, IEdmValidCoreModelElement
	{
		// Token: 0x06000C6E RID: 3182 RVA: 0x00022F84 File Offset: 0x00021184
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
				this.primitiveTypeKinds[edmValidCoreModelPrimitiveType34.Namespace + '.' + edmValidCoreModelPrimitiveType34.Name] = edmValidCoreModelPrimitiveType34.PrimitiveKind;
				this.primitiveTypesByKind[edmValidCoreModelPrimitiveType34.PrimitiveKind] = edmValidCoreModelPrimitiveType34;
				this.primitiveTypeByName[edmValidCoreModelPrimitiveType34.Namespace + '.' + edmValidCoreModelPrimitiveType34.Name] = edmValidCoreModelPrimitiveType34;
				this.primitiveTypeByName[edmValidCoreModelPrimitiveType34.Name] = edmValidCoreModelPrimitiveType34;
			}
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06000C6F RID: 3183 RVA: 0x000233D1 File Offset: 0x000215D1
		public static string Namespace
		{
			get
			{
				return "Edm";
			}
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06000C70 RID: 3184 RVA: 0x000233D8 File Offset: 0x000215D8
		public IEnumerable<IEdmSchemaElement> SchemaElements
		{
			get
			{
				return this.primitiveTypes;
			}
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x06000C71 RID: 3185 RVA: 0x000233E0 File Offset: 0x000215E0
		public IEnumerable<string> DeclaredNamespaces
		{
			get
			{
				return Enumerable.Empty<string>();
			}
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x06000C72 RID: 3186 RVA: 0x000233E7 File Offset: 0x000215E7
		public IEnumerable<IEdmVocabularyAnnotation> VocabularyAnnotations
		{
			get
			{
				return Enumerable.Empty<IEdmVocabularyAnnotation>();
			}
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x06000C73 RID: 3187 RVA: 0x000233EE File Offset: 0x000215EE
		public IEnumerable<IEdmModel> ReferencedModels
		{
			get
			{
				return Enumerable.Empty<IEdmModel>();
			}
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x06000C74 RID: 3188 RVA: 0x000233F5 File Offset: 0x000215F5
		public IEdmDirectValueAnnotationsManager DirectValueAnnotationsManager
		{
			get
			{
				return this.annotationsManager;
			}
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x06000C75 RID: 3189 RVA: 0x000233FD File Offset: 0x000215FD
		public IEdmEntityContainer EntityContainer
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000C76 RID: 3190 RVA: 0x00023400 File Offset: 0x00021600
		public static IEdmCollectionTypeReference GetCollection(IEdmTypeReference elementType)
		{
			return new EdmCollectionTypeReference(new EdmCollectionType(elementType));
		}

		// Token: 0x06000C77 RID: 3191 RVA: 0x00023410 File Offset: 0x00021610
		public IEdmSchemaType FindDeclaredType(string qualifiedName)
		{
			EdmCoreModel.EdmValidCoreModelPrimitiveType edmValidCoreModelPrimitiveType;
			if (!this.primitiveTypeByName.TryGetValue(qualifiedName, ref edmValidCoreModelPrimitiveType))
			{
				return null;
			}
			return edmValidCoreModelPrimitiveType;
		}

		// Token: 0x06000C78 RID: 3192 RVA: 0x00023430 File Offset: 0x00021630
		public IEnumerable<IEdmOperation> FindDeclaredBoundOperations(IEdmType bindingType)
		{
			return Enumerable.Empty<IEdmOperation>();
		}

		// Token: 0x06000C79 RID: 3193 RVA: 0x00023437 File Offset: 0x00021637
		public IEnumerable<IEdmOperation> FindDeclaredBoundOperations(string qualifiedName, IEdmType bindingType)
		{
			return Enumerable.Empty<IEdmOperation>();
		}

		// Token: 0x06000C7A RID: 3194 RVA: 0x0002343E File Offset: 0x0002163E
		public IEdmValueTerm FindDeclaredValueTerm(string qualifiedName)
		{
			return null;
		}

		// Token: 0x06000C7B RID: 3195 RVA: 0x00023441 File Offset: 0x00021641
		public IEnumerable<IEdmOperation> FindDeclaredOperations(string qualifiedName)
		{
			return Enumerable.Empty<IEdmOperation>();
		}

		// Token: 0x06000C7C RID: 3196 RVA: 0x00023448 File Offset: 0x00021648
		public IEnumerable<IEdmOperationImport> FindOperationImportsByNameNonBindingParameterType(string operationImportName, IEnumerable<string> parameterNames)
		{
			return Enumerable.Empty<IEdmOperationImport>();
		}

		// Token: 0x06000C7D RID: 3197 RVA: 0x0002344F File Offset: 0x0002164F
		public IEdmPrimitiveType GetPrimitiveType(EdmPrimitiveTypeKind kind)
		{
			return this.GetCoreModelPrimitiveType(kind);
		}

		// Token: 0x06000C7E RID: 3198 RVA: 0x00023458 File Offset: 0x00021658
		public EdmPrimitiveTypeKind GetPrimitiveTypeKind(string typeName)
		{
			EdmPrimitiveTypeKind edmPrimitiveTypeKind;
			if (!this.primitiveTypeKinds.TryGetValue(typeName, ref edmPrimitiveTypeKind))
			{
				return EdmPrimitiveTypeKind.None;
			}
			return edmPrimitiveTypeKind;
		}

		// Token: 0x06000C7F RID: 3199 RVA: 0x00023478 File Offset: 0x00021678
		public IEdmPrimitiveTypeReference GetPrimitive(EdmPrimitiveTypeKind kind, bool isNullable)
		{
			IEdmPrimitiveType coreModelPrimitiveType = this.GetCoreModelPrimitiveType(kind);
			if (coreModelPrimitiveType != null)
			{
				return coreModelPrimitiveType.GetPrimitiveTypeReference(isNullable);
			}
			throw new InvalidOperationException(Strings.EdmPrimitive_UnexpectedKind);
		}

		// Token: 0x06000C80 RID: 3200 RVA: 0x000234A2 File Offset: 0x000216A2
		public IEdmPrimitiveTypeReference GetInt16(bool isNullable)
		{
			return new EdmPrimitiveTypeReference(this.GetCoreModelPrimitiveType(EdmPrimitiveTypeKind.Int16), isNullable);
		}

		// Token: 0x06000C81 RID: 3201 RVA: 0x000234B1 File Offset: 0x000216B1
		public IEdmPrimitiveTypeReference GetInt32(bool isNullable)
		{
			return new EdmPrimitiveTypeReference(this.GetCoreModelPrimitiveType(EdmPrimitiveTypeKind.Int32), isNullable);
		}

		// Token: 0x06000C82 RID: 3202 RVA: 0x000234C1 File Offset: 0x000216C1
		public IEdmPrimitiveTypeReference GetInt64(bool isNullable)
		{
			return new EdmPrimitiveTypeReference(this.GetCoreModelPrimitiveType(EdmPrimitiveTypeKind.Int64), isNullable);
		}

		// Token: 0x06000C83 RID: 3203 RVA: 0x000234D1 File Offset: 0x000216D1
		public IEdmPrimitiveTypeReference GetBoolean(bool isNullable)
		{
			return new EdmPrimitiveTypeReference(this.GetCoreModelPrimitiveType(EdmPrimitiveTypeKind.Boolean), isNullable);
		}

		// Token: 0x06000C84 RID: 3204 RVA: 0x000234E0 File Offset: 0x000216E0
		public IEdmPrimitiveTypeReference GetByte(bool isNullable)
		{
			return new EdmPrimitiveTypeReference(this.GetCoreModelPrimitiveType(EdmPrimitiveTypeKind.Byte), isNullable);
		}

		// Token: 0x06000C85 RID: 3205 RVA: 0x000234EF File Offset: 0x000216EF
		public IEdmPrimitiveTypeReference GetSByte(bool isNullable)
		{
			return new EdmPrimitiveTypeReference(this.GetCoreModelPrimitiveType(EdmPrimitiveTypeKind.SByte), isNullable);
		}

		// Token: 0x06000C86 RID: 3206 RVA: 0x000234FF File Offset: 0x000216FF
		public IEdmPrimitiveTypeReference GetGuid(bool isNullable)
		{
			return new EdmPrimitiveTypeReference(this.GetCoreModelPrimitiveType(EdmPrimitiveTypeKind.Guid), isNullable);
		}

		// Token: 0x06000C87 RID: 3207 RVA: 0x0002350E File Offset: 0x0002170E
		public IEdmPrimitiveTypeReference GetDate(bool isNullable)
		{
			return new EdmPrimitiveTypeReference(this.GetCoreModelPrimitiveType(EdmPrimitiveTypeKind.Date), isNullable);
		}

		// Token: 0x06000C88 RID: 3208 RVA: 0x0002351E File Offset: 0x0002171E
		public IEdmTemporalTypeReference GetDateTimeOffset(bool isNullable)
		{
			return new EdmTemporalTypeReference(this.GetCoreModelPrimitiveType(EdmPrimitiveTypeKind.DateTimeOffset), isNullable);
		}

		// Token: 0x06000C89 RID: 3209 RVA: 0x0002352D File Offset: 0x0002172D
		public IEdmTemporalTypeReference GetDuration(bool isNullable)
		{
			return new EdmTemporalTypeReference(this.GetCoreModelPrimitiveType(EdmPrimitiveTypeKind.Duration), isNullable);
		}

		// Token: 0x06000C8A RID: 3210 RVA: 0x0002353D File Offset: 0x0002173D
		public IEdmTemporalTypeReference GetTimeOfDay(bool isNullable)
		{
			return new EdmTemporalTypeReference(this.GetCoreModelPrimitiveType(EdmPrimitiveTypeKind.TimeOfDay), isNullable);
		}

		// Token: 0x06000C8B RID: 3211 RVA: 0x0002354D File Offset: 0x0002174D
		public IEdmDecimalTypeReference GetDecimal(int? precision, int? scale, bool isNullable)
		{
			if (precision != null || scale != null)
			{
				return new EdmDecimalTypeReference(this.GetCoreModelPrimitiveType(EdmPrimitiveTypeKind.Decimal), isNullable, precision, scale);
			}
			return new EdmDecimalTypeReference(this.GetCoreModelPrimitiveType(EdmPrimitiveTypeKind.Decimal), isNullable);
		}

		// Token: 0x06000C8C RID: 3212 RVA: 0x0002357E File Offset: 0x0002177E
		public IEdmDecimalTypeReference GetDecimal(bool isNullable)
		{
			return new EdmDecimalTypeReference(this.GetCoreModelPrimitiveType(EdmPrimitiveTypeKind.Decimal), isNullable);
		}

		// Token: 0x06000C8D RID: 3213 RVA: 0x0002358D File Offset: 0x0002178D
		public IEdmPrimitiveTypeReference GetSingle(bool isNullable)
		{
			return new EdmPrimitiveTypeReference(this.GetCoreModelPrimitiveType(EdmPrimitiveTypeKind.Single), isNullable);
		}

		// Token: 0x06000C8E RID: 3214 RVA: 0x0002359D File Offset: 0x0002179D
		public IEdmPrimitiveTypeReference GetDouble(bool isNullable)
		{
			return new EdmPrimitiveTypeReference(this.GetCoreModelPrimitiveType(EdmPrimitiveTypeKind.Double), isNullable);
		}

		// Token: 0x06000C8F RID: 3215 RVA: 0x000235AC File Offset: 0x000217AC
		public IEdmPrimitiveTypeReference GetStream(bool isNullable)
		{
			return new EdmPrimitiveTypeReference(this.GetCoreModelPrimitiveType(EdmPrimitiveTypeKind.Stream), isNullable);
		}

		// Token: 0x06000C90 RID: 3216 RVA: 0x000235BC File Offset: 0x000217BC
		public IEdmTemporalTypeReference GetTemporal(EdmPrimitiveTypeKind kind, int? precision, bool isNullable)
		{
			if (kind == EdmPrimitiveTypeKind.DateTimeOffset || kind == EdmPrimitiveTypeKind.Duration || kind == EdmPrimitiveTypeKind.TimeOfDay)
			{
				return new EdmTemporalTypeReference(this.GetCoreModelPrimitiveType(kind), isNullable, precision);
			}
			throw new InvalidOperationException(Strings.EdmPrimitive_UnexpectedKind);
		}

		// Token: 0x06000C91 RID: 3217 RVA: 0x000235F4 File Offset: 0x000217F4
		public IEdmTemporalTypeReference GetTemporal(EdmPrimitiveTypeKind kind, bool isNullable)
		{
			if (kind == EdmPrimitiveTypeKind.DateTimeOffset || kind == EdmPrimitiveTypeKind.Duration || kind == EdmPrimitiveTypeKind.TimeOfDay)
			{
				return new EdmTemporalTypeReference(this.GetCoreModelPrimitiveType(kind), isNullable);
			}
			throw new InvalidOperationException(Strings.EdmPrimitive_UnexpectedKind);
		}

		// Token: 0x06000C92 RID: 3218 RVA: 0x00023629 File Offset: 0x00021829
		public IEdmBinaryTypeReference GetBinary(bool isUnbounded, int? maxLength, bool isNullable)
		{
			return new EdmBinaryTypeReference(this.GetCoreModelPrimitiveType(EdmPrimitiveTypeKind.Binary), isNullable, isUnbounded, maxLength);
		}

		// Token: 0x06000C93 RID: 3219 RVA: 0x0002363A File Offset: 0x0002183A
		public IEdmBinaryTypeReference GetBinary(bool isNullable)
		{
			return new EdmBinaryTypeReference(this.GetCoreModelPrimitiveType(EdmPrimitiveTypeKind.Binary), isNullable);
		}

		// Token: 0x06000C94 RID: 3220 RVA: 0x0002364C File Offset: 0x0002184C
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

		// Token: 0x06000C95 RID: 3221 RVA: 0x000236C0 File Offset: 0x000218C0
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

		// Token: 0x06000C96 RID: 3222 RVA: 0x00023732 File Offset: 0x00021932
		public IEdmStringTypeReference GetString(bool isUnbounded, int? maxLength, bool? isUnicode, bool isNullable)
		{
			return new EdmStringTypeReference(this.GetCoreModelPrimitiveType(EdmPrimitiveTypeKind.String), isNullable, isUnbounded, maxLength, isUnicode);
		}

		// Token: 0x06000C97 RID: 3223 RVA: 0x00023746 File Offset: 0x00021946
		public IEdmStringTypeReference GetString(bool isNullable)
		{
			return new EdmStringTypeReference(this.GetCoreModelPrimitiveType(EdmPrimitiveTypeKind.String), isNullable);
		}

		// Token: 0x06000C98 RID: 3224 RVA: 0x00023756 File Offset: 0x00021956
		public IEnumerable<IEdmVocabularyAnnotation> FindDeclaredVocabularyAnnotations(IEdmVocabularyAnnotatable element)
		{
			return Enumerable.Empty<IEdmVocabularyAnnotation>();
		}

		// Token: 0x06000C99 RID: 3225 RVA: 0x0002375D File Offset: 0x0002195D
		public IEnumerable<IEdmStructuredType> FindDirectlyDerivedTypes(IEdmStructuredType baseType)
		{
			return Enumerable.Empty<IEdmStructuredType>();
		}

		// Token: 0x06000C9A RID: 3226 RVA: 0x00023764 File Offset: 0x00021964
		private EdmCoreModel.EdmValidCoreModelPrimitiveType GetCoreModelPrimitiveType(EdmPrimitiveTypeKind kind)
		{
			EdmCoreModel.EdmValidCoreModelPrimitiveType edmValidCoreModelPrimitiveType;
			if (!this.primitiveTypesByKind.TryGetValue(kind, ref edmValidCoreModelPrimitiveType))
			{
				return null;
			}
			return edmValidCoreModelPrimitiveType;
		}

		// Token: 0x040005C5 RID: 1477
		private const string EdmNamespace = "Edm";

		// Token: 0x040005C6 RID: 1478
		public static readonly EdmCoreModel Instance = new EdmCoreModel();

		// Token: 0x040005C7 RID: 1479
		private readonly EdmCoreModel.EdmValidCoreModelPrimitiveType[] primitiveTypes;

		// Token: 0x040005C8 RID: 1480
		private readonly Dictionary<string, EdmPrimitiveTypeKind> primitiveTypeKinds = new Dictionary<string, EdmPrimitiveTypeKind>();

		// Token: 0x040005C9 RID: 1481
		private readonly Dictionary<EdmPrimitiveTypeKind, EdmCoreModel.EdmValidCoreModelPrimitiveType> primitiveTypesByKind = new Dictionary<EdmPrimitiveTypeKind, EdmCoreModel.EdmValidCoreModelPrimitiveType>();

		// Token: 0x040005CA RID: 1482
		private readonly Dictionary<string, EdmCoreModel.EdmValidCoreModelPrimitiveType> primitiveTypeByName = new Dictionary<string, EdmCoreModel.EdmValidCoreModelPrimitiveType>();

		// Token: 0x040005CB RID: 1483
		private readonly IEdmDirectValueAnnotationsManager annotationsManager = new EdmDirectValueAnnotationsManager();

		// Token: 0x02000218 RID: 536
		private sealed class EdmValidCoreModelPrimitiveType : EdmType, IEdmPrimitiveType, IEdmSchemaType, IEdmSchemaElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmType, IEdmElement, IEdmValidCoreModelElement
		{
			// Token: 0x06000C9C RID: 3228 RVA: 0x00023790 File Offset: 0x00021990
			public EdmValidCoreModelPrimitiveType(string namespaceName, string name, EdmPrimitiveTypeKind primitiveKind)
			{
				this.namespaceName = namespaceName ?? string.Empty;
				this.name = name ?? string.Empty;
				this.primitiveKind = primitiveKind;
			}

			// Token: 0x17000483 RID: 1155
			// (get) Token: 0x06000C9D RID: 3229 RVA: 0x000237BF File Offset: 0x000219BF
			public string Name
			{
				get
				{
					return this.name;
				}
			}

			// Token: 0x17000484 RID: 1156
			// (get) Token: 0x06000C9E RID: 3230 RVA: 0x000237C7 File Offset: 0x000219C7
			public string Namespace
			{
				get
				{
					return this.namespaceName;
				}
			}

			// Token: 0x17000485 RID: 1157
			// (get) Token: 0x06000C9F RID: 3231 RVA: 0x000237CF File Offset: 0x000219CF
			public override EdmTypeKind TypeKind
			{
				get
				{
					return EdmTypeKind.Primitive;
				}
			}

			// Token: 0x17000486 RID: 1158
			// (get) Token: 0x06000CA0 RID: 3232 RVA: 0x000237D2 File Offset: 0x000219D2
			public EdmPrimitiveTypeKind PrimitiveKind
			{
				get
				{
					return this.primitiveKind;
				}
			}

			// Token: 0x17000487 RID: 1159
			// (get) Token: 0x06000CA1 RID: 3233 RVA: 0x000237DA File Offset: 0x000219DA
			public EdmSchemaElementKind SchemaElementKind
			{
				get
				{
					return EdmSchemaElementKind.TypeDefinition;
				}
			}

			// Token: 0x040005CC RID: 1484
			private readonly string namespaceName;

			// Token: 0x040005CD RID: 1485
			private readonly string name;

			// Token: 0x040005CE RID: 1486
			private readonly EdmPrimitiveTypeKind primitiveKind;
		}
	}
}
