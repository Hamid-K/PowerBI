using System;
using System.Collections;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x02000011 RID: 17
	internal sealed class PageReportItems
	{
		// Token: 0x170002F1 RID: 753
		public ReportItem this[int index]
		{
			get
			{
				if (0 > index || index >= this.Count)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, new object[] { index, 0, this.Count });
				}
				return (ReportItem)this.m_innerArrayList[index];
			}
			set
			{
				if (0 > index || index >= this.Count)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, new object[] { index, 0, this.Count });
				}
				this.m_innerArrayList[index] = value;
			}
		}

		// Token: 0x06000321 RID: 801 RVA: 0x00007B0C File Offset: 0x00005D0C
		public void Add(ReportItem value)
		{
			if (value != null)
			{
				this.m_innerArrayList.Add(value);
			}
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000322 RID: 802 RVA: 0x00007B1E File Offset: 0x00005D1E
		public int Count
		{
			get
			{
				return this.m_innerArrayList.Count;
			}
		}

		// Token: 0x06000323 RID: 803 RVA: 0x00007B2B File Offset: 0x00005D2B
		public void Clear()
		{
			this.m_innerArrayList.Clear();
		}

		// Token: 0x04000031 RID: 49
		private ArrayList m_innerArrayList = new ArrayList();
	}
}
