using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml;
using Microsoft.OData.Edm.Annotations;
using Microsoft.OData.Edm.Csdl.CsdlSemantics;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Expressions;
using Microsoft.OData.Edm.Library;
using Microsoft.OData.Edm.Values;

namespace Microsoft.OData.Edm.Csdl.Serialization
{
	// Token: 0x020001DF RID: 479
	internal class EdmModelCsdlSchemaWriter
	{
		// Token: 0x06000A1E RID: 2590 RVA: 0x0001A92B File Offset: 0x00018B2B
		internal EdmModelCsdlSchemaWriter(IEdmModel model, VersioningDictionary<string, string> namespaceAliasMappings, XmlWriter xmlWriter, Version edmVersion)
		{
			this.xmlWriter = xmlWriter;
			this.version = edmVersion;
			this.edmxNamespace = CsdlConstants.SupportedEdmxVersions[edmVersion];
			this.model = model;
			this.namespaceAliasMappings = namespaceAliasMappings;
		}

		// Token: 0x06000A1F RID: 2591 RVA: 0x0001A962 File Offset: 0x00018B62
		internal static string PathAsXml(IEnumerable<string> path)
		{
			return EdmUtil.JoinInternal<string>("/", path);
		}

		// Token: 0x06000A20 RID: 2592 RVA: 0x0001A96F File Offset: 0x00018B6F
		internal void WriteReferenceElementHeader(IEdmReference reference)
		{
			this.xmlWriter.WriteStartElement("edmx", "Reference", this.edmxNamespace);
			this.WriteRequiredAttribute<string>("Uri", reference.Uri, new Func<string, string>(EdmValueWriter.StringAsXml));
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x0001A9AC File Offset: 0x00018BAC
		internal void WriteIncludeElement(IEdmInclude include)
		{
			this.xmlWriter.WriteStartElement("edmx", "Include", this.edmxNamespace);
			this.WriteRequiredAttribute<string>("Namespace", include.Namespace, new Func<string, string>(EdmValueWriter.StringAsXml));
			this.WriteRequiredAttribute<string>("Alias", include.Alias, new Func<string, string>(EdmValueWriter.StringAsXml));
			this.xmlWriter.WriteEndElement();
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x0001AA1C File Offset: 0x00018C1C
		internal void WriteIncludeAnnotationsElement(IEdmIncludeAnnotations includeAnnotations)
		{
			this.xmlWriter.WriteStartElement("edmx", "IncludeAnnotations", this.edmxNamespace);
			this.WriteRequiredAttribute<string>("TermNamespace", includeAnnotations.TermNamespace, new Func<string, string>(EdmValueWriter.StringAsXml));
			this.WriteOptionalAttribute<string>("Qualifier", includeAnnotations.Qualifier, new Func<string, string>(EdmValueWriter.StringAsXml));
			this.WriteOptionalAttribute<string>("TargetNamespace", includeAnnotations.TargetNamespace, new Func<string, string>(EdmValueWriter.StringAsXml));
			this.xmlWriter.WriteEndElement();
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x0001AAA8 File Offset: 0x00018CA8
		internal void WriteValueTermElementHeader(IEdmValueTerm term, bool inlineType)
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

		// Token: 0x06000A24 RID: 2596 RVA: 0x0001AB44 File Offset: 0x00018D44
		internal void WriteComplexTypeElementHeader(IEdmComplexType complexType)
		{
			this.xmlWriter.WriteStartElement("ComplexType");
			this.WriteRequiredAttribute<string>("Name", complexType.Name, new Func<string, string>(EdmValueWriter.StringAsXml));
			this.WriteOptionalAttribute<IEdmComplexType>("BaseType", complexType.BaseComplexType(), new Func<IEdmComplexType, string>(this.TypeDefinitionAsXml));
			this.WriteOptionalAttribute<bool>("Abstract", complexType.IsAbstract, false, new Func<bool, string>(EdmValueWriter.BooleanAsXml));
			this.WriteOptionalAttribute<bool>("OpenType", complexType.IsOpen, false, new Func<bool, string>(EdmValueWriter.BooleanAsXml));
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x0001ABD8 File Offset: 0x00018DD8
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

		// Token: 0x06000A26 RID: 2598 RVA: 0x0001AC5C File Offset: 0x00018E5C
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

		// Token: 0x06000A27 RID: 2599 RVA: 0x0001ACD0 File Offset: 0x00018ED0
		internal void WriteEntitySetElementHeader(IEdmEntitySet entitySet)
		{
			this.xmlWriter.WriteStartElement("EntitySet");
			this.WriteRequiredAttribute<string>("Name", entitySet.Name, new Func<string, string>(EdmValueWriter.StringAsXml));
			this.WriteRequiredAttribute<string>("EntityType", entitySet.EntityType().FullName(), new Func<string, string>(EdmValueWriter.StringAsXml));
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x0001AD2C File Offset: 0x00018F2C
		internal void WriteSingletonElementHeader(IEdmSingleton singleton)
		{
			this.xmlWriter.WriteStartElement("Singleton");
			this.WriteRequiredAttribute<string>("Name", singleton.Name, new Func<string, string>(EdmValueWriter.StringAsXml));
			this.WriteRequiredAttribute<string>("Type", singleton.EntityType().FullName(), new Func<string, string>(EdmValueWriter.StringAsXml));
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x0001AD88 File Offset: 0x00018F88
		internal void WriteNavigationPropertyBinding(IEdmNavigationSource navigationSource, IEdmNavigationPropertyBinding binding)
		{
			this.WriteNavigationPropertyBinding(binding, navigationSource.EntityType());
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x0001AD98 File Offset: 0x00018F98
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

		// Token: 0x06000A2B RID: 2603 RVA: 0x0001AE74 File Offset: 0x00019074
		internal void WriteDelaredKeyPropertiesElementHeader()
		{
			this.xmlWriter.WriteStartElement("Key");
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x0001AE86 File Offset: 0x00019086
		internal void WritePropertyRefElement(IEdmStructuralProperty property)
		{
			this.xmlWriter.WriteStartElement("PropertyRef");
			this.WriteRequiredAttribute<string>("Name", property.Name, new Func<string, string>(EdmValueWriter.StringAsXml));
			this.WriteEndElement();
		}

		// Token: 0x06000A2D RID: 2605 RVA: 0x0001AEBC File Offset: 0x000190BC
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
				this.WriteRequiredAttribute<string>("Partner", member.Partner.Name, new Func<string, string>(EdmValueWriter.StringAsXml));
			}
			this.WriteOptionalAttribute<bool>("ContainsTarget", member.ContainsTarget, false, new Func<bool, string>(EdmValueWriter.BooleanAsXml));
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x0001AF97 File Offset: 0x00019197
		internal void WriteOperationActionElement(string elementName, EdmOnDeleteAction operationAction)
		{
			this.xmlWriter.WriteStartElement(elementName);
			this.WriteRequiredAttribute<string>("Action", operationAction.ToString(), new Func<string, string>(EdmValueWriter.StringAsXml));
			this.WriteEndElement();
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x0001AFD0 File Offset: 0x000191D0
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

		// Token: 0x06000A30 RID: 2608 RVA: 0x0001B08C File Offset: 0x0001928C
		internal void WriteAnnotationsElementHeader(string annotationsTarget)
		{
			this.xmlWriter.WriteStartElement("Annotations");
			this.WriteRequiredAttribute<string>("Target", annotationsTarget, new Func<string, string>(EdmValueWriter.StringAsXml));
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x0001B0B8 File Offset: 0x000192B8
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

		// Token: 0x06000A32 RID: 2610 RVA: 0x0001B150 File Offset: 0x00019350
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

		// Token: 0x06000A33 RID: 2611 RVA: 0x0001B1C6 File Offset: 0x000193C6
		internal void WriteNullableAttribute(IEdmTypeReference reference)
		{
			this.WriteOptionalAttribute<bool>("Nullable", reference.IsNullable, true, new Func<bool, string>(EdmValueWriter.BooleanAsXml));
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x0001B1E8 File Offset: 0x000193E8
		internal void WriteBinaryTypeAttributes(IEdmBinaryTypeReference reference)
		{
			if (reference.IsUnbounded)
			{
				this.WriteRequiredAttribute<string>("MaxLength", "max", new Func<string, string>(EdmValueWriter.StringAsXml));
				return;
			}
			this.WriteOptionalAttribute<int?>("MaxLength", reference.MaxLength, new Func<int?, string>(EdmValueWriter.IntAsXml));
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x0001B238 File Offset: 0x00019438
		internal void WriteDecimalTypeAttributes(IEdmDecimalTypeReference reference)
		{
			this.WriteOptionalAttribute<int?>("Precision", reference.Precision, new Func<int?, string>(EdmValueWriter.IntAsXml));
			this.WriteOptionalAttribute<int?>("Scale", reference.Scale, new int?(0), new Func<int?, string>(EdmModelCsdlSchemaWriter.ScaleAsXml));
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x0001B285 File Offset: 0x00019485
		internal void WriteSpatialTypeAttributes(IEdmSpatialTypeReference reference)
		{
			this.WriteRequiredAttribute<int?>("SRID", reference.SpatialReferenceIdentifier, new Func<int?, string>(EdmModelCsdlSchemaWriter.SridAsXml));
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x0001B2A4 File Offset: 0x000194A4
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

		// Token: 0x06000A38 RID: 2616 RVA: 0x0001B328 File Offset: 0x00019528
		internal void WriteTemporalTypeAttributes(IEdmTemporalTypeReference reference)
		{
			if (reference.Precision != null)
			{
				this.WriteOptionalAttribute<int?>("Precision", reference.Precision, new int?(0), new Func<int?, string>(EdmValueWriter.IntAsXml));
			}
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x0001B368 File Offset: 0x00019568
		internal void WriteReferentialConstraintElementHeader(IEdmNavigationProperty constraint)
		{
			this.xmlWriter.WriteStartElement("ReferentialConstraint");
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x0001B37C File Offset: 0x0001957C
		internal void WriteReferentialConstraintPair(EdmReferentialConstraintPropertyPair pair)
		{
			this.xmlWriter.WriteStartElement("ReferentialConstraint");
			this.WriteRequiredAttribute<string>("Property", pair.DependentProperty.Name, new Func<string, string>(EdmValueWriter.StringAsXml));
			this.WriteRequiredAttribute<string>("ReferencedProperty", pair.PrincipalProperty.Name, new Func<string, string>(EdmValueWriter.StringAsXml));
			this.WriteEndElement();
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x0001B3E4 File Offset: 0x000195E4
		internal void WriteAnnotationStringAttribute(IEdmDirectValueAnnotation annotation)
		{
			IEdmPrimitiveValue edmPrimitiveValue = (IEdmPrimitiveValue)annotation.Value;
			if (edmPrimitiveValue != null)
			{
				this.xmlWriter.WriteAttributeString(annotation.Name, annotation.NamespaceUri, EdmValueWriter.PrimitiveValueAsXml(edmPrimitiveValue));
			}
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x0001B420 File Offset: 0x00019620
		internal void WriteAnnotationStringElement(IEdmDirectValueAnnotation annotation)
		{
			IEdmPrimitiveValue edmPrimitiveValue = (IEdmPrimitiveValue)annotation.Value;
			if (edmPrimitiveValue != null)
			{
				this.xmlWriter.WriteRaw(((IEdmStringValue)edmPrimitiveValue).Value);
			}
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x0001B452 File Offset: 0x00019652
		internal void WriteActionElementHeader(IEdmAction action)
		{
			this.xmlWriter.WriteStartElement("Action");
			this.WriteOperationElementAttributes(action);
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x0001B46B File Offset: 0x0001966B
		internal void WriteFunctionElementHeader(IEdmFunction function)
		{
			this.xmlWriter.WriteStartElement("Function");
			this.WriteOperationElementAttributes(function);
			if (function.IsComposable)
			{
				this.WriteOptionalAttribute<bool>("IsComposable", function.IsComposable, new Func<bool, string>(EdmValueWriter.BooleanAsXml));
			}
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x0001B4A9 File Offset: 0x000196A9
		internal void WriteReturnTypeElementHeader()
		{
			this.xmlWriter.WriteStartElement("ReturnType");
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x0001B4BB File Offset: 0x000196BB
		internal void WriteTypeAttribute(IEdmTypeReference typeReference)
		{
			this.WriteRequiredAttribute<IEdmTypeReference>("Type", typeReference, new Func<IEdmTypeReference, string>(this.TypeReferenceAsXml));
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x0001B4D5 File Offset: 0x000196D5
		internal void WriteActionImportElementHeader(IEdmActionImport actionImport)
		{
			this.xmlWriter.WriteStartElement("ActionImport");
			this.WriteOperationImportAttributes(actionImport, "Action");
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x0001B4F3 File Offset: 0x000196F3
		internal void WriteFunctionImportElementHeader(IEdmFunctionImport functionImport)
		{
			this.xmlWriter.WriteStartElement("FunctionImport");
			this.WriteOperationImportAttributes(functionImport, "Function");
			this.WriteOptionalAttribute<bool>("IncludeInServiceDocument", functionImport.IncludeInServiceDocument, false, new Func<bool, string>(EdmValueWriter.BooleanAsXml));
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x0001B530 File Offset: 0x00019730
		internal void WriteOperationParameterElementHeader(IEdmOperationParameter parameter, bool inlineType)
		{
			this.xmlWriter.WriteStartElement("Parameter");
			this.WriteRequiredAttribute<string>("Name", parameter.Name, new Func<string, string>(EdmValueWriter.StringAsXml));
			if (inlineType)
			{
				this.WriteRequiredAttribute<IEdmTypeReference>("Type", parameter.Type, new Func<IEdmTypeReference, string>(this.TypeReferenceAsXml));
			}
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x0001B58A File Offset: 0x0001978A
		internal void WriteCollectionTypeElementHeader(IEdmCollectionType collectionType, bool inlineType)
		{
			this.xmlWriter.WriteStartElement("CollectionType");
			if (inlineType)
			{
				this.WriteRequiredAttribute<IEdmTypeReference>("ElementType", collectionType.ElementType, new Func<IEdmTypeReference, string>(this.TypeReferenceAsXml));
			}
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x0001B5BC File Offset: 0x000197BC
		internal void WriteInlineExpression(IEdmExpression expression)
		{
			EdmExpressionKind expressionKind = expression.ExpressionKind;
			switch (expressionKind)
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
				break;
			case EdmExpressionKind.Path:
				this.WriteRequiredAttribute<IEnumerable<string>>("Path", ((IEdmPathExpression)expression).Path, new Func<IEnumerable<string>, string>(EdmModelCsdlSchemaWriter.PathAsXml));
				return;
			default:
				switch (expressionKind)
				{
				case EdmExpressionKind.PropertyPath:
					this.WriteRequiredAttribute<IEnumerable<string>>("PropertyPath", ((IEdmPathExpression)expression).Path, new Func<IEnumerable<string>, string>(EdmModelCsdlSchemaWriter.PathAsXml));
					return;
				case EdmExpressionKind.NavigationPropertyPath:
					this.WriteRequiredAttribute<IEnumerable<string>>("NavigationPropertyPath", ((IEdmPathExpression)expression).Path, new Func<IEnumerable<string>, string>(EdmModelCsdlSchemaWriter.PathAsXml));
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
				break;
			}
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x0001B810 File Offset: 0x00019A10
		internal void WriteValueAnnotationElementHeader(IEdmValueAnnotation annotation, bool isInline)
		{
			this.xmlWriter.WriteStartElement("Annotation");
			this.WriteRequiredAttribute<IEdmTerm>("Term", annotation.Term, new Func<IEdmTerm, string>(this.TermAsXml));
			this.WriteOptionalAttribute<string>("Qualifier", annotation.Qualifier, new Func<string, string>(EdmValueWriter.StringAsXml));
			if (isInline)
			{
				this.WriteInlineExpression(annotation.Value);
			}
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x0001B878 File Offset: 0x00019A78
		internal void WritePropertyValueElementHeader(IEdmPropertyValueBinding value, bool isInline)
		{
			this.xmlWriter.WriteStartElement("PropertyValue");
			this.WriteRequiredAttribute<string>("Property", value.BoundProperty.Name, new Func<string, string>(EdmValueWriter.StringAsXml));
			if (isInline)
			{
				this.WriteInlineExpression(value.Value);
			}
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x0001B8C6 File Offset: 0x00019AC6
		internal void WriteRecordExpressionElementHeader(IEdmRecordExpression expression)
		{
			this.xmlWriter.WriteStartElement("Record");
			this.WriteOptionalAttribute<IEdmStructuredTypeReference>("Type", expression.DeclaredType, new Func<IEdmStructuredTypeReference, string>(this.TypeReferenceAsXml));
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x0001B8F5 File Offset: 0x00019AF5
		internal void WritePropertyConstructorElementHeader(IEdmPropertyConstructor constructor, bool isInline)
		{
			this.xmlWriter.WriteStartElement("PropertyValue");
			this.WriteRequiredAttribute<string>("Property", constructor.Name, new Func<string, string>(EdmValueWriter.StringAsXml));
			if (isInline)
			{
				this.WriteInlineExpression(constructor.Value);
			}
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x0001B933 File Offset: 0x00019B33
		internal void WriteStringConstantExpressionElement(IEdmStringConstantExpression expression)
		{
			this.xmlWriter.WriteStartElement("String");
			this.xmlWriter.WriteString(EdmValueWriter.StringAsXml(expression.Value));
			this.WriteEndElement();
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x0001B961 File Offset: 0x00019B61
		internal void WriteBinaryConstantExpressionElement(IEdmBinaryConstantExpression expression)
		{
			this.xmlWriter.WriteStartElement("Binary");
			this.xmlWriter.WriteString(EdmValueWriter.BinaryAsXml(expression.Value));
			this.WriteEndElement();
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x0001B98F File Offset: 0x00019B8F
		internal void WriteBooleanConstantExpressionElement(IEdmBooleanConstantExpression expression)
		{
			this.xmlWriter.WriteStartElement("Bool");
			this.xmlWriter.WriteString(EdmValueWriter.BooleanAsXml(expression.Value));
			this.WriteEndElement();
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x0001B9BD File Offset: 0x00019BBD
		internal void WriteNullConstantExpressionElement(IEdmNullExpression expression)
		{
			this.xmlWriter.WriteStartElement("Null");
			this.WriteEndElement();
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x0001B9D5 File Offset: 0x00019BD5
		internal void WriteDateConstantExpressionElement(IEdmDateConstantExpression expression)
		{
			this.xmlWriter.WriteStartElement("Date");
			this.xmlWriter.WriteString(EdmValueWriter.DateAsXml(expression.Value));
			this.WriteEndElement();
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x0001BA03 File Offset: 0x00019C03
		internal void WriteDateTimeOffsetConstantExpressionElement(IEdmDateTimeOffsetConstantExpression expression)
		{
			this.xmlWriter.WriteStartElement("DateTimeOffset");
			this.xmlWriter.WriteString(EdmValueWriter.DateTimeOffsetAsXml(expression.Value));
			this.WriteEndElement();
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x0001BA31 File Offset: 0x00019C31
		internal void WriteDurationConstantExpressionElement(IEdmDurationConstantExpression expression)
		{
			this.xmlWriter.WriteStartElement("Duration");
			this.xmlWriter.WriteString(EdmValueWriter.DurationAsXml(expression.Value));
			this.WriteEndElement();
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x0001BA5F File Offset: 0x00019C5F
		internal void WriteDecimalConstantExpressionElement(IEdmDecimalConstantExpression expression)
		{
			this.xmlWriter.WriteStartElement("Decimal");
			this.xmlWriter.WriteString(EdmValueWriter.DecimalAsXml(expression.Value));
			this.WriteEndElement();
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x0001BA8D File Offset: 0x00019C8D
		internal void WriteFloatingConstantExpressionElement(IEdmFloatingConstantExpression expression)
		{
			this.xmlWriter.WriteStartElement("Float");
			this.xmlWriter.WriteString(EdmValueWriter.FloatAsXml(expression.Value));
			this.WriteEndElement();
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x0001BABB File Offset: 0x00019CBB
		internal void WriteFunctionApplicationElementHeader(IEdmApplyExpression expression, bool isFunction)
		{
			this.xmlWriter.WriteStartElement("Apply");
			if (isFunction)
			{
				this.WriteRequiredAttribute<IEdmOperation>("Function", ((IEdmOperationReferenceExpression)expression.AppliedOperation).ReferencedOperation, new Func<IEdmOperation, string>(this.FunctionAsXml));
			}
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x0001BAF7 File Offset: 0x00019CF7
		internal void WriteGuidConstantExpressionElement(IEdmGuidConstantExpression expression)
		{
			this.xmlWriter.WriteStartElement("Guid");
			this.xmlWriter.WriteString(EdmValueWriter.GuidAsXml(expression.Value));
			this.WriteEndElement();
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x0001BB25 File Offset: 0x00019D25
		internal void WriteIntegerConstantExpressionElement(IEdmIntegerConstantExpression expression)
		{
			this.xmlWriter.WriteStartElement("Int");
			this.xmlWriter.WriteString(EdmValueWriter.LongAsXml(expression.Value));
			this.WriteEndElement();
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x0001BB53 File Offset: 0x00019D53
		internal void WritePathExpressionElement(IEdmPathExpression expression)
		{
			this.xmlWriter.WriteStartElement("Path");
			this.xmlWriter.WriteString(EdmModelCsdlSchemaWriter.PathAsXml(expression.Path));
			this.WriteEndElement();
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x0001BB81 File Offset: 0x00019D81
		internal void WritePropertyPathExpressionElement(IEdmPathExpression expression)
		{
			this.xmlWriter.WriteStartElement("PropertyPath");
			this.xmlWriter.WriteString(EdmModelCsdlSchemaWriter.PathAsXml(expression.Path));
			this.WriteEndElement();
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x0001BBAF File Offset: 0x00019DAF
		internal void WriteNavigationPropertyPathExpressionElement(IEdmPathExpression expression)
		{
			this.xmlWriter.WriteStartElement("NavigationPropertyPath");
			this.xmlWriter.WriteString(EdmModelCsdlSchemaWriter.PathAsXml(expression.Path));
			this.WriteEndElement();
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x0001BBDD File Offset: 0x00019DDD
		internal void WriteIfExpressionElementHeader(IEdmIfExpression expression)
		{
			this.xmlWriter.WriteStartElement("If");
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x0001BBEF File Offset: 0x00019DEF
		internal void WriteCollectionExpressionElementHeader(IEdmCollectionExpression expression)
		{
			this.xmlWriter.WriteStartElement("Collection");
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x0001BC01 File Offset: 0x00019E01
		internal void WriteLabeledElementHeader(IEdmLabeledExpression labeledElement)
		{
			this.xmlWriter.WriteStartElement("LabeledElement");
			this.WriteRequiredAttribute<string>("Name", labeledElement.Name, new Func<string, string>(EdmValueWriter.StringAsXml));
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x0001BC30 File Offset: 0x00019E30
		internal void WriteTimeOfDayConstantExpressionElement(IEdmTimeOfDayConstantExpression expression)
		{
			this.xmlWriter.WriteStartElement("TimeOfDay");
			this.xmlWriter.WriteString(EdmValueWriter.TimeOfDayAsXml(expression.Value));
			this.WriteEndElement();
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x0001BC5E File Offset: 0x00019E5E
		internal void WriteIsTypeExpressionElementHeader(IEdmIsTypeExpression expression, bool inlineType)
		{
			this.xmlWriter.WriteStartElement("IsType");
			if (inlineType)
			{
				this.WriteRequiredAttribute<IEdmTypeReference>("Type", expression.Type, new Func<IEdmTypeReference, string>(this.TypeReferenceAsXml));
			}
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x0001BC90 File Offset: 0x00019E90
		internal void WriteCastExpressionElementHeader(IEdmCastExpression expression, bool inlineType)
		{
			this.xmlWriter.WriteStartElement("Cast");
			if (inlineType)
			{
				this.WriteRequiredAttribute<IEdmTypeReference>("Type", expression.Type, new Func<IEdmTypeReference, string>(this.TypeReferenceAsXml));
			}
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x0001BCC2 File Offset: 0x00019EC2
		internal void WriteEntitySetReferenceExpressionElement(IEdmEntitySetReferenceExpression expression)
		{
			this.xmlWriter.WriteStartElement("EntitySetReference");
			this.WriteRequiredAttribute<IEdmEntitySet>("Name", expression.ReferencedEntitySet, new Func<IEdmEntitySet, string>(EdmModelCsdlSchemaWriter.EntitySetAsXml));
			this.WriteEndElement();
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x0001BCF7 File Offset: 0x00019EF7
		internal void WriteParameterReferenceExpressionElement(IEdmParameterReferenceExpression expression)
		{
			this.xmlWriter.WriteStartElement("ParameterReference");
			this.WriteRequiredAttribute<IEdmOperationParameter>("Name", expression.ReferencedParameter, new Func<IEdmOperationParameter, string>(EdmModelCsdlSchemaWriter.ParameterAsXml));
			this.WriteEndElement();
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x0001BD2C File Offset: 0x00019F2C
		internal void WriteOperationReferenceExpressionElement(IEdmOperationReferenceExpression expression)
		{
			this.xmlWriter.WriteStartElement("FunctionReference");
			this.WriteRequiredAttribute<IEdmOperation>("Name", expression.ReferencedOperation, new Func<IEdmOperation, string>(this.FunctionAsXml));
			this.WriteEndElement();
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x0001BD61 File Offset: 0x00019F61
		internal void WriteEnumMemberExpressionElement(IEdmEnumMemberExpression expression)
		{
			this.xmlWriter.WriteStartElement("EnumMember");
			this.xmlWriter.WriteString(EdmModelCsdlSchemaWriter.EnumMemberAsXml(expression.EnumMembers));
			this.WriteEndElement();
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x0001BD8F File Offset: 0x00019F8F
		internal void WriteEnumMemberReferenceExpressionElement(IEdmEnumMemberReferenceExpression expression)
		{
			this.xmlWriter.WriteStartElement("EnumMember");
			this.xmlWriter.WriteString(EdmModelCsdlSchemaWriter.EnumMemberAsXml(expression.ReferencedEnumMember));
			this.WriteEndElement();
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x0001BDBD File Offset: 0x00019FBD
		internal void WritePropertyReferenceExpressionElementHeader(IEdmPropertyReferenceExpression expression)
		{
			this.xmlWriter.WriteStartElement("PropertyReference");
			this.WriteRequiredAttribute<IEdmProperty>("Name", expression.ReferencedProperty, new Func<IEdmProperty, string>(EdmModelCsdlSchemaWriter.PropertyAsXml));
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x0001BDEC File Offset: 0x00019FEC
		internal void WriteTypeDefinitionElementHeader(IEdmTypeDefinition typeDefinition)
		{
			this.xmlWriter.WriteStartElement("TypeDefinition");
			this.WriteRequiredAttribute<string>("Name", typeDefinition.Name, new Func<string, string>(EdmValueWriter.StringAsXml));
			this.WriteRequiredAttribute<IEdmPrimitiveType>("UnderlyingType", typeDefinition.UnderlyingType, new Func<IEdmPrimitiveType, string>(this.TypeDefinitionAsXml));
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x0001BE43 File Offset: 0x0001A043
		internal void WriteEndElement()
		{
			this.xmlWriter.WriteEndElement();
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x0001BE50 File Offset: 0x0001A050
		internal void WriteOptionalAttribute<T>(string attribute, T value, T defaultValue, Func<T, string> toXml)
		{
			if (!value.Equals(defaultValue))
			{
				this.xmlWriter.WriteAttributeString(attribute, toXml.Invoke(value));
			}
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x0001BE7B File Offset: 0x0001A07B
		internal void WriteOptionalAttribute<T>(string attribute, T value, Func<T, string> toXml)
		{
			if (value != null)
			{
				this.xmlWriter.WriteAttributeString(attribute, toXml.Invoke(value));
			}
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x0001BE98 File Offset: 0x0001A098
		internal void WriteRequiredAttribute<T>(string attribute, T value, Func<T, string> toXml)
		{
			this.xmlWriter.WriteAttributeString(attribute, toXml.Invoke(value));
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x0001BEB0 File Offset: 0x0001A0B0
		private void WriteOperationElementAttributes(IEdmOperation operation)
		{
			this.WriteRequiredAttribute<string>("Name", operation.Name, new Func<string, string>(EdmValueWriter.StringAsXml));
			if (operation.IsBound)
			{
				this.WriteOptionalAttribute<bool>("IsBound", operation.IsBound, new Func<bool, string>(EdmValueWriter.BooleanAsXml));
			}
			if (operation.EntitySetPath != null)
			{
				this.WriteOptionalAttribute<IEnumerable<string>>("EntitySetPath", operation.EntitySetPath.Path, new Func<IEnumerable<string>, string>(EdmModelCsdlSchemaWriter.PathAsXml));
			}
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x0001BF2C File Offset: 0x0001A12C
		private void WriteNavigationPropertyBinding(IEdmNavigationPropertyBinding binding, IEdmEntityType entityType)
		{
			this.xmlWriter.WriteStartElement("NavigationPropertyBinding");
			string text = binding.NavigationProperty.Name;
			if (!entityType.IsOrInheritsFrom(binding.NavigationProperty.DeclaringType))
			{
				text = binding.NavigationProperty.DeclaringEntityType().FullName() + '/' + text;
			}
			this.WriteRequiredAttribute<string>("Path", text, new Func<string, string>(EdmValueWriter.StringAsXml));
			this.WriteRequiredAttribute<string>("Target", binding.Target.Name, new Func<string, string>(EdmValueWriter.StringAsXml));
			this.xmlWriter.WriteEndElement();
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x0001BFCC File Offset: 0x0001A1CC
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

		// Token: 0x06000A6D RID: 2669 RVA: 0x0001C00C File Offset: 0x0001A20C
		private static string ParameterAsXml(IEdmOperationParameter parameter)
		{
			return parameter.Name;
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x0001C014 File Offset: 0x0001A214
		private static string PropertyAsXml(IEdmProperty property)
		{
			return property.Name;
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x0001C01C File Offset: 0x0001A21C
		private static string EnumMemberAsXml(IEdmEnumMember member)
		{
			return member.DeclaringType.FullName() + "/" + member.Name;
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x0001C03C File Offset: 0x0001A23C
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

		// Token: 0x06000A71 RID: 2673 RVA: 0x0001C0BC File Offset: 0x0001A2BC
		private static string EntitySetAsXml(IEdmEntitySet set)
		{
			return set.Container.FullName() + "/" + set.Name;
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x0001C0D9 File Offset: 0x0001A2D9
		private static string SridAsXml(int? i)
		{
			if (i == null)
			{
				return "Variable";
			}
			return Convert.ToString(i.Value, CultureInfo.InvariantCulture);
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x0001C0FB File Offset: 0x0001A2FB
		private static string ScaleAsXml(int? i)
		{
			if (i == null)
			{
				return "Variable";
			}
			return Convert.ToString(i.Value, CultureInfo.InvariantCulture);
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x0001C120 File Offset: 0x0001A320
		private static string GetCsdlNamespace(Version edmVersion)
		{
			string[] array;
			if (CsdlConstants.SupportedVersions.TryGetValue(edmVersion, ref array))
			{
				return array[0];
			}
			throw new InvalidOperationException(Strings.Serializer_UnknownEdmVersion);
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x0001C14C File Offset: 0x0001A34C
		private void WriteOperationImportAttributes(IEdmOperationImport operationImport, string operationAttributeName)
		{
			this.WriteRequiredAttribute<string>("Name", operationImport.Name, new Func<string, string>(EdmValueWriter.StringAsXml));
			this.WriteRequiredAttribute<string>(operationAttributeName, operationImport.Operation.FullName(), new Func<string, string>(EdmValueWriter.StringAsXml));
			if (operationImport.EntitySet == null)
			{
				return;
			}
			IEdmEntitySetReferenceExpression edmEntitySetReferenceExpression = operationImport.EntitySet as IEdmEntitySetReferenceExpression;
			if (edmEntitySetReferenceExpression != null)
			{
				this.WriteOptionalAttribute<string>("EntitySet", edmEntitySetReferenceExpression.ReferencedEntitySet.Name, new Func<string, string>(EdmValueWriter.StringAsXml));
				return;
			}
			IEdmPathExpression edmPathExpression = operationImport.EntitySet as IEdmPathExpression;
			if (edmPathExpression != null)
			{
				this.WriteOptionalAttribute<IEnumerable<string>>("EntitySet", edmPathExpression.Path, new Func<IEnumerable<string>, string>(EdmModelCsdlSchemaWriter.PathAsXml));
				return;
			}
			throw new InvalidOperationException(Strings.EdmModel_Validator_Semantic_OperationImportEntitySetExpressionIsInvalid(operationImport.Name));
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x0001C20C File Offset: 0x0001A40C
		private string SerializationName(IEdmSchemaElement element)
		{
			string text;
			if (this.namespaceAliasMappings != null && this.namespaceAliasMappings.TryGetValue(element.Namespace, out text))
			{
				return text + "." + element.Name;
			}
			return element.FullName();
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x0001C250 File Offset: 0x0001A450
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

		// Token: 0x06000A78 RID: 2680 RVA: 0x0001C2D1 File Offset: 0x0001A4D1
		private string TypeDefinitionAsXml(IEdmSchemaType type)
		{
			return this.SerializationName(type);
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x0001C2DA File Offset: 0x0001A4DA
		private string FunctionAsXml(IEdmOperation operation)
		{
			return this.SerializationName(operation);
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x0001C2E3 File Offset: 0x0001A4E3
		private string TermAsXml(IEdmTerm term)
		{
			if (term == null)
			{
				return string.Empty;
			}
			return this.SerializationName(term);
		}

		// Token: 0x040004E3 RID: 1251
		protected XmlWriter xmlWriter;

		// Token: 0x040004E4 RID: 1252
		protected Version version;

		// Token: 0x040004E5 RID: 1253
		private readonly string edmxNamespace;

		// Token: 0x040004E6 RID: 1254
		private readonly VersioningDictionary<string, string> namespaceAliasMappings;

		// Token: 0x040004E7 RID: 1255
		private readonly IEdmModel model;
	}
}
