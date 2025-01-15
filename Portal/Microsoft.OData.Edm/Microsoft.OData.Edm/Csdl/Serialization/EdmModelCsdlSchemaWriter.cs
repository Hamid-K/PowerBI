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
	// Token: 0x0200015C RID: 348
	internal class EdmModelCsdlSchemaWriter
	{
		// Token: 0x060008E4 RID: 2276 RVA: 0x000185C3 File Offset: 0x000167C3
		internal EdmModelCsdlSchemaWriter(IEdmModel model, VersioningDictionary<string, string> namespaceAliasMappings, XmlWriter xmlWriter, Version edmVersion)
		{
			this.xmlWriter = xmlWriter;
			this.version = edmVersion;
			this.edmxNamespace = CsdlConstants.SupportedEdmxVersions[edmVersion];
			this.model = model;
			this.namespaceAliasMappings = namespaceAliasMappings;
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x000185FA File Offset: 0x000167FA
		internal static string PathAsXml(IEnumerable<string> path)
		{
			return EdmUtil.JoinInternal<string>("/", path);
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x00018607 File Offset: 0x00016807
		internal void WriteReferenceElementHeader(IEdmReference reference)
		{
			this.xmlWriter.WriteStartElement("edmx", "Reference", this.edmxNamespace);
			this.WriteRequiredAttribute<Uri>("Uri", reference.Uri, new Func<Uri, string>(EdmValueWriter.UriAsXml));
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x00018644 File Offset: 0x00016844
		internal void WriteIncludeElement(IEdmInclude include)
		{
			this.xmlWriter.WriteStartElement("edmx", "Include", this.edmxNamespace);
			this.WriteRequiredAttribute<string>("Namespace", include.Namespace, new Func<string, string>(EdmValueWriter.StringAsXml));
			this.WriteRequiredAttribute<string>("Alias", include.Alias, new Func<string, string>(EdmValueWriter.StringAsXml));
			this.xmlWriter.WriteEndElement();
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x000186B4 File Offset: 0x000168B4
		internal void WriteIncludeAnnotationsElement(IEdmIncludeAnnotations includeAnnotations)
		{
			this.xmlWriter.WriteStartElement("edmx", "IncludeAnnotations", this.edmxNamespace);
			this.WriteRequiredAttribute<string>("TermNamespace", includeAnnotations.TermNamespace, new Func<string, string>(EdmValueWriter.StringAsXml));
			this.WriteOptionalAttribute<string>("Qualifier", includeAnnotations.Qualifier, new Func<string, string>(EdmValueWriter.StringAsXml));
			this.WriteOptionalAttribute<string>("TargetNamespace", includeAnnotations.TargetNamespace, new Func<string, string>(EdmValueWriter.StringAsXml));
			this.xmlWriter.WriteEndElement();
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x00018740 File Offset: 0x00016940
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

		// Token: 0x060008EA RID: 2282 RVA: 0x000187DC File Offset: 0x000169DC
		internal void WriteComplexTypeElementHeader(IEdmComplexType complexType)
		{
			this.xmlWriter.WriteStartElement("ComplexType");
			this.WriteRequiredAttribute<string>("Name", complexType.Name, new Func<string, string>(EdmValueWriter.StringAsXml));
			this.WriteOptionalAttribute<IEdmComplexType>("BaseType", complexType.BaseComplexType(), new Func<IEdmComplexType, string>(this.TypeDefinitionAsXml));
			this.WriteOptionalAttribute<bool>("Abstract", complexType.IsAbstract, false, new Func<bool, string>(EdmValueWriter.BooleanAsXml));
			this.WriteOptionalAttribute<bool>("OpenType", complexType.IsOpen, false, new Func<bool, string>(EdmValueWriter.BooleanAsXml));
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x00018870 File Offset: 0x00016A70
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

		// Token: 0x060008EC RID: 2284 RVA: 0x000188F4 File Offset: 0x00016AF4
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

		// Token: 0x060008ED RID: 2285 RVA: 0x00018968 File Offset: 0x00016B68
		internal void WriteEntitySetElementHeader(IEdmEntitySet entitySet)
		{
			this.xmlWriter.WriteStartElement("EntitySet");
			this.WriteRequiredAttribute<string>("Name", entitySet.Name, new Func<string, string>(EdmValueWriter.StringAsXml));
			this.WriteRequiredAttribute<string>("EntityType", entitySet.EntityType().FullName(), new Func<string, string>(EdmValueWriter.StringAsXml));
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x000189C4 File Offset: 0x00016BC4
		internal void WriteSingletonElementHeader(IEdmSingleton singleton)
		{
			this.xmlWriter.WriteStartElement("Singleton");
			this.WriteRequiredAttribute<string>("Name", singleton.Name, new Func<string, string>(EdmValueWriter.StringAsXml));
			this.WriteRequiredAttribute<string>("Type", singleton.EntityType().FullName(), new Func<string, string>(EdmValueWriter.StringAsXml));
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x00018A20 File Offset: 0x00016C20
		internal void WriteNavigationPropertyBinding(IEdmNavigationSource navigationSource, IEdmNavigationPropertyBinding binding)
		{
			this.WriteNavigationPropertyBinding(binding);
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x00018A2C File Offset: 0x00016C2C
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

		// Token: 0x060008F1 RID: 2289 RVA: 0x00018B08 File Offset: 0x00016D08
		internal void WriteDelaredKeyPropertiesElementHeader()
		{
			this.xmlWriter.WriteStartElement("Key");
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x00018B1A File Offset: 0x00016D1A
		internal void WritePropertyRefElement(IEdmStructuralProperty property)
		{
			this.xmlWriter.WriteStartElement("PropertyRef");
			this.WriteRequiredAttribute<string>("Name", property.Name, new Func<string, string>(EdmValueWriter.StringAsXml));
			this.WriteEndElement();
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x00018B50 File Offset: 0x00016D50
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

		// Token: 0x060008F4 RID: 2292 RVA: 0x00018C2B File Offset: 0x00016E2B
		internal void WriteOperationActionElement(string elementName, EdmOnDeleteAction operationAction)
		{
			this.xmlWriter.WriteStartElement(elementName);
			this.WriteRequiredAttribute<string>("Action", operationAction.ToString(), new Func<string, string>(EdmValueWriter.StringAsXml));
			this.WriteEndElement();
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x00018C64 File Offset: 0x00016E64
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

		// Token: 0x060008F6 RID: 2294 RVA: 0x00018D20 File Offset: 0x00016F20
		internal void WriteAnnotationsElementHeader(string annotationsTarget)
		{
			this.xmlWriter.WriteStartElement("Annotations");
			this.WriteRequiredAttribute<string>("Target", annotationsTarget, new Func<string, string>(EdmValueWriter.StringAsXml));
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x00018D4C File Offset: 0x00016F4C
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

		// Token: 0x060008F8 RID: 2296 RVA: 0x00018DC4 File Offset: 0x00016FC4
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

		// Token: 0x060008F9 RID: 2297 RVA: 0x00018E3D File Offset: 0x0001703D
		internal void WriteNullableAttribute(IEdmTypeReference reference)
		{
			this.WriteOptionalAttribute<bool>("Nullable", reference.IsNullable, true, new Func<bool, string>(EdmValueWriter.BooleanAsXml));
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x00018E60 File Offset: 0x00017060
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

		// Token: 0x060008FB RID: 2299 RVA: 0x00018EDC File Offset: 0x000170DC
		internal void WriteBinaryTypeAttributes(IEdmBinaryTypeReference reference)
		{
			if (reference.IsUnbounded)
			{
				this.WriteRequiredAttribute<string>("MaxLength", "max", new Func<string, string>(EdmValueWriter.StringAsXml));
				return;
			}
			this.WriteOptionalAttribute<int?>("MaxLength", reference.MaxLength, new Func<int?, string>(EdmValueWriter.IntAsXml));
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x00018F2C File Offset: 0x0001712C
		internal void WriteDecimalTypeAttributes(IEdmDecimalTypeReference reference)
		{
			this.WriteOptionalAttribute<int?>("Precision", reference.Precision, new Func<int?, string>(EdmValueWriter.IntAsXml));
			this.WriteOptionalAttribute<int?>("Scale", reference.Scale, new int?(0), new Func<int?, string>(EdmModelCsdlSchemaWriter.ScaleAsXml));
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x00018F7C File Offset: 0x0001717C
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

		// Token: 0x060008FE RID: 2302 RVA: 0x00018FE4 File Offset: 0x000171E4
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

		// Token: 0x060008FF RID: 2303 RVA: 0x00019068 File Offset: 0x00017268
		internal void WriteTemporalTypeAttributes(IEdmTemporalTypeReference reference)
		{
			if (reference.Precision != null)
			{
				this.WriteOptionalAttribute<int?>("Precision", reference.Precision, new int?(0), new Func<int?, string>(EdmValueWriter.IntAsXml));
			}
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x000190A8 File Offset: 0x000172A8
		internal void WriteReferentialConstraintElementHeader(IEdmNavigationProperty constraint)
		{
			this.xmlWriter.WriteStartElement("ReferentialConstraint");
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x000190BC File Offset: 0x000172BC
		internal void WriteReferentialConstraintPair(EdmReferentialConstraintPropertyPair pair)
		{
			this.xmlWriter.WriteStartElement("ReferentialConstraint");
			this.WriteRequiredAttribute<string>("Property", pair.DependentProperty.Name, new Func<string, string>(EdmValueWriter.StringAsXml));
			this.WriteRequiredAttribute<string>("ReferencedProperty", pair.PrincipalProperty.Name, new Func<string, string>(EdmValueWriter.StringAsXml));
			this.WriteEndElement();
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x00019124 File Offset: 0x00017324
		internal void WriteAnnotationStringAttribute(IEdmDirectValueAnnotation annotation)
		{
			IEdmPrimitiveValue edmPrimitiveValue = (IEdmPrimitiveValue)annotation.Value;
			if (edmPrimitiveValue != null)
			{
				this.xmlWriter.WriteAttributeString(annotation.Name, annotation.NamespaceUri, EdmValueWriter.PrimitiveValueAsXml(edmPrimitiveValue));
			}
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x00019160 File Offset: 0x00017360
		internal void WriteAnnotationStringElement(IEdmDirectValueAnnotation annotation)
		{
			IEdmPrimitiveValue edmPrimitiveValue = (IEdmPrimitiveValue)annotation.Value;
			if (edmPrimitiveValue != null)
			{
				this.xmlWriter.WriteRaw(((IEdmStringValue)edmPrimitiveValue).Value);
			}
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x00019192 File Offset: 0x00017392
		internal void WriteActionElementHeader(IEdmAction action)
		{
			this.xmlWriter.WriteStartElement("Action");
			this.WriteOperationElementAttributes(action);
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x000191AB File Offset: 0x000173AB
		internal void WriteFunctionElementHeader(IEdmFunction function)
		{
			this.xmlWriter.WriteStartElement("Function");
			this.WriteOperationElementAttributes(function);
			if (function.IsComposable)
			{
				this.WriteOptionalAttribute<bool>("IsComposable", function.IsComposable, new Func<bool, string>(EdmValueWriter.BooleanAsXml));
			}
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x000191E9 File Offset: 0x000173E9
		internal void WriteReturnTypeElementHeader()
		{
			this.xmlWriter.WriteStartElement("ReturnType");
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x000191FB File Offset: 0x000173FB
		internal void WriteTypeAttribute(IEdmTypeReference typeReference)
		{
			this.WriteRequiredAttribute<IEdmTypeReference>("Type", typeReference, new Func<IEdmTypeReference, string>(this.TypeReferenceAsXml));
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x00019215 File Offset: 0x00017415
		internal void WriteActionImportElementHeader(IEdmActionImport actionImport)
		{
			this.xmlWriter.WriteStartElement("ActionImport");
			this.WriteOperationImportAttributes(actionImport, "Action");
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x00019233 File Offset: 0x00017433
		internal void WriteFunctionImportElementHeader(IEdmFunctionImport functionImport)
		{
			this.xmlWriter.WriteStartElement("FunctionImport");
			this.WriteOperationImportAttributes(functionImport, "Function");
			this.WriteOptionalAttribute<bool>("IncludeInServiceDocument", functionImport.IncludeInServiceDocument, false, new Func<bool, string>(EdmValueWriter.BooleanAsXml));
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x00019270 File Offset: 0x00017470
		internal void WriteOperationParameterElementHeader(IEdmOperationParameter parameter, bool inlineType)
		{
			this.xmlWriter.WriteStartElement("Parameter");
			this.WriteRequiredAttribute<string>("Name", parameter.Name, new Func<string, string>(EdmValueWriter.StringAsXml));
			if (inlineType)
			{
				this.WriteRequiredAttribute<IEdmTypeReference>("Type", parameter.Type, new Func<IEdmTypeReference, string>(this.TypeReferenceAsXml));
			}
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x000192CC File Offset: 0x000174CC
		internal void WriteOperationParameterEndElement(IEdmOperationParameter parameter)
		{
			IEdmOptionalParameter edmOptionalParameter = parameter as IEdmOptionalParameter;
			if (edmOptionalParameter != null)
			{
				if (!edmOptionalParameter.VocabularyAnnotations(this.model).Any((IEdmVocabularyAnnotation a) => a.Term == CoreVocabularyModel.OptionalParameterTerm))
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

		// Token: 0x0600090C RID: 2316 RVA: 0x0001937E File Offset: 0x0001757E
		internal void WriteCollectionTypeElementHeader(IEdmCollectionType collectionType, bool inlineType)
		{
			this.xmlWriter.WriteStartElement("CollectionType");
			if (inlineType)
			{
				this.WriteRequiredAttribute<IEdmTypeReference>("ElementType", collectionType.ElementType, new Func<IEdmTypeReference, string>(this.TypeReferenceAsXml));
			}
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x000193B0 File Offset: 0x000175B0
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

		// Token: 0x0600090E RID: 2318 RVA: 0x00019614 File Offset: 0x00017814
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

		// Token: 0x0600090F RID: 2319 RVA: 0x0001967A File Offset: 0x0001787A
		internal void WritePropertyValueElementHeader(IEdmPropertyConstructor value, bool isInline)
		{
			this.xmlWriter.WriteStartElement("PropertyValue");
			this.WriteRequiredAttribute<string>("Property", value.Name, new Func<string, string>(EdmValueWriter.StringAsXml));
			if (isInline)
			{
				this.WriteInlineExpression(value.Value);
			}
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x000196B8 File Offset: 0x000178B8
		internal void WriteRecordExpressionElementHeader(IEdmRecordExpression expression)
		{
			this.xmlWriter.WriteStartElement("Record");
			this.WriteOptionalAttribute<IEdmStructuredTypeReference>("Type", expression.DeclaredType, new Func<IEdmStructuredTypeReference, string>(this.TypeReferenceAsXml));
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x0001967A File Offset: 0x0001787A
		internal void WritePropertyConstructorElementHeader(IEdmPropertyConstructor constructor, bool isInline)
		{
			this.xmlWriter.WriteStartElement("PropertyValue");
			this.WriteRequiredAttribute<string>("Property", constructor.Name, new Func<string, string>(EdmValueWriter.StringAsXml));
			if (isInline)
			{
				this.WriteInlineExpression(constructor.Value);
			}
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x000196E7 File Offset: 0x000178E7
		internal void WriteStringConstantExpressionElement(IEdmStringConstantExpression expression)
		{
			this.xmlWriter.WriteStartElement("String");
			this.xmlWriter.WriteString(EdmValueWriter.StringAsXml(expression.Value));
			this.WriteEndElement();
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x00019715 File Offset: 0x00017915
		internal void WriteBinaryConstantExpressionElement(IEdmBinaryConstantExpression expression)
		{
			this.xmlWriter.WriteStartElement("Binary");
			this.xmlWriter.WriteString(EdmValueWriter.BinaryAsXml(expression.Value));
			this.WriteEndElement();
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x00019743 File Offset: 0x00017943
		internal void WriteBooleanConstantExpressionElement(IEdmBooleanConstantExpression expression)
		{
			this.xmlWriter.WriteStartElement("Bool");
			this.xmlWriter.WriteString(EdmValueWriter.BooleanAsXml(expression.Value));
			this.WriteEndElement();
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x00019771 File Offset: 0x00017971
		internal void WriteNullConstantExpressionElement(IEdmNullExpression expression)
		{
			this.xmlWriter.WriteStartElement("Null");
			this.WriteEndElement();
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x00019789 File Offset: 0x00017989
		internal void WriteDateConstantExpressionElement(IEdmDateConstantExpression expression)
		{
			this.xmlWriter.WriteStartElement("Date");
			this.xmlWriter.WriteString(EdmValueWriter.DateAsXml(expression.Value));
			this.WriteEndElement();
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x000197B7 File Offset: 0x000179B7
		internal void WriteDateTimeOffsetConstantExpressionElement(IEdmDateTimeOffsetConstantExpression expression)
		{
			this.xmlWriter.WriteStartElement("DateTimeOffset");
			this.xmlWriter.WriteString(EdmValueWriter.DateTimeOffsetAsXml(expression.Value));
			this.WriteEndElement();
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x000197E5 File Offset: 0x000179E5
		internal void WriteDurationConstantExpressionElement(IEdmDurationConstantExpression expression)
		{
			this.xmlWriter.WriteStartElement("Duration");
			this.xmlWriter.WriteString(EdmValueWriter.DurationAsXml(expression.Value));
			this.WriteEndElement();
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x00019813 File Offset: 0x00017A13
		internal void WriteDecimalConstantExpressionElement(IEdmDecimalConstantExpression expression)
		{
			this.xmlWriter.WriteStartElement("Decimal");
			this.xmlWriter.WriteString(EdmValueWriter.DecimalAsXml(expression.Value));
			this.WriteEndElement();
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x00019841 File Offset: 0x00017A41
		internal void WriteFloatingConstantExpressionElement(IEdmFloatingConstantExpression expression)
		{
			this.xmlWriter.WriteStartElement("Float");
			this.xmlWriter.WriteString(EdmValueWriter.FloatAsXml(expression.Value));
			this.WriteEndElement();
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x0001986F File Offset: 0x00017A6F
		internal void WriteFunctionApplicationElementHeader(IEdmApplyExpression expression)
		{
			this.xmlWriter.WriteStartElement("Apply");
			this.WriteRequiredAttribute<IEdmFunction>("Function", expression.AppliedFunction, new Func<IEdmFunction, string>(this.FunctionAsXml));
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x0001989E File Offset: 0x00017A9E
		internal void WriteGuidConstantExpressionElement(IEdmGuidConstantExpression expression)
		{
			this.xmlWriter.WriteStartElement("Guid");
			this.xmlWriter.WriteString(EdmValueWriter.GuidAsXml(expression.Value));
			this.WriteEndElement();
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x000198CC File Offset: 0x00017ACC
		internal void WriteIntegerConstantExpressionElement(IEdmIntegerConstantExpression expression)
		{
			this.xmlWriter.WriteStartElement("Int");
			this.xmlWriter.WriteString(EdmValueWriter.LongAsXml(expression.Value));
			this.WriteEndElement();
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x000198FA File Offset: 0x00017AFA
		internal void WritePathExpressionElement(IEdmPathExpression expression)
		{
			this.xmlWriter.WriteStartElement("Path");
			this.xmlWriter.WriteString(EdmModelCsdlSchemaWriter.PathAsXml(expression.PathSegments));
			this.WriteEndElement();
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x00019928 File Offset: 0x00017B28
		internal void WritePropertyPathExpressionElement(IEdmPathExpression expression)
		{
			this.xmlWriter.WriteStartElement("PropertyPath");
			this.xmlWriter.WriteString(EdmModelCsdlSchemaWriter.PathAsXml(expression.PathSegments));
			this.WriteEndElement();
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x00019956 File Offset: 0x00017B56
		internal void WriteNavigationPropertyPathExpressionElement(IEdmPathExpression expression)
		{
			this.xmlWriter.WriteStartElement("NavigationPropertyPath");
			this.xmlWriter.WriteString(EdmModelCsdlSchemaWriter.PathAsXml(expression.PathSegments));
			this.WriteEndElement();
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x00019984 File Offset: 0x00017B84
		internal void WriteIfExpressionElementHeader(IEdmIfExpression expression)
		{
			this.xmlWriter.WriteStartElement("If");
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x00019996 File Offset: 0x00017B96
		internal void WriteCollectionExpressionElementHeader(IEdmCollectionExpression expression)
		{
			this.xmlWriter.WriteStartElement("Collection");
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x000199A8 File Offset: 0x00017BA8
		internal void WriteLabeledElementHeader(IEdmLabeledExpression labeledElement)
		{
			this.xmlWriter.WriteStartElement("LabeledElement");
			this.WriteRequiredAttribute<string>("Name", labeledElement.Name, new Func<string, string>(EdmValueWriter.StringAsXml));
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x000199D7 File Offset: 0x00017BD7
		internal void WriteTimeOfDayConstantExpressionElement(IEdmTimeOfDayConstantExpression expression)
		{
			this.xmlWriter.WriteStartElement("TimeOfDay");
			this.xmlWriter.WriteString(EdmValueWriter.TimeOfDayAsXml(expression.Value));
			this.WriteEndElement();
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x00019A05 File Offset: 0x00017C05
		internal void WriteIsTypeExpressionElementHeader(IEdmIsTypeExpression expression, bool inlineType)
		{
			this.xmlWriter.WriteStartElement("IsType");
			if (inlineType)
			{
				this.WriteRequiredAttribute<IEdmTypeReference>("Type", expression.Type, new Func<IEdmTypeReference, string>(this.TypeReferenceAsXml));
			}
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x00019A37 File Offset: 0x00017C37
		internal void WriteCastExpressionElementHeader(IEdmCastExpression expression, bool inlineType)
		{
			this.xmlWriter.WriteStartElement("Cast");
			if (inlineType)
			{
				this.WriteRequiredAttribute<IEdmTypeReference>("Type", expression.Type, new Func<IEdmTypeReference, string>(this.TypeReferenceAsXml));
			}
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x00019A69 File Offset: 0x00017C69
		internal void WriteEnumMemberExpressionElement(IEdmEnumMemberExpression expression)
		{
			this.xmlWriter.WriteStartElement("EnumMember");
			this.xmlWriter.WriteString(EdmModelCsdlSchemaWriter.EnumMemberAsXml(expression.EnumMembers));
			this.WriteEndElement();
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x00019A98 File Offset: 0x00017C98
		internal void WriteTypeDefinitionElementHeader(IEdmTypeDefinition typeDefinition)
		{
			this.xmlWriter.WriteStartElement("TypeDefinition");
			this.WriteRequiredAttribute<string>("Name", typeDefinition.Name, new Func<string, string>(EdmValueWriter.StringAsXml));
			this.WriteRequiredAttribute<IEdmPrimitiveType>("UnderlyingType", typeDefinition.UnderlyingType, new Func<IEdmPrimitiveType, string>(this.TypeDefinitionAsXml));
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x00019AEF File Offset: 0x00017CEF
		internal void WriteEndElement()
		{
			this.xmlWriter.WriteEndElement();
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x00019AFC File Offset: 0x00017CFC
		internal void WriteOptionalAttribute<T>(string attribute, T value, T defaultValue, Func<T, string> toXml)
		{
			if (!value.Equals(defaultValue))
			{
				this.xmlWriter.WriteAttributeString(attribute, toXml(value));
			}
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x00019B27 File Offset: 0x00017D27
		internal void WriteOptionalAttribute<T>(string attribute, T value, Func<T, string> toXml)
		{
			if (value != null)
			{
				this.xmlWriter.WriteAttributeString(attribute, toXml(value));
			}
		}

		// Token: 0x0600092C RID: 2348 RVA: 0x00019B44 File Offset: 0x00017D44
		internal void WriteRequiredAttribute<T>(string attribute, T value, Func<T, string> toXml)
		{
			this.xmlWriter.WriteAttributeString(attribute, toXml(value));
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x00019B5C File Offset: 0x00017D5C
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

		// Token: 0x0600092E RID: 2350 RVA: 0x00019BD8 File Offset: 0x00017DD8
		private void WriteNavigationPropertyBinding(IEdmNavigationPropertyBinding binding)
		{
			this.xmlWriter.WriteStartElement("NavigationPropertyBinding");
			this.WriteRequiredAttribute<string>("Path", binding.Path.Path, new Func<string, string>(EdmValueWriter.StringAsXml));
			IEdmContainedEntitySet edmContainedEntitySet = binding.Target as IEdmContainedEntitySet;
			if (edmContainedEntitySet != null)
			{
				this.WriteRequiredAttribute<string>("Target", edmContainedEntitySet.Path.Path, new Func<string, string>(EdmValueWriter.StringAsXml));
			}
			else
			{
				this.WriteRequiredAttribute<string>("Target", binding.Target.Name, new Func<string, string>(EdmValueWriter.StringAsXml));
			}
			this.xmlWriter.WriteEndElement();
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x00019C78 File Offset: 0x00017E78
		private static string EnumMemberAsXml(IEnumerable<IEdmEnumMember> members)
		{
			string text = members.First<IEdmEnumMember>().DeclaringType.FullName();
			List<string> list = new List<string>();
			foreach (IEdmEnumMember edmEnumMember in members)
			{
				list.Add(text + "/" + edmEnumMember.Name);
			}
			return string.Join(" ", list.ToArray());
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x00019CF8 File Offset: 0x00017EF8
		private static string SridAsXml(int? i)
		{
			if (i == null)
			{
				return "Variable";
			}
			return Convert.ToString(i.Value, CultureInfo.InvariantCulture);
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x00019CF8 File Offset: 0x00017EF8
		private static string ScaleAsXml(int? i)
		{
			if (i == null)
			{
				return "Variable";
			}
			return Convert.ToString(i.Value, CultureInfo.InvariantCulture);
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x00019D1C File Offset: 0x00017F1C
		private static string GetCsdlNamespace(Version edmVersion)
		{
			string[] array;
			if (CsdlConstants.SupportedVersions.TryGetValue(edmVersion, out array))
			{
				return array[0];
			}
			throw new InvalidOperationException(Strings.Serializer_UnknownEdmVersion);
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x00019D48 File Offset: 0x00017F48
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

		// Token: 0x06000934 RID: 2356 RVA: 0x00019DD8 File Offset: 0x00017FD8
		private string SerializationName(IEdmSchemaElement element)
		{
			string text;
			if (this.namespaceAliasMappings != null && this.namespaceAliasMappings.TryGetValue(element.Namespace, out text))
			{
				return text + "." + element.Name;
			}
			return element.FullName();
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x00019E1C File Offset: 0x0001801C
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

		// Token: 0x06000936 RID: 2358 RVA: 0x00019E9D File Offset: 0x0001809D
		private string TypeDefinitionAsXml(IEdmSchemaType type)
		{
			return this.SerializationName(type);
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x00019E9D File Offset: 0x0001809D
		private string FunctionAsXml(IEdmOperation operation)
		{
			return this.SerializationName(operation);
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x00019EA6 File Offset: 0x000180A6
		private string TermAsXml(IEdmTerm term)
		{
			if (term == null)
			{
				return string.Empty;
			}
			return this.SerializationName(term);
		}

		// Token: 0x040005C2 RID: 1474
		protected XmlWriter xmlWriter;

		// Token: 0x040005C3 RID: 1475
		protected Version version;

		// Token: 0x040005C4 RID: 1476
		private readonly string edmxNamespace;

		// Token: 0x040005C5 RID: 1477
		private readonly VersioningDictionary<string, string> namespaceAliasMappings;

		// Token: 0x040005C6 RID: 1478
		private readonly IEdmModel model;
	}
}
