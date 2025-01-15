using System;

namespace Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Entities
{
	// Token: 0x020001E4 RID: 484
	public class GuidToken : DashedNumbersToken
	{
		// Token: 0x06000A88 RID: 2696 RVA: 0x0001C9F3 File Offset: 0x0001ABF3
		public GuidToken(string source, int start, int end)
			: base(source, start, end)
		{
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000A89 RID: 2697 RVA: 0x0001FF7A File Offset: 0x0001E17A
		public override double ScoreMultiplier
		{
			get
			{
				return base.ScoreMultiplier * 2.0;
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000A8A RID: 2698 RVA: 0x0001FF8C File Offset: 0x0001E18C
		public override string EntityName
		{
			get
			{
				return "GUID";
			}
		}
	}
}
