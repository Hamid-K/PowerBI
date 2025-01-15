using System;

namespace Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Entities
{
	// Token: 0x020001F0 RID: 496
	public class MaskedCreditCardNumberToken : DashedNumbersToken
	{
		// Token: 0x06000AB8 RID: 2744 RVA: 0x0001C9F3 File Offset: 0x0001ABF3
		public MaskedCreditCardNumberToken(string source, int start, int end)
			: base(source, start, end)
		{
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000AB9 RID: 2745 RVA: 0x0002065B File Offset: 0x0001E85B
		public override string EntityName
		{
			get
			{
				return "Masked Credit Card Number";
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000ABA RID: 2746 RVA: 0x0001CA05 File Offset: 0x0001AC05
		public override double ScoreMultiplier
		{
			get
			{
				return base.ScoreMultiplier * 5.0;
			}
		}
	}
}
