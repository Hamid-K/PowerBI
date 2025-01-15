using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Features;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Features
{
	// Token: 0x0200170B RID: 5899
	public class RankingScoreFeature : Feature<double>
	{
		// Token: 0x0600C463 RID: 50275 RVA: 0x002A483D File Offset: 0x002A2A3D
		public RankingScoreFeature(Grammar grammar, RankingDebugTrace debugTrace)
			: base(grammar, "Score", false, false, null, Feature<double>.FeatureInfoResolution.Declared)
		{
			this._debugTrace = debugTrace;
			this._awardRatioFeature = new RankingAwardRatioFeature(grammar, this._debugTrace);
			this._penaltyRatioFeature = new RankingPenaltyRatioFeature(grammar, this._debugTrace);
		}

		// Token: 0x0600C464 RID: 50276 RVA: 0x002A487C File Offset: 0x002A2A7C
		public override double Calculate(ProgramNode program, LearningInfo learningInfo)
		{
			GrammarRule grammarRule = program.GrammarRule;
			if (grammarRule != null && !(grammarRule is ConversionRule))
			{
				return this.Score(learningInfo);
			}
			return base.Calculate(program, learningInfo);
		}

		// Token: 0x0600C465 RID: 50277 RVA: 0x002A48B4 File Offset: 0x002A2AB4
		public double Score(LearningInfo learningInfo)
		{
			ProgramNode programNode = learningInfo.ProgramNode;
			IEnumerable<double> enumerable = programNode.GetFeatureValue<IEnumerable<double>>(this._awardRatioFeature, learningInfo.WithProgramNode(programNode));
			enumerable = enumerable.ToReadOnlyList<double>();
			IEnumerable<double> featureValue = programNode.GetFeatureValue<IEnumerable<double>>(this._penaltyRatioFeature, learningInfo.WithProgramNode(programNode));
			double num = (enumerable.Any<double>() ? enumerable.Average() : 0.0);
			double num2 = 100.0 * num;
			double num3 = 100000.0 + num2;
			double num4 = featureValue.Sum();
			double num5 = num4 * 1000.0;
			double num6 = num3 - num5;
			RankingDebugTrace debugTrace = this._debugTrace;
			if (debugTrace != null)
			{
				debugTrace.Record(new RankingDebugAggregateBuffer
				{
					Node = programNode,
					MaxScore = 100000.0,
					AwardRatio = num,
					AwardScore = num2,
					PenaltyRatio = num4,
					PenaltyScore = num5,
					Score = num6
				});
			}
			return num6;
		}

		// Token: 0x0600C466 RID: 50278 RVA: 0x0001AF59 File Offset: 0x00019159
		protected override double GetFeatureValueForVariable(VariableNode variable)
		{
			return 0.0;
		}

		// Token: 0x04004C9D RID: 19613
		private const double _awardRange = 100.0;

		// Token: 0x04004C9E RID: 19614
		private const double _maxScore = 100000.0;

		// Token: 0x04004C9F RID: 19615
		private const double _penaltyUnit = 1000.0;

		// Token: 0x04004CA0 RID: 19616
		private readonly RankingAwardRatioFeature _awardRatioFeature;

		// Token: 0x04004CA1 RID: 19617
		private readonly RankingDebugTrace _debugTrace;

		// Token: 0x04004CA2 RID: 19618
		private readonly RankingPenaltyRatioFeature _penaltyRatioFeature;
	}
}
