using System;

namespace Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Entities
{
	// Token: 0x020001E3 RID: 483
	public class FormattedNumberToken : NumericToken
	{
		// Token: 0x06000A85 RID: 2693 RVA: 0x0001FF4F File Offset: 0x0001E14F
		public FormattedNumberToken(string source, int start, int end, double numericValue)
			: base(source, start, end, numericValue)
		{
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000A86 RID: 2694 RVA: 0x0001FF6B File Offset: 0x0001E16B
		public override string EntityName
		{
			get
			{
				return "Formatted Number";
			}
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000A87 RID: 2695 RVA: 0x0001FF72 File Offset: 0x0001E172
		public override double ScoreMultiplier { get; } = 1.0;
	}
}
