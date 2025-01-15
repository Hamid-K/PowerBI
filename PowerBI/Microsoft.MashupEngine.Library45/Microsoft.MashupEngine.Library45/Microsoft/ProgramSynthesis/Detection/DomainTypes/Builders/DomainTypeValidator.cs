using System;

namespace Microsoft.ProgramSynthesis.Detection.DomainTypes.Builders
{
	// Token: 0x02000AFC RID: 2812
	internal sealed class DomainTypeValidator
	{
		// Token: 0x17000CA6 RID: 3238
		// (get) Token: 0x0600466C RID: 18028 RVA: 0x000DC63E File Offset: 0x000DA83E
		public Predicate<string> Validator { get; }

		// Token: 0x17000CA7 RID: 3239
		// (get) Token: 0x0600466D RID: 18029 RVA: 0x000DC646 File Offset: 0x000DA846
		public string DomainTypeName { get; }

		// Token: 0x17000CA8 RID: 3240
		// (get) Token: 0x0600466E RID: 18030 RVA: 0x000DC64E File Offset: 0x000DA84E
		// (set) Token: 0x0600466F RID: 18031 RVA: 0x000DC656 File Offset: 0x000DA856
		public int ValidCount { get; private set; }

		// Token: 0x17000CA9 RID: 3241
		// (get) Token: 0x06004670 RID: 18032 RVA: 0x000DC65F File Offset: 0x000DA85F
		// (set) Token: 0x06004671 RID: 18033 RVA: 0x000DC667 File Offset: 0x000DA867
		public int NullStringCount { get; private set; }

		// Token: 0x17000CAA RID: 3242
		// (get) Token: 0x06004672 RID: 18034 RVA: 0x000DC670 File Offset: 0x000DA870
		// (set) Token: 0x06004673 RID: 18035 RVA: 0x000DC678 File Offset: 0x000DA878
		public int EmptyStringCount { get; private set; }

		// Token: 0x17000CAB RID: 3243
		// (get) Token: 0x06004674 RID: 18036 RVA: 0x000DC681 File Offset: 0x000DA881
		// (set) Token: 0x06004675 RID: 18037 RVA: 0x000DC689 File Offset: 0x000DA889
		public int SampleCount { get; private set; }

		// Token: 0x06004676 RID: 18038 RVA: 0x000DC692 File Offset: 0x000DA892
		public DomainTypeValidator(Predicate<string> validator, string domainTypeName)
		{
			this.Validator = validator;
			this.DomainTypeName = domainTypeName;
		}

		// Token: 0x06004677 RID: 18039 RVA: 0x000DC6A8 File Offset: 0x000DA8A8
		public void ProcessSample(string v)
		{
			int num = this.SampleCount + 1;
			this.SampleCount = num;
			if (v == null)
			{
				num = this.NullStringCount + 1;
				this.NullStringCount = num;
				return;
			}
			if (v.Length == 0)
			{
				num = this.EmptyStringCount + 1;
				this.EmptyStringCount = num;
				return;
			}
			if (this.Validator(v))
			{
				num = this.ValidCount + 1;
				this.ValidCount = num;
			}
		}
	}
}
