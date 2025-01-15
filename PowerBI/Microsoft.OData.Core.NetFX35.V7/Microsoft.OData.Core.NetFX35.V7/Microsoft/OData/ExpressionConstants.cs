using System;

namespace Microsoft.OData
{
	// Token: 0x020000D0 RID: 208
	internal static class ExpressionConstants
	{
		// Token: 0x04000333 RID: 819
		internal const string It = "$it";

		// Token: 0x04000334 RID: 820
		internal const string KeywordAdd = "add";

		// Token: 0x04000335 RID: 821
		internal const string KeywordAnd = "and";

		// Token: 0x04000336 RID: 822
		internal const string KeywordAscending = "asc";

		// Token: 0x04000337 RID: 823
		internal const string KeywordDescending = "desc";

		// Token: 0x04000338 RID: 824
		internal const string KeywordDivide = "div";

		// Token: 0x04000339 RID: 825
		internal const string KeywordModulo = "mod";

		// Token: 0x0400033A RID: 826
		internal const string KeywordMultiply = "mul";

		// Token: 0x0400033B RID: 827
		internal const string KeywordNot = "not";

		// Token: 0x0400033C RID: 828
		internal const string KeywordOr = "or";

		// Token: 0x0400033D RID: 829
		internal const string KeywordSub = "sub";

		// Token: 0x0400033E RID: 830
		internal const string SymbolNegate = "-";

		// Token: 0x0400033F RID: 831
		internal const string SymbolEqual = "=";

		// Token: 0x04000340 RID: 832
		internal const string SymbolComma = ",";

		// Token: 0x04000341 RID: 833
		internal const string SymbolDot = ".";

		// Token: 0x04000342 RID: 834
		internal const string SymbolForwardSlash = "/";

		// Token: 0x04000343 RID: 835
		internal const string SymbolOpenParen = "(";

		// Token: 0x04000344 RID: 836
		internal const string SymbolClosedParen = ")";

		// Token: 0x04000345 RID: 837
		internal const string SymbolQueryStart = "?";

		// Token: 0x04000346 RID: 838
		internal const string SymbolQueryConcatenate = "&";

		// Token: 0x04000347 RID: 839
		internal const string SymbolSingleQuote = "'";

		// Token: 0x04000348 RID: 840
		internal const string SymbolSingleQuoteEscaped = "''";

		// Token: 0x04000349 RID: 841
		internal const string SymbolEscapedSpace = "%20";

		// Token: 0x0400034A RID: 842
		internal const string KeywordEqual = "eq";

		// Token: 0x0400034B RID: 843
		internal const string KeywordFalse = "false";

		// Token: 0x0400034C RID: 844
		internal const string KeywordGreaterThan = "gt";

		// Token: 0x0400034D RID: 845
		internal const string KeywordGreaterThanOrEqual = "ge";

		// Token: 0x0400034E RID: 846
		internal const string KeywordLessThan = "lt";

		// Token: 0x0400034F RID: 847
		internal const string KeywordLessThanOrEqual = "le";

		// Token: 0x04000350 RID: 848
		internal const string KeywordNotEqual = "ne";

		// Token: 0x04000351 RID: 849
		internal const string KeywordHas = "has";

		// Token: 0x04000352 RID: 850
		internal const string KeywordNull = "null";

		// Token: 0x04000353 RID: 851
		internal const string KeywordTrue = "true";

		// Token: 0x04000354 RID: 852
		internal const string KeywordMax = "max";

		// Token: 0x04000355 RID: 853
		internal const string UnboundFunctionCast = "cast";

		// Token: 0x04000356 RID: 854
		internal const string UnboundFunctionIsOf = "isof";

		// Token: 0x04000357 RID: 855
		internal const string UnboundFunctionLength = "geo.length";

		// Token: 0x04000358 RID: 856
		internal const string UnboundFunctionIntersects = "geo.intersects";

		// Token: 0x04000359 RID: 857
		internal const string InfinityLiteral = "INF";

		// Token: 0x0400035A RID: 858
		internal const string NaNLiteral = "NaN";

		// Token: 0x0400035B RID: 859
		internal const string LiteralPrefixDuration = "duration";

		// Token: 0x0400035C RID: 860
		internal const string LiteralPrefixGeometry = "geometry";

		// Token: 0x0400035D RID: 861
		internal const string LiteralPrefixGeography = "geography";

		// Token: 0x0400035E RID: 862
		internal const string LiteralPrefixBinary = "binary";

		// Token: 0x0400035F RID: 863
		internal const string LiteralSuffixInt64 = "L";

		// Token: 0x04000360 RID: 864
		internal const string LiteralSuffixSingle = "f";

		// Token: 0x04000361 RID: 865
		internal const string LiteralSuffixDouble = "D";

		// Token: 0x04000362 RID: 866
		internal const string LiteralSuffixDecimal = "M";

		// Token: 0x04000363 RID: 867
		internal const string LiteralSingleQuote = "'";

		// Token: 0x04000364 RID: 868
		internal const string QueryOptionFilter = "$filter";

		// Token: 0x04000365 RID: 869
		internal const string QueryOptionOrderby = "$orderby";

		// Token: 0x04000366 RID: 870
		internal const string QueryOptionTop = "$top";

		// Token: 0x04000367 RID: 871
		internal const string QueryOptionSkip = "$skip";

		// Token: 0x04000368 RID: 872
		internal const string QueryOptionCount = "$count";

		// Token: 0x04000369 RID: 873
		internal const string QueryOptionLevels = "$levels";

		// Token: 0x0400036A RID: 874
		internal const string QueryOptionSearch = "$search";

		// Token: 0x0400036B RID: 875
		internal const string QueryOptionSelect = "$select";

		// Token: 0x0400036C RID: 876
		internal const string QueryOptionExpand = "$expand";

		// Token: 0x0400036D RID: 877
		internal const string SearchKeywordAnd = "AND";

		// Token: 0x0400036E RID: 878
		internal const string SearchKeywordNot = "NOT";

		// Token: 0x0400036F RID: 879
		internal const string SearchKeywordOr = "OR";

		// Token: 0x04000370 RID: 880
		internal const string KeywordAny = "any";

		// Token: 0x04000371 RID: 881
		internal const string KeywordAll = "all";

		// Token: 0x04000372 RID: 882
		internal const string QueryOptionApply = "$apply";

		// Token: 0x04000373 RID: 883
		internal const string KeywordAggregate = "aggregate";

		// Token: 0x04000374 RID: 884
		internal const string KeywordFilter = "filter";

		// Token: 0x04000375 RID: 885
		internal const string KeywordGroupBy = "groupby";

		// Token: 0x04000376 RID: 886
		internal const string KeywordSum = "sum";

		// Token: 0x04000377 RID: 887
		internal const string KeywordMin = "min";

		// Token: 0x04000378 RID: 888
		internal const string KeywordAverage = "average";

		// Token: 0x04000379 RID: 889
		internal const string KeywordCountDistinct = "countdistinct";

		// Token: 0x0400037A RID: 890
		internal const string KeywordAs = "as";

		// Token: 0x0400037B RID: 891
		internal const string KeywordWith = "with";

		// Token: 0x0400037C RID: 892
		internal const string QueryOptionCompute = "$compute";
	}
}
