using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020000FB RID: 251
	internal class SecurityLoginOptionsHelper : OptionsHelper<PrincipalOptionKind>
	{
		// Token: 0x0600148E RID: 5262 RVA: 0x00090356 File Offset: 0x0008E556
		private SecurityLoginOptionsHelper()
		{
			base.AddOptionMapping(PrincipalOptionKind.CheckExpiration, "CHECK_EXPIRATION");
			base.AddOptionMapping(PrincipalOptionKind.CheckPolicy, "CHECK_POLICY");
		}

		// Token: 0x04000B09 RID: 2825
		internal static readonly SecurityLoginOptionsHelper Instance = new SecurityLoginOptionsHelper();
	}
}
