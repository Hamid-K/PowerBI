using System;

namespace Microsoft.PowerBI.ExploreHost
{
	// Token: 0x02000033 RID: 51
	public sealed class SemanticQueryDiagnosticsInfo
	{
		// Token: 0x06000176 RID: 374 RVA: 0x00004716 File Offset: 0x00002916
		public SemanticQueryDiagnosticsInfo(string csdl)
		{
			this.Csdl = csdl;
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000177 RID: 375 RVA: 0x00004725 File Offset: 0x00002925
		public string Csdl { get; }
	}
}
