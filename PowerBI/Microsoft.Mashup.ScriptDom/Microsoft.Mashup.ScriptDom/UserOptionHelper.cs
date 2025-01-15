using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200011F RID: 287
	[Serializable]
	internal class UserOptionHelper : OptionsHelper<PrincipalOptionKind>
	{
		// Token: 0x060014B0 RID: 5296 RVA: 0x00090A03 File Offset: 0x0008EC03
		private UserOptionHelper()
		{
			base.AddOptionMapping(PrincipalOptionKind.DefaultSchema, "DEFAULT_SCHEMA");
			base.AddOptionMapping(PrincipalOptionKind.DefaultLanguage, "DEFAULT_LANGUAGE");
			base.AddOptionMapping(PrincipalOptionKind.Name, "NAME");
			base.AddOptionMapping(PrincipalOptionKind.Login, "LOGIN");
		}

		// Token: 0x04001125 RID: 4389
		internal static readonly UserOptionHelper Instance = new UserOptionHelper();
	}
}
