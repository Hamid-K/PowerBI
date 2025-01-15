using System;
using System.Runtime.InteropServices;

namespace Microsoft.AnalysisServices.PlatformHost
{
	// Token: 0x02000032 RID: 50
	[ComVisible(true)]
	[Guid("2AB5F25A-1C50-4D17-A36C-A5CEB128F74F")]
	public struct EngineErrorInfo
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060020FA RID: 8442 RVA: 0x0004DC17 File Offset: 0x0004BE17
		public bool IsSet
		{
			get
			{
				return this.ErrorCode != 0L;
			}
		}

		// Token: 0x060020FB RID: 8443 RVA: 0x0004DC23 File Offset: 0x0004BE23
		public EngineErrorInfo(EngineException ex)
		{
			this.ErrorCode = (long)ex.ErrorCode;
			this.Tags = ex.Tags;
			this.Notes = ex.Notes;
		}

		// Token: 0x0400014B RID: 331
		public long ErrorCode;

		// Token: 0x0400014C RID: 332
		public string[] Tags;

		// Token: 0x0400014D RID: 333
		public string[] Notes;
	}
}
