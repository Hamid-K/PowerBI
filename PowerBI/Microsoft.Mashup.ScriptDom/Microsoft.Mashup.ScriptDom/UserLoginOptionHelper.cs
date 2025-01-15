using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000120 RID: 288
	[Serializable]
	internal class UserLoginOptionHelper : OptionsHelper<UserLoginOptionType>
	{
		// Token: 0x060014B2 RID: 5298 RVA: 0x00090A48 File Offset: 0x0008EC48
		private UserLoginOptionHelper()
		{
			base.AddOptionMapping(UserLoginOptionType.Certificate, "CERTIFICATE");
			base.AddOptionMapping(UserLoginOptionType.Login, "LOGIN");
		}

		// Token: 0x04001126 RID: 4390
		internal static readonly UserLoginOptionHelper Instance = new UserLoginOptionHelper();
	}
}
