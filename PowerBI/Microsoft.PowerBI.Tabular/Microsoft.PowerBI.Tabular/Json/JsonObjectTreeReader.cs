using System;
using Microsoft.AnalysisServices.Tabular.Extensions;
using Microsoft.AnalysisServices.Tabular.Json.Linq;
using Microsoft.AnalysisServices.Tabular.Metadata;

namespace Microsoft.AnalysisServices.Tabular.Json
{
	// Token: 0x020001B3 RID: 435
	internal static class JsonObjectTreeReader
	{
		// Token: 0x06001A7C RID: 6780 RVA: 0x000AF7B8 File Offset: 0x000AD9B8
		public static void ReadModel(JObject jObj, ObjectPath currentPath, JsonObjectTreeReaderSettings settings)
		{
			if (settings.ShouldReadObject(ObjectType.Model))
			{
				settings.ReadObject(jObj, ObjectType.Model, currentPath);
			}
			if (settings.ShouldReadCollection(ObjectType.Table) && jObj.ContainsProperty("tables"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["tables"], ObjectType.Table, currentPath, settings);
			}
			if (settings.ShouldReadCollection(ObjectType.Relationship) && jObj.ContainsProperty("relationships"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["relationships"], ObjectType.Relationship, currentPath, settings);
			}
			if (settings.ShouldReadCollection(ObjectType.DataSource) && jObj.ContainsProperty("dataSources"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["dataSources"], ObjectType.DataSource, currentPath, settings);
			}
			if (settings.ShouldReadCollection(ObjectType.Perspective) && jObj.ContainsProperty("perspectives"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["perspectives"], ObjectType.Perspective, currentPath, settings);
			}
			if (settings.ShouldReadCollection(ObjectType.Culture) && jObj.ContainsProperty("cultures"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["cultures"], ObjectType.Culture, currentPath, settings);
			}
			if (settings.ShouldReadCollection(ObjectType.Role) && jObj.ContainsProperty("roles"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["roles"], ObjectType.Role, currentPath, settings);
			}
			if (settings.ShouldReadCollection(ObjectType.Expression) && jObj.ContainsProperty("expressions"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["expressions"], ObjectType.Expression, currentPath, settings);
			}
			if (settings.ShouldReadCollection(ObjectType.QueryGroup) && jObj.ContainsProperty("queryGroups"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["queryGroups"], ObjectType.QueryGroup, currentPath, settings);
			}
			if (settings.ShouldReadCollection(ObjectType.AnalyticsAIMetadata) && jObj.ContainsProperty("analyticsAIMetadata"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["analyticsAIMetadata"], ObjectType.AnalyticsAIMetadata, currentPath, settings);
			}
			if (settings.ShouldReadCollection(ObjectType.Function) && jObj.ContainsProperty("functions"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["functions"], ObjectType.Function, currentPath, settings);
			}
			if (settings.ShouldReadCollection(ObjectType.BindingInfo) && jObj.ContainsProperty("bindingInfoCollection"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["bindingInfoCollection"], ObjectType.BindingInfo, currentPath, settings);
			}
			if (settings.ShouldReadCollection(ObjectType.Annotation) && jObj.ContainsProperty("annotations"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["annotations"], ObjectType.Annotation, currentPath, settings);
			}
			if (settings.ShouldReadCollection(ObjectType.ExtendedProperty) && jObj.ContainsProperty("extendedProperties"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["extendedProperties"], ObjectType.ExtendedProperty, currentPath, settings);
			}
			if (settings.ShouldReadCollection(ObjectType.ExcludedArtifact) && jObj.ContainsProperty("excludedArtifacts"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["excludedArtifacts"], ObjectType.ExcludedArtifact, currentPath, settings);
			}
		}

		// Token: 0x06001A7D RID: 6781 RVA: 0x000AFA2C File Offset: 0x000ADC2C
		public static void ReadDataSource(JObject jObj, ObjectPath currentPath, JsonObjectTreeReaderSettings settings)
		{
			if (settings.ShouldReadObject(ObjectType.DataSource))
			{
				settings.ReadObject(jObj, ObjectType.DataSource, currentPath);
			}
			if (settings.ShouldReadCollection(ObjectType.Annotation) && jObj.ContainsProperty("annotations"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["annotations"], ObjectType.Annotation, currentPath, settings);
			}
			if (settings.ShouldReadCollection(ObjectType.ExtendedProperty) && jObj.ContainsProperty("extendedProperties"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["extendedProperties"], ObjectType.ExtendedProperty, currentPath, settings);
			}
		}

		// Token: 0x06001A7E RID: 6782 RVA: 0x000AFAA4 File Offset: 0x000ADCA4
		public static void ReadTable(JObject jObj, ObjectPath currentPath, JsonObjectTreeReaderSettings settings)
		{
			if (settings.ShouldReadObject(ObjectType.Table))
			{
				settings.ReadObject(jObj, ObjectType.Table, currentPath);
			}
			if (settings.ShouldReadObject(ObjectType.DetailRowsDefinition) && jObj.ContainsProperty("defaultDetailRowsDefinition"))
			{
				JToken jtoken = jObj["defaultDetailRowsDefinition"];
				if (jtoken.Type != 1)
				{
					throw JsonSerializationUtil.CreateException(TomSR.Exception_CannotReadPropertyObjectExpected(typeof(DetailRowsDefinition).Name, "defaultDetailRowsDefinition"), jtoken, null);
				}
				currentPath.Push(ObjectType.DetailRowsDefinition, string.Empty);
				JsonObjectTreeReader.ReadDetailRowsDefinition((JObject)jtoken, currentPath, settings);
				currentPath.Pop();
			}
			if (settings.ShouldReadObject(ObjectType.RefreshPolicy) && jObj.ContainsProperty("refreshPolicy"))
			{
				JToken jtoken2 = jObj["refreshPolicy"];
				if (jtoken2.Type != 1)
				{
					throw JsonSerializationUtil.CreateException(TomSR.Exception_CannotReadPropertyObjectExpected(typeof(RefreshPolicy).Name, "refreshPolicy"), jtoken2, null);
				}
				currentPath.Push(ObjectType.RefreshPolicy, string.Empty);
				JsonObjectTreeReader.ReadRefreshPolicy((JObject)jtoken2, currentPath, settings);
				currentPath.Pop();
			}
			if (settings.ShouldReadObject(ObjectType.CalculationGroup) && jObj.ContainsProperty("calculationGroup"))
			{
				JToken jtoken3 = jObj["calculationGroup"];
				if (jtoken3.Type != 1)
				{
					throw JsonSerializationUtil.CreateException(TomSR.Exception_CannotReadPropertyObjectExpected(typeof(CalculationGroup).Name, "calculationGroup"), jtoken3, null);
				}
				currentPath.Push(ObjectType.CalculationGroup, string.Empty);
				JsonObjectTreeReader.ReadCalculationGroup((JObject)jtoken3, currentPath, settings);
				currentPath.Pop();
			}
			if (settings.ShouldReadCollection(ObjectType.Column) && jObj.ContainsProperty("columns"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["columns"], ObjectType.Column, currentPath, settings);
			}
			if (settings.ShouldReadCollection(ObjectType.Measure) && jObj.ContainsProperty("measures"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["measures"], ObjectType.Measure, currentPath, settings);
			}
			if (settings.ShouldReadCollection(ObjectType.Hierarchy) && jObj.ContainsProperty("hierarchies"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["hierarchies"], ObjectType.Hierarchy, currentPath, settings);
			}
			if (settings.ShouldReadCollection(ObjectType.Set) && jObj.ContainsProperty("sets"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["sets"], ObjectType.Set, currentPath, settings);
			}
			if (settings.ShouldReadCollection(ObjectType.Annotation) && jObj.ContainsProperty("annotations"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["annotations"], ObjectType.Annotation, currentPath, settings);
			}
			if (settings.ShouldReadCollection(ObjectType.ExtendedProperty) && jObj.ContainsProperty("extendedProperties"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["extendedProperties"], ObjectType.ExtendedProperty, currentPath, settings);
			}
			if (settings.ShouldReadCollection(ObjectType.ExcludedArtifact) && jObj.ContainsProperty("excludedArtifacts"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["excludedArtifacts"], ObjectType.ExcludedArtifact, currentPath, settings);
			}
			if (settings.ShouldReadCollection(ObjectType.ChangedProperty) && jObj.ContainsProperty("changedProperties"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["changedProperties"], ObjectType.ChangedProperty, currentPath, settings);
			}
			if (settings.ShouldReadCollection(ObjectType.Calendar) && jObj.ContainsProperty("calendars"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["calendars"], ObjectType.Calendar, currentPath, settings);
			}
		}

