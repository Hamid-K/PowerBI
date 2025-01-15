using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Features;
using Microsoft.ProgramSynthesis.Learning;
using Microsoft.ProgramSynthesis.Read.FlatFile.Learning;
using Microsoft.ProgramSynthesis.VersionSpace;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Read.FlatFile
{
	// Token: 0x02001244 RID: 4676
	public class Learner : ProgramLearner<Program, string, ITable<string>>
	{
		// Token: 0x06008CBA RID: 36026 RVA: 0x001D8FA3 File Offset: 0x001D71A3
		private Learner()
			: base(false, true)
		{
		}

		// Token: 0x1700181E RID: 6174
		// (get) Token: 0x06008CBB RID: 36027 RVA: 0x001D8FBD File Offset: 0x001D71BD
		public static Learner Instance { get; } = new Learner();

		// Token: 0x1700181F RID: 6175
		// (get) Token: 0x06008CBC RID: 36028 RVA: 0x001D8FC4 File Offset: 0x001D71C4
		public override Feature<double> ScoreFeature { get; } = new RankingScore(Language.Grammar);

		// Token: 0x06008CBD RID: 36029 RVA: 0x001D8FCC File Offset: 0x001D71CC
		public override ProgramSet LearnAll(IEnumerable<Constraint<string, ITable<string>>> constraints, IEnumerable<string> additionalInputs = null, CancellationToken cancel = default(CancellationToken))
		{
			IEnumerable<Program> enumerable = Learner.Learn(constraints, null, additionalInputs, null, cancel);
			return new DirectProgramSet(Language.Build.Symbol.readFlatFile, enumerable.Select((Program p) => p.ProgramNode));
		}

		// Token: 0x06008CBE RID: 36030 RVA: 0x001D902D File Offset: 0x001D722D
		protected override ProgramCollection<Program, string, ITable<string>, TFeatureValue> LearnTopKUnchecked<TFeatureValue>(IEnumerable<Constraint<string, ITable<string>>> constraints, Feature<TFeatureValue> feature, int k, int? numRandomProgramsToInclude, ProgramSamplingStrategy samplingStrategy, IEnumerable<string> additionalInputs = null, CancellationToken cancel = default(CancellationToken), Guid? guid = null)
		{
			return new ProgramCollection<Program, string, ITable<string>, TFeatureValue>(Learner.Learn(constraints, new int?(k), additionalInputs, guid, cancel).ToList<Program>(), null, null, null);
		}

		// Token: 0x06008CBF RID: 36031 RVA: 0x001D9050 File Offset: 0x001D7250
		private static IEnumerable<Program> Learn(IEnumerable<Constraint<string, ITable<string>>> constraints, int? k, IEnumerable<string> additionalInputs, Guid? guid, CancellationToken cancel)
		{
			IReadOnlyList<string> readOnlyList = additionalInputs.ToList<string>();
			if (readOnlyList.Count > 1)
			{
				throw new ArgumentException("Expected at most one input", "additionalInputs");
			}
			string text = ((readOnlyList.Count == 0) ? null : readOnlyList[0]);
			IEnumerable<LearnResult> enumerable = new AutoWitnesses(Learner.GetOptions(constraints)).LearnAuto(text, k, guid, cancel);
			if (k != null)
			{
				enumerable = enumerable.Take(k.Value);
			}
			IEnumerable<LearnResult> enumerable2 = enumerable;
			Func<LearnResult, Program> func;
			if ((func = Learner.<>O.<0>__CreateProgram) == null)
			{
				func = (Learner.<>O.<0>__CreateProgram = new Func<LearnResult, Program>(Learner.CreateProgram));
			}
			return enumerable2.Select(func);
		}

		// Token: 0x06008CC0 RID: 36032 RVA: 0x001D90E0 File Offset: 0x001D72E0
		private static Options GetOptions(IEnumerable<Constraint<string, ITable<string>>> constraints)
		{
			Options options = new Options();
			foreach (Constraint<string, ITable<string>> constraint in constraints)
			{
				IOptionConstraint<Options> optionConstraint = constraint as IOptionConstraint<Options>;
				if (optionConstraint == null)
				{
					throw new NotSupportedException(string.Format("Unsupported constraint: {0}", constraint));
				}
				optionConstraint.SetOptions(options);
			}
			return options;
		}

		// Token: 0x06008CC1 RID: 36033 RVA: 0x001D914C File Offset: 0x001D734C
		private static Program CreateProgram(LearnResult result)
		{
			return result.Switch<Program>((LearnCsvResult csvRes) => new CsvProgram(csvRes.ColumnNames, csvRes.Skip, csvRes.SkipFooter, csvRes.Delimiter, csvRes.FilterEmptyLines, csvRes.CommentStr, csvRes.QuoteChar, csvRes.EscapeChar, csvRes.DoubleQuoteEscape, csvRes.RawColumnNames, csvRes.SkipEmptyAndCommentCount, csvRes.HasEmptyLines, csvRes.HasMultiLineRows, csvRes.NewLineStrings), (LearnFwResult fwRes) => new FwProgram(fwRes.ColumnNames, fwRes.Skip, result.SkipFooter, fwRes.FieldPositions, fwRes.FilterEmptyLines, fwRes.CommentStr, fwRes.RawColumnNames, fwRes.SkipEmptyAndCommentCount, fwRes.HasEmptyLines, fwRes.HasMultiLineRows, fwRes.NewLineStrings), (LearnETextResult etextRes) => new ExtractionTextProgram(etextRes.LearnedProgram));
		}

		// Token: 0x02001245 RID: 4677
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x0400399F RID: 14751
			public static Func<LearnResult, Program> <0>__CreateProgram;
		}
	}
}
