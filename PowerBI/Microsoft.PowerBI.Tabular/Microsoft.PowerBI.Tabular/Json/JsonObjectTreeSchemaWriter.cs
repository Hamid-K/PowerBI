using System;

namespace Microsoft.AnalysisServices.Tabular.Json
{
	// Token: 0x020001B5 RID: 437
	internal static class JsonObjectTreeSchemaWriter
	{
		// Token: 0x06001AB4 RID: 6836 RVA: 0x000B197C File Offset: 0x000AFB7C
		public static void WriteSchemaForModel(JsonWriter writer, JsonObjectTreeSchemaWriterSettings settings)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("name");
			JsonSchemaWriter.WriteSchemaForString(writer);
			if (settings.ShouldWriteObject(ObjectType.Model))
			{
				settings.WriteObjectProperties(writer, ObjectType.Model);
			}
			if (settings.ShouldWriteCollection(ObjectType.Table))
			{
				writer.WritePropertyName("tables");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForTable(writer, settings);
				writer.WriteEndObject();
			}
			if (settings.ShouldWriteCollection(ObjectType.Relationship))
			{
				writer.WritePropertyName("relationships");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForRelationship(writer, settings);
				writer.WriteEndObject();
			}
			if (settings.ShouldWriteCollection(ObjectType.DataSource))
			{
				writer.WritePropertyName("dataSources");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForDataSource(writer, settings);
				writer.WriteEndObject();
			}
			if (settings.ShouldWriteCollection(ObjectType.Perspective))
			{
				writer.WritePropertyName("perspectives");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForPerspective(writer, settings);
				writer.WriteEndObject();
			}
			if (settings.ShouldWriteCollection(ObjectType.Culture))
			{
				writer.WritePropertyName("cultures");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForCulture(writer, settings);
				writer.WriteEndObject();
			}
			if (settings.ShouldWriteCollection(ObjectType.Role))
			{
				writer.WritePropertyName("roles");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForModelRole(writer, settings);
				writer.WriteEndObject();
			}
			if (settings.ShouldWriteCollection(ObjectType.Expression))
			{
				writer.WritePropertyName("expressions");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForNamedExpression(writer, settings);
				writer.WriteEndObject();
			}
			if (settings.ShouldWriteCollection(ObjectType.QueryGroup))
			{
				writer.WritePropertyName("queryGroups");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForQueryGroup(writer, settings);
				writer.WriteEndObject();
			}
			if (settings.ShouldWriteCollection(ObjectType.AnalyticsAIMetadata))
			{
				writer.WritePropertyName("analyticsAIMetadata");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForAnalyticsAIMetadata(writer, settings);
				writer.WriteEndObject();
			}
			if (settings.ShouldWriteCollection(ObjectType.Function))
			{
				writer.WritePropertyName("functions");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForFunction(writer, settings);
				writer.WriteEndObject();
			}
			if (settings.ShouldWriteCollection(ObjectType.BindingInfo))
			{
				writer.WritePropertyName("bindingInfoCollection");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForBindingInfo(writer, settings);
				writer.WriteEndObject();
			}
			if (settings.ShouldWriteCollection(ObjectType.Annotation))
			{
				writer.WritePropertyName("annotations");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForAnnotation(writer, settings);
				writer.WriteEndObject();
			}
			if (settings.ShouldWriteCollection(ObjectType.ExtendedProperty))
			{
				writer.WritePropertyName("extendedProperties");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForExtendedProperty(writer, settings);
				writer.WriteEndObject();
			}
			if (settings.ShouldWriteCollection(ObjectType.ExcludedArtifact))
			{
				writer.WritePropertyName("excludedArtifacts");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForExcludedArtifact(writer, settings);
				writer.WriteEndObject();
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001AB5 RID: 6837 RVA: 0x000B1DF4 File Offset: 0x000AFFF4
		public static void WriteSchemaForDataSource(JsonWriter writer, JsonObjectTreeSchemaWriterSettings settings)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("name");
			JsonSchemaWriter.WriteSchemaForString(writer);
			if (settings.ShouldWriteObject(ObjectType.DataSource))
			{
				settings.WriteObjectProperties(writer, ObjectType.DataSource);
			}
			if (settings.ShouldWriteCollection(ObjectType.Annotation))
			{
				writer.WritePropertyName("annotations");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForAnnotation(writer, settings);
				writer.WriteEndObject();
			}
			if (settings.ShouldWriteCollection(ObjectType.ExtendedProperty))
			{
				writer.WritePropertyName("extendedProperties");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForExtendedProperty(writer, settings);
				writer.WriteEndObject();
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001AB6 RID: 6838 RVA: 0x000B1F00 File Offset: 0x000B0100
		public static void WriteSchemaForTable(JsonWriter writer, JsonObjectTreeSchemaWriterSettings settings)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("name");
			JsonSchemaWriter.WriteSchemaForString(writer);
			if (settings.ShouldWriteObject(ObjectType.Table))
			{
				settings.WriteObjectProperties(writer, ObjectType.Table);
			}
			if (settings.ShouldWriteObject(ObjectType.DetailRowsDefinition))
			{
				writer.WritePropertyName("defaultDetailRowsDefinition");
				JsonObjectTreeSchemaWriter.WriteSchemaForDetailRowsDefinition(writer, settings);
			}
			if (settings.ShouldWriteObject(ObjectType.RefreshPolicy))
			{
				writer.WritePropertyName("refreshPolicy");
				JsonObjectTreeSchemaWriter.WriteSchemaForRefreshPolicy(writer, settings);
			}
			if (settings.ShouldWriteObject(ObjectType.CalculationGroup))
			{
				writer.WritePropertyName("calculationGroup");
				JsonObjectTreeSchemaWriter.WriteSchemaForCalculationGroup(writer, settings);
			}
			if (settings.ShouldWriteCollection(ObjectType.Column))
			{
				writer.WritePropertyName("columns");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForColumn(writer, settings);
				writer.WriteEndObject();
			}
			if (settings.ShouldWriteCollection(ObjectType.Measure))
			{
				writer.WritePropertyName("measures");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForMeasure(writer, settings);
				writer.WriteEndObject();
			}
			if (settings.ShouldWriteCollection(ObjectType.Hierarchy))
			{
				writer.WritePropertyName("hierarchies");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForHierarchy(writer, settings);
				writer.WriteEndObject();
			}
			if (settings.ShouldWriteCollection(ObjectType.Set))
			{
				writer.WritePropertyName("sets");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForSet(writer, settings);
				writer.WriteEndObject();
			}
			if (settings.ShouldWriteCollection(ObjectType.Annotation))
			{
				writer.WritePropertyName("annotations");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForAnnotation(writer, settings);
				writer.WriteEndObject();
			}
			if (settings.ShouldWriteCollection(ObjectType.ExtendedProperty))
			{
				writer.WritePropertyName("extendedProperties");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForExtendedProperty(writer, settings);
				writer.WriteEndObject();
			}
			if (settings.ShouldWriteCollection(ObjectType.ExcludedArtifact))
			{
				writer.WritePropertyName("excludedArtifacts");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForExcludedArtifact(writer, settings);
				writer.WriteEndObject();
			}
			if (settings.ShouldWriteCollection(ObjectType.ChangedProperty))
			{
				writer.WritePropertyName("changedProperties");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForChangedProperty(writer, settings);
				writer.WriteEndObject();
			}
			if (settings.ShouldWriteCollection(ObjectType.Calendar))
			{
				writer.WritePropertyName("calendars");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForCalendar(writer, settings);
				writer.WriteEndObject();
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001AB7 RID: 6839 RVA: 0x000B2260 File Offset: 0x000B0460
		public static void WriteSchemaForColumn(JsonWriter writer, JsonObjectTreeSchemaWriterSettings settings)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("name");
			JsonSchemaWriter.WriteSchemaForString(writer);
			if (settings.ShouldWriteObject(ObjectType.Column))
			{
				settings.WriteObjectProperties(writer, ObjectType.Column);
			}
			if (settings.ShouldWriteObject(ObjectType.AttributeHierarchy))
			{
				writer.WritePropertyName("attributeHierarchy");
				JsonObjectTreeSchemaWriter.WriteSchemaForAttributeHierarchy(writer, settings);
			}
			if (settings.ShouldWriteObject(ObjectType.RelatedColumnDetails))
			{
				writer.WritePropertyName("relatedColumnDetails");
				JsonObjectTreeSchemaWriter.WriteSchemaForRelatedColumnDetails(writer, settings);
			}
			if (settings.ShouldWriteObject(ObjectType.AlternateOf))
			{
				writer.WritePropertyName("alternateOf");
				JsonObjectTreeSchemaWriter.WriteSchemaForAlternateOf(writer, settings);
			}
			if (settings.ShouldWriteCollection(ObjectType.Variation))
			{
				writer.WritePropertyName("variations");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForVariation(writer, settings);
				writer.WriteEndObject();
			}
			if (settings.ShouldWriteCollection(ObjectType.Annotation))
			{
				writer.WritePropertyName("annotations");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForAnnotation(writer, settings);
				writer.WriteEndObject();
			}
			if (settings.ShouldWriteCollection(ObjectType.ExtendedProperty))
			{
				writer.WritePropertyName("extendedProperties");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForExtendedProperty(writer, settings);
				writer.WriteEndObject();
			}
			if (settings.ShouldWriteCollection(ObjectType.ChangedProperty))
			{
				writer.WritePropertyName("changedProperties");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForChangedProperty(writer, settings);
				writer.WriteEndObject();
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001AB8 RID: 6840 RVA: 0x000B2454 File Offset: 0x000B0654
		public static void WriteSchemaForAttributeHierarchy(JsonWriter writer, JsonObjectTreeSchemaWriterSettings settings)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			if (settings.ShouldWriteObject(ObjectType.AttributeHierarchy))
			{
				settings.WriteObjectProperties(writer, ObjectType.AttributeHierarchy);
			}
			if (settings.ShouldWriteCollection(ObjectType.Annotation))
			{
				writer.WritePropertyName("annotations");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForAnnotation(writer, settings);
				writer.WriteEndObject();
			}
			if (settings.ShouldWriteCollection(ObjectType.ExtendedProperty))
			{
				writer.WritePropertyName("extendedProperties");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForExtendedProperty(writer, settings);
				writer.WriteEndObject();
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001AB9 RID: 6841 RVA: 0x000B2550 File Offset: 0x000B0750
		public static void WriteSchemaForPartition(JsonWriter writer, JsonObjectTreeSchemaWriterSettings settings)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("name");
			JsonSchemaWriter.WriteSchemaForString(writer);
			if (settings.ShouldWriteObject(ObjectType.Partition))
			{
				settings.WriteObjectProperties(writer, ObjectType.Partition);
			}
			if (settings.ShouldWriteObject(ObjectType.DataCoverageDefinition))
			{
				writer.WritePropertyName("dataCoverageDefinition");
				JsonObjectTreeSchemaWriter.WriteSchemaForDataCoverageDefinition(writer, settings);
			}
			if (settings.ShouldWriteCollection(ObjectType.Annotation))
			{
				writer.WritePropertyName("annotations");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForAnnotation(writer, settings);
				writer.WriteEndObject();
			}
			if (settings.ShouldWriteCollection(ObjectType.ExtendedProperty))
			{
				writer.WritePropertyName("extendedProperties");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForExtendedProperty(writer, settings);
				writer.WriteEndObject();
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001ABA RID: 6842 RVA: 0x000B2678 File Offset: 0x000B0878
		public static void WriteSchemaForRelationship(JsonWriter writer, JsonObjectTreeSchemaWriterSettings settings)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("name");
			JsonSchemaWriter.WriteSchemaForString(writer);
			if (settings.ShouldWriteObject(ObjectType.Relationship))
			{
				settings.WriteObjectProperties(writer, ObjectType.Relationship);
			}
			if (settings.ShouldWriteCollection(ObjectType.Annotation))
			{
				writer.WritePropertyName("annotations");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForAnnotation(writer, settings);
				writer.WriteEndObject();
			}
			if (settings.ShouldWriteCollection(ObjectType.ExtendedProperty))
			{
				writer.WritePropertyName("extendedProperties");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForExtendedProperty(writer, settings);
				writer.WriteEndObject();
			}
			if (settings.ShouldWriteCollection(ObjectType.ChangedProperty))
			{
				writer.WritePropertyName("changedProperties");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForChangedProperty(writer, settings);
				writer.WriteEndObject();
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001ABB RID: 6843 RVA: 0x000B27D0 File Offset: 0x000B09D0
		public static void WriteSchemaForMeasure(JsonWriter writer, JsonObjectTreeSchemaWriterSettings settings)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("name");
			JsonSchemaWriter.WriteSchemaForString(writer);
			if (settings.ShouldWriteObject(ObjectType.Measure))
			{
				settings.WriteObjectProperties(writer, ObjectType.Measure);
			}
			if (settings.ShouldWriteObject(ObjectType.KPI))
			{
				writer.WritePropertyName("kpi");
				JsonObjectTreeSchemaWriter.WriteSchemaForKPI(writer, settings);
			}
			if (settings.ShouldWriteObject(ObjectType.DetailRowsDefinition))
			{
				writer.WritePropertyName("detailRowsDefinition");
				JsonObjectTreeSchemaWriter.WriteSchemaForDetailRowsDefinition(writer, settings);
			}
			if (settings.ShouldWriteObject(ObjectType.FormatStringDefinition))
			{
				writer.WritePropertyName("formatStringDefinition");
				JsonObjectTreeSchemaWriter.WriteSchemaForFormatStringDefinition(writer, settings);
			}
			if (settings.ShouldWriteCollection(ObjectType.Annotation))
			{
				writer.WritePropertyName("annotations");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForAnnotation(writer, settings);
				writer.WriteEndObject();
			}
			if (settings.ShouldWriteCollection(ObjectType.ExtendedProperty))
			{
				writer.WritePropertyName("extendedProperties");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForExtendedProperty(writer, settings);
				writer.WriteEndObject();
			}
			if (settings.ShouldWriteCollection(ObjectType.ChangedProperty))
			{
				writer.WritePropertyName("changedProperties");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForChangedProperty(writer, settings);
				writer.WriteEndObject();
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001ABC RID: 6844 RVA: 0x000B297C File Offset: 0x000B0B7C
		public static void WriteSchemaForHierarchy(JsonWriter writer, JsonObjectTreeSchemaWriterSettings settings)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("name");
			JsonSchemaWriter.WriteSchemaForString(writer);
			if (settings.ShouldWriteObject(ObjectType.Hierarchy))
			{
				settings.WriteObjectProperties(writer, ObjectType.Hierarchy);
			}
			if (settings.ShouldWriteCollection(ObjectType.Level))
			{
				writer.WritePropertyName("levels");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForLevel(writer, settings);
				writer.WriteEndObject();
			}
			if (settings.ShouldWriteCollection(ObjectType.Annotation))
			{
				writer.WritePropertyName("annotations");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForAnnotation(writer, settings);
				writer.WriteEndObject();
			}
			if (settings.ShouldWriteCollection(ObjectType.ExtendedProperty))
			{
				writer.WritePropertyName("extendedProperties");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForExtendedProperty(writer, settings);
				writer.WriteEndObject();
			}
			if (settings.ShouldWriteCollection(ObjectType.ExcludedArtifact))
			{
				writer.WritePropertyName("excludedArtifacts");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForExcludedArtifact(writer, settings);
				writer.WriteEndObject();
			}
			if (settings.ShouldWriteCollection(ObjectType.ChangedProperty))
			{
				writer.WritePropertyName("changedProperties");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForChangedProperty(writer, settings);
				writer.WriteEndObject();
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001ABD RID: 6845 RVA: 0x000B2B68 File Offset: 0x000B0D68
		public static void WriteSchemaForLevel(JsonWriter writer, JsonObjectTreeSchemaWriterSettings settings)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("name");
			JsonSchemaWriter.WriteSchemaForString(writer);
			if (settings.ShouldWriteObject(ObjectType.Level))
			{
				settings.WriteObjectProperties(writer, ObjectType.Level);
			}
			if (settings.ShouldWriteCollection(ObjectType.Annotation))
			{
				writer.WritePropertyName("annotations");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForAnnotation(writer, settings);
				writer.WriteEndObject();
			}
			if (settings.ShouldWriteCollection(ObjectType.ExtendedProperty))
			{
				writer.WritePropertyName("extendedProperties");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForExtendedProperty(writer, settings);
				writer.WriteEndObject();
			}
			if (settings.ShouldWriteCollection(ObjectType.ChangedProperty))
			{
				writer.WritePropertyName("changedProperties");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForChangedProperty(writer, settings);
				writer.WriteEndObject();
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001ABE RID: 6846 RVA: 0x000B2CC0 File Offset: 0x000B0EC0
		public static void WriteSchemaForAnnotation(JsonWriter writer, JsonObjectTreeSchemaWriterSettings settings)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("name");
			JsonSchemaWriter.WriteSchemaForString(writer);
			if (settings.ShouldWriteObject(ObjectType.Annotation))
			{
				settings.WriteObjectProperties(writer, ObjectType.Annotation);
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001ABF RID: 6847 RVA: 0x000B2D3C File Offset: 0x000B0F3C
		public static void WriteSchemaForKPI(JsonWriter writer, JsonObjectTreeSchemaWriterSettings settings)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			if (settings.ShouldWriteObject(ObjectType.KPI))
			{
				settings.WriteObjectProperties(writer, ObjectType.KPI);
			}
			if (settings.ShouldWriteCollection(ObjectType.Annotation))
			{
				writer.WritePropertyName("annotations");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForAnnotation(writer, settings);
				writer.WriteEndObject();
			}
			if (settings.ShouldWriteCollection(ObjectType.ExtendedProperty))
			{
				writer.WritePropertyName("extendedProperties");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForExtendedProperty(writer, settings);
				writer.WriteEndObject();
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001AC0 RID: 6848 RVA: 0x000B2E3C File Offset: 0x000B103C
		public static void WriteSchemaForCulture(JsonWriter writer, JsonObjectTreeSchemaWriterSettings settings)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("name");
			JsonSchemaWriter.WriteSchemaForString(writer);
			if (settings.ShouldWriteObject(ObjectType.Culture))
			{
				settings.WriteObjectProperties(writer, ObjectType.Culture);
			}
			if (settings.ShouldWriteObject(ObjectType.LinguisticMetadata))
			{
				writer.WritePropertyName("linguisticMetadata");
				JsonObjectTreeSchemaWriter.WriteSchemaForLinguisticMetadata(writer, settings);
			}
			if (settings.ShouldWriteCollection(ObjectType.Annotation))
			{
				writer.WritePropertyName("annotations");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForAnnotation(writer, settings);
				writer.WriteEndObject();
			}
			if (settings.ShouldWriteCollection(ObjectType.ExtendedProperty))
			{
				writer.WritePropertyName("extendedProperties");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForExtendedProperty(writer, settings);
				writer.WriteEndObject();
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001AC1 RID: 6849 RVA: 0x000B2F68 File Offset: 0x000B1168
		public static void WriteSchemaForLinguisticMetadata(JsonWriter writer, JsonObjectTreeSchemaWriterSettings settings)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			if (settings.ShouldWriteObject(ObjectType.LinguisticMetadata))
			{
				settings.WriteObjectProperties(writer, ObjectType.LinguisticMetadata);
			}
			if (settings.ShouldWriteCollection(ObjectType.Annotation))
			{
				writer.WritePropertyName("annotations");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForAnnotation(writer, settings);
				writer.WriteEndObject();
			}
			if (settings.ShouldWriteCollection(ObjectType.ExtendedProperty))
			{
				writer.WritePropertyName("extendedProperties");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForExtendedProperty(writer, settings);
				writer.WriteEndObject();
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001AC2 RID: 6850 RVA: 0x000B3068 File Offset: 0x000B1268
		public static void WriteSchemaForPerspective(JsonWriter writer, JsonObjectTreeSchemaWriterSettings settings)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("name");
			JsonSchemaWriter.WriteSchemaForString(writer);
			if (settings.ShouldWriteObject(ObjectType.Perspective))
			{
				settings.WriteObjectProperties(writer, ObjectType.Perspective);
			}
			if (settings.ShouldWriteCollection(ObjectType.PerspectiveTable))
			{
				writer.WritePropertyName("tables");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForPerspectiveTable(writer, settings);
				writer.WriteEndObject();
			}
			if (settings.ShouldWriteCollection(ObjectType.Annotation))
			{
				writer.WritePropertyName("annotations");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForAnnotation(writer, settings);
				writer.WriteEndObject();
			}
			if (settings.ShouldWriteCollection(ObjectType.ExtendedProperty))
			{
				writer.WritePropertyName("extendedProperties");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForExtendedProperty(writer, settings);
				writer.WriteEndObject();
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001AC3 RID: 6851 RVA: 0x000B31C0 File Offset: 0x000B13C0
		public static void WriteSchemaForPerspectiveTable(JsonWriter writer, JsonObjectTreeSchemaWriterSettings settings)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("name");
			JsonSchemaWriter.WriteSchemaForString(writer);
			if (settings.ShouldWriteObject(ObjectType.PerspectiveTable))
			{
				settings.WriteObjectProperties(writer, ObjectType.PerspectiveTable);
			}
			if (settings.ShouldWriteCollection(ObjectType.PerspectiveColumn))
			{
				writer.WritePropertyName("columns");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForPerspectiveColumn(writer, settings);
				writer.WriteEndObject();
			}
			if (settings.ShouldWriteCollection(ObjectType.PerspectiveMeasure))
			{
				writer.WritePropertyName("measures");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForPerspectiveMeasure(writer, settings);
				writer.WriteEndObject();
			}
			if (settings.ShouldWriteCollection(ObjectType.PerspectiveHierarchy))
			{
				writer.WritePropertyName("hierarchies");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForPerspectiveHierarchy(writer, settings);
				writer.WriteEndObject();
			}
			if (settings.ShouldWriteCollection(ObjectType.PerspectiveSet))
			{
				writer.WritePropertyName("sets");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForPerspectiveSet(writer, settings);
				writer.WriteEndObject();
			}
			if (settings.ShouldWriteCollection(ObjectType.Annotation))
			{
				writer.WritePropertyName("annotations");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForAnnotation(writer, settings);
				writer.WriteEndObject();
			}
			if (settings.ShouldWriteCollection(ObjectType.ExtendedProperty))
			{
				writer.WritePropertyName("extendedProperties");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForExtendedProperty(writer, settings);
				writer.WriteEndObject();
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001AC4 RID: 6852 RVA: 0x000B33F4 File Offset: 0x000B15F4
		public static void WriteSchemaForPerspectiveColumn(JsonWriter writer, JsonObjectTreeSchemaWriterSettings settings)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("name");
			JsonSchemaWriter.WriteSchemaForString(writer);
			if (settings.ShouldWriteObject(ObjectType.PerspectiveColumn))
			{
				settings.WriteObjectProperties(writer, ObjectType.PerspectiveColumn);
			}
			if (settings.ShouldWriteCollection(ObjectType.Annotation))
			{
				writer.WritePropertyName("annotations");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForAnnotation(writer, settings);
				writer.WriteEndObject();
			}
			if (settings.ShouldWriteCollection(ObjectType.ExtendedProperty))
			{
				writer.WritePropertyName("extendedProperties");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForExtendedProperty(writer, settings);
				writer.WriteEndObject();
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001AC5 RID: 6853 RVA: 0x000B3504 File Offset: 0x000B1704
		public static void WriteSchemaForPerspectiveHierarchy(JsonWriter writer, JsonObjectTreeSchemaWriterSettings settings)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("name");
			JsonSchemaWriter.WriteSchemaForString(writer);
			if (settings.ShouldWriteObject(ObjectType.PerspectiveHierarchy))
			{
				settings.WriteObjectProperties(writer, ObjectType.PerspectiveHierarchy);
			}
			if (settings.ShouldWriteCollection(ObjectType.Annotation))
			{
				writer.WritePropertyName("annotations");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForAnnotation(writer, settings);
				writer.WriteEndObject();
			}
			if (settings.ShouldWriteCollection(ObjectType.ExtendedProperty))
			{
				writer.WritePropertyName("extendedProperties");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForExtendedProperty(writer, settings);
				writer.WriteEndObject();
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001AC6 RID: 6854 RVA: 0x000B3614 File Offset: 0x000B1814
		public static void WriteSchemaForPerspectiveMeasure(JsonWriter writer, JsonObjectTreeSchemaWriterSettings settings)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("name");
			JsonSchemaWriter.WriteSchemaForString(writer);
			if (settings.ShouldWriteObject(ObjectType.PerspectiveMeasure))
			{
				settings.WriteObjectProperties(writer, ObjectType.PerspectiveMeasure);
			}
			if (settings.ShouldWriteCollection(ObjectType.Annotation))
			{
				writer.WritePropertyName("annotations");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForAnnotation(writer, settings);
				writer.WriteEndObject();
			}
			if (settings.ShouldWriteCollection(ObjectType.ExtendedProperty))
			{
				writer.WritePropertyName("extendedProperties");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForExtendedProperty(writer, settings);
				writer.WriteEndObject();
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001AC7 RID: 6855 RVA: 0x000B3724 File Offset: 0x000B1924
		public static void WriteSchemaForModelRole(JsonWriter writer, JsonObjectTreeSchemaWriterSettings settings)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("name");
			JsonSchemaWriter.WriteSchemaForString(writer);
			if (settings.ShouldWriteObject(ObjectType.Role))
			{
				settings.WriteObjectProperties(writer, ObjectType.Role);
			}
			if (settings.ShouldWriteCollection(ObjectType.RoleMembership))
			{
				writer.WritePropertyName("members");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForModelRoleMember(writer, settings);
				writer.WriteEndObject();
			}
			if (settings.ShouldWriteCollection(ObjectType.TablePermission))
			{
				writer.WritePropertyName("tablePermissions");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForTablePermission(writer, settings);
				writer.WriteEndObject();
			}
			if (settings.ShouldWriteCollection(ObjectType.Annotation))
			{
				writer.WritePropertyName("annotations");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForAnnotation(writer, settings);
				writer.WriteEndObject();
			}
			if (settings.ShouldWriteCollection(ObjectType.ExtendedProperty))
			{
				writer.WritePropertyName("extendedProperties");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForExtendedProperty(writer, settings);
				writer.WriteEndObject();
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001AC8 RID: 6856 RVA: 0x000B38C4 File Offset: 0x000B1AC4
		public static void WriteSchemaForModelRoleMember(JsonWriter writer, JsonObjectTreeSchemaWriterSettings settings)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("name");
			JsonSchemaWriter.WriteSchemaForString(writer);
			if (settings.ShouldWriteObject(ObjectType.RoleMembership))
			{
				settings.WriteObjectProperties(writer, ObjectType.RoleMembership);
			}
			if (settings.ShouldWriteCollection(ObjectType.Annotation))
			{
				writer.WritePropertyName("annotations");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForAnnotation(writer, settings);
				writer.WriteEndObject();
			}
			if (settings.ShouldWriteCollection(ObjectType.ExtendedProperty))
			{
				writer.WritePropertyName("extendedProperties");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForExtendedProperty(writer, settings);
				writer.WriteEndObject();
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001AC9 RID: 6857 RVA: 0x000B39D4 File Offset: 0x000B1BD4
		public static void WriteSchemaForTablePermission(JsonWriter writer, JsonObjectTreeSchemaWriterSettings settings)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("name");
			JsonSchemaWriter.WriteSchemaForString(writer);
			if (settings.ShouldWriteObject(ObjectType.TablePermission))
			{
				settings.WriteObjectProperties(writer, ObjectType.TablePermission);
			}
			if (settings.ShouldWriteCollection(ObjectType.Annotation))
			{
				writer.WritePropertyName("annotations");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForAnnotation(writer, settings);
				writer.WriteEndObject();
			}
			if (settings.ShouldWriteCollection(ObjectType.ExtendedProperty))
			{
				writer.WritePropertyName("extendedProperties");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForExtendedProperty(writer, settings);
				writer.WriteEndObject();
			}
			if (settings.ShouldWriteCollection(ObjectType.ColumnPermission))
			{
				writer.WritePropertyName("columnPermissions");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForColumnPermission(writer, settings);
				writer.WriteEndObject();
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001ACA RID: 6858 RVA: 0x000B3B2C File Offset: 0x000B1D2C
		public static void WriteSchemaForVariation(JsonWriter writer, JsonObjectTreeSchemaWriterSettings settings)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("name");
			JsonSchemaWriter.WriteSchemaForString(writer);
			if (settings.ShouldWriteObject(ObjectType.Variation))
			{
				settings.WriteObjectProperties(writer, ObjectType.Variation);
			}
			if (settings.ShouldWriteCollection(ObjectType.Annotation))
			{
				writer.WritePropertyName("annotations");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForAnnotation(writer, settings);
				writer.WriteEndObject();
			}
			if (settings.ShouldWriteCollection(ObjectType.ExtendedProperty))
			{
				writer.WritePropertyName("extendedProperties");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForExtendedProperty(writer, settings);
				writer.WriteEndObject();
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001ACB RID: 6859 RVA: 0x000B3C3C File Offset: 0x000B1E3C
		public static void WriteSchemaForSet(JsonWriter writer, JsonObjectTreeSchemaWriterSettings settings)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("name");
			JsonSchemaWriter.WriteSchemaForString(writer);
			if (settings.ShouldWriteObject(ObjectType.Set))
			{
				settings.WriteObjectProperties(writer, ObjectType.Set);
			}
			if (settings.ShouldWriteCollection(ObjectType.Annotation))
			{
				writer.WritePropertyName("annotations");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForAnnotation(writer, settings);
				writer.WriteEndObject();
			}
			if (settings.ShouldWriteCollection(ObjectType.ExtendedProperty))
			{
				writer.WritePropertyName("extendedProperties");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForExtendedProperty(writer, settings);
				writer.WriteEndObject();
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001ACC RID: 6860 RVA: 0x000B3D4C File Offset: 0x000B1F4C
		public static void WriteSchemaForPerspectiveSet(JsonWriter writer, JsonObjectTreeSchemaWriterSettings settings)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("name");
			JsonSchemaWriter.WriteSchemaForString(writer);
			if (settings.ShouldWriteObject(ObjectType.PerspectiveSet))
			{
				settings.WriteObjectProperties(writer, ObjectType.PerspectiveSet);
			}
			if (settings.ShouldWriteCollection(ObjectType.Annotation))
			{
				writer.WritePropertyName("annotations");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForAnnotation(writer, settings);
				writer.WriteEndObject();
			}
			if (settings.ShouldWriteCollection(ObjectType.ExtendedProperty))
			{
				writer.WritePropertyName("extendedProperties");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForExtendedProperty(writer, settings);
				writer.WriteEndObject();
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001ACD RID: 6861 RVA: 0x000B3E5C File Offset: 0x000B205C
		public static void WriteSchemaForExtendedProperty(JsonWriter writer, JsonObjectTreeSchemaWriterSettings settings)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("name");
			JsonSchemaWriter.WriteSchemaForString(writer);
			if (settings.ShouldWriteObject(ObjectType.ExtendedProperty))
			{
				settings.WriteObjectProperties(writer, ObjectType.ExtendedProperty);
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001ACE RID: 6862 RVA: 0x000B3ED8 File Offset: 0x000B20D8
		public static void WriteSchemaForNamedExpression(JsonWriter writer, JsonObjectTreeSchemaWriterSettings settings)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("name");
			JsonSchemaWriter.WriteSchemaForString(writer);
			if (settings.ShouldWriteObject(ObjectType.Expression))
			{
				settings.WriteObjectProperties(writer, ObjectType.Expression);
			}
			if (settings.ShouldWriteCollection(ObjectType.Annotation))
			{
				writer.WritePropertyName("annotations");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForAnnotation(writer, settings);
				writer.WriteEndObject();
			}
			if (settings.ShouldWriteCollection(ObjectType.ExtendedProperty))
			{
				writer.WritePropertyName("extendedProperties");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForExtendedProperty(writer, settings);
				writer.WriteEndObject();
			}
			if (settings.ShouldWriteCollection(ObjectType.ExcludedArtifact))
			{
				writer.WritePropertyName("excludedArtifacts");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForExcludedArtifact(writer, settings);
				writer.WriteEndObject();
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001ACF RID: 6863 RVA: 0x000B4030 File Offset: 0x000B2230
		public static void WriteSchemaForColumnPermission(JsonWriter writer, JsonObjectTreeSchemaWriterSettings settings)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("name");
			JsonSchemaWriter.WriteSchemaForString(writer);
			if (settings.ShouldWriteObject(ObjectType.ColumnPermission))
			{
				settings.WriteObjectProperties(writer, ObjectType.ColumnPermission);
			}
			if (settings.ShouldWriteCollection(ObjectType.Annotation))
			{
				writer.WritePropertyName("annotations");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForAnnotation(writer, settings);
				writer.WriteEndObject();
			}
			if (settings.ShouldWriteCollection(ObjectType.ExtendedProperty))
			{
				writer.WritePropertyName("extendedProperties");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForExtendedProperty(writer, settings);
				writer.WriteEndObject();
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001AD0 RID: 6864 RVA: 0x000B4140 File Offset: 0x000B2340
		public static void WriteSchemaForDetailRowsDefinition(JsonWriter writer, JsonObjectTreeSchemaWriterSettings settings)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			if (settings.ShouldWriteObject(ObjectType.DetailRowsDefinition))
			{
				settings.WriteObjectProperties(writer, ObjectType.DetailRowsDefinition);
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001AD1 RID: 6865 RVA: 0x000B41AC File Offset: 0x000B23AC
		public static void WriteSchemaForRelatedColumnDetails(JsonWriter writer, JsonObjectTreeSchemaWriterSettings settings)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			if (settings.ShouldWriteObject(ObjectType.RelatedColumnDetails))
			{
				settings.WriteObjectProperties(writer, ObjectType.RelatedColumnDetails);
			}
			if (settings.ShouldWriteCollection(ObjectType.GroupByColumn))
			{
				writer.WritePropertyName("groupByColumns");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForGroupByColumn(writer, settings);
				writer.WriteEndObject();
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001AD2 RID: 6866 RVA: 0x000B4260 File Offset: 0x000B2460
		public static void WriteSchemaForGroupByColumn(JsonWriter writer, JsonObjectTreeSchemaWriterSettings settings)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			if (settings.ShouldWriteObject(ObjectType.GroupByColumn))
			{
				settings.WriteObjectProperties(writer, ObjectType.GroupByColumn);
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001AD3 RID: 6867 RVA: 0x000B42CC File Offset: 0x000B24CC
		public static void WriteSchemaForCalculationGroup(JsonWriter writer, JsonObjectTreeSchemaWriterSettings settings)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			if (settings.ShouldWriteObject(ObjectType.CalculationGroup))
			{
				settings.WriteObjectProperties(writer, ObjectType.CalculationGroup);
			}
			if (settings.ShouldWriteObject(ObjectType.CalculationExpression))
			{
				writer.WritePropertyName("multipleOrEmptySelectionExpression");
				JsonObjectTreeSchemaWriter.WriteSchemaForCalculationGroupExpression(writer, settings);
				writer.WritePropertyName("noSelectionExpression");
				JsonObjectTreeSchemaWriter.WriteSchemaForCalculationGroupExpression(writer, settings);
			}
			if (settings.ShouldWriteCollection(ObjectType.Annotation))
			{
				writer.WritePropertyName("annotations");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForAnnotation(writer, settings);
				writer.WriteEndObject();
			}
			if (settings.ShouldWriteCollection(ObjectType.CalculationItem))
			{
				writer.WritePropertyName("calculationItems");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForCalculationItem(writer, settings);
				writer.WriteEndObject();
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001AD4 RID: 6868 RVA: 0x000B43F8 File Offset: 0x000B25F8
		public static void WriteSchemaForCalculationItem(JsonWriter writer, JsonObjectTreeSchemaWriterSettings settings)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("name");
			JsonSchemaWriter.WriteSchemaForString(writer);
			if (settings.ShouldWriteObject(ObjectType.CalculationItem))
			{
				settings.WriteObjectProperties(writer, ObjectType.CalculationItem);
			}
			if (settings.ShouldWriteObject(ObjectType.FormatStringDefinition))
			{
				writer.WritePropertyName("formatStringDefinition");
				JsonObjectTreeSchemaWriter.WriteSchemaForFormatStringDefinition(writer, settings);
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001AD5 RID: 6869 RVA: 0x000B4490 File Offset: 0x000B2690
		public static void WriteSchemaForAlternateOf(JsonWriter writer, JsonObjectTreeSchemaWriterSettings settings)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			if (settings.ShouldWriteObject(ObjectType.AlternateOf))
			{
				settings.WriteObjectProperties(writer, ObjectType.AlternateOf);
			}
			if (settings.ShouldWriteCollection(ObjectType.Annotation))
			{
				writer.WritePropertyName("annotations");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForAnnotation(writer, settings);
				writer.WriteEndObject();
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001AD6 RID: 6870 RVA: 0x000B4544 File Offset: 0x000B2744
		public static void WriteSchemaForRefreshPolicy(JsonWriter writer, JsonObjectTreeSchemaWriterSettings settings)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			if (settings.ShouldWriteObject(ObjectType.RefreshPolicy))
			{
				settings.WriteObjectProperties(writer, ObjectType.RefreshPolicy);
			}
			if (settings.ShouldWriteCollection(ObjectType.Annotation))
			{
				writer.WritePropertyName("annotations");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForAnnotation(writer, settings);
				writer.WriteEndObject();
			}
			if (settings.ShouldWriteCollection(ObjectType.ExtendedProperty))
			{
				writer.WritePropertyName("extendedProperties");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForExtendedProperty(writer, settings);
				writer.WriteEndObject();
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001AD7 RID: 6871 RVA: 0x000B4644 File Offset: 0x000B2844
		public static void WriteSchemaForFormatStringDefinition(JsonWriter writer, JsonObjectTreeSchemaWriterSettings settings)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			if (settings.ShouldWriteObject(ObjectType.FormatStringDefinition))
			{
				settings.WriteObjectProperties(writer, ObjectType.FormatStringDefinition);
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001AD8 RID: 6872 RVA: 0x000B46B0 File Offset: 0x000B28B0
		public static void WriteSchemaForQueryGroup(JsonWriter writer, JsonObjectTreeSchemaWriterSettings settings)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("name");
			JsonSchemaWriter.WriteSchemaForString(writer);
			if (settings.ShouldWriteObject(ObjectType.QueryGroup))
			{
				settings.WriteObjectProperties(writer, ObjectType.QueryGroup);
			}
			if (settings.ShouldWriteCollection(ObjectType.Annotation))
			{
				writer.WritePropertyName("annotations");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForAnnotation(writer, settings);
				writer.WriteEndObject();
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001AD9 RID: 6873 RVA: 0x000B4778 File Offset: 0x000B2978
		public static void WriteSchemaForAnalyticsAIMetadata(JsonWriter writer, JsonObjectTreeSchemaWriterSettings settings)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("name");
			JsonSchemaWriter.WriteSchemaForString(writer);
			if (settings.ShouldWriteObject(ObjectType.AnalyticsAIMetadata))
			{
				settings.WriteObjectProperties(writer, ObjectType.AnalyticsAIMetadata);
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001ADA RID: 6874 RVA: 0x000B47F4 File Offset: 0x000B29F4
		public static void WriteSchemaForChangedProperty(JsonWriter writer, JsonObjectTreeSchemaWriterSettings settings)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			if (settings.ShouldWriteObject(ObjectType.ChangedProperty))
			{
				settings.WriteObjectProperties(writer, ObjectType.ChangedProperty);
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001ADB RID: 6875 RVA: 0x000B4860 File Offset: 0x000B2A60
		public static void WriteSchemaForExcludedArtifact(JsonWriter writer, JsonObjectTreeSchemaWriterSettings settings)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			if (settings.ShouldWriteObject(ObjectType.ExcludedArtifact))
			{
				settings.WriteObjectProperties(writer, ObjectType.ExcludedArtifact);
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001ADC RID: 6876 RVA: 0x000B48CC File Offset: 0x000B2ACC
		public static void WriteSchemaForDataCoverageDefinition(JsonWriter writer, JsonObjectTreeSchemaWriterSettings settings)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			if (settings.ShouldWriteObject(ObjectType.DataCoverageDefinition))
			{
				settings.WriteObjectProperties(writer, ObjectType.DataCoverageDefinition);
			}
			if (settings.ShouldWriteCollection(ObjectType.Annotation))
			{
				writer.WritePropertyName("annotations");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForAnnotation(writer, settings);
				writer.WriteEndObject();
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001ADD RID: 6877 RVA: 0x000B4980 File Offset: 0x000B2B80
		public static void WriteSchemaForCalculationGroupExpression(JsonWriter writer, JsonObjectTreeSchemaWriterSettings settings)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			if (settings.ShouldWriteObject(ObjectType.CalculationExpression))
			{
				settings.WriteObjectProperties(writer, ObjectType.CalculationExpression);
			}
			if (settings.ShouldWriteObject(ObjectType.FormatStringDefinition))
			{
				writer.WritePropertyName("formatStringDefinition");
				JsonObjectTreeSchemaWriter.WriteSchemaForFormatStringDefinition(writer, settings);
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001ADE RID: 6878 RVA: 0x000B4A08 File Offset: 0x000B2C08
		public static void WriteSchemaForCalendar(JsonWriter writer, JsonObjectTreeSchemaWriterSettings settings)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("name");
			JsonSchemaWriter.WriteSchemaForString(writer);
			if (settings.ShouldWriteObject(ObjectType.Calendar))
			{
				settings.WriteObjectProperties(writer, ObjectType.Calendar);
			}
			if (settings.ShouldWriteCollection(ObjectType.TimeUnitColumnAssociation))
			{
				writer.WritePropertyName("timeUnitColumnAssociations");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForTimeUnitColumnAssociation(writer, settings);
				writer.WriteEndObject();
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001ADF RID: 6879 RVA: 0x000B4AD0 File Offset: 0x000B2CD0
		public static void WriteSchemaForTimeUnitColumnAssociation(JsonWriter writer, JsonObjectTreeSchemaWriterSettings settings)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("name");
			JsonSchemaWriter.WriteSchemaForString(writer);
			if (settings.ShouldWriteObject(ObjectType.TimeUnitColumnAssociation))
			{
				settings.WriteObjectProperties(writer, ObjectType.TimeUnitColumnAssociation);
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001AE0 RID: 6880 RVA: 0x000B4B4C File Offset: 0x000B2D4C
		public static void WriteSchemaForFunction(JsonWriter writer, JsonObjectTreeSchemaWriterSettings settings)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("name");
			JsonSchemaWriter.WriteSchemaForString(writer);
			if (settings.ShouldWriteObject(ObjectType.Function))
			{
				settings.WriteObjectProperties(writer, ObjectType.Function);
			}
			if (settings.ShouldWriteCollection(ObjectType.Annotation))
			{
				writer.WritePropertyName("annotations");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForAnnotation(writer, settings);
				writer.WriteEndObject();
			}
			if (settings.ShouldWriteCollection(ObjectType.ExtendedProperty))
			{
				writer.WritePropertyName("extendedProperties");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForExtendedProperty(writer, settings);
				writer.WriteEndObject();
			}
			if (settings.ShouldWriteCollection(ObjectType.ChangedProperty))
			{
				writer.WritePropertyName("changedProperties");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForChangedProperty(writer, settings);
				writer.WriteEndObject();
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001AE1 RID: 6881 RVA: 0x000B4CA4 File Offset: 0x000B2EA4
		public static void WriteSchemaForBindingInfo(JsonWriter writer, JsonObjectTreeSchemaWriterSettings settings)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("name");
			JsonSchemaWriter.WriteSchemaForString(writer);
			if (settings.ShouldWriteObject(ObjectType.BindingInfo))
			{
				settings.WriteObjectProperties(writer, ObjectType.BindingInfo);
			}
			if (settings.ShouldWriteCollection(ObjectType.Annotation))
			{
				writer.WritePropertyName("annotations");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForAnnotation(writer, settings);
				writer.WriteEndObject();
			}
			if (settings.ShouldWriteCollection(ObjectType.ExtendedProperty))
			{
				writer.WritePropertyName("extendedProperties");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonObjectTreeSchemaWriter.WriteSchemaForExtendedProperty(writer, settings);
				writer.WriteEndObject();
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}
	}
}
