using System;
using System.Text.RegularExpressions;

namespace Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Utils
{
	// Token: 0x020001AE RID: 430
	public struct TokenPatternMatch
	{
		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000983 RID: 2435 RVA: 0x0001C07B File Offset: 0x0001A27B
		public Group LeftContextGroup
		{
			get
			{
				return this.FullMatch.Groups[TokenPattern.LeftContextGroupName];
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000984 RID: 2436 RVA: 0x0001C092 File Offset: 0x0001A292
		public Group RightContextGroup
		{
			get
			{
				return this.FullMatch.Groups[TokenPattern.RightContextGroupName];
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000985 RID: 2437 RVA: 0x0001C0A9 File Offset: 0x0001A2A9
		public Group ContentGroup
		{
			get
			{
				return this.FullMatch.Groups[TokenPattern.ContentGroupName];
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000986 RID: 2438 RVA: 0x0001C0C0 File Offset: 0x0001A2C0
		public readonly Match FullMatch { get; }

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000987 RID: 2439 RVA: 0x0001C0C8 File Offset: 0x0001A2C8
		public readonly string Source { get; }

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000988 RID: 2440 RVA: 0x0001C0D0 File Offset: 0x0001A2D0
		public readonly TokenPattern Pattern { get; }

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000989 RID: 2441 RVA: 0x0001C0D8 File Offset: 0x0001A2D8
		public int Start
		{
			get
			{
				return this.ContentGroup.Index;
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x0600098A RID: 2442 RVA: 0x0001C0E5 File Offset: 0x0001A2E5
		public int End
		{
			get
			{
				return this.ContentGroup.Index + this.ContentGroup.Length;
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x0600098B RID: 2443 RVA: 0x0001C0FE File Offset: 0x0001A2FE
		public int Length
		{
			get
			{
				return this.ContentGroup.Length;
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x0600098C RID: 2444 RVA: 0x0001C10B File Offset: 0x0001A30B
		public string Value
		{
			get
			{
				return this.ContentGroup.Value;
			}
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x0001C118 File Offset: 0x0001A318
		internal TokenPatternMatch(Match m, string source, TokenPattern pattern)
		{
			this.FullMatch = m;
			this.Source = source;
			this.Pattern = pattern;
		}
	}
}
