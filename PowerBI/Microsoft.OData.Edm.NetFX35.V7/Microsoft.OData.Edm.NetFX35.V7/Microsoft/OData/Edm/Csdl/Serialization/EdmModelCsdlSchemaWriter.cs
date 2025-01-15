using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml;
using Microsoft.OData.Edm.Csdl.CsdlSemantics;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Vocabularies;
using Microsoft.OData.Edm.Vocabularies.V1;

namespace Microsoft.OData.Edm.Csdl.Serialization
{
	// Token: 0x02000150 RID: 336
	internal class EdmModelCsdlSchemaWriter
	{
		// Token: 0x06000843 RID: 2115 RVA: 0x000168AB File Offset: 0x00014AAB
		internal EdmModelCsdlSchemaWriter(IEdmModel model, VersioningDictionary<string, string> namespaceAliasMappings, XmlWriter xmlWriter, Version edmVersion)
		{
			this.xmlWriter = xmlWriter;
			this.version = edmVersion;
			this.edmxNamespace = CsdlConstants.SupportedEdmxVersions[edmVersion];
			this.model = model;
			this.namespaceAliasMappings = namespaceAliasMappings;
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x000168E2 File Offset: 0x00014AE2
		internal static string PathAsXml(IEnumerable<string> path)
		{
			return EdmUtil.JoinInternal<string>("/", path);
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x000168EF File Offset: 0x00014AEF
		internal void WriteReferenceElementHeader(IEdmReference reference)
		{
			this.xmlWriter.WriteStartElement("edmx", "Reference", this.edmxNamespace);
			this.WriteRequiredAttribute<Uri>("Uri", reference.Uri, new Func<Uri, string>(EdmValueWriter.UriAsXml));
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x0001692C File Offset: 0x00014B2C
		internal void WriteIncludeElement(IEdmInclude include)
		{
			this.xmlWriter.WriteStartElement("edmx", "Include", this.edmxNamespace);
			this.WriteRequiredAttribute<string>("Namespace", include.Namespace, new Func<string, string>(EdmValueWriter.StringAsXml));
			this.WriteRequiredAttribute<string>("Alias", include.Alias, new Func<string, string>(EdmValueWriter.StringAsXml));
			this.xmlWriter.WriteEndElement();
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x0001699C File Offset: 0x00014B9C
		internal void WriteIncludeAnnotationsElement(IEdmIncludeAnnotations includeAnnotations)
		{
			this.xmlWriter.WriteStartElement("edmx", "IncludeAnnotations", this.edmxNamespace);
			this.WriteRequiredAttribute<string>("TermNamespace", includeAnnotations.TermNamespace, new Func<string, string>(EdmValueWriter.StringAsXml));
			this.WriteOptionalAttribute<string>("Qualifier", includeAnnotations.Qualifier, new Func<string, string>(EdmValueWriter.StringAsXml));
			this.WriteOptionalAttribute<string>("TargetNamespace", includeAnnotations.TargetNamespace, new Func<string, string>(EdmValueWriter.StringAsXml));
			this.xmlWriter.WriteEndElement();
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x00016A28 File Offset: 0x00014C28
		internal void WriteTermElementHeader(IEdmTerm term, bool inlineType)
		{
			this.xmlWriter.WriteStartElement("Term");
			this.WriteRequiredAttribute<string>("Name", term.Name, new Func<string, string>(EdmValueWriter.StringAsXml));
			if (inlineType && term.Type != null)
			{
				this.WriteRequiredAttribute<IEdmTypeReference>("Type", term.Type, new Func<IEdmTypeReference, string>(this.TypeReferenceAsXml));
			}
			this.WriteOptionalAttribute<string>("DefaultValue", term.DefaultValue, new Func<string, string>(EdmValueWriter.StringAsXml));
			this.WriteOptionalAttribute<string>("AppliesTo", term.AppliesTo, new Func<string, string>(EdmValueWriter.StringAsXml));
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x00016AC4 File Offset: 0x00014CC4
		internal void WriteComplexTypeElementHeader(IEdmComplexType complexType)
		{
			this.xmlWriter.WriteStartElement("ComplexType");
			this.WriteRequiredAttribute<string>("Name", complexType.Name, new Func<string, string>(EdmValueWriter.StringAsXml));
			this.WriteOptionalAttribute<IEdmComplexType>("BaseType", complexType.BaseComplexType(), new Func<IEdmComplexType, string>(this.TypeDefinitionAsXml));
			this.WriteOptionalAttribute<bool>("Abstract", complexType.IsAbstract, false, new Func<bool, string>(EdmValueWriter.BooleanAsXml));
			this.WriteOptionalAttribute<bool>("OpenType", complexType.IsOpen, false, new Func<bool, string>(EdmValueWriter.BooleanAsXml));
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x00016B58 File Offset: 0x00014D58
		internal void WriteEnumTypeElementHeader(IEdmEnumType enumType)
		{
			this.xmlWriter.WriteStartElement("EnumType");
			this.WriteRequiredAttribute<string>("Name", enumType.Name, new Func<string, string>(EdmValueWriter.StringAsXml));
			if (enumType.UnderlyingType.PrimitiveKind != EdmPrimitiveTypeKind.Int32)
			{
				this.WriteRequiredAttribute<IEdmPrimitiveType>("UnderlyingType", enumType.UnderlyingType, new Func<IEdmPrimitiveType, string>(this.TypeDefinitionAsXml));
			}
			this.WriteOptionalAttribute<bool>("IsFlags", enumType.IsFlags, false, new Func<bool, string>(EdmValueWriter.BooleanAsXml));
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x00016BDC File Offset: 0x00014DDC
		internal void WriteEntityContainerElementHeader(IEdmEntityContainer container)
		{
			this.xmlWriter.WriteStartElement("EntityContainer");
			this.WriteRequiredAttribute<string>("Name", container.Name, new Func<string, string>(EdmValueWriter.StringAsXml));
			CsdlSemanticsEntityContainer csdlSemanticsEntityContainer = container as CsdlSemanticsEntityContainer;
			CsdlEntityContainer csdlEntityContainer;
			if (csdlSemanticsEntityContainer != null && (csdlEntityContainer = csdlSemanticsEntityContainer.Element as CsdlEntityContainer) != null)
			{
				this.WriteOptionalAttribute<string>("Extends", csdlEntityContainer.Extends, new Func<string, string>(EdmValueWriter.StringAsXml));
			}
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x00016C50 File Offset: 0x00014E50
		internal void WriteEntitySetElementHeader(IEdmEntitySet entitySet)
		{
			this.xmlWriter.WriteStartElement("EntitySet");
			this.WriteRequiredAttribute<string>("Name", entitySet.Name, new Func<string, string>(EdmValueWriter.StringAsXml));
			this.WriteRequiredAttribute<string>("EntityType", entitySet.EntityType().FullName(), new Func<string, string>(EdmValueWriter.StringAsXml));
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x00016CAC File Offset: 0x00014EAC
		internal void WriteSingletonElementHeader(IEdmSingleton singleton)
		{
			this.xmlWriter.WriteStartElement("Singleton");
			this.WriteRequiredAttribute<string>("Name", singleton.Name, new Func<string, string>(EdmValueWriter.StringAsXml));
			this.WriteRequiredAttribute<string>("Type", singleton.EntityType().FullName(), new Func<string, string>(EdmValueWriter.StringAsXml));
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x00016D08 File Offset: 0x00014F08
		internal void WriteNavigationPropertyBinding(IEdmNavigationSource navigationSource, IEdmNavigationPropertyBinding binding)
		{
			this.WriteNavigationPropertyBinding(binding);
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x00016D14 File Offset: 0x00014F14
		internal void WriteEntityTypeElementHeader(IEdmEntityType entityType)
		{
			this.xmlWriter.WriteStartElement("EntityType");
			this.WriteRequiredAttribute<string>("Name", entityType.Name, new Func<string, string>(EdmValueWriter.StringAsXml));
			this.WriteOptionalAttribute<IEdmEntityType>("BaseType", entityType.BaseEntityType(), new Func<IEdmEntityType, string>(this.TypeDefinitionAsXml));
			this.WriteOptionalAttribute<bool>("Abstract", entityType.IsAbstract, false, new Func<bool, string>(EdmValueWriter.BooleanAsXml));
			this.WriteOptionalAttribute<bool>("OpenType", entityType.IsOpen, false, new Func<bool, string>(EdmValueWriter.BooleanAsXml));
			bool flag = entityType.HasStream && (entityType.BaseEntityType() == null || (entityType.BaseEntityType() != null && !entityType.BaseEntityType().HasStream));
			this.WriteOptionalAttribute<bool>("HasStream", flag, false, new Func<bool, string>(EdmValueWriter.BooleanAsXml));
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x00016DF0 File Offset: 0x00014FF0
		internal void WriteDelaredKeyPropertiesElementHeader()
		{
			this.xmlWriter.WriteStartElement("Key");
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x00016E02 File Offset: 0x00015002
		internal void WritePropertyRefElement(IEdmStructuralProperty property)
		{
			this.xmlWriter.WriteStartElement("PropertyRef");
			this.WriteRequiredAttribute<string>("Name", property.Name, new Func<string, string>(EdmValueWriter.StringAsXml));
			this.WriteEndElement();
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x00016E38 File Offset: 0x00015038
		internal void WriteNavigationPropertyElementHeader(IEdmNavigationProperty member)
		{
			this.xmlWriter.WriteStartElement("NavigationProperty");
			this.WriteRequiredAttribute<string>("Name", member.Name, new Func<string, string>(EdmValueWriter.StringAsXml));
			this.WriteRequiredAttribute<IEdmTypeReference>("Type", member.Type, new Func<IEdmTypeReference, string>(this.TypeReferenceAsXml));
			if (!member.Type.IsCollection() && !member.Type.IsNullable)
			{
				this.WriteRequiredAttribute<bool>("Nullable", member.Type.IsNullable, new Func<bool, string>(EdmValueWriter.BooleanAsXml));
			}
			if (member.Partner != null)
			{
				this.WriteRequiredAttribute<string>("Partner", member.GetPartnerPath().Path, new Func<string, string>(EdmValueWriter.StringAsXml));
			}
			this.WriteOptionalAttribute<bool>("ContainsTarget", member.ContainsTarget, false, new Func<bool, string>(EdmValueWriter.BooleanAsXml));
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x00016F13 File Offset: 0x00015113
		internal void WriteOperationActionElement(string elementName, EdmOnDeleteAction operationAction)
		{
			this.xmlWriter.WriteStartElement(elementName);
			this.WriteRequiredAttribute<string>("Action", operationAction.ToString(), new Func<string, string>(EdmValueWriter.StringAsXml));
			this.WriteEndElement();
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x00016F4C File Offset: 0x0001514C
		internal void WriteSchemaElementHeader(EdmSchema schema, string alias, IEnumerable<KeyValuePair<string, string>> mappings)
		{
			string csdlNamespace = EdmModelCsdlSchemaWriter.GetCsdlNamespace(this.version);
			this.xmlWriter.WriteStartElement("Schema", csdlNamespace);
			this.WriteOptionalAttribute<string>("Namespace", schema.Namespace, string.Empty, new Func<string, string>(EdmValueWriter.StringAsXml));
			this.WriteOptionalAttribute<string>("Alias", alias, new Func<string, string>(EdmValueWriter.StringAsXml));
			if (mappings != null)
			{
				foreach (KeyValuePair<string, string> keyValuePair in mappings)
				{
					this.xmlWriter.WriteAttributeString("xmlns", keyValuePair.Key, null, keyValuePair.Value);
				}
			}
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x00017008 File Offset: 0x00015208
		internal void WriteAnnotationsElementHeader(string annotationsTarget)
		{
			this.xmlWriter.WriteStartElement("Annotations");
			this.WriteRequiredAttribute<string>("Target", annotationsTarget, new Func<string, string>(EdmValueWriter.StringAsXml));
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x00017034 File Offset: 0x00015234
		internal void WriteStructuralPropertyElementHeader(IEdmStructuralProperty property, bool inlineType)
		{
			this.xmlWriter.WriteStartElement("Property");
			this.WriteRequiredAttribute<string>("Name", property.Name, new Func<string, string>(EdmValueWriter.StringAsXml));
			if (inlineType)
			{
				this.WriteRequiredAttribute<IEdmTypeReference>("Type", property.Type, new Func<IEdmTypeReference, string>(this.TypeReferenceAsXml));
			}
			this.WriteOptionalAttribute<string>("DefaultValue", property.DefaultValueString, new Func<string, string>(EdmValueWriter.StringAsXml));
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x000170AC File Offset: 0x000152AC
		internal void WriteEnumMemberElementHeader(IEdmEnumMember member)
		{
			this.xmlWriter.WriteStartElement("Member");
			this.WriteRequiredAttribute<string>("Name", member.Name, new Func<string, string>(EdmValueWriter.StringAsXml));
			bool? flag = member.IsValueExplicit(this.model);
			if (flag == null || flag.Value)
			{
				this.xmlWriter.WriteAttributeString("Value", EdmValueWriter.LongAsXml(member.Value.Value));
			}
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x00017125 File Offset: 0x00015325
		internal void WriteNullableAttribute(IEdmTypeReference reference)
		{
			this.WriteOptionalAttribute<bool>("Nullable", reference.IsNullable, true, new Func<bool, string>(EdmValueWriter.BooleanAsXml));
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x00017148 File Offset: 0x00015348
		internal void WriteTypeDefinitionAttributes(IEdmTypeDefinitionReference reference)
		{
			IEdmTypeReference edmTypeReference = reference.AsActualTypeReference();
			if (edmTypeReference.IsBinary())
			{
				this.WriteBinaryTypeAttributes(edmTypeReference.AsBinary());
				return;
			}
			if (edmTypeReference.IsString())
			{
				this.WriteStringTypeAttributes(edmTypeReference.AsString());
				return;
			}
			if (edmTypeReference.IsTemporal())
			{
				this.WriteTemporalTypeAttributes(edmTypeReference.AsTemporal());
				return;
			}
			if (edmTypeReference.IsDecimal())
			{
				this.WriteDecimalTypeAttributes(edmTypeReference.AsDecimal());
				return;
			}
			if (edmTypeReference.IsSpatial())
			{
				this.WriteSpatialTypeAttributes(edmTypeReference.AsSpatial());
			}
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x000171C4 File Offset: 0x000153C4
		internal void WriteBinaryTypeAttributes(IEdmBinaryTypeReference reference)
		{
			if (reference.IsUnbounded)
			{
				this.WriteRequiredAttribute<string>("MaxLength", "max", new Func<string, string>(EdmValueWriter.StringAsXml));
				return;
			}
			this.WriteOptionalAttribute<int?>("MaxLength", reference.MaxLength, new Func<int?, string>(EdmValueWriter.IntAsXml));
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x00017214 File Offset: 0x00015414
		internal void WriteDecimalTypeAttributes(IEdmDecimalTypeReference reference)
		{
			this.WriteOptionalAttribute<int?>("Precision", reference.Precision, new Func<int?, string>(EdmValueWriter.IntAsXml));
			this.WriteOptionalAttribute<int?>("Scale", reference.Scale, new int?(0), new Func<int?, string>(EdmModelCsdlSchemaWriter.ScaleAsXml));
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x00017264 File Offset: 0x00015464
		internal void WriteSpatialTypeAttributes(IEdmSpatialTypeReference reference)
		{
			if (reference.IsGeography())
			{
				this.WriteOptionalAttribute<int?>("SRID", reference.SpatialReferenceIdentifier, new int?(4326), new Func<int?, string>(EdmModelCsdlSchemaWriter.SridAsXml));
				return;
			}
			if (reference.IsGeometry())
			{
				this.WriteOptionalAttribute<int?>("SRID", reference.SpatialReferenceIdentifier, new int?(0), new Func<int?, string>(EdmModelCsdlSchemaWriter.SridAsXml));
			}
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x000172CC File Offset: 0x000154CC
		internal void WriteStringTypeAttributes(IEdmStringTypeReference reference)
		{
			if (reference.IsUnbounded)
			{
				this.WriteRequiredAttribute<string>("MaxLength", "max", new Func<string, string>(EdmValueWriter.StringAsXml));
			}
			else
			{
				this.WriteOptionalAttribute<int?>("MaxLength", reference.MaxLength, new Func<int?, string>(EdmValueWriter.IntAsXml));
			}
			if (reference.IsUnicode != null)
			{
				this.WriteOptionalAttribute<bool?>("Unicode", reference.IsUnicode, new bool?(true), new Func<bool?, string>(EdmValueWriter.BooleanAsXml));
			}
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x00017350 File Offset: 0x00015550
		internal void WriteTemporalTypeAttributes(IEdmTemporalTypeReference reference)
		{
			if (reference.Precision != null)
			{
				this.WriteOptionalAttribute<int?>("Precision", reference.Precision, new int?(0), new Func<int?, string>(EdmValueWriter.IntAsXml));
			}
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x00017390 File Offset: 0x00015590
		internal void WriteReferentialConstraintElementHeader(IEdmNavigationProperty constraint)
		{
			this.xmlWriter.WriteStartElement("ReferentialConstraint");
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x000173A4 File Offset: 0x000155A4
		internal void WriteReferentialConstraintPair(EdmReferentialConstraintPropertyPair pair)
		{
			this.xmlWriter.WriteStartElement("ReferentialConstraint");
			this.WriteRequiredAttribute<string>("Property", pair.DependentProperty.Name, new Func<string, string>(EdmValueWriter.StringAsXml));
			this.WriteRequiredAttribute<string>("ReferencedProperty", pair.PrincipalProperty.Name, new Func<string, string>(EdmValueWriter.StringAsXml));
			this.WriteEndElement();
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x0001740C File Offset: 0x0001560C
		internal void WriteAnnotationStringAttribute(IEdmDirectValueAnnotation annotation)
		{
			IEdmPrimitiveValue edmPrimitiveValue = (IEdmPrimitiveValue)annotation.Value;
			if (edmPrimitiveValue != null)
			{
				this.xmlWriter.WriteAttributeString(annotation.Name, annotation.NamespaceUri, EdmValueWriter.PrimitiveValueAsXml(edmPrimitiveValue));
			}
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x00017448 File Offset: 0x00015648
		internal void WriteAnnotationStringElement(IEdmDirectValueAnnotation annotation)
		{
			IEdmPrimitiveValue edmPrimitiveValue = (IEdmPrimitiveValue)annotation.Value;
			if (edmPrimitiveValue != null)
			{
				this.xmlWriter.WriteRaw(((IEdmStringValue)edmPrimitiveValue).Value);
			}
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x0001747A File Offset: 0x0001567A
		internal void WriteActionElementHeader(IEdmAction action)
		{
			this.xmlWriter.WriteStartElement("Action");
			this.WriteOperationElementAttributes(action);
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x00017493 File Offset: 0x00015693
		internal void WriteFunctionElementHeader(IEdmFunction function)
		{
			this.xmlWriter.WriteStartElement("Function");
			this.WriteOperationElementAttributes(function);
			if (function.IsComposable)
			{
				this.WriteOptionalAttribute<bool>("IsComposable", function.IsComposable, new Func<bool, string>(EdmValueWriter.BooleanAsXml));
			}
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x000174D1 File Offset: 0x000156D1
		internal void WriteReturnTypeElementHeader()
		{
			this.xmlWriter.WriteStartElement("ReturnType");
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x000174E3 File Offset: 0x000156E3
		internal void WriteTypeAttribute(IEdmTypeReference typeReference)
		{
			this.WriteRequiredAttribute<IEdmTypeReference>("Type", typeReference, new Func<IEdmTypeReference, string>(this.TypeReferenceAsXml));
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x000174FD File Offset: 0x000156FD
		internal void WriteActionImportElementHeader(IEdmActionImport actionImport)
		{
			this.xmlWriter.WriteStartElement("ActionImport");
			this.WriteOperationImportAttributes(actionImport, "Action");
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x0001751B File Offset: 0x0001571B
		internal void WriteFunctionImportElementHeader(IEdmFunctionImport functionImport)
		{
			this.xmlWriter.WriteStartElement("FunctionImport");
			this.WriteOperationImportAttributes(functionImport, "Function");
			this.WriteOptionalAttribute<bool>("IncludeInServiceDocument", functionImport.IncludeInServiceDocument, false, new Func<bool, string>(EdmValueWriter.BooleanAsXml));
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x00017558 File Offset: 0x00015758
		internal void WriteOperationParameterElementHeader(IEdmOperationParameter parameter, bool inlineType)
		{
			this.xmlWriter.WriteStartElement("Parameter");
			this.WriteRequiredAttribute<string>("Name", parameter.Name, new Func<string, string>(EdmValueWriter.StringAsXml));
			if (inlineType)
			{
				this.WriteRequiredAttribute<IEdmTypeReference>("Type", parameter.Type, new Func<IEdmTypeReference, string>(this.TypeReferenceAsXml));
			}
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x000175B4 File Offset: 0x000157B4
		internal void WriteOperationParameterEndElement(IEdmOperationParameter parameter)
		{
			IEdmOptionalParameter edmOptionalParameter = parameter as IEdmOptionalParameter;
			if (edmOptionalParameter != null)
			{
				if (!Enumerable.Any<IEdmVocabularyAnnotation>(edmOptionalParameter.VocabularyAnnotations(this.model), (IEdmVocabularyAnnotation a) => a.Term == CoreVocabularyModel.OptionalParameterTerm))
				{
					string defaultValueString = edmOptionalParameter.DefaultValueString;
					EdmRecordExpression edmRecordExpression = new EdmRecordExpression(new IEdmPropertyConstructor[0]);
					this.WriteVocabularyAnnotationElementHeader(new EdmVocabularyAnnotation(parameter, CoreVocabularyModel.OptionalParameterTerm, edmRecordExpression), false);
					if (!string.IsNullOrEmpty(defaultValueString))
					{
						EdmPropertyConstructor edmPropertyConstructor = new EdmPropertyConstructor("DefaultValue", new EdmStringConstant(defaultValueString));
						this.WriteRecordExpressionElementHeader(edmRecordExpression);
						this.WritePropertyValueElementHeader(edmPropertyConstructor, true);
						this.WriteEndElement();
						this.WriteEndElement();
					}
					this.WriteEndElement();
				}
			}
			this.WriteEndElement();
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x00017666 File Offset: 0x00015866
		internal void WriteCollectionTypeElementHeader(IEdmCollectionType collectionType, bool inlineType)
		{
			this.xmlWriter.WriteStartElement("CollectionType");
			if (inlineType)
			{
				this.WriteRequiredAttribute<IEdmTypeReference>("ElementType", collectionType.ElementType, new Func<IEdmTypeReference, string>(this.TypeReferenceAsXml));
			}
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x00017698 File Offset: 0x00015898
		internal void WriteInlineExpression(IEdmExpression expression)
		{
			switch (expression.ExpressionKind)
			{
			case EdmExpressionKind.BinaryConstant:
				this.WriteRequiredAttribute<byte[]>("Binary", ((IEdmBinaryConstantExpression)expression).Value, new Func<byte[], string>(EdmValueWriter.BinaryAsXml));
				return;
			case EdmExpressionKind.BooleanConstant:
				this.WriteRequiredAttribute<bool>("Bool", ((IEdmBooleanConstantExpression)expression).Value, new Func<bool, string>(EdmValueWriter.BooleanAsXml));
				return;
			case EdmExpressionKind.DateTimeOffsetConstant:
				this.WriteRequiredAttribute<DateTimeOffset>("DateTimeOffset", ((IEdmDateTimeOffsetConstantExpression)expression).Value, new Func<DateTimeOffset, string>(EdmValueWriter.DateTimeOffsetAsXml));
				return;
			case EdmExpressionKind.DecimalConstant:
				this.WriteRequiredAttribute<decimal>("Decimal", ((IEdmDecimalConstantExpression)expression).Value, new Func<decimal, string>(EdmValueWriter.DecimalAsXml));
				return;
			case EdmExpressionKind.FloatingConstant:
				this.WriteRequiredAttribute<double>("Float", ((IEdmFloatingConstantExpression)expression).Value, new Func<double, string>(EdmValueWriter.FloatAsXml));
				return;
			case EdmExpressionKind.GuidConstant:
				this.WriteRequiredAttribute<Guid>("Guid", ((IEdmGuidConstantExpression)expression).Value, new Func<Guid, string>(EdmValueWriter.GuidAsXml));
				return;
			case EdmExpressionKind.IntegerConstant:
				this.WriteRequiredAttribute<long>("Int", ((IEdmIntegerConstantExpression)expression).Value, new Func<long, string>(EdmValueWriter.LongAsXml));
				return;
			case EdmExpressionKind.StringConstant:
				this.WriteRequiredAttribute<string>("String", ((IEdmStringConstantExpression)expression).Value, new Func<string, string>(EdmValueWriter.StringAsXml));
				return;
			case EdmExpressionKind.DurationConstant:
				this.WriteRequiredAttribute<TimeSpan>("Duration", ((IEdmDurationConstantExpression)expression).Value, new Func<TimeSpan, string>(EdmValueWriter.DurationAsXml));
				return;
			case EdmExpressionKind.Null:
			case EdmExpressionKind.Record:
			case EdmExpressionKind.Collection:
			case EdmExpressionKind.If:
			case EdmExpressionKind.Cast:
			case EdmExpressionKind.IsType:
			case EdmExpressionKind.FunctionApplication:
			case EdmExpressionKind.LabeledExpressionReference:
			case EdmExpressionKind.Labeled:
				break;
			case EdmExpressionKind.Path:
				this.WriteRequiredAttribute<IEnumerable<string>>("Path", ((IEdmPathExpression)expression).PathSegments, new Func<IEnumerable<string>, string>(EdmModelCsdlSchemaWriter.PathAsXml));
				return;
			case EdmExpressionKind.PropertyPath:
				this.WriteRequiredAttribute<IEnumerable<string>>("PropertyPath", ((IEdmPathExpression)expression).PathSegments, new Func<IEnumerable<string>, string>(EdmModelCsdlSchemaWriter.PathAsXml));
				return;
			case EdmExpressionKind.NavigationPropertyPath:
				this.WriteRequiredAttribute<IEnumerable<string>>("NavigationPropertyPath", ((IEdmPathExpression)expression).PathSegments, new Func<IEnumerable<string>, string>(EdmModelCsdlSchemaWriter.PathAsXml));
				return;
			case EdmExpressionKind.DateConstant:
				this.WriteRequiredAttribute<Date>("Date", ((IEdmDateConstantExpression)expression).Value, new Func<Date, string>(EdmValueWriter.DateAsXml));
				return;
			case EdmExpressionKind.TimeOfDayConstant:
				this.WriteRequiredAttribute<TimeOfDay>("TimeOfDay", ((IEdmTimeOfDayConstantExpression)expression).Value, new Func<TimeOfDay, string>(EdmValueWriter.TimeOfDayAsXml));
				break;
			default:
				return;
			}
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x000178FC File Offset: 0x00015AFC
		internal void WriteVocabularyAnnotationElementHeader(IEdmVocabularyAnnotation annotation, bool isInline)
		{
			this.xmlWriter.WriteStartElement("Annotation");
			this.WriteRequiredAttribute<IEdmTerm>("Term", annotation.Term, new Func<IEdmTerm, string>(this.TermAsXml));
			this.WriteOptionalAttribute<string>("Qualifier", annotation.Qualifier, new Func<string, string>(EdmValueWriter.StringAsXml));
			if (isInline)
			{
				this.WriteInlineExpression(annotation.Value);
			}
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x00017962 File Offset: 0x00015B62
		internal void WritePropertyValueElementHeader(IEdmPropertyConstructor value, bool isInline)
		{
			this.xmlWriter.WriteStartElement("PropertyValue");
			this.WriteRequiredAttribute<string>("Property", value.Name, new Func<string, string>(EdmValueWriter.StringAsXml));
			if (isInline)
			{
				this.WriteInlineExpression(value.Value);
			}
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x000179A0 File Offset: 0x00015BA0
		internal void WriteRecordExpressionElementHeader(IEdmRecordExpression expression)
		{
			this.xmlWriter.WriteStartElement("Record");
			this.WriteOptionalAttribute<IEdmStructuredTypeReference>("Type", expression.DeclaredType, new Func<IEdmStructuredTypeReference, string>(this.TypeReferenceAsXml));
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x00017962 File Offset: 0x00015B62
		internal void WritePropertyConstructorElementHeader(IEdmPropertyConstructor constructor, bool isInline)
		{
			this.xmlWriter.WriteStartElement("PropertyValue");
			this.WriteRequiredAttribute<string>("Property", constructor.Name, new Func<string, string>(EdmValueWriter.StringAsXml));
			if (isInline)
			{
				this.WriteInlineExpression(constructor.Value);
			}
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x000179CF File Offset: 0x00015BCF
		internal void WriteStringConstantExpressionElement(IEdmStringConstantExpression expression)
		{
			this.xmlWriter.WriteStartElement("String");
			this.xmlWriter.WriteString(EdmValueWriter.StringAsXml(expression.Value));
			this.WriteEndElement();
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x000179FD File Offset: 0x00015BFD
		internal void WriteBinaryConstantExpressionElement(IEdmBinaryConstantExpression expression)
		{
			this.xmlWriter.WriteStartElement("Binary");
			this.xmlWriter.WriteString(EdmValueWriter.BinaryAsXml(expression.Value));
			this.WriteEndElement();
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x00017A2B File Offset: 0x00015C2B
		internal void WriteBooleanConstantExpressionElement(IEdmBooleanConstantExpression expression)
		{
			this.xmlWriter.WriteStartElement("Bool");
			this.xmlWriter.WriteString(EdmValueWriter.BooleanAsXml(expression.Value));
			this.WriteEndElement();
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x00017A59 File Offset: 0x00015C59
		internal void WriteNullConstantExpressionElement(IEdmNullExpression expression)
		{
			this.xmlWriter.WriteStartElement("Null");
			this.WriteEndElement();
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x00017A71 File Offset: 0x00015C71
		internal void WriteDateConstantExpressionElement(IEdmDateConstantExpression expression)
		{
			this.xmlWriter.WriteStartElement("Date");
			this.xmlWriter.WriteString(EdmValueWriter.DateAsXml(expression.Value));
			this.WriteEndElement();
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x00017A9F File Offset: 0x00015C9F
		internal void WriteDateTimeOffsetConstantExpressionElement(IEdmDateTimeOffsetConstantExpression expression)
		{
			this.xmlWriter.WriteStartElement("DateTimeOffset");
			this.xmlWriter.WriteString(EdmValueWriter.DateTimeOffsetAsXml(expression.Value));
			this.WriteEndElement();
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x00017ACD File Offset: 0x00015CCD
		internal void WriteDurationConstantExpressionElement(IEdmDurationConstantExpression expression)
		{
			this.xmlWriter.WriteStartElement("Duration");
			this.xmlWriter.WriteString(EdmValueWriter.DurationAsXml(expression.Value));
			this.WriteEndElement();
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x00017AFB File Offset: 0x00015CFB
		internal void WriteDecimalConstantExpressionElement(IEdmDecimalConstantExpression expression)
		{
			this.xmlWriter.WriteStartElement("Decimal");
			this.xmlWriter.WriteString(EdmValueWriter.DecimalAsXml(expression.Value));
			this.WriteEndElement();
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x00017B29 File Offset: 0x00015D29
		internal void WriteFloatingConstantExpressionElement(IEdmFloatingConstantExpression expression)
		{
			this.xmlWriter.WriteStartElement("Float");
			this.xmlWriter.WriteString(EdmValueWriter.FloatAsXml(expression.Value));
			this.WriteEndElement();
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x00017B57 File Offset: 0x00015D57
		internal void WriteFunctionApplicationElementHeader(IEdmApplyExpression expression)
		{
			this.xmlWriter.WriteStartElement("Apply");
			this.WriteRequiredAttribute<IEdmFunction>("Function", expression.AppliedFunction, new Func<IEdmFunction, string>(this.FunctionAsXml));
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x00017B86 File Offset: 0x00015D86
		internal void WriteGuidConstantExpressionElement(IEdmGuidConstantExpression expression)
		{
			this.xmlWriter.WriteStartElement("Guid");
			this.xmlWriter.WriteString(EdmValueWriter.GuidAsXml(expression.Value));
			this.WriteEndElement();
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x00017BB4 File Offset: 0x00015DB4
		internal void WriteIntegerConstantExpressionElement(IEdmIntegerConstantExpression expression)
		{
			this.xmlWriter.WriteStartElement("Int");
			this.xmlWriter.WriteString(EdmValueWriter.LongAsXml(expression.Value));
			this.WriteEndElement();
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x00017BE2 File Offset: 0x00015DE2
		internal void WritePathExpressionElement(IEdmPathExpression expression)
		{
			this.xmlWriter.WriteStartElement("Path");
			this.xmlWriter.WriteString(EdmModelCsdlSchemaWriter.PathAsXml(expression.PathSegments));
			this.WriteEndElement();
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x00017C10 File Offset: 0x00015E10
		internal void WritePropertyPathExpressionElement(IEdmPathExpression expression)
		{
			this.xmlWriter.WriteStartElement("PropertyPath");
			this.xmlWriter.WriteString(EdmModelCsdlSchemaWriter.PathAsXml(expression.PathSegments));
			this.WriteEndElement();
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x00017C3E File Offset: 0x00015E3E
		internal void WriteNavigationPropertyPathExpressionElement(IEdmPathExpression expression)
		{
			this.xmlWriter.WriteStartElement("NavigationPropertyPath");
			this.xmlWriter.WriteString(EdmModelCsdlSchemaWriter.PathAsXml(expression.PathSegments));
			this.WriteEndElement();
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x00017C6C File Offset: 0x00015E6C
		internal void WriteIfExpressionElementHeader(IEdmIfExpression expression)
		{
			this.xmlWriter.WriteStartElement("If");
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x00017C7E File Offset: 0x00015E7E
		internal void WriteCollectionExpressionElementHeader(IEdmCollectionExpression expression)
		{
			this.xmlWriter.WriteStartElement("Collection");
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x00017C90 File Offset: 0x00015E90
		internal void WriteLabeledElementHeader(IEdmLabeledExpression labeledElement)
		{
			this.xmlWriter.WriteStartElement("LabeledElement");
			this.WriteRequiredAttribute<string>("Name", labeledElement.Name, new Func<string, string>(EdmValueWriter.StringAsXml));
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x00017CBF File Offset: 0x00015EBF
		internal void WriteTimeOfDayConstantExpressionElement(IEdmTimeOfDayConstantExpression expression)
		{
			this.xmlWriter.WriteStartElement("TimeOfDay");
			this.xmlWriter.WriteString(EdmValueWriter.TimeOfDayAsXml(expression.Value));
			this.WriteEndElement();
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x00017CED File Offset: 0x00015EED
		internal void WriteIsTypeExpressionElementHeader(IEdmIsTypeExpression expression, bool inlineType)
		{
			this.xmlWriter.WriteStartElement("IsType");
			if (inlineType)
			{
				this.WriteRequiredAttribute<IEdmTypeReference>("Type", expression.Type, new Func<IEdmTypeReference, string>(this.TypeReferenceAsXml));
			}
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x00017D1F File Offset: 0x00015F1F
		internal void WriteCastExpressionElementHeader(IEdmCastExpression expression, bool inlineType)
		{
			this.xmlWriter.WriteStartElement("Cast");
			if (inlineType)
			{
				this.WriteRequiredAttribute<IEdmTypeReference>("Type", expression.Type, new Func<IEdmTypeReference, string>(this.TypeReferenceAsXml));
			}
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x00017D51 File Offset: 0x00015F51
		internal void WriteEnumMemberExpressionElement(IEdmEnumMemberExpression expression)
		{
			this.xmlWriter.WriteStartElement("EnumMember");
			this.xmlWriter.WriteString(EdmModelCsdlSchemaWriter.EnumMemberAsXml(expression.EnumMembers));
			this.WriteEndElement();
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x00017D80 File Offset: 0x00015F80
		internal void WriteTypeDefinitionElementHeader(IEdmTypeDefinition typeDefinition)
		{
			this.xmlWriter.WriteStartElement("TypeDefinition");
			this.WriteRequiredAttribute<string>("Name", typeDefinition.Name, new Func<string, string>(EdmValueWriter.StringAsXml));
			this.WriteRequiredAttribute<IEdmPrimitiveType>("UnderlyingType", typeDefinition.UnderlyingType, new Func<IEdmPrimitiveType, string>(this.TypeDefinitionAsXml));
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x00017DD7 File Offset: 0x00015FD7
		internal void WriteEndElement()
		{
			this.xmlWriter.WriteEndElement();
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x00017DE4 File Offset: 0x00015FE4
		internal void WriteOptionalAttribute<T>(string attribute, T value, T defaultValue, Func<T, string> toXml)
		{
			if (!value.Equals(defaultValue))
			{
				this.xmlWriter.WriteAttributeString(attribute, toXml.Invoke(value));
			}
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x00017E0F File Offset: 0x0001600F
		internal void WriteOptionalAttribute<T>(string attribute, T value, Func<T, string> toXml)
		{
			if (value != null)
			{
				this.xmlWriter.WriteAttributeString(attribute, toXml.Invoke(value));
			}
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x00017E2C File Offset: 0x0001602C
		internal void WriteRequiredAttribute<T>(string attribute, T value, Func<T, string> toXml)
		{
			this.xmlWriter.WriteAttributeString(attribute, toXml.Invoke(value));
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x00017E44 File Offset: 0x00016044
		private void WriteOperationElementAttributes(IEdmOperation operation)
		{
			this.WriteRequiredAttribute<string>("Name", operation.Name, new Func<string, string>(EdmValueWriter.StringAsXml));
			if (operation.IsBound)
			{
				this.WriteOptionalAttribute<bool>("IsBound", operation.IsBound, new Func<bool, string>(EdmValueWriter.BooleanAsXml));
			}
			if (operation.EntitySetPath != null)
			{
				this.WriteOptionalAttribute<IEnumerable<string>>("EntitySetPath", operation.EntitySetPath.PathSegments, new Func<IEnumerable<string>, string>(EdmModelCsdlSchemaWriter.PathAsXml));
			}
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x00017EC0 File Offset: 0x000160C0
		private void WriteNavigationPropertyBinding(IEdmNavigationPropertyBinding binding)
		{
			this.xmlWriter.WriteStartElement("NavigationPropertyBinding");
			this.WriteRequiredAttribute<string>("Path", binding.Path.Path, new Func<string, string>(EdmValueWriter.StringAsXml));
			this.WriteRequiredAttribute<string>("Target", binding.Target.Name, new Func<string, string>(EdmValueWriter.StringAsXml));
			this.xmlWriter.WriteEndElement();
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x00017F2C File Offset: 0x0001612C
		private static string EnumMemberAsXml(IEnumerable<IEdmEnumMember> members)
		{
			string text = Enumerable.First<IEdmEnumMember>(members).DeclaringType.FullName();
			List<string> list = new List<string>();
			foreach (IEdmEnumMember edmEnumMember in members)
			{
				list.Add(text + "/" + edmEnumMember.Name);
			}
			return string.Join(" ", list.ToArray());
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x00017FAC File Offset: 0x000161AC
		private static string SridAsXml(int? i)
		{
			if (i == null)
			{
				return "Variable";
			}
			return Convert.ToString(i.Value, CultureInfo.InvariantCulture);
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x00017FAC File Offset: 0x000161AC
		private static string ScaleAsXml(int? i)
		{
			if (i == null)
			{
				return "Variable";
			}
			return Convert.ToString(i.Value, CultureInfo.InvariantCulture);
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x00017FD0 File Offset: 0x000161D0
		private static string GetCsdlNamespace(Version edmVersion)
		{
			string[] array;
			if (CsdlConstants.SupportedVersions.TryGetValue(edmVersion, ref array))
			{
				return array[0];
			}
			throw new InvalidOperationException(Strings.Serializer_UnknownEdmVersion);
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x00017FFC File Offset: 0x000161FC
		private void WriteOperationImportAttributes(IEdmOperationImport operationImport, string operationAttributeName)
		{
			this.WriteRequiredAttribute<string>("Name", operationImport.Name, new Func<string, string>(EdmValueWriter.StringAsXml));
			this.WriteRequiredAttribute<string>(operationAttributeName, operationImport.Operation.FullName(), new Func<string, string>(EdmValueWriter.StringAsXml));
			if (operationImport.EntitySet == null)
			{
				return;
			}
			IEdmPathExpression edmPathExpression = operationImport.EntitySet as IEdmPathExpression;
			if (edmPathExpression != null)
			{
				this.WriteOptionalAttribute<IEnumerable<string>>("EntitySet", edmPathExpression.PathSegments, new Func<IEnumerable<string>, string>(EdmModelCsdlSchemaWriter.PathAsXml));
				return;
			}
			throw new InvalidOperationException(Strings.EdmModel_Validator_Semantic_OperationImportEntitySetExpressionIsInvalid(operationImport.Name));
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x0001808C File Offset: 0x0001628C
		private string SerializationName(IEdmSchemaElement element)
		{
			string text;
			if (this.namespaceAliasMappings != null && this.namespaceAliasMappings.TryGetValue(element.Namespace, out text))
			{
				return text + "." + element.Name;
			}
			return element.FullName();
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x000180D0 File Offset: 0x000162D0
		private string TypeReferenceAsXml(IEdmTypeReference type)
		{
			if (type.IsCollection())
			{
				IEdmCollectionTypeReference edmCollectionTypeReference = type.AsCollection();
				return "Collection(" + this.SerializationName((IEdmSchemaElement)edmCollectionTypeReference.ElementType().Definition) + ")";
			}
			if (type.IsEntityReference())
			{
				return "Ref(" + this.SerializationName(type.AsEntityReference().EntityReferenceDefinition().EntityType) + ")";
			}
			return this.SerializationName((IEdmSchemaElement)type.Definition);
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x00018151 File Offset: 0x00016351
		private string TypeDefinitionAsXml(IEdmSchemaType type)
		{
			return this.SerializationName(type);
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x00018151 File Offset: 0x00016351
		private string FunctionAsXml(IEdmOperation operation)
		{
			return this.SerializationName(operation);
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x0001815A File Offset: 0x0001635A
		private string TermAsXml(IEdmTerm term)
		{
			if (term == null)
			{
				return string.Empty;
			}
			return this.SerializationName(term);
		}

		// Token: 0x04000558 RID: 1368
		protected XmlWriter xmlWriter;

		// Token: 0x04000559 RID: 1369
		protected Version version;

		// Token: 0x0400055A RID: 1370
		private readonly string edmxNamespace;

		// Token: 0x0400055B RID: 1371
		private readonly VersioningDictionary<string, string> namespaceAliasMappings;

		// Token: 0x0400055C RID: 1372
		private readonly IEdmModel model;
	}
}
