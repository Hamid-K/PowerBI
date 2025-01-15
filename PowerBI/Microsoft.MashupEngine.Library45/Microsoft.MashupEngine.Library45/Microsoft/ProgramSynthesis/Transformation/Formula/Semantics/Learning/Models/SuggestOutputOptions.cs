using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Models
{
	// Token: 0x020016EB RID: 5867
	public class SuggestOutputOptions
	{
		// Token: 0x1700215D RID: 8541
		// (get) Token: 0x0600C3B0 RID: 50096 RVA: 0x002A19E4 File Offset: 0x0029FBE4
		// (set) Token: 0x0600C3B1 RID: 50097 RVA: 0x002A19EC File Offset: 0x0029FBEC
		public SuggestOutputDateMatch DateMatch { get; set; }

		// Token: 0x1700215E RID: 8542
		// (get) Token: 0x0600C3B2 RID: 50098 RVA: 0x002A19F5 File Offset: 0x0029FBF5
		// (set) Token: 0x0600C3B3 RID: 50099 RVA: 0x002A19FD File Offset: 0x0029FBFD
		public int SubstringLimit { get; set; } = 10;

		// Token: 0x1700215F RID: 8543
		// (get) Token: 0x0600C3B4 RID: 50100 RVA: 0x002A1A06 File Offset: 0x0029FC06
		// (set) Token: 0x0600C3B5 RID: 50101 RVA: 0x002A1A0E File Offset: 0x0029FC0E
		public int SubstringLimitDefault { get; set; } = 5;

		// Token: 0x17002160 RID: 8544
		// (get) Token: 0x0600C3B6 RID: 50102 RVA: 0x002A1A18 File Offset: 0x0029FC18
		public IReadOnlyList<CultureInfo> SuggestionCultures
		{
			get
			{
				IReadOnlyList<CultureInfo> readOnlyList;
				if ((readOnlyList = this._suggestionCultures) == null)
				{
					readOnlyList = (this._suggestionCultures = this.SuggestionLocales.Select((string l) => new CultureInfo(l)).ToList<CultureInfo>());
				}
				return readOnlyList;
			}
		}

		// Token: 0x17002161 RID: 8545
		// (get) Token: 0x0600C3B7 RID: 50103 RVA: 0x002A1A67 File Offset: 0x0029FC67
		// (set) Token: 0x0600C3B8 RID: 50104 RVA: 0x002A1A6F File Offset: 0x0029FC6F
		public int SuggestionLimit { get; set; } = 50;

		// Token: 0x17002162 RID: 8546
		// (get) Token: 0x0600C3B9 RID: 50105 RVA: 0x002A1A78 File Offset: 0x0029FC78
		// (set) Token: 0x0600C3BA RID: 50106 RVA: 0x002A1A80 File Offset: 0x0029FC80
		public IReadOnlyList<string> SuggestionLocales { get; set; } = new string[] { "en-US" };

		// Token: 0x0600C3BB RID: 50107 RVA: 0x002A1A8C File Offset: 0x0029FC8C
		public override string ToString()
		{
			string text;
			if ((text = this._toString) == null)
			{
				text = (this._toString = string.Concat(new string[]
				{
					string.Format("DateMatch={0};", this.DateMatch),
					string.Format(" SubstringLimit={0};", this.SubstringLimit),
					string.Format(" SubstringLimitDefault={0};", this.SubstringLimitDefault),
					string.Format(" SuggestionLimit={0};", this.SuggestionLimit),
					" SuggestionLocale=",
					string.Join(",", this.SuggestionLocales),
					";"
				}));
			}
			return text;
		}

		// Token: 0x04004C3A RID: 19514
		private IReadOnlyList<CultureInfo> _suggestionCultures;

		// Token: 0x04004C3B RID: 19515
		private string _toString;
	}
}
