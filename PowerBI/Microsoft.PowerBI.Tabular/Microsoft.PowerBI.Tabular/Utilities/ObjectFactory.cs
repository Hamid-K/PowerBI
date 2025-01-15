using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AnalysisServices.Tabular.DataRefresh;
using Microsoft.AnalysisServices.Tabular.Extensions;
using Microsoft.AnalysisServices.Tabular.Json;
using Microsoft.AnalysisServices.Tabular.Json.Linq;
using Microsoft.AnalysisServices.Tabular.Serialization;

namespace Microsoft.AnalysisServices.Tabular.Utilities
{
	// Token: 0x02000197 RID: 407
	internal static class ObjectFactory
	{
		// Token: 0x06001893 RID: 6291 RVA: 0x000A4548 File Offset: 0x000A2748
		public static MetadataObject CreateDefaultObjectOfType(ObjectType type)
		{
			switch (type)
			{
			case ObjectType.Model:
				return new Model();
			case ObjectType.DataSource:
				return new ProviderDataSource();
			case ObjectType.Table:
				return new Table();
			case ObjectType.Column:
				return new DataColumn();
			case ObjectType.AttributeHierarchy:
				return new AttributeHierarchy();
			case ObjectType.Partition:
				return new Partition();
			case ObjectType.Relationship:
				return new SingleColumnRelationship();
			case ObjectType.Measure:
				return new Measure();
			case ObjectType.Hierarchy:
				return new Hierarchy();
			case ObjectType.Level:
				return new Level();
			case ObjectType.Annotation:
				return new Annotation();
			case ObjectType.KPI:
				return new KPI();
			case ObjectType.Culture:
				return new Culture();
			case ObjectType.ObjectTranslation:
				return new ObjectTranslation();
			case ObjectType.LinguisticMetadata:
				return new LinguisticMetadata();
			case ObjectType.Perspective:
				return new Perspective();
			case ObjectType.PerspectiveTable:
				return new PerspectiveTable();
			case ObjectType.PerspectiveColumn:
				return new PerspectiveColumn();
			case ObjectType.PerspectiveHierarchy:
				return new PerspectiveHierarchy();
			case ObjectType.PerspectiveMeasure:
				return new PerspectiveMeasure();
			case ObjectType.Role:
				return new ModelRole();
			case ObjectType.RoleMembership:
				return new WindowsModelRoleMember();
			case ObjectType.TablePermission:
				return new TablePermission();
			case ObjectType.Variation:
				return new Variation();
			case ObjectType.Set:
				return new Set();
			case ObjectType.PerspectiveSet:
				return new PerspectiveSet();
			case ObjectType.ExtendedProperty:
				return new StringExtendedProperty();
			case ObjectType.Expression:
				return new NamedExpression();
			case ObjectType.ColumnPermission:
				return new ColumnPermission();
			case ObjectType.DetailRowsDefinition:
				return new DetailRowsDefinition();
			case ObjectType.RelatedColumnDetails:
				return new RelatedColumnDetails();
			case ObjectType.GroupByColumn:
				return new GroupByColumn();
			case ObjectType.CalculationGroup:
				return new CalculationGroup();
			case ObjectType.CalculationItem:
				return new CalculationItem();
			case ObjectType.AlternateOf:
				return new AlternateOf();
			case ObjectType.RefreshPolicy:
				return new BasicRefreshPolicy();
			case ObjectType.FormatStringDefinition:
				return new FormatStringDefinition();
			case ObjectType.QueryGroup:
				return new QueryGroup();
			case ObjectType.AnalyticsAIMetadata:
				return new AnalyticsAIMetadata();
			case ObjectType.ChangedProperty:
				return new ChangedProperty();
			case ObjectType.ExcludedArtifact:
				return new ExcludedArtifact();
			case ObjectType.DataCoverageDefinition:
				return new DataCoverageDefinition();
			case ObjectType.CalculationExpression:
				return new CalculationGroupExpression();
			case ObjectType.Calendar:
				return new Calendar();
			case ObjectType.TimeUnitColumnAssociation:
				return new TimeUnitColumnAssociation();
			case ObjectType.CalendarColumnReference:
				return new CalendarColumnReference();
			case ObjectType.Function:
				return new Function();
			case ObjectType.BindingInfo:
				return new DataBindingHint();
			}
			throw new TomInternalException();
		}

