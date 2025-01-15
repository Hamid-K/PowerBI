using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.AdobeAnalytics
{
	// Token: 0x02000F5E RID: 3934
	internal class AdobeAnalyticsFilterExpression : AdobeAnalyticsExpression
	{
		// Token: 0x060067DF RID: 26591 RVA: 0x0016546D File Offset: 0x0016366D
		private AdobeAnalyticsFilterExpression(IDictionary<string, AdobeAnalyticsDimensionSearch> dimToSearch)
		{
			this.dimensionToSearch = dimToSearch;
		}

		// Token: 0x17001E0F RID: 7695
		// (get) Token: 0x060067E0 RID: 26592 RVA: 0x000023C4 File Offset: 0x000005C4
		public override AdobeAnalyticsExpressionKind Kind
		{
			get
			{
				return AdobeAnalyticsExpressionKind.Filter;
			}
		}

		// Token: 0x060067E1 RID: 26593 RVA: 0x0016547C File Offset: 0x0016367C
		public static AdobeAnalyticsFilterExpression New(AdobeAnalyticsDimensionFilterKind kind, string dimensionId, string value)
		{
			AdobeAnalyticsDimensionSearch adobeAnalyticsDimensionSearch = new AdobeAnalyticsDimensionSearch();
			IDictionary<string, AdobeAnalyticsDimensionSearch> dictionary = new Dictionary<string, AdobeAnalyticsDimensionSearch>();
			dictionary[dimensionId] = adobeAnalyticsDimensionSearch.ConcatKeywords(kind, new string[] { value });
			return new AdobeAnalyticsFilterExpression(dictionary);
		}

		// Token: 0x060067E2 RID: 26594 RVA: 0x001654B4 File Offset: 0x001636B4
		public bool TryMergeOr(AdobeAnalyticsFilterExpression other, out AdobeAnalyticsExpression merged)
		{
			KeyValuePair<string, AdobeAnalyticsDimensionSearch> localPair = this.dimensionToSearch.FirstOrDefault<KeyValuePair<string, AdobeAnalyticsDimensionSearch>>();
			KeyValuePair<string, AdobeAnalyticsDimensionSearch> keyValuePair = other.dimensionToSearch.FirstOrDefault<KeyValuePair<string, AdobeAnalyticsDimensionSearch>>();
			if (this.dimensionToSearch.Count == 1 && other.dimensionToSearch.Count == 1 && localPair.Key == keyValuePair.Key && localPair.Value.FilterKind == AdobeAnalyticsDimensionFilterKind.Or && keyValuePair.Value.FilterKind == AdobeAnalyticsDimensionFilterKind.Or)
			{
				string key = localPair.Key;
				IEnumerable<string> enumerable = keyValuePair.Value.Keywords.Where((string otherKey) => !localPair.Value.Keywords.Any((string localKey) => localKey == otherKey));
				IDictionary<string, AdobeAnalyticsDimensionSearch> dictionary = new Dictionary<string, AdobeAnalyticsDimensionSearch>();
				dictionary[key] = localPair.Value.ConcatKeywords(enumerable.ToArray<string>());
				merged = new AdobeAnalyticsFilterExpression(dictionary);
				return true;
			}
			merged = null;
			return false;
		}

		// Token: 0x060067E3 RID: 26595 RVA: 0x001655A0 File Offset: 0x001637A0
		public bool TryMergeAnd(AdobeAnalyticsFilterExpression other, out AdobeAnalyticsExpression merged)
		{
			AdobeAnalyticsFilterExpression.AdobeAnalyticsFilterBuilder adobeAnalyticsFilterBuilder = new AdobeAnalyticsFilterExpression.AdobeAnalyticsFilterBuilder(new Dictionary<string, AdobeAnalyticsDimensionSearch>(this.dimensionToSearch));
			foreach (KeyValuePair<string, AdobeAnalyticsDimensionSearch> keyValuePair in other.dimensionToSearch)
			{
				if (keyValuePair.Value.FilterKind == AdobeAnalyticsDimensionFilterKind.Or)
				{
					using (IEnumerator<string> enumerator2 = keyValuePair.Value.Keywords.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							string text = enumerator2.Current;
							if (!adobeAnalyticsFilterBuilder.TryAddOr(keyValuePair.Key, text))
							{
								merged = null;
								return false;
							}
						}
						continue;
					}
				}
				if (keyValuePair.Value.FilterKind == AdobeAnalyticsDimensionFilterKind.And)
				{
					using (IEnumerator<string> enumerator2 = keyValuePair.Value.Keywords.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							string text2 = enumerator2.Current;
							if (!adobeAnalyticsFilterBuilder.TryAddAnd(keyValuePair.Key, text2))
							{
								merged = null;
								return false;
							}
						}
						continue;
					}
				}
				if (keyValuePair.Value.FilterKind == AdobeAnalyticsDimensionFilterKind.Select)
				{
					foreach (string text3 in keyValuePair.Value.Keywords)
					{
						if (!adobeAnalyticsFilterBuilder.TryAddSelect(keyValuePair.Key, text3))
						{
							merged = null;
							return false;
						}
					}
				}
			}
			merged = adobeAnalyticsFilterBuilder.ToExpression();
			return true;
		}

		// Token: 0x060067E4 RID: 26596 RVA: 0x00165770 File Offset: 0x00163970
		public bool TryGetClause(string dimension, out string clause)
		{
			AdobeAnalyticsDimensionSearch adobeAnalyticsDimensionSearch;
			if (this.dimensionToSearch.TryGetValue(dimension, out adobeAnalyticsDimensionSearch))
			{
				clause = adobeAnalyticsDimensionSearch.Clause;
				return true;
			}
			clause = null;
			return false;
		}

		// Token: 0x060067E5 RID: 26597 RVA: 0x0016579C File Offset: 0x0016399C
		public IDictionary<string, Value> GetFilterRecords()
		{
			IDictionary<string, Value> dictionary = new Dictionary<string, Value>();
			foreach (KeyValuePair<string, AdobeAnalyticsDimensionSearch> keyValuePair in this.dimensionToSearch)
			{
				ListValue keywordList = keyValuePair.Value.GetKeywordList();
				if (keyValuePair.Value.FilterKind == AdobeAnalyticsDimensionFilterKind.Select)
				{
					dictionary.Add(keyValuePair.Key, RecordValue.New(AdobeAnalyticsFilterExpression.SelectedKey, new Value[] { keywordList }));
				}
				else
				{
					string text = ((keyValuePair.Value.FilterKind == AdobeAnalyticsDimensionFilterKind.And) ? "and" : "or");
					RecordValue recordValue = RecordValue.New(AdobeAnalyticsFilterExpression.SearchRecordKeys, new Value[]
					{
						TextValue.New(text),
						keywordList
					});
					dictionary.Add(keyValuePair.Key, RecordValue.New(AdobeAnalyticsFilterExpression.SearchKey, new Value[] { recordValue }));
				}
			}
			return dictionary;
		}

		// Token: 0x0400392B RID: 14635
		private static readonly Keys SearchKey = Keys.New("search");

		// Token: 0x0400392C RID: 14636
		private static readonly Keys SelectedKey = Keys.New("selected");

		// Token: 0x0400392D RID: 14637
		private static readonly Keys SearchRecordKeys = Keys.New("type", "keywords");

		// Token: 0x0400392E RID: 14638
		private IDictionary<string, AdobeAnalyticsDimensionSearch> dimensionToSearch;

		// Token: 0x02000F5F RID: 3935
		private class AdobeAnalyticsFilterBuilder
		{
			// Token: 0x060067E7 RID: 26599 RVA: 0x001658C0 File Offset: 0x00163AC0
			public AdobeAnalyticsFilterBuilder(IDictionary<string, AdobeAnalyticsDimensionSearch> dimensionToSearch)
			{
				this.dimensionToSearch = dimensionToSearch;
			}

			// Token: 0x060067E8 RID: 26600 RVA: 0x001658CF File Offset: 0x00163ACF
			public AdobeAnalyticsFilterExpression ToExpression()
			{
				return new AdobeAnalyticsFilterExpression(this.dimensionToSearch);
			}

			// Token: 0x060067E9 RID: 26601 RVA: 0x001658DC File Offset: 0x00163ADC
			public bool TryAddAnd(string dimensionId, string value)
			{
				AdobeAnalyticsDimensionSearch adobeAnalyticsDimensionSearch;
				if (!this.dimensionToSearch.TryGetValue(dimensionId, out adobeAnalyticsDimensionSearch))
				{
					adobeAnalyticsDimensionSearch = new AdobeAnalyticsDimensionSearch();
				}
				if (adobeAnalyticsDimensionSearch.FilterKind == AdobeAnalyticsDimensionFilterKind.And)
				{
					this.dimensionToSearch[dimensionId] = adobeAnalyticsDimensionSearch.ConcatKeywords(new string[] { value });
					return true;
				}
				return false;
			}

			// Token: 0x060067EA RID: 26602 RVA: 0x00165928 File Offset: 0x00163B28
			public bool TryAddOr(string dimensionId, string value)
			{
				AdobeAnalyticsDimensionSearch adobeAnalyticsDimensionSearch;
				if (this.dimensionToSearch.Count == 1 && this.dimensionToSearch.TryGetValue(dimensionId, out adobeAnalyticsDimensionSearch) && (adobeAnalyticsDimensionSearch.FilterKind == AdobeAnalyticsDimensionFilterKind.Or || (adobeAnalyticsDimensionSearch.FilterKind == AdobeAnalyticsDimensionFilterKind.And && adobeAnalyticsDimensionSearch.Keywords.Count<string>() == 1)))
				{
					this.dimensionToSearch[dimensionId] = adobeAnalyticsDimensionSearch.ConcatKeywords(AdobeAnalyticsDimensionFilterKind.Or, new string[] { value });
					return true;
				}
				return false;
			}

			// Token: 0x060067EB RID: 26603 RVA: 0x00165994 File Offset: 0x00163B94
			public bool TryAddSelect(string dimensionId, string value)
			{
				AdobeAnalyticsDimensionSearch adobeAnalyticsDimensionSearch;
				if (!this.dimensionToSearch.TryGetValue(dimensionId, out adobeAnalyticsDimensionSearch))
				{
					adobeAnalyticsDimensionSearch = new AdobeAnalyticsDimensionSearch();
					this.dimensionToSearch[dimensionId] = adobeAnalyticsDimensionSearch.ConcatKeywords(AdobeAnalyticsDimensionFilterKind.Select, new string[] { value });
					return true;
				}
				if (adobeAnalyticsDimensionSearch.FilterKind == AdobeAnalyticsDimensionFilterKind.Select)
				{
					this.dimensionToSearch[dimensionId] = adobeAnalyticsDimensionSearch.ConcatKeywords(new string[] { value });
					return true;
				}
				return false;
			}

			// Token: 0x0400392F RID: 14639
			private IDictionary<string, AdobeAnalyticsDimensionSearch> dimensionToSearch;
		}
	}
}