		// Token: 0x06001A7F RID: 6783 RVA: 0x000AFD8C File Offset: 0x000ADF8C
		public static void ReadColumn(JObject jObj, ObjectPath currentPath, JsonObjectTreeReaderSettings settings)
		{
			if (settings.ShouldReadObject(ObjectType.Column))
			{
				settings.ReadObject(jObj, ObjectType.Column, currentPath);
			}
			if (settings.ShouldReadObject(ObjectType.AttributeHierarchy) && jObj.ContainsProperty("attributeHierarchy"))
			{
				JToken jtoken = jObj["attributeHierarchy"];
				if (jtoken.Type != 1)
				{
					throw JsonSerializationUtil.CreateException(TomSR.Exception_CannotReadPropertyObjectExpected(typeof(AttributeHierarchy).Name, "attributeHierarchy"), jtoken, null);
				}
				currentPath.Push(ObjectType.AttributeHierarchy, string.Empty);
				JsonObjectTreeReader.ReadAttributeHierarchy((JObject)jtoken, currentPath, settings);
				currentPath.Pop();
			}
			if (settings.ShouldReadObject(ObjectType.RelatedColumnDetails) && jObj.ContainsProperty("relatedColumnDetails"))
			{
				JToken jtoken2 = jObj["relatedColumnDetails"];
				if (jtoken2.Type != 1)
				{
					throw JsonSerializationUtil.CreateException(TomSR.Exception_CannotReadPropertyObjectExpected(typeof(RelatedColumnDetails).Name, "relatedColumnDetails"), jtoken2, null);
				}
				currentPath.Push(ObjectType.RelatedColumnDetails, string.Empty);
				JsonObjectTreeReader.ReadRelatedColumnDetails((JObject)jtoken2, currentPath, settings);
				currentPath.Pop();
			}
			if (settings.ShouldReadObject(ObjectType.AlternateOf) && jObj.ContainsProperty("alternateOf"))
			{
				JToken jtoken3 = jObj["alternateOf"];
				if (jtoken3.Type != 1)
				{
					throw JsonSerializationUtil.CreateException(TomSR.Exception_CannotReadPropertyObjectExpected(typeof(AlternateOf).Name, "alternateOf"), jtoken3, null);
				}
				currentPath.Push(ObjectType.AlternateOf, string.Empty);
				JsonObjectTreeReader.ReadAlternateOf((JObject)jtoken3, currentPath, settings);
				currentPath.Pop();
			}
			if (settings.ShouldReadCollection(ObjectType.Variation) && jObj.ContainsProperty("variations"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["variations"], ObjectType.Variation, currentPath, settings);
			}
			if (settings.ShouldReadCollection(ObjectType.Annotation) && jObj.ContainsProperty("annotations"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["annotations"], ObjectType.Annotation, currentPath, settings);
			}
			if (settings.ShouldReadCollection(ObjectType.ExtendedProperty) && jObj.ContainsProperty("extendedProperties"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["extendedProperties"], ObjectType.ExtendedProperty, currentPath, settings);
			}
			if (settings.ShouldReadCollection(ObjectType.ChangedProperty) && jObj.ContainsProperty("changedProperties"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["changedProperties"], ObjectType.ChangedProperty, currentPath, settings);
			}
		}

		// Token: 0x06001A80 RID: 6784 RVA: 0x000AFF9C File Offset: 0x000AE19C
		public static void ReadAttributeHierarchy(JObject jObj, ObjectPath currentPath, JsonObjectTreeReaderSettings settings)
		{
			if (settings.ShouldReadObject(ObjectType.AttributeHierarchy))
			{
				settings.ReadObject(jObj, ObjectType.AttributeHierarchy, currentPath);
			}
			if (settings.ShouldReadCollection(ObjectType.Annotation) && jObj.ContainsProperty("annotations"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["annotations"], ObjectType.Annotation, currentPath, settings);
			}
			if (settings.ShouldReadCollection(ObjectType.ExtendedProperty) && jObj.ContainsProperty("extendedProperties"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["extendedProperties"], ObjectType.ExtendedProperty, currentPath, settings);
			}
		}

		// Token: 0x06001A81 RID: 6785 RVA: 0x000B0014 File Offset: 0x000AE214
		public static void ReadPartition(JObject jObj, ObjectPath currentPath, JsonObjectTreeReaderSettings settings)
		{
			if (settings.ShouldReadObject(ObjectType.Partition))
			{
				settings.ReadObject(jObj, ObjectType.Partition, currentPath);
			}
			if (settings.ShouldReadObject(ObjectType.DataCoverageDefinition) && jObj.ContainsProperty("dataCoverageDefinition"))
			{
				JToken jtoken = jObj["dataCoverageDefinition"];
				if (jtoken.Type != 1)
				{
					throw JsonSerializationUtil.CreateException(TomSR.Exception_CannotReadPropertyObjectExpected(typeof(DataCoverageDefinition).Name, "dataCoverageDefinition"), jtoken, null);
				}
				currentPath.Push(ObjectType.DataCoverageDefinition, string.Empty);
				JsonObjectTreeReader.ReadDataCoverageDefinition((JObject)jtoken, currentPath, settings);
				currentPath.Pop();
			}
			if (settings.ShouldReadCollection(ObjectType.Annotation) && jObj.ContainsProperty("annotations"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["annotations"], ObjectType.Annotation, currentPath, settings);
			}
			if (settings.ShouldReadCollection(ObjectType.ExtendedProperty) && jObj.ContainsProperty("extendedProperties"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["extendedProperties"], ObjectType.ExtendedProperty, currentPath, settings);
			}
		}

		// Token: 0x06001A82 RID: 6786 RVA: 0x000B00F8 File Offset: 0x000AE2F8
		public static void ReadRelationship(JObject jObj, ObjectPath currentPath, JsonObjectTreeReaderSettings settings)
		{
			if (settings.ShouldReadObject(ObjectType.Relationship))
			{
				settings.ReadObject(jObj, ObjectType.Relationship, currentPath);
			}
			if (settings.ShouldReadCollection(ObjectType.Annotation) && jObj.ContainsProperty("annotations"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["annotations"], ObjectType.Annotation, currentPath, settings);
			}
			if (settings.ShouldReadCollection(ObjectType.ExtendedProperty) && jObj.ContainsProperty("extendedProperties"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["extendedProperties"], ObjectType.ExtendedProperty, currentPath, settings);
			}
			if (settings.ShouldReadCollection(ObjectType.ChangedProperty) && jObj.ContainsProperty("changedProperties"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["changedProperties"], ObjectType.ChangedProperty, currentPath, settings);
			}
		}

