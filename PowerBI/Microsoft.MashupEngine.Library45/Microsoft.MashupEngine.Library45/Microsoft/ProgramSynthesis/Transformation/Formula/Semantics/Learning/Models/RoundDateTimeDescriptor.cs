using System;
using Microsoft.ProgramSynthesis.DslLibrary.Numbers;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Models
{
	// Token: 0x020016DD RID: 5853
	public class RoundDateTimeDescriptor : IEquatable<RoundDateTimeDescriptor>
	{
		// Token: 0x1700213A RID: 8506
		// (get) Token: 0x0600C332 RID: 49970 RVA: 0x002A0866 File Offset: 0x0029EA66
		// (set) Token: 0x0600C333 RID: 49971 RVA: 0x002A086E File Offset: 0x0029EA6E
		public RoundDatePeriodCeiling Ceiling { get; set; } = RoundDatePeriodCeiling.Ceiling;

		// Token: 0x1700213B RID: 8507
		// (get) Token: 0x0600C334 RID: 49972 RVA: 0x002A0877 File Offset: 0x0029EA77
		// (set) Token: 0x0600C335 RID: 49973 RVA: 0x002A087F File Offset: 0x0029EA7F
		public RoundingMode Mode { get; set; }

		// Token: 0x1700213C RID: 8508
		// (get) Token: 0x0600C336 RID: 49974 RVA: 0x002A0888 File Offset: 0x0029EA88
		// (set) Token: 0x0600C337 RID: 49975 RVA: 0x002A0890 File Offset: 0x0029EA90
		public RoundDateTimePeriod Period { get; set; }

		// Token: 0x0600C338 RID: 49976 RVA: 0x002A0899 File Offset: 0x0029EA99
		public bool Equals(RoundDateTimeDescriptor other)
		{
			return other != null && this.ToString() == other.ToString();
		}

		// Token: 0x0600C339 RID: 49977 RVA: 0x002A08B7 File Offset: 0x0029EAB7
		public override bool Equals(object other)
		{
			return this.Equals(other as RoundDateTimeDescriptor);
		}

		// Token: 0x0600C33A RID: 49978 RVA: 0x00218E7F File Offset: 0x0021707F
		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}

		// Token: 0x0600C33B RID: 49979 RVA: 0x002A08C5 File Offset: 0x0029EAC5
		public static bool operator ==(RoundDateTimeDescriptor left, RoundDateTimeDescriptor right)
		{
			return (left == null && right == null) || (left != null && left.Equals(right));
		}

		// Token: 0x0600C33C RID: 49980 RVA: 0x002A08DB File Offset: 0x0029EADB
		public static bool operator !=(RoundDateTimeDescriptor left, RoundDateTimeDescriptor right)
		{
			return !(left == right);
		}

		// Token: 0x0600C33D RID: 49981 RVA: 0x002A08E8 File Offset: 0x0029EAE8
		public override string ToString()
		{
			string text;
			if ((text = this._toString) == null)
			{
				text = (this._toString = ((this.Ceiling == RoundDatePeriodCeiling.LastDay) ? string.Format("{{{0},{1},{2}}}", this.Mode, this.Period, this.Ceiling) : string.Format("{{{0},{1}}}", this.Mode, this.Period)));
			}
			return text;
		}

		// Token: 0x04004BF4 RID: 19444
		private string _toString;
	}
}
