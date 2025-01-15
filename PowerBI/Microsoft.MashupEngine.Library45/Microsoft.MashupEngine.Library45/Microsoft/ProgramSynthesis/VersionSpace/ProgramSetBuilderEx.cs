using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Features;
using Microsoft.ProgramSynthesis.Learning;
using Microsoft.ProgramSynthesis.Learning.Logging;

namespace Microsoft.ProgramSynthesis.VersionSpace
{
	// Token: 0x02000291 RID: 657
	public static class ProgramSetBuilderEx
	{
		// Token: 0x06000E4B RID: 3659 RVA: 0x00029BB9 File Offset: 0x00027DB9
		public static ProgramSetBuilder<T> ToProgramSet<T>(this T builder) where T : IProgramNodeBuilder
		{
			return ProgramSetBuilder.List<T>(new T[] { builder });
		}

		// Token: 0x06000E4C RID: 3660 RVA: 0x00029BCE File Offset: 0x00027DCE
		public static ProgramSetBuilder<T> NormalizedUnion<T>(this IEnumerable<ProgramSetBuilder<T>> builders) where T : IProgramNodeBuilder
		{
			return ProgramSetBuilder<T>.CreateUnsafe(builders.Select(delegate(ProgramSetBuilder<T> b)
			{
				if (b == null)
				{
					return null;
				}
				return b.Set;
			}).NormalizedUnion());
		}

		// Token: 0x06000E4D RID: 3661 RVA: 0x00029BFF File Offset: 0x00027DFF
		public static ProgramSetBuilder<T> Intersect<T>(this ProgramSetBuilder<T> set1, ProgramSetBuilder<T> set2) where T : IProgramNodeBuilder
		{
			return ProgramSetBuilder.Unsafe<T>(set1.Set.Intersect((set2 != null) ? set2.Set : null));
		}

		// Token: 0x06000E4E RID: 3662 RVA: 0x00029C1D File Offset: 0x00027E1D
		public static ProgramSetBuilder<T> Prune<T>(this ProgramSetBuilder<T> set, LearningTask task, Random random, LogListener logListener) where T : IProgramNodeBuilder
		{
			return ProgramSetBuilder.Unsafe<T>(set.Set.Prune(task.PruningRequest, task.FeatureCalculationContext, random, logListener));
		}

		// Token: 0x06000E4F RID: 3663 RVA: 0x00029C3D File Offset: 0x00027E3D
		public static ProgramSetBuilder<T> Prune<T>(this ProgramSetBuilder<T> set, PruningRequest pruningRequest, FeatureCalculationContext fcc, Random random, LogListener logListener) where T : IProgramNodeBuilder
		{
			return ProgramSetBuilder.Unsafe<T>(set.Set.Prune(pruningRequest, fcc, random, logListener));
		}
	}
}