		// Token: 0x06001A83 RID: 6787 RVA: 0x000B0198 File Offset: 0x000AE398
		public static void ReadMeasure(JObject jObj, ObjectPath currentPath, JsonObjectTreeReaderSettings settings)
		{
			if (settings.ShouldReadObject(ObjectType.Measure))
			{
				settings.ReadObject(jObj, ObjectType.Measure, currentPath);
			}
			if (settings.ShouldReadObject(ObjectType.KPI) && jObj.ContainsProperty("kpi"))
			{
				JToken jtoken = jObj["kpi"];
				if (jtoken.Type != 1)
				{
					throw JsonSerializationUtil.CreateException(TomSR.Exception_CannotReadPropertyObjectExpected(typeof(KPI).Name, "kpi"), jtoken, null);
				}
				currentPath.Push(ObjectType.KPI, string.Empty);
				JsonObjectTreeReader.ReadKPI((JObject)jtoken, currentPath, settings);
				currentPath.Pop();
			}
			if (settings.ShouldReadObject(ObjectType.DetailRowsDefinition) && jObj.ContainsProperty("detailRowsDefinition"))
			{
				JToken jtoken2 = jObj["detailRowsDefinition"];
				if (jtoken2.Type != 1)
				{
					throw JsonSerializationUtil.CreateException(TomSR.Exception_CannotReadPropertyObjectExpected(typeof(DetailRowsDefinition).Name, "detailRowsDefinition"), jtoken2, null);
				}
				currentPath.Push(ObjectType.DetailRowsDefinition, string.Empty);
				JsonObjectTreeReader.ReadDetailRowsDefinition((JObject)jtoken2, currentPath, settings);
				currentPath.Pop();
			}
			if (settings.ShouldReadObject(ObjectType.FormatStringDefinition) && jObj.ContainsProperty("formatStringDefinition"))
			{
				JToken jtoken3 = jObj["formatStringDefinition"];
				if (jtoken3.Type != 1)
				{
					throw JsonSerializationUtil.CreateException(TomSR.Exception_CannotReadPropertyObjectExpected(typeof(FormatStringDefinition).Name, "formatStringDefinition"), jtoken3, null);
				}
				currentPath.Push(ObjectType.FormatStringDefinition, string.Empty);
				JsonObjectTreeReader.ReadFormatStringDefinition((JObject)jtoken3, currentPath, settings);
				currentPath.Pop();
			}
			if (settings.ShouldReadCollection(ObjectType.Annotation) && jObj.ContainsProperty("annotations"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["annotations"], ObjectType.Annotation, currentPath, settings);
			}
			if (settings.ShouldReadCollection(ObjectType.ExtendedProperty) && jObj.ContainsProperty("extendedProperties"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["extendedProperties"], ObjectType.ExtendedProperty, currentPath, settings);
			}
			if (settings.ShouldReadCollection(ObjectType.ChangedProperty) && jObj.ContainsProperty("changedProperties"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["changedProperties"], ObjectType.ChangedProperty, currentPath, settings);
			}
		}

		// Token: 0x06001A84 RID: 6788 RVA: 0x000B0380 File Offset: 0x000AE580
		public static void ReadHierarchy(JObject jObj, ObjectPath currentPath, JsonObjectTreeReaderSettings settings)
		{
			if (settings.ShouldReadObject(ObjectType.Hierarchy))
			{
				settings.ReadObject(jObj, ObjectType.Hierarchy, currentPath);
			}
			if (settings.ShouldReadCollection(ObjectType.Level) && jObj.ContainsProperty("levels"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["levels"], ObjectType.Level, currentPath, settings);
			}
			if (settings.ShouldReadCollection(ObjectType.Annotation) && jObj.ContainsProperty("annotations"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["annotations"], ObjectType.Annotation, currentPath, settings);
			}
			if (settings.ShouldReadCollection(ObjectType.ExtendedProperty) && jObj.ContainsProperty("extendedProperties"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["extendedProperties"], ObjectType.ExtendedProperty, currentPath, settings);
			}
			if (settings.ShouldReadCollection(ObjectType.ExcludedArtifact) && jObj.ContainsProperty("excludedArtifacts"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["excludedArtifacts"], ObjectType.ExcludedArtifact, currentPath, settings);
			}
			if (settings.ShouldReadCollection(ObjectType.ChangedProperty) && jObj.ContainsProperty("changedProperties"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["changedProperties"], ObjectType.ChangedProperty, currentPath, settings);
			}
		}

		// Token: 0x06001A85 RID: 6789 RVA: 0x000B0478 File Offset: 0x000AE678
		public static void ReadLevel(JObject jObj, ObjectPath currentPath, JsonObjectTreeReaderSettings settings)
		{
			if (settings.ShouldReadObject(ObjectType.Level))
			{
				settings.ReadObject(jObj, ObjectType.Level, currentPath);
			}
			if (settings.ShouldReadCollection(ObjectType.Annotation) && jObj.ContainsProperty("annotations"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["annotations"], ObjectType.Annotation, currentPath, settings);
			}
			if (settings.ShouldReadCollection(ObjectType.ExtendedProperty) && jObj.ContainsProperty("extendedProperties"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["extendedProperties"], ObjectType.ExtendedProperty, currentPath, settings);
			}
			if (settings.ShouldReadCollection(ObjectType.ChangedProperty) && jObj.ContainsProperty("changedProperties"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["changedProperties"], ObjectType.ChangedProperty, currentPath, settings);
			}
		}

		// Token: 0x06001A86 RID: 6790 RVA: 0x000B051A File Offset: 0x000AE71A
		public static void ReadAnnotation(JObject jObj, ObjectPath currentPath, JsonObjectTreeReaderSettings settings)
		{
			if (settings.ShouldReadObject(ObjectType.Annotation))
			{
				settings.ReadObject(jObj, ObjectType.Annotation, currentPath);
			}
		}

		// Token: 0x06001A87 RID: 6791 RVA: 0x000B0530 File Offset: 0x000AE730
		public static void ReadKPI(JObject jObj, ObjectPath currentPath, JsonObjectTreeReaderSettings settings)
		{
			if (settings.ShouldReadObject(ObjectType.KPI))
			{
				settings.ReadObject(jObj, ObjectType.KPI, currentPath);
			}
			if (settings.ShouldReadCollection(ObjectType.Annotation) && jObj.ContainsProperty("annotations"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["annotations"], ObjectType.Annotation, currentPath, settings);
			}
			if (settings.ShouldReadCollection(ObjectType.ExtendedProperty) && jObj.ContainsProperty("extendedProperties"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["extendedProperties"], ObjectType.ExtendedProperty, currentPath, settings);
			}
		}

