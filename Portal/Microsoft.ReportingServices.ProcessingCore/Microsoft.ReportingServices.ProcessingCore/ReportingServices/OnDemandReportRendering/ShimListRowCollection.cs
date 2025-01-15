using System;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000351 RID: 849
	internal sealed class ShimListRowCollection : TablixRowCollection
	{
		// Token: 0x0600209E RID: 8350 RVA: 0x0007EC83 File Offset: 0x0007CE83
		internal ShimListRowCollection(Tablix owner)
			: base(owner)
		{
			this.m_row = new ShimListRow(owner);
		}

		// Token: 0x17001267 RID: 4711
		public override TablixRow this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, new object[] { index, 0, this.Count });
				}
				return this.m_row;
			}
		}

		// Token: 0x17001268 RID: 4712
		// (get) Token: 0x060020A0 RID: 8352 RVA: 0x0007ECE9 File Offset: 0x0007CEE9
		public override int Count
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x04001067 RID: 4199
		private ShimListRow m_row;
	}
}
