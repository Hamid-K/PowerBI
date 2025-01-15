using System;
using Microsoft.ProgramSynthesis.Wrangling.AutoCompletion;
using Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Entities
{
	// Token: 0x020001BE RID: 446
	public class CurrencyToken : NumericToken
	{
		// Token: 0x060009CD RID: 2509 RVA: 0x0001CA17 File Offset: 0x0001AC17
		public CurrencyToken(string source, int start, int end, double numericValue, string currencySymbol)
			: base(source, start, end, numericValue)
		{
			this.CurrencySymbol = currencySymbol;
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x060009CE RID: 2510 RVA: 0x0001CA2C File Offset: 0x0001AC2C
		public string CurrencySymbol { get; }

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x060009CF RID: 2511 RVA: 0x0001C63C File Offset: 0x0001A83C
		public override double ScoreMultiplier
		{
			get
			{
				return 1.5;
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x060009D0 RID: 2512 RVA: 0x0001CA34 File Offset: 0x0001AC34
		public override string EntityName
		{
			get
			{
				return "Currency Value";
			}
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x0001CA3C File Offset: 0x0001AC3C
		public override void MakeSearchTreeEntries(IAutoCompleteSearchTree tree, bool includeNonExtensionCompletions = false)
		{
			foreach (CompletionInfo completionInfo in base.GetSearchTreeEntries())
			{
				tree.Add(completionInfo.Key, completionInfo);
				tree.Add(this.CurrencySymbol + completionInfo.Key, completionInfo.CloneWithValue(this.CurrencySymbol + completionInfo.Key, null));
				tree.Add(completionInfo.Key + this.CurrencySymbol, completionInfo.CloneWithValue(completionInfo.Key + this.CurrencySymbol, null));
			}
		}
	}
}
