using System;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000751 RID: 1873
	[Serializable]
	internal sealed class ChartDataPointInstance : InstanceInfoOwner
	{
		// Token: 0x060067E9 RID: 26601 RVA: 0x00195136 File Offset: 0x00193336
		internal ChartDataPointInstance(ReportProcessing.ProcessingContext pc, Chart chart, ChartDataPoint dataPointDef, int dataPointIndex)
		{
			this.m_uniqueName = pc.CreateUniqueName();
			this.m_instanceInfo = new ChartDataPointInstanceInfo(pc, chart, dataPointDef, dataPointIndex, this);
		}

		// Token: 0x060067EA RID: 26602 RVA: 0x0019515B File Offset: 0x0019335B
		internal ChartDataPointInstance()
		{
		}

		// Token: 0x170024BA RID: 9402
		// (get) Token: 0x060067EB RID: 26603 RVA: 0x00195163 File Offset: 0x00193363
		// (set) Token: 0x060067EC RID: 26604 RVA: 0x0019516B File Offset: 0x0019336B
		internal int UniqueName
		{
			get
			{
				return this.m_uniqueName;
			}
			set
			{
				this.m_uniqueName = value;
			}
		}

		// Token: 0x060067ED RID: 26605 RVA: 0x00195174 File Offset: 0x00193374
		internal ChartDataPointInstanceInfo GetInstanceInfo(ChunkManager.RenderingChunkManager chunkManager, ChartDataPointList chartDataPoints)
		{
			if (this.m_instanceInfo is OffsetInfo)
			{
				Global.Tracer.Assert(chunkManager != null);
				return chunkManager.GetReader(((OffsetInfo)this.m_instanceInfo).Offset).ReadChartDataPointInstanceInfo(chartDataPoints);
			}
			return (ChartDataPointInstanceInfo)this.m_instanceInfo;
		}

		// Token: 0x170024BB RID: 9403
		// (get) Token: 0x060067EE RID: 26606 RVA: 0x001951C4 File Offset: 0x001933C4
		internal ChartDataPointInstanceInfo InstanceInfo
		{
			get
			{
				if (this.m_instanceInfo is OffsetInfo)
				{
					Global.Tracer.Assert(false, string.Empty);
					return null;
				}
				return (ChartDataPointInstanceInfo)this.m_instanceInfo;
			}
		}

		// Token: 0x060067EF RID: 26607 RVA: 0x001951F0 File Offset: 0x001933F0
		internal new static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.InstanceInfoOwner, new MemberInfoList
			{
				new MemberInfo(MemberName.UniqueName, Token.Int32)
			});
		}

		// Token: 0x04003370 RID: 13168
		private int m_uniqueName;
	}
}
