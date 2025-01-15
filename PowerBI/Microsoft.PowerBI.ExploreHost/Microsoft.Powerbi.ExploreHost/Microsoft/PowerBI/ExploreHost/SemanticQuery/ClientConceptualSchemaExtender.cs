using System;
using System.Globalization;
using Microsoft.AnalysisServices.Tabular;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Microsoft.PowerBI.ExploreHost.SemanticQuery
{
	// Token: 0x02000036 RID: 54
	internal sealed class ClientConceptualSchemaExtender : IClientConceptualSchemaHelper
	{
		// Token: 0x060001A6 RID: 422 RVA: 0x00005220 File Offset: 0x00003420
		public ClientConceptualSchemaExtender(IModel model, ModelLocation modelLocation)
		{
			this.m_model = model;
			this.m_modelLocation = modelLocation;
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00005236 File Offset: 0x00003436
		public bool HasDirectQueryContent()
		{
			return this.m_model.HasDirectQueryContent;
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00005243 File Offset: 0x00003443
		public ModelLocation GetModelLocation()
		{
			return this.m_modelLocation;
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x0000524B File Offset: 0x0000344B
		public bool SupportsQnA()
		{
			return this.m_model.SupportsQnA;
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00005258 File Offset: 0x00003458
		public bool IsQnaEnabled()
		{
			return true;
		}

		// Token: 0x060001AB RID: 427 RVA: 0x0000525B File Offset: 0x0000345B
		public bool SupportsFastRefresh()
		{
			return this.m_model.SupportsFastRefresh;
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00005268 File Offset: 0x00003468
		public bool SupportsInsights()
		{
			return this.m_model.SupportsInsights;
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00005275 File Offset: 0x00003475
		public InsightsCapabilities GetInsightsCapabilities()
		{
			return this.m_model.InsightsCapabilities;
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00005282 File Offset: 0x00003482
		public bool SupportsCalculatedColumns()
		{
			return this.m_model.SupportsCalculatedColumns;
		}

		// Token: 0x060001AF RID: 431 RVA: 0x0000528F File Offset: 0x0000348F
		public bool SupportsGrouping()
		{
			return this.m_model.SupportsGrouping;
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0000529C File Offset: 0x0000349C
		public bool CanEditChangeDetectionMeasure()
		{
			return this.m_model.CanEditChangeDetectionMeasure;
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x000052A9 File Offset: 0x000034A9
		public bool SupportChangeDetectionMeasureRefresh()
		{
			return this.m_model.SupportChangeDetectionMeasureRefresh;
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x000052B8 File Offset: 0x000034B8
		public QueryableState GetEntityQueryableState(string entityName)
		{
			ITable table = this.GetTable(entityName);
			if (table.HasErrors)
			{
				return new QueryableState(ClientConceptualQueryableState.Error, table.ErrorMessage);
			}
			return new QueryableState(ClientConceptualQueryableState.Queryable, null);
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x000052EC File Offset: 0x000034EC
		public QueryableState GetPropertyQueryableState(string entityName, string propertyName)
		{
			ITable table = this.GetTable(entityName);
			IColumn column = table.FindColumn(propertyName);
			if (column != null)
			{
				if (column.HasErrors)
				{
					return new QueryableState(ClientConceptualQueryableState.Error, column.ErrorMessage);
				}
				return new QueryableState(ClientConceptualQueryableState.Queryable, null);
			}
			else
			{
				IMeasure measure = table.FindMeasure(propertyName);
				if (measure == null)
				{
					return new QueryableState(ClientConceptualQueryableState.Queryable, null);
				}
				if (measure.HasErrors)
				{
					return new QueryableState(ClientConceptualQueryableState.Error, measure.ErrorMessage);
				}
				return new QueryableState(ClientConceptualQueryableState.Queryable, null);
			}
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00005358 File Offset: 0x00003558
		private JObject GetAnnotationObject(IColumn column, string name)
		{
			string annotation = column.GetAnnotation(name);
			if (annotation != null)
			{
				return (JObject)JsonConvert.DeserializeObject(annotation);
			}
			return null;
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x0000537D File Offset: 0x0000357D
		private GroupsMetadata ParseGroupMetadata(IColumn column)
		{
			if (this.GetAnnotationObject(column, "GroupsMetadata") != null)
			{
				return new GroupsMetadata();
			}
			return null;
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00005394 File Offset: 0x00003594
		private BinsMetadata ParseBinMetadata(IColumn column)
		{
			JObject annotationObject = this.GetAnnotationObject(column, "BinsMetadata");
			if (annotationObject != null)
			{
				JToken jtoken = annotationObject["binSize"];
				if (jtoken != null)
				{
					JValue jvalue = jtoken["value"] as JValue;
					JValue jvalue2 = jtoken["unit"] as JValue;
					if (jvalue != null && jvalue2 != null)
					{
						try
						{
							return new BinsMetadata(new ClientConceptualBinningMetadata(new ClientConceptualBinSize(Convert.ToDouble(jvalue.Value, CultureInfo.InvariantCulture), (ConceptualBinUnit)Convert.ToInt32(jvalue2.Value))));
						}
						catch (InvalidCastException)
						{
						}
					}
				}
			}
			return null;
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00005430 File Offset: 0x00003630
		public void GetColumnMetadata(string entityName, string propertyName, out bool calculated, out GroupsMetadata groupsMetadata, out BinsMetadata binsMetadata)
		{
			IColumn column = this.GetTable(entityName).FindColumn(propertyName);
			groupsMetadata = this.ParseGroupMetadata(column);
			binsMetadata = this.ParseBinMetadata(column);
			calculated = column != null && column.Type == ColumnType.Calculated;
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00005470 File Offset: 0x00003670
		public bool IsCalculatedColumn(string entityName, string propertyName)
		{
			IColumn column = this.GetTable(entityName).FindColumn(propertyName);
			return column != null && column.IsCalculated;
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000548A File Offset: 0x0000368A
		public bool IsCalculatedTable(string entityName)
		{
			ITable table = this.GetTable(entityName);
			return table != null && table.IsCalculated;
		}

		// Token: 0x060001BA RID: 442 RVA: 0x000054A0 File Offset: 0x000036A0
		public bool CanDelete(string entityName, string propertyName)
		{
			ITable table = this.GetTable(entityName);
			IMeasure measure = table.FindMeasure(propertyName);
			if (measure == null)
			{
				IColumn column = table.FindColumn(propertyName);
				return column == null || column.CanDelete;
			}
			return measure == null || measure.CanDelete;
		}

		// Token: 0x060001BB RID: 443 RVA: 0x000054DE File Offset: 0x000036DE
		public bool CanRefreshTable(string entityName)
		{
			ITable table = this.GetTable(entityName);
			return table == null || table.CanRefreshIndependently;
		}

		// Token: 0x060001BC RID: 444 RVA: 0x000054F2 File Offset: 0x000036F2
		public bool CanEditTableSource(string entityName)
		{
			ITable table = this.GetTable(entityName);
			return table == null || table.CanEditSource;
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00005506 File Offset: 0x00003706
		public bool CanRenameTable(string entityName)
		{
			ITable table = this.GetTable(entityName);
			return table == null || table.CanRename;
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0000551A File Offset: 0x0000371A
		public bool CanDeleteTable(string entityName)
		{
			ITable table = this.GetTable(entityName);
			return table == null || table.CanDelete;
		}

		// Token: 0x060001BF RID: 447 RVA: 0x0000552E File Offset: 0x0000372E
		public bool CanDeleteHierarchy(string entityName, string hierarchyName)
		{
			IHierarchy hierarchy = this.GetTable(entityName).FindHierarchy(hierarchyName);
			return hierarchy == null || hierarchy.CanDelete;
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00005548 File Offset: 0x00003748
		public bool CanEditMeasure(string entityName, string measureName)
		{
			IMeasure measure = this.GetTable(entityName).FindMeasure(measureName);
			return measure == null || measure.CanEdit;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00005562 File Offset: 0x00003762
		public bool CanDeleteHierarchyLevel(string entityName, string hierarchyName, string hierarchyLevelName)
		{
			IHierarchyLevel hierarchyLevel = this.GetTable(entityName).FindHierarchy(hierarchyName).FindLevel(hierarchyLevelName);
			return hierarchyLevel == null || hierarchyLevel.CanDelete;
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00005582 File Offset: 0x00003782
		public bool CanEditStorageMode(string entityName)
		{
			return this.GetTable(entityName).CanEditStorageMode;
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00005590 File Offset: 0x00003790
		public ClientConceptualEntityMode GetMode(string entityName)
		{
			ModeType? storageMode = this.GetTable(entityName).StorageMode;
			if (storageMode != null)
			{
				switch (storageMode.GetValueOrDefault())
				{
				case ModeType.Import:
					return ClientConceptualEntityMode.Import;
				case ModeType.DirectQuery:
					return ClientConceptualEntityMode.DirectQuery;
				case ModeType.Dual:
					return ClientConceptualEntityMode.Dual;
				}
			}
			return ClientConceptualEntityMode.Unknown;
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x000055DE File Offset: 0x000037DE
		public DataViewCapabilities GetDataViewCapabilities(string entityName)
		{
			return this.GetTable(entityName).GetDataViewCapabilities();
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x000055EC File Offset: 0x000037EC
		private ITable GetTable(string entityName)
		{
			return this.m_model.FindTable(entityName);
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x000055FA File Offset: 0x000037FA
		public bool IsCloudRlsModel()
		{
			return false;
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x000055FD File Offset: 0x000037FD
		public bool IsPushDataModel()
		{
			return false;
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00005600 File Offset: 0x00003800
		public bool IsRealTimeModel()
		{
			return false;
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00005603 File Offset: 0x00003803
		public DateTime? GetRefreshedTime(string entityName)
		{
			return this.GetTable(entityName).RefreshedTime;
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00005611 File Offset: 0x00003811
		public void GetDirectQueryResourceInfo(string entityName, out string sourceType, out string sourceName)
		{
			this.GetTable(entityName).GetDirectQueryResourceInfo(out sourceType, out sourceName);
		}

		// Token: 0x0400009C RID: 156
		private readonly IModel m_model;

		// Token: 0x0400009D RID: 157
		private readonly ModelLocation m_modelLocation;

		// Token: 0x0400009E RID: 158
		private const string GroupsMetadataAnnotationName = "GroupsMetadata";

		// Token: 0x0400009F RID: 159
		private const string BinsMetadataAnnotationName = "BinsMetadata";

		// Token: 0x040000A0 RID: 160
		private const string BinSize = "binSize";

		// Token: 0x040000A1 RID: 161
		private const string BinSizeValue = "value";

		// Token: 0x040000A2 RID: 162
		private const string BinSizeUnit = "unit";
	}
}
