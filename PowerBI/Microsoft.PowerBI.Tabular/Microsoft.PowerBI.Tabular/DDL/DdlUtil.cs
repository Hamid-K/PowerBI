using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Microsoft.AnalysisServices.Core;
using Microsoft.AnalysisServices.Extensions;
using Microsoft.AnalysisServices.Hosting;
using Microsoft.AnalysisServices.Tabular.DataRefresh;
using Microsoft.AnalysisServices.Tabular.Metadata;
using Microsoft.AnalysisServices.Tabular.Serialization;
using Microsoft.AnalysisServices.Tabular.Utilities;

namespace Microsoft.AnalysisServices.Tabular.DDL
{
	// Token: 0x02000121 RID: 289
	internal static class DdlUtil
	{
		// Token: 0x06001439 RID: 5177 RVA: 0x000821C8 File Offset: 0x000803C8
		static DdlUtil()
		{
			DdlUtil.objectTypeToInfoMap[ObjectType.Model] = new DdlUtil.ObjectTypeInformation(CompatibilityRestrictionSet.Empty, "Model", "TMSCHEMA_MODEL");
			DdlUtil.objectTypeToInfoMap[ObjectType.DataSource] = new DdlUtil.ObjectTypeInformation(CompatibilityRestrictionSet.Empty, "DataSources", "TMSCHEMA_DATA_SOURCES");
			DdlUtil.objectTypeToInfoMap[ObjectType.Table] = new DdlUtil.ObjectTypeInformation(CompatibilityRestrictionSet.Empty, "Tables", "TMSCHEMA_TABLES");
			DdlUtil.objectTypeToInfoMap[ObjectType.Column] = new DdlUtil.ObjectTypeInformation(CompatibilityRestrictionSet.Empty, "Columns", "TMSCHEMA_COLUMNS");
			DdlUtil.objectTypeToInfoMap[ObjectType.AttributeHierarchy] = new DdlUtil.ObjectTypeInformation(CompatibilityRestrictionSet.Empty, "AttributeHierarchies", "TMSCHEMA_ATTRIBUTE_HIERARCHIES");
			DdlUtil.objectTypeToInfoMap[ObjectType.Partition] = new DdlUtil.ObjectTypeInformation(CompatibilityRestrictionSet.Empty, "Partitions", "TMSCHEMA_PARTITIONS");
			DdlUtil.objectTypeToInfoMap[ObjectType.Relationship] = new DdlUtil.ObjectTypeInformation(CompatibilityRestrictionSet.Empty, "Relationships", "TMSCHEMA_RELATIONSHIPS");
			DdlUtil.objectTypeToInfoMap[ObjectType.Measure] = new DdlUtil.ObjectTypeInformation(CompatibilityRestrictionSet.Empty, "Measures", "TMSCHEMA_MEASURES");
			DdlUtil.objectTypeToInfoMap[ObjectType.Hierarchy] = new DdlUtil.ObjectTypeInformation(CompatibilityRestrictionSet.Empty, "Hierarchies", "TMSCHEMA_HIERARCHIES");
			DdlUtil.objectTypeToInfoMap[ObjectType.Level] = new DdlUtil.ObjectTypeInformation(CompatibilityRestrictionSet.Empty, "Levels", "TMSCHEMA_LEVELS");
			DdlUtil.objectTypeToInfoMap[ObjectType.Annotation] = new DdlUtil.ObjectTypeInformation(CompatibilityRestrictionSet.Empty, "Annotations", "TMSCHEMA_ANNOTATIONS");
			DdlUtil.objectTypeToInfoMap[ObjectType.KPI] = new DdlUtil.ObjectTypeInformation(CompatibilityRestrictionSet.Empty, "Kpis", "TMSCHEMA_KPIS");
			DdlUtil.objectTypeToInfoMap[ObjectType.Culture] = new DdlUtil.ObjectTypeInformation(CompatibilityRestrictionSet.Empty, "Cultures", "TMSCHEMA_CULTURES");
			DdlUtil.objectTypeToInfoMap[ObjectType.ObjectTranslation] = new DdlUtil.ObjectTypeInformation(CompatibilityRestrictionSet.Empty, "ObjectTranslations", "TMSCHEMA_OBJECT_TRANSLATIONS");
			DdlUtil.objectTypeToInfoMap[ObjectType.LinguisticMetadata] = new DdlUtil.ObjectTypeInformation(CompatibilityRestrictionSet.Empty, "LinguisticMetadata", "TMSCHEMA_LINGUISTIC_METADATA");
			DdlUtil.objectTypeToInfoMap[ObjectType.Perspective] = new DdlUtil.ObjectTypeInformation(CompatibilityRestrictionSet.Empty, "Perspectives", "TMSCHEMA_PERSPECTIVES");
			DdlUtil.objectTypeToInfoMap[ObjectType.PerspectiveTable] = new DdlUtil.ObjectTypeInformation(CompatibilityRestrictionSet.Empty, "PerspectiveTables", "TMSCHEMA_PERSPECTIVE_TABLES");
			DdlUtil.objectTypeToInfoMap[ObjectType.PerspectiveColumn] = new DdlUtil.ObjectTypeInformation(CompatibilityRestrictionSet.Empty, "PerspectiveColumns", "TMSCHEMA_PERSPECTIVE_COLUMNS");
			DdlUtil.objectTypeToInfoMap[ObjectType.PerspectiveHierarchy] = new DdlUtil.ObjectTypeInformation(CompatibilityRestrictionSet.Empty, "PerspectiveHierarchies", "TMSCHEMA_PERSPECTIVE_HIERARCHIES");
			DdlUtil.objectTypeToInfoMap[ObjectType.PerspectiveMeasure] = new DdlUtil.ObjectTypeInformation(CompatibilityRestrictionSet.Empty, "PerspectiveMeasures", "TMSCHEMA_PERSPECTIVE_MEASURES");
			DdlUtil.objectTypeToInfoMap[ObjectType.Role] = new DdlUtil.ObjectTypeInformation(CompatibilityRestrictionSet.Empty, "Roles", "TMSCHEMA_ROLES");
			DdlUtil.objectTypeToInfoMap[ObjectType.RoleMembership] = new DdlUtil.ObjectTypeInformation(CompatibilityRestrictionSet.Empty, "RoleMemberships", "TMSCHEMA_ROLE_MEMBERSHIPS");
			DdlUtil.objectTypeToInfoMap[ObjectType.TablePermission] = new DdlUtil.ObjectTypeInformation(CompatibilityRestrictionSet.Empty, "TablePermissions", "TMSCHEMA_TABLE_PERMISSIONS");
			DdlUtil.objectTypeToInfoMap[ObjectType.Variation] = new DdlUtil.ObjectTypeInformation(CompatibilityRestrictions.Variation, "Variations", "TMSCHEMA_VARIATIONS");
			DdlUtil.objectTypeToInfoMap[ObjectType.Set] = new DdlUtil.ObjectTypeInformation(CompatibilityRestrictions.Set, "Sets", "TMSCHEMA_SETS");
			DdlUtil.objectTypeToInfoMap[ObjectType.PerspectiveSet] = new DdlUtil.ObjectTypeInformation(CompatibilityRestrictions.PerspectiveSet, "PerspectiveSets", "TMSCHEMA_PERSPECTIVE_SETS");
			DdlUtil.objectTypeToInfoMap[ObjectType.ExtendedProperty] = new DdlUtil.ObjectTypeInformation(CompatibilityRestrictions.ExtendedProperty, "ExtendedProperties", "TMSCHEMA_EXTENDED_PROPERTIES");
			DdlUtil.objectTypeToInfoMap[ObjectType.Expression] = new DdlUtil.ObjectTypeInformation(CompatibilityRestrictions.NamedExpression, "Expressions", "TMSCHEMA_EXPRESSIONS");
			DdlUtil.objectTypeToInfoMap[ObjectType.ColumnPermission] = new DdlUtil.ObjectTypeInformation(CompatibilityRestrictions.ColumnPermission, "ColumnPermissions", "TMSCHEMA_COLUMN_PERMISSIONS");
			DdlUtil.objectTypeToInfoMap[ObjectType.DetailRowsDefinition] = new DdlUtil.ObjectTypeInformation(CompatibilityRestrictions.DetailRowsDefinition, "DetailRowsDefinitions", "TMSCHEMA_DETAIL_ROWS_DEFINITIONS");
			DdlUtil.objectTypeToInfoMap[ObjectType.RelatedColumnDetails] = new DdlUtil.ObjectTypeInformation(CompatibilityRestrictions.RelatedColumnDetails, "RelatedColumnDetails", "TMSCHEMA_RELATED_COLUMN_DETAILS");
			DdlUtil.objectTypeToInfoMap[ObjectType.GroupByColumn] = new DdlUtil.ObjectTypeInformation(CompatibilityRestrictions.GroupByColumn, "GroupByColumns", "TMSCHEMA_GROUP_BY_COLUMNS");
			DdlUtil.objectTypeToInfoMap[ObjectType.CalculationGroup] = new DdlUtil.ObjectTypeInformation(CompatibilityRestrictions.CalculationGroup, "CalculationGroups", "TMSCHEMA_CALCULATION_GROUPS");
			DdlUtil.objectTypeToInfoMap[ObjectType.CalculationItem] = new DdlUtil.ObjectTypeInformation(CompatibilityRestrictions.CalculationItem, "CalculationItems", "TMSCHEMA_CALCULATION_ITEMS");
			DdlUtil.objectTypeToInfoMap[ObjectType.AlternateOf] = new DdlUtil.ObjectTypeInformation(CompatibilityRestrictions.AlternateOf, "AlternateOfDefinitions", "TMSCHEMA_ALTERNATE_OF_DEFINITIONS");
			DdlUtil.objectTypeToInfoMap[ObjectType.RefreshPolicy] = new DdlUtil.ObjectTypeInformation(CompatibilityRestrictions.RefreshPolicy, "RefreshPolicies", "TMSCHEMA_REFRESH_POLICIES");
			DdlUtil.objectTypeToInfoMap[ObjectType.FormatStringDefinition] = new DdlUtil.ObjectTypeInformation(CompatibilityRestrictions.FormatStringDefinition, "FormatStringDefinitions", "TMSCHEMA_FORMAT_STRING_DEFINITIONS");
			DdlUtil.objectTypeToInfoMap[ObjectType.QueryGroup] = new DdlUtil.ObjectTypeInformation(CompatibilityRestrictions.QueryGroup, "QueryGroups", "TMSCHEMA_QUERY_GROUPS");
			DdlUtil.objectTypeToInfoMap[ObjectType.AnalyticsAIMetadata] = new DdlUtil.ObjectTypeInformation(CompatibilityRestrictions.AnalyticsAIMetadata, "AnalyticsAIMetadata", "TMSCHEMA_ANALYTICS_AIMETADATA");
			DdlUtil.objectTypeToInfoMap[ObjectType.ChangedProperty] = new DdlUtil.ObjectTypeInformation(CompatibilityRestrictions.ChangedProperty, "ChangedProperties", "TMSCHEMA_CHANGED_PROPERTIES");
			DdlUtil.objectTypeToInfoMap[ObjectType.ExcludedArtifact] = new DdlUtil.ObjectTypeInformation(CompatibilityRestrictions.ExcludedArtifact, "ExcludedArtifacts", "TMSCHEMA_EXCLUDED_ARTIFACTS");
			DdlUtil.objectTypeToInfoMap[ObjectType.DataCoverageDefinition] = new DdlUtil.ObjectTypeInformation(CompatibilityRestrictions.DataCoverageDefinition, "DataCoverageDefinitions", "TMSCHEMA_DATA_COVERAGE_DEFINITIONS");
			DdlUtil.objectTypeToInfoMap[ObjectType.CalculationExpression] = new DdlUtil.ObjectTypeInformation(CompatibilityRestrictions.CalculationGroupExpression, "CalculationExpressions", "TMSCHEMA_CALCULATION_EXPRESSIONS");
			DdlUtil.objectTypeToInfoMap[ObjectType.Calendar] = new DdlUtil.ObjectTypeInformation(CompatibilityRestrictions.Calendar, "Calendars", "TMSCHEMA_CALENDARS");
			DdlUtil.objectTypeToInfoMap[ObjectType.TimeUnitColumnAssociation] = new DdlUtil.ObjectTypeInformation(CompatibilityRestrictions.TimeUnitColumnAssociation, "TimeUnitColumnAssociations", "TMSCHEMA_TIME_UNIT_COLUMN_ASSOCIATIONS");
			DdlUtil.objectTypeToInfoMap[ObjectType.CalendarColumnReference] = new DdlUtil.ObjectTypeInformation(CompatibilityRestrictions.CalendarColumnReference, "CalendarColumnReferences", "TMSCHEMA_CALENDAR_COLUMN_REFERENCES");
			DdlUtil.objectTypeToInfoMap[ObjectType.Function] = new DdlUtil.ObjectTypeInformation(CompatibilityRestrictions.Function, "Functions", "TMSCHEMA_FUNCTIONS");
			DdlUtil.objectTypeToInfoMap[ObjectType.BindingInfo] = new DdlUtil.ObjectTypeInformation(CompatibilityRestrictions.BindingInfo, "BindingInfoCollection", "TMSCHEMA_BINDING_INFO_COLLECTION");
		}

		// Token: 0x0600143A RID: 5178 RVA: 0x00082974 File Offset: 0x00080B74
		internal static void DiscoverModelMetadataStatus(Database db, out long version, out DateTime modifiedTime, out DateTime structureModifiedTime, out DateTime lastRefreshTime)
		{
			string effectiveDatabaseName = DdlUtil.GetEffectiveDatabaseName(db);
			XElement xelement = DdlUtil.GenerateDiscoverBatch(effectiveDatabaseName, DdlUtil.objectTypesForMetadataStatus);
			DataSet dataSet = DdlUtil.DiscoverModelDatasetImpl(db, xelement);
			DataTable dataTable = DdlUtil.ObtainModelTable(dataSet, effectiveDatabaseName);
			version = DdlUtil.GetVersionFromDataTable(dataTable);
			modifiedTime = DdlUtil.GetDateTimeFromDataTable(dataTable, "ModifiedTime", 0);
			structureModifiedTime = DdlUtil.GetDateTimeFromDataTable(dataTable, "StructureModifiedTime", 0);
			lastRefreshTime = DateTime.MinValue;
			DataTable dataTable2 = dataSet.Tables["Partition"];
			if (dataTable2 != null && dataTable2.Rows.Count > 0)
			{
				for (int i = 0; i < dataTable2.Rows.Count; i++)
				{
					DateTime dateTimeFromDataTable = DdlUtil.GetDateTimeFromDataTable(dataTable2, "RefreshedTime", i);
					if (dateTimeFromDataTable.CompareTo(lastRefreshTime) > 0)
					{
						lastRefreshTime = dateTimeFromDataTable;
					}
				}
			}
		}

		// Token: 0x0600143B RID: 5179 RVA: 0x00082A40 File Offset: 0x00080C40
		internal static Model DiscoverModel(Database db)
		{
			IList<ObjectType> list;
			string text;
			DataSet dataSet = DdlUtil.DiscoverModelDataset(db, out list, out text);
			DataTable dataTable = DdlUtil.ObtainModelTable(dataSet, text);
			object obj = dataTable.Rows[0]["Culture"];
			Utils.Verify(obj != null, "Culture can not be null for Model table");
			object obj2 = dataTable.Rows[0]["Collation"];
			Utils.Verify(obj2 != null, "Collation can not be null for Model table");
			IDictionary<ObjectId, MetadataObject> dictionary;
			Model model = DdlUtil.LoadModelFromSchema(db.CompatibilityMode, db.CompatibilityLevel, dataSet, list, DdlUtil.GetStringComparer(obj.ToString(), obj2.ToString()), out dictionary);
			model.UpdateModelSyncTime();
			return model;
		}

		// Token: 0x0600143C RID: 5180 RVA: 0x00082AD8 File Offset: 0x00080CD8
		private static DataSet DiscoverModelDataset(Database db, out IList<ObjectType> objectTypes, out string effectiveDbName)
		{
			effectiveDbName = DdlUtil.GetEffectiveDatabaseName(db);
			XElement xelement = DdlUtil.GenerateDiscoverBatch(db.CompatibilityMode, db.GetEffectiveCompatibilityLevel(false), effectiveDbName, out objectTypes);
			return DdlUtil.DiscoverModelDatasetImpl(db, xelement);
		}

		// Token: 0x0600143D RID: 5181 RVA: 0x00082B0A File Offset: 0x00080D0A
		private static string GetEffectiveDatabaseName(Database db)
		{
			return db.NameOnServer ?? db.Name;
		}

		// Token: 0x0600143E RID: 5182 RVA: 0x00082B1C File Offset: 0x00080D1C
		private static DataSet DiscoverModelDatasetImpl(Database db, XElement batch)
		{
			DataSet dataSet = new DataSet();
			string text = batch.ToString();
			XmlaResultCollection xmlaResultCollection;
			using (AmoDataReader amoDataReader = db.Parent.ExecuteReader(text, out xmlaResultCollection, null, true))
			{
				if (xmlaResultCollection != null && xmlaResultCollection.ContainsErrors)
				{
					throw new OperationException(TomSR.Exception_DiscoverModelFailed(xmlaResultCollection.GetAggregatedMessage()), xmlaResultCollection, text);
				}
				Utils.Verify(amoDataReader != null, "reader cannot be null unless we're in capture mode");
				new AmoDataAdapter(amoDataReader).Fill(dataSet);
			}
			return dataSet;
		}

		// Token: 0x0600143F RID: 5183 RVA: 0x00082BA0 File Offset: 0x00080DA0
		private static DataTable ObtainModelTable(DataSet dataSet, string effectiveDbName)
		{
			DataTable dataTable = dataSet.Tables["Model"];
			if (dataTable == null || dataTable.Rows.Count == 0)
			{
				throw new TomException(TomSR.Exception_DiscoverModelFailedBadDb(effectiveDbName));
			}
			return dataTable;
		}

		// Token: 0x06001440 RID: 5184 RVA: 0x00082BDB File Offset: 0x00080DDB
		private static XElement GenerateDiscoverBatch(string dbName, IEnumerable<ObjectType> objectTypes)
		{
			return DdlUtil.GenerateDiscoverBatchImpl(dbName, objectTypes.Select((ObjectType type) => DdlUtil.objectTypeToInfoMap[type].DiscoverSchemaName));
		}