		// Token: 0x06001A88 RID: 6792 RVA: 0x000B05A8 File Offset: 0x000AE7A8
		public static void ReadCulture(JObject jObj, ObjectPath currentPath, JsonObjectTreeReaderSettings settings)
		{
			if (settings.ShouldReadObject(ObjectType.Culture))
			{
				settings.ReadObject(jObj, ObjectType.Culture, currentPath);
			}
			if (settings.ShouldReadObject(ObjectType.LinguisticMetadata) && jObj.ContainsProperty("linguisticMetadata"))
			{
				JToken jtoken = jObj["linguisticMetadata"];
				if (jtoken.Type != 1)
				{
					throw JsonSerializationUtil.CreateException(TomSR.Exception_CannotReadPropertyObjectExpected(typeof(LinguisticMetadata).Name, "linguisticMetadata"), jtoken, null);
				}
				currentPath.Push(ObjectType.LinguisticMetadata, string.Empty);
				JsonObjectTreeReader.ReadLinguisticMetadata((JObject)jtoken, currentPath, settings);
				currentPath.Pop();
			}
			if (settings.ShouldReadCollection(ObjectType.Annotation) && jObj.ContainsProperty("annotations"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["annotations"], ObjectType.Annotation, currentPath, settings);
			}
			if (settings.ShouldReadCollection(ObjectType.ExtendedProperty) && jObj.ContainsProperty("extendedProperties"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["extendedProperties"], ObjectType.ExtendedProperty, currentPath, settings);
			}
		}

		// Token: 0x06001A89 RID: 6793 RVA: 0x000B068C File Offset: 0x000AE88C
		public static void ReadLinguisticMetadata(JObject jObj, ObjectPath currentPath, JsonObjectTreeReaderSettings settings)
		{
			if (settings.ShouldReadObject(ObjectType.LinguisticMetadata))
			{
				settings.ReadObject(jObj, ObjectType.LinguisticMetadata, currentPath);
			}
			if (settings.ShouldReadCollection(ObjectType.Annotation) && jObj.ContainsProperty("annotations"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["annotations"], ObjectType.Annotation, currentPath, settings);
			}
			if (settings.ShouldReadCollection(ObjectType.ExtendedProperty) && jObj.ContainsProperty("extendedProperties"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["extendedProperties"], ObjectType.ExtendedProperty, currentPath, settings);
			}
		}

		// Token: 0x06001A8A RID: 6794 RVA: 0x000B0704 File Offset: 0x000AE904
		public static void ReadPerspective(JObject jObj, ObjectPath currentPath, JsonObjectTreeReaderSettings settings)
		{
			if (settings.ShouldReadObject(ObjectType.Perspective))
			{
				settings.ReadObject(jObj, ObjectType.Perspective, currentPath);
			}
			if (settings.ShouldReadCollection(ObjectType.PerspectiveTable) && jObj.ContainsProperty("tables"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["tables"], ObjectType.PerspectiveTable, currentPath, settings);
			}
			if (settings.ShouldReadCollection(ObjectType.Annotation) && jObj.ContainsProperty("annotations"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["annotations"], ObjectType.Annotation, currentPath, settings);
			}
			if (settings.ShouldReadCollection(ObjectType.ExtendedProperty) && jObj.ContainsProperty("extendedProperties"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["extendedProperties"], ObjectType.ExtendedProperty, currentPath, settings);
			}
		}

		// Token: 0x06001A8B RID: 6795 RVA: 0x000B07A8 File Offset: 0x000AE9A8
		public static void ReadPerspectiveTable(JObject jObj, ObjectPath currentPath, JsonObjectTreeReaderSettings settings)
		{
			if (settings.ShouldReadObject(ObjectType.PerspectiveTable))
			{
				settings.ReadObject(jObj, ObjectType.PerspectiveTable, currentPath);
			}
			if (settings.ShouldReadCollection(ObjectType.PerspectiveColumn) && jObj.ContainsProperty("columns"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["columns"], ObjectType.PerspectiveColumn, currentPath, settings);
			}
			if (settings.ShouldReadCollection(ObjectType.PerspectiveMeasure) && jObj.ContainsProperty("measures"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["measures"], ObjectType.PerspectiveMeasure, currentPath, settings);
			}
			if (settings.ShouldReadCollection(ObjectType.PerspectiveHierarchy) && jObj.ContainsProperty("hierarchies"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["hierarchies"], ObjectType.PerspectiveHierarchy, currentPath, settings);
			}
			if (settings.ShouldReadCollection(ObjectType.PerspectiveSet) && jObj.ContainsProperty("sets"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["sets"], ObjectType.PerspectiveSet, currentPath, settings);
			}
			if (settings.ShouldReadCollection(ObjectType.Annotation) && jObj.ContainsProperty("annotations"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["annotations"], ObjectType.Annotation, currentPath, settings);
			}
			if (settings.ShouldReadCollection(ObjectType.ExtendedProperty) && jObj.ContainsProperty("extendedProperties"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["extendedProperties"], ObjectType.ExtendedProperty, currentPath, settings);
			}
		}

		// Token: 0x06001A8C RID: 6796 RVA: 0x000B08CC File Offset: 0x000AEACC
		public static void ReadPerspectiveColumn(JObject jObj, ObjectPath currentPath, JsonObjectTreeReaderSettings settings)
		{
			if (settings.ShouldReadObject(ObjectType.PerspectiveColumn))
			{
				settings.ReadObject(jObj, ObjectType.PerspectiveColumn, currentPath);
			}
			if (settings.ShouldReadCollection(ObjectType.Annotation) && jObj.ContainsProperty("annotations"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["annotations"], ObjectType.Annotation, currentPath, settings);
			}
			if (settings.ShouldReadCollection(ObjectType.ExtendedProperty) && jObj.ContainsProperty("extendedProperties"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["extendedProperties"], ObjectType.ExtendedProperty, currentPath, settings);
			}
		}

		// Token: 0x06001A8D RID: 6797 RVA: 0x000B0944 File Offset: 0x000AEB44
		public static void ReadPerspectiveHierarchy(JObject jObj, ObjectPath currentPath, JsonObjectTreeReaderSettings settings)
		{
			if (settings.ShouldReadObject(ObjectType.PerspectiveHierarchy))
			{
				settings.ReadObject(jObj, ObjectType.PerspectiveHierarchy, currentPath);
			}
			if (settings.ShouldReadCollection(ObjectType.Annotation) && jObj.ContainsProperty("annotations"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["annotations"], ObjectType.Annotation, currentPath, settings);
			}
			if (settings.ShouldReadCollection(ObjectType.ExtendedProperty) && jObj.ContainsProperty("extendedProperties"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["extendedProperties"], ObjectType.ExtendedProperty, currentPath, settings);
			}
		}

		// Token: 0x06001A8E RID: 6798 RVA: 0x000B09BC File Offset: 0x000AEBBC
		public static void ReadPerspectiveMeasure(JObject jObj, ObjectPath currentPath, JsonObjectTreeReaderSettings settings)
		{
			if (settings.ShouldReadObject(ObjectType.PerspectiveMeasure))
			{
				settings.ReadObject(jObj, ObjectType.PerspectiveMeasure, currentPath);
			}
			if (settings.ShouldReadCollection(ObjectType.Annotation) && jObj.ContainsProperty("annotations"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["annotations"], ObjectType.Annotation, currentPath, settings);
			}
			if (settings.ShouldReadCollection(ObjectType.ExtendedProperty) && jObj.ContainsProperty("extendedProperties"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["extendedProperties"], ObjectType.ExtendedProperty, currentPath, settings);
			}
		}

