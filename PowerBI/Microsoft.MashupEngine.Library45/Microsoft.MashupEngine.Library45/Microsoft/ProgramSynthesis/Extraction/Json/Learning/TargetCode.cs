using System;

namespace Microsoft.ProgramSynthesis.Extraction.Json.Learning
{
	// Token: 0x02000B84 RID: 2948
	public class TargetCode
	{
		// Token: 0x06004AE5 RID: 19173 RVA: 0x000EB530 File Offset: 0x000E9730
		public TargetCode(string import, string code)
		{
			this.Import = import;
			this.Code = code;
		}

		// Token: 0x17000D6B RID: 3435
		// (get) Token: 0x06004AE6 RID: 19174 RVA: 0x000EB546 File Offset: 0x000E9746
		public string Import { get; }

		// Token: 0x17000D6C RID: 3436
		// (get) Token: 0x06004AE7 RID: 19175 RVA: 0x000EB54E File Offset: 0x000E974E
		public string Code { get; }
	}
}
