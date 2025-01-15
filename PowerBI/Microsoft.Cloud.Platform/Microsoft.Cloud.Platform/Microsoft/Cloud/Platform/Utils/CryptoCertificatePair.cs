using System;
using System.Security.Cryptography.X509Certificates;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020001BA RID: 442
	public sealed class CryptoCertificatePair
	{
		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000B6A RID: 2922 RVA: 0x00027D35 File Offset: 0x00025F35
		// (set) Token: 0x06000B6B RID: 2923 RVA: 0x00027D3D File Offset: 0x00025F3D
		public X509Certificate2 Primary { get; private set; }

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000B6C RID: 2924 RVA: 0x00027D46 File Offset: 0x00025F46
		// (set) Token: 0x06000B6D RID: 2925 RVA: 0x00027D4E File Offset: 0x00025F4E
		public X509Certificate2 Secondary { get; private set; }

		// Token: 0x06000B6E RID: 2926 RVA: 0x00027D57 File Offset: 0x00025F57
		public CryptoCertificatePair([NotNull] X509Certificate2 primary, [NotNull] X509Certificate2 secondary)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<X509Certificate2>(primary, "primary");
			ExtendedDiagnostics.EnsureArgumentNotNull<X509Certificate2>(secondary, "secondary");
			this.Primary = primary;
			this.Secondary = secondary;
		}
	}
}