		// Token: 0x06001A8F RID: 6799 RVA: 0x000B0A34 File Offset: 0x000AEC34
		public static void ReadModelRole(JObject jObj, ObjectPath currentPath, JsonObjectTreeReaderSettings settings)
		{
			if (settings.ShouldReadObject(ObjectType.Role))
			{
				settings.ReadObject(jObj, ObjectType.Role, currentPath);
			}
			if (settings.ShouldReadCollection(ObjectType.RoleMembership) && jObj.ContainsProperty("members"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["members"], ObjectType.RoleMembership, currentPath, settings);
			}
			if (settings.ShouldReadCollection(ObjectType.TablePermission) && jObj.ContainsProperty("tablePermissions"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["tablePermissions"], ObjectType.TablePermission, currentPath, settings);
			}
			if (settings.ShouldReadCollection(ObjectType.Annotation) && jObj.ContainsProperty("annotations"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["annotations"], ObjectType.Annotation, currentPath, settings);
			}
			if (settings.ShouldReadCollection(ObjectType.ExtendedProperty) && jObj.ContainsProperty("extendedProperties"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["extendedProperties"], ObjectType.ExtendedProperty, currentPath, settings);
			}
		}

		// Token: 0x06001A90 RID: 6800 RVA: 0x000B0B04 File Offset: 0x000AED04
		public static void ReadModelRoleMember(JObject jObj, ObjectPath currentPath, JsonObjectTreeReaderSettings settings)
		{
			if (settings.ShouldReadObject(ObjectType.RoleMembership))
			{
				settings.ReadObject(jObj, ObjectType.RoleMembership, currentPath);
			}
			if (settings.ShouldReadCollection(ObjectType.Annotation) && jObj.ContainsProperty("annotations"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["annotations"], ObjectType.Annotation, currentPath, settings);
			}
			if (settings.ShouldReadCollection(ObjectType.ExtendedProperty) && jObj.ContainsProperty("extendedProperties"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["extendedProperties"], ObjectType.ExtendedProperty, currentPath, settings);
			}
		}

		// Token: 0x06001A91 RID: 6801 RVA: 0x000B0B7C File Offset: 0x000AED7C
		public static void ReadTablePermission(JObject jObj, ObjectPath currentPath, JsonObjectTreeReaderSettings settings)
		{
			if (settings.ShouldReadObject(ObjectType.TablePermission))
			{
				settings.ReadObject(jObj, ObjectType.TablePermission, currentPath);
			}
			if (settings.ShouldReadCollection(ObjectType.Annotation) && jObj.ContainsProperty("annotations"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["annotations"], ObjectType.Annotation, currentPath, settings);
			}
			if (settings.ShouldReadCollection(ObjectType.ExtendedProperty) && jObj.ContainsProperty("extendedProperties"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["extendedProperties"], ObjectType.ExtendedProperty, currentPath, settings);
			}
			if (settings.ShouldReadCollection(ObjectType.ColumnPermission) && jObj.ContainsProperty("columnPermissions"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["columnPermissions"], ObjectType.ColumnPermission, currentPath, settings);
			}
		}

		// Token: 0x06001A92 RID: 6802 RVA: 0x000B0C20 File Offset: 0x000AEE20
		public static void ReadVariation(JObject jObj, ObjectPath currentPath, JsonObjectTreeReaderSettings settings)
		{
			if (settings.ShouldReadObject(ObjectType.Variation))
			{
				settings.ReadObject(jObj, ObjectType.Variation, currentPath);
			}
			if (settings.ShouldReadCollection(ObjectType.Annotation) && jObj.ContainsProperty("annotations"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["annotations"], ObjectType.Annotation, currentPath, settings);
			}
			if (settings.ShouldReadCollection(ObjectType.ExtendedProperty) && jObj.ContainsProperty("extendedProperties"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["extendedProperties"], ObjectType.ExtendedProperty, currentPath, settings);
			}
		}

		// Token: 0x06001A93 RID: 6803 RVA: 0x000B0C98 File Offset: 0x000AEE98
		public static void ReadSet(JObject jObj, ObjectPath currentPath, JsonObjectTreeReaderSettings settings)
		{
			if (settings.ShouldReadObject(ObjectType.Set))
			{
				settings.ReadObject(jObj, ObjectType.Set, currentPath);
			}
			if (settings.ShouldReadCollection(ObjectType.Annotation) && jObj.ContainsProperty("annotations"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["annotations"], ObjectType.Annotation, currentPath, settings);
			}
			if (settings.ShouldReadCollection(ObjectType.ExtendedProperty) && jObj.ContainsProperty("extendedProperties"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["extendedProperties"], ObjectType.ExtendedProperty, currentPath, settings);
			}
		}

		// Token: 0x06001A94 RID: 6804 RVA: 0x000B0D10 File Offset: 0x000AEF10
		public static void ReadPerspectiveSet(JObject jObj, ObjectPath currentPath, JsonObjectTreeReaderSettings settings)
		{
			if (settings.ShouldReadObject(ObjectType.PerspectiveSet))
			{
				settings.ReadObject(jObj, ObjectType.PerspectiveSet, currentPath);
			}
			if (settings.ShouldReadCollection(ObjectType.Annotation) && jObj.ContainsProperty("annotations"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["annotations"], ObjectType.Annotation, currentPath, settings);
			}
			if (settings.ShouldReadCollection(ObjectType.ExtendedProperty) && jObj.ContainsProperty("extendedProperties"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["extendedProperties"], ObjectType.ExtendedProperty, currentPath, settings);
			}
		}

		// Token: 0x06001A95 RID: 6805 RVA: 0x000B0D87 File Offset: 0x000AEF87
		public static void ReadExtendedProperty(JObject jObj, ObjectPath currentPath, JsonObjectTreeReaderSettings settings)
		{
			if (settings.ShouldReadObject(ObjectType.ExtendedProperty))
			{
				settings.ReadObject(jObj, ObjectType.ExtendedProperty, currentPath);
			}
		}

		// Token: 0x06001A96 RID: 6806 RVA: 0x000B0DA0 File Offset: 0x000AEFA0
		public static void ReadNamedExpression(JObject jObj, ObjectPath currentPath, JsonObjectTreeReaderSettings settings)
		{
			if (settings.ShouldReadObject(ObjectType.Expression))
			{
				settings.ReadObject(jObj, ObjectType.Expression, currentPath);
			}
			if (settings.ShouldReadCollection(ObjectType.Annotation) && jObj.ContainsProperty("annotations"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["annotations"], ObjectType.Annotation, currentPath, settings);
			}
			if (settings.ShouldReadCollection(ObjectType.ExtendedProperty) && jObj.ContainsProperty("extendedProperties"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["extendedProperties"], ObjectType.ExtendedProperty, currentPath, settings);
			}
			if (settings.ShouldReadCollection(ObjectType.ExcludedArtifact) && jObj.ContainsProperty("excludedArtifacts"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["excludedArtifacts"], ObjectType.ExcludedArtifact, currentPath, settings);
			}
		}

		// Token: 0x06001A97 RID: 6807 RVA: 0x000B0E44 File Offset: 0x000AF044
		public static void ReadColumnPermission(JObject jObj, ObjectPath currentPath, JsonObjectTreeReaderSettings settings)
		{
			if (settings.ShouldReadObject(ObjectType.ColumnPermission))
			{
				settings.ReadObject(jObj, ObjectType.ColumnPermission, currentPath);
			}
			if (settings.ShouldReadCollection(ObjectType.Annotation) && jObj.ContainsProperty("annotations"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["annotations"], ObjectType.Annotation, currentPath, settings);
			}
			if (settings.ShouldReadCollection(ObjectType.ExtendedProperty) && jObj.ContainsProperty("extendedProperties"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["extendedProperties"], ObjectType.ExtendedProperty, currentPath, settings);
			}
		}

