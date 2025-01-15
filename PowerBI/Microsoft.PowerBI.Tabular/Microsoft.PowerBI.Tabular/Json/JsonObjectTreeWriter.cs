using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.AnalysisServices.Tabular.Json
{
	// Token: 0x020001B7 RID: 439
	internal static class JsonObjectTreeWriter
	{
		// Token: 0x06001AEA RID: 6890 RVA: 0x000B4E78 File Offset: 0x000B3078
		public static void WriteModel(Model metadataObj, JsonObject result, JsonObjectTreeWriterSettings settings)
		{
			result["name", TomPropCategory.Name, 0, false] = metadataObj.Name;
			if (settings.ShouldWriteObject(metadataObj))
			{
				settings.WriteObject(metadataObj, result);
			}
			if (settings.ShouldWriteCollection(metadataObj.Tables))
			{
				object[] array = metadataObj.Tables.Where((Table o) => settings.ShouldWriteObject(o)).Select(delegate(Table obj)
				{
					JsonObject jsonObject = new JsonObject();
					JsonObjectTreeWriter.WriteTable(obj, jsonObject, settings);
					return jsonObject.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array2 = array;
				result["tables", TomPropCategory.ChildCollection, 2, false] = array2;
			}
			if (settings.ShouldWriteCollection(metadataObj.Relationships))
			{
				object[] array = metadataObj.Relationships.Where((Relationship o) => settings.ShouldWriteObject(o)).Select(delegate(Relationship obj)
				{
					JsonObject jsonObject2 = new JsonObject();
					JsonObjectTreeWriter.WriteRelationship(obj, jsonObject2, settings);
					return jsonObject2.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array3 = array;
				result["relationships", TomPropCategory.ChildCollection, 6, false] = array3;
			}
			if (settings.ShouldWriteCollection(metadataObj.DataSources))
			{
				object[] array = metadataObj.DataSources.Where((DataSource o) => settings.ShouldWriteObject(o)).Select(delegate(DataSource obj)
				{
					JsonObject jsonObject3 = new JsonObject();
					JsonObjectTreeWriter.WriteDataSource(obj, jsonObject3, settings);
					return jsonObject3.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array4 = array;
				result["dataSources", TomPropCategory.ChildCollection, 1, false] = array4;
			}
			if (settings.ShouldWriteCollection(metadataObj.Perspectives))
			{
				object[] array = metadataObj.Perspectives.Where((Perspective o) => settings.ShouldWriteObject(o)).Select(delegate(Perspective obj)
				{
					JsonObject jsonObject4 = new JsonObject();
					JsonObjectTreeWriter.WritePerspective(obj, jsonObject4, settings);
					return jsonObject4.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array5 = array;
				result["perspectives", TomPropCategory.ChildCollection, 28, false] = array5;
			}
			if (settings.ShouldWriteCollection(metadataObj.Cultures))
			{
				object[] array = metadataObj.Cultures.Where((Culture o) => settings.ShouldWriteObject(o)).Select(delegate(Culture obj)
				{
					JsonObject jsonObject5 = new JsonObject();
					JsonObjectTreeWriter.WriteCulture(obj, jsonObject5, settings);
					return jsonObject5.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array6 = array;
				result["cultures", TomPropCategory.ChildCollection, 12, false] = array6;
			}
			if (settings.ShouldWriteCollection(metadataObj.Roles))
			{
				object[] array = metadataObj.Roles.Where((ModelRole o) => settings.ShouldWriteObject(o)).Select(delegate(ModelRole obj)
				{
					JsonObject jsonObject6 = new JsonObject();
					JsonObjectTreeWriter.WriteModelRole(obj, jsonObject6, settings);
					return jsonObject6.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array7 = array;
				result["roles", TomPropCategory.ChildCollection, 33, false] = array7;
			}
			if (settings.ShouldWriteCollection(metadataObj.Expressions))
			{
				object[] array = metadataObj.Expressions.Where((NamedExpression o) => settings.ShouldWriteObject(o)).Select(delegate(NamedExpression obj)
				{
					JsonObject jsonObject7 = new JsonObject();
					JsonObjectTreeWriter.WriteNamedExpression(obj, jsonObject7, settings);
					return jsonObject7.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array8 = array;
				result["expressions", TomPropCategory.ChildCollection, 40, false] = array8;
			}
			if (settings.ShouldWriteCollection(metadataObj.QueryGroups))
			{
				object[] array = metadataObj.QueryGroups.Where((QueryGroup o) => settings.ShouldWriteObject(o)).Select(delegate(QueryGroup obj)
				{
					JsonObject jsonObject8 = new JsonObject();
					JsonObjectTreeWriter.WriteQueryGroup(obj, jsonObject8, settings);
					return jsonObject8.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array9 = array;
				result["queryGroups", TomPropCategory.ChildCollection, 50, false] = array9;
			}
			if (settings.ShouldWriteCollection(metadataObj.AnalyticsAIMetadata))
			{
				object[] array = metadataObj.AnalyticsAIMetadata.Where((AnalyticsAIMetadata o) => settings.ShouldWriteObject(o)).Select(delegate(AnalyticsAIMetadata obj)
				{
					JsonObject jsonObject9 = new JsonObject();
					JsonObjectTreeWriter.WriteAnalyticsAIMetadata(obj, jsonObject9, settings);
					return jsonObject9.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array10 = array;
				result["analyticsAIMetadata", TomPropCategory.ChildCollection, 51, false] = array10;
			}
			if (settings.ShouldWriteCollection(metadataObj.Functions))
			{
				object[] array = metadataObj.Functions.Where((Function o) => settings.ShouldWriteObject(o)).Select(delegate(Function obj)
				{
					JsonObject jsonObject10 = new JsonObject();
					JsonObjectTreeWriter.WriteFunction(obj, jsonObject10, settings);
					return jsonObject10.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array11 = array;
				result["functions", TomPropCategory.ChildCollection, 62, false] = array11;
			}
			if (settings.ShouldWriteCollection(metadataObj.BindingInfoCollection))
			{
				object[] array = metadataObj.BindingInfoCollection.Where((BindingInfo o) => settings.ShouldWriteObject(o)).Select(delegate(BindingInfo obj)
				{
					JsonObject jsonObject11 = new JsonObject();
					JsonObjectTreeWriter.WriteBindingInfo(obj, jsonObject11, settings);
					return jsonObject11.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array12 = array;
				result["bindingInfoCollection", TomPropCategory.ChildCollection, 63, false] = array12;
			}
			if (settings.ShouldWriteCollection(metadataObj.Annotations))
			{
				object[] array = metadataObj.Annotations.Where((Annotation o) => settings.ShouldWriteObject(o)).Select(delegate(Annotation obj)
				{
					JsonObject jsonObject12 = new JsonObject();
					JsonObjectTreeWriter.WriteAnnotation(obj, jsonObject12, settings);
					return jsonObject12.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array13 = array;
				result["annotations", TomPropCategory.ChildCollection, 1000, false] = array13;
			}
			if (settings.ShouldWriteCollection(metadataObj.ExtendedProperties))
			{
				object[] array = metadataObj.ExtendedProperties.Where((ExtendedProperty o) => settings.ShouldWriteObject(o)).Select(delegate(ExtendedProperty obj)
				{
					JsonObject jsonObject13 = new JsonObject();
					JsonObjectTreeWriter.WriteExtendedProperty(obj, jsonObject13, settings);
					return jsonObject13.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array14 = array;
				result["extendedProperties", TomPropCategory.ChildCollection, 39, false] = array14;
			}
			if (settings.ShouldWriteCollection(metadataObj.ExcludedArtifacts))
			{
				object[] array = metadataObj.ExcludedArtifacts.Where((ExcludedArtifact o) => settings.ShouldWriteObject(o)).Select(delegate(ExcludedArtifact obj)
				{
					JsonObject jsonObject14 = new JsonObject();
					JsonObjectTreeWriter.WriteExcludedArtifact(obj, jsonObject14, settings);
					return jsonObject14.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array15 = array;
				result["excludedArtifacts", TomPropCategory.ChildCollection, 53, false] = array15;
			}
		}

		// Token: 0x06001AEB RID: 6891 RVA: 0x000B5364 File Offset: 0x000B3564
		public static void WriteDataSource(DataSource metadataObj, JsonObject result, JsonObjectTreeWriterSettings settings)
		{
			result["name", TomPropCategory.Name, 0, false] = metadataObj.Name;
			if (settings.ShouldWriteObject(metadataObj))
			{
				settings.WriteObject(metadataObj, result);
			}
			if (settings.ShouldWriteCollection(metadataObj.Annotations))
			{
				object[] array = metadataObj.Annotations.Where((Annotation o) => settings.ShouldWriteObject(o)).Select(delegate(Annotation obj)
				{
					JsonObject jsonObject = new JsonObject();
					JsonObjectTreeWriter.WriteAnnotation(obj, jsonObject, settings);
					return jsonObject.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array2 = array;
				result["annotations", TomPropCategory.ChildCollection, 1000, false] = array2;
			}
			if (settings.ShouldWriteCollection(metadataObj.ExtendedProperties))
			{
				object[] array = metadataObj.ExtendedProperties.Where((ExtendedProperty o) => settings.ShouldWriteObject(o)).Select(delegate(ExtendedProperty obj)
				{
					JsonObject jsonObject2 = new JsonObject();
					JsonObjectTreeWriter.WriteExtendedProperty(obj, jsonObject2, settings);
					return jsonObject2.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array3 = array;
				result["extendedProperties", TomPropCategory.ChildCollection, 39, false] = array3;
			}
		}

		// Token: 0x06001AEC RID: 6892 RVA: 0x000B5458 File Offset: 0x000B3658
		public static void WriteTable(Table metadataObj, JsonObject result, JsonObjectTreeWriterSettings settings)
		{
			result["name", TomPropCategory.Name, 0, false] = metadataObj.Name;
			if (settings.ShouldWriteObject(metadataObj))
			{
				settings.WriteObject(metadataObj, result);
			}
			if (metadataObj.body.DefaultDetailRowsDefinitionID.Object != null && settings.ShouldWriteObject(metadataObj.body.DefaultDetailRowsDefinitionID.Object))
			{
				JsonObject jsonObject = new JsonObject();
				JsonObjectTreeWriter.WriteDetailRowsDefinition(metadataObj.body.DefaultDetailRowsDefinitionID.Object, jsonObject, settings);
				result["defaultDetailRowsDefinition", TomPropCategory.ChildLink, 12, false] = jsonObject.ToDictObject();
			}
			if (metadataObj.body.RefreshPolicyID.Object != null && settings.ShouldWriteObject(metadataObj.body.RefreshPolicyID.Object))
			{
				JsonObject jsonObject2 = new JsonObject();
				JsonObjectTreeWriter.WriteRefreshPolicy(metadataObj.body.RefreshPolicyID.Object, jsonObject2, settings);
				result["refreshPolicy", TomPropCategory.ChildLink, 14, false] = jsonObject2.ToDictObject();
			}
			if (metadataObj.body.CalculationGroupID.Object != null && settings.ShouldWriteObject(metadataObj.body.CalculationGroupID.Object))
			{
				JsonObject jsonObject3 = new JsonObject();
				JsonObjectTreeWriter.WriteCalculationGroup(metadataObj.body.CalculationGroupID.Object, jsonObject3, settings);
				result["calculationGroup", TomPropCategory.ChildLink, 15, false] = jsonObject3.ToDictObject();
			}
			if (settings.ShouldWriteCollection(metadataObj.Columns))
			{
				object[] array = metadataObj.Columns.Where((Column o) => settings.ShouldWriteObject(o)).Select(delegate(Column obj)
				{
					JsonObject jsonObject4 = new JsonObject();
					JsonObjectTreeWriter.WriteColumn(obj, jsonObject4, settings);
					return jsonObject4.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array2 = array;
				result["columns", TomPropCategory.ChildCollection, 3, false] = array2;
			}
			if (settings.ShouldWriteCollection(metadataObj.Partitions))
			{
				object[] array = metadataObj.Partitions.Where((Partition o) => settings.ShouldWriteObject(o)).Select(delegate(Partition obj)
				{
					JsonObject jsonObject5 = new JsonObject();
					JsonObjectTreeWriter.WritePartition(obj, jsonObject5, settings);
					return jsonObject5.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array3 = array;
				result["partitions", TomPropCategory.ChildCollection, 5, false] = array3;
			}
			if (settings.ShouldWriteCollection(metadataObj.Measures))
			{
				object[] array = metadataObj.Measures.Where((Measure o) => settings.ShouldWriteObject(o)).Select(delegate(Measure obj)
				{
					JsonObject jsonObject6 = new JsonObject();
					JsonObjectTreeWriter.WriteMeasure(obj, jsonObject6, settings);
					return jsonObject6.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array4 = array;
				result["measures", TomPropCategory.ChildCollection, 7, false] = array4;
			}
			if (settings.ShouldWriteCollection(metadataObj.Hierarchies))
			{
				object[] array = metadataObj.Hierarchies.Where((Hierarchy o) => settings.ShouldWriteObject(o)).Select(delegate(Hierarchy obj)
				{
					JsonObject jsonObject7 = new JsonObject();
					JsonObjectTreeWriter.WriteHierarchy(obj, jsonObject7, settings);
					return jsonObject7.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array5 = array;
				result["hierarchies", TomPropCategory.ChildCollection, 8, false] = array5;
			}
			if (settings.ShouldWriteCollection(metadataObj.Sets))
			{
				object[] array = metadataObj.Sets.Where((Set o) => settings.ShouldWriteObject(o)).Select(delegate(Set obj)
				{
					JsonObject jsonObject8 = new JsonObject();
					JsonObjectTreeWriter.WriteSet(obj, jsonObject8, settings);
					return jsonObject8.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array6 = array;
				result["sets", TomPropCategory.ChildCollection, 37, false] = array6;
			}
			if (settings.ShouldWriteCollection(metadataObj.Annotations))
			{
				object[] array = metadataObj.Annotations.Where((Annotation o) => settings.ShouldWriteObject(o)).Select(delegate(Annotation obj)
				{
					JsonObject jsonObject9 = new JsonObject();
					JsonObjectTreeWriter.WriteAnnotation(obj, jsonObject9, settings);
					return jsonObject9.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array7 = array;
				result["annotations", TomPropCategory.ChildCollection, 1000, false] = array7;
			}
			if (settings.ShouldWriteCollection(metadataObj.ExtendedProperties))
			{
				object[] array = metadataObj.ExtendedProperties.Where((ExtendedProperty o) => settings.ShouldWriteObject(o)).Select(delegate(ExtendedProperty obj)
				{
					JsonObject jsonObject10 = new JsonObject();
					JsonObjectTreeWriter.WriteExtendedProperty(obj, jsonObject10, settings);
					return jsonObject10.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array8 = array;
				result["extendedProperties", TomPropCategory.ChildCollection, 39, false] = array8;
			}
			if (settings.ShouldWriteCollection(metadataObj.ExcludedArtifacts))
			{
				object[] array = metadataObj.ExcludedArtifacts.Where((ExcludedArtifact o) => settings.ShouldWriteObject(o)).Select(delegate(ExcludedArtifact obj)
				{
					JsonObject jsonObject11 = new JsonObject();
					JsonObjectTreeWriter.WriteExcludedArtifact(obj, jsonObject11, settings);
					return jsonObject11.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array9 = array;
				result["excludedArtifacts", TomPropCategory.ChildCollection, 53, false] = array9;
			}
			if (settings.ShouldWriteCollection(metadataObj.ChangedProperties))
			{
				object[] array = metadataObj.ChangedProperties.Where((ChangedProperty o) => settings.ShouldWriteObject(o)).Select(delegate(ChangedProperty obj)
				{
					JsonObject jsonObject12 = new JsonObject();
					JsonObjectTreeWriter.WriteChangedProperty(obj, jsonObject12, settings);
					return jsonObject12.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array10 = array;
				result["changedProperties", TomPropCategory.ChildCollection, 52, false] = array10;
			}
			if (settings.ShouldWriteCollection(metadataObj.Calendars))
			{
				object[] array = metadataObj.Calendars.Where((Calendar o) => settings.ShouldWriteObject(o)).Select(delegate(Calendar obj)
				{
					JsonObject jsonObject13 = new JsonObject();
					JsonObjectTreeWriter.WriteCalendar(obj, jsonObject13, settings);
					return jsonObject13.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array11 = array;
				result["calendars", TomPropCategory.ChildCollection, 59, false] = array11;
			}
		}

		// Token: 0x06001AED RID: 6893 RVA: 0x000B5938 File Offset: 0x000B3B38
		public static void WriteColumn(Column metadataObj, JsonObject result, JsonObjectTreeWriterSettings settings)
		{
			result["name", TomPropCategory.Name, 0, false] = metadataObj.Name;
			if (settings.ShouldWriteObject(metadataObj))
			{
				settings.WriteObject(metadataObj, result);
			}
			if (metadataObj.body.AttributeHierarchyID.Object != null && settings.ShouldWriteObject(metadataObj.body.AttributeHierarchyID.Object))
			{
				JsonObject jsonObject = new JsonObject();
				JsonObjectTreeWriter.WriteAttributeHierarchy(metadataObj.body.AttributeHierarchyID.Object, jsonObject, settings);
				result["attributeHierarchy", TomPropCategory.ChildLink, 28, false] = jsonObject.ToDictObject();
			}
			if (metadataObj.body.RelatedColumnDetailsID.Object != null && settings.ShouldWriteObject(metadataObj.body.RelatedColumnDetailsID.Object))
			{
				JsonObject jsonObject2 = new JsonObject();
				JsonObjectTreeWriter.WriteRelatedColumnDetails(metadataObj.body.RelatedColumnDetailsID.Object, jsonObject2, settings);
				result["relatedColumnDetails", TomPropCategory.ChildLink, 39, false] = jsonObject2.ToDictObject();
			}
			if (metadataObj.body.AlternateOfID.Object != null && settings.ShouldWriteObject(metadataObj.body.AlternateOfID.Object))
			{
				JsonObject jsonObject3 = new JsonObject();
				JsonObjectTreeWriter.WriteAlternateOf(metadataObj.body.AlternateOfID.Object, jsonObject3, settings);
				result["alternateOf", TomPropCategory.ChildLink, 40, false] = jsonObject3.ToDictObject();
			}
			if (settings.ShouldWriteCollection(metadataObj.Variations))
			{
				object[] array = metadataObj.Variations.Where((Variation o) => settings.ShouldWriteObject(o)).Select(delegate(Variation obj)
				{
					JsonObject jsonObject4 = new JsonObject();
					JsonObjectTreeWriter.WriteVariation(obj, jsonObject4, settings);
					return jsonObject4.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array2 = array;
				result["variations", TomPropCategory.ChildCollection, 36, false] = array2;
			}
			if (settings.ShouldWriteCollection(metadataObj.Annotations))
			{
				object[] array = metadataObj.Annotations.Where((Annotation o) => settings.ShouldWriteObject(o)).Select(delegate(Annotation obj)
				{
					JsonObject jsonObject5 = new JsonObject();
					JsonObjectTreeWriter.WriteAnnotation(obj, jsonObject5, settings);
					return jsonObject5.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array3 = array;
				result["annotations", TomPropCategory.ChildCollection, 1000, false] = array3;
			}
			if (settings.ShouldWriteCollection(metadataObj.ExtendedProperties))
			{
				object[] array = metadataObj.ExtendedProperties.Where((ExtendedProperty o) => settings.ShouldWriteObject(o)).Select(delegate(ExtendedProperty obj)
				{
					JsonObject jsonObject6 = new JsonObject();
					JsonObjectTreeWriter.WriteExtendedProperty(obj, jsonObject6, settings);
					return jsonObject6.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array4 = array;
				result["extendedProperties", TomPropCategory.ChildCollection, 39, false] = array4;
			}
			if (settings.ShouldWriteCollection(metadataObj.ChangedProperties))
			{
				object[] array = metadataObj.ChangedProperties.Where((ChangedProperty o) => settings.ShouldWriteObject(o)).Select(delegate(ChangedProperty obj)
				{
					JsonObject jsonObject7 = new JsonObject();
					JsonObjectTreeWriter.WriteChangedProperty(obj, jsonObject7, settings);
					return jsonObject7.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array5 = array;
				result["changedProperties", TomPropCategory.ChildCollection, 52, false] = array5;
			}
		}

		// Token: 0x06001AEE RID: 6894 RVA: 0x000B5C14 File Offset: 0x000B3E14
		public static void WriteAttributeHierarchy(AttributeHierarchy metadataObj, JsonObject result, JsonObjectTreeWriterSettings settings)
		{
			if (settings.ShouldWriteObject(metadataObj))
			{
				settings.WriteObject(metadataObj, result);
			}
			if (settings.ShouldWriteCollection(metadataObj.Annotations))
			{
				object[] array = metadataObj.Annotations.Where((Annotation o) => settings.ShouldWriteObject(o)).Select(delegate(Annotation obj)
				{
					JsonObject jsonObject = new JsonObject();
					JsonObjectTreeWriter.WriteAnnotation(obj, jsonObject, settings);
					return jsonObject.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array2 = array;
				result["annotations", TomPropCategory.ChildCollection, 1000, false] = array2;
			}
			if (settings.ShouldWriteCollection(metadataObj.ExtendedProperties))
			{
				object[] array = metadataObj.ExtendedProperties.Where((ExtendedProperty o) => settings.ShouldWriteObject(o)).Select(delegate(ExtendedProperty obj)
				{
					JsonObject jsonObject2 = new JsonObject();
					JsonObjectTreeWriter.WriteExtendedProperty(obj, jsonObject2, settings);
					return jsonObject2.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array3 = array;
				result["extendedProperties", TomPropCategory.ChildCollection, 39, false] = array3;
			}
		}

		// Token: 0x06001AEF RID: 6895 RVA: 0x000B5CF4 File Offset: 0x000B3EF4
		public static void WritePartition(Partition metadataObj, JsonObject result, JsonObjectTreeWriterSettings settings)
		{
			result["name", TomPropCategory.Name, 0, false] = metadataObj.Name;
			if (settings.ShouldWriteObject(metadataObj))
			{
				settings.WriteObject(metadataObj, result);
			}
			if (metadataObj.body.DataCoverageDefinitionID.Object != null && settings.ShouldWriteObject(metadataObj.body.DataCoverageDefinitionID.Object))
			{
				JsonObject jsonObject = new JsonObject();
				JsonObjectTreeWriter.WriteDataCoverageDefinition(metadataObj.body.DataCoverageDefinitionID.Object, jsonObject, settings);
				result["dataCoverageDefinition", TomPropCategory.ChildLink, 23, false] = jsonObject.ToDictObject();
			}
			if (settings.ShouldWriteCollection(metadataObj.Annotations))
			{
				object[] array = metadataObj.Annotations.Where((Annotation o) => settings.ShouldWriteObject(o)).Select(delegate(Annotation obj)
				{
					JsonObject jsonObject2 = new JsonObject();
					JsonObjectTreeWriter.WriteAnnotation(obj, jsonObject2, settings);
					return jsonObject2.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array2 = array;
				result["annotations", TomPropCategory.ChildCollection, 1000, false] = array2;
			}
			if (settings.ShouldWriteCollection(metadataObj.ExtendedProperties))
			{
				object[] array = metadataObj.ExtendedProperties.Where((ExtendedProperty o) => settings.ShouldWriteObject(o)).Select(delegate(ExtendedProperty obj)
				{
					JsonObject jsonObject3 = new JsonObject();
					JsonObjectTreeWriter.WriteExtendedProperty(obj, jsonObject3, settings);
					return jsonObject3.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array3 = array;
				result["extendedProperties", TomPropCategory.ChildCollection, 39, false] = array3;
			}
		}

		// Token: 0x06001AF0 RID: 6896 RVA: 0x000B5E50 File Offset: 0x000B4050
		public static void WriteRelationship(Relationship metadataObj, JsonObject result, JsonObjectTreeWriterSettings settings)
		{
			result["name", TomPropCategory.Name, 0, false] = metadataObj.Name;
			if (settings.ShouldWriteObject(metadataObj))
			{
				settings.WriteObject(metadataObj, result);
			}
			if (settings.ShouldWriteCollection(metadataObj.Annotations))
			{
				object[] array = metadataObj.Annotations.Where((Annotation o) => settings.ShouldWriteObject(o)).Select(delegate(Annotation obj)
				{
					JsonObject jsonObject = new JsonObject();
					JsonObjectTreeWriter.WriteAnnotation(obj, jsonObject, settings);
					return jsonObject.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array2 = array;
				result["annotations", TomPropCategory.ChildCollection, 1000, false] = array2;
			}
			if (settings.ShouldWriteCollection(metadataObj.ExtendedProperties))
			{
				object[] array = metadataObj.ExtendedProperties.Where((ExtendedProperty o) => settings.ShouldWriteObject(o)).Select(delegate(ExtendedProperty obj)
				{
					JsonObject jsonObject2 = new JsonObject();
					JsonObjectTreeWriter.WriteExtendedProperty(obj, jsonObject2, settings);
					return jsonObject2.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array3 = array;
				result["extendedProperties", TomPropCategory.ChildCollection, 39, false] = array3;
			}
			if (settings.ShouldWriteCollection(metadataObj.ChangedProperties))
			{
				object[] array = metadataObj.ChangedProperties.Where((ChangedProperty o) => settings.ShouldWriteObject(o)).Select(delegate(ChangedProperty obj)
				{
					JsonObject jsonObject3 = new JsonObject();
					JsonObjectTreeWriter.WriteChangedProperty(obj, jsonObject3, settings);
					return jsonObject3.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array4 = array;
				result["changedProperties", TomPropCategory.ChildCollection, 52, false] = array4;
			}
		}

		// Token: 0x06001AF1 RID: 6897 RVA: 0x000B5F98 File Offset: 0x000B4198
		public static void WriteMeasure(Measure metadataObj, JsonObject result, JsonObjectTreeWriterSettings settings)
		{
			result["name", TomPropCategory.Name, 0, false] = metadataObj.Name;
			if (settings.ShouldWriteObject(metadataObj))
			{
				settings.WriteObject(metadataObj, result);
			}
			if (metadataObj.body.KPIID.Object != null && settings.ShouldWriteObject(metadataObj.body.KPIID.Object))
			{
				JsonObject jsonObject = new JsonObject();
				JsonObjectTreeWriter.WriteKPI(metadataObj.body.KPIID.Object, jsonObject, settings);
				result["kpi", TomPropCategory.ChildLink, 11, false] = jsonObject.ToDictObject();
			}
			if (metadataObj.body.DetailRowsDefinitionID.Object != null && settings.ShouldWriteObject(metadataObj.body.DetailRowsDefinitionID.Object))
			{
				JsonObject jsonObject2 = new JsonObject();
				JsonObjectTreeWriter.WriteDetailRowsDefinition(metadataObj.body.DetailRowsDefinitionID.Object, jsonObject2, settings);
				result["detailRowsDefinition", TomPropCategory.ChildLink, 15, false] = jsonObject2.ToDictObject();
			}
			if (metadataObj.body.FormatStringDefinitionID.Object != null && settings.ShouldWriteObject(metadataObj.body.FormatStringDefinitionID.Object))
			{
				JsonObject jsonObject3 = new JsonObject();
				JsonObjectTreeWriter.WriteFormatStringDefinition(metadataObj.body.FormatStringDefinitionID.Object, jsonObject3, settings);
				result["formatStringDefinition", TomPropCategory.ChildLink, 17, false] = jsonObject3.ToDictObject();
			}
			if (settings.ShouldWriteCollection(metadataObj.Annotations))
			{
				object[] array = metadataObj.Annotations.Where((Annotation o) => settings.ShouldWriteObject(o)).Select(delegate(Annotation obj)
				{
					JsonObject jsonObject4 = new JsonObject();
					JsonObjectTreeWriter.WriteAnnotation(obj, jsonObject4, settings);
					return jsonObject4.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array2 = array;
				result["annotations", TomPropCategory.ChildCollection, 1000, false] = array2;
			}
			if (settings.ShouldWriteCollection(metadataObj.ExtendedProperties))
			{
				object[] array = metadataObj.ExtendedProperties.Where((ExtendedProperty o) => settings.ShouldWriteObject(o)).Select(delegate(ExtendedProperty obj)
				{
					JsonObject jsonObject5 = new JsonObject();
					JsonObjectTreeWriter.WriteExtendedProperty(obj, jsonObject5, settings);
					return jsonObject5.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array3 = array;
				result["extendedProperties", TomPropCategory.ChildCollection, 39, false] = array3;
			}
			if (settings.ShouldWriteCollection(metadataObj.ChangedProperties))
			{
				object[] array = metadataObj.ChangedProperties.Where((ChangedProperty o) => settings.ShouldWriteObject(o)).Select(delegate(ChangedProperty obj)
				{
					JsonObject jsonObject6 = new JsonObject();
					JsonObjectTreeWriter.WriteChangedProperty(obj, jsonObject6, settings);
					return jsonObject6.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array4 = array;
				result["changedProperties", TomPropCategory.ChildCollection, 52, false] = array4;
			}
		}

		// Token: 0x06001AF2 RID: 6898 RVA: 0x000B621C File Offset: 0x000B441C
		public static void WriteHierarchy(Hierarchy metadataObj, JsonObject result, JsonObjectTreeWriterSettings settings)
		{
			result["name", TomPropCategory.Name, 0, false] = metadataObj.Name;
			if (settings.ShouldWriteObject(metadataObj))
			{
				settings.WriteObject(metadataObj, result);
			}
			if (settings.ShouldWriteCollection(metadataObj.Levels))
			{
				object[] array = metadataObj.Levels.Where((Level o) => settings.ShouldWriteObject(o)).Select(delegate(Level obj)
				{
					JsonObject jsonObject = new JsonObject();
					JsonObjectTreeWriter.WriteLevel(obj, jsonObject, settings);
					return jsonObject.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array2 = array;
				result["levels", TomPropCategory.ChildCollection, 9, false] = array2;
			}
			if (settings.ShouldWriteCollection(metadataObj.Annotations))
			{
				object[] array = metadataObj.Annotations.Where((Annotation o) => settings.ShouldWriteObject(o)).Select(delegate(Annotation obj)
				{
					JsonObject jsonObject2 = new JsonObject();
					JsonObjectTreeWriter.WriteAnnotation(obj, jsonObject2, settings);
					return jsonObject2.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array3 = array;
				result["annotations", TomPropCategory.ChildCollection, 1000, false] = array3;
			}
			if (settings.ShouldWriteCollection(metadataObj.ExtendedProperties))
			{
				object[] array = metadataObj.ExtendedProperties.Where((ExtendedProperty o) => settings.ShouldWriteObject(o)).Select(delegate(ExtendedProperty obj)
				{
					JsonObject jsonObject3 = new JsonObject();
					JsonObjectTreeWriter.WriteExtendedProperty(obj, jsonObject3, settings);
					return jsonObject3.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array4 = array;
				result["extendedProperties", TomPropCategory.ChildCollection, 39, false] = array4;
			}
			if (settings.ShouldWriteCollection(metadataObj.ExcludedArtifacts))
			{
				object[] array = metadataObj.ExcludedArtifacts.Where((ExcludedArtifact o) => settings.ShouldWriteObject(o)).Select(delegate(ExcludedArtifact obj)
				{
					JsonObject jsonObject4 = new JsonObject();
					JsonObjectTreeWriter.WriteExcludedArtifact(obj, jsonObject4, settings);
					return jsonObject4.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array5 = array;
				result["excludedArtifacts", TomPropCategory.ChildCollection, 53, false] = array5;
			}
			if (settings.ShouldWriteCollection(metadataObj.ChangedProperties))
			{
				object[] array = metadataObj.ChangedProperties.Where((ChangedProperty o) => settings.ShouldWriteObject(o)).Select(delegate(ChangedProperty obj)
				{
					JsonObject jsonObject5 = new JsonObject();
					JsonObjectTreeWriter.WriteChangedProperty(obj, jsonObject5, settings);
					return jsonObject5.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array6 = array;
				result["changedProperties", TomPropCategory.ChildCollection, 52, false] = array6;
			}
		}

		// Token: 0x06001AF3 RID: 6899 RVA: 0x000B6410 File Offset: 0x000B4610
		public static void WriteLevel(Level metadataObj, JsonObject result, JsonObjectTreeWriterSettings settings)
		{
			result["name", TomPropCategory.Name, 0, false] = metadataObj.Name;
			if (settings.ShouldWriteObject(metadataObj))
			{
				settings.WriteObject(metadataObj, result);
			}
			if (settings.ShouldWriteCollection(metadataObj.Annotations))
			{
				object[] array = metadataObj.Annotations.Where((Annotation o) => settings.ShouldWriteObject(o)).Select(delegate(Annotation obj)
				{
					JsonObject jsonObject = new JsonObject();
					JsonObjectTreeWriter.WriteAnnotation(obj, jsonObject, settings);
					return jsonObject.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array2 = array;
				result["annotations", TomPropCategory.ChildCollection, 1000, false] = array2;
			}
			if (settings.ShouldWriteCollection(metadataObj.ExtendedProperties))
			{
				object[] array = metadataObj.ExtendedProperties.Where((ExtendedProperty o) => settings.ShouldWriteObject(o)).Select(delegate(ExtendedProperty obj)
				{
					JsonObject jsonObject2 = new JsonObject();
					JsonObjectTreeWriter.WriteExtendedProperty(obj, jsonObject2, settings);
					return jsonObject2.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array3 = array;
				result["extendedProperties", TomPropCategory.ChildCollection, 39, false] = array3;
			}
			if (settings.ShouldWriteCollection(metadataObj.ChangedProperties))
			{
				object[] array = metadataObj.ChangedProperties.Where((ChangedProperty o) => settings.ShouldWriteObject(o)).Select(delegate(ChangedProperty obj)
				{
					JsonObject jsonObject3 = new JsonObject();
					JsonObjectTreeWriter.WriteChangedProperty(obj, jsonObject3, settings);
					return jsonObject3.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array4 = array;
				result["changedProperties", TomPropCategory.ChildCollection, 52, false] = array4;
			}
		}

		// Token: 0x06001AF4 RID: 6900 RVA: 0x000B6557 File Offset: 0x000B4757
		public static void WriteAnnotation(Annotation metadataObj, JsonObject result, JsonObjectTreeWriterSettings settings)
		{
			result["name", TomPropCategory.Name, 0, false] = metadataObj.Name;
			if (settings.ShouldWriteObject(metadataObj))
			{
				settings.WriteObject(metadataObj, result);
			}
		}

		// Token: 0x06001AF5 RID: 6901 RVA: 0x000B6580 File Offset: 0x000B4780
		public static void WriteKPI(KPI metadataObj, JsonObject result, JsonObjectTreeWriterSettings settings)
		{
			if (settings.ShouldWriteObject(metadataObj))
			{
				settings.WriteObject(metadataObj, result);
			}
			if (settings.ShouldWriteCollection(metadataObj.Annotations))
			{
				object[] array = metadataObj.Annotations.Where((Annotation o) => settings.ShouldWriteObject(o)).Select(delegate(Annotation obj)
				{
					JsonObject jsonObject = new JsonObject();
					JsonObjectTreeWriter.WriteAnnotation(obj, jsonObject, settings);
					return jsonObject.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array2 = array;
				result["annotations", TomPropCategory.ChildCollection, 1000, false] = array2;
			}
			if (settings.ShouldWriteCollection(metadataObj.ExtendedProperties))
			{
				object[] array = metadataObj.ExtendedProperties.Where((ExtendedProperty o) => settings.ShouldWriteObject(o)).Select(delegate(ExtendedProperty obj)
				{
					JsonObject jsonObject2 = new JsonObject();
					JsonObjectTreeWriter.WriteExtendedProperty(obj, jsonObject2, settings);
					return jsonObject2.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array3 = array;
				result["extendedProperties", TomPropCategory.ChildCollection, 39, false] = array3;
			}
		}

		// Token: 0x06001AF6 RID: 6902 RVA: 0x000B6660 File Offset: 0x000B4860
		public static void WriteCulture(Culture metadataObj, JsonObject result, JsonObjectTreeWriterSettings settings)
		{
			result["name", TomPropCategory.Name, 0, false] = metadataObj.Name;
			if (settings.ShouldWriteObject(metadataObj))
			{
				settings.WriteObject(metadataObj, result);
			}
			if (metadataObj.body.LinguisticMetadataID.Object != null && settings.ShouldWriteObject(metadataObj.body.LinguisticMetadataID.Object))
			{
				JsonObject jsonObject = new JsonObject();
				JsonObjectTreeWriter.WriteLinguisticMetadata(metadataObj.body.LinguisticMetadataID.Object, jsonObject, settings);
				result["linguisticMetadata", TomPropCategory.ChildLink, 3, false] = jsonObject.ToDictObject();
			}
			if (settings.ShouldWriteCollection(metadataObj.Annotations))
			{
				object[] array = metadataObj.Annotations.Where((Annotation o) => settings.ShouldWriteObject(o)).Select(delegate(Annotation obj)
				{
					JsonObject jsonObject2 = new JsonObject();
					JsonObjectTreeWriter.WriteAnnotation(obj, jsonObject2, settings);
					return jsonObject2.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array2 = array;
				result["annotations", TomPropCategory.ChildCollection, 1000, false] = array2;
			}
			if (settings.ShouldWriteCollection(metadataObj.ExtendedProperties))
			{
				object[] array = metadataObj.ExtendedProperties.Where((ExtendedProperty o) => settings.ShouldWriteObject(o)).Select(delegate(ExtendedProperty obj)
				{
					JsonObject jsonObject3 = new JsonObject();
					JsonObjectTreeWriter.WriteExtendedProperty(obj, jsonObject3, settings);
					return jsonObject3.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array3 = array;
				result["extendedProperties", TomPropCategory.ChildCollection, 39, false] = array3;
			}
		}

		// Token: 0x06001AF7 RID: 6903 RVA: 0x000B67BC File Offset: 0x000B49BC
		public static void WriteLinguisticMetadata(LinguisticMetadata metadataObj, JsonObject result, JsonObjectTreeWriterSettings settings)
		{
			if (settings.ShouldWriteObject(metadataObj))
			{
				settings.WriteObject(metadataObj, result);
			}
			if (settings.ShouldWriteCollection(metadataObj.Annotations))
			{
				object[] array = metadataObj.Annotations.Where((Annotation o) => settings.ShouldWriteObject(o)).Select(delegate(Annotation obj)
				{
					JsonObject jsonObject = new JsonObject();
					JsonObjectTreeWriter.WriteAnnotation(obj, jsonObject, settings);
					return jsonObject.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array2 = array;
				result["annotations", TomPropCategory.ChildCollection, 1000, false] = array2;
			}
			if (settings.ShouldWriteCollection(metadataObj.ExtendedProperties))
			{
				object[] array = metadataObj.ExtendedProperties.Where((ExtendedProperty o) => settings.ShouldWriteObject(o)).Select(delegate(ExtendedProperty obj)
				{
					JsonObject jsonObject2 = new JsonObject();
					JsonObjectTreeWriter.WriteExtendedProperty(obj, jsonObject2, settings);
					return jsonObject2.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array3 = array;
				result["extendedProperties", TomPropCategory.ChildCollection, 39, false] = array3;
			}
		}

		// Token: 0x06001AF8 RID: 6904 RVA: 0x000B689C File Offset: 0x000B4A9C
		public static void WritePerspective(Perspective metadataObj, JsonObject result, JsonObjectTreeWriterSettings settings)
		{
			result["name", TomPropCategory.Name, 0, false] = metadataObj.Name;
			if (settings.ShouldWriteObject(metadataObj))
			{
				settings.WriteObject(metadataObj, result);
			}
			if (settings.ShouldWriteCollection(metadataObj.PerspectiveTables))
			{
				object[] array = metadataObj.PerspectiveTables.Where((PerspectiveTable o) => settings.ShouldWriteObject(o)).Select(delegate(PerspectiveTable obj)
				{
					JsonObject jsonObject = new JsonObject();
					JsonObjectTreeWriter.WritePerspectiveTable(obj, jsonObject, settings);
					return jsonObject.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array2 = array;
				result["tables", TomPropCategory.ChildCollection, 29, false] = array2;
			}
			if (settings.ShouldWriteCollection(metadataObj.Annotations))
			{
				object[] array = metadataObj.Annotations.Where((Annotation o) => settings.ShouldWriteObject(o)).Select(delegate(Annotation obj)
				{
					JsonObject jsonObject2 = new JsonObject();
					JsonObjectTreeWriter.WriteAnnotation(obj, jsonObject2, settings);
					return jsonObject2.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array3 = array;
				result["annotations", TomPropCategory.ChildCollection, 1000, false] = array3;
			}
			if (settings.ShouldWriteCollection(metadataObj.ExtendedProperties))
			{
				object[] array = metadataObj.ExtendedProperties.Where((ExtendedProperty o) => settings.ShouldWriteObject(o)).Select(delegate(ExtendedProperty obj)
				{
					JsonObject jsonObject3 = new JsonObject();
					JsonObjectTreeWriter.WriteExtendedProperty(obj, jsonObject3, settings);
					return jsonObject3.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array4 = array;
				result["extendedProperties", TomPropCategory.ChildCollection, 39, false] = array4;
			}
		}

		// Token: 0x06001AF9 RID: 6905 RVA: 0x000B69E4 File Offset: 0x000B4BE4
		public static void WritePerspectiveTable(PerspectiveTable metadataObj, JsonObject result, JsonObjectTreeWriterSettings settings)
		{
			result["name", TomPropCategory.Name, 0, false] = metadataObj.Name;
			if (settings.ShouldWriteObject(metadataObj))
			{
				settings.WriteObject(metadataObj, result);
			}
			if (settings.ShouldWriteCollection(metadataObj.PerspectiveColumns))
			{
				object[] array = metadataObj.PerspectiveColumns.Where((PerspectiveColumn o) => settings.ShouldWriteObject(o)).Select(delegate(PerspectiveColumn obj)
				{
					JsonObject jsonObject = new JsonObject();
					JsonObjectTreeWriter.WritePerspectiveColumn(obj, jsonObject, settings);
					return jsonObject.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array2 = array;
				result["columns", TomPropCategory.ChildCollection, 30, false] = array2;
			}
			if (settings.ShouldWriteCollection(metadataObj.PerspectiveMeasures))
			{
				object[] array = metadataObj.PerspectiveMeasures.Where((PerspectiveMeasure o) => settings.ShouldWriteObject(o)).Select(delegate(PerspectiveMeasure obj)
				{
					JsonObject jsonObject2 = new JsonObject();
					JsonObjectTreeWriter.WritePerspectiveMeasure(obj, jsonObject2, settings);
					return jsonObject2.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array3 = array;
				result["measures", TomPropCategory.ChildCollection, 32, false] = array3;
			}
			if (settings.ShouldWriteCollection(metadataObj.PerspectiveHierarchies))
			{
				object[] array = metadataObj.PerspectiveHierarchies.Where((PerspectiveHierarchy o) => settings.ShouldWriteObject(o)).Select(delegate(PerspectiveHierarchy obj)
				{
					JsonObject jsonObject3 = new JsonObject();
					JsonObjectTreeWriter.WritePerspectiveHierarchy(obj, jsonObject3, settings);
					return jsonObject3.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array4 = array;
				result["hierarchies", TomPropCategory.ChildCollection, 31, false] = array4;
			}
			if (settings.ShouldWriteCollection(metadataObj.PerspectiveSets))
			{
				object[] array = metadataObj.PerspectiveSets.Where((PerspectiveSet o) => settings.ShouldWriteObject(o)).Select(delegate(PerspectiveSet obj)
				{
					JsonObject jsonObject4 = new JsonObject();
					JsonObjectTreeWriter.WritePerspectiveSet(obj, jsonObject4, settings);
					return jsonObject4.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array5 = array;
				result["sets", TomPropCategory.ChildCollection, 38, false] = array5;
			}
			if (settings.ShouldWriteCollection(metadataObj.Annotations))
			{
				object[] array = metadataObj.Annotations.Where((Annotation o) => settings.ShouldWriteObject(o)).Select(delegate(Annotation obj)
				{
					JsonObject jsonObject5 = new JsonObject();
					JsonObjectTreeWriter.WriteAnnotation(obj, jsonObject5, settings);
					return jsonObject5.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array6 = array;
				result["annotations", TomPropCategory.ChildCollection, 1000, false] = array6;
			}
			if (settings.ShouldWriteCollection(metadataObj.ExtendedProperties))
			{
				object[] array = metadataObj.ExtendedProperties.Where((ExtendedProperty o) => settings.ShouldWriteObject(o)).Select(delegate(ExtendedProperty obj)
				{
					JsonObject jsonObject6 = new JsonObject();
					JsonObjectTreeWriter.WriteExtendedProperty(obj, jsonObject6, settings);
					return jsonObject6.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array7 = array;
				result["extendedProperties", TomPropCategory.ChildCollection, 39, false] = array7;
			}
		}

		// Token: 0x06001AFA RID: 6906 RVA: 0x000B6C2C File Offset: 0x000B4E2C
		public static void WritePerspectiveColumn(PerspectiveColumn metadataObj, JsonObject result, JsonObjectTreeWriterSettings settings)
		{
			result["name", TomPropCategory.Name, 0, false] = metadataObj.Name;
			if (settings.ShouldWriteObject(metadataObj))
			{
				settings.WriteObject(metadataObj, result);
			}
			if (settings.ShouldWriteCollection(metadataObj.Annotations))
			{
				object[] array = metadataObj.Annotations.Where((Annotation o) => settings.ShouldWriteObject(o)).Select(delegate(Annotation obj)
				{
					JsonObject jsonObject = new JsonObject();
					JsonObjectTreeWriter.WriteAnnotation(obj, jsonObject, settings);
					return jsonObject.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array2 = array;
				result["annotations", TomPropCategory.ChildCollection, 1000, false] = array2;
			}
			if (settings.ShouldWriteCollection(metadataObj.ExtendedProperties))
			{
				object[] array = metadataObj.ExtendedProperties.Where((ExtendedProperty o) => settings.ShouldWriteObject(o)).Select(delegate(ExtendedProperty obj)
				{
					JsonObject jsonObject2 = new JsonObject();
					JsonObjectTreeWriter.WriteExtendedProperty(obj, jsonObject2, settings);
					return jsonObject2.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array3 = array;
				result["extendedProperties", TomPropCategory.ChildCollection, 39, false] = array3;
			}
		}

		// Token: 0x06001AFB RID: 6907 RVA: 0x000B6D20 File Offset: 0x000B4F20
		public static void WritePerspectiveHierarchy(PerspectiveHierarchy metadataObj, JsonObject result, JsonObjectTreeWriterSettings settings)
		{
			result["name", TomPropCategory.Name, 0, false] = metadataObj.Name;
			if (settings.ShouldWriteObject(metadataObj))
			{
				settings.WriteObject(metadataObj, result);
			}
			if (settings.ShouldWriteCollection(metadataObj.Annotations))
			{
				object[] array = metadataObj.Annotations.Where((Annotation o) => settings.ShouldWriteObject(o)).Select(delegate(Annotation obj)
				{
					JsonObject jsonObject = new JsonObject();
					JsonObjectTreeWriter.WriteAnnotation(obj, jsonObject, settings);
					return jsonObject.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array2 = array;
				result["annotations", TomPropCategory.ChildCollection, 1000, false] = array2;
			}
			if (settings.ShouldWriteCollection(metadataObj.ExtendedProperties))
			{
				object[] array = metadataObj.ExtendedProperties.Where((ExtendedProperty o) => settings.ShouldWriteObject(o)).Select(delegate(ExtendedProperty obj)
				{
					JsonObject jsonObject2 = new JsonObject();
					JsonObjectTreeWriter.WriteExtendedProperty(obj, jsonObject2, settings);
					return jsonObject2.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array3 = array;
				result["extendedProperties", TomPropCategory.ChildCollection, 39, false] = array3;
			}
		}

		// Token: 0x06001AFC RID: 6908 RVA: 0x000B6E14 File Offset: 0x000B5014
		public static void WritePerspectiveMeasure(PerspectiveMeasure metadataObj, JsonObject result, JsonObjectTreeWriterSettings settings)
		{
			result["name", TomPropCategory.Name, 0, false] = metadataObj.Name;
			if (settings.ShouldWriteObject(metadataObj))
			{
				settings.WriteObject(metadataObj, result);
			}
			if (settings.ShouldWriteCollection(metadataObj.Annotations))
			{
				object[] array = metadataObj.Annotations.Where((Annotation o) => settings.ShouldWriteObject(o)).Select(delegate(Annotation obj)
				{
					JsonObject jsonObject = new JsonObject();
					JsonObjectTreeWriter.WriteAnnotation(obj, jsonObject, settings);
					return jsonObject.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array2 = array;
				result["annotations", TomPropCategory.ChildCollection, 1000, false] = array2;
			}
			if (settings.ShouldWriteCollection(metadataObj.ExtendedProperties))
			{
				object[] array = metadataObj.ExtendedProperties.Where((ExtendedProperty o) => settings.ShouldWriteObject(o)).Select(delegate(ExtendedProperty obj)
				{
					JsonObject jsonObject2 = new JsonObject();
					JsonObjectTreeWriter.WriteExtendedProperty(obj, jsonObject2, settings);
					return jsonObject2.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array3 = array;
				result["extendedProperties", TomPropCategory.ChildCollection, 39, false] = array3;
			}
		}

		// Token: 0x06001AFD RID: 6909 RVA: 0x000B6F08 File Offset: 0x000B5108
		public static void WriteModelRole(ModelRole metadataObj, JsonObject result, JsonObjectTreeWriterSettings settings)
		{
			result["name", TomPropCategory.Name, 0, false] = metadataObj.Name;
			if (settings.ShouldWriteObject(metadataObj))
			{
				settings.WriteObject(metadataObj, result);
			}
			if (settings.ShouldWriteCollection(metadataObj.Members))
			{
				object[] array = metadataObj.Members.Where((ModelRoleMember o) => settings.ShouldWriteObject(o)).Select(delegate(ModelRoleMember obj)
				{
					JsonObject jsonObject = new JsonObject();
					JsonObjectTreeWriter.WriteModelRoleMember(obj, jsonObject, settings);
					return jsonObject.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array2 = array;
				result["members", TomPropCategory.ChildCollection, 34, false] = array2;
			}
			if (settings.ShouldWriteCollection(metadataObj.TablePermissions))
			{
				object[] array = metadataObj.TablePermissions.Where((TablePermission o) => settings.ShouldWriteObject(o)).Select(delegate(TablePermission obj)
				{
					JsonObject jsonObject2 = new JsonObject();
					JsonObjectTreeWriter.WriteTablePermission(obj, jsonObject2, settings);
					return jsonObject2.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array3 = array;
				result["tablePermissions", TomPropCategory.ChildCollection, 35, false] = array3;
			}
			if (settings.ShouldWriteCollection(metadataObj.Annotations))
			{
				object[] array = metadataObj.Annotations.Where((Annotation o) => settings.ShouldWriteObject(o)).Select(delegate(Annotation obj)
				{
					JsonObject jsonObject3 = new JsonObject();
					JsonObjectTreeWriter.WriteAnnotation(obj, jsonObject3, settings);
					return jsonObject3.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array4 = array;
				result["annotations", TomPropCategory.ChildCollection, 1000, false] = array4;
			}
			if (settings.ShouldWriteCollection(metadataObj.ExtendedProperties))
			{
				object[] array = metadataObj.ExtendedProperties.Where((ExtendedProperty o) => settings.ShouldWriteObject(o)).Select(delegate(ExtendedProperty obj)
				{
					JsonObject jsonObject4 = new JsonObject();
					JsonObjectTreeWriter.WriteExtendedProperty(obj, jsonObject4, settings);
					return jsonObject4.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array5 = array;
				result["extendedProperties", TomPropCategory.ChildCollection, 39, false] = array5;
			}
		}

		// Token: 0x06001AFE RID: 6910 RVA: 0x000B70A4 File Offset: 0x000B52A4
		public static void WriteModelRoleMember(ModelRoleMember metadataObj, JsonObject result, JsonObjectTreeWriterSettings settings)
		{
			result["name", TomPropCategory.Name, 0, false] = metadataObj.Name;
			if (settings.ShouldWriteObject(metadataObj))
			{
				settings.WriteObject(metadataObj, result);
			}
			if (settings.ShouldWriteCollection(metadataObj.Annotations))
			{
				object[] array = metadataObj.Annotations.Where((Annotation o) => settings.ShouldWriteObject(o)).Select(delegate(Annotation obj)
				{
					JsonObject jsonObject = new JsonObject();
					JsonObjectTreeWriter.WriteAnnotation(obj, jsonObject, settings);
					return jsonObject.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array2 = array;
				result["annotations", TomPropCategory.ChildCollection, 1000, false] = array2;
			}
			if (settings.ShouldWriteCollection(metadataObj.ExtendedProperties))
			{
				object[] array = metadataObj.ExtendedProperties.Where((ExtendedProperty o) => settings.ShouldWriteObject(o)).Select(delegate(ExtendedProperty obj)
				{
					JsonObject jsonObject2 = new JsonObject();
					JsonObjectTreeWriter.WriteExtendedProperty(obj, jsonObject2, settings);
					return jsonObject2.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array3 = array;
				result["extendedProperties", TomPropCategory.ChildCollection, 39, false] = array3;
			}
		}

		// Token: 0x06001AFF RID: 6911 RVA: 0x000B7198 File Offset: 0x000B5398
		public static void WriteTablePermission(TablePermission metadataObj, JsonObject result, JsonObjectTreeWriterSettings settings)
		{
			result["name", TomPropCategory.Name, 0, false] = metadataObj.Name;
			if (settings.ShouldWriteObject(metadataObj))
			{
				settings.WriteObject(metadataObj, result);
			}
			if (settings.ShouldWriteCollection(metadataObj.Annotations))
			{
				object[] array = metadataObj.Annotations.Where((Annotation o) => settings.ShouldWriteObject(o)).Select(delegate(Annotation obj)
				{
					JsonObject jsonObject = new JsonObject();
					JsonObjectTreeWriter.WriteAnnotation(obj, jsonObject, settings);
					return jsonObject.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array2 = array;
				result["annotations", TomPropCategory.ChildCollection, 1000, false] = array2;
			}
			if (settings.ShouldWriteCollection(metadataObj.ExtendedProperties))
			{
				object[] array = metadataObj.ExtendedProperties.Where((ExtendedProperty o) => settings.ShouldWriteObject(o)).Select(delegate(ExtendedProperty obj)
				{
					JsonObject jsonObject2 = new JsonObject();
					JsonObjectTreeWriter.WriteExtendedProperty(obj, jsonObject2, settings);
					return jsonObject2.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array3 = array;
				result["extendedProperties", TomPropCategory.ChildCollection, 39, false] = array3;
			}
			if (settings.ShouldWriteCollection(metadataObj.ColumnPermissions))
			{
				object[] array = metadataObj.ColumnPermissions.Where((ColumnPermission o) => settings.ShouldWriteObject(o)).Select(delegate(ColumnPermission obj)
				{
					JsonObject jsonObject3 = new JsonObject();
					JsonObjectTreeWriter.WriteColumnPermission(obj, jsonObject3, settings);
					return jsonObject3.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array4 = array;
				result["columnPermissions", TomPropCategory.ChildCollection, 41, false] = array4;
			}
		}

		// Token: 0x06001B00 RID: 6912 RVA: 0x000B72E0 File Offset: 0x000B54E0
		public static void WriteVariation(Variation metadataObj, JsonObject result, JsonObjectTreeWriterSettings settings)
		{
			result["name", TomPropCategory.Name, 0, false] = metadataObj.Name;
			if (settings.ShouldWriteObject(metadataObj))
			{
				settings.WriteObject(metadataObj, result);
			}
			if (settings.ShouldWriteCollection(metadataObj.Annotations))
			{
				object[] array = metadataObj.Annotations.Where((Annotation o) => settings.ShouldWriteObject(o)).Select(delegate(Annotation obj)
				{
					JsonObject jsonObject = new JsonObject();
					JsonObjectTreeWriter.WriteAnnotation(obj, jsonObject, settings);
					return jsonObject.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array2 = array;
				result["annotations", TomPropCategory.ChildCollection, 1000, false] = array2;
			}
			if (settings.ShouldWriteCollection(metadataObj.ExtendedProperties))
			{
				object[] array = metadataObj.ExtendedProperties.Where((ExtendedProperty o) => settings.ShouldWriteObject(o)).Select(delegate(ExtendedProperty obj)
				{
					JsonObject jsonObject2 = new JsonObject();
					JsonObjectTreeWriter.WriteExtendedProperty(obj, jsonObject2, settings);
					return jsonObject2.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array3 = array;
				result["extendedProperties", TomPropCategory.ChildCollection, 39, false] = array3;
			}
		}

		// Token: 0x06001B01 RID: 6913 RVA: 0x000B73D4 File Offset: 0x000B55D4
		public static void WriteSet(Set metadataObj, JsonObject result, JsonObjectTreeWriterSettings settings)
		{
			result["name", TomPropCategory.Name, 0, false] = metadataObj.Name;
			if (settings.ShouldWriteObject(metadataObj))
			{
				settings.WriteObject(metadataObj, result);
			}
			if (settings.ShouldWriteCollection(metadataObj.Annotations))
			{
				object[] array = metadataObj.Annotations.Where((Annotation o) => settings.ShouldWriteObject(o)).Select(delegate(Annotation obj)
				{
					JsonObject jsonObject = new JsonObject();
					JsonObjectTreeWriter.WriteAnnotation(obj, jsonObject, settings);
					return jsonObject.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array2 = array;
				result["annotations", TomPropCategory.ChildCollection, 1000, false] = array2;
			}
			if (settings.ShouldWriteCollection(metadataObj.ExtendedProperties))
			{
				object[] array = metadataObj.ExtendedProperties.Where((ExtendedProperty o) => settings.ShouldWriteObject(o)).Select(delegate(ExtendedProperty obj)
				{
					JsonObject jsonObject2 = new JsonObject();
					JsonObjectTreeWriter.WriteExtendedProperty(obj, jsonObject2, settings);
					return jsonObject2.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array3 = array;
				result["extendedProperties", TomPropCategory.ChildCollection, 39, false] = array3;
			}
		}

		// Token: 0x06001B02 RID: 6914 RVA: 0x000B74C8 File Offset: 0x000B56C8
		public static void WritePerspectiveSet(PerspectiveSet metadataObj, JsonObject result, JsonObjectTreeWriterSettings settings)
		{
			result["name", TomPropCategory.Name, 0, false] = metadataObj.Name;
			if (settings.ShouldWriteObject(metadataObj))
			{
				settings.WriteObject(metadataObj, result);
			}
			if (settings.ShouldWriteCollection(metadataObj.Annotations))
			{
				object[] array = metadataObj.Annotations.Where((Annotation o) => settings.ShouldWriteObject(o)).Select(delegate(Annotation obj)
				{
					JsonObject jsonObject = new JsonObject();
					JsonObjectTreeWriter.WriteAnnotation(obj, jsonObject, settings);
					return jsonObject.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array2 = array;
				result["annotations", TomPropCategory.ChildCollection, 1000, false] = array2;
			}
			if (settings.ShouldWriteCollection(metadataObj.ExtendedProperties))
			{
				object[] array = metadataObj.ExtendedProperties.Where((ExtendedProperty o) => settings.ShouldWriteObject(o)).Select(delegate(ExtendedProperty obj)
				{
					JsonObject jsonObject2 = new JsonObject();
					JsonObjectTreeWriter.WriteExtendedProperty(obj, jsonObject2, settings);
					return jsonObject2.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array3 = array;
				result["extendedProperties", TomPropCategory.ChildCollection, 39, false] = array3;
			}
		}

		// Token: 0x06001B03 RID: 6915 RVA: 0x000B75BA File Offset: 0x000B57BA
		public static void WriteExtendedProperty(ExtendedProperty metadataObj, JsonObject result, JsonObjectTreeWriterSettings settings)
		{
			result["name", TomPropCategory.Name, 0, false] = metadataObj.Name;
			if (settings.ShouldWriteObject(metadataObj))
			{
				settings.WriteObject(metadataObj, result);
			}
		}

		// Token: 0x06001B04 RID: 6916 RVA: 0x000B75E4 File Offset: 0x000B57E4
		public static void WriteNamedExpression(NamedExpression metadataObj, JsonObject result, JsonObjectTreeWriterSettings settings)
		{
			result["name", TomPropCategory.Name, 0, false] = metadataObj.Name;
			if (settings.ShouldWriteObject(metadataObj))
			{
				settings.WriteObject(metadataObj, result);
			}
			if (settings.ShouldWriteCollection(metadataObj.Annotations))
			{
				object[] array = metadataObj.Annotations.Where((Annotation o) => settings.ShouldWriteObject(o)).Select(delegate(Annotation obj)
				{
					JsonObject jsonObject = new JsonObject();
					JsonObjectTreeWriter.WriteAnnotation(obj, jsonObject, settings);
					return jsonObject.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array2 = array;
				result["annotations", TomPropCategory.ChildCollection, 1000, false] = array2;
			}
			if (settings.ShouldWriteCollection(metadataObj.ExtendedProperties))
			{
				object[] array = metadataObj.ExtendedProperties.Where((ExtendedProperty o) => settings.ShouldWriteObject(o)).Select(delegate(ExtendedProperty obj)
				{
					JsonObject jsonObject2 = new JsonObject();
					JsonObjectTreeWriter.WriteExtendedProperty(obj, jsonObject2, settings);
					return jsonObject2.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array3 = array;
				result["extendedProperties", TomPropCategory.ChildCollection, 39, false] = array3;
			}
			if (settings.ShouldWriteCollection(metadataObj.ExcludedArtifacts))
			{
				object[] array = metadataObj.ExcludedArtifacts.Where((ExcludedArtifact o) => settings.ShouldWriteObject(o)).Select(delegate(ExcludedArtifact obj)
				{
					JsonObject jsonObject3 = new JsonObject();
					JsonObjectTreeWriter.WriteExcludedArtifact(obj, jsonObject3, settings);
					return jsonObject3.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array4 = array;
				result["excludedArtifacts", TomPropCategory.ChildCollection, 53, false] = array4;
			}
		}

		// Token: 0x06001B05 RID: 6917 RVA: 0x000B772C File Offset: 0x000B592C
		public static void WriteColumnPermission(ColumnPermission metadataObj, JsonObject result, JsonObjectTreeWriterSettings settings)
		{
			result["name", TomPropCategory.Name, 0, false] = metadataObj.Name;
			if (settings.ShouldWriteObject(metadataObj))
			{
				settings.WriteObject(metadataObj, result);
			}
			if (settings.ShouldWriteCollection(metadataObj.Annotations))
			{
				object[] array = metadataObj.Annotations.Where((Annotation o) => settings.ShouldWriteObject(o)).Select(delegate(Annotation obj)
				{
					JsonObject jsonObject = new JsonObject();
					JsonObjectTreeWriter.WriteAnnotation(obj, jsonObject, settings);
					return jsonObject.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array2 = array;
				result["annotations", TomPropCategory.ChildCollection, 1000, false] = array2;
			}
			if (settings.ShouldWriteCollection(metadataObj.ExtendedProperties))
			{
				object[] array = metadataObj.ExtendedProperties.Where((ExtendedProperty o) => settings.ShouldWriteObject(o)).Select(delegate(ExtendedProperty obj)
				{
					JsonObject jsonObject2 = new JsonObject();
					JsonObjectTreeWriter.WriteExtendedProperty(obj, jsonObject2, settings);
					return jsonObject2.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array3 = array;
				result["extendedProperties", TomPropCategory.ChildCollection, 39, false] = array3;
			}
		}

		// Token: 0x06001B06 RID: 6918 RVA: 0x000B781E File Offset: 0x000B5A1E
		public static void WriteDetailRowsDefinition(DetailRowsDefinition metadataObj, JsonObject result, JsonObjectTreeWriterSettings settings)
		{
			if (settings.ShouldWriteObject(metadataObj))
			{
				settings.WriteObject(metadataObj, result);
			}
		}

		// Token: 0x06001B07 RID: 6919 RVA: 0x000B7834 File Offset: 0x000B5A34
		public static void WriteRelatedColumnDetails(RelatedColumnDetails metadataObj, JsonObject result, JsonObjectTreeWriterSettings settings)
		{
			if (settings.ShouldWriteObject(metadataObj))
			{
				settings.WriteObject(metadataObj, result);
			}
			if (settings.ShouldWriteCollection(metadataObj.GroupByColumns))
			{
				object[] array = metadataObj.GroupByColumns.Where((GroupByColumn o) => settings.ShouldWriteObject(o)).Select(delegate(GroupByColumn obj)
				{
					JsonObject jsonObject = new JsonObject();
					JsonObjectTreeWriter.WriteGroupByColumn(obj, jsonObject, settings);
					return jsonObject.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array2 = array;
				result["groupByColumns", TomPropCategory.ChildCollection, 44, false] = array2;
			}
		}

		// Token: 0x06001B08 RID: 6920 RVA: 0x000B78BC File Offset: 0x000B5ABC
		public static void WriteGroupByColumn(GroupByColumn metadataObj, JsonObject result, JsonObjectTreeWriterSettings settings)
		{
			if (settings.ShouldWriteObject(metadataObj))
			{
				settings.WriteObject(metadataObj, result);
			}
		}

		// Token: 0x06001B09 RID: 6921 RVA: 0x000B78D0 File Offset: 0x000B5AD0
		public static void WriteCalculationGroup(CalculationGroup metadataObj, JsonObject result, JsonObjectTreeWriterSettings settings)
		{
			if (settings.ShouldWriteObject(metadataObj))
			{
				settings.WriteObject(metadataObj, result);
			}
			if (metadataObj.MultipleOrEmptySelectionExpression != null && settings.ShouldWriteObject(metadataObj.MultipleOrEmptySelectionExpression))
			{
				JsonObject jsonObject = new JsonObject();
				JsonObjectTreeWriter.WriteCalculationGroupExpression(metadataObj.MultipleOrEmptySelectionExpression, jsonObject, settings);
				result["multipleOrEmptySelectionExpression", TomPropCategory.ChildLink, 5, false] = jsonObject.ToDictObject();
			}
			if (metadataObj.NoSelectionExpression != null && settings.ShouldWriteObject(metadataObj.NoSelectionExpression))
			{
				JsonObject jsonObject2 = new JsonObject();
				JsonObjectTreeWriter.WriteCalculationGroupExpression(metadataObj.NoSelectionExpression, jsonObject2, settings);
				result["noSelectionExpression", TomPropCategory.ChildLink, 6, false] = jsonObject2.ToDictObject();
			}
			if (settings.ShouldWriteCollection(metadataObj.Annotations))
			{
				object[] array = metadataObj.Annotations.Where((Annotation o) => settings.ShouldWriteObject(o)).Select(delegate(Annotation obj)
				{
					JsonObject jsonObject3 = new JsonObject();
					JsonObjectTreeWriter.WriteAnnotation(obj, jsonObject3, settings);
					return jsonObject3.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array2 = array;
				result["annotations", TomPropCategory.ChildCollection, 1000, false] = array2;
			}
			if (settings.ShouldWriteCollection(metadataObj.CalculationItems))
			{
				object[] array = metadataObj.CalculationItems.Where((CalculationItem o) => settings.ShouldWriteObject(o)).Select(delegate(CalculationItem obj)
				{
					JsonObject jsonObject4 = new JsonObject();
					JsonObjectTreeWriter.WriteCalculationItem(obj, jsonObject4, settings);
					return jsonObject4.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array3 = array;
				result["calculationItems", TomPropCategory.ChildCollection, 46, false] = array3;
			}
		}

		// Token: 0x06001B0A RID: 6922 RVA: 0x000B7A44 File Offset: 0x000B5C44
		public static void WriteCalculationItem(CalculationItem metadataObj, JsonObject result, JsonObjectTreeWriterSettings settings)
		{
			result["name", TomPropCategory.Name, 0, false] = metadataObj.Name;
			if (settings.ShouldWriteObject(metadataObj))
			{
				settings.WriteObject(metadataObj, result);
			}
			if (metadataObj.body.FormatStringDefinitionID.Object != null && settings.ShouldWriteObject(metadataObj.body.FormatStringDefinitionID.Object))
			{
				JsonObject jsonObject = new JsonObject();
				JsonObjectTreeWriter.WriteFormatStringDefinition(metadataObj.body.FormatStringDefinitionID.Object, jsonObject, settings);
				result["formatStringDefinition", TomPropCategory.ChildLink, 2, false] = jsonObject.ToDictObject();
			}
		}

		// Token: 0x06001B0B RID: 6923 RVA: 0x000B7AD4 File Offset: 0x000B5CD4
		public static void WriteAlternateOf(AlternateOf metadataObj, JsonObject result, JsonObjectTreeWriterSettings settings)
		{
			if (settings.ShouldWriteObject(metadataObj))
			{
				settings.WriteObject(metadataObj, result);
			}
			if (settings.ShouldWriteCollection(metadataObj.Annotations))
			{
				object[] array = metadataObj.Annotations.Where((Annotation o) => settings.ShouldWriteObject(o)).Select(delegate(Annotation obj)
				{
					JsonObject jsonObject = new JsonObject();
					JsonObjectTreeWriter.WriteAnnotation(obj, jsonObject, settings);
					return jsonObject.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array2 = array;
				result["annotations", TomPropCategory.ChildCollection, 1000, false] = array2;
			}
		}

		// Token: 0x06001B0C RID: 6924 RVA: 0x000B7B60 File Offset: 0x000B5D60
		public static void WriteRefreshPolicy(RefreshPolicy metadataObj, JsonObject result, JsonObjectTreeWriterSettings settings)
		{
			if (settings.ShouldWriteObject(metadataObj))
			{
				settings.WriteObject(metadataObj, result);
			}
			if (settings.ShouldWriteCollection(metadataObj.Annotations))
			{
				object[] array = metadataObj.Annotations.Where((Annotation o) => settings.ShouldWriteObject(o)).Select(delegate(Annotation obj)
				{
					JsonObject jsonObject = new JsonObject();
					JsonObjectTreeWriter.WriteAnnotation(obj, jsonObject, settings);
					return jsonObject.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array2 = array;
				result["annotations", TomPropCategory.ChildCollection, 1000, false] = array2;
			}
			if (settings.ShouldWriteCollection(metadataObj.ExtendedProperties))
			{
				object[] array = metadataObj.ExtendedProperties.Where((ExtendedProperty o) => settings.ShouldWriteObject(o)).Select(delegate(ExtendedProperty obj)
				{
					JsonObject jsonObject2 = new JsonObject();
					JsonObjectTreeWriter.WriteExtendedProperty(obj, jsonObject2, settings);
					return jsonObject2.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array3 = array;
				result["extendedProperties", TomPropCategory.ChildCollection, 39, false] = array3;
			}
		}

		// Token: 0x06001B0D RID: 6925 RVA: 0x000B7C3E File Offset: 0x000B5E3E
		public static void WriteFormatStringDefinition(FormatStringDefinition metadataObj, JsonObject result, JsonObjectTreeWriterSettings settings)
		{
			if (settings.ShouldWriteObject(metadataObj))
			{
				settings.WriteObject(metadataObj, result);
			}
		}

		// Token: 0x06001B0E RID: 6926 RVA: 0x000B7C54 File Offset: 0x000B5E54
		public static void WriteQueryGroup(QueryGroup metadataObj, JsonObject result, JsonObjectTreeWriterSettings settings)
		{
			result["name", TomPropCategory.Name, 0, false] = metadataObj.Name;
			if (settings.ShouldWriteObject(metadataObj))
			{
				settings.WriteObject(metadataObj, result);
			}
			if (settings.ShouldWriteCollection(metadataObj.Annotations))
			{
				object[] array = metadataObj.Annotations.Where((Annotation o) => settings.ShouldWriteObject(o)).Select(delegate(Annotation obj)
				{
					JsonObject jsonObject = new JsonObject();
					JsonObjectTreeWriter.WriteAnnotation(obj, jsonObject, settings);
					return jsonObject.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array2 = array;
				result["annotations", TomPropCategory.ChildCollection, 1000, false] = array2;
			}
		}

		// Token: 0x06001B0F RID: 6927 RVA: 0x000B7CF3 File Offset: 0x000B5EF3
		public static void WriteAnalyticsAIMetadata(AnalyticsAIMetadata metadataObj, JsonObject result, JsonObjectTreeWriterSettings settings)
		{
			result["name", TomPropCategory.Name, 0, false] = metadataObj.Name;
			if (settings.ShouldWriteObject(metadataObj))
			{
				settings.WriteObject(metadataObj, result);
			}
		}

		// Token: 0x06001B10 RID: 6928 RVA: 0x000B7D1A File Offset: 0x000B5F1A
		public static void WriteChangedProperty(ChangedProperty metadataObj, JsonObject result, JsonObjectTreeWriterSettings settings)
		{
			if (settings.ShouldWriteObject(metadataObj))
			{
				settings.WriteObject(metadataObj, result);
			}
		}

		// Token: 0x06001B11 RID: 6929 RVA: 0x000B7D2D File Offset: 0x000B5F2D
		public static void WriteExcludedArtifact(ExcludedArtifact metadataObj, JsonObject result, JsonObjectTreeWriterSettings settings)
		{
			if (settings.ShouldWriteObject(metadataObj))
			{
				settings.WriteObject(metadataObj, result);
			}
		}

		// Token: 0x06001B12 RID: 6930 RVA: 0x000B7D40 File Offset: 0x000B5F40
		public static void WriteDataCoverageDefinition(DataCoverageDefinition metadataObj, JsonObject result, JsonObjectTreeWriterSettings settings)
		{
			if (settings.ShouldWriteObject(metadataObj))
			{
				settings.WriteObject(metadataObj, result);
			}
			if (settings.ShouldWriteCollection(metadataObj.Annotations))
			{
				object[] array = metadataObj.Annotations.Where((Annotation o) => settings.ShouldWriteObject(o)).Select(delegate(Annotation obj)
				{
					JsonObject jsonObject = new JsonObject();
					JsonObjectTreeWriter.WriteAnnotation(obj, jsonObject, settings);
					return jsonObject.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array2 = array;
				result["annotations", TomPropCategory.ChildCollection, 1000, false] = array2;
			}
		}

		// Token: 0x06001B13 RID: 6931 RVA: 0x000B7DCC File Offset: 0x000B5FCC
		public static void WriteCalculationGroupExpression(CalculationGroupExpression metadataObj, JsonObject result, JsonObjectTreeWriterSettings settings)
		{
			if (settings.ShouldWriteObject(metadataObj))
			{
				settings.WriteObject(metadataObj, result);
			}
			if (metadataObj.body.FormatStringDefinitionID.Object != null && settings.ShouldWriteObject(metadataObj.body.FormatStringDefinitionID.Object))
			{
				JsonObject jsonObject = new JsonObject();
				JsonObjectTreeWriter.WriteFormatStringDefinition(metadataObj.body.FormatStringDefinitionID.Object, jsonObject, settings);
				result["formatStringDefinition", TomPropCategory.ChildLink, 2, false] = jsonObject.ToDictObject();
			}
		}

		// Token: 0x06001B14 RID: 6932 RVA: 0x000B7E48 File Offset: 0x000B6048
		public static void WriteCalendar(Calendar metadataObj, JsonObject result, JsonObjectTreeWriterSettings settings)
		{
			result["name", TomPropCategory.Name, 0, false] = metadataObj.Name;
			if (settings.ShouldWriteObject(metadataObj))
			{
				settings.WriteObject(metadataObj, result);
			}
			if (settings.ShouldWriteCollection(metadataObj.TimeUnitColumnAssociations))
			{
				object[] array = metadataObj.TimeUnitColumnAssociations.Where((TimeUnitColumnAssociation o) => settings.ShouldWriteObject(o)).Select(delegate(TimeUnitColumnAssociation obj)
				{
					JsonObject jsonObject = new JsonObject();
					JsonObjectTreeWriter.WriteTimeUnitColumnAssociation(obj, jsonObject, settings);
					return jsonObject.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array2 = array;
				result["timeUnitColumnAssociations", TomPropCategory.ChildCollection, 60, false] = array2;
			}
		}

		// Token: 0x06001B15 RID: 6933 RVA: 0x000B7EE4 File Offset: 0x000B60E4
		public static void WriteTimeUnitColumnAssociation(TimeUnitColumnAssociation metadataObj, JsonObject result, JsonObjectTreeWriterSettings settings)
		{
			result["timeUnit", TomPropCategory.Name, 0, false] = JsonPropertyHelper.ConvertEnumToJsonValue<TimeUnit>(metadataObj.body.TimeUnit);
			if (settings.ShouldWriteObject(metadataObj))
			{
				settings.WriteObject(metadataObj, result);
			}
		}

		// Token: 0x06001B16 RID: 6934 RVA: 0x000B7F18 File Offset: 0x000B6118
		public static void WriteFunction(Function metadataObj, JsonObject result, JsonObjectTreeWriterSettings settings)
		{
			result["name", TomPropCategory.Name, 0, false] = metadataObj.Name;
			if (settings.ShouldWriteObject(metadataObj))
			{
				settings.WriteObject(metadataObj, result);
			}
			if (settings.ShouldWriteCollection(metadataObj.Annotations))
			{
				object[] array = metadataObj.Annotations.Where((Annotation o) => settings.ShouldWriteObject(o)).Select(delegate(Annotation obj)
				{
					JsonObject jsonObject = new JsonObject();
					JsonObjectTreeWriter.WriteAnnotation(obj, jsonObject, settings);
					return jsonObject.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array2 = array;
				result["annotations", TomPropCategory.ChildCollection, 1000, false] = array2;
			}
			if (settings.ShouldWriteCollection(metadataObj.ExtendedProperties))
			{
				object[] array = metadataObj.ExtendedProperties.Where((ExtendedProperty o) => settings.ShouldWriteObject(o)).Select(delegate(ExtendedProperty obj)
				{
					JsonObject jsonObject2 = new JsonObject();
					JsonObjectTreeWriter.WriteExtendedProperty(obj, jsonObject2, settings);
					return jsonObject2.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array3 = array;
				result["extendedProperties", TomPropCategory.ChildCollection, 39, false] = array3;
			}
			if (settings.ShouldWriteCollection(metadataObj.ChangedProperties))
			{
				object[] array = metadataObj.ChangedProperties.Where((ChangedProperty o) => settings.ShouldWriteObject(o)).Select(delegate(ChangedProperty obj)
				{
					JsonObject jsonObject3 = new JsonObject();
					JsonObjectTreeWriter.WriteChangedProperty(obj, jsonObject3, settings);
					return jsonObject3.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array4 = array;
				result["changedProperties", TomPropCategory.ChildCollection, 52, false] = array4;
			}
		}

		// Token: 0x06001B17 RID: 6935 RVA: 0x000B8060 File Offset: 0x000B6260
		public static void WriteBindingInfo(BindingInfo metadataObj, JsonObject result, JsonObjectTreeWriterSettings settings)
		{
			result["name", TomPropCategory.Name, 0, false] = metadataObj.Name;
			if (settings.ShouldWriteObject(metadataObj))
			{
				settings.WriteObject(metadataObj, result);
			}
			if (settings.ShouldWriteCollection(metadataObj.Annotations))
			{
				object[] array = metadataObj.Annotations.Where((Annotation o) => settings.ShouldWriteObject(o)).Select(delegate(Annotation obj)
				{
					JsonObject jsonObject = new JsonObject();
					JsonObjectTreeWriter.WriteAnnotation(obj, jsonObject, settings);
					return jsonObject.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array2 = array;
				result["annotations", TomPropCategory.ChildCollection, 1000, false] = array2;
			}
			if (settings.ShouldWriteCollection(metadataObj.ExtendedProperties))
			{
				object[] array = metadataObj.ExtendedProperties.Where((ExtendedProperty o) => settings.ShouldWriteObject(o)).Select(delegate(ExtendedProperty obj)
				{
					JsonObject jsonObject2 = new JsonObject();
					JsonObjectTreeWriter.WriteExtendedProperty(obj, jsonObject2, settings);
					return jsonObject2.ToDictObject();
				}).ToArray<IDictionary<string, object>>();
				object[] array3 = array;
				result["extendedProperties", TomPropCategory.ChildCollection, 39, false] = array3;
			}
		}
	}
}
