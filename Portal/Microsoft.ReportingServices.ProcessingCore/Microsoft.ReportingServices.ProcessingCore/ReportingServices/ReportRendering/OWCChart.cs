using System;
using System.Drawing.Imaging;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x02000055 RID: 85
	internal sealed class OWCChart : DataRegion
	{
		// Token: 0x0600065A RID: 1626 RVA: 0x00018C12 File Offset: 0x00016E12
		internal OWCChart(int intUniqueName, OWCChart reportItemDef, OWCChartInstance reportItemInstance, RenderingContext renderingContext)
			: base(intUniqueName, reportItemDef, reportItemInstance, renderingContext)
		{
		}

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x0600065B RID: 1627 RVA: 0x00018C20 File Offset: 0x00016E20
		public OWCChartColumnCollection ChartData
		{
			get
			{
				OWCChartColumnCollection owcchartColumnCollection = this.m_chartData;
				if (this.m_chartData == null)
				{
					owcchartColumnCollection = new OWCChartColumnCollection((OWCChart)base.ReportItemDef, (OWCChartInstance)base.ReportItemInstance, this);
					if (base.RenderingContext.CacheState)
					{
						this.m_chartData = owcchartColumnCollection;
					}
				}
				return owcchartColumnCollection;
			}
		}

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x0600065C RID: 1628 RVA: 0x00018C6E File Offset: 0x00016E6E
		public string ChartDefinition
		{
			get
			{
				return ((OWCChart)base.ReportItemDef).ChartDefinition;
			}
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x0600065D RID: 1629 RVA: 0x00018C80 File Offset: 0x00016E80
		public override bool NoRows
		{
			get
			{
				int count = ((OWCChart)base.ReportItemDef).ChartData.Count;
				bool flag = true;
				if (count > 0)
				{
					OWCChartInstanceInfo owcchartInstanceInfo = (OWCChartInstanceInfo)base.InstanceInfo;
					for (int i = 0; i < count; i++)
					{
						if (0 < owcchartInstanceInfo[i].Count)
						{
							flag = false;
							break;
						}
					}
				}
				return flag;
			}
		}

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x0600065E RID: 1630 RVA: 0x00018CD5 File Offset: 0x00016ED5
		internal override string InstanceInfoNoRowMessage
		{
			get
			{
				if (base.InstanceInfo != null)
				{
					return ((OWCChartInstanceInfo)base.InstanceInfo).NoRows;
				}
				return null;
			}
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x00018CF1 File Offset: 0x00016EF1
		public void ChartDataXML(IChartStream chartStream)
		{
			((OWCChartInstanceInfo)base.InstanceInfo).ChartDataXML(chartStream);
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x00018D04 File Offset: 0x00016F04
		internal bool ProcessChartXMLPivotList(ref string newDefinition, string chartDataUrl)
		{
			return false;
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x00018D07 File Offset: 0x00016F07
		public Metafile GetImage()
		{
			return null;
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x00018D0A File Offset: 0x00016F0A
		public byte[] GetChart()
		{
			return null;
		}

		// Token: 0x040001A2 RID: 418
		private OWCChartColumnCollection m_chartData;
	}
}