		// Token: 0x06001A98 RID: 6808 RVA: 0x000B0EBB File Offset: 0x000AF0BB
		public static void ReadDetailRowsDefinition(JObject jObj, ObjectPath currentPath, JsonObjectTreeReaderSettings settings)
		{
			if (settings.ShouldReadObject(ObjectType.DetailRowsDefinition))
			{
				settings.ReadObject(jObj, ObjectType.DetailRowsDefinition, currentPath);
			}
		}

		// Token: 0x06001A99 RID: 6809 RVA: 0x000B0ED4 File Offset: 0x000AF0D4
		public static void ReadRelatedColumnDetails(JObject jObj, ObjectPath currentPath, JsonObjectTreeReaderSettings settings)
		{
			if (settings.ShouldReadObject(ObjectType.RelatedColumnDetails))
			{
				settings.ReadObject(jObj, ObjectType.RelatedColumnDetails, currentPath);
			}
			if (settings.ShouldReadCollection(ObjectType.GroupByColumn) && jObj.ContainsProperty("groupByColumns"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["groupByColumns"], ObjectType.GroupByColumn, currentPath, settings);
			}
		}

		// Token: 0x06001A9A RID: 6810 RVA: 0x000B0F20 File Offset: 0x000AF120
		public static void ReadGroupByColumn(JObject jObj, ObjectPath currentPath, JsonObjectTreeReaderSettings settings)
		{
			if (settings.ShouldReadObject(ObjectType.GroupByColumn))
			{
				settings.ReadObject(jObj, ObjectType.GroupByColumn, currentPath);
			}
		}

		// Token: 0x06001A9B RID: 6811 RVA: 0x000B0F38 File Offset: 0x000AF138
		public static void ReadCalculationGroup(JObject jObj, ObjectPath currentPath, JsonObjectTreeReaderSettings settings)
		{
			if (settings.ShouldReadObject(ObjectType.CalculationGroup))
			{
				settings.ReadObject(jObj, ObjectType.CalculationGroup, currentPath);
			}
			if (settings.ShouldReadObject(ObjectType.CalculationExpression))
			{
				if (jObj.ContainsProperty("multipleOrEmptySelectionExpression"))
				{
					JToken jtoken = jObj["multipleOrEmptySelectionExpression"];
					if (jtoken.Type != 1)
					{
						throw JsonSerializationUtil.CreateException(TomSR.Exception_CannotReadPropertyObjectExpected(typeof(CalculationGroupExpression).Name, "multipleOrEmptySelectionExpression"), jtoken, null);
					}
					currentPath.Push(ObjectType.CalculationExpression, "multipleOrEmptySelectionExpression");
					JsonObjectTreeReader.ReadCalculationGroupExpression((JObject)jtoken, currentPath, settings);
					currentPath.Pop();
				}
				if (jObj.ContainsProperty("noSelectionExpression"))
				{
					JToken jtoken2 = jObj["noSelectionExpression"];
					if (jtoken2.Type != 1)
					{
						throw JsonSerializationUtil.CreateException(TomSR.Exception_CannotReadPropertyObjectExpected(typeof(CalculationGroupExpression).Name, "noSelectionExpression"), jtoken2, null);
					}
					currentPath.Push(ObjectType.CalculationExpression, "noSelectionExpression");
					JsonObjectTreeReader.ReadCalculationGroupExpression((JObject)jtoken2, currentPath, settings);
					currentPath.Pop();
				}
			}
			if (settings.ShouldReadCollection(ObjectType.Annotation) && jObj.ContainsProperty("annotations"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["annotations"], ObjectType.Annotation, currentPath, settings);
			}
			if (settings.ShouldReadCollection(ObjectType.CalculationItem) && jObj.ContainsProperty("calculationItems"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["calculationItems"], ObjectType.CalculationItem, currentPath, settings);
			}
		}

		// Token: 0x06001A9C RID: 6812 RVA: 0x000B1084 File Offset: 0x000AF284
		public static void ReadCalculationItem(JObject jObj, ObjectPath currentPath, JsonObjectTreeReaderSettings settings)
		{
			if (settings.ShouldReadObject(ObjectType.CalculationItem))
			{
				settings.ReadObject(jObj, ObjectType.CalculationItem, currentPath);
			}
			if (settings.ShouldReadObject(ObjectType.FormatStringDefinition) && jObj.ContainsProperty("formatStringDefinition"))
			{
				JToken jtoken = jObj["formatStringDefinition"];
				if (jtoken.Type != 1)
				{
					throw JsonSerializationUtil.CreateException(TomSR.Exception_CannotReadPropertyObjectExpected(typeof(FormatStringDefinition).Name, "formatStringDefinition"), jtoken, null);
				}
				currentPath.Push(ObjectType.FormatStringDefinition, string.Empty);
				JsonObjectTreeReader.ReadFormatStringDefinition((JObject)jtoken, currentPath, settings);
				currentPath.Pop();
			}
		}

		// Token: 0x06001A9D RID: 6813 RVA: 0x000B1114 File Offset: 0x000AF314
		public static void ReadAlternateOf(JObject jObj, ObjectPath currentPath, JsonObjectTreeReaderSettings settings)
		{
			if (settings.ShouldReadObject(ObjectType.AlternateOf))
			{
				settings.ReadObject(jObj, ObjectType.AlternateOf, currentPath);
			}
			if (settings.ShouldReadCollection(ObjectType.Annotation) && jObj.ContainsProperty("annotations"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["annotations"], ObjectType.Annotation, currentPath, settings);
			}
		}

		// Token: 0x06001A9E RID: 6814 RVA: 0x000B1160 File Offset: 0x000AF360
		public static void ReadRefreshPolicy(JObject jObj, ObjectPath currentPath, JsonObjectTreeReaderSettings settings)
		{
			if (settings.ShouldReadObject(ObjectType.RefreshPolicy))
			{
				settings.ReadObject(jObj, ObjectType.RefreshPolicy, currentPath);
			}
			if (settings.ShouldReadCollection(ObjectType.Annotation) && jObj.ContainsProperty("annotations"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["annotations"], ObjectType.Annotation, currentPath, settings);
			}
			if (settings.ShouldReadCollection(ObjectType.ExtendedProperty) && jObj.ContainsProperty("extendedProperties"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["extendedProperties"], ObjectType.ExtendedProperty, currentPath, settings);
			}
		}

		// Token: 0x06001A9F RID: 6815 RVA: 0x000B11D7 File Offset: 0x000AF3D7
		public static void ReadFormatStringDefinition(JObject jObj, ObjectPath currentPath, JsonObjectTreeReaderSettings settings)
		{
			if (settings.ShouldReadObject(ObjectType.FormatStringDefinition))
			{
				settings.ReadObject(jObj, ObjectType.FormatStringDefinition, currentPath);
			}
		}

		// Token: 0x06001AA0 RID: 6816 RVA: 0x000B11F0 File Offset: 0x000AF3F0
		public static void ReadQueryGroup(JObject jObj, ObjectPath currentPath, JsonObjectTreeReaderSettings settings)
		{
			if (settings.ShouldReadObject(ObjectType.QueryGroup))
			{
				settings.ReadObject(jObj, ObjectType.QueryGroup, currentPath);
			}
			if (settings.ShouldReadCollection(ObjectType.Annotation) && jObj.ContainsProperty("annotations"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["annotations"], ObjectType.Annotation, currentPath, settings);
			}
		}

