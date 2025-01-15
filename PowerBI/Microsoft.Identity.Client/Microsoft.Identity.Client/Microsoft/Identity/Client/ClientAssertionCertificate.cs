using System;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Identity.Client.PlatformsCommon.Interfaces;

namespace Microsoft.Identity.Client
{
	// Token: 0x02000166 RID: 358
	[Obsolete("Use ConfidentialClientApplicationBuilder.WithCertificate instead. See https://aka.ms/msal-net-3-breaking-changes. ", true)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public sealed class ClientAssertionCertificate
	{
		// Token: 0x06001189 RID: 4489 RVA: 0x0003BED9 File Offset: 0x0003A0D9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public ClientAssertionCertificate(X509Certificate2 certificate)
		{
			throw MigrationHelper.CreateMsalNet3BreakingChangesException();
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x0600118A RID: 4490 RVA: 0x0003BEE6 File Offset: 0x0003A0E6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static int MinKeySizeInBits
		{
			get
			{
				return 2048;
			}
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x0600118B RID: 4491 RVA: 0x0003BEED File Offset: 0x0003A0ED
		[EditorBrowsable(EditorBrowsableState.Never)]
		public X509Certificate2 Certificate
		{
			get
			{
				throw MigrationHelper.CreateMsalNet3BreakingChangesException();
			}
		}

		// Token: 0x0600118C RID: 4492 RVA: 0x0003BEF4 File Offset: 0x0003A0F4
		[EditorBrowsable(EditorBrowsableState.Never)]
		internal byte[] Sign(ICryptographyManager cryptographyManager, string message)
		{
			throw MigrationHelper.CreateMsalNet3BreakingChangesException();
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x0600118D RID: 4493 RVA: 0x0003BEFB File Offset: 0x0003A0FB
		[EditorBrowsable(EditorBrowsableState.Never)]
		internal string Thumbprint
		{
			get
			{
				throw MigrationHelper.CreateMsalNet3BreakingChangesException();
			}
		}
	}
}
