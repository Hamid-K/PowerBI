using System;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020002C4 RID: 708
	internal sealed class ReportSizeCollection : ReportElementCollectionBase<ReportSize>
	{
		// Token: 0x06001AD1 RID: 6865 RVA: 0x0006B8FB File Offset: 0x00069AFB
		internal ReportSizeCollection(int count)
		{
			this.m_reportSizeCollection = new ReportSize[count];
		}

		// Token: 0x17000F2C RID: 3884
		public override ReportSize this[int index]
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

		// Token: 0x17000F2D RID: 3885
		// (get) Token: 0x06001AD4 RID: 6868 RVA: 0x0006B9B8 File Offset: 0x00069BB8
		public override int Count
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

		// Token: 0x04000D59 RID: 3417
		private ReportSize[] m_reportSizeCollection;
	}
}
