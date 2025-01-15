using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Features;
using Microsoft.ProgramSynthesis.Transformation.Tree.Build;
using Microsoft.ProgramSynthesis.Utils.Interactive;
using Microsoft.ProgramSynthesis.Wrangling.Tree;

namespace Microsoft.ProgramSynthesis.Transformation.Tree.Learning
{
	// Token: 0x02001EB1 RID: 7857
	public class RankingScore : Feature<double>
	{
		// Token: 0x06010943 RID: 67907 RVA: 0x003902F7 File Offset: 0x0038E4F7
		public RankingScore(Grammar grammar, Witnesses.Options options = null)
			: base(grammar, "Score", false, false, null, Feature<double>.FeatureInfoResolution.Declared)
		{
			this._build = GrammarBuilders.Instance(grammar);
			this._options = options;
		}

		// Token: 0x06010944 RID: 67908 RVA: 0x0001AF59 File Offset: 0x00019159
		protected override double GetFeatureValueForVariable(VariableNode variable)
		{
			return 0.0;
		}

		// Token: 0x06010945 RID: 67909 RVA: 0x00004FAE File Offset: 0x000031AE
		[FeatureCalculator("TmpFilter")]
		public static double Scores_TmpFilter(double match, double nodes)
		{
			return match;
		}

		// Token: 0x06010946 RID: 67910 RVA: 0x0001AF59 File Offset: 0x00019159
		[FeatureCalculator("Not")]
		public static double Scores_Not(double pred)
		{
			return 0.0;
		}

		// Token: 0x06010947 RID: 67911 RVA: 0x000BFE71 File Offset: 0x000BE071
		[FeatureCalculator("Conj")]
		public static double Scores_Conj(double pred, double match)
		{
			return pred + match;
		}

		// Token: 0x06010948 RID: 67912 RVA: 0x00004FAE File Offset: 0x000031AE
		[FeatureCalculator("AbsPos")]
		public static double Score_AbsPos(double p)
		{
			return p;
		}

		// Token: 0x06010949 RID: 67913 RVA: 0x0039031C File Offset: 0x0038E51C
		[FeatureCalculator("RelChild", Method = CalculationMethod.FromChildrenNodes)]
		public double Score_RelChild(ProgramNode n)
		{
			ProgramNode programNode = n.Children[0].Children[0];
			int num = (RankingScore.<Score_RelChild>g__HasBlockParent|9_0(programNode.Children[0]) ? 150 : 50);
			if (programNode.GetFeatureValue<double>(this, null) < (double)num)
			{
				return double.NegativeInfinity;
			}
			return n.GetFeatureValue<double>(this, null);
		}

		// Token: 0x0601094A RID: 67914 RVA: 0x0039036E File Offset: 0x0038E56E
		[FeatureCalculator("IsNthChild")]
		public static double Score_IsNthChild(double x, double k)
		{
			return 10.0 * k;
		}

		// Token: 0x0601094B RID: 67915 RVA: 0x0003B61D File Offset: 0x0003981D
		[FeatureCalculator("IsKind")]
		public static double Score_IsKind(double x, double k, double kind)
		{
			return kind;
		}

		// Token: 0x0601094C RID: 67916 RVA: 0x0039037B File Offset: 0x0038E57B
		[FeatureCalculator("IsAttributePresent")]
		public static double Score_IsAttributePresent(double x, double k, double name, double value)
		{
			return 8.0 * value * name;
		}

		// Token: 0x0601094D RID: 67917 RVA: 0x0039038A File Offset: 0x0038E58A
		[FeatureCalculator("HasNChildren")]
		public static double Score_HasNChildren(double x, double path, double k)
		{
			return 10.0 * k;
		}

