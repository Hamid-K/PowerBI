using System;
using Microsoft.ProgramSynthesis.Transformation.Text.Semantics;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Constraints
{
	// Token: 0x02001DDC RID: 7644
	public class AvoidImperialDateTimeFormat : Constraint<IRow, object>, IOptionConstraint<Witnesses.Options>
	{
		// Token: 0x06010043 RID: 65603 RVA: 0x002DD5BA File Offset: 0x002DB7BA
		private AvoidImperialDateTimeFormat()
		{
		}

		// Token: 0x17002A89 RID: 10889
		// (get) Token: 0x06010044 RID: 65604 RVA: 0x00370BC8 File Offset: 0x0036EDC8
		public static AvoidImperialDateTimeFormat Instance { get; } = new AvoidImperialDateTimeFormat();

		// Token: 0x06010045 RID: 65605 RVA: 0x00310122 File Offset: 0x0030E322
		public override bool Equals(Constraint<IRow, object> other)
		{
			return other is AvoidImperialDateTimeFormat;
		}

		// Token: 0x06010046 RID: 65606 RVA: 0x00370BCF File Offset: 0x0036EDCF
		public void SetOptions(Witnesses.Options options)
		{
			options.AvoidImperialDateTimeFormat = true;
		}

		// Token: 0x06010047 RID: 65607 RVA: 0x0000FA11 File Offset: 0x0000DC11
		public override bool ConflictsWith(Constraint<IRow, object> other)
		{
			return false;
		}

		// Token: 0x06010048 RID: 65608 RVA: 0x0000A5FD File Offset: 0x000087FD
		public override bool Valid(Program<IRow, object> program)
		{
			return true;
		}

		// Token: 0x06010049 RID: 65609 RVA: 0x00370BD8 File Offset: 0x0036EDD8
		public override int GetHashCode()
		{
			return 31223;
		}
	}
}
