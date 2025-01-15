using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000010 RID: 16
	internal abstract class EdmModelVisitor
	{
		// Token: 0x06000050 RID: 80 RVA: 0x000034B7 File Offset: 0x000016B7
		protected EdmModelVisitor(IEdmModel model)
		{
			this.Model = model;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000034C6 File Offset: 0x000016C6
		public void VisitEdmModel()
		{
			this.ProcessModel(this.Model);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000034D4 File Offset: 0x000016D4
		public void VisitSchemaElements(IEnumerable<IEdmSchemaElement> elements)
		{
			EdmModelVisitor.VisitCollection<IEdmSchemaElement>(elements, new Action<IEdmSchemaElement>(this.VisitSchemaElement));
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000034E8 File Offset: 0x000016E8
		public void VisitSchemaElement(IEdmSchemaElement element)
		{
			switch (element.SchemaElementKind)
			{
			case EdmSchemaElementKind.None:
				this.ProcessSchemaElement(element);
				return;
			case EdmSchemaElementKind.TypeDefinition:
				this.VisitSchemaType((IEdmType)element);
				return;
			case EdmSchemaElementKind.Term:
				this.ProcessTerm((IEdmTerm)element);
				return;
			case EdmSchemaElementKind.Action:
				this.ProcessAction((IEdmAction)element);
				return;
			case EdmSchemaElementKind.EntityContainer:
				this.ProcessEntityContainer((IEdmEntityContainer)element);
				return;
			case EdmSchemaElementKind.Function:
				this.ProcessFunction((IEdmFunction)element);
				return;
			default:
				throw new InvalidOperationException(Strings.UnknownEnumVal_SchemaElementKind(element.SchemaElementKind));
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x0000357A File Offset: 0x0000177A
		public void VisitAnnotations(IEnumerable<IEdmDirectValueAnnotation> annotations)
		{
			EdmModelVisitor.VisitCollection<IEdmDirectValueAnnotation>(annotations, new Action<IEdmDirectValueAnnotation>(this.VisitAnnotation));
		}

		// Token: 0x06000055 RID: 85 RVA: 0x0000358E File Offset: 0x0000178E
		public void VisitVocabularyAnnotations(IEnumerable<IEdmVocabularyAnnotation> annotations)
		{
			EdmModelVisitor.VisitCollection<IEdmVocabularyAnnotation>(annotations, new Action<IEdmVocabularyAnnotation>(this.VisitVocabularyAnnotation));
		}

		// Token: 0x06000056 RID: 86 RVA: 0x000035A2 File Offset: 0x000017A2
		public void VisitAnnotation(IEdmDirectValueAnnotation annotation)
		{
			this.ProcessImmediateValueAnnotation(annotation);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x000035AB File Offset: 0x000017AB
		public void VisitVocabularyAnnotation(IEdmVocabularyAnnotation annotation)
		{
			if (annotation.Term != null)
			{
				this.ProcessAnnotation(annotation);
				return;
			}
			this.ProcessVocabularyAnnotation(annotation);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000035C4 File Offset: 0x000017C4
		public void VisitPropertyValueBindings(IEnumerable<IEdmPropertyValueBinding> bindings)
		{
			EdmModelVisitor.VisitCollection<IEdmPropertyValueBinding>(bindings, new Action<IEdmPropertyValueBinding>(this.ProcessPropertyValueBinding));
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000035D9 File Offset: 0x000017D9
		public void VisitExpressions(IEnumerable<IEdmExpression> expressions)
		{
			EdmModelVisitor.VisitCollection<IEdmExpression>(expressions, new Action<IEdmExpression>(this.VisitExpression));
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000035F0 File Offset: 0x000017F0
		public void VisitExpression(IEdmExpression expression)
		{
			switch (expression.ExpressionKind)
			{
			case EdmExpressionKind.None:
				this.ProcessExpression(expression);
				return;
			case EdmExpressionKind.BinaryConstant:
				this.ProcessBinaryConstantExpression((IEdmBinaryConstantExpression)expression);
				return;
			case EdmExpressionKind.BooleanConstant:
				this.ProcessBooleanConstantExpression((IEdmBooleanConstantExpression)expression);
				return;
			case EdmExpressionKind.DateTimeOffsetConstant:
				this.ProcessDateTimeOffsetConstantExpression((IEdmDateTimeOffsetConstantExpression)expression);
				return;
			case EdmExpressionKind.DecimalConstant:
				this.ProcessDecimalConstantExpression((IEdmDecimalConstantExpression)expression);
				return;
			case EdmExpressionKind.FloatingConstant:
				this.ProcessFloatingConstantExpression((IEdmFloatingConstantExpression)expression);
				return;
			case EdmExpressionKind.GuidConstant:
				this.ProcessGuidConstantExpression((IEdmGuidConstantExpression)expression);
				return;
			case EdmExpressionKind.IntegerConstant:
				this.ProcessIntegerConstantExpression((IEdmIntegerConstantExpression)expression);
				return;
			case EdmExpressionKind.StringConstant:
				this.ProcessStringConstantExpression((IEdmStringConstantExpression)expression);
				return;
			case EdmExpressionKind.DurationConstant:
				this.ProcessDurationConstantExpression((IEdmDurationConstantExpression)expression);
				return;
			case EdmExpressionKind.Null:
				this.ProcessNullConstantExpression((IEdmNullExpression)expression);
				return;
			case EdmExpressionKind.Record:
				this.ProcessRecordExpression((IEdmRecordExpression)expression);
				return;
			case EdmExpressionKind.Collection:
				this.ProcessCollectionExpression((IEdmCollectionExpression)expression);
				return;
			case EdmExpressionKind.Path:
				this.ProcessPathExpression((IEdmPathExpression)expression);
				return;
			case EdmExpressionKind.If:
				this.ProcessIfExpression((IEdmIfExpression)expression);
				return;
			case EdmExpressionKind.Cast:
				this.ProcessCastExpression((IEdmCastExpression)expression);
				return;
			case EdmExpressionKind.IsType:
				this.ProcessIsTypeExpression((IEdmIsTypeExpression)expression);
				return;
			case EdmExpressionKind.FunctionApplication:
				this.ProcessFunctionApplicationExpression((IEdmApplyExpression)expression);
				return;
			case EdmExpressionKind.LabeledExpressionReference:
				this.ProcessLabeledExpressionReferenceExpression((IEdmLabeledExpressionReferenceExpression)expression);
				return;
			case EdmExpressionKind.Labeled:
				this.ProcessLabeledExpression((IEdmLabeledExpression)expression);
				return;
			case EdmExpressionKind.PropertyPath:
				this.ProcessPropertyPathExpression((IEdmPathExpression)expression);
				return;
			case EdmExpressionKind.NavigationPropertyPath:
				this.ProcessNavigationPropertyPathExpression((IEdmPathExpression)expression);
				return;
			case EdmExpressionKind.DateConstant:
				this.ProcessDateConstantExpression((IEdmDateConstantExpression)expression);
				return;
			case EdmExpressionKind.TimeOfDayConstant:
				this.ProcessTimeOfDayConstantExpression((IEdmTimeOfDayConstantExpression)expression);
				return;
			case EdmExpressionKind.EnumMember:
				this.ProcessEnumMemberExpression((IEdmEnumMemberExpression)expression);
				return;
			default:
				throw new InvalidOperationException(Strings.UnknownEnumVal_ExpressionKind(expression.ExpressionKind));
			}
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000037C8 File Offset: 0x000019C8
		public void VisitPropertyConstructors(IEnumerable<IEdmPropertyConstructor> constructor)
		{
			EdmModelVisitor.VisitCollection<IEdmPropertyConstructor>(constructor, new Action<IEdmPropertyConstructor>(this.ProcessPropertyConstructor));
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000037E0 File Offset: 0x000019E0
		public virtual void VisitEntityContainerElements(IEnumerable<IEdmEntityContainerElement> elements)
		{
			foreach (IEdmEntityContainerElement edmEntityContainerElement in elements)
			{
				switch (edmEntityContainerElement.ContainerElementKind)
				{
				case EdmContainerElementKind.None:
					this.ProcessEntityContainerElement(edmEntityContainerElement);
					break;
				case EdmContainerElementKind.EntitySet:
					this.ProcessEntitySet((IEdmEntitySet)edmEntityContainerElement);
					break;
				case EdmContainerElementKind.ActionImport:
					this.ProcessActionImport((IEdmActionImport)edmEntityContainerElement);
					break;
				case EdmContainerElementKind.FunctionImport:
					this.ProcessFunctionImport((IEdmFunctionImport)edmEntityContainerElement);
					break;
				case EdmContainerElementKind.Singleton:
					this.ProcessSingleton((IEdmSingleton)edmEntityContainerElement);
					break;
				default:
					throw new InvalidOperationException(Strings.UnknownEnumVal_ContainerElementKind(edmEntityContainerElement.ContainerElementKind.ToString()));
				}
			}
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000038AC File Offset: 0x00001AAC
		public void VisitTypeReference(IEdmTypeReference reference)
		{
			switch (reference.TypeKind())
			{
			case EdmTypeKind.None:
				this.ProcessTypeReference(reference);
				return;
			case EdmTypeKind.Primitive:
				this.VisitPrimitiveTypeReference(reference.AsPrimitive());
				return;
			case EdmTypeKind.Entity:
				this.ProcessEntityTypeReference(reference.AsEntity());
				return;
			case EdmTypeKind.Complex:
				this.ProcessComplexTypeReference(reference.AsComplex());
				return;
			case EdmTypeKind.Collection:
				this.ProcessCollectionTypeReference(reference.AsCollection());
				return;
			case EdmTypeKind.EntityReference:
				this.ProcessEntityReferenceTypeReference(reference.AsEntityReference());
				return;
			case EdmTypeKind.Enum:
				this.ProcessEnumTypeReference(reference.AsEnum());
				return;
			case EdmTypeKind.TypeDefinition:
				this.ProcessTypeDefinitionReference(reference.AsTypeDefinition());
				return;
			default:
				throw new InvalidOperationException(Strings.UnknownEnumVal_TypeKind(reference.TypeKind().ToString()));
			}
		}

		// Token: 0x0600005E RID: 94 RVA: 0x0000396C File Offset: 0x00001B6C
		public void VisitPrimitiveTypeReference(IEdmPrimitiveTypeReference reference)
		{
			switch (reference.PrimitiveKind())
			{
			case EdmPrimitiveTypeKind.None:
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
			case EdmPrimitiveTypeKind.Date:
				this.ProcessPrimitiveTypeReference(reference);
				return;
			case EdmPrimitiveTypeKind.Binary:
				this.ProcessBinaryTypeReference(reference.AsBinary());
				return;
			case EdmPrimitiveTypeKind.DateTimeOffset:
			case EdmPrimitiveTypeKind.Duration:
			case EdmPrimitiveTypeKind.TimeOfDay:
				this.ProcessTemporalTypeReference(reference.AsTemporal());
				return;
			case EdmPrimitiveTypeKind.Decimal:
				this.ProcessDecimalTypeReference(reference.AsDecimal());
				return;
			case EdmPrimitiveTypeKind.String:
				this.ProcessStringTypeReference(reference.AsString());
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
				this.ProcessSpatialTypeReference(reference.AsSpatial());
				return;
			default:
				throw new InvalidOperationException(Strings.UnknownEnumVal_PrimitiveKind(reference.PrimitiveKind().ToString()));
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003A78 File Offset: 0x00001C78
		public void VisitSchemaType(IEdmType definition)
		{
			switch (definition.TypeKind)
			{
			case EdmTypeKind.None:
				this.VisitSchemaType(definition);
				return;
			case EdmTypeKind.Entity:
				this.ProcessEntityType((IEdmEntityType)definition);
				return;
			case EdmTypeKind.Complex:
				this.ProcessComplexType((IEdmComplexType)definition);
				return;
			case EdmTypeKind.Enum:
				this.ProcessEnumType((IEdmEnumType)definition);
				return;
			case EdmTypeKind.TypeDefinition:
				this.ProcessTypeDefinition((IEdmTypeDefinition)definition);
				return;
			}
			throw new InvalidOperationException(Strings.UnknownEnumVal_TypeKind(definition.TypeKind));
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003B05 File Offset: 0x00001D05
		public void VisitProperties(IEnumerable<IEdmProperty> properties)
		{
			EdmModelVisitor.VisitCollection<IEdmProperty>(properties, new Action<IEdmProperty>(this.VisitProperty));
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003B1C File Offset: 0x00001D1C
		public void VisitProperty(IEdmProperty property)
		{
			switch (property.PropertyKind)
			{
			case EdmPropertyKind.None:
				this.ProcessProperty(property);
				return;
			case EdmPropertyKind.Structural:
				this.ProcessStructuralProperty((IEdmStructuralProperty)property);
				return;
			case EdmPropertyKind.Navigation:
				this.ProcessNavigationProperty((IEdmNavigationProperty)property);
				return;
			default:
				throw new InvalidOperationException(Strings.UnknownEnumVal_PropertyKind(property.PropertyKind.ToString()));
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003B84 File Offset: 0x00001D84
		public void VisitEnumMembers(IEnumerable<IEdmEnumMember> enumMembers)
		{
			EdmModelVisitor.VisitCollection<IEdmEnumMember>(enumMembers, new Action<IEdmEnumMember>(this.VisitEnumMember));
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003B98 File Offset: 0x00001D98
		public void VisitEnumMember(IEdmEnumMember enumMember)
		{
			this.ProcessEnumMember(enumMember);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003BA1 File Offset: 0x00001DA1
		public void VisitOperationParameters(IEnumerable<IEdmOperationParameter> parameters)
		{
			EdmModelVisitor.VisitCollection<IEdmOperationParameter>(parameters, new Action<IEdmOperationParameter>(this.ProcessOperationParameter));
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003BB8 File Offset: 0x00001DB8
		protected static void VisitCollection<T>(IEnumerable<T> collection, Action<T> visitMethod)
		{
			foreach (T t in collection)
			{
				visitMethod.Invoke(t);
			}
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003C00 File Offset: 0x00001E00
		protected virtual void ProcessModel(IEdmModel model)
		{
			this.ProcessElement(model);
			this.VisitSchemaElements(model.SchemaElements);
			this.VisitVocabularyAnnotations(model.VocabularyAnnotations);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003C21 File Offset: 0x00001E21
		protected virtual void ProcessElement(IEdmElement element)
		{
			this.VisitAnnotations(this.Model.DirectValueAnnotations(element));
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003C35 File Offset: 0x00001E35
		protected virtual void ProcessNamedElement(IEdmNamedElement element)
		{
			this.ProcessElement(element);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003C3E File Offset: 0x00001E3E
		protected virtual void ProcessSchemaElement(IEdmSchemaElement element)
		{
			this.ProcessVocabularyAnnotatable(element);
			this.ProcessNamedElement(element);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003C4E File Offset: 0x00001E4E
		protected virtual void ProcessVocabularyAnnotatable(IEdmVocabularyAnnotatable annotatable)
		{
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003C50 File Offset: 0x00001E50
		protected virtual void ProcessComplexTypeReference(IEdmComplexTypeReference reference)
		{
			this.ProcessStructuredTypeReference(reference);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003C50 File Offset: 0x00001E50
		protected virtual void ProcessEntityTypeReference(IEdmEntityTypeReference reference)
		{
			this.ProcessStructuredTypeReference(reference);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003C59 File Offset: 0x00001E59
		protected virtual void ProcessEntityReferenceTypeReference(IEdmEntityReferenceTypeReference reference)
		{
			this.ProcessTypeReference(reference);
			this.ProcessEntityReferenceType(reference.EntityReferenceDefinition());
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003C6E File Offset: 0x00001E6E
		protected virtual void ProcessCollectionTypeReference(IEdmCollectionTypeReference reference)
		{
			this.ProcessTypeReference(reference);
			this.ProcessCollectionType(reference.CollectionDefinition());
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003C83 File Offset: 0x00001E83
		protected virtual void ProcessEnumTypeReference(IEdmEnumTypeReference reference)
		{
			this.ProcessTypeReference(reference);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003C83 File Offset: 0x00001E83
		protected virtual void ProcessTypeDefinitionReference(IEdmTypeDefinitionReference reference)
		{
			this.ProcessTypeReference(reference);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003C8C File Offset: 0x00001E8C
		protected virtual void ProcessBinaryTypeReference(IEdmBinaryTypeReference reference)
		{
			this.ProcessPrimitiveTypeReference(reference);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003C8C File Offset: 0x00001E8C
		protected virtual void ProcessDecimalTypeReference(IEdmDecimalTypeReference reference)
		{
			this.ProcessPrimitiveTypeReference(reference);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003C8C File Offset: 0x00001E8C
		protected virtual void ProcessSpatialTypeReference(IEdmSpatialTypeReference reference)
		{
			this.ProcessPrimitiveTypeReference(reference);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003C8C File Offset: 0x00001E8C
		protected virtual void ProcessStringTypeReference(IEdmStringTypeReference reference)
		{
			this.ProcessPrimitiveTypeReference(reference);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003C8C File Offset: 0x00001E8C
		protected virtual void ProcessTemporalTypeReference(IEdmTemporalTypeReference reference)
		{
			this.ProcessPrimitiveTypeReference(reference);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003C83 File Offset: 0x00001E83
		protected virtual void ProcessPrimitiveTypeReference(IEdmPrimitiveTypeReference reference)
		{
			this.ProcessTypeReference(reference);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003C83 File Offset: 0x00001E83
		protected virtual void ProcessStructuredTypeReference(IEdmStructuredTypeReference reference)
		{
			this.ProcessTypeReference(reference);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003C35 File Offset: 0x00001E35
		protected virtual void ProcessTypeReference(IEdmTypeReference element)
		{
			this.ProcessElement(element);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003C95 File Offset: 0x00001E95
		protected virtual void ProcessTerm(IEdmTerm term)
		{
			this.ProcessSchemaElement(term);
			this.VisitTypeReference(term.Type);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003CAA File Offset: 0x00001EAA
		protected virtual void ProcessComplexType(IEdmComplexType definition)
		{
			this.ProcessSchemaElement(definition);
			this.ProcessStructuredType(definition);
			this.ProcessSchemaType(definition);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003CAA File Offset: 0x00001EAA
		protected virtual void ProcessEntityType(IEdmEntityType definition)
		{
			this.ProcessSchemaElement(definition);
			this.ProcessStructuredType(definition);
			this.ProcessSchemaType(definition);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003CC1 File Offset: 0x00001EC1
		protected virtual void ProcessCollectionType(IEdmCollectionType definition)
		{
			this.ProcessElement(definition);
			this.ProcessType(definition);
			this.VisitTypeReference(definition.ElementType);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003CDD File Offset: 0x00001EDD
		protected virtual void ProcessEnumType(IEdmEnumType definition)
		{
			this.ProcessSchemaElement(definition);
			this.ProcessType(definition);
			this.ProcessSchemaType(definition);
			this.VisitEnumMembers(definition.Members);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003D00 File Offset: 0x00001F00
		protected virtual void ProcessTypeDefinition(IEdmTypeDefinition definition)
		{
			this.ProcessSchemaElement(definition);
			this.ProcessType(definition);
			this.ProcessSchemaType(definition);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003D17 File Offset: 0x00001F17
		protected virtual void ProcessEntityReferenceType(IEdmEntityReferenceType definition)
		{
			this.ProcessElement(definition);
			this.ProcessType(definition);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003D27 File Offset: 0x00001F27
		protected virtual void ProcessStructuredType(IEdmStructuredType definition)
		{
			this.ProcessType(definition);
			this.VisitProperties(definition.DeclaredProperties);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003C4E File Offset: 0x00001E4E
		protected virtual void ProcessSchemaType(IEdmSchemaType type)
		{
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003C4E File Offset: 0x00001E4E
		protected virtual void ProcessType(IEdmType definition)
		{
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003D3C File Offset: 0x00001F3C
		protected virtual void ProcessNavigationProperty(IEdmNavigationProperty property)
		{
			this.ProcessProperty(property);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003D3C File Offset: 0x00001F3C
		protected virtual void ProcessStructuralProperty(IEdmStructuralProperty property)
		{
			this.ProcessProperty(property);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003D45 File Offset: 0x00001F45
		protected virtual void ProcessProperty(IEdmProperty property)
		{
			this.ProcessVocabularyAnnotatable(property);
			this.ProcessNamedElement(property);
			this.VisitTypeReference(property.Type);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003D61 File Offset: 0x00001F61
		protected virtual void ProcessEnumMember(IEdmEnumMember enumMember)
		{
			this.ProcessNamedElement(enumMember);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003C35 File Offset: 0x00001E35
		protected virtual void ProcessVocabularyAnnotation(IEdmVocabularyAnnotation annotation)
		{
			this.ProcessElement(annotation);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003D61 File Offset: 0x00001F61
		protected virtual void ProcessImmediateValueAnnotation(IEdmDirectValueAnnotation annotation)
		{
			this.ProcessNamedElement(annotation);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003D6A File Offset: 0x00001F6A
		protected virtual void ProcessAnnotation(IEdmVocabularyAnnotation annotation)
		{
			this.ProcessVocabularyAnnotation(annotation);
			this.VisitExpression(annotation.Value);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003D7F File Offset: 0x00001F7F
		protected virtual void ProcessPropertyValueBinding(IEdmPropertyValueBinding binding)
		{
			this.VisitExpression(binding.Value);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003C4E File Offset: 0x00001E4E
		protected virtual void ProcessExpression(IEdmExpression expression)
		{
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003D8D File Offset: 0x00001F8D
		protected virtual void ProcessStringConstantExpression(IEdmStringConstantExpression expression)
		{
			this.ProcessExpression(expression);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003D8D File Offset: 0x00001F8D
		protected virtual void ProcessBinaryConstantExpression(IEdmBinaryConstantExpression expression)
		{
			this.ProcessExpression(expression);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003D96 File Offset: 0x00001F96
		protected virtual void ProcessRecordExpression(IEdmRecordExpression expression)
		{
			this.ProcessExpression(expression);
			if (expression.DeclaredType != null)
			{
				this.VisitTypeReference(expression.DeclaredType);
			}
			this.VisitPropertyConstructors(expression.Properties);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003D8D File Offset: 0x00001F8D
		protected virtual void ProcessPathExpression(IEdmPathExpression expression)
		{
			this.ProcessExpression(expression);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003D8D File Offset: 0x00001F8D
		protected virtual void ProcessPropertyPathExpression(IEdmPathExpression expression)
		{
			this.ProcessExpression(expression);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003D8D File Offset: 0x00001F8D
		protected virtual void ProcessNavigationPropertyPathExpression(IEdmPathExpression expression)
		{
			this.ProcessExpression(expression);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003DBF File Offset: 0x00001FBF
		protected virtual void ProcessCollectionExpression(IEdmCollectionExpression expression)
		{
			this.ProcessExpression(expression);
			this.VisitExpressions(expression.Elements);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003D8D File Offset: 0x00001F8D
		protected virtual void ProcessLabeledExpressionReferenceExpression(IEdmLabeledExpressionReferenceExpression expression)
		{
			this.ProcessExpression(expression);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003DD4 File Offset: 0x00001FD4
		protected virtual void ProcessIsTypeExpression(IEdmIsTypeExpression expression)
		{
			this.ProcessExpression(expression);
			this.VisitTypeReference(expression.Type);
			this.VisitExpression(expression.Operand);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003D8D File Offset: 0x00001F8D
		protected virtual void ProcessIntegerConstantExpression(IEdmIntegerConstantExpression expression)
		{
			this.ProcessExpression(expression);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003DF5 File Offset: 0x00001FF5
		protected virtual void ProcessIfExpression(IEdmIfExpression expression)
		{
			this.ProcessExpression(expression);
			this.VisitExpression(expression.TestExpression);
			this.VisitExpression(expression.TrueExpression);
			this.VisitExpression(expression.FalseExpression);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003E22 File Offset: 0x00002022
		protected virtual void ProcessFunctionApplicationExpression(IEdmApplyExpression expression)
		{
			this.ProcessExpression(expression);
			this.VisitExpressions(expression.Arguments);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003D8D File Offset: 0x00001F8D
		protected virtual void ProcessFloatingConstantExpression(IEdmFloatingConstantExpression expression)
		{
			this.ProcessExpression(expression);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003D8D File Offset: 0x00001F8D
		protected virtual void ProcessGuidConstantExpression(IEdmGuidConstantExpression expression)
		{
			this.ProcessExpression(expression);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003D8D File Offset: 0x00001F8D
		protected virtual void ProcessEnumMemberExpression(IEdmEnumMemberExpression expression)
		{
			this.ProcessExpression(expression);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003D8D File Offset: 0x00001F8D
		protected virtual void ProcessDecimalConstantExpression(IEdmDecimalConstantExpression expression)
		{
			this.ProcessExpression(expression);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003D8D File Offset: 0x00001F8D
		protected virtual void ProcessDateConstantExpression(IEdmDateConstantExpression expression)
		{
			this.ProcessExpression(expression);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003D8D File Offset: 0x00001F8D
		protected virtual void ProcessTimeOfDayConstantExpression(IEdmTimeOfDayConstantExpression expression)
		{
			this.ProcessExpression(expression);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003D8D File Offset: 0x00001F8D
		protected virtual void ProcessDateTimeOffsetConstantExpression(IEdmDateTimeOffsetConstantExpression expression)
		{
			this.ProcessExpression(expression);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003D8D File Offset: 0x00001F8D
		protected virtual void ProcessDurationConstantExpression(IEdmDurationConstantExpression expression)
		{
			this.ProcessExpression(expression);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003D8D File Offset: 0x00001F8D
		protected virtual void ProcessBooleanConstantExpression(IEdmBooleanConstantExpression expression)
		{
			this.ProcessExpression(expression);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00003E37 File Offset: 0x00002037
		protected virtual void ProcessCastExpression(IEdmCastExpression expression)
		{
			this.ProcessExpression(expression);
			this.VisitTypeReference(expression.Type);
			this.VisitExpression(expression.Operand);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00003E58 File Offset: 0x00002058
		protected virtual void ProcessLabeledExpression(IEdmLabeledExpression element)
		{
			this.VisitExpression(element.Expression);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003E66 File Offset: 0x00002066
		protected virtual void ProcessPropertyConstructor(IEdmPropertyConstructor constructor)
		{
			this.VisitExpression(constructor.Value);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003D8D File Offset: 0x00001F8D
		protected virtual void ProcessNullConstantExpression(IEdmNullExpression expression)
		{
			this.ProcessExpression(expression);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003E74 File Offset: 0x00002074
		protected virtual void ProcessEntityContainer(IEdmEntityContainer container)
		{
			this.ProcessVocabularyAnnotatable(container);
			this.ProcessNamedElement(container);
			this.VisitEntityContainerElements(container.Elements);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003D61 File Offset: 0x00001F61
		protected virtual void ProcessEntityContainerElement(IEdmEntityContainerElement element)
		{
			this.ProcessNamedElement(element);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00003E90 File Offset: 0x00002090
		protected virtual void ProcessEntitySet(IEdmEntitySet set)
		{
			this.ProcessEntityContainerElement(set);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003E90 File Offset: 0x00002090
		protected virtual void ProcessSingleton(IEdmSingleton singleton)
		{
			this.ProcessEntityContainerElement(singleton);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00003E99 File Offset: 0x00002099
		protected virtual void ProcessAction(IEdmAction action)
		{
			this.ProcessSchemaElement(action);
			this.ProcessOperation(action);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00003E99 File Offset: 0x00002099
		protected virtual void ProcessFunction(IEdmFunction function)
		{
			this.ProcessSchemaElement(function);
			this.ProcessOperation(function);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00003E90 File Offset: 0x00002090
		protected virtual void ProcessActionImport(IEdmActionImport actionImport)
		{
			this.ProcessEntityContainerElement(actionImport);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00003E90 File Offset: 0x00002090
		protected virtual void ProcessFunctionImport(IEdmFunctionImport functionImport)
		{
			this.ProcessEntityContainerElement(functionImport);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00003EA9 File Offset: 0x000020A9
		protected virtual void ProcessOperation(IEdmOperation operation)
		{
			if (operation.ReturnType != null)
			{
				this.VisitTypeReference(operation.ReturnType);
			}
			this.VisitOperationParameters(operation.Parameters);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00003ECB File Offset: 0x000020CB
		protected virtual void ProcessOperationParameter(IEdmOperationParameter parameter)
		{
			this.ProcessVocabularyAnnotatable(parameter);
			this.ProcessNamedElement(parameter);
			this.VisitTypeReference(parameter.Type);
		}

		// Token: 0x04000019 RID: 25
		protected readonly IEdmModel Model;
	}
}