		// Token: 0x06001894 RID: 6292 RVA: 0x000A4788 File Offset: 0x000A2988
		public static MetadataObject CreateObjectFromRowset(ObjectType type, IPropertyReader reader, IEqualityComparer<string> comparer)
		{
			switch (type)
			{
			case ObjectType.Model:
				return new Model(comparer);
			case ObjectType.DataSource:
				return ObjectFactory.CreateDataSourceFromRowset(reader, comparer);
			case ObjectType.Table:
				return new Table(comparer);
			case ObjectType.Column:
				return ObjectFactory.CreateColumnFromRowset(reader, comparer);
			case ObjectType.AttributeHierarchy:
				return new AttributeHierarchy(comparer);
			case ObjectType.Partition:
				return new Partition(comparer);
			case ObjectType.Relationship:
				return ObjectFactory.CreateRelationshipFromRowset(reader, comparer);
			case ObjectType.Measure:
				return new Measure(comparer);
			case ObjectType.Hierarchy:
				return new Hierarchy(comparer);
			case ObjectType.Level:
				return new Level(comparer);
			case ObjectType.Annotation:
				return new Annotation(comparer);
			case ObjectType.KPI:
				return new KPI(comparer);
			case ObjectType.Culture:
				return new Culture(comparer);
			case ObjectType.ObjectTranslation:
				return new ObjectTranslation(comparer);
			case ObjectType.LinguisticMetadata:
				return new LinguisticMetadata(comparer);
			case ObjectType.Perspective:
				return new Perspective(comparer);
			case ObjectType.PerspectiveTable:
				return new PerspectiveTable(comparer);
			case ObjectType.PerspectiveColumn:
				return new PerspectiveColumn(comparer);
			case ObjectType.PerspectiveHierarchy:
				return new PerspectiveHierarchy(comparer);
			case ObjectType.PerspectiveMeasure:
				return new PerspectiveMeasure(comparer);
			case ObjectType.Role:
				return new ModelRole(comparer);
			case ObjectType.RoleMembership:
				return ObjectFactory.CreateModelRoleMemberFromRowset(reader, comparer);
			case ObjectType.TablePermission:
				return new TablePermission(comparer);
			case ObjectType.Variation:
				return new Variation(comparer);
			case ObjectType.Set:
				return new Set(comparer);
			case ObjectType.PerspectiveSet:
				return new PerspectiveSet(comparer);
			case ObjectType.ExtendedProperty:
				return ObjectFactory.CreateExtendedPropertyFromRowset(reader, comparer);
			case ObjectType.Expression:
				return new NamedExpression(comparer);
			case ObjectType.ColumnPermission:
				return new ColumnPermission(comparer);
			case ObjectType.DetailRowsDefinition:
				return new DetailRowsDefinition(comparer);
			case ObjectType.RelatedColumnDetails:
				return new RelatedColumnDetails(comparer);
			case ObjectType.GroupByColumn:
				return new GroupByColumn(comparer);
			case ObjectType.CalculationGroup:
				return new CalculationGroup(comparer);
			case ObjectType.CalculationItem:
				return new CalculationItem(comparer);
			case ObjectType.AlternateOf:
				return new AlternateOf(comparer);
			case ObjectType.RefreshPolicy:
				return ObjectFactory.CreateRefreshPolicyFromRowset(reader, comparer);
			case ObjectType.FormatStringDefinition:
				return new FormatStringDefinition(comparer);
			case ObjectType.QueryGroup:
				return new QueryGroup(comparer);
			case ObjectType.AnalyticsAIMetadata:
				return new AnalyticsAIMetadata(comparer);
			case ObjectType.ChangedProperty:
				return new ChangedProperty(comparer);
			case ObjectType.ExcludedArtifact:
				return new ExcludedArtifact(comparer);
			case ObjectType.DataCoverageDefinition:
				return new DataCoverageDefinition(comparer);
			case ObjectType.CalculationExpression:
				return new CalculationGroupExpression(comparer);
			case ObjectType.Calendar:
				return new Calendar(comparer);
			case ObjectType.TimeUnitColumnAssociation:
				return new TimeUnitColumnAssociation(comparer);
			case ObjectType.CalendarColumnReference:
				return new CalendarColumnReference(comparer);
			case ObjectType.Function:
				return new Function(comparer);
			case ObjectType.BindingInfo:
				return ObjectFactory.CreateBindingInfoFromRowset(reader, comparer);
			}
			throw new TomInternalException();
		}

