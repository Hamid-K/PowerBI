using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000056 RID: 86
	internal class AllowConnectionsOptionsHelper : OptionsHelper<AllowConnectionsOptionKind>
	{
		// Token: 0x060001EC RID: 492 RVA: 0x00006517 File Offset: 0x00004717
		private AllowConnectionsOptionsHelper()
		{
			base.AddOptionMapping(AllowConnectionsOptionKind.All, "ALL");
			base.AddOptionMapping(AllowConnectionsOptionKind.No, "NO");
			base.AddOptionMapping(AllowConnectionsOptionKind.ReadOnly, "READ_ONLY");
			base.AddOptionMapping(AllowConnectionsOptionKind.ReadWrite, "READ_WRITE");
		}

		// Token: 0x04000178 RID: 376
		public static readonly AllowConnectionsOptionsHelper Instance = new AllowConnectionsOptionsHelper();
	}
}
