using System;

namespace Microsoft.OData.Core.UriParser
{
	// Token: 0x020001AC RID: 428
	internal static class ExpressionConstants
	{
		// Token: 0x04000701 RID: 1793
		internal const string It = "$it";

		// Token: 0x04000702 RID: 1794
		internal const string KeywordAdd = "add";

		// Token: 0x04000703 RID: 1795
		internal const string KeywordAnd = "and";

		// Token: 0x04000704 RID: 1796
		internal const string KeywordAscending = "asc";

		// Token: 0x04000705 RID: 1797
		internal const string KeywordDescending = "desc";

		// Token: 0x04000706 RID: 1798
		internal const string KeywordDivide = "div";

		// Token: 0x04000707 RID: 1799
		internal const string KeywordModulo = "mod";

		// Token: 0x04000708 RID: 1800
		internal const string KeywordMultiply = "mul";

		// Token: 0x04000709 RID: 1801
		internal const string KeywordNot = "not";

		// Token: 0x0400070A RID: 1802
		internal const string KeywordOr = "or";

		// Token: 0x0400070B RID: 1803
		internal const string KeywordSub = "sub";

		// Token: 0x0400070C RID: 1804
		internal const string SymbolNegate = "-";

		// Token: 0x0400070D RID: 1805
		internal const string SymbolEqual = "=";

		// Token: 0x0400070E RID: 1806
		internal const string SymbolComma = ",";

		// Token: 0x0400070F RID: 1807
		internal const string SymbolForwardSlash = "/";

		// Token: 0x04000710 RID: 1808
		internal const string SymbolOpenParen = "(";

		// Token: 0x04000711 RID: 1809
		internal const string SymbolClosedParen = ")";

		// Token: 0x04000712 RID: 1810
		internal const string SymbolQueryStart = "?";

		// Token: 0x04000713 RID: 1811
		internal const string SymbolQueryConcatenate = "&";

		// Token: 0x04000714 RID: 1812
		internal const string SymbolSingleQuote = "'";

		// Token: 0x04000715 RID: 1813
		internal const string SymbolSingleQuoteEscaped = "''";

		// Token: 0x04000716 RID: 1814
		internal const string SymbolEscapedSpace = "%20";

		// Token: 0x04000717 RID: 1815
		internal const string KeywordEqual = "eq";

		// Token: 0x04000718 RID: 1816
		internal const string KeywordFalse = "false";

		// Token: 0x04000719 RID: 1817
		internal const string KeywordGreaterThan = "gt";

		// Token: 0x0400071A RID: 1818
		internal const string KeywordGreaterThanOrEqual = "ge";

		// Token: 0x0400071B RID: 1819
		internal const string KeywordLessThan = "lt";

		// Token: 0x0400071C RID: 1820
		internal const string KeywordLessThanOrEqual = "le";

		// Token: 0x0400071D RID: 1821
		internal const string KeywordNotEqual = "ne";

		// Token: 0x0400071E RID: 1822
		internal const string KeywordHas = "has";

		// Token: 0x0400071F RID: 1823
		internal const string KeywordNull = "null";

		// Token: 0x04000720 RID: 1824
		internal const string KeywordTrue = "true";

		// Token: 0x04000721 RID: 1825
		internal const string KeywordMax = "max";

		// Token: 0x04000722 RID: 1826
		internal const string UnboundFunctionCast = "cast";

		// Token: 0x04000723 RID: 1827
		internal const string UnboundFunctionIsOf = "isof";

		// Token: 0x04000724 RID: 1828
		internal const string UnboundFunctionLength = "geo.length";

		// Token: 0x04000725 RID: 1829
		internal const string UnboundFunctionIntersects = "geo.intersects";

		// Token: 0x04000726 RID: 1830
		internal const string InfinityLiteral = "INF";

		// Token: 0x04000727 RID: 1831
		internal const string NaNLiteral = "NaN";

		// Token: 0x04000728 RID: 1832
		internal const string LiteralPrefixDuration = "duration";

		// Token: 0x04000729 RID: 1833
		internal const string LiteralPrefixGeometry = "geometry";

		// Token: 0x0400072A RID: 1834
		internal const string LiteralPrefixGeography = "geography";

		// Token: 0x0400072B RID: 1835
		internal const string LiteralPrefixBinary = "binary";

		// Token: 0x0400072C RID: 1836
		internal const string LiteralSuffixInt64 = "L";

		// Token: 0x0400072D RID: 1837
		internal const string LiteralSuffixSingle = "f";

		// Token: 0x0400072E RID: 1838
		internal const string LiteralSuffixDouble = "D";

		// Token: 0x0400072F RID: 1839
		internal const string LiteralSuffixDecimal = "M";

		// Token: 0x04000730 RID: 1840
		internal const string LiteralSingleQuote = "'";

		// Token: 0x04000731 RID: 1841
		internal const string QueryOptionFilter = "$filter";

		// Token: 0x04000732 RID: 1842
		internal const string QueryOptionOrderby = "$orderby";

		// Token: 0x04000733 RID: 1843
		internal const string QueryOptionTop = "$top";

		// Token: 0x04000734 RID: 1844
		internal const string QueryOptionSkip = "$skip";

		// Token: 0x04000735 RID: 1845
		internal const string QueryOptionCount = "$count";

		// Token: 0x04000736 RID: 1846
		internal const string QueryOptionLevels = "$levels";

		// Token: 0x04000737 RID: 1847
		internal const string QueryOptionSearch = "$search";

		// Token: 0x04000738 RID: 1848
		internal const string QueryOptionSelect = "$select";

		// Token: 0x04000739 RID: 1849
		internal const string QueryOptionExpand = "$expand";

		// Token: 0x0400073A RID: 1850
		internal const string SearchKeywordAnd = "AND";

		// Token: 0x0400073B RID: 1851
		internal const string SearchKeywordNot = "NOT";

		// Token: 0x0400073C RID: 1852
		internal const string SearchKeywordOr = "OR";

		// Token: 0x0400073D RID: 1853
		internal const string KeywordAny = "any";

		// Token: 0x0400073E RID: 1854
		internal const string KeywordAll = "all";

		// Token: 0x0400073F RID: 1855
		internal const string QueryOptionApply = "$apply";

		// Token: 0x04000740 RID: 1856
		internal const string KeywordAggregate = "aggregate";

		// Token: 0x04000741 RID: 1857
		internal const string KeywordFilter = "filter";

		// Token: 0x04000742 RID: 1858
		internal const string KeywordGroupBy = "groupby";

		// Token: 0x04000743 RID: 1859
		internal const string KeywordSum = "sum";

		// Token: 0x04000744 RID: 1860
		internal const string KeywordMin = "min";

		// Token: 0x04000745 RID: 1861
		internal const string KeywordAverage = "average";

		// Token: 0x04000746 RID: 1862
		internal const string KeywordCountDistinct = "countdistinct";

		// Token: 0x04000747 RID: 1863
		internal const string KeywordAs = "as";

		// Token: 0x04000748 RID: 1864
		internal const string KeywordWith = "with";
	}
}
