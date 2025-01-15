using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Csdl.Parsing.Common;
using Microsoft.OData.Edm.Library;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Values;

namespace Microsoft.OData.Edm.Csdl.Parsing
{
	// Token: 0x020001A0 RID: 416
	internal class CsdlDocumentParser : EdmXmlDocumentParser<CsdlSchema>
	{
		// Token: 0x06000817 RID: 2071 RVA: 0x00013CD6 File Offset: 0x00011ED6
		internal CsdlDocumentParser(string documentPath, XmlReader reader)
			: base(documentPath, reader)
		{
			this.entityContainerCount = 0;
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06000818 RID: 2072 RVA: 0x00013D3A File Offset: 0x00011F3A
		internal override IEnumerable<KeyValuePair<Version, string>> SupportedVersions
		{
			get
			{
				return Enumerable.SelectMany<KeyValuePair<Version, string[]>, KeyValuePair<Version, string>>(CsdlConstants.SupportedVersions, (KeyValuePair<Version, string[]> kvp) => Enumerable.Select<string, KeyValuePair<Version, string>>(kvp.Value, (string ns) => new KeyValuePair<Version, string>(kvp.Key, ns)));
			}
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x00013D63 File Offset: 0x00011F63
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

		// Token: 0x0600081A RID: 2074 RVA: 0x00013D9C File Offset: 0x00011F9C
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

		// Token: 0x0600081B RID: 2075 RVA: 0x00013E78 File Offset: 0x00012078
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
			XmlElementParser<CsdlExpressionBase> xmlElementParser20 = base.CsdlElement<CsdlExpressionBase>("FunctionReference", new Func<XmlElementInfo, XmlElementValueCollection, CsdlExpressionBase>(this.OnFunctionReferenceExpression), new XmlElementParser[0]);
			XmlElementParser<CsdlExpressionBase> xmlElementParser21 = base.CsdlElement<CsdlExpressionBase>("ParameterReference", new Func<XmlElementInfo, XmlElementValueCollection, CsdlExpressionBase>(this.OnParameterReferenceExpression), new XmlElementParser[0]);
			XmlElementParser<CsdlExpressionBase> xmlElementParser22 = base.CsdlElement<CsdlExpressionBase>("EntitySetReference", new Func<XmlElementInfo, XmlElementValueCollection, CsdlExpressionBase>(this.OnEntitySetReferenceExpression), new XmlElementParser[0]);
			XmlElementParser<CsdlExpressionBase> xmlElementParser23 = base.CsdlElement<CsdlExpressionBase>("EnumMember", new Func<XmlElementInfo, XmlElementValueCollection, CsdlExpressionBase>(this.OnEnumMemberExpression), new XmlElementParser[0]);
			XmlElementParser<CsdlExpressionBase> xmlElementParser24 = base.CsdlElement<CsdlExpressionBase>("PropertyReference", new Func<XmlElementInfo, XmlElementValueCollection, CsdlExpressionBase>(this.OnPropertyReferenceExpression), new XmlElementParser[0]);
			XmlElementParser<CsdlExpressionBase> xmlElementParser25 = base.CsdlElement<CsdlExpressionBase>("If", new Func<XmlElementInfo, XmlElementValueCollection, CsdlExpressionBase>(this.OnIfExpression), new XmlElementParser[0]);
			XmlElementParser<CsdlExpressionBase> xmlElementParser26 = base.CsdlElement<CsdlExpressionBase>("Cast", new Func<XmlElementInfo, XmlElementValueCollection, CsdlExpressionBase>(this.OnCastExpression), new XmlElementParser[0]);
			XmlElementParser<CsdlExpressionBase> xmlElementParser27 = base.CsdlElement<CsdlExpressionBase>("IsType", new Func<XmlElementInfo, XmlElementValueCollection, CsdlExpressionBase>(this.OnIsTypeExpression), new XmlElementParser[0]);
			XmlElementParser<CsdlPropertyValue> xmlElementParser28 = base.CsdlElement<CsdlPropertyValue>("PropertyValue", new Func<XmlElementInfo, XmlElementValueCollection, CsdlPropertyValue>(this.OnPropertyValueElement), new XmlElementParser[0]);
			XmlElementParser<CsdlRecordExpression> xmlElementParser29 = base.CsdlElement<CsdlRecordExpression>("Record", new Func<XmlElementInfo, XmlElementValueCollection, CsdlRecordExpression>(this.OnRecordElement), new XmlElementParser[] { xmlElementParser28 });
			XmlElementParser<CsdlLabeledExpression> xmlElementParser30 = base.CsdlElement<CsdlLabeledExpression>("LabeledElement", new Func<XmlElementInfo, XmlElementValueCollection, CsdlLabeledExpression>(this.OnLabeledElement), new XmlElementParser[0]);
			XmlElementParser<CsdlCollectionExpression> xmlElementParser31 = base.CsdlElement<CsdlCollectionExpression>("Collection", new Func<XmlElementInfo, XmlElementValueCollection, CsdlCollectionExpression>(this.OnCollectionElement), new XmlElementParser[0]);
			XmlElementParser<CsdlExpressionBase> xmlElementParser32 = base.CsdlElement<CsdlExpressionBase>("Apply", new Func<XmlElementInfo, XmlElementValueCollection, CsdlExpressionBase>(this.OnApplyElement), new XmlElementParser[0]);
			XmlElementParser<CsdlExpressionBase> xmlElementParser33 = base.CsdlElement<CsdlExpressionBase>("LabeledElementReference", new Func<XmlElementInfo, XmlElementValueCollection, CsdlExpressionBase>(this.OnLabeledElementReferenceExpression), new XmlElementParser[0]);
			XmlElementParser[] array2 = new XmlElementParser[]
			{
				xmlElementParser5, xmlElementParser6, xmlElementParser7, xmlElementParser8, xmlElementParser9, xmlElementParser10, xmlElementParser11, xmlElementParser13, xmlElementParser15, xmlElementParser12,
				xmlElementParser14, xmlElementParser16, xmlElementParser17, xmlElementParser18, xmlElementParser19, xmlElementParser25, xmlElementParser27, xmlElementParser26, xmlElementParser29, xmlElementParser31,
				xmlElementParser33, xmlElementParser24, xmlElementParser28, xmlElementParser30, xmlElementParser20, xmlElementParser22, xmlElementParser23, xmlElementParser21, xmlElementParser32
			};
			CsdlDocumentParser.AddChildParsers(xmlElementParser24, array2);
			CsdlDocumentParser.AddChildParsers(xmlElementParser25, array2);
			CsdlDocumentParser.AddChildParsers(xmlElementParser26, array2);
			CsdlDocumentParser.AddChildParsers(xmlElementParser27, array2);
			CsdlDocumentParser.AddChildParsers(xmlElementParser28, array2);
			CsdlDocumentParser.AddChildParsers(xmlElementParser31, array2);
			CsdlDocumentParser.AddChildParsers(xmlElementParser30, array2);
			CsdlDocumentParser.AddChildParsers(xmlElementParser32, array2);
			XmlElementParser<CsdlAnnotation> xmlElementParser34 = base.CsdlElement<CsdlAnnotation>("Annotation", new Func<XmlElementInfo, XmlElementValueCollection, CsdlAnnotation>(this.OnAnnotationElement), new XmlElementParser[0]);
			CsdlDocumentParser.AddChildParsers(xmlElementParser34, array2);
			xmlElementParser4.AddChildParser(xmlElementParser34);
			xmlElementParser3.AddChildParser(xmlElementParser3);
			return base.CsdlElement<CsdlSchema>("Schema", new Func<XmlElementInfo, XmlElementValueCollection, CsdlSchema>(this.OnSchemaElement), new XmlElementParser[]
			{
				xmlElementParser,
				base.CsdlElement<CsdlComplexType>("ComplexType", new Func<XmlElementInfo, XmlElementValueCollection, CsdlComplexType>(this.OnComplexTypeElement), new XmlElementParser[] { xmlElementParser, xmlElementParser4, xmlElementParser34 }),
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
						xmlElementParser34
					}),
					xmlElementParser34
				}),
				base.CsdlElement<CsdlEnumType>("EnumType", new Func<XmlElementInfo, XmlElementValueCollection, CsdlEnumType>(this.OnEnumTypeElement), new XmlElementParser[]
				{
					xmlElementParser,
					base.CsdlElement<CsdlEnumMember>("Member", new Func<XmlElementInfo, XmlElementValueCollection, CsdlEnumMember>(this.OnEnumMemberElement), new XmlElementParser[] { xmlElementParser, xmlElementParser34 }),
					xmlElementParser34
				}),
				base.CsdlElement<CsdlTypeDefinition>("TypeDefinition", new Func<XmlElementInfo, XmlElementValueCollection, CsdlTypeDefinition>(this.OnTypeDefinitionElement), new XmlElementParser[] { xmlElementParser34 }),
				base.CsdlElement<CsdlAction>("Action", new Func<XmlElementInfo, XmlElementValueCollection, CsdlAction>(this.OnActionElement), new XmlElementParser[]
				{
					xmlElementParser,
					base.CsdlElement<CsdlOperationParameter>("Parameter", new Func<XmlElementInfo, XmlElementValueCollection, CsdlOperationParameter>(this.OnParameterElement), new XmlElementParser[]
					{
						xmlElementParser,
						base.CsdlElement<CsdlTypeReference>("TypeRef", new Func<XmlElementInfo, XmlElementValueCollection, CsdlTypeReference>(this.OnTypeRefElement), new XmlElementParser[] { xmlElementParser }),
						xmlElementParser3,
						xmlElementParser2,
						xmlElementParser34
					}),
					base.CsdlElement<CsdlOperationReturnType>("ReturnType", new Func<XmlElementInfo, XmlElementValueCollection, CsdlOperationReturnType>(this.OnReturnTypeElement), new XmlElementParser[]
					{
						xmlElementParser,
						base.CsdlElement<CsdlTypeReference>("TypeRef", new Func<XmlElementInfo, XmlElementValueCollection, CsdlTypeReference>(this.OnTypeRefElement), new XmlElementParser[] { xmlElementParser }),
						xmlElementParser3,
						xmlElementParser2
					}),
					xmlElementParser34
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
						xmlElementParser34
					}),
					base.CsdlElement<CsdlOperationReturnType>("ReturnType", new Func<XmlElementInfo, XmlElementValueCollection, CsdlOperationReturnType>(this.OnReturnTypeElement), new XmlElementParser[]
					{
						xmlElementParser,
						base.CsdlElement<CsdlTypeReference>("TypeRef", new Func<XmlElementInfo, XmlElementValueCollection, CsdlTypeReference>(this.OnTypeRefElement), new XmlElementParser[] { xmlElementParser }),
						xmlElementParser3,
						xmlElementParser2
					}),
					xmlElementParser34
				}),
				base.CsdlElement<CsdlTerm>("Term", new Func<XmlElementInfo, XmlElementValueCollection, CsdlTerm>(this.OnTermElement), new XmlElementParser[]
				{
					base.CsdlElement<CsdlTypeReference>("TypeRef", new Func<XmlElementInfo, XmlElementValueCollection, CsdlTypeReference>(this.OnTypeRefElement), new XmlElementParser[] { xmlElementParser }),
					xmlElementParser3,
					xmlElementParser2,
					xmlElementParser34
				}),
				base.CsdlElement<CsdlAnnotations>("Annotations", new Func<XmlElementInfo, XmlElementValueCollection, CsdlAnnotations>(this.OnAnnotationsElement), new XmlElementParser[] { xmlElementParser34 }),
				base.CsdlElement<CsdlEntityContainer>("EntityContainer", new Func<XmlElementInfo, XmlElementValueCollection, CsdlEntityContainer>(this.OnEntityContainerElement), new XmlElementParser[]
				{
					xmlElementParser,
					base.CsdlElement<CsdlEntitySet>("EntitySet", new Func<XmlElementInfo, XmlElementValueCollection, CsdlEntitySet>(this.OnEntitySetElement), new XmlElementParser[]
					{
						xmlElementParser,
						base.CsdlElement<CsdlNavigationPropertyBinding>("NavigationPropertyBinding", new Func<XmlElementInfo, XmlElementValueCollection, CsdlNavigationPropertyBinding>(this.OnNavigationPropertyBindingElement), new XmlElementParser[0]),
						xmlElementParser34
					}),
					base.CsdlElement<CsdlSingleton>("Singleton", new Func<XmlElementInfo, XmlElementValueCollection, CsdlSingleton>(this.OnSingletonElement), new XmlElementParser[]
					{
						xmlElementParser,
						base.CsdlElement<CsdlNavigationPropertyBinding>("NavigationPropertyBinding", new Func<XmlElementInfo, XmlElementValueCollection, CsdlNavigationPropertyBinding>(this.OnNavigationPropertyBindingElement), new XmlElementParser[0]),
						xmlElementParser34
					}),
					base.CsdlElement<CsdlActionImport>("ActionImport", new Func<XmlElementInfo, XmlElementValueCollection, CsdlActionImport>(this.OnActionImportElement), new XmlElementParser[] { xmlElementParser, xmlElementParser34 }),
					base.CsdlElement<CsdlOperationImport>("FunctionImport", new Func<XmlElementInfo, XmlElementValueCollection, CsdlOperationImport>(this.OnFunctionImportElement), new XmlElementParser[]
					{
						xmlElementParser,
						base.CsdlElement<CsdlOperationParameter>("Parameter", new Func<XmlElementInfo, XmlElementValueCollection, CsdlOperationParameter>(this.OnFunctionImportParameterElement), new XmlElementParser[] { xmlElementParser, xmlElementParser34 }),
						xmlElementParser34
					}),
					xmlElementParser34
				})
			});
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x00014A45 File Offset: 0x00012C45
		private static CsdlDocumentation Documentation(XmlElementValueCollection childValues)
		{
			return Enumerable.FirstOrDefault<CsdlDocumentation>(childValues.ValuesOfType<CsdlDocumentation>());
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x00014A54 File Offset: 0x00012C54
		private CsdlSchema OnSchemaElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.Optional("Namespace") ?? string.Empty;
			string text2 = base.OptionalAlias("Alias");
			return new CsdlSchema(text, text2, this.artifactVersion, childValues.ValuesOfType<CsdlStructuredType>(), childValues.ValuesOfType<CsdlEnumType>(), childValues.ValuesOfType<CsdlOperation>(), childValues.ValuesOfType<CsdlTerm>(), childValues.ValuesOfType<CsdlEntityContainer>(), childValues.ValuesOfType<CsdlAnnotations>(), childValues.ValuesOfType<CsdlTypeDefinition>(), CsdlDocumentParser.Documentation(childValues), element.Location);
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x00014AC7 File Offset: 0x00012CC7
		private CsdlDocumentation OnDocumentationElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			return new CsdlDocumentation(childValues["Summary"].TextValue, childValues["LongDescription"].TextValue, element.Location);
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x00014AF4 File Offset: 0x00012CF4
		private CsdlComplexType OnComplexTypeElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.Required("Name");
			string text2 = base.OptionalQualifiedName("BaseType");
			bool flag = base.OptionalBoolean("OpenType") ?? false;
			bool flag2 = base.OptionalBoolean("Abstract") ?? false;
			return new CsdlComplexType(text, text2, flag2, flag, childValues.ValuesOfType<CsdlProperty>(), CsdlDocumentParser.Documentation(childValues), element.Location);
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x00014B78 File Offset: 0x00012D78
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

		// Token: 0x06000821 RID: 2081 RVA: 0x00014C34 File Offset: 0x00012E34
		private CsdlProperty OnPropertyElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.OptionalType("Type");
			CsdlTypeReference csdlTypeReference = this.ParseTypeReference(text, childValues, element.Location, CsdlDocumentParser.Optionality.Required);
			string text2 = base.Required("Name");
			string text3 = base.Optional("DefaultValue");
			bool flag = (base.OptionalConcurrencyMode("ConcurrencyMode") ?? EdmConcurrencyMode.None) == EdmConcurrencyMode.Fixed;
			return new CsdlProperty(text2, csdlTypeReference, flag, text3, CsdlDocumentParser.Documentation(childValues), element.Location);
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x00014CB4 File Offset: 0x00012EB4
		private CsdlTerm OnTermElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.OptionalType("Type");
			CsdlTypeReference csdlTypeReference = this.ParseTypeReference(text, childValues, element.Location, CsdlDocumentParser.Optionality.Required);
			string text2 = base.Required("Name");
			string text3 = base.Optional("AppliesTo");
			string text4 = base.Optional("DefaultValue");
			return new CsdlTerm(text2, csdlTypeReference, text3, text4, CsdlDocumentParser.Documentation(childValues), element.Location);
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x00014D18 File Offset: 0x00012F18
		private CsdlAnnotations OnAnnotationsElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.Required("Target");
			string text2 = base.Optional("Qualifier");
			IEnumerable<CsdlAnnotation> enumerable = childValues.ValuesOfType<CsdlAnnotation>();
			return new CsdlAnnotations(enumerable, text, text2);
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x00014D4C File Offset: 0x00012F4C
		private CsdlAnnotation OnAnnotationElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.RequiredQualifiedName("Term");
			string text2 = base.Optional("Qualifier");
			CsdlExpressionBase csdlExpressionBase = this.ParseAnnotationExpression(element, childValues);
			return new CsdlAnnotation(text, text2, csdlExpressionBase, element.Location);
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x00014D88 File Offset: 0x00012F88
		private CsdlPropertyValue OnPropertyValueElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.Required("Property");
			CsdlExpressionBase csdlExpressionBase = this.ParseAnnotationExpression(element, childValues);
			return new CsdlPropertyValue(text, csdlExpressionBase, element.Location);
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x00014DB8 File Offset: 0x00012FB8
		private CsdlRecordExpression OnRecordElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.OptionalQualifiedName("Type");
			IEnumerable<CsdlPropertyValue> enumerable = childValues.ValuesOfType<CsdlPropertyValue>();
			return new CsdlRecordExpression((text != null) ? new CsdlNamedTypeReference(text, false, element.Location) : null, enumerable, element.Location);
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x00014DF8 File Offset: 0x00012FF8
		private CsdlCollectionExpression OnCollectionElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.OptionalType("Type");
			CsdlTypeReference csdlTypeReference = this.ParseTypeReference(text, childValues, element.Location, CsdlDocumentParser.Optionality.Optional);
			IEnumerable<CsdlExpressionBase> enumerable = childValues.ValuesOfType<CsdlExpressionBase>();
			return new CsdlCollectionExpression(csdlTypeReference, enumerable, element.Location);
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x00014E38 File Offset: 0x00013038
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

		// Token: 0x06000829 RID: 2089 RVA: 0x00014E8C File Offset: 0x0001308C
		private CsdlApplyExpression OnApplyElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.Optional("Function");
			IEnumerable<CsdlExpressionBase> enumerable = childValues.ValuesOfType<CsdlExpressionBase>();
			return new CsdlApplyExpression(text, enumerable, element.Location);
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x00014EBC File Offset: 0x000130BC
		private static void AddChildParsers(XmlElementParser parent, IEnumerable<XmlElementParser> children)
		{
			foreach (XmlElementParser xmlElementParser in children)
			{
				parent.AddChildParser(xmlElementParser);
			}
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x00014F04 File Offset: 0x00013104
		private static CsdlConstantExpression ConstantExpression(EdmValueKind kind, XmlElementValueCollection childValues, CsdlLocation location)
		{
			XmlTextValue firstText = childValues.FirstText;
			return new CsdlConstantExpression(kind, (firstText != null) ? firstText.TextValue : string.Empty, location);
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x00014F2F File Offset: 0x0001312F
		private static CsdlConstantExpression OnIntConstantExpression(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			return CsdlDocumentParser.ConstantExpression(EdmValueKind.Integer, childValues, element.Location);
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x00014F3F File Offset: 0x0001313F
		private static CsdlConstantExpression OnStringConstantExpression(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			return CsdlDocumentParser.ConstantExpression(EdmValueKind.String, childValues, element.Location);
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x00014F4F File Offset: 0x0001314F
		private static CsdlConstantExpression OnBinaryConstantExpression(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			return CsdlDocumentParser.ConstantExpression(EdmValueKind.Binary, childValues, element.Location);
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x00014F5E File Offset: 0x0001315E
		private static CsdlConstantExpression OnFloatConstantExpression(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			return CsdlDocumentParser.ConstantExpression(EdmValueKind.Floating, childValues, element.Location);
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x00014F6D File Offset: 0x0001316D
		private static CsdlConstantExpression OnGuidConstantExpression(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			return CsdlDocumentParser.ConstantExpression(EdmValueKind.Guid, childValues, element.Location);
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x00014F7C File Offset: 0x0001317C
		private static CsdlConstantExpression OnDecimalConstantExpression(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			return CsdlDocumentParser.ConstantExpression(EdmValueKind.Decimal, childValues, element.Location);
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x00014F8B File Offset: 0x0001318B
		private static CsdlConstantExpression OnBoolConstantExpression(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			return CsdlDocumentParser.ConstantExpression(EdmValueKind.Boolean, childValues, element.Location);
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x00014F9A File Offset: 0x0001319A
		private static CsdlConstantExpression OnDurationConstantExpression(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			return CsdlDocumentParser.ConstantExpression(EdmValueKind.Duration, childValues, element.Location);
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x00014FAA File Offset: 0x000131AA
		private static CsdlConstantExpression OnDateConstantExpression(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			return CsdlDocumentParser.ConstantExpression(EdmValueKind.Date, childValues, element.Location);
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x00014FBA File Offset: 0x000131BA
		private static CsdlConstantExpression OnDateTimeOffsetConstantExpression(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			return CsdlDocumentParser.ConstantExpression(EdmValueKind.DateTimeOffset, childValues, element.Location);
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x00014FC9 File Offset: 0x000131C9
		private static CsdlConstantExpression OnTimeOfDayConstantExpression(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			return CsdlDocumentParser.ConstantExpression(EdmValueKind.TimeOfDay, childValues, element.Location);
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x00014FD9 File Offset: 0x000131D9
		private static CsdlConstantExpression OnNullConstantExpression(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			return CsdlDocumentParser.ConstantExpression(EdmValueKind.Null, childValues, element.Location);
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x00014FEC File Offset: 0x000131EC
		private static CsdlPathExpression OnPathExpression(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			XmlTextValue firstText = childValues.FirstText;
			return new CsdlPathExpression((firstText != null) ? firstText.TextValue : string.Empty, element.Location);
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x0001501C File Offset: 0x0001321C
		private static CsdlPropertyPathExpression OnPropertyPathExpression(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			XmlTextValue firstText = childValues.FirstText;
			return new CsdlPropertyPathExpression((firstText != null) ? firstText.TextValue : string.Empty, element.Location);
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x0001504C File Offset: 0x0001324C
		private static CsdlNavigationPropertyPathExpression OnNavigationPropertyPathExpression(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			XmlTextValue firstText = childValues.FirstText;
			return new CsdlNavigationPropertyPathExpression((firstText != null) ? firstText.TextValue : string.Empty, element.Location);
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x0001507C File Offset: 0x0001327C
		private CsdlLabeledExpressionReferenceExpression OnLabeledElementReferenceExpression(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.Required("Name");
			return new CsdlLabeledExpressionReferenceExpression(text, element.Location);
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x000150A4 File Offset: 0x000132A4
		private CsdlEntitySetReferenceExpression OnEntitySetReferenceExpression(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.RequiredEntitySetPath("Name");
			return new CsdlEntitySetReferenceExpression(text, element.Location);
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x000150CC File Offset: 0x000132CC
		private CsdlEnumMemberExpression OnEnumMemberExpression(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.RequiredEnumMemberPath(childValues.FirstText);
			return new CsdlEnumMemberExpression(text, element.Location);
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x000150F4 File Offset: 0x000132F4
		private CsdlPropertyReferenceExpression OnPropertyReferenceExpression(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.Required("Name");
			return new CsdlPropertyReferenceExpression(text, Enumerable.FirstOrDefault<CsdlExpressionBase>(childValues.ValuesOfType<CsdlExpressionBase>()), element.Location);
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x00015124 File Offset: 0x00013324
		private CsdlOperationReferenceExpression OnFunctionReferenceExpression(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.RequiredQualifiedName("Name");
			return new CsdlOperationReferenceExpression(text, element.Location);
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x0001514C File Offset: 0x0001334C
		private CsdlParameterReferenceExpression OnParameterReferenceExpression(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.Required("Name");
			return new CsdlParameterReferenceExpression(text, element.Location);
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x00015174 File Offset: 0x00013374
		private CsdlExpressionBase OnIfExpression(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			IEnumerable<CsdlExpressionBase> enumerable = childValues.ValuesOfType<CsdlExpressionBase>();
			if (Enumerable.Count<CsdlExpressionBase>(enumerable) != 3)
			{
				base.ReportError(element.Location, EdmErrorCode.InvalidIfExpressionIncorrectNumberOfOperands, Strings.CsdlParser_InvalidIfExpressionIncorrectNumberOfOperands);
			}
			return new CsdlIfExpression(Enumerable.ElementAtOrDefault<CsdlExpressionBase>(enumerable, 0), Enumerable.ElementAtOrDefault<CsdlExpressionBase>(enumerable, 1), Enumerable.ElementAtOrDefault<CsdlExpressionBase>(enumerable, 2), element.Location);
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x000151C8 File Offset: 0x000133C8
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

		// Token: 0x06000843 RID: 2115 RVA: 0x0001522C File Offset: 0x0001342C
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

		// Token: 0x06000844 RID: 2116 RVA: 0x00015290 File Offset: 0x00013490
		private CsdlTypeDefinition OnTypeDefinitionElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.Required("Name");
			string text2 = base.RequiredType("UnderlyingType");
			return new CsdlTypeDefinition(text, text2, element.Location);
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x000152C4 File Offset: 0x000134C4
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
			string text5 = base.Optional("String");
			EdmValueKind edmValueKind;
			if (text5 != null)
			{
				edmValueKind = EdmValueKind.String;
			}
			else
			{
				text5 = base.Optional("Bool");
				if (text5 != null)
				{
					edmValueKind = EdmValueKind.Boolean;
				}
				else
				{
					text5 = base.Optional("Int");
					if (text5 != null)
					{
						edmValueKind = EdmValueKind.Integer;
					}
					else
					{
						text5 = base.Optional("Float");
						if (text5 != null)
						{
							edmValueKind = EdmValueKind.Floating;
						}
						else
						{
							text5 = base.Optional("DateTimeOffset");
							if (text5 != null)
							{
								edmValueKind = EdmValueKind.DateTimeOffset;
							}
							else
							{
								text5 = base.Optional("Duration");
								if (text5 != null)
								{
									edmValueKind = EdmValueKind.Duration;
								}
								else
								{
									text5 = base.Optional("Decimal");
									if (text5 != null)
									{
										edmValueKind = EdmValueKind.Decimal;
									}
									else
									{
										text5 = base.Optional("Binary");
										if (text5 != null)
										{
											edmValueKind = EdmValueKind.Binary;
										}
										else
										{
											text5 = base.Optional("Guid");
											if (text5 != null)
											{
												edmValueKind = EdmValueKind.Guid;
											}
											else
											{
												text5 = base.Optional("Date");
												if (text5 != null)
												{
													edmValueKind = EdmValueKind.Date;
												}
												else
												{
													text5 = base.Optional("TimeOfDay");
													if (text5 == null)
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
			return new CsdlConstantExpression(edmValueKind, text5, element.Location);
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x00015474 File Offset: 0x00013674
		private CsdlNamedTypeReference ParseNamedTypeReference(string typeName, bool isNullable, CsdlLocation parentLocation)
		{
			EdmCoreModel instance = EdmCoreModel.Instance;
			EdmPrimitiveTypeKind primitiveTypeKind = instance.GetPrimitiveTypeKind(typeName);
			switch (primitiveTypeKind)
			{
			case EdmPrimitiveTypeKind.Binary:
			{
				bool flag;
				int? num;
				this.ParseBinaryFacets(out flag, out num);
				return new CsdlBinaryTypeReference(flag, num, typeName, isNullable, parentLocation);
			}
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
			{
				int? num2;
				this.ParseTemporalFacets(out num2);
				return new CsdlTemporalTypeReference(primitiveTypeKind, num2, typeName, isNullable, parentLocation);
			}
			case EdmPrimitiveTypeKind.Decimal:
			{
				int? num3;
				int? num4;
				this.ParseDecimalFacets(out num3, out num4);
				return new CsdlDecimalTypeReference(num3, num4, typeName, isNullable, parentLocation);
			}
			case EdmPrimitiveTypeKind.String:
			{
				bool flag2;
				int? num5;
				bool? flag3;
				string text;
				this.ParseStringFacets(out flag2, out num5, out flag3, out text);
				return new CsdlStringTypeReference(flag2, num5, flag3, text, typeName, isNullable, parentLocation);
			}
			case EdmPrimitiveTypeKind.Geography:
			case EdmPrimitiveTypeKind.GeographyPoint:
			case EdmPrimitiveTypeKind.GeographyLineString:
			case EdmPrimitiveTypeKind.GeographyPolygon:
			case EdmPrimitiveTypeKind.GeographyCollection:
			case EdmPrimitiveTypeKind.GeographyMultiPolygon:
			case EdmPrimitiveTypeKind.GeographyMultiLineString:
			case EdmPrimitiveTypeKind.GeographyMultiPoint:
			{
				int? num6;
				this.ParseSpatialFacets(out num6, 4326);
				return new CsdlSpatialTypeReference(primitiveTypeKind, num6, typeName, isNullable, parentLocation);
			}
			case EdmPrimitiveTypeKind.Geometry:
			case EdmPrimitiveTypeKind.GeometryPoint:
			case EdmPrimitiveTypeKind.GeometryLineString:
			case EdmPrimitiveTypeKind.GeometryPolygon:
			case EdmPrimitiveTypeKind.GeometryCollection:
			case EdmPrimitiveTypeKind.GeometryMultiPolygon:
			case EdmPrimitiveTypeKind.GeometryMultiLineString:
			case EdmPrimitiveTypeKind.GeometryMultiPoint:
			{
				int? num7;
				this.ParseSpatialFacets(out num7, 0);
				return new CsdlSpatialTypeReference(primitiveTypeKind, num7, typeName, isNullable, parentLocation);
			}
			default:
				return new CsdlNamedTypeReference(typeName, isNullable, parentLocation);
			}
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x000155C4 File Offset: 0x000137C4
		private CsdlTypeReference ParseTypeReference(string typeString, XmlElementValueCollection childValues, CsdlLocation parentLocation, CsdlDocumentParser.Optionality typeInfoOptionality)
		{
			bool flag = base.OptionalBoolean("Nullable") ?? true;
			CsdlTypeReference csdlTypeReference = null;
			if (typeString != null)
			{
				string[] array = typeString.Split(new char[] { '(', ')' });
				string text = array[0];
				string text2;
				if ((text2 = text) != null)
				{
					if (text2 == "Collection")
					{
						string text3 = ((Enumerable.Count<string>(array) > 1) ? array[1] : typeString);
						csdlTypeReference = new CsdlExpressionTypeReference(new CsdlCollectionType(this.ParseNamedTypeReference(text3, flag, parentLocation), parentLocation), flag, parentLocation);
						goto IL_00DF;
					}
					if (text2 == "Ref")
					{
						string text4 = ((Enumerable.Count<string>(array) > 1) ? array[1] : typeString);
						csdlTypeReference = new CsdlExpressionTypeReference(new CsdlEntityReferenceType(this.ParseNamedTypeReference(text4, flag, parentLocation), parentLocation), true, parentLocation);
						goto IL_00DF;
					}
				}
				csdlTypeReference = this.ParseNamedTypeReference(text, flag, parentLocation);
			}
			else if (childValues != null)
			{
				csdlTypeReference = Enumerable.FirstOrDefault<CsdlTypeReference>(childValues.ValuesOfType<CsdlTypeReference>());
			}
			IL_00DF:
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

		// Token: 0x06000848 RID: 2120 RVA: 0x000156D8 File Offset: 0x000138D8
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

		// Token: 0x06000849 RID: 2121 RVA: 0x0001576C File Offset: 0x0001396C
		private static CsdlKey OnEntityKeyElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			return new CsdlKey(new List<CsdlPropertyReference>(childValues.ValuesOfType<CsdlPropertyReference>()), element.Location);
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x00015784 File Offset: 0x00013984
		private CsdlPropertyReference OnPropertyRefElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			return new CsdlPropertyReference(base.Required("Name"), element.Location);
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x0001579C File Offset: 0x0001399C
		private CsdlEnumType OnEnumTypeElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.Required("Name");
			string text2 = base.OptionalType("UnderlyingType");
			bool? flag = base.OptionalBoolean("IsFlags");
			return new CsdlEnumType(text, text2, flag ?? false, childValues.ValuesOfType<CsdlEnumMember>(), CsdlDocumentParser.Documentation(childValues), element.Location);
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x000157FC File Offset: 0x000139FC
		private CsdlEnumMember OnEnumMemberElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.Required("Name");
			long? num = base.OptionalLong("Value");
			return new CsdlEnumMember(text, num, CsdlDocumentParser.Documentation(childValues), element.Location);
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x00015834 File Offset: 0x00013A34
		private CsdlOnDelete OnDeleteActionElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			EdmOnDeleteAction edmOnDeleteAction = base.RequiredOnDeleteAction("Action");
			return new CsdlOnDelete(edmOnDeleteAction, CsdlDocumentParser.Documentation(childValues), element.Location);
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x00015860 File Offset: 0x00013A60
		private CsdlReferentialConstraint OnReferentialConstraintElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.Required("Property");
			string text2 = base.Required("ReferencedProperty");
			return new CsdlReferentialConstraint(text, text2, CsdlDocumentParser.Documentation(childValues), element.Location);
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x00015898 File Offset: 0x00013A98
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

		// Token: 0x06000850 RID: 2128 RVA: 0x00015924 File Offset: 0x00013B24
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

		// Token: 0x06000851 RID: 2129 RVA: 0x000159D2 File Offset: 0x00013BD2
		private void ReportOperationReadErrorsIfExist(string entitySetPath, bool isBound, string name)
		{
			if (entitySetPath != null && !isBound)
			{
				base.ReportError(this.currentElement.Location, EdmErrorCode.InvalidEntitySetPath, Strings.CsdlParser_InvalidEntitySetPathWithUnboundAction("Action", name));
			}
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x000159FC File Offset: 0x00013BFC
		private CsdlOperationParameter OnParameterElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.Required("Name");
			string text2 = base.OptionalType("Type");
			CsdlTypeReference csdlTypeReference = this.ParseTypeReference(text2, childValues, element.Location, CsdlDocumentParser.Optionality.Required);
			return new CsdlOperationParameter(text, csdlTypeReference, CsdlDocumentParser.Documentation(childValues), element.Location);
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x00015A44 File Offset: 0x00013C44
		private CsdlActionImport OnActionImportElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.Required("Name");
			string text2 = base.RequiredQualifiedName("Action");
			string text3 = base.Optional("EntitySet");
			return new CsdlActionImport(text, text2, text3, CsdlDocumentParser.Documentation(childValues), element.Location);
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x00015A8C File Offset: 0x00013C8C
		private CsdlFunctionImport OnFunctionImportElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.Required("Name");
			string text2 = base.RequiredQualifiedName("Function");
			string text3 = base.Optional("EntitySet");
			bool flag = base.OptionalBoolean("IncludeInServiceDocument") ?? false;
			return new CsdlFunctionImport(text, text2, text3, flag, CsdlDocumentParser.Documentation(childValues), element.Location);
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x00015AF4 File Offset: 0x00013CF4
		private CsdlOperationParameter OnFunctionImportParameterElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.Required("Name");
			string text2 = base.RequiredType("Type");
			CsdlTypeReference csdlTypeReference = this.ParseTypeReference(text2, null, element.Location, CsdlDocumentParser.Optionality.Required);
			return new CsdlOperationParameter(text, csdlTypeReference, CsdlDocumentParser.Documentation(childValues), element.Location);
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x00015B3C File Offset: 0x00013D3C
		private CsdlTypeReference OnEntityReferenceTypeElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.RequiredType("Type");
			return new CsdlExpressionTypeReference(new CsdlEntityReferenceType(this.ParseTypeReference(text, null, element.Location, CsdlDocumentParser.Optionality.Required), element.Location), true, element.Location);
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x00015B7C File Offset: 0x00013D7C
		private CsdlTypeReference OnTypeRefElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.RequiredType("Type");
			return this.ParseTypeReference(text, null, element.Location, CsdlDocumentParser.Optionality.Required);
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x00015BA4 File Offset: 0x00013DA4
		private CsdlTypeReference OnCollectionTypeElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.OptionalType("ElementType");
			CsdlTypeReference csdlTypeReference = this.ParseTypeReference(text, childValues, element.Location, CsdlDocumentParser.Optionality.Required);
			return new CsdlExpressionTypeReference(new CsdlCollectionType(csdlTypeReference, element.Location), csdlTypeReference.IsNullable, element.Location);
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x00015BEC File Offset: 0x00013DEC
		private CsdlOperationReturnType OnReturnTypeElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.RequiredType("Type");
			CsdlTypeReference csdlTypeReference = this.ParseTypeReference(text, childValues, element.Location, CsdlDocumentParser.Optionality.Required);
			return new CsdlOperationReturnType(csdlTypeReference, element.Location);
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x00015C24 File Offset: 0x00013E24
		private CsdlEntityContainer OnEntityContainerElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.Required("Name");
			string text2 = base.Optional("Extends");
			if (this.entityContainerCount++ > 0)
			{
				base.ReportError(this.currentElement.Location, EdmErrorCode.MetadataDocumentCannotHaveMoreThanOneEntityContainer, Strings.CsdlParser_MetadataDocumentCannotHaveMoreThanOneEntityContainer);
			}
			return new CsdlEntityContainer(text, text2, childValues.ValuesOfType<CsdlEntitySet>(), childValues.ValuesOfType<CsdlSingleton>(), childValues.ValuesOfType<CsdlOperationImport>(), CsdlDocumentParser.Documentation(childValues), element.Location);
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x00015CA0 File Offset: 0x00013EA0
		private CsdlEntitySet OnEntitySetElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.Required("Name");
			string text2 = base.RequiredQualifiedName("EntityType");
			return new CsdlEntitySet(text, text2, childValues.ValuesOfType<CsdlNavigationPropertyBinding>(), CsdlDocumentParser.Documentation(childValues), element.Location);
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x00015CE0 File Offset: 0x00013EE0
		private CsdlSingleton OnSingletonElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.Required("Name");
			string text2 = base.RequiredQualifiedName("Type");
			return new CsdlSingleton(text, text2, childValues.ValuesOfType<CsdlNavigationPropertyBinding>(), CsdlDocumentParser.Documentation(childValues), element.Location);
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x00015D20 File Offset: 0x00013F20
		private CsdlNavigationPropertyBinding OnNavigationPropertyBindingElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.Required("Path");
			string text2 = base.Required("Target");
			return new CsdlNavigationPropertyBinding(text, text2, CsdlDocumentParser.Documentation(childValues), element.Location);
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x00015D58 File Offset: 0x00013F58
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

		// Token: 0x0600085F RID: 2143 RVA: 0x00015DAB File Offset: 0x00013FAB
		private void ParseBinaryFacets(out bool Unbounded, out int? maxLength)
		{
			this.ParseMaxLength(out Unbounded, out maxLength);
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x00015DB5 File Offset: 0x00013FB5
		private void ParseDecimalFacets(out int? precision, out int? scale)
		{
			precision = base.OptionalInteger("Precision");
			scale = base.OptionalScale("Scale");
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x00015DDC File Offset: 0x00013FDC
		private void ParseStringFacets(out bool Unbounded, out int? maxLength, out bool? unicode, out string collation)
		{
			this.ParseMaxLength(out Unbounded, out maxLength);
			unicode = new bool?(base.OptionalBoolean("Unicode") ?? true);
			collation = base.Optional("Collation");
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x00015E2C File Offset: 0x0001402C
		private void ParseTemporalFacets(out int? precision)
		{
			precision = new int?(base.OptionalInteger("Precision") ?? 0);
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x00015E63 File Offset: 0x00014063
		private void ParseSpatialFacets(out int? srid, int defaultSrid)
		{
			srid = base.OptionalSrid("SRID", defaultSrid);
		}

		// Token: 0x04000422 RID: 1058
		private Version artifactVersion;

		// Token: 0x04000423 RID: 1059
		private int entityContainerCount;

		// Token: 0x020001A1 RID: 417
		private enum Optionality
		{
			// Token: 0x04000428 RID: 1064
			Optional,
			// Token: 0x04000429 RID: 1065
			Required
		}
	}
}
