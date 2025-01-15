using System;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace Microsoft.Mashup.Storage
{
	// Token: 0x0200204E RID: 8270
	public class IdentityContext
	{
		// Token: 0x0600CA62 RID: 51810 RVA: 0x002871B1 File Offset: 0x002853B1
		public IdentityContext(SafeHandle threadIdentity)
		{
			this.threadIdentity = threadIdentity;
		}

		// Token: 0x170030AD RID: 12461
		// (get) Token: 0x0600CA63 RID: 51811 RVA: 0x002871C0 File Offset: 0x002853C0
		public SafeHandle ThreadIdentity
		{
			get
			{
				return this.threadIdentity;
			}
		}

		// Token: 0x170030AE RID: 12462
		// (get) Token: 0x0600CA64 RID: 51812 RVA: 0x002871C8 File Offset: 0x002853C8
		public string Username
		{
			get
			{
				if (this.username == null && this.threadIdentity != null)
				{
					this.username = new WindowsIdentity(this.threadIdentity.DangerousGetHandle()).Name;
				}
				return this.username;
			}
		}

		// Token: 0x040066E5 RID: 26341
		private readonly SafeHandle threadIdentity;

		// Token: 0x040066E6 RID: 26342
		private string username;
	}
}
