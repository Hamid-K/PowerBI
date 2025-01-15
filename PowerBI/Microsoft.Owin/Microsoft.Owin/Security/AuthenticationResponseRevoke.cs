using System;

namespace Microsoft.Owin.Security
{
	// Token: 0x02000023 RID: 35
	public class AuthenticationResponseRevoke
	{
		// Token: 0x060001A8 RID: 424 RVA: 0x0000498D File Offset: 0x00002B8D
		public AuthenticationResponseRevoke(string[] authenticationTypes)
			: this(authenticationTypes, new AuthenticationProperties())
		{
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x0000499B File Offset: 0x00002B9B
		public AuthenticationResponseRevoke(string[] authenticationTypes, AuthenticationProperties properties)
		{
			this.AuthenticationTypes = authenticationTypes;
			this.Properties = properties;
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060001AA RID: 426 RVA: 0x000049B1 File Offset: 0x00002BB1
		// (set) Token: 0x060001AB RID: 427 RVA: 0x000049B9 File Offset: 0x00002BB9
		public string[] AuthenticationTypes { get; private set; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060001AC RID: 428 RVA: 0x000049C2 File Offset: 0x00002BC2
		// (set) Token: 0x060001AD RID: 429 RVA: 0x000049CA File Offset: 0x00002BCA
		public AuthenticationProperties Properties { get; private set; }
	}
}
