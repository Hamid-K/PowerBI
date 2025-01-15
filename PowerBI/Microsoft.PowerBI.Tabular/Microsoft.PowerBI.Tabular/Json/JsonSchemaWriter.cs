using System;
using Microsoft.AnalysisServices.Tabular.Extensions;
using Microsoft.AnalysisServices.Tabular.Metadata;
using Microsoft.AnalysisServices.Tabular.Serialization;
using Microsoft.AnalysisServices.Tabular.Utilities;

namespace Microsoft.AnalysisServices.Tabular.Json
{
	// Token: 0x020001BC RID: 444
	internal static class JsonSchemaWriter
	{
		// Token: 0x06001B63 RID: 7011 RVA: 0x000B929C File Offset: 0x000B749C
		private static void WriteSchemaForPartitionSource(JsonWriter writer, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("anyOf");
			writer.WriteStartArray();
			JsonSchemaWriter.WriteSchemaForQueryPartitionSource(writer, options, mode, dbCompatibilityLevel);
			JsonSchemaWriter.WriteSchemaForCalculatedPartitionSource(writer, options, mode, dbCompatibilityLevel);
			JsonSchemaWriter.WriteSchemaForNonePartitionSource(writer, options, mode, dbCompatibilityLevel);
			if (CompatibilityRestrictions.PartitionSourceType_M.IsCompatible(mode, dbCompatibilityLevel))
			{
				JsonSchemaWriter.WriteSchemaForMPartitionSource(writer, options, mode, dbCompatibilityLevel);
			}
			if (CompatibilityRestrictions.PartitionSourceType_Entity.IsCompatible(mode, dbCompatibilityLevel))
			{
				JsonSchemaWriter.WriteSchemaForEntityPartitionSource(writer, options, mode, dbCompatibilityLevel);
			}
			if (CompatibilityRestrictions.PartitionSourceType_PolicyRange.IsCompatible(mode, dbCompatibilityLevel))
			{
				JsonSchemaWriter.WriteSchemaForPolicyRangePartitionSource(writer, options, mode, dbCompatibilityLevel);
			}
			if (CompatibilityRestrictions.PartitionSourceType_CalculationGroup.IsCompatible(mode, dbCompatibilityLevel))
			{
				JsonSchemaWriter.WriteSchemaForCalculationGroupPartitionSource(writer, options, mode, dbCompatibilityLevel);
			}
			if (CompatibilityRestrictions.PartitionSourceType_Inferred.IsCompatible(mode, dbCompatibilityLevel))
			{
				JsonSchemaWriter.WriteSchemaForInferredPartitionSource(writer, options, mode, dbCompatibilityLevel);
			}
			if (CompatibilityRestrictions.PartitionSourceType_Parquet.IsCompatible(mode, dbCompatibilityLevel))
			{
				JsonSchemaWriter.WriteSchemaForParquetPartitionSource(writer, options, mode, dbCompatibilityLevel);
			}
			writer.WriteEndArray();
			writer.WriteEndObject();
		}

		// Token: 0x06001B64 RID: 7012 RVA: 0x000B9374 File Offset: 0x000B7574
		private static void WriteSchemaForPartitionSourceType(JsonWriter writer, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WritePropertyName("type");
			writer.WriteStartObject();
			writer.WritePropertyName("enum");
			writer.WriteStartArray();
			writer.WriteValue("query");
			writer.WriteValue("calculated");
			writer.WriteValue("none");
			if (CompatibilityRestrictions.PartitionSourceType_M.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WriteValue("m");
			}
			if (CompatibilityRestrictions.PartitionSourceType_Entity.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WriteValue("entity");
			}
			if (CompatibilityRestrictions.PartitionSourceType_PolicyRange.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WriteValue("policyRange");
			}
			if (CompatibilityRestrictions.PartitionSourceType_CalculationGroup.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WriteValue("calculationGroup");
			}
			if (CompatibilityRestrictions.PartitionSourceType_Inferred.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WriteValue("inferred");
			}
			if (CompatibilityRestrictions.PartitionSourceType_Parquet.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WriteValue("parquet");
			}
			writer.WriteEndArray();
			writer.WriteEndObject();
		}

