using System;
using System.Linq;
using System.Threading;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Specifications;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.VersionSpace;

namespace Microsoft.ProgramSynthesis.Learning.Strategies
{
	// Token: 0x020006FE RID: 1790
	public class EnumerativeSynthesis : SynthesisStrategy<Spec>
	{
		// Token: 0x060026EC RID: 9964 RVA: 0x0006DFCB File Offset: 0x0006C1CB
		public EnumerativeSynthesis(EnumerativeSynthesis.Config config = null)
			: base(Array.Empty<StrategyAttribute>())
		{
			this.Configuration = config ?? new EnumerativeSynthesis.Config();
		}

		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x060026ED RID: 9965 RVA: 0x0006DFE8 File Offset: 0x0006C1E8
		// (set) Token: 0x060026EE RID: 9966 RVA: 0x0006DFF0 File Offset: 0x0006C1F0
		public EnumerativeSynthesis.Config Configuration { get; set; }

		// Token: 0x060026EF RID: 9967 RVA: 0x0006DFFC File Offset: 0x0006C1FC
		public override Optional<ProgramSet> Learn(SynthesisEngine engine, LearningTask<Spec> task, CancellationToken cancel)
		{
			return task.Symbol.TryGetAllPrograms(true, false).SelectMany(delegate(ProgramSet s)
			{
				if (s.RealizedPrograms.Skip(this.Configuration.MaximalSize).Any<ProgramNode>())
				{
					return Optional<ProgramSet>.Nothing;
				}
				return s.Filter(task.Spec).SomeIfNotNull<ProgramSet>();
			});
		}

		// Token: 0x020006FF RID: 1791
		public sealed class Config
		{
			// Token: 0x170006D7 RID: 1751
			// (get) Token: 0x060026F0 RID: 9968 RVA: 0x0006E040 File Offset: 0x0006C240
			// (set) Token: 0x060026F1 RID: 9969 RVA: 0x0006E048 File Offset: 0x0006C248
			public int MaximalSize { get; set; } = 30;
		}
	}
}
