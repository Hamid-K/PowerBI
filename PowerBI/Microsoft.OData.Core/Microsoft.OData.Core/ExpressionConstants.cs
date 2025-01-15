using System;

namespace Microsoft.OData
{
	// Token: 0x020000D3 RID: 211
	internal static class ExpressionConstants
	{
		// Token: 0x0400035E RID: 862
		internal const string It = "$it";

		// Token: 0x0400035F RID: 863
		internal const string KeywordAdd = "add";

		// Token: 0x04000360 RID: 864
		internal const string KeywordAnd = "and";

		// Token: 0x04000361 RID: 865
		internal const string KeywordAscending = "asc";

		// Token: 0x04000362 RID: 866
		internal const string KeywordDescending = "desc";

		// Token: 0x04000363 RID: 867
		internal const string KeywordDivide = "div";

		// Token: 0x04000364 RID: 868
		internal const string KeywordModulo = "mod";

		// Token: 0x04000365 RID: 869
		internal const string KeywordMultiply = "mul";

		// Token: 0x04000366 RID: 870
		internal const string KeywordNot = "not";

		// Token: 0x04000367 RID: 871
		internal const string KeywordOr = "or";

		// Token: 0x04000368 RID: 872
		internal const string KeywordSub = "sub";

		// Token: 0x04000369 RID: 873
		internal const string SymbolNegate = "-";

		// Token: 0x0400036A RID: 874
		internal const string SymbolEqual = "=";

		// Token: 0x0400036B RID: 875
		internal const string SymbolComma = ",";

		// Token: 0x0400036C RID: 876
		internal const string SymbolDot = ".";

		// Token: 0x0400036D RID: 877
		internal const string SymbolForwardSlash = "/";

		// Token: 0x0400036E RID: 878
		internal const string SymbolOpenParen = "(";

		// Token: 0x0400036F RID: 879
		internal const string SymbolClosedParen = ")";

		// Token: 0x04000370 RID: 880
		internal const string SymbolQueryStart = "?";

		// Token: 0x04000371 RID: 881
		internal const string SymbolQueryConcatenate = "&";

		// Token: 0x04000372 RID: 882
		internal const string SymbolSingleQuote = "'";

		// Token: 0x04000373 RID: 883
		internal const string SymbolSingleQuoteEscaped = "''";

		// Token: 0x04000374 RID: 884
		internal const string SymbolEscapedSpace = "%20";

		// Token: 0x04000375 RID: 885
		internal const string KeywordEqual = "eq";

		// Token: 0x04000376 RID: 886
		internal const string KeywordFalse = "false";

		// Token: 0x04000377 RID: 887
		internal const string KeywordGreaterThan = "gt";

		// Token: 0x04000378 RID: 888
		internal const string KeywordGreaterThanOrEqual = "ge";

		// Token: 0x04000379 RID: 889
		internal const string KeywordLessThan = "lt";

		// Token: 0x0400037A RID: 890
		internal const string KeywordLessThanOrEqual = "le";

		// Token: 0x0400037B RID: 891
		internal const string KeywordNotEqual = "ne";

		// Token: 0x0400037C RID: 892
		internal const string KeywordHas = "has";

		// Token: 0x0400037D RID: 893
		internal const string KeywordNull = "null";

		// Token: 0x0400037E RID: 894
		internal const string KeywordTrue = "true";

		// Token: 0x0400037F RID: 895
		internal const string KeywordMax = "max";

		// Token: 0x04000380 RID: 896
		internal const string KeywordIn = "in";

		// Token: 0x04000381 RID: 897
		internal const string UnboundFunctionCast = "cast";

		// Token: 0x04000382 RID: 898
		internal const string UnboundFunctionIsOf = "isof";

		// Token: 0x04000383 RID: 899
		internal const string UnboundFunctionLength = "geo.length";

		// Token: 0x04000384 RID: 900
		internal const string UnboundFunctionIntersects = "geo.intersects";

		// Token: 0x04000385 RID: 901
		internal const string InfinityLiteral = "INF";

		// Token: 0x04000386 RID: 902
		internal const string NaNLiteral = "NaN";

		// Token: 0x04000387 RID: 903
		internal const string LiteralPrefixDuration = "duration";

		// Token: 0x04000388 RID: 904
		internal const string LiteralPrefixGeometry = "geometry";

		// Token: 0x04000389 RID: 905
		internal const string LiteralPrefixGeography = "geography";

		// Token: 0x0400038A RID: 906
		internal const string LiteralPrefixBinary = "binary";

		// Token: 0x0400038B RID: 907
		internal const string LiteralSuffixInt64 = "L";

		// Token: 0x0400038C RID: 908
		internal const string LiteralSuffixSingle = "f";

		// Token: 0x0400038D RID: 909
		internal const string LiteralSuffixDouble = "D";

		// Token: 0x0400038E RID: 910
		internal const string LiteralSuffixDecimal = "M";

		// Token: 0x0400038F RID: 911
		internal const string LiteralSingleQuote = "'";

		// Token: 0x04000390 RID: 912
		internal const string QueryOptionFilter = "$filter";

		// Token: 0x04000391 RID: 913
		internal const string QueryOptionOrderby = "$orderby";

		// Token: 0x04000392 RID: 914
		internal const string QueryOptionTop = "$top";

		// Token: 0x04000393 RID: 915
		internal const string QueryOptionSkip = "$skip";

		// Token: 0x04000394 RID: 916
		internal const string QueryOptionCount = "$count";

		// Token: 0x04000395 RID: 917
		internal const string QueryOptionLevels = "$levels";

		// Token: 0x04000396 RID: 918
		internal const string QueryOptionSearch = "$search";

		// Token: 0x04000397 RID: 919
		internal const string QueryOptionSelect = "$select";

		// Token: 0x04000398 RID: 920
		internal const string QueryOptionExpand = "$expand";

		// Token: 0x04000399 RID: 921
		internal const string SearchKeywordAnd = "AND";

		// Token: 0x0400039A RID: 922
		internal const string SearchKeywordNot = "NOT";

		// Token: 0x0400039B RID: 923
		internal const string SearchKeywordOr = "OR";

		// Token: 0x0400039C RID: 924
		internal const string KeywordAny = "any";

		// Token: 0x0400039D RID: 925
		internal const string KeywordAll = "all";

		// Token: 0x0400039E RID: 926
		internal const string QueryOptionApply = "$apply";

		// Token: 0x0400039F RID: 927
		internal const string KeywordAggregate = "aggregate";

		// Token: 0x040003A0 RID: 928
		internal const string KeywordFilter = "filter";

		// Token: 0x040003A1 RID: 929
		internal const string KeywordGroupBy = "groupby";

		// Token: 0x040003A2 RID: 930
		internal const string KeywordCompute = "compute";

		// Token: 0x040003A3 RID: 931
		internal const string KeywordSum = "sum";

		// Token: 0x040003A4 RID: 932
		internal const string KeywordMin = "min";

		// Token: 0x040003A5 RID: 933
		internal const string KeywordAverage = "average";

		// Token: 0x040003A6 RID: 934
		internal const string KeywordCountDistinct = "countdistinct";

		// Token: 0x040003A7 RID: 935
		internal const string KeywordAs = "as";

		// Token: 0x040003A8 RID: 936
		internal const string KeywordWith = "with";

		// Token: 0x040003A9 RID: 937
		internal const string QueryOptionCompute = "$compute";

		// Token: 0x040003AA RID: 938
		internal const string KeywordExpand = "expand";
	}
}
