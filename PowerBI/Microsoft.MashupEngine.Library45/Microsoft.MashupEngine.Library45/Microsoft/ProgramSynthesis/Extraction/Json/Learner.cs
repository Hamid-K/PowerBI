using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Extraction.Json.Learning;
using Microsoft.ProgramSynthesis.Features;
using Microsoft.ProgramSynthesis.Learning;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.VersionSpace;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;
using Microsoft.ProgramSynthesis.Wrangling.Exceptions;
using Microsoft.ProgramSynthesis.Wrangling.Json;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;
using Newtonsoft.Json.Linq;

namespace Microsoft.ProgramSynthesis.Extraction.Json
{
	// Token: 0x02000B28 RID: 2856
	public class Learner : ProgramLearner<Program, string, ITable<string>>
	{
		// Token: 0x0600474B RID: 18251 RVA: 0x000DF627 File Offset: 0x000DD827
		private Learner()
			: base(false, false)
		{
		}

		// Token: 0x17000CCE RID: 3278
		// (get) Token: 0x0600474C RID: 18252 RVA: 0x000DF641 File Offset: 0x000DD841
		public static Learner Instance { get; } = new Learner();

		// Token: 0x17000CCF RID: 3279
		// (get) Token: 0x0600474D RID: 18253 RVA: 0x000DF648 File Offset: 0x000DD848
		public override Feature<double> ScoreFeature { get; } = new RankingScore(Language.Grammar);

		// Token: 0x0600474E RID: 18254 RVA: 0x000DF650 File Offset: 0x000DD850
		protected override ProgramCollection<Program, string, ITable<string>, TFeatureValue> LearnTopKUnchecked<TFeatureValue>(IEnumerable<Constraint<string, ITable<string>>> constraints, Feature<TFeatureValue> feature, int k, int? numRandomProgramsToInclude, ProgramSamplingStrategy samplingStrategy, IEnumerable<string> additionalInputs = null, CancellationToken cancel = default(CancellationToken), Guid? guid = null)
		{
			bool isNdJson;
			string startDelimiter;
			string endDelimiter;
			bool handleInvalidJson;
			IReadOnlyList<Tuple<output, MultiTargetCode>> readOnlyList = this.LearnImpl(constraints, additionalInputs, out isNdJson, out startDelimiter, out endDelimiter, out handleInvalidJson);
			if (readOnlyList != null && readOnlyList.Count != 0)
			{
				return new ProgramCollection<Program, string, ITable<string>, TFeatureValue>(readOnlyList.Take(k).Select2((output p, MultiTargetCode code) => new Program(p, isNdJson, startDelimiter, endDelimiter, handleInvalidJson, 0.0, code)), null, null, null);
			}
			return ProgramCollection<Program, string, ITable<string>, TFeatureValue>.Empty;
		}

		// Token: 0x0600474F RID: 18255 RVA: 0x000DF6B8 File Offset: 0x000DD8B8
		public override ProgramSet LearnAll(IEnumerable<Constraint<string, ITable<string>>> constraints, IEnumerable<string> additionalInputs = null, CancellationToken cancel = default(CancellationToken))
		{
			Symbol startSymbol = Language.Grammar.StartSymbol;
			bool flag;
			string text;
			string text2;
			bool flag2;
			IReadOnlyList<Tuple<output, MultiTargetCode>> readOnlyList = this.LearnImpl(constraints, additionalInputs, out flag, out text, out text2, out flag2);
			IEnumerable<output> enumerable;
			if (readOnlyList == null)
			{
				enumerable = null;
			}
			else
			{
				enumerable = readOnlyList.Select((Tuple<output, MultiTargetCode> p) => p.Item1);
			}
			return ProgramSetBuilder.List<output>(startSymbol, enumerable).Set;
		}

		// Token: 0x06004750 RID: 18256 RVA: 0x000DF714 File Offset: 0x000DD914
		private IReadOnlyList<Tuple<output, MultiTargetCode>> LearnImpl(IEnumerable<Constraint<string, ITable<string>>> constraints, IEnumerable<string> inputs, out bool isNdJson, out string startDelimiter, out string endDelimiter, out bool handleInvalidJson)
		{
			SynthesisOptions synthesisOptions = new SynthesisOptions();
			foreach (Constraint<string, ITable<string>> constraint in constraints)
			{
				IOptionConstraint<SynthesisOptions> optionConstraint = constraint as IOptionConstraint<SynthesisOptions>;
				if (optionConstraint == null)
				{
					if (!(constraint is KnownProgram<string, ITable<string>>))
					{
						throw new InvalidConstraintException(FormattableString.Invariant(FormattableStringFactory.Create("Unsupported constraints {0}", new object[] { constraint.GetType() })));
					}
				}
				else
				{
					optionConstraint.SetOptions(synthesisOptions);
				}
			}
			List<ParsedJson> list = new List<ParsedJson>();
			handleInvalidJson = synthesisOptions.HandleInvalidJson;
			if (inputs != null)
			{
				using (IEnumerator<string> enumerator2 = inputs.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						ParsedJson parsedJson;
						if (Utils.TryParse(enumerator2.Current, out parsedJson))
						{
							if (parsedJson.Regions.Select((JsonRegion region) => region.Token.Type).All((JTokenType t) => t == JTokenType.Array || t == JTokenType.Object) && (parsedJson.Errors == JsonErrors.None || synthesisOptions.HandleInvalidJson))
							{
								handleInvalidJson = handleInvalidJson || parsedJson.Errors > JsonErrors.None;
								list.Add(parsedJson);
							}
						}
					}
				}
			}
			if (list.Count == 0)
			{
				throw new InputsRequiredException("No valid JSON input found.");
			}
			HashSet<bool> hashSet = list.Select((ParsedJson json) => json.IsDelimitedJson).ConvertToHashSet<bool>();
			if (hashSet.Count > 1)
			{
				throw new InvalidInputException(FormattableString.Invariant(FormattableStringFactory.Create("Inputs contain mix of newline-delimited and non newline-delimited Json text.", Array.Empty<object>())));
			}
			isNdJson = hashSet.Count == 1 && hashSet.First<bool>();
			HashSet<string> hashSet2 = list.Select((ParsedJson json) => json.StartDelimiter).ConvertToHashSet<string>();
			if (hashSet2.Count > 1)
			{
				throw new InvalidInputException(FormattableString.Invariant(FormattableStringFactory.Create("Inputs contain mix of delimiters in Json text.", Array.Empty<object>())));
			}
			startDelimiter = hashSet2.First<string>();
			HashSet<string> hashSet3 = list.Select((ParsedJson json) => json.EndDelimiter).ConvertToHashSet<string>();
			if (hashSet3.Count > 1)
			{
				throw new InvalidInputException(FormattableString.Invariant(FormattableStringFactory.Create("Inputs contain mix of delimiters in Json text.", Array.Empty<object>())));
			}
			endDelimiter = hashSet3.First<string>();
			IReadOnlyList<output> readOnlyList = new AutoLearner(Language.Grammar, synthesisOptions).Learn(list);
			MultiTargetCode targetCodes = TargetLearner.Learn(list, synthesisOptions);
			if (readOnlyList == null)
			{
				return null;
			}
			return readOnlyList.Select((output p) => Tuple.Create<output, MultiTargetCode>(p, targetCodes)).ToList<Tuple<output, MultiTargetCode>>();
		}
	}
}
