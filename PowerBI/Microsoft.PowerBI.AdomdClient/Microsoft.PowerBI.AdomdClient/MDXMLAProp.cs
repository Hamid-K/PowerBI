using System;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x0200001B RID: 27
	internal struct MDXMLAProp
	{
		// Token: 0x06000112 RID: 274 RVA: 0x000072F8 File Offset: 0x000054F8
		internal MDXMLAProp(string theOleDbName, string theXmlAName)
		{
			this.strOleDbName = theOleDbName;
			this.strXmlAName = theXmlAName;
		}

		// Token: 0x04000177 RID: 375
		internal string strOleDbName;

		// Token: 0x04000178 RID: 376
		internal string strXmlAName;
	}
}
