using System;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x02000134 RID: 308
	public class AuthenticatedEncryptionResult
	{
		// Token: 0x06000F0B RID: 3851 RVA: 0x0003C2E9 File Offset: 0x0003A4E9
		public AuthenticatedEncryptionResult(SecurityKey key, byte[] ciphertext, byte[] iv, byte[] authenticationTag)
		{
			this.Key = key;
			this.Ciphertext = ciphertext;
			this.IV = iv;
			this.AuthenticationTag = authenticationTag;
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000F0C RID: 3852 RVA: 0x0003C30E File Offset: 0x0003A50E
		// (set) Token: 0x06000F0D RID: 3853 RVA: 0x0003C316 File Offset: 0x0003A516
		public SecurityKey Key { get; private set; }

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000F0E RID: 3854 RVA: 0x0003C31F File Offset: 0x0003A51F
		// (set) Token: 0x06000F0F RID: 3855 RVA: 0x0003C327 File Offset: 0x0003A527
		public byte[] Ciphertext { get; private set; }

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000F10 RID: 3856 RVA: 0x0003C330 File Offset: 0x0003A530
		// (set) Token: 0x06000F11 RID: 3857 RVA: 0x0003C338 File Offset: 0x0003A538
		public byte[] IV { get; private set; }

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000F12 RID: 3858 RVA: 0x0003C341 File Offset: 0x0003A541
		// (set) Token: 0x06000F13 RID: 3859 RVA: 0x0003C349 File Offset: 0x0003A549
		public byte[] AuthenticationTag { get; private set; }
	}
}
