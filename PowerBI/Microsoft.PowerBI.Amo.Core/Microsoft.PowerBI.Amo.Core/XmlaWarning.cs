using System;

namespace Microsoft.AnalysisServices
{
	// Token: 0x0200006A RID: 106
	[Serializable]
	public sealed class XmlaWarning : XmlaMessage
	{
		// Token: 0x060005BF RID: 1471 RVA: 0x00022265 File Offset: 0x00020465
		internal XmlaWarning(int warningCode, string description, string source, string helpFile, XmlaMessageLocation location)
			: base(description, source, helpFile, location)
		{
			this.m_warningCode = warningCode;
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060005C0 RID: 1472 RVA: 0x0002227A File Offset: 0x0002047A
		public int WarningCode
		{
			get
			{
				return this.m_warningCode;
			}
		}

		// Token: 0x04000407 RID: 1031
		private int m_warningCode;
	}
}
