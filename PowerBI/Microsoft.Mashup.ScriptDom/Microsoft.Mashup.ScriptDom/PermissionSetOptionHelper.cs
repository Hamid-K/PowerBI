using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000144 RID: 324
	[Serializable]
	internal class PermissionSetOptionHelper : OptionsHelper<PermissionSetOption>
	{
		// Token: 0x060014D6 RID: 5334 RVA: 0x00091227 File Offset: 0x0008F427
		private PermissionSetOptionHelper()
		{
			base.AddOptionMapping(PermissionSetOption.Safe, "SAFE");
			base.AddOptionMapping(PermissionSetOption.ExternalAccess, "EXTERNAL_ACCESS");
			base.AddOptionMapping(PermissionSetOption.Unsafe, "UNSAFE");
		}

		// Token: 0x040011DD RID: 4573
		internal static readonly PermissionSetOptionHelper Instance = new PermissionSetOptionHelper();
	}
}
