using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.Serialization
{
	// Token: 0x02000151 RID: 337
	internal sealed class EdmModelCsdlSerializationVisitor : EdmModelVisitor
	{
		// Token: 0x06000898 RID: 2200 RVA: 0x0001816C File Offset: 0x0001636C
		internal EdmModelCsdlSerializationVisitor(IEdmModel model, XmlWriter xmlWriter, Version edmVersion)
			: base(model)
		{
			this.edmVersion = edmVersion;
			this.namespaceAliasMappings = model.GetNamespaceAliases();
			this.schemaWriter = new EdmModelCsdlSchemaWriter(model, this.namespaceAliasMappings, xmlWriter, this.edmVersion);
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x000181AC File Offset: 0x000163AC
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

		// Token: 0x0600089A RID: 2202 RVA: 0x00018318 File Offset: 0x00016518
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

		// Token: 0x0600089B RID: 2203 RVA: 0x000183E4 File Offset: 0x000165E4
		protected override void ProcessEntityContainer(IEdmEntityContainer element)
		{
			this.BeginElement<IEdmEntityContainer>(element, new Action<IEdmEntityContainer>(this.schemaWriter.WriteEntityContainerElementHeader), new Action<IEdmEntityContainer>[0]);
			base.ProcessEntityContainer(element);
			this.EndElement(element);
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x00018414 File Offset: 0x00016614
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

		// Token: 0x0600089D RID: 2205 RVA: 0x00018494 File Offset: 0x00016694
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

		// Token: 0x0600089E RID: 2206 RVA: 0x00018514 File Offset: 0x00016714
		protected override void ProcessEntityType(IEdmEntityType element)
		{
			this.BeginElement<IEdmEntityType>(element, new Action<IEdmEntityType>(this.schemaWriter.WriteEntityTypeElementHeader), new Action<IEdmEntityType>[0]);
			if (element.DeclaredKey != null && Enumerable.Any<IEdmStructuralProperty>(element.DeclaredKey))
			{
				this.VisitEntityTypeDeclaredKey(element.DeclaredKey);
			}
			base.VisitProperties(Enumerable.Cast<IEdmProperty>(element.DeclaredStructuralProperties()));
			base.VisitProperties(Enumerable.Cast<IEdmProperty>(element.DeclaredNavigationProperties()));
			this.EndElement(element);
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x0001858C File Offset: 0x0001678C
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

		// Token: 0x060008A0 RID: 2208 RVA: 0x000185FA File Offset: 0x000167FA
		protected override void ProcessTypeDefinitionReference(IEdmTypeDefinitionReference element)
		{
			this.schemaWriter.WriteTypeDefinitionAttributes(element);
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x00018608 File Offset: 0x00016808
		protected override void ProcessBinaryTypeReference(IEdmBinaryTypeReference element)
		{
			this.schemaWriter.WriteBinaryTypeAttributes(element);
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x00018616 File Offset: 0x00016816
		protected override void ProcessDecimalTypeReference(IEdmDecimalTypeReference element)
		{
			this.schemaWriter.WriteDecimalTypeAttributes(element);
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x00018624 File Offset: 0x00016824
		protected override void ProcessSpatialTypeReference(IEdmSpatialTypeReference element)
		{
			this.schemaWriter.WriteSpatialTypeAttributes(element);
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x00018632 File Offset: 0x00016832
		protected override void ProcessStringTypeReference(IEdmStringTypeReference element)
		{
			this.schemaWriter.WriteStringTypeAttributes(element);
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x00018640 File Offset: 0x00016840
		protected override void ProcessTemporalTypeReference(IEdmTemporalTypeReference element)
		{
			this.schemaWriter.WriteTemporalTypeAttributes(element);
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x00018650 File Offset: 0x00016850
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

		// Token: 0x060008A7 RID: 2215 RVA: 0x000186B8 File Offset: 0x000168B8
		protected override void ProcessComplexType(IEdmComplexType element)
		{
			this.BeginElement<IEdmComplexType>(element, new Action<IEdmComplexType>(this.schemaWriter.WriteComplexTypeElementHeader), new Action<IEdmComplexType>[0]);
			base.ProcessComplexType(element);
			this.EndElement(element);
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x000186E6 File Offset: 0x000168E6
		protected override void ProcessEnumType(IEdmEnumType element)
		{
			this.BeginElement<IEdmEnumType>(element, new Action<IEdmEnumType>(this.schemaWriter.WriteEnumTypeElementHeader), new Action<IEdmEnumType>[0]);
			base.ProcessEnumType(element);
			this.EndElement(element);
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x00018714 File Offset: 0x00016914
		protected override void ProcessEnumMember(IEdmEnumMember element)
		{
			this.BeginElement<IEdmEnumMember>(element, new Action<IEdmEnumMember>(this.schemaWriter.WriteEnumMemberElementHeader), new Action<IEdmEnumMember>[0]);
			this.EndElement(element);
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x0001873B File Offset: 0x0001693B
		protected override void ProcessTypeDefinition(IEdmTypeDefinition element)
		{
			this.BeginElement<IEdmTypeDefinition>(element, new Action<IEdmTypeDefinition>(this.schemaWriter.WriteTypeDefinitionElementHeader), new Action<IEdmTypeDefinition>[0]);
			base.ProcessTypeDefinition(element);
			this.EndElement(element);
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x0001876C File Offset: 0x0001696C
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

		// Token: 0x060008AC RID: 2220 RVA: 0x000187ED File Offset: 0x000169ED
		protected override void ProcessAction(IEdmAction action)
		{
			this.ProcessOperation<IEdmAction>(action, new Action<IEdmAction>(this.schemaWriter.WriteActionElementHeader));
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x00018807 File Offset: 0x00016A07
		protected override void ProcessFunction(IEdmFunction function)
		{
			this.ProcessOperation<IEdmFunction>(function, new Action<IEdmFunction>(this.schemaWriter.WriteFunctionElementHeader));
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x00018824 File Offset: 0x00016A24
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
				this.VisitElementVocabularyAnnotations(Enumerable.Where<IEdmVocabularyAnnotation>(this.Model.FindDeclaredVocabularyAnnotations(element), (IEdmVocabularyAnnotation a) => a.IsInline(this.Model)));
			}
			this.schemaWriter.WriteOperationParameterEndElement(element);
		}

		// Token: 0x060008AF RID: 2223 RVA: 0x000188D4 File Offset: 0x00016AD4
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

		// Token: 0x060008B0 RID: 2224 RVA: 0x00018942 File Offset: 0x00016B42
		protected override void ProcessActionImport(IEdmActionImport actionImport)
		{
			this.BeginElement<IEdmActionImport>(actionImport, new Action<IEdmActionImport>(this.schemaWriter.WriteActionImportElementHeader), new Action<IEdmActionImport>[0]);
			this.EndElement(actionImport);
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x00018969 File Offset: 0x00016B69
		protected override void ProcessFunctionImport(IEdmFunctionImport functionImport)
		{
			this.BeginElement<IEdmFunctionImport>(functionImport, new Action<IEdmFunctionImport>(this.schemaWriter.WriteFunctionImportElementHeader), new Action<IEdmFunctionImport>[0]);
			this.EndElement(functionImport);
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x00018990 File Offset: 0x00016B90
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

		// Token: 0x060008B3 RID: 2227 RVA: 0x000189EA File Offset: 0x00016BEA
		protected override void ProcessStringConstantExpression(IEdmStringConstantExpression expression)
		{
			this.schemaWriter.WriteStringConstantExpressionElement(expression);
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x000189F8 File Offset: 0x00016BF8
		protected override void ProcessBinaryConstantExpression(IEdmBinaryConstantExpression expression)
		{
			this.schemaWriter.WriteBinaryConstantExpressionElement(expression);
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x00018A06 File Offset: 0x00016C06
		protected override void ProcessRecordExpression(IEdmRecordExpression expression)
		{
			this.BeginElement<IEdmRecordExpression>(expression, new Action<IEdmRecordExpression>(this.schemaWriter.WriteRecordExpressionElementHeader), new Action<IEdmRecordExpression>[0]);
			base.VisitPropertyConstructors(expression.Properties);
			this.EndElement(expression);
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x00018A39 File Offset: 0x00016C39
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

		// Token: 0x060008B7 RID: 2231 RVA: 0x00018A78 File Offset: 0x00016C78
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

		// Token: 0x060008B8 RID: 2232 RVA: 0x00018AD2 File Offset: 0x00016CD2
		protected override void ProcessPathExpression(IEdmPathExpression expression)
		{
			this.schemaWriter.WritePathExpressionElement(expression);
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x00018AE0 File Offset: 0x00016CE0
		protected override void ProcessPropertyPathExpression(IEdmPathExpression expression)
		{
			this.schemaWriter.WritePropertyPathExpressionElement(expression);
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x00018AEE File Offset: 0x00016CEE
		protected override void ProcessNavigationPropertyPathExpression(IEdmPathExpression expression)
		{
			this.schemaWriter.WriteNavigationPropertyPathExpressionElement(expression);
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x00018AFC File Offset: 0x00016CFC
		protected override void ProcessCollectionExpression(IEdmCollectionExpression expression)
		{
			this.BeginElement<IEdmCollectionExpression>(expression, new Action<IEdmCollectionExpression>(this.schemaWriter.WriteCollectionExpressionElementHeader), new Action<IEdmCollectionExpression>[0]);
			base.VisitExpressions(expression.Elements);
			this.EndElement(expression);
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x00018B30 File Offset: 0x00016D30
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

		// Token: 0x060008BD RID: 2237 RVA: 0x00018BAA File Offset: 0x00016DAA
		protected override void ProcessIntegerConstantExpression(IEdmIntegerConstantExpression expression)
		{
			this.schemaWriter.WriteIntegerConstantExpressionElement(expression);
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x00018BB8 File Offset: 0x00016DB8
		protected override void ProcessIfExpression(IEdmIfExpression expression)
		{
			this.BeginElement<IEdmIfExpression>(expression, new Action<IEdmIfExpression>(this.schemaWriter.WriteIfExpressionElementHeader), new Action<IEdmIfExpression>[0]);
			base.ProcessIfExpression(expression);
			this.EndElement(expression);
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x00018BE6 File Offset: 0x00016DE6
		protected override void ProcessFunctionApplicationExpression(IEdmApplyExpression expression)
		{
			this.BeginElement<IEdmApplyExpression>(expression, delegate(IEdmApplyExpression e)
			{
				this.schemaWriter.WriteFunctionApplicationElementHeader(e);
			}, new Action<IEdmApplyExpression>[0]);
			base.VisitExpressions(expression.Arguments);
			this.EndElement(expression);
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x00018C14 File Offset: 0x00016E14
		protected override void ProcessFloatingConstantExpression(IEdmFloatingConstantExpression expression)
		{
			this.schemaWriter.WriteFloatingConstantExpressionElement(expression);
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x00018C22 File Offset: 0x00016E22
		protected override void ProcessGuidConstantExpression(IEdmGuidConstantExpression expression)
		{
			this.schemaWriter.WriteGuidConstantExpressionElement(expression);
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x00018C30 File Offset: 0x00016E30
		protected override void ProcessEnumMemberExpression(IEdmEnumMemberExpression expression)
		{
			this.schemaWriter.WriteEnumMemberExpressionElement(expression);
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x00018C3E File Offset: 0x00016E3E
		protected override void ProcessDecimalConstantExpression(IEdmDecimalConstantExpression expression)
		{
			this.schemaWriter.WriteDecimalConstantExpressionElement(expression);
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x00018C4C File Offset: 0x00016E4C
		protected override void ProcessDateConstantExpression(IEdmDateConstantExpression expression)
		{
			this.schemaWriter.WriteDateConstantExpressionElement(expression);
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x00018C5A File Offset: 0x00016E5A
		protected override void ProcessDateTimeOffsetConstantExpression(IEdmDateTimeOffsetConstantExpression expression)
		{
			this.schemaWriter.WriteDateTimeOffsetConstantExpressionElement(expression);
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x00018C68 File Offset: 0x00016E68
		protected override void ProcessDurationConstantExpression(IEdmDurationConstantExpression expression)
		{
			this.schemaWriter.WriteDurationConstantExpressionElement(expression);
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x00018C76 File Offset: 0x00016E76
		protected override void ProcessTimeOfDayConstantExpression(IEdmTimeOfDayConstantExpression expression)
		{
			this.schemaWriter.WriteTimeOfDayConstantExpressionElement(expression);
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x00018C84 File Offset: 0x00016E84
		protected override void ProcessBooleanConstantExpression(IEdmBooleanConstantExpression expression)
		{
			this.schemaWriter.WriteBooleanConstantExpressionElement(expression);
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x00018C92 File Offset: 0x00016E92
		protected override void ProcessNullConstantExpression(IEdmNullExpression expression)
		{
			this.schemaWriter.WriteNullConstantExpressionElement(expression);
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x00018CA0 File Offset: 0x00016EA0
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

		// Token: 0x060008CB RID: 2251 RVA: 0x00018D1A File Offset: 0x00016F1A
		private static bool IsInlineType(IEdmTypeReference reference)
		{
			return reference.Definition is IEdmSchemaElement || reference.IsEntityReference() || (reference.IsCollection() && reference.AsCollection().CollectionDefinition().ElementType.Definition is IEdmSchemaElement);
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x00018D5C File Offset: 0x00016F5C
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

		// Token: 0x060008CD RID: 2253 RVA: 0x00018DDC File Offset: 0x00016FDC
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

		// Token: 0x060008CE RID: 2254 RVA: 0x00018E10 File Offset: 0x00017010
		private void ProcessOperation<TOperation>(TOperation operation, Action<TOperation> writeElementAction) where TOperation : IEdmOperation
		{
			this.BeginElement<TOperation>(operation, writeElementAction, new Action<TOperation>[0]);
			base.VisitOperationParameters(operation.Parameters);
			this.ProcessOperationReturnType(operation.ReturnType);
			this.EndElement(operation);
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x00018E60 File Offset: 0x00017060
		private void ProcessOperationReturnType(IEdmTypeReference operationReturnType)
		{
			if (operationReturnType == null)
			{
				return;
			}
			bool inlineType = EdmModelCsdlSerializationVisitor.IsInlineType(operationReturnType);
			this.BeginElement<IEdmTypeReference>(operationReturnType, delegate(IEdmTypeReference type)
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
			this.EndElement(operationReturnType);
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x00018EBC File Offset: 0x000170BC
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

		// Token: 0x060008D1 RID: 2257 RVA: 0x00018F14 File Offset: 0x00017114
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

		// Token: 0x060008D2 RID: 2258 RVA: 0x00018F7B File Offset: 0x0001717B
		private void VisitEntityTypeDeclaredKey(IEnumerable<IEdmStructuralProperty> keyProperties)
		{
			this.schemaWriter.WriteDelaredKeyPropertiesElementHeader();
			this.VisitPropertyRefs(keyProperties);
			this.schemaWriter.WriteEndElement();
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x00018F9C File Offset: 0x0001719C
		private void VisitPropertyRefs(IEnumerable<IEdmStructuralProperty> properties)
		{
			foreach (IEdmStructuralProperty edmStructuralProperty in properties)
			{
				this.schemaWriter.WritePropertyRefElement(edmStructuralProperty);
			}
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x00018FEC File Offset: 0x000171EC
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

		// Token: 0x060008D5 RID: 2261 RVA: 0x00019074 File Offset: 0x00017274
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

		// Token: 0x060008D6 RID: 2262 RVA: 0x000190FC File Offset: 0x000172FC
		private void ProcessAttributeAnnotation(IEdmDirectValueAnnotation annotation)
		{
			this.schemaWriter.WriteAnnotationStringAttribute(annotation);
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x0001910A File Offset: 0x0001730A
		private void ProcessElementAnnotation(IEdmDirectValueAnnotation annotation)
		{
			this.schemaWriter.WriteAnnotationStringElement(annotation);
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x00019118 File Offset: 0x00017318
		private void VisitElementVocabularyAnnotations(IEnumerable<IEdmVocabularyAnnotation> annotations)
		{
			foreach (IEdmVocabularyAnnotation edmVocabularyAnnotation in annotations)
			{
				this.ProcessAnnotation(edmVocabularyAnnotation);
			}
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x00019160 File Offset: 0x00017360
		private void BeginElement<TElement>(TElement element, Action<TElement> elementHeaderWriter, params Action<TElement>[] additionalAttributeWriters) where TElement : IEdmElement
		{
			elementHeaderWriter.Invoke(element);
			if (additionalAttributeWriters != null)
			{
				foreach (Action<TElement> action in additionalAttributeWriters)
				{
					action.Invoke(element);
				}
			}
			this.VisitAttributeAnnotations(this.Model.DirectValueAnnotations(element));
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x000191AC File Offset: 0x000173AC
		private void EndElement(IEdmElement element)
		{
			this.VisitPrimitiveElementAnnotations(this.Model.DirectValueAnnotations(element));
			IEdmVocabularyAnnotatable edmVocabularyAnnotatable = element as IEdmVocabularyAnnotatable;
			if (edmVocabularyAnnotatable != null)
			{
				this.VisitElementVocabularyAnnotations(Enumerable.Where<IEdmVocabularyAnnotation>(this.Model.FindDeclaredVocabularyAnnotations(edmVocabularyAnnotatable), (IEdmVocabularyAnnotation a) => a.IsInline(this.Model)));
			}
			this.schemaWriter.WriteEndElement();
		}

		// Token: 0x0400055D RID: 1373
		private readonly Version edmVersion;

		// Token: 0x0400055E RID: 1374
		private readonly EdmModelCsdlSchemaWriter schemaWriter;

		// Token: 0x0400055F RID: 1375
		private readonly List<IEdmNavigationProperty> navigationProperties = new List<IEdmNavigationProperty>();

		// Token: 0x04000560 RID: 1376
		private readonly VersioningDictionary<string, string> namespaceAliasMappings;
	}
}
