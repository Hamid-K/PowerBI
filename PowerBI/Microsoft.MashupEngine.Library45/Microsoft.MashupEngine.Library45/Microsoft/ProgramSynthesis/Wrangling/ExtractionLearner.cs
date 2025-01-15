using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.Features;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Wrangling
{
	// Token: 0x020000A1 RID: 161
	public abstract class ExtractionLearner<TSequenceProgram, TRegionProgram, TRegion> where TSequenceProgram : SequenceExtractionProgram<TSequenceProgram, TRegion> where TRegionProgram : RegionExtractionProgram<TRegionProgram, TRegion> where TRegion : IRegion<TRegion>
	{
		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060003C7 RID: 967
		public abstract ProgramLearner<TSequenceProgram, IEnumerable<TRegion>, IEnumerable<IEnumerable<TRegion>>> Sequence { get; }

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060003C8 RID: 968
		public abstract ProgramLearner<TRegionProgram, IEnumerable<TRegion>, IEnumerable<TRegion>> Region { get; }

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060003C9 RID: 969
		public abstract Grammar Grammar { get; }

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060003CA RID: 970
		public abstract Feature<double> ScoreFeature { get; }

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060003CB RID: 971
		public abstract ExtractionLoader<TSequenceProgram, TRegionProgram, TRegion> Loader { get; }

		// Token: 0x060003CC RID: 972
		public abstract IEnumerable<Constraint<IEnumerable<TRegion>, IEnumerable<IEnumerable<TRegion>>>> BuildNegativeSequenceConstraints(TRegion reference, IEnumerable<TRegion> negativeRegions);

		// Token: 0x060003CD RID: 973
		public abstract Constraint<IEnumerable<TRegion>, IEnumerable<IEnumerable<TRegion>>> GetNextSequenceConstraint(TRegion input, IReadOnlyList<TRegion> output, IEnumerable<TRegion> existingOutput);

		// Token: 0x060003CE RID: 974
		public abstract IEnumerable<Constraint<IEnumerable<TRegion>, IEnumerable<IEnumerable<TRegion>>>> GetImplicitPositiveSequenceConstraints(TRegion input, IEnumerable<TRegion> implicitOutputPrefix, IEnumerable<Constraint<IEnumerable<TRegion>, IEnumerable<IEnumerable<TRegion>>>> learningConstraints);
	}
}
