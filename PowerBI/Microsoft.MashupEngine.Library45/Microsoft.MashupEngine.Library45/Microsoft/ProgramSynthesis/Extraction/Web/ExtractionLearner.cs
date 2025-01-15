using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Extraction.Web.Learning;
using Microsoft.ProgramSynthesis.Extraction.Web.Semantics;
using Microsoft.ProgramSynthesis.Features;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Extraction.Web
{
	// Token: 0x02000FAD RID: 4013
	public class ExtractionLearner : ExtractionLearner<SequenceProgram, RegionProgram, WebRegion>
	{
		// Token: 0x170013BF RID: 5055
		// (get) Token: 0x06006EE4 RID: 28388 RVA: 0x0016AF00 File Offset: 0x00169100
		public static ExtractionLearner Instance { get; } = new ExtractionLearner();

		// Token: 0x170013C0 RID: 5056
		// (get) Token: 0x06006EE5 RID: 28389 RVA: 0x0016AF07 File Offset: 0x00169107
		public override Feature<double> ScoreFeature { get; } = new Ranking(Language.Grammar);

		// Token: 0x06006EE6 RID: 28390 RVA: 0x0016AF0F File Offset: 0x0016910F
		private ExtractionLearner()
		{
		}

		// Token: 0x170013C1 RID: 5057
		// (get) Token: 0x06006EE7 RID: 28391 RVA: 0x0016AF27 File Offset: 0x00169127
		public override ProgramLearner<SequenceProgram, IEnumerable<WebRegion>, IEnumerable<IEnumerable<WebRegion>>> Sequence
		{
			get
			{
				return SequenceLearner.Instance;
			}
		}

		// Token: 0x170013C2 RID: 5058
		// (get) Token: 0x06006EE8 RID: 28392 RVA: 0x0016AF2E File Offset: 0x0016912E
		public override ProgramLearner<RegionProgram, IEnumerable<WebRegion>, IEnumerable<WebRegion>> Region
		{
			get
			{
				return RegionLearner.Instance;
			}
		}

		// Token: 0x170013C3 RID: 5059
		// (get) Token: 0x06006EE9 RID: 28393 RVA: 0x0016AF35 File Offset: 0x00169135
		public override Grammar Grammar
		{
			get
			{
				return Language.Grammar;
			}
		}

		// Token: 0x170013C4 RID: 5060
		// (get) Token: 0x06006EEA RID: 28394 RVA: 0x0016AF3C File Offset: 0x0016913C
		public override ExtractionLoader<SequenceProgram, RegionProgram, WebRegion> Loader
		{
			get
			{
				return Microsoft.ProgramSynthesis.Extraction.Web.Loader.Instance;
			}
		}

		// Token: 0x06006EEB RID: 28395 RVA: 0x0016AF43 File Offset: 0x00169143
		public override IEnumerable<Constraint<IEnumerable<WebRegion>, IEnumerable<IEnumerable<WebRegion>>>> BuildNegativeSequenceConstraints(WebRegion reference, IEnumerable<WebRegion> negativeRegions)
		{
			return new NegativeMemberSubset<WebRegion, WebRegion>[]
			{
				new NegativeMemberSubset<WebRegion, WebRegion>(reference, negativeRegions, false)
			};
		}

		// Token: 0x06006EEC RID: 28396 RVA: 0x0016AF56 File Offset: 0x00169156
		public override Constraint<IEnumerable<WebRegion>, IEnumerable<IEnumerable<WebRegion>>> GetNextSequenceConstraint(WebRegion input, IReadOnlyList<WebRegion> output, IEnumerable<WebRegion> existingOutput)
		{
			return new MemberSubset<WebRegion, WebRegion>(input, Seq.Of<WebRegion>(new WebRegion[] { output.Except(existingOutput).First<WebRegion>() }), false);
		}

		// Token: 0x06006EED RID: 28397 RVA: 0x0016AF7C File Offset: 0x0016917C
		public override IEnumerable<Constraint<IEnumerable<WebRegion>, IEnumerable<IEnumerable<WebRegion>>>> GetImplicitPositiveSequenceConstraints(WebRegion input, IEnumerable<WebRegion> implicitOutputPrefix, IEnumerable<Constraint<IEnumerable<WebRegion>, IEnumerable<IEnumerable<WebRegion>>>> learningConstraints)
		{
			IEnumerable<WebRegion> enumerable = (from s in learningConstraints.OfType<MemberSubset<WebRegion, WebRegion>>()
				where object.Equals(s.InputMember, input)
				select s).SelectMany((MemberSubset<WebRegion, WebRegion> s) => s.OutputMember);
			return new MemberSubset<WebRegion, WebRegion>[]
			{
				new MemberSubset<WebRegion, WebRegion>(input, implicitOutputPrefix.Except(enumerable).ToList<WebRegion>(), true)
			};
		}
	}
}