		// Token: 0x06001895 RID: 6293 RVA: 0x000A4A00 File Offset: 0x000A2C00
		private static Column CreateColumnFromRowset(IPropertyReader reader, IEqualityComparer<string> comparer)
		{
			ColumnType columnType;
			if (!reader.TryReadProperty<ColumnType>("Type", out columnType))
			{
				throw new ResponseFormatException(TomSR.Exception_CanNotReadFindTypePropObject("Column"));
			}
			switch (columnType)
			{
			case ColumnType.Data:
				return new DataColumn(comparer);
			case ColumnType.Calculated:
				return new CalculatedColumn(comparer);
			case ColumnType.RowNumber:
				return new RowNumberColumn(comparer);
			case ColumnType.CalculatedTableColumn:
				return new CalculatedTableColumn(comparer);
			default:
				throw new ResponseFormatException(TomSR.Exception_UnrecognizedValueOfType("ColumnType", columnType.ToString()));
			}
		}

		// Token: 0x06001896 RID: 6294 RVA: 0x000A4A80 File Offset: 0x000A2C80
		private static DataSource CreateDataSourceFromRowset(IPropertyReader reader, IEqualityComparer<string> comparer)
		{
			DataSourceType dataSourceType;
			if (!reader.TryReadProperty<DataSourceType>("Type", out dataSourceType))
			{
				throw new ResponseFormatException(TomSR.Exception_CanNotReadFindTypePropObject("DataSource"));
			}
			if (dataSourceType == DataSourceType.Provider)
			{
				return new ProviderDataSource(comparer);
			}
			if (dataSourceType != DataSourceType.Structured)
			{
				throw new ResponseFormatException(TomSR.Exception_UnrecognizedValueOfType("DataSourceType", dataSourceType.ToString()));
			}
			return new StructuredDataSource(comparer);
		}

		// Token: 0x06001897 RID: 6295 RVA: 0x000A4AE0 File Offset: 0x000A2CE0
		private static Relationship CreateRelationshipFromRowset(IPropertyReader reader, IEqualityComparer<string> comparer)
		{
			RelationshipType relationshipType;
			if (!reader.TryReadProperty<RelationshipType>("Type", out relationshipType))
			{
				throw new ResponseFormatException(TomSR.Exception_CanNotReadFindTypePropObject("Relationship"));
			}
			if (relationshipType == RelationshipType.SingleColumn)
			{
				return new SingleColumnRelationship(comparer);
			}
			throw new ResponseFormatException(TomSR.Exception_UnrecognizedValueOfType("RelationshipType", relationshipType.ToString()));
		}

		// Token: 0x06001898 RID: 6296 RVA: 0x000A4B34 File Offset: 0x000A2D34
		private static ModelRoleMember CreateModelRoleMemberFromRowset(IPropertyReader reader, IEqualityComparer<string> comparer)
		{
			string text;
			if (!reader.TryReadProperty<string>("IdentityProvider", out text) || string.IsNullOrEmpty(text))
			{
				return new WindowsModelRoleMember(comparer);
			}
			return new ExternalModelRoleMember(comparer);
		}

