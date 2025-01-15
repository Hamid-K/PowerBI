using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.Session;

namespace Microsoft.ProgramSynthesis.Wrangling.SignificantInputs
{
	// Token: 0x02000101 RID: 257
	public class ProgramSamplingStrategy
	{
		// Token: 0x060005EE RID: 1518 RVA: 0x000133D4 File Offset: 0x000115D4
		public ProgramSamplingStrategy(int numTopPrograms, int numRandomPrograms, bool cachedOnly)
		{
			this.NumTopPrograms = numTopPrograms;
			this.NumRandomPrograms = numRandomPrograms;
			this.CachedOnly = cachedOnly;
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x060005EF RID: 1519 RVA: 0x000133F1 File Offset: 0x000115F1
		internal int NumTopPrograms { get; }

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x060005F0 RID: 1520 RVA: 0x000133F9 File Offset: 0x000115F9
		internal int NumRandomPrograms { get; }

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x060005F1 RID: 1521 RVA: 0x00013401 File Offset: 0x00011601
		internal bool CachedOnly { get; }

		// Token: 0x060005F2 RID: 1522 RVA: 0x00013409 File Offset: 0x00011609
		public static ProgramSamplingStrategy TopK(int k, bool cachedOnly = false)
		{
			return new ProgramSamplingStrategy(k, 0, cachedOnly);
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x00013413 File Offset: 0x00011613
		internal static ProgramSamplingStrategy TopKRandomK(int topK, int randomK)
		{
			return new ProgramSamplingStrategy(topK, randomK, false);
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x00013420 File Offset: 0x00011620
		internal Record<IReadOnlyList<TProgram>, IReadOnlyList<TProgram>> SamplePrograms<TSession, TProgram, TInput, TOutput>(LearnProgramRequest<TProgram, TInput, TOutput> request, double confidenceThreshold, TSession session, CancellationToken cancel) where TSession : NonInteractiveSession<TProgram, TInput, TOutput> where TProgram : Program<TInput, TOutput>
		{
			int? num = ((this.NumRandomPrograms != 0) ? new int?(this.NumRandomPrograms) : null);
			Session<TProgram, TInput, TOutput>.PrunedProgramSetWrapper prunedProgramSetWrapper = session.LearnTopKRandomKCached(request, RankingMode.MostLikely, this.NumTopPrograms, num, this.CachedOnly, cancel);
			IReadOnlyList<TProgram> topPrograms = ((prunedProgramSetWrapper != null) ? prunedProgramSetWrapper._topPrograms : null) ?? new List<TProgram>();
			IReadOnlyList<TProgram> readOnlyList = ((prunedProgramSetWrapper != null) ? prunedProgramSetWrapper._sampledPrograms : null) ?? new List<TProgram>();
			topPrograms = topPrograms.Where((TProgram x) => session.ComputeConfidence(topPrograms[0], x) >= confidenceThreshold).ToList<TProgram>();
			return Record.Create<IReadOnlyList<TProgram>, IReadOnlyList<TProgram>>(topPrograms, readOnlyList);
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x000134E8 File Offset: 0x000116E8
		public override string ToString()
		{
			return FormattableString.Invariant(FormattableStringFactory.Create("[Top {0}, Random {1}, {2}", new object[] { this.NumTopPrograms, this.NumRandomPrograms, this.CachedOnly }));
		}

		// Token: 0x04000275 RID: 629
		public static readonly ProgramSamplingStrategy DefaultSamplingStrategy = ProgramSamplingStrategy.TopKRandomK(5, 10);

		// Token: 0x04000276 RID: 630
		public static readonly ProgramSamplingStrategy[] DefaultSamplingStrategies = new ProgramSamplingStrategy[]
		{
			ProgramSamplingStrategy.TopK(1, true),
			ProgramSamplingStrategy.TopK(1, false),
			ProgramSamplingStrategy.TopK(5, false),
			ProgramSamplingStrategy.TopKRandomK(1, 10),
			ProgramSamplingStrategy.TopKRandomK(5, 10)
		};
	}
}
