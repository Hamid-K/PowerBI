using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Models
{
	// Token: 0x020016CD RID: 5837
	public class FormatNumberSymbolDescriptor
	{
		// Token: 0x0600C2DE RID: 49886 RVA: 0x002A0031 File Offset: 0x0029E231
		public FormatNumberSymbolDescriptor(string text, bool prefix, bool isCurrency, bool isPercent)
		{
			this.Text = text;
			this.Prefix = prefix;
			this.IsCurrency = isCurrency;
			this.IsPercent = isPercent;
		}

		// Token: 0x17002122 RID: 8482
		// (get) Token: 0x0600C2DF RID: 49887 RVA: 0x002A0056 File Offset: 0x0029E256
		// (set) Token: 0x0600C2E0 RID: 49888 RVA: 0x002A005E File Offset: 0x0029E25E
		public bool IsCurrency { get; set; }

		// Token: 0x17002123 RID: 8483
		// (get) Token: 0x0600C2E1 RID: 49889 RVA: 0x002A0067 File Offset: 0x0029E267
		public bool IsPercent { get; }

		// Token: 0x17002124 RID: 8484
		// (get) Token: 0x0600C2E2 RID: 49890 RVA: 0x002A006F File Offset: 0x0029E26F
		// (set) Token: 0x0600C2E3 RID: 49891 RVA: 0x002A0077 File Offset: 0x0029E277
		public bool Prefix { get; set; }

		// Token: 0x17002125 RID: 8485
		// (get) Token: 0x0600C2E4 RID: 49892 RVA: 0x002A0080 File Offset: 0x0029E280
		// (set) Token: 0x0600C2E5 RID: 49893 RVA: 0x002A0088 File Offset: 0x0029E288
		public string Text { get; set; }

		// Token: 0x0600C2E6 RID: 49894 RVA: 0x002A0091 File Offset: 0x0029E291
		public override string ToString()
		{
			return string.Format("{0};Prefix={1};", this.Text, this.Prefix);
		}
	}
}
