using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200007F RID: 127
	internal abstract class EdmModelVisitor
	{
		// Token: 0x06000278 RID: 632 RVA: 0x00006100 File Offset: 0x00004300
		protected EdmModelVisitor(IEdmModel model)
		{
			this.Model = model;
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000610F File Offset: 0x0000430F
		public void VisitEdmModel()
		{
			this.ProcessModel(this.Model);
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000611D File Offset: 0x0000431D
		public void VisitSchemaElements(IEnumerable<IEdmSchemaElement> elements)
		{
			EdmModelVisitor.VisitCollection<IEdmSchemaElement>(elements, new Action<IEdmSchemaElement>(this.VisitSchemaElement));
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00006134 File Offset: 0x00004334
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

		// Token: 0x0600027C RID: 636 RVA: 0x000061C6 File Offset: 0x000043C6
		public void VisitAnnotations(IEnumerable<IEdmDirectValueAnnotation> annotations)
		{
			EdmModelVisitor.VisitCollection<IEdmDirectValueAnnotation>(annotations, new Action<IEdmDirectValueAnnotation>(this.VisitAnnotation));
		}

		// Token: 0x0600027D RID: 637 RVA: 0x000061DA File Offset: 0x000043DA
		public void VisitVocabularyAnnotations(IEnumerable<IEdmVocabularyAnnotation> annotations)
		{
			EdmModelVisitor.VisitCollection<IEdmVocabularyAnnotation>(annotations, new Action<IEdmVocabularyAnnotation>(this.VisitVocabularyAnnotation));
		}

		// Token: 0x0600027E RID: 638 RVA: 0x000061EE File Offset: 0x000043EE
		public void VisitAnnotation(IEdmDirectValueAnnotation annotation)
		{
			this.ProcessImmediateValueAnnotation(annotation);
		}

		// Token: 0x0600027F RID: 639 RVA: 0x000061F7 File Offset: 0x000043F7
		public void VisitVocabularyAnnotation(IEdmVocabularyAnnotation annotation)
		{
			if (annotation.Term != null)
			{
				this.ProcessAnnotation(annotation);
				return;
			}
			this.ProcessVocabularyAnnotation(annotation);
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00006210 File Offset: 0x00004410
		public void VisitPropertyValueBindings(IEnumerable<IEdmPropertyValueBinding> bindings)
		{
			EdmModelVisitor.VisitCollection<IEdmPropertyValueBinding>(bindings, new Action<IEdmPropertyValueBinding>(this.ProcessPropertyValueBinding));
		}

		// Token: 0x06000281 RID: 641 RVA: 0x00006225 File Offset: 0x00004425
		public void VisitExpressions(IEnumerable<IEdmExpression> expressions)
		{
			EdmModelVisitor.VisitCollection<IEdmExpression>(expressions, new Action<IEdmExpression>(this.VisitExpression));
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000623C File Offset: 0x0000443C
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

		// Token: 0x06000283 RID: 643 RVA: 0x00006414 File Offset: 0x00004614
		public void VisitPropertyConstructors(IEnumerable<IEdmPropertyConstructor> constructor)
		{
			EdmModelVisitor.VisitCollection<IEdmPropertyConstructor>(constructor, new Action<IEdmPropertyConstructor>(this.ProcessPropertyConstructor));
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000642C File Offset: 0x0000462C
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

		// Token: 0x06000285 RID: 645 RVA: 0x000064F8 File Offset: 0x000046F8
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
			case EdmTypeKind.Path:
				this.ProcessPathTypeReference(reference.AsPath());
				return;
			}
			throw new InvalidOperationException(Strings.UnknownEnumVal_TypeKind(reference.TypeKind().ToString()));
		}

		// Token: 0x06000286 RID: 646 RVA: 0x000065CC File Offset: 0x000047CC
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
			case EdmPrimitiveTypeKind.PrimitiveType:
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

		// Token: 0x06000287 RID: 647 RVA: 0x000066DC File Offset: 0x000048DC
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

		// Token: 0x06000288 RID: 648 RVA: 0x00006769 File Offset: 0x00004969
		public void VisitProperties(IEnumerable<IEdmProperty> properties)
		{
			EdmModelVisitor.VisitCollection<IEdmProperty>(properties, new Action<IEdmProperty>(this.VisitProperty));
		}

		// Token: 0x06000289 RID: 649 RVA: 0x00006780 File Offset: 0x00004980
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

		// Token: 0x0600028A RID: 650 RVA: 0x000067E8 File Offset: 0x000049E8
		public void VisitEnumMembers(IEnumerable<IEdmEnumMember> enumMembers)
		{
			EdmModelVisitor.VisitCollection<IEdmEnumMember>(enumMembers, new Action<IEdmEnumMember>(this.VisitEnumMember));
		}

		// Token: 0x0600028B RID: 651 RVA: 0x000067FC File Offset: 0x000049FC
		public void VisitEnumMember(IEdmEnumMember enumMember)
		{
			this.ProcessEnumMember(enumMember);
		}

		// Token: 0x0600028C RID: 652 RVA: 0x00006805 File Offset: 0x00004A05
		public void VisitOperationParameters(IEnumerable<IEdmOperationParameter> parameters)
		{
			EdmModelVisitor.VisitCollection<IEdmOperationParameter>(parameters, new Action<IEdmOperationParameter>(this.ProcessOperationParameter));
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000681C File Offset: 0x00004A1C
		protected static void VisitCollection<T>(IEnumerable<T> collection, Action<T> visitMethod)
		{
			foreach (T t in collection)
			{
				visitMethod(t);
			}
		}

		// Token: 0x0600028E RID: 654 RVA: 0x00006864 File Offset: 0x00004A64
		protected virtual void ProcessModel(IEdmModel model)
		{
			this.ProcessElement(model);
			this.VisitSchemaElements(model.SchemaElements);
			this.VisitVocabularyAnnotations(model.VocabularyAnnotations);
		}

		// Token: 0x0600028F RID: 655 RVA: 0x00006885 File Offset: 0x00004A85
		protected virtual void ProcessElement(IEdmElement element)
		{
			this.VisitAnnotations(this.Model.DirectValueAnnotations(element));
		}

		// Token: 0x06000290 RID: 656 RVA: 0x00006899 File Offset: 0x00004A99
		protected virtual void ProcessNamedElement(IEdmNamedElement element)
		{
			this.ProcessElement(element);
		}

		// Token: 0x06000291 RID: 657 RVA: 0x000068A2 File Offset: 0x00004AA2
		protected virtual void ProcessSchemaElement(IEdmSchemaElement element)
		{
			this.ProcessVocabularyAnnotatable(element);
			this.ProcessNamedElement(element);
		}

		// Token: 0x06000292 RID: 658 RVA: 0x000068B2 File Offset: 0x00004AB2
		protected virtual void ProcessVocabularyAnnotatable(IEdmVocabularyAnnotatable annotatable)
		{
		}

		// Token: 0x06000293 RID: 659 RVA: 0x000068B4 File Offset: 0x00004AB4
		protected virtual void ProcessComplexTypeReference(IEdmComplexTypeReference reference)
		{
			this.ProcessStructuredTypeReference(reference);
		}

		// Token: 0x06000294 RID: 660 RVA: 0x000068B4 File Offset: 0x00004AB4
		protected virtual void ProcessEntityTypeReference(IEdmEntityTypeReference reference)
		{
			this.ProcessStructuredTypeReference(reference);
		}

		// Token: 0x06000295 RID: 661 RVA: 0x000068BD File Offset: 0x00004ABD
		protected virtual void ProcessEntityReferenceTypeReference(IEdmEntityReferenceTypeReference reference)
		{
			this.ProcessTypeReference(reference);
			this.ProcessEntityReferenceType(reference.EntityReferenceDefinition());
		}

		// Token: 0x06000296 RID: 662 RVA: 0x000068D2 File Offset: 0x00004AD2
		protected virtual void ProcessCollectionTypeReference(IEdmCollectionTypeReference reference)
		{
			this.ProcessTypeReference(reference);
			this.ProcessCollectionType(reference.CollectionDefinition());
		}

		// Token: 0x06000297 RID: 663 RVA: 0x000068E7 File Offset: 0x00004AE7
		protected virtual void ProcessEnumTypeReference(IEdmEnumTypeReference reference)
		{
			this.ProcessTypeReference(reference);
		}

		// Token: 0x06000298 RID: 664 RVA: 0x000068E7 File Offset: 0x00004AE7
		protected virtual void ProcessTypeDefinitionReference(IEdmTypeDefinitionReference reference)
		{
			this.ProcessTypeReference(reference);
		}

		// Token: 0x06000299 RID: 665 RVA: 0x000068F0 File Offset: 0x00004AF0
		protected virtual void ProcessBinaryTypeReference(IEdmBinaryTypeReference reference)
		{
			this.ProcessPrimitiveTypeReference(reference);
		}

		// Token: 0x0600029A RID: 666 RVA: 0x000068F0 File Offset: 0x00004AF0
		protected virtual void ProcessDecimalTypeReference(IEdmDecimalTypeReference reference)
		{
			this.ProcessPrimitiveTypeReference(reference);
		}

		// Token: 0x0600029B RID: 667 RVA: 0x000068F0 File Offset: 0x00004AF0
		protected virtual void ProcessSpatialTypeReference(IEdmSpatialTypeReference reference)
		{
			this.ProcessPrimitiveTypeReference(reference);
		}

		// Token: 0x0600029C RID: 668 RVA: 0x000068F0 File Offset: 0x00004AF0
		protected virtual void ProcessStringTypeReference(IEdmStringTypeReference reference)
		{
			this.ProcessPrimitiveTypeReference(reference);
		}

		// Token: 0x0600029D RID: 669 RVA: 0x000068F0 File Offset: 0x00004AF0
		protected virtual void ProcessTemporalTypeReference(IEdmTemporalTypeReference reference)
		{
			this.ProcessPrimitiveTypeReference(reference);
		}

		// Token: 0x0600029E RID: 670 RVA: 0x000068E7 File Offset: 0x00004AE7
		protected virtual void ProcessPrimitiveTypeReference(IEdmPrimitiveTypeReference reference)
		{
			this.ProcessTypeReference(reference);
		}

		// Token: 0x0600029F RID: 671 RVA: 0x000068E7 File Offset: 0x00004AE7
		protected virtual void ProcessStructuredTypeReference(IEdmStructuredTypeReference reference)
		{
			this.ProcessTypeReference(reference);
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x00006899 File Offset: 0x00004A99
		protected virtual void ProcessTypeReference(IEdmTypeReference element)
		{
			this.ProcessElement(element);
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x000068E7 File Offset: 0x00004AE7
		protected virtual void ProcessPathTypeReference(IEdmPathTypeReference reference)
		{
			this.ProcessTypeReference(reference);
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x000068F9 File Offset: 0x00004AF9
		protected virtual void ProcessTerm(IEdmTerm term)
		{
			this.ProcessSchemaElement(term);
			this.VisitTypeReference(term.Type);
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000690E File Offset: 0x00004B0E
		protected virtual void ProcessComplexType(IEdmComplexType definition)
		{
			this.ProcessSchemaElement(definition);
			this.ProcessStructuredType(definition);
			this.ProcessSchemaType(definition);
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000690E File Offset: 0x00004B0E
		protected virtual void ProcessEntityType(IEdmEntityType definition)
		{
			this.ProcessSchemaElement(definition);
			this.ProcessStructuredType(definition);
			this.ProcessSchemaType(definition);
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x00006925 File Offset: 0x00004B25
		protected virtual void ProcessCollectionType(IEdmCollectionType definition)
		{
			this.ProcessElement(definition);
			this.ProcessType(definition);
			this.VisitTypeReference(definition.ElementType);
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x00006941 File Offset: 0x00004B41
		protected virtual void ProcessEnumType(IEdmEnumType definition)
		{
			this.ProcessSchemaElement(definition);
			this.ProcessType(definition);
			this.ProcessSchemaType(definition);
			this.VisitEnumMembers(definition.Members);
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x00006964 File Offset: 0x00004B64
		protected virtual void ProcessTypeDefinition(IEdmTypeDefinition definition)
		{
			this.ProcessSchemaElement(definition);
			this.ProcessType(definition);
			this.ProcessSchemaType(definition);
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000697B File Offset: 0x00004B7B
		protected virtual void ProcessEntityReferenceType(IEdmEntityReferenceType definition)
		{
			this.ProcessElement(definition);
			this.ProcessType(definition);
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000698B File Offset: 0x00004B8B
		protected virtual void ProcessStructuredType(IEdmStructuredType definition)
		{
			this.ProcessType(definition);
			this.VisitProperties(definition.DeclaredProperties);
		}

		// Token: 0x060002AA RID: 682 RVA: 0x000068B2 File Offset: 0x00004AB2
		protected virtual void ProcessSchemaType(IEdmSchemaType type)
		{
		}

		// Token: 0x060002AB RID: 683 RVA: 0x000068B2 File Offset: 0x00004AB2
		protected virtual void ProcessType(IEdmType definition)
		{
		}

		// Token: 0x060002AC RID: 684 RVA: 0x000069A0 File Offset: 0x00004BA0
		protected virtual void ProcessNavigationProperty(IEdmNavigationProperty property)
		{
			this.ProcessProperty(property);
		}

		// Token: 0x060002AD RID: 685 RVA: 0x000069A0 File Offset: 0x00004BA0
		protected virtual void ProcessStructuralProperty(IEdmStructuralProperty property)
		{
			this.ProcessProperty(property);
		}

		// Token: 0x060002AE RID: 686 RVA: 0x000069A9 File Offset: 0x00004BA9
		protected virtual void ProcessProperty(IEdmProperty property)
		{
			this.ProcessVocabularyAnnotatable(property);
			this.ProcessNamedElement(property);
			this.VisitTypeReference(property.Type);
		}

		// Token: 0x060002AF RID: 687 RVA: 0x000069C5 File Offset: 0x00004BC5
		protected virtual void ProcessEnumMember(IEdmEnumMember enumMember)
		{
			this.ProcessNamedElement(enumMember);
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x00006899 File Offset: 0x00004A99
		protected virtual void ProcessVocabularyAnnotation(IEdmVocabularyAnnotation annotation)
		{
			this.ProcessElement(annotation);
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x000069C5 File Offset: 0x00004BC5
		protected virtual void ProcessImmediateValueAnnotation(IEdmDirectValueAnnotation annotation)
		{
			this.ProcessNamedElement(annotation);
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x000069CE File Offset: 0x00004BCE
		protected virtual void ProcessAnnotation(IEdmVocabularyAnnotation annotation)
		{
			this.ProcessVocabularyAnnotation(annotation);
			this.VisitExpression(annotation.Value);
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x000069E3 File Offset: 0x00004BE3
		protected virtual void ProcessPropertyValueBinding(IEdmPropertyValueBinding binding)
		{
			this.VisitExpression(binding.Value);
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x000068B2 File Offset: 0x00004AB2
		protected virtual void ProcessExpression(IEdmExpression expression)
		{
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x000069F1 File Offset: 0x00004BF1
		protected virtual void ProcessStringConstantExpression(IEdmStringConstantExpression expression)
		{
			this.ProcessExpression(expression);
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x000069F1 File Offset: 0x00004BF1
		protected virtual void ProcessBinaryConstantExpression(IEdmBinaryConstantExpression expression)
		{
			this.ProcessExpression(expression);
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x000069FA File Offset: 0x00004BFA
		protected virtual void ProcessRecordExpression(IEdmRecordExpression expression)
		{
			this.ProcessExpression(expression);
			if (expression.DeclaredType != null)
			{
				this.VisitTypeReference(expression.DeclaredType);
			}
			this.VisitPropertyConstructors(expression.Properties);
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x000069F1 File Offset: 0x00004BF1
		protected virtual void ProcessPathExpression(IEdmPathExpression expression)
		{
			this.ProcessExpression(expression);
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x000069F1 File Offset: 0x00004BF1
		protected virtual void ProcessPropertyPathExpression(IEdmPathExpression expression)
		{
			this.ProcessExpression(expression);
		}

		// Token: 0x060002BA RID: 698 RVA: 0x000069F1 File Offset: 0x00004BF1
		protected virtual void ProcessNavigationPropertyPathExpression(IEdmPathExpression expression)
		{
			this.ProcessExpression(expression);
		}

		// Token: 0x060002BB RID: 699 RVA: 0x00006A23 File Offset: 0x00004C23
		protected virtual void ProcessCollectionExpression(IEdmCollectionExpression expression)
		{
			this.ProcessExpression(expression);
			this.VisitExpressions(expression.Elements);
		}

		// Token: 0x060002BC RID: 700 RVA: 0x000069F1 File Offset: 0x00004BF1
		protected virtual void ProcessLabeledExpressionReferenceExpression(IEdmLabeledExpressionReferenceExpression expression)
		{
			this.ProcessExpression(expression);
		}

		// Token: 0x060002BD RID: 701 RVA: 0x00006A38 File Offset: 0x00004C38
		protected virtual void ProcessIsTypeExpression(IEdmIsTypeExpression expression)
		{
			this.ProcessExpression(expression);
			this.VisitTypeReference(expression.Type);
			this.VisitExpression(expression.Operand);
		}

		// Token: 0x060002BE RID: 702 RVA: 0x000069F1 File Offset: 0x00004BF1
		protected virtual void ProcessIntegerConstantExpression(IEdmIntegerConstantExpression expression)
		{
			this.ProcessExpression(expression);
		}

		// Token: 0x060002BF RID: 703 RVA: 0x00006A59 File Offset: 0x00004C59
		protected virtual void ProcessIfExpression(IEdmIfExpression expression)
		{
			this.ProcessExpression(expression);
			this.VisitExpression(expression.TestExpression);
			this.VisitExpression(expression.TrueExpression);
			this.VisitExpression(expression.FalseExpression);
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x00006A86 File Offset: 0x00004C86
		protected virtual void ProcessFunctionApplicationExpression(IEdmApplyExpression expression)
		{
			this.ProcessExpression(expression);
			this.VisitExpressions(expression.Arguments);
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x000069F1 File Offset: 0x00004BF1
		protected virtual void ProcessFloatingConstantExpression(IEdmFloatingConstantExpression expression)
		{
			this.ProcessExpression(expression);
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x000069F1 File Offset: 0x00004BF1
		protected virtual void ProcessGuidConstantExpression(IEdmGuidConstantExpression expression)
		{
			this.ProcessExpression(expression);
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x000069F1 File Offset: 0x00004BF1
		protected virtual void ProcessEnumMemberExpression(IEdmEnumMemberExpression expression)
		{
			this.ProcessExpression(expression);
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x000069F1 File Offset: 0x00004BF1
		protected virtual void ProcessDecimalConstantExpression(IEdmDecimalConstantExpression expression)
		{
			this.ProcessExpression(expression);
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x000069F1 File Offset: 0x00004BF1
		protected virtual void ProcessDateConstantExpression(IEdmDateConstantExpression expression)
		{
			this.ProcessExpression(expression);
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x000069F1 File Offset: 0x00004BF1
		protected virtual void ProcessTimeOfDayConstantExpression(IEdmTimeOfDayConstantExpression expression)
		{
			this.ProcessExpression(expression);
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x000069F1 File Offset: 0x00004BF1
		protected virtual void ProcessDateTimeOffsetConstantExpression(IEdmDateTimeOffsetConstantExpression expression)
		{
			this.ProcessExpression(expression);
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x000069F1 File Offset: 0x00004BF1
		protected virtual void ProcessDurationConstantExpression(IEdmDurationConstantExpression expression)
		{
			this.ProcessExpression(expression);
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x000069F1 File Offset: 0x00004BF1
		protected virtual void ProcessBooleanConstantExpression(IEdmBooleanConstantExpression expression)
		{
			this.ProcessExpression(expression);
		}

		// Token: 0x060002CA RID: 714 RVA: 0x00006A9B File Offset: 0x00004C9B
		protected virtual void ProcessCastExpression(IEdmCastExpression expression)
		{
			this.ProcessExpression(expression);
			this.VisitTypeReference(expression.Type);
			this.VisitExpression(expression.Operand);
		}

		// Token: 0x060002CB RID: 715 RVA: 0x00006ABC File Offset: 0x00004CBC
		protected virtual void ProcessLabeledExpression(IEdmLabeledExpression element)
		{
			this.VisitExpression(element.Expression);
		}

		// Token: 0x060002CC RID: 716 RVA: 0x00006ACA File Offset: 0x00004CCA
		protected virtual void ProcessPropertyConstructor(IEdmPropertyConstructor constructor)
		{
			this.VisitExpression(constructor.Value);
		}

		// Token: 0x060002CD RID: 717 RVA: 0x000069F1 File Offset: 0x00004BF1
		protected virtual void ProcessNullConstantExpression(IEdmNullExpression expression)
		{
			this.ProcessExpression(expression);
		}

		// Token: 0x060002CE RID: 718 RVA: 0x00006AD8 File Offset: 0x00004CD8
		protected virtual void ProcessEntityContainer(IEdmEntityContainer container)
		{
			this.ProcessVocabularyAnnotatable(container);
			this.ProcessNamedElement(container);
			this.VisitEntityContainerElements(container.Elements);
		}

		// Token: 0x060002CF RID: 719 RVA: 0x000069C5 File Offset: 0x00004BC5
		protected virtual void ProcessEntityContainerElement(IEdmEntityContainerElement element)
		{
			this.ProcessNamedElement(element);
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x00006AF4 File Offset: 0x00004CF4
		protected virtual void ProcessEntitySet(IEdmEntitySet set)
		{
			this.ProcessEntityContainerElement(set);
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x00006AF4 File Offset: 0x00004CF4
		protected virtual void ProcessSingleton(IEdmSingleton singleton)
		{
			this.ProcessEntityContainerElement(singleton);
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x00006AFD File Offset: 0x00004CFD
		protected virtual void ProcessAction(IEdmAction action)
		{
			this.ProcessSchemaElement(action);
			this.ProcessOperation(action);
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x00006AFD File Offset: 0x00004CFD
		protected virtual void ProcessFunction(IEdmFunction function)
		{
			this.ProcessSchemaElement(function);
			this.ProcessOperation(function);
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x00006AF4 File Offset: 0x00004CF4
		protected virtual void ProcessActionImport(IEdmActionImport actionImport)
		{
			this.ProcessEntityContainerElement(actionImport);
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x00006AF4 File Offset: 0x00004CF4
		protected virtual void ProcessFunctionImport(IEdmFunctionImport functionImport)
		{
			this.ProcessEntityContainerElement(functionImport);
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x00006B10 File Offset: 0x00004D10
		protected virtual void ProcessOperation(IEdmOperation operation)
		{
			this.VisitOperationParameters(operation.Parameters);
			IEdmOperationReturn @return = operation.GetReturn();
			this.ProcessOperationReturn(@return);
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x00006B37 File Offset: 0x00004D37
		protected virtual void ProcessOperationParameter(IEdmOperationParameter parameter)
		{
			this.ProcessVocabularyAnnotatable(parameter);
			this.ProcessNamedElement(parameter);
			this.VisitTypeReference(parameter.Type);
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x00006B53 File Offset: 0x00004D53
		protected virtual void ProcessOperationReturn(IEdmOperationReturn operationReturn)
		{
			if (operationReturn == null)
			{
				return;
			}
			this.ProcessVocabularyAnnotatable(operationReturn);
			this.VisitTypeReference(operationReturn.Type);
		}

		// Token: 0x040000DB RID: 219
		protected readonly IEdmModel Model;
	}
}
