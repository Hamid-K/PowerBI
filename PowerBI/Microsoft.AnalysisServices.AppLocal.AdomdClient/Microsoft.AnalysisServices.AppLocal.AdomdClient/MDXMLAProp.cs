using System;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x0200001B RID: 27
	internal struct MDXMLAProp
	{
		// Token: 0x0600011F RID: 287 RVA: 0x000075F8 File Offset: 0x000057F8
		internal MDXMLAProp(string theOleDbName, string theXmlAName)
		{
			this.strOleDbName = theOleDbName;
			this.strXmlAName = theXmlAName;
		}

		// Token: 0x04000184 RID: 388
		internal string strOleDbName;

		// Token: 0x04000185 RID: 389
		internal string strXmlAName;
	}
}
