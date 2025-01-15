using System;

namespace Model
{
	// Token: 0x02000048 RID: 72
	public sealed class CredentialsStoredInServer
	{
		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x00003005 File Offset: 0x00001205
		// (set) Token: 0x060001C9 RID: 457 RVA: 0x0000300D File Offset: 0x0000120D
		public string UserName { get; set; }

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060001CA RID: 458 RVA: 0x00003016 File Offset: 0x00001216
		// (set) Token: 0x060001CB RID: 459 RVA: 0x0000301E File Offset: 0x0000121E
		public string Password { get; set; }

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060001CC RID: 460 RVA: 0x00003027 File Offset: 0x00001227
		// (set) Token: 0x060001CD RID: 461 RVA: 0x0000302F File Offset: 0x0000122F
		public bool UseAsWindowsCredentials { get; set; }

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060001CE RID: 462 RVA: 0x00003038 File Offset: 0x00001238
		// (set) Token: 0x060001CF RID: 463 RVA: 0x00003040 File Offset: 0x00001240
		public bool ImpersonateAuthenticatedUser { get; set; }
	}
}