		// Token: 0x06001441 RID: 5185 RVA: 0x00082C08 File Offset: 0x00080E08
		private static XElement GenerateDiscoverBatch(CompatibilityMode mode, int compatibilityLevel, string dbName, out IList<ObjectType> objectTypes)
		{
			List<ObjectType> compatibleTypes = new List<ObjectType>();
			XElement xelement = DdlUtil.GenerateDiscoverBatchImpl(dbName, (from type in DdlUtil.objectTypes
				select new KeyValuePair<ObjectType, DdlUtil.ObjectTypeInformation>(type, DdlUtil.objectTypeToInfoMap[type]) into typeAndInfo
				where typeAndInfo.Value.RestrictionSet.IsCompatible(mode, compatibilityLevel)
				select typeAndInfo).Select(delegate(KeyValuePair<ObjectType, DdlUtil.ObjectTypeInformation> typeAndInfo)
			{
				compatibleTypes.Add(typeAndInfo.Key);
				return typeAndInfo.Value.DiscoverSchemaName;
			}));
			objectTypes = compatibleTypes;
			return xelement;
		}

		// Token: 0x06001442 RID: 5186 RVA: 0x00082C90 File Offset: 0x00080E90
		private static XElement GenerateDiscoverBatchImpl(string dbName, IEnumerable<string> schemaNames)
		{
			XElement xelement = new XElement(XmlaConstants.XNS.ddl + "Batch", new XAttribute("Transaction", "false"));
			foreach (string text in schemaNames)
			{
				xelement.Add(DdlUtil.GenerateDiscoverRequest(text, dbName));
			}
			return xelement;
		}

		// Token: 0x06001443 RID: 5187 RVA: 0x00082D08 File Offset: 0x00080F08
		private static XElement GenerateDiscoverRequest(string schemaName, string dbName)
		{
			XElement xelement = new XElement(XmlaConstants.XNS.ana + "RestrictionList", new XElement(XmlaConstants.XNS.ana + "DatabaseName", dbName));
			return new XElement(XmlaConstants.XNS.ana + "Discover", new object[]
			{
				new XElement(XmlaConstants.XNS.ana + "RequestType", schemaName),
				new XElement(XmlaConstants.XNS.ana + "Restrictions", xelement),
				new XElement(XmlaConstants.XNS.ana + "Properties", new XElement(XmlaConstants.XNS.ana + "PropertyList"))
			});
		}

		// Token: 0x06001444 RID: 5188 RVA: 0x00082DB4 File Offset: 0x00080FB4
		internal static Model LoadModelFromSchema(CompatibilityMode mode, int compatibilityLevel, DataSet schema, IList<ObjectType> objectTypes, IEqualityComparer<string> comparer, out IDictionary<ObjectId, MetadataObject> objectMap)
		{
			IList<MetadataObject> list = DdlUtil.LoadObjectsFromSchema(mode, compatibilityLevel, schema, objectTypes, comparer, out objectMap);
			Utils.Verify(list.Count > 0, "Because Model must exist on Server, we expect positive 'roots.Count' value");
			List<Model> list2 = new List<Model>(from o in list
				where o is Model
				select (Model)o);
			Utils.Verify(list2.Count == 1, "Expected a single toplevel Model object, got {0}", new KeyValuePair<InfoRestrictionType, object>[]
			{
				new KeyValuePair<InfoRestrictionType, object>(InfoRestrictionType.Unrestricted, (list2.Count == 0) ? "none" : "more than one")
			});
			return list2[0];
		}

		// Token: 0x06001445 RID: 5189 RVA: 0x00082E70 File Offset: 0x00081070
		private static IList<MetadataObject> LoadObjectsFromSchema(CompatibilityMode mode, int compatibilityLevel, DataSet schema, IList<ObjectType> types, IEqualityComparer<string> comparer, out IDictionary<ObjectId, MetadataObject> objectMap)
		{
			objectMap = new Dictionary<ObjectId, MetadataObject>();
			for (int i = 0; i < schema.Tables.Count; i++)
			{
				ObjectType objectType = types[i];
				DataTable dataTable = schema.Tables[i];
				if (dataTable.Rows.Count > 0)
				{
					DdlUtil.LoadDataTable(objectType, mode, compatibilityLevel, dataTable, objectMap, comparer);
					if (objectType == ObjectType.Model)
					{
						((Model)objectMap[ObjectId.Model]).Version = DdlUtil.GetVersionFromDataTable(dataTable);
					}
				}
			}
			List<MetadataObject> list = new List<MetadataObject>();
			foreach (MetadataObject metadataObject in objectMap.Values)
			{
				metadataObject.ResolveLinks(objectMap, true);
				if (metadataObject.Parent == null)
				{
					list.Add(metadataObject);
				}
			}
			return list;
		}

		// Token: 0x06001446 RID: 5190 RVA: 0x00082F54 File Offset: 0x00081154
		private static void LoadDataTable(ObjectType objectType, CompatibilityMode mode, int compatibilityLevel, DataTable table, IDictionary<ObjectId, MetadataObject> objectMap, IEqualityComparer<string> comparer)
		{
			RowsetPropertyReader rowsetPropertyReader = new RowsetPropertyReader();
			foreach (object obj in table.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				rowsetPropertyReader.SetDataRow(dataRow);
				MetadataObject metadataObject = ObjectFactory.CreateObjectFromRowset(objectType, rowsetPropertyReader, comparer);
				metadataObject.ReadAllBodyProperties(rowsetPropertyReader, mode, compatibilityLevel);
				ObjectId id = metadataObject.Id;
				if (objectMap.ContainsKey(id))
				{
					throw TomInternalException.CreateWithRestrictedInfo("Duplicate object ID {0}, first in '{1}', another one in '{2}'", new KeyValuePair<InfoRestrictionType, object>[]
					{
						new KeyValuePair<InfoRestrictionType, object>(InfoRestrictionType.Unrestricted, id),
						new KeyValuePair<InfoRestrictionType, object>(InfoRestrictionType.CCON, objectMap[id]),
						new KeyValuePair<InfoRestrictionType, object>(InfoRestrictionType.CCON, metadataObject)
					});
				}
				objectMap.Add(id, metadataObject);
			}
		}

		// Token: 0x06001447 RID: 5191 RVA: 0x00083038 File Offset: 0x00081238
		private static long GetVersionFromDataTable(DataTable table)
		{
			if (!table.Columns.Contains("Version"))
			{
				throw new ResponseFormatException(TomSR.Exception_NoVersionColumnInRowset);
			}
			DataColumn dataColumn = table.Columns["Version"];
			object obj = table.Rows[0].ItemArray[dataColumn.Ordinal];
			Utils.Verify(obj is long);
			return (long)obj;
		}

		// Token: 0x06001448 RID: 5192 RVA: 0x000830A0 File Offset: 0x000812A0
		private static DateTime GetDateTimeFromDataTable(DataTable table, string columnName, int rowIndex = 0)
		{
			if (!table.Columns.Contains(columnName))
			{
				return DateTime.MinValue;
			}
			DataColumn dataColumn = table.Columns[columnName];
			DataRow dataRow = table.Rows[rowIndex];
			if (dataRow.IsNull(dataColumn.Ordinal))
			{
				return DateTime.MinValue;
			}
			object obj = dataRow.ItemArray[dataColumn.Ordinal];
			Utils.Verify(obj is DateTime);
			return (DateTime)obj;
		}

		// Token: 0x06001449 RID: 5193 RVA: 0x0008310F File Offset: 0x0008130F
		internal static IEqualityComparer<string> GetStringComparer(string culture, string collation)
		{
			return StringComparer.OrdinalIgnoreCase;
		}

		// Token: 0x0600144A RID: 5194 RVA: 0x00083118 File Offset: 0x00081318
		internal static ServerImpact GetImpactFromSchema(CompatibilityMode mode, int compatibilityLevel, DataSet schema, IEqualityComparer<string> comparer)
		{
			ServerImpact serverImpact = new ServerImpact();
			foreach (object obj in schema.Tables)
			{
				DataTable dataTable = (DataTable)obj;
				DdlUtil.LoadImpactFromTable(mode, compatibilityLevel, serverImpact, dataTable, comparer);
			}
			return serverImpact;
		}

		// Token: 0x0600144B RID: 5195 RVA: 0x0008317C File Offset: 0x0008137C
		private static void LoadImpactFromTable(CompatibilityMode mode, int compatibilityLevel, ServerImpact impact, DataTable table, IEqualityComparer<string> comparer)
		{
			ObjectType objectType;
			if (Enum.TryParse<ObjectType>(table.TableName, true, out objectType))
			{
				RowsetPropertyReader rowsetPropertyReader = new RowsetPropertyReader();
				foreach (object obj in table.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					rowsetPropertyReader.SetDataRow(dataRow);
					DdlUtil.LoadImpactRow(impact, objectType, mode, compatibilityLevel, rowsetPropertyReader, comparer);
				}
			}
		}

		// Token: 0x0600144C RID: 5196 RVA: 0x000831FC File Offset: 0x000813FC
		private static void LoadImpactRow(ServerImpact impact, ObjectType objectType, CompatibilityMode mode, int compatibilityLevel, RowsetPropertyReader reader, IEqualityComparer<string> comparer)
		{
			ServerImpactType serverImpactType = DdlUtil.ImpactTypeFromRow(reader.Row);
			if (serverImpactType == ServerImpactType.Modified)
			{
				MetadataObject metadataObject = ObjectFactory.CreateObjectFromRowset(objectType, reader, comparer);
				metadataObject.ReadAllBodyProperties(reader, mode, compatibilityLevel);
				impact.AddAffectedObject(metadataObject);
				return;
			}
			if (serverImpactType == ServerImpactType.Deleted)
			{
				ObjectId objectId = DdlUtil.ObjectIdFromRow(reader.Row);
				impact.AddDeletedObject(objectId);
				return;
			}
			throw TomInternalException.Create("Unknown impact type '{0}'", new object[] { serverImpactType.ToString() });
		}

		// Token: 0x0600144D RID: 5197 RVA: 0x00083270 File Offset: 0x00081470
		private static ServerImpactType ImpactTypeFromRow(DataRow row)
		{
			int num = row.Table.Columns.IndexOf("ImpactType");
			object obj = row[num];
			if (!(obj is DBNull))
			{
				return (ServerImpactType)((int)obj);
			}
			return ServerImpactType.Modified;
		}

		// Token: 0x0600144E RID: 5198 RVA: 0x000832AC File Offset: 0x000814AC
		private static ObjectId ObjectIdFromRow(DataRow row)
		{
			int num = row.Table.Columns.IndexOf("ID");
			return ObjectId.FromUInt64((ulong)row[num]);
		}

