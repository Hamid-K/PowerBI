using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Transformation.Text.Semantics;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Constraints
{
	// Token: 0x02001DF5 RID: 7669
	public class PrefixOfOutputConstraint : ValueToValueConstraint<IRow, object>
	{
		// Token: 0x17002A9F RID: 10911
		// (get) Token: 0x060100EE RID: 65774 RVA: 0x00372DBD File Offset: 0x00370FBD
		public StringPrefixSet OutputPrefixes
		{
			get
			{
				return base.Output as StringPrefixSet;
			}
		}

		// Token: 0x060100EF RID: 65775 RVA: 0x00372DCC File Offset: 0x00370FCC
		public override bool ConflictsWith(Constraint<IRow, object> other)
		{
			if (other == null)
			{
				return false;
			}
			PrefixOfOutputConstraint prefixOfOutputConstraint = other as PrefixOfOutputConstraint;
			return prefixOfOutputConstraint != null && (EqualityComparer<IRow>.Default.Equals(base.Input, prefixOfOutputConstraint.Input) && !this.OutputPrefixes.Prefix.StartsWith(prefixOfOutputConstraint.OutputPrefixes.Prefix)) && !prefixOfOutputConstraint.OutputPrefixes.Prefix.StartsWith(this.OutputPrefixes.Prefix);
		}

		// Token: 0x060100F0 RID: 65776 RVA: 0x00372E40 File Offset: 0x00371040
		public override bool Valid(Program<IRow, object> program)
		{
			string text = (string)program.Run(base.Input);
			return text != null && this.OutputPrefixes.Contains(text);
		}

		// Token: 0x060100F1 RID: 65777 RVA: 0x00372E70 File Offset: 0x00371070
		public override int GetHashCode()
		{
			return base.GetHashCode() ^ base.GetType().GetHashCode();
		}

		// Token: 0x060100F2 RID: 65778 RVA: 0x00372E84 File Offset: 0x00371084
		public PrefixOfOutputConstraint(IRow input, StringPrefixSet output, bool isSoft = false)
			: base(input, output, isSoft)
		{
		}

		// Token: 0x060100F3 RID: 65779 RVA: 0x00372E8F File Offset: 0x0037108F
		public override string ToString()
		{
			return FormattableString.Invariant(FormattableStringFactory.Create("'{0}' -> {1}", new object[] { base.Input, this.OutputPrefixes }));
		}
	}
}
