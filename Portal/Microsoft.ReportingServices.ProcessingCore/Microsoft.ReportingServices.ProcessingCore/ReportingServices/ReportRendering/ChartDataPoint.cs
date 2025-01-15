using System;
using System.Collections.Specialized;
using System.Globalization;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x0200004E RID: 78
	internal sealed class ChartDataPoint
	{
		// Token: 0x060005EF RID: 1519 RVA: 0x00014754 File Offset: 0x00012954
		internal ChartDataPoint(Chart owner, int seriesIndex, int categoryIndex)
		{
			this.m_owner = owner;
			this.m_seriesIndex = seriesIndex;
			this.m_categoryIndex = categoryIndex;
			if (!owner.NoRows)
			{
				ChartDataPointInstancesList dataPoints = ((ChartInstance)owner.ReportItemInstance).DataPoints;
				this.m_chartDataPointInstance = dataPoints[seriesIndex][categoryIndex];
			}
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x060005F0 RID: 1520 RVA: 0x000147A8 File Offset: 0x000129A8
		public object[] DataValues
		{
			get
			{
				if (this.InstanceInfo == null)
				{
					return null;
				}
				return this.InstanceInfo.DataValues;
			}
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x060005F1 RID: 1521 RVA: 0x000147C0 File Offset: 0x000129C0
		public string DataElementName
		{
			get
			{
				Chart chart = (Chart)this.m_owner.ReportItemDef;
				int num = this.IndexDataPointDefinition(chart);
				return chart.ChartDataPoints[num].DataElementName;
			}
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x060005F2 RID: 1522 RVA: 0x000147F8 File Offset: 0x000129F8
		public DataElementOutputTypes DataElementOutput
		{
			get
			{
				Chart chart = (Chart)this.m_owner.ReportItemDef;
				int num = this.IndexDataPointDefinition(chart);
				return chart.ChartDataPoints[num].DataElementOutput;
			}
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x060005F3 RID: 1523 RVA: 0x00014830 File Offset: 0x00012A30
		public CustomPropertyCollection CustomProperties
		{
			get
			{
				CustomPropertyCollection customPropertyCollection = this.m_customProperties;
				if (this.m_customProperties == null)
				{
					ChartDataPoint chartDataPointDefinition = this.ChartDataPointDefinition;
					Global.Tracer.Assert(chartDataPointDefinition != null);
					if (chartDataPointDefinition.CustomProperties == null)
					{
						return null;
					}
					if (this.m_owner.NoRows)
					{
						customPropertyCollection = new CustomPropertyCollection(chartDataPointDefinition.CustomProperties, null);
					}
					else
					{
						ChartDataPointInstanceInfo instanceInfo = this.InstanceInfo;
						Global.Tracer.Assert(instanceInfo != null);
						customPropertyCollection = new CustomPropertyCollection(chartDataPointDefinition.CustomProperties, instanceInfo.CustomPropertyInstances);
					}
					if (this.m_owner.RenderingContext.CacheState)
					{
						this.m_customProperties = customPropertyCollection;
					}
				}
				return customPropertyCollection;
			}
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x060005F4 RID: 1524 RVA: 0x000148C8 File Offset: 0x00012AC8
		public ImageMapAreasCollection MapAreas
		{
			get
			{
				if (this.m_owner.DataPointMapAreas == null)
				{
					return null;
				}
				int num = this.m_seriesIndex * this.m_owner.DataPointCollection.CategoryCount + this.m_categoryIndex;
				Global.Tracer.Assert(num >= 0 && num < this.m_owner.DataPointMapAreas.Length);
				return this.m_owner.DataPointMapAreas[num];
			}
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x060005F5 RID: 1525 RVA: 0x00014934 File Offset: 0x00012B34
		public ReportUrl HyperLinkURL
		{
			get
			{
				ActionInfo actionInfo = this.m_actionInfo;
				if (actionInfo == null)
				{
					actionInfo = this.ActionInfo;
				}
				if (actionInfo != null)
				{
					return actionInfo.Actions[0].HyperLinkURL;
				}
				return null;
			}
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x060005F6 RID: 1526 RVA: 0x00014968 File Offset: 0x00012B68
		public ReportUrl DrillthroughReport
		{
			get
			{
				ActionInfo actionInfo = this.m_actionInfo;
				if (actionInfo == null)
				{
					actionInfo = this.ActionInfo;
				}
				if (actionInfo != null)
				{
					return actionInfo.Actions[0].DrillthroughReport;
				}
				return null;
			}
		}

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x060005F7 RID: 1527 RVA: 0x0001499C File Offset: 0x00012B9C
		public NameValueCollection DrillthroughParameters
		{
			get
			{
				ActionInfo actionInfo = this.m_actionInfo;
				if (actionInfo == null)
				{
					actionInfo = this.ActionInfo;
				}
				if (actionInfo != null)
				{
					return actionInfo.Actions[0].DrillthroughParameters;
				}
				return null;
			}
		}

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x060005F8 RID: 1528 RVA: 0x000149D0 File Offset: 0x00012BD0
		public string BookmarkLink
		{
			get
			{
				ActionInfo actionInfo = this.m_actionInfo;
				if (actionInfo == null)
				{
					actionInfo = this.ActionInfo;
				}
				if (actionInfo != null)
				{
					return actionInfo.Actions[0].BookmarkLink;
				}
				return null;
			}
		}

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x060005F9 RID: 1529 RVA: 0x00014A04 File Offset: 0x00012C04
		public ActionInfo ActionInfo
		{
			get
			{
				ActionInfo actionInfo = this.m_actionInfo;
				if (actionInfo == null)
				{
					ChartDataPoint chartDataPointDefinition = this.ChartDataPointDefinition;
					if (chartDataPointDefinition != null)
					{
						Microsoft.ReportingServices.ReportProcessing.Action action = chartDataPointDefinition.Action;
						if (action != null)
						{
							ActionInstance actionInstance = null;
							string text = null;
							if (this.m_chartDataPointInstance != null)
							{
								actionInstance = this.InstanceInfo.Action;
								text = this.m_chartDataPointInstance.UniqueName.ToString(CultureInfo.InvariantCulture);
							}
							actionInfo = new ActionInfo(action, actionInstance, text, this.m_owner.RenderingContext);
							if (this.m_owner.RenderingContext.CacheState)
							{
								this.m_actionInfo = actionInfo;
							}
						}
					}
				}
				return actionInfo;
			}
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x00014A94 File Offset: 0x00012C94
		private int IndexDataPointDefinition(Chart chartDef)
		{
			int num;
			if (this.m_owner.NoRows)
			{
				num = this.m_seriesIndex * chartDef.StaticCategoryCount + this.m_categoryIndex;
			}
			else
			{
				num = this.InstanceInfo.DataPointIndex;
			}
			return num;
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x060005FB RID: 1531 RVA: 0x00014AD4 File Offset: 0x00012CD4
		internal ChartDataPointInstanceInfo InstanceInfo
		{
			get
			{
				if (this.m_chartDataPointInstance == null)
				{
					return null;
				}
				if (this.m_chartDataPointInstanceInfo == null)
				{
					this.m_chartDataPointInstanceInfo = this.m_chartDataPointInstance.GetInstanceInfo(this.m_owner.RenderingContext.ChunkManager, ((Chart)this.m_owner.ReportItemDef).ChartDataPoints);
				}
				return this.m_chartDataPointInstanceInfo;
			}
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x060005FC RID: 1532 RVA: 0x00014B30 File Offset: 0x00012D30
		private ChartDataPoint ChartDataPointDefinition
		{
			get
			{
				Chart chart = (Chart)this.m_owner.ReportItemDef;
				if (this.m_owner.NoRows)
				{
					return chart.ChartDataPoints[this.m_seriesIndex * chart.StaticCategoryCount + this.m_categoryIndex];
				}
				if (this.InstanceInfo != null)
				{
					return chart.ChartDataPoints[this.InstanceInfo.DataPointIndex];
				}
				return null;
			}
		}

		// Token: 0x0400017A RID: 378
		private Chart m_owner;

		// Token: 0x0400017B RID: 379
		private int m_seriesIndex;

		// Token: 0x0400017C RID: 380
		private int m_categoryIndex;

		// Token: 0x0400017D RID: 381
		private ChartDataPointInstance m_chartDataPointInstance;

		// Token: 0x0400017E RID: 382
		private ChartDataPointInstanceInfo m_chartDataPointInstanceInfo;

		// Token: 0x0400017F RID: 383
		private CustomPropertyCollection m_customProperties;

		// Token: 0x04000180 RID: 384
		private ActionInfo m_actionInfo;
	}
}
