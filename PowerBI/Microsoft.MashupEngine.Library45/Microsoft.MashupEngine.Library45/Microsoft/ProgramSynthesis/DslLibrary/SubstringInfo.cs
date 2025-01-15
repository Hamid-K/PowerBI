using System;

namespace Microsoft.ProgramSynthesis.DslLibrary
{
	// Token: 0x0200080A RID: 2058
	public struct SubstringInfo
	{
		// Token: 0x06002C1D RID: 11293 RVA: 0x0007BCE6 File Offset: 0x00079EE6
		public SubstringInfo(string source, uint start, uint end)
		{
			this.Source = source;
			this.Start = start;
			this.End = end;
		}

		// Token: 0x170007BA RID: 1978
		// (get) Token: 0x06002C1E RID: 11294 RVA: 0x0007BCFD File Offset: 0x00079EFD
		public readonly string Source { get; }

		// Token: 0x170007BB RID: 1979
		// (get) Token: 0x06002C1F RID: 11295 RVA: 0x0007BD05 File Offset: 0x00079F05
		public readonly uint Start { get; }

		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x06002C20 RID: 11296 RVA: 0x0007BD0D File Offset: 0x00079F0D
		public readonly uint End { get; }
	}
}
