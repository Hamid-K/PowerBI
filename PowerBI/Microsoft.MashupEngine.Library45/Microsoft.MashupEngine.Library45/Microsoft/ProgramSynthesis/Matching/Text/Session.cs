using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.ProgramSynthesis.Matching.Text.Learning;
using Microsoft.ProgramSynthesis.Matching.Text.Semantics;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;
using Microsoft.ProgramSynthesis.Wrangling.Logging;
using Microsoft.ProgramSynthesis.Wrangling.Session;

namespace Microsoft.ProgramSynthesis.Matching.Text
{
	// Token: 0x020011BC RID: 4540
	public class Session : NonInteractiveSession<Program, string, bool>
	{
		// Token: 0x0600871F RID: 34591 RVA: 0x001C5AC4 File Offset: 0x001C3CC4
		public Session(IJournalStorage journalStorage = null, CultureInfo culture = null, ILogger logger = null)
			: base(Learner.Instance, Loader.Instance, journalStorage, culture, "Matching.Text", logger, true)
		{
			base.Constraints.Changed += delegate(object sender, CollectionEvent<Constraint<string, bool>> change)
			{
				if (change.Action == CollectionAction.PreAdd && change.ChangedItems.OfType<Example<string, bool>>().Any<Example<string, bool>>())
				{
					throw new ArgumentException(FormattableString.Invariant(FormattableStringFactory.Create("Do not add inputs as {0}. Instead add them as {1}", new object[] { "Constraints", "Inputs" })));
				}
			};
		}

		// Token: 0x06008720 RID: 34592 RVA: 0x001C5B14 File Offset: 0x001C3D14
		protected override LearnProgramRequest<Program, string, bool> BuildLearnProgramRequest(bool? useInputsInLearn = null)
		{
			LearnProgramRequest<Program, string, bool> learnProgramRequest = base.BuildLearnProgramRequest(useInputsInLearn);
			if (learnProgramRequest == null)
			{
				return learnProgramRequest;
			}
			IEnumerable<Constraint<string, bool>> enumerable = learnProgramRequest.Constraints;
			enumerable = (enumerable.OfType<AllowedTokens<string, bool>>().Any<AllowedTokens<string, bool>>() ? enumerable : enumerable.AppendItem(new AllowedTokens<string, bool>(DefaultTokens.AllTokens)));
			enumerable = (enumerable.OfType<ClusteringMethod<string, bool>>().Any<ClusteringMethod<string, bool>>() ? enumerable : enumerable.AppendItem(new ClusteringMethod<string, bool>(new ClusteringAlgorithm?(ClusteringAlgorithm.Sampling))));
			return new LearnProgramRequest<Program, string, bool>(learnProgramRequest.Inputs, enumerable.ToImmutableArray<Constraint<string, bool>>());
		}

		// Token: 0x17001724 RID: 5924
		// (get) Token: 0x06008721 RID: 34593 RVA: 0x001C5B94 File Offset: 0x001C3D94
		public Tuple<string, char> FirstUnsupportedCharacterInInputs
		{
			get
			{
				return base.Inputs.FirstValue(delegate(string input)
				{
					if (input == null)
					{
						return Optional<Tuple<string, char>>.Nothing;
					}
					Match match = Session.UnsupportedCharacters.Match(input);
					if (!match.Success)
					{
						return Optional<Tuple<string, char>>.Nothing;
					}
					return Tuple.Create<string, char>(input, match.Value[0]).Some<Tuple<string, char>>();
				}).OrElseDefault<Tuple<string, char>>();
			}
		}

		// Token: 0x06008722 RID: 34594 RVA: 0x001C5BC8 File Offset: 0x001C3DC8
		public IReadOnlyList<PatternInfo> LearnPatterns(ClusteringAlgorithm defaultAlgorithm = ClusteringAlgorithm.Sampling, CancellationToken cancel = default(CancellationToken))
		{
			List<Constraint<string, bool>> list = new List<Constraint<string, bool>>();
			if (!base.Constraints.OfType<ClusteringMethod<string, bool>>().Any<ClusteringMethod<string, bool>>())
			{
				list.Add(new ClusteringMethod<string, bool>(new ClusteringAlgorithm?(defaultAlgorithm)));
			}
			list.Add(new OutlierLimit<string, bool>(0.0));
			List<Constraint<string, bool>> list2 = base.Constraints.Concat(list).ToList<Constraint<string, bool>>();
			IReadOnlyList<PatternInfo> readOnlyList = Learner.Instance.LearnPatterns(base.Inputs, list2, cancel);
			return readOnlyList ?? Session.NoPatterns;
		}

		// Token: 0x06008723 RID: 34595 RVA: 0x001C5C41 File Offset: 0x001C3E41
		public PatternInfo LearnSinglePattern(CancellationToken cancel = default(CancellationToken))
		{
			IReadOnlyList<PatternInfo> readOnlyList = this.LearnPatterns(ClusteringAlgorithm.None, cancel);
			if (readOnlyList == null)
			{
				return null;
			}
			return readOnlyList.Single<PatternInfo>();
		}

		// Token: 0x040037D9 RID: 14297
		private static readonly IReadOnlyList<PatternInfo> NoPatterns = new PatternInfo[0];

		// Token: 0x040037DA RID: 14298
		private static readonly Regex UnsupportedCharacters = new Regex("[\\p{L}\\p{N}\\p{P}-[-.,:?/`~!@#$%^&*_=+\\|0-9a-zA-Z]]", RegexOptions.ExplicitCapture | RegexOptions.Compiled);
	}
}
