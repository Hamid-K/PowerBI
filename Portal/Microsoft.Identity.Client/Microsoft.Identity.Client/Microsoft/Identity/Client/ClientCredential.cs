using System;
using System.ComponentModel;

namespace Microsoft.Identity.Client
{
	// Token: 0x02000167 RID: 359
	[Obsolete("Use ConfidentialClientApplicationBuilder.WithCertificate or WithClientSecret instead. See https://aka.ms/msal-net-3-breaking-changes. ", true)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public sealed class ClientCredential
	{
		// Token: 0x0600118E RID: 4494 RVA: 0x0003BF02 File Offset: 0x0003A102
		[EditorBrowsable(EditorBrowsableState.Never)]
		public ClientCredential(ClientAssertionCertificate certificate)
		{
			throw MigrationHelper.CreateMsalNet3BreakingChangesException();
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x0600118F RID: 4495 RVA: 0x0003BF0F File Offset: 0x0003A10F
		[EditorBrowsable(EditorBrowsableState.Never)]
		internal ClientAssertionCertificate Certificate
		{
			get
			{
				throw MigrationHelper.CreateMsalNet3BreakingChangesException();
			}
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06001190 RID: 4496 RVA: 0x0003BF16 File Offset: 0x0003A116
		// (set) Token: 0x06001191 RID: 4497 RVA: 0x0003BF1D File Offset: 0x0003A11D
		[EditorBrowsable(EditorBrowsableState.Never)]
		internal string Assertion
		{
			get
			{
				throw MigrationHelper.CreateMsalNet3BreakingChangesException();
			}
			set
			{
				throw MigrationHelper.CreateMsalNet3BreakingChangesException();
			}
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06001192 RID: 4498 RVA: 0x0003BF24 File Offset: 0x0003A124
		// (set) Token: 0x06001193 RID: 4499 RVA: 0x0003BF2B File Offset: 0x0003A12B
		[EditorBrowsable(EditorBrowsableState.Never)]
		internal long ValidTo
		{
			get
			{
				throw MigrationHelper.CreateMsalNet3BreakingChangesException();
			}
			set
			{
				throw MigrationHelper.CreateMsalNet3BreakingChangesException();
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06001194 RID: 4500 RVA: 0x0003BF32 File Offset: 0x0003A132
		// (set) Token: 0x06001195 RID: 4501 RVA: 0x0003BF39 File Offset: 0x0003A139
		[EditorBrowsable(EditorBrowsableState.Never)]
		internal bool ContainsX5C
		{
			get
			{
				throw MigrationHelper.CreateMsalNet3BreakingChangesException();
			}
			set
			{
				throw MigrationHelper.CreateMsalNet3BreakingChangesException();
			}
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06001196 RID: 4502 RVA: 0x0003BF40 File Offset: 0x0003A140
		// (set) Token: 0x06001197 RID: 4503 RVA: 0x0003BF47 File Offset: 0x0003A147
		[EditorBrowsable(EditorBrowsableState.Never)]
		internal string Audience
		{
			get
			{
				throw MigrationHelper.CreateMsalNet3BreakingChangesException();
			}
			set
			{
				throw MigrationHelper.CreateMsalNet3BreakingChangesException();
			}
		}

		// Token: 0x06001198 RID: 4504 RVA: 0x0003BF4E File Offset: 0x0003A14E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public ClientCredential(string secret)
		{
			throw MigrationHelper.CreateMsalNet3BreakingChangesException();
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06001199 RID: 4505 RVA: 0x0003BF5B File Offset: 0x0003A15B
		[EditorBrowsable(EditorBrowsableState.Never)]
		internal string Secret
		{
			get
			{
				throw MigrationHelper.CreateMsalNet3BreakingChangesException();
			}
		}
	}
}
