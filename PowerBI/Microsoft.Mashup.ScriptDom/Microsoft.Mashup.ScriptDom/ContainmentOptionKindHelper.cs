using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200004F RID: 79
	internal class ContainmentOptionKindHelper : OptionsHelper<ContainmentOptionKind>
	{
		// Token: 0x060001E8 RID: 488 RVA: 0x0000648F File Offset: 0x0000468F
		private ContainmentOptionKindHelper()
		{
			base.AddOptionMapping(ContainmentOptionKind.None, "NONE");
			base.AddOptionMapping(ContainmentOptionKind.Partial, "PARTIAL");
		}

		// Token: 0x0400015E RID: 350
		internal static readonly ContainmentOptionKindHelper Instance = new ContainmentOptionKindHelper();
	}
}
