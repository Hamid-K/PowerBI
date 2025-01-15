using System;

namespace Microsoft.ReportingServices.OData.Json
{
	// Token: 0x0200001A RID: 26
	internal sealed class Scope
	{
		// Token: 0x060000D3 RID: 211 RVA: 0x00003EA4 File Offset: 0x000020A4
		public Scope(ScopeType type)
		{
			this.type = type;
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x00003EB3 File Offset: 0x000020B3
		// (set) Token: 0x060000D5 RID: 213 RVA: 0x00003EBB File Offset: 0x000020BB
		public int ObjectCount { get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00003EC4 File Offset: 0x000020C4
		public ScopeType Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00003ECC File Offset: 0x000020CC
		// (set) Token: 0x060000D8 RID: 216 RVA: 0x00003ED4 File Offset: 0x000020D4
		public string PendingValueName
		{
			get
			{
				return this.pendingValueName;
			}
			set
			{
				this.pendingValueName = value;
			}
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00003EDD File Offset: 0x000020DD
		public void ClearPendingValueName()
		{
			this.pendingValueName = null;
		}

		// Token: 0x040000AC RID: 172
		private string pendingValueName;

		// Token: 0x040000AD RID: 173
		private readonly ScopeType type;
	}
}