		// Token: 0x06001899 RID: 6297 RVA: 0x000A4B68 File Offset: 0x000A2D68
		private static ExtendedProperty CreateExtendedPropertyFromRowset(IPropertyReader reader, IEqualityComparer<string> comparer)
		{
			ExtendedPropertyType extendedPropertyType;
			if (!reader.TryReadProperty<ExtendedPropertyType>("Type", out extendedPropertyType))
			{
				throw new ResponseFormatException(TomSR.Exception_CanNotReadFindTypePropObject("ExtendedProperty"));
			}
			if (extendedPropertyType == ExtendedPropertyType.String)
			{
				return new StringExtendedProperty(comparer);
			}
			if (extendedPropertyType != ExtendedPropertyType.Json)
			{
				throw new ResponseFormatException(TomSR.Exception_UnrecognizedValueOfType("ExtendedPropertyType", extendedPropertyType.ToString()));
			}
			return new JsonExtendedProperty(comparer);
		}

		// Token: 0x0600189A RID: 6298 RVA: 0x000A4BC8 File Offset: 0x000A2DC8
		private static RefreshPolicy CreateRefreshPolicyFromRowset(IPropertyReader reader, IEqualityComparer<string> comparer)
		{
			RefreshPolicyType refreshPolicyType;
			if (!reader.TryReadProperty<RefreshPolicyType>("PolicyType", out refreshPolicyType))
			{
				throw new ResponseFormatException(TomSR.Exception_CanNotReadFindTypePropObject("PolicyType"));
			}
			if (refreshPolicyType == RefreshPolicyType.Basic)
			{
				return new BasicRefreshPolicy(comparer);
			}
			throw new ResponseFormatException(TomSR.Exception_UnrecognizedValueOfType("PolicyType", refreshPolicyType.ToString()));
		}

		// Token: 0x0600189B RID: 6299 RVA: 0x000A4C1C File Offset: 0x000A2E1C
		private static BindingInfo CreateBindingInfoFromRowset(IPropertyReader reader, IEqualityComparer<string> comparer)
		{
			BindingInfoType bindingInfoType;
			if (!reader.TryReadProperty<BindingInfoType>("Type", out bindingInfoType))
			{
				throw new ResponseFormatException(TomSR.Exception_CanNotReadFindTypePropObject("BindingInfo"));
			}
			if (bindingInfoType != BindingInfoType.Unknown && bindingInfoType == BindingInfoType.DataBindingHint)
			{
				return new DataBindingHint(comparer);
			}
			throw new ResponseFormatException(TomSR.Exception_UnrecognizedValueOfType("BindingInfoType", bindingInfoType.ToString()));
		}

