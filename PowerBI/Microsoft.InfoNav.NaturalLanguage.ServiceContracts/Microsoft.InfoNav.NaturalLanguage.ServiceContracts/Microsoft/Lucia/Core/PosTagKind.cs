using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Microsoft.Lucia.Core
{
	// Token: 0x020000B8 RID: 184
	[ImmutableObject(true)]
	public struct PosTagKind
	{
		// Token: 0x060003AF RID: 943 RVA: 0x00006E6A File Offset: 0x0000506A
		public PosTagKind(long val)
		{
			this._value = val;
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060003B0 RID: 944 RVA: 0x00006E73 File Offset: 0x00005073
		public long Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x00006E7B File Offset: 0x0000507B
		public static PosTagKind operator |(PosTagKind posTagKind1, PosTagKind posTagKind2)
		{
			return new PosTagKind(posTagKind1.Value | posTagKind2.Value);
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x00006E91 File Offset: 0x00005091
		public static PosTagKind operator &(PosTagKind posTagKind1, PosTagKind posTagKind2)
		{
			return new PosTagKind(posTagKind1.Value & posTagKind2.Value);
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x00006EA7 File Offset: 0x000050A7
		public static PosTagKind operator ~(PosTagKind posTagKind)
		{
			return new PosTagKind(~posTagKind.Value);
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x00006EB6 File Offset: 0x000050B6
		public bool HasFeature(PosTagKind featureMask, PosTagKind featureValue)
		{
			return (this._value & featureMask._value) == featureValue._value;
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x00006ECD File Offset: 0x000050CD
		public bool Equals(PosTagKind other)
		{
			return this._value == other.Value;
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x00006EDE File Offset: 0x000050DE
		public bool Overlaps(PosTagKind other)
		{
			return (this._value & other.Value) != 0L;
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x00006EF2 File Offset: 0x000050F2
		public override string ToString()
		{
			return FormattableString.Invariant(FormattableStringFactory.Create("0x{0:x}", new object[] { this._value }));
		}

		// Token: 0x040003E2 RID: 994
		public static readonly PosTagKind None = new PosTagKind(0L);

		// Token: 0x040003E3 RID: 995
		private readonly long _value;
	}
}
