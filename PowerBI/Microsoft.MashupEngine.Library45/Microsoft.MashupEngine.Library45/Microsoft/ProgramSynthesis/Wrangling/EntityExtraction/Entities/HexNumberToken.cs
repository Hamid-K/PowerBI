using System;

namespace Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Entities
{
	// Token: 0x020001E5 RID: 485
	public class HexNumberToken : NumericToken
	{
		// Token: 0x06000A8B RID: 2699 RVA: 0x0001FF93 File Offset: 0x0001E193
		public HexNumberToken(string source, int start, int end, ulong numericValue)
			: base(source, start, end, numericValue)
		{
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000A8C RID: 2700 RVA: 0x0001FFA2 File Offset: 0x0001E1A2
		public override string EntityName
		{
			get
			{
				return "Hexadecimal Number";
			}
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000A8D RID: 2701 RVA: 0x0001FFA9 File Offset: 0x0001E1A9
		public override double ScoreMultiplier
		{
			get
			{
				return base.ScoreMultiplier * 2.0;
			}
		}
	}
}
