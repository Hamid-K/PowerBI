using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Csdl.Parsing.Common;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Vocabularies;
using Microsoft.OData.Edm.Vocabularies.V1;

namespace Microsoft.OData.Edm.Csdl.Parsing
{
	// Token: 0x020001B8 RID: 440
	internal class CsdlDocumentParser : EdmXmlDocumentParser<CsdlSchema>
	{
		// Token: 0x06000C37 RID: 3127 RVA: 0x00021FDA File Offset: 0x000201DA
		internal CsdlDocumentParser(string documentPath, XmlReader reader)
			: base(documentPath, reader)
		{
			this.entityContainerCount = 0;
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06000C38 RID: 3128 RVA: 0x00021FEB File Offset: 0x000201EB
		internal override IEnumerable<KeyValuePair<Version, string>> SupportedVersions
		{
			get
			{
				return CsdlConstants.SupportedVersions.SelectMany((KeyValuePair<Version, string[]> kvp) => kvp.Value.Select((string ns) => new KeyValuePair<Version, string>(kvp.Key, ns)));
			}
		}

		// Token: 0x06000C39 RID: 3129 RVA: 0x00022016 File Offset: 0x00020216
		protected override bool TryGetDocumentElementParser(Version csdlArtifactVersion, XmlElementInfo rootElement, out XmlElementParser<CsdlSchema> parser)
		{
			EdmUtil.CheckArgumentNull<XmlElementInfo>(rootElement, "rootElement");
			this.artifactVersion = csdlArtifactVersion;
			if (string.Equals(rootElement.Name, "Schema", StringComparison.Ordinal))
			{
				parser = this.CreateRootElementParser();
				return true;
			}
			parser = null;
			return false;
		}

		// Token: 0x06000C3A RID: 3130 RVA: 0x0002204C File Offset: 0x0002024C
		protected override void AnnotateItem(object result, XmlElementValueCollection childValues)
		{
			CsdlElement csdlElement = result as CsdlElement;
			if (csdlElement == null)
			{
				return;
			}
			foreach (XmlAnnotationInfo xmlAnnotationInfo in this.currentElement.Annotations)
			{
				csdlElement.AddAnnotation(new CsdlDirectValueAnnotation(xmlAnnotationInfo.NamespaceName, xmlAnnotationInfo.Name, xmlAnnotationInfo.Value, xmlAnnotationInfo.IsAttribute, xmlAnnotationInfo.Location));
			}
			foreach (CsdlAnnotation csdlAnnotation in childValues.ValuesOfType<CsdlAnnotation>())
			{
				csdlElement.AddAnnotation(csdlAnnotation);
			}
		}

		// Token: 0x06000C3B RID: 3131 RVA: 0x0002210C File Offset: 0x0002030C
		private XmlElementParser<CsdlSchema> CreateRootElementParser()
		{
			XmlElementParser<CsdlTypeReference> xmlElementParser = base.CsdlElement<CsdlTypeReference>("ReferenceType", new Func<XmlElementInfo, XmlElementValueCollection, CsdlTypeReference>(this.OnEntityReferenceTypeElement), new XmlElementParser[0]);
			XmlElementParser<CsdlTypeReference> xmlElementParser2 = base.CsdlElement<CsdlTypeReference>("CollectionType", new Func<XmlElementInfo, XmlElementValueCollection, CsdlTypeReference>(this.OnCollectionTypeElement), new XmlElementParser[]
			{
				base.CsdlElement<CsdlTypeReference>("TypeRef", new Func<XmlElementInfo, XmlElementValueCollection, CsdlTypeReference>(this.OnTypeRefElement), new XmlElementParser[0]),
				xmlElementParser
			});
			XmlElementParser<CsdlProperty> xmlElementParser3 = base.CsdlElement<CsdlProperty>("Property", new Func<XmlElementInfo, XmlElementValueCollection, CsdlProperty>(this.OnPropertyElement), new XmlElementParser[0]);
			XmlElementParser<CsdlExpressionBase> xmlElementParser4 = base.CsdlElement<CsdlExpressionBase>("String", new Func<XmlElementInfo, XmlElementValueCollection, CsdlExpressionBase>(CsdlDocumentParser.OnStringConstantExpression), new XmlElementParser[0]);
			XmlElementParser<CsdlExpressionBase> xmlElementParser5 = base.CsdlElement<CsdlExpressionBase>("Binary", new Func<XmlElementInfo, XmlElementValueCollection, CsdlExpressionBase>(CsdlDocumentParser.OnBinaryConstantExpression), new XmlElementParser[0]);
			XmlElementParser<CsdlExpressionBase> xmlElementParser6 = base.CsdlElement<CsdlExpressionBase>("Int", new Func<XmlElementInfo, XmlElementValueCollection, CsdlExpressionBase>(CsdlDocumentParser.OnIntConstantExpression), new XmlElementParser[0]);
			XmlElementParser<CsdlExpressionBase> xmlElementParser7 = base.CsdlElement<CsdlExpressionBase>("Float", new Func<XmlElementInfo, XmlElementValueCollection, CsdlExpressionBase>(CsdlDocumentParser.OnFloatConstantExpression), new XmlElementParser[0]);
			XmlElementParser<CsdlExpressionBase> xmlElementParser8 = base.CsdlElement<CsdlExpressionBase>("Guid", new Func<XmlElementInfo, XmlElementValueCollection, CsdlExpressionBase>(CsdlDocumentParser.OnGuidConstantExpression), new XmlElementParser[0]);
			XmlElementParser<CsdlExpressionBase> xmlElementParser9 = base.CsdlElement<CsdlExpressionBase>("Decimal", new Func<XmlElementInfo, XmlElementValueCollection, CsdlExpressionBase>(CsdlDocumentParser.OnDecimalConstantExpression), new XmlElementParser[0]);
			XmlElementParser<CsdlExpressionBase> xmlElementParser10 = base.CsdlElement<CsdlExpressionBase>("Bool", new Func<XmlElementInfo, XmlElementValueCollection, CsdlExpressionBase>(CsdlDocumentParser.OnBoolConstantExpression), new XmlElementParser[0]);
			XmlElementParser<CsdlExpressionBase> xmlElementParser11 = base.CsdlElement<CsdlExpressionBase>("Duration", new Func<XmlElementInfo, XmlElementValueCollection, CsdlExpressionBase>(CsdlDocumentParser.OnDurationConstantExpression), new XmlElementParser[0]);
			XmlElementParser<CsdlExpressionBase> xmlElementParser12 = base.CsdlElement<CsdlExpressionBase>("Date", new Func<XmlElementInfo, XmlElementValueCollection, CsdlExpressionBase>(CsdlDocumentParser.OnDateConstantExpression), new XmlElementParser[0]);
			XmlElementParser<CsdlExpressionBase> xmlElementParser13 = base.CsdlElement<CsdlExpressionBase>("TimeOfDay", new Func<XmlElementInfo, XmlElementValueCollection, CsdlExpressionBase>(CsdlDocumentParser.OnTimeOfDayConstantExpression), new XmlElementParser[0]);
			XmlElementParser<CsdlExpressionBase> xmlElementParser14 = base.CsdlElement<CsdlExpressionBase>("DateTimeOffset", new Func<XmlElementInfo, XmlElementValueCollection, CsdlExpressionBase>(CsdlDocumentParser.OnDateTimeOffsetConstantExpression), new XmlElementParser[0]);
			XmlElementParser<CsdlExpressionBase> xmlElementParser15 = base.CsdlElement<CsdlExpressionBase>("Null", new Func<XmlElementInfo, XmlElementValueCollection, CsdlExpressionBase>(CsdlDocumentParser.OnNullConstantExpression), new XmlElementParser[0]);
			XmlElementParser<CsdlExpressionBase> xmlElementParser16 = base.CsdlElement<CsdlExpressionBase>("Path", new Func<XmlElementInfo, XmlElementValueCollection, CsdlExpressionBase>(CsdlDocumentParser.OnPathExpression), new XmlElementParser[0]);
			XmlElementParser<CsdlExpressionBase> xmlElementParser17 = base.CsdlElement<CsdlExpressionBase>("PropertyPath", new Func<XmlElementInfo, XmlElementValueCollection, CsdlExpressionBase>(CsdlDocumentParser.OnPropertyPathExpression), new XmlElementParser[0]);
			XmlElementParser<CsdlExpressionBase> xmlElementParser18 = base.CsdlElement<CsdlExpressionBase>("NavigationPropertyPath", new Func<XmlElementInfo, XmlElementValueCollection, CsdlExpressionBase>(CsdlDocumentParser.OnNavigationPropertyPathExpression), new XmlElementParser[0]);
			XmlElementParser<CsdlExpressionBase> xmlElementParser19 = base.CsdlElement<CsdlExpressionBase>("EnumMember", new Func<XmlElementInfo, XmlElementValueCollection, CsdlExpressionBase>(this.OnEnumMemberExpression), new XmlElementParser[0]);
			XmlElementParser<CsdlExpressionBase> xmlElementParser20 = base.CsdlElement<CsdlExpressionBase>("If", new Func<XmlElementInfo, XmlElementValueCollection, CsdlExpressionBase>(this.OnIfExpression), new XmlElementParser[0]);
			XmlElementParser<CsdlExpressionBase> xmlElementParser21 = base.CsdlElement<CsdlExpressionBase>("Cast", new Func<XmlElementInfo, XmlElementValueCollection, CsdlExpressionBase>(this.OnCastExpression), new XmlElementParser[0]);
			XmlElementParser<CsdlExpressionBase> xmlElementParser22 = base.CsdlElement<CsdlExpressionBase>("IsType", new Func<XmlElementInfo, XmlElementValueCollection, CsdlExpressionBase>(this.OnIsTypeExpression), new XmlElementParser[0]);
			XmlElementParser<CsdlPropertyValue> xmlElementParser23 = base.CsdlElement<CsdlPropertyValue>("PropertyValue", new Func<XmlElementInfo, XmlElementValueCollection, CsdlPropertyValue>(this.OnPropertyValueElement), new XmlElementParser[0]);
			XmlElementParser<CsdlRecordExpression> xmlElementParser24 = base.CsdlElement<CsdlRecordExpression>("Record", new Func<XmlElementInfo, XmlElementValueCollection, CsdlRecordExpression>(this.OnRecordElement), new XmlElementParser[] { xmlElementParser23 });
			XmlElementParser<CsdlLabeledExpression> xmlElementParser25 = base.CsdlElement<CsdlLabeledExpression>("LabeledElement", new Func<XmlElementInfo, XmlElementValueCollection, CsdlLabeledExpression>(this.OnLabeledElement), new XmlElementParser[0]);
			XmlElementParser<CsdlCollectionExpression> xmlElementParser26 = base.CsdlElement<CsdlCollectionExpression>("Collection", new Func<XmlElementInfo, XmlElementValueCollection, CsdlCollectionExpression>(this.OnCollectionElement), new XmlElementParser[0]);
			XmlElementParser<CsdlExpressionBase> xmlElementParser27 = base.CsdlElement<CsdlExpressionBase>("Apply", new Func<XmlElementInfo, XmlElementValueCollection, CsdlExpressionBase>(this.OnApplyElement), new XmlElementParser[0]);
			XmlElementParser<CsdlExpressionBase> xmlElementParser28 = base.CsdlElement<CsdlExpressionBase>("LabeledElementReference", new Func<XmlElementInfo, XmlElementValueCollection, CsdlExpressionBase>(this.OnLabeledElementReferenceExpression), new XmlElementParser[0]);
			XmlElementParser[] array = new XmlElementParser[]
			{
				xmlElementParser4, xmlElementParser5, xmlElementParser6, xmlElementParser7, xmlElementParser8, xmlElementParser9, xmlElementParser10, xmlElementParser12, xmlElementParser14, xmlElementParser11,
				xmlElementParser13, xmlElementParser15, xmlElementParser16, xmlElementParser17, xmlElementParser18, xmlElementParser20, xmlElementParser22, xmlElementParser21, xmlElementParser24, xmlElementParser26,
				xmlElementParser28, xmlElementParser23, xmlElementParser25, xmlElementParser19, xmlElementParser27
			};
			CsdlDocumentParser.AddChildParsers(xmlElementParser20, array);
			CsdlDocumentParser.AddChildParsers(xmlElementParser21, array);
			CsdlDocumentParser.AddChildParsers(xmlElementParser22, array);
			CsdlDocumentParser.AddChildParsers(xmlElementParser23, array);
			CsdlDocumentParser.AddChildParsers(xmlElementParser26, array);
			CsdlDocumentParser.AddChildParsers(xmlElementParser25, array);
			CsdlDocumentParser.AddChildParsers(xmlElementParser27, array);
			XmlElementParser<CsdlAnnotation> xmlElementParser29 = base.CsdlElement<CsdlAnnotation>("Annotation", new Func<XmlElementInfo, XmlElementValueCollection, CsdlAnnotation>(this.OnAnnotationElement), new XmlElementParser[0]);
			CsdlDocumentParser.AddChildParsers(xmlElementParser29, array);
			xmlElementParser3.AddChildParser(xmlElementParser29);
			xmlElementParser2.AddChildParser(xmlElementParser2);
			return base.CsdlElement<CsdlSchema>("Schema", new Func<XmlElementInfo, XmlElementValueCollection, CsdlSchema>(this.OnSchemaElement), new XmlElementParser[]
			{
				base.CsdlElement<CsdlComplexType>("ComplexType", new Func<XmlElementInfo, XmlElementValueCollection, CsdlComplexType>(this.OnComplexTypeElement), new XmlElementParser[]
				{
					xmlElementParser3,
					base.CsdlElement<CsdlNamedElement>("NavigationProperty", new Func<XmlElementInfo, XmlElementValueCollection, CsdlNamedElement>(this.OnNavigationPropertyElement), new XmlElementParser[]
					{
						base.CsdlElement<CsdlReferentialConstraint>("ReferentialConstraint", new Func<XmlElementInfo, XmlElementValueCollection, CsdlReferentialConstraint>(this.OnReferentialConstraintElement), new XmlElementParser[0]),
						base.CsdlElement<CsdlOnDelete>("OnDelete", new Func<XmlElementInfo, XmlElementValueCollection, CsdlOnDelete>(this.OnDeleteActionElement), new XmlElementParser[0]),
						xmlElementParser29
					}),
					xmlElementParser29
				}),
				base.CsdlElement<CsdlEntityType>("EntityType", new Func<XmlElementInfo, XmlElementValueCollection, CsdlEntityType>(this.OnEntityTypeElement), new XmlElementParser[]
				{
					base.CsdlElement<CsdlKey>("Key", new Func<XmlElementInfo, XmlElementValueCollection, CsdlKey>(CsdlDocumentParser.OnEntityKeyElement), new XmlElementParser[] { base.CsdlElement<CsdlPropertyReference>("PropertyRef", new Func<XmlElementInfo, XmlElementValueCollection, CsdlPropertyReference>(this.OnPropertyRefElement), new XmlElementParser[0]) }),
					xmlElementParser3,
					base.CsdlElement<CsdlNamedElement>("NavigationProperty", new Func<XmlElementInfo, XmlElementValueCollection, CsdlNamedElement>(this.OnNavigationPropertyElement), new XmlElementParser[]
					{
						base.CsdlElement<CsdlReferentialConstraint>("ReferentialConstraint", new Func<XmlElementInfo, XmlElementValueCollection, CsdlReferentialConstraint>(this.OnReferentialConstraintElement), new XmlElementParser[0]),
						base.CsdlElement<CsdlOnDelete>("OnDelete", new Func<XmlElementInfo, XmlElementValueCollection, CsdlOnDelete>(this.OnDeleteActionElement), new XmlElementParser[0]),
						xmlElementParser29
					}),
					xmlElementParser29
				}),
				base.CsdlElement<CsdlEnumType>("EnumType", new Func<XmlElementInfo, XmlElementValueCollection, CsdlEnumType>(this.OnEnumTypeElement), new XmlElementParser[]
				{
					base.CsdlElement<CsdlEnumMember>("Member", new Func<XmlElementInfo, XmlElementValueCollection, CsdlEnumMember>(this.OnEnumMemberElement), new XmlElementParser[] { xmlElementParser29 }),
					xmlElementParser29
				}),
				base.CsdlElement<CsdlTypeDefinition>("TypeDefinition", new Func<XmlElementInfo, XmlElementValueCollection, CsdlTypeDefinition>(this.OnTypeDefinitionElement), new XmlElementParser[] { xmlElementParser29 }),
				base.CsdlElement<CsdlAction>("Action", new Func<XmlElementInfo, XmlElementValueCollection, CsdlAction>(this.OnActionElement), new XmlElementParser[]
				{
					base.CsdlElement<CsdlOperationParameter>("Parameter", new Func<XmlElementInfo, XmlElementValueCollection, CsdlOperationParameter>(this.OnParameterElement), new XmlElementParser[]
					{
						base.CsdlElement<CsdlTypeReference>("TypeRef", new Func<XmlElementInfo, XmlElementValueCollection, CsdlTypeReference>(this.OnTypeRefElement), new XmlElementParser[0]),
						xmlElementParser2,
						xmlElementParser,
						xmlElementParser29
					}),
					base.CsdlElement<CsdlOperationReturn>("ReturnType", new Func<XmlElementInfo, XmlElementValueCollection, CsdlOperationReturn>(this.OnReturnTypeElement), new XmlElementParser[]
					{
						base.CsdlElement<CsdlTypeReference>("TypeRef", new Func<XmlElementInfo, XmlElementValueCollection, CsdlTypeReference>(this.OnTypeRefElement), new XmlElementParser[0]),
						xmlElementParser2,
						xmlElementParser,
						xmlElementParser29
					}),
					xmlElementParser29
				}),
				base.CsdlElement<CsdlOperation>("Function", new Func<XmlElementInfo, XmlElementValueCollection, CsdlOperation>(this.OnFunctionElement), new XmlElementParser[]
				{
					base.CsdlElement<CsdlOperationParameter>("Parameter", new Func<XmlElementInfo, XmlElementValueCollection, CsdlOperationParameter>(this.OnParameterElement), new XmlElementParser[]
					{
						base.CsdlElement<CsdlTypeReference>("TypeRef", new Func<XmlElementInfo, XmlElementValueCollection, CsdlTypeReference>(this.OnTypeRefElement), new XmlElementParser[0]),
						xmlElementParser2,
						xmlElementParser,
						xmlElementParser29
					}),
					base.CsdlElement<CsdlOperationReturn>("ReturnType", new Func<XmlElementInfo, XmlElementValueCollection, CsdlOperationReturn>(this.OnReturnTypeElement), new XmlElementParser[]
					{
						base.CsdlElement<CsdlTypeReference>("TypeRef", new Func<XmlElementInfo, XmlElementValueCollection, CsdlTypeReference>(this.OnTypeRefElement), new XmlElementParser[0]),
						xmlElementParser2,
						xmlElementParser,
						xmlElementParser29
					}),
					xmlElementParser29
				}),
				base.CsdlElement<CsdlTerm>("Term", new Func<XmlElementInfo, XmlElementValueCollection, CsdlTerm>(this.OnTermElement), new XmlElementParser[]
				{
					base.CsdlElement<CsdlTypeReference>("TypeRef", new Func<XmlElementInfo, XmlElementValueCollection, CsdlTypeReference>(this.OnTypeRefElement), new XmlElementParser[0]),
					xmlElementParser2,
					xmlElementParser,
					xmlElementParser29
				}),
				base.CsdlElement<CsdlAnnotations>("Annotations", new Func<XmlElementInfo, XmlElementValueCollection, CsdlAnnotations>(this.OnAnnotationsElement), new XmlElementParser[] { xmlElementParser29 }),
				base.CsdlElement<CsdlEntityContainer>("EntityContainer", new Func<XmlElementInfo, XmlElementValueCollection, CsdlEntityContainer>(this.OnEntityContainerElement), new XmlElementParser[]
				{
					base.CsdlElement<CsdlEntitySet>("EntitySet", new Func<XmlElementInfo, XmlElementValueCollection, CsdlEntitySet>(this.OnEntitySetElement), new XmlElementParser[]
					{
						base.CsdlElement<CsdlNavigationPropertyBinding>("NavigationPropertyBinding", new Func<XmlElementInfo, XmlElementValueCollection, CsdlNavigationPropertyBinding>(this.OnNavigationPropertyBindingElement), new XmlElementParser[0]),
						xmlElementParser29
					}),
					base.CsdlElement<CsdlSingleton>("Singleton", new Func<XmlElementInfo, XmlElementValueCollection, CsdlSingleton>(this.OnSingletonElement), new XmlElementParser[]
					{
						base.CsdlElement<CsdlNavigationPropertyBinding>("NavigationPropertyBinding", new Func<XmlElementInfo, XmlElementValueCollection, CsdlNavigationPropertyBinding>(this.OnNavigationPropertyBindingElement), new XmlElementParser[0]),
						xmlElementParser29
					}),
					base.CsdlElement<CsdlActionImport>("ActionImport", new Func<XmlElementInfo, XmlElementValueCollection, CsdlActionImport>(this.OnActionImportElement), new XmlElementParser[] { xmlElementParser29 }),
					base.CsdlElement<CsdlOperationImport>("FunctionImport", new Func<XmlElementInfo, XmlElementValueCollection, CsdlOperationImport>(this.OnFunctionImportElement), new XmlElementParser[]
					{
						base.CsdlElement<CsdlOperationParameter>("Parameter", new Func<XmlElementInfo, XmlElementValueCollection, CsdlOperationParameter>(this.OnFunctionImportParameterElement), new XmlElementParser[] { xmlElementParser29 }),
						xmlElementParser29
					}),
					xmlElementParser29
				})
			});
		}

		// Token: 0x06000C3C RID: 3132 RVA: 0x00022AA8 File Offset: 0x00020CA8
		private CsdlSchema OnSchemaElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.Optional("Namespace") ?? string.Empty;
			string text2 = base.OptionalAlias("Alias");
			return new CsdlSchema(text, text2, this.artifactVersion, childValues.ValuesOfType<CsdlStructuredType>(), childValues.ValuesOfType<CsdlEnumType>(), childValues.ValuesOfType<CsdlOperation>(), childValues.ValuesOfType<CsdlTerm>(), childValues.ValuesOfType<CsdlEntityContainer>(), childValues.ValuesOfType<CsdlAnnotations>(), childValues.ValuesOfType<CsdlTypeDefinition>(), element.Location);
		}

