using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Features;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Features
{
	// Token: 0x020016FE RID: 5886
	public abstract class RankingRatioFeatureBase : Feature<IEnumerable<double>>
	{
		// Token: 0x0600C42F RID: 50223 RVA: 0x002A41C8 File Offset: 0x002A23C8
		protected RankingRatioFeatureBase(Grammar grammar, string featureName, RankingDebugTrace debugTrace)
			: base(grammar, featureName, false, false, null, Feature<IEnumerable<double>>.FeatureInfoResolution.Declared)
		{
			this._debugTrace = debugTrace;
			this.Builder = GrammarBuilders.Instance(grammar);
		}

		// Token: 0x17002172 RID: 8562
		// (get) Token: 0x0600C430 RID: 50224 RVA: 0x002A4254 File Offset: 0x002A2454
		protected GrammarBuilders Builder { get; }

		// Token: 0x17002173 RID: 8563
		// (get) Token: 0x0600C431 RID: 50225
		protected abstract RankingRatioKind Kind { get; }

		// Token: 0x0600C432 RID: 50226 RVA: 0x002A425C File Offset: 0x002A245C
		public override IEnumerable<double> Calculate(ProgramNode node, LearningInfo learningInfo)
		{
			VariableNode variableNode = node as VariableNode;
			if (variableNode != null)
			{
				return this.GetFeatureValueForVariable(variableNode);
			}
			LiteralNode literalNode = node as LiteralNode;
			if (literalNode != null)
			{
				Func<object, LearningInfo, double?> func;
				double? num = (this._symbolLookup.TryGetValue(literalNode.Symbol, out func) ? func(literalNode.Value, learningInfo) : this.DefaultSymbolRatio(learningInfo));
				RankingDebugTrace debugTrace = this._debugTrace;
				if (debugTrace != null)
				{
					debugTrace.RecordRatio(node, num, this.Kind);
				}
				IEnumerable<double> enumerable = ((num != null) ? num.GetValueOrDefault().Yield<double>().ToList<double>() : null);
				return enumerable ?? Utils.Empty<double>();
			}
			GrammarRule grammarRule = node.GrammarRule;
			string text = ((grammarRule != null) ? grammarRule.Id : null);
			List<double> list = new List<double>();
			if (text != null && node is NonterminalNode && !(node.GrammarRule is ConversionRule))
			{
				Func<LearningInfo, double?> func2;
				double? num2 = (this._ruleLookup.TryGetValue(text, out func2) ? func2(learningInfo) : this.DefaultRuleRatio(learningInfo));
				if (num2 != null)
				{
					list.Add(num2.Value);
				}
				RankingDebugTrace debugTrace2 = this._debugTrace;
				if (debugTrace2 != null)
				{
					debugTrace2.RecordRatio(node, num2, this.Kind);
				}
			}
			for (int i = 0; i < node.Children.Length; i++)
			{
				IEnumerable<double> featureValue = node.Children[i].GetFeatureValue<IEnumerable<double>>(this, (learningInfo != null) ? learningInfo.ForChild(i) : null);
				list.AddRange(featureValue);
			}
			return list.ToList<double>();
		}

		// Token: 0x0600C433 RID: 50227 RVA: 0x002A43CA File Offset: 0x002A25CA
		protected override IEnumerable<double> GetFeatureValueForVariable(VariableNode variable)
		{
			return Utils.Empty<double>();
		}

		// Token: 0x17002174 RID: 8564
		// (get) Token: 0x0600C434 RID: 50228 RVA: 0x002A43D1 File Offset: 0x002A25D1
		// (set) Token: 0x0600C435 RID: 50229 RVA: 0x002A43D9 File Offset: 0x002A25D9
		public Func<LearningInfo, double?> DefaultRuleRatio { get; private set; } = (LearningInfo _) => null;

		// Token: 0x17002175 RID: 8565
		// (get) Token: 0x0600C436 RID: 50230 RVA: 0x002A43E2 File Offset: 0x002A25E2
		// (set) Token: 0x0600C437 RID: 50231 RVA: 0x002A43EA File Offset: 0x002A25EA
		public Func<LearningInfo, double?> DefaultSymbolRatio { get; private set; } = (LearningInfo _) => null;

		// Token: 0x0600C438 RID: 50232 RVA: 0x002A43F3 File Offset: 0x002A25F3
		public void WithDefaultRuleRatio(Func<LearningInfo, double?> transform)
		{
			this.DefaultRuleRatio = transform;
		}

		// Token: 0x0600C439 RID: 50233 RVA: 0x002A43FC File Offset: 0x002A25FC
		public void WithDefaultRuleRatio(double? value)
		{
			this.DefaultRuleRatio = (LearningInfo _) => value;
		}

		// Token: 0x0600C43A RID: 50234 RVA: 0x002A4428 File Offset: 0x002A2628
		public void WithDefaultSymbolRatio(Func<LearningInfo, double?> transform)
		{
			this.DefaultSymbolRatio = transform;
		}

		// Token: 0x0600C43B RID: 50235 RVA: 0x002A4434 File Offset: 0x002A2634
		public void WithDefaultSymbolRatio(double? value)
		{
			this.DefaultSymbolRatio = (LearningInfo _) => value;
		}

		// Token: 0x0600C43C RID: 50236 RVA: 0x002A4460 File Offset: 0x002A2660
		public void WithRuleRatio(string ruleName, double? value)
		{
			this._ruleLookup.Add(ruleName, (LearningInfo _) => value);
		}

		// Token: 0x0600C43D RID: 50237 RVA: 0x002A4494 File Offset: 0x002A2694
		public void WithRuleRatio(IEnumerable<string> ruleNames, double? value)
		{
			Func<LearningInfo, double?> <>9__0;
			foreach (string text in ruleNames)
			{
				Dictionary<string, Func<LearningInfo, double?>> ruleLookup = this._ruleLookup;
				string text2 = text;
				Func<LearningInfo, double?> func;
				if ((func = <>9__0) == null)
				{
					func = (<>9__0 = (LearningInfo _) => value);
				}
				ruleLookup.Add(text2, func);
			}
		}

		// Token: 0x0600C43E RID: 50238 RVA: 0x002A4510 File Offset: 0x002A2710
		public void WithRuleRatio(string ruleName, Func<double?> transform)
		{
			this._ruleLookup.Add(ruleName, (LearningInfo _) => transform());
		}

		// Token: 0x0600C43F RID: 50239 RVA: 0x002A4542 File Offset: 0x002A2742
		public void WithRuleRatio(string ruleName, Func<LearningInfo, double?> transform)
		{
			this._ruleLookup.Add(ruleName, transform);
		}

		// Token: 0x0600C440 RID: 50240 RVA: 0x002A4554 File Offset: 0x002A2754
		public void WithRuleRatio(IEnumerable<string> ruleNames, Func<LearningInfo, double?> transform)
		{
			foreach (string text in ruleNames)
			{
				this._ruleLookup.Add(text, transform);
			}
		}

		// Token: 0x0600C441 RID: 50241 RVA: 0x002A45A4 File Offset: 0x002A27A4
		public void WithSymbolRatio(Symbol symbol, double? value)
		{
			this._symbolLookup.Add(symbol, (object _, LearningInfo _) => value);
		}

		// Token: 0x0600C442 RID: 50242 RVA: 0x002A45D8 File Offset: 0x002A27D8
		public void WithSymbolRatio<TValue>(Symbol symbol, Func<TValue, double?> transform)
		{
			this._symbolLookup.Add(symbol, (object value, LearningInfo _) => transform((TValue)((object)value)));
		}

		// Token: 0x0600C443 RID: 50243 RVA: 0x002A460C File Offset: 0x002A280C
		public void WithSymbolRatio<TValue>(Symbol symbol, Func<TValue, LearningInfo, double?> transform)
		{
			this._symbolLookup.Add(symbol, (object value, LearningInfo info) => transform((TValue)((object)value), info));
		}

		// Token: 0x0600C444 RID: 50244 RVA: 0x002A4640 File Offset: 0x002A2840
		public void WithSymbolRatio(Symbol symbol, Func<double?> transform)
		{
			this._symbolLookup.Add(symbol, (object _, LearningInfo _) => transform());
		}

		// Token: 0x0600C445 RID: 50245 RVA: 0x002A4674 File Offset: 0x002A2874
		public void WithSymbolRatio(Symbol symbol, Func<object, double?> transform)
		{
			this._symbolLookup.Add(symbol, (object value, LearningInfo _) => transform(value));
		}

		// Token: 0x0600C446 RID: 50246 RVA: 0x002A46A6 File Offset: 0x002A28A6
		public void WithSymbolRatio(Symbol symbol, Func<object, LearningInfo, double?> transform)
		{
			this._symbolLookup.Add(symbol, transform);
		}

		// Token: 0x0600C447 RID: 50247 RVA: 0x002A46B5 File Offset: 0x002A28B5
		protected static double InverseProportion(double value, int maxValue)
		{
			return 1.0 - RankingRatioFeatureBase.Proportion(value, (double)maxValue);
		}

		// Token: 0x0600C448 RID: 50248 RVA: 0x002A46CC File Offset: 0x002A28CC
		protected static double LocaleProportion(string locale, LearningInfo info)
		{
			double? num = null;
			RankingScoreFeatureOptions rankingScoreFeatureOptions = info.Options as RankingScoreFeatureOptions;
			if (rankingScoreFeatureOptions != null)
			{
				int? num2 = rankingScoreFeatureOptions.DataCultures.Select((CultureInfo c) => c.Name).IndexOf(locale);
				if (num2 != null)
				{
					num = new double?(RankingRatioFeatureBase.InverseProportion((double)num2.Value, rankingScoreFeatureOptions.DataCultures.Count));
				}
			}
			if (num != null && num.GetValueOrDefault() != 0.0)
			{
				return num.Value;
			}
			return 1E-06;
		}

		// Token: 0x0600C449 RID: 50249 RVA: 0x002A477E File Offset: 0x002A297E
		protected static double Proportion(double value, double maxValue)
		{
			return Math.Min(value, maxValue) / maxValue;
		}

		// Token: 0x04004C85 RID: 19589
		private readonly RankingDebugTrace _debugTrace;

		// Token: 0x04004C87 RID: 19591
		private readonly Dictionary<string, Func<LearningInfo, double?>> _ruleLookup = new Dictionary<string, Func<LearningInfo, double?>>();

		// Token: 0x04004C88 RID: 19592
		private readonly Dictionary<Symbol, Func<object, LearningInfo, double?>> _symbolLookup = new Dictionary<Symbol, Func<object, LearningInfo, double?>>();
	}
}