		// Token: 0x06001AA1 RID: 6817 RVA: 0x000B123C File Offset: 0x000AF43C
		public static void ReadAnalyticsAIMetadata(JObject jObj, ObjectPath currentPath, JsonObjectTreeReaderSettings settings)
		{
			if (settings.ShouldReadObject(ObjectType.AnalyticsAIMetadata))
			{
				settings.ReadObject(jObj, ObjectType.AnalyticsAIMetadata, currentPath);
			}
		}

		// Token: 0x06001AA2 RID: 6818 RVA: 0x000B1252 File Offset: 0x000AF452
		public static void ReadChangedProperty(JObject jObj, ObjectPath currentPath, JsonObjectTreeReaderSettings settings)
		{
			if (settings.ShouldReadObject(ObjectType.ChangedProperty))
			{
				settings.ReadObject(jObj, ObjectType.ChangedProperty, currentPath);
			}
		}

		// Token: 0x06001AA3 RID: 6819 RVA: 0x000B1268 File Offset: 0x000AF468
		public static void ReadExcludedArtifact(JObject jObj, ObjectPath currentPath, JsonObjectTreeReaderSettings settings)
		{
			if (settings.ShouldReadObject(ObjectType.ExcludedArtifact))
			{
				settings.ReadObject(jObj, ObjectType.ExcludedArtifact, currentPath);
			}
		}

		// Token: 0x06001AA4 RID: 6820 RVA: 0x000B1280 File Offset: 0x000AF480
		public static void ReadDataCoverageDefinition(JObject jObj, ObjectPath currentPath, JsonObjectTreeReaderSettings settings)
		{
			if (settings.ShouldReadObject(ObjectType.DataCoverageDefinition))
			{
				settings.ReadObject(jObj, ObjectType.DataCoverageDefinition, currentPath);
			}
			if (settings.ShouldReadCollection(ObjectType.Annotation) && jObj.ContainsProperty("annotations"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["annotations"], ObjectType.Annotation, currentPath, settings);
			}
		}

		// Token: 0x06001AA5 RID: 6821 RVA: 0x000B12CC File Offset: 0x000AF4CC
		public static void ReadCalculationGroupExpression(JObject jObj, ObjectPath currentPath, JsonObjectTreeReaderSettings settings)
		{
			if (settings.ShouldReadObject(ObjectType.CalculationExpression))
			{
				settings.ReadObject(jObj, ObjectType.CalculationExpression, currentPath);
			}
			if (settings.ShouldReadObject(ObjectType.FormatStringDefinition) && jObj.ContainsProperty("formatStringDefinition"))
			{
				JToken jtoken = jObj["formatStringDefinition"];
				if (jtoken.Type != 1)
				{
					throw JsonSerializationUtil.CreateException(TomSR.Exception_CannotReadPropertyObjectExpected(typeof(FormatStringDefinition).Name, "formatStringDefinition"), jtoken, null);
				}
				currentPath.Push(ObjectType.FormatStringDefinition, string.Empty);
				JsonObjectTreeReader.ReadFormatStringDefinition((JObject)jtoken, currentPath, settings);
				currentPath.Pop();
			}
		}

		// Token: 0x06001AA6 RID: 6822 RVA: 0x000B135C File Offset: 0x000AF55C
		public static void ReadCalendar(JObject jObj, ObjectPath currentPath, JsonObjectTreeReaderSettings settings)
		{
			if (settings.ShouldReadObject(ObjectType.Calendar))
			{
				settings.ReadObject(jObj, ObjectType.Calendar, currentPath);
			}
			if (settings.ShouldReadCollection(ObjectType.TimeUnitColumnAssociation) && jObj.ContainsProperty("timeUnitColumnAssociations"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["timeUnitColumnAssociations"], ObjectType.TimeUnitColumnAssociation, currentPath, settings);
			}
		}

		// Token: 0x06001AA7 RID: 6823 RVA: 0x000B13A8 File Offset: 0x000AF5A8
		public static void ReadTimeUnitColumnAssociation(JObject jObj, ObjectPath currentPath, JsonObjectTreeReaderSettings settings)
		{
			if (settings.ShouldReadObject(ObjectType.TimeUnitColumnAssociation))
			{
				settings.ReadObject(jObj, ObjectType.TimeUnitColumnAssociation, currentPath);
			}
		}

		// Token: 0x06001AA8 RID: 6824 RVA: 0x000B13C0 File Offset: 0x000AF5C0
		public static void ReadFunction(JObject jObj, ObjectPath currentPath, JsonObjectTreeReaderSettings settings)
		{
			if (settings.ShouldReadObject(ObjectType.Function))
			{
				settings.ReadObject(jObj, ObjectType.Function, currentPath);
			}
			if (settings.ShouldReadCollection(ObjectType.Annotation) && jObj.ContainsProperty("annotations"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["annotations"], ObjectType.Annotation, currentPath, settings);
			}
			if (settings.ShouldReadCollection(ObjectType.ExtendedProperty) && jObj.ContainsProperty("extendedProperties"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["extendedProperties"], ObjectType.ExtendedProperty, currentPath, settings);
			}
			if (settings.ShouldReadCollection(ObjectType.ChangedProperty) && jObj.ContainsProperty("changedProperties"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["changedProperties"], ObjectType.ChangedProperty, currentPath, settings);
			}
		}

		// Token: 0x06001AA9 RID: 6825 RVA: 0x000B1464 File Offset: 0x000AF664
		public static void ReadBindingInfo(JObject jObj, ObjectPath currentPath, JsonObjectTreeReaderSettings settings)
		{
			if (settings.ShouldReadObject(ObjectType.BindingInfo))
			{
				settings.ReadObject(jObj, ObjectType.BindingInfo, currentPath);
			}
			if (settings.ShouldReadCollection(ObjectType.Annotation) && jObj.ContainsProperty("annotations"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["annotations"], ObjectType.Annotation, currentPath, settings);
			}
			if (settings.ShouldReadCollection(ObjectType.ExtendedProperty) && jObj.ContainsProperty("extendedProperties"))
			{
				JsonObjectTreeReader.ReadCollection(jObj["extendedProperties"], ObjectType.ExtendedProperty, currentPath, settings);
			}
		}

		// Token: 0x06001AAA RID: 6826 RVA: 0x000B14DC File Offset: 0x000AF6DC
		private static void ReadCollection(JToken jsonArray, ObjectType collectionItemType, ObjectPath currentPath, JsonObjectTreeReaderSettings settings)
		{
			if (jsonArray.Type != 2)
			{
				throw JsonSerializationUtil.CreateException(TomSR.Exception_CannotReadMetadataObjectCollectionWithTypeFromJson(collectionItemType.ToString()), jsonArray, null);
			}
			JArray jarray = jsonArray as JArray;
			Utils.Verify(jarray != null);
			foreach (object obj in jarray)
			{
				JToken jtoken = (JToken)obj;
				if (jtoken.Type != 1)
				{
					throw JsonSerializationUtil.CreateException(TomSR.Exception_CannotReadMetadataObjectCollectionWithTypeFromJson(collectionItemType.ToString()), jtoken, null);
				}
				JObject jobject = jtoken as JObject;
				Utils.Verify(jobject != null);
				if (!jobject.ContainsProperty("name"))
				{
					throw new InvalidOperationException();
				}
				JToken jtoken2 = jobject["name"];
				if (jtoken2.Type != 8)
				{
					throw JsonSerializationUtil.CreateException(TomSR.Exception_Json_MissingRequiredProperty("name"), jtoken2, null);
				}
				currentPath.Push(collectionItemType, jobject.Value<string>("name"));
				JsonObjectTreeReader.ReadObject(jobject, collectionItemType, currentPath, settings);
				currentPath.Pop();
			}
		}

