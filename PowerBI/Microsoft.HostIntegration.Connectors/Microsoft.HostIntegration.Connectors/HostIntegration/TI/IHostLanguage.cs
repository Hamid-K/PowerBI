using System;

namespace Microsoft.HostIntegration.TI
{
	// Token: 0x0200073C RID: 1852
	public interface IHostLanguage
	{
		// Token: 0x17000CF1 RID: 3313
		// (get) Token: 0x060039F8 RID: 14840
		// (set) Token: 0x060039F9 RID: 14841
		string Description { get; set; }

		// Token: 0x17000CF2 RID: 3314
		// (get) Token: 0x060039FA RID: 14842
		// (set) Token: 0x060039FB RID: 14843
		string DevImport { get; set; }

		// Token: 0x17000CF3 RID: 3315
		// (get) Token: 0x060039FC RID: 14844
		// (set) Token: 0x060039FD RID: 14845
		string LanguageExtension { get; set; }

		// Token: 0x17000CF4 RID: 3316
		// (get) Token: 0x060039FE RID: 14846
		// (set) Token: 0x060039FF RID: 14847
		string DisplayName { get; set; }

		// Token: 0x17000CF5 RID: 3317
		// (get) Token: 0x06003A00 RID: 14848
		// (set) Token: 0x06003A01 RID: 14849
		string FileExt { get; set; }

		// Token: 0x17000CF6 RID: 3318
		// (get) Token: 0x06003A02 RID: 14850
		// (set) Token: 0x06003A03 RID: 14851
		string ImporterAssembly { get; set; }

		// Token: 0x17000CF7 RID: 3319
		// (get) Token: 0x06003A04 RID: 14852
		// (set) Token: 0x06003A05 RID: 14853
		string ImporterClass { get; set; }

		// Token: 0x17000CF8 RID: 3320
		// (get) Token: 0x06003A06 RID: 14854
		// (set) Token: 0x06003A07 RID: 14855
		string Name { get; set; }

		// Token: 0x17000CF9 RID: 3321
		// (get) Token: 0x06003A08 RID: 14856
		// (set) Token: 0x06003A09 RID: 14857
		string GUID { get; set; }

		// Token: 0x17000CFA RID: 3322
		// (get) Token: 0x06003A0A RID: 14858
		// (set) Token: 0x06003A0B RID: 14859
		bool IsCOBOL { get; set; }

		// Token: 0x17000CFB RID: 3323
		// (get) Token: 0x06003A0C RID: 14860
		// (set) Token: 0x06003A0D RID: 14861
		bool IsRPG { get; set; }

		// Token: 0x17000CFC RID: 3324
		// (get) Token: 0x06003A0E RID: 14862
		// (set) Token: 0x06003A0F RID: 14863
		bool IsPLI { get; set; }
	}
}
