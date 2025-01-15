using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Microsoft.OData.Edm.Annotations;
using Microsoft.OData.Edm.Expressions;
using Microsoft.OData.Edm.Values;

namespace Microsoft.OData.Edm.Csdl.Serialization
{
	// Token: 0x020001E0 RID: 480
	internal sealed class EdmModelCsdlSerializationVisitor : EdmModelVisitor
	{
		// Token: 0x06000A7B RID: 2683 RVA: 0x0001C2F5 File Offset: 0x0001A4F5
		internal EdmModelCsdlSerializationVisitor(IEdmModel model, XmlWriter xmlWriter, Version edmVersion)
			: base(model)
		{
			this.edmVersion = edmVersion;
			this.namespaceAliasMappings = model.GetNamespaceAliases();
			this.schemaWriter = new EdmModelCsdlSchemaWriter(model, this.namespaceAliasMappings, xmlWriter, this.edmVersion);
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x0001C338 File Offset: 0x0001A538
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

		// Token: 0x06000A7D RID: 2685 RVA: 0x0001C4A0 File Offset: 0x0001A6A0
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

		// Token: 0x06000A7E RID: 2686 RVA: 0x0001C56C File Offset: 0x0001A76C
		protected override void ProcessEntityContainer(IEdmEntityContainer element)
		{
			this.BeginElement<IEdmEntityContainer>(element, new Action<IEdmEntityContainer>(this.schemaWriter.WriteEntityContainerElementHeader), new Action<IEdmEntityContainer>[0]);
			base.ProcessEntityContainer(element);
			this.EndElement(element);
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x0001C59C File Offset: 0x0001A79C
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

		// Token: 0x06000A80 RID: 2688 RVA: 0x0001C61C File Offset: 0x0001A81C
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

		// Token: 0x06000A81 RID: 2689 RVA: 0x0001C69C File Offset: 0x0001A89C
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

		// Token: 0x06000A82 RID: 2690 RVA: 0x0001C74C File Offset: 0x0001A94C
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

		// Token: 0x06000A83 RID: 2691 RVA: 0x0001C7BC File Offset: 0x0001A9BC
		protected override void ProcessBinaryTypeReference(IEdmBinaryTypeReference element)
		{
			this.schemaWriter.WriteBinaryTypeAttributes(element);
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x0001C7CA File Offset: 0x0001A9CA
		protected override void ProcessDecimalTypeReference(IEdmDecimalTypeReference element)
		{
			this.schemaWriter.WriteDecimalTypeAttributes(element);
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x0001C7D8 File Offset: 0x0001A9D8
		protected override void ProcessSpatialTypeReference(IEdmSpatialTypeReference element)
		{
			this.schemaWriter.WriteSpatialTypeAttributes(element);
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x0001C7E6 File Offset: 0x0001A9E6
		protected override void ProcessStringTypeReference(IEdmStringTypeReference element)
		{
			this.schemaWriter.WriteStringTypeAttributes(element);
		}

		// Token: 0x06000A87 RID: 2695 RVA: 0x0001C7F4 File Offset: 0x0001A9F4
		protected override void ProcessTemporalTypeReference(IEdmTemporalTypeReference element)
		{
			this.schemaWriter.WriteTemporalTypeAttributes(element);
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x0001C804 File Offset: 0x0001AA04
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

		// Token: 0x06000A89 RID: 2697 RVA: 0x0001C86C File Offset: 0x0001AA6C
		protected override void ProcessComplexType(IEdmComplexType element)
		{
			this.BeginElement<IEdmComplexType>(element, new Action<IEdmComplexType>(this.schemaWriter.WriteComplexTypeElementHeader), new Action<IEdmComplexType>[0]);
			base.ProcessComplexType(element);
			this.EndElement(element);
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x0001C89A File Offset: 0x0001AA9A
		protected override void ProcessEnumType(IEdmEnumType element)
		{
			this.BeginElement<IEdmEnumType>(element, new Action<IEdmEnumType>(this.schemaWriter.WriteEnumTypeElementHeader), new Action<IEdmEnumType>[0]);
			base.ProcessEnumType(element);
			this.EndElement(element);
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x0001C8C8 File Offset: 0x0001AAC8
		protected override void ProcessEnumMember(IEdmEnumMember element)
		{
			this.BeginElement<IEdmEnumMember>(element, new Action<IEdmEnumMember>(this.schemaWriter.WriteEnumMemberElementHeader), new Action<IEdmEnumMember>[0]);
			this.EndElement(element);
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x0001C8EF File Offset: 0x0001AAEF
		protected override void ProcessTypeDefinition(IEdmTypeDefinition element)
		{
			this.BeginElement<IEdmTypeDefinition>(element, new Action<IEdmTypeDefinition>(this.schemaWriter.WriteTypeDefinitionElementHeader), new Action<IEdmTypeDefinition>[0]);
			base.ProcessTypeDefinition(element);
			this.EndElement(element);
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x0001C958 File Offset: 0x0001AB58
		protected override void ProcessValueTerm(IEdmValueTerm term)
		{
			bool inlineType = term.Type != null && EdmModelCsdlSerializationVisitor.IsInlineType(term.Type);
			this.BeginElement<IEdmValueTerm>(term, delegate(IEdmValueTerm t)
			{
				this.schemaWriter.WriteValueTermElementHeader(t, inlineType);
			}, new Action<IEdmValueTerm>[]
			{
				delegate(IEdmValueTerm e)
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

		// Token: 0x06000A8E RID: 2702 RVA: 0x0001C9DB File Offset: 0x0001ABDB
		protected override void ProcessAction(IEdmAction action)
		{
			this.ProcessOperation<IEdmAction>(action, new Action<IEdmAction>(this.schemaWriter.WriteActionElementHeader));
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x0001C9F5 File Offset: 0x0001ABF5
		protected override void ProcessFunction(IEdmFunction function)
		{
			this.ProcessOperation<IEdmFunction>(function, new Action<IEdmFunction>(this.schemaWriter.WriteFunctionElementHeader));
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x0001CA4C File Offset: 0x0001AC4C
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
			this.EndElement(element);
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x0001CAF8 File Offset: 0x0001ACF8
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

		// Token: 0x06000A92 RID: 2706 RVA: 0x0001CB68 File Offset: 0x0001AD68
		protected override void ProcessActionImport(IEdmActionImport actionImport)
		{
			this.BeginElement<IEdmActionImport>(actionImport, new Action<IEdmActionImport>(this.schemaWriter.WriteActionImportElementHeader), new Action<IEdmActionImport>[0]);
			this.EndElement(actionImport);
		}

		// Token: 0x06000A93 RID: 2707 RVA: 0x0001CB8F File Offset: 0x0001AD8F
		protected override void ProcessFunctionImport(IEdmFunctionImport functionImport)
		{
			this.BeginElement<IEdmFunctionImport>(functionImport, new Action<IEdmFunctionImport>(this.schemaWriter.WriteFunctionImportElementHeader), new Action<IEdmFunctionImport>[0]);
			this.EndElement(functionImport);
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x0001CBD8 File Offset: 0x0001ADD8
		protected override void ProcessAnnotation(IEdmValueAnnotation annotation)
		{
			bool isInline = EdmModelCsdlSerializationVisitor.IsInlineExpression(annotation.Value);
			this.BeginElement<IEdmValueAnnotation>(annotation, delegate(IEdmValueAnnotation t)
			{
				this.schemaWriter.WriteValueAnnotationElementHeader(t, isInline);
			}, new Action<IEdmValueAnnotation>[0]);
			if (!isInline)
			{
				base.ProcessAnnotation(annotation);
			}
			this.EndElement(annotation);
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x0001CC32 File Offset: 0x0001AE32
		protected override void ProcessStringConstantExpression(IEdmStringConstantExpression expression)
		{
			this.schemaWriter.WriteStringConstantExpressionElement(expression);
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x0001CC40 File Offset: 0x0001AE40
		protected override void ProcessBinaryConstantExpression(IEdmBinaryConstantExpression expression)
		{
			this.schemaWriter.WriteBinaryConstantExpressionElement(expression);
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x0001CC4E File Offset: 0x0001AE4E
		protected override void ProcessRecordExpression(IEdmRecordExpression expression)
		{
			this.BeginElement<IEdmRecordExpression>(expression, new Action<IEdmRecordExpression>(this.schemaWriter.WriteRecordExpressionElementHeader), new Action<IEdmRecordExpression>[0]);
			base.VisitPropertyConstructors(expression.Properties);
			this.EndElement(expression);
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x0001CC81 File Offset: 0x0001AE81
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

		// Token: 0x06000A99 RID: 2713 RVA: 0x0001CCE0 File Offset: 0x0001AEE0
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

		// Token: 0x06000A9A RID: 2714 RVA: 0x0001CD3A File Offset: 0x0001AF3A
		protected override void ProcessPropertyReferenceExpression(IEdmPropertyReferenceExpression expression)
		{
			this.BeginElement<IEdmPropertyReferenceExpression>(expression, new Action<IEdmPropertyReferenceExpression>(this.schemaWriter.WritePropertyReferenceExpressionElementHeader), new Action<IEdmPropertyReferenceExpression>[0]);
			if (expression.Base != null)
			{
				base.VisitExpression(expression.Base);
			}
			this.EndElement(expression);
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x0001CD75 File Offset: 0x0001AF75
		protected override void ProcessPathExpression(IEdmPathExpression expression)
		{
			this.schemaWriter.WritePathExpressionElement(expression);
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x0001CD83 File Offset: 0x0001AF83
		protected override void ProcessPropertyPathExpression(IEdmPathExpression expression)
		{
			this.schemaWriter.WritePropertyPathExpressionElement(expression);
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x0001CD91 File Offset: 0x0001AF91
		protected override void ProcessNavigationPropertyPathExpression(IEdmPathExpression expression)
		{
			this.schemaWriter.WriteNavigationPropertyPathExpressionElement(expression);
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x0001CD9F File Offset: 0x0001AF9F
		protected override void ProcessParameterReferenceExpression(IEdmParameterReferenceExpression expression)
		{
			this.schemaWriter.WriteParameterReferenceExpressionElement(expression);
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x0001CDAD File Offset: 0x0001AFAD
		protected override void ProcessCollectionExpression(IEdmCollectionExpression expression)
		{
			this.BeginElement<IEdmCollectionExpression>(expression, new Action<IEdmCollectionExpression>(this.schemaWriter.WriteCollectionExpressionElementHeader), new Action<IEdmCollectionExpression>[0]);
			base.VisitExpressions(expression.Elements);
			this.EndElement(expression);
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x0001CE1C File Offset: 0x0001B01C
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

		// Token: 0x06000AA1 RID: 2721 RVA: 0x0001CE98 File Offset: 0x0001B098
		protected override void ProcessIntegerConstantExpression(IEdmIntegerConstantExpression expression)
		{
			this.schemaWriter.WriteIntegerConstantExpressionElement(expression);
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x0001CEA6 File Offset: 0x0001B0A6
		protected override void ProcessIfExpression(IEdmIfExpression expression)
		{
			this.BeginElement<IEdmIfExpression>(expression, new Action<IEdmIfExpression>(this.schemaWriter.WriteIfExpressionElementHeader), new Action<IEdmIfExpression>[0]);
			base.ProcessIfExpression(expression);
			this.EndElement(expression);
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x0001CED4 File Offset: 0x0001B0D4
		protected override void ProcessOperationReferenceExpression(IEdmOperationReferenceExpression expression)
		{
			this.schemaWriter.WriteOperationReferenceExpressionElement(expression);
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x0001CF04 File Offset: 0x0001B104
		protected override void ProcessOperationApplicationExpression(IEdmApplyExpression expression)
		{
			bool isFunction = expression.AppliedOperation.ExpressionKind == EdmExpressionKind.OperationReference;
			this.BeginElement<IEdmApplyExpression>(expression, delegate(IEdmApplyExpression e)
			{
				this.schemaWriter.WriteFunctionApplicationElementHeader(e, isFunction);
			}, new Action<IEdmApplyExpression>[0]);
			if (!isFunction)
			{
				base.VisitExpression(expression.AppliedOperation);
			}
			base.VisitExpressions(expression.Arguments);
			this.EndElement(expression);
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x0001CF73 File Offset: 0x0001B173
		protected override void ProcessFloatingConstantExpression(IEdmFloatingConstantExpression expression)
		{
			this.schemaWriter.WriteFloatingConstantExpressionElement(expression);
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x0001CF81 File Offset: 0x0001B181
		protected override void ProcessGuidConstantExpression(IEdmGuidConstantExpression expression)
		{
			this.schemaWriter.WriteGuidConstantExpressionElement(expression);
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x0001CF8F File Offset: 0x0001B18F
		protected override void ProcessEnumMemberExpression(IEdmEnumMemberExpression expression)
		{
			this.schemaWriter.WriteEnumMemberExpressionElement(expression);
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x0001CF9D File Offset: 0x0001B19D
		protected override void ProcessEnumMemberReferenceExpression(IEdmEnumMemberReferenceExpression expression)
		{
			this.schemaWriter.WriteEnumMemberReferenceExpressionElement(expression);
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x0001CFAB File Offset: 0x0001B1AB
		protected override void ProcessEntitySetReferenceExpression(IEdmEntitySetReferenceExpression expression)
		{
			this.schemaWriter.WriteEntitySetReferenceExpressionElement(expression);
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x0001CFB9 File Offset: 0x0001B1B9
		protected override void ProcessDecimalConstantExpression(IEdmDecimalConstantExpression expression)
		{
			this.schemaWriter.WriteDecimalConstantExpressionElement(expression);
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x0001CFC7 File Offset: 0x0001B1C7
		protected override void ProcessDateConstantExpression(IEdmDateConstantExpression expression)
		{
			this.schemaWriter.WriteDateConstantExpressionElement(expression);
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x0001CFD5 File Offset: 0x0001B1D5
		protected override void ProcessDateTimeOffsetConstantExpression(IEdmDateTimeOffsetConstantExpression expression)
		{
			this.schemaWriter.WriteDateTimeOffsetConstantExpressionElement(expression);
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x0001CFE3 File Offset: 0x0001B1E3
		protected override void ProcessDurationConstantExpression(IEdmDurationConstantExpression expression)
		{
			this.schemaWriter.WriteDurationConstantExpressionElement(expression);
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x0001CFF1 File Offset: 0x0001B1F1
		protected override void ProcessTimeOfDayConstantExpression(IEdmTimeOfDayConstantExpression expression)
		{
			this.schemaWriter.WriteTimeOfDayConstantExpressionElement(expression);
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x0001CFFF File Offset: 0x0001B1FF
		protected override void ProcessBooleanConstantExpression(IEdmBooleanConstantExpression expression)
		{
			this.schemaWriter.WriteBooleanConstantExpressionElement(expression);
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x0001D00D File Offset: 0x0001B20D
		protected override void ProcessNullConstantExpression(IEdmNullExpression expression)
		{
			this.schemaWriter.WriteNullConstantExpressionElement(expression);
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x0001D058 File Offset: 0x0001B258
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

		// Token: 0x06000AB2 RID: 2738 RVA: 0x0001D0D4 File Offset: 0x0001B2D4
		private static bool IsInlineType(IEdmTypeReference reference)
		{
			return reference.Definition is IEdmSchemaElement || reference.IsEntityReference() || (reference.IsCollection() && reference.AsCollection().CollectionDefinition().ElementType.Definition is IEdmSchemaElement);
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x0001D114 File Offset: 0x0001B314
		private static bool IsInlineExpression(IEdmExpression expression)
		{
			EdmExpressionKind expressionKind = expression.ExpressionKind;
			switch (expressionKind)
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
				break;
			case EdmExpressionKind.Null:
			case EdmExpressionKind.Record:
			case EdmExpressionKind.Collection:
				return false;
			default:
				switch (expressionKind)
				{
				case EdmExpressionKind.PropertyPath:
				case EdmExpressionKind.NavigationPropertyPath:
				case EdmExpressionKind.DateConstant:
				case EdmExpressionKind.TimeOfDayConstant:
					break;
				default:
					return false;
				}
				break;
			}
			return true;
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x0001D184 File Offset: 0x0001B384
		private static string GetEntitySetString(IEdmOperationImport operationImport)
		{
			if (operationImport.EntitySet != null)
			{
				IEdmEntitySetReferenceExpression edmEntitySetReferenceExpression = operationImport.EntitySet as IEdmEntitySetReferenceExpression;
				if (edmEntitySetReferenceExpression != null)
				{
					return edmEntitySetReferenceExpression.ReferencedEntitySet.Name;
				}
				IEdmPathExpression edmPathExpression = operationImport.EntitySet as IEdmPathExpression;
				if (edmPathExpression != null)
				{
					return EdmModelCsdlSchemaWriter.PathAsXml(edmPathExpression.Path);
				}
			}
			return null;
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x0001D1D0 File Offset: 0x0001B3D0
		private void ProcessOperation<TOperation>(TOperation operation, Action<TOperation> writeElementAction) where TOperation : IEdmOperation
		{
			this.BeginElement<TOperation>(operation, writeElementAction, new Action<TOperation>[0]);
			base.VisitOperationParameters(operation.Parameters);
			this.ProcessOperationReturnType(operation.ReturnType);
			this.EndElement(operation);
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x0001D268 File Offset: 0x0001B468
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

		// Token: 0x06000AB7 RID: 2743 RVA: 0x0001D2C4 File Offset: 0x0001B4C4
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

		// Token: 0x06000AB8 RID: 2744 RVA: 0x0001D31C File Offset: 0x0001B51C
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

		// Token: 0x06000AB9 RID: 2745 RVA: 0x0001D383 File Offset: 0x0001B583
		private void VisitEntityTypeDeclaredKey(IEnumerable<IEdmStructuralProperty> keyProperties)
		{
			this.schemaWriter.WriteDelaredKeyPropertiesElementHeader();
			this.VisitPropertyRefs(keyProperties);
			this.schemaWriter.WriteEndElement();
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x0001D3A4 File Offset: 0x0001B5A4
		private void VisitPropertyRefs(IEnumerable<IEdmStructuralProperty> properties)
		{
			foreach (IEdmStructuralProperty edmStructuralProperty in properties)
			{
				this.schemaWriter.WritePropertyRefElement(edmStructuralProperty);
			}
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x0001D3F4 File Offset: 0x0001B5F4
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

		// Token: 0x06000ABC RID: 2748 RVA: 0x0001D47C File Offset: 0x0001B67C
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

		// Token: 0x06000ABD RID: 2749 RVA: 0x0001D504 File Offset: 0x0001B704
		private void ProcessAttributeAnnotation(IEdmDirectValueAnnotation annotation)
		{
			this.schemaWriter.WriteAnnotationStringAttribute(annotation);
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x0001D512 File Offset: 0x0001B712
		private void ProcessElementAnnotation(IEdmDirectValueAnnotation annotation)
		{
			this.schemaWriter.WriteAnnotationStringElement(annotation);
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x0001D520 File Offset: 0x0001B720
		private void VisitElementVocabularyAnnotations(IEnumerable<IEdmVocabularyAnnotation> annotations)
		{
			foreach (IEdmVocabularyAnnotation edmVocabularyAnnotation in annotations)
			{
				switch (edmVocabularyAnnotation.Term.TermKind)
				{
				case EdmTermKind.None:
					this.ProcessVocabularyAnnotation(edmVocabularyAnnotation);
					break;
				case EdmTermKind.Type:
				case EdmTermKind.Value:
					this.ProcessAnnotation((IEdmValueAnnotation)edmVocabularyAnnotation);
					break;
				default:
					throw new InvalidOperationException(Strings.UnknownEnumVal_TermKind(edmVocabularyAnnotation.Term.TermKind));
				}
			}
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x0001D5B4 File Offset: 0x0001B7B4
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

		// Token: 0x06000AC1 RID: 2753 RVA: 0x0001D60C File Offset: 0x0001B80C
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

		// Token: 0x040004E8 RID: 1256
		private readonly Version edmVersion;

		// Token: 0x040004E9 RID: 1257
		private readonly EdmModelCsdlSchemaWriter schemaWriter;

		// Token: 0x040004EA RID: 1258
		private readonly List<IEdmNavigationProperty> navigationProperties = new List<IEdmNavigationProperty>();

		// Token: 0x040004EB RID: 1259
		private readonly VersioningDictionary<string, string> namespaceAliasMappings;
	}
}
