using System;
using Microsoft.ProgramSynthesis.Transformation.Text.Description;
using Microsoft.ProgramSynthesis.Transformation.Text.Semantics;
using Microsoft.ProgramSynthesis.Transformation.Text.Semantics.CustomExtraction;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Constraints
{
	// Token: 0x02001DE1 RID: 7649
	public class ExternalExtractor : Constraint<IRow, object>, IOptionConstraint<Witnesses.Options>
	{
		// Token: 0x17002A90 RID: 10896
		// (get) Token: 0x0601006A RID: 65642 RVA: 0x00370F4B File Offset: 0x0036F14B
		public CustomExtractor Extractor { get; }

		// Token: 0x0601006B RID: 65643 RVA: 0x00370F53 File Offset: 0x0036F153
		public ExternalExtractor(CustomExtractor extractor)
		{
			this.Extractor = extractor;
		}

		// Token: 0x0601006C RID: 65644 RVA: 0x00370F62 File Offset: 0x0036F162
		public void SetOptions(Witnesses.Options options)
		{
			options.ExternalExtractors = options.ExternalExtractors.Add(this.Extractor);
			options.ForbidAllTransformations();
			options.AllowTransformations(new TransformationKind[] { TransformationKind.Substring });
		}

		// Token: 0x0601006D RID: 65645 RVA: 0x00370F91 File Offset: 0x0036F191
		public bool Equals(ExternalExtractor other)
		{
			return other != null && (other == this || this.Extractor.Equals(other.Extractor));
		}

		// Token: 0x0601006E RID: 65646 RVA: 0x00370FAF File Offset: 0x0036F1AF
		public override bool Equals(Constraint<IRow, object> other)
		{
			return other == this || (other != null && !(other.GetType() != base.GetType()) && this.Equals((ExternalExtractor)other));
		}

		// Token: 0x0601006F RID: 65647 RVA: 0x00370FDD File Offset: 0x0036F1DD
		public override bool Equals(object other)
		{
			return other != null && (other == this || (!(other.GetType() != base.GetType()) && this.Equals((ExternalExtractor)other)));
		}

		// Token: 0x06010070 RID: 65648 RVA: 0x0000FA11 File Offset: 0x0000DC11
		public override bool ConflictsWith(Constraint<IRow, object> other)
		{
			return false;
		}

		// Token: 0x06010071 RID: 65649 RVA: 0x0000A5FD File Offset: 0x000087FD
		public override bool Valid(Program<IRow, object> program)
		{
			return true;
		}

		// Token: 0x06010072 RID: 65650 RVA: 0x0037100B File Offset: 0x0036F20B
		public override int GetHashCode()
		{
			return this.Extractor.GetHashCode() * 16633;
		}
	}
}
