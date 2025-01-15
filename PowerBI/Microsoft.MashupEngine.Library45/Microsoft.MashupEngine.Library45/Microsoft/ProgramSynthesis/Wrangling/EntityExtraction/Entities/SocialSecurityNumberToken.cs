using System;

namespace Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Entities
{
	// Token: 0x0200020C RID: 524
	public class SocialSecurityNumberToken : DashedNumbersToken
	{
		// Token: 0x06000B4E RID: 2894 RVA: 0x0001C9F3 File Offset: 0x0001ABF3
		public SocialSecurityNumberToken(string source, int start, int end)
			: base(source, start, end)
		{
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000B4F RID: 2895 RVA: 0x0002276F File Offset: 0x0002096F
		public override string EntityName
		{
			get
			{
				return "Social Security Number";
			}
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000B50 RID: 2896 RVA: 0x0001CA05 File Offset: 0x0001AC05
		public override double ScoreMultiplier
		{
			get
			{
				return base.ScoreMultiplier * 5.0;
			}
		}
	}
}
