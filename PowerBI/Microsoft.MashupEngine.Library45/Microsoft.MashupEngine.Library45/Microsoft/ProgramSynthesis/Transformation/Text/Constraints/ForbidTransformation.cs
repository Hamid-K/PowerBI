using System;
using System.Linq;
using Microsoft.ProgramSynthesis.Features;
using Microsoft.ProgramSynthesis.Transformation.Text.Description;
using Microsoft.ProgramSynthesis.Transformation.Text.Semantics;
using Microsoft.ProgramSynthesis.Transformation.Text.Semantics.Ranking;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Constraints
{
	// Token: 0x02001DE5 RID: 7653
	public class ForbidTransformation : Constraint<IRow, object>, IOptionConstraint<Witnesses.Options>
	{
		// Token: 0x06010085 RID: 65669 RVA: 0x00371149 File Offset: 0x0036F349
		public ForbidTransformation(TransformationKind forbiddenTransformations)
		{
			this.ForbiddenTransformations = forbiddenTransformations;
		}

		// Token: 0x17002A93 RID: 10899
		// (get) Token: 0x06010086 RID: 65670 RVA: 0x00371158 File Offset: 0x0036F358
		public TransformationKind ForbiddenTransformations { get; }

		// Token: 0x06010087 RID: 65671 RVA: 0x00371160 File Offset: 0x0036F360
		public override bool Equals(Constraint<IRow, object> other)
		{
			ForbidTransformation forbidTransformation = other as ForbidTransformation;
			return forbidTransformation != null && forbidTransformation.ForbiddenTransformations == this.ForbiddenTransformations;
		}

		// Token: 0x06010088 RID: 65672 RVA: 0x0000FA11 File Offset: 0x0000DC11
		public override bool ConflictsWith(Constraint<IRow, object> other)
		{
			return false;
		}

		// Token: 0x06010089 RID: 65673 RVA: 0x00371190 File Offset: 0x0036F390
		public override bool Valid(Program<IRow, object> program)
		{
			Program program2 = program as Program;
			if (program2 == null)
			{
				return false;
			}
			TransformationKind transformationKind = program2.AllTransformations.Select((TransformationDescription t) => t.Kind).Aggregate((TransformationKind a, TransformationKind b) => a | b);
			if (program.ProgramNode.GetFeatureValue<double>(ForbidTransformation.HasConcatenations, null) != 0.0)
			{
				transformationKind |= TransformationKind.Concat;
			}
			return (this.ForbiddenTransformations & transformationKind) == TransformationKind.Unknown;
		}

		// Token: 0x0601008A RID: 65674 RVA: 0x00371229 File Offset: 0x0036F429
		public void SetOptions(Witnesses.Options options)
		{
			options.AllowedTransformations &= ~this.ForbiddenTransformations;
			if ((this.ForbiddenTransformations & TransformationKind.Lookup) != TransformationKind.Unknown)
			{
				options.LookupFallbackMode = LookupFallbackMode.Never;
			}
		}

		// Token: 0x0601008B RID: 65675 RVA: 0x00371254 File Offset: 0x0036F454
		public override int GetHashCode()
		{
			return this.ForbiddenTransformations.GetHashCode() ^ 227233;
		}

		// Token: 0x0601008C RID: 65676 RVA: 0x0037127B File Offset: 0x0036F47B
		public override string ToString()
		{
			return string.Format("ForbidTransformation({0})", this.ForbiddenTransformations);
		}

		// Token: 0x0400607D RID: 24701
		private static readonly Feature<double> HasConcatenations = new HasConcatenations(Language.Grammar);
	}
}
