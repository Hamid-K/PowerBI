using System;
using System.Collections;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200075D RID: 1885
	[Serializable]
	internal sealed class CustomReportItemHeadingInstanceList : ArrayList
	{
		// Token: 0x06006855 RID: 26709 RVA: 0x0019602E File Offset: 0x0019422E
		internal CustomReportItemHeadingInstanceList()
		{
		}

		// Token: 0x06006856 RID: 26710 RVA: 0x00196036 File Offset: 0x00194236
		internal CustomReportItemHeadingInstanceList(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x170024DB RID: 9435
		internal CustomReportItemHeadingInstance this[int index]
		{
			get
			{
				return (CustomReportItemHeadingInstance)base[index];
			}
		}

		// Token: 0x06006858 RID: 26712 RVA: 0x0019604D File Offset: 0x0019424D
		internal void Add(CustomReportItemHeadingInstance headingInstance, ReportProcessing.ProcessingContext pc)
		{
			if (this.m_lastHeadingInstance != null)
			{
				this.m_lastHeadingInstance.HeadingSpan = headingInstance.HeadingCellIndex - this.m_lastHeadingInstance.HeadingCellIndex;
			}
			base.Add(headingInstance);
			this.m_lastHeadingInstance = headingInstance;
		}

		// Token: 0x06006859 RID: 26713 RVA: 0x00196083 File Offset: 0x00194283
		internal void SetLastHeadingSpan(int currentCellIndex, ReportProcessing.ProcessingContext pc)
		{
			if (this.m_lastHeadingInstance != null)
			{
				this.m_lastHeadingInstance.HeadingSpan = currentCellIndex - this.m_lastHeadingInstance.HeadingCellIndex;
			}
		}

		// Token: 0x0400338A RID: 13194
		[NonSerialized]
		private CustomReportItemHeadingInstance m_lastHeadingInstance;
	}
}
