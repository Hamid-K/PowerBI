using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020000F9 RID: 249
	internal class IdentifierCreateLoginOptionsHelper : OptionsHelper<PrincipalOptionKind>
	{
		// Token: 0x0600148C RID: 5260 RVA: 0x0009031E File Offset: 0x0008E51E
		private IdentifierCreateLoginOptionsHelper()
		{
			base.AddOptionMapping(PrincipalOptionKind.DefaultDatabase, "DEFAULT_DATABASE");
			base.AddOptionMapping(PrincipalOptionKind.DefaultLanguage, "DEFAULT_LANGUAGE");
			base.AddOptionMapping(PrincipalOptionKind.Credential, "CREDENTIAL");
		}

		// Token: 0x04000B04 RID: 2820
		internal static readonly IdentifierCreateLoginOptionsHelper Instance = new IdentifierCreateLoginOptionsHelper();
	}
}
