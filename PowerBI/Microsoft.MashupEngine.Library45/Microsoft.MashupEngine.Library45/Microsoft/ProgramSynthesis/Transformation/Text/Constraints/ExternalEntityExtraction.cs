using System;
using System.Collections.Immutable;
using Microsoft.ProgramSynthesis.Transformation.Text.Semantics;
using Microsoft.ProgramSynthesis.Transformation.Text.Semantics.CustomExtraction;
using Microsoft.ProgramSynthesis.Transformation.Text.Semantics.ExtractByEntity;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Constraints
{
	// Token: 0x02001DE0 RID: 7648
	public class ExternalEntityExtraction : Constraint<IRow, object>, IOptionConstraint<Witnesses.Options>
	{
		// Token: 0x17002A8C RID: 10892
		// (get) Token: 0x0601005F RID: 65631 RVA: 0x00370E27 File Offset: 0x0036F027
		public TokenizerCollectionToExtractor Extractor { get; }

		// Token: 0x17002A8D RID: 10893
		// (get) Token: 0x06010060 RID: 65632 RVA: 0x00370E2F File Offset: 0x0036F02F
		public int K { get; }

		// Token: 0x17002A8E RID: 10894
		// (get) Token: 0x06010061 RID: 65633 RVA: 0x00370E37 File Offset: 0x0036F037
		public string ColumnName { get; }

		// Token: 0x17002A8F RID: 10895
		// (get) Token: 0x06010062 RID: 65634 RVA: 0x00370E3F File Offset: 0x0036F03F
		public EntityType EntityType
		{
			get
			{
				return this.Extractor.EntityType;
			}
		}

		// Token: 0x06010063 RID: 65635 RVA: 0x00370E4C File Offset: 0x0036F04C
		public ExternalEntityExtraction(TokenizerCollectionToExtractor extractor, int k, string columnName)
		{
			this.Extractor = extractor;
			this.K = k;
			this.ColumnName = columnName;
		}

		// Token: 0x06010064 RID: 65636 RVA: 0x00370E6C File Offset: 0x0036F06C
		public bool Equals(ExternalEntityExtraction other)
		{
			return other == this || (other != null && (this.Extractor.Equals(other.Extractor) && this.K == other.K) && this.ColumnName == other.ColumnName);
		}

		// Token: 0x06010065 RID: 65637 RVA: 0x00370EB8 File Offset: 0x0036F0B8
		public override bool Equals(Constraint<IRow, object> other)
		{
			return other == this || (other != null && !(other.GetType() != base.GetType()) && this.Equals((ExternalEntityExtraction)other));
		}

		// Token: 0x06010066 RID: 65638 RVA: 0x00370EE6 File Offset: 0x0036F0E6
		public override bool ConflictsWith(Constraint<IRow, object> other)
		{
			return this.Equals(other);
		}

		// Token: 0x06010067 RID: 65639 RVA: 0x0000A5FD File Offset: 0x000087FD
		public override bool Valid(Program<IRow, object> program)
		{
			return true;
		}

		// Token: 0x06010068 RID: 65640 RVA: 0x00370EEF File Offset: 0x0036F0EF
		public void SetOptions(Witnesses.Options options)
		{
			options.ExternalExtractors = new CustomExtractor[] { this.Extractor }.ToImmutableHashSet<CustomExtractor>();
		}

		// Token: 0x06010069 RID: 65641 RVA: 0x00370F0C File Offset: 0x0036F10C
		public override int GetHashCode()
		{
			return (((this.Extractor.GetHashCode() * 5147) ^ this.K.GetHashCode()) * 2389) ^ this.ColumnName.GetHashCode();
		}
	}
}
