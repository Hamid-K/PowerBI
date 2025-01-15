using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Wrangling.Session
{
	// Token: 0x02000114 RID: 276
	public static class SessionUtils
	{
		// Token: 0x0600063D RID: 1597 RVA: 0x000142BC File Offset: 0x000124BC
		public static async Task<LearnResult<TProgram, TInput, TOutput>> ComputeLearnResultAsync<TProgram, TInput, TOutput>(this Session<TProgram, TInput, TOutput> session, int numPrograms = 1, RankingMode rankingMode = null, CancellationToken cancel = default(CancellationToken), Guid? guid = null) where TProgram : Program<TInput, TOutput>
		{
			LearnResult<TProgram, TInput, TOutput> learnResult2;
			if (session.CanLearn)
			{
				List<TProgram> list = (await session.LearnTopKAsync(numPrograms, rankingMode, cancel, guid)).ToList<TProgram>();
				LearnResult<TProgram, TInput, TOutput> learnResult = default(LearnResult<TProgram, TInput, TOutput>);
				learnResult.Kind = (list.Any<TProgram>() ? LearnResultKind.Success : LearnResultKind.NoPrograms);
				learnResult.LearnedPrograms = list;
				learnResult.ProgramOutputs = list.ToDictionary((TProgram p) => p, (TProgram p) => session.Inputs.Select((TInput input) => new Example<TInput, TOutput>(input, p.Run(input), false)).ToList<Example<TInput, TOutput>>());
				learnResult2 = learnResult;
			}
			else
			{
				LearnResult<TProgram, TInput, TOutput> learnResult = (learnResult2 = new LearnResult<TProgram, TInput, TOutput>
				{
					Kind = LearnResultKind.HasConficts,
					Conflicts = session.Conflicts.ToList<Conflict>()
				});
			}
			return learnResult2;
		}
	}
}
