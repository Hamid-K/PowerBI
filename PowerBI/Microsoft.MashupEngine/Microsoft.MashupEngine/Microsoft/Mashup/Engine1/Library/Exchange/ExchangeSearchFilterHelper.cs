using System;
using System.Text;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Mashup.Engine1.Library.Exchange
{
	// Token: 0x02000BED RID: 3053
	internal static class ExchangeSearchFilterHelper
	{
		// Token: 0x06005326 RID: 21286 RVA: 0x00119908 File Offset: 0x00117B08
		private static SearchFilter.RelationalFilter GetRelationSearchFilter(FilterOptions option)
		{
			switch (option)
			{
			case FilterOptions.Equals:
				return new SearchFilter.IsEqualTo();
			case FilterOptions.IsGreaterThan:
				return new SearchFilter.IsGreaterThan();
			case FilterOptions.IsLessThan:
				return new SearchFilter.IsLessThan();
			case FilterOptions.IsGreaterOrEqual:
				return new SearchFilter.IsGreaterThanOrEqualTo();
			case FilterOptions.IsLessOrEqual:
				return new SearchFilter.IsLessThanOrEqualTo();
			case FilterOptions.NotEquals:
				return new SearchFilter.IsNotEqualTo();
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06005327 RID: 21287 RVA: 0x00119960 File Offset: 0x00117B60
		public static SearchFilter GetSearchFilterCollection(LogicalOperator op, SearchFilter left, SearchFilter right)
		{
			if (left == null && right == null)
			{
				return null;
			}
			if (left == null || right == null)
			{
				return left ?? right;
			}
			return new SearchFilter.SearchFilterCollection(op, new SearchFilter[] { left, right });
		}

		// Token: 0x06005328 RID: 21288 RVA: 0x0011998B File Offset: 0x00117B8B
		public static SearchFilter GetSearchFilter(FilterOptions filterOption, PropertyDefinitionBase property, object value)
		{
			if (filterOption != FilterOptions.ContainsString)
			{
				SearchFilter.RelationalFilter relationSearchFilter = ExchangeSearchFilterHelper.GetRelationSearchFilter(filterOption);
				relationSearchFilter.PropertyDefinition = property;
				relationSearchFilter.Value = value;
				return relationSearchFilter;
			}
			return new SearchFilter.ContainsSubstring(property, (string)value)
			{
				ComparisonMode = ComparisonMode.IgnoreNonSpacingCharacters
			};
		}

		// Token: 0x06005329 RID: 21289 RVA: 0x001199B8 File Offset: 0x00117BB8
		public static string GetSearchFilterString(this SearchFilter searchFilter)
		{
			StringBuilder stringBuilder = new StringBuilder();
			ExchangeSearchFilterHelper.GetSearchFilterString(searchFilter, stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x0600532A RID: 21290 RVA: 0x001199D8 File Offset: 0x00117BD8
		private static void GetSearchFilterString(SearchFilter searchFilter, StringBuilder sb)
		{
			if (searchFilter is SearchFilter.SearchFilterCollection)
			{
				ExchangeSearchFilterHelper.GetSearchFilterCollectionString((SearchFilter.SearchFilterCollection)searchFilter, sb);
				return;
			}
			ExchangeSearchFilterHelper.GetPropertyBasedSearchFilterString(searchFilter, sb);
		}

		// Token: 0x0600532B RID: 21291 RVA: 0x001199F8 File Offset: 0x00117BF8
		private static void GetPropertyBasedSearchFilterString(SearchFilter searchFilter, StringBuilder sb)
		{
			string text;
			string text2;
			string text3;
			if (searchFilter is SearchFilter.IsEqualTo)
			{
				SearchFilter.IsEqualTo isEqualTo = (SearchFilter.IsEqualTo)searchFilter;
				text = "=";
				text2 = isEqualTo.PropertyDefinition.GetName();
				text3 = isEqualTo.Value.ToString();
			}
			else if (searchFilter is SearchFilter.IsGreaterThan)
			{
				SearchFilter.IsGreaterThan isGreaterThan = (SearchFilter.IsGreaterThan)searchFilter;
				text = ">";
				text2 = isGreaterThan.PropertyDefinition.GetName();
				text3 = isGreaterThan.Value.ToString();
			}
			else if (searchFilter is SearchFilter.IsGreaterThanOrEqualTo)
			{
				SearchFilter.IsGreaterThanOrEqualTo isGreaterThanOrEqualTo = (SearchFilter.IsGreaterThanOrEqualTo)searchFilter;
				text = ">=";
				text2 = isGreaterThanOrEqualTo.PropertyDefinition.GetName();
				text3 = isGreaterThanOrEqualTo.Value.ToString();
			}
			else if (searchFilter is SearchFilter.IsLessThan)
			{
				SearchFilter.IsLessThan isLessThan = (SearchFilter.IsLessThan)searchFilter;
				text = "<";
				text2 = isLessThan.PropertyDefinition.GetName();
				text3 = isLessThan.Value.ToString();
			}
			else if (searchFilter is SearchFilter.IsLessThanOrEqualTo)
			{
				SearchFilter.IsLessThanOrEqualTo isLessThanOrEqualTo = (SearchFilter.IsLessThanOrEqualTo)searchFilter;
				text = "<=";
				text2 = isLessThanOrEqualTo.PropertyDefinition.GetName();
				text3 = isLessThanOrEqualTo.Value.ToString();
			}
			else if (searchFilter is SearchFilter.IsNotEqualTo)
			{
				SearchFilter.IsNotEqualTo isNotEqualTo = (SearchFilter.IsNotEqualTo)searchFilter;
				text = "!=";
				text2 = isNotEqualTo.PropertyDefinition.GetName();
				text3 = isNotEqualTo.Value.ToString();
			}
			else
			{
				if (!(searchFilter is SearchFilter.ContainsSubstring))
				{
					string text4 = "Can persist search filter type: ";
					Type type = searchFilter.GetType();
					throw new InvalidOperationException(text4 + ((type != null) ? type.ToString() : null));
				}
				SearchFilter.ContainsSubstring containsSubstring = (SearchFilter.ContainsSubstring)searchFilter;
				text = "contains";
				text2 = containsSubstring.PropertyDefinition.GetName();
				text3 = containsSubstring.Value.ToString();
			}
			sb.Append(text);
			sb.Append("{" + text2 + ",");
			sb.Append(text3 + "}");
		}

		// Token: 0x0600532C RID: 21292 RVA: 0x00119BA0 File Offset: 0x00117DA0
		private static void GetSearchFilterCollectionString(SearchFilter.SearchFilterCollection searchCollection, StringBuilder sb)
		{
			sb.Append(searchCollection.LogicalOperator.ToString());
			sb.Append("(");
			foreach (SearchFilter searchFilter in searchCollection)
			{
				sb.Append(searchFilter.GetSearchFilterString());
				sb.Append(".");
			}
			sb.Append(")");
		}
	}
}
