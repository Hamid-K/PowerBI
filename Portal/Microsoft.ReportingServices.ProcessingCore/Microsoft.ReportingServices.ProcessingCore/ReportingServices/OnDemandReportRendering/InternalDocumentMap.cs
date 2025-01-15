using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020002B9 RID: 697
	internal sealed class InternalDocumentMap : DocumentMap, IDocumentMap, IEnumerator<OnDemandDocumentMapNode>, IDisposable, IEnumerator
	{
		// Token: 0x06001A97 RID: 6807 RVA: 0x0006B195 File Offset: 0x00069395
		internal InternalDocumentMap(DocumentMapReader aReader)
		{
			this.m_reader = aReader;
		}

		// Token: 0x06001A98 RID: 6808 RVA: 0x0006B1A4 File Offset: 0x000693A4
		public override void Close()
		{
			if (this.m_reader != null)
			{
				this.m_reader.Close();
			}
			this.m_reader = null;
			this.m_isClosed = true;
		}

		// Token: 0x17000F1D RID: 3869
		// (get) Token: 0x06001A99 RID: 6809 RVA: 0x0006B1C7 File Offset: 0x000693C7
		OnDemandDocumentMapNode IEnumerator<OnDemandDocumentMapNode>.Current
		{
			get
			{
				return this.Current;
			}
		}

		// Token: 0x17000F1E RID: 3870
		// (get) Token: 0x06001A9A RID: 6810 RVA: 0x0006B1CF File Offset: 0x000693CF
		public override Microsoft.ReportingServices.OnDemandReportRendering.DocumentMapNode Current
		{
			get
			{
				return this.m_reader.Current;
			}
		}

		// Token: 0x06001A9B RID: 6811 RVA: 0x0006B1DC File Offset: 0x000693DC
		public override void Dispose()
		{
			this.Close();
		}

		// Token: 0x17000F1F RID: 3871
		// (get) Token: 0x06001A9C RID: 6812 RVA: 0x0006B1E4 File Offset: 0x000693E4
		object IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		// Token: 0x06001A9D RID: 6813 RVA: 0x0006B1EC File Offset: 0x000693EC
		public override bool MoveNext()
		{
			return this.m_reader.MoveNext();
		}

		// Token: 0x06001A9E RID: 6814 RVA: 0x0006B1F9 File Offset: 0x000693F9
		public override void Reset()
		{
			this.m_reader.Reset();
		}

		// Token: 0x04000D3B RID: 3387
		private DocumentMapReader m_reader;
	}
}
