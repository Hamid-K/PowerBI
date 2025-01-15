using System;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x0200004D RID: 77
	internal sealed class XmlaWarning : XmlaMessage
	{
		// Token: 0x06000508 RID: 1288 RVA: 0x0001E8C5 File Offset: 0x0001CAC5
		internal XmlaWarning(int warningCode, string description, string source, string helpFile, XmlaMessageLocation location)
			: base(description, source, helpFile, location)
		{
			this.m_warningCode = warningCode;
		}

		// Token: 0x040003D8 RID: 984
		private int m_warningCode;
	}
}
