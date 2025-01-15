using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000329 RID: 809
	public sealed class ParagraphInstanceCollection : IEnumerable<ParagraphInstance>, IEnumerable
	{
		// Token: 0x06001E56 RID: 7766 RVA: 0x000760E2 File Offset: 0x000742E2
		internal ParagraphInstanceCollection(TextBox textbox)
		{
			this.m_textbox = textbox;
		}

		// Token: 0x06001E57 RID: 7767 RVA: 0x000760F1 File Offset: 0x000742F1
		public IEnumerator<ParagraphInstance> GetEnumerator()
		{
			return new ParagraphInstanceEnumerator(this.m_textbox);
		}

		// Token: 0x06001E58 RID: 7768 RVA: 0x000760FE File Offset: 0x000742FE
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000F8D RID: 3981
		private TextBox m_textbox;
	}
}
