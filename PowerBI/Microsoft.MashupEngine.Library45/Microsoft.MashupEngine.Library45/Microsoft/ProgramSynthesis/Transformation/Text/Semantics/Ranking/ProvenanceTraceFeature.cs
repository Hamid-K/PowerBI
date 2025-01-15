using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Features;
using Microsoft.ProgramSynthesis.Transformation.Text.Description;
using Microsoft.ProgramSynthesis.Wrangling;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Semantics.Ranking
{
	// Token: 0x02001D5D RID: 7517
	internal abstract class ProvenanceTraceFeature : BooleanRankingFeature
	{
		// Token: 0x0600FCC4 RID: 64708 RVA: 0x0035EA34 File Offset: 0x0035CC34
		protected ProvenanceTraceFeature(Grammar grammar, string name, double learnedCoefficient = 0.0)
			: base(grammar, name, learnedCoefficient, false)
		{
		}

		// Token: 0x0600FCC5 RID: 64709 RVA: 0x0035EA40 File Offset: 0x0035CC40
		[FeatureCalculator("Transformation", Method = CalculationMethod.FromProgramNode, SupportsLearningInfo = true)]
		public double Calculate(LearningInfo learningInfo, ProgramNode program)
		{
			if (learningInfo == null || learningInfo.FeatureCalculationContext.AllInputs.Count == 0)
			{
				return 0.0;
			}
			foreach (KeyValuePair<State, object> keyValuePair in learningInfo.GetInputOutputPairs(InputKind.All))
			{
				IRow row = (IRow)keyValuePair.Key[program.Grammar.InputSymbol];
				ValueSubstring valueSubstring = keyValuePair.Value as ValueSubstring;
				if (!(valueSubstring == null))
				{
					if ((ulong)valueSubstring.Length != (ulong)((long)valueSubstring.Source.Length))
					{
						string value = valueSubstring.Value;
						IType type = valueSubstring.Type;
						valueSubstring = ValueSubstring.Create(value, null, null, type, null);
					}
					foreach (var keyValuePair2 in (from p in OutputProvenance.Compute(base.Grammar, this._build.Node.Cast.st(program), DescriptionLookup.Create(program), row, valueSubstring).ToList<OutputProvenance>()
						where p.InputColumnName != null
						group p by p.InputColumnName).ToDictionary((IGrouping<string, OutputProvenance> group) => group.Key, (IGrouping<string, OutputProvenance> group) => (from p in @group
						where p.InputSubstring != null
						select new
						{
							p.InputSubstring.Start,
							p.InputSubstring.End
						}).ToList()))
					{
						string key = keyValuePair2.Key;
						var value2 = keyValuePair2.Value;
						for (int i = 0; i < value2.Count; i++)
						{
							uint start = value2[i].Start;
							uint end = value2[i].End;
							for (int j = i + 1; j < value2.Count; j++)
							{
								uint start2 = value2[j].Start;
								uint end2 = value2[j].End;
								if (this.HasOccurrence(key, start, end, start2, end2))
								{
									return 1.0;
								}
							}
						}
					}
				}
			}
			return 0.0;
		}

		// Token: 0x0600FCC6 RID: 64710
		protected abstract bool HasOccurrence(string input, uint start1, uint end1, uint start2, uint end2);
	}
}