		// Token: 0x0600189C RID: 6300 RVA: 0x000A4C74 File Offset: 0x000A2E74
		public static MetadataObject CreateMetadataObjectFromJsonObject(ObjectType type, JObject jsonObject)
		{
			switch (type)
			{
			case ObjectType.Model:
				return new Model();
			case ObjectType.DataSource:
				return ObjectFactory.CreateDataSourceFromJsonObject(jsonObject);
			case ObjectType.Table:
				return new Table();
			case ObjectType.Column:
				return ObjectFactory.CreateColumnFromJsonObject(jsonObject);
			case ObjectType.AttributeHierarchy:
				return new AttributeHierarchy();
			case ObjectType.Partition:
				return new Partition();
			case ObjectType.Relationship:
				return ObjectFactory.CreateRelationshipFromJsonObject(jsonObject);
			case ObjectType.Measure:
				return new Measure();
			case ObjectType.Hierarchy:
				return new Hierarchy();
			case ObjectType.Level:
				return new Level();
			case ObjectType.Annotation:
				return new Annotation();
			case ObjectType.KPI:
				return new KPI();
			case ObjectType.Culture:
				return new Culture();
			case ObjectType.ObjectTranslation:
				return new ObjectTranslation();
			case ObjectType.LinguisticMetadata:
				return new LinguisticMetadata();
			case ObjectType.Perspective:
				return new Perspective();
			case ObjectType.PerspectiveTable:
				return new PerspectiveTable();
			case ObjectType.PerspectiveColumn:
				return new PerspectiveColumn();
			case ObjectType.PerspectiveHierarchy:
				return new PerspectiveHierarchy();
			case ObjectType.PerspectiveMeasure:
				return new PerspectiveMeasure();
			case ObjectType.Role:
				return new ModelRole();
			case ObjectType.RoleMembership:
				return ObjectFactory.CreateModelRoleMemberFromJsonObject(jsonObject);
			case ObjectType.TablePermission:
				return new TablePermission();
			case ObjectType.Variation:
				return new Variation();
			case ObjectType.Set:
				return new Set();
			case ObjectType.PerspectiveSet:
				return new PerspectiveSet();
			case ObjectType.ExtendedProperty:
				return ObjectFactory.CreateExtendedPropertyFromJsonObject(jsonObject);
			case ObjectType.Expression:
				return new NamedExpression();
			case ObjectType.ColumnPermission:
				return new ColumnPermission();
			case ObjectType.DetailRowsDefinition:
				return new DetailRowsDefinition();
			case ObjectType.RelatedColumnDetails:
				return new RelatedColumnDetails();
			case ObjectType.GroupByColumn:
				return new GroupByColumn();
			case ObjectType.CalculationGroup:
				return new CalculationGroup();
			case ObjectType.CalculationItem:
				return new CalculationItem();
			case ObjectType.AlternateOf:
				return new AlternateOf();
			case ObjectType.RefreshPolicy:
				return ObjectFactory.CreateRefreshPolicyFromJsonObject(jsonObject);
			case ObjectType.FormatStringDefinition:
				return new FormatStringDefinition();
			case ObjectType.QueryGroup:
				return new QueryGroup();
			case ObjectType.AnalyticsAIMetadata:
				return new AnalyticsAIMetadata();
			case ObjectType.ChangedProperty:
				return new ChangedProperty();
			case ObjectType.ExcludedArtifact:
				return new ExcludedArtifact();
			case ObjectType.DataCoverageDefinition:
				return new DataCoverageDefinition();
			case ObjectType.CalculationExpression:
				return new CalculationGroupExpression();
			case ObjectType.Calendar:
				return new Calendar();
			case ObjectType.TimeUnitColumnAssociation:
				return new TimeUnitColumnAssociation();
			case ObjectType.CalendarColumnReference:
				return new CalendarColumnReference();
			case ObjectType.Function:
				return new Function();
			case ObjectType.BindingInfo:
				return ObjectFactory.CreateBindingInfoFromJsonObject(jsonObject);
			}
			throw new TomInternalException();
		}

		// Token: 0x0600189D RID: 6301 RVA: 0x000A4EBC File Offset: 0x000A30BC
		private static Relationship CreateRelationshipFromJsonObject(JObject jsonObject)
		{
			JToken jtoken;
			if (!jsonObject.TryGetValue("type", ref jtoken))
			{
				return (Relationship)ObjectFactory.CreateDefaultObjectOfType(ObjectType.Relationship);
			}
			RelationshipType relationshipType = JsonPropertyHelper.ConvertJsonValueToEnum<RelationshipType>(jtoken);
			if (relationshipType == RelationshipType.SingleColumn)
			{
				return new SingleColumnRelationship();
			}
			throw JsonSerializationUtil.CreateException(TomSR.Exception_UnrecognizedValueOfType("RelationshipType", relationshipType.ToString()), jtoken, null);
		}

		// Token: 0x0600189E RID: 6302 RVA: 0x000A4F14 File Offset: 0x000A3114
		private static Column CreateColumnFromJsonObject(JObject jsonObject)
		{
			JToken jtoken;
			if (!jsonObject.TryGetValue("type", ref jtoken))
			{
				return (Column)ObjectFactory.CreateDefaultObjectOfType(ObjectType.Column);
			}
			ColumnType columnType = JsonPropertyHelper.ConvertJsonValueToEnum<ColumnType>(jtoken);
			switch (columnType)
			{
			case ColumnType.Data:
				return new DataColumn();
			case ColumnType.Calculated:
				return new CalculatedColumn();
			case ColumnType.RowNumber:
				return new RowNumberColumn();
			case ColumnType.CalculatedTableColumn:
				return new CalculatedTableColumn();
			default:
				throw JsonSerializationUtil.CreateException(TomSR.Exception_UnrecognizedValueOfType("ColumnType", columnType.ToString()), jtoken, null);
			}
		}

