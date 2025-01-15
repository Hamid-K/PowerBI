using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000103 RID: 259
	internal class AuthenticationTypesHelper : OptionsHelper<AuthenticationTypes>
	{
		// Token: 0x06001492 RID: 5266 RVA: 0x000903C8 File Offset: 0x0008E5C8
		private AuthenticationTypesHelper()
		{
			base.AddOptionMapping(AuthenticationTypes.Basic, "BASIC");
			base.AddOptionMapping(AuthenticationTypes.Digest, "DIGEST");
			base.AddOptionMapping(AuthenticationTypes.Integrated, "INTEGRATED");
			base.AddOptionMapping(AuthenticationTypes.Kerberos, "KERBEROS");
			base.AddOptionMapping(AuthenticationTypes.Ntlm, "NTLM");
		}

		// Token: 0x04000B3C RID: 2876
		internal static readonly AuthenticationTypesHelper Instance = new AuthenticationTypesHelper();
	}
}