		// Token: 0x06001B65 RID: 7013 RVA: 0x000B9468 File Offset: 0x000B7668
		private static void WriteSchemaForQueryPartitionSource(JsonWriter writer, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("description");
			writer.WriteValue("QueryPartitionSource object of Tabular Object Model (TOM)");
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			JsonSchemaWriter.WriteSchemaForPartitionSourceType(writer, options, mode, dbCompatibilityLevel);
			writer.WritePropertyName("query");
			if (options.SplitMultilineStrings)
			{
				JsonSchemaWriter.WriteSchemaForMultilineString(writer);
			}
			else
			{
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			writer.WritePropertyName("dataSource");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001B66 RID: 7014 RVA: 0x000B9514 File Offset: 0x000B7714
		private static void WriteSchemaForCalculatedPartitionSource(JsonWriter writer, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("description");
			writer.WriteValue("CalculatedPartitionSource object of Tabular Object Model (TOM)");
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			JsonSchemaWriter.WriteSchemaForPartitionSourceType(writer, options, mode, dbCompatibilityLevel);
			writer.WritePropertyName("expression");
			if (options.SplitMultilineStrings)
			{
				JsonSchemaWriter.WriteSchemaForMultilineString(writer);
			}
			else
			{
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			if (CompatibilityRestrictions.Partition_RetainDataTillForceCalculate.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("retainDataTillForceCalculate");
				JsonSchemaWriter.WriteSchemaForBoolean(writer);
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001B67 RID: 7015 RVA: 0x000B95CC File Offset: 0x000B77CC
		private static void WriteSchemaForNonePartitionSource(JsonWriter writer, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("description");
			writer.WriteValue("Empty PartitionSource object of Tabular Object Model (TOM)");
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			JsonSchemaWriter.WriteSchemaForPartitionSourceType(writer, options, mode, dbCompatibilityLevel);
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001B68 RID: 7016 RVA: 0x000B9644 File Offset: 0x000B7844
		private static void WriteSchemaForMPartitionSource(JsonWriter writer, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("description");
			writer.WriteValue("MPartitionSource object of Tabular Object Model (TOM)");
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			JsonSchemaWriter.WriteSchemaForPartitionSourceType(writer, options, mode, dbCompatibilityLevel);
			writer.WritePropertyName("expression");
			if (options.SplitMultilineStrings)
			{
				JsonSchemaWriter.WriteSchemaForMultilineString(writer);
			}
			else
			{
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			if (CompatibilityRestrictions.Partition_MAttributes.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("attributes");
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001B69 RID: 7017 RVA: 0x000B96FC File Offset: 0x000B78FC
		private static void WriteSchemaForEntityPartitionSource(JsonWriter writer, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("description");
			writer.WriteValue("EntityPartitionSource object of Tabular Object Model (TOM)");
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			JsonSchemaWriter.WriteSchemaForPartitionSourceType(writer, options, mode, dbCompatibilityLevel);
			writer.WritePropertyName("entityName");
			if (options.SplitMultilineStrings)
			{
				JsonSchemaWriter.WriteSchemaForMultilineString(writer);
			}
			else
			{
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			writer.WritePropertyName("dataSource");
			JsonSchemaWriter.WriteSchemaForString(writer);
			if (CompatibilityRestrictions.Partition_ExpressionSource.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("expressionSource");
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			if (CompatibilityRestrictions.Partition_SchemaName.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("schemaName");
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001B6A RID: 7018 RVA: 0x000B97E4 File Offset: 0x000B79E4
		private static void WriteSchemaForPolicyRangePartitionSource(JsonWriter writer, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("description");
			writer.WriteValue("PolicyRangePartitionSource object of Tabular Object Model (TOM)");
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			JsonSchemaWriter.WriteSchemaForPartitionSourceType(writer, options, mode, dbCompatibilityLevel);
			writer.WritePropertyName("start");
			JsonSchemaWriter.WriteSchemaForDateTime(writer);
			writer.WritePropertyName("end");
			JsonSchemaWriter.WriteSchemaForDateTime(writer);
			writer.WritePropertyName("granularity");
			writer.WriteStartObject();
			writer.WritePropertyName("enum");
			writer.WriteStartArray();
			writer.WriteValue("day");
			writer.WriteValue("month");
			writer.WriteValue("quarter");
			writer.WriteValue("year");
			writer.WriteEndArray();
			writer.WriteEndObject();
			writer.WritePropertyName("refreshBookmark");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001B6B RID: 7019 RVA: 0x000B98E8 File Offset: 0x000B7AE8
		private static void WriteSchemaForCalculationGroupPartitionSource(JsonWriter writer, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("description");
			writer.WriteValue("CalculationGroupPartitionSource object of Tabular Object Model (TOM)");
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			JsonSchemaWriter.WriteSchemaForPartitionSourceType(writer, options, mode, dbCompatibilityLevel);
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001B6C RID: 7020 RVA: 0x000B9960 File Offset: 0x000B7B60
		private static void WriteSchemaForInferredPartitionSource(JsonWriter writer, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("description");
			writer.WriteValue("InferredPartitionSource object of Tabular Object Model (TOM)");
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			JsonSchemaWriter.WriteSchemaForPartitionSourceType(writer, options, mode, dbCompatibilityLevel);
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001B6D RID: 7021 RVA: 0x000B99D8 File Offset: 0x000B7BD8
		private static void WriteSchemaForParquetPartitionSource(JsonWriter writer, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("description");
			writer.WriteValue("ParquetPartitionSource object of Tabular Object Model (TOM)");
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			JsonSchemaWriter.WriteSchemaForPartitionSourceType(writer, options, mode, dbCompatibilityLevel);
			writer.WritePropertyName("location");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001B6E RID: 7022 RVA: 0x000B9A60 File Offset: 0x000B7C60
		private static void WriteSchemaForObjectTranslations(JsonWriter writer, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			JsonObjectTreeSchemaWriterSettings jsonObjectTreeSchemaWriterSettings = new JsonObjectTreeSchemaWriterSettings(new JsonObjectTreeSchemaWriterSettings.WriteObjectPropertiesMethod(JsonSchemaWriter.WriteSchemaPropertiesForObjectTranslations), options, mode, dbCompatibilityLevel)
			{
				WriteObjectFilter = new Predicate<ObjectType>(ObjectTreeHelper.HasTranslatableDescendants),
				WriteCollectionFilter = new Predicate<ObjectType>(ObjectTreeHelper.HasTranslatableDescendants)
			};
			writer.WriteStartObject();
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("model");
			JsonObjectTreeSchemaWriter.WriteSchemaForModel(writer, jsonObjectTreeSchemaWriterSettings);
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001B6F RID: 7023 RVA: 0x000B9B04 File Offset: 0x000B7D04
		private static void WriteSchemaPropertiesForObjectTranslations(JsonWriter writer, ObjectType type, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			foreach (TranslatablePropertyInfo translatablePropertyInfo in ObjectTreeHelper.GetTranslatedProperties(type, mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName(JsonPropertyName.Misc.GetTranslatedPropertyName(translatablePropertyInfo.Property));
				if (translatablePropertyInfo.IsMultiline && options.SplitMultilineStrings)
				{
					JsonSchemaWriter.WriteSchemaForMultilineString(writer);
				}
				else
				{
					JsonSchemaWriter.WriteSchemaForString(writer);
				}
			}
		}

		// Token: 0x06001B70 RID: 7024 RVA: 0x000B9B7C File Offset: 0x000B7D7C
		internal static void WriteSchemaForDatabase(JsonWriter writer, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("description");
			writer.WriteValue("Database object of Tabular Object Model (TOM)");
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("name");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WritePropertyName("id");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WritePropertyName("description");
			if (options.SplitMultilineStrings)
			{
				JsonSchemaWriter.WriteSchemaForMultilineString(writer);
			}
			else
			{
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			writer.WritePropertyName("compatibilityLevel");
			JsonSchemaWriter.WriteSchemaForInteger(writer);
			writer.WritePropertyName("readWriteMode");
			JsonSchemaWriter.WriteSchemaForEnum(writer, typeof(ReadWriteMode));
			if (!options.IgnoreInferredProperties && !options.IgnoreTimestamps)
			{
				writer.WritePropertyName("createdTimestamp");
				JsonSchemaWriter.WriteSchemaForDateTime(writer);
			}
			if (!options.IgnoreInferredProperties && !options.IgnoreTimestamps)
			{
				writer.WritePropertyName("lastUpdate");
				JsonSchemaWriter.WriteSchemaForDateTime(writer);
			}
			if (!options.IgnoreInferredProperties && !options.IgnoreTimestamps)
			{
				writer.WritePropertyName("lastSchemaUpdate");
				JsonSchemaWriter.WriteSchemaForDateTime(writer);
			}
			if (!options.IgnoreInferredProperties && !options.IgnoreTimestamps)
			{
				writer.WritePropertyName("lastProcessed");
				JsonSchemaWriter.WriteSchemaForDateTime(writer);
			}
			if (!options.IgnoreChildren && !options.IgnoreChildrenExceptAnnotations)
			{
				writer.WritePropertyName("model");
				JsonSchemaWriter.WriteSchemaForModel(writer, options, mode, dbCompatibilityLevel);
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001B71 RID: 7025 RVA: 0x000B9D04 File Offset: 0x000B7F04
		internal static void WriteSchemaForObjectPath(JsonWriter writer, ObjectType objectType, bool includeDatabase)
		{
			if (objectType <= ObjectType.Role)
			{
				switch (objectType)
				{
				case ObjectType.DataSource:
					writer.WriteStartObject();
					writer.WritePropertyName("description");
					writer.WriteValue("Path for object DataSource");
					writer.WritePropertyName("type");
					writer.WriteValue("object");
					writer.WritePropertyName("properties");
					writer.WriteStartObject();
					if (includeDatabase)
					{
						writer.WritePropertyName("database");
						JsonSchemaWriter.WriteSchemaForString(writer);
					}
					writer.WritePropertyName("dataSource");
					JsonSchemaWriter.WriteSchemaForString(writer);
					writer.WriteEndObject();
					writer.WritePropertyName("additionalProperties");
					writer.WriteValue(false);
					writer.WriteEndObject();
					return;
				case ObjectType.Table:
					writer.WriteStartObject();
					writer.WritePropertyName("description");
					writer.WriteValue("Path for object Table");
					writer.WritePropertyName("type");
					writer.WriteValue("object");
					writer.WritePropertyName("properties");
					writer.WriteStartObject();
					if (includeDatabase)
					{
						writer.WritePropertyName("database");
						JsonSchemaWriter.WriteSchemaForString(writer);
					}
					writer.WritePropertyName("table");
					JsonSchemaWriter.WriteSchemaForString(writer);
					writer.WriteEndObject();
					writer.WritePropertyName("additionalProperties");
					writer.WriteValue(false);
					writer.WriteEndObject();
					return;
				case ObjectType.Column:
					writer.WriteStartObject();
					writer.WritePropertyName("description");
					writer.WriteValue("Path for object Column");
					writer.WritePropertyName("type");
					writer.WriteValue("object");
					writer.WritePropertyName("properties");
					writer.WriteStartObject();
					if (includeDatabase)
					{
						writer.WritePropertyName("database");
						JsonSchemaWriter.WriteSchemaForString(writer);
					}
					writer.WritePropertyName("table");
					JsonSchemaWriter.WriteSchemaForString(writer);
					writer.WritePropertyName("column");
					JsonSchemaWriter.WriteSchemaForString(writer);
					writer.WriteEndObject();
					writer.WritePropertyName("additionalProperties");
					writer.WriteValue(false);
					writer.WriteEndObject();
					return;
				case ObjectType.AttributeHierarchy:
					break;
				case ObjectType.Partition:
					writer.WriteStartObject();
					writer.WritePropertyName("description");
					writer.WriteValue("Path for object Partition");
					writer.WritePropertyName("type");
					writer.WriteValue("object");
					writer.WritePropertyName("properties");
					writer.WriteStartObject();
					if (includeDatabase)
					{
						writer.WritePropertyName("database");
						JsonSchemaWriter.WriteSchemaForString(writer);
					}
					writer.WritePropertyName("table");
					JsonSchemaWriter.WriteSchemaForString(writer);
					writer.WritePropertyName("partition");
					JsonSchemaWriter.WriteSchemaForString(writer);
					writer.WriteEndObject();
					writer.WritePropertyName("additionalProperties");
					writer.WriteValue(false);
					writer.WriteEndObject();
					return;
				default:
					if (objectType == ObjectType.Role)
					{
						writer.WriteStartObject();
						writer.WritePropertyName("description");
						writer.WriteValue("Path for object Role");
						writer.WritePropertyName("type");
						writer.WriteValue("object");
						writer.WritePropertyName("properties");
						writer.WriteStartObject();
						if (includeDatabase)
						{
							writer.WritePropertyName("database");
							JsonSchemaWriter.WriteSchemaForString(writer);
						}
						writer.WritePropertyName("role");
						JsonSchemaWriter.WriteSchemaForString(writer);
						writer.WriteEndObject();
						writer.WritePropertyName("additionalProperties");
						writer.WriteValue(false);
						writer.WriteEndObject();
						return;
					}
					break;
				}
			}
			else
			{
				if (objectType == ObjectType.Expression)
				{
					writer.WriteStartObject();
					writer.WritePropertyName("description");
					writer.WriteValue("Path for object Expression");
					writer.WritePropertyName("type");
					writer.WriteValue("object");
					writer.WritePropertyName("properties");
					writer.WriteStartObject();
					if (includeDatabase)
					{
						writer.WritePropertyName("database");
						JsonSchemaWriter.WriteSchemaForString(writer);
					}
					writer.WritePropertyName("expression");
					JsonSchemaWriter.WriteSchemaForString(writer);
					writer.WriteEndObject();
					writer.WritePropertyName("additionalProperties");
					writer.WriteValue(false);
					writer.WriteEndObject();
					return;
				}
				if (objectType == ObjectType.Database)
				{
					writer.WriteStartObject();
					writer.WritePropertyName("description");
					writer.WriteValue("Path for object Database");
					writer.WritePropertyName("type");
					writer.WriteValue("object");
					writer.WritePropertyName("properties");
					writer.WriteStartObject();
					if (includeDatabase)
					{
						writer.WritePropertyName("database");
						JsonSchemaWriter.WriteSchemaForString(writer);
					}
					writer.WriteEndObject();
					writer.WritePropertyName("additionalProperties");
					writer.WriteValue(false);
					writer.WriteEndObject();
					return;
				}
			}
			throw TomInternalException.Create("Cannot write object path for object type {0}", new object[] { objectType });
		}

		// Token: 0x06001B72 RID: 7026 RVA: 0x000BA130 File Offset: 0x000B8330
		internal static void WriteSchemaForMultilineString(JsonWriter writer)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("anyOf");
			writer.WriteStartArray();
			JsonSchemaWriter.WriteSchemaForString(writer);
			JsonSchemaWriter.WriteSchemaForStringArray(writer, null, false);
			writer.WriteEndArray();
			writer.WriteEndObject();
		}

		// Token: 0x06001B73 RID: 7027 RVA: 0x000BA176 File Offset: 0x000B8376
		internal static void WriteSchemaForString(JsonWriter writer)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("type");
			writer.WriteValue("string");
			writer.WriteEndObject();
		}

		// Token: 0x06001B74 RID: 7028 RVA: 0x000BA19C File Offset: 0x000B839C
		internal static void WriteSchemaForStringArray(JsonWriter writer, int? minItems, bool uniqueItems)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("type");
			writer.WriteValue("array");
			writer.WritePropertyName("items");
			writer.WriteStartObject();
			writer.WritePropertyName("type");
			writer.WriteValue("string");
			writer.WriteEndObject();
			if (minItems != null)
			{
				writer.WritePropertyName("minItems");
				writer.WriteValue(minItems.Value);
			}
			if (uniqueItems)
			{
				writer.WritePropertyName("uniqueItems");
				writer.WriteValue(true);
			}
			writer.WriteEndObject();
		}

		// Token: 0x06001B75 RID: 7029 RVA: 0x000BA22E File Offset: 0x000B842E
		internal static void WriteSchemaForDateTime(JsonWriter writer)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("type");
			writer.WriteValue("string");
			writer.WritePropertyName("format");
			writer.WriteValue("date-time");
			writer.WriteEndObject();
		}

		// Token: 0x06001B76 RID: 7030 RVA: 0x000BA268 File Offset: 0x000B8468
		internal static void WriteSchemaForBoolean(JsonWriter writer)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("type");
			writer.WriteValue("boolean");
			writer.WriteEndObject();
		}

		// Token: 0x06001B77 RID: 7031 RVA: 0x000BA28C File Offset: 0x000B848C
		internal static void WriteSchemaForDouble(JsonWriter writer)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("type");
			writer.WriteValue("number");
			writer.WriteEndObject();
		}

		// Token: 0x06001B78 RID: 7032 RVA: 0x000BA2B0 File Offset: 0x000B84B0
		internal static void WriteSchemaForInteger(JsonWriter writer)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("type");
			writer.WriteValue("integer");
			writer.WriteEndObject();
		}

		// Token: 0x06001B79 RID: 7033 RVA: 0x000BA2D4 File Offset: 0x000B84D4
		internal static void WriteSchemaForGenericObject(JsonWriter writer)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WriteEndObject();
		}

		// Token: 0x06001B7A RID: 7034 RVA: 0x000BA2F8 File Offset: 0x000B84F8
		internal static void WriteSchemaForEnum(JsonWriter writer, Type enumType)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("enum");
			writer.WriteStartArray();
			foreach (string text in Enum.GetNames(enumType))
			{
				writer.WriteValue(text.ToJsonCase());
			}
			writer.WriteEndArray();
			writer.WriteEndObject();
		}

		// Token: 0x06001B7B RID: 7035 RVA: 0x000BA350 File Offset: 0x000B8550
		internal static void WriteSchemaForModel(JsonWriter writer, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("description");
			writer.WriteValue("Model object of Tabular Object Model (TOM)");
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("name");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WritePropertyName("description");
			if (options.SplitMultilineStrings)
			{
				JsonSchemaWriter.WriteSchemaForMultilineString(writer);
			}
			else
			{
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			writer.WritePropertyName("storageLocation");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WritePropertyName("defaultMode");
			writer.WriteStartObject();
			writer.WritePropertyName("enum");
			writer.WriteStartArray();
			writer.WriteValue("import");
			writer.WriteValue("directQuery");
			writer.WriteValue("default");
			if (CompatibilityRestrictions.ModeType_Push.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WriteValue("push");
			}
			if (CompatibilityRestrictions.ModeType_Dual.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WriteValue("dual");
			}
			if (CompatibilityRestrictions.ModeType_DirectLake.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WriteValue("directLake");
			}
			writer.WriteEndArray();
			writer.WriteEndObject();
			writer.WritePropertyName("defaultDataView");
			writer.WriteStartObject();
			writer.WritePropertyName("enum");
			writer.WriteStartArray();
			writer.WriteValue("full");
			writer.WriteValue("sample");
			writer.WriteValue("default");
			writer.WriteEndArray();
			writer.WriteEndObject();
			writer.WritePropertyName("culture");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WritePropertyName("collation");
			JsonSchemaWriter.WriteSchemaForString(writer);
			if (!options.IgnoreInferredProperties && !options.IgnoreTimestamps)
			{
				writer.WritePropertyName("modifiedTime");
				JsonSchemaWriter.WriteSchemaForDateTime(writer);
			}
			if (!options.IgnoreInferredProperties && !options.IgnoreTimestamps)
			{
				writer.WritePropertyName("structureModifiedTime");
				JsonSchemaWriter.WriteSchemaForDateTime(writer);
			}
			if (CompatibilityRestrictions.Model_DataAccessOptions.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("dataAccessOptions");
				JsonSchemaWriter.WriteSchemaForGenericObject(writer);
			}
			if (CompatibilityRestrictions.Model_DefaultPowerBIDataSourceVersion.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("defaultPowerBIDataSourceVersion");
				writer.WriteStartObject();
				writer.WritePropertyName("enum");
				writer.WriteStartArray();
				writer.WriteValue("powerBI_V1");
				writer.WriteValue("powerBI_V2");
				if (CompatibilityRestrictions.PowerBIDataSourceVersion_PowerBI_V3.IsCompatible(mode, dbCompatibilityLevel))
				{
					writer.WriteValue("powerBI_V3");
				}
				writer.WriteEndArray();
				writer.WriteEndObject();
			}
			if (CompatibilityRestrictions.Model_ForceUniqueNames.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("forceUniqueNames");
				JsonSchemaWriter.WriteSchemaForBoolean(writer);
			}
			if (CompatibilityRestrictions.Model_DiscourageImplicitMeasures.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("discourageImplicitMeasures");
				JsonSchemaWriter.WriteSchemaForBoolean(writer);
			}
			if (CompatibilityRestrictions.Model_DiscourageReportMeasures.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("discourageReportMeasures");
				JsonSchemaWriter.WriteSchemaForBoolean(writer);
			}
			if (CompatibilityRestrictions.Model_DataSourceVariablesOverrideBehavior.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("dataSourceVariablesOverrideBehavior");
				writer.WriteStartObject();
				writer.WritePropertyName("enum");
				writer.WriteStartArray();
				writer.WriteValue("disallow");
				writer.WriteValue("allow");
				writer.WriteEndArray();
				writer.WriteEndObject();
			}
			if (CompatibilityRestrictions.Model_DataSourceDefaultMaxConnections.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("dataSourceDefaultMaxConnections");
				JsonSchemaWriter.WriteSchemaForInteger(writer);
			}
			if (CompatibilityRestrictions.Model_SourceQueryCulture.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("sourceQueryCulture");
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			if (CompatibilityRestrictions.Model_MAttributes.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("mAttributes");
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			if (CompatibilityRestrictions.Model_DiscourageCompositeModels.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("discourageCompositeModels");
				JsonSchemaWriter.WriteSchemaForBoolean(writer);
			}
			if (CompatibilityRestrictions.Model_AutomaticAggregationOptions.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("automaticAggregationOptions");
				JsonSchemaWriter.WriteSchemaForGenericObject(writer);
			}
			if (CompatibilityRestrictions.Model_DisableAutoExists.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("disableAutoExists");
				JsonSchemaWriter.WriteSchemaForInteger(writer);
			}
			if (CompatibilityRestrictions.Model_MaxParallelismPerRefresh.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("maxParallelismPerRefresh");
				JsonSchemaWriter.WriteSchemaForInteger(writer);
			}
			if (CompatibilityRestrictions.Model_MaxParallelismPerQuery.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("maxParallelismPerQuery");
				JsonSchemaWriter.WriteSchemaForInteger(writer);
			}
			if (CompatibilityRestrictions.Model_DirectLakeBehavior.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("directLakeBehavior");
				writer.WriteStartObject();
				writer.WritePropertyName("enum");
				writer.WriteStartArray();
				writer.WriteValue("automatic");
				writer.WriteValue("directLakeOnly");
				writer.WriteValue("directQueryOnly");
				writer.WriteEndArray();
				writer.WriteEndObject();
			}
			if (CompatibilityRestrictions.Model_ValueFilterBehavior.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("valueFilterBehavior");
				writer.WriteStartObject();
				writer.WritePropertyName("enum");
				writer.WriteStartArray();
				writer.WriteValue("automatic");
				writer.WriteValue("independent");
				writer.WriteValue("coalesced");
				writer.WriteEndArray();
				writer.WriteEndObject();
			}
			if (CompatibilityRestrictions.Model_DefaultMeasure.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("defaultMeasure");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("object");
				writer.WritePropertyName("properties");
				writer.WriteStartObject();
				writer.WritePropertyName("table");
				JsonSchemaWriter.WriteSchemaForString(writer);
				writer.WritePropertyName("measure");
				JsonSchemaWriter.WriteSchemaForString(writer);
				writer.WriteEndObject();
				writer.WriteEndObject();
			}
			if (!options.IgnoreChildren)
			{
				writer.WritePropertyName("annotations");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForAnnotation(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			if (!options.IgnoreChildren && CompatibilityRestrictions.ExtendedProperty.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("extendedProperties");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForExtendedProperty(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			if (!options.IgnoreChildren && CompatibilityRestrictions.ExcludedArtifact.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("excludedArtifacts");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForExcludedArtifact(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			if (!options.IgnoreChildren && !options.IgnoreChildrenExceptAnnotations)
			{
				if (!options.IgnoreInferredObjects || !ObjectTreeHelper.IsInferredObject(ObjectType.Table))
				{
					writer.WritePropertyName("tables");
					writer.WriteStartObject();
					writer.WritePropertyName("type");
					writer.WriteValue("array");
					writer.WritePropertyName("items");
					JsonSchemaWriter.WriteSchemaForTable(writer, options, mode, dbCompatibilityLevel);
					writer.WriteEndObject();
				}
				if (!options.IgnoreInferredObjects || !ObjectTreeHelper.IsInferredObject(ObjectType.Relationship))
				{
					writer.WritePropertyName("relationships");
					writer.WriteStartObject();
					writer.WritePropertyName("type");
					writer.WriteValue("array");
					writer.WritePropertyName("items");
					JsonSchemaWriter.WriteSchemaForRelationship(writer, options, mode, dbCompatibilityLevel);
					writer.WriteEndObject();
				}
				if (!options.IgnoreInferredObjects || !ObjectTreeHelper.IsInferredObject(ObjectType.DataSource))
				{
					writer.WritePropertyName("dataSources");
					writer.WriteStartObject();
					writer.WritePropertyName("type");
					writer.WriteValue("array");
					writer.WritePropertyName("items");
					JsonSchemaWriter.WriteSchemaForDataSource(writer, options, mode, dbCompatibilityLevel);
					writer.WriteEndObject();
				}
				if (!options.IgnoreInferredObjects || !ObjectTreeHelper.IsInferredObject(ObjectType.Perspective))
				{
					writer.WritePropertyName("perspectives");
					writer.WriteStartObject();
					writer.WritePropertyName("type");
					writer.WriteValue("array");
					writer.WritePropertyName("items");
					JsonSchemaWriter.WriteSchemaForPerspective(writer, options, mode, dbCompatibilityLevel);
					writer.WriteEndObject();
				}
				if (!options.IgnoreInferredObjects || !ObjectTreeHelper.IsInferredObject(ObjectType.Culture))
				{
					writer.WritePropertyName("cultures");
					writer.WriteStartObject();
					writer.WritePropertyName("type");
					writer.WriteValue("array");
					writer.WritePropertyName("items");
					JsonSchemaWriter.WriteSchemaForCulture(writer, options, mode, dbCompatibilityLevel);
					writer.WriteEndObject();
				}
				if (!options.IgnoreInferredObjects || !ObjectTreeHelper.IsInferredObject(ObjectType.Role))
				{
					writer.WritePropertyName("roles");
					writer.WriteStartObject();
					writer.WritePropertyName("type");
					writer.WriteValue("array");
					writer.WritePropertyName("items");
					JsonSchemaWriter.WriteSchemaForModelRole(writer, options, mode, dbCompatibilityLevel);
					writer.WriteEndObject();
				}
				if ((!options.IgnoreInferredObjects || !ObjectTreeHelper.IsInferredObject(ObjectType.Expression)) && CompatibilityRestrictions.NamedExpression.IsCompatible(mode, dbCompatibilityLevel))
				{
					writer.WritePropertyName("expressions");
					writer.WriteStartObject();
					writer.WritePropertyName("type");
					writer.WriteValue("array");
					writer.WritePropertyName("items");
					JsonSchemaWriter.WriteSchemaForNamedExpression(writer, options, mode, dbCompatibilityLevel);
					writer.WriteEndObject();
				}
				if ((!options.IgnoreInferredObjects || !ObjectTreeHelper.IsInferredObject(ObjectType.QueryGroup)) && CompatibilityRestrictions.QueryGroup.IsCompatible(mode, dbCompatibilityLevel))
				{
					writer.WritePropertyName("queryGroups");
					writer.WriteStartObject();
					writer.WritePropertyName("type");
					writer.WriteValue("array");
					writer.WritePropertyName("items");
					JsonSchemaWriter.WriteSchemaForQueryGroup(writer, options, mode, dbCompatibilityLevel);
					writer.WriteEndObject();
				}
				if ((!options.IgnoreInferredObjects || !ObjectTreeHelper.IsInferredObject(ObjectType.AnalyticsAIMetadata)) && CompatibilityRestrictions.AnalyticsAIMetadata.IsCompatible(mode, dbCompatibilityLevel))
				{
					writer.WritePropertyName("analyticsAIMetadata");
					writer.WriteStartObject();
					writer.WritePropertyName("type");
					writer.WriteValue("array");
					writer.WritePropertyName("items");
					JsonSchemaWriter.WriteSchemaForAnalyticsAIMetadata(writer, options, mode, dbCompatibilityLevel);
					writer.WriteEndObject();
				}
				if ((!options.IgnoreInferredObjects || !ObjectTreeHelper.IsInferredObject(ObjectType.Function)) && CompatibilityRestrictions.Function.IsCompatible(mode, dbCompatibilityLevel))
				{
					writer.WritePropertyName("functions");
					writer.WriteStartObject();
					writer.WritePropertyName("type");
					writer.WriteValue("array");
					writer.WritePropertyName("items");
					JsonSchemaWriter.WriteSchemaForFunction(writer, options, mode, dbCompatibilityLevel);
					writer.WriteEndObject();
				}
				if ((!options.IgnoreInferredObjects || !ObjectTreeHelper.IsInferredObject(ObjectType.BindingInfo)) && CompatibilityRestrictions.BindingInfo.IsCompatible(mode, dbCompatibilityLevel))
				{
					writer.WritePropertyName("bindingInfoCollection");
					writer.WriteStartObject();
					writer.WritePropertyName("type");
					writer.WriteValue("array");
					writer.WritePropertyName("items");
					JsonSchemaWriter.WriteSchemaForBindingInfo(writer, options, mode, dbCompatibilityLevel);
					writer.WriteEndObject();
				}
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001B7C RID: 7036 RVA: 0x000BAD84 File Offset: 0x000B8F84
		internal static void WriteSchemaForTable(JsonWriter writer, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("description");
			writer.WriteValue("Table object of Tabular Object Model (TOM)");
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("name");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WritePropertyName("dataCategory");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WritePropertyName("description");
			if (options.SplitMultilineStrings)
			{
				JsonSchemaWriter.WriteSchemaForMultilineString(writer);
			}
			else
			{
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			writer.WritePropertyName("isHidden");
			JsonSchemaWriter.WriteSchemaForBoolean(writer);
			if (!options.IgnoreInferredProperties && !options.IgnoreTimestamps)
			{
				writer.WritePropertyName("modifiedTime");
				JsonSchemaWriter.WriteSchemaForDateTime(writer);
			}
			if (!options.IgnoreInferredProperties && !options.IgnoreTimestamps)
			{
				writer.WritePropertyName("structureModifiedTime");
				JsonSchemaWriter.WriteSchemaForDateTime(writer);
			}
			if (CompatibilityRestrictions.Table_ShowAsVariationsOnly.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("showAsVariationsOnly");
				JsonSchemaWriter.WriteSchemaForBoolean(writer);
			}
			if (CompatibilityRestrictions.Table_IsPrivate.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("isPrivate");
				JsonSchemaWriter.WriteSchemaForBoolean(writer);
			}
			if (CompatibilityRestrictions.Table_AlternateSourcePrecedence.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("alternateSourcePrecedence");
				JsonSchemaWriter.WriteSchemaForInteger(writer);
			}
			if (CompatibilityRestrictions.Table_ExcludeFromModelRefresh.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("excludeFromModelRefresh");
				JsonSchemaWriter.WriteSchemaForBoolean(writer);
			}
			if (CompatibilityRestrictions.Table_LineageTag.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("lineageTag");
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			if (CompatibilityRestrictions.Table_SourceLineageTag.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("sourceLineageTag");
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			if (CompatibilityRestrictions.Table_SystemManaged.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("systemManaged");
				JsonSchemaWriter.WriteSchemaForBoolean(writer);
			}
			if (CompatibilityRestrictions.Table_ExcludeFromAutomaticAggregations.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("excludeFromAutomaticAggregations");
				JsonSchemaWriter.WriteSchemaForBoolean(writer);
			}
			if (!options.IgnoreChildren && !options.IgnoreChildrenExceptAnnotations)
			{
				if ((!options.IgnoreInferredObjects || !ObjectTreeHelper.IsInferredObject(ObjectType.DetailRowsDefinition)) && CompatibilityRestrictions.Table_DefaultDetailRowsDefinition.IsCompatible(mode, dbCompatibilityLevel))
				{
					writer.WritePropertyName("defaultDetailRowsDefinition");
					JsonSchemaWriter.WriteSchemaForDetailRowsDefinition(writer, options, mode, dbCompatibilityLevel);
				}
				if ((!options.IgnoreInferredObjects || !ObjectTreeHelper.IsInferredObject(ObjectType.RefreshPolicy)) && CompatibilityRestrictions.Table_RefreshPolicy.IsCompatible(mode, dbCompatibilityLevel))
				{
					writer.WritePropertyName("refreshPolicy");
					JsonSchemaWriter.WriteSchemaForRefreshPolicy(writer, options, mode, dbCompatibilityLevel);
				}
				if ((!options.IgnoreInferredObjects || !ObjectTreeHelper.IsInferredObject(ObjectType.CalculationGroup)) && CompatibilityRestrictions.Table_CalculationGroup.IsCompatible(mode, dbCompatibilityLevel))
				{
					writer.WritePropertyName("calculationGroup");
					JsonSchemaWriter.WriteSchemaForCalculationGroup(writer, options, mode, dbCompatibilityLevel);
				}
			}
			JsonSchemaWriter.WriteAdditionalSchemaPropertiesForTable(writer, options, mode, dbCompatibilityLevel);
			if (!options.IgnoreChildren)
			{
				writer.WritePropertyName("annotations");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForAnnotation(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			if (!options.IgnoreChildren && CompatibilityRestrictions.ExtendedProperty.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("extendedProperties");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForExtendedProperty(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			if (!options.IgnoreChildren && CompatibilityRestrictions.ExcludedArtifact.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("excludedArtifacts");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForExcludedArtifact(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			if (!options.IgnoreChildren && CompatibilityRestrictions.ChangedProperty.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("changedProperties");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForChangedProperty(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			if (!options.IgnoreChildren && !options.IgnoreChildrenExceptAnnotations)
			{
				if (!options.IgnoreInferredObjects || !ObjectTreeHelper.IsInferredObject(ObjectType.Column))
				{
					writer.WritePropertyName("columns");
					writer.WriteStartObject();
					writer.WritePropertyName("type");
					writer.WriteValue("array");
					writer.WritePropertyName("items");
					JsonSchemaWriter.WriteSchemaForColumn(writer, options, mode, dbCompatibilityLevel);
					writer.WriteEndObject();
				}
				if (!options.IgnoreInferredObjects || !ObjectTreeHelper.IsInferredObject(ObjectType.Measure))
				{
					writer.WritePropertyName("measures");
					writer.WriteStartObject();
					writer.WritePropertyName("type");
					writer.WriteValue("array");
					writer.WritePropertyName("items");
					JsonSchemaWriter.WriteSchemaForMeasure(writer, options, mode, dbCompatibilityLevel);
					writer.WriteEndObject();
				}
				if (!options.IgnoreInferredObjects || !ObjectTreeHelper.IsInferredObject(ObjectType.Hierarchy))
				{
					writer.WritePropertyName("hierarchies");
					writer.WriteStartObject();
					writer.WritePropertyName("type");
					writer.WriteValue("array");
					writer.WritePropertyName("items");
					JsonSchemaWriter.WriteSchemaForHierarchy(writer, options, mode, dbCompatibilityLevel);
					writer.WriteEndObject();
				}
				if ((!options.IgnoreInferredObjects || !ObjectTreeHelper.IsInferredObject(ObjectType.Set)) && CompatibilityRestrictions.Set.IsCompatible(mode, dbCompatibilityLevel))
				{
					writer.WritePropertyName("sets");
					writer.WriteStartObject();
					writer.WritePropertyName("type");
					writer.WriteValue("array");
					writer.WritePropertyName("items");
					JsonSchemaWriter.WriteSchemaForSet(writer, options, mode, dbCompatibilityLevel);
					writer.WriteEndObject();
				}
				if ((!options.IgnoreInferredObjects || !ObjectTreeHelper.IsInferredObject(ObjectType.Calendar)) && CompatibilityRestrictions.Calendar.IsCompatible(mode, dbCompatibilityLevel))
				{
					writer.WritePropertyName("calendars");
					writer.WriteStartObject();
					writer.WritePropertyName("type");
					writer.WriteValue("array");
					writer.WritePropertyName("items");
					JsonSchemaWriter.WriteSchemaForCalendar(writer, options, mode, dbCompatibilityLevel);
					writer.WriteEndObject();
				}
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001B7D RID: 7037 RVA: 0x000BB350 File Offset: 0x000B9550
		private static void WriteAdditionalSchemaPropertiesForTable(JsonWriter writer, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (!options.IgnoreChildren && !options.IgnoreChildrenExceptAnnotations)
			{
				if (options.PartitionsMergedWithTable)
				{
					JsonSchemaWriter.WriteSchemaPropertiesForPartitionInMergedWithTableMode(writer, options, mode, dbCompatibilityLevel);
					return;
				}
				writer.WritePropertyName("partitions");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForPartition(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
		}

		// Token: 0x06001B7E RID: 7038 RVA: 0x000BB3C0 File Offset: 0x000B95C0
		internal static void WriteSchemaForAttributeHierarchy(JsonWriter writer, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("description");
			writer.WriteValue("AttributeHierarchy object of Tabular Object Model (TOM)");
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			if (!options.IgnoreInferredProperties)
			{
				writer.WritePropertyName("state");
				writer.WriteStartObject();
				writer.WritePropertyName("enum");
				writer.WriteStartArray();
				writer.WriteValue("ready");
				writer.WriteValue("noData");
				writer.WriteValue("calculationNeeded");
				writer.WriteValue("semanticError");
				writer.WriteValue("evaluationError");
				writer.WriteValue("dependencyError");
				writer.WriteValue("incomplete");
				if (CompatibilityRestrictions.ObjectState_ForceCalculationNeeded.IsCompatible(mode, dbCompatibilityLevel))
				{
					writer.WriteValue("forceCalculationNeeded");
				}
				writer.WriteEndArray();
				writer.WriteEndObject();
			}
			if (!options.IgnoreInferredProperties && !options.IgnoreTimestamps)
			{
				writer.WritePropertyName("modifiedTime");
				JsonSchemaWriter.WriteSchemaForDateTime(writer);
			}
			if (!options.IgnoreInferredProperties && !options.IgnoreTimestamps)
			{
				writer.WritePropertyName("refreshedTime");
				JsonSchemaWriter.WriteSchemaForDateTime(writer);
			}
			if (!options.IgnoreChildren)
			{
				writer.WritePropertyName("annotations");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForAnnotation(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			if (!options.IgnoreChildren && CompatibilityRestrictions.ExtendedProperty.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("extendedProperties");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForExtendedProperty(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001B7F RID: 7039 RVA: 0x000BB5B0 File Offset: 0x000B97B0
		internal static void WriteSchemaForPartition(JsonWriter writer, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("description");
			writer.WriteValue("Partition object of Tabular Object Model (TOM)");
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("name");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WritePropertyName("description");
			if (options.SplitMultilineStrings)
			{
				JsonSchemaWriter.WriteSchemaForMultilineString(writer);
			}
			else
			{
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			if (!options.IgnoreInferredProperties)
			{
				writer.WritePropertyName("state");
				writer.WriteStartObject();
				writer.WritePropertyName("enum");
				writer.WriteStartArray();
				writer.WriteValue("ready");
				writer.WriteValue("noData");
				writer.WriteValue("calculationNeeded");
				writer.WriteValue("semanticError");
				writer.WriteValue("evaluationError");
				writer.WriteValue("dependencyError");
				writer.WriteValue("incomplete");
				if (CompatibilityRestrictions.ObjectState_ForceCalculationNeeded.IsCompatible(mode, dbCompatibilityLevel))
				{
					writer.WriteValue("forceCalculationNeeded");
				}
				writer.WriteEndArray();
				writer.WriteEndObject();
			}
			writer.WritePropertyName("mode");
			writer.WriteStartObject();
			writer.WritePropertyName("enum");
			writer.WriteStartArray();
			writer.WriteValue("import");
			writer.WriteValue("directQuery");
			writer.WriteValue("default");
			if (CompatibilityRestrictions.ModeType_Push.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WriteValue("push");
			}
			if (CompatibilityRestrictions.ModeType_Dual.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WriteValue("dual");
			}
			if (CompatibilityRestrictions.ModeType_DirectLake.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WriteValue("directLake");
			}
			writer.WriteEndArray();
			writer.WriteEndObject();
			writer.WritePropertyName("dataView");
			writer.WriteStartObject();
			writer.WritePropertyName("enum");
			writer.WriteStartArray();
			writer.WriteValue("full");
			writer.WriteValue("sample");
			writer.WriteValue("default");
			writer.WriteEndArray();
			writer.WriteEndObject();
			if (!options.IgnoreInferredProperties && !options.IgnoreTimestamps)
			{
				writer.WritePropertyName("modifiedTime");
				JsonSchemaWriter.WriteSchemaForDateTime(writer);
			}
			if (!options.IgnoreInferredProperties && !options.IgnoreTimestamps)
			{
				writer.WritePropertyName("refreshedTime");
				JsonSchemaWriter.WriteSchemaForDateTime(writer);
			}
			if (!options.IgnoreInferredProperties)
			{
				writer.WritePropertyName("errorMessage");
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			if (CompatibilityRestrictions.Partition_QueryGroup.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("queryGroup");
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			if (!options.IgnoreChildren && !options.IgnoreChildrenExceptAnnotations && (!options.IgnoreInferredObjects || !ObjectTreeHelper.IsInferredObject(ObjectType.DataCoverageDefinition)) && CompatibilityRestrictions.Partition_DataCoverageDefinition.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("dataCoverageDefinition");
				JsonSchemaWriter.WriteSchemaForDataCoverageDefinition(writer, options, mode, dbCompatibilityLevel);
			}
			JsonSchemaWriter.WriteAdditionalSchemaPropertiesForPartition(writer, options, mode, dbCompatibilityLevel);
			if (!options.IgnoreChildren)
			{
				writer.WritePropertyName("annotations");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForAnnotation(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			if (!options.IgnoreChildren && CompatibilityRestrictions.ExtendedProperty.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("extendedProperties");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForExtendedProperty(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001B80 RID: 7040 RVA: 0x000BB93E File Offset: 0x000B9B3E
		private static void WriteAdditionalSchemaPropertiesForPartition(JsonWriter writer, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WritePropertyName("source");
			JsonSchemaWriter.WriteSchemaForPartitionSource(writer, options, mode, dbCompatibilityLevel);
		}

		// Token: 0x06001B81 RID: 7041 RVA: 0x000BB954 File Offset: 0x000B9B54
		internal static void WriteSchemaForMeasure(JsonWriter writer, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("description");
			writer.WriteValue("Measure object of Tabular Object Model (TOM)");
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("name");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WritePropertyName("description");
			if (options.SplitMultilineStrings)
			{
				JsonSchemaWriter.WriteSchemaForMultilineString(writer);
			}
			else
			{
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			if (!options.IgnoreInferredProperties)
			{
				writer.WritePropertyName("dataType");
				writer.WriteStartObject();
				writer.WritePropertyName("enum");
				writer.WriteStartArray();
				writer.WriteValue("automatic");
				writer.WriteValue("string");
				writer.WriteValue("int64");
				writer.WriteValue("double");
				writer.WriteValue("dateTime");
				writer.WriteValue("decimal");
				writer.WriteValue("boolean");
				writer.WriteValue("binary");
				writer.WriteValue("unknown");
				writer.WriteValue("variant");
				writer.WriteEndArray();
				writer.WriteEndObject();
			}
			writer.WritePropertyName("expression");
			if (options.SplitMultilineStrings)
			{
				JsonSchemaWriter.WriteSchemaForMultilineString(writer);
			}
			else
			{
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			writer.WritePropertyName("formatString");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WritePropertyName("isHidden");
			JsonSchemaWriter.WriteSchemaForBoolean(writer);
			if (!options.IgnoreInferredProperties)
			{
				writer.WritePropertyName("state");
				writer.WriteStartObject();
				writer.WritePropertyName("enum");
				writer.WriteStartArray();
				writer.WriteValue("ready");
				writer.WriteValue("noData");
				writer.WriteValue("calculationNeeded");
				writer.WriteValue("semanticError");
				writer.WriteValue("evaluationError");
				writer.WriteValue("dependencyError");
				writer.WriteValue("incomplete");
				if (CompatibilityRestrictions.ObjectState_ForceCalculationNeeded.IsCompatible(mode, dbCompatibilityLevel))
				{
					writer.WriteValue("forceCalculationNeeded");
				}
				writer.WriteEndArray();
				writer.WriteEndObject();
			}
			if (!options.IgnoreInferredProperties && !options.IgnoreTimestamps)
			{
				writer.WritePropertyName("modifiedTime");
				JsonSchemaWriter.WriteSchemaForDateTime(writer);
			}
			if (!options.IgnoreInferredProperties && !options.IgnoreTimestamps)
			{
				writer.WritePropertyName("structureModifiedTime");
				JsonSchemaWriter.WriteSchemaForDateTime(writer);
			}
			writer.WritePropertyName("isSimpleMeasure");
			JsonSchemaWriter.WriteSchemaForBoolean(writer);
			if (!options.IgnoreInferredProperties)
			{
				writer.WritePropertyName("errorMessage");
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			writer.WritePropertyName("displayFolder");
			JsonSchemaWriter.WriteSchemaForString(writer);
			if (CompatibilityRestrictions.Measure_DataCategory.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("dataCategory");
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			if (CompatibilityRestrictions.Measure_LineageTag.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("lineageTag");
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			if (CompatibilityRestrictions.Measure_SourceLineageTag.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("sourceLineageTag");
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			if (!options.IgnoreChildren && !options.IgnoreChildrenExceptAnnotations)
			{
				if (!options.IgnoreInferredObjects || !ObjectTreeHelper.IsInferredObject(ObjectType.KPI))
				{
					writer.WritePropertyName("kpi");
					JsonSchemaWriter.WriteSchemaForKPI(writer, options, mode, dbCompatibilityLevel);
				}
				if ((!options.IgnoreInferredObjects || !ObjectTreeHelper.IsInferredObject(ObjectType.DetailRowsDefinition)) && CompatibilityRestrictions.Measure_DetailRowsDefinition.IsCompatible(mode, dbCompatibilityLevel))
				{
					writer.WritePropertyName("detailRowsDefinition");
					JsonSchemaWriter.WriteSchemaForDetailRowsDefinition(writer, options, mode, dbCompatibilityLevel);
				}
				if ((!options.IgnoreInferredObjects || !ObjectTreeHelper.IsInferredObject(ObjectType.FormatStringDefinition)) && CompatibilityRestrictions.Measure_FormatStringDefinition.IsCompatible(mode, dbCompatibilityLevel))
				{
					writer.WritePropertyName("formatStringDefinition");
					JsonSchemaWriter.WriteSchemaForFormatStringDefinition(writer, options, mode, dbCompatibilityLevel);
				}
			}
			if (!options.IgnoreChildren)
			{
				writer.WritePropertyName("annotations");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForAnnotation(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			if (!options.IgnoreChildren && CompatibilityRestrictions.ExtendedProperty.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("extendedProperties");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForExtendedProperty(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			if (!options.IgnoreChildren && CompatibilityRestrictions.ChangedProperty.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("changedProperties");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForChangedProperty(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001B82 RID: 7042 RVA: 0x000BBDF0 File Offset: 0x000B9FF0
		internal static void WriteSchemaForHierarchy(JsonWriter writer, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("description");
			writer.WriteValue("Hierarchy object of Tabular Object Model (TOM)");
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("name");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WritePropertyName("description");
			if (options.SplitMultilineStrings)
			{
				JsonSchemaWriter.WriteSchemaForMultilineString(writer);
			}
			else
			{
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			writer.WritePropertyName("isHidden");
			JsonSchemaWriter.WriteSchemaForBoolean(writer);
			if (!options.IgnoreInferredProperties)
			{
				writer.WritePropertyName("state");
				writer.WriteStartObject();
				writer.WritePropertyName("enum");
				writer.WriteStartArray();
				writer.WriteValue("ready");
				writer.WriteValue("noData");
				writer.WriteValue("calculationNeeded");
				writer.WriteValue("semanticError");
				writer.WriteValue("evaluationError");
				writer.WriteValue("dependencyError");
				writer.WriteValue("incomplete");
				if (CompatibilityRestrictions.ObjectState_ForceCalculationNeeded.IsCompatible(mode, dbCompatibilityLevel))
				{
					writer.WriteValue("forceCalculationNeeded");
				}
				writer.WriteEndArray();
				writer.WriteEndObject();
			}
			if (!options.IgnoreInferredProperties && !options.IgnoreTimestamps)
			{
				writer.WritePropertyName("modifiedTime");
				JsonSchemaWriter.WriteSchemaForDateTime(writer);
			}
			if (!options.IgnoreInferredProperties && !options.IgnoreTimestamps)
			{
				writer.WritePropertyName("structureModifiedTime");
				JsonSchemaWriter.WriteSchemaForDateTime(writer);
			}
			if (!options.IgnoreInferredProperties && !options.IgnoreTimestamps)
			{
				writer.WritePropertyName("refreshedTime");
				JsonSchemaWriter.WriteSchemaForDateTime(writer);
			}
			writer.WritePropertyName("displayFolder");
			JsonSchemaWriter.WriteSchemaForString(writer);
			if (CompatibilityRestrictions.Hierarchy_HideMembers.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("hideMembers");
				writer.WriteStartObject();
				writer.WritePropertyName("enum");
				writer.WriteStartArray();
				writer.WriteValue("default");
				writer.WriteValue("hideBlankMembers");
				writer.WriteEndArray();
				writer.WriteEndObject();
			}
			if (CompatibilityRestrictions.Hierarchy_LineageTag.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("lineageTag");
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			if (CompatibilityRestrictions.Hierarchy_SourceLineageTag.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("sourceLineageTag");
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			if (!options.IgnoreChildren)
			{
				writer.WritePropertyName("annotations");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForAnnotation(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			if (!options.IgnoreChildren && CompatibilityRestrictions.ExtendedProperty.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("extendedProperties");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForExtendedProperty(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			if (!options.IgnoreChildren && CompatibilityRestrictions.ExcludedArtifact.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("excludedArtifacts");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForExcludedArtifact(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			if (!options.IgnoreChildren && CompatibilityRestrictions.ChangedProperty.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("changedProperties");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForChangedProperty(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			if (!options.IgnoreChildren && !options.IgnoreChildrenExceptAnnotations && (!options.IgnoreInferredObjects || !ObjectTreeHelper.IsInferredObject(ObjectType.Level)))
			{
				writer.WritePropertyName("levels");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForLevel(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001B83 RID: 7043 RVA: 0x000BC1F4 File Offset: 0x000BA3F4
		internal static void WriteSchemaForLevel(JsonWriter writer, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("description");
			writer.WriteValue("Level object of Tabular Object Model (TOM)");
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("ordinal");
			JsonSchemaWriter.WriteSchemaForInteger(writer);
			writer.WritePropertyName("name");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WritePropertyName("description");
			if (options.SplitMultilineStrings)
			{
				JsonSchemaWriter.WriteSchemaForMultilineString(writer);
			}
			else
			{
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			if (!options.IgnoreInferredProperties && !options.IgnoreTimestamps)
			{
				writer.WritePropertyName("modifiedTime");
				JsonSchemaWriter.WriteSchemaForDateTime(writer);
			}
			if (CompatibilityRestrictions.Level_LineageTag.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("lineageTag");
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			if (CompatibilityRestrictions.Level_SourceLineageTag.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("sourceLineageTag");
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			writer.WritePropertyName("column");
			JsonSchemaWriter.WriteSchemaForString(writer);
			if (!options.IgnoreChildren)
			{
				writer.WritePropertyName("annotations");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForAnnotation(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			if (!options.IgnoreChildren && CompatibilityRestrictions.ExtendedProperty.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("extendedProperties");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForExtendedProperty(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			if (!options.IgnoreChildren && CompatibilityRestrictions.ChangedProperty.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("changedProperties");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForChangedProperty(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001B84 RID: 7044 RVA: 0x000BC40C File Offset: 0x000BA60C
		internal static void WriteSchemaForAnnotation(JsonWriter writer, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("description");
			writer.WriteValue("Annotation object of Tabular Object Model (TOM)");
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("name");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WritePropertyName("value");
			if (options.SplitMultilineStrings)
			{
				JsonSchemaWriter.WriteSchemaForMultilineString(writer);
			}
			else
			{
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			if (!options.IgnoreInferredProperties && !options.IgnoreTimestamps)
			{
				writer.WritePropertyName("modifiedTime");
				JsonSchemaWriter.WriteSchemaForDateTime(writer);
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001B85 RID: 7045 RVA: 0x000BC4D0 File Offset: 0x000BA6D0
		internal static void WriteSchemaForKPI(JsonWriter writer, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("description");
			writer.WriteValue("KPI object of Tabular Object Model (TOM)");
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("description");
			if (options.SplitMultilineStrings)
			{
				JsonSchemaWriter.WriteSchemaForMultilineString(writer);
			}
			else
			{
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			writer.WritePropertyName("targetDescription");
			if (options.SplitMultilineStrings)
			{
				JsonSchemaWriter.WriteSchemaForMultilineString(writer);
			}
			else
			{
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			writer.WritePropertyName("targetExpression");
			if (options.SplitMultilineStrings)
			{
				JsonSchemaWriter.WriteSchemaForMultilineString(writer);
			}
			else
			{
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			writer.WritePropertyName("targetFormatString");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WritePropertyName("statusGraphic");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WritePropertyName("statusDescription");
			if (options.SplitMultilineStrings)
			{
				JsonSchemaWriter.WriteSchemaForMultilineString(writer);
			}
			else
			{
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			writer.WritePropertyName("statusExpression");
			if (options.SplitMultilineStrings)
			{
				JsonSchemaWriter.WriteSchemaForMultilineString(writer);
			}
			else
			{
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			writer.WritePropertyName("trendGraphic");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WritePropertyName("trendDescription");
			if (options.SplitMultilineStrings)
			{
				JsonSchemaWriter.WriteSchemaForMultilineString(writer);
			}
			else
			{
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			writer.WritePropertyName("trendExpression");
			if (options.SplitMultilineStrings)
			{
				JsonSchemaWriter.WriteSchemaForMultilineString(writer);
			}
			else
			{
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			if (!options.IgnoreInferredProperties && !options.IgnoreTimestamps)
			{
				writer.WritePropertyName("modifiedTime");
				JsonSchemaWriter.WriteSchemaForDateTime(writer);
			}
			if (!options.IgnoreChildren)
			{
				writer.WritePropertyName("annotations");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForAnnotation(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			if (!options.IgnoreChildren && CompatibilityRestrictions.ExtendedProperty.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("extendedProperties");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForExtendedProperty(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001B86 RID: 7046 RVA: 0x000BC71C File Offset: 0x000BA91C
		internal static void WriteSchemaForCulture(JsonWriter writer, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("description");
			writer.WriteValue("Culture object of Tabular Object Model (TOM)");
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("name");
			JsonSchemaWriter.WriteSchemaForString(writer);
			if (!options.IgnoreInferredProperties && !options.IgnoreTimestamps)
			{
				writer.WritePropertyName("modifiedTime");
				JsonSchemaWriter.WriteSchemaForDateTime(writer);
			}
			if (!options.IgnoreInferredProperties && !options.IgnoreTimestamps)
			{
				writer.WritePropertyName("structureModifiedTime");
				JsonSchemaWriter.WriteSchemaForDateTime(writer);
			}
			if (!options.IgnoreChildren && !options.IgnoreChildrenExceptAnnotations && (!options.IgnoreInferredObjects || !ObjectTreeHelper.IsInferredObject(ObjectType.LinguisticMetadata)))
			{
				writer.WritePropertyName("linguisticMetadata");
				JsonSchemaWriter.WriteSchemaForLinguisticMetadata(writer, options, mode, dbCompatibilityLevel);
			}
			JsonSchemaWriter.WriteAdditionalSchemaPropertiesForCulture(writer, options, mode, dbCompatibilityLevel);
			if (!options.IgnoreChildren)
			{
				writer.WritePropertyName("annotations");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForAnnotation(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			if (!options.IgnoreChildren && CompatibilityRestrictions.ExtendedProperty.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("extendedProperties");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForExtendedProperty(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001B87 RID: 7047 RVA: 0x000BC8BB File Offset: 0x000BAABB
		private static void WriteAdditionalSchemaPropertiesForCulture(JsonWriter writer, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WritePropertyName("translations");
			JsonSchemaWriter.WriteSchemaForObjectTranslations(writer, options, mode, dbCompatibilityLevel);
		}

		// Token: 0x06001B88 RID: 7048 RVA: 0x000BC8D4 File Offset: 0x000BAAD4
		internal static void WriteSchemaForLinguisticMetadata(JsonWriter writer, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("description");
			writer.WriteValue("LinguisticMetadata object of Tabular Object Model (TOM)");
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			if (!options.IgnoreInferredProperties && !options.IgnoreTimestamps)
			{
				writer.WritePropertyName("modifiedTime");
				JsonSchemaWriter.WriteSchemaForDateTime(writer);
			}
			if (CompatibilityRestrictions.LinguisticMetadata_ContentType.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("contentType");
				writer.WriteStartObject();
				writer.WritePropertyName("enum");
				writer.WriteStartArray();
				writer.WriteValue("xml");
				writer.WriteValue("json");
				writer.WriteEndArray();
				writer.WriteEndObject();
			}
			JsonSchemaWriter.WriteAdditionalSchemaPropertiesForLinguisticMetadata(writer, options, mode, dbCompatibilityLevel);
			if (!options.IgnoreChildren)
			{
				writer.WritePropertyName("annotations");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForAnnotation(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			if (!options.IgnoreChildren && CompatibilityRestrictions.ExtendedProperty.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("extendedProperties");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForExtendedProperty(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001B89 RID: 7049 RVA: 0x000BCA5E File Offset: 0x000BAC5E
		private static void WriteAdditionalSchemaPropertiesForLinguisticMetadata(JsonWriter writer, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WritePropertyName("content");
			writer.WriteStartObject();
			writer.WritePropertyName("anyOf");
			writer.WriteStartArray();
			JsonSchemaWriter.WriteSchemaForString(writer);
			JsonSchemaWriter.WriteSchemaForGenericObject(writer);
			writer.WriteEndArray();
			writer.WriteEndObject();
		}

		// Token: 0x06001B8A RID: 7050 RVA: 0x000BCA9C File Offset: 0x000BAC9C
		internal static void WriteSchemaForPerspective(JsonWriter writer, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("description");
			writer.WriteValue("Perspective object of Tabular Object Model (TOM)");
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("name");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WritePropertyName("description");
			if (options.SplitMultilineStrings)
			{
				JsonSchemaWriter.WriteSchemaForMultilineString(writer);
			}
			else
			{
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			if (!options.IgnoreInferredProperties && !options.IgnoreTimestamps)
			{
				writer.WritePropertyName("modifiedTime");
				JsonSchemaWriter.WriteSchemaForDateTime(writer);
			}
			if (!options.IgnoreChildren)
			{
				writer.WritePropertyName("annotations");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForAnnotation(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			if (!options.IgnoreChildren && CompatibilityRestrictions.ExtendedProperty.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("extendedProperties");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForExtendedProperty(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			if (!options.IgnoreChildren && !options.IgnoreChildrenExceptAnnotations && (!options.IgnoreInferredObjects || !ObjectTreeHelper.IsInferredObject(ObjectType.PerspectiveTable)))
			{
				writer.WritePropertyName("tables");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForPerspectiveTable(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001B8B RID: 7051 RVA: 0x000BCC60 File Offset: 0x000BAE60
		internal static void WriteSchemaForPerspectiveTable(JsonWriter writer, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("description");
			writer.WriteValue("PerspectiveTable object of Tabular Object Model (TOM)");
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("name");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WritePropertyName("includeAll");
			JsonSchemaWriter.WriteSchemaForBoolean(writer);
			if (!options.IgnoreInferredProperties && !options.IgnoreTimestamps)
			{
				writer.WritePropertyName("modifiedTime");
				JsonSchemaWriter.WriteSchemaForDateTime(writer);
			}
			if (!options.IgnoreChildren)
			{
				writer.WritePropertyName("annotations");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForAnnotation(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			if (!options.IgnoreChildren && CompatibilityRestrictions.ExtendedProperty.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("extendedProperties");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForExtendedProperty(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			if (!options.IgnoreChildren && !options.IgnoreChildrenExceptAnnotations)
			{
				if (!options.IgnoreInferredObjects || !ObjectTreeHelper.IsInferredObject(ObjectType.PerspectiveColumn))
				{
					writer.WritePropertyName("columns");
					writer.WriteStartObject();
					writer.WritePropertyName("type");
					writer.WriteValue("array");
					writer.WritePropertyName("items");
					JsonSchemaWriter.WriteSchemaForPerspectiveColumn(writer, options, mode, dbCompatibilityLevel);
					writer.WriteEndObject();
				}
				if (!options.IgnoreInferredObjects || !ObjectTreeHelper.IsInferredObject(ObjectType.PerspectiveMeasure))
				{
					writer.WritePropertyName("measures");
					writer.WriteStartObject();
					writer.WritePropertyName("type");
					writer.WriteValue("array");
					writer.WritePropertyName("items");
					JsonSchemaWriter.WriteSchemaForPerspectiveMeasure(writer, options, mode, dbCompatibilityLevel);
					writer.WriteEndObject();
				}
				if (!options.IgnoreInferredObjects || !ObjectTreeHelper.IsInferredObject(ObjectType.PerspectiveHierarchy))
				{
					writer.WritePropertyName("hierarchies");
					writer.WriteStartObject();
					writer.WritePropertyName("type");
					writer.WriteValue("array");
					writer.WritePropertyName("items");
					JsonSchemaWriter.WriteSchemaForPerspectiveHierarchy(writer, options, mode, dbCompatibilityLevel);
					writer.WriteEndObject();
				}
				if ((!options.IgnoreInferredObjects || !ObjectTreeHelper.IsInferredObject(ObjectType.PerspectiveSet)) && CompatibilityRestrictions.PerspectiveSet.IsCompatible(mode, dbCompatibilityLevel))
				{
					writer.WritePropertyName("sets");
					writer.WriteStartObject();
					writer.WritePropertyName("type");
					writer.WriteValue("array");
					writer.WritePropertyName("items");
					JsonSchemaWriter.WriteSchemaForPerspectiveSet(writer, options, mode, dbCompatibilityLevel);
					writer.WriteEndObject();
				}
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001B8C RID: 7052 RVA: 0x000BCF20 File Offset: 0x000BB120
		internal static void WriteSchemaForPerspectiveColumn(JsonWriter writer, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("description");
			writer.WriteValue("PerspectiveColumn object of Tabular Object Model (TOM)");
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("name");
			JsonSchemaWriter.WriteSchemaForString(writer);
			if (!options.IgnoreInferredProperties && !options.IgnoreTimestamps)
			{
				writer.WritePropertyName("modifiedTime");
				JsonSchemaWriter.WriteSchemaForDateTime(writer);
			}
			if (!options.IgnoreChildren)
			{
				writer.WritePropertyName("annotations");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForAnnotation(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			if (!options.IgnoreChildren && CompatibilityRestrictions.ExtendedProperty.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("extendedProperties");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForExtendedProperty(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001B8D RID: 7053 RVA: 0x000BD060 File Offset: 0x000BB260
		internal static void WriteSchemaForPerspectiveHierarchy(JsonWriter writer, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("description");
			writer.WriteValue("PerspectiveHierarchy object of Tabular Object Model (TOM)");
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("name");
			JsonSchemaWriter.WriteSchemaForString(writer);
			if (!options.IgnoreInferredProperties && !options.IgnoreTimestamps)
			{
				writer.WritePropertyName("modifiedTime");
				JsonSchemaWriter.WriteSchemaForDateTime(writer);
			}
			if (!options.IgnoreChildren)
			{
				writer.WritePropertyName("annotations");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForAnnotation(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			if (!options.IgnoreChildren && CompatibilityRestrictions.ExtendedProperty.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("extendedProperties");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForExtendedProperty(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001B8E RID: 7054 RVA: 0x000BD1A0 File Offset: 0x000BB3A0
		internal static void WriteSchemaForPerspectiveMeasure(JsonWriter writer, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("description");
			writer.WriteValue("PerspectiveMeasure object of Tabular Object Model (TOM)");
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("name");
			JsonSchemaWriter.WriteSchemaForString(writer);
			if (!options.IgnoreInferredProperties && !options.IgnoreTimestamps)
			{
				writer.WritePropertyName("modifiedTime");
				JsonSchemaWriter.WriteSchemaForDateTime(writer);
			}
			if (!options.IgnoreChildren)
			{
				writer.WritePropertyName("annotations");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForAnnotation(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			if (!options.IgnoreChildren && CompatibilityRestrictions.ExtendedProperty.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("extendedProperties");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForExtendedProperty(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001B8F RID: 7055 RVA: 0x000BD2E0 File Offset: 0x000BB4E0
		internal static void WriteSchemaForModelRole(JsonWriter writer, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("description");
			writer.WriteValue("ModelRole object of Tabular Object Model (TOM)");
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("name");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WritePropertyName("description");
			if (options.SplitMultilineStrings)
			{
				JsonSchemaWriter.WriteSchemaForMultilineString(writer);
			}
			else
			{
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			writer.WritePropertyName("modelPermission");
			writer.WriteStartObject();
			writer.WritePropertyName("enum");
			writer.WriteStartArray();
			writer.WriteValue("none");
			writer.WriteValue("read");
			writer.WriteValue("readRefresh");
			writer.WriteValue("refresh");
			writer.WriteValue("administrator");
			writer.WriteEndArray();
			writer.WriteEndObject();
			if (!options.IgnoreInferredProperties && !options.IgnoreTimestamps)
			{
				writer.WritePropertyName("modifiedTime");
				JsonSchemaWriter.WriteSchemaForDateTime(writer);
			}
			if (!options.IgnoreChildren)
			{
				writer.WritePropertyName("annotations");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForAnnotation(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			if (!options.IgnoreChildren && CompatibilityRestrictions.ExtendedProperty.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("extendedProperties");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForExtendedProperty(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			if (!options.IgnoreChildren && !options.IgnoreChildrenExceptAnnotations)
			{
				if (!options.IgnoreInferredObjects || !ObjectTreeHelper.IsInferredObject(ObjectType.RoleMembership))
				{
					writer.WritePropertyName("members");
					writer.WriteStartObject();
					writer.WritePropertyName("type");
					writer.WriteValue("array");
					writer.WritePropertyName("items");
					JsonSchemaWriter.WriteSchemaForModelRoleMember(writer, options, mode, dbCompatibilityLevel);
					writer.WriteEndObject();
				}
				if (!options.IgnoreInferredObjects || !ObjectTreeHelper.IsInferredObject(ObjectType.TablePermission))
				{
					writer.WritePropertyName("tablePermissions");
					writer.WriteStartObject();
					writer.WritePropertyName("type");
					writer.WriteValue("array");
					writer.WritePropertyName("items");
					JsonSchemaWriter.WriteSchemaForTablePermission(writer, options, mode, dbCompatibilityLevel);
					writer.WriteEndObject();
				}
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001B90 RID: 7056 RVA: 0x000BD560 File Offset: 0x000BB760
		internal static void WriteSchemaForTablePermission(JsonWriter writer, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("description");
			writer.WriteValue("TablePermission object of Tabular Object Model (TOM)");
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("name");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WritePropertyName("filterExpression");
			if (options.SplitMultilineStrings)
			{
				JsonSchemaWriter.WriteSchemaForMultilineString(writer);
			}
			else
			{
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			if (!options.IgnoreInferredProperties && !options.IgnoreTimestamps)
			{
				writer.WritePropertyName("modifiedTime");
				JsonSchemaWriter.WriteSchemaForDateTime(writer);
			}
			if (!options.IgnoreInferredProperties)
			{
				writer.WritePropertyName("state");
				writer.WriteStartObject();
				writer.WritePropertyName("enum");
				writer.WriteStartArray();
				writer.WriteValue("ready");
				writer.WriteValue("noData");
				writer.WriteValue("calculationNeeded");
				writer.WriteValue("semanticError");
				writer.WriteValue("evaluationError");
				writer.WriteValue("dependencyError");
				writer.WriteValue("incomplete");
				if (CompatibilityRestrictions.ObjectState_ForceCalculationNeeded.IsCompatible(mode, dbCompatibilityLevel))
				{
					writer.WriteValue("forceCalculationNeeded");
				}
				writer.WriteEndArray();
				writer.WriteEndObject();
			}
			if (!options.IgnoreInferredProperties)
			{
				writer.WritePropertyName("errorMessage");
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			if (CompatibilityRestrictions.TablePermission_MetadataPermission.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("metadataPermission");
				writer.WriteStartObject();
				writer.WritePropertyName("enum");
				writer.WriteStartArray();
				writer.WriteValue("default");
				writer.WriteValue("none");
				writer.WriteValue("read");
				writer.WriteEndArray();
				writer.WriteEndObject();
			}
			if (!options.IgnoreChildren)
			{
				writer.WritePropertyName("annotations");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForAnnotation(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			if (!options.IgnoreChildren && CompatibilityRestrictions.ExtendedProperty.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("extendedProperties");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForExtendedProperty(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			if (!options.IgnoreChildren && !options.IgnoreChildrenExceptAnnotations && (!options.IgnoreInferredObjects || !ObjectTreeHelper.IsInferredObject(ObjectType.ColumnPermission)) && CompatibilityRestrictions.ColumnPermission.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("columnPermissions");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForColumnPermission(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001B91 RID: 7057 RVA: 0x000BD848 File Offset: 0x000BBA48
		internal static void WriteSchemaForVariation(JsonWriter writer, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("description");
			writer.WriteValue("Variation object of Tabular Object Model (TOM)");
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("name");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WritePropertyName("description");
			if (options.SplitMultilineStrings)
			{
				JsonSchemaWriter.WriteSchemaForMultilineString(writer);
			}
			else
			{
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			writer.WritePropertyName("isDefault");
			JsonSchemaWriter.WriteSchemaForBoolean(writer);
			writer.WritePropertyName("relationship");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WritePropertyName("defaultHierarchy");
			writer.WriteStartObject();
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("table");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WritePropertyName("hierarchy");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WriteEndObject();
			writer.WriteEndObject();
			writer.WritePropertyName("defaultColumn");
			writer.WriteStartObject();
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("table");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WritePropertyName("column");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WriteEndObject();
			writer.WriteEndObject();
			if (!options.IgnoreChildren)
			{
				writer.WritePropertyName("annotations");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForAnnotation(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			if (!options.IgnoreChildren && CompatibilityRestrictions.ExtendedProperty.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("extendedProperties");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForExtendedProperty(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001B92 RID: 7058 RVA: 0x000BDA78 File Offset: 0x000BBC78
		internal static void WriteSchemaForSet(JsonWriter writer, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("description");
			writer.WriteValue("Set object of Tabular Object Model (TOM)");
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("name");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WritePropertyName("description");
			if (options.SplitMultilineStrings)
			{
				JsonSchemaWriter.WriteSchemaForMultilineString(writer);
			}
			else
			{
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			writer.WritePropertyName("expression");
			if (options.SplitMultilineStrings)
			{
				JsonSchemaWriter.WriteSchemaForMultilineString(writer);
			}
			else
			{
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			writer.WritePropertyName("isDynamic");
			JsonSchemaWriter.WriteSchemaForBoolean(writer);
			writer.WritePropertyName("isHidden");
			JsonSchemaWriter.WriteSchemaForBoolean(writer);
			if (!options.IgnoreInferredProperties)
			{
				writer.WritePropertyName("state");
				writer.WriteStartObject();
				writer.WritePropertyName("enum");
				writer.WriteStartArray();
				writer.WriteValue("ready");
				writer.WriteValue("noData");
				writer.WriteValue("calculationNeeded");
				writer.WriteValue("semanticError");
				writer.WriteValue("evaluationError");
				writer.WriteValue("dependencyError");
				writer.WriteValue("incomplete");
				if (CompatibilityRestrictions.ObjectState_ForceCalculationNeeded.IsCompatible(mode, dbCompatibilityLevel))
				{
					writer.WriteValue("forceCalculationNeeded");
				}
				writer.WriteEndArray();
				writer.WriteEndObject();
			}
			if (!options.IgnoreInferredProperties && !options.IgnoreTimestamps)
			{
				writer.WritePropertyName("modifiedTime");
				JsonSchemaWriter.WriteSchemaForDateTime(writer);
			}
			if (!options.IgnoreInferredProperties && !options.IgnoreTimestamps)
			{
				writer.WritePropertyName("structureModifiedTime");
				JsonSchemaWriter.WriteSchemaForDateTime(writer);
			}
			if (!options.IgnoreInferredProperties)
			{
				writer.WritePropertyName("errorMessage");
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			writer.WritePropertyName("displayFolder");
			JsonSchemaWriter.WriteSchemaForString(writer);
			if (!options.IgnoreChildren)
			{
				writer.WritePropertyName("annotations");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForAnnotation(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			if (!options.IgnoreChildren && CompatibilityRestrictions.ExtendedProperty.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("extendedProperties");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForExtendedProperty(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001B93 RID: 7059 RVA: 0x000BDD08 File Offset: 0x000BBF08
		internal static void WriteSchemaForPerspectiveSet(JsonWriter writer, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("description");
			writer.WriteValue("PerspectiveSet object of Tabular Object Model (TOM)");
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("name");
			JsonSchemaWriter.WriteSchemaForString(writer);
			if (!options.IgnoreInferredProperties && !options.IgnoreTimestamps)
			{
				writer.WritePropertyName("modifiedTime");
				JsonSchemaWriter.WriteSchemaForDateTime(writer);
			}
			if (!options.IgnoreChildren)
			{
				writer.WritePropertyName("annotations");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForAnnotation(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			if (!options.IgnoreChildren && CompatibilityRestrictions.ExtendedProperty.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("extendedProperties");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForExtendedProperty(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001B94 RID: 7060 RVA: 0x000BDE48 File Offset: 0x000BC048
		internal static void WriteSchemaForNamedExpression(JsonWriter writer, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("description");
			writer.WriteValue("NamedExpression object of Tabular Object Model (TOM)");
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("name");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WritePropertyName("description");
			if (options.SplitMultilineStrings)
			{
				JsonSchemaWriter.WriteSchemaForMultilineString(writer);
			}
			else
			{
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			writer.WritePropertyName("kind");
			writer.WriteStartObject();
			writer.WritePropertyName("enum");
			writer.WriteStartArray();
			writer.WriteValue("m");
			writer.WriteEndArray();
			writer.WriteEndObject();
			writer.WritePropertyName("expression");
			if (options.SplitMultilineStrings)
			{
				JsonSchemaWriter.WriteSchemaForMultilineString(writer);
			}
			else
			{
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			if (!options.IgnoreInferredProperties && !options.IgnoreTimestamps)
			{
				writer.WritePropertyName("modifiedTime");
				JsonSchemaWriter.WriteSchemaForDateTime(writer);
			}
			if (CompatibilityRestrictions.NamedExpression_MAttributes.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("mAttributes");
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			if (CompatibilityRestrictions.NamedExpression_LineageTag.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("lineageTag");
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			if (CompatibilityRestrictions.NamedExpression_SourceLineageTag.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("sourceLineageTag");
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			if (CompatibilityRestrictions.NamedExpression_RemoteParameterName.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("remoteParameterName");
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			if (CompatibilityRestrictions.NamedExpression_QueryGroup.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("queryGroup");
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			if (CompatibilityRestrictions.NamedExpression_ParameterValuesColumn.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("parameterValuesColumn");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("object");
				writer.WritePropertyName("properties");
				writer.WriteStartObject();
				writer.WritePropertyName("table");
				JsonSchemaWriter.WriteSchemaForString(writer);
				writer.WritePropertyName("column");
				JsonSchemaWriter.WriteSchemaForString(writer);
				writer.WriteEndObject();
				writer.WriteEndObject();
			}
			if (CompatibilityRestrictions.NamedExpression_ExpressionSource.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("expressionSource");
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			if (!options.IgnoreChildren)
			{
				writer.WritePropertyName("annotations");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForAnnotation(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			if (!options.IgnoreChildren && CompatibilityRestrictions.ExtendedProperty.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("extendedProperties");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForExtendedProperty(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			if (!options.IgnoreChildren && CompatibilityRestrictions.ExcludedArtifact.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("excludedArtifacts");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForExcludedArtifact(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001B95 RID: 7061 RVA: 0x000BE188 File Offset: 0x000BC388
		internal static void WriteSchemaForColumnPermission(JsonWriter writer, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("description");
			writer.WriteValue("ColumnPermission object of Tabular Object Model (TOM)");
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("name");
			JsonSchemaWriter.WriteSchemaForString(writer);
			if (!options.IgnoreInferredProperties && !options.IgnoreTimestamps)
			{
				writer.WritePropertyName("modifiedTime");
				JsonSchemaWriter.WriteSchemaForDateTime(writer);
			}
			writer.WritePropertyName("metadataPermission");
			writer.WriteStartObject();
			writer.WritePropertyName("enum");
			writer.WriteStartArray();
			writer.WriteValue("default");
			writer.WriteValue("none");
			writer.WriteValue("read");
			writer.WriteEndArray();
			writer.WriteEndObject();
			if (!options.IgnoreChildren)
			{
				writer.WritePropertyName("annotations");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForAnnotation(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			if (!options.IgnoreChildren && CompatibilityRestrictions.ExtendedProperty.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("extendedProperties");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForExtendedProperty(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001B96 RID: 7062 RVA: 0x000BE318 File Offset: 0x000BC518
		internal static void WriteSchemaForDetailRowsDefinition(JsonWriter writer, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("description");
			writer.WriteValue("DetailRowsDefinition object of Tabular Object Model (TOM)");
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("expression");
			if (options.SplitMultilineStrings)
			{
				JsonSchemaWriter.WriteSchemaForMultilineString(writer);
			}
			else
			{
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			if (!options.IgnoreInferredProperties && !options.IgnoreTimestamps)
			{
				writer.WritePropertyName("modifiedTime");
				JsonSchemaWriter.WriteSchemaForDateTime(writer);
			}
			if (!options.IgnoreInferredProperties)
			{
				writer.WritePropertyName("state");
				writer.WriteStartObject();
				writer.WritePropertyName("enum");
				writer.WriteStartArray();
				writer.WriteValue("ready");
				writer.WriteValue("noData");
				writer.WriteValue("calculationNeeded");
				writer.WriteValue("semanticError");
				writer.WriteValue("evaluationError");
				writer.WriteValue("dependencyError");
				writer.WriteValue("incomplete");
				if (CompatibilityRestrictions.ObjectState_ForceCalculationNeeded.IsCompatible(mode, dbCompatibilityLevel))
				{
					writer.WriteValue("forceCalculationNeeded");
				}
				writer.WriteEndArray();
				writer.WriteEndObject();
			}
			if (!options.IgnoreInferredProperties)
			{
				writer.WritePropertyName("errorMessage");
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001B97 RID: 7063 RVA: 0x000BE480 File Offset: 0x000BC680
		internal static void WriteSchemaForRelatedColumnDetails(JsonWriter writer, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("description");
			writer.WriteValue("RelatedColumnDetails object of Tabular Object Model (TOM)");
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			if (!options.IgnoreInferredProperties && !options.IgnoreTimestamps)
			{
				writer.WritePropertyName("modifiedTime");
				JsonSchemaWriter.WriteSchemaForDateTime(writer);
			}
			if (!options.IgnoreChildren && !options.IgnoreChildrenExceptAnnotations && (!options.IgnoreInferredObjects || !ObjectTreeHelper.IsInferredObject(ObjectType.GroupByColumn)) && CompatibilityRestrictions.GroupByColumn.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("groupByColumns");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForGroupByColumn(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001B98 RID: 7064 RVA: 0x000BE580 File Offset: 0x000BC780
		internal static void WriteSchemaForGroupByColumn(JsonWriter writer, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("description");
			writer.WriteValue("GroupByColumn object of Tabular Object Model (TOM)");
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			if (!options.IgnoreInferredProperties && !options.IgnoreTimestamps)
			{
				writer.WritePropertyName("modifiedTime");
				JsonSchemaWriter.WriteSchemaForDateTime(writer);
			}
			writer.WritePropertyName("groupingColumn");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001B99 RID: 7065 RVA: 0x000BE620 File Offset: 0x000BC820
		internal static void WriteSchemaForCalculationGroup(JsonWriter writer, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("description");
			writer.WriteValue("CalculationGroup object of Tabular Object Model (TOM)");
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("description");
			if (options.SplitMultilineStrings)
			{
				JsonSchemaWriter.WriteSchemaForMultilineString(writer);
			}
			else
			{
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			if (!options.IgnoreInferredProperties && !options.IgnoreTimestamps)
			{
				writer.WritePropertyName("modifiedTime");
				JsonSchemaWriter.WriteSchemaForDateTime(writer);
			}
			writer.WritePropertyName("precedence");
			JsonSchemaWriter.WriteSchemaForInteger(writer);
			if (!options.IgnoreChildren && !options.IgnoreChildrenExceptAnnotations && CompatibilityRestrictions.CalculationGroupExpression.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("multipleOrEmptySelectionExpression");
				JsonSchemaWriter.WriteSchemaForCalculationGroupExpression(writer, options, mode, dbCompatibilityLevel);
				writer.WritePropertyName("noSelectionExpression");
				JsonSchemaWriter.WriteSchemaForCalculationGroupExpression(writer, options, mode, dbCompatibilityLevel);
			}
			if (!options.IgnoreChildren)
			{
				writer.WritePropertyName("annotations");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForAnnotation(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			if (!options.IgnoreChildren && !options.IgnoreChildrenExceptAnnotations && (!options.IgnoreInferredObjects || !ObjectTreeHelper.IsInferredObject(ObjectType.CalculationItem)) && CompatibilityRestrictions.CalculationItem.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("calculationItems");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForCalculationItem(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001B9A RID: 7066 RVA: 0x000BE7E0 File Offset: 0x000BC9E0
		internal static void WriteSchemaForCalculationItem(JsonWriter writer, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("description");
			writer.WriteValue("CalculationItem object of Tabular Object Model (TOM)");
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("name");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WritePropertyName("description");
			if (options.SplitMultilineStrings)
			{
				JsonSchemaWriter.WriteSchemaForMultilineString(writer);
			}
			else
			{
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			if (!options.IgnoreInferredProperties && !options.IgnoreTimestamps)
			{
				writer.WritePropertyName("modifiedTime");
				JsonSchemaWriter.WriteSchemaForDateTime(writer);
			}
			if (!options.IgnoreInferredProperties)
			{
				writer.WritePropertyName("state");
				writer.WriteStartObject();
				writer.WritePropertyName("enum");
				writer.WriteStartArray();
				writer.WriteValue("ready");
				writer.WriteValue("noData");
				writer.WriteValue("calculationNeeded");
				writer.WriteValue("semanticError");
				writer.WriteValue("evaluationError");
				writer.WriteValue("dependencyError");
				writer.WriteValue("incomplete");
				if (CompatibilityRestrictions.ObjectState_ForceCalculationNeeded.IsCompatible(mode, dbCompatibilityLevel))
				{
					writer.WriteValue("forceCalculationNeeded");
				}
				writer.WriteEndArray();
				writer.WriteEndObject();
			}
			if (!options.IgnoreInferredProperties)
			{
				writer.WritePropertyName("errorMessage");
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			writer.WritePropertyName("expression");
			if (options.SplitMultilineStrings)
			{
				JsonSchemaWriter.WriteSchemaForMultilineString(writer);
			}
			else
			{
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			if (CompatibilityRestrictions.CalculationItem_Ordinal.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("ordinal");
				JsonSchemaWriter.WriteSchemaForInteger(writer);
			}
			if (!options.IgnoreChildren && !options.IgnoreChildrenExceptAnnotations && (!options.IgnoreInferredObjects || !ObjectTreeHelper.IsInferredObject(ObjectType.FormatStringDefinition)))
			{
				writer.WritePropertyName("formatStringDefinition");
				JsonSchemaWriter.WriteSchemaForFormatStringDefinition(writer, options, mode, dbCompatibilityLevel);
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001B9B RID: 7067 RVA: 0x000BE9D0 File Offset: 0x000BCBD0
		internal static void WriteSchemaForAlternateOf(JsonWriter writer, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("description");
			writer.WriteValue("AlternateOf object of Tabular Object Model (TOM)");
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("summarization");
			writer.WriteStartObject();
			writer.WritePropertyName("enum");
			writer.WriteStartArray();
			writer.WriteValue("groupBy");
			writer.WriteValue("sum");
			writer.WriteValue("count");
			writer.WriteValue("min");
			writer.WriteValue("max");
			writer.WriteEndArray();
			writer.WriteEndObject();
			writer.WritePropertyName("baseColumn");
			writer.WriteStartObject();
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("table");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WritePropertyName("column");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WriteEndObject();
			writer.WriteEndObject();
			writer.WritePropertyName("baseTable");
			JsonSchemaWriter.WriteSchemaForString(writer);
			if (!options.IgnoreChildren)
			{
				writer.WritePropertyName("annotations");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForAnnotation(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001B9C RID: 7068 RVA: 0x000BEB64 File Offset: 0x000BCD64
		internal static void WriteSchemaForFormatStringDefinition(JsonWriter writer, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("description");
			writer.WriteValue("FormatStringDefinition object of Tabular Object Model (TOM)");
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("expression");
			if (options.SplitMultilineStrings)
			{
				JsonSchemaWriter.WriteSchemaForMultilineString(writer);
			}
			else
			{
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			if (!options.IgnoreInferredProperties && !options.IgnoreTimestamps)
			{
				writer.WritePropertyName("modifiedTime");
				JsonSchemaWriter.WriteSchemaForDateTime(writer);
			}
			if (!options.IgnoreInferredProperties)
			{
				writer.WritePropertyName("state");
				writer.WriteStartObject();
				writer.WritePropertyName("enum");
				writer.WriteStartArray();
				writer.WriteValue("ready");
				writer.WriteValue("noData");
				writer.WriteValue("calculationNeeded");
				writer.WriteValue("semanticError");
				writer.WriteValue("evaluationError");
				writer.WriteValue("dependencyError");
				writer.WriteValue("incomplete");
				if (CompatibilityRestrictions.ObjectState_ForceCalculationNeeded.IsCompatible(mode, dbCompatibilityLevel))
				{
					writer.WriteValue("forceCalculationNeeded");
				}
				writer.WriteEndArray();
				writer.WriteEndObject();
			}
			if (!options.IgnoreInferredProperties)
			{
				writer.WritePropertyName("errorMessage");
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001B9D RID: 7069 RVA: 0x000BECCC File Offset: 0x000BCECC
		internal static void WriteSchemaForQueryGroup(JsonWriter writer, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("description");
			writer.WriteValue("QueryGroup object of Tabular Object Model (TOM)");
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("folder");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WritePropertyName("description");
			if (options.SplitMultilineStrings)
			{
				JsonSchemaWriter.WriteSchemaForMultilineString(writer);
			}
			else
			{
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			if (!options.IgnoreChildren)
			{
				writer.WritePropertyName("annotations");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForAnnotation(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001B9E RID: 7070 RVA: 0x000BEDB8 File Offset: 0x000BCFB8
		internal static void WriteSchemaForAnalyticsAIMetadata(JsonWriter writer, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("description");
			writer.WriteValue("AnalyticsAIMetadata object of Tabular Object Model (TOM)");
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("name");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WritePropertyName("measureAnalysisDefinition");
			JsonSchemaWriter.WriteSchemaForGenericObject(writer);
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001B9F RID: 7071 RVA: 0x000BEE48 File Offset: 0x000BD048
		internal static void WriteSchemaForChangedProperty(JsonWriter writer, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("description");
			writer.WriteValue("ChangedProperty object of Tabular Object Model (TOM)");
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("property");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001BA0 RID: 7072 RVA: 0x000BEEC8 File Offset: 0x000BD0C8
		internal static void WriteSchemaForExcludedArtifact(JsonWriter writer, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("description");
			writer.WriteValue("ExcludedArtifact object of Tabular Object Model (TOM)");
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("artifactType");
			JsonSchemaWriter.WriteSchemaForInteger(writer);
			writer.WritePropertyName("reference");
			JsonSchemaWriter.WriteSchemaForGenericObject(writer);
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001BA1 RID: 7073 RVA: 0x000BEF58 File Offset: 0x000BD158
		internal static void WriteSchemaForDataCoverageDefinition(JsonWriter writer, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("description");
			writer.WriteValue("DataCoverageDefinition object of Tabular Object Model (TOM)");
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("description");
			if (options.SplitMultilineStrings)
			{
				JsonSchemaWriter.WriteSchemaForMultilineString(writer);
			}
			else
			{
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			writer.WritePropertyName("expression");
			if (options.SplitMultilineStrings)
			{
				JsonSchemaWriter.WriteSchemaForMultilineString(writer);
			}
			else
			{
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			if (!options.IgnoreInferredProperties && !options.IgnoreTimestamps)
			{
				writer.WritePropertyName("modifiedTime");
				JsonSchemaWriter.WriteSchemaForDateTime(writer);
			}
			if (!options.IgnoreInferredProperties)
			{
				writer.WritePropertyName("state");
				writer.WriteStartObject();
				writer.WritePropertyName("enum");
				writer.WriteStartArray();
				writer.WriteValue("ready");
				writer.WriteValue("noData");
				writer.WriteValue("calculationNeeded");
				writer.WriteValue("semanticError");
				writer.WriteValue("evaluationError");
				writer.WriteValue("dependencyError");
				writer.WriteValue("incomplete");
				if (CompatibilityRestrictions.ObjectState_ForceCalculationNeeded.IsCompatible(mode, dbCompatibilityLevel))
				{
					writer.WriteValue("forceCalculationNeeded");
				}
				writer.WriteEndArray();
				writer.WriteEndObject();
			}
			if (!options.IgnoreInferredProperties)
			{
				writer.WritePropertyName("errorMessage");
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			if (!options.IgnoreChildren)
			{
				writer.WritePropertyName("annotations");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForAnnotation(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001BA2 RID: 7074 RVA: 0x000BF12C File Offset: 0x000BD32C
		internal static void WriteSchemaForCalculationGroupExpression(JsonWriter writer, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("description");
			writer.WriteValue("CalculationGroupExpression object of Tabular Object Model (TOM)");
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("description");
			if (options.SplitMultilineStrings)
			{
				JsonSchemaWriter.WriteSchemaForMultilineString(writer);
			}
			else
			{
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			writer.WritePropertyName("expression");
			if (options.SplitMultilineStrings)
			{
				JsonSchemaWriter.WriteSchemaForMultilineString(writer);
			}
			else
			{
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			if (!options.IgnoreInferredProperties && !options.IgnoreTimestamps)
			{
				writer.WritePropertyName("modifiedTime");
				JsonSchemaWriter.WriteSchemaForDateTime(writer);
			}
			if (!options.IgnoreInferredProperties)
			{
				writer.WritePropertyName("state");
				writer.WriteStartObject();
				writer.WritePropertyName("enum");
				writer.WriteStartArray();
				writer.WriteValue("ready");
				writer.WriteValue("noData");
				writer.WriteValue("calculationNeeded");
				writer.WriteValue("semanticError");
				writer.WriteValue("evaluationError");
				writer.WriteValue("dependencyError");
				writer.WriteValue("incomplete");
				if (CompatibilityRestrictions.ObjectState_ForceCalculationNeeded.IsCompatible(mode, dbCompatibilityLevel))
				{
					writer.WriteValue("forceCalculationNeeded");
				}
				writer.WriteEndArray();
				writer.WriteEndObject();
			}
			if (!options.IgnoreInferredProperties)
			{
				writer.WritePropertyName("errorMessage");
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			if (!options.IgnoreChildren && !options.IgnoreChildrenExceptAnnotations && (!options.IgnoreInferredObjects || !ObjectTreeHelper.IsInferredObject(ObjectType.FormatStringDefinition)))
			{
				writer.WritePropertyName("formatStringDefinition");
				JsonSchemaWriter.WriteSchemaForFormatStringDefinition(writer, options, mode, dbCompatibilityLevel);
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001BA3 RID: 7075 RVA: 0x000BF2EC File Offset: 0x000BD4EC
		internal static void WriteSchemaForCalendar(JsonWriter writer, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("description");
			writer.WriteValue("Calendar object of Tabular Object Model (TOM)");
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("name");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WritePropertyName("description");
			if (options.SplitMultilineStrings)
			{
				JsonSchemaWriter.WriteSchemaForMultilineString(writer);
			}
			else
			{
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			writer.WritePropertyName("lineageTag");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WritePropertyName("sourceLineageTag");
			JsonSchemaWriter.WriteSchemaForString(writer);
			if (!options.IgnoreInferredProperties && !options.IgnoreTimestamps)
			{
				writer.WritePropertyName("modifiedTime");
				JsonSchemaWriter.WriteSchemaForDateTime(writer);
			}
			if (!options.IgnoreChildren && !options.IgnoreChildrenExceptAnnotations && (!options.IgnoreInferredObjects || !ObjectTreeHelper.IsInferredObject(ObjectType.TimeUnitColumnAssociation)) && CompatibilityRestrictions.TimeUnitColumnAssociation.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("timeUnitColumnAssociations");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForTimeUnitColumnAssociation(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001BA4 RID: 7076 RVA: 0x000BF440 File Offset: 0x000BD640
		internal static void WriteSchemaForTimeUnitColumnAssociation(JsonWriter writer, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("description");
			writer.WriteValue("TimeUnitColumnAssociation object of Tabular Object Model (TOM)");
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("timeUnit");
			writer.WriteStartObject();
			writer.WritePropertyName("enum");
			writer.WriteStartArray();
			writer.WriteValue("unknown");
			writer.WriteValue("year");
			writer.WriteValue("semester");
			writer.WriteValue("semesterOfYear");
			writer.WriteValue("quarter");
			writer.WriteValue("quarterOfYear");
			writer.WriteValue("quarterOfSemester");
			writer.WriteValue("month");
			writer.WriteValue("monthOfYear");
			writer.WriteValue("monthOfSemester");
			writer.WriteValue("monthOfQuarter");
			writer.WriteValue("week");
			writer.WriteValue("weekOfYear");
			writer.WriteValue("weekOfSemester");
			writer.WriteValue("weekOfQuarter");
			writer.WriteValue("weekOfMonth");
			writer.WriteValue("date");
			writer.WriteValue("dayOfYear");
			writer.WriteValue("dayOfSemester");
			writer.WriteValue("dayOfQuarter");
			writer.WriteValue("dayOfMonth");
			writer.WriteValue("dayOfWeek");
			writer.WriteEndArray();
			writer.WriteEndObject();
			if (!options.IgnoreInferredProperties && !options.IgnoreTimestamps)
			{
				writer.WritePropertyName("modifiedTime");
				JsonSchemaWriter.WriteSchemaForDateTime(writer);
			}
			JsonSchemaWriter.WriteAdditionalSchemaPropertiesForTimeUnitColumnAssociation(writer, options, mode, dbCompatibilityLevel);
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001BA5 RID: 7077 RVA: 0x000BF5F8 File Offset: 0x000BD7F8
		private static void WriteAdditionalSchemaPropertiesForTimeUnitColumnAssociation(JsonWriter writer, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WritePropertyName("primaryColumn");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WritePropertyName("associatedColumns");
			JsonSchemaWriter.WriteSchemaForStringArray(writer, null, true);
		}

		// Token: 0x06001BA6 RID: 7078 RVA: 0x000BF634 File Offset: 0x000BD834
		internal static void WriteSchemaForFunction(JsonWriter writer, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("description");
			writer.WriteValue("Function object of Tabular Object Model (TOM)");
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("name");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WritePropertyName("description");
			if (options.SplitMultilineStrings)
			{
				JsonSchemaWriter.WriteSchemaForMultilineString(writer);
			}
			else
			{
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			writer.WritePropertyName("expression");
			if (options.SplitMultilineStrings)
			{
				JsonSchemaWriter.WriteSchemaForMultilineString(writer);
			}
			else
			{
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			writer.WritePropertyName("isHidden");
			JsonSchemaWriter.WriteSchemaForBoolean(writer);
			if (!options.IgnoreInferredProperties)
			{
				writer.WritePropertyName("state");
				writer.WriteStartObject();
				writer.WritePropertyName("enum");
				writer.WriteStartArray();
				writer.WriteValue("ready");
				writer.WriteValue("noData");
				writer.WriteValue("calculationNeeded");
				writer.WriteValue("semanticError");
				writer.WriteValue("evaluationError");
				writer.WriteValue("dependencyError");
				writer.WriteValue("incomplete");
				if (CompatibilityRestrictions.ObjectState_ForceCalculationNeeded.IsCompatible(mode, dbCompatibilityLevel))
				{
					writer.WriteValue("forceCalculationNeeded");
				}
				writer.WriteEndArray();
				writer.WriteEndObject();
			}
			if (!options.IgnoreInferredProperties)
			{
				writer.WritePropertyName("errorMessage");
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			if (!options.IgnoreInferredProperties && !options.IgnoreTimestamps)
			{
				writer.WritePropertyName("modifiedTime");
				JsonSchemaWriter.WriteSchemaForDateTime(writer);
			}
			if (!options.IgnoreInferredProperties && !options.IgnoreTimestamps)
			{
				writer.WritePropertyName("structureModifiedTime");
				JsonSchemaWriter.WriteSchemaForDateTime(writer);
			}
			writer.WritePropertyName("lineageTag");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WritePropertyName("sourceLineageTag");
			JsonSchemaWriter.WriteSchemaForString(writer);
			if (!options.IgnoreChildren)
			{
				writer.WritePropertyName("annotations");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForAnnotation(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			if (!options.IgnoreChildren && CompatibilityRestrictions.ExtendedProperty.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("extendedProperties");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForExtendedProperty(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			if (!options.IgnoreChildren && CompatibilityRestrictions.ChangedProperty.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("changedProperties");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForChangedProperty(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001BA7 RID: 7079 RVA: 0x000BF91C File Offset: 0x000BDB1C
		internal static void WriteSchemaForProviderDataSource(JsonWriter writer, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("description");
			writer.WriteValue("ProviderDataSource object of Tabular Object Model (TOM)");
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("name");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WritePropertyName("description");
			if (options.SplitMultilineStrings)
			{
				JsonSchemaWriter.WriteSchemaForMultilineString(writer);
			}
			else
			{
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			writer.WritePropertyName("type");
			writer.WriteStartObject();
			writer.WritePropertyName("enum");
			writer.WriteStartArray();
			writer.WriteValue("provider");
			if (CompatibilityRestrictions.DataSourceType_Structured.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WriteValue("structured");
			}
			writer.WriteEndArray();
			writer.WriteEndObject();
			writer.WritePropertyName("maxConnections");
			JsonSchemaWriter.WriteSchemaForInteger(writer);
			if (!options.IgnoreInferredProperties && !options.IgnoreTimestamps)
			{
				writer.WritePropertyName("modifiedTime");
				JsonSchemaWriter.WriteSchemaForDateTime(writer);
			}
			writer.WritePropertyName("connectionString");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WritePropertyName("impersonationMode");
			writer.WriteStartObject();
			writer.WritePropertyName("enum");
			writer.WriteStartArray();
			writer.WriteValue("default");
			writer.WriteValue("impersonateAccount");
			writer.WriteValue("impersonateAnonymous");
			writer.WriteValue("impersonateCurrentUser");
			writer.WriteValue("impersonateServiceAccount");
			writer.WriteValue("impersonateUnattendedAccount");
			writer.WriteEndArray();
			writer.WriteEndObject();
			writer.WritePropertyName("account");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WritePropertyName("password");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WritePropertyName("isolation");
			writer.WriteStartObject();
			writer.WritePropertyName("enum");
			writer.WriteStartArray();
			writer.WriteValue("readCommitted");
			writer.WriteValue("snapshot");
			writer.WriteEndArray();
			writer.WriteEndObject();
			writer.WritePropertyName("timeout");
			JsonSchemaWriter.WriteSchemaForInteger(writer);
			writer.WritePropertyName("provider");
			JsonSchemaWriter.WriteSchemaForString(writer);
			if (!options.IgnoreChildren)
			{
				writer.WritePropertyName("annotations");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForAnnotation(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			if (!options.IgnoreChildren && CompatibilityRestrictions.ExtendedProperty.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("extendedProperties");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForExtendedProperty(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001BA8 RID: 7080 RVA: 0x000BFBEC File Offset: 0x000BDDEC
		internal static void WriteSchemaForStructuredDataSource(JsonWriter writer, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("description");
			writer.WriteValue("StructuredDataSource object of Tabular Object Model (TOM)");
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("name");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WritePropertyName("description");
			if (options.SplitMultilineStrings)
			{
				JsonSchemaWriter.WriteSchemaForMultilineString(writer);
			}
			else
			{
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			writer.WritePropertyName("type");
			writer.WriteStartObject();
			writer.WritePropertyName("enum");
			writer.WriteStartArray();
			writer.WriteValue("provider");
			if (CompatibilityRestrictions.DataSourceType_Structured.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WriteValue("structured");
			}
			writer.WriteEndArray();
			writer.WriteEndObject();
			writer.WritePropertyName("maxConnections");
			JsonSchemaWriter.WriteSchemaForInteger(writer);
			if (!options.IgnoreInferredProperties && !options.IgnoreTimestamps)
			{
				writer.WritePropertyName("modifiedTime");
				JsonSchemaWriter.WriteSchemaForDateTime(writer);
			}
			if (CompatibilityRestrictions.StructuredDataSource_ConnectionDetails.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("connectionDetails");
				JsonSchemaWriter.WriteSchemaForGenericObject(writer);
			}
			if (CompatibilityRestrictions.StructuredDataSource_Options.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("options");
				JsonSchemaWriter.WriteSchemaForGenericObject(writer);
			}
			if (CompatibilityRestrictions.StructuredDataSource_Credential.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("credential");
				JsonSchemaWriter.WriteSchemaForGenericObject(writer);
			}
			if (CompatibilityRestrictions.StructuredDataSource_ContextExpression.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("contextExpression");
				if (options.SplitMultilineStrings)
				{
					JsonSchemaWriter.WriteSchemaForMultilineString(writer);
				}
				else
				{
					JsonSchemaWriter.WriteSchemaForString(writer);
				}
			}
			if (!options.IgnoreChildren)
			{
				writer.WritePropertyName("annotations");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForAnnotation(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			if (!options.IgnoreChildren && CompatibilityRestrictions.ExtendedProperty.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("extendedProperties");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForExtendedProperty(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001BA9 RID: 7081 RVA: 0x000BFE3C File Offset: 0x000BE03C
		internal static void WriteSchemaForDataColumn(JsonWriter writer, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("description");
			writer.WriteValue("DataColumn object of Tabular Object Model (TOM)");
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("name");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WritePropertyName("dataType");
			writer.WriteStartObject();
			writer.WritePropertyName("enum");
			writer.WriteStartArray();
			writer.WriteValue("automatic");
			writer.WriteValue("string");
			writer.WriteValue("int64");
			writer.WriteValue("double");
			writer.WriteValue("dateTime");
			writer.WriteValue("decimal");
			writer.WriteValue("boolean");
			writer.WriteValue("binary");
			writer.WriteValue("unknown");
			writer.WriteValue("variant");
			writer.WriteEndArray();
			writer.WriteEndObject();
			writer.WritePropertyName("dataCategory");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WritePropertyName("description");
			if (options.SplitMultilineStrings)
			{
				JsonSchemaWriter.WriteSchemaForMultilineString(writer);
			}
			else
			{
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			writer.WritePropertyName("isHidden");
			JsonSchemaWriter.WriteSchemaForBoolean(writer);
			if (!options.IgnoreInferredProperties)
			{
				writer.WritePropertyName("state");
				writer.WriteStartObject();
				writer.WritePropertyName("enum");
				writer.WriteStartArray();
				writer.WriteValue("ready");
				writer.WriteValue("noData");
				writer.WriteValue("calculationNeeded");
				writer.WriteValue("semanticError");
				writer.WriteValue("evaluationError");
				writer.WriteValue("dependencyError");
				writer.WriteValue("incomplete");
				if (CompatibilityRestrictions.ObjectState_ForceCalculationNeeded.IsCompatible(mode, dbCompatibilityLevel))
				{
					writer.WriteValue("forceCalculationNeeded");
				}
				writer.WriteEndArray();
				writer.WriteEndObject();
			}
			writer.WritePropertyName("isUnique");
			JsonSchemaWriter.WriteSchemaForBoolean(writer);
			writer.WritePropertyName("isKey");
			JsonSchemaWriter.WriteSchemaForBoolean(writer);
			writer.WritePropertyName("isNullable");
			JsonSchemaWriter.WriteSchemaForBoolean(writer);
			writer.WritePropertyName("alignment");
			writer.WriteStartObject();
			writer.WritePropertyName("enum");
			writer.WriteStartArray();
			writer.WriteValue("default");
			writer.WriteValue("left");
			writer.WriteValue("right");
			writer.WriteValue("center");
			writer.WriteEndArray();
			writer.WriteEndObject();
			writer.WritePropertyName("tableDetailPosition");
			JsonSchemaWriter.WriteSchemaForInteger(writer);
			writer.WritePropertyName("isDefaultLabel");
			JsonSchemaWriter.WriteSchemaForBoolean(writer);
			writer.WritePropertyName("isDefaultImage");
			JsonSchemaWriter.WriteSchemaForBoolean(writer);
			writer.WritePropertyName("summarizeBy");
			writer.WriteStartObject();
			writer.WritePropertyName("enum");
			writer.WriteStartArray();
			writer.WriteValue("default");
			writer.WriteValue("none");
			writer.WriteValue("sum");
			writer.WriteValue("min");
			writer.WriteValue("max");
			writer.WriteValue("count");
			writer.WriteValue("average");
			writer.WriteValue("distinctCount");
			writer.WriteEndArray();
			writer.WriteEndObject();
			writer.WritePropertyName("type");
			writer.WriteStartObject();
			writer.WritePropertyName("enum");
			writer.WriteStartArray();
			writer.WriteValue("data");
			writer.WriteValue("calculated");
			writer.WriteValue("rowNumber");
			writer.WriteValue("calculatedTableColumn");
			writer.WriteEndArray();
			writer.WriteEndObject();
			writer.WritePropertyName("formatString");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WritePropertyName("isAvailableInMdx");
			JsonSchemaWriter.WriteSchemaForBoolean(writer);
			if (!options.IgnoreInferredProperties && !options.IgnoreTimestamps)
			{
				writer.WritePropertyName("modifiedTime");
				JsonSchemaWriter.WriteSchemaForDateTime(writer);
			}
			if (!options.IgnoreInferredProperties && !options.IgnoreTimestamps)
			{
				writer.WritePropertyName("structureModifiedTime");
				JsonSchemaWriter.WriteSchemaForDateTime(writer);
			}
			if (!options.IgnoreInferredProperties && !options.IgnoreTimestamps)
			{
				writer.WritePropertyName("refreshedTime");
				JsonSchemaWriter.WriteSchemaForDateTime(writer);
			}
			writer.WritePropertyName("keepUniqueRows");
			JsonSchemaWriter.WriteSchemaForBoolean(writer);
			writer.WritePropertyName("displayOrdinal");
			JsonSchemaWriter.WriteSchemaForInteger(writer);
			if (!options.IgnoreInferredProperties)
			{
				writer.WritePropertyName("errorMessage");
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			writer.WritePropertyName("sourceProviderType");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WritePropertyName("displayFolder");
			JsonSchemaWriter.WriteSchemaForString(writer);
			if (CompatibilityRestrictions.Column_EncodingHint.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("encodingHint");
				writer.WriteStartObject();
				writer.WritePropertyName("enum");
				writer.WriteStartArray();
				writer.WriteValue("default");
				writer.WriteValue("hash");
				writer.WriteValue("value");
				writer.WriteEndArray();
				writer.WriteEndObject();
			}
			if (CompatibilityRestrictions.Column_LineageTag.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("lineageTag");
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			if (CompatibilityRestrictions.Column_SourceLineageTag.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("sourceLineageTag");
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			writer.WritePropertyName("isDataTypeInferred");
			JsonSchemaWriter.WriteSchemaForBoolean(writer);
			writer.WritePropertyName("sourceColumn");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WritePropertyName("sortByColumn");
			JsonSchemaWriter.WriteSchemaForString(writer);
			if (!options.IgnoreChildren && !options.IgnoreChildrenExceptAnnotations)
			{
				if (!options.IgnoreInferredObjects || !ObjectTreeHelper.IsInferredObject(ObjectType.AttributeHierarchy))
				{
					writer.WritePropertyName("attributeHierarchy");
					JsonSchemaWriter.WriteSchemaForAttributeHierarchy(writer, options, mode, dbCompatibilityLevel);
				}
				if ((!options.IgnoreInferredObjects || !ObjectTreeHelper.IsInferredObject(ObjectType.RelatedColumnDetails)) && CompatibilityRestrictions.Column_RelatedColumnDetails.IsCompatible(mode, dbCompatibilityLevel))
				{
					writer.WritePropertyName("relatedColumnDetails");
					JsonSchemaWriter.WriteSchemaForRelatedColumnDetails(writer, options, mode, dbCompatibilityLevel);
				}
				if ((!options.IgnoreInferredObjects || !ObjectTreeHelper.IsInferredObject(ObjectType.AlternateOf)) && CompatibilityRestrictions.Column_AlternateOf.IsCompatible(mode, dbCompatibilityLevel))
				{
					writer.WritePropertyName("alternateOf");
					JsonSchemaWriter.WriteSchemaForAlternateOf(writer, options, mode, dbCompatibilityLevel);
				}
			}
			if (!options.IgnoreChildren)
			{
				writer.WritePropertyName("annotations");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForAnnotation(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			if (!options.IgnoreChildren && CompatibilityRestrictions.ExtendedProperty.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("extendedProperties");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForExtendedProperty(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			if (!options.IgnoreChildren && CompatibilityRestrictions.ChangedProperty.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("changedProperties");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForChangedProperty(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			if (!options.IgnoreChildren && !options.IgnoreChildrenExceptAnnotations && (!options.IgnoreInferredObjects || !ObjectTreeHelper.IsInferredObject(ObjectType.Variation)) && CompatibilityRestrictions.Variation.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("variations");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForVariation(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001BAA RID: 7082 RVA: 0x000C0590 File Offset: 0x000BE790
		internal static void WriteSchemaForRowNumberColumn(JsonWriter writer, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("description");
			writer.WriteValue("RowNumberColumn object of Tabular Object Model (TOM)");
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("name");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WritePropertyName("dataType");
			writer.WriteStartObject();
			writer.WritePropertyName("enum");
			writer.WriteStartArray();
			writer.WriteValue("automatic");
			writer.WriteValue("string");
			writer.WriteValue("int64");
			writer.WriteValue("double");
			writer.WriteValue("dateTime");
			writer.WriteValue("decimal");
			writer.WriteValue("boolean");
			writer.WriteValue("binary");
			writer.WriteValue("unknown");
			writer.WriteValue("variant");
			writer.WriteEndArray();
			writer.WriteEndObject();
			writer.WritePropertyName("dataCategory");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WritePropertyName("description");
			if (options.SplitMultilineStrings)
			{
				JsonSchemaWriter.WriteSchemaForMultilineString(writer);
			}
			else
			{
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			writer.WritePropertyName("isHidden");
			JsonSchemaWriter.WriteSchemaForBoolean(writer);
			if (!options.IgnoreInferredProperties)
			{
				writer.WritePropertyName("state");
				writer.WriteStartObject();
				writer.WritePropertyName("enum");
				writer.WriteStartArray();
				writer.WriteValue("ready");
				writer.WriteValue("noData");
				writer.WriteValue("calculationNeeded");
				writer.WriteValue("semanticError");
				writer.WriteValue("evaluationError");
				writer.WriteValue("dependencyError");
				writer.WriteValue("incomplete");
				if (CompatibilityRestrictions.ObjectState_ForceCalculationNeeded.IsCompatible(mode, dbCompatibilityLevel))
				{
					writer.WriteValue("forceCalculationNeeded");
				}
				writer.WriteEndArray();
				writer.WriteEndObject();
			}
			writer.WritePropertyName("isUnique");
			JsonSchemaWriter.WriteSchemaForBoolean(writer);
			writer.WritePropertyName("isKey");
			JsonSchemaWriter.WriteSchemaForBoolean(writer);
			writer.WritePropertyName("isNullable");
			JsonSchemaWriter.WriteSchemaForBoolean(writer);
			writer.WritePropertyName("alignment");
			writer.WriteStartObject();
			writer.WritePropertyName("enum");
			writer.WriteStartArray();
			writer.WriteValue("default");
			writer.WriteValue("left");
			writer.WriteValue("right");
			writer.WriteValue("center");
			writer.WriteEndArray();
			writer.WriteEndObject();
			writer.WritePropertyName("tableDetailPosition");
			JsonSchemaWriter.WriteSchemaForInteger(writer);
			writer.WritePropertyName("isDefaultLabel");
			JsonSchemaWriter.WriteSchemaForBoolean(writer);
			writer.WritePropertyName("isDefaultImage");
			JsonSchemaWriter.WriteSchemaForBoolean(writer);
			writer.WritePropertyName("summarizeBy");
			writer.WriteStartObject();
			writer.WritePropertyName("enum");
			writer.WriteStartArray();
			writer.WriteValue("default");
			writer.WriteValue("none");
			writer.WriteValue("sum");
			writer.WriteValue("min");
			writer.WriteValue("max");
			writer.WriteValue("count");
			writer.WriteValue("average");
			writer.WriteValue("distinctCount");
			writer.WriteEndArray();
			writer.WriteEndObject();
			writer.WritePropertyName("type");
			writer.WriteStartObject();
			writer.WritePropertyName("enum");
			writer.WriteStartArray();
			writer.WriteValue("data");
			writer.WriteValue("calculated");
			writer.WriteValue("rowNumber");
			writer.WriteValue("calculatedTableColumn");
			writer.WriteEndArray();
			writer.WriteEndObject();
			writer.WritePropertyName("formatString");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WritePropertyName("isAvailableInMdx");
			JsonSchemaWriter.WriteSchemaForBoolean(writer);
			if (!options.IgnoreInferredProperties && !options.IgnoreTimestamps)
			{
				writer.WritePropertyName("modifiedTime");
				JsonSchemaWriter.WriteSchemaForDateTime(writer);
			}
			if (!options.IgnoreInferredProperties && !options.IgnoreTimestamps)
			{
				writer.WritePropertyName("structureModifiedTime");
				JsonSchemaWriter.WriteSchemaForDateTime(writer);
			}
			if (!options.IgnoreInferredProperties && !options.IgnoreTimestamps)
			{
				writer.WritePropertyName("refreshedTime");
				JsonSchemaWriter.WriteSchemaForDateTime(writer);
			}
			writer.WritePropertyName("keepUniqueRows");
			JsonSchemaWriter.WriteSchemaForBoolean(writer);
			writer.WritePropertyName("displayOrdinal");
			JsonSchemaWriter.WriteSchemaForInteger(writer);
			if (!options.IgnoreInferredProperties)
			{
				writer.WritePropertyName("errorMessage");
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			writer.WritePropertyName("sourceProviderType");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WritePropertyName("displayFolder");
			JsonSchemaWriter.WriteSchemaForString(writer);
			if (CompatibilityRestrictions.Column_EncodingHint.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("encodingHint");
				writer.WriteStartObject();
				writer.WritePropertyName("enum");
				writer.WriteStartArray();
				writer.WriteValue("default");
				writer.WriteValue("hash");
				writer.WriteValue("value");
				writer.WriteEndArray();
				writer.WriteEndObject();
			}
			if (CompatibilityRestrictions.Column_LineageTag.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("lineageTag");
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			if (CompatibilityRestrictions.Column_SourceLineageTag.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("sourceLineageTag");
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			writer.WritePropertyName("sortByColumn");
			JsonSchemaWriter.WriteSchemaForString(writer);
			if (!options.IgnoreChildren && !options.IgnoreChildrenExceptAnnotations)
			{
				if (!options.IgnoreInferredObjects || !ObjectTreeHelper.IsInferredObject(ObjectType.AttributeHierarchy))
				{
					writer.WritePropertyName("attributeHierarchy");
					JsonSchemaWriter.WriteSchemaForAttributeHierarchy(writer, options, mode, dbCompatibilityLevel);
				}
				if ((!options.IgnoreInferredObjects || !ObjectTreeHelper.IsInferredObject(ObjectType.RelatedColumnDetails)) && CompatibilityRestrictions.Column_RelatedColumnDetails.IsCompatible(mode, dbCompatibilityLevel))
				{
					writer.WritePropertyName("relatedColumnDetails");
					JsonSchemaWriter.WriteSchemaForRelatedColumnDetails(writer, options, mode, dbCompatibilityLevel);
				}
				if ((!options.IgnoreInferredObjects || !ObjectTreeHelper.IsInferredObject(ObjectType.AlternateOf)) && CompatibilityRestrictions.Column_AlternateOf.IsCompatible(mode, dbCompatibilityLevel))
				{
					writer.WritePropertyName("alternateOf");
					JsonSchemaWriter.WriteSchemaForAlternateOf(writer, options, mode, dbCompatibilityLevel);
				}
			}
			if (!options.IgnoreChildren)
			{
				writer.WritePropertyName("annotations");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForAnnotation(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			if (!options.IgnoreChildren && CompatibilityRestrictions.ExtendedProperty.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("extendedProperties");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForExtendedProperty(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			if (!options.IgnoreChildren && CompatibilityRestrictions.ChangedProperty.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("changedProperties");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForChangedProperty(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			if (!options.IgnoreChildren && !options.IgnoreChildrenExceptAnnotations && (!options.IgnoreInferredObjects || !ObjectTreeHelper.IsInferredObject(ObjectType.Variation)) && CompatibilityRestrictions.Variation.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("variations");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForVariation(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001BAB RID: 7083 RVA: 0x000C0CC4 File Offset: 0x000BEEC4
		internal static void WriteSchemaForCalculatedTableColumn(JsonWriter writer, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("description");
			writer.WriteValue("CalculatedTableColumn object of Tabular Object Model (TOM)");
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("name");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WritePropertyName("dataType");
			writer.WriteStartObject();
			writer.WritePropertyName("enum");
			writer.WriteStartArray();
			writer.WriteValue("automatic");
			writer.WriteValue("string");
			writer.WriteValue("int64");
			writer.WriteValue("double");
			writer.WriteValue("dateTime");
			writer.WriteValue("decimal");
			writer.WriteValue("boolean");
			writer.WriteValue("binary");
			writer.WriteValue("unknown");
			writer.WriteValue("variant");
			writer.WriteEndArray();
			writer.WriteEndObject();
			writer.WritePropertyName("dataCategory");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WritePropertyName("description");
			if (options.SplitMultilineStrings)
			{
				JsonSchemaWriter.WriteSchemaForMultilineString(writer);
			}
			else
			{
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			writer.WritePropertyName("isHidden");
			JsonSchemaWriter.WriteSchemaForBoolean(writer);
			if (!options.IgnoreInferredProperties)
			{
				writer.WritePropertyName("state");
				writer.WriteStartObject();
				writer.WritePropertyName("enum");
				writer.WriteStartArray();
				writer.WriteValue("ready");
				writer.WriteValue("noData");
				writer.WriteValue("calculationNeeded");
				writer.WriteValue("semanticError");
				writer.WriteValue("evaluationError");
				writer.WriteValue("dependencyError");
				writer.WriteValue("incomplete");
				if (CompatibilityRestrictions.ObjectState_ForceCalculationNeeded.IsCompatible(mode, dbCompatibilityLevel))
				{
					writer.WriteValue("forceCalculationNeeded");
				}
				writer.WriteEndArray();
				writer.WriteEndObject();
			}
			writer.WritePropertyName("isUnique");
			JsonSchemaWriter.WriteSchemaForBoolean(writer);
			writer.WritePropertyName("isKey");
			JsonSchemaWriter.WriteSchemaForBoolean(writer);
			writer.WritePropertyName("isNullable");
			JsonSchemaWriter.WriteSchemaForBoolean(writer);
			writer.WritePropertyName("alignment");
			writer.WriteStartObject();
			writer.WritePropertyName("enum");
			writer.WriteStartArray();
			writer.WriteValue("default");
			writer.WriteValue("left");
			writer.WriteValue("right");
			writer.WriteValue("center");
			writer.WriteEndArray();
			writer.WriteEndObject();
			writer.WritePropertyName("tableDetailPosition");
			JsonSchemaWriter.WriteSchemaForInteger(writer);
			writer.WritePropertyName("isDefaultLabel");
			JsonSchemaWriter.WriteSchemaForBoolean(writer);
			writer.WritePropertyName("isDefaultImage");
			JsonSchemaWriter.WriteSchemaForBoolean(writer);
			writer.WritePropertyName("summarizeBy");
			writer.WriteStartObject();
			writer.WritePropertyName("enum");
			writer.WriteStartArray();
			writer.WriteValue("default");
			writer.WriteValue("none");
			writer.WriteValue("sum");
			writer.WriteValue("min");
			writer.WriteValue("max");
			writer.WriteValue("count");
			writer.WriteValue("average");
			writer.WriteValue("distinctCount");
			writer.WriteEndArray();
			writer.WriteEndObject();
			writer.WritePropertyName("type");
			writer.WriteStartObject();
			writer.WritePropertyName("enum");
			writer.WriteStartArray();
			writer.WriteValue("data");
			writer.WriteValue("calculated");
			writer.WriteValue("rowNumber");
			writer.WriteValue("calculatedTableColumn");
			writer.WriteEndArray();
			writer.WriteEndObject();
			writer.WritePropertyName("formatString");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WritePropertyName("isAvailableInMdx");
			JsonSchemaWriter.WriteSchemaForBoolean(writer);
			if (!options.IgnoreInferredProperties && !options.IgnoreTimestamps)
			{
				writer.WritePropertyName("modifiedTime");
				JsonSchemaWriter.WriteSchemaForDateTime(writer);
			}
			if (!options.IgnoreInferredProperties && !options.IgnoreTimestamps)
			{
				writer.WritePropertyName("structureModifiedTime");
				JsonSchemaWriter.WriteSchemaForDateTime(writer);
			}
			if (!options.IgnoreInferredProperties && !options.IgnoreTimestamps)
			{
				writer.WritePropertyName("refreshedTime");
				JsonSchemaWriter.WriteSchemaForDateTime(writer);
			}
			writer.WritePropertyName("keepUniqueRows");
			JsonSchemaWriter.WriteSchemaForBoolean(writer);
			writer.WritePropertyName("displayOrdinal");
			JsonSchemaWriter.WriteSchemaForInteger(writer);
			if (!options.IgnoreInferredProperties)
			{
				writer.WritePropertyName("errorMessage");
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			writer.WritePropertyName("sourceProviderType");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WritePropertyName("displayFolder");
			JsonSchemaWriter.WriteSchemaForString(writer);
			if (CompatibilityRestrictions.Column_EncodingHint.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("encodingHint");
				writer.WriteStartObject();
				writer.WritePropertyName("enum");
				writer.WriteStartArray();
				writer.WriteValue("default");
				writer.WriteValue("hash");
				writer.WriteValue("value");
				writer.WriteEndArray();
				writer.WriteEndObject();
			}
			if (CompatibilityRestrictions.Column_LineageTag.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("lineageTag");
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			if (CompatibilityRestrictions.Column_SourceLineageTag.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("sourceLineageTag");
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			writer.WritePropertyName("isNameInferred");
			JsonSchemaWriter.WriteSchemaForBoolean(writer);
			writer.WritePropertyName("isDataTypeInferred");
			JsonSchemaWriter.WriteSchemaForBoolean(writer);
			writer.WritePropertyName("sourceColumn");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WritePropertyName("sortByColumn");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WritePropertyName("columnOriginTable");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WritePropertyName("columnOriginColumn");
			JsonSchemaWriter.WriteSchemaForString(writer);
			if (!options.IgnoreChildren && !options.IgnoreChildrenExceptAnnotations)
			{
				if (!options.IgnoreInferredObjects || !ObjectTreeHelper.IsInferredObject(ObjectType.AttributeHierarchy))
				{
					writer.WritePropertyName("attributeHierarchy");
					JsonSchemaWriter.WriteSchemaForAttributeHierarchy(writer, options, mode, dbCompatibilityLevel);
				}
				if ((!options.IgnoreInferredObjects || !ObjectTreeHelper.IsInferredObject(ObjectType.RelatedColumnDetails)) && CompatibilityRestrictions.Column_RelatedColumnDetails.IsCompatible(mode, dbCompatibilityLevel))
				{
					writer.WritePropertyName("relatedColumnDetails");
					JsonSchemaWriter.WriteSchemaForRelatedColumnDetails(writer, options, mode, dbCompatibilityLevel);
				}
				if ((!options.IgnoreInferredObjects || !ObjectTreeHelper.IsInferredObject(ObjectType.AlternateOf)) && CompatibilityRestrictions.Column_AlternateOf.IsCompatible(mode, dbCompatibilityLevel))
				{
					writer.WritePropertyName("alternateOf");
					JsonSchemaWriter.WriteSchemaForAlternateOf(writer, options, mode, dbCompatibilityLevel);
				}
			}
			if (!options.IgnoreChildren)
			{
				writer.WritePropertyName("annotations");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForAnnotation(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			if (!options.IgnoreChildren && CompatibilityRestrictions.ExtendedProperty.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("extendedProperties");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForExtendedProperty(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			if (!options.IgnoreChildren && CompatibilityRestrictions.ChangedProperty.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("changedProperties");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForChangedProperty(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			if (!options.IgnoreChildren && !options.IgnoreChildrenExceptAnnotations && (!options.IgnoreInferredObjects || !ObjectTreeHelper.IsInferredObject(ObjectType.Variation)) && CompatibilityRestrictions.Variation.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("variations");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForVariation(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001BAC RID: 7084 RVA: 0x000C144C File Offset: 0x000BF64C
		internal static void WriteSchemaForCalculatedColumn(JsonWriter writer, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("description");
			writer.WriteValue("CalculatedColumn object of Tabular Object Model (TOM)");
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("name");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WritePropertyName("dataType");
			writer.WriteStartObject();
			writer.WritePropertyName("enum");
			writer.WriteStartArray();
			writer.WriteValue("automatic");
			writer.WriteValue("string");
			writer.WriteValue("int64");
			writer.WriteValue("double");
			writer.WriteValue("dateTime");
			writer.WriteValue("decimal");
			writer.WriteValue("boolean");
			writer.WriteValue("binary");
			writer.WriteValue("unknown");
			writer.WriteValue("variant");
			writer.WriteEndArray();
			writer.WriteEndObject();
			writer.WritePropertyName("dataCategory");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WritePropertyName("description");
			if (options.SplitMultilineStrings)
			{
				JsonSchemaWriter.WriteSchemaForMultilineString(writer);
			}
			else
			{
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			writer.WritePropertyName("isHidden");
			JsonSchemaWriter.WriteSchemaForBoolean(writer);
			if (!options.IgnoreInferredProperties)
			{
				writer.WritePropertyName("state");
				writer.WriteStartObject();
				writer.WritePropertyName("enum");
				writer.WriteStartArray();
				writer.WriteValue("ready");
				writer.WriteValue("noData");
				writer.WriteValue("calculationNeeded");
				writer.WriteValue("semanticError");
				writer.WriteValue("evaluationError");
				writer.WriteValue("dependencyError");
				writer.WriteValue("incomplete");
				if (CompatibilityRestrictions.ObjectState_ForceCalculationNeeded.IsCompatible(mode, dbCompatibilityLevel))
				{
					writer.WriteValue("forceCalculationNeeded");
				}
				writer.WriteEndArray();
				writer.WriteEndObject();
			}
			writer.WritePropertyName("isUnique");
			JsonSchemaWriter.WriteSchemaForBoolean(writer);
			writer.WritePropertyName("isKey");
			JsonSchemaWriter.WriteSchemaForBoolean(writer);
			writer.WritePropertyName("isNullable");
			JsonSchemaWriter.WriteSchemaForBoolean(writer);
			writer.WritePropertyName("alignment");
			writer.WriteStartObject();
			writer.WritePropertyName("enum");
			writer.WriteStartArray();
			writer.WriteValue("default");
			writer.WriteValue("left");
			writer.WriteValue("right");
			writer.WriteValue("center");
			writer.WriteEndArray();
			writer.WriteEndObject();
			writer.WritePropertyName("tableDetailPosition");
			JsonSchemaWriter.WriteSchemaForInteger(writer);
			writer.WritePropertyName("isDefaultLabel");
			JsonSchemaWriter.WriteSchemaForBoolean(writer);
			writer.WritePropertyName("isDefaultImage");
			JsonSchemaWriter.WriteSchemaForBoolean(writer);
			writer.WritePropertyName("summarizeBy");
			writer.WriteStartObject();
			writer.WritePropertyName("enum");
			writer.WriteStartArray();
			writer.WriteValue("default");
			writer.WriteValue("none");
			writer.WriteValue("sum");
			writer.WriteValue("min");
			writer.WriteValue("max");
			writer.WriteValue("count");
			writer.WriteValue("average");
			writer.WriteValue("distinctCount");
			writer.WriteEndArray();
			writer.WriteEndObject();
			writer.WritePropertyName("type");
			writer.WriteStartObject();
			writer.WritePropertyName("enum");
			writer.WriteStartArray();
			writer.WriteValue("data");
			writer.WriteValue("calculated");
			writer.WriteValue("rowNumber");
			writer.WriteValue("calculatedTableColumn");
			writer.WriteEndArray();
			writer.WriteEndObject();
			writer.WritePropertyName("formatString");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WritePropertyName("isAvailableInMdx");
			JsonSchemaWriter.WriteSchemaForBoolean(writer);
			if (!options.IgnoreInferredProperties && !options.IgnoreTimestamps)
			{
				writer.WritePropertyName("modifiedTime");
				JsonSchemaWriter.WriteSchemaForDateTime(writer);
			}
			if (!options.IgnoreInferredProperties && !options.IgnoreTimestamps)
			{
				writer.WritePropertyName("structureModifiedTime");
				JsonSchemaWriter.WriteSchemaForDateTime(writer);
			}
			if (!options.IgnoreInferredProperties && !options.IgnoreTimestamps)
			{
				writer.WritePropertyName("refreshedTime");
				JsonSchemaWriter.WriteSchemaForDateTime(writer);
			}
			writer.WritePropertyName("keepUniqueRows");
			JsonSchemaWriter.WriteSchemaForBoolean(writer);
			writer.WritePropertyName("displayOrdinal");
			JsonSchemaWriter.WriteSchemaForInteger(writer);
			if (!options.IgnoreInferredProperties)
			{
				writer.WritePropertyName("errorMessage");
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			writer.WritePropertyName("sourceProviderType");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WritePropertyName("displayFolder");
			JsonSchemaWriter.WriteSchemaForString(writer);
			if (CompatibilityRestrictions.Column_EncodingHint.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("encodingHint");
				writer.WriteStartObject();
				writer.WritePropertyName("enum");
				writer.WriteStartArray();
				writer.WriteValue("default");
				writer.WriteValue("hash");
				writer.WriteValue("value");
				writer.WriteEndArray();
				writer.WriteEndObject();
			}
			if (CompatibilityRestrictions.Column_LineageTag.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("lineageTag");
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			if (CompatibilityRestrictions.Column_SourceLineageTag.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("sourceLineageTag");
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			writer.WritePropertyName("isDataTypeInferred");
			JsonSchemaWriter.WriteSchemaForBoolean(writer);
			writer.WritePropertyName("expression");
			if (options.SplitMultilineStrings)
			{
				JsonSchemaWriter.WriteSchemaForMultilineString(writer);
			}
			else
			{
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			if (CompatibilityRestrictions.CalculatedColumn_EvaluationBehavior.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("evaluationBehavior");
				writer.WriteStartObject();
				writer.WritePropertyName("enum");
				writer.WriteStartArray();
				writer.WriteValue("automatic");
				writer.WriteValue("static");
				writer.WriteValue("dynamic");
				writer.WriteEndArray();
				writer.WriteEndObject();
			}
			writer.WritePropertyName("sortByColumn");
			JsonSchemaWriter.WriteSchemaForString(writer);
			if (!options.IgnoreChildren && !options.IgnoreChildrenExceptAnnotations)
			{
				if (!options.IgnoreInferredObjects || !ObjectTreeHelper.IsInferredObject(ObjectType.AttributeHierarchy))
				{
					writer.WritePropertyName("attributeHierarchy");
					JsonSchemaWriter.WriteSchemaForAttributeHierarchy(writer, options, mode, dbCompatibilityLevel);
				}
				if ((!options.IgnoreInferredObjects || !ObjectTreeHelper.IsInferredObject(ObjectType.RelatedColumnDetails)) && CompatibilityRestrictions.Column_RelatedColumnDetails.IsCompatible(mode, dbCompatibilityLevel))
				{
					writer.WritePropertyName("relatedColumnDetails");
					JsonSchemaWriter.WriteSchemaForRelatedColumnDetails(writer, options, mode, dbCompatibilityLevel);
				}
				if ((!options.IgnoreInferredObjects || !ObjectTreeHelper.IsInferredObject(ObjectType.AlternateOf)) && CompatibilityRestrictions.Column_AlternateOf.IsCompatible(mode, dbCompatibilityLevel))
				{
					writer.WritePropertyName("alternateOf");
					JsonSchemaWriter.WriteSchemaForAlternateOf(writer, options, mode, dbCompatibilityLevel);
				}
			}
			if (!options.IgnoreChildren)
			{
				writer.WritePropertyName("annotations");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForAnnotation(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			if (!options.IgnoreChildren && CompatibilityRestrictions.ExtendedProperty.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("extendedProperties");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForExtendedProperty(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			if (!options.IgnoreChildren && CompatibilityRestrictions.ChangedProperty.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("changedProperties");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForChangedProperty(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			if (!options.IgnoreChildren && !options.IgnoreChildrenExceptAnnotations && (!options.IgnoreInferredObjects || !ObjectTreeHelper.IsInferredObject(ObjectType.Variation)) && CompatibilityRestrictions.Variation.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("variations");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForVariation(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001BAD RID: 7085 RVA: 0x000C1C10 File Offset: 0x000BFE10
		internal static void WriteSchemaForSingleColumnRelationship(JsonWriter writer, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("description");
			writer.WriteValue("SingleColumnRelationship object of Tabular Object Model (TOM)");
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("name");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WritePropertyName("isActive");
			JsonSchemaWriter.WriteSchemaForBoolean(writer);
			writer.WritePropertyName("type");
			writer.WriteStartObject();
			writer.WritePropertyName("enum");
			writer.WriteStartArray();
			writer.WriteValue("singleColumn");
			writer.WriteEndArray();
			writer.WriteEndObject();
			writer.WritePropertyName("crossFilteringBehavior");
			writer.WriteStartObject();
			writer.WritePropertyName("enum");
			writer.WriteStartArray();
			writer.WriteValue("oneDirection");
			writer.WriteValue("bothDirections");
			writer.WriteValue("automatic");
			writer.WriteEndArray();
			writer.WriteEndObject();
			writer.WritePropertyName("joinOnDateBehavior");
			writer.WriteStartObject();
			writer.WritePropertyName("enum");
			writer.WriteStartArray();
			writer.WriteValue("dateAndTime");
			writer.WriteValue("datePartOnly");
			writer.WriteEndArray();
			writer.WriteEndObject();
			writer.WritePropertyName("relyOnReferentialIntegrity");
			JsonSchemaWriter.WriteSchemaForBoolean(writer);
			if (!options.IgnoreInferredProperties)
			{
				writer.WritePropertyName("state");
				writer.WriteStartObject();
				writer.WritePropertyName("enum");
				writer.WriteStartArray();
				writer.WriteValue("ready");
				writer.WriteValue("noData");
				writer.WriteValue("calculationNeeded");
				writer.WriteValue("semanticError");
				writer.WriteValue("evaluationError");
				writer.WriteValue("dependencyError");
				writer.WriteValue("incomplete");
				if (CompatibilityRestrictions.ObjectState_ForceCalculationNeeded.IsCompatible(mode, dbCompatibilityLevel))
				{
					writer.WriteValue("forceCalculationNeeded");
				}
				writer.WriteEndArray();
				writer.WriteEndObject();
			}
			if (!options.IgnoreInferredProperties && !options.IgnoreTimestamps)
			{
				writer.WritePropertyName("modifiedTime");
				JsonSchemaWriter.WriteSchemaForDateTime(writer);
			}
			if (!options.IgnoreInferredProperties && !options.IgnoreTimestamps)
			{
				writer.WritePropertyName("refreshedTime");
				JsonSchemaWriter.WriteSchemaForDateTime(writer);
			}
			writer.WritePropertyName("securityFilteringBehavior");
			writer.WriteStartObject();
			writer.WritePropertyName("enum");
			writer.WriteStartArray();
			writer.WriteValue("oneDirection");
			writer.WriteValue("bothDirections");
			if (CompatibilityRestrictions.SecurityFilteringBehavior_None.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WriteValue("none");
			}
			writer.WriteEndArray();
			writer.WriteEndObject();
			writer.WritePropertyName("fromCardinality");
			writer.WriteStartObject();
			writer.WritePropertyName("enum");
			writer.WriteStartArray();
			writer.WriteValue("none");
			writer.WriteValue("one");
			writer.WriteValue("many");
			writer.WriteEndArray();
			writer.WriteEndObject();
			writer.WritePropertyName("toCardinality");
			writer.WriteStartObject();
			writer.WritePropertyName("enum");
			writer.WriteStartArray();
			writer.WriteValue("none");
			writer.WriteValue("one");
			writer.WriteValue("many");
			writer.WriteEndArray();
			writer.WriteEndObject();
			JsonSchemaWriter.WriteAdditionalSchemaPropertiesForSingleColumnRelationship(writer, options, mode, dbCompatibilityLevel);
			if (!options.IgnoreChildren)
			{
				writer.WritePropertyName("annotations");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForAnnotation(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			if (!options.IgnoreChildren && CompatibilityRestrictions.ExtendedProperty.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("extendedProperties");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForExtendedProperty(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			if (!options.IgnoreChildren && CompatibilityRestrictions.ChangedProperty.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("changedProperties");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForChangedProperty(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001BAE RID: 7086 RVA: 0x000C205C File Offset: 0x000C025C
		private static void WriteAdditionalSchemaPropertiesForSingleColumnRelationship(JsonWriter writer, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WritePropertyName("fromColumn");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WritePropertyName("fromTable");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WritePropertyName("toColumn");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WritePropertyName("toTable");
			JsonSchemaWriter.WriteSchemaForString(writer);
		}

		// Token: 0x06001BAF RID: 7087 RVA: 0x000C20B0 File Offset: 0x000C02B0
		internal static void WriteSchemaForWindowsModelRoleMember(JsonWriter writer, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("description");
			writer.WriteValue("WindowsModelRoleMember object of Tabular Object Model (TOM)");
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("memberName");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WritePropertyName("memberId");
			JsonSchemaWriter.WriteSchemaForString(writer);
			if (!options.IgnoreInferredProperties && !options.IgnoreTimestamps)
			{
				writer.WritePropertyName("modifiedTime");
				JsonSchemaWriter.WriteSchemaForDateTime(writer);
			}
			if (!options.IgnoreChildren)
			{
				writer.WritePropertyName("annotations");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForAnnotation(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			if (!options.IgnoreChildren && CompatibilityRestrictions.ExtendedProperty.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("extendedProperties");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForExtendedProperty(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001BB0 RID: 7088 RVA: 0x000C2204 File Offset: 0x000C0404
		internal static void WriteSchemaForExternalModelRoleMember(JsonWriter writer, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("description");
			writer.WriteValue("ExternalModelRoleMember object of Tabular Object Model (TOM)");
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("memberName");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WritePropertyName("memberId");
			JsonSchemaWriter.WriteSchemaForString(writer);
			if (!options.IgnoreInferredProperties && !options.IgnoreTimestamps)
			{
				writer.WritePropertyName("modifiedTime");
				JsonSchemaWriter.WriteSchemaForDateTime(writer);
			}
			writer.WritePropertyName("identityProvider");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WritePropertyName("memberType");
			writer.WriteStartObject();
			writer.WritePropertyName("enum");
			writer.WriteStartArray();
			writer.WriteValue("auto");
			writer.WriteValue("user");
			writer.WriteValue("group");
			writer.WriteEndArray();
			writer.WriteEndObject();
			if (!options.IgnoreChildren)
			{
				writer.WritePropertyName("annotations");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForAnnotation(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			if (!options.IgnoreChildren && CompatibilityRestrictions.ExtendedProperty.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("extendedProperties");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForExtendedProperty(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001BB1 RID: 7089 RVA: 0x000C23B8 File Offset: 0x000C05B8
		internal static void WriteSchemaForStringExtendedProperty(JsonWriter writer, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("description");
			writer.WriteValue("StringExtendedProperty object of Tabular Object Model (TOM)");
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("name");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WritePropertyName("type");
			writer.WriteStartObject();
			writer.WritePropertyName("enum");
			writer.WriteStartArray();
			writer.WriteValue("string");
			writer.WriteValue("json");
			writer.WriteEndArray();
			writer.WriteEndObject();
			if (!options.IgnoreInferredProperties && !options.IgnoreTimestamps)
			{
				writer.WritePropertyName("modifiedTime");
				JsonSchemaWriter.WriteSchemaForDateTime(writer);
			}
			writer.WritePropertyName("value");
			if (options.SplitMultilineStrings)
			{
				JsonSchemaWriter.WriteSchemaForMultilineString(writer);
			}
			else
			{
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001BB2 RID: 7090 RVA: 0x000C24C0 File Offset: 0x000C06C0
		internal static void WriteSchemaForJsonExtendedProperty(JsonWriter writer, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("description");
			writer.WriteValue("JsonExtendedProperty object of Tabular Object Model (TOM)");
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("name");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WritePropertyName("type");
			writer.WriteStartObject();
			writer.WritePropertyName("enum");
			writer.WriteStartArray();
			writer.WriteValue("string");
			writer.WriteValue("json");
			writer.WriteEndArray();
			writer.WriteEndObject();
			if (!options.IgnoreInferredProperties && !options.IgnoreTimestamps)
			{
				writer.WritePropertyName("modifiedTime");
				JsonSchemaWriter.WriteSchemaForDateTime(writer);
			}
			writer.WritePropertyName("value");
			JsonSchemaWriter.WriteSchemaForGenericObject(writer);
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001BB3 RID: 7091 RVA: 0x000C25B8 File Offset: 0x000C07B8
		internal static void WriteSchemaForBasicRefreshPolicy(JsonWriter writer, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("description");
			writer.WriteValue("BasicRefreshPolicy object of Tabular Object Model (TOM)");
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("policyType");
			writer.WriteStartObject();
			writer.WritePropertyName("enum");
			writer.WriteStartArray();
			writer.WriteValue("basic");
			writer.WriteEndArray();
			writer.WriteEndObject();
			if (CompatibilityRestrictions.RefreshPolicy_Mode.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("mode");
				writer.WriteStartObject();
				writer.WritePropertyName("enum");
				writer.WriteStartArray();
				writer.WriteValue("import");
				writer.WriteValue("hybrid");
				writer.WriteEndArray();
				writer.WriteEndObject();
			}
			writer.WritePropertyName("rollingWindowGranularity");
			writer.WriteStartObject();
			writer.WritePropertyName("enum");
			writer.WriteStartArray();
			writer.WriteValue("invalid");
			writer.WriteValue("day");
			writer.WriteValue("month");
			writer.WriteValue("quarter");
			writer.WriteValue("year");
			writer.WriteEndArray();
			writer.WriteEndObject();
			writer.WritePropertyName("rollingWindowPeriods");
			JsonSchemaWriter.WriteSchemaForInteger(writer);
			writer.WritePropertyName("incrementalGranularity");
			writer.WriteStartObject();
			writer.WritePropertyName("enum");
			writer.WriteStartArray();
			writer.WriteValue("invalid");
			writer.WriteValue("day");
			writer.WriteValue("month");
			writer.WriteValue("quarter");
			writer.WriteValue("year");
			writer.WriteEndArray();
			writer.WriteEndObject();
			writer.WritePropertyName("incrementalPeriods");
			JsonSchemaWriter.WriteSchemaForInteger(writer);
			writer.WritePropertyName("incrementalPeriodsOffset");
			JsonSchemaWriter.WriteSchemaForInteger(writer);
			writer.WritePropertyName("pollingExpression");
			if (options.SplitMultilineStrings)
			{
				JsonSchemaWriter.WriteSchemaForMultilineString(writer);
			}
			else
			{
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			writer.WritePropertyName("sourceExpression");
			if (options.SplitMultilineStrings)
			{
				JsonSchemaWriter.WriteSchemaForMultilineString(writer);
			}
			else
			{
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			if (!options.IgnoreChildren)
			{
				writer.WritePropertyName("annotations");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForAnnotation(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			if (!options.IgnoreChildren && CompatibilityRestrictions.ExtendedProperty.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("extendedProperties");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForExtendedProperty(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001BB4 RID: 7092 RVA: 0x000C2890 File Offset: 0x000C0A90
		internal static void WriteSchemaForDataBindingHint(JsonWriter writer, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("description");
			writer.WriteValue("DataBindingHint object of Tabular Object Model (TOM)");
			writer.WritePropertyName("type");
			writer.WriteValue("object");
			writer.WritePropertyName("properties");
			writer.WriteStartObject();
			writer.WritePropertyName("name");
			JsonSchemaWriter.WriteSchemaForString(writer);
			writer.WritePropertyName("description");
			if (options.SplitMultilineStrings)
			{
				JsonSchemaWriter.WriteSchemaForMultilineString(writer);
			}
			else
			{
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			writer.WritePropertyName("type");
			writer.WriteStartObject();
			writer.WritePropertyName("enum");
			writer.WriteStartArray();
			writer.WriteValue("unknown");
			writer.WriteValue("dataBindingHint");
			writer.WriteEndArray();
			writer.WriteEndObject();
			writer.WritePropertyName("connectionId");
			JsonSchemaWriter.WriteSchemaForString(writer);
			if (!options.IgnoreChildren)
			{
				writer.WritePropertyName("annotations");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForAnnotation(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			if (!options.IgnoreChildren && CompatibilityRestrictions.ExtendedProperty.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WritePropertyName("extendedProperties");
				writer.WriteStartObject();
				writer.WritePropertyName("type");
				writer.WriteValue("array");
				writer.WritePropertyName("items");
				JsonSchemaWriter.WriteSchemaForExtendedProperty(writer, options, mode, dbCompatibilityLevel);
				writer.WriteEndObject();
			}
			writer.WriteEndObject();
			writer.WritePropertyName("additionalProperties");
			writer.WriteValue(false);
			writer.WriteEndObject();
		}

		// Token: 0x06001BB5 RID: 7093 RVA: 0x000C2A28 File Offset: 0x000C0C28
		internal static void WriteSchemaForDataSource(JsonWriter writer, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("anyOf");
			writer.WriteStartArray();
			JsonSchemaWriter.WriteSchemaForProviderDataSource(writer, options, mode, dbCompatibilityLevel);
			if (CompatibilityRestrictions.StructuredDataSource.IsCompatible(mode, dbCompatibilityLevel))
			{
				JsonSchemaWriter.WriteSchemaForStructuredDataSource(writer, options, mode, dbCompatibilityLevel);
			}
			writer.WriteEndArray();
			writer.WriteEndObject();
		}

		// Token: 0x06001BB6 RID: 7094 RVA: 0x000C2A78 File Offset: 0x000C0C78
		internal static void WriteSchemaForColumn(JsonWriter writer, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("anyOf");
			writer.WriteStartArray();
			JsonSchemaWriter.WriteSchemaForDataColumn(writer, options, mode, dbCompatibilityLevel);
			if (!options.IgnoreInferredObjects)
			{
				JsonSchemaWriter.WriteSchemaForRowNumberColumn(writer, options, mode, dbCompatibilityLevel);
			}
			JsonSchemaWriter.WriteSchemaForCalculatedTableColumn(writer, options, mode, dbCompatibilityLevel);
			JsonSchemaWriter.WriteSchemaForCalculatedColumn(writer, options, mode, dbCompatibilityLevel);
			writer.WriteEndArray();
			writer.WriteEndObject();
		}

		// Token: 0x06001BB7 RID: 7095 RVA: 0x000C2AD4 File Offset: 0x000C0CD4
		internal static void WriteSchemaForRelationship(JsonWriter writer, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("anyOf");
			writer.WriteStartArray();
			JsonSchemaWriter.WriteSchemaForSingleColumnRelationship(writer, options, mode, dbCompatibilityLevel);
			writer.WriteEndArray();
			writer.WriteEndObject();
		}

		// Token: 0x06001BB8 RID: 7096 RVA: 0x000C2B02 File Offset: 0x000C0D02
		internal static void WriteSchemaForModelRoleMember(JsonWriter writer, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("anyOf");
			writer.WriteStartArray();
			JsonSchemaWriter.WriteSchemaForWindowsModelRoleMember(writer, options, mode, dbCompatibilityLevel);
			JsonSchemaWriter.WriteSchemaForExternalModelRoleMember(writer, options, mode, dbCompatibilityLevel);
			writer.WriteEndArray();
			writer.WriteEndObject();
		}

		// Token: 0x06001BB9 RID: 7097 RVA: 0x000C2B39 File Offset: 0x000C0D39
		internal static void WriteSchemaForExtendedProperty(JsonWriter writer, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("anyOf");
			writer.WriteStartArray();
			JsonSchemaWriter.WriteSchemaForStringExtendedProperty(writer, options, mode, dbCompatibilityLevel);
			JsonSchemaWriter.WriteSchemaForJsonExtendedProperty(writer, options, mode, dbCompatibilityLevel);
			writer.WriteEndArray();
			writer.WriteEndObject();
		}

		// Token: 0x06001BBA RID: 7098 RVA: 0x000C2B70 File Offset: 0x000C0D70
		internal static void WriteSchemaForRefreshPolicy(JsonWriter writer, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("anyOf");
			writer.WriteStartArray();
			JsonSchemaWriter.WriteSchemaForBasicRefreshPolicy(writer, options, mode, dbCompatibilityLevel);
			writer.WriteEndArray();
			writer.WriteEndObject();
		}

		// Token: 0x06001BBB RID: 7099 RVA: 0x000C2B9E File Offset: 0x000C0D9E
		internal static void WriteSchemaForBindingInfo(JsonWriter writer, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("anyOf");
			writer.WriteStartArray();
			JsonSchemaWriter.WriteSchemaForDataBindingHint(writer, options, mode, dbCompatibilityLevel);
			writer.WriteEndArray();
			writer.WriteEndObject();
		}

		// Token: 0x06001BBC RID: 7100 RVA: 0x000C2BCC File Offset: 0x000C0DCC
		private static void WriteSchemaPropertiesForPartitionInMergedWithTableMode(JsonWriter writer, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			Utils.Verify(options.PartitionsMergedWithTable, "WriteSchemaPropertiesForPartitionInMergedWithTableMode is called when PartitionsMergedWithTable == false");
			if (!options.IgnoreInferredProperties)
			{
				writer.WritePropertyName("state");
				writer.WriteStartObject();
				writer.WritePropertyName("enum");
				writer.WriteStartArray();
				writer.WriteValue("ready");
				writer.WriteValue("noData");
				writer.WriteValue("calculationNeeded");
				writer.WriteValue("semanticError");
				writer.WriteValue("evaluationError");
				writer.WriteValue("dependencyError");
				writer.WriteValue("incomplete");
				if (CompatibilityRestrictions.ObjectState_ForceCalculationNeeded.IsCompatible(mode, dbCompatibilityLevel))
				{
					writer.WriteValue("forceCalculationNeeded");
				}
				writer.WriteEndArray();
				writer.WriteEndObject();
			}
			writer.WritePropertyName("mode");
			writer.WriteStartObject();
			writer.WritePropertyName("enum");
			writer.WriteStartArray();
			writer.WriteValue("import");
			writer.WriteValue("directQuery");
			writer.WriteValue("default");
			if (CompatibilityRestrictions.ModeType_Push.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WriteValue("push");
			}
			if (CompatibilityRestrictions.ModeType_Dual.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WriteValue("dual");
			}
			if (CompatibilityRestrictions.ModeType_DirectLake.IsCompatible(mode, dbCompatibilityLevel))
			{
				writer.WriteValue("directLake");
			}
			writer.WriteEndArray();
			writer.WriteEndObject();
			writer.WritePropertyName("dataView");
			writer.WriteStartObject();
			writer.WritePropertyName("enum");
			writer.WriteStartArray();
			writer.WriteValue("full");
			writer.WriteValue("sample");
			writer.WriteValue("default");
			writer.WriteEndArray();
			writer.WriteEndObject();
			if (!options.IgnoreInferredProperties && !options.IgnoreTimestamps)
			{
				writer.WritePropertyName("refreshedTime");
				JsonSchemaWriter.WriteSchemaForDateTime(writer);
			}
			if (!options.IgnoreInferredProperties)
			{
				writer.WritePropertyName("errorMessage");
				JsonSchemaWriter.WriteSchemaForString(writer);
			}
			JsonSchemaWriter.WriteAdditionalSchemaPropertiesForPartition(writer, options, mode, dbCompatibilityLevel);
		}
	}
}
