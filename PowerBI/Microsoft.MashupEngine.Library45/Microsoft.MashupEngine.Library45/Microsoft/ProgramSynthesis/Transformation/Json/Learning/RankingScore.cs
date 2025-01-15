using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Features;
using Microsoft.ProgramSynthesis.Transformation.Text;
using Microsoft.ProgramSynthesis.Transformation.Text.Semantics;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.Json;
using Microsoft.ProgramSynthesis.Wrangling.Json.JpathStep;
using Newtonsoft.Json.Linq;

namespace Microsoft.ProgramSynthesis.Transformation.Json.Learning
{
	// Token: 0x02001A4F RID: 6735
	public class RankingScore : Feature<double>
	{
		// Token: 0x0600DE02 RID: 56834 RVA: 0x002F26B8 File Offset: 0x002F08B8
		public RankingScore(Grammar grammar)
			: base(grammar, "Score", false, false, null, Feature<double>.FeatureInfoResolution.Declared)
		{
		}

		// Token: 0x0600DE03 RID: 56835 RVA: 0x0001AF59 File Offset: 0x00019159
		protected override double GetFeatureValueForVariable(VariableNode variable)
		{
			return 0.0;
		}

		// Token: 0x0600DE04 RID: 56836 RVA: 0x00004FAE File Offset: 0x000031AE
		[FeatureCalculator("Object")]
		public static double Score_Object(double keyValue)
		{
			return keyValue;
		}

		// Token: 0x0600DE05 RID: 56837 RVA: 0x002F26F4 File Offset: 0x002F08F4
		[FeatureCalculator("Append")]
		public static double Score_Append(double keyValue, double obj)
		{
			return -100.0 + keyValue + obj;
		}

		// Token: 0x0600DE06 RID: 56838 RVA: 0x002F2703 File Offset: 0x002F0903
		[FeatureCalculator("FlattenObject")]
		public static double Score_FlattenObject(double x, double path)
		{
			return RankingScore.Score_Select(path);
		}

		// Token: 0x0600DE07 RID: 56839 RVA: 0x00004FAE File Offset: 0x000031AE
		[FeatureCalculator("Array")]
		public static double Score_Array(double elements)
		{
			return elements;
		}

		// Token: 0x0600DE08 RID: 56840 RVA: 0x002F270B File Offset: 0x002F090B
		[FeatureCalculator("ToArray")]
		public static double Score_ToArray(double x, double path)
		{
			return -500.0 + RankingScore.Score_Select(path);
		}

		// Token: 0x0600DE09 RID: 56841 RVA: 0x002F2720 File Offset: 0x002F0920
		[FeatureCalculator("Property", Method = CalculationMethod.FromChildrenNodes)]
		public double Score_Property(ProgramNode key, ProgramNode value)
		{
			double num = key.GetFeatureValue<double>(this, null) + value.GetFeatureValue<double>(this, null);
			HashSet<int> hashSet = RankingScore.ArrayIndices(key, new HashSet<int>());
			HashSet<int> hashSet2 = RankingScore.ArrayIndices(value, new HashSet<int>());
			return num + (double)hashSet2.Except(hashSet).Count<int>() * -100.0 / 10.0;
		}

		// Token: 0x0600DE0A RID: 56842 RVA: 0x002F2778 File Offset: 0x002F0978
		[FeatureCalculator("Key")]
		public static double Score_KeyFromValue(double selectValue)
		{
			return -100.0 + selectValue;
		}

		// Token: 0x0600DE0B RID: 56843 RVA: 0x002F2785 File Offset: 0x002F0985
		[FeatureCalculator("ConstKey")]
		public static double Score_ConstKey(double str)
		{
			return -250.0 + str;
		}

		// Token: 0x0600DE0C RID: 56844 RVA: 0x002F2778 File Offset: 0x002F0978
		[FeatureCalculator("Value")]
		public static double Score_Value(double selectKey)
		{
			return -100.0 + selectKey;
		}

		// Token: 0x0600DE0D RID: 56845 RVA: 0x002F2792 File Offset: 0x002F0992
		[FeatureCalculator("TransformValue")]
		public static double Score_TransformValue(double transformLet)
		{
			return -500.0 + transformLet;
		}

		// Token: 0x0600DE0E RID: 56846 RVA: 0x002F279F File Offset: 0x002F099F
		[FeatureCalculator("ConstValue")]
		public static double Score_ConstValue(double str)
		{
			return -1000.0 + str;
		}

		// Token: 0x0600DE0F RID: 56847 RVA: 0x000BFE71 File Offset: 0x000BE071
		[FeatureCalculator("Transform")]
		public static double Score_Transform(double token, double arrayValue)
		{
			return token + arrayValue;
		}

		// Token: 0x0600DE10 RID: 56848 RVA: 0x000BFE71 File Offset: 0x000BE071
		[FeatureCalculator("TransformFlatten")]
		public static double Score_Flatten(double token, double arrayValue)
		{
			return token + arrayValue;
		}

