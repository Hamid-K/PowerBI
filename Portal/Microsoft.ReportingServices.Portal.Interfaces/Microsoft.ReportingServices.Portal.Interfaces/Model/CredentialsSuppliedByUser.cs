using System;

namespace Model
{
	// Token: 0x02000047 RID: 71
	public sealed class CredentialsSuppliedByUser
	{
		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x00002FE3 File Offset: 0x000011E3
		// (set) Token: 0x060001C4 RID: 452 RVA: 0x00002FEB File Offset: 0x000011EB
		public string DisplayText { get; set; }

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x00002FF4 File Offset: 0x000011F4
		// (set) Token: 0x060001C6 RID: 454 RVA: 0x00002FFC File Offset: 0x000011FC
		public bool UseAsWindowsCredentials { get; set; }
	}
}
