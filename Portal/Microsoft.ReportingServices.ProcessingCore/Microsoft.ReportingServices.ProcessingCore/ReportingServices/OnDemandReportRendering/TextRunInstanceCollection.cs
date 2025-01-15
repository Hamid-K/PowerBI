using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200032A RID: 810
	public sealed class TextRunInstanceCollection : IEnumerable<TextRunInstance>, IEnumerable
	{
		// Token: 0x06001E59 RID: 7769 RVA: 0x00076106 File Offset: 0x00074306
		internal TextRunInstanceCollection(ParagraphInstance paragraphInstance)
		{
			this.m_paragraphInstance = paragraphInstance;
		}

		// Token: 0x06001E5A RID: 7770 RVA: 0x00076115 File Offset: 0x00074315
		public IEnumerator<TextRunInstance> GetEnumerator()
		{
			return new TextRunInstanceEnumerator(this.m_paragraphInstance);
		}

		// Token: 0x06001E5B RID: 7771 RVA: 0x00076122 File Offset: 0x00074322
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000F8E RID: 3982
		private ParagraphInstance m_paragraphInstance;
	}
}
