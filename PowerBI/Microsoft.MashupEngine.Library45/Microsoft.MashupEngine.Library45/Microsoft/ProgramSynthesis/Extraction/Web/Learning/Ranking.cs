using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Features;
using Microsoft.ProgramSynthesis.Transformation.Text;
using Microsoft.ProgramSynthesis.Transformation.Text.Semantics;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Learning
{
	// Token: 0x020010CC RID: 4300
	public class Ranking : Feature<double>
	{
		// Token: 0x06008118 RID: 33048 RVA: 0x001B019C File Offset: 0x001AE39C
		public Ranking(Grammar grammar)
			: base(grammar, "Score", false, false, null, Feature<double>.FeatureInfoResolution.Declared)
		{
		}

		// Token: 0x06008119 RID: 33049 RVA: 0x0001EA46 File Offset: 0x0001CC46
		protected override double GetFeatureValueForVariable(VariableNode variable)
		{
			return 10.0;
		}

		// Token: 0x0600811A RID: 33050 RVA: 0x00164721 File Offset: 0x00162921
		[FeatureCalculator("idx1", Method = CalculationMethod.FromLiteral)]
		[FeatureCalculator("idx2", Method = CalculationMethod.FromLiteral)]
		[FeatureCalculator("count", Method = CalculationMethod.FromLiteral)]
		[FeatureCalculator("direction", Method = CalculationMethod.FromLiteral)]
		public static double IdxRank(int val)
		{
			return (double)val;
		}

		// Token: 0x0600811B RID: 33051 RVA: 0x00164725 File Offset: 0x00162925
		[FeatureCalculator("name", Method = CalculationMethod.FromLiteral)]
		[FeatureCalculator("value", Method = CalculationMethod.FromLiteral)]
		[FeatureCalculator("cssSelector", Method = CalculationMethod.FromLiteral)]
		[FeatureCalculator("className", Method = CalculationMethod.FromLiteral)]
		[FeatureCalculator("propName", Method = CalculationMethod.FromLiteral)]
		[FeatureCalculator("nodeName", Method = CalculationMethod.FromLiteral)]
		[FeatureCalculator("idName", Method = CalculationMethod.FromLiteral)]
		public static double SexprRank(string sexpr)
		{
			return (double)sexpr.Length;
		}

		// Token: 0x0600811C RID: 33052 RVA: 0x001B01D8 File Offset: 0x001AE3D8
		[FeatureCalculator("names", Method = CalculationMethod.FromLiteral)]
		public static double SarrRank(string[] names)
		{
			return (double)names.Length;
		}

		// Token: 0x0600811D RID: 33053 RVA: 0x001B01D8 File Offset: 0x001AE3D8
		[FeatureCalculator("entityObjs", Method = CalculationMethod.FromLiteral)]
		public static double OarrRank(object[] names)
		{
			return (double)names.Length;
		}

		// Token: 0x0600811E RID: 33054 RVA: 0x00012DE5 File Offset: 0x00010FE5
		[FeatureCalculator("obj", Method = CalculationMethod.FromLiteral)]
		public static double ObjRank(object obj)
		{
			return 1.0;
		}

		// Token: 0x0600811F RID: 33055 RVA: 0x00012DE5 File Offset: 0x00010FE5
		[FeatureCalculator("EmptySequence")]
		public static double EmptySeqRank()
		{
			return 1.0;
		}

		// Token: 0x06008120 RID: 33056 RVA: 0x00012DE5 File Offset: 0x00010FE5
		[FeatureCalculator("substringFeatureNames", Method = CalculationMethod.FromLiteral)]
		public static double SubsFeatureNameRank(string[] substringFeatureNames)
		{
			return 1.0;
		}

		// Token: 0x06008121 RID: 33057 RVA: 0x00012DE5 File Offset: 0x00010FE5
		[FeatureCalculator("substringFeatureValues", Method = CalculationMethod.FromLiteral)]
		public static double SubsFeatureValueRank(int[] substringFeatureValues)
		{
			return 1.0;
		}

		// Token: 0x06008122 RID: 33058 RVA: 0x001B01DE File Offset: 0x001AE3DE
		[FeatureCalculator("NodeName")]
		[FeatureCalculator("NthChild")]
		[FeatureCalculator("NthLastChild")]
		[FeatureCalculator("Class")]
		[FeatureCalculator("ChildrenCount")]
		[FeatureCalculator("ID_substring")]
		[FeatureCalculator("NodeNames")]
		[FeatureCalculator("ContainsLeafNodes")]
		[FeatureCalculator("ColumnSequence")]
		[FeatureCalculator("And")]
		[FeatureCalculator("LeafAnd")]
		[FeatureCalculator("DisjSelection1")]
		[FeatureCalculator("DisjSelection2")]
		[FeatureCalculator("DisjSelection3")]
		[FeatureCalculator("DisjSelection4")]
		[FeatureCalculator("DisjSelection5")]
		[FeatureCalculator("DisjSubstring")]
		[FeatureCalculator("CSSSelection")]
		public static double Rank_Contains(double k1, double k2)
		{
			return 9.0 + k1 + k2;
		}

		// Token: 0x06008123 RID: 33059 RVA: 0x001B01DE File Offset: 0x001AE3DE
		[FeatureCalculator("HasEntityAnchor")]
		public static double Rank_HasEntityAnchor(double k1, double k2, double k3)
		{
			return 9.0 + k1 + k2;
		}

		// Token: 0x06008124 RID: 33060 RVA: 0x001B01ED File Offset: 0x001AE3ED
		[FeatureCalculator("HasAttribute")]
		[FeatureCalculator("HasStyle")]
		public static double Rank_HasAttribute(double k1, double k2, double k3)
		{
			return k2 + k1 + 2.0;
		}

		// Token: 0x06008125 RID: 33061 RVA: 0x001B01ED File Offset: 0x001AE3ED
		[FeatureCalculator("NodeRegionToWebRegionInSequence")]
		public static double Rank_NodeRegionToWebRegionInSequence(double k1, double k2)
		{
			return k2 + k1 + 2.0;
		}

		// Token: 0x06008126 RID: 33062 RVA: 0x00012DE5 File Offset: 0x00010FE5
		[FeatureCalculator("ExtractRowBasedTable")]
		public static double Rank_TablePrograms(double k1, double k2)
		{
			return 1.0;
		}

		// Token: 0x06008127 RID: 33063 RVA: 0x00012DE5 File Offset: 0x00010FE5
		[FeatureCalculator("ExtractTable")]
		public static double Rank_TablePrograms(double k1)
		{
			return 1.0;
		}

		// Token: 0x06008128 RID: 33064 RVA: 0x001B01ED File Offset: 0x001AE3ED
		[FeatureCalculator("KthNodeInSelection")]
		[FeatureCalculator("LeafFilter1")]
		[FeatureCalculator("LeafFilter2")]
		[FeatureCalculator("LeafFilter3")]
		[FeatureCalculator("LeafFilter4")]
		[FeatureCalculator("LeafFilter5")]
		[FeatureCalculator("FindEndNode")]
		[FeatureCalculator("FilterNodesEnd")]
		[FeatureCalculator("TakeWhileNodesEnd")]
		[FeatureCalculator("TitleIs")]
		[FeatureCalculator("AppendField")]
		[FeatureCalculator("ClassFilter")]
		[FeatureCalculator("ItemPropFilter")]
		[FeatureCalculator("IDFilter")]
		[FeatureCalculator("NodeNameFilter")]
		[FeatureCalculator("NthChildFilter")]
		[FeatureCalculator("NthLastChildFilter")]
		[FeatureCalculator("GEN_NthChildFilter")]
		[FeatureCalculator("GEN_NthLastChildFilter")]
		[FeatureCalculator("GEN_ClassFilter")]
		[FeatureCalculator("GEN_ItemPropFilter")]
		[FeatureCalculator("GEN_IDFilter")]
		[FeatureCalculator("GEN_NodeNameFilter")]
		[FeatureCalculator("Union")]
		public static double Rank_Filter(double k1, double k2)
		{
			return k2 + k1 + 2.0;
		}

		// Token: 0x06008129 RID: 33065 RVA: 0x001B01FC File Offset: 0x001AE3FC
		[FeatureCalculator("MapToWebRegion")]
		public static double Rank_SubNodeSequence(double k1, double k2)
		{
			return k2 + k1 + 3.0;
		}

		// Token: 0x0600812A RID: 33066 RVA: 0x001B020B File Offset: 0x001AE40B
		[FeatureCalculator("SelectSubstring")]
		public static double Rank_SelectSubstring(double k1, double k2, double k3)
		{
			return k3 + k2 + k1;
		}

		// Token: 0x0600812B RID: 33067 RVA: 0x001B0212 File Offset: 0x001AE412
		[FeatureCalculator("TrimmedTextField")]
		[FeatureCalculator("SingleColumn")]
		[FeatureCalculator("GetValueSubstring")]
		[FeatureCalculator("NodeToWebRegion")]
		[FeatureCalculator("NodeToWebRegionInSequence")]
		[FeatureCalculator("SubstringField")]
		[FeatureCalculator("SingleSubstring")]
		[FeatureCalculator("ContainsDate")]
		[FeatureCalculator("ContainsNum")]
		[FeatureCalculator("SingleSelection1")]
		[FeatureCalculator("SingleSelection2")]
		[FeatureCalculator("SingleSelection3")]
		[FeatureCalculator("SingleSelection4")]
		[FeatureCalculator("SingleSelection5")]
		public static double Rank_SubNode(double k1)
		{
			return k1 - 10.0;
		}

		// Token: 0x170016A8 RID: 5800
		// (get) Token: 0x0600812C RID: 33068 RVA: 0x001B021F File Offset: 0x001AE41F
		[ExternFeatureMapping("Substring", 0)]
		public IFeature SubstringScore { get; } = new RankingScore(Language.Grammar, new RankingScoreModel(), false, null);

		// Token: 0x0600812D RID: 33069 RVA: 0x001B0227 File Offset: 0x001AE427
		[FeatureCalculator("NodeRegionToWebRegion")]
		public static double Rank_Region(double k1, double k2)
		{
			return k1 - 11.0;
		}

		// Token: 0x0600812E RID: 33070 RVA: 0x001B0234 File Offset: 0x001AE434
		[FeatureCalculator("ConvertToWebRegions")]
		[FeatureCalculator("AsCollection")]
		[FeatureCalculator("DescendantsOf")]
		[FeatureCalculator("RightSiblingOf")]
		[FeatureCalculator("LeafChildrenOf1")]
		[FeatureCalculator("LeafChildrenOf2")]
		[FeatureCalculator("LeafChildrenOf3")]
		[FeatureCalculator("LeafChildrenOf4")]
		[FeatureCalculator("YoungerSiblingsOf")]
		public static double Rank_Children(double k)
		{
			return k - 7.0;
		}

		// Token: 0x0600812F RID: 33071 RVA: 0x001B0234 File Offset: 0x001AE434
		[FeatureCalculator("KthNode")]
		public static double Rank_KthNode(double k1, double k2)
		{
			return k1 - 7.0;
		}

		// Token: 0x06008130 RID: 33072 RVA: 0x001B0241 File Offset: 0x001AE441
		[FeatureCalculator("k", Method = CalculationMethod.FromLiteral)]
		public static double KScore(int k)
		{
			if (k < 0)
			{
				return 1.0 / ((double)(-(double)k) + 1.1);
			}
			return 1.0 / ((double)k + 1.0);
		}
	}
}
