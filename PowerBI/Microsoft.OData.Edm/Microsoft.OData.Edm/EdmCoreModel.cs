using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200000F RID: 15
	public class EdmCoreModel : EdmElement, IEdmModel, IEdmElement, IEdmCoreModelElement
	{
		// Token: 0x06000051 RID: 81 RVA: 0x000027A0 File Offset: 0x000009A0
		private EdmCoreModel()
		{
			IList<EdmCoreModelPrimitiveType> list = new List<EdmCoreModelPrimitiveType>
			{
				new EdmCoreModelPrimitiveType(EdmPrimitiveTypeKind.Double),
				new EdmCoreModelPrimitiveType(EdmPrimitiveTypeKind.Single),
				new EdmCoreModelPrimitiveType(EdmPrimitiveTypeKind.Int64),
				new EdmCoreModelPrimitiveType(EdmPrimitiveTypeKind.Int32),
				new EdmCoreModelPrimitiveType(EdmPrimitiveTypeKind.Int16),
				new EdmCoreModelPrimitiveType(EdmPrimitiveTypeKind.SByte),
				new EdmCoreModelPrimitiveType(EdmPrimitiveTypeKind.Byte),
				new EdmCoreModelPrimitiveType(EdmPrimitiveTypeKind.Boolean),
				new EdmCoreModelPrimitiveType(EdmPrimitiveTypeKind.Guid),
				new EdmCoreModelPrimitiveType(EdmPrimitiveTypeKind.Duration),
				new EdmCoreModelPrimitiveType(EdmPrimitiveTypeKind.TimeOfDay),
				new EdmCoreModelPrimitiveType(EdmPrimitiveTypeKind.DateTimeOffset),
				new EdmCoreModelPrimitiveType(EdmPrimitiveTypeKind.Date),
				new EdmCoreModelPrimitiveType(EdmPrimitiveTypeKind.Decimal),
				new EdmCoreModelPrimitiveType(EdmPrimitiveTypeKind.Binary),
				new EdmCoreModelPrimitiveType(EdmPrimitiveTypeKind.String),
				new EdmCoreModelPrimitiveType(EdmPrimitiveTypeKind.Stream),
				new EdmCoreModelPrimitiveType(EdmPrimitiveTypeKind.Geography),
				new EdmCoreModelPrimitiveType(EdmPrimitiveTypeKind.GeographyPoint),
				new EdmCoreModelPrimitiveType(EdmPrimitiveTypeKind.GeographyLineString),
				new EdmCoreModelPrimitiveType(EdmPrimitiveTypeKind.GeographyPolygon),
				new EdmCoreModelPrimitiveType(EdmPrimitiveTypeKind.GeographyCollection),
				new EdmCoreModelPrimitiveType(EdmPrimitiveTypeKind.GeographyMultiPolygon),
				new EdmCoreModelPrimitiveType(EdmPrimitiveTypeKind.GeographyMultiLineString),
				new EdmCoreModelPrimitiveType(EdmPrimitiveTypeKind.GeographyMultiPoint),
				new EdmCoreModelPrimitiveType(EdmPrimitiveTypeKind.Geometry),
				new EdmCoreModelPrimitiveType(EdmPrimitiveTypeKind.GeometryPoint),
				new EdmCoreModelPrimitiveType(EdmPrimitiveTypeKind.GeometryLineString),
				new EdmCoreModelPrimitiveType(EdmPrimitiveTypeKind.GeometryPolygon),
				new EdmCoreModelPrimitiveType(EdmPrimitiveTypeKind.GeometryCollection),
				new EdmCoreModelPrimitiveType(EdmPrimitiveTypeKind.GeometryMultiPolygon),
				new EdmCoreModelPrimitiveType(EdmPrimitiveTypeKind.GeometryMultiLineString),
				new EdmCoreModelPrimitiveType(EdmPrimitiveTypeKind.GeometryMultiPoint),
				this.primitiveType
			};
			foreach (EdmCoreModelPrimitiveType edmCoreModelPrimitiveType in list)
			{
				this.primitiveTypeKinds[edmCoreModelPrimitiveType.Name] = edmCoreModelPrimitiveType.PrimitiveKind;
				this.primitiveTypeKinds[edmCoreModelPrimitiveType.Namespace + "." + edmCoreModelPrimitiveType.Name] = edmCoreModelPrimitiveType.PrimitiveKind;
				this.primitiveTypesByKind[edmCoreModelPrimitiveType.PrimitiveKind] = edmCoreModelPrimitiveType;
				this.coreSchemaTypes[edmCoreModelPrimitiveType.Namespace + "." + edmCoreModelPrimitiveType.Name] = edmCoreModelPrimitiveType;
				this.coreSchemaTypes[edmCoreModelPrimitiveType.Name] = edmCoreModelPrimitiveType;
				this.coreSchemaElements.Add(edmCoreModelPrimitiveType);
			}
			this.coreSchemaElements.Add(this.complexType);
			this.coreSchemaTypes[this.complexType.Namespace + "." + this.complexType.Name] = this.complexType;
			this.coreSchemaTypes[this.complexType.Name] = this.complexType;
			this.coreSchemaElements.Add(this.entityType);
			this.coreSchemaTypes[this.entityType.Namespace + "." + this.entityType.Name] = this.entityType;
			this.coreSchemaTypes[this.entityType.Name] = this.entityType;
			this.coreSchemaElements.Add(this.untypedType);
			this.coreSchemaTypes[this.untypedType.Namespace + "." + this.untypedType.Name] = this.untypedType;
			this.coreSchemaTypes[this.untypedType.Name] = this.untypedType;
			EdmCoreModelPathType[] array = new EdmCoreModelPathType[]
			{
				new EdmCoreModelPathType(EdmPathTypeKind.AnnotationPath),
				new EdmCoreModelPathType(EdmPathTypeKind.PropertyPath),
				new EdmCoreModelPathType(EdmPathTypeKind.NavigationPropertyPath)
			};
			foreach (EdmCoreModelPathType edmCoreModelPathType in array)
			{
				this.pathTypeKinds[edmCoreModelPathType.Name] = edmCoreModelPathType.PathKind;
				this.pathTypeKinds[edmCoreModelPathType.Namespace + "." + edmCoreModelPathType.Name] = edmCoreModelPathType.PathKind;
				this.pathTypesByKind[edmCoreModelPathType.PathKind] = edmCoreModelPathType;
				this.coreSchemaTypes[edmCoreModelPathType.Namespace + "." + edmCoreModelPathType.Name] = edmCoreModelPathType;
				this.coreSchemaTypes[edmCoreModelPathType.Name] = edmCoreModelPathType;
				this.coreSchemaElements.Add(edmCoreModelPathType);
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00002698 File Offset: 0x00000898
		public static string Namespace
		{
			get
			{
				return "Edm";
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00002CAC File Offset: 0x00000EAC
		public IEnumerable<IEdmSchemaElement> SchemaElements
		{
			get
			{
				return this.coreSchemaElements;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00002CB4 File Offset: 0x00000EB4
		public IEnumerable<string> DeclaredNamespaces
		{
			get
			{
				return Enumerable.Empty<string>();
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00002CBB File Offset: 0x00000EBB
		public IEnumerable<IEdmVocabularyAnnotation> VocabularyAnnotations
		{
			get
			{
				return Enumerable.Empty<IEdmVocabularyAnnotation>();
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002CC2 File Offset: 0x00000EC2
		public IEnumerable<IEdmModel> ReferencedModels
		{
			get
			{
				return Enumerable.Empty<IEdmModel>();
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00002CC9 File Offset: 0x00000EC9
		public IEdmDirectValueAnnotationsManager DirectValueAnnotationsManager
		{
			get
			{
				return this.annotationsManager;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000058 RID: 88 RVA: 0x000026B0 File Offset: 0x000008B0
		public IEdmEntityContainer EntityContainer
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002CD1 File Offset: 0x00000ED1
		public static IEdmCollectionTypeReference GetCollection(IEdmTypeReference elementType)
		{
			return new EdmCollectionTypeReference(new EdmCollectionType(elementType));
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002CE0 File Offset: 0x00000EE0
		public IEdmSchemaType FindDeclaredType(string qualifiedName)
		{
			IEdmSchemaType edmSchemaType;
			if (this.coreSchemaTypes.TryGetValue(qualifiedName, out edmSchemaType))
			{
				return edmSchemaType;
			}
			return null;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002D00 File Offset: 0x00000F00
		public IEnumerable<IEdmOperation> FindDeclaredBoundOperations(IEdmType bindingType)
		{
			return Enumerable.Empty<IEdmOperation>();
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002D00 File Offset: 0x00000F00
		public IEnumerable<IEdmOperation> FindDeclaredBoundOperations(string qualifiedName, IEdmType bindingType)
		{
			return Enumerable.Empty<IEdmOperation>();
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000026B0 File Offset: 0x000008B0
		public IEdmTerm FindDeclaredTerm(string qualifiedName)
		{
			return null;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002D00 File Offset: 0x00000F00
		public IEnumerable<IEdmOperation> FindDeclaredOperations(string qualifiedName)
		{
			return Enumerable.Empty<IEdmOperation>();
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002D07 File Offset: 0x00000F07
		public IEnumerable<IEdmOperationImport> FindOperationImportsByNameNonBindingParameterType(string operationImportName, IEnumerable<string> parameterNames)
		{
			return Enumerable.Empty<IEdmOperationImport>();
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002D0E File Offset: 0x00000F0E
		public IEdmPrimitiveType GetPrimitiveType(EdmPrimitiveTypeKind kind)
		{
			return this.GetCoreModelPrimitiveType(kind);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002D17 File Offset: 0x00000F17
		public IEdmPrimitiveType GetPrimitiveType()
		{
			return this.primitiveType;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002D1F File Offset: 0x00000F1F
		public IEdmComplexType GetComplexType()
		{
			return this.complexType;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002D27 File Offset: 0x00000F27
		public IEdmEntityType GetEntityType()
		{
			return this.entityType;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002D2F File Offset: 0x00000F2F
		public IEdmUntypedType GetUntypedType()
		{
			return this.untypedType;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002D38 File Offset: 0x00000F38
		public IEdmPathType GetPathType(EdmPathTypeKind kind)
		{
			EdmCoreModelPathType edmCoreModelPathType;
			if (!this.pathTypesByKind.TryGetValue(kind, out edmCoreModelPathType))
			{
				return null;
			}
			return edmCoreModelPathType;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002D58 File Offset: 0x00000F58
		public EdmPrimitiveTypeKind GetPrimitiveTypeKind(string typeName)
		{
			EdmPrimitiveTypeKind edmPrimitiveTypeKind;
			if (!this.primitiveTypeKinds.TryGetValue(typeName, out edmPrimitiveTypeKind))
			{
				return EdmPrimitiveTypeKind.None;
			}
			return edmPrimitiveTypeKind;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002D78 File Offset: 0x00000F78
		public IEdmPrimitiveTypeReference GetPrimitive(EdmPrimitiveTypeKind kind, bool isNullable)
		{
			IEdmPrimitiveType coreModelPrimitiveType = this.GetCoreModelPrimitiveType(kind);
			if (coreModelPrimitiveType != null)
			{
				return coreModelPrimitiveType.GetPrimitiveTypeReference(isNullable);
			}
			throw new InvalidOperationException(Strings.EdmPrimitive_UnexpectedKind);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002DA4 File Offset: 0x00000FA4
		public EdmPathTypeKind GetPathTypeKind(string typeName)
		{
			EdmPathTypeKind edmPathTypeKind;
			if (!this.pathTypeKinds.TryGetValue(typeName, out edmPathTypeKind))
			{
				return EdmPathTypeKind.None;
			}
			return edmPathTypeKind;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002DC4 File Offset: 0x00000FC4
		public IEdmPathTypeReference GetPathType(EdmPathTypeKind kind, bool isNullable)
		{
			IEdmPathType pathType = this.GetPathType(kind);
			if (pathType != null)
			{
				return new EdmPathTypeReference(pathType, isNullable);
			}
			throw new InvalidOperationException(Strings.EdmPath_UnexpectedKind);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002DEE File Offset: 0x00000FEE
		public IEdmPathTypeReference GetAnnotationPath(bool isNullable)
		{
			return new EdmPathTypeReference(this.GetPathType(EdmPathTypeKind.AnnotationPath), isNullable);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002DFD File Offset: 0x00000FFD
		public IEdmPathTypeReference GetPropertyPath(bool isNullable)
		{
			return new EdmPathTypeReference(this.GetPathType(EdmPathTypeKind.PropertyPath), isNullable);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002E0C File Offset: 0x0000100C
		public IEdmPathTypeReference GetNavigationPropertyPath(bool isNullable)
		{
			return new EdmPathTypeReference(this.GetPathType(EdmPathTypeKind.NavigationPropertyPath), isNullable);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00002E1B File Offset: 0x0000101B
		public IEdmEntityTypeReference GetEntityType(bool isNullable)
		{
			return new EdmEntityTypeReference(this.entityType, isNullable);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002E29 File Offset: 0x00001029
		public IEdmComplexTypeReference GetComplexType(bool isNullable)
		{
			return new EdmComplexTypeReference(this.complexType, isNullable);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002E37 File Offset: 0x00001037
		public IEdmPrimitiveTypeReference GetPrimitiveType(bool isNullable)
		{
			return new EdmPrimitiveTypeReference(this.primitiveType, isNullable);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002E45 File Offset: 0x00001045
		public IEdmPrimitiveTypeReference GetInt16(bool isNullable)
		{
			return new EdmPrimitiveTypeReference(this.GetCoreModelPrimitiveType(EdmPrimitiveTypeKind.Int16), isNullable);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00002E54 File Offset: 0x00001054
		public IEdmPrimitiveTypeReference GetInt32(bool isNullable)
		{
			return new EdmPrimitiveTypeReference(this.GetCoreModelPrimitiveType(EdmPrimitiveTypeKind.Int32), isNullable);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00002E64 File Offset: 0x00001064
		public IEdmPrimitiveTypeReference GetInt64(bool isNullable)
		{
			return new EdmPrimitiveTypeReference(this.GetCoreModelPrimitiveType(EdmPrimitiveTypeKind.Int64), isNullable);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00002E74 File Offset: 0x00001074
		public IEdmPrimitiveTypeReference GetBoolean(bool isNullable)
		{
			return new EdmPrimitiveTypeReference(this.GetCoreModelPrimitiveType(EdmPrimitiveTypeKind.Boolean), isNullable);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00002E83 File Offset: 0x00001083
		public IEdmPrimitiveTypeReference GetByte(bool isNullable)
		{
			return new EdmPrimitiveTypeReference(this.GetCoreModelPrimitiveType(EdmPrimitiveTypeKind.Byte), isNullable);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00002E92 File Offset: 0x00001092
		public IEdmPrimitiveTypeReference GetSByte(bool isNullable)
		{
			return new EdmPrimitiveTypeReference(this.GetCoreModelPrimitiveType(EdmPrimitiveTypeKind.SByte), isNullable);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00002EA2 File Offset: 0x000010A2
		public IEdmPrimitiveTypeReference GetGuid(bool isNullable)
		{
			return new EdmPrimitiveTypeReference(this.GetCoreModelPrimitiveType(EdmPrimitiveTypeKind.Guid), isNullable);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00002EB1 File Offset: 0x000010B1
		public IEdmPrimitiveTypeReference GetDate(bool isNullable)
		{
			return new EdmPrimitiveTypeReference(this.GetCoreModelPrimitiveType(EdmPrimitiveTypeKind.Date), isNullable);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00002EC1 File Offset: 0x000010C1
		public IEdmTemporalTypeReference GetDateTimeOffset(bool isNullable)
		{
			return new EdmTemporalTypeReference(this.GetCoreModelPrimitiveType(EdmPrimitiveTypeKind.DateTimeOffset), isNullable);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00002ED0 File Offset: 0x000010D0
		public IEdmTemporalTypeReference GetDuration(bool isNullable)
		{
			return new EdmTemporalTypeReference(this.GetCoreModelPrimitiveType(EdmPrimitiveTypeKind.Duration), isNullable);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00002EE0 File Offset: 0x000010E0
		public IEdmTemporalTypeReference GetTimeOfDay(bool isNullable)
		{
			return new EdmTemporalTypeReference(this.GetCoreModelPrimitiveType(EdmPrimitiveTypeKind.TimeOfDay), isNullable);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00002EF0 File Offset: 0x000010F0
		public IEdmDecimalTypeReference GetDecimal(int? precision, int? scale, bool isNullable)
		{
			if (precision != null || scale != null)
			{
				return new EdmDecimalTypeReference(this.GetCoreModelPrimitiveType(EdmPrimitiveTypeKind.Decimal), isNullable, precision, scale);
			}
			return new EdmDecimalTypeReference(this.GetCoreModelPrimitiveType(EdmPrimitiveTypeKind.Decimal), isNullable);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00002F21 File Offset: 0x00001121
		public IEdmDecimalTypeReference GetDecimal(bool isNullable)
		{
			return new EdmDecimalTypeReference(this.GetCoreModelPrimitiveType(EdmPrimitiveTypeKind.Decimal), isNullable);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00002F30 File Offset: 0x00001130
		public IEdmPrimitiveTypeReference GetSingle(bool isNullable)
		{
			return new EdmPrimitiveTypeReference(this.GetCoreModelPrimitiveType(EdmPrimitiveTypeKind.Single), isNullable);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00002F40 File Offset: 0x00001140
		public IEdmPrimitiveTypeReference GetDouble(bool isNullable)
		{
			return new EdmPrimitiveTypeReference(this.GetCoreModelPrimitiveType(EdmPrimitiveTypeKind.Double), isNullable);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00002F4F File Offset: 0x0000114F
		public IEdmPrimitiveTypeReference GetStream(bool isNullable)
		{
			return new EdmPrimitiveTypeReference(this.GetCoreModelPrimitiveType(EdmPrimitiveTypeKind.Stream), isNullable);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00002F5F File Offset: 0x0000115F
		public IEdmTemporalTypeReference GetTemporal(EdmPrimitiveTypeKind kind, int? precision, bool isNullable)
		{
			if (kind == EdmPrimitiveTypeKind.DateTimeOffset || kind == EdmPrimitiveTypeKind.Duration || kind == EdmPrimitiveTypeKind.TimeOfDay)
			{
				return new EdmTemporalTypeReference(this.GetCoreModelPrimitiveType(kind), isNullable, precision);
			}
			throw new InvalidOperationException(Strings.EdmPrimitive_UnexpectedKind);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00002F88 File Offset: 0x00001188
		public IEdmTemporalTypeReference GetTemporal(EdmPrimitiveTypeKind kind, bool isNullable)
		{
			if (kind == EdmPrimitiveTypeKind.DateTimeOffset || kind == EdmPrimitiveTypeKind.Duration || kind == EdmPrimitiveTypeKind.TimeOfDay)
			{
				return new EdmTemporalTypeReference(this.GetCoreModelPrimitiveType(kind), isNullable);
			}
			throw new InvalidOperationException(Strings.EdmPrimitive_UnexpectedKind);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00002FB0 File Offset: 0x000011B0
		public IEdmBinaryTypeReference GetBinary(bool isUnbounded, int? maxLength, bool isNullable)
		{
			return new EdmBinaryTypeReference(this.GetCoreModelPrimitiveType(EdmPrimitiveTypeKind.Binary), isNullable, isUnbounded, maxLength);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00002FC1 File Offset: 0x000011C1
		public IEdmBinaryTypeReference GetBinary(bool isNullable)
		{
			return new EdmBinaryTypeReference(this.GetCoreModelPrimitiveType(EdmPrimitiveTypeKind.Binary), isNullable);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00002FD0 File Offset: 0x000011D0
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

		// Token: 0x06000085 RID: 133 RVA: 0x00003044 File Offset: 0x00001244
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

		// Token: 0x06000086 RID: 134 RVA: 0x000030B4 File Offset: 0x000012B4
		public IEdmStringTypeReference GetString(bool isUnbounded, int? maxLength, bool? isUnicode, bool isNullable)
		{
			return new EdmStringTypeReference(this.GetCoreModelPrimitiveType(EdmPrimitiveTypeKind.String), isNullable, isUnbounded, maxLength, isUnicode);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x000030C8 File Offset: 0x000012C8
		public IEdmStringTypeReference GetString(bool isNullable)
		{
			return new EdmStringTypeReference(this.GetCoreModelPrimitiveType(EdmPrimitiveTypeKind.String), isNullable);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000030D8 File Offset: 0x000012D8
		public IEdmUntypedTypeReference GetUntyped()
		{
			return new EdmUntypedTypeReference(this.GetUntypedType());
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00002CBB File Offset: 0x00000EBB
		public IEnumerable<IEdmVocabularyAnnotation> FindDeclaredVocabularyAnnotations(IEdmVocabularyAnnotatable element)
		{
			return Enumerable.Empty<IEdmVocabularyAnnotation>();
		}

		// Token: 0x0600008A RID: 138 RVA: 0x000030E5 File Offset: 0x000012E5
		public IEnumerable<IEdmStructuredType> FindDirectlyDerivedTypes(IEdmStructuredType baseType)
		{
			return Enumerable.Empty<IEdmStructuredType>();
		}

		// Token: 0x0600008B RID: 139 RVA: 0x000030EC File Offset: 0x000012EC
		private EdmCoreModelPrimitiveType GetCoreModelPrimitiveType(EdmPrimitiveTypeKind kind)
		{
			EdmCoreModelPrimitiveType edmCoreModelPrimitiveType;
			if (!this.primitiveTypesByKind.TryGetValue(kind, out edmCoreModelPrimitiveType))
			{
				return null;
			}
			return edmCoreModelPrimitiveType;
		}

		// Token: 0x04000010 RID: 16
		public static readonly EdmCoreModel Instance = new EdmCoreModel();

		// Token: 0x04000011 RID: 17
		private readonly IDictionary<string, EdmPrimitiveTypeKind> primitiveTypeKinds = new Dictionary<string, EdmPrimitiveTypeKind>();

		// Token: 0x04000012 RID: 18
		private readonly IDictionary<EdmPrimitiveTypeKind, EdmCoreModelPrimitiveType> primitiveTypesByKind = new Dictionary<EdmPrimitiveTypeKind, EdmCoreModelPrimitiveType>();

		// Token: 0x04000013 RID: 19
		private readonly IDictionary<string, EdmPathTypeKind> pathTypeKinds = new Dictionary<string, EdmPathTypeKind>();

		// Token: 0x04000014 RID: 20
		private readonly IDictionary<EdmPathTypeKind, EdmCoreModelPathType> pathTypesByKind = new Dictionary<EdmPathTypeKind, EdmCoreModelPathType>();

		// Token: 0x04000015 RID: 21
		private readonly IEdmDirectValueAnnotationsManager annotationsManager = new EdmDirectValueAnnotationsManager();

		// Token: 0x04000016 RID: 22
		private readonly IList<IEdmSchemaElement> coreSchemaElements = new List<IEdmSchemaElement>();

		// Token: 0x04000017 RID: 23
		private readonly IDictionary<string, IEdmSchemaType> coreSchemaTypes = new Dictionary<string, IEdmSchemaType>();

		// Token: 0x04000018 RID: 24
		private readonly EdmCoreModelComplexType complexType = EdmCoreModelComplexType.Instance;

		// Token: 0x04000019 RID: 25
		private readonly EdmCoreModelEntityType entityType = EdmCoreModelEntityType.Instance;

		// Token: 0x0400001A RID: 26
		private readonly EdmCoreModelUntypedType untypedType = EdmCoreModelUntypedType.Instance;

		// Token: 0x0400001B RID: 27
		private readonly EdmCoreModelPrimitiveType primitiveType = new EdmCoreModelPrimitiveType(EdmPrimitiveTypeKind.PrimitiveType);
	}
}
