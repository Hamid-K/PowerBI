using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.AdobeAnalytics
{
	// Token: 0x02000F62 RID: 3938
	internal class AdobeAnalyticsDimensionSearch
	{
		// Token: 0x060067F0 RID: 26608 RVA: 0x00165A4C File Offset: 0x00163C4C
		public AdobeAnalyticsDimensionSearch()
		{
			this.FilterKind = AdobeAnalyticsDimensionFilterKind.And;
			this.keywords = EmptyArray<string>.Instance;
		}

		// Token: 0x060067F1 RID: 26609 RVA: 0x00165A66 File Offset: 0x00163C66
		private AdobeAnalyticsDimensionSearch(AdobeAnalyticsDimensionFilterKind kind, string[] keywords)
		{
			this.FilterKind = kind;
			this.keywords = keywords;
		}

		// Token: 0x17001E10 RID: 7696
		// (get) Token: 0x060067F2 RID: 26610 RVA: 0x00165A7C File Offset: 0x00163C7C
		// (set) Token: 0x060067F3 RID: 26611 RVA: 0x00165A84 File Offset: 0x00163C84
		public AdobeAnalyticsDimensionFilterKind FilterKind { get; private set; }

		// Token: 0x17001E11 RID: 7697
		// (get) Token: 0x060067F4 RID: 26612 RVA: 0x00165A8D File Offset: 0x00163C8D
		public IEnumerable<string> Keywords
		{
			get
			{
				return this.keywords;
			}
		}

		// Token: 0x17001E12 RID: 7698
		// (get) Token: 0x060067F5 RID: 26613 RVA: 0x00165A98 File Offset: 0x00163C98
		public string Clause
		{
			get
			{
				if (this.clause == null)
				{
					string text = ((this.FilterKind == AdobeAnalyticsDimensionFilterKind.And) ? " AND " : " OR ");
					string text2 = ((this.FilterKind == AdobeAnalyticsDimensionFilterKind.Select) ? "MATCH " : "CONTAINS ");
					StringBuilder stringBuilder = new StringBuilder();
					foreach (string text3 in this.keywords)
					{
						if (stringBuilder.Length > 0)
						{
							stringBuilder.Append(text);
						}
						stringBuilder.Append('(');
						stringBuilder.Append(text2);
						stringBuilder.Append('\'');
						stringBuilder.Append(text3.Replace("\\", "\\\\").Replace("'", "\\'").Replace("*", "\\*")
							.Replace("?", "\\?")
							.Replace("^", "\\^")
							.Replace("$", "\\$"));
						stringBuilder.Append("')");
					}
					this.clause = stringBuilder.ToString();
				}
				return this.clause;
			}
		}

		// Token: 0x060067F6 RID: 26614 RVA: 0x00165BB2 File Offset: 0x00163DB2
		public AdobeAnalyticsDimensionSearch ConcatKeywords(AdobeAnalyticsDimensionFilterKind kind, params string[] keywords)
		{
			return new AdobeAnalyticsDimensionSearch(kind, this.keywords.Concat(keywords).ToArray<string>());
		}

		// Token: 0x060067F7 RID: 26615 RVA: 0x00165BCB File Offset: 0x00163DCB
		public AdobeAnalyticsDimensionSearch ConcatKeywords(params string[] keywords)
		{
			return new AdobeAnalyticsDimensionSearch(this.FilterKind, this.keywords.Concat(keywords).ToArray<string>());
		}

		// Token: 0x060067F8 RID: 26616 RVA: 0x00165BE9 File Offset: 0x00163DE9
		public ListValue GetKeywordList()
		{
			return ListValue.New(this.keywords);
		}

		// Token: 0x04003932 RID: 14642
		private readonly string[] keywords;

		// Token: 0x04003933 RID: 14643
		private string clause;
	}
}
