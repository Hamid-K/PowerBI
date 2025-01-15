using System;

namespace Microsoft.DataShaping.Common.Json
{
	// Token: 0x02000023 RID: 35
	internal sealed class Scope
	{
		// Token: 0x0600013F RID: 319 RVA: 0x00004AFC File Offset: 0x00002CFC
		internal Scope(ScopeType type)
		{
			this.type = type;
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000140 RID: 320 RVA: 0x00004B0B File Offset: 0x00002D0B
		// (set) Token: 0x06000141 RID: 321 RVA: 0x00004B13 File Offset: 0x00002D13
		public int ObjectCount { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000142 RID: 322 RVA: 0x00004B1C File Offset: 0x00002D1C
		public ScopeType Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000143 RID: 323 RVA: 0x00004B24 File Offset: 0x00002D24
		// (set) Token: 0x06000144 RID: 324 RVA: 0x00004B2C File Offset: 0x00002D2C
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

		// Token: 0x06000145 RID: 325 RVA: 0x00004B35 File Offset: 0x00002D35
		public void ClearPendingValueName()
		{
			this.pendingValueName = null;
		}

		// Token: 0x04000066 RID: 102
		private readonly ScopeType type;

		// Token: 0x04000067 RID: 103
		private string pendingValueName;
	}
}
