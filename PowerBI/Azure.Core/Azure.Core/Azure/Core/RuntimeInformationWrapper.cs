using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Azure.Core
{
	// Token: 0x0200004A RID: 74
	[NullableContext(1)]
	[Nullable(0)]
	internal class RuntimeInformationWrapper
	{
		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x0600022A RID: 554 RVA: 0x00006D83 File Offset: 0x00004F83
		public virtual string FrameworkDescription
		{
			get
			{
				return RuntimeInformation.FrameworkDescription;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x0600022B RID: 555 RVA: 0x00006D8A File Offset: 0x00004F8A
		public virtual string OSDescription
		{
			get
			{
				return RuntimeInformation.OSDescription;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x0600022C RID: 556 RVA: 0x00006D91 File Offset: 0x00004F91
		public virtual Architecture OSArchitecture
		{
			get
			{
				return RuntimeInformation.OSArchitecture;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x0600022D RID: 557 RVA: 0x00006D98 File Offset: 0x00004F98
		public virtual Architecture ProcessArchitecture
		{
			get
			{
				return RuntimeInformation.ProcessArchitecture;
			}
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00006D9F File Offset: 0x00004F9F
		public virtual bool IsOSPlatform(OSPlatform osPlatform)
		{
			return RuntimeInformation.IsOSPlatform(osPlatform);
		}
	}
}
