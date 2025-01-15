using System;

namespace Microsoft.AnalysisServices
{
	// Token: 0x02000033 RID: 51
	internal struct MDXMLAProp
	{
		// Token: 0x060001AB RID: 427 RVA: 0x0000A50C File Offset: 0x0000870C
		internal MDXMLAProp(string theOleDbName, string theXmlAName)
		{
			this.strOleDbName = theOleDbName;
			this.strXmlAName = theXmlAName;
		}

		// Token: 0x040001C9 RID: 457
		internal string strOleDbName;

		// Token: 0x040001CA RID: 458
		internal string strXmlAName;
	}
}
