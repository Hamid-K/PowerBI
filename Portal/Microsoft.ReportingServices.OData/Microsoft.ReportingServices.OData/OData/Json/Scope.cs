using System;

namespace Microsoft.ReportingServices.OData.Json
{
	// Token: 0x02000017 RID: 23
	internal sealed class Scope
	{
		// Token: 0x060000D6 RID: 214 RVA: 0x00003F90 File Offset: 0x00002190
		public Scope(ScopeType type)
		{
			this.type = type;
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00003F9F File Offset: 0x0000219F
		// (set) Token: 0x060000D8 RID: 216 RVA: 0x00003FA7 File Offset: 0x000021A7
		public int ObjectCount { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00003FB0 File Offset: 0x000021B0
		public ScopeType Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00003FB8 File Offset: 0x000021B8
		// (set) Token: 0x060000DB RID: 219 RVA: 0x00003FC0 File Offset: 0x000021C0
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

		// Token: 0x060000DC RID: 220 RVA: 0x00003FC9 File Offset: 0x000021C9
		public void ClearPendingValueName()
		{
			this.pendingValueName = null;
		}

		// Token: 0x04000089 RID: 137
		private string pendingValueName;

		// Token: 0x0400008A RID: 138
		private readonly ScopeType type;
	}
}
