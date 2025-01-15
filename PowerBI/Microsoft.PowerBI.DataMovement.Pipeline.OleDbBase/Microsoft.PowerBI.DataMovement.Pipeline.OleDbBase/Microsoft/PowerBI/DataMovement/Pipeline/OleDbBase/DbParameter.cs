using System;
using System.Runtime.CompilerServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x0200003E RID: 62
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public class DbParameter
	{
		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000228 RID: 552 RVA: 0x000066ED File Offset: 0x000048ED
		// (set) Token: 0x06000229 RID: 553 RVA: 0x000066F5 File Offset: 0x000048F5
		public uint Ordinal { get; set; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600022A RID: 554 RVA: 0x000066FE File Offset: 0x000048FE
		// (set) Token: 0x0600022B RID: 555 RVA: 0x00006706 File Offset: 0x00004906
		public DBPARAMFLAGS Flags { get; set; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600022C RID: 556 RVA: 0x0000670F File Offset: 0x0000490F
		// (set) Token: 0x0600022D RID: 557 RVA: 0x00006717 File Offset: 0x00004917
		public string Name { get; set; }

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600022E RID: 558 RVA: 0x00006720 File Offset: 0x00004920
		// (set) Token: 0x0600022F RID: 559 RVA: 0x00006728 File Offset: 0x00004928
		public DBLENGTH ParamSize { get; set; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000230 RID: 560 RVA: 0x00006731 File Offset: 0x00004931
		// (set) Token: 0x06000231 RID: 561 RVA: 0x00006739 File Offset: 0x00004939
		public string TypeName { get; set; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000232 RID: 562 RVA: 0x00006742 File Offset: 0x00004942
		// (set) Token: 0x06000233 RID: 563 RVA: 0x0000674A File Offset: 0x0000494A
		public byte Precision { get; set; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000234 RID: 564 RVA: 0x00006753 File Offset: 0x00004953
		// (set) Token: 0x06000235 RID: 565 RVA: 0x0000675B File Offset: 0x0000495B
		public byte Scale { get; set; }
	}
}
