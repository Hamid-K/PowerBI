using System;
using System.IO.Packaging;
using System.Security.Cryptography.X509Certificates;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x020019F7 RID: 6647
	public class LibraryDescription
	{
		// Token: 0x17002ACB RID: 10955
		// (get) Token: 0x0600A812 RID: 43026 RVA: 0x0022BCAF File Offset: 0x00229EAF
		// (set) Token: 0x0600A813 RID: 43027 RVA: 0x0022BCB7 File Offset: 0x00229EB7
		public string LibraryProvider { get; set; }

		// Token: 0x17002ACC RID: 10956
		// (get) Token: 0x0600A814 RID: 43028 RVA: 0x0022BCC0 File Offset: 0x00229EC0
		// (set) Token: 0x0600A815 RID: 43029 RVA: 0x0022BCC8 File Offset: 0x00229EC8
		public string LibraryIdentifier { get; set; }

		// Token: 0x17002ACD RID: 10957
		// (get) Token: 0x0600A816 RID: 43030 RVA: 0x0022BCD1 File Offset: 0x00229ED1
		// (set) Token: 0x0600A817 RID: 43031 RVA: 0x0022BCD9 File Offset: 0x00229ED9
		public string LibraryVersion { get; set; }

		// Token: 0x17002ACE RID: 10958
		// (get) Token: 0x0600A818 RID: 43032 RVA: 0x0022BCE2 File Offset: 0x00229EE2
		// (set) Token: 0x0600A819 RID: 43033 RVA: 0x0022BCEA File Offset: 0x00229EEA
		public string ModuleName { get; set; }

		// Token: 0x17002ACF RID: 10959
		// (get) Token: 0x0600A81A RID: 43034 RVA: 0x0022BCF3 File Offset: 0x00229EF3
		// (set) Token: 0x0600A81B RID: 43035 RVA: 0x0022BCFB File Offset: 0x00229EFB
		public string ModuleVersion { get; set; }

		// Token: 0x17002AD0 RID: 10960
		// (get) Token: 0x0600A81C RID: 43036 RVA: 0x0022BD04 File Offset: 0x00229F04
		// (set) Token: 0x0600A81D RID: 43037 RVA: 0x0022BD0C File Offset: 0x00229F0C
		public string[] DataSourceKinds { get; set; }

		// Token: 0x17002AD1 RID: 10961
		// (get) Token: 0x0600A81E RID: 43038 RVA: 0x0022BD15 File Offset: 0x00229F15
		// (set) Token: 0x0600A81F RID: 43039 RVA: 0x0022BD1D File Offset: 0x00229F1D
		public VerifyResult Verification { get; set; }

		// Token: 0x17002AD2 RID: 10962
		// (get) Token: 0x0600A820 RID: 43040 RVA: 0x0022BD26 File Offset: 0x00229F26
		// (set) Token: 0x0600A821 RID: 43041 RVA: 0x0022BD2E File Offset: 0x00229F2E
		public X509Certificate2[] Signers { get; set; }

		// Token: 0x17002AD3 RID: 10963
		// (get) Token: 0x0600A822 RID: 43042 RVA: 0x0022BD37 File Offset: 0x00229F37
		// (set) Token: 0x0600A823 RID: 43043 RVA: 0x0022BD3F File Offset: 0x00229F3F
		public byte[] LibraryContents { get; set; }
	}
}
