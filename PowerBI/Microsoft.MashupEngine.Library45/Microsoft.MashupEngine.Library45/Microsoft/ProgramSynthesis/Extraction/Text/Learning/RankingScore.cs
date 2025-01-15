using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Features;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Extraction.Text.Learning
{
	// Token: 0x02000F56 RID: 3926
	public class RankingScore : Feature<double>
	{
		// Token: 0x06006D37 RID: 27959 RVA: 0x000BFE5F File Offset: 0x000BE05F
		public RankingScore(Grammar grammar)
			: base(grammar, "Score", false, false, null, Feature<double>.FeatureInfoResolution.Declared)
		{
		}

		// Token: 0x06006D38 RID: 27960 RVA: 0x0001AF59 File Offset: 0x00019159
		protected override double GetFeatureValueForVariable(VariableNode variable)
		{
			return 0.0;
		}

		// Token: 0x06006D39 RID: 27961 RVA: 0x0000E945 File Offset: 0x0000CB45
		[FeatureCalculator("Table")]
		public static double CsvTableScore(double columnNames, double rows)
		{
			return rows;
		}

		// Token: 0x06006D3A RID: 27962 RVA: 0x000BFE71 File Offset: 0x000BE071
		[FeatureCalculator("RowMap")]
		public static double RowMapScore(double splitCol, double records)
		{
			return splitCol + records;
		}

		// Token: 0x06006D3B RID: 27963 RVA: 0x00004FAE File Offset: 0x000031AE
		[FeatureCalculator("List")]
		public static double ListScore(double extract)
		{
			return extract;
		}

		// Token: 0x06006D3C RID: 27964 RVA: 0x000BFE71 File Offset: 0x000BE071
		[FeatureCalculator("Prepend")]
		public static double PrependScore(double extractTup, double colSplit)
		{
			return extractTup + colSplit;
		}

		// Token: 0x06006D3D RID: 27965 RVA: 0x00004FAE File Offset: 0x000031AE
		[FeatureCalculator("First")]
		public static double FirstScore(double tup)
		{
			return tup;
		}

		// Token: 0x06006D3E RID: 27966 RVA: 0x00004FAE File Offset: 0x000031AE
		[FeatureCalculator("Second")]
		public static double SecondScore(double tup)
		{
			return tup;
		}

		// Token: 0x06006D3F RID: 27967 RVA: 0x000BFE8B File Offset: 0x000BE08B
		[FeatureCalculator("Slice")]
		public static double SliceScore(double row, double k1, double k2)
		{
			return row + k1 + k2;
		}

		// Token: 0x06006D40 RID: 27968 RVA: 0x000BFE8B File Offset: 0x000BE08B
		[FeatureCalculator("BetweenDelimiters")]
		public static double BetweenDelimitersScore(double row, double prefix, double suffix)
		{
			return row + prefix + suffix;
		}

		// Token: 0x06006D41 RID: 27969 RVA: 0x000BFE8B File Offset: 0x000BE08B
		[FeatureCalculator("Substring")]
		public static double SubstringScore(double row, double index, double length)
		{
			return row + index + length;
		}

		// Token: 0x06006D42 RID: 27970 RVA: 0x0000E945 File Offset: 0x0000CB45
		[FeatureCalculator("SplitPosition")]
		public static double SplitPositionScore(double row, double k)
		{
			return k;
		}

		// Token: 0x06006D43 RID: 27971 RVA: 0x0014755B File Offset: 0x0014575B
		[FeatureCalculator("SplitDelimiter")]
		public static double SplitDelimiterScore(double row, double str, double k)
		{
			return str + k;
		}

		// Token: 0x06006D44 RID: 27972 RVA: 0x000BFE71 File Offset: 0x000BE071
		[FeatureCalculator("Group")]
		public static double GroupScore(double re, double skip)
		{
			return re + skip;
		}

		// Token: 0x06006D45 RID: 27973 RVA: 0x000BFE71 File Offset: 0x000BE071
		[FeatureCalculator("MergeEvery")]
		public static double MergeEveryScore(double k, double skip)
		{
			return k + skip;
		}

		// Token: 0x06006D46 RID: 27974 RVA: 0x000BFE71 File Offset: 0x000BE071
		[FeatureCalculator("Select")]
		public static double SelectScore(double re, double skip)
		{
			return re + skip;
		}

		// Token: 0x06006D47 RID: 27975 RVA: 0x000BFE71 File Offset: 0x000BE071
		[FeatureCalculator("Skip")]
		public static double SkipScore(double k, double lines)
		{
			return k + lines;
		}

		// Token: 0x06006D48 RID: 27976 RVA: 0x00004FAE File Offset: 0x000031AE
		[FeatureCalculator("Trim")]
		public static double TrimScore(double s)
		{
			return s;
		}

		// Token: 0x06006D49 RID: 27977 RVA: 0x0001AF59 File Offset: 0x00019159
		[FeatureCalculator("SplitLines")]
		public static double SplitLastScore(double v)
		{
			return 0.0;
		}

		// Token: 0x06006D4A RID: 27978 RVA: 0x00164721 File Offset: 0x00162921
		[FeatureCalculator("k", Method = CalculationMethod.FromLiteral)]
		public static double IdxScore(int idx)
		{
			return (double)idx;
		}

		// Token: 0x06006D4B RID: 27979 RVA: 0x00164725 File Offset: 0x00162925
		[FeatureCalculator("str", Method = CalculationMethod.FromLiteral)]
		public static double StrScore(string str)
		{
			return (double)str.Length;
		}

		// Token: 0x06006D4C RID: 27980 RVA: 0x0016472E File Offset: 0x0016292E
		[FeatureCalculator("del", Method = CalculationMethod.FromLiteral)]
		public static double DelScore(Optional<string> str)
		{
			string value = str.Value;
			return (double)((value != null) ? value.Length : 0);
		}

		// Token: 0x06006D4D RID: 27981 RVA: 0x00164744 File Offset: 0x00162944
		[FeatureCalculator("re", Method = CalculationMethod.FromLiteral)]
		public static double ReScore(Regex re)
		{
			return (double)re.ToString().Length;
		}

		// Token: 0x06006D4E RID: 27982 RVA: 0x0001AF59 File Offset: 0x00019159
		[FeatureCalculator("columnNames", Method = CalculationMethod.FromLiteral)]
		public static double ColumnNamesScore(IReadOnlyList<string> columnNames)
		{
			return 0.0;
		}
	}
}
