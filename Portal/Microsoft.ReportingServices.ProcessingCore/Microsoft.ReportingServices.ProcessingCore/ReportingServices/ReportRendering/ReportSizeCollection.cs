using System;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x02000035 RID: 53
	internal class ReportSizeCollection
	{
		// Token: 0x060004D6 RID: 1238 RVA: 0x0000EE07 File Offset: 0x0000D007
		internal ReportSizeCollection()
		{
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x0000EE0F File Offset: 0x0000D00F
		internal ReportSizeCollection(int count)
		{
			this.m_reportSizeCollection = new ReportSize[count];
		}

		// Token: 0x170003E3 RID: 995
		public virtual ReportSize this[int index]
		{
			get
			{
				if (0 > index || index >= this.Count)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, new object[] { index, 0, this.Count });
				}
				return this.m_reportSizeCollection[index];
			}
			set
			{
				if (0 > index || index >= this.Count)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, new object[] { index, 0, this.Count });
				}
				this.m_reportSizeCollection[index] = value;
			}
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x060004DA RID: 1242 RVA: 0x0000EECC File Offset: 0x0000D0CC
		public virtual int Count
		{
			get
			{
				if (this.m_reportSizeCollection == null)
				{
					return 0;
				}
				return this.m_reportSizeCollection.Length;
			}
		}

		// Token: 0x040000F6 RID: 246
		private ReportSize[] m_reportSizeCollection;
	}
}
