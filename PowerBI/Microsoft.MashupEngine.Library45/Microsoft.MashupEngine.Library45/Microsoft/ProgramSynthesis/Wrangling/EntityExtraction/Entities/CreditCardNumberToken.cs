using System;

namespace Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Entities
{
	// Token: 0x020001BD RID: 445
	public class CreditCardNumberToken : DashedNumbersToken
	{
		// Token: 0x060009CA RID: 2506 RVA: 0x0001C9F3 File Offset: 0x0001ABF3
		public CreditCardNumberToken(string source, int start, int end)
			: base(source, start, end)
		{
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x060009CB RID: 2507 RVA: 0x0001C9FE File Offset: 0x0001ABFE
		public override string EntityName
		{
			get
			{
				return "Credit Card Number";
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x060009CC RID: 2508 RVA: 0x0001CA05 File Offset: 0x0001AC05
		public override double ScoreMultiplier
		{
			get
			{
				return base.ScoreMultiplier * 5.0;
			}
		}
	}
}
