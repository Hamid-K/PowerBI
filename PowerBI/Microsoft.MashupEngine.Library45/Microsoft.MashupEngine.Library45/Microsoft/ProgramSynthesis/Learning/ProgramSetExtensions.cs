using System;
using System.Threading;
using Microsoft.ProgramSynthesis.Specifications;
using Microsoft.ProgramSynthesis.Utils.Interactive;
using Microsoft.ProgramSynthesis.VersionSpace;

namespace Microsoft.ProgramSynthesis.Learning
{
	// Token: 0x020006C1 RID: 1729
	public static class ProgramSetExtensions
	{
		// Token: 0x06002595 RID: 9621 RVA: 0x00068A00 File Offset: 0x00066C00
		public static ProgramSet Filter(this ProgramSet set, Spec spec, SynthesisEngine engine, CancellationToken cancel = default(CancellationToken))
		{
			if (spec is TopSpec)
			{
				return set;
			}
			LearningTask learningTask = new LearningTask(set, spec);
			return engine.Learn(learningTask, cancel);
		}

		// Token: 0x06002596 RID: 9622 RVA: 0x00068A27 File Offset: 0x00066C27
		public static void ClearCaches(this ProgramSet set)
		{
			set.AcceptVisitor<bool>(ProgramSetExtensions.ClearCacheVisitor.Instance);
		}

		// Token: 0x020006C2 RID: 1730
		private class ClearCacheVisitor : ProgramSetVisitor<bool>
		{
			// Token: 0x06002597 RID: 9623 RVA: 0x00068A35 File Offset: 0x00066C35
			private ClearCacheVisitor()
			{
			}

			// Token: 0x06002598 RID: 9624 RVA: 0x00068A3D File Offset: 0x00066C3D
			public override bool VisitDirect(DirectProgramSet set)
			{
				set.ClearTopKCache();
				return true;
			}

			// Token: 0x06002599 RID: 9625 RVA: 0x00068A46 File Offset: 0x00066C46
			public override bool VisitJoin(JoinProgramSet set)
			{
				set.ClearTopKCache();
				set.ParameterSpaces.ForEach(delegate(ProgramSet child)
				{
					child.AcceptVisitor<bool>(this);
				});
				return true;
			}

			// Token: 0x0600259A RID: 9626 RVA: 0x00068A66 File Offset: 0x00066C66
			public override bool VisitUnion(UnionProgramSet set)
			{
				set.ClearTopKCache();
				set.UnionSpaces.ForEach(delegate(ProgramSet child)
				{
					child.AcceptVisitor<bool>(this);
				});
				return true;
			}

			// Token: 0x040011D2 RID: 4562
			public static readonly ProgramSetExtensions.ClearCacheVisitor Instance = new ProgramSetExtensions.ClearCacheVisitor();
		}
	}
}
