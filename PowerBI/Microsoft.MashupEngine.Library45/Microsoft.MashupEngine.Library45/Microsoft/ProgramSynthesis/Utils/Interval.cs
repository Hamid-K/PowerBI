using System;
using System.Runtime.CompilerServices;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x02000499 RID: 1177
	public struct Interval : IEquatable<Interval>
	{
		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x06001A74 RID: 6772 RVA: 0x0004FC35 File Offset: 0x0004DE35
		public readonly int Start { get; }

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x06001A75 RID: 6773 RVA: 0x0004FC3D File Offset: 0x0004DE3D
		public readonly int Length { get; }

		// Token: 0x06001A76 RID: 6774 RVA: 0x0004FC48 File Offset: 0x0004DE48
		public Interval(int start, int length)
		{
			if (start < 0 || Convert.ToInt64(start) + Convert.ToInt64(length) > 2147483647L)
			{
				throw new ArgumentException(FormattableString.Invariant(FormattableStringFactory.Create("Arguments to constructor of {0} must both be non-negative and their sum must not overflow.", new object[] { typeof(Interval) })));
			}
			this.Start = start;
			this.Length = length;
		}

		// Token: 0x06001A77 RID: 6775 RVA: 0x0004FCA4 File Offset: 0x0004DEA4
		public bool Equals(Interval other)
		{
			return other.Start == this.Start && other.Length == this.Length;
		}

		// Token: 0x06001A78 RID: 6776 RVA: 0x0004FCC6 File Offset: 0x0004DEC6
		public override bool Equals(object obj)
		{
			return obj.GetType() == base.GetType() && this.Equals((Interval)obj);
		}

		// Token: 0x06001A79 RID: 6777 RVA: 0x0004FCF4 File Offset: 0x0004DEF4
		public override int GetHashCode()
		{
			return (this.Start.GetHashCode() * 33637) ^ this.Length.GetHashCode();
		}

		// Token: 0x06001A7A RID: 6778 RVA: 0x0004FD24 File Offset: 0x0004DF24
		public override string ToString()
		{
			return FormattableString.Invariant(FormattableStringFactory.Create("{{{0}:{1}}}", new object[] { this.Start, this.Length }));
		}
	}
}
