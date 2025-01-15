using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000115 RID: 277
	internal class ApplicationRoleOptionHelper : OptionsHelper<ApplicationRoleOptionKind>
	{
		// Token: 0x060014A0 RID: 5280 RVA: 0x000907E1 File Offset: 0x0008E9E1
		private ApplicationRoleOptionHelper()
		{
			base.AddOptionMapping(ApplicationRoleOptionKind.DefaultSchema, "DEFAULT_SCHEMA");
			base.AddOptionMapping(ApplicationRoleOptionKind.Password, "PASSWORD");
			base.AddOptionMapping(ApplicationRoleOptionKind.Name, "NAME");
			base.AddOptionMapping(ApplicationRoleOptionKind.Login, "LOGIN");
		}

		// Token: 0x0400110F RID: 4367
		internal static readonly ApplicationRoleOptionHelper Instance = new ApplicationRoleOptionHelper();
	}
}
