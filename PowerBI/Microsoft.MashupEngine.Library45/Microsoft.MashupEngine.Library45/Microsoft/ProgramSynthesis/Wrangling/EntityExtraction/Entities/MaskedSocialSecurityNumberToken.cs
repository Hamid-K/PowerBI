using System;

namespace Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Entities
{
	// Token: 0x020001F1 RID: 497
	public class MaskedSocialSecurityNumberToken : DashedNumbersToken
	{
		// Token: 0x06000ABB RID: 2747 RVA: 0x0001C9F3 File Offset: 0x0001ABF3
		public MaskedSocialSecurityNumberToken(string source, int start, int end)
			: base(source, start, end)
		{
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000ABC RID: 2748 RVA: 0x00020662 File Offset: 0x0001E862
		public override string EntityName
		{
			get
			{
				return "Masked Social Security Number";
			}
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000ABD RID: 2749 RVA: 0x0001CA05 File Offset: 0x0001AC05
		public override double ScoreMultiplier
		{
			get
			{
				return base.ScoreMultiplier * 5.0;
			}
		}
	}
}
