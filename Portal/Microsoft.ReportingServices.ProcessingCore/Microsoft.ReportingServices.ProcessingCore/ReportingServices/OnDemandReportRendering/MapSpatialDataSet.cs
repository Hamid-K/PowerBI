using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001AE RID: 430
	public sealed class MapSpatialDataSet : MapSpatialData
	{
		// Token: 0x0600111E RID: 4382 RVA: 0x00047E67 File Offset: 0x00046067
		internal MapSpatialDataSet(MapVectorLayer mapVectorLayer, Map map)
			: base(mapVectorLayer, map)
		{
		}

		// Token: 0x17000933 RID: 2355
		// (get) Token: 0x0600111F RID: 4383 RVA: 0x00047E71 File Offset: 0x00046071
		public ReportStringProperty DataSetName
		{
			get
			{
				if (this.m_dataSetName == null && this.MapSpatialDataSetDef.DataSetName != null)
				{
					this.m_dataSetName = new ReportStringProperty(this.MapSpatialDataSetDef.DataSetName);
				}
				return this.m_dataSetName;
			}
		}

		// Token: 0x17000934 RID: 2356
		// (get) Token: 0x06001120 RID: 4384 RVA: 0x00047EA4 File Offset: 0x000460A4
		public ReportStringProperty SpatialField
		{
			get
			{
				if (this.m_spatialField == null && this.MapSpatialDataSetDef.SpatialField != null)
				{
					this.m_spatialField = new ReportStringProperty(this.MapSpatialDataSetDef.SpatialField);
				}
				return this.m_spatialField;
			}
		}

		// Token: 0x17000935 RID: 2357
		// (get) Token: 0x06001121 RID: 4385 RVA: 0x00047ED7 File Offset: 0x000460D7
		public MapFieldNameCollection MapFieldNames
		{
			get
			{
				if (this.m_mapFieldNames == null && this.MapSpatialDataSetDef.MapFieldNames != null)
				{
					this.m_mapFieldNames = new MapFieldNameCollection(this.MapSpatialDataSetDef.MapFieldNames, this.m_map);
				}
				return this.m_mapFieldNames;
			}
		}

		// Token: 0x17000936 RID: 2358
		// (get) Token: 0x06001122 RID: 4386 RVA: 0x00047F10 File Offset: 0x00046110
		internal Microsoft.ReportingServices.OnDemandReportRendering.DataSet DataSet
		{
			get
			{
				string text = "";
				Microsoft.ReportingServices.OnDemandReportRendering.DataSet dataSet = null;
				IDefinitionPath definitionPath = base.MapDef.ParentDefinitionPath;
				while (definitionPath.ParentDefinitionPath != null && !(definitionPath is Microsoft.ReportingServices.OnDemandReportRendering.Report))
				{
					definitionPath = definitionPath.ParentDefinitionPath;
				}
				if (definitionPath is Microsoft.ReportingServices.OnDemandReportRendering.Report)
				{
					text = this.EvaluateDataSetName();
					dataSet = ((Microsoft.ReportingServices.OnDemandReportRendering.Report)definitionPath).DataSets[text];
				}
				if (dataSet == null)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidDataSetName, new object[]
					{
						base.MapDef.MapDef.ObjectType,
						base.MapDef.Name,
						"DataSetName",
						text
					});
				}
				return dataSet;
			}
		}

		// Token: 0x17000937 RID: 2359
		// (get) Token: 0x06001123 RID: 4387 RVA: 0x00047FAF File Offset: 0x000461AF
		internal MapSpatialDataSet MapSpatialDataSetDef
		{
			get
			{
				return (MapSpatialDataSet)base.MapSpatialDataDef;
			}
		}

		// Token: 0x17000938 RID: 2360
		// (get) Token: 0x06001124 RID: 4388 RVA: 0x00047FBC File Offset: 0x000461BC
		public new MapSpatialDataSetInstance Instance
		{
			get
			{
				return (MapSpatialDataSetInstance)this.GetInstance();
			}
		}

		// Token: 0x06001125 RID: 4389 RVA: 0x00047FCC File Offset: 0x000461CC
		private string EvaluateDataSetName()
		{
			ReportStringProperty dataSetName = this.DataSetName;
			if (!dataSetName.IsExpression)
			{
				return dataSetName.Value;
			}
			return this.Instance.DataSetName;
		}

		// Token: 0x06001126 RID: 4390 RVA: 0x00047FFA File Offset: 0x000461FA
		internal override MapSpatialDataInstance GetInstance()
		{
			if (this.m_map.RenderingContext.InstanceAccessDisallowed)
			{
				return null;
			}
			if (this.m_instance == null)
			{
				this.m_instance = new MapSpatialDataSetInstance(this);
			}
			return this.m_instance;
		}

		// Token: 0x06001127 RID: 4391 RVA: 0x0004802A File Offset: 0x0004622A
		internal override void SetNewContext()
		{
			base.SetNewContext();
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
			if (this.m_mapFieldNames != null)
			{
				this.m_mapFieldNames.SetNewContext();
			}
		}

		// Token: 0x0400081A RID: 2074
		private ReportStringProperty m_dataSetName;

		// Token: 0x0400081B RID: 2075
		private ReportStringProperty m_spatialField;

		// Token: 0x0400081C RID: 2076
		private MapFieldNameCollection m_mapFieldNames;
	}
}
