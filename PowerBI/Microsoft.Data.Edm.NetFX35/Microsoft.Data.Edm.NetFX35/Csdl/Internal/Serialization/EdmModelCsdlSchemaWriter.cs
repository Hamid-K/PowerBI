using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using Microsoft.Data.Edm.Annotations;
using Microsoft.Data.Edm.Expressions;
using Microsoft.Data.Edm.Internal;
using Microsoft.Data.Edm.Values;

namespace Microsoft.Data.Edm.Csdl.Internal.Serialization
{
	// Token: 0x020001B0 RID: 432
	internal class EdmModelCsdlSchemaWriter
	{
		// Token: 0x06000988 RID: 2440 RVA: 0x0001A139 File Offset: 0x00018339
		internal EdmModelCsdlSchemaWriter(IEdmModel model, VersioningDictionary<string, string> namespaceAliasMappings, XmlWriter xmlWriter, Version edmVersion)
		{
			this.xmlWriter = xmlWriter;
			this.version = edmVersion;
			this.model = model;
			this.namespaceAliasMappings = namespaceAliasMappings;
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x0001A160 File Offset: 0x00018360
		internal void WriteValueTermElementHeader(IEdmValueTerm term, bool inlineType)
		{
			this.xmlWriter.WriteStartElement("ValueTerm");
			this.WriteRequiredAttribute<string>("Name", term.Name, new Func<string, string>(EdmValueWriter.StringAsXml));
			if (inlineType && term.Type != null)
			{
				this.WriteRequiredAttribute<IEdmTypeReference>("Type", term.Type, new Func<IEdmTypeReference, string>(this.TypeReferenceAsXml));
			}
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x0001A1C2 File Offset: 0x000183C2
		internal void WriteAssociationElementHeader(IEdmNavigationProperty navigationProperty)
		{
			this.xmlWriter.WriteStartElement("Association");
			this.WriteRequiredAttribute<string>("Name", this.model.GetAssociationName(navigationProperty), new Func<string, string>(EdmValueWriter.StringAsXml));
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x0001A1F8 File Offset: 0x000183F8
		internal void WriteAssociationSetElementHeader(IEdmEntitySet entitySet, IEdmNavigationProperty navigationProperty)
		{
			this.xmlWriter.WriteStartElement("AssociationSet");
			this.WriteRequiredAttribute<string>("Name", this.model.GetAssociationSetName(entitySet, navigationProperty), new Func<string, string>(EdmValueWriter.StringAsXml));
			this.WriteRequiredAttribute<string>("Association", this.model.GetAssociationFullName(navigationProperty), new Func<string, string>(EdmValueWriter.StringAsXml));
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x0001A25C File Offset: 0x0001845C
		internal void WriteComplexTypeElementHeader(IEdmComplexType complexType)
		{
			this.xmlWriter.WriteStartElement("ComplexType");
			this.WriteRequiredAttribute<string>("Name", complexType.Name, new Func<string, string>(EdmValueWriter.StringAsXml));
			this.WriteOptionalAttribute<IEdmComplexType>("BaseType", complexType.BaseComplexType(), new Func<IEdmComplexType, string>(this.TypeDefinitionAsXml));
			this.WriteOptionalAttribute<bool>("Abstract", complexType.IsAbstract, false, new Func<bool, string>(EdmValueWriter.BooleanAsXml));
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x0001A2D4 File Offset: 0x000184D4
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

		// Token: 0x0600098E RID: 2446 RVA: 0x0001A358 File Offset: 0x00018558
		internal void WriteDocumentationElement(IEdmDocumentation documentation)
		{
			this.xmlWriter.WriteStartElement("Documentation");
			if (documentation.Summary != null)
			{
				this.xmlWriter.WriteStartElement("Summary");
				this.xmlWriter.WriteString(documentation.Summary);
				this.WriteEndElement();
			}
			if (documentation.Description != null)
			{
				this.xmlWriter.WriteStartElement("LongDescription");
				this.xmlWriter.WriteString(documentation.Description);
				this.WriteEndElement();
			}
			this.WriteEndElement();
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x0001A3DC File Offset: 0x000185DC
		internal void WriteAssociationSetEndElementHeader(IEdmEntitySet entitySet, IEdmNavigationProperty property)
		{
			this.xmlWriter.WriteStartElement("End");
			this.WriteRequiredAttribute<string>("Role", this.model.GetAssociationEndName(property), new Func<string, string>(EdmValueWriter.StringAsXml));
			this.WriteRequiredAttribute<string>("EntitySet", entitySet.Name, new Func<string, string>(EdmValueWriter.StringAsXml));
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x0001A43C File Offset: 0x0001863C
		internal void WriteAssociationEndElementHeader(IEdmNavigationProperty associationEnd)
		{
			this.xmlWriter.WriteStartElement("End");
			this.WriteRequiredAttribute<string>("Type", ((IEdmEntityType)associationEnd.DeclaringType).FullName(), new Func<string, string>(EdmValueWriter.StringAsXml));
			this.WriteRequiredAttribute<string>("Role", this.model.GetAssociationEndName(associationEnd), new Func<string, string>(EdmValueWriter.StringAsXml));
			this.WriteRequiredAttribute<EdmMultiplicity>("Multiplicity", associationEnd.Multiplicity(), new Func<EdmMultiplicity, string>(EdmModelCsdlSchemaWriter.MultiplicityAsXml));
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x0001A4C0 File Offset: 0x000186C0
		internal void WriteEntityContainerElementHeader(IEdmEntityContainer container)
		{
			this.xmlWriter.WriteStartElement("EntityContainer");
			this.WriteRequiredAttribute<string>("Name", container.Name, new Func<string, string>(EdmValueWriter.StringAsXml));
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x0001A4F0 File Offset: 0x000186F0
		internal void WriteEntitySetElementHeader(IEdmEntitySet entitySet)
		{
			this.xmlWriter.WriteStartElement("EntitySet");
			this.WriteRequiredAttribute<string>("Name", entitySet.Name, new Func<string, string>(EdmValueWriter.StringAsXml));
			this.WriteRequiredAttribute<string>("EntityType", entitySet.ElementType.FullName(), new Func<string, string>(EdmValueWriter.StringAsXml));
		}

		// Token: 0x06000993 RID: 2451 RVA: 0x0001A54C File Offset: 0x0001874C
		internal void WriteEntityTypeElementHeader(IEdmEntityType entityType)
		{
			this.xmlWriter.WriteStartElement("EntityType");
			this.WriteRequiredAttribute<string>("Name", entityType.Name, new Func<string, string>(EdmValueWriter.StringAsXml));
			this.WriteOptionalAttribute<IEdmEntityType>("BaseType", entityType.BaseEntityType(), new Func<IEdmEntityType, string>(this.TypeDefinitionAsXml));
			this.WriteOptionalAttribute<bool>("Abstract", entityType.IsAbstract, false, new Func<bool, string>(EdmValueWriter.BooleanAsXml));
			this.WriteOptionalAttribute<bool>("OpenType", entityType.IsOpen, false, new Func<bool, string>(EdmValueWriter.BooleanAsXml));
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x0001A5DF File Offset: 0x000187DF
		internal void WriteDelaredKeyPropertiesElementHeader()
		{
			this.xmlWriter.WriteStartElement("Key");
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x0001A5F1 File Offset: 0x000187F1
		internal void WritePropertyRefElement(IEdmStructuralProperty property)
		{
			this.xmlWriter.WriteStartElement("PropertyRef");
			this.WriteRequiredAttribute<string>("Name", property.Name, new Func<string, string>(EdmValueWriter.StringAsXml));
			this.WriteEndElement();
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x0001A628 File Offset: 0x00018828
		internal void WriteNavigationPropertyElementHeader(IEdmNavigationProperty member)
		{
			this.xmlWriter.WriteStartElement("NavigationProperty");
			this.WriteRequiredAttribute<string>("Name", member.Name, new Func<string, string>(EdmValueWriter.StringAsXml));
			this.WriteRequiredAttribute<string>("Relationship", this.model.GetAssociationFullName(member), new Func<string, string>(EdmValueWriter.StringAsXml));
			this.WriteRequiredAttribute<string>("ToRole", this.model.GetAssociationEndName(member.Partner), new Func<string, string>(EdmValueWriter.StringAsXml));
			this.WriteRequiredAttribute<string>("FromRole", this.model.GetAssociationEndName(member), new Func<string, string>(EdmValueWriter.StringAsXml));
			this.WriteOptionalAttribute<bool>("ContainsTarget", member.ContainsTarget, false, new Func<bool, string>(EdmValueWriter.BooleanAsXml));
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x0001A6EE File Offset: 0x000188EE
		internal void WriteOperationActionElement(string elementName, EdmOnDeleteAction operationAction)
		{
			this.xmlWriter.WriteStartElement(elementName);
			this.WriteRequiredAttribute<string>("Action", operationAction.ToString(), new Func<string, string>(EdmValueWriter.StringAsXml));
			this.WriteEndElement();
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x0001A724 File Offset: 0x00018924
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

		// Token: 0x06000999 RID: 2457 RVA: 0x0001A7E0 File Offset: 0x000189E0
		internal void WriteAnnotationsElementHeader(string annotationsTarget)
		{
			this.xmlWriter.WriteStartElement("Annotations");
			this.WriteRequiredAttribute<string>("Target", annotationsTarget, new Func<string, string>(EdmValueWriter.StringAsXml));
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x0001A80C File Offset: 0x00018A0C
		internal void WriteStructuralPropertyElementHeader(IEdmStructuralProperty property, bool inlineType)
		{
			this.xmlWriter.WriteStartElement("Property");
			this.WriteRequiredAttribute<string>("Name", property.Name, new Func<string, string>(EdmValueWriter.StringAsXml));
			if (inlineType)
			{
				this.WriteRequiredAttribute<IEdmTypeReference>("Type", property.Type, new Func<IEdmTypeReference, string>(this.TypeReferenceAsXml));
			}
			this.WriteOptionalAttribute<EdmConcurrencyMode>("ConcurrencyMode", property.ConcurrencyMode, EdmConcurrencyMode.None, new Func<EdmConcurrencyMode, string>(EdmModelCsdlSchemaWriter.ConcurrencyModeAsXml));
			this.WriteOptionalAttribute<string>("DefaultValue", property.DefaultValueString, new Func<string, string>(EdmValueWriter.StringAsXml));
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x0001A8A4 File Offset: 0x00018AA4
		internal void WriteEnumMemberElementHeader(IEdmEnumMember member)
		{
			this.xmlWriter.WriteStartElement("Member");
			this.WriteRequiredAttribute<string>("Name", member.Name, new Func<string, string>(EdmValueWriter.StringAsXml));
			bool? flag = member.IsValueExplicit(this.model);
			if (flag == null || flag.Value)
			{
				this.WriteRequiredAttribute<IEdmPrimitiveValue>("Value", member.Value, new Func<IEdmPrimitiveValue, string>(EdmValueWriter.PrimitiveValueAsXml));
			}
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x0001A91A File Offset: 0x00018B1A
		internal void WriteNullableAttribute(IEdmTypeReference reference)
		{
			this.WriteOptionalAttribute<bool>("Nullable", reference.IsNullable, true, new Func<bool, string>(EdmValueWriter.BooleanAsXml));
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x0001A93C File Offset: 0x00018B3C
		internal void WriteBinaryTypeAttributes(IEdmBinaryTypeReference reference)
		{
			if (reference.IsUnbounded)
			{
				this.WriteRequiredAttribute<string>("MaxLength", "Max", new Func<string, string>(EdmValueWriter.StringAsXml));
			}
			else
			{
				this.WriteOptionalAttribute<int?>("MaxLength", reference.MaxLength, new Func<int?, string>(EdmValueWriter.IntAsXml));
			}
			this.WriteOptionalAttribute<bool?>("FixedLength", reference.IsFixedLength, new Func<bool?, string>(EdmValueWriter.BooleanAsXml));
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x0001A9A9 File Offset: 0x00018BA9
		internal void WriteDecimalTypeAttributes(IEdmDecimalTypeReference reference)
		{
			this.WriteOptionalAttribute<int?>("Precision", reference.Precision, new Func<int?, string>(EdmValueWriter.IntAsXml));
			this.WriteOptionalAttribute<int?>("Scale", reference.Scale, new Func<int?, string>(EdmValueWriter.IntAsXml));
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x0001A9E5 File Offset: 0x00018BE5
		internal void WriteSpatialTypeAttributes(IEdmSpatialTypeReference reference)
		{
			this.WriteRequiredAttribute<int?>("SRID", reference.SpatialReferenceIdentifier, new Func<int?, string>(EdmModelCsdlSchemaWriter.SridAsXml));
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x0001AA04 File Offset: 0x00018C04
		internal void WriteStringTypeAttributes(IEdmStringTypeReference reference)
		{
			this.WriteOptionalAttribute<string>("Collation", reference.Collation, new Func<string, string>(EdmValueWriter.StringAsXml));
			if (reference.IsUnbounded)
			{
				this.WriteRequiredAttribute<string>("MaxLength", "Max", new Func<string, string>(EdmValueWriter.StringAsXml));
			}
			else
			{
				this.WriteOptionalAttribute<int?>("MaxLength", reference.MaxLength, new Func<int?, string>(EdmValueWriter.IntAsXml));
			}
			this.WriteOptionalAttribute<bool?>("FixedLength", reference.IsFixedLength, new Func<bool?, string>(EdmValueWriter.BooleanAsXml));
			this.WriteOptionalAttribute<bool?>("Unicode", reference.IsUnicode, new Func<bool?, string>(EdmValueWriter.BooleanAsXml));
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x0001AAAB File Offset: 0x00018CAB
		internal void WriteTemporalTypeAttributes(IEdmTemporalTypeReference reference)
		{
			this.WriteOptionalAttribute<int?>("Precision", reference.Precision, new Func<int?, string>(EdmValueWriter.IntAsXml));
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x0001AACA File Offset: 0x00018CCA
		internal void WriteReferentialConstraintElementHeader(IEdmNavigationProperty constraint)
		{
			this.xmlWriter.WriteStartElement("ReferentialConstraint");
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x0001AADC File Offset: 0x00018CDC
		internal void WriteReferentialConstraintPrincipalEndElementHeader(IEdmNavigationProperty end)
		{
			this.xmlWriter.WriteStartElement("Principal");
			this.WriteRequiredAttribute<string>("Role", this.model.GetAssociationEndName(end), new Func<string, string>(EdmValueWriter.StringAsXml));
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x0001AB11 File Offset: 0x00018D11
		internal void WriteReferentialConstraintDependentEndElementHeader(IEdmNavigationProperty end)
		{
			this.xmlWriter.WriteStartElement("Dependent");
			this.WriteRequiredAttribute<string>("Role", this.model.GetAssociationEndName(end), new Func<string, string>(EdmValueWriter.StringAsXml));
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x0001AB48 File Offset: 0x00018D48
		internal void WriteNamespaceUsingElement(string usingNamespace, string alias)
		{
			this.xmlWriter.WriteStartElement("Using");
			this.WriteRequiredAttribute<string>("Namespace", usingNamespace, new Func<string, string>(EdmValueWriter.StringAsXml));
			this.WriteRequiredAttribute<string>("Alias", alias, new Func<string, string>(EdmValueWriter.StringAsXml));
			this.WriteEndElement();
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x0001AB9C File Offset: 0x00018D9C
		internal void WriteAnnotationStringAttribute(IEdmDirectValueAnnotation annotation)
		{
			IEdmPrimitiveValue edmPrimitiveValue = (IEdmPrimitiveValue)annotation.Value;
			if (edmPrimitiveValue != null)
			{
				this.xmlWriter.WriteAttributeString(annotation.Name, annotation.NamespaceUri, EdmValueWriter.PrimitiveValueAsXml(edmPrimitiveValue));
			}
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x0001ABD8 File Offset: 0x00018DD8
		internal void WriteAnnotationStringElement(IEdmDirectValueAnnotation annotation)
		{
			IEdmPrimitiveValue edmPrimitiveValue = (IEdmPrimitiveValue)annotation.Value;
			if (edmPrimitiveValue != null)
			{
				this.xmlWriter.WriteRaw(((IEdmStringValue)edmPrimitiveValue).Value);
			}
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x0001AC0C File Offset: 0x00018E0C
		internal void WriteFunctionElementHeader(IEdmFunction function, bool inlineReturnType)
		{
			this.xmlWriter.WriteStartElement("Function");
			this.WriteRequiredAttribute<string>("Name", function.Name, new Func<string, string>(EdmValueWriter.StringAsXml));
			if (inlineReturnType)
			{
				this.WriteRequiredAttribute<IEdmTypeReference>("ReturnType", function.ReturnType, new Func<IEdmTypeReference, string>(this.TypeReferenceAsXml));
			}
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x0001AC66 File Offset: 0x00018E66
		internal void WriteDefiningExpressionElement(string expression)
		{
			this.xmlWriter.WriteStartElement("DefiningExpression");
			this.xmlWriter.WriteString(expression);
			this.xmlWriter.WriteEndElement();
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x0001AC8F File Offset: 0x00018E8F
		internal void WriteReturnTypeElementHeader()
		{
			this.xmlWriter.WriteStartElement("ReturnType");
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x0001ACA4 File Offset: 0x00018EA4
		internal void WriteFunctionImportElementHeader(IEdmFunctionImport functionImport)
		{
			if (functionImport.IsComposable && functionImport.IsSideEffecting)
			{
				throw new InvalidOperationException(Strings.EdmModel_Validator_Semantic_ComposableFunctionImportCannotBeSideEffecting(functionImport.Name));
			}
			this.xmlWriter.WriteStartElement("FunctionImport");
			this.WriteRequiredAttribute<string>("Name", functionImport.Name, new Func<string, string>(EdmValueWriter.StringAsXml));
			this.WriteOptionalAttribute<IEdmTypeReference>("ReturnType", functionImport.ReturnType, new Func<IEdmTypeReference, string>(this.TypeReferenceAsXml));
			if (!functionImport.IsComposable && !functionImport.IsSideEffecting)
			{
				this.WriteRequiredAttribute<bool>("IsSideEffecting", functionImport.IsSideEffecting, new Func<bool, string>(EdmValueWriter.BooleanAsXml));
			}
			this.WriteOptionalAttribute<bool>("IsComposable", functionImport.IsComposable, false, new Func<bool, string>(EdmValueWriter.BooleanAsXml));
			this.WriteOptionalAttribute<bool>("IsBindable", functionImport.IsBindable, false, new Func<bool, string>(EdmValueWriter.BooleanAsXml));
			if (functionImport.EntitySet == null)
			{
				return;
			}
			IEdmEntitySetReferenceExpression edmEntitySetReferenceExpression = functionImport.EntitySet as IEdmEntitySetReferenceExpression;
			if (edmEntitySetReferenceExpression != null)
			{
				this.WriteOptionalAttribute<string>("EntitySet", edmEntitySetReferenceExpression.ReferencedEntitySet.Name, new Func<string, string>(EdmValueWriter.StringAsXml));
				return;
			}
			IEdmPathExpression edmPathExpression = functionImport.EntitySet as IEdmPathExpression;
			if (edmPathExpression != null)
			{
				this.WriteOptionalAttribute<IEnumerable<string>>("EntitySetPath", edmPathExpression.Path, new Func<IEnumerable<string>, string>(EdmModelCsdlSchemaWriter.PathAsXml));
				return;
			}
			throw new InvalidOperationException(Strings.EdmModel_Validator_Semantic_FunctionImportEntitySetExpressionIsInvalid(functionImport.Name));
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x0001AE00 File Offset: 0x00019000
		internal void WriteFunctionParameterElementHeader(IEdmFunctionParameter parameter, bool inlineType)
		{
			this.xmlWriter.WriteStartElement("Parameter");
			this.WriteRequiredAttribute<string>("Name", parameter.Name, new Func<string, string>(EdmValueWriter.StringAsXml));
			if (inlineType)
			{
				this.WriteRequiredAttribute<IEdmTypeReference>("Type", parameter.Type, new Func<IEdmTypeReference, string>(this.TypeReferenceAsXml));
			}
			this.WriteOptionalAttribute<EdmFunctionParameterMode>("Mode", parameter.Mode, EdmFunctionParameterMode.In, new Func<EdmFunctionParameterMode, string>(EdmModelCsdlSchemaWriter.FunctionParameterModeAsXml));
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x0001AE78 File Offset: 0x00019078
		internal void WriteCollectionTypeElementHeader(IEdmCollectionType collectionType, bool inlineType)
		{
			this.xmlWriter.WriteStartElement("CollectionType");
			if (inlineType)
			{
				this.WriteRequiredAttribute<IEdmTypeReference>("ElementType", collectionType.ElementType, new Func<IEdmTypeReference, string>(this.TypeReferenceAsXml));
			}
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x0001AEAA File Offset: 0x000190AA
		internal void WriteRowTypeElementHeader()
		{
			this.xmlWriter.WriteStartElement("RowType");
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x0001AEBC File Offset: 0x000190BC
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
			case EdmExpressionKind.DateTimeConstant:
				this.WriteRequiredAttribute<DateTime>("DateTime", ((IEdmDateTimeConstantExpression)expression).Value, new Func<DateTime, string>(EdmValueWriter.DateTimeAsXml));
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
			case EdmExpressionKind.TimeConstant:
				this.WriteRequiredAttribute<TimeSpan>("Time", ((IEdmTimeConstantExpression)expression).Value, new Func<TimeSpan, string>(EdmValueWriter.TimeAsXml));
				break;
			case EdmExpressionKind.Null:
			case EdmExpressionKind.Record:
			case EdmExpressionKind.Collection:
				break;
			case EdmExpressionKind.Path:
				this.WriteRequiredAttribute<IEnumerable<string>>("Path", ((IEdmPathExpression)expression).Path, new Func<IEnumerable<string>, string>(EdmModelCsdlSchemaWriter.PathAsXml));
				return;
			default:
				return;
			}
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x0001B094 File Offset: 0x00019294
		internal void WriteValueAnnotationElementHeader(IEdmValueAnnotation annotation, bool isInline)
		{
			this.xmlWriter.WriteStartElement("ValueAnnotation");
			this.WriteRequiredAttribute<IEdmTerm>("Term", annotation.Term, new Func<IEdmTerm, string>(this.TermAsXml));
			this.WriteOptionalAttribute<string>("Qualifier", annotation.Qualifier, new Func<string, string>(EdmValueWriter.StringAsXml));
			if (isInline)
			{
				this.WriteInlineExpression(annotation.Value);
			}
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x0001B0FC File Offset: 0x000192FC
		internal void WriteTypeAnnotationElementHeader(IEdmTypeAnnotation annotation)
		{
			this.xmlWriter.WriteStartElement("TypeAnnotation");
			this.WriteRequiredAttribute<IEdmTerm>("Term", annotation.Term, new Func<IEdmTerm, string>(this.TermAsXml));
			this.WriteOptionalAttribute<string>("Qualifier", annotation.Qualifier, new Func<string, string>(EdmValueWriter.StringAsXml));
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x0001B154 File Offset: 0x00019354
		internal void WritePropertyValueElementHeader(IEdmPropertyValueBinding value, bool isInline)
		{
			this.xmlWriter.WriteStartElement("PropertyValue");
			this.WriteRequiredAttribute<string>("Property", value.BoundProperty.Name, new Func<string, string>(EdmValueWriter.StringAsXml));
			if (isInline)
			{
				this.WriteInlineExpression(value.Value);
			}
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x0001B1A2 File Offset: 0x000193A2
		internal void WriteRecordExpressionElementHeader(IEdmRecordExpression expression)
		{
			this.xmlWriter.WriteStartElement("Record");
			this.WriteOptionalAttribute<IEdmStructuredTypeReference>("Type", expression.DeclaredType, new Func<IEdmStructuredTypeReference, string>(this.TypeReferenceAsXml));
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x0001B1D1 File Offset: 0x000193D1
		internal void WritePropertyConstructorElementHeader(IEdmPropertyConstructor constructor, bool isInline)
		{
			this.xmlWriter.WriteStartElement("PropertyValue");
			this.WriteRequiredAttribute<string>("Property", constructor.Name, new Func<string, string>(EdmValueWriter.StringAsXml));
			if (isInline)
			{
				this.WriteInlineExpression(constructor.Value);
			}
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x0001B20F File Offset: 0x0001940F
		internal void WriteStringConstantExpressionElement(IEdmStringConstantExpression expression)
		{
			this.xmlWriter.WriteStartElement("String");
			this.xmlWriter.WriteString(EdmValueWriter.StringAsXml(expression.Value));
			this.WriteEndElement();
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x0001B23D File Offset: 0x0001943D
		internal void WriteBinaryConstantExpressionElement(IEdmBinaryConstantExpression expression)
		{
			this.xmlWriter.WriteStartElement("String");
			this.xmlWriter.WriteString(EdmValueWriter.BinaryAsXml(expression.Value));
			this.WriteEndElement();
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x0001B26B File Offset: 0x0001946B
		internal void WriteBooleanConstantExpressionElement(IEdmBooleanConstantExpression expression)
		{
			this.xmlWriter.WriteStartElement("Bool");
			this.xmlWriter.WriteString(EdmValueWriter.BooleanAsXml(expression.Value));
			this.WriteEndElement();
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x0001B299 File Offset: 0x00019499
		internal void WriteNullConstantExpressionElement(IEdmNullExpression expression)
		{
			this.xmlWriter.WriteStartElement("Null");
			this.WriteEndElement();
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x0001B2B1 File Offset: 0x000194B1
		internal void WriteDateTimeConstantExpressionElement(IEdmDateTimeConstantExpression expression)
		{
			this.xmlWriter.WriteStartElement("DateTime");
			this.xmlWriter.WriteString(EdmValueWriter.DateTimeAsXml(expression.Value));
			this.WriteEndElement();
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x0001B2DF File Offset: 0x000194DF
		internal void WriteDateTimeOffsetConstantExpressionElement(IEdmDateTimeOffsetConstantExpression expression)
		{
			this.xmlWriter.WriteStartElement("DateTimeOffset");
			this.xmlWriter.WriteString(EdmValueWriter.DateTimeOffsetAsXml(expression.Value));
			this.WriteEndElement();
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x0001B30D File Offset: 0x0001950D
		internal void WriteDecimalConstantExpressionElement(IEdmDecimalConstantExpression expression)
		{
			this.xmlWriter.WriteStartElement("Decimal");
			this.xmlWriter.WriteString(EdmValueWriter.DecimalAsXml(expression.Value));
			this.WriteEndElement();
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x0001B33B File Offset: 0x0001953B
		internal void WriteFloatingConstantExpressionElement(IEdmFloatingConstantExpression expression)
		{
			this.xmlWriter.WriteStartElement("Float");
			this.xmlWriter.WriteString(EdmValueWriter.FloatAsXml(expression.Value));
			this.WriteEndElement();
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x0001B369 File Offset: 0x00019569
		internal void WriteFunctionApplicationElementHeader(IEdmApplyExpression expression, bool isFunction)
		{
			this.xmlWriter.WriteStartElement("Apply");
			if (isFunction)
			{
				this.WriteRequiredAttribute<IEdmFunction>("Function", ((IEdmFunctionReferenceExpression)expression.AppliedFunction).ReferencedFunction, new Func<IEdmFunction, string>(this.FunctionAsXml));
			}
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x0001B3A5 File Offset: 0x000195A5
		internal void WriteGuidConstantExpressionElement(IEdmGuidConstantExpression expression)
		{
			this.xmlWriter.WriteStartElement("Guid");
			this.xmlWriter.WriteString(EdmValueWriter.GuidAsXml(expression.Value));
			this.WriteEndElement();
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x0001B3D3 File Offset: 0x000195D3
		internal void WriteIntegerConstantExpressionElement(IEdmIntegerConstantExpression expression)
		{
			this.xmlWriter.WriteStartElement("Int");
			this.xmlWriter.WriteString(EdmValueWriter.LongAsXml(expression.Value));
			this.WriteEndElement();
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x0001B401 File Offset: 0x00019601
		internal void WritePathExpressionElement(IEdmPathExpression expression)
		{
			this.xmlWriter.WriteStartElement("Path");
			this.xmlWriter.WriteString(EdmModelCsdlSchemaWriter.PathAsXml(expression.Path));
			this.WriteEndElement();
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x0001B42F File Offset: 0x0001962F
		internal void WriteIfExpressionElementHeader(IEdmIfExpression expression)
		{
			this.xmlWriter.WriteStartElement("If");
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x0001B441 File Offset: 0x00019641
		internal void WriteCollectionExpressionElementHeader(IEdmCollectionExpression expression)
		{
			this.xmlWriter.WriteStartElement("Collection");
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x0001B453 File Offset: 0x00019653
		internal void WriteLabeledElementHeader(IEdmLabeledExpression labeledElement)
		{
			this.xmlWriter.WriteStartElement("LabeledElement");
			this.WriteRequiredAttribute<string>("Name", labeledElement.Name, new Func<string, string>(EdmValueWriter.StringAsXml));
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x0001B482 File Offset: 0x00019682
		internal void WriteIsTypeExpressionElementHeader(IEdmIsTypeExpression expression, bool inlineType)
		{
			this.xmlWriter.WriteStartElement("IsType");
			if (inlineType)
			{
				this.WriteRequiredAttribute<IEdmTypeReference>("Type", expression.Type, new Func<IEdmTypeReference, string>(this.TypeReferenceAsXml));
			}
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x0001B4B4 File Offset: 0x000196B4
		internal void WriteAssertTypeExpressionElementHeader(IEdmAssertTypeExpression expression, bool inlineType)
		{
			this.xmlWriter.WriteStartElement("AssertType");
			if (inlineType)
			{
				this.WriteRequiredAttribute<IEdmTypeReference>("Type", expression.Type, new Func<IEdmTypeReference, string>(this.TypeReferenceAsXml));
			}
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x0001B4E6 File Offset: 0x000196E6
		internal void WriteEntitySetReferenceExpressionElement(IEdmEntitySetReferenceExpression expression)
		{
			this.xmlWriter.WriteStartElement("EntitySetReference");
			this.WriteRequiredAttribute<IEdmEntitySet>("Name", expression.ReferencedEntitySet, new Func<IEdmEntitySet, string>(EdmModelCsdlSchemaWriter.EntitySetAsXml));
			this.WriteEndElement();
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x0001B51B File Offset: 0x0001971B
		internal void WriteParameterReferenceExpressionElement(IEdmParameterReferenceExpression expression)
		{
			this.xmlWriter.WriteStartElement("ParameterReference");
			this.WriteRequiredAttribute<IEdmFunctionParameter>("Name", expression.ReferencedParameter, new Func<IEdmFunctionParameter, string>(EdmModelCsdlSchemaWriter.ParameterAsXml));
			this.WriteEndElement();
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x0001B550 File Offset: 0x00019750
		internal void WriteFunctionReferenceExpressionElement(IEdmFunctionReferenceExpression expression)
		{
			this.xmlWriter.WriteStartElement("FunctionReference");
			this.WriteRequiredAttribute<IEdmFunction>("Name", expression.ReferencedFunction, new Func<IEdmFunction, string>(this.FunctionAsXml));
			this.WriteEndElement();
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x0001B585 File Offset: 0x00019785
		internal void WriteEnumMemberReferenceExpressionElement(IEdmEnumMemberReferenceExpression expression)
		{
			this.xmlWriter.WriteStartElement("EnumMemberReference");
			this.WriteRequiredAttribute<IEdmEnumMember>("Name", expression.ReferencedEnumMember, new Func<IEdmEnumMember, string>(EdmModelCsdlSchemaWriter.EnumMemberAsXml));
			this.WriteEndElement();
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x0001B5BA File Offset: 0x000197BA
		internal void WritePropertyReferenceExpressionElementHeader(IEdmPropertyReferenceExpression expression)
		{
			this.xmlWriter.WriteStartElement("PropertyReference");
			this.WriteRequiredAttribute<IEdmProperty>("Name", expression.ReferencedProperty, new Func<IEdmProperty, string>(EdmModelCsdlSchemaWriter.PropertyAsXml));
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x0001B5E9 File Offset: 0x000197E9
		internal void WriteEndElement()
		{
			this.xmlWriter.WriteEndElement();
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x0001B5F6 File Offset: 0x000197F6
		internal void WriteOptionalAttribute<T>(string attribute, T value, T defaultValue, Func<T, string> toXml)
		{
			if (!value.Equals(defaultValue))
			{
				this.xmlWriter.WriteAttributeString(attribute, toXml.Invoke(value));
			}
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x0001B621 File Offset: 0x00019821
		internal void WriteOptionalAttribute<T>(string attribute, T value, Func<T, string> toXml)
		{
			if (value != null)
			{
				this.xmlWriter.WriteAttributeString(attribute, toXml.Invoke(value));
			}
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x0001B63E File Offset: 0x0001983E
		internal void WriteRequiredAttribute<T>(string attribute, T value, Func<T, string> toXml)
		{
			this.xmlWriter.WriteAttributeString(attribute, toXml.Invoke(value));
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x0001B654 File Offset: 0x00019854
		private static string MultiplicityAsXml(EdmMultiplicity endKind)
		{
			switch (endKind)
			{
			case EdmMultiplicity.ZeroOrOne:
				return "0..1";
			case EdmMultiplicity.One:
				return "1";
			case EdmMultiplicity.Many:
				return "*";
			default:
				throw new InvalidOperationException(Strings.UnknownEnumVal_Multiplicity(endKind.ToString()));
			}
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x0001B6A0 File Offset: 0x000198A0
		private static string FunctionParameterModeAsXml(EdmFunctionParameterMode mode)
		{
			switch (mode)
			{
			case EdmFunctionParameterMode.In:
				return "In";
			case EdmFunctionParameterMode.Out:
				return "Out";
			case EdmFunctionParameterMode.InOut:
				return "InOut";
			default:
				throw new InvalidOperationException(Strings.UnknownEnumVal_FunctionParameterMode(mode.ToString()));
			}
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x0001B6EC File Offset: 0x000198EC
		private static string ConcurrencyModeAsXml(EdmConcurrencyMode mode)
		{
			switch (mode)
			{
			case EdmConcurrencyMode.None:
				return "None";
			case EdmConcurrencyMode.Fixed:
				return "Fixed";
			default:
				throw new InvalidOperationException(Strings.UnknownEnumVal_ConcurrencyMode(mode.ToString()));
			}
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x0001B72C File Offset: 0x0001992C
		private static string PathAsXml(IEnumerable<string> path)
		{
			return EdmUtil.JoinInternal<string>("/", path);
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x0001B739 File Offset: 0x00019939
		private static string ParameterAsXml(IEdmFunctionParameter parameter)
		{
			return parameter.Name;
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x0001B741 File Offset: 0x00019941
		private static string PropertyAsXml(IEdmProperty property)
		{
			return property.Name;
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x0001B749 File Offset: 0x00019949
		private static string EnumMemberAsXml(IEdmEnumMember member)
		{
			return member.DeclaringType.FullName() + "/" + member.Name;
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x0001B766 File Offset: 0x00019966
		private static string EntitySetAsXml(IEdmEntitySet set)
		{
			return set.Container.FullName() + "/" + set.Name;
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x0001B783 File Offset: 0x00019983
		private static string SridAsXml(int? i)
		{
			if (i == null)
			{
				return "Variable";
			}
			return Convert.ToString(i.Value, CultureInfo.InvariantCulture);
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x0001B7A8 File Offset: 0x000199A8
		private static string GetCsdlNamespace(Version edmVersion)
		{
			string[] array;
			if (CsdlConstants.SupportedVersions.TryGetValue(edmVersion, ref array))
			{
				return array[0];
			}
			throw new InvalidOperationException(Strings.Serializer_UnknownEdmVersion);
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x0001B7D4 File Offset: 0x000199D4
		private string SerializationName(IEdmSchemaElement element)
		{
			string text;
			if (this.namespaceAliasMappings != null && this.namespaceAliasMappings.TryGetValue(element.Namespace, out text))
			{
				return text + "." + element.Name;
			}
			return element.FullName();
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x0001B818 File Offset: 0x00019A18
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

		// Token: 0x060009DB RID: 2523 RVA: 0x0001B899 File Offset: 0x00019A99
		private string TypeDefinitionAsXml(IEdmSchemaType type)
		{
			return this.SerializationName(type);
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x0001B8A2 File Offset: 0x00019AA2
		private string FunctionAsXml(IEdmFunction function)
		{
			return this.SerializationName(function);
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x0001B8AB File Offset: 0x00019AAB
		private string TermAsXml(IEdmTerm term)
		{
			if (term == null)
			{
				return string.Empty;
			}
			return this.SerializationName(term);
		}

		// Token: 0x040004A5 RID: 1189
		protected XmlWriter xmlWriter;

		// Token: 0x040004A6 RID: 1190
		protected Version version;

		// Token: 0x040004A7 RID: 1191
		private readonly VersioningDictionary<string, string> namespaceAliasMappings;

		// Token: 0x040004A8 RID: 1192
		private readonly IEdmModel model;
	}
}
