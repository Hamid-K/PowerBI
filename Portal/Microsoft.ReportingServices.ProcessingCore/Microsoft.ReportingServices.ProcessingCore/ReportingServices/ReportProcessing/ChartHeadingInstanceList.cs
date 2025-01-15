using System;
using System.Collections;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006A3 RID: 1699
	[Serializable]
	internal sealed class ChartHeadingInstanceList : ArrayList
	{
		// Token: 0x06005C79 RID: 23673 RVA: 0x001798DA File Offset: 0x00177ADA
		internal ChartHeadingInstanceList()
		{
		}

		// Token: 0x06005C7A RID: 23674 RVA: 0x001798E2 File Offset: 0x00177AE2
		internal ChartHeadingInstanceList(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x1700207E RID: 8318
		internal ChartHeadingInstance this[int index]
		{
			get
			{
				return (ChartHeadingInstance)base[index];
			}
		}

		// Token: 0x06005C7C RID: 23676 RVA: 0x001798FC File Offset: 0x00177AFC
		internal void Add(ChartHeadingInstance chartHeadingInstance, ReportProcessing.ProcessingContext pc)
		{
			if (this.m_lastHeadingInstance != null)
			{
				this.m_lastHeadingInstance.InstanceInfo.HeadingSpan = chartHeadingInstance.InstanceInfo.HeadingCellIndex - this.m_lastHeadingInstance.InstanceInfo.HeadingCellIndex;
				pc.ChunkManager.AddInstance(this.m_lastHeadingInstance.InstanceInfo, this.m_lastHeadingInstance, pc.InPageSection);
			}
			base.Add(chartHeadingInstance);
			this.m_lastHeadingInstance = chartHeadingInstance;
		}

		// Token: 0x06005C7D RID: 23677 RVA: 0x00179970 File Offset: 0x00177B70
		internal void SetLastHeadingSpan(int currentCellIndex, ReportProcessing.ProcessingContext pc)
		{
			if (this.m_lastHeadingInstance != null)
			{
				this.m_lastHeadingInstance.InstanceInfo.HeadingSpan = currentCellIndex - this.m_lastHeadingInstance.InstanceInfo.HeadingCellIndex;
				pc.ChunkManager.AddInstance(this.m_lastHeadingInstance.InstanceInfo, this.m_lastHeadingInstance, pc.InPageSection);
			}
		}

		// Token: 0x04002F96 RID: 12182
		[NonSerialized]
		private ChartHeadingInstance m_lastHeadingInstance;
	}
}
