using System;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x0200055F RID: 1375
	internal class ValueCondition : IEquatable<ValueCondition>
	{
		// Token: 0x06004323 RID: 17187 RVA: 0x000E6E85 File Offset: 0x000E5085
		private ValueCondition(string description, bool isSentinel)
		{
			this.Description = description;
			this.IsSentinel = isSentinel;
		}

		// Token: 0x06004324 RID: 17188 RVA: 0x000E6E9B File Offset: 0x000E509B
		internal ValueCondition(string description)
			: this(description, false)
		{
		}

		// Token: 0x17000D53 RID: 3411
		// (get) Token: 0x06004325 RID: 17189 RVA: 0x000E6EA5 File Offset: 0x000E50A5
		internal bool IsNotNullCondition
		{
			get
			{
				return this == ValueCondition.IsNotNull;
			}
		}

		// Token: 0x06004326 RID: 17190 RVA: 0x000E6EAF File Offset: 0x000E50AF
		public bool Equals(ValueCondition other)
		{
			return other.IsSentinel == this.IsSentinel && other.Description == this.Description;
		}

		// Token: 0x06004327 RID: 17191 RVA: 0x000E6ED2 File Offset: 0x000E50D2
		public override int GetHashCode()
		{
			return this.Description.GetHashCode();
		}

		// Token: 0x06004328 RID: 17192 RVA: 0x000E6EDF File Offset: 0x000E50DF
		public override string ToString()
		{
			return this.Description;
		}

		// Token: 0x040017F4 RID: 6132
		internal readonly string Description;

		// Token: 0x040017F5 RID: 6133
		internal readonly bool IsSentinel;

		// Token: 0x040017F6 RID: 6134
		internal const string IsNullDescription = "NULL";

		// Token: 0x040017F7 RID: 6135
		internal const string IsNotNullDescription = "NOT NULL";

		// Token: 0x040017F8 RID: 6136
		internal const string IsOtherDescription = "OTHER";

		// Token: 0x040017F9 RID: 6137
		internal static readonly ValueCondition IsNull = new ValueCondition("NULL", true);

		// Token: 0x040017FA RID: 6138
		internal static readonly ValueCondition IsNotNull = new ValueCondition("NOT NULL", true);

		// Token: 0x040017FB RID: 6139
		internal static readonly ValueCondition IsOther = new ValueCondition("OTHER", true);
	}
}