		// Token: 0x0600144F RID: 5199 RVA: 0x000832E0 File Offset: 0x000814E0
		private static string CreateGetAdaptiveCachingRecommendationsCommand(AutomaticAggregationOptions options)
		{
			StringBuilder stringBuilder = new StringBuilder();
			using (XmlWriter xmlWriter = XmlWriter.Create(stringBuilder, new XmlWriterSettings
			{
				OmitXmlDeclaration = true
			}))
			{
				xmlWriter.WriteStartElement("Statement");
				xmlWriter.WriteCData(AutomaticAggregationOptions.GetDaxRecomendationQuery(options));
				xmlWriter.WriteEndElement();
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001450 RID: 5200 RVA: 0x00083348 File Offset: 0x00081548
		internal static DataSet GetAdaptiveCachingRecommendations(Model model, AutomaticAggregationOptions options)
		{
			XmlaResultCollection xmlaResultCollection;
			DataSet dataSet2;
			using (AmoDataReader amoDataReader = model.Server.ExecuteReader(DdlUtil.CreateGetAdaptiveCachingRecommendationsCommand(options), out xmlaResultCollection, null, true))
			{
				if (xmlaResultCollection != null && xmlaResultCollection.ContainsErrors)
				{
					throw XmlaResultCollection.ExceptionOnError(xmlaResultCollection);
				}
				if (amoDataReader == null)
				{
					throw new TomException(TomSR.Exception_CannotRetrieveAdaptiveCachingRecommendations);
				}
				DataAdapter dataAdapter = new AmoDataAdapter(amoDataReader);
				DataSet dataSet = new DataSet();
				dataAdapter.Fill(dataSet);
				dataSet2 = dataSet;
			}
			return dataSet2;
		}

		// Token: 0x06001451 RID: 5201 RVA: 0x000833C0 File Offset: 0x000815C0
		internal static XElement GetBatchElement(bool isTransactional)
		{
			return new XElement(XmlaConstants.XNS.ddl + "Batch", new XAttribute("Transaction", isTransactional ? "true" : "false"));
		}

		// Token: 0x06001452 RID: 5202 RVA: 0x000833F4 File Offset: 0x000815F4
		internal static string FormatBatchWithCommands(IEnumerable<string> commands, bool isTransactional)
		{
			StringBuilder stringBuilder = new StringBuilder();
			using (XmlWriter xmlWriter = XmlWriter.Create(stringBuilder, new XmlWriterSettings
			{
				Indent = true,
				ConformanceLevel = ConformanceLevel.Fragment
			}))
			{
				xmlWriter.WriteStartElement("Batch", "http://schemas.microsoft.com/analysisservices/2003/engine");
				xmlWriter.WriteAttributeString("Transaction", XmlConvert.ToString(isTransactional));
				foreach (string text in commands)
				{
					xmlWriter.WriteRaw(text);
				}
				xmlWriter.WriteEndElement();
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001453 RID: 5203 RVA: 0x000834A4 File Offset: 0x000816A4
		internal static XElement FormatCreate(WriteOptions options, string databaseId, CompatibilityMode mode, int dbCompatibilityLevel, IEnumerable<MetadataObject> newObjects)
		{
			XElement xelement = new XElement(DdlUtil.GetDDLRequestElement(DDLType.Create));
			xelement.Add(DdlUtil.GetDatabaseIDElement(databaseId));
			DdlPropertyWriter ddlPropertyWriter = new DdlPropertyWriter();
			Dictionary<ObjectType, List<MetadataObject>> dictionary = (from o in newObjects
				where !ObjectTreeHelper.IsInferredObject(o)
				select o into obj
				group obj by obj.ObjectType).ToDictionary((IGrouping<ObjectType, MetadataObject> g) => g.Key, (IGrouping<ObjectType, MetadataObject> g) => g.ToList<MetadataObject>());
			List<ObjectType> list = dictionary.Keys.ToList<ObjectType>();
			list.Sort((ObjectType t1, ObjectType t2) => ObjectTreeHelper.GetObjectTypeTopologicalOrder(t1) - ObjectTreeHelper.GetObjectTypeTopologicalOrder(t2));
			foreach (ObjectType objectType in list)
			{
				XElement objectTypeContainerElement = DdlUtil.GetObjectTypeContainerElement(objectType, mode);
				xelement.Add(objectTypeContainerElement);
				IObjectRowsetSchema objectRowsetSchema = DdlUtil.FormatSchema(objectType, DDLRowsetType.Create, mode, dbCompatibilityLevel);
				objectTypeContainerElement.Add(new XElement(objectRowsetSchema.XmlSchema));
				foreach (MetadataObject metadataObject in dictionary[objectType])
				{
					XElement xelement2 = DdlUtil.CreateRowElement();
					ddlPropertyWriter.SetParentElement(xelement2);
					ddlPropertyWriter.SetSchema(objectRowsetSchema);
					ddlPropertyWriter.BeginWrite();
					metadataObject.WriteAllBodyProperties(ddlPropertyWriter, options, mode, dbCompatibilityLevel);
					ddlPropertyWriter.EndWrite();
					objectTypeContainerElement.Add(xelement2);
					ddlPropertyWriter.SetParentElement(null);
				}
			}
			return xelement;
		}

		// Token: 0x06001454 RID: 5204 RVA: 0x00083678 File Offset: 0x00081878
		internal static XElement FormatAlter(string databaseId, CompatibilityMode mode, int dbCompatibilityLevel, IEnumerable<PropertyChangeEntry> propChanges)
		{
			XElement xelement = new XElement(DdlUtil.GetDDLRequestElement(DDLType.Alter));
			xelement.Add(DdlUtil.GetDatabaseIDElement(databaseId));
			foreach (PropertyChangeEntry propertyChangeEntry in propChanges.Where((PropertyChangeEntry p) => p.IsUserProperty))
			{
				propertyChangeEntry.Validate();
			}
			Dictionary<ObjectType, Dictionary<MetadataObject, List<PropertyChangeEntry>>> dictionary = (from p in propChanges
				where p.IsDDLProperty && !p.IsReadOnlyProperty
				group p by p.Object.ObjectType).ToDictionary((IGrouping<ObjectType, PropertyChangeEntry> g) => g.Key, (IGrouping<ObjectType, PropertyChangeEntry> g) => (from p in g
				group p by p.Object).ToDictionary((IGrouping<MetadataObject, PropertyChangeEntry> g2) => g2.Key, (IGrouping<MetadataObject, PropertyChangeEntry> g2) => g2.ToList<PropertyChangeEntry>()));
			DdlPropertyWriter ddlPropertyWriter = new DdlPropertyWriter();
			foreach (ObjectType objectType in dictionary.Keys)
			{
				XElement objectTypeContainerElement = DdlUtil.GetObjectTypeContainerElement(objectType, mode);
				xelement.Add(objectTypeContainerElement);
				IObjectRowsetSchema objectRowsetSchema = DdlUtil.FormatSchema(objectType, DDLRowsetType.Alter, mode, dbCompatibilityLevel);
				objectTypeContainerElement.Add(new XElement(objectRowsetSchema.XmlSchema));
				foreach (MetadataObject metadataObject in dictionary[objectType].Keys)
				{
					XElement xelement2 = DdlUtil.CreateRowElement();
					ddlPropertyWriter.SetParentElement(xelement2);
					ddlPropertyWriter.SetSchema(objectRowsetSchema);
					ddlPropertyWriter.BeginWrite();
					MetadataObject.WriteObjectId(ddlPropertyWriter, WriteOptions.Default, "ID", metadataObject);
					foreach (PropertyChangeEntry propertyChangeEntry2 in dictionary[objectType][metadataObject])
					{
						((IPropertyWriter)ddlPropertyWriter).WriteProperty(WriteOptions.Default, propertyChangeEntry2.PropertyName, propertyChangeEntry2.PropertyType, propertyChangeEntry2.NewValue);
					}
					ddlPropertyWriter.EndWrite();
					objectTypeContainerElement.Add(xelement2);
				}
			}
			return xelement;
		}

		// Token: 0x06001455 RID: 5205 RVA: 0x00083918 File Offset: 0x00081B18
		internal static XElement FormatAlterSingleObject(string databaseId, CompatibilityMode mode, int dbCompatibilityLevel, MetadataObject obj)
		{
			XElement xelement = new XElement(DdlUtil.GetDDLRequestElement(DDLType.Alter));
			xelement.Add(DdlUtil.GetDatabaseIDElement(databaseId));
			DdlPropertyWriter ddlPropertyWriter = new DdlPropertyWriter();
			ObjectType objectType = obj.ObjectType;
			XElement objectTypeContainerElement = DdlUtil.GetObjectTypeContainerElement(objectType, mode);
			xelement.Add(objectTypeContainerElement);
			IObjectRowsetSchema objectRowsetSchema = DdlUtil.FormatSchema(objectType, DDLRowsetType.Alter, mode, dbCompatibilityLevel);
			objectTypeContainerElement.Add(new XElement(objectRowsetSchema.XmlSchema));
			XElement xelement2 = DdlUtil.CreateRowElement();
			ddlPropertyWriter.SetParentElement(xelement2);
			ddlPropertyWriter.SetSchema(objectRowsetSchema);
			ddlPropertyWriter.BeginWrite();
			obj.WriteAllBodyProperties(ddlPropertyWriter, WriteOptions.Default, mode, dbCompatibilityLevel);
			ddlPropertyWriter.EndWrite();
			objectTypeContainerElement.Add(xelement2);
			ddlPropertyWriter.SetParentElement(null);
			return xelement;
		}

		// Token: 0x06001456 RID: 5206 RVA: 0x000839B0 File Offset: 0x00081BB0
		internal static XElement FormatDelete(string databaseId, CompatibilityMode mode, int dbCompatibilityLevel, IEnumerable<MetadataObject> deletedObjects)
		{
			XElement xelement = new XElement(DdlUtil.GetDDLRequestElement(DDLType.Delete));
			xelement.Add(DdlUtil.GetDatabaseIDElement(databaseId));
			DdlPropertyWriter ddlPropertyWriter = new DdlPropertyWriter();
			Dictionary<ObjectType, List<MetadataObject>> dictionary = (from o in deletedObjects
				where !ObjectTreeHelper.IsInferredObject(o)
				select o into obj
				group obj by obj.ObjectType).ToDictionary((IGrouping<ObjectType, MetadataObject> g) => g.Key, (IGrouping<ObjectType, MetadataObject> g) => g.ToList<MetadataObject>());
			foreach (ObjectType objectType in dictionary.Keys)
			{
				XElement objectTypeContainerElement = DdlUtil.GetObjectTypeContainerElement(objectType, mode);
				xelement.Add(objectTypeContainerElement);
				IObjectRowsetSchema objectRowsetSchema = DdlUtil.FormatSchema(objectType, DDLRowsetType.Delete, mode, dbCompatibilityLevel);
				objectTypeContainerElement.Add(new XElement(objectRowsetSchema.XmlSchema));
				foreach (MetadataObject metadataObject in dictionary[objectType])
				{
					XElement xelement2 = DdlUtil.CreateRowElement();
					ddlPropertyWriter.SetParentElement(xelement2);
					ddlPropertyWriter.SetSchema(objectRowsetSchema);
					ddlPropertyWriter.BeginWrite();
					MetadataObject.WriteObjectId(ddlPropertyWriter, WriteOptions.Default, "ID", metadataObject);
					ddlPropertyWriter.EndWrite();
					objectTypeContainerElement.Add(xelement2);
				}
			}
			return xelement;
		}

		// Token: 0x06001457 RID: 5207 RVA: 0x00083B5C File Offset: 0x00081D5C
		internal static XElement FormatMergePartitions(string databaseId, CompatibilityMode mode, int dbCompatibilityLevel, Partition target)
		{
			DdlPropertyWriter ddlPropertyWriter = new DdlPropertyWriter();
			XElement xelement = new XElement(DdlUtil.GetDDLRequestElement(DDLType.MergePartitions));
			xelement.Add(DdlUtil.GetDatabaseIDElement(databaseId));
			xelement.Add(DdlUtil.GetTableNameElement(target.Table.Name));
			xelement.Add(DdlUtil.GetPartitionNameElement(target.Name));
			XElement objectTypeContainerElement = DdlUtil.GetObjectTypeContainerElement(target.ObjectType, mode);
			xelement.Add(objectTypeContainerElement);
			IObjectRowsetSchema objectRowsetSchema = DdlUtil.FormatSchema(target.ObjectType, DDLRowsetType.MergePartitions, mode, dbCompatibilityLevel);
			objectTypeContainerElement.Add(new XElement(objectRowsetSchema.XmlSchema));
			foreach (Partition partition in target.body.MergePartitionSources)
			{
				XElement xelement2 = DdlUtil.CreateRowElement();
				ddlPropertyWriter.SetParentElement(xelement2);
				ddlPropertyWriter.SetSchema(objectRowsetSchema);
				ddlPropertyWriter.BeginWrite();
				((IPropertyWriter)ddlPropertyWriter).WriteProperty(WriteOptions.Default, "ID", typeof(MetadataObject), partition);
				ddlPropertyWriter.EndWrite();
				objectTypeContainerElement.Add(xelement2);
				ddlPropertyWriter.SetParentElement(null);
			}
			return xelement;
		}

		// Token: 0x06001458 RID: 5208 RVA: 0x00083C70 File Offset: 0x00081E70
		internal static XElement FormatRename(string databaseId, CompatibilityMode mode, int dbCompatibilityLevel, IEnumerable<MetadataObject> renamedObjects)
		{
			XElement xelement = new XElement(DdlUtil.GetDDLRequestElement(DDLType.Rename));
			xelement.Add(DdlUtil.GetDatabaseIDElement(databaseId));
			Dictionary<ObjectType, List<MetadataObject>> dictionary = (from obj in renamedObjects
				group obj by obj.ObjectType).ToDictionary((IGrouping<ObjectType, MetadataObject> g) => g.Key, (IGrouping<ObjectType, MetadataObject> g) => g.ToList<MetadataObject>());
			DdlPropertyWriter ddlPropertyWriter = new DdlPropertyWriter();
			foreach (ObjectType objectType in dictionary.Keys)
			{
				XElement objectTypeContainerElement = DdlUtil.GetObjectTypeContainerElement(objectType, mode);
				xelement.Add(objectTypeContainerElement);
				IObjectRowsetSchema objectRowsetSchema = DdlUtil.FormatSchema(objectType, DDLRowsetType.Rename, mode, dbCompatibilityLevel);
				objectTypeContainerElement.Add(new XElement(objectRowsetSchema.XmlSchema));
				foreach (MetadataObject metadataObject in dictionary[objectType])
				{
					NamedMetadataObject namedMetadataObject = (NamedMetadataObject)metadataObject;
					XElement xelement2 = DdlUtil.CreateRowElement();
					ddlPropertyWriter.SetParentElement(xelement2);
					ddlPropertyWriter.SetSchema(objectRowsetSchema);
					ddlPropertyWriter.BeginWrite();
					((IPropertyWriter)ddlPropertyWriter).WriteProperty(WriteOptions.Default, "ID", typeof(MetadataObject), metadataObject);
					((IPropertyWriter)ddlPropertyWriter).WriteProperty(WriteOptions.Default, Utils.GetNamePropertyForObjectType(metadataObject.ObjectType), typeof(string), namedMetadataObject.Name);
					ddlPropertyWriter.EndWrite();
					objectTypeContainerElement.Add(xelement2);
					ddlPropertyWriter.SetParentElement(null);
				}
			}
			return xelement;
		}

		// Token: 0x06001459 RID: 5209 RVA: 0x00083E3C File Offset: 0x0008203C
		internal static XElement FormatAnalyzeRefreshPolicyImpact(string databaseId, CompatibilityMode mode, int dbCompatibilityLevel, IEnumerable<Partition> analyzeObjects)
		{
			XElement xelement = new XElement(DdlUtil.GetDDLRequestElement(DDLType.AnalyzeRefreshPolicyImpact));
			xelement.Add(DdlUtil.GetDatabaseIDElement(databaseId));
			DdlPropertyWriter ddlPropertyWriter = new DdlPropertyWriter();
			int num = 6;
			XElement objectTypeContainerElement = DdlUtil.GetObjectTypeContainerElement((ObjectType)num, mode);
			xelement.Add(objectTypeContainerElement);
			IObjectRowsetSchema objectRowsetSchema = DdlUtil.FormatSchema((ObjectType)num, DDLRowsetType.AnalyzeRefreshPolicyImpact, mode, dbCompatibilityLevel);
			objectTypeContainerElement.Add(new XElement(objectRowsetSchema.XmlSchema));
			foreach (Partition partition in analyzeObjects)
			{
				Partition partition2 = (Partition)partition;
				XElement xelement2 = DdlUtil.CreateRowElement();
				ddlPropertyWriter.SetParentElement(xelement2);
				ddlPropertyWriter.SetSchema(objectRowsetSchema);
				ddlPropertyWriter.BeginWrite();
				((IPropertyWriter)ddlPropertyWriter).WriteProperty(WriteOptions.Default, "ID.Table", typeof(string), partition2.Table.Name);
				((IPropertyWriter)ddlPropertyWriter).WriteProperty(WriteOptions.Default, "ID.Partition", typeof(string), partition2.Name);
				ddlPropertyWriter.EndWrite();
				objectTypeContainerElement.Add(xelement2);
				ddlPropertyWriter.SetParentElement(null);
			}
			return xelement;
		}

		// Token: 0x0600145A RID: 5210 RVA: 0x00083F44 File Offset: 0x00082144
		internal static XElement FormatRefresh(string databaseId, CompatibilityMode mode, int dbCompatibilityLevel, IEnumerable<MetadataObject> objectsToRefresh, int maxParallelism)
		{
			Dictionary<ObjectType, List<MetadataObject>> dictionary = (from obj in objectsToRefresh
				group obj by obj.ObjectType).ToDictionary((IGrouping<ObjectType, MetadataObject> g) => g.Key, (IGrouping<ObjectType, MetadataObject> g) => g.ToList<MetadataObject>());
			DdlPropertyWriter ddlPropertyWriter = new DdlPropertyWriter();
			XElement xelement = new XElement(DdlUtil.GetDDLRequestElement(DDLType.Refresh));
			xelement.Add(DdlUtil.GetDatabaseIDElement(databaseId));
			if (maxParallelism > 0)
			{
				xelement.Add(DdlUtil.GetMaxParallelismElement(maxParallelism));
			}
			foreach (ObjectType objectType in dictionary.Keys)
			{
				XElement objectTypeContainerElement = DdlUtil.GetObjectTypeContainerElement(objectType, mode);
				xelement.Add(objectTypeContainerElement);
				IObjectRowsetSchema objectRowsetSchema = DdlUtil.FormatSchema(objectType, DDLRowsetType.Refresh, mode, dbCompatibilityLevel);
				objectTypeContainerElement.Add(new XElement(objectRowsetSchema.XmlSchema));
				foreach (MetadataObject metadataObject in dictionary[objectType])
				{
					foreach (RefreshType refreshType in Utils.ConvertRefreshMaskToType(((IRefreshableMetadataObjectBody)metadataObject.Body).RequestedRefreshMask))
					{
						XElement xelement2 = DdlUtil.CreateRowElement();
						ddlPropertyWriter.SetParentElement(xelement2);
						ddlPropertyWriter.SetSchema(objectRowsetSchema);
						ddlPropertyWriter.BeginWrite();
						if (metadataObject.ObjectType != ObjectType.Model)
						{
							((IPropertyWriter)ddlPropertyWriter).WriteProperty(WriteOptions.Default, "ID", typeof(MetadataObject), metadataObject);
						}
						((IPropertyWriter)ddlPropertyWriter).WriteProperty(WriteOptions.Default, "RefreshType", typeof(RefreshType), refreshType);
						ddlPropertyWriter.EndWrite();
						objectTypeContainerElement.Add(xelement2);
						ddlPropertyWriter.SetParentElement(null);
					}
				}
			}
			IEnumerable<OverrideCollection> enumerable = objectsToRefresh.Where((MetadataObject o) => ((IRefreshableMetadataObjectBody)o.Body).Overrides != null).SelectMany((MetadataObject o) => ((IRefreshableMetadataObjectBody)o.Body).Overrides).Distinct<OverrideCollection>();
			DdlUtil.FormatOolBindings(xelement, mode, dbCompatibilityLevel, enumerable);
			return xelement;
		}

		// Token: 0x0600145B RID: 5211 RVA: 0x000841E4 File Offset: 0x000823E4
		internal static XElement FormatRefreshClearModel(Database database, int maxParallelism)
		{
			DdlPropertyWriter ddlPropertyWriter = new DdlPropertyWriter();
			XElement xelement = new XElement(DdlUtil.GetDDLRequestElement(DDLType.Refresh));
			xelement.Add(DdlUtil.GetDatabaseIDElement(database.ID));
			if (maxParallelism > 0)
			{
				xelement.Add(DdlUtil.GetMaxParallelismElement(maxParallelism));
			}
			XElement objectTypeContainerElement = DdlUtil.GetObjectTypeContainerElement(ObjectType.Model, database.CompatibilityMode);
			xelement.Add(objectTypeContainerElement);
			IObjectRowsetSchema objectRowsetSchema = DdlUtil.FormatSchema(ObjectType.Model, DDLRowsetType.Refresh, database.CompatibilityMode, database.CompatibilityLevel);
			objectTypeContainerElement.Add(new XElement(objectRowsetSchema.XmlSchema));
			XElement xelement2 = DdlUtil.CreateRowElement();
			ddlPropertyWriter.SetParentElement(xelement2);
			ddlPropertyWriter.SetSchema(objectRowsetSchema);
			ddlPropertyWriter.BeginWrite();
			((IPropertyWriter)ddlPropertyWriter).WriteProperty(WriteOptions.Default, "RefreshType", typeof(RefreshType), RefreshType.ClearValues);
			ddlPropertyWriter.EndWrite();
			objectTypeContainerElement.Add(xelement2);
			ddlPropertyWriter.SetParentElement(null);
			return xelement;
		}

		// Token: 0x0600145C RID: 5212 RVA: 0x000842A8 File Offset: 0x000824A8
		private static void FormatOolBindings(XElement refreshElem, CompatibilityMode mode, int dbCompatibilityLevel, IEnumerable<OverrideCollection> overrides)
		{
			if (!overrides.Any<OverrideCollection>())
			{
				return;
			}
			XElement xelement = new XElement(XmlaConstants.XNS.tmddl + "Bindings");
			foreach (OverrideCollection overrideCollection in overrides)
			{
				xelement.Add(DdlUtil.FormatOolBinding(mode, dbCompatibilityLevel, overrideCollection));
			}
			refreshElem.Add(xelement);
		}

		// Token: 0x0600145D RID: 5213 RVA: 0x0008431C File Offset: 0x0008251C
		private static XElement FormatOolBinding(CompatibilityMode mode, int dbCompatibilityLevel, OverrideCollection overrideCollection)
		{
			Utils.Verify(overrideCollection != null);
			Utils.Verify(overrideCollection.Scope != null);
			XElement xelement = new XElement(XmlaConstants.XNS.tmddl + "Binding");
			DdlUtil.FormatOolBindingTarget(xelement, overrideCollection.Scope);
			if (overrideCollection.DataSources.Any<DataSourceOverride>())
			{
				xelement.Add(DdlUtil.FormatOolObjectCollection(ObjectType.DataSource, mode, dbCompatibilityLevel, overrideCollection.DataSources.Cast<IObjectOverride>()));
			}
			if (overrideCollection.Columns.Any<ColumnOverride>())
			{
				xelement.Add(DdlUtil.FormatOolObjectCollection(ObjectType.Column, mode, dbCompatibilityLevel, overrideCollection.Columns.Cast<IObjectOverride>()));
			}
			if (overrideCollection.Partitions.Any<PartitionOverride>())
			{
				xelement.Add(DdlUtil.FormatOolObjectCollection(ObjectType.Partition, mode, dbCompatibilityLevel, overrideCollection.Partitions.Cast<IObjectOverride>()));
			}
			if (overrideCollection.Expressions.Any<NamedExpressionOverride>())
			{
				xelement.Add(DdlUtil.FormatOolObjectCollection(ObjectType.Expression, mode, dbCompatibilityLevel, overrideCollection.Expressions.Cast<IObjectOverride>()));
			}
			return xelement;
		}

		// Token: 0x0600145E RID: 5214 RVA: 0x000843FC File Offset: 0x000825FC
		private static void FormatOolBindingTarget(XElement bindingElem, MetadataObject targetObj)
		{
			if (!targetObj.Id.IsNull)
			{
				bindingElem.Add(new XElement(XmlaConstants.XNS.tmddl + "ObjectID", targetObj.Id));
				return;
			}
			ObjectType objectType = targetObj.ObjectType;
			if (objectType == ObjectType.Model)
			{
				return;
			}
			if (objectType == ObjectType.Table)
			{
				Table table = (Table)targetObj;
				bindingElem.Add(new XElement(XmlaConstants.XNS.tmddl + "TableName", table.Name));
				return;
			}
			if (objectType == ObjectType.Partition)
			{
				Partition partition = (Partition)targetObj;
				Utils.Verify(partition.Table != null);
				bindingElem.Add(new object[]
				{
					new XElement(XmlaConstants.XNS.tmddl + "TableName", partition.Table.Name),
					new XElement(XmlaConstants.XNS.tmddl + "PartitionName", partition.Name)
				});
				return;
			}
			throw TomInternalException.Create("Object '{0}' cannot be a target of out-of-line bindings. This must be validated earlier.", new object[] { targetObj.ObjectType.ToString() });
		}

		// Token: 0x0600145F RID: 5215 RVA: 0x0008450C File Offset: 0x0008270C
		private static XElement FormatOolObjectCollection(ObjectType objectType, CompatibilityMode mode, int dbCompatibilityLevel, IEnumerable<IObjectOverride> overrideObjects)
		{
			DdlPropertyWriter ddlPropertyWriter = new DdlPropertyWriter();
			XElement objectTypeContainerElement = DdlUtil.GetObjectTypeContainerElement(objectType, mode);
			IObjectRowsetSchema objectRowsetSchema = DdlUtil.FormatSchema(objectType, DDLRowsetType.Bindings, mode, dbCompatibilityLevel);
			objectTypeContainerElement.Add(new XElement(objectRowsetSchema.XmlSchema));
			foreach (IObjectOverride objectOverride in overrideObjects)
			{
				Utils.Verify(objectOverride.ObjectType == objectType);
				XElement xelement = DdlUtil.CreateRowElement();
				ddlPropertyWriter.SetParentElement(xelement);
				ddlPropertyWriter.SetSchema(objectRowsetSchema);
				ddlPropertyWriter.BeginWrite();
				MetadataObject originalObject = objectOverride.OriginalObject;
				Utils.Verify(originalObject != null);
				MetadataObject.WriteObjectId(ddlPropertyWriter, WriteOptions.Default, "ID", originalObject);
				((IMetadataObjectWithOverrides)originalObject).WriteAllOverridenBodyProperties(ddlPropertyWriter, WriteOptions.Default, mode, dbCompatibilityLevel, objectOverride.ReplacementProperties);
				ddlPropertyWriter.EndWrite();
				objectTypeContainerElement.Add(xelement);
				ddlPropertyWriter.SetParentElement(null);
			}
			return objectTypeContainerElement;
		}

		// Token: 0x06001460 RID: 5216 RVA: 0x000845F8 File Offset: 0x000827F8
		private static XElement FormatSequencePoint(string databaseId)
		{
			return new XElement(DdlUtil.GetDDLRequestElement(DDLType.SequencePoint), new XElement(XmlaConstants.XNS.tmddl + "DatabaseID", databaseId));
		}

		// Token: 0x06001461 RID: 5217 RVA: 0x0008461C File Offset: 0x0008281C
		internal static XElement FormatUpgrade(WriteOptions options, string databaseId, CompatibilityMode mode, int dbCompatibilityLevel, IEnumerable<MetadataObject> newObjects)
		{
			XElement xelement = new XElement(DdlUtil.GetDDLRequestElement(DDLType.Upgrade));
			xelement.Add(DdlUtil.GetDatabaseIDElement(databaseId));
			DdlPropertyWriter ddlPropertyWriter = new DdlPropertyWriter();
			Dictionary<ObjectType, List<MetadataObject>> dictionary = (from o in newObjects
				where !ObjectTreeHelper.IsInferredObject(o)
				select o into obj
				group obj by obj.ObjectType).ToDictionary((IGrouping<ObjectType, MetadataObject> g) => g.Key, (IGrouping<ObjectType, MetadataObject> g) => g.ToList<MetadataObject>());
			List<ObjectType> list = dictionary.Keys.ToList<ObjectType>();
			list.Sort((ObjectType t1, ObjectType t2) => ObjectTreeHelper.GetObjectTypeTopologicalOrder(t1) - ObjectTreeHelper.GetObjectTypeTopologicalOrder(t2));
			foreach (ObjectType objectType in list)
			{
				XElement objectTypeContainerElement = DdlUtil.GetObjectTypeContainerElement(objectType, mode);
				xelement.Add(objectTypeContainerElement);
				IObjectRowsetSchema objectRowsetSchema = DdlUtil.FormatSchema(objectType, DDLRowsetType.Create, mode, dbCompatibilityLevel);
				objectTypeContainerElement.Add(new XElement(objectRowsetSchema.XmlSchema));
				foreach (MetadataObject metadataObject in dictionary[objectType])
				{
					XElement xelement2 = DdlUtil.CreateRowElement();
					ddlPropertyWriter.SetParentElement(xelement2);
					ddlPropertyWriter.SetSchema(objectRowsetSchema);
					ddlPropertyWriter.BeginWrite();
					metadataObject.WriteAllBodyProperties(ddlPropertyWriter, options, mode, dbCompatibilityLevel);
					ddlPropertyWriter.EndWrite();
					objectTypeContainerElement.Add(xelement2);
					ddlPropertyWriter.SetParentElement(null);
				}
			}
			return xelement;
		}

		// Token: 0x06001462 RID: 5218 RVA: 0x000847F0 File Offset: 0x000829F0
		internal static IEnumerable<XElement> GetTabularRequests(string databaseId, CompatibilityMode mode, int dbCompatibilityLevel, ObjectChangelist changelist, SaveFlags saveFlags, int maxParallelism)
		{
			bool triggerSequencePoint = SaveOptions.IsFlagEnabled(saveFlags, SaveFlags.ForceValidation);
			if (!triggerSequencePoint && !changelist.IsEmpty && !SaveOptions.IsFlagEnabled(saveFlags, SaveFlags.DelayValidation))
			{
				triggerSequencePoint = true;
			}
			IEnumerable<PropertyChangeEntry> enumerable = changelist.PropChanges.Where((PropertyChangeEntry p) => p.Object.ObjectType == ObjectType.Model && (p.Flags & PropertyFlags.ModelReference) == PropertyFlags.None);
			IEnumerable<PropertyChangeEntry> modelReferenceChanges = changelist.PropChanges.Where((PropertyChangeEntry p) => p.Object.ObjectType == ObjectType.Model && (p.Flags & PropertyFlags.ModelReference) == PropertyFlags.ModelReference);
			if (enumerable.Any<PropertyChangeEntry>())
			{
				yield return DdlUtil.FormatAlter(databaseId, mode, dbCompatibilityLevel, enumerable);
				if (modelReferenceChanges.Any<PropertyChangeEntry>() && changelist.AddedObjects.Count > 0)
				{
					yield return DdlUtil.FormatSequencePoint(databaseId);
				}
			}
			if (changelist.RemovedSubtreeRoots.Count > 0)
			{
				yield return DdlUtil.FormatDelete(databaseId, mode, dbCompatibilityLevel, changelist.RemovedSubtreeRoots);
			}
			if (changelist.AddedObjects.Count > 0)
			{
				yield return DdlUtil.FormatCreate(WriteOptions.WriteOriginalNameInPath, databaseId, mode, dbCompatibilityLevel, changelist.AddedObjects);
			}
			IEnumerable<PropertyChangeEntry> enumerable2 = changelist.PropChanges.Where((PropertyChangeEntry p) => p.Object.ObjectType != ObjectType.Model);
			if (enumerable2.Any<PropertyChangeEntry>())
			{
				if (modelReferenceChanges.Any<PropertyChangeEntry>())
				{
					yield return DdlUtil.FormatAlter(databaseId, mode, dbCompatibilityLevel, enumerable2.Union(modelReferenceChanges));
				}
				else
				{
					yield return DdlUtil.FormatAlter(databaseId, mode, dbCompatibilityLevel, enumerable2);
				}
			}
			else if (modelReferenceChanges.Any<PropertyChangeEntry>())
			{
				yield return DdlUtil.FormatAlter(databaseId, mode, dbCompatibilityLevel, modelReferenceChanges);
			}
			if (changelist.AnalyzeRefreshPolicyImpactObjects.Count > 0)
			{
				yield return DdlUtil.FormatAnalyzeRefreshPolicyImpact(databaseId, mode, dbCompatibilityLevel, changelist.AnalyzeRefreshPolicyImpactObjects);
			}
			if (changelist.RefreshObjects.Count > 0)
			{
				yield return DdlUtil.FormatRefresh(databaseId, mode, dbCompatibilityLevel, changelist.RefreshObjects, maxParallelism);
			}
			if (changelist.PartitionMergedObjects.Count > 0)
			{
				foreach (Partition partition in changelist.PartitionMergedObjects)
				{
					yield return DdlUtil.FormatMergePartitions(databaseId, mode, dbCompatibilityLevel, partition);
				}
				IEnumerator<Partition> enumerator = null;
			}
			if (changelist.RenamedObjects.Count > 0)
			{
				yield return DdlUtil.FormatRename(databaseId, mode, dbCompatibilityLevel, changelist.RenamedObjects.Cast<MetadataObject>());
			}
			if (triggerSequencePoint)
			{
				yield return DdlUtil.FormatSequencePoint(databaseId);
			}
			yield break;
			yield break;
		}

		// Token: 0x06001463 RID: 5219 RVA: 0x00084825 File Offset: 0x00082A25
		internal static void AddProperty(XElement row, string name, string value)
		{
			row.Add(new XElement(XmlaConstants.XNS.rst + name, value));
		}

		// Token: 0x06001464 RID: 5220 RVA: 0x00084840 File Offset: 0x00082A40
		private static XElement GetObjectTypeContainerElement(ObjectType type, CompatibilityMode mode)
		{
			DdlUtil.ObjectTypeInformation objectTypeInformation;
			if (!DdlUtil.objectTypeToInfoMap.TryGetValue(type, out objectTypeInformation) || !objectTypeInformation.RestrictionSet.IsCompatible(mode))
			{
				throw TomInternalException.Create("Can't send Create/Alter/Delete requests for objects of type '{0}'. No matching container element found.", new object[] { type });
			}
			return new XElement(XmlaConstants.XNS.tmddl + objectTypeInformation.XmlElementName);
		}

		// Token: 0x06001465 RID: 5221 RVA: 0x0008489C File Offset: 0x00082A9C
		private static XName GetDDLRequestElement(DDLType ddlType)
		{
			switch (ddlType)
			{
			case DDLType.Create:
				return XmlaConstants.DDLConstants.Create;
			case DDLType.Alter:
				return XmlaConstants.DDLConstants.Alter;
			case DDLType.Delete:
				return XmlaConstants.DDLConstants.Delete;
			case DDLType.Refresh:
				return XmlaConstants.DDLConstants.Refresh;
			case DDLType.Rename:
				return XmlaConstants.DDLConstants.Rename;
			case DDLType.SequencePoint:
				return XmlaConstants.DDLConstants.SequencePoint;
			case DDLType.Upgrade:
				return XmlaConstants.DDLConstants.Upgrade;
			case DDLType.MergePartitions:
				return XmlaConstants.DDLConstants.MergePartitions;
			case DDLType.AnalyzeRefreshPolicyImpact:
				return XmlaConstants.DDLConstants.AnalyzeRefreshPolicyImpact;
			default:
				throw TomInternalException.Create("Unknown ddl request type {0}", new object[] { ddlType });
			}
		}

		// Token: 0x06001466 RID: 5222 RVA: 0x00084924 File Offset: 0x00082B24
		internal static IObjectRowsetSchema FormatSchema(ObjectType objectType, DDLRowsetType ddlType, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			ObjectRowsetSchema objectRowsetSchema = new ObjectRowsetSchema();
			switch (objectType)
			{
			case ObjectType.Model:
				switch (ddlType)
				{
				case DDLRowsetType.Create:
					objectRowsetSchema.AddXmlSchemaField("Name", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Description", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("StorageLocation", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("DefaultMode", "xs:long");
					objectRowsetSchema.AddXmlSchemaField("DefaultDataView", "xs:long");
					objectRowsetSchema.AddXmlSchemaField("Culture", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Collation", "xs:string");
					if (CompatibilityRestrictions.Model_DataAccessOptions.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("DataAccessOptions", "xs:string");
					}
					if (CompatibilityRestrictions.Model_DefaultMeasure.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("DefaultMeasureID", "xs:unsignedLong");
						objectRowsetSchema.AddXmlSchemaField("DefaultMeasureID.Table", "xs:string");
						objectRowsetSchema.AddXmlSchemaField("DefaultMeasureID.Measure", "xs:string");
					}
					if (CompatibilityRestrictions.Model_DefaultPowerBIDataSourceVersion.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("DefaultPowerBIDataSourceVersion", "xs:long");
					}
					if (CompatibilityRestrictions.Model_ForceUniqueNames.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("ForceUniqueNames", "xs:boolean");
					}
					if (CompatibilityRestrictions.Model_DiscourageImplicitMeasures.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("DiscourageImplicitMeasures", "xs:boolean");
					}
					if (CompatibilityRestrictions.Model_DiscourageReportMeasures.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("DiscourageReportMeasures", "xs:boolean");
					}
					if (CompatibilityRestrictions.Model_DataSourceVariablesOverrideBehavior.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("DataSourceVariablesOverrideBehavior", "xs:long");
					}
					if (CompatibilityRestrictions.Model_DataSourceDefaultMaxConnections.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("DataSourceDefaultMaxConnections", "xs:int");
					}
					if (CompatibilityRestrictions.Model_SourceQueryCulture.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("SourceQueryCulture", "xs:string");
					}
					if (CompatibilityRestrictions.Model_MAttributes.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("MAttributes", "xs:string");
					}
					if (CompatibilityRestrictions.Model_DiscourageCompositeModels.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("DiscourageCompositeModels", "xs:boolean");
					}
					if (CompatibilityRestrictions.Model_AutomaticAggregationOptions.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("AutomaticAggregationOptions", "xs:string");
					}
					if (CompatibilityRestrictions.Model_DisableAutoExists.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("DisableAutoExists", "xs:int");
					}
					if (CompatibilityRestrictions.Model_MaxParallelismPerRefresh.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("MaxParallelismPerRefresh", "xs:int");
					}
					if (CompatibilityRestrictions.Model_MaxParallelismPerQuery.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("MaxParallelismPerQuery", "xs:int");
					}
					if (CompatibilityRestrictions.Model_DisableSystemDefaultExpression.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("DisableSystemDefaultExpression", "xs:boolean");
					}
					if (CompatibilityRestrictions.Model_DirectLakeBehavior.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("DirectLakeBehavior", "xs:long");
					}
					if (CompatibilityRestrictions.Model_ValueFilterBehavior.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("ValueFilterBehavior", "xs:long");
						return objectRowsetSchema;
					}
					return objectRowsetSchema;
				case DDLRowsetType.Alter:
					objectRowsetSchema.AddXmlSchemaField("Name", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Description", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("StorageLocation", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("DefaultMode", "xs:long");
					objectRowsetSchema.AddXmlSchemaField("DefaultDataView", "xs:long");
					objectRowsetSchema.AddXmlSchemaField("Culture", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Collation", "xs:string");
					if (CompatibilityRestrictions.Model_DataAccessOptions.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("DataAccessOptions", "xs:string");
					}
					if (CompatibilityRestrictions.Model_DefaultMeasure.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("DefaultMeasureID", "xs:unsignedLong");
						objectRowsetSchema.AddXmlSchemaField("DefaultMeasureID.Table", "xs:string");
						objectRowsetSchema.AddXmlSchemaField("DefaultMeasureID.Measure", "xs:string");
					}
					if (CompatibilityRestrictions.Model_DefaultPowerBIDataSourceVersion.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("DefaultPowerBIDataSourceVersion", "xs:long");
					}
					if (CompatibilityRestrictions.Model_ForceUniqueNames.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("ForceUniqueNames", "xs:boolean");
					}
					if (CompatibilityRestrictions.Model_DiscourageImplicitMeasures.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("DiscourageImplicitMeasures", "xs:boolean");
					}
					if (CompatibilityRestrictions.Model_DiscourageReportMeasures.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("DiscourageReportMeasures", "xs:boolean");
					}
					if (CompatibilityRestrictions.Model_DataSourceVariablesOverrideBehavior.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("DataSourceVariablesOverrideBehavior", "xs:long");
					}
					if (CompatibilityRestrictions.Model_DataSourceDefaultMaxConnections.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("DataSourceDefaultMaxConnections", "xs:int");
					}
					if (CompatibilityRestrictions.Model_SourceQueryCulture.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("SourceQueryCulture", "xs:string");
					}
					if (CompatibilityRestrictions.Model_MAttributes.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("MAttributes", "xs:string");
					}
					if (CompatibilityRestrictions.Model_DiscourageCompositeModels.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("DiscourageCompositeModels", "xs:boolean");
					}
					if (CompatibilityRestrictions.Model_AutomaticAggregationOptions.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("AutomaticAggregationOptions", "xs:string");
					}
					if (CompatibilityRestrictions.Model_DisableAutoExists.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("DisableAutoExists", "xs:int");
					}
					if (CompatibilityRestrictions.Model_MaxParallelismPerRefresh.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("MaxParallelismPerRefresh", "xs:int");
					}
					if (CompatibilityRestrictions.Model_MaxParallelismPerQuery.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("MaxParallelismPerQuery", "xs:int");
					}
					if (CompatibilityRestrictions.Model_DisableSystemDefaultExpression.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("DisableSystemDefaultExpression", "xs:boolean");
					}
					if (CompatibilityRestrictions.Model_DirectLakeBehavior.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("DirectLakeBehavior", "xs:long");
					}
					if (CompatibilityRestrictions.Model_ValueFilterBehavior.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("ValueFilterBehavior", "xs:long");
						return objectRowsetSchema;
					}
					return objectRowsetSchema;
				case DDLRowsetType.Delete:
					return objectRowsetSchema;
				case DDLRowsetType.Refresh:
					objectRowsetSchema.AddXmlSchemaField("RefreshType", "xs:long");
					return objectRowsetSchema;
				case DDLRowsetType.Rename:
					objectRowsetSchema.AddXmlSchemaField("Name", "xs:string");
					return objectRowsetSchema;
				default:
					throw TomInternalException.Create("Invalid DDL type {0}", new object[] { ddlType });
				}
				break;
			case ObjectType.DataSource:
				switch (ddlType)
				{
				case DDLRowsetType.Create:
					objectRowsetSchema.AddXmlSchemaField("Name", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Description", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Type", "xs:long");
					objectRowsetSchema.AddXmlSchemaField("ConnectionString", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ImpersonationMode", "xs:long");
					objectRowsetSchema.AddXmlSchemaField("Account", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Password", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("MaxConnections", "xs:int");
					objectRowsetSchema.AddXmlSchemaField("Isolation", "xs:long");
					objectRowsetSchema.AddXmlSchemaField("Timeout", "xs:int");
					objectRowsetSchema.AddXmlSchemaField("Provider", "xs:string");
					if (CompatibilityRestrictions.DataSource_ConnectionDetails.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("ConnectionDetails", "xs:string");
					}
					if (CompatibilityRestrictions.DataSource_Options.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("Options", "xs:string");
					}
					if (CompatibilityRestrictions.DataSource_Credential.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("Credential", "xs:string");
					}
					if (CompatibilityRestrictions.DataSource_ContextExpression.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("ContextExpression", "xs:string");
						return objectRowsetSchema;
					}
					return objectRowsetSchema;
				case DDLRowsetType.Alter:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.DataSource", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Name", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Description", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ConnectionString", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ImpersonationMode", "xs:long");
					objectRowsetSchema.AddXmlSchemaField("Account", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Password", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("MaxConnections", "xs:int");
					objectRowsetSchema.AddXmlSchemaField("Isolation", "xs:long");
					objectRowsetSchema.AddXmlSchemaField("Timeout", "xs:int");
					objectRowsetSchema.AddXmlSchemaField("Provider", "xs:string");
					if (CompatibilityRestrictions.DataSource_ConnectionDetails.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("ConnectionDetails", "xs:string");
					}
					if (CompatibilityRestrictions.DataSource_Options.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("Options", "xs:string");
					}
					if (CompatibilityRestrictions.DataSource_Credential.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("Credential", "xs:string");
					}
					if (CompatibilityRestrictions.DataSource_ContextExpression.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("ContextExpression", "xs:string");
						return objectRowsetSchema;
					}
					return objectRowsetSchema;
				case DDLRowsetType.Delete:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.DataSource", "xs:string");
					return objectRowsetSchema;
				case DDLRowsetType.Rename:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.DataSource", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Name", "xs:string");
					return objectRowsetSchema;
				case DDLRowsetType.Bindings:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.DataSource", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ConnectionString", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ImpersonationMode", "xs:long");
					objectRowsetSchema.AddXmlSchemaField("Account", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Password", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("MaxConnections", "xs:int");
					objectRowsetSchema.AddXmlSchemaField("Isolation", "xs:long");
					objectRowsetSchema.AddXmlSchemaField("Timeout", "xs:int");
					objectRowsetSchema.AddXmlSchemaField("Provider", "xs:string");
					if (CompatibilityRestrictions.DataSource_ConnectionDetails.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("ConnectionDetails", "xs:string");
					}
					if (CompatibilityRestrictions.DataSource_Options.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("Options", "xs:string");
					}
					if (CompatibilityRestrictions.DataSource_Credential.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("Credential", "xs:string");
					}
					if (CompatibilityRestrictions.DataSource_ContextExpression.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("ContextExpression", "xs:string");
						return objectRowsetSchema;
					}
					return objectRowsetSchema;
				}
				throw TomInternalException.Create("Invalid DDL type {0}", new object[] { ddlType });
			case ObjectType.Table:
				switch (ddlType)
				{
				case DDLRowsetType.Create:
					objectRowsetSchema.AddXmlSchemaField("Name", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("DataCategory", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Description", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("IsHidden", "xs:boolean");
					if (CompatibilityRestrictions.Table_ShowAsVariationsOnly.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("ShowAsVariationsOnly", "xs:boolean");
					}
					if (CompatibilityRestrictions.Table_IsPrivate.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("IsPrivate", "xs:boolean");
					}
					if (CompatibilityRestrictions.Table_AlternateSourcePrecedence.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("AlternateSourcePrecedence", "xs:int");
					}
					if (CompatibilityRestrictions.Table_ExcludeFromModelRefresh.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("ExcludeFromModelRefresh", "xs:boolean");
					}
					if (CompatibilityRestrictions.Table_LineageTag.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("LineageTag", "xs:string");
					}
					if (CompatibilityRestrictions.Table_SourceLineageTag.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("SourceLineageTag", "xs:string");
					}
					if (CompatibilityRestrictions.Table_SystemManaged.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("SystemManaged", "xs:boolean");
					}
					if (CompatibilityRestrictions.Table_ExcludeFromAutomaticAggregations.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("ExcludeFromAutomaticAggregations", "xs:boolean");
						return objectRowsetSchema;
					}
					return objectRowsetSchema;
				case DDLRowsetType.Alter:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Name", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("DataCategory", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Description", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("IsHidden", "xs:boolean");
					if (CompatibilityRestrictions.Table_ShowAsVariationsOnly.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("ShowAsVariationsOnly", "xs:boolean");
					}
					if (CompatibilityRestrictions.Table_IsPrivate.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("IsPrivate", "xs:boolean");
					}
					if (CompatibilityRestrictions.Table_AlternateSourcePrecedence.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("AlternateSourcePrecedence", "xs:int");
					}
					if (CompatibilityRestrictions.Table_ExcludeFromModelRefresh.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("ExcludeFromModelRefresh", "xs:boolean");
					}
					if (CompatibilityRestrictions.Table_LineageTag.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("LineageTag", "xs:string");
					}
					if (CompatibilityRestrictions.Table_SourceLineageTag.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("SourceLineageTag", "xs:string");
					}
					if (CompatibilityRestrictions.Table_SystemManaged.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("SystemManaged", "xs:boolean");
					}
					if (CompatibilityRestrictions.Table_ExcludeFromAutomaticAggregations.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("ExcludeFromAutomaticAggregations", "xs:boolean");
						return objectRowsetSchema;
					}
					return objectRowsetSchema;
				case DDLRowsetType.Delete:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Table", "xs:string");
					return objectRowsetSchema;
				case DDLRowsetType.Refresh:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("RefreshType", "xs:long");
					return objectRowsetSchema;
				case DDLRowsetType.Rename:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Name", "xs:string");
					return objectRowsetSchema;
				default:
					throw TomInternalException.Create("Invalid DDL type {0}", new object[] { ddlType });
				}
				break;
			case ObjectType.Column:
				switch (ddlType)
				{
				case DDLRowsetType.Create:
					objectRowsetSchema.AddXmlSchemaField("TableID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("TableID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ExplicitName", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("InferredName", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ExplicitDataType", "xs:long");
					objectRowsetSchema.AddXmlSchemaField("DataCategory", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Description", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("IsHidden", "xs:boolean");
					objectRowsetSchema.AddXmlSchemaField("IsUnique", "xs:boolean");
					objectRowsetSchema.AddXmlSchemaField("IsKey", "xs:boolean");
					objectRowsetSchema.AddXmlSchemaField("IsNullable", "xs:boolean");
					objectRowsetSchema.AddXmlSchemaField("Alignment", "xs:long");
					objectRowsetSchema.AddXmlSchemaField("TableDetailPosition", "xs:int");
					objectRowsetSchema.AddXmlSchemaField("IsDefaultLabel", "xs:boolean");
					objectRowsetSchema.AddXmlSchemaField("IsDefaultImage", "xs:boolean");
					objectRowsetSchema.AddXmlSchemaField("SummarizeBy", "xs:long");
					objectRowsetSchema.AddXmlSchemaField("Type", "xs:long");
					objectRowsetSchema.AddXmlSchemaField("SourceColumn", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Expression", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("FormatString", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("IsAvailableInMDX", "xs:boolean");
					objectRowsetSchema.AddXmlSchemaField("SortByColumnID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("SortByColumnID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("SortByColumnID.Column", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("KeepUniqueRows", "xs:boolean");
					objectRowsetSchema.AddXmlSchemaField("DisplayOrdinal", "xs:int");
					objectRowsetSchema.AddXmlSchemaField("SourceProviderType", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("DisplayFolder", "xs:string");
					if (CompatibilityRestrictions.Column_EncodingHint.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("EncodingHint", "xs:long");
					}
					if (CompatibilityRestrictions.Column_LineageTag.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("LineageTag", "xs:string");
					}
					if (CompatibilityRestrictions.Column_SourceLineageTag.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("SourceLineageTag", "xs:string");
					}
					if (CompatibilityRestrictions.Column_EvaluationBehavior.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("EvaluationBehavior", "xs:long");
						return objectRowsetSchema;
					}
					return objectRowsetSchema;
				case DDLRowsetType.Alter:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ID.Column", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ExplicitName", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ExplicitDataType", "xs:long");
					objectRowsetSchema.AddXmlSchemaField("DataCategory", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Description", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("IsHidden", "xs:boolean");
					objectRowsetSchema.AddXmlSchemaField("IsUnique", "xs:boolean");
					objectRowsetSchema.AddXmlSchemaField("IsKey", "xs:boolean");
					objectRowsetSchema.AddXmlSchemaField("IsNullable", "xs:boolean");
					objectRowsetSchema.AddXmlSchemaField("Alignment", "xs:long");
					objectRowsetSchema.AddXmlSchemaField("TableDetailPosition", "xs:int");
					objectRowsetSchema.AddXmlSchemaField("IsDefaultLabel", "xs:boolean");
					objectRowsetSchema.AddXmlSchemaField("IsDefaultImage", "xs:boolean");
					objectRowsetSchema.AddXmlSchemaField("SummarizeBy", "xs:long");
					objectRowsetSchema.AddXmlSchemaField("SourceColumn", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Expression", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("FormatString", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("IsAvailableInMDX", "xs:boolean");
					objectRowsetSchema.AddXmlSchemaField("SortByColumnID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("SortByColumnID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("SortByColumnID.Column", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("KeepUniqueRows", "xs:boolean");
					objectRowsetSchema.AddXmlSchemaField("DisplayOrdinal", "xs:int");
					objectRowsetSchema.AddXmlSchemaField("SourceProviderType", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("DisplayFolder", "xs:string");
					if (CompatibilityRestrictions.Column_EncodingHint.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("EncodingHint", "xs:long");
					}
					if (CompatibilityRestrictions.Column_LineageTag.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("LineageTag", "xs:string");
					}
					if (CompatibilityRestrictions.Column_SourceLineageTag.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("SourceLineageTag", "xs:string");
					}
					if (CompatibilityRestrictions.Column_EvaluationBehavior.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("EvaluationBehavior", "xs:long");
						return objectRowsetSchema;
					}
					return objectRowsetSchema;
				case DDLRowsetType.Delete:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ID.Column", "xs:string");
					return objectRowsetSchema;
				case DDLRowsetType.Rename:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ID.Column", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ExplicitName", "xs:string");
					return objectRowsetSchema;
				case DDLRowsetType.Bindings:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ID.Column", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("SourceColumn", "xs:string");
					return objectRowsetSchema;
				}
				throw TomInternalException.Create("Invalid DDL type {0}", new object[] { ddlType });
			case ObjectType.Partition:
				switch (ddlType)
				{
				case DDLRowsetType.Create:
					objectRowsetSchema.AddXmlSchemaField("TableID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("TableID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Name", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Description", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("DataSourceID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("DataSourceID.DataSource", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("QueryDefinition", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Type", "xs:long");
					objectRowsetSchema.AddXmlSchemaField("Mode", "xs:long");
					objectRowsetSchema.AddXmlSchemaField("DataView", "xs:long");
					if (CompatibilityRestrictions.Partition_RetainDataTillForceCalculate.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("RetainDataTillForceCalculate", "xs:boolean");
					}
					if (CompatibilityRestrictions.Partition_RangeStart.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("RangeStart", "xs:dateTime");
					}
					if (CompatibilityRestrictions.Partition_RangeEnd.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("RangeEnd", "xs:dateTime");
					}
					if (CompatibilityRestrictions.Partition_RangeGranularity.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("RangeGranularity", "xs:long");
					}
					if (CompatibilityRestrictions.Partition_RefreshBookmark.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("RefreshBookmark", "xs:string");
					}
					if (CompatibilityRestrictions.Partition_QueryGroup.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("QueryGroupID", "xs:unsignedLong");
						objectRowsetSchema.AddXmlSchemaField("QueryGroupID.QueryGroup", "xs:string");
					}
					if (CompatibilityRestrictions.Partition_ExpressionSource.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("ExpressionSourceID", "xs:unsignedLong");
						objectRowsetSchema.AddXmlSchemaField("ExpressionSourceID.Expression", "xs:string");
					}
					if (CompatibilityRestrictions.Partition_MAttributes.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("MAttributes", "xs:string");
					}
					if (CompatibilityRestrictions.Partition_SchemaName.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("SchemaName", "xs:string");
						return objectRowsetSchema;
					}
					return objectRowsetSchema;
				case DDLRowsetType.Alter:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ID.Partition", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Name", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Description", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("DataSourceID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("DataSourceID.DataSource", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("QueryDefinition", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Type", "xs:long");
					objectRowsetSchema.AddXmlSchemaField("Mode", "xs:long");
					objectRowsetSchema.AddXmlSchemaField("DataView", "xs:long");
					if (CompatibilityRestrictions.Partition_RangeStart.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("RangeStart", "xs:dateTime");
					}
					if (CompatibilityRestrictions.Partition_RangeEnd.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("RangeEnd", "xs:dateTime");
					}
					if (CompatibilityRestrictions.Partition_RangeGranularity.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("RangeGranularity", "xs:long");
					}
					if (CompatibilityRestrictions.Partition_RefreshBookmark.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("RefreshBookmark", "xs:string");
					}
					if (CompatibilityRestrictions.Partition_QueryGroup.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("QueryGroupID", "xs:unsignedLong");
						objectRowsetSchema.AddXmlSchemaField("QueryGroupID.QueryGroup", "xs:string");
					}
					if (CompatibilityRestrictions.Partition_ExpressionSource.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("ExpressionSourceID", "xs:unsignedLong");
						objectRowsetSchema.AddXmlSchemaField("ExpressionSourceID.Expression", "xs:string");
					}
					if (CompatibilityRestrictions.Partition_MAttributes.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("MAttributes", "xs:string");
					}
					if (CompatibilityRestrictions.Partition_SchemaName.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("SchemaName", "xs:string");
						return objectRowsetSchema;
					}
					return objectRowsetSchema;
				case DDLRowsetType.Delete:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ID.Partition", "xs:string");
					return objectRowsetSchema;
				case DDLRowsetType.Refresh:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ID.Partition", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("RefreshType", "xs:long");
					return objectRowsetSchema;
				case DDLRowsetType.Rename:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ID.Partition", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Name", "xs:string");
					return objectRowsetSchema;
				case DDLRowsetType.Bindings:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ID.Partition", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("DataSourceID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("DataSourceID.DataSource", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("QueryDefinition", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Type", "xs:long");
					if (CompatibilityRestrictions.Partition_ExpressionSource.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("ExpressionSourceID", "xs:unsignedLong");
						objectRowsetSchema.AddXmlSchemaField("ExpressionSourceID.Expression", "xs:string");
						return objectRowsetSchema;
					}
					return objectRowsetSchema;
				case DDLRowsetType.MergePartitions:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ID.Partition", "xs:string");
					return objectRowsetSchema;
				case DDLRowsetType.AnalyzeRefreshPolicyImpact:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ID.Partition", "xs:string");
					return objectRowsetSchema;
				}
				throw TomInternalException.Create("Invalid DDL type {0}", new object[] { ddlType });
			case ObjectType.Relationship:
				switch (ddlType)
				{
				case DDLRowsetType.Create:
					objectRowsetSchema.AddXmlSchemaField("Name", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("IsActive", "xs:boolean");
					objectRowsetSchema.AddXmlSchemaField("Type", "xs:long");
					objectRowsetSchema.AddXmlSchemaField("CrossFilteringBehavior", "xs:long");
					objectRowsetSchema.AddXmlSchemaField("JoinOnDateBehavior", "xs:long");
					objectRowsetSchema.AddXmlSchemaField("RelyOnReferentialIntegrity", "xs:boolean");
					objectRowsetSchema.AddXmlSchemaField("FromTableID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("FromTableID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("FromColumnID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("FromColumnID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("FromColumnID.Column", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("FromCardinality", "xs:long");
					objectRowsetSchema.AddXmlSchemaField("ToTableID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ToTableID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ToColumnID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ToColumnID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ToColumnID.Column", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ToCardinality", "xs:long");
					objectRowsetSchema.AddXmlSchemaField("SecurityFilteringBehavior", "xs:long");
					return objectRowsetSchema;
				case DDLRowsetType.Alter:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Relationship", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Name", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("IsActive", "xs:boolean");
					objectRowsetSchema.AddXmlSchemaField("Type", "xs:long");
					objectRowsetSchema.AddXmlSchemaField("CrossFilteringBehavior", "xs:long");
					objectRowsetSchema.AddXmlSchemaField("JoinOnDateBehavior", "xs:long");
					objectRowsetSchema.AddXmlSchemaField("RelyOnReferentialIntegrity", "xs:boolean");
					objectRowsetSchema.AddXmlSchemaField("FromTableID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("FromTableID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("FromColumnID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("FromColumnID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("FromColumnID.Column", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("FromCardinality", "xs:long");
					objectRowsetSchema.AddXmlSchemaField("ToTableID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ToTableID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ToColumnID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ToColumnID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ToColumnID.Column", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ToCardinality", "xs:long");
					objectRowsetSchema.AddXmlSchemaField("SecurityFilteringBehavior", "xs:long");
					return objectRowsetSchema;
				case DDLRowsetType.Delete:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Relationship", "xs:string");
					return objectRowsetSchema;
				case DDLRowsetType.Rename:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Relationship", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Name", "xs:string");
					return objectRowsetSchema;
				}
				throw TomInternalException.Create("Invalid DDL type {0}", new object[] { ddlType });
			case ObjectType.Measure:
				switch (ddlType)
				{
				case DDLRowsetType.Create:
					objectRowsetSchema.AddXmlSchemaField("TableID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("TableID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Name", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Description", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Expression", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("FormatString", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("IsHidden", "xs:boolean");
					objectRowsetSchema.AddXmlSchemaField("IsSimpleMeasure", "xs:boolean");
					objectRowsetSchema.AddXmlSchemaField("DisplayFolder", "xs:string");
					if (CompatibilityRestrictions.Measure_DataCategory.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("DataCategory", "xs:string");
					}
					if (CompatibilityRestrictions.Measure_LineageTag.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("LineageTag", "xs:string");
					}
					if (CompatibilityRestrictions.Measure_SourceLineageTag.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("SourceLineageTag", "xs:string");
						return objectRowsetSchema;
					}
					return objectRowsetSchema;
				case DDLRowsetType.Alter:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ID.Measure", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Name", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Description", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Expression", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("FormatString", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("IsHidden", "xs:boolean");
					objectRowsetSchema.AddXmlSchemaField("IsSimpleMeasure", "xs:boolean");
					objectRowsetSchema.AddXmlSchemaField("DisplayFolder", "xs:string");
					if (CompatibilityRestrictions.Measure_DataCategory.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("DataCategory", "xs:string");
					}
					if (CompatibilityRestrictions.Measure_LineageTag.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("LineageTag", "xs:string");
					}
					if (CompatibilityRestrictions.Measure_SourceLineageTag.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("SourceLineageTag", "xs:string");
						return objectRowsetSchema;
					}
					return objectRowsetSchema;
				case DDLRowsetType.Delete:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ID.Measure", "xs:string");
					return objectRowsetSchema;
				case DDLRowsetType.Rename:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ID.Measure", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Name", "xs:string");
					return objectRowsetSchema;
				}
				throw TomInternalException.Create("Invalid DDL type {0}", new object[] { ddlType });
			case ObjectType.Hierarchy:
				switch (ddlType)
				{
				case DDLRowsetType.Create:
					objectRowsetSchema.AddXmlSchemaField("TableID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("TableID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Name", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Description", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("IsHidden", "xs:boolean");
					objectRowsetSchema.AddXmlSchemaField("DisplayFolder", "xs:string");
					if (CompatibilityRestrictions.Hierarchy_HideMembers.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("HideMembers", "xs:long");
					}
					if (CompatibilityRestrictions.Hierarchy_LineageTag.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("LineageTag", "xs:string");
					}
					if (CompatibilityRestrictions.Hierarchy_SourceLineageTag.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("SourceLineageTag", "xs:string");
						return objectRowsetSchema;
					}
					return objectRowsetSchema;
				case DDLRowsetType.Alter:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ID.Hierarchy", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Name", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Description", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("IsHidden", "xs:boolean");
					objectRowsetSchema.AddXmlSchemaField("DisplayFolder", "xs:string");
					if (CompatibilityRestrictions.Hierarchy_HideMembers.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("HideMembers", "xs:long");
					}
					if (CompatibilityRestrictions.Hierarchy_LineageTag.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("LineageTag", "xs:string");
					}
					if (CompatibilityRestrictions.Hierarchy_SourceLineageTag.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("SourceLineageTag", "xs:string");
						return objectRowsetSchema;
					}
					return objectRowsetSchema;
				case DDLRowsetType.Delete:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ID.Hierarchy", "xs:string");
					return objectRowsetSchema;
				case DDLRowsetType.Rename:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ID.Hierarchy", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Name", "xs:string");
					return objectRowsetSchema;
				}
				throw TomInternalException.Create("Invalid DDL type {0}", new object[] { ddlType });
			case ObjectType.Level:
				switch (ddlType)
				{
				case DDLRowsetType.Create:
					objectRowsetSchema.AddXmlSchemaField("HierarchyID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("HierarchyID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("HierarchyID.Hierarchy", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Ordinal", "xs:int");
					objectRowsetSchema.AddXmlSchemaField("Name", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Description", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ColumnID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ColumnID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ColumnID.Column", "xs:string");
					if (CompatibilityRestrictions.Level_LineageTag.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("LineageTag", "xs:string");
					}
					if (CompatibilityRestrictions.Level_SourceLineageTag.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("SourceLineageTag", "xs:string");
						return objectRowsetSchema;
					}
					return objectRowsetSchema;
				case DDLRowsetType.Alter:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ID.Hierarchy", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ID.Level", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Ordinal", "xs:int");
					objectRowsetSchema.AddXmlSchemaField("Name", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Description", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ColumnID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ColumnID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ColumnID.Column", "xs:string");
					if (CompatibilityRestrictions.Level_LineageTag.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("LineageTag", "xs:string");
					}
					if (CompatibilityRestrictions.Level_SourceLineageTag.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("SourceLineageTag", "xs:string");
						return objectRowsetSchema;
					}
					return objectRowsetSchema;
				case DDLRowsetType.Delete:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ID.Hierarchy", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ID.Level", "xs:string");
					return objectRowsetSchema;
				case DDLRowsetType.Rename:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ID.Hierarchy", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ID.Level", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Name", "xs:string");
					return objectRowsetSchema;
				}
				throw TomInternalException.Create("Invalid DDL type {0}", new object[] { ddlType });
			case ObjectType.Annotation:
				switch (ddlType)
				{
				case DDLRowsetType.Create:
					objectRowsetSchema.AddXmlSchemaField("ObjectID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ObjectID.DataSource", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ObjectID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ObjectID.Column", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ObjectID.Partition", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ObjectID.Relationship", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ObjectID.Measure", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ObjectID.Hierarchy", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ObjectID.Level", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ObjectID.Culture", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ObjectID.Perspective", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ObjectID.PerspectiveTable", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ObjectID.PerspectiveColumn", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ObjectID.PerspectiveHierarchy", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ObjectID.PerspectiveMeasure", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ObjectID.Role", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ObjectID.RoleMembership", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ObjectID.TablePermission", "xs:string");
					if (CompatibilityRestrictions.Variation.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("ObjectID.Variation", "xs:string");
					}
					if (CompatibilityRestrictions.Set.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("ObjectID.Set", "xs:string");
					}
					if (CompatibilityRestrictions.PerspectiveSet.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("ObjectID.PerspectiveSet", "xs:string");
					}
					if (CompatibilityRestrictions.NamedExpression.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("ObjectID.Expression", "xs:string");
					}
					if (CompatibilityRestrictions.ColumnPermission.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("ObjectID.ColumnPermission", "xs:string");
					}
					if (CompatibilityRestrictions.QueryGroup.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("ObjectID.QueryGroup", "xs:string");
					}
					if (CompatibilityRestrictions.Function.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("ObjectID.Function", "xs:string");
					}
					if (CompatibilityRestrictions.BindingInfo.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("ObjectID.BindingInfo", "xs:string");
					}
					objectRowsetSchema.AddXmlSchemaField("ObjectType", "xs:int");
					objectRowsetSchema.AddXmlSchemaField("Name", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Value", "xs:string");
					return objectRowsetSchema;
				case DDLRowsetType.Alter:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("Name", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Value", "xs:string");
					return objectRowsetSchema;
				case DDLRowsetType.Delete:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					return objectRowsetSchema;
				case DDLRowsetType.Rename:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("Name", "xs:string");
					return objectRowsetSchema;
				}
				throw TomInternalException.Create("Invalid DDL type {0}", new object[] { ddlType });
			case ObjectType.KPI:
				switch (ddlType)
				{
				case DDLRowsetType.Create:
					objectRowsetSchema.AddXmlSchemaField("MeasureID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("MeasureID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("MeasureID.Measure", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Description", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("TargetDescription", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("TargetExpression", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("TargetFormatString", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("StatusGraphic", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("StatusDescription", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("StatusExpression", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("TrendGraphic", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("TrendDescription", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("TrendExpression", "xs:string");
					return objectRowsetSchema;
				case DDLRowsetType.Alter:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ID.Measure", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ID.KPI", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Description", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("TargetDescription", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("TargetExpression", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("TargetFormatString", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("StatusGraphic", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("StatusDescription", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("StatusExpression", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("TrendGraphic", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("TrendDescription", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("TrendExpression", "xs:string");
					return objectRowsetSchema;
				case DDLRowsetType.Delete:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ID.Measure", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ID.KPI", "xs:string");
					return objectRowsetSchema;
				default:
					throw TomInternalException.Create("Invalid DDL type {0}", new object[] { ddlType });
				}
				break;
			case ObjectType.Culture:
				switch (ddlType)
				{
				case DDLRowsetType.Create:
					objectRowsetSchema.AddXmlSchemaField("Name", "xs:string");
					return objectRowsetSchema;
				case DDLRowsetType.Alter:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Culture", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Name", "xs:string");
					return objectRowsetSchema;
				case DDLRowsetType.Delete:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Culture", "xs:string");
					return objectRowsetSchema;
				case DDLRowsetType.Rename:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Culture", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Name", "xs:string");
					return objectRowsetSchema;
				}
				throw TomInternalException.Create("Invalid DDL type {0}", new object[] { ddlType });
			case ObjectType.ObjectTranslation:
				switch (ddlType)
				{
				case DDLRowsetType.Create:
					objectRowsetSchema.AddXmlSchemaField("CultureID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("CultureID.Culture", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ObjectID", "xs:unsignedLong");
					if (dbCompatibilityLevel == 1200)
					{
						objectRowsetSchema.AddXmlSchemaField("ObjectID.DataSource", "xs:string");
						objectRowsetSchema.AddXmlSchemaField("ObjectID.Table", "xs:string");
						objectRowsetSchema.AddXmlSchemaField("ObjectID.Column", "xs:string");
						objectRowsetSchema.AddXmlSchemaField("ObjectID.Partition", "xs:string");
						objectRowsetSchema.AddXmlSchemaField("ObjectID.Relationship", "xs:string");
						objectRowsetSchema.AddXmlSchemaField("ObjectID.Measure", "xs:string");
						objectRowsetSchema.AddXmlSchemaField("ObjectID.Hierarchy", "xs:string");
						objectRowsetSchema.AddXmlSchemaField("ObjectID.Level", "xs:string");
						objectRowsetSchema.AddXmlSchemaField("ObjectID.Culture", "xs:string");
						objectRowsetSchema.AddXmlSchemaField("ObjectID.Perspective", "xs:string");
						objectRowsetSchema.AddXmlSchemaField("ObjectID.PerspectiveTable", "xs:string");
						objectRowsetSchema.AddXmlSchemaField("ObjectID.PerspectiveColumn", "xs:string");
						objectRowsetSchema.AddXmlSchemaField("ObjectID.PerspectiveHierarchy", "xs:string");
						objectRowsetSchema.AddXmlSchemaField("ObjectID.PerspectiveMeasure", "xs:string");
						objectRowsetSchema.AddXmlSchemaField("ObjectID.Role", "xs:string");
						objectRowsetSchema.AddXmlSchemaField("ObjectID.RoleMembership", "xs:string");
						objectRowsetSchema.AddXmlSchemaField("ObjectID.TablePermission", "xs:string");
						if (CompatibilityRestrictions.Variation.IsCompatible(mode, dbCompatibilityLevel))
						{
							objectRowsetSchema.AddXmlSchemaField("ObjectID.Variation", "xs:string");
						}
					}
					else
					{
						objectRowsetSchema.AddXmlSchemaField("ObjectID.Table", "xs:string");
						objectRowsetSchema.AddXmlSchemaField("ObjectID.Column", "xs:string");
						objectRowsetSchema.AddXmlSchemaField("ObjectID.Measure", "xs:string");
						objectRowsetSchema.AddXmlSchemaField("ObjectID.Hierarchy", "xs:string");
						objectRowsetSchema.AddXmlSchemaField("ObjectID.Level", "xs:string");
						objectRowsetSchema.AddXmlSchemaField("ObjectID.Perspective", "xs:string");
						objectRowsetSchema.AddXmlSchemaField("ObjectID.Role", "xs:string");
						if (CompatibilityRestrictions.Variation.IsCompatible(mode, dbCompatibilityLevel))
						{
							objectRowsetSchema.AddXmlSchemaField("ObjectID.Variation", "xs:string");
						}
						if (CompatibilityRestrictions.Set.IsCompatible(mode, dbCompatibilityLevel))
						{
							objectRowsetSchema.AddXmlSchemaField("ObjectID.Set", "xs:string");
						}
						if (CompatibilityRestrictions.NamedExpression.IsCompatible(mode, dbCompatibilityLevel))
						{
							objectRowsetSchema.AddXmlSchemaField("ObjectID.Expression", "xs:string");
						}
					}
					objectRowsetSchema.AddXmlSchemaField("ObjectType", "xs:int");
					objectRowsetSchema.AddXmlSchemaField("Property", "xs:long");
					objectRowsetSchema.AddXmlSchemaField("Value", "xs:string");
					if (CompatibilityRestrictions.ObjectTranslation_Altered.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("Altered", "xs:boolean");
						return objectRowsetSchema;
					}
					return objectRowsetSchema;
				case DDLRowsetType.Alter:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("Value", "xs:string");
					if (CompatibilityRestrictions.ObjectTranslation_Altered.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("Altered", "xs:boolean");
						return objectRowsetSchema;
					}
					return objectRowsetSchema;
				case DDLRowsetType.Delete:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					return objectRowsetSchema;
				default:
					throw TomInternalException.Create("Invalid DDL type {0}", new object[] { ddlType });
				}
				break;
			case ObjectType.LinguisticMetadata:
				switch (ddlType)
				{
				case DDLRowsetType.Create:
					objectRowsetSchema.AddXmlSchemaField("CultureID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("CultureID.Culture", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Content", "xs:string");
					if (CompatibilityRestrictions.LinguisticMetadata_ContentType.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("ContentType", "xs:long");
						return objectRowsetSchema;
					}
					return objectRowsetSchema;
				case DDLRowsetType.Alter:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Culture", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ID.LinguisticMetadata", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Content", "xs:string");
					if (CompatibilityRestrictions.LinguisticMetadata_ContentType.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("ContentType", "xs:long");
						return objectRowsetSchema;
					}
					return objectRowsetSchema;
				case DDLRowsetType.Delete:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Culture", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ID.LinguisticMetadata", "xs:string");
					return objectRowsetSchema;
				default:
					throw TomInternalException.Create("Invalid DDL type {0}", new object[] { ddlType });
				}
				break;
			case ObjectType.Perspective:
				switch (ddlType)
				{
				case DDLRowsetType.Create:
					objectRowsetSchema.AddXmlSchemaField("Name", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Description", "xs:string");
					return objectRowsetSchema;
				case DDLRowsetType.Alter:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Perspective", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Name", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Description", "xs:string");
					return objectRowsetSchema;
				case DDLRowsetType.Delete:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Perspective", "xs:string");
					return objectRowsetSchema;
				case DDLRowsetType.Rename:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Perspective", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Name", "xs:string");
					return objectRowsetSchema;
				}
				throw TomInternalException.Create("Invalid DDL type {0}", new object[] { ddlType });
			case ObjectType.PerspectiveTable:
				switch (ddlType)
				{
				case DDLRowsetType.Create:
					objectRowsetSchema.AddXmlSchemaField("PerspectiveID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("PerspectiveID.Perspective", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("TableID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("TableID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("IncludeAll", "xs:boolean");
					return objectRowsetSchema;
				case DDLRowsetType.Alter:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Perspective", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ID.PerspectiveTable", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("TableID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("TableID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("IncludeAll", "xs:boolean");
					return objectRowsetSchema;
				case DDLRowsetType.Delete:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Perspective", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ID.PerspectiveTable", "xs:string");
					return objectRowsetSchema;
				default:
					throw TomInternalException.Create("Invalid DDL type {0}", new object[] { ddlType });
				}
				break;
			case ObjectType.PerspectiveColumn:
				switch (ddlType)
				{
				case DDLRowsetType.Create:
					objectRowsetSchema.AddXmlSchemaField("PerspectiveTableID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("PerspectiveTableID.Perspective", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("PerspectiveTableID.PerspectiveTable", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ColumnID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ColumnID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ColumnID.Column", "xs:string");
					return objectRowsetSchema;
				case DDLRowsetType.Alter:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Perspective", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ID.PerspectiveTable", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ID.PerspectiveColumn", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ColumnID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ColumnID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ColumnID.Column", "xs:string");
					return objectRowsetSchema;
				case DDLRowsetType.Delete:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Perspective", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ID.PerspectiveTable", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ID.PerspectiveColumn", "xs:string");
					return objectRowsetSchema;
				default:
					throw TomInternalException.Create("Invalid DDL type {0}", new object[] { ddlType });
				}
				break;
			case ObjectType.PerspectiveHierarchy:
				switch (ddlType)
				{
				case DDLRowsetType.Create:
					objectRowsetSchema.AddXmlSchemaField("PerspectiveTableID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("PerspectiveTableID.Perspective", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("PerspectiveTableID.PerspectiveTable", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("HierarchyID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("HierarchyID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("HierarchyID.Hierarchy", "xs:string");
					return objectRowsetSchema;
				case DDLRowsetType.Alter:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Perspective", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ID.PerspectiveTable", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ID.PerspectiveHierarchy", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("HierarchyID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("HierarchyID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("HierarchyID.Hierarchy", "xs:string");
					return objectRowsetSchema;
				case DDLRowsetType.Delete:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Perspective", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ID.PerspectiveTable", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ID.PerspectiveHierarchy", "xs:string");
					return objectRowsetSchema;
				default:
					throw TomInternalException.Create("Invalid DDL type {0}", new object[] { ddlType });
				}
				break;
			case ObjectType.PerspectiveMeasure:
				switch (ddlType)
				{
				case DDLRowsetType.Create:
					objectRowsetSchema.AddXmlSchemaField("PerspectiveTableID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("PerspectiveTableID.Perspective", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("PerspectiveTableID.PerspectiveTable", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("MeasureID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("MeasureID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("MeasureID.Measure", "xs:string");
					return objectRowsetSchema;
				case DDLRowsetType.Alter:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Perspective", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ID.PerspectiveTable", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ID.PerspectiveMeasure", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("MeasureID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("MeasureID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("MeasureID.Measure", "xs:string");
					return objectRowsetSchema;
				case DDLRowsetType.Delete:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Perspective", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ID.PerspectiveTable", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ID.PerspectiveMeasure", "xs:string");
					return objectRowsetSchema;
				default:
					throw TomInternalException.Create("Invalid DDL type {0}", new object[] { ddlType });
				}
				break;
			case ObjectType.Role:
				switch (ddlType)
				{
				case DDLRowsetType.Create:
					objectRowsetSchema.AddXmlSchemaField("Name", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Description", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ModelPermission", "xs:long");
					return objectRowsetSchema;
				case DDLRowsetType.Alter:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Role", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Name", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Description", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ModelPermission", "xs:long");
					return objectRowsetSchema;
				case DDLRowsetType.Delete:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Role", "xs:string");
					return objectRowsetSchema;
				case DDLRowsetType.Rename:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Role", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Name", "xs:string");
					return objectRowsetSchema;
				}
				throw TomInternalException.Create("Invalid DDL type {0}", new object[] { ddlType });
			case ObjectType.RoleMembership:
				switch (ddlType)
				{
				case DDLRowsetType.Create:
					objectRowsetSchema.AddXmlSchemaField("RoleID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("RoleID.Role", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("MemberName", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("MemberID", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("IdentityProvider", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("MemberType", "xs:long");
					return objectRowsetSchema;
				case DDLRowsetType.Alter:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Role", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ID.RoleMembership", "xs:string");
					return objectRowsetSchema;
				case DDLRowsetType.Delete:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Role", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ID.RoleMembership", "xs:string");
					return objectRowsetSchema;
				default:
					throw TomInternalException.Create("Invalid DDL type {0}", new object[] { ddlType });
				}
				break;
			case ObjectType.TablePermission:
				switch (ddlType)
				{
				case DDLRowsetType.Create:
					objectRowsetSchema.AddXmlSchemaField("RoleID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("RoleID.Role", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("TableID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("TableID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("FilterExpression", "xs:string");
					if (CompatibilityRestrictions.TablePermission_MetadataPermission.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("MetadataPermission", "xs:long");
						return objectRowsetSchema;
					}
					return objectRowsetSchema;
				case DDLRowsetType.Alter:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Role", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ID.TablePermission", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("TableID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("TableID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("FilterExpression", "xs:string");
					if (CompatibilityRestrictions.TablePermission_MetadataPermission.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("MetadataPermission", "xs:long");
						return objectRowsetSchema;
					}
					return objectRowsetSchema;
				case DDLRowsetType.Delete:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Role", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ID.TablePermission", "xs:string");
					return objectRowsetSchema;
				default:
					throw TomInternalException.Create("Invalid DDL type {0}", new object[] { ddlType });
				}
				break;
			case ObjectType.Variation:
				if (!CompatibilityRestrictions.Variation.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Object type {0} is incompatible with the db compatibility level {1} at mode {2}", new object[] { objectType, mode, dbCompatibilityLevel });
				}
				switch (ddlType)
				{
				case DDLRowsetType.Create:
					objectRowsetSchema.AddXmlSchemaField("ColumnID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ColumnID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ColumnID.Column", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Name", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Description", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("RelationshipID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("RelationshipID.Relationship", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("DefaultHierarchyID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("DefaultHierarchyID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("DefaultHierarchyID.Hierarchy", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("DefaultColumnID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("DefaultColumnID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("DefaultColumnID.Column", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("IsDefault", "xs:boolean");
					return objectRowsetSchema;
				case DDLRowsetType.Alter:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ID.Column", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ID.Variation", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Name", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Description", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("RelationshipID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("RelationshipID.Relationship", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("DefaultHierarchyID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("DefaultHierarchyID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("DefaultHierarchyID.Hierarchy", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("DefaultColumnID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("DefaultColumnID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("DefaultColumnID.Column", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("IsDefault", "xs:boolean");
					return objectRowsetSchema;
				case DDLRowsetType.Delete:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ID.Column", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ID.Variation", "xs:string");
					return objectRowsetSchema;
				case DDLRowsetType.Rename:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ID.Column", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ID.Variation", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Name", "xs:string");
					return objectRowsetSchema;
				}
				throw TomInternalException.Create("Invalid DDL type {0}", new object[] { ddlType });
			case ObjectType.Set:
				if (!CompatibilityRestrictions.Set.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Object type {0} is incompatible with the db compatibility level {1} at mode {2}", new object[] { objectType, mode, dbCompatibilityLevel });
				}
				switch (ddlType)
				{
				case DDLRowsetType.Create:
					objectRowsetSchema.AddXmlSchemaField("TableID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("TableID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Name", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Description", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Expression", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("IsDynamic", "xs:boolean");
					objectRowsetSchema.AddXmlSchemaField("IsHidden", "xs:boolean");
					objectRowsetSchema.AddXmlSchemaField("DisplayFolder", "xs:string");
					return objectRowsetSchema;
				case DDLRowsetType.Alter:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ID.Set", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Name", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Description", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Expression", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("IsDynamic", "xs:boolean");
					objectRowsetSchema.AddXmlSchemaField("IsHidden", "xs:boolean");
					objectRowsetSchema.AddXmlSchemaField("DisplayFolder", "xs:string");
					return objectRowsetSchema;
				case DDLRowsetType.Delete:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ID.Set", "xs:string");
					return objectRowsetSchema;
				case DDLRowsetType.Rename:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ID.Set", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Name", "xs:string");
					return objectRowsetSchema;
				}
				throw TomInternalException.Create("Invalid DDL type {0}", new object[] { ddlType });
			case ObjectType.PerspectiveSet:
				if (!CompatibilityRestrictions.PerspectiveSet.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Object type {0} is incompatible with the db compatibility level {1} at mode {2}", new object[] { objectType, mode, dbCompatibilityLevel });
				}
				switch (ddlType)
				{
				case DDLRowsetType.Create:
					objectRowsetSchema.AddXmlSchemaField("PerspectiveTableID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("PerspectiveTableID.Perspective", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("PerspectiveTableID.PerspectiveTable", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("SetID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("SetID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("SetID.Set", "xs:string");
					return objectRowsetSchema;
				case DDLRowsetType.Alter:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Perspective", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ID.PerspectiveTable", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ID.PerspectiveSet", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("SetID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("SetID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("SetID.Set", "xs:string");
					return objectRowsetSchema;
				case DDLRowsetType.Delete:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Perspective", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ID.PerspectiveTable", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ID.PerspectiveSet", "xs:string");
					return objectRowsetSchema;
				default:
					throw TomInternalException.Create("Invalid DDL type {0}", new object[] { ddlType });
				}
				break;
			case ObjectType.ExtendedProperty:
				if (!CompatibilityRestrictions.ExtendedProperty.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Object type {0} is incompatible with the db compatibility level {1} at mode {2}", new object[] { objectType, mode, dbCompatibilityLevel });
				}
				switch (ddlType)
				{
				case DDLRowsetType.Create:
					objectRowsetSchema.AddXmlSchemaField("ObjectID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ObjectID.DataSource", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ObjectID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ObjectID.Column", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ObjectID.Partition", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ObjectID.Relationship", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ObjectID.Measure", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ObjectID.Hierarchy", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ObjectID.Level", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ObjectID.Culture", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ObjectID.Perspective", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ObjectID.PerspectiveTable", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ObjectID.PerspectiveColumn", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ObjectID.PerspectiveHierarchy", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ObjectID.PerspectiveMeasure", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ObjectID.Role", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ObjectID.RoleMembership", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ObjectID.TablePermission", "xs:string");
					if (CompatibilityRestrictions.Variation.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("ObjectID.Variation", "xs:string");
					}
					if (CompatibilityRestrictions.Set.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("ObjectID.Set", "xs:string");
					}
					if (CompatibilityRestrictions.PerspectiveSet.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("ObjectID.PerspectiveSet", "xs:string");
					}
					if (CompatibilityRestrictions.NamedExpression.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("ObjectID.Expression", "xs:string");
					}
					if (CompatibilityRestrictions.ColumnPermission.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("ObjectID.ColumnPermission", "xs:string");
					}
					if (CompatibilityRestrictions.Function.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("ObjectID.Function", "xs:string");
					}
					if (CompatibilityRestrictions.BindingInfo.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("ObjectID.BindingInfo", "xs:string");
					}
					objectRowsetSchema.AddXmlSchemaField("ObjectType", "xs:int");
					objectRowsetSchema.AddXmlSchemaField("Name", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Type", "xs:long");
					objectRowsetSchema.AddXmlSchemaField("Value", "xs:string");
					return objectRowsetSchema;
				case DDLRowsetType.Alter:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("Name", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Value", "xs:string");
					return objectRowsetSchema;
				case DDLRowsetType.Delete:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					return objectRowsetSchema;
				case DDLRowsetType.Rename:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("Name", "xs:string");
					return objectRowsetSchema;
				}
				throw TomInternalException.Create("Invalid DDL type {0}", new object[] { ddlType });
			case ObjectType.Expression:
				if (!CompatibilityRestrictions.NamedExpression.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Object type {0} is incompatible with the db compatibility level {1} at mode {2}", new object[] { objectType, mode, dbCompatibilityLevel });
				}
				switch (ddlType)
				{
				case DDLRowsetType.Create:
					objectRowsetSchema.AddXmlSchemaField("Name", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Description", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Kind", "xs:long");
					objectRowsetSchema.AddXmlSchemaField("Expression", "xs:string");
					if (CompatibilityRestrictions.NamedExpression_QueryGroup.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("QueryGroupID", "xs:unsignedLong");
						objectRowsetSchema.AddXmlSchemaField("QueryGroupID.QueryGroup", "xs:string");
					}
					if (CompatibilityRestrictions.NamedExpression_ParameterValuesColumn.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("ParameterValuesColumnID", "xs:unsignedLong");
						objectRowsetSchema.AddXmlSchemaField("ParameterValuesColumnID.Table", "xs:string");
						objectRowsetSchema.AddXmlSchemaField("ParameterValuesColumnID.Column", "xs:string");
					}
					if (CompatibilityRestrictions.NamedExpression_MAttributes.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("MAttributes", "xs:string");
					}
					if (CompatibilityRestrictions.NamedExpression_LineageTag.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("LineageTag", "xs:string");
					}
					if (CompatibilityRestrictions.NamedExpression_SourceLineageTag.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("SourceLineageTag", "xs:string");
					}
					if (CompatibilityRestrictions.NamedExpression_RemoteParameterName.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("RemoteParameterName", "xs:string");
					}
					if (CompatibilityRestrictions.NamedExpression_ExpressionSource.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("ExpressionSourceID", "xs:unsignedLong");
						objectRowsetSchema.AddXmlSchemaField("ExpressionSourceID.Expression", "xs:string");
						return objectRowsetSchema;
					}
					return objectRowsetSchema;
				case DDLRowsetType.Alter:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Expression", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Name", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Description", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Kind", "xs:long");
					objectRowsetSchema.AddXmlSchemaField("Expression", "xs:string");
					if (CompatibilityRestrictions.NamedExpression_QueryGroup.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("QueryGroupID", "xs:unsignedLong");
						objectRowsetSchema.AddXmlSchemaField("QueryGroupID.QueryGroup", "xs:string");
					}
					if (CompatibilityRestrictions.NamedExpression_ParameterValuesColumn.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("ParameterValuesColumnID", "xs:unsignedLong");
						objectRowsetSchema.AddXmlSchemaField("ParameterValuesColumnID.Table", "xs:string");
						objectRowsetSchema.AddXmlSchemaField("ParameterValuesColumnID.Column", "xs:string");
					}
					if (CompatibilityRestrictions.NamedExpression_MAttributes.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("MAttributes", "xs:string");
					}
					if (CompatibilityRestrictions.NamedExpression_LineageTag.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("LineageTag", "xs:string");
					}
					if (CompatibilityRestrictions.NamedExpression_SourceLineageTag.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("SourceLineageTag", "xs:string");
					}
					if (CompatibilityRestrictions.NamedExpression_RemoteParameterName.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("RemoteParameterName", "xs:string");
					}
					if (CompatibilityRestrictions.NamedExpression_ExpressionSource.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("ExpressionSourceID", "xs:unsignedLong");
						objectRowsetSchema.AddXmlSchemaField("ExpressionSourceID.Expression", "xs:string");
						return objectRowsetSchema;
					}
					return objectRowsetSchema;
				case DDLRowsetType.Delete:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Expression", "xs:string");
					return objectRowsetSchema;
				case DDLRowsetType.Rename:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Expression", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Name", "xs:string");
					return objectRowsetSchema;
				case DDLRowsetType.Bindings:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Expression", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Expression", "xs:string");
					if (CompatibilityRestrictions.NamedExpression_ExpressionSource.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("ExpressionSourceID", "xs:unsignedLong");
						objectRowsetSchema.AddXmlSchemaField("ExpressionSourceID.Expression", "xs:string");
						return objectRowsetSchema;
					}
					return objectRowsetSchema;
				}
				throw TomInternalException.Create("Invalid DDL type {0}", new object[] { ddlType });
			case ObjectType.ColumnPermission:
				if (!CompatibilityRestrictions.ColumnPermission.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Object type {0} is incompatible with the db compatibility level {1} at mode {2}", new object[] { objectType, mode, dbCompatibilityLevel });
				}
				switch (ddlType)
				{
				case DDLRowsetType.Create:
					objectRowsetSchema.AddXmlSchemaField("TablePermissionID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("TablePermissionID.Role", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("TablePermissionID.TablePermission", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ColumnID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ColumnID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ColumnID.Column", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("MetadataPermission", "xs:long");
					return objectRowsetSchema;
				case DDLRowsetType.Alter:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Role", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ID.TablePermission", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ID.ColumnPermission", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ColumnID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ColumnID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ColumnID.Column", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("MetadataPermission", "xs:long");
					return objectRowsetSchema;
				case DDLRowsetType.Delete:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Role", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ID.TablePermission", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ID.ColumnPermission", "xs:string");
					return objectRowsetSchema;
				default:
					throw TomInternalException.Create("Invalid DDL type {0}", new object[] { ddlType });
				}
				break;
			case ObjectType.DetailRowsDefinition:
				if (!CompatibilityRestrictions.DetailRowsDefinition.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Object type {0} is incompatible with the db compatibility level {1} at mode {2}", new object[] { objectType, mode, dbCompatibilityLevel });
				}
				switch (ddlType)
				{
				case DDLRowsetType.Create:
					objectRowsetSchema.AddXmlSchemaField("ObjectID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ObjectID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ObjectID.Measure", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ObjectType", "xs:int");
					objectRowsetSchema.AddXmlSchemaField("Expression", "xs:string");
					return objectRowsetSchema;
				case DDLRowsetType.Alter:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("Expression", "xs:string");
					return objectRowsetSchema;
				case DDLRowsetType.Delete:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					return objectRowsetSchema;
				default:
					throw TomInternalException.Create("Invalid DDL type {0}", new object[] { ddlType });
				}
				break;
			case ObjectType.RelatedColumnDetails:
				if (!CompatibilityRestrictions.RelatedColumnDetails.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Object type {0} is incompatible with the db compatibility level {1} at mode {2}", new object[] { objectType, mode, dbCompatibilityLevel });
				}
				switch (ddlType)
				{
				case DDLRowsetType.Create:
					objectRowsetSchema.AddXmlSchemaField("ColumnID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ColumnID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ColumnID.Column", "xs:string");
					return objectRowsetSchema;
				case DDLRowsetType.Alter:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ID.Column", "xs:string");
					return objectRowsetSchema;
				case DDLRowsetType.Delete:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ID.Column", "xs:string");
					return objectRowsetSchema;
				default:
					throw TomInternalException.Create("Invalid DDL type {0}", new object[] { ddlType });
				}
				break;
			case ObjectType.GroupByColumn:
				if (!CompatibilityRestrictions.GroupByColumn.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Object type {0} is incompatible with the db compatibility level {1} at mode {2}", new object[] { objectType, mode, dbCompatibilityLevel });
				}
				switch (ddlType)
				{
				case DDLRowsetType.Create:
					objectRowsetSchema.AddXmlSchemaField("RelatedColumnDetailsID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("RelatedColumnDetailsID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("RelatedColumnDetailsID.Column", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("GroupingColumnID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("GroupingColumnID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("GroupingColumnID.Column", "xs:string");
					return objectRowsetSchema;
				case DDLRowsetType.Alter:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("GroupingColumnID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("GroupingColumnID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("GroupingColumnID.Column", "xs:string");
					return objectRowsetSchema;
				case DDLRowsetType.Delete:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					return objectRowsetSchema;
				default:
					throw TomInternalException.Create("Invalid DDL type {0}", new object[] { ddlType });
				}
				break;
			case ObjectType.CalculationGroup:
				if (!CompatibilityRestrictions.CalculationGroup.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Object type {0} is incompatible with the db compatibility level {1} at mode {2}", new object[] { objectType, mode, dbCompatibilityLevel });
				}
				switch (ddlType)
				{
				case DDLRowsetType.Create:
					objectRowsetSchema.AddXmlSchemaField("TableID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("TableID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Description", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Precedence", "xs:int");
					return objectRowsetSchema;
				case DDLRowsetType.Alter:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Description", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Precedence", "xs:int");
					return objectRowsetSchema;
				case DDLRowsetType.Delete:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Table", "xs:string");
					return objectRowsetSchema;
				default:
					throw TomInternalException.Create("Invalid DDL type {0}", new object[] { ddlType });
				}
				break;
			case ObjectType.CalculationItem:
				if (!CompatibilityRestrictions.CalculationItem.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Object type {0} is incompatible with the db compatibility level {1} at mode {2}", new object[] { objectType, mode, dbCompatibilityLevel });
				}
				switch (ddlType)
				{
				case DDLRowsetType.Create:
					objectRowsetSchema.AddXmlSchemaField("CalculationGroupID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("CalculationGroupID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Name", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Description", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Expression", "xs:string");
					if (CompatibilityRestrictions.CalculationItem_Ordinal.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("Ordinal", "xs:int");
						return objectRowsetSchema;
					}
					return objectRowsetSchema;
				case DDLRowsetType.Alter:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ID.CalculationItem", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Name", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Description", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Expression", "xs:string");
					if (CompatibilityRestrictions.CalculationItem_Ordinal.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("Ordinal", "xs:int");
						return objectRowsetSchema;
					}
					return objectRowsetSchema;
				case DDLRowsetType.Delete:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ID.CalculationItem", "xs:string");
					return objectRowsetSchema;
				case DDLRowsetType.Rename:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ID.CalculationItem", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Name", "xs:string");
					return objectRowsetSchema;
				}
				throw TomInternalException.Create("Invalid DDL type {0}", new object[] { ddlType });
			case ObjectType.AlternateOf:
				if (!CompatibilityRestrictions.AlternateOf.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Object type {0} is incompatible with the db compatibility level {1} at mode {2}", new object[] { objectType, mode, dbCompatibilityLevel });
				}
				switch (ddlType)
				{
				case DDLRowsetType.Create:
					objectRowsetSchema.AddXmlSchemaField("ColumnID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ColumnID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ColumnID.Column", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("BaseColumnID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("BaseColumnID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("BaseColumnID.Column", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("BaseTableID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("BaseTableID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Summarization", "xs:long");
					return objectRowsetSchema;
				case DDLRowsetType.Alter:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ID.Column", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("BaseColumnID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("BaseColumnID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("BaseColumnID.Column", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("BaseTableID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("BaseTableID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Summarization", "xs:long");
					return objectRowsetSchema;
				case DDLRowsetType.Delete:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ID.Column", "xs:string");
					return objectRowsetSchema;
				default:
					throw TomInternalException.Create("Invalid DDL type {0}", new object[] { ddlType });
				}
				break;
			case ObjectType.RefreshPolicy:
				if (!CompatibilityRestrictions.RefreshPolicy.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Object type {0} is incompatible with the db compatibility level {1} at mode {2}", new object[] { objectType, mode, dbCompatibilityLevel });
				}
				switch (ddlType)
				{
				case DDLRowsetType.Create:
					objectRowsetSchema.AddXmlSchemaField("TableID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("TableID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("PolicyType", "xs:long");
					objectRowsetSchema.AddXmlSchemaField("RollingWindowGranularity", "xs:long");
					objectRowsetSchema.AddXmlSchemaField("RollingWindowPeriods", "xs:int");
					objectRowsetSchema.AddXmlSchemaField("IncrementalGranularity", "xs:long");
					objectRowsetSchema.AddXmlSchemaField("IncrementalPeriods", "xs:int");
					objectRowsetSchema.AddXmlSchemaField("IncrementalPeriodsOffset", "xs:int");
					objectRowsetSchema.AddXmlSchemaField("PollingExpression", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("SourceExpression", "xs:string");
					if (CompatibilityRestrictions.RefreshPolicy_Mode.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("Mode", "xs:long");
						return objectRowsetSchema;
					}
					return objectRowsetSchema;
				case DDLRowsetType.Alter:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("PolicyType", "xs:long");
					objectRowsetSchema.AddXmlSchemaField("RollingWindowGranularity", "xs:long");
					objectRowsetSchema.AddXmlSchemaField("RollingWindowPeriods", "xs:int");
					objectRowsetSchema.AddXmlSchemaField("IncrementalGranularity", "xs:long");
					objectRowsetSchema.AddXmlSchemaField("IncrementalPeriods", "xs:int");
					objectRowsetSchema.AddXmlSchemaField("IncrementalPeriodsOffset", "xs:int");
					objectRowsetSchema.AddXmlSchemaField("PollingExpression", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("SourceExpression", "xs:string");
					if (CompatibilityRestrictions.RefreshPolicy_Mode.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("Mode", "xs:long");
						return objectRowsetSchema;
					}
					return objectRowsetSchema;
				case DDLRowsetType.Delete:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Table", "xs:string");
					return objectRowsetSchema;
				default:
					throw TomInternalException.Create("Invalid DDL type {0}", new object[] { ddlType });
				}
				break;
			case ObjectType.FormatStringDefinition:
				if (!CompatibilityRestrictions.FormatStringDefinition.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Object type {0} is incompatible with the db compatibility level {1} at mode {2}", new object[] { objectType, mode, dbCompatibilityLevel });
				}
				switch (ddlType)
				{
				case DDLRowsetType.Create:
					objectRowsetSchema.AddXmlSchemaField("ObjectID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ObjectID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ObjectID.Measure", "xs:string");
					if (CompatibilityRestrictions.CalculationItem.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("ObjectID.CalculationItem", "xs:string");
					}
					if (CompatibilityRestrictions.CalculationGroupExpression.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("ObjectID.CalculationExpression", "xs:string");
					}
					objectRowsetSchema.AddXmlSchemaField("ObjectType", "xs:int");
					objectRowsetSchema.AddXmlSchemaField("Expression", "xs:string");
					return objectRowsetSchema;
				case DDLRowsetType.Alter:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("Expression", "xs:string");
					return objectRowsetSchema;
				case DDLRowsetType.Delete:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					return objectRowsetSchema;
				default:
					throw TomInternalException.Create("Invalid DDL type {0}", new object[] { ddlType });
				}
				break;
			case ObjectType.QueryGroup:
				if (!CompatibilityRestrictions.QueryGroup.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Object type {0} is incompatible with the db compatibility level {1} at mode {2}", new object[] { objectType, mode, dbCompatibilityLevel });
				}
				switch (ddlType)
				{
				case DDLRowsetType.Create:
					objectRowsetSchema.AddXmlSchemaField("Folder", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Description", "xs:string");
					return objectRowsetSchema;
				case DDLRowsetType.Alter:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.QueryGroup", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Folder", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Description", "xs:string");
					return objectRowsetSchema;
				case DDLRowsetType.Delete:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.QueryGroup", "xs:string");
					return objectRowsetSchema;
				default:
					throw TomInternalException.Create("Invalid DDL type {0}", new object[] { ddlType });
				}
				break;
			case ObjectType.AnalyticsAIMetadata:
				if (!CompatibilityRestrictions.AnalyticsAIMetadata.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Object type {0} is incompatible with the db compatibility level {1} at mode {2}", new object[] { objectType, mode, dbCompatibilityLevel });
				}
				switch (ddlType)
				{
				case DDLRowsetType.Create:
					objectRowsetSchema.AddXmlSchemaField("Name", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("MeasureAnalysisDefinition", "xs:string");
					return objectRowsetSchema;
				case DDLRowsetType.Alter:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.AnalyticsAIMetadata", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Name", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("MeasureAnalysisDefinition", "xs:string");
					return objectRowsetSchema;
				case DDLRowsetType.Delete:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.AnalyticsAIMetadata", "xs:string");
					return objectRowsetSchema;
				default:
					throw TomInternalException.Create("Invalid DDL type {0}", new object[] { ddlType });
				}
				break;
			case ObjectType.ChangedProperty:
				if (!CompatibilityRestrictions.ChangedProperty.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Object type {0} is incompatible with the db compatibility level {1} at mode {2}", new object[] { objectType, mode, dbCompatibilityLevel });
				}
				switch (ddlType)
				{
				case DDLRowsetType.Create:
					objectRowsetSchema.AddXmlSchemaField("ObjectID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ObjectID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ObjectID.Column", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ObjectID.Relationship", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ObjectID.Measure", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ObjectID.Hierarchy", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ObjectID.Level", "xs:string");
					if (CompatibilityRestrictions.Function.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("ObjectID.Function", "xs:string");
					}
					objectRowsetSchema.AddXmlSchemaField("ObjectType", "xs:int");
					objectRowsetSchema.AddXmlSchemaField("Property", "xs:string");
					return objectRowsetSchema;
				case DDLRowsetType.Alter:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("Property", "xs:string");
					return objectRowsetSchema;
				case DDLRowsetType.Delete:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					return objectRowsetSchema;
				default:
					throw TomInternalException.Create("Invalid DDL type {0}", new object[] { ddlType });
				}
				break;
			case ObjectType.ExcludedArtifact:
				if (!CompatibilityRestrictions.ExcludedArtifact.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Object type {0} is incompatible with the db compatibility level {1} at mode {2}", new object[] { objectType, mode, dbCompatibilityLevel });
				}
				switch (ddlType)
				{
				case DDLRowsetType.Create:
					objectRowsetSchema.AddXmlSchemaField("ObjectID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ObjectID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ObjectID.Hierarchy", "xs:string");
					if (CompatibilityRestrictions.NamedExpression.IsCompatible(mode, dbCompatibilityLevel))
					{
						objectRowsetSchema.AddXmlSchemaField("ObjectID.Expression", "xs:string");
					}
					objectRowsetSchema.AddXmlSchemaField("ObjectType", "xs:int");
					objectRowsetSchema.AddXmlSchemaField("ArtifactType", "xs:int");
					objectRowsetSchema.AddXmlSchemaField("Reference", "xs:string");
					return objectRowsetSchema;
				case DDLRowsetType.Alter:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ArtifactType", "xs:int");
					objectRowsetSchema.AddXmlSchemaField("Reference", "xs:string");
					return objectRowsetSchema;
				case DDLRowsetType.Delete:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					return objectRowsetSchema;
				default:
					throw TomInternalException.Create("Invalid DDL type {0}", new object[] { ddlType });
				}
				break;
			case ObjectType.DataCoverageDefinition:
				if (!CompatibilityRestrictions.DataCoverageDefinition.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Object type {0} is incompatible with the db compatibility level {1} at mode {2}", new object[] { objectType, mode, dbCompatibilityLevel });
				}
				switch (ddlType)
				{
				case DDLRowsetType.Create:
					objectRowsetSchema.AddXmlSchemaField("PartitionID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("PartitionID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("PartitionID.Partition", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Description", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Expression", "xs:string");
					return objectRowsetSchema;
				case DDLRowsetType.Alter:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ID.Partition", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Description", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Expression", "xs:string");
					return objectRowsetSchema;
				case DDLRowsetType.Delete:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ID.Partition", "xs:string");
					return objectRowsetSchema;
				default:
					throw TomInternalException.Create("Invalid DDL type {0}", new object[] { ddlType });
				}
				break;
			case ObjectType.CalculationExpression:
				if (!CompatibilityRestrictions.CalculationGroupExpression.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Object type {0} is incompatible with the db compatibility level {1} at mode {2}", new object[] { objectType, mode, dbCompatibilityLevel });
				}
				switch (ddlType)
				{
				case DDLRowsetType.Create:
					objectRowsetSchema.AddXmlSchemaField("CalculationGroupID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("CalculationGroupID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Description", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Expression", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("SelectionMode", "xs:long");
					return objectRowsetSchema;
				case DDLRowsetType.Alter:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ID.CalculationExpression", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Description", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Expression", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("SelectionMode", "xs:long");
					return objectRowsetSchema;
				case DDLRowsetType.Delete:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ID.CalculationExpression", "xs:string");
					return objectRowsetSchema;
				default:
					throw TomInternalException.Create("Invalid DDL type {0}", new object[] { ddlType });
				}
				break;
			case ObjectType.Calendar:
				if (!CompatibilityRestrictions.Calendar.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Object type {0} is incompatible with the db compatibility level {1} at mode {2}", new object[] { objectType, mode, dbCompatibilityLevel });
				}
				switch (ddlType)
				{
				case DDLRowsetType.Create:
					objectRowsetSchema.AddXmlSchemaField("TableID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("TableID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Name", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Description", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("LineageTag", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("SourceLineageTag", "xs:string");
					return objectRowsetSchema;
				case DDLRowsetType.Alter:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ID.Calendar", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Name", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Description", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("LineageTag", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("SourceLineageTag", "xs:string");
					return objectRowsetSchema;
				case DDLRowsetType.Delete:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ID.Calendar", "xs:string");
					return objectRowsetSchema;
				case DDLRowsetType.Rename:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ID.Calendar", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Name", "xs:string");
					return objectRowsetSchema;
				}
				throw TomInternalException.Create("Invalid DDL type {0}", new object[] { ddlType });
			case ObjectType.TimeUnitColumnAssociation:
				if (!CompatibilityRestrictions.TimeUnitColumnAssociation.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Object type {0} is incompatible with the db compatibility level {1} at mode {2}", new object[] { objectType, mode, dbCompatibilityLevel });
				}
				switch (ddlType)
				{
				case DDLRowsetType.Create:
					objectRowsetSchema.AddXmlSchemaField("CalendarID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("CalendarID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("CalendarID.Calendar", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("TimeUnit", "xs:long");
					return objectRowsetSchema;
				case DDLRowsetType.Alter:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ID.Calendar", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ID.TimeUnitColumnAssociation", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("TimeUnit", "xs:long");
					return objectRowsetSchema;
				case DDLRowsetType.Delete:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ID.Calendar", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ID.TimeUnitColumnAssociation", "xs:string");
					return objectRowsetSchema;
				default:
					throw TomInternalException.Create("Invalid DDL type {0}", new object[] { ddlType });
				}
				break;
			case ObjectType.CalendarColumnReference:
				if (!CompatibilityRestrictions.CalendarColumnReference.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Object type {0} is incompatible with the db compatibility level {1} at mode {2}", new object[] { objectType, mode, dbCompatibilityLevel });
				}
				switch (ddlType)
				{
				case DDLRowsetType.Create:
					objectRowsetSchema.AddXmlSchemaField("TimeUnitColumnAssociationID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("TimeUnitColumnAssociationID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("TimeUnitColumnAssociationID.Calendar", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("TimeUnitColumnAssociationID.TimeUnitColumnAssociation", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ColumnID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ColumnID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ColumnID.Column", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("IsPrimaryColumn", "xs:boolean");
					return objectRowsetSchema;
				case DDLRowsetType.Alter:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ID.Calendar", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ID.TimeUnitColumnAssociation", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ID.CalendarColumnReference", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ColumnID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ColumnID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ColumnID.Column", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("IsPrimaryColumn", "xs:boolean");
					return objectRowsetSchema;
				case DDLRowsetType.Delete:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Table", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ID.Calendar", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ID.TimeUnitColumnAssociation", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("ID.CalendarColumnReference", "xs:string");
					return objectRowsetSchema;
				default:
					throw TomInternalException.Create("Invalid DDL type {0}", new object[] { ddlType });
				}
				break;
			case ObjectType.Function:
				if (!CompatibilityRestrictions.Function.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Object type {0} is incompatible with the db compatibility level {1} at mode {2}", new object[] { objectType, mode, dbCompatibilityLevel });
				}
				switch (ddlType)
				{
				case DDLRowsetType.Create:
					objectRowsetSchema.AddXmlSchemaField("Name", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Description", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Expression", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("IsHidden", "xs:boolean");
					objectRowsetSchema.AddXmlSchemaField("LineageTag", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("SourceLineageTag", "xs:string");
					return objectRowsetSchema;
				case DDLRowsetType.Alter:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Function", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Name", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Description", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Expression", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("IsHidden", "xs:boolean");
					objectRowsetSchema.AddXmlSchemaField("LineageTag", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("SourceLineageTag", "xs:string");
					return objectRowsetSchema;
				case DDLRowsetType.Delete:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Function", "xs:string");
					return objectRowsetSchema;
				case DDLRowsetType.Rename:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.Function", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Name", "xs:string");
					return objectRowsetSchema;
				}
				throw TomInternalException.Create("Invalid DDL type {0}", new object[] { ddlType });
			case ObjectType.BindingInfo:
				if (!CompatibilityRestrictions.BindingInfo.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Object type {0} is incompatible with the db compatibility level {1} at mode {2}", new object[] { objectType, mode, dbCompatibilityLevel });
				}
				switch (ddlType)
				{
				case DDLRowsetType.Create:
					objectRowsetSchema.AddXmlSchemaField("Name", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Description", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Type", "xs:long");
					objectRowsetSchema.AddXmlSchemaField("ConnectionId", "xs:string");
					return objectRowsetSchema;
				case DDLRowsetType.Alter:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.BindingInfo", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Name", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Description", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Type", "xs:long");
					objectRowsetSchema.AddXmlSchemaField("ConnectionId", "xs:string");
					return objectRowsetSchema;
				case DDLRowsetType.Delete:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.BindingInfo", "xs:string");
					return objectRowsetSchema;
				case DDLRowsetType.Rename:
					objectRowsetSchema.AddXmlSchemaField("ID", "xs:unsignedLong");
					objectRowsetSchema.AddXmlSchemaField("ID.BindingInfo", "xs:string");
					objectRowsetSchema.AddXmlSchemaField("Name", "xs:string");
					return objectRowsetSchema;
				}
				throw TomInternalException.Create("Invalid DDL type {0}", new object[] { ddlType });
			}
			throw TomInternalException.Create("Invalid object type {0}", new object[] { objectType });
		}

		// Token: 0x06001467 RID: 5223 RVA: 0x0008AAF4 File Offset: 0x00088CF4
		private static XElement CreateRowElement()
		{
			return new XElement(XmlaConstants.XNS.rst + "row");
		}

		// Token: 0x06001468 RID: 5224 RVA: 0x0008AB0A File Offset: 0x00088D0A
		private static XElement GetDatabaseIDElement(string databaseId)
		{
			return new XElement(XmlaConstants.XNS.tmddl + "DatabaseID", databaseId);
		}

		// Token: 0x06001469 RID: 5225 RVA: 0x0008AB21 File Offset: 0x00088D21
		private static XElement GetTableNameElement(string tableName)
		{
			return new XElement(XmlaConstants.XNS.tmddl + "TableName", tableName);
		}

		// Token: 0x0600146A RID: 5226 RVA: 0x0008AB38 File Offset: 0x00088D38
		private static XElement GetPartitionNameElement(string partitionName)
		{
			return new XElement(XmlaConstants.XNS.tmddl + "PartitionName", partitionName);
		}

		// Token: 0x0600146B RID: 5227 RVA: 0x0008AB4F File Offset: 0x00088D4F
		private static XElement GetMaxParallelismElement(int maxParallelism)
		{
			return new XElement(XmlaConstants.XNS.tmddl + "MaxParallelism", maxParallelism.ToString());
		}

		// Token: 0x040002FC RID: 764
		private const string ModelVersionColumn = "Version";

		// Token: 0x040002FD RID: 765
		private const string ModelModifiedTimeColumn = "ModifiedTime";

		// Token: 0x040002FE RID: 766
		private const string ModelStructureModifiedTimeColumn = "StructureModifiedTime";

		// Token: 0x040002FF RID: 767
		private const string PartitionRefreshedTimeColumn = "RefreshedTime";

		// Token: 0x04000300 RID: 768
		private const string ServerImpactTypeColumn = "ImpactType";

		// Token: 0x04000301 RID: 769
		private const string ServerImpactIdColumn = "ID";

		// Token: 0x04000302 RID: 770
		private static readonly List<ObjectType> objectTypes = new List<ObjectType>
		{
			ObjectType.Model,
			ObjectType.DataSource,
			ObjectType.Table,
			ObjectType.Column,
			ObjectType.AttributeHierarchy,
			ObjectType.Partition,
			ObjectType.Relationship,
			ObjectType.Measure,
			ObjectType.Hierarchy,
			ObjectType.Level,
			ObjectType.Annotation,
			ObjectType.KPI,
			ObjectType.Culture,
			ObjectType.ObjectTranslation,
			ObjectType.LinguisticMetadata,
			ObjectType.Perspective,
			ObjectType.PerspectiveTable,
			ObjectType.PerspectiveColumn,
			ObjectType.PerspectiveHierarchy,
			ObjectType.PerspectiveMeasure,
			ObjectType.Role,
			ObjectType.RoleMembership,
			ObjectType.TablePermission,
			ObjectType.Variation,
			ObjectType.Set,
			ObjectType.PerspectiveSet,
			ObjectType.ExtendedProperty,
			ObjectType.Expression,
			ObjectType.ColumnPermission,
			ObjectType.DetailRowsDefinition,
			ObjectType.RelatedColumnDetails,
			ObjectType.GroupByColumn,
			ObjectType.CalculationGroup,
			ObjectType.CalculationItem,
			ObjectType.AlternateOf,
			ObjectType.RefreshPolicy,
			ObjectType.FormatStringDefinition,
			ObjectType.QueryGroup,
			ObjectType.AnalyticsAIMetadata,
			ObjectType.ChangedProperty,
			ObjectType.ExcludedArtifact,
			ObjectType.DataCoverageDefinition,
			ObjectType.CalculationExpression,
			ObjectType.Calendar,
			ObjectType.TimeUnitColumnAssociation,
			ObjectType.CalendarColumnReference,
			ObjectType.Function,
			ObjectType.BindingInfo
		};

		// Token: 0x04000303 RID: 771
		private static readonly List<ObjectType> objectTypesForMetadataStatus = new List<ObjectType>
		{
			ObjectType.Model,
			ObjectType.Partition
		};

		// Token: 0x04000304 RID: 772
		private static readonly Dictionary<ObjectType, DdlUtil.ObjectTypeInformation> objectTypeToInfoMap = new Dictionary<ObjectType, DdlUtil.ObjectTypeInformation>();

		// Token: 0x02000314 RID: 788
		private struct ObjectTypeInformation
		{
			// Token: 0x060024A0 RID: 9376 RVA: 0x000E4AB7 File Offset: 0x000E2CB7
			public ObjectTypeInformation(CompatibilityRestrictionSet restrictionSet, string xmlElementName, string discoverSchemaName)
			{
				this.restrictionSet = restrictionSet;
				this.xmlElementName = xmlElementName;
				this.discoverSchemaName = discoverSchemaName;
			}

			// Token: 0x17000783 RID: 1923
			// (get) Token: 0x060024A1 RID: 9377 RVA: 0x000E4ACE File Offset: 0x000E2CCE
			public CompatibilityRestrictionSet RestrictionSet
			{
				get
				{
					return this.restrictionSet;
				}
			}

			// Token: 0x17000784 RID: 1924
			// (get) Token: 0x060024A2 RID: 9378 RVA: 0x000E4AD6 File Offset: 0x000E2CD6
			public string XmlElementName
			{
				get
				{
					return this.xmlElementName;
				}
			}

			// Token: 0x17000785 RID: 1925
			// (get) Token: 0x060024A3 RID: 9379 RVA: 0x000E4ADE File Offset: 0x000E2CDE
			public string DiscoverSchemaName
			{
				get
				{
					return this.discoverSchemaName;
				}
			}

			// Token: 0x04000D3F RID: 3391
			private string xmlElementName;

			// Token: 0x04000D40 RID: 3392
			private string discoverSchemaName;

			// Token: 0x04000D41 RID: 3393
			private readonly CompatibilityRestrictionSet restrictionSet;
		}
	}
}