		// Token: 0x0601094E RID: 67918 RVA: 0x00390398 File Offset: 0x0038E598
		[FeatureCalculator("Select", SupportsLearningInfo = true)]
		public double Scores_Select(LearningInfo info, double pred, double k)
		{
			int? num;
			if (info == null)
			{
				num = null;
			}
			else
			{
				IEnumerable<object> outputs = info.GetOutputs(InputKind.All);
				if (outputs == null)
				{
					num = null;
				}
				else
				{
					IEnumerable<Node> enumerable = outputs.Cast<Node>();
					if (enumerable == null)
					{
						num = null;
					}
					else
					{
						num = new int?(enumerable.Sum(delegate(Node node)
						{
							if (node == null)
							{
								return 0;
							}
							return node.Count;
						}));
					}
				}
			}
			int? num2 = num;
			int? num3;
			if (info == null)
			{
				num3 = null;
			}
			else
			{
				IEnumerable<KeyValuePair<State, object>> inputOutputPairs = info.GetInputOutputPairs(InputKind.All);
				num3 = ((inputOutputPairs != null) ? new int?(inputOutputPairs.Sum(delegate(KeyValuePair<State, object> kvp)
				{
					Node node = (Node)kvp.Key[this._build.Symbol.selectedNode];
					if (node == null)
					{
						return 0;
					}
					return node.Count;
				})) : null);
			}
			int? num4 = num3;
			double? num5;
			if (num4 == null)
			{
				num5 = null;
			}
			else
			{
				int? num6 = num2;
				double? num7 = ((num6 != null) ? new double?((double)num6.GetValueOrDefault()) : null);
				num6 = num4;
				num5 = num7 / ((num6 != null) ? new double?((double)num6.GetValueOrDefault()) : null);
			}
			double? num8 = num5;
			return 4.0 * ((num8 * num8) ?? 1.0) * pred - k;
		}

		// Token: 0x0601094F RID: 67919 RVA: 0x00004FAE File Offset: 0x000031AE
		[FeatureCalculator("SingleList")]
		public static double Scores_SingleList(double x)
		{
			return x;
		}

		// Token: 0x06010950 RID: 67920 RVA: 0x00012DE5 File Offset: 0x00010FE5
		[FeatureCalculator("InOrderAllNodes")]
		public static double Score_InOrderAllNodes(double node)
		{
			return 1.0;
		}

		// Token: 0x06010951 RID: 67921 RVA: 0x000BFE71 File Offset: 0x000BE071
		[FeatureCalculator("Prepend")]
		public static double Score_Prepend(double x, double elements)
		{
			return x + elements;
		}

		// Token: 0x06010952 RID: 67922 RVA: 0x00390537 File Offset: 0x0038E737
		[FeatureCalculator("DeleteChild")]
		public static double Score_DeleteChild(double select, double index)
		{
			return 0.1 * select + 0.5 * index;
		}

		// Token: 0x06010953 RID: 67923 RVA: 0x00390550 File Offset: 0x0038E750
		[FeatureCalculator("InsertAtAbs")]
		public static double Score_InsertAt(double select, double index, double node)
		{
			return 0.1 * select + 0.5 * index + node;
		}

		// Token: 0x06010954 RID: 67924 RVA: 0x00390550 File Offset: 0x0038E750
		[FeatureCalculator("InsertAtRel")]
		public static double Score_InsertAtRel(double select, double index, double node)
		{
			return 0.1 * select + 0.5 * index + node;
		}

		// Token: 0x06010955 RID: 67925 RVA: 0x00390550 File Offset: 0x0038E750
		[FeatureCalculator("ReplaceChildren")]
		public static double Score_ReplaceChildren(double select, double relPoslist, double children)
		{
			return 0.1 * select + 0.5 * relPoslist + children;
		}

		// Token: 0x06010956 RID: 67926 RVA: 0x000BFE71 File Offset: 0x000BE071
		[FeatureCalculator("ConcatChild")]
		public static double Score_ConcatChild(double head, double tail)
		{
			return head + tail;
		}

		// Token: 0x06010957 RID: 67927 RVA: 0x00004FAE File Offset: 0x000031AE
		[FeatureCalculator("SinglePosList")]
		public static double Score_SinglePosList(double pos)
		{
			return pos;
		}

		// Token: 0x06010958 RID: 67928 RVA: 0x0001AE68 File Offset: 0x00019068
		[FeatureCalculator("LeafConstLabelNode")]
		public static double Score_LeafConstLabelNode(double label, double attribute)
		{
			return -1.0;
		}

		// Token: 0x06010959 RID: 67929 RVA: 0x0001AE68 File Offset: 0x00019068
		[FeatureCalculator("LeafConstSequenceLabelNode")]
		public static double Score_LeafConstSequenceLabelNode(double label, double attribute, double separator)
		{
			return -1.0;
		}

		// Token: 0x0601095A RID: 67930 RVA: 0x0039056B File Offset: 0x0038E76B
		[FeatureCalculator("ConstLabelNode")]
		public static double Score_ConstLabelNode(double label, double attribute, double children)
		{
			return -1.0 + children;
		}

		// Token: 0x0601095B RID: 67931 RVA: 0x00390578 File Offset: 0x0038E778
		[FeatureCalculator("ConstSequenceLabelNode")]
		public static double Score_ConstSequenceLabelNode(double label, double attribute, double separator, double children)
		{
			return -1.0 + children;
		}