		// Token: 0x06000C3D RID: 3133 RVA: 0x00022B18 File Offset: 0x00020D18
		private CsdlComplexType OnComplexTypeElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.Required("Name");
			string text2 = base.OptionalQualifiedName("BaseType");
			bool flag = base.OptionalBoolean("OpenType") ?? false;
			bool flag2 = base.OptionalBoolean("Abstract") ?? false;
			return new CsdlComplexType(text, text2, flag2, flag, childValues.ValuesOfType<CsdlProperty>(), childValues.ValuesOfType<CsdlNavigationProperty>(), element.Location);
		}

		// Token: 0x06000C3E RID: 3134 RVA: 0x00022B9C File Offset: 0x00020D9C
		private CsdlEntityType OnEntityTypeElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.Required("Name");
			string text2 = base.OptionalQualifiedName("BaseType");
			bool flag = base.OptionalBoolean("OpenType") ?? false;
			bool flag2 = base.OptionalBoolean("Abstract") ?? false;
			bool flag3 = base.OptionalBoolean("HasStream") ?? false;
			CsdlKey csdlKey = childValues.ValuesOfType<CsdlKey>().FirstOrDefault<CsdlKey>();
			return new CsdlEntityType(text, text2, flag2, flag, flag3, csdlKey, childValues.ValuesOfType<CsdlProperty>(), childValues.ValuesOfType<CsdlNavigationProperty>(), element.Location);
		}

		// Token: 0x06000C3F RID: 3135 RVA: 0x00022C54 File Offset: 0x00020E54
		private CsdlProperty OnPropertyElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.OptionalType("Type");
			CsdlTypeReference csdlTypeReference = this.ParseTypeReference(text, childValues, element.Location, CsdlDocumentParser.Optionality.Required);
			string text2 = base.Required("Name");
			string text3 = base.Optional("DefaultValue");
			return new CsdlProperty(text2, csdlTypeReference, text3, element.Location);
		}

		// Token: 0x06000C40 RID: 3136 RVA: 0x00022CA4 File Offset: 0x00020EA4
		private CsdlTerm OnTermElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.OptionalType("Type");
			CsdlTypeReference csdlTypeReference = this.ParseTypeReference(text, childValues, element.Location, CsdlDocumentParser.Optionality.Required);
			string text2 = base.Required("Name");
			string text3 = base.Optional("AppliesTo");
			string text4 = base.Optional("DefaultValue");
			return new CsdlTerm(text2, csdlTypeReference, text3, text4, element.Location);
		}

		// Token: 0x06000C41 RID: 3137 RVA: 0x00022D04 File Offset: 0x00020F04
		private CsdlAnnotations OnAnnotationsElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.Required("Target");
			string text2 = base.Optional("Qualifier");
			IEnumerable<CsdlAnnotation> enumerable = childValues.ValuesOfType<CsdlAnnotation>();
			return new CsdlAnnotations(enumerable, text, text2);
		}

		// Token: 0x06000C42 RID: 3138 RVA: 0x00022D38 File Offset: 0x00020F38
		private CsdlAnnotation OnAnnotationElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.RequiredQualifiedName("Term");
			string text2 = base.Optional("Qualifier");
			CsdlExpressionBase csdlExpressionBase = this.ParseAnnotationExpression(element, childValues);
			return new CsdlAnnotation(text, text2, csdlExpressionBase, element.Location);
		}

		// Token: 0x06000C43 RID: 3139 RVA: 0x00022D74 File Offset: 0x00020F74
		private CsdlPropertyValue OnPropertyValueElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.Required("Property");
			CsdlExpressionBase csdlExpressionBase = this.ParseAnnotationExpression(element, childValues);
			return new CsdlPropertyValue(text, csdlExpressionBase, element.Location);
		}

		// Token: 0x06000C44 RID: 3140 RVA: 0x00022DA4 File Offset: 0x00020FA4
		private CsdlRecordExpression OnRecordElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.OptionalQualifiedName("Type");
			IEnumerable<CsdlPropertyValue> enumerable = childValues.ValuesOfType<CsdlPropertyValue>();
			return new CsdlRecordExpression((text != null) ? new CsdlNamedTypeReference(text, false, element.Location) : null, enumerable, element.Location);
		}

		// Token: 0x06000C45 RID: 3141 RVA: 0x00022DE4 File Offset: 0x00020FE4
		private CsdlCollectionExpression OnCollectionElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.OptionalType("Type");
			CsdlTypeReference csdlTypeReference = this.ParseTypeReference(text, childValues, element.Location, CsdlDocumentParser.Optionality.Optional);
			IEnumerable<CsdlExpressionBase> enumerable = childValues.ValuesOfType<CsdlExpressionBase>();
			return new CsdlCollectionExpression(csdlTypeReference, enumerable, element.Location);
		}

		// Token: 0x06000C46 RID: 3142 RVA: 0x00022E24 File Offset: 0x00021024
		private CsdlLabeledExpression OnLabeledElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.Required("Name");
			IEnumerable<CsdlExpressionBase> enumerable = childValues.ValuesOfType<CsdlExpressionBase>();
			if (enumerable.Count<CsdlExpressionBase>() != 1)
			{
				base.ReportError(element.Location, EdmErrorCode.InvalidLabeledElementExpressionIncorrectNumberOfOperands, Strings.CsdlParser_InvalidLabeledElementExpressionIncorrectNumberOfOperands);
			}
			return new CsdlLabeledExpression(text, enumerable.ElementAtOrDefault(0), element.Location);
		}

		// Token: 0x06000C47 RID: 3143 RVA: 0x00022E78 File Offset: 0x00021078
		private CsdlApplyExpression OnApplyElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.Optional("Function");
			IEnumerable<CsdlExpressionBase> enumerable = childValues.ValuesOfType<CsdlExpressionBase>();
			return new CsdlApplyExpression(text, enumerable, element.Location);
		}

		// Token: 0x06000C48 RID: 3144 RVA: 0x00022EA8 File Offset: 0x000210A8
		private static void AddChildParsers(XmlElementParser parent, IEnumerable<XmlElementParser> children)
		{
			foreach (XmlElementParser xmlElementParser in children)
			{
				parent.AddChildParser(xmlElementParser);
			}
		}

		// Token: 0x06000C49 RID: 3145 RVA: 0x00022EF0 File Offset: 0x000210F0
		private static CsdlConstantExpression ConstantExpression(EdmValueKind kind, XmlElementValueCollection childValues, CsdlLocation location)
		{
			XmlTextValue firstText = childValues.FirstText;
			return new CsdlConstantExpression(kind, (firstText != null) ? firstText.TextValue : string.Empty, location);
		}

		// Token: 0x06000C4A RID: 3146 RVA: 0x00022F1B File Offset: 0x0002111B
		private static CsdlConstantExpression OnIntConstantExpression(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			return CsdlDocumentParser.ConstantExpression(EdmValueKind.Integer, childValues, element.Location);
		}

		// Token: 0x06000C4B RID: 3147 RVA: 0x00022F2B File Offset: 0x0002112B
		private static CsdlConstantExpression OnStringConstantExpression(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			return CsdlDocumentParser.ConstantExpression(EdmValueKind.String, childValues, element.Location);
		}

		// Token: 0x06000C4C RID: 3148 RVA: 0x00022F3B File Offset: 0x0002113B
		private static CsdlConstantExpression OnBinaryConstantExpression(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			return CsdlDocumentParser.ConstantExpression(EdmValueKind.Binary, childValues, element.Location);
		}

		// Token: 0x06000C4D RID: 3149 RVA: 0x00022F4A File Offset: 0x0002114A
		private static CsdlConstantExpression OnFloatConstantExpression(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			return CsdlDocumentParser.ConstantExpression(EdmValueKind.Floating, childValues, element.Location);
		}

		// Token: 0x06000C4E RID: 3150 RVA: 0x00022F59 File Offset: 0x00021159
		private static CsdlConstantExpression OnGuidConstantExpression(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			return CsdlDocumentParser.ConstantExpression(EdmValueKind.Guid, childValues, element.Location);
		}

		// Token: 0x06000C4F RID: 3151 RVA: 0x00022F68 File Offset: 0x00021168
		private static CsdlConstantExpression OnDecimalConstantExpression(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			return CsdlDocumentParser.ConstantExpression(EdmValueKind.Decimal, childValues, element.Location);
		}

		// Token: 0x06000C50 RID: 3152 RVA: 0x00022F77 File Offset: 0x00021177
		private static CsdlConstantExpression OnBoolConstantExpression(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			return CsdlDocumentParser.ConstantExpression(EdmValueKind.Boolean, childValues, element.Location);
		}

		// Token: 0x06000C51 RID: 3153 RVA: 0x00022F86 File Offset: 0x00021186
		private static CsdlConstantExpression OnDurationConstantExpression(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			return CsdlDocumentParser.ConstantExpression(EdmValueKind.Duration, childValues, element.Location);
		}

		// Token: 0x06000C52 RID: 3154 RVA: 0x00022F96 File Offset: 0x00021196
		private static CsdlConstantExpression OnDateConstantExpression(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			return CsdlDocumentParser.ConstantExpression(EdmValueKind.Date, childValues, element.Location);
		}

		// Token: 0x06000C53 RID: 3155 RVA: 0x00022FA6 File Offset: 0x000211A6
		private static CsdlConstantExpression OnDateTimeOffsetConstantExpression(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			return CsdlDocumentParser.ConstantExpression(EdmValueKind.DateTimeOffset, childValues, element.Location);
		}

		// Token: 0x06000C54 RID: 3156 RVA: 0x00022FB5 File Offset: 0x000211B5
		private static CsdlConstantExpression OnTimeOfDayConstantExpression(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			return CsdlDocumentParser.ConstantExpression(EdmValueKind.TimeOfDay, childValues, element.Location);
		}

		// Token: 0x06000C55 RID: 3157 RVA: 0x00022FC5 File Offset: 0x000211C5
		private static CsdlConstantExpression OnNullConstantExpression(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			return CsdlDocumentParser.ConstantExpression(EdmValueKind.Null, childValues, element.Location);
		}

		// Token: 0x06000C56 RID: 3158 RVA: 0x00022FD8 File Offset: 0x000211D8
		private static CsdlPathExpression OnPathExpression(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			XmlTextValue firstText = childValues.FirstText;
			return new CsdlPathExpression((firstText != null) ? firstText.TextValue : string.Empty, element.Location);
		}

		// Token: 0x06000C57 RID: 3159 RVA: 0x00023008 File Offset: 0x00021208
		private static CsdlPropertyPathExpression OnPropertyPathExpression(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			XmlTextValue firstText = childValues.FirstText;
			return new CsdlPropertyPathExpression((firstText != null) ? firstText.TextValue : string.Empty, element.Location);
		}

		// Token: 0x06000C58 RID: 3160 RVA: 0x00023038 File Offset: 0x00021238
		private static CsdlNavigationPropertyPathExpression OnNavigationPropertyPathExpression(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			XmlTextValue firstText = childValues.FirstText;
			return new CsdlNavigationPropertyPathExpression((firstText != null) ? firstText.TextValue : string.Empty, element.Location);
		}

		// Token: 0x06000C59 RID: 3161 RVA: 0x00023068 File Offset: 0x00021268
		private CsdlLabeledExpressionReferenceExpression OnLabeledElementReferenceExpression(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.Required("Name");
			return new CsdlLabeledExpressionReferenceExpression(text, element.Location);
		}

		// Token: 0x06000C5A RID: 3162 RVA: 0x00023090 File Offset: 0x00021290
		private CsdlEnumMemberExpression OnEnumMemberExpression(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.RequiredEnumMemberPath(childValues.FirstText);
			return new CsdlEnumMemberExpression(text, element.Location);
		}

		// Token: 0x06000C5B RID: 3163 RVA: 0x000230B8 File Offset: 0x000212B8
		private CsdlExpressionBase OnIfExpression(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			IEnumerable<CsdlExpressionBase> enumerable = childValues.ValuesOfType<CsdlExpressionBase>();
			if (enumerable.Count<CsdlExpressionBase>() != 3)
			{
				base.ReportError(element.Location, EdmErrorCode.InvalidIfExpressionIncorrectNumberOfOperands, Strings.CsdlParser_InvalidIfExpressionIncorrectNumberOfOperands);
			}
			return new CsdlIfExpression(enumerable.ElementAtOrDefault(0), enumerable.ElementAtOrDefault(1), enumerable.ElementAtOrDefault(2), element.Location);
		}

		// Token: 0x06000C5C RID: 3164 RVA: 0x0002310C File Offset: 0x0002130C
		private CsdlExpressionBase OnCastExpression(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.OptionalType("Type");
			CsdlTypeReference csdlTypeReference = this.ParseTypeReference(text, childValues, element.Location, CsdlDocumentParser.Optionality.Required);
			IEnumerable<CsdlExpressionBase> enumerable = childValues.ValuesOfType<CsdlExpressionBase>();
			if (enumerable.Count<CsdlExpressionBase>() != 1)
			{
				base.ReportError(element.Location, EdmErrorCode.InvalidCastExpressionIncorrectNumberOfOperands, Strings.CsdlParser_InvalidCastExpressionIncorrectNumberOfOperands);
			}
			return new CsdlCastExpression(csdlTypeReference, enumerable.ElementAtOrDefault(0), element.Location);
		}

		// Token: 0x06000C5D RID: 3165 RVA: 0x00023170 File Offset: 0x00021370
		private CsdlExpressionBase OnIsTypeExpression(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.OptionalType("Type");
			CsdlTypeReference csdlTypeReference = this.ParseTypeReference(text, childValues, element.Location, CsdlDocumentParser.Optionality.Required);
			IEnumerable<CsdlExpressionBase> enumerable = childValues.ValuesOfType<CsdlExpressionBase>();
			if (enumerable.Count<CsdlExpressionBase>() != 1)
			{
				base.ReportError(element.Location, EdmErrorCode.InvalidIsTypeExpressionIncorrectNumberOfOperands, Strings.CsdlParser_InvalidIsTypeExpressionIncorrectNumberOfOperands);
			}
			return new CsdlIsTypeExpression(csdlTypeReference, enumerable.ElementAtOrDefault(0), element.Location);
		}

		// Token: 0x06000C5E RID: 3166 RVA: 0x000231D4 File Offset: 0x000213D4
		private CsdlTypeDefinition OnTypeDefinitionElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.Required("Name");
			string text2 = base.RequiredType("UnderlyingType");
			return new CsdlTypeDefinition(text, text2, element.Location);
		}

		// Token: 0x06000C5F RID: 3167 RVA: 0x00023208 File Offset: 0x00021408
		private CsdlExpressionBase ParseAnnotationExpression(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			CsdlExpressionBase csdlExpressionBase = childValues.ValuesOfType<CsdlExpressionBase>().FirstOrDefault<CsdlExpressionBase>();
			if (csdlExpressionBase != null)
			{
				return csdlExpressionBase;
			}
			string text = base.Optional("Path");
			if (text != null)
			{
				return new CsdlPathExpression(text, element.Location);
			}
			string text2 = base.Optional("PropertyPath");
			if (text2 != null)
			{
				return new CsdlPropertyPathExpression(text2, element.Location);
			}
			string text3 = base.Optional("NavigationPropertyPath");
			if (text3 != null)
			{
				return new CsdlNavigationPropertyPathExpression(text3, element.Location);
			}
			string text4 = base.Optional("EnumMember");
			if (text4 != null)
			{
				return new CsdlEnumMemberExpression(base.ValidateEnumMembersPath(text4), element.Location);
			}
			string text5 = base.Optional("AnnotationPath");
			if (text5 != null)
			{
				return new CsdlAnnotationPathExpression(text5, element.Location);
			}
			string text6 = base.Optional("String");
			EdmValueKind edmValueKind;
			if (text6 != null)
			{
				edmValueKind = EdmValueKind.String;
			}
			else
			{
				text6 = base.Optional("Bool");
				if (text6 != null)
				{
					edmValueKind = EdmValueKind.Boolean;
				}
				else
				{
					text6 = base.Optional("Int");
					if (text6 != null)
					{
						edmValueKind = EdmValueKind.Integer;
					}
					else
					{
						text6 = base.Optional("Float");
						if (text6 != null)
						{
							edmValueKind = EdmValueKind.Floating;
						}
						else
						{
							text6 = base.Optional("DateTimeOffset");
							if (text6 != null)
							{
								edmValueKind = EdmValueKind.DateTimeOffset;
							}
							else
							{
								text6 = base.Optional("Duration");
								if (text6 != null)
								{
									edmValueKind = EdmValueKind.Duration;
								}
								else
								{
									text6 = base.Optional("Decimal");
									if (text6 != null)
									{
										edmValueKind = EdmValueKind.Decimal;
									}
									else
									{
										text6 = base.Optional("Binary");
										if (text6 != null)
										{
											edmValueKind = EdmValueKind.Binary;
										}
										else
										{
											text6 = base.Optional("Guid");
											if (text6 != null)
											{
												edmValueKind = EdmValueKind.Guid;
											}
											else
											{
												text6 = base.Optional("Date");
												if (text6 != null)
												{
													edmValueKind = EdmValueKind.Date;
												}
												else
												{
													text6 = base.Optional("TimeOfDay");
													if (text6 == null)
													{
														return null;
													}
													edmValueKind = EdmValueKind.TimeOfDay;
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			return new CsdlConstantExpression(edmValueKind, text6, element.Location);
		}

		// Token: 0x06000C60 RID: 3168 RVA: 0x000233D8 File Offset: 0x000215D8
		private CsdlNamedTypeReference ParseNamedTypeReference(string typeName, bool isNullable, CsdlLocation parentLocation)
		{
			EdmPrimitiveTypeKind primitiveTypeKind = EdmCoreModel.Instance.GetPrimitiveTypeKind(typeName);
			bool flag;
			int? num;
			int? num2;
			int? num3;
			bool? flag2;
			int? num4;
			switch (primitiveTypeKind)
			{
			case EdmPrimitiveTypeKind.None:
				if (string.Equals(typeName, "Edm.Untyped", StringComparison.Ordinal))
				{
					return new CsdlUntypedTypeReference(typeName, parentLocation);
				}
				break;
			case EdmPrimitiveTypeKind.Binary:
				this.ParseBinaryFacets(out flag, out num);
				return new CsdlBinaryTypeReference(flag, num, typeName, isNullable, parentLocation);
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
				return new CsdlPrimitiveTypeReference(primitiveTypeKind, typeName, isNullable, parentLocation);
			case EdmPrimitiveTypeKind.DateTimeOffset:
			case EdmPrimitiveTypeKind.Duration:
			case EdmPrimitiveTypeKind.TimeOfDay:
				this.ParseTemporalFacets(out num2);
				return new CsdlTemporalTypeReference(primitiveTypeKind, num2, typeName, isNullable, parentLocation);
			case EdmPrimitiveTypeKind.Decimal:
				this.ParseDecimalFacets(out num2, out num3);
				return new CsdlDecimalTypeReference(num2, num3, typeName, isNullable, parentLocation);
			case EdmPrimitiveTypeKind.String:
				this.ParseStringFacets(out flag, out num, out flag2);
				return new CsdlStringTypeReference(flag, num, flag2, typeName, isNullable, parentLocation);
			case EdmPrimitiveTypeKind.Geography:
			case EdmPrimitiveTypeKind.GeographyPoint:
			case EdmPrimitiveTypeKind.GeographyLineString:
			case EdmPrimitiveTypeKind.GeographyPolygon:
			case EdmPrimitiveTypeKind.GeographyCollection:
			case EdmPrimitiveTypeKind.GeographyMultiPolygon:
			case EdmPrimitiveTypeKind.GeographyMultiLineString:
			case EdmPrimitiveTypeKind.GeographyMultiPoint:
				this.ParseSpatialFacets(out num4, 4326);
				return new CsdlSpatialTypeReference(primitiveTypeKind, num4, typeName, isNullable, parentLocation);
			case EdmPrimitiveTypeKind.Geometry:
			case EdmPrimitiveTypeKind.GeometryPoint:
			case EdmPrimitiveTypeKind.GeometryLineString:
			case EdmPrimitiveTypeKind.GeometryPolygon:
			case EdmPrimitiveTypeKind.GeometryCollection:
			case EdmPrimitiveTypeKind.GeometryMultiPolygon:
			case EdmPrimitiveTypeKind.GeometryMultiLineString:
			case EdmPrimitiveTypeKind.GeometryMultiPoint:
				this.ParseSpatialFacets(out num4, 0);
				return new CsdlSpatialTypeReference(primitiveTypeKind, num4, typeName, isNullable, parentLocation);
			}
			this.ParseTypeDefinitionFacets(out flag, out num, out flag2, out num2, out num3, out num4);
			return new CsdlNamedTypeReference(flag, num, flag2, num2, num3, num4, typeName, isNullable, parentLocation);
		}

		// Token: 0x06000C61 RID: 3169 RVA: 0x00023554 File Offset: 0x00021754
		private CsdlTypeReference ParseTypeReference(string typeString, XmlElementValueCollection childValues, CsdlLocation parentLocation, CsdlDocumentParser.Optionality typeInfoOptionality)
		{
			bool flag = base.OptionalBoolean("Nullable") ?? true;
			CsdlTypeReference csdlTypeReference = null;
			if (typeString != null)
			{
				string[] array = typeString.Split(new char[] { '(', ')' });
				string text = array[0];
				if (!(text == "Collection"))
				{
					if (!(text == "Ref"))
					{
						csdlTypeReference = this.ParseNamedTypeReference(text, flag, parentLocation);
					}
					else
					{
						string text2 = ((array.Count<string>() > 1) ? array[1] : typeString);
						csdlTypeReference = new CsdlExpressionTypeReference(new CsdlEntityReferenceType(this.ParseNamedTypeReference(text2, flag, parentLocation), parentLocation), true, parentLocation);
					}
				}
				else
				{
					string text3 = ((array.Count<string>() > 1) ? array[1] : typeString);
					csdlTypeReference = new CsdlExpressionTypeReference(new CsdlCollectionType(this.ParseNamedTypeReference(text3, flag, parentLocation), parentLocation), flag, parentLocation);
				}
			}
			else if (childValues != null)
			{
				csdlTypeReference = childValues.ValuesOfType<CsdlTypeReference>().FirstOrDefault<CsdlTypeReference>();
			}
			if (csdlTypeReference == null && typeInfoOptionality == CsdlDocumentParser.Optionality.Required)
			{
				if (childValues != null)
				{
					base.ReportError(parentLocation, EdmErrorCode.MissingType, Strings.CsdlParser_MissingTypeAttributeOrElement);
				}
				csdlTypeReference = new CsdlNamedTypeReference(string.Empty, flag, parentLocation);
			}
			return csdlTypeReference;
		}

		// Token: 0x06000C62 RID: 3170 RVA: 0x0002365C File Offset: 0x0002185C
		private CsdlNamedElement OnNavigationPropertyElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.Required("Name");
			string text2 = base.RequiredType("Type");
			bool? flag = base.OptionalBoolean("Nullable");
			string text3 = base.Optional("Partner");
			bool? flag2 = base.OptionalBoolean("ContainsTarget");
			CsdlOnDelete csdlOnDelete = childValues.ValuesOfType<CsdlOnDelete>().FirstOrDefault<CsdlOnDelete>();
			IEnumerable<CsdlReferentialConstraint> enumerable = childValues.ValuesOfType<CsdlReferentialConstraint>().ToList<CsdlReferentialConstraint>();
			return new CsdlNavigationProperty(text, text2, flag, text3, flag2 ?? false, csdlOnDelete, enumerable, element.Location);
		}

		// Token: 0x06000C63 RID: 3171 RVA: 0x000236EA File Offset: 0x000218EA
		private static CsdlKey OnEntityKeyElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			return new CsdlKey(new List<CsdlPropertyReference>(childValues.ValuesOfType<CsdlPropertyReference>()), element.Location);
		}

		// Token: 0x06000C64 RID: 3172 RVA: 0x00023702 File Offset: 0x00021902
		private CsdlPropertyReference OnPropertyRefElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			return new CsdlPropertyReference(base.Required("Name"), element.Location);
		}

		// Token: 0x06000C65 RID: 3173 RVA: 0x0002371C File Offset: 0x0002191C
		private CsdlEnumType OnEnumTypeElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.Required("Name");
			string text2 = base.OptionalType("UnderlyingType");
			bool? flag = base.OptionalBoolean("IsFlags");
			return new CsdlEnumType(text, text2, flag ?? false, childValues.ValuesOfType<CsdlEnumMember>(), element.Location);
		}

		// Token: 0x06000C66 RID: 3174 RVA: 0x00023778 File Offset: 0x00021978
		private CsdlEnumMember OnEnumMemberElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.Required("Name");
			long? num = base.OptionalLong("Value");
			return new CsdlEnumMember(text, num, element.Location);
		}

		// Token: 0x06000C67 RID: 3175 RVA: 0x000237AC File Offset: 0x000219AC
		private CsdlOnDelete OnDeleteActionElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			EdmOnDeleteAction edmOnDeleteAction = base.RequiredOnDeleteAction("Action");
			return new CsdlOnDelete(edmOnDeleteAction, element.Location);
		}

		// Token: 0x06000C68 RID: 3176 RVA: 0x000237D4 File Offset: 0x000219D4
		private CsdlReferentialConstraint OnReferentialConstraintElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.Required("Property");
			string text2 = base.Required("ReferencedProperty");
			return new CsdlReferentialConstraint(text, text2, element.Location);
		}

		// Token: 0x06000C69 RID: 3177 RVA: 0x00023808 File Offset: 0x00021A08
		internal CsdlAction OnActionElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.Required("Name");
			bool flag = base.OptionalBoolean("IsBound") ?? false;
			string text2 = base.Optional("EntitySetPath");
			IEnumerable<CsdlOperationParameter> enumerable = childValues.ValuesOfType<CsdlOperationParameter>();
			CsdlOperationReturn csdlOperationReturn = childValues.ValuesOfType<CsdlOperationReturn>().FirstOrDefault<CsdlOperationReturn>();
			this.ReportOperationReadErrorsIfExist(text2, flag, text);
			return new CsdlAction(text, enumerable, csdlOperationReturn, flag, text2, element.Location);
		}

		// Token: 0x06000C6A RID: 3178 RVA: 0x0002387C File Offset: 0x00021A7C
		internal CsdlFunction OnFunctionElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.Required("Name");
			bool flag = base.OptionalBoolean("IsBound") ?? false;
			string text2 = base.Optional("EntitySetPath");
			bool flag2 = base.OptionalBoolean("IsComposable") ?? false;
			IEnumerable<CsdlOperationParameter> enumerable = childValues.ValuesOfType<CsdlOperationParameter>();
			CsdlOperationReturn csdlOperationReturn = childValues.ValuesOfType<CsdlOperationReturn>().FirstOrDefault<CsdlOperationReturn>();
			this.ReportOperationReadErrorsIfExist(text2, flag, text);
			return new CsdlFunction(text, enumerable, csdlOperationReturn, flag, text2, flag2, element.Location);
		}

		// Token: 0x06000C6B RID: 3179 RVA: 0x00023914 File Offset: 0x00021B14
		private void ReportOperationReadErrorsIfExist(string entitySetPath, bool isBound, string name)
		{
			if (entitySetPath != null && !isBound)
			{
				base.ReportError(this.currentElement.Location, EdmErrorCode.InvalidEntitySetPath, Strings.CsdlParser_InvalidEntitySetPathWithUnboundAction("Action", name));
			}
		}

		// Token: 0x06000C6C RID: 3180 RVA: 0x00023940 File Offset: 0x00021B40
		private CsdlOperationParameter OnParameterElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.Required("Name");
			string text2 = base.OptionalType("Type");
			string text3 = null;
			bool flag = false;
			CsdlTypeReference csdlTypeReference = this.ParseTypeReference(text2, childValues, element.Location, CsdlDocumentParser.Optionality.Required);
			XmlElementValue xmlElementValue = childValues.Where((XmlElementValue c) => c is XmlElementValue<CsdlAnnotation> && (c.ValueAs<CsdlAnnotation>().Term == CoreVocabularyModel.OptionalParameterTerm.ShortQualifiedName() || c.ValueAs<CsdlAnnotation>().Term == CoreVocabularyModel.OptionalParameterTerm.FullName())).FirstOrDefault<XmlElementValue>();
			if (xmlElementValue != null)
			{
				flag = true;
				CsdlRecordExpression csdlRecordExpression = xmlElementValue.ValueAs<CsdlAnnotation>().Expression as CsdlRecordExpression;
				if (csdlRecordExpression != null)
				{
					foreach (CsdlPropertyValue csdlPropertyValue in csdlRecordExpression.PropertyValues)
					{
						CsdlConstantExpression csdlConstantExpression = csdlPropertyValue.Expression as CsdlConstantExpression;
						if (csdlConstantExpression != null && csdlPropertyValue.Property == "DefaultValue")
						{
							text3 = csdlConstantExpression.Value;
						}
					}
				}
				childValues.Remove(xmlElementValue);
			}
			return new CsdlOperationParameter(text, csdlTypeReference, element.Location, flag, text3);
		}

		// Token: 0x06000C6D RID: 3181 RVA: 0x00023A4C File Offset: 0x00021C4C
		private CsdlActionImport OnActionImportElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.Required("Name");
			string text2 = base.RequiredQualifiedName("Action");
			string text3 = base.Optional("EntitySet");
			return new CsdlActionImport(text, text2, text3, element.Location);
		}

		// Token: 0x06000C6E RID: 3182 RVA: 0x00023A8C File Offset: 0x00021C8C
		private CsdlFunctionImport OnFunctionImportElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.Required("Name");
			string text2 = base.RequiredQualifiedName("Function");
			string text3 = base.Optional("EntitySet");
			bool flag = base.OptionalBoolean("IncludeInServiceDocument") ?? false;
			return new CsdlFunctionImport(text, text2, text3, flag, element.Location);
		}

		// Token: 0x06000C6F RID: 3183 RVA: 0x00023AF0 File Offset: 0x00021CF0
		private CsdlOperationParameter OnFunctionImportParameterElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.Required("Name");
			string text2 = base.RequiredType("Type");
			CsdlTypeReference csdlTypeReference = this.ParseTypeReference(text2, null, element.Location, CsdlDocumentParser.Optionality.Required);
			return new CsdlOperationParameter(text, csdlTypeReference, element.Location);
		}

		// Token: 0x06000C70 RID: 3184 RVA: 0x00023B34 File Offset: 0x00021D34
		private CsdlTypeReference OnEntityReferenceTypeElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.RequiredType("Type");
			return new CsdlExpressionTypeReference(new CsdlEntityReferenceType(this.ParseTypeReference(text, null, element.Location, CsdlDocumentParser.Optionality.Required), element.Location), true, element.Location);
		}

		// Token: 0x06000C71 RID: 3185 RVA: 0x00023B74 File Offset: 0x00021D74
		private CsdlTypeReference OnTypeRefElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.RequiredType("Type");
			return this.ParseTypeReference(text, null, element.Location, CsdlDocumentParser.Optionality.Required);
		}

		// Token: 0x06000C72 RID: 3186 RVA: 0x00023B9C File Offset: 0x00021D9C
		private CsdlTypeReference OnCollectionTypeElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.OptionalType("ElementType");
			CsdlTypeReference csdlTypeReference = this.ParseTypeReference(text, childValues, element.Location, CsdlDocumentParser.Optionality.Required);
			return new CsdlExpressionTypeReference(new CsdlCollectionType(csdlTypeReference, element.Location), csdlTypeReference.IsNullable, element.Location);
		}

		// Token: 0x06000C73 RID: 3187 RVA: 0x00023BE4 File Offset: 0x00021DE4
		private CsdlOperationReturn OnReturnTypeElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.RequiredType("Type");
			CsdlTypeReference csdlTypeReference = this.ParseTypeReference(text, childValues, element.Location, CsdlDocumentParser.Optionality.Required);
			return new CsdlOperationReturn(csdlTypeReference, element.Location);
		}

		// Token: 0x06000C74 RID: 3188 RVA: 0x00023C1C File Offset: 0x00021E1C
		private CsdlEntityContainer OnEntityContainerElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.Required("Name");
			string text2 = base.Optional("Extends");
			int num = this.entityContainerCount;
			this.entityContainerCount = num + 1;
			if (num > 0)
			{
				base.ReportError(this.currentElement.Location, EdmErrorCode.MetadataDocumentCannotHaveMoreThanOneEntityContainer, Strings.CsdlParser_MetadataDocumentCannotHaveMoreThanOneEntityContainer);
			}
			return new CsdlEntityContainer(text, text2, childValues.ValuesOfType<CsdlEntitySet>(), childValues.ValuesOfType<CsdlSingleton>(), childValues.ValuesOfType<CsdlOperationImport>(), element.Location);
		}

		// Token: 0x06000C75 RID: 3189 RVA: 0x00023C90 File Offset: 0x00021E90
		private CsdlEntitySet OnEntitySetElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.Required("Name");
			string text2 = base.RequiredQualifiedName("EntityType");
			bool? flag = base.OptionalBoolean("IncludeInServiceDocument");
			if (flag == null)
			{
				return new CsdlEntitySet(text, text2, childValues.ValuesOfType<CsdlNavigationPropertyBinding>(), element.Location);
			}
			return new CsdlEntitySet(text, text2, childValues.ValuesOfType<CsdlNavigationPropertyBinding>(), element.Location, flag.Value);
		}

		// Token: 0x06000C76 RID: 3190 RVA: 0x00023CF8 File Offset: 0x00021EF8
		private CsdlSingleton OnSingletonElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.Required("Name");
			string text2 = base.RequiredQualifiedName("Type");
			return new CsdlSingleton(text, text2, childValues.ValuesOfType<CsdlNavigationPropertyBinding>(), element.Location);
		}

		// Token: 0x06000C77 RID: 3191 RVA: 0x00023D30 File Offset: 0x00021F30
		private CsdlNavigationPropertyBinding OnNavigationPropertyBindingElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.Required("Path");
			string text2 = base.Required("Target");
			return new CsdlNavigationPropertyBinding(text, text2, element.Location);
		}

		// Token: 0x06000C78 RID: 3192 RVA: 0x00023D64 File Offset: 0x00021F64
		private void ParseMaxLength(out bool Unbounded, out int? maxLength)
		{
			string text = base.Optional("MaxLength");
			if (text == null)
			{
				Unbounded = false;
				maxLength = null;
				return;
			}
			if (text.EqualsOrdinalIgnoreCase("max"))
			{
				Unbounded = true;
				maxLength = null;
				return;
			}
			Unbounded = false;
			maxLength = base.OptionalMaxLength("MaxLength");
		}

		// Token: 0x06000C79 RID: 3193 RVA: 0x00023DB7 File Offset: 0x00021FB7
		private void ParseBinaryFacets(out bool Unbounded, out int? maxLength)
		{
			this.ParseMaxLength(out Unbounded, out maxLength);
		}

		// Token: 0x06000C7A RID: 3194 RVA: 0x00023DC1 File Offset: 0x00021FC1
		private void ParseDecimalFacets(out int? precision, out int? scale)
		{
			precision = base.OptionalInteger("Precision");
			scale = base.OptionalScale("Scale");
		}

		// Token: 0x06000C7B RID: 3195 RVA: 0x00023DE8 File Offset: 0x00021FE8
		private void ParseStringFacets(out bool Unbounded, out int? maxLength, out bool? unicode)
		{
			this.ParseMaxLength(out Unbounded, out maxLength);
			unicode = new bool?(base.OptionalBoolean("Unicode") ?? true);
		}

		// Token: 0x06000C7C RID: 3196 RVA: 0x00023E28 File Offset: 0x00022028
		private void ParseTemporalFacets(out int? precision)
		{
			precision = new int?(base.OptionalInteger("Precision") ?? 0);
		}

		// Token: 0x06000C7D RID: 3197 RVA: 0x00023E5F File Offset: 0x0002205F
		private void ParseSpatialFacets(out int? srid, int defaultSrid)
		{
			srid = base.OptionalSrid("SRID", defaultSrid);
		}

		// Token: 0x06000C7E RID: 3198 RVA: 0x00023E74 File Offset: 0x00022074
		private void ParseTypeDefinitionFacets(out bool isUnbounded, out int? maxLength, out bool? unicode, out int? precision, out int? scale, out int? srid)
		{
			this.ParseMaxLength(out isUnbounded, out maxLength);
			unicode = base.OptionalBoolean("Unicode");
			precision = base.OptionalInteger("Precision");
			scale = base.OptionalScale("Scale");
			srid = base.OptionalSrid("SRID", int.MinValue);
		}

		// Token: 0x04000711 RID: 1809
		private Version artifactVersion;

		// Token: 0x04000712 RID: 1810
		private int entityContainerCount;

		// Token: 0x020002F5 RID: 757
		private enum Optionality
		{
			// Token: 0x040008E6 RID: 2278
			Optional,
			// Token: 0x040008E7 RID: 2279
			Required
		}
	}
}
