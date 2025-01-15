using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.Serialization
{
	// Token: 0x0200015D RID: 349
	internal sealed class EdmModelCsdlSerializationVisitor : EdmModelVisitor
	{
		// Token: 0x06000939 RID: 2361 RVA: 0x00019EB8 File Offset: 0x000180B8
		internal EdmModelCsdlSerializationVisitor(IEdmModel model, XmlWriter xmlWriter, Version edmVersion)
			: base(model)
		{
			this.edmVersion = edmVersion;
			this.namespaceAliasMappings = model.GetNamespaceAliases();
			this.schemaWriter = new EdmModelCsdlSchemaWriter(model, this.namespaceAliasMappings, xmlWriter, this.edmVersion);
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x00019EF8 File Offset: 0x000180F8
		public override void VisitEntityContainerElements(IEnumerable<IEdmEntityContainerElement> elements)
		{
			HashSet<string> hashSet = new HashSet<string>();
			HashSet<string> hashSet2 = new HashSet<string>();
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
				{
					IEdmActionImport edmActionImport = (IEdmActionImport)edmEntityContainerElement;
					string text = edmActionImport.Name + "_" + edmActionImport.Action.FullName() + EdmModelCsdlSerializationVisitor.GetEntitySetString(edmActionImport);
					if (!hashSet2.Contains(text))
					{
						hashSet2.Add(text);
						this.ProcessActionImport(edmActionImport);
					}
					break;
				}
				case EdmContainerElementKind.FunctionImport:
				{
					IEdmFunctionImport edmFunctionImport = (IEdmFunctionImport)edmEntityContainerElement;
					string text2 = edmFunctionImport.Name + "_" + edmFunctionImport.Function.FullName() + EdmModelCsdlSerializationVisitor.GetEntitySetString(edmFunctionImport);
					if (!hashSet.Contains(text2))
					{
						hashSet.Add(text2);
						this.ProcessFunctionImport(edmFunctionImport);
					}
					break;
				}
				case EdmContainerElementKind.Singleton:
					this.ProcessSingleton((IEdmSingleton)edmEntityContainerElement);
					break;
				default:
					throw new InvalidOperationException(Strings.UnknownEnumVal_ContainerElementKind(edmEntityContainerElement.ContainerElementKind.ToString()));
				}
			}
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x0001A064 File Offset: 0x00018264
		internal void VisitEdmSchema(EdmSchema element, IEnumerable<KeyValuePair<string, string>> mappings)
		{
			string text = null;
			if (this.namespaceAliasMappings != null)
			{
				this.namespaceAliasMappings.TryGetValue(element.Namespace, out text);
			}
			this.schemaWriter.WriteSchemaElementHeader(element, text, mappings);
			base.VisitSchemaElements(element.SchemaElements);
			EdmModelVisitor.VisitCollection<IEdmEntityContainer>(element.EntityContainers, new Action<IEdmEntityContainer>(this.ProcessEntityContainer));
			foreach (KeyValuePair<string, List<IEdmVocabularyAnnotation>> keyValuePair in element.OutOfLineAnnotations)
			{
				this.schemaWriter.WriteAnnotationsElementHeader(keyValuePair.Key);
				base.VisitVocabularyAnnotations(keyValuePair.Value);
				this.schemaWriter.WriteEndElement();
			}
			this.schemaWriter.WriteEndElement();
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x0001A130 File Offset: 0x00018330
		protected override void ProcessEntityContainer(IEdmEntityContainer element)
		{
			this.BeginElement<IEdmEntityContainer>(element, new Action<IEdmEntityContainer>(this.schemaWriter.WriteEntityContainerElementHeader), new Action<IEdmEntityContainer>[0]);
			base.ProcessEntityContainer(element);
			this.EndElement(element);
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x0001A160 File Offset: 0x00018360
		protected override void ProcessEntitySet(IEdmEntitySet element)
		{
			this.BeginElement<IEdmEntitySet>(element, new Action<IEdmEntitySet>(this.schemaWriter.WriteEntitySetElementHeader), new Action<IEdmEntitySet>[0]);
			base.ProcessEntitySet(element);
			foreach (IEdmNavigationPropertyBinding edmNavigationPropertyBinding in element.NavigationPropertyBindings)
			{
				this.schemaWriter.WriteNavigationPropertyBinding(element, edmNavigationPropertyBinding);
			}
			this.EndElement(element);
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x0001A1E0 File Offset: 0x000183E0
		protected override void ProcessSingleton(IEdmSingleton element)
		{
			this.BeginElement<IEdmSingleton>(element, new Action<IEdmSingleton>(this.schemaWriter.WriteSingletonElementHeader), new Action<IEdmSingleton>[0]);
			base.ProcessSingleton(element);
			foreach (IEdmNavigationPropertyBinding edmNavigationPropertyBinding in element.NavigationPropertyBindings)
			{
				this.schemaWriter.WriteNavigationPropertyBinding(element, edmNavigationPropertyBinding);
			}
			this.EndElement(element);
		}

		// Token: 0x0600093F RID: 2367 RVA: 0x0001A260 File Offset: 0x00018460
		protected override void ProcessEntityType(IEdmEntityType element)
		{
			this.BeginElement<IEdmEntityType>(element, new Action<IEdmEntityType>(this.schemaWriter.WriteEntityTypeElementHeader), new Action<IEdmEntityType>[0]);
			if (element.DeclaredKey != null && element.DeclaredKey.Any<IEdmStructuralProperty>())
			{
				this.VisitEntityTypeDeclaredKey(element.DeclaredKey);
			}
			base.VisitProperties(element.DeclaredStructuralProperties().Cast<IEdmProperty>());
			base.VisitProperties(element.DeclaredNavigationProperties().Cast<IEdmProperty>());
			this.EndElement(element);
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x0001A2D8 File Offset: 0x000184D8
		protected override void ProcessStructuralProperty(IEdmStructuralProperty element)
		{
			bool inlineType = EdmModelCsdlSerializationVisitor.IsInlineType(element.Type);
			this.BeginElement<IEdmStructuralProperty>(element, delegate(IEdmStructuralProperty t)
			{
				this.schemaWriter.WriteStructuralPropertyElementHeader(t, inlineType);
			}, new Action<IEdmStructuralProperty>[]
			{
				delegate(IEdmStructuralProperty e)
				{
					this.ProcessFacets(e.Type, inlineType);
				}
			});
			if (!inlineType)
			{
				base.VisitTypeReference(element.Type);
			}
			this.EndElement(element);
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x0001A346 File Offset: 0x00018546
		protected override void ProcessTypeDefinitionReference(IEdmTypeDefinitionReference element)
		{
			this.schemaWriter.WriteTypeDefinitionAttributes(element);
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x0001A354 File Offset: 0x00018554
		protected override void ProcessBinaryTypeReference(IEdmBinaryTypeReference element)
		{
			this.schemaWriter.WriteBinaryTypeAttributes(element);
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x0001A362 File Offset: 0x00018562
		protected override void ProcessDecimalTypeReference(IEdmDecimalTypeReference element)
		{
			this.schemaWriter.WriteDecimalTypeAttributes(element);
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x0001A370 File Offset: 0x00018570
		protected override void ProcessSpatialTypeReference(IEdmSpatialTypeReference element)
		{
			this.schemaWriter.WriteSpatialTypeAttributes(element);
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x0001A37E File Offset: 0x0001857E
		protected override void ProcessStringTypeReference(IEdmStringTypeReference element)
		{
			this.schemaWriter.WriteStringTypeAttributes(element);
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x0001A38C File Offset: 0x0001858C
		protected override void ProcessTemporalTypeReference(IEdmTemporalTypeReference element)
		{
			this.schemaWriter.WriteTemporalTypeAttributes(element);
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x0001A39C File Offset: 0x0001859C
		protected override void ProcessNavigationProperty(IEdmNavigationProperty element)
		{
			this.BeginElement<IEdmNavigationProperty>(element, new Action<IEdmNavigationProperty>(this.schemaWriter.WriteNavigationPropertyElementHeader), new Action<IEdmNavigationProperty>[0]);
			if (element.OnDelete != EdmOnDeleteAction.None)
			{
				this.schemaWriter.WriteOperationActionElement("OnDelete", element.OnDelete);
			}
			this.ProcessReferentialConstraint(element.ReferentialConstraint);
			this.EndElement(element);
			this.navigationProperties.Add(element);
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x0001A404 File Offset: 0x00018604
		protected override void ProcessComplexType(IEdmComplexType element)
		{
			this.BeginElement<IEdmComplexType>(element, new Action<IEdmComplexType>(this.schemaWriter.WriteComplexTypeElementHeader), new Action<IEdmComplexType>[0]);
			base.ProcessComplexType(element);
			this.EndElement(element);
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x0001A432 File Offset: 0x00018632
		protected override void ProcessEnumType(IEdmEnumType element)
		{
			this.BeginElement<IEdmEnumType>(element, new Action<IEdmEnumType>(this.schemaWriter.WriteEnumTypeElementHeader), new Action<IEdmEnumType>[0]);
			base.ProcessEnumType(element);
			this.EndElement(element);
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x0001A460 File Offset: 0x00018660
		protected override void ProcessEnumMember(IEdmEnumMember element)
		{
			this.BeginElement<IEdmEnumMember>(element, new Action<IEdmEnumMember>(this.schemaWriter.WriteEnumMemberElementHeader), new Action<IEdmEnumMember>[0]);
			this.EndElement(element);
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x0001A487 File Offset: 0x00018687
		protected override void ProcessTypeDefinition(IEdmTypeDefinition element)
		{
			this.BeginElement<IEdmTypeDefinition>(element, new Action<IEdmTypeDefinition>(this.schemaWriter.WriteTypeDefinitionElementHeader), new Action<IEdmTypeDefinition>[0]);
			base.ProcessTypeDefinition(element);
			this.EndElement(element);
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x0001A4B8 File Offset: 0x000186B8
		protected override void ProcessTerm(IEdmTerm term)
		{
			bool inlineType = term.Type != null && EdmModelCsdlSerializationVisitor.IsInlineType(term.Type);
			this.BeginElement<IEdmTerm>(term, delegate(IEdmTerm t)
			{
				this.schemaWriter.WriteTermElementHeader(t, inlineType);
			}, new Action<IEdmTerm>[]
			{
				delegate(IEdmTerm e)
				{
					this.ProcessFacets(e.Type, inlineType);
				}
			});
			if (!inlineType && term.Type != null)
			{
				base.VisitTypeReference(term.Type);
			}
			this.EndElement(term);
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x0001A539 File Offset: 0x00018739
		protected override void ProcessAction(IEdmAction action)
		{
			this.ProcessOperation<IEdmAction>(action, new Action<IEdmAction>(this.schemaWriter.WriteActionElementHeader));
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x0001A553 File Offset: 0x00018753
		protected override void ProcessFunction(IEdmFunction function)
		{
			this.ProcessOperation<IEdmFunction>(function, new Action<IEdmFunction>(this.schemaWriter.WriteFunctionElementHeader));
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x0001A570 File Offset: 0x00018770
		protected override void ProcessOperationParameter(IEdmOperationParameter element)
		{
			bool inlineType = EdmModelCsdlSerializationVisitor.IsInlineType(element.Type);
			this.BeginElement<IEdmOperationParameter>(element, delegate(IEdmOperationParameter t)
			{
				this.schemaWriter.WriteOperationParameterElementHeader(t, inlineType);
			}, new Action<IEdmOperationParameter>[]
			{
				delegate(IEdmOperationParameter e)
				{
					this.ProcessFacets(e.Type, inlineType);
				}
			});
			if (!inlineType)
			{
				base.VisitTypeReference(element.Type);
			}
			this.VisitPrimitiveElementAnnotations(this.Model.DirectValueAnnotations(element));
			if (element != null)
			{
				this.VisitElementVocabularyAnnotations(from a in this.Model.FindDeclaredVocabularyAnnotations(element)
					where a.IsInline(this.Model)
					select a);
			}
			this.schemaWriter.WriteOperationParameterEndElement(element);
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x0001A620 File Offset: 0x00018820
		protected override void ProcessOperationReturn(IEdmOperationReturn operationReturn)
		{
			if (operationReturn == null)
			{
				return;
			}
			bool inlineType = EdmModelCsdlSerializationVisitor.IsInlineType(operationReturn.Type);
			this.BeginElement<IEdmTypeReference>(operationReturn.Type, delegate(IEdmTypeReference type)
			{
				this.schemaWriter.WriteReturnTypeElementHeader();
			}, new Action<IEdmTypeReference>[]
			{
				delegate(IEdmTypeReference type)
				{
					if (inlineType)
					{
						this.schemaWriter.WriteTypeAttribute(type);
						this.ProcessFacets(type, true);
						return;
					}
					this.VisitTypeReference(type);
				}
			});
			this.EndElement(operationReturn);
		}

		// Token: 0x06000951 RID: 2385 RVA: 0x0001A684 File Offset: 0x00018884
		protected override void ProcessCollectionType(IEdmCollectionType element)
		{
			bool inlineType = EdmModelCsdlSerializationVisitor.IsInlineType(element.ElementType);
			this.BeginElement<IEdmCollectionType>(element, delegate(IEdmCollectionType t)
			{
				this.schemaWriter.WriteCollectionTypeElementHeader(t, inlineType);
			}, new Action<IEdmCollectionType>[]
			{
				delegate(IEdmCollectionType e)
				{
					this.ProcessFacets(e.ElementType, inlineType);
				}
			});
			if (!inlineType)
			{
				base.VisitTypeReference(element.ElementType);
			}
			this.EndElement(element);
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x0001A6F2 File Offset: 0x000188F2
		protected override void ProcessActionImport(IEdmActionImport actionImport)
		{
			this.BeginElement<IEdmActionImport>(actionImport, new Action<IEdmActionImport>(this.schemaWriter.WriteActionImportElementHeader), new Action<IEdmActionImport>[0]);
			this.EndElement(actionImport);
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x0001A719 File Offset: 0x00018919
		protected override void ProcessFunctionImport(IEdmFunctionImport functionImport)
		{
			this.BeginElement<IEdmFunctionImport>(functionImport, new Action<IEdmFunctionImport>(this.schemaWriter.WriteFunctionImportElementHeader), new Action<IEdmFunctionImport>[0]);
			this.EndElement(functionImport);
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x0001A740 File Offset: 0x00018940
		protected override void ProcessAnnotation(IEdmVocabularyAnnotation annotation)
		{
			bool isInline = EdmModelCsdlSerializationVisitor.IsInlineExpression(annotation.Value);
			this.BeginElement<IEdmVocabularyAnnotation>(annotation, delegate(IEdmVocabularyAnnotation t)
			{
				this.schemaWriter.WriteVocabularyAnnotationElementHeader(t, isInline);
			}, new Action<IEdmVocabularyAnnotation>[0]);
			if (!isInline)
			{
				base.ProcessAnnotation(annotation);
			}
			this.EndElement(annotation);
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x0001A79A File Offset: 0x0001899A
		protected override void ProcessStringConstantExpression(IEdmStringConstantExpression expression)
		{
			this.schemaWriter.WriteStringConstantExpressionElement(expression);
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x0001A7A8 File Offset: 0x000189A8
		protected override void ProcessBinaryConstantExpression(IEdmBinaryConstantExpression expression)
		{
			this.schemaWriter.WriteBinaryConstantExpressionElement(expression);
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x0001A7B6 File Offset: 0x000189B6
		protected override void ProcessRecordExpression(IEdmRecordExpression expression)
		{
			this.BeginElement<IEdmRecordExpression>(expression, new Action<IEdmRecordExpression>(this.schemaWriter.WriteRecordExpressionElementHeader), new Action<IEdmRecordExpression>[0]);
			base.VisitPropertyConstructors(expression.Properties);
			this.EndElement(expression);
		}

		// Token: 0x06000958 RID: 2392 RVA: 0x0001A7E9 File Offset: 0x000189E9
		protected override void ProcessLabeledExpression(IEdmLabeledExpression element)
		{
			if (element.Name == null)
			{
				base.ProcessLabeledExpression(element);
				return;
			}
			this.BeginElement<IEdmLabeledExpression>(element, new Action<IEdmLabeledExpression>(this.schemaWriter.WriteLabeledElementHeader), new Action<IEdmLabeledExpression>[0]);
			base.ProcessLabeledExpression(element);
			this.EndElement(element);
		}

		// Token: 0x06000959 RID: 2393 RVA: 0x0001A828 File Offset: 0x00018A28
		protected override void ProcessPropertyConstructor(IEdmPropertyConstructor constructor)
		{
			bool isInline = EdmModelCsdlSerializationVisitor.IsInlineExpression(constructor.Value);
			this.BeginElement<IEdmPropertyConstructor>(constructor, delegate(IEdmPropertyConstructor t)
			{
				this.schemaWriter.WritePropertyConstructorElementHeader(t, isInline);
			}, new Action<IEdmPropertyConstructor>[0]);
			if (!isInline)
			{
				base.ProcessPropertyConstructor(constructor);
			}
			this.EndElement(constructor);
		}

		// Token: 0x0600095A RID: 2394 RVA: 0x0001A882 File Offset: 0x00018A82
		protected override void ProcessPathExpression(IEdmPathExpression expression)
		{
			this.schemaWriter.WritePathExpressionElement(expression);
		}

		// Token: 0x0600095B RID: 2395 RVA: 0x0001A890 File Offset: 0x00018A90
		protected override void ProcessPropertyPathExpression(IEdmPathExpression expression)
		{
			this.schemaWriter.WritePropertyPathExpressionElement(expression);
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x0001A89E File Offset: 0x00018A9E
		protected override void ProcessNavigationPropertyPathExpression(IEdmPathExpression expression)
		{
			this.schemaWriter.WriteNavigationPropertyPathExpressionElement(expression);
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x0001A8AC File Offset: 0x00018AAC
		protected override void ProcessCollectionExpression(IEdmCollectionExpression expression)
		{
			this.BeginElement<IEdmCollectionExpression>(expression, new Action<IEdmCollectionExpression>(this.schemaWriter.WriteCollectionExpressionElementHeader), new Action<IEdmCollectionExpression>[0]);
			base.VisitExpressions(expression.Elements);
			this.EndElement(expression);
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x0001A8E0 File Offset: 0x00018AE0
		protected override void ProcessIsTypeExpression(IEdmIsTypeExpression expression)
		{
			bool inlineType = EdmModelCsdlSerializationVisitor.IsInlineType(expression.Type);
			this.BeginElement<IEdmIsTypeExpression>(expression, delegate(IEdmIsTypeExpression t)
			{
				this.schemaWriter.WriteIsTypeExpressionElementHeader(t, inlineType);
			}, new Action<IEdmIsTypeExpression>[]
			{
				delegate(IEdmIsTypeExpression e)
				{
					this.ProcessFacets(e.Type, inlineType);
				}
			});
			if (!inlineType)
			{
				base.VisitTypeReference(expression.Type);
			}
			base.VisitExpression(expression.Operand);
			this.EndElement(expression);
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x0001A95A File Offset: 0x00018B5A
		protected override void ProcessIntegerConstantExpression(IEdmIntegerConstantExpression expression)
		{
			this.schemaWriter.WriteIntegerConstantExpressionElement(expression);
		}

		// Token: 0x06000960 RID: 2400 RVA: 0x0001A968 File Offset: 0x00018B68
		protected override void ProcessIfExpression(IEdmIfExpression expression)
		{
			this.BeginElement<IEdmIfExpression>(expression, new Action<IEdmIfExpression>(this.schemaWriter.WriteIfExpressionElementHeader), new Action<IEdmIfExpression>[0]);
			base.ProcessIfExpression(expression);
			this.EndElement(expression);
		}

		// Token: 0x06000961 RID: 2401 RVA: 0x0001A996 File Offset: 0x00018B96
		protected override void ProcessFunctionApplicationExpression(IEdmApplyExpression expression)
		{
			this.BeginElement<IEdmApplyExpression>(expression, delegate(IEdmApplyExpression e)
			{
				this.schemaWriter.WriteFunctionApplicationElementHeader(e);
			}, new Action<IEdmApplyExpression>[0]);
			base.VisitExpressions(expression.Arguments);
			this.EndElement(expression);
		}

		// Token: 0x06000962 RID: 2402 RVA: 0x0001A9C4 File Offset: 0x00018BC4
		protected override void ProcessFloatingConstantExpression(IEdmFloatingConstantExpression expression)
		{
			this.schemaWriter.WriteFloatingConstantExpressionElement(expression);
		}

		// Token: 0x06000963 RID: 2403 RVA: 0x0001A9D2 File Offset: 0x00018BD2
		protected override void ProcessGuidConstantExpression(IEdmGuidConstantExpression expression)
		{
			this.schemaWriter.WriteGuidConstantExpressionElement(expression);
		}

		// Token: 0x06000964 RID: 2404 RVA: 0x0001A9E0 File Offset: 0x00018BE0
		protected override void ProcessEnumMemberExpression(IEdmEnumMemberExpression expression)
		{
			this.schemaWriter.WriteEnumMemberExpressionElement(expression);
		}

		// Token: 0x06000965 RID: 2405 RVA: 0x0001A9EE File Offset: 0x00018BEE
		protected override void ProcessDecimalConstantExpression(IEdmDecimalConstantExpression expression)
		{
			this.schemaWriter.WriteDecimalConstantExpressionElement(expression);
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x0001A9FC File Offset: 0x00018BFC
		protected override void ProcessDateConstantExpression(IEdmDateConstantExpression expression)
		{
			this.schemaWriter.WriteDateConstantExpressionElement(expression);
		}

		// Token: 0x06000967 RID: 2407 RVA: 0x0001AA0A File Offset: 0x00018C0A
		protected override void ProcessDateTimeOffsetConstantExpression(IEdmDateTimeOffsetConstantExpression expression)
		{
			this.schemaWriter.WriteDateTimeOffsetConstantExpressionElement(expression);
		}

		// Token: 0x06000968 RID: 2408 RVA: 0x0001AA18 File Offset: 0x00018C18
		protected override void ProcessDurationConstantExpression(IEdmDurationConstantExpression expression)
		{
			this.schemaWriter.WriteDurationConstantExpressionElement(expression);
		}

		// Token: 0x06000969 RID: 2409 RVA: 0x0001AA26 File Offset: 0x00018C26
		protected override void ProcessTimeOfDayConstantExpression(IEdmTimeOfDayConstantExpression expression)
		{
			this.schemaWriter.WriteTimeOfDayConstantExpressionElement(expression);
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x0001AA34 File Offset: 0x00018C34
		protected override void ProcessBooleanConstantExpression(IEdmBooleanConstantExpression expression)
		{
			this.schemaWriter.WriteBooleanConstantExpressionElement(expression);
		}

		// Token: 0x0600096B RID: 2411 RVA: 0x0001AA42 File Offset: 0x00018C42
		protected override void ProcessNullConstantExpression(IEdmNullExpression expression)
		{
			this.schemaWriter.WriteNullConstantExpressionElement(expression);
		}

		// Token: 0x0600096C RID: 2412 RVA: 0x0001AA50 File Offset: 0x00018C50
		protected override void ProcessCastExpression(IEdmCastExpression expression)
		{
			bool inlineType = EdmModelCsdlSerializationVisitor.IsInlineType(expression.Type);
			this.BeginElement<IEdmCastExpression>(expression, delegate(IEdmCastExpression t)
			{
				this.schemaWriter.WriteCastExpressionElementHeader(t, inlineType);
			}, new Action<IEdmCastExpression>[]
			{
				delegate(IEdmCastExpression e)
				{
					this.ProcessFacets(e.Type, inlineType);
				}
			});
			if (!inlineType)
			{
				base.VisitTypeReference(expression.Type);
			}
			base.VisitExpression(expression.Operand);
			this.EndElement(expression);
		}

		// Token: 0x0600096D RID: 2413 RVA: 0x0001AACA File Offset: 0x00018CCA
		private static bool IsInlineType(IEdmTypeReference reference)
		{
			return reference.Definition is IEdmSchemaElement || reference.IsEntityReference() || (reference.IsCollection() && reference.AsCollection().CollectionDefinition().ElementType.Definition is IEdmSchemaElement);
		}

		// Token: 0x0600096E RID: 2414 RVA: 0x0001AB0C File Offset: 0x00018D0C
		private static bool IsInlineExpression(IEdmExpression expression)
		{
			switch (expression.ExpressionKind)
			{
			case EdmExpressionKind.BinaryConstant:
			case EdmExpressionKind.BooleanConstant:
			case EdmExpressionKind.DateTimeOffsetConstant:
			case EdmExpressionKind.DecimalConstant:
			case EdmExpressionKind.FloatingConstant:
			case EdmExpressionKind.GuidConstant:
			case EdmExpressionKind.IntegerConstant:
			case EdmExpressionKind.StringConstant:
			case EdmExpressionKind.DurationConstant:
			case EdmExpressionKind.Path:
			case EdmExpressionKind.PropertyPath:
			case EdmExpressionKind.NavigationPropertyPath:
			case EdmExpressionKind.DateConstant:
			case EdmExpressionKind.TimeOfDayConstant:
				return true;
			}
			return false;
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x0001AB8C File Offset: 0x00018D8C
		private static string GetEntitySetString(IEdmOperationImport operationImport)
		{
			if (operationImport.EntitySet != null)
			{
				IEdmPathExpression edmPathExpression = operationImport.EntitySet as IEdmPathExpression;
				if (edmPathExpression != null)
				{
					return EdmModelCsdlSchemaWriter.PathAsXml(edmPathExpression.PathSegments);
				}
			}
			return null;
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x0001ABC0 File Offset: 0x00018DC0
		private void ProcessOperation<TOperation>(TOperation operation, Action<TOperation> writeElementAction) where TOperation : IEdmOperation
		{
			this.BeginElement<TOperation>(operation, writeElementAction, new Action<TOperation>[0]);
			base.VisitOperationParameters(operation.Parameters);
			IEdmOperationReturn @return = operation.GetReturn();
			this.ProcessOperationReturn(@return);
			this.EndElement(operation);
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x0001AC10 File Offset: 0x00018E10
		private void ProcessReferentialConstraint(IEdmReferentialConstraint element)
		{
			if (element != null)
			{
				foreach (EdmReferentialConstraintPropertyPair edmReferentialConstraintPropertyPair in element.PropertyPairs)
				{
					this.schemaWriter.WriteReferentialConstraintPair(edmReferentialConstraintPropertyPair);
				}
			}
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x0001AC68 File Offset: 0x00018E68
		private void ProcessFacets(IEdmTypeReference element, bool inlineType)
		{
			if (element != null)
			{
				if (element.IsEntityReference())
				{
					return;
				}
				if (inlineType)
				{
					if (element.TypeKind() == EdmTypeKind.Collection)
					{
						IEdmCollectionTypeReference edmCollectionTypeReference = element.AsCollection();
						this.schemaWriter.WriteNullableAttribute(edmCollectionTypeReference.CollectionDefinition().ElementType);
						base.VisitTypeReference(edmCollectionTypeReference.CollectionDefinition().ElementType);
						return;
					}
					this.schemaWriter.WriteNullableAttribute(element);
					base.VisitTypeReference(element);
				}
			}
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x0001ACCF File Offset: 0x00018ECF
		private void VisitEntityTypeDeclaredKey(IEnumerable<IEdmStructuralProperty> keyProperties)
		{
			this.schemaWriter.WriteDelaredKeyPropertiesElementHeader();
			this.VisitPropertyRefs(keyProperties);
			this.schemaWriter.WriteEndElement();
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x0001ACF0 File Offset: 0x00018EF0
		private void VisitPropertyRefs(IEnumerable<IEdmStructuralProperty> properties)
		{
			foreach (IEdmStructuralProperty edmStructuralProperty in properties)
			{
				this.schemaWriter.WritePropertyRefElement(edmStructuralProperty);
			}
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x0001AD40 File Offset: 0x00018F40
		private void VisitAttributeAnnotations(IEnumerable<IEdmDirectValueAnnotation> annotations)
		{
			foreach (IEdmDirectValueAnnotation edmDirectValueAnnotation in annotations)
			{
				if (edmDirectValueAnnotation.NamespaceUri != "http://schemas.microsoft.com/ado/2011/04/edm/internal")
				{
					IEdmValue edmValue = edmDirectValueAnnotation.Value as IEdmValue;
					if (edmValue != null && !edmValue.IsSerializedAsElement(this.Model) && edmValue.Type.TypeKind() == EdmTypeKind.Primitive)
					{
						this.ProcessAttributeAnnotation(edmDirectValueAnnotation);
					}
				}
			}
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x0001ADC8 File Offset: 0x00018FC8
		private void VisitPrimitiveElementAnnotations(IEnumerable<IEdmDirectValueAnnotation> annotations)
		{
			foreach (IEdmDirectValueAnnotation edmDirectValueAnnotation in annotations)
			{
				if (edmDirectValueAnnotation.NamespaceUri != "http://schemas.microsoft.com/ado/2011/04/edm/internal")
				{
					IEdmValue edmValue = edmDirectValueAnnotation.Value as IEdmValue;
					if (edmValue != null && edmValue.IsSerializedAsElement(this.Model) && edmValue.Type.TypeKind() == EdmTypeKind.Primitive)
					{
						this.ProcessElementAnnotation(edmDirectValueAnnotation);
					}
				}
			}
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x0001AE50 File Offset: 0x00019050
		private void ProcessAttributeAnnotation(IEdmDirectValueAnnotation annotation)
		{
			this.schemaWriter.WriteAnnotationStringAttribute(annotation);
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x0001AE5E File Offset: 0x0001905E
		private void ProcessElementAnnotation(IEdmDirectValueAnnotation annotation)
		{
			this.schemaWriter.WriteAnnotationStringElement(annotation);
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x0001AE6C File Offset: 0x0001906C
		private void VisitElementVocabularyAnnotations(IEnumerable<IEdmVocabularyAnnotation> annotations)
		{
			foreach (IEdmVocabularyAnnotation edmVocabularyAnnotation in annotations)
			{
				this.ProcessAnnotation(edmVocabularyAnnotation);
			}
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x0001AEB4 File Offset: 0x000190B4
		private void BeginElement<TElement>(TElement element, Action<TElement> elementHeaderWriter, params Action<TElement>[] additionalAttributeWriters) where TElement : IEdmElement
		{
			elementHeaderWriter(element);
			if (additionalAttributeWriters != null)
			{
				foreach (Action<TElement> action in additionalAttributeWriters)
				{
					action(element);
				}
			}
			this.VisitAttributeAnnotations(this.Model.DirectValueAnnotations(element));
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x0001AF00 File Offset: 0x00019100
		private void EndElement(IEdmElement element)
		{
			this.VisitPrimitiveElementAnnotations(this.Model.DirectValueAnnotations(element));
			IEdmVocabularyAnnotatable edmVocabularyAnnotatable = element as IEdmVocabularyAnnotatable;
			if (edmVocabularyAnnotatable != null)
			{
				this.VisitElementVocabularyAnnotations(from a in this.Model.FindDeclaredVocabularyAnnotations(edmVocabularyAnnotatable)
					where a.IsInline(this.Model)
					select a);
			}
			this.schemaWriter.WriteEndElement();
		}

		// Token: 0x040005C7 RID: 1479
		private readonly Version edmVersion;

		// Token: 0x040005C8 RID: 1480
		private readonly EdmModelCsdlSchemaWriter schemaWriter;

		// Token: 0x040005C9 RID: 1481
		private readonly List<IEdmNavigationProperty> navigationProperties = new List<IEdmNavigationProperty>();

		// Token: 0x040005CA RID: 1482
		private readonly VersioningDictionary<string, string> namespaceAliasMappings;
	}
}
