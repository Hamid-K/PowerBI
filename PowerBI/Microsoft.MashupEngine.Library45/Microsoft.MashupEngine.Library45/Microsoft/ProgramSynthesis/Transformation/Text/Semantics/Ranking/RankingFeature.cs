using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Features;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Transformation.Text.Build;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Semantics.Ranking
{
	// Token: 0x02001D5A RID: 7514
	internal abstract class RankingFeature : Feature<double>
	{
		// Token: 0x0600FCB5 RID: 64693 RVA: 0x0035E7C0 File Offset: 0x0035C9C0
		protected RankingFeature(Grammar grammar, string name, double learnedCoefficient = 0.0, bool accumulateDefinitions = true)
			: base(grammar, name, false, false, null, Feature<double>.FeatureInfoResolution.AlwaysCreate)
		{
			this._build = GrammarBuilders.Instance(grammar);
			this.Name = name;
			this.LearnedCoefficient = learnedCoefficient;
			this.AccumulateDefinitions = accumulateDefinitions;
			foreach (KeyValuePair<GrammarRule, FeatureCalculator> keyValuePair in base.Calculator)
			{
				if (keyValuePair.Value is CustomFeatureCalculator)
				{
					this._definingRules.Add(keyValuePair.Key);
				}
			}
			Queue<GrammarRule> queue = new Queue<GrammarRule>(this._definingRules);
			HashSet<GrammarRule> hashSet = new HashSet<GrammarRule>(this._definingRules);
			while (queue.Count > 0)
			{
				foreach (GrammarRule grammarRule in queue.Dequeue().Head.ReferencedBy)
				{
					if (!hashSet.Contains(grammarRule))
					{
						hashSet.Add(grammarRule);
						queue.Enqueue(grammarRule);
					}
				}
			}
			hashSet.ExceptWith(this._definingRules);
			this._accumulatingRules = hashSet;
		}

		// Token: 0x17002A1C RID: 10780
		// (get) Token: 0x0600FCB6 RID: 64694 RVA: 0x0035E8FC File Offset: 0x0035CAFC
		public string Name { get; }

		// Token: 0x17002A1D RID: 10781
		// (get) Token: 0x0600FCB7 RID: 64695 RVA: 0x0035E904 File Offset: 0x0035CB04
		// (set) Token: 0x0600FCB8 RID: 64696 RVA: 0x0035E90C File Offset: 0x0035CB0C
		public double LearnedCoefficient { get; set; }

		// Token: 0x17002A1E RID: 10782
		// (get) Token: 0x0600FCB9 RID: 64697 RVA: 0x0035E915 File Offset: 0x0035CB15
		public virtual double InitialOptimizationCoefficient { get; }

		// Token: 0x17002A1F RID: 10783
		// (get) Token: 0x0600FCBA RID: 64698 RVA: 0x0035E91D File Offset: 0x0035CB1D
		// (set) Token: 0x0600FCBB RID: 64699 RVA: 0x0035E925 File Offset: 0x0035CB25
		public bool AccumulateDefinitions { get; set; }

		// Token: 0x0600FCBC RID: 64700 RVA: 0x0001AF59 File Offset: 0x00019159
		public virtual double DefaultValue(ProgramNode program)
		{
			return 0.0;
		}

		// Token: 0x0600FCBD RID: 64701 RVA: 0x0035E92E File Offset: 0x0035CB2E
		protected override double GetFeatureValueForVariable(VariableNode variable)
		{
			return this.DefaultValue(variable);
		}

		// Token: 0x0600FCBE RID: 64702 RVA: 0x0035E938 File Offset: 0x0035CB38
		public override double Calculate(ProgramNode program, LearningInfo learningInfo)
		{
			GrammarRule grammarRule = program.GrammarRule;
			if (grammarRule == null)
			{
				return this.DefaultValue(program);
			}
			bool flag = this._definingRules.Contains(grammarRule);
			bool flag2 = this._accumulatingRules.Contains(grammarRule);
			if (!flag && !flag2)
			{
				return this.DefaultValue(program);
			}
			if (!flag2 && !this.AccumulateDefinitions)
			{
				return base.Calculate(program, learningInfo);
			}
			double num = program.Children.Select((ProgramNode p) => p.GetFeatureValue<double>(this, learningInfo)).Aggregate(this.DefaultValue(program), new Func<double, double, double>(this.Accumulate));
			if (!flag2)
			{
				return this.Accumulate(num, base.Calculate(program, learningInfo));
			}
			return num;
		}

		// Token: 0x0600FCBF RID: 64703 RVA: 0x0014755B File Offset: 0x0014575B
		protected virtual double Accumulate(double x, double y)
		{
			return x + y;
		}

		// Token: 0x04005E4F RID: 24143
		protected readonly GrammarBuilders _build;

		// Token: 0x04005E50 RID: 24144
		private readonly HashSet<GrammarRule> _accumulatingRules;

		// Token: 0x04005E51 RID: 24145
		private readonly HashSet<GrammarRule> _definingRules = new HashSet<GrammarRule>();
	}
}