		// Token: 0x0600DE11 RID: 56849 RVA: 0x002F2703 File Offset: 0x002F0903
		[FeatureCalculator("SelectObject")]
		public static double Score_SelectObject(double x, double path)
		{
			return RankingScore.Score_Select(path);
		}

		// Token: 0x0600DE12 RID: 56850 RVA: 0x002F2703 File Offset: 0x002F0903
		[FeatureCalculator("SelectProperty")]
		public static double Score_SelectProperty(double x, double path)
		{
			return RankingScore.Score_Select(path);
		}

		// Token: 0x0600DE13 RID: 56851 RVA: 0x002F2703 File Offset: 0x002F0903
		[FeatureCalculator("SelectKey")]
		public static double Score_SelectKey(double x, double path)
		{
			return RankingScore.Score_Select(path);
		}

		// Token: 0x0600DE14 RID: 56852 RVA: 0x002F2703 File Offset: 0x002F0903
		[FeatureCalculator("SelectValue")]
		public static double Score_SelectValue(double x, double path)
		{
			return RankingScore.Score_Select(path);
		}

		// Token: 0x0600DE15 RID: 56853 RVA: 0x0001AF59 File Offset: 0x00019159
		[FeatureCalculator("SelectStringValues")]
		public static double Score_SelectStringValue(double x)
		{
			return 0.0;
		}

		// Token: 0x0600DE16 RID: 56854 RVA: 0x002F27AC File Offset: 0x002F09AC
		[FeatureCalculator("ValueToString")]
		public static double Score_ValueToString(double x, double path)
		{
			return RankingScore.Score_SelectValue(x, path) - 1.0;
		}

		// Token: 0x0600DE17 RID: 56855 RVA: 0x002F27BF File Offset: 0x002F09BF
		[FeatureCalculator("ConvertValueTo")]
		public static double Score_ConvertValueTo(double x, double t, double path)
		{
			return RankingScore.Score_SelectValue(x, path) - 1.0;
		}

		// Token: 0x17002524 RID: 9508
		// (get) Token: 0x0600DE18 RID: 56856 RVA: 0x002F27D2 File Offset: 0x002F09D2
		[ExternFeatureMapping("TransformString", 0)]
		public IFeature Score_TransformString { get; } = new RankingScore(Language.Grammar, new RankingScoreModel(), false, null);

		// Token: 0x0600DE19 RID: 56857 RVA: 0x002F2703 File Offset: 0x002F0903
		[FeatureCalculator("SelectArray")]
		public static double Score_SelectArray(double x, double path)
		{
			return RankingScore.Score_Select(path);
		}

		// Token: 0x0600DE1A RID: 56858 RVA: 0x002F2778 File Offset: 0x002F0978
		private static double Score_Select(double path)
		{
			return -100.0 + path;
		}

		// Token: 0x0600DE1B RID: 56859 RVA: 0x000EB288 File Offset: 0x000E9488
		[FeatureCalculator("path", Method = CalculationMethod.FromLiteral)]
		public static double Score_Path(JPath path)
		{
			return path.Score;
		}

		// Token: 0x0600DE1C RID: 56860 RVA: 0x002F27DA File Offset: 0x002F09DA
		[FeatureCalculator("str", Method = CalculationMethod.FromLiteral)]
		public static double Score_ConstStr(string str)
		{
			return (double)(-(double)str.Length);
		}

		// Token: 0x0600DE1D RID: 56861 RVA: 0x0001AF59 File Offset: 0x00019159
		[FeatureCalculator("t", Method = CalculationMethod.FromLiteral)]
		public static double Score_Type(JTokenType type)
		{
			return 0.0;
		}

		// Token: 0x0600DE1E RID: 56862 RVA: 0x002F27E4 File Offset: 0x002F09E4
		private static HashSet<int> ArrayIndices(ProgramNode node, HashSet<int> indices)
		{
			LiteralNode literalNode = node as LiteralNode;
			if (literalNode != null)
			{
				JPath jpath = literalNode.Value as JPath;
				HashSet<int> hashSet;
				if (jpath == null)
				{
					hashSet = null;
				}
				else
				{
					hashSet = (from idx in jpath.Steps.OfType<IndexStep>()
						select idx.K).ConvertToHashSet<int>();
				}
				return hashSet ?? new HashSet<int>();
			}
			if (node as NonterminalNode == null)
			{
				return new HashSet<int>();
			}
			HashSet<int> hashSet2 = new HashSet<int>();
			foreach (ProgramNode programNode in node.Children)
			{
				hashSet2.UnionWith(RankingScore.ArrayIndices(programNode, indices));
			}
			return hashSet2;
		}

		// Token: 0x04005440 RID: 21568
		private const double BaseScore = -100.0;
	}
}
