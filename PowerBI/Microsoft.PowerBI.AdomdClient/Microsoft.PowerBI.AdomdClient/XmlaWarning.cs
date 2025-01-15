using System;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x0200004D RID: 77
	internal sealed class XmlaWarning : XmlaMessage
	{
		// Token: 0x060004FB RID: 1275 RVA: 0x0001E595 File Offset: 0x0001C795
		internal XmlaWarning(int warningCode, string description, string source, string helpFile, XmlaMessageLocation location)
			: base(description, source, helpFile, location)
		{
			this.m_warningCode = warningCode;
		}

		// Token: 0x040003CB RID: 971
		private int m_warningCode;
	}
}