		// Token: 0x06001AAB RID: 6827 RVA: 0x000B15F4 File Offset: 0x000AF7F4
		private static void ReadObject(JObject jObj, ObjectType type, ObjectPath currentPath, JsonObjectTreeReaderSettings settings)
		{
			switch (type)
			{
			case ObjectType.Model:
				JsonObjectTreeReader.ReadModel(jObj, currentPath, settings);
				return;
			case ObjectType.DataSource:
				JsonObjectTreeReader.ReadDataSource(jObj, currentPath, settings);
				return;
			case ObjectType.Table:
				JsonObjectTreeReader.ReadTable(jObj, currentPath, settings);
				return;
			case ObjectType.Column:
				JsonObjectTreeReader.ReadColumn(jObj, currentPath, settings);
				return;
			case ObjectType.AttributeHierarchy:
				JsonObjectTreeReader.ReadAttributeHierarchy(jObj, currentPath, settings);
				return;
			case ObjectType.Partition:
				JsonObjectTreeReader.ReadPartition(jObj, currentPath, settings);
				return;
			case ObjectType.Relationship:
				JsonObjectTreeReader.ReadRelationship(jObj, currentPath, settings);
				return;
			case ObjectType.Measure:
				JsonObjectTreeReader.ReadMeasure(jObj, currentPath, settings);
				return;
			case ObjectType.Hierarchy:
				JsonObjectTreeReader.ReadHierarchy(jObj, currentPath, settings);
				return;
			case ObjectType.Level:
				JsonObjectTreeReader.ReadLevel(jObj, currentPath, settings);
				return;
			case ObjectType.Annotation:
				JsonObjectTreeReader.ReadAnnotation(jObj, currentPath, settings);
				return;
			case ObjectType.KPI:
				JsonObjectTreeReader.ReadKPI(jObj, currentPath, settings);
				return;
			case ObjectType.Culture:
				JsonObjectTreeReader.ReadCulture(jObj, currentPath, settings);
				return;
			case ObjectType.LinguisticMetadata:
				JsonObjectTreeReader.ReadLinguisticMetadata(jObj, currentPath, settings);
				return;
			case ObjectType.Perspective:
				JsonObjectTreeReader.ReadPerspective(jObj, currentPath, settings);
				return;
			case ObjectType.PerspectiveTable:
				JsonObjectTreeReader.ReadPerspectiveTable(jObj, currentPath, settings);
				return;
			case ObjectType.PerspectiveColumn:
				JsonObjectTreeReader.ReadPerspectiveColumn(jObj, currentPath, settings);
				return;
			case ObjectType.PerspectiveHierarchy:
				JsonObjectTreeReader.ReadPerspectiveHierarchy(jObj, currentPath, settings);
				return;
			case ObjectType.PerspectiveMeasure:
				JsonObjectTreeReader.ReadPerspectiveMeasure(jObj, currentPath, settings);
				return;
			case ObjectType.Role:
				JsonObjectTreeReader.ReadModelRole(jObj, currentPath, settings);
				return;
			case ObjectType.RoleMembership:
				JsonObjectTreeReader.ReadModelRoleMember(jObj, currentPath, settings);
				return;
			case ObjectType.TablePermission:
				JsonObjectTreeReader.ReadTablePermission(jObj, currentPath, settings);
				return;
			case ObjectType.Variation:
				JsonObjectTreeReader.ReadVariation(jObj, currentPath, settings);
				return;
			case ObjectType.Set:
				JsonObjectTreeReader.ReadSet(jObj, currentPath, settings);
				return;
			case ObjectType.PerspectiveSet:
				JsonObjectTreeReader.ReadPerspectiveSet(jObj, currentPath, settings);
				return;
			case ObjectType.ExtendedProperty:
				JsonObjectTreeReader.ReadExtendedProperty(jObj, currentPath, settings);
				return;
			case ObjectType.Expression:
				JsonObjectTreeReader.ReadNamedExpression(jObj, currentPath, settings);
				return;
			case ObjectType.ColumnPermission:
				JsonObjectTreeReader.ReadColumnPermission(jObj, currentPath, settings);
				return;
			case ObjectType.DetailRowsDefinition:
				JsonObjectTreeReader.ReadDetailRowsDefinition(jObj, currentPath, settings);
				return;
			case ObjectType.RelatedColumnDetails:
				JsonObjectTreeReader.ReadRelatedColumnDetails(jObj, currentPath, settings);
				return;
			case ObjectType.GroupByColumn:
				JsonObjectTreeReader.ReadGroupByColumn(jObj, currentPath, settings);
				return;
			case ObjectType.CalculationGroup:
				JsonObjectTreeReader.ReadCalculationGroup(jObj, currentPath, settings);
				return;
			case ObjectType.CalculationItem:
				JsonObjectTreeReader.ReadCalculationItem(jObj, currentPath, settings);
				return;
			case ObjectType.AlternateOf:
				JsonObjectTreeReader.ReadAlternateOf(jObj, currentPath, settings);
				return;
			case ObjectType.RefreshPolicy:
				JsonObjectTreeReader.ReadRefreshPolicy(jObj, currentPath, settings);
				return;
			case ObjectType.FormatStringDefinition:
				JsonObjectTreeReader.ReadFormatStringDefinition(jObj, currentPath, settings);
				return;
			case ObjectType.QueryGroup:
				JsonObjectTreeReader.ReadQueryGroup(jObj, currentPath, settings);
				return;
			case ObjectType.AnalyticsAIMetadata:
				JsonObjectTreeReader.ReadAnalyticsAIMetadata(jObj, currentPath, settings);
				return;
			case ObjectType.ChangedProperty:
				JsonObjectTreeReader.ReadChangedProperty(jObj, currentPath, settings);
				return;
			case ObjectType.ExcludedArtifact:
				JsonObjectTreeReader.ReadExcludedArtifact(jObj, currentPath, settings);
				return;
			case ObjectType.DataCoverageDefinition:
				JsonObjectTreeReader.ReadDataCoverageDefinition(jObj, currentPath, settings);
				return;
			case ObjectType.CalculationExpression:
				JsonObjectTreeReader.ReadCalculationGroupExpression(jObj, currentPath, settings);
				return;
			case ObjectType.Calendar:
				JsonObjectTreeReader.ReadCalendar(jObj, currentPath, settings);
				return;
			case ObjectType.TimeUnitColumnAssociation:
				JsonObjectTreeReader.ReadTimeUnitColumnAssociation(jObj, currentPath, settings);
				return;
			case ObjectType.Function:
				JsonObjectTreeReader.ReadFunction(jObj, currentPath, settings);
				return;
			case ObjectType.BindingInfo:
				JsonObjectTreeReader.ReadBindingInfo(jObj, currentPath, settings);
				return;
			}
			throw TomInternalException.Create("Unexpected object type: {0}", new object[] { type });
		}
	}
}
