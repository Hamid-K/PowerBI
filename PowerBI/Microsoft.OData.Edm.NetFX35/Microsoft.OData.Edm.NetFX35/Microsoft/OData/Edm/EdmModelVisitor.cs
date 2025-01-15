using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Annotations;
using Microsoft.OData.Edm.Expressions;

namespace Microsoft.OData.Edm
{
	// Token: 0x020000D6 RID: 214
	internal abstract class EdmModelVisitor
	{
		// Token: 0x060003CE RID: 974 RVA: 0x000098AF File Offset: 0x00007AAF
		protected EdmModelVisitor(IEdmModel model)
		{
			this.Model = model;
		}

		// Token: 0x060003CF RID: 975 RVA: 0x000098BE File Offset: 0x00007ABE
		public void VisitEdmModel()
		{
			this.ProcessModel(this.Model);
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x000098CC File Offset: 0x00007ACC
		public void VisitSchemaElements(IEnumerable<IEdmSchemaElement> elements)
		{
			EdmModelVisitor.VisitCollection<IEdmSchemaElement>(elements, new Action<IEdmSchemaElement>(this.VisitSchemaElement));
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x000098E0 File Offset: 0x00007AE0
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
			case EdmSchemaElementKind.ValueTerm:
				this.ProcessValueTerm((IEdmValueTerm)element);
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

		// Token: 0x060003D2 RID: 978 RVA: 0x00009972 File Offset: 0x00007B72
		public void VisitAnnotations(IEnumerable<IEdmDirectValueAnnotation> annotations)
		{
			EdmModelVisitor.VisitCollection<IEdmDirectValueAnnotation>(annotations, new Action<IEdmDirectValueAnnotation>(this.VisitAnnotation));
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x00009986 File Offset: 0x00007B86
		public void VisitVocabularyAnnotations(IEnumerable<IEdmVocabularyAnnotation> annotations)
		{
			EdmModelVisitor.VisitCollection<IEdmVocabularyAnnotation>(annotations, new Action<IEdmVocabularyAnnotation>(this.VisitVocabularyAnnotation));
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0000999A File Offset: 0x00007B9A
		public void VisitAnnotation(IEdmDirectValueAnnotation annotation)
		{
			this.ProcessImmediateValueAnnotation(annotation);
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x000099A4 File Offset: 0x00007BA4
		public void VisitVocabularyAnnotation(IEdmVocabularyAnnotation annotation)
		{
			if (annotation.Term == null)
			{
				this.ProcessVocabularyAnnotation(annotation);
				return;
			}
			switch (annotation.Term.TermKind)
			{
			case EdmTermKind.None:
				this.ProcessVocabularyAnnotation(annotation);
				return;
			case EdmTermKind.Type:
			case EdmTermKind.Value:
				this.ProcessAnnotation((IEdmValueAnnotation)annotation);
				return;
			default:
				throw new InvalidOperationException(Strings.UnknownEnumVal_TermKind(annotation.Term.TermKind));
			}
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x00009A10 File Offset: 0x00007C10
		public void VisitPropertyValueBindings(IEnumerable<IEdmPropertyValueBinding> bindings)
		{
			EdmModelVisitor.VisitCollection<IEdmPropertyValueBinding>(bindings, new Action<IEdmPropertyValueBinding>(this.ProcessPropertyValueBinding));
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x00009A25 File Offset: 0x00007C25
		public void VisitExpressions(IEnumerable<IEdmExpression> expressions)
		{
			EdmModelVisitor.VisitCollection<IEdmExpression>(expressions, new Action<IEdmExpression>(this.VisitExpression));
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x00009A3C File Offset: 0x00007C3C
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
			case EdmExpressionKind.ParameterReference:
				this.ProcessParameterReferenceExpression((IEdmParameterReferenceExpression)expression);
				return;
			case EdmExpressionKind.OperationReference:
				this.ProcessOperationReferenceExpression((IEdmOperationReferenceExpression)expression);
				return;
			case EdmExpressionKind.PropertyReference:
				this.ProcessPropertyReferenceExpression((IEdmPropertyReferenceExpression)expression);
				return;
			case EdmExpressionKind.ValueTermReference:
				this.ProcessPropertyReferenceExpression((IEdmPropertyReferenceExpression)expression);
				return;
			case EdmExpressionKind.EntitySetReference:
				this.ProcessEntitySetReferenceExpression((IEdmEntitySetReferenceExpression)expression);
				return;
			case EdmExpressionKind.EnumMemberReference:
				this.ProcessEnumMemberReferenceExpression((IEdmEnumMemberReferenceExpression)expression);
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
			case EdmExpressionKind.OperationApplication:
				this.ProcessOperationApplicationExpression((IEdmApplyExpression)expression);
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

		// Token: 0x060003D9 RID: 985 RVA: 0x00009C7A File Offset: 0x00007E7A
		public void VisitPropertyConstructors(IEnumerable<IEdmPropertyConstructor> constructor)
		{
			EdmModelVisitor.VisitCollection<IEdmPropertyConstructor>(constructor, new Action<IEdmPropertyConstructor>(this.ProcessPropertyConstructor));
		}

		// Token: 0x060003DA RID: 986 RVA: 0x00009C90 File Offset: 0x00007E90
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

		// Token: 0x060003DB RID: 987 RVA: 0x00009D58 File Offset: 0x00007F58
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

		// Token: 0x060003DC RID: 988 RVA: 0x00009E14 File Offset: 0x00008014
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

		// Token: 0x060003DD RID: 989 RVA: 0x00009F1C File Offset: 0x0000811C
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

		// Token: 0x060003DE RID: 990 RVA: 0x00009FA9 File Offset: 0x000081A9
		public void VisitProperties(IEnumerable<IEdmProperty> properties)
		{
			EdmModelVisitor.VisitCollection<IEdmProperty>(properties, new Action<IEdmProperty>(this.VisitProperty));
		}

		// Token: 0x060003DF RID: 991 RVA: 0x00009FC0 File Offset: 0x000081C0
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

		// Token: 0x060003E0 RID: 992 RVA: 0x0000A024 File Offset: 0x00008224
		public void VisitEnumMembers(IEnumerable<IEdmEnumMember> enumMembers)
		{
			EdmModelVisitor.VisitCollection<IEdmEnumMember>(enumMembers, new Action<IEdmEnumMember>(this.VisitEnumMember));
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x0000A038 File Offset: 0x00008238
		public void VisitEnumMember(IEdmEnumMember enumMember)
		{
			this.ProcessEnumMember(enumMember);
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x0000A041 File Offset: 0x00008241
		public void VisitOperationParameters(IEnumerable<IEdmOperationParameter> parameters)
		{
			EdmModelVisitor.VisitCollection<IEdmOperationParameter>(parameters, new Action<IEdmOperationParameter>(this.ProcessOperationParameter));
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x0000A058 File Offset: 0x00008258
		protected static void VisitCollection<T>(IEnumerable<T> collection, Action<T> visitMethod)
		{
			foreach (T t in collection)
			{
				visitMethod.Invoke(t);
			}
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x0000A0A0 File Offset: 0x000082A0
		protected virtual void ProcessModel(IEdmModel model)
		{
			this.ProcessElement(model);
			this.VisitSchemaElements(model.SchemaElements);
			this.VisitVocabularyAnnotations(model.VocabularyAnnotations);
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x0000A0C1 File Offset: 0x000082C1
		protected virtual void ProcessElement(IEdmElement element)
		{
			this.VisitAnnotations(this.Model.DirectValueAnnotations(element));
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x0000A0D5 File Offset: 0x000082D5
		protected virtual void ProcessNamedElement(IEdmNamedElement element)
		{
			this.ProcessElement(element);
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x0000A0DE File Offset: 0x000082DE
		protected virtual void ProcessSchemaElement(IEdmSchemaElement element)
		{
			this.ProcessVocabularyAnnotatable(element);
			this.ProcessNamedElement(element);
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x0000A0EE File Offset: 0x000082EE
		protected virtual void ProcessVocabularyAnnotatable(IEdmVocabularyAnnotatable annotatable)
		{
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x0000A0F0 File Offset: 0x000082F0
		protected virtual void ProcessComplexTypeReference(IEdmComplexTypeReference reference)
		{
			this.ProcessStructuredTypeReference(reference);
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x0000A0F9 File Offset: 0x000082F9
		protected virtual void ProcessEntityTypeReference(IEdmEntityTypeReference reference)
		{
			this.ProcessStructuredTypeReference(reference);
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x0000A102 File Offset: 0x00008302
		protected virtual void ProcessEntityReferenceTypeReference(IEdmEntityReferenceTypeReference reference)
		{
			this.ProcessTypeReference(reference);
			this.ProcessEntityReferenceType(reference.EntityReferenceDefinition());
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x0000A117 File Offset: 0x00008317
		protected virtual void ProcessCollectionTypeReference(IEdmCollectionTypeReference reference)
		{
			this.ProcessTypeReference(reference);
			this.ProcessCollectionType(reference.CollectionDefinition());
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x0000A12C File Offset: 0x0000832C
		protected virtual void ProcessEnumTypeReference(IEdmEnumTypeReference reference)
		{
			this.ProcessTypeReference(reference);
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x0000A135 File Offset: 0x00008335
		protected virtual void ProcessTypeDefinitionReference(IEdmTypeDefinitionReference reference)
		{
			this.ProcessTypeReference(reference);
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x0000A13E File Offset: 0x0000833E
		protected virtual void ProcessBinaryTypeReference(IEdmBinaryTypeReference reference)
		{
			this.ProcessPrimitiveTypeReference(reference);
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x0000A147 File Offset: 0x00008347
		protected virtual void ProcessDecimalTypeReference(IEdmDecimalTypeReference reference)
		{
			this.ProcessPrimitiveTypeReference(reference);
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x0000A150 File Offset: 0x00008350
		protected virtual void ProcessSpatialTypeReference(IEdmSpatialTypeReference reference)
		{
			this.ProcessPrimitiveTypeReference(reference);
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x0000A159 File Offset: 0x00008359
		protected virtual void ProcessStringTypeReference(IEdmStringTypeReference reference)
		{
			this.ProcessPrimitiveTypeReference(reference);
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x0000A162 File Offset: 0x00008362
		protected virtual void ProcessTemporalTypeReference(IEdmTemporalTypeReference reference)
		{
			this.ProcessPrimitiveTypeReference(reference);
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x0000A16B File Offset: 0x0000836B
		protected virtual void ProcessPrimitiveTypeReference(IEdmPrimitiveTypeReference reference)
		{
			this.ProcessTypeReference(reference);
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x0000A174 File Offset: 0x00008374
		protected virtual void ProcessStructuredTypeReference(IEdmStructuredTypeReference reference)
		{
			this.ProcessTypeReference(reference);
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0000A17D File Offset: 0x0000837D
		protected virtual void ProcessTypeReference(IEdmTypeReference element)
		{
			this.ProcessElement(element);
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x0000A186 File Offset: 0x00008386
		protected virtual void ProcessTerm(IEdmTerm term)
		{
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x0000A188 File Offset: 0x00008388
		protected virtual void ProcessValueTerm(IEdmValueTerm term)
		{
			this.ProcessSchemaElement(term);
			this.ProcessTerm(term);
			this.VisitTypeReference(term.Type);
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x0000A1A4 File Offset: 0x000083A4
		protected virtual void ProcessComplexType(IEdmComplexType definition)
		{
			this.ProcessSchemaElement(definition);
			this.ProcessStructuredType(definition);
			this.ProcessSchemaType(definition);
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x0000A1BB File Offset: 0x000083BB
		protected virtual void ProcessEntityType(IEdmEntityType definition)
		{
			this.ProcessSchemaElement(definition);
			this.ProcessTerm(definition);
			this.ProcessStructuredType(definition);
			this.ProcessSchemaType(definition);
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x0000A1D9 File Offset: 0x000083D9
		protected virtual void ProcessCollectionType(IEdmCollectionType definition)
		{
			this.ProcessElement(definition);
			this.ProcessType(definition);
			this.VisitTypeReference(definition.ElementType);
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x0000A1F5 File Offset: 0x000083F5
		protected virtual void ProcessEnumType(IEdmEnumType definition)
		{
			this.ProcessSchemaElement(definition);
			this.ProcessType(definition);
			this.ProcessSchemaType(definition);
			this.VisitEnumMembers(definition.Members);
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0000A218 File Offset: 0x00008418
		protected virtual void ProcessTypeDefinition(IEdmTypeDefinition definition)
		{
			this.ProcessSchemaElement(definition);
			this.ProcessType(definition);
			this.ProcessSchemaType(definition);
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0000A22F File Offset: 0x0000842F
		protected virtual void ProcessEntityReferenceType(IEdmEntityReferenceType definition)
		{
			this.ProcessElement(definition);
			this.ProcessType(definition);
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0000A23F File Offset: 0x0000843F
		protected virtual void ProcessStructuredType(IEdmStructuredType definition)
		{
			this.ProcessType(definition);
			this.VisitProperties(definition.DeclaredProperties);
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x0000A254 File Offset: 0x00008454
		protected virtual void ProcessSchemaType(IEdmSchemaType type)
		{
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0000A256 File Offset: 0x00008456
		protected virtual void ProcessType(IEdmType definition)
		{
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0000A258 File Offset: 0x00008458
		protected virtual void ProcessNavigationProperty(IEdmNavigationProperty property)
		{
			this.ProcessProperty(property);
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0000A261 File Offset: 0x00008461
		protected virtual void ProcessStructuralProperty(IEdmStructuralProperty property)
		{
			this.ProcessProperty(property);
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x0000A26A File Offset: 0x0000846A
		protected virtual void ProcessProperty(IEdmProperty property)
		{
			this.ProcessVocabularyAnnotatable(property);
			this.ProcessNamedElement(property);
			this.VisitTypeReference(property.Type);
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0000A286 File Offset: 0x00008486
		protected virtual void ProcessEnumMember(IEdmEnumMember enumMember)
		{
			this.ProcessNamedElement(enumMember);
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0000A28F File Offset: 0x0000848F
		protected virtual void ProcessVocabularyAnnotation(IEdmVocabularyAnnotation annotation)
		{
			this.ProcessElement(annotation);
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0000A298 File Offset: 0x00008498
		protected virtual void ProcessImmediateValueAnnotation(IEdmDirectValueAnnotation annotation)
		{
			this.ProcessNamedElement(annotation);
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0000A2A1 File Offset: 0x000084A1
		protected virtual void ProcessAnnotation(IEdmValueAnnotation annotation)
		{
			this.ProcessVocabularyAnnotation(annotation);
			this.VisitExpression(annotation.Value);
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0000A2B6 File Offset: 0x000084B6
		protected virtual void ProcessPropertyValueBinding(IEdmPropertyValueBinding binding)
		{
			this.VisitExpression(binding.Value);
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0000A2C4 File Offset: 0x000084C4
		protected virtual void ProcessExpression(IEdmExpression expression)
		{
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x0000A2C6 File Offset: 0x000084C6
		protected virtual void ProcessStringConstantExpression(IEdmStringConstantExpression expression)
		{
			this.ProcessExpression(expression);
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x0000A2CF File Offset: 0x000084CF
		protected virtual void ProcessBinaryConstantExpression(IEdmBinaryConstantExpression expression)
		{
			this.ProcessExpression(expression);
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0000A2D8 File Offset: 0x000084D8
		protected virtual void ProcessRecordExpression(IEdmRecordExpression expression)
		{
			this.ProcessExpression(expression);
			if (expression.DeclaredType != null)
			{
				this.VisitTypeReference(expression.DeclaredType);
			}
			this.VisitPropertyConstructors(expression.Properties);
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x0000A301 File Offset: 0x00008501
		protected virtual void ProcessPropertyReferenceExpression(IEdmPropertyReferenceExpression expression)
		{
			this.ProcessExpression(expression);
			if (expression.Base != null)
			{
				this.VisitExpression(expression.Base);
			}
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0000A31E File Offset: 0x0000851E
		protected virtual void ProcessPathExpression(IEdmPathExpression expression)
		{
			this.ProcessExpression(expression);
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0000A327 File Offset: 0x00008527
		protected virtual void ProcessPropertyPathExpression(IEdmPathExpression expression)
		{
			this.ProcessExpression(expression);
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0000A330 File Offset: 0x00008530
		protected virtual void ProcessNavigationPropertyPathExpression(IEdmPathExpression expression)
		{
			this.ProcessExpression(expression);
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0000A339 File Offset: 0x00008539
		protected virtual void ProcessParameterReferenceExpression(IEdmParameterReferenceExpression expression)
		{
			this.ProcessExpression(expression);
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0000A342 File Offset: 0x00008542
		protected virtual void ProcessCollectionExpression(IEdmCollectionExpression expression)
		{
			this.ProcessExpression(expression);
			this.VisitExpressions(expression.Elements);
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0000A357 File Offset: 0x00008557
		protected virtual void ProcessLabeledExpressionReferenceExpression(IEdmLabeledExpressionReferenceExpression expression)
		{
			this.ProcessExpression(expression);
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0000A360 File Offset: 0x00008560
		protected virtual void ProcessIsTypeExpression(IEdmIsTypeExpression expression)
		{
			this.ProcessExpression(expression);
			this.VisitTypeReference(expression.Type);
			this.VisitExpression(expression.Operand);
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0000A381 File Offset: 0x00008581
		protected virtual void ProcessIntegerConstantExpression(IEdmIntegerConstantExpression expression)
		{
			this.ProcessExpression(expression);
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0000A38A File Offset: 0x0000858A
		protected virtual void ProcessIfExpression(IEdmIfExpression expression)
		{
			this.ProcessExpression(expression);
			this.VisitExpression(expression.TestExpression);
			this.VisitExpression(expression.TrueExpression);
			this.VisitExpression(expression.FalseExpression);
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x0000A3B7 File Offset: 0x000085B7
		protected virtual void ProcessOperationReferenceExpression(IEdmOperationReferenceExpression expression)
		{
			this.ProcessExpression(expression);
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0000A3C0 File Offset: 0x000085C0
		protected virtual void ProcessOperationApplicationExpression(IEdmApplyExpression expression)
		{
			this.ProcessExpression(expression);
			this.VisitExpression(expression.AppliedOperation);
			this.VisitExpressions(expression.Arguments);
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x0000A3E1 File Offset: 0x000085E1
		protected virtual void ProcessFloatingConstantExpression(IEdmFloatingConstantExpression expression)
		{
			this.ProcessExpression(expression);
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x0000A3EA File Offset: 0x000085EA
		protected virtual void ProcessGuidConstantExpression(IEdmGuidConstantExpression expression)
		{
			this.ProcessExpression(expression);
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0000A3F3 File Offset: 0x000085F3
		protected virtual void ProcessEnumMemberReferenceExpression(IEdmEnumMemberReferenceExpression expression)
		{
			this.ProcessExpression(expression);
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x0000A3FC File Offset: 0x000085FC
		protected virtual void ProcessEnumMemberExpression(IEdmEnumMemberExpression expression)
		{
			this.ProcessExpression(expression);
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x0000A405 File Offset: 0x00008605
		protected virtual void ProcessEntitySetReferenceExpression(IEdmEntitySetReferenceExpression expression)
		{
			this.ProcessExpression(expression);
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x0000A40E File Offset: 0x0000860E
		protected virtual void ProcessDecimalConstantExpression(IEdmDecimalConstantExpression expression)
		{
			this.ProcessExpression(expression);
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0000A417 File Offset: 0x00008617
		protected virtual void ProcessDateConstantExpression(IEdmDateConstantExpression expression)
		{
			this.ProcessExpression(expression);
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x0000A420 File Offset: 0x00008620
		protected virtual void ProcessTimeOfDayConstantExpression(IEdmTimeOfDayConstantExpression expression)
		{
			this.ProcessExpression(expression);
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x0000A429 File Offset: 0x00008629
		protected virtual void ProcessDateTimeOffsetConstantExpression(IEdmDateTimeOffsetConstantExpression expression)
		{
			this.ProcessExpression(expression);
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x0000A432 File Offset: 0x00008632
		protected virtual void ProcessDurationConstantExpression(IEdmDurationConstantExpression expression)
		{
			this.ProcessExpression(expression);
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0000A43B File Offset: 0x0000863B
		protected virtual void ProcessBooleanConstantExpression(IEdmBooleanConstantExpression expression)
		{
			this.ProcessExpression(expression);
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0000A444 File Offset: 0x00008644
		protected virtual void ProcessCastExpression(IEdmCastExpression expression)
		{
			this.ProcessExpression(expression);
			this.VisitTypeReference(expression.Type);
			this.VisitExpression(expression.Operand);
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x0000A465 File Offset: 0x00008665
		protected virtual void ProcessLabeledExpression(IEdmLabeledExpression element)
		{
			this.VisitExpression(element.Expression);
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x0000A473 File Offset: 0x00008673
		protected virtual void ProcessPropertyConstructor(IEdmPropertyConstructor constructor)
		{
			this.VisitExpression(constructor.Value);
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x0000A481 File Offset: 0x00008681
		protected virtual void ProcessNullConstantExpression(IEdmNullExpression expression)
		{
			this.ProcessExpression(expression);
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x0000A48A File Offset: 0x0000868A
		protected virtual void ProcessEntityContainer(IEdmEntityContainer container)
		{
			this.ProcessVocabularyAnnotatable(container);
			this.ProcessNamedElement(container);
			this.VisitEntityContainerElements(container.Elements);
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x0000A4A6 File Offset: 0x000086A6
		protected virtual void ProcessEntityContainerElement(IEdmEntityContainerElement element)
		{
			this.ProcessNamedElement(element);
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x0000A4AF File Offset: 0x000086AF
		protected virtual void ProcessEntitySet(IEdmEntitySet set)
		{
			this.ProcessEntityContainerElement(set);
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x0000A4B8 File Offset: 0x000086B8
		protected virtual void ProcessSingleton(IEdmSingleton singleton)
		{
			this.ProcessEntityContainerElement(singleton);
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x0000A4C1 File Offset: 0x000086C1
		protected virtual void ProcessAction(IEdmAction action)
		{
			this.ProcessSchemaElement(action);
			this.ProcessOperation(action);
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x0000A4D1 File Offset: 0x000086D1
		protected virtual void ProcessFunction(IEdmFunction function)
		{
			this.ProcessSchemaElement(function);
			this.ProcessOperation(function);
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x0000A4E1 File Offset: 0x000086E1
		protected virtual void ProcessActionImport(IEdmActionImport actionImport)
		{
			this.ProcessEntityContainerElement(actionImport);
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x0000A4EA File Offset: 0x000086EA
		protected virtual void ProcessFunctionImport(IEdmFunctionImport functionImport)
		{
			this.ProcessEntityContainerElement(functionImport);
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x0000A4F3 File Offset: 0x000086F3
		protected virtual void ProcessOperation(IEdmOperation operation)
		{
			if (operation.ReturnType != null)
			{
				this.VisitTypeReference(operation.ReturnType);
			}
			this.VisitOperationParameters(operation.Parameters);
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x0000A515 File Offset: 0x00008715
		protected virtual void ProcessOperationParameter(IEdmOperationParameter parameter)
		{
			this.ProcessVocabularyAnnotatable(parameter);
			this.ProcessNamedElement(parameter);
			this.VisitTypeReference(parameter.Type);
		}

		// Token: 0x040001A1 RID: 417
		protected readonly IEdmModel Model;
	}
}
