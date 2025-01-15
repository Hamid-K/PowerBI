using System;
using Microsoft.ProgramSynthesis.Transformation.Text.Semantics;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Constraints
{
	// Token: 0x02001DE2 RID: 7650
	public class FallbackToLookup : Constraint<IRow, object>, IOptionConstraint<Witnesses.Options>
	{
		// Token: 0x06010073 RID: 65651 RVA: 0x00371020 File Offset: 0x0036F220
		public FallbackToLookup(LookupFallbackMode lookupFallbackMode)
		{
			if (!Enum.IsDefined(typeof(LookupFallbackMode), lookupFallbackMode))
			{
				throw new ArgumentException("Unknown LookupFallbackMode: " + lookupFallbackMode.ToString());
			}
			this.LookupFallbackMode = lookupFallbackMode;
		}

		// Token: 0x17002A91 RID: 10897
		// (get) Token: 0x06010074 RID: 65652 RVA: 0x0037106E File Offset: 0x0036F26E
		public LookupFallbackMode LookupFallbackMode { get; }

		// Token: 0x06010075 RID: 65653 RVA: 0x00371076 File Offset: 0x0036F276
		public void SetOptions(Witnesses.Options options)
		{
			options.LookupFallbackMode = this.LookupFallbackMode;
		}

		// Token: 0x06010076 RID: 65654 RVA: 0x00371084 File Offset: 0x0036F284
		public override bool Equals(Constraint<IRow, object> other)
		{
			return other is FallbackToLookup;
		}

		// Token: 0x06010077 RID: 65655 RVA: 0x0037108F File Offset: 0x0036F28F
		public override bool ConflictsWith(Constraint<IRow, object> other)
		{
			return other is FallbackToLookup && ((FallbackToLookup)other).LookupFallbackMode != this.LookupFallbackMode;
		}

		// Token: 0x06010078 RID: 65656 RVA: 0x0000A5FD File Offset: 0x000087FD
		public override bool Valid(Program<IRow, object> program)
		{
			return true;
		}

		// Token: 0x06010079 RID: 65657 RVA: 0x003710B4 File Offset: 0x0036F2B4
		public override int GetHashCode()
		{
			return 942565249 ^ this.LookupFallbackMode.GetHashCode();
		}
	}
}
