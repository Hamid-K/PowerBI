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
	// Token: 0x020001AB RID: 427
	internal class CsdlDocumentParser : EdmXmlDocumentParser<CsdlSchema>
	{
		// Token: 0x06000B83 RID: 2947 RVA: 0x0001FC5E File Offset: 0x0001DE5E
		internal CsdlDocumentParser(string documentPath, XmlReader reader)
			: base(documentPath, reader)
		{
			this.entityContainerCount = 0;
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06000B84 RID: 2948 RVA: 0x0001FC6F File Offset: 0x0001DE6F
		internal override IEnumerable<KeyValuePair<Version, string>> SupportedVersions
		{
			get
			{
				return Enumerable.SelectMany<KeyValuePair<Version, string[]>, KeyValuePair<Version, string>>(CsdlConstants.SupportedVersions, (KeyValuePair<Version, string[]> kvp) => Enumerable.Select<string, KeyValuePair<Version, string>>(kvp.Value, (string ns) => new KeyValuePair<Version, string>(kvp.Key, ns)));
			}
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x0001FC9A File Offset: 0x0001DE9A
		protected override bool TryGetDocumentElementParser(Version csdlArtifactVersion, XmlElementInfo rootElement, out XmlElementParser<CsdlSchema> parser)
		{
			EdmUtil.CheckArgumentNull<XmlElementInfo>(rootElement, "rootElement");
			this.artifactVersion = csdlArtifactVersion;
			if (string.Equals(rootElement.Name, "Schema", 4))
			{
				parser = this.CreateRootElementParser();
				return true;
			}
			parser = null;
			return false;
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x0001FCD0 File Offset: 0x0001DED0
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

		// Token: 0x06000B87 RID: 2951 RVA: 0x0001FD90 File Offset: 0x0001DF90
		private XmlElementParser<CsdlSchema> CreateRootElementParser()
		{
			string text = "Documentation";
			Func<XmlElementInfo, XmlElementValueCollection, CsdlDocumentation> func = new Func<XmlElementInfo, XmlElementValueCollection, CsdlDocumentation>(this.OnDocumentationElement);
			XmlElementParser[] array = new XmlElementParser[2];
			array[0] = this.Element<string>("Summary", (XmlElementInfo element, XmlElementValueCollection children) => children.FirstText.Value, new XmlElementParser[0]);
			array[1] = this.Element<string>("LongDescription", (XmlElementInfo element, XmlElementValueCollection children) => children.FirstText.TextValue, new XmlElementParser[0]);
			XmlElementParser<CsdlDocumentation> xmlElementParser = base.CsdlElement<CsdlDocumentation>(text, func, array);
			XmlElementParser<CsdlTypeReference> xmlElementParser2 = base.CsdlElement<CsdlTypeReference>("ReferenceType", new Func<XmlElementInfo, XmlElementValueCollection, CsdlTypeReference>(this.OnEntityReferenceTypeElement), new XmlElementParser[] { xmlElementParser });
			XmlElementParser<CsdlTypeReference> xmlElementParser3 = base.CsdlElement<CsdlTypeReference>("CollectionType", new Func<XmlElementInfo, XmlElementValueCollection, CsdlTypeReference>(this.OnCollectionTypeElement), new XmlElementParser[]
			{
				xmlElementParser,
				base.CsdlElement<CsdlTypeReference>("TypeRef", new Func<XmlElementInfo, XmlElementValueCollection, CsdlTypeReference>(this.OnTypeRefElement), new XmlElementParser[] { xmlElementParser }),
				xmlElementParser2
			});
			XmlElementParser<CsdlProperty> xmlElementParser4 = base.CsdlElement<CsdlProperty>("Property", new Func<XmlElementInfo, XmlElementValueCollection, CsdlProperty>(this.OnPropertyElement), new XmlElementParser[] { xmlElementParser });
			XmlElementParser<CsdlExpressionBase> xmlElementParser5 = base.CsdlElement<CsdlExpressionBase>("String", new Func<XmlElementInfo, XmlElementValueCollection, CsdlExpressionBase>(CsdlDocumentParser.OnStringConstantExpression), new XmlElementParser[0]);
			XmlElementParser<CsdlExpressionBase> xmlElementParser6 = base.CsdlElement<CsdlExpressionBase>("Binary", new Func<XmlElementInfo, XmlElementValueCollection, CsdlExpressionBase>(CsdlDocumentParser.OnBinaryConstantExpression), new XmlElementParser[0]);
			XmlElementParser<CsdlExpressionBase> xmlElementParser7 = base.CsdlElement<CsdlExpressionBase>("Int", new Func<XmlElementInfo, XmlElementValueCollection, CsdlExpressionBase>(CsdlDocumentParser.OnIntConstantExpression), new XmlElementParser[0]);
			XmlElementParser<CsdlExpressionBase> xmlElementParser8 = base.CsdlElement<CsdlExpressionBase>("Float", new Func<XmlElementInfo, XmlElementValueCollection, CsdlExpressionBase>(CsdlDocumentParser.OnFloatConstantExpression), new XmlElementParser[0]);
			XmlElementParser<CsdlExpressionBase> xmlElementParser9 = base.CsdlElement<CsdlExpressionBase>("Guid", new Func<XmlElementInfo, XmlElementValueCollection, CsdlExpressionBase>(CsdlDocumentParser.OnGuidConstantExpression), new XmlElementParser[0]);
			XmlElementParser<CsdlExpressionBase> xmlElementParser10 = base.CsdlElement<CsdlExpressionBase>("Decimal", new Func<XmlElementInfo, XmlElementValueCollection, CsdlExpressionBase>(CsdlDocumentParser.OnDecimalConstantExpression), new XmlElementParser[0]);
			XmlElementParser<CsdlExpressionBase> xmlElementParser11 = base.CsdlElement<CsdlExpressionBase>("Bool", new Func<XmlElementInfo, XmlElementValueCollection, CsdlExpressionBase>(CsdlDocumentParser.OnBoolConstantExpression), new XmlElementParser[0]);
			XmlElementParser<CsdlExpressionBase> xmlElementParser12 = base.CsdlElement<CsdlExpressionBase>("Duration", new Func<XmlElementInfo, XmlElementValueCollection, CsdlExpressionBase>(CsdlDocumentParser.OnDurationConstantExpression), new XmlElementParser[0]);
			XmlElementParser<CsdlExpressionBase> xmlElementParser13 = base.CsdlElement<CsdlExpressionBase>("Date", new Func<XmlElementInfo, XmlElementValueCollection, CsdlExpressionBase>(CsdlDocumentParser.OnDateConstantExpression), new XmlElementParser[0]);
			XmlElementParser<CsdlExpressionBase> xmlElementParser14 = base.CsdlElement<CsdlExpressionBase>("TimeOfDay", new Func<XmlElementInfo, XmlElementValueCollection, CsdlExpressionBase>(CsdlDocumentParser.OnTimeOfDayConstantExpression), new XmlElementParser[0]);
			XmlElementParser<CsdlExpressionBase> xmlElementParser15 = base.CsdlElement<CsdlExpressionBase>("DateTimeOffset", new Func<XmlElementInfo, XmlElementValueCollection, CsdlExpressionBase>(CsdlDocumentParser.OnDateTimeOffsetConstantExpression), new XmlElementParser[0]);
			XmlElementParser<CsdlExpressionBase> xmlElementParser16 = base.CsdlElement<CsdlExpressionBase>("Null", new Func<XmlElementInfo, XmlElementValueCollection, CsdlExpressionBase>(CsdlDocumentParser.OnNullConstantExpression), new XmlElementParser[0]);
			XmlElementParser<CsdlExpressionBase> xmlElementParser17 = base.CsdlElement<CsdlExpressionBase>("Path", new Func<XmlElementInfo, XmlElementValueCollection, CsdlExpressionBase>(CsdlDocumentParser.OnPathExpression), new XmlElementParser[0]);
			XmlElementParser<CsdlExpressionBase> xmlElementParser18 = base.CsdlElement<CsdlExpressionBase>("PropertyPath", new Func<XmlElementInfo, XmlElementValueCollection, CsdlExpressionBase>(CsdlDocumentParser.OnPropertyPathExpression), new XmlElementParser[0]);
			XmlElementParser<CsdlExpressionBase> xmlElementParser19 = base.CsdlElement<CsdlExpressionBase>("NavigationPropertyPath", new Func<XmlElementInfo, XmlElementValueCollection, CsdlExpressionBase>(CsdlDocumentParser.OnNavigationPropertyPathExpression), new XmlElementParser[0]);
			XmlElementParser<CsdlExpressionBase> xmlElementParser20 = base.CsdlElement<CsdlExpressionBase>("EnumMember", new Func<XmlElementInfo, XmlElementValueCollection, CsdlExpressionBase>(this.OnEnumMemberExpression), new XmlElementParser[0]);
			XmlElementParser<CsdlExpressionBase> xmlElementParser21 = base.CsdlElement<CsdlExpressionBase>("If", new Func<XmlElementInfo, XmlElementValueCollection, CsdlExpressionBase>(this.OnIfExpression), new XmlElementParser[0]);
			XmlElementParser<CsdlExpressionBase> xmlElementParser22 = base.CsdlElement<CsdlExpressionBase>("Cast", new Func<XmlElementInfo, XmlElementValueCollection, CsdlExpressionBase>(this.OnCastExpression), new XmlElementParser[0]);
			XmlElementParser<CsdlExpressionBase> xmlElementParser23 = base.CsdlElement<CsdlExpressionBase>("IsType", new Func<XmlElementInfo, XmlElementValueCollection, CsdlExpressionBase>(this.OnIsTypeExpression), new XmlElementParser[0]);
			XmlElementParser<CsdlPropertyValue> xmlElementParser24 = base.CsdlElement<CsdlPropertyValue>("PropertyValue", new Func<XmlElementInfo, XmlElementValueCollection, CsdlPropertyValue>(this.OnPropertyValueElement), new XmlElementParser[0]);
			XmlElementParser<CsdlRecordExpression> xmlElementParser25 = base.CsdlElement<CsdlRecordExpression>("Record", new Func<XmlElementInfo, XmlElementValueCollection, CsdlRecordExpression>(this.OnRecordElement), new XmlElementParser[] { xmlElementParser24 });
			XmlElementParser<CsdlLabeledExpression> xmlElementParser26 = base.CsdlElement<CsdlLabeledExpression>("LabeledElement", new Func<XmlElementInfo, XmlElementValueCollection, CsdlLabeledExpression>(this.OnLabeledElement), new XmlElementParser[0]);
			XmlElementParser<CsdlCollectionExpression> xmlElementParser27 = base.CsdlElement<CsdlCollectionExpression>("Collection", new Func<XmlElementInfo, XmlElementValueCollection, CsdlCollectionExpression>(this.OnCollectionElement), new XmlElementParser[0]);
			XmlElementParser<CsdlExpressionBase> xmlElementParser28 = base.CsdlElement<CsdlExpressionBase>("Apply", new Func<XmlElementInfo, XmlElementValueCollection, CsdlExpressionBase>(this.OnApplyElement), new XmlElementParser[0]);
			XmlElementParser<CsdlExpressionBase> xmlElementParser29 = base.CsdlElement<CsdlExpressionBase>("LabeledElementReference", new Func<XmlElementInfo, XmlElementValueCollection, CsdlExpressionBase>(this.OnLabeledElementReferenceExpression), new XmlElementParser[0]);
			XmlElementParser[] array2 = new XmlElementParser[]
			{
				xmlElementParser5, xmlElementParser6, xmlElementParser7, xmlElementParser8, xmlElementParser9, xmlElementParser10, xmlElementParser11, xmlElementParser13, xmlElementParser15, xmlElementParser12,
				xmlElementParser14, xmlElementParser16, xmlElementParser17, xmlElementParser18, xmlElementParser19, xmlElementParser21, xmlElementParser23, xmlElementParser22, xmlElementParser25, xmlElementParser27,
				xmlElementParser29, xmlElementParser24, xmlElementParser26, xmlElementParser20, xmlElementParser28
			};
			CsdlDocumentParser.AddChildParsers(xmlElementParser21, array2);
			CsdlDocumentParser.AddChildParsers(xmlElementParser22, array2);
			CsdlDocumentParser.AddChildParsers(xmlElementParser23, array2);
			CsdlDocumentParser.AddChildParsers(xmlElementParser24, array2);
			CsdlDocumentParser.AddChildParsers(xmlElementParser27, array2);
			CsdlDocumentParser.AddChildParsers(xmlElementParser26, array2);
			CsdlDocumentParser.AddChildParsers(xmlElementParser28, array2);
			XmlElementParser<CsdlAnnotation> xmlElementParser30 = base.CsdlElement<CsdlAnnotation>("Annotation", new Func<XmlElementInfo, XmlElementValueCollection, CsdlAnnotation>(this.OnAnnotationElement), new XmlElementParser[0]);
			CsdlDocumentParser.AddChildParsers(xmlElementParser30, array2);
			xmlElementParser4.AddChildParser(xmlElementParser30);
			xmlElementParser3.AddChildParser(xmlElementParser3);
			return base.CsdlElement<CsdlSchema>("Schema", new Func<XmlElementInfo, XmlElementValueCollection, CsdlSchema>(this.OnSchemaElement), new XmlElementParser[]
			{
				xmlElementParser,
				base.CsdlElement<CsdlComplexType>("ComplexType", new Func<XmlElementInfo, XmlElementValueCollection, CsdlComplexType>(this.OnComplexTypeElement), new XmlElementParser[]
				{
					xmlElementParser,
					xmlElementParser4,
					base.CsdlElement<CsdlNamedElement>("NavigationProperty", new Func<XmlElementInfo, XmlElementValueCollection, CsdlNamedElement>(this.OnNavigationPropertyElement), new XmlElementParser[]
					{
						xmlElementParser,
						base.CsdlElement<CsdlReferentialConstraint>("ReferentialConstraint", new Func<XmlElementInfo, XmlElementValueCollection, CsdlReferentialConstraint>(this.OnReferentialConstraintElement), new XmlElementParser[] { xmlElementParser }),
						base.CsdlElement<CsdlOnDelete>("OnDelete", new Func<XmlElementInfo, XmlElementValueCollection, CsdlOnDelete>(this.OnDeleteActionElement), new XmlElementParser[] { xmlElementParser }),
						xmlElementParser30
					}),
					xmlElementParser30
				}),
				base.CsdlElement<CsdlEntityType>("EntityType", new Func<XmlElementInfo, XmlElementValueCollection, CsdlEntityType>(this.OnEntityTypeElement), new XmlElementParser[]
				{
					xmlElementParser,
					base.CsdlElement<CsdlKey>("Key", new Func<XmlElementInfo, XmlElementValueCollection, CsdlKey>(CsdlDocumentParser.OnEntityKeyElement), new XmlElementParser[] { base.CsdlElement<CsdlPropertyReference>("PropertyRef", new Func<XmlElementInfo, XmlElementValueCollection, CsdlPropertyReference>(this.OnPropertyRefElement), new XmlElementParser[0]) }),
					xmlElementParser4,
					base.CsdlElement<CsdlNamedElement>("NavigationProperty", new Func<XmlElementInfo, XmlElementValueCollection, CsdlNamedElement>(this.OnNavigationPropertyElement), new XmlElementParser[]
					{
						xmlElementParser,
						base.CsdlElement<CsdlReferentialConstraint>("ReferentialConstraint", new Func<XmlElementInfo, XmlElementValueCollection, CsdlReferentialConstraint>(this.OnReferentialConstraintElement), new XmlElementParser[] { xmlElementParser }),
						base.CsdlElement<CsdlOnDelete>("OnDelete", new Func<XmlElementInfo, XmlElementValueCollection, CsdlOnDelete>(this.OnDeleteActionElement), new XmlElementParser[] { xmlElementParser }),
						xmlElementParser30
					}),
					xmlElementParser30
				}),
				base.CsdlElement<CsdlEnumType>("EnumType", new Func<XmlElementInfo, XmlElementValueCollection, CsdlEnumType>(this.OnEnumTypeElement), new XmlElementParser[]
				{
					xmlElementParser,
					base.CsdlElement<CsdlEnumMember>("Member", new Func<XmlElementInfo, XmlElementValueCollection, CsdlEnumMember>(this.OnEnumMemberElement), new XmlElementParser[] { xmlElementParser, xmlElementParser30 }),
					xmlElementParser30
				}),
				base.CsdlElement<CsdlTypeDefinition>("TypeDefinition", new Func<XmlElementInfo, XmlElementValueCollection, CsdlTypeDefinition>(this.OnTypeDefinitionElement), new XmlElementParser[] { xmlElementParser30 }),
				base.CsdlElement<CsdlAction>("Action", new Func<XmlElementInfo, XmlElementValueCollection, CsdlAction>(this.OnActionElement), new XmlElementParser[]
				{
					xmlElementParser,
					base.CsdlElement<CsdlOperationParameter>("Parameter", new Func<XmlElementInfo, XmlElementValueCollection, CsdlOperationParameter>(this.OnParameterElement), new XmlElementParser[]
					{
						xmlElementParser,
						base.CsdlElement<CsdlTypeReference>("TypeRef", new Func<XmlElementInfo, XmlElementValueCollection, CsdlTypeReference>(this.OnTypeRefElement), new XmlElementParser[] { xmlElementParser }),
						xmlElementParser3,
						xmlElementParser2,
						xmlElementParser30
					}),
					base.CsdlElement<CsdlOperationReturnType>("ReturnType", new Func<XmlElementInfo, XmlElementValueCollection, CsdlOperationReturnType>(this.OnReturnTypeElement), new XmlElementParser[]
					{
						xmlElementParser,
						base.CsdlElement<CsdlTypeReference>("TypeRef", new Func<XmlElementInfo, XmlElementValueCollection, CsdlTypeReference>(this.OnTypeRefElement), new XmlElementParser[] { xmlElementParser }),
						xmlElementParser3,
						xmlElementParser2,
						xmlElementParser30
					}),
					xmlElementParser30
				}),
				base.CsdlElement<CsdlOperation>("Function", new Func<XmlElementInfo, XmlElementValueCollection, CsdlOperation>(this.OnFunctionElement), new XmlElementParser[]
				{
					xmlElementParser,
					base.CsdlElement<CsdlOperationParameter>("Parameter", new Func<XmlElementInfo, XmlElementValueCollection, CsdlOperationParameter>(this.OnParameterElement), new XmlElementParser[]
					{
						xmlElementParser,
						base.CsdlElement<CsdlTypeReference>("TypeRef", new Func<XmlElementInfo, XmlElementValueCollection, CsdlTypeReference>(this.OnTypeRefElement), new XmlElementParser[] { xmlElementParser }),
						xmlElementParser3,
						xmlElementParser2,
						xmlElementParser30
					}),
					base.CsdlElement<CsdlOperationReturnType>("ReturnType", new Func<XmlElementInfo, XmlElementValueCollection, CsdlOperationReturnType>(this.OnReturnTypeElement), new XmlElementParser[]
					{
						xmlElementParser,
						base.CsdlElement<CsdlTypeReference>("TypeRef", new Func<XmlElementInfo, XmlElementValueCollection, CsdlTypeReference>(this.OnTypeRefElement), new XmlElementParser[] { xmlElementParser }),
						xmlElementParser3,
						xmlElementParser2,
						xmlElementParser30
					}),
					xmlElementParser30
				}),
				base.CsdlElement<CsdlTerm>("Term", new Func<XmlElementInfo, XmlElementValueCollection, CsdlTerm>(this.OnTermElement), new XmlElementParser[]
				{
					base.CsdlElement<CsdlTypeReference>("TypeRef", new Func<XmlElementInfo, XmlElementValueCollection, CsdlTypeReference>(this.OnTypeRefElement), new XmlElementParser[] { xmlElementParser }),
					xmlElementParser3,
					xmlElementParser2,
					xmlElementParser30
				}),
				base.CsdlElement<CsdlAnnotations>("Annotations", new Func<XmlElementInfo, XmlElementValueCollection, CsdlAnnotations>(this.OnAnnotationsElement), new XmlElementParser[] { xmlElementParser30 }),
				base.CsdlElement<CsdlEntityContainer>("EntityContainer", new Func<XmlElementInfo, XmlElementValueCollection, CsdlEntityContainer>(this.OnEntityContainerElement), new XmlElementParser[]
				{
					xmlElementParser,
					base.CsdlElement<CsdlEntitySet>("EntitySet", new Func<XmlElementInfo, XmlElementValueCollection, CsdlEntitySet>(this.OnEntitySetElement), new XmlElementParser[]
					{
						xmlElementParser,
						base.CsdlElement<CsdlNavigationPropertyBinding>("NavigationPropertyBinding", new Func<XmlElementInfo, XmlElementValueCollection, CsdlNavigationPropertyBinding>(this.OnNavigationPropertyBindingElement), new XmlElementParser[0]),
						xmlElementParser30
					}),
					base.CsdlElement<CsdlSingleton>("Singleton", new Func<XmlElementInfo, XmlElementValueCollection, CsdlSingleton>(this.OnSingletonElement), new XmlElementParser[]
					{
						xmlElementParser,
						base.CsdlElement<CsdlNavigationPropertyBinding>("NavigationPropertyBinding", new Func<XmlElementInfo, XmlElementValueCollection, CsdlNavigationPropertyBinding>(this.OnNavigationPropertyBindingElement), new XmlElementParser[0]),
						xmlElementParser30
					}),
					base.CsdlElement<CsdlActionImport>("ActionImport", new Func<XmlElementInfo, XmlElementValueCollection, CsdlActionImport>(this.OnActionImportElement), new XmlElementParser[] { xmlElementParser, xmlElementParser30 }),
					base.CsdlElement<CsdlOperationImport>("FunctionImport", new Func<XmlElementInfo, XmlElementValueCollection, CsdlOperationImport>(this.OnFunctionImportElement), new XmlElementParser[]
					{
						xmlElementParser,
						base.CsdlElement<CsdlOperationParameter>("Parameter", new Func<XmlElementInfo, XmlElementValueCollection, CsdlOperationParameter>(this.OnFunctionImportParameterElement), new XmlElementParser[] { xmlElementParser, xmlElementParser30 }),
						xmlElementParser30
					}),
					xmlElementParser30
				})
			});
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x00020833 File Offset: 0x0001EA33
		private static CsdlDocumentation Documentation(XmlElementValueCollection childValues)
		{
			return Enumerable.FirstOrDefault<CsdlDocumentation>(childValues.ValuesOfType<CsdlDocumentation>());
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x00020840 File Offset: 0x0001EA40
		private CsdlSchema OnSchemaElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.Optional("Namespace") ?? string.Empty;
			string text2 = base.OptionalAlias("Alias");
			return new CsdlSchema(text, text2, this.artifactVersion, childValues.ValuesOfType<CsdlStructuredType>(), childValues.ValuesOfType<CsdlEnumType>(), childValues.ValuesOfType<CsdlOperation>(), childValues.ValuesOfType<CsdlTerm>(), childValues.ValuesOfType<CsdlEntityContainer>(), childValues.ValuesOfType<CsdlAnnotations>(), childValues.ValuesOfType<CsdlTypeDefinition>(), CsdlDocumentParser.Documentation(childValues), element.Location);
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x000208B3 File Offset: 0x0001EAB3
		private CsdlDocumentation OnDocumentationElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			return new CsdlDocumentation(childValues["Summary"].TextValue, childValues["LongDescription"].TextValue, element.Location);
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x000208E0 File Offset: 0x0001EAE0
		private CsdlComplexType OnComplexTypeElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.Required("Name");
			string text2 = base.OptionalQualifiedName("BaseType");
			bool flag = base.OptionalBoolean("OpenType") ?? false;
			bool flag2 = base.OptionalBoolean("Abstract") ?? false;
			return new CsdlComplexType(text, text2, flag2, flag, childValues.ValuesOfType<CsdlProperty>(), childValues.ValuesOfType<CsdlNavigationProperty>(), CsdlDocumentParser.Documentation(childValues), element.Location);
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x00020968 File Offset: 0x0001EB68
		private CsdlEntityType OnEntityTypeElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.Required("Name");
			string text2 = base.OptionalQualifiedName("BaseType");
			bool flag = base.OptionalBoolean("OpenType") ?? false;
			bool flag2 = base.OptionalBoolean("Abstract") ?? false;
			bool flag3 = base.OptionalBoolean("HasStream") ?? false;
			CsdlKey csdlKey = Enumerable.FirstOrDefault<CsdlKey>(childValues.ValuesOfType<CsdlKey>());
			return new CsdlEntityType(text, text2, flag2, flag, flag3, csdlKey, childValues.ValuesOfType<CsdlProperty>(), childValues.ValuesOfType<CsdlNavigationProperty>(), CsdlDocumentParser.Documentation(childValues), element.Location);
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x00020A24 File Offset: 0x0001EC24
		private CsdlProperty OnPropertyElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.OptionalType("Type");
			CsdlTypeReference csdlTypeReference = this.ParseTypeReference(text, childValues, element.Location, CsdlDocumentParser.Optionality.Required);
			string text2 = base.Required("Name");
			string text3 = base.Optional("DefaultValue");
			return new CsdlProperty(text2, csdlTypeReference, text3, CsdlDocumentParser.Documentation(childValues), element.Location);
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x00020A7C File Offset: 0x0001EC7C
		private CsdlTerm OnTermElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.OptionalType("Type");
			CsdlTypeReference csdlTypeReference = this.ParseTypeReference(text, childValues, element.Location, CsdlDocumentParser.Optionality.Required);
			string text2 = base.Required("Name");
			string text3 = base.Optional("AppliesTo");
			string text4 = base.Optional("DefaultValue");
			return new CsdlTerm(text2, csdlTypeReference, text3, text4, CsdlDocumentParser.Documentation(childValues), element.Location);
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x00020AE0 File Offset: 0x0001ECE0
		private CsdlAnnotations OnAnnotationsElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.Required("Target");
			string text2 = base.Optional("Qualifier");
			IEnumerable<CsdlAnnotation> enumerable = childValues.ValuesOfType<CsdlAnnotation>();
			return new CsdlAnnotations(enumerable, text, text2);
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x00020B14 File Offset: 0x0001ED14
		private CsdlAnnotation OnAnnotationElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.RequiredQualifiedName("Term");
			string text2 = base.Optional("Qualifier");
			CsdlExpressionBase csdlExpressionBase = this.ParseAnnotationExpression(element, childValues);
			return new CsdlAnnotation(text, text2, csdlExpressionBase, element.Location);
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x00020B50 File Offset: 0x0001ED50
		private CsdlPropertyValue OnPropertyValueElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.Required("Property");
			CsdlExpressionBase csdlExpressionBase = this.ParseAnnotationExpression(element, childValues);
			return new CsdlPropertyValue(text, csdlExpressionBase, element.Location);
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x00020B80 File Offset: 0x0001ED80
		private CsdlRecordExpression OnRecordElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.OptionalQualifiedName("Type");
			IEnumerable<CsdlPropertyValue> enumerable = childValues.ValuesOfType<CsdlPropertyValue>();
			return new CsdlRecordExpression((text != null) ? new CsdlNamedTypeReference(text, false, element.Location) : null, enumerable, element.Location);
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x00020BC0 File Offset: 0x0001EDC0
		private CsdlCollectionExpression OnCollectionElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.OptionalType("Type");
			CsdlTypeReference csdlTypeReference = this.ParseTypeReference(text, childValues, element.Location, CsdlDocumentParser.Optionality.Optional);
			IEnumerable<CsdlExpressionBase> enumerable = childValues.ValuesOfType<CsdlExpressionBase>();
			return new CsdlCollectionExpression(csdlTypeReference, enumerable, element.Location);
		}

		// Token: 0x06000B94 RID: 2964 RVA: 0x00020C00 File Offset: 0x0001EE00
		private CsdlLabeledExpression OnLabeledElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.Required("Name");
			IEnumerable<CsdlExpressionBase> enumerable = childValues.ValuesOfType<CsdlExpressionBase>();
			if (Enumerable.Count<CsdlExpressionBase>(enumerable) != 1)
			{
				base.ReportError(element.Location, EdmErrorCode.InvalidLabeledElementExpressionIncorrectNumberOfOperands, Strings.CsdlParser_InvalidLabeledElementExpressionIncorrectNumberOfOperands);
			}
			return new CsdlLabeledExpression(text, Enumerable.ElementAtOrDefault<CsdlExpressionBase>(enumerable, 0), element.Location);
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x00020C54 File Offset: 0x0001EE54
		private CsdlApplyExpression OnApplyElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.Optional("Function");
			IEnumerable<CsdlExpressionBase> enumerable = childValues.ValuesOfType<CsdlExpressionBase>();
			return new CsdlApplyExpression(text, enumerable, element.Location);
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x00020C84 File Offset: 0x0001EE84
		private static void AddChildParsers(XmlElementParser parent, IEnumerable<XmlElementParser> children)
		{
			foreach (XmlElementParser xmlElementParser in children)
			{
				parent.AddChildParser(xmlElementParser);
			}
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x00020CCC File Offset: 0x0001EECC
		private static CsdlConstantExpression ConstantExpression(EdmValueKind kind, XmlElementValueCollection childValues, CsdlLocation location)
		{
			XmlTextValue firstText = childValues.FirstText;
			return new CsdlConstantExpression(kind, (firstText != null) ? firstText.TextValue : string.Empty, location);
		}

		// Token: 0x06000B98 RID: 2968 RVA: 0x00020CF7 File Offset: 0x0001EEF7
		private static CsdlConstantExpression OnIntConstantExpression(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			return CsdlDocumentParser.ConstantExpression(EdmValueKind.Integer, childValues, element.Location);
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x00020D07 File Offset: 0x0001EF07
		private static CsdlConstantExpression OnStringConstantExpression(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			return CsdlDocumentParser.ConstantExpression(EdmValueKind.String, childValues, element.Location);
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x00020D17 File Offset: 0x0001EF17
		private static CsdlConstantExpression OnBinaryConstantExpression(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			return CsdlDocumentParser.ConstantExpression(EdmValueKind.Binary, childValues, element.Location);
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x00020D26 File Offset: 0x0001EF26
		private static CsdlConstantExpression OnFloatConstantExpression(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			return CsdlDocumentParser.ConstantExpression(EdmValueKind.Floating, childValues, element.Location);
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x00020D35 File Offset: 0x0001EF35
		private static CsdlConstantExpression OnGuidConstantExpression(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			return CsdlDocumentParser.ConstantExpression(EdmValueKind.Guid, childValues, element.Location);
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x00020D44 File Offset: 0x0001EF44
		private static CsdlConstantExpression OnDecimalConstantExpression(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			return CsdlDocumentParser.ConstantExpression(EdmValueKind.Decimal, childValues, element.Location);
		}

		// Token: 0x06000B9E RID: 2974 RVA: 0x00020D53 File Offset: 0x0001EF53
		private static CsdlConstantExpression OnBoolConstantExpression(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			return CsdlDocumentParser.ConstantExpression(EdmValueKind.Boolean, childValues, element.Location);
		}

		// Token: 0x06000B9F RID: 2975 RVA: 0x00020D62 File Offset: 0x0001EF62
		private static CsdlConstantExpression OnDurationConstantExpression(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			return CsdlDocumentParser.ConstantExpression(EdmValueKind.Duration, childValues, element.Location);
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x00020D72 File Offset: 0x0001EF72
		private static CsdlConstantExpression OnDateConstantExpression(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			return CsdlDocumentParser.ConstantExpression(EdmValueKind.Date, childValues, element.Location);
		}

		// Token: 0x06000BA1 RID: 2977 RVA: 0x00020D82 File Offset: 0x0001EF82
		private static CsdlConstantExpression OnDateTimeOffsetConstantExpression(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			return CsdlDocumentParser.ConstantExpression(EdmValueKind.DateTimeOffset, childValues, element.Location);
		}

		// Token: 0x06000BA2 RID: 2978 RVA: 0x00020D91 File Offset: 0x0001EF91
		private static CsdlConstantExpression OnTimeOfDayConstantExpression(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			return CsdlDocumentParser.ConstantExpression(EdmValueKind.TimeOfDay, childValues, element.Location);
		}

		// Token: 0x06000BA3 RID: 2979 RVA: 0x00020DA1 File Offset: 0x0001EFA1
		private static CsdlConstantExpression OnNullConstantExpression(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			return CsdlDocumentParser.ConstantExpression(EdmValueKind.Null, childValues, element.Location);
		}

		// Token: 0x06000BA4 RID: 2980 RVA: 0x00020DB4 File Offset: 0x0001EFB4
		private static CsdlPathExpression OnPathExpression(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			XmlTextValue firstText = childValues.FirstText;
			return new CsdlPathExpression((firstText != null) ? firstText.TextValue : string.Empty, element.Location);
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x00020DE4 File Offset: 0x0001EFE4
		private static CsdlPropertyPathExpression OnPropertyPathExpression(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			XmlTextValue firstText = childValues.FirstText;
			return new CsdlPropertyPathExpression((firstText != null) ? firstText.TextValue : string.Empty, element.Location);
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x00020E14 File Offset: 0x0001F014
		private static CsdlNavigationPropertyPathExpression OnNavigationPropertyPathExpression(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			XmlTextValue firstText = childValues.FirstText;
			return new CsdlNavigationPropertyPathExpression((firstText != null) ? firstText.TextValue : string.Empty, element.Location);
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x00020E44 File Offset: 0x0001F044
		private CsdlLabeledExpressionReferenceExpression OnLabeledElementReferenceExpression(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.Required("Name");
			return new CsdlLabeledExpressionReferenceExpression(text, element.Location);
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x00020E6C File Offset: 0x0001F06C
		private CsdlEnumMemberExpression OnEnumMemberExpression(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.RequiredEnumMemberPath(childValues.FirstText);
			return new CsdlEnumMemberExpression(text, element.Location);
		}

		// Token: 0x06000BA9 RID: 2985 RVA: 0x00020E94 File Offset: 0x0001F094
		private CsdlExpressionBase OnIfExpression(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			IEnumerable<CsdlExpressionBase> enumerable = childValues.ValuesOfType<CsdlExpressionBase>();
			if (Enumerable.Count<CsdlExpressionBase>(enumerable) != 3)
			{
				base.ReportError(element.Location, EdmErrorCode.InvalidIfExpressionIncorrectNumberOfOperands, Strings.CsdlParser_InvalidIfExpressionIncorrectNumberOfOperands);
			}
			return new CsdlIfExpression(Enumerable.ElementAtOrDefault<CsdlExpressionBase>(enumerable, 0), Enumerable.ElementAtOrDefault<CsdlExpressionBase>(enumerable, 1), Enumerable.ElementAtOrDefault<CsdlExpressionBase>(enumerable, 2), element.Location);
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x00020EE8 File Offset: 0x0001F0E8
		private CsdlExpressionBase OnCastExpression(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.OptionalType("Type");
			CsdlTypeReference csdlTypeReference = this.ParseTypeReference(text, childValues, element.Location, CsdlDocumentParser.Optionality.Required);
			IEnumerable<CsdlExpressionBase> enumerable = childValues.ValuesOfType<CsdlExpressionBase>();
			if (Enumerable.Count<CsdlExpressionBase>(enumerable) != 1)
			{
				base.ReportError(element.Location, EdmErrorCode.InvalidCastExpressionIncorrectNumberOfOperands, Strings.CsdlParser_InvalidCastExpressionIncorrectNumberOfOperands);
			}
			return new CsdlCastExpression(csdlTypeReference, Enumerable.ElementAtOrDefault<CsdlExpressionBase>(enumerable, 0), element.Location);
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x00020F4C File Offset: 0x0001F14C
		private CsdlExpressionBase OnIsTypeExpression(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.OptionalType("Type");
			CsdlTypeReference csdlTypeReference = this.ParseTypeReference(text, childValues, element.Location, CsdlDocumentParser.Optionality.Required);
			IEnumerable<CsdlExpressionBase> enumerable = childValues.ValuesOfType<CsdlExpressionBase>();
			if (Enumerable.Count<CsdlExpressionBase>(enumerable) != 1)
			{
				base.ReportError(element.Location, EdmErrorCode.InvalidIsTypeExpressionIncorrectNumberOfOperands, Strings.CsdlParser_InvalidIsTypeExpressionIncorrectNumberOfOperands);
			}
			return new CsdlIsTypeExpression(csdlTypeReference, Enumerable.ElementAtOrDefault<CsdlExpressionBase>(enumerable, 0), element.Location);
		}

		// Token: 0x06000BAC RID: 2988 RVA: 0x00020FB0 File Offset: 0x0001F1B0
		private CsdlTypeDefinition OnTypeDefinitionElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.Required("Name");
			string text2 = base.RequiredType("UnderlyingType");
			return new CsdlTypeDefinition(text, text2, element.Location);
		}

		// Token: 0x06000BAD RID: 2989 RVA: 0x00020FE4 File Offset: 0x0001F1E4
		private CsdlExpressionBase ParseAnnotationExpression(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			CsdlExpressionBase csdlExpressionBase = Enumerable.FirstOrDefault<CsdlExpressionBase>(childValues.ValuesOfType<CsdlExpressionBase>());
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

		// Token: 0x06000BAE RID: 2990 RVA: 0x000211B4 File Offset: 0x0001F3B4
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
				if (string.Equals(typeName, "Edm.Untyped", 4))
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

		// Token: 0x06000BAF RID: 2991 RVA: 0x0002132C File Offset: 0x0001F52C
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
						string text2 = ((Enumerable.Count<string>(array) > 1) ? array[1] : typeString);
						csdlTypeReference = new CsdlExpressionTypeReference(new CsdlEntityReferenceType(this.ParseNamedTypeReference(text2, flag, parentLocation), parentLocation), true, parentLocation);
					}
				}
				else
				{
					string text3 = ((Enumerable.Count<string>(array) > 1) ? array[1] : typeString);
					csdlTypeReference = new CsdlExpressionTypeReference(new CsdlCollectionType(this.ParseNamedTypeReference(text3, flag, parentLocation), parentLocation), flag, parentLocation);
				}
			}
			else if (childValues != null)
			{
				csdlTypeReference = Enumerable.FirstOrDefault<CsdlTypeReference>(childValues.ValuesOfType<CsdlTypeReference>());
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

		// Token: 0x06000BB0 RID: 2992 RVA: 0x00021434 File Offset: 0x0001F634
		private CsdlNamedElement OnNavigationPropertyElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.Required("Name");
			string text2 = base.RequiredType("Type");
			bool? flag = base.OptionalBoolean("Nullable");
			string text3 = base.Optional("Partner");
			bool? flag2 = base.OptionalBoolean("ContainsTarget");
			CsdlOnDelete csdlOnDelete = Enumerable.FirstOrDefault<CsdlOnDelete>(childValues.ValuesOfType<CsdlOnDelete>());
			IEnumerable<CsdlReferentialConstraint> enumerable = Enumerable.ToList<CsdlReferentialConstraint>(childValues.ValuesOfType<CsdlReferentialConstraint>());
			return new CsdlNavigationProperty(text, text2, flag, text3, flag2 ?? false, csdlOnDelete, enumerable, CsdlDocumentParser.Documentation(childValues), element.Location);
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x000214C8 File Offset: 0x0001F6C8
		private static CsdlKey OnEntityKeyElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			return new CsdlKey(new List<CsdlPropertyReference>(childValues.ValuesOfType<CsdlPropertyReference>()), element.Location);
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x000214E0 File Offset: 0x0001F6E0
		private CsdlPropertyReference OnPropertyRefElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			return new CsdlPropertyReference(base.Required("Name"), element.Location);
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x000214F8 File Offset: 0x0001F6F8
		private CsdlEnumType OnEnumTypeElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.Required("Name");
			string text2 = base.OptionalType("UnderlyingType");
			bool? flag = base.OptionalBoolean("IsFlags");
			return new CsdlEnumType(text, text2, flag ?? false, childValues.ValuesOfType<CsdlEnumMember>(), CsdlDocumentParser.Documentation(childValues), element.Location);
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x00021558 File Offset: 0x0001F758
		private CsdlEnumMember OnEnumMemberElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.Required("Name");
			long? num = base.OptionalLong("Value");
			return new CsdlEnumMember(text, num, CsdlDocumentParser.Documentation(childValues), element.Location);
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x00021590 File Offset: 0x0001F790
		private CsdlOnDelete OnDeleteActionElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			EdmOnDeleteAction edmOnDeleteAction = base.RequiredOnDeleteAction("Action");
			return new CsdlOnDelete(edmOnDeleteAction, CsdlDocumentParser.Documentation(childValues), element.Location);
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x000215BC File Offset: 0x0001F7BC
		private CsdlReferentialConstraint OnReferentialConstraintElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.Required("Property");
			string text2 = base.Required("ReferencedProperty");
			return new CsdlReferentialConstraint(text, text2, CsdlDocumentParser.Documentation(childValues), element.Location);
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x000215F4 File Offset: 0x0001F7F4
		internal CsdlAction OnActionElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.Required("Name");
			bool flag = base.OptionalBoolean("IsBound") ?? false;
			string text2 = base.Optional("EntitySetPath");
			IEnumerable<CsdlOperationParameter> enumerable = childValues.ValuesOfType<CsdlOperationParameter>();
			CsdlOperationReturnType csdlOperationReturnType = Enumerable.FirstOrDefault<CsdlOperationReturnType>(childValues.ValuesOfType<CsdlOperationReturnType>());
			CsdlTypeReference csdlTypeReference = ((csdlOperationReturnType == null) ? null : csdlOperationReturnType.ReturnType);
			this.ReportOperationReadErrorsIfExist(text2, flag, text);
			return new CsdlAction(text, enumerable, csdlTypeReference, flag, text2, CsdlDocumentParser.Documentation(childValues), element.Location);
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x00021680 File Offset: 0x0001F880
		internal CsdlFunction OnFunctionElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.Required("Name");
			bool flag = base.OptionalBoolean("IsBound") ?? false;
			string text2 = base.Optional("EntitySetPath");
			bool flag2 = base.OptionalBoolean("IsComposable") ?? false;
			IEnumerable<CsdlOperationParameter> enumerable = childValues.ValuesOfType<CsdlOperationParameter>();
			CsdlOperationReturnType csdlOperationReturnType = Enumerable.FirstOrDefault<CsdlOperationReturnType>(childValues.ValuesOfType<CsdlOperationReturnType>());
			CsdlTypeReference csdlTypeReference = ((csdlOperationReturnType == null) ? null : csdlOperationReturnType.ReturnType);
			this.ReportOperationReadErrorsIfExist(text2, flag, text);
			return new CsdlFunction(text, enumerable, csdlTypeReference, flag, text2, flag2, CsdlDocumentParser.Documentation(childValues), element.Location);
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x0002172E File Offset: 0x0001F92E
		private void ReportOperationReadErrorsIfExist(string entitySetPath, bool isBound, string name)
		{
			if (entitySetPath != null && !isBound)
			{
				base.ReportError(this.currentElement.Location, EdmErrorCode.InvalidEntitySetPath, Strings.CsdlParser_InvalidEntitySetPathWithUnboundAction("Action", name));
			}
		}

		// Token: 0x06000BBA RID: 3002 RVA: 0x00021758 File Offset: 0x0001F958
		private CsdlOperationParameter OnParameterElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.Required("Name");
			string text2 = base.OptionalType("Type");
			string text3 = null;
			bool flag = false;
			CsdlTypeReference csdlTypeReference = this.ParseTypeReference(text2, childValues, element.Location, CsdlDocumentParser.Optionality.Required);
			XmlElementValue xmlElementValue = Enumerable.FirstOrDefault<XmlElementValue>(Enumerable.Where<XmlElementValue>(childValues, (XmlElementValue c) => c is XmlElementValue<CsdlAnnotation> && (c.ValueAs<CsdlAnnotation>().Term == CoreVocabularyModel.OptionalParameterTerm.ShortQualifiedName() || c.ValueAs<CsdlAnnotation>().Term == CoreVocabularyModel.OptionalParameterTerm.FullName())));
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
			return new CsdlOperationParameter(text, csdlTypeReference, CsdlDocumentParser.Documentation(childValues), element.Location, flag, text3);
		}

		// Token: 0x06000BBB RID: 3003 RVA: 0x00021868 File Offset: 0x0001FA68
		private CsdlActionImport OnActionImportElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.Required("Name");
			string text2 = base.RequiredQualifiedName("Action");
			string text3 = base.Optional("EntitySet");
			return new CsdlActionImport(text, text2, text3, CsdlDocumentParser.Documentation(childValues), element.Location);
		}

		// Token: 0x06000BBC RID: 3004 RVA: 0x000218B0 File Offset: 0x0001FAB0
		private CsdlFunctionImport OnFunctionImportElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.Required("Name");
			string text2 = base.RequiredQualifiedName("Function");
			string text3 = base.Optional("EntitySet");
			bool flag = base.OptionalBoolean("IncludeInServiceDocument") ?? false;
			return new CsdlFunctionImport(text, text2, text3, flag, CsdlDocumentParser.Documentation(childValues), element.Location);
		}

		// Token: 0x06000BBD RID: 3005 RVA: 0x00021918 File Offset: 0x0001FB18
		private CsdlOperationParameter OnFunctionImportParameterElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.Required("Name");
			string text2 = base.RequiredType("Type");
			CsdlTypeReference csdlTypeReference = this.ParseTypeReference(text2, null, element.Location, CsdlDocumentParser.Optionality.Required);
			return new CsdlOperationParameter(text, csdlTypeReference, CsdlDocumentParser.Documentation(childValues), element.Location);
		}

		// Token: 0x06000BBE RID: 3006 RVA: 0x00021960 File Offset: 0x0001FB60
		private CsdlTypeReference OnEntityReferenceTypeElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.RequiredType("Type");
			return new CsdlExpressionTypeReference(new CsdlEntityReferenceType(this.ParseTypeReference(text, null, element.Location, CsdlDocumentParser.Optionality.Required), element.Location), true, element.Location);
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x000219A0 File Offset: 0x0001FBA0
		private CsdlTypeReference OnTypeRefElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.RequiredType("Type");
			return this.ParseTypeReference(text, null, element.Location, CsdlDocumentParser.Optionality.Required);
		}

		// Token: 0x06000BC0 RID: 3008 RVA: 0x000219C8 File Offset: 0x0001FBC8
		private CsdlTypeReference OnCollectionTypeElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.OptionalType("ElementType");
			CsdlTypeReference csdlTypeReference = this.ParseTypeReference(text, childValues, element.Location, CsdlDocumentParser.Optionality.Required);
			return new CsdlExpressionTypeReference(new CsdlCollectionType(csdlTypeReference, element.Location), csdlTypeReference.IsNullable, element.Location);
		}

		// Token: 0x06000BC1 RID: 3009 RVA: 0x00021A10 File Offset: 0x0001FC10
		private CsdlOperationReturnType OnReturnTypeElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.RequiredType("Type");
			CsdlTypeReference csdlTypeReference = this.ParseTypeReference(text, childValues, element.Location, CsdlDocumentParser.Optionality.Required);
			return new CsdlOperationReturnType(csdlTypeReference, element.Location);
		}

		// Token: 0x06000BC2 RID: 3010 RVA: 0x00021A48 File Offset: 0x0001FC48
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
			return new CsdlEntityContainer(text, text2, childValues.ValuesOfType<CsdlEntitySet>(), childValues.ValuesOfType<CsdlSingleton>(), childValues.ValuesOfType<CsdlOperationImport>(), CsdlDocumentParser.Documentation(childValues), element.Location);
		}

		// Token: 0x06000BC3 RID: 3011 RVA: 0x00021AC4 File Offset: 0x0001FCC4
		private CsdlEntitySet OnEntitySetElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.Required("Name");
			string text2 = base.RequiredQualifiedName("EntityType");
			bool? flag = base.OptionalBoolean("IncludeInServiceDocument");
			if (flag == null)
			{
				return new CsdlEntitySet(text, text2, childValues.ValuesOfType<CsdlNavigationPropertyBinding>(), CsdlDocumentParser.Documentation(childValues), element.Location);
			}
			return new CsdlEntitySet(text, text2, childValues.ValuesOfType<CsdlNavigationPropertyBinding>(), CsdlDocumentParser.Documentation(childValues), element.Location, flag.Value);
		}

		// Token: 0x06000BC4 RID: 3012 RVA: 0x00021B38 File Offset: 0x0001FD38
		private CsdlSingleton OnSingletonElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.Required("Name");
			string text2 = base.RequiredQualifiedName("Type");
			return new CsdlSingleton(text, text2, childValues.ValuesOfType<CsdlNavigationPropertyBinding>(), CsdlDocumentParser.Documentation(childValues), element.Location);
		}

		// Token: 0x06000BC5 RID: 3013 RVA: 0x00021B78 File Offset: 0x0001FD78
		private CsdlNavigationPropertyBinding OnNavigationPropertyBindingElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.Required("Path");
			string text2 = base.Required("Target");
			return new CsdlNavigationPropertyBinding(text, text2, CsdlDocumentParser.Documentation(childValues), element.Location);
		}

		// Token: 0x06000BC6 RID: 3014 RVA: 0x00021BB0 File Offset: 0x0001FDB0
		private void ParseMaxLength(out bool Unbounded, out int? maxLength)
		{
			string text = base.Optional("MaxLength");
			if (text == null)
			{
				Unbounded = false;
				maxLength = default(int?);
				return;
			}
			if (text.EqualsOrdinalIgnoreCase("max"))
			{
				Unbounded = true;
				maxLength = default(int?);
				return;
			}
			Unbounded = false;
			maxLength = base.OptionalMaxLength("MaxLength");
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x00021C03 File Offset: 0x0001FE03
		private void ParseBinaryFacets(out bool Unbounded, out int? maxLength)
		{
			this.ParseMaxLength(out Unbounded, out maxLength);
		}

		// Token: 0x06000BC8 RID: 3016 RVA: 0x00021C0D File Offset: 0x0001FE0D
		private void ParseDecimalFacets(out int? precision, out int? scale)
		{
			precision = base.OptionalInteger("Precision");
			scale = base.OptionalScale("Scale");
		}

		// Token: 0x06000BC9 RID: 3017 RVA: 0x00021C34 File Offset: 0x0001FE34
		private void ParseStringFacets(out bool Unbounded, out int? maxLength, out bool? unicode)
		{
			this.ParseMaxLength(out Unbounded, out maxLength);
			unicode = new bool?(base.OptionalBoolean("Unicode") ?? true);
		}

		// Token: 0x06000BCA RID: 3018 RVA: 0x00021C74 File Offset: 0x0001FE74
		private void ParseTemporalFacets(out int? precision)
		{
			precision = new int?(base.OptionalInteger("Precision") ?? 0);
		}

		// Token: 0x06000BCB RID: 3019 RVA: 0x00021CAB File Offset: 0x0001FEAB
		private void ParseSpatialFacets(out int? srid, int defaultSrid)
		{
			srid = base.OptionalSrid("SRID", defaultSrid);
		}

		// Token: 0x06000BCC RID: 3020 RVA: 0x00021CC0 File Offset: 0x0001FEC0
		private void ParseTypeDefinitionFacets(out bool isUnbounded, out int? maxLength, out bool? unicode, out int? precision, out int? scale, out int? srid)
		{
			this.ParseMaxLength(out isUnbounded, out maxLength);
			unicode = base.OptionalBoolean("Unicode");
			precision = base.OptionalInteger("Precision");
			scale = base.OptionalScale("Scale");
			srid = base.OptionalSrid("SRID", int.MinValue);
		}

		// Token: 0x04000698 RID: 1688
		private Version artifactVersion;

		// Token: 0x04000699 RID: 1689
		private int entityContainerCount;

		// Token: 0x020002DC RID: 732
		private enum Optionality
		{
			// Token: 0x04000853 RID: 2131
			Optional,
			// Token: 0x04000854 RID: 2132
			Required
		}
	}
}