		// Token: 0x0600189F RID: 6303 RVA: 0x000A4F94 File Offset: 0x000A3194
		private static DataSource CreateDataSourceFromJsonObject(JObject jsonObject)
		{
			JToken jtoken;
			if (!jsonObject.TryGetValue("type", ref jtoken))
			{
				return (DataSource)ObjectFactory.CreateDefaultObjectOfType(ObjectType.DataSource);
			}
			DataSourceType dataSourceType = JsonPropertyHelper.ConvertJsonValueToEnum<DataSourceType>(jtoken);
			if (dataSourceType == DataSourceType.Provider)
			{
				return new ProviderDataSource();
			}
			if (dataSourceType != DataSourceType.Structured)
			{
				throw JsonSerializationUtil.CreateException(TomSR.Exception_UnrecognizedValueOfType("DataSourceType", dataSourceType.ToString()), jtoken, null);
			}
			return new StructuredDataSource();
		}

		// Token: 0x060018A0 RID: 6304 RVA: 0x000A4FF8 File Offset: 0x000A31F8
		internal static RefreshPolicy CreateRefreshPolicyFromJsonObject(JObject jsonObject)
		{
			JToken jtoken;
			if (!jsonObject.TryGetValue("policyType", ref jtoken))
			{
				return (RefreshPolicy)ObjectFactory.CreateDefaultObjectOfType(ObjectType.RefreshPolicy);
			}
			RefreshPolicyType refreshPolicyType = JsonPropertyHelper.ConvertJsonValueToEnum<RefreshPolicyType>(jtoken);
			if (refreshPolicyType == RefreshPolicyType.Basic)
			{
				return new BasicRefreshPolicy();
			}
			throw JsonSerializationUtil.CreateException(TomSR.Exception_UnrecognizedValueOfType("PolicyType", refreshPolicyType.ToString()), jtoken, null);
		}

		// Token: 0x060018A1 RID: 6305 RVA: 0x000A504F File Offset: 0x000A324F
		private static DataSourceType GuessDataSourceTypeFromJsonObject(JObject jsonObject)
		{
			if (jsonObject["type"] != null)
			{
				return JsonPropertyHelper.ConvertJsonValueToEnum<DataSourceType>(jsonObject["type"]);
			}
			return DataSourceType.Provider;
		}

		// Token: 0x060018A2 RID: 6306 RVA: 0x000A5070 File Offset: 0x000A3270
		private static DataSourceOverride CreateDataSourceOverrideFromJsonObject(JObject jsonObject)
		{
			DataSourceOverride dataSourceOverride;
			if (ObjectFactory.GuessDataSourceTypeFromJsonObject(jsonObject) == DataSourceType.Structured)
			{
				dataSourceOverride = new StructuredDataSourceOverride();
			}
			else
			{
				dataSourceOverride = new ProviderDataSourceOverride();
			}
			JsonTextReader jsonTextReader = new JsonTextReader(new StringReader(jsonObject.ToString()));
			jsonTextReader.Read();
			dataSourceOverride.ReadFromJson(jsonTextReader);
			return dataSourceOverride;
		}

		// Token: 0x060018A3 RID: 6307 RVA: 0x000A50B4 File Offset: 0x000A32B4
		public static DataSourceOverride CreateDataSourceOverrideFromJsonReader(JsonTextReader jsonReader)
		{
			jsonReader.VerifyToken(1);
			return ObjectFactory.CreateDataSourceOverrideFromJsonObject(JObject.Load(jsonReader));
		}

		// Token: 0x060018A4 RID: 6308 RVA: 0x000A50C8 File Offset: 0x000A32C8
		private static PartitionSourceOverride CreatePartitionSourceOverrideByType(PartitionSourceType partitionSourceType)
		{
			if (partitionSourceType == PartitionSourceType.Query)
			{
				return new QueryPartitionSourceOverride();
			}
			if (partitionSourceType != PartitionSourceType.M)
			{
				return null;
			}
			return new MPartitionSourceOverride();
		}