		// Token: 0x0601095C RID: 67932 RVA: 0x00390585 File Offset: 0x0038E785
		[FeatureCalculator("label", Method = CalculationMethod.FromLiteral)]
		public static double Score_String(string s)
		{
			return (double)(Witnesses.Keywords.Contains(s) ? (1000 + s.GetHashCode()) : (1 + s.GetHashCode()));
		}

		// Token: 0x0601095D RID: 67933 RVA: 0x003905AC File Offset: 0x0038E7AC
		[FeatureCalculator("attributes", Method = CalculationMethod.FromLiteral)]
		public static double Score_Attributes(Dictionary<string, string> attributes)
		{
			if (attributes.IsEmpty<KeyValuePair<string, string>>())
			{
				return 0.0;
			}
			if (attributes.Values.Any((string v) => Witnesses.Keywords.Contains(v)))
			{
				return 1000.0;
			}
			return (double)attributes.Count;
		}

		// Token: 0x0601095E RID: 67934 RVA: 0x00390608 File Offset: 0x0038E808
		[FeatureCalculator("kind", Method = CalculationMethod.FromLiteral)]
		public static double Score_Kind(string kind)
		{
			return 15.0;
		}

		// Token: 0x0601095F RID: 67935 RVA: 0x00390614 File Offset: 0x0038E814
		[FeatureCalculator("name", Method = CalculationMethod.FromLiteral)]
		public double Score_Name(string name)
		{
			Witnesses.Options options = this._options;
			double num;
			if (((options != null) ? options.AttributeNameScore : null) == null || !this._options.AttributeNameScore.TryGetValue(name, out num))
			{
				return 1.0;
			}
			return num;
		}

		// Token: 0x06010960 RID: 67936 RVA: 0x00390655 File Offset: 0x0038E855
		[FeatureCalculator("value", Method = CalculationMethod.FromLiteral)]
		public static double Score_Value(string value)
		{
			return !(value == string.Empty);
		}

		// Token: 0x06010961 RID: 67937 RVA: 0x00390666 File Offset: 0x0038E866
		[FeatureCalculator("k", Method = CalculationMethod.FromLiteral)]
		public static double Score_k(int k)
		{
			return (double)(1 + Math.Abs(k));
		}

		// Token: 0x06010962 RID: 67938 RVA: 0x00390671 File Offset: 0x0038E871
		[FeatureCalculator("p", Method = CalculationMethod.FromLiteral)]
		public static double Score_p(int p)
		{
			if (Math.Abs(p) == 1)
			{
				return 50.0;
			}
			if (p < 0)
			{
				return -200.0;
			}
			return (double)p;
		}

		// Token: 0x06010963 RID: 67939 RVA: 0x00390696 File Offset: 0x0038E896
		[FeatureCalculator("path", Method = CalculationMethod.FromLiteral)]
		public static double Score_Path(TreePath path)
		{
			if (path == null)
			{
				return double.MinValue;
			}
			return path.Score;
		}

		// Token: 0x06010964 RID: 67940 RVA: 0x000BFE71 File Offset: 0x000BE071
		[FeatureCalculator("SequenceMap")]
		public static double Score_SequenceMap(double node, double parentChildren)
		{
			return node + parentChildren;
		}

		// Token: 0x06010965 RID: 67941 RVA: 0x00004FAE File Offset: 0x000031AE
		[FeatureCalculator("Children")]
		public static double Score_Children(double parent)
		{
			return parent;
		}

		// Token: 0x06010966 RID: 67942 RVA: 0x000BFE71 File Offset: 0x000BE071
		[FeatureCalculator("GuardedRule")]
		public static double Score_GuardedRule(double guard, double transformation)
		{
			return guard + transformation;
		}

		// Token: 0x06010967 RID: 67943 RVA: 0x003906AC File Offset: 0x0038E8AC
		[CompilerGenerated]
		internal static bool <Score_RelChild>g__HasBlockParent|9_0(ProgramNode currentNode)
		{
			return currentNode.Children.Length != 0 && (currentNode.Children[0].ToString() == "IsKind(x, \"[ParentStep]\", \"BlockSeq\")" || (currentNode.Children.Length == 2 && RankingScore.<Score_RelChild>g__HasBlockParent|9_0(currentNode.Children[1])));
		}

		// Token: 0x0400631C RID: 25372
		private const double DUMMY_SCORE = 6174.0;

		// Token: 0x0400631D RID: 25373
		private readonly GrammarBuilders _build;

		// Token: 0x0400631E RID: 25374
		private readonly Witnesses.Options _options;
	}
}