		// Token: 0x060018A5 RID: 6309 RVA: 0x000A50E4 File Offset: 0x000A32E4
		private static PartitionSourceType GuessPartitionSourceTypeFromSourceJsonObject(JObject jsonObject)
		{
			if (jsonObject["type"] != null)
			{
				PartitionSourceType partitionSourceType = JsonPropertyHelper.ConvertJsonValueToEnum<PartitionSourceType>(jsonObject["type"]);
				if (partitionSourceType != PartitionSourceType.Query && partitionSourceType != PartitionSourceType.M)
				{
					throw JsonSerializationUtil.CreateException(TomSR.Exception_UnrecognizedValueOfType("PartitionSourceOverrideType", partitionSourceType.ToString()), jsonObject["type"], null);
				}
				return partitionSourceType;
			}
			else
			{
				if (jsonObject["expression"] != null)
				{
					return PartitionSourceType.M;
				}
				return PartitionSourceType.Query;
			}
		}

		// Token: 0x060018A6 RID: 6310 RVA: 0x000A5154 File Offset: 0x000A3354
		public static PartitionSourceOverride CreatePartitionSourceOverrideFromJsonReader(JsonTextReader jsonReader)
		{
			JObject jobject = JObject.Load(jsonReader);
			if (jobject.Type == 10)
			{
				return null;
			}
			PartitionSourceOverride partitionSourceOverride = ObjectFactory.CreatePartitionSourceOverrideByType(ObjectFactory.GuessPartitionSourceTypeFromSourceJsonObject(jobject));
			partitionSourceOverride.ReadFromJsonObject(jobject);
			return partitionSourceOverride;
		}

		// Token: 0x060018A7 RID: 6311 RVA: 0x000A5188 File Offset: 0x000A3388
		private static ModelRoleMember CreateModelRoleMemberFromJsonObject(JObject jsonObject)
		{
			JToken jtoken;
			if (!jsonObject.TryGetValue("identityProvider", ref jtoken))
			{
				return (ModelRoleMember)ObjectFactory.CreateDefaultObjectOfType(ObjectType.RoleMembership);
			}
			if (string.IsNullOrEmpty(JsonPropertyHelper.ConvertJsonValueToString(jtoken)))
			{
				return new WindowsModelRoleMember();
			}
			return new ExternalModelRoleMember();
		}

		// Token: 0x060018A8 RID: 6312 RVA: 0x000A51CC File Offset: 0x000A33CC
		private static ExtendedProperty CreateExtendedPropertyFromJsonObject(JObject jsonObject)
		{
			JToken jtoken;
			if (!jsonObject.TryGetValue("type", ref jtoken))
			{
				return (ExtendedProperty)ObjectFactory.CreateDefaultObjectOfType(ObjectType.ExtendedProperty);
			}
			ExtendedPropertyType extendedPropertyType = JsonPropertyHelper.ConvertJsonValueToEnum<ExtendedPropertyType>(jtoken);
			if (extendedPropertyType == ExtendedPropertyType.String)
			{
				return new StringExtendedProperty();
			}
			if (extendedPropertyType != ExtendedPropertyType.Json)
			{
				throw JsonSerializationUtil.CreateException(TomSR.Exception_UnrecognizedValueOfType("ExtendedPropertyType", extendedPropertyType.ToString()), jtoken, null);
			}
			return new JsonExtendedProperty();
		}

		// Token: 0x060018A9 RID: 6313 RVA: 0x000A5230 File Offset: 0x000A3430
		private static BindingInfo CreateBindingInfoFromJsonObject(JObject jsonObject)
		{
			JToken jtoken;
			if (!jsonObject.TryGetValue("type", ref jtoken))
			{
				return (BindingInfo)ObjectFactory.CreateDefaultObjectOfType(ObjectType.BindingInfo);
			}
			BindingInfoType bindingInfoType = JsonPropertyHelper.ConvertJsonValueToEnum<BindingInfoType>(jtoken);
			if (bindingInfoType != BindingInfoType.Unknown && bindingInfoType == BindingInfoType.DataBindingHint)
			{
				return new DataBindingHint();
			}
			throw JsonSerializationUtil.CreateException(TomSR.Exception_UnrecognizedValueOfType("BindingInfoType", bindingInfoType.ToString()), jtoken, null);
		}
	}
}
