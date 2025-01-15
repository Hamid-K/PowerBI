using System;
using System.Runtime.CompilerServices;

namespace Microsoft.ProgramSynthesis.Compound.Split.Learning
{
	// Token: 0x0200099E RID: 2462
	public class FwColumnFormat : IEquatable<FwColumnFormat>
	{
		// Token: 0x06003B24 RID: 15140 RVA: 0x000B67F7 File Offset: 0x000B49F7
		public FwColumnFormat(int start, int? end, string name = null, string type = null, string description = null)
		{
			this.Start = start;
			this.End = end;
			this.Name = name;
			this.Type = type;
			this.Description = description;
		}

		// Token: 0x17000A89 RID: 2697
		// (get) Token: 0x06003B25 RID: 15141 RVA: 0x000B6824 File Offset: 0x000B4A24
		public int Start { get; }

		// Token: 0x17000A8A RID: 2698
		// (get) Token: 0x06003B26 RID: 15142 RVA: 0x000B682C File Offset: 0x000B4A2C
		public int? End { get; }

		// Token: 0x17000A8B RID: 2699
		// (get) Token: 0x06003B27 RID: 15143 RVA: 0x000B6834 File Offset: 0x000B4A34
		public string Name { get; }

		// Token: 0x17000A8C RID: 2700
		// (get) Token: 0x06003B28 RID: 15144 RVA: 0x000B683C File Offset: 0x000B4A3C
		public string Type { get; }

		// Token: 0x17000A8D RID: 2701
		// (get) Token: 0x06003B29 RID: 15145 RVA: 0x000B6844 File Offset: 0x000B4A44
		public string Description { get; }

		// Token: 0x06003B2A RID: 15146 RVA: 0x000B684C File Offset: 0x000B4A4C
		internal FwColumnFormat.ColumnPosition RelativePosition(FwColumnFormat other)
		{
			int? num2;
			if (this.Start == other.Start)
			{
				int? num = this.End;
				num2 = other.End;
				if ((num.GetValueOrDefault() == num2.GetValueOrDefault()) & (num != null == (num2 != null)))
				{
					return FwColumnFormat.ColumnPosition.Equal;
				}
			}
			if (this.Start <= other.Start)
			{
				num2 = this.End;
				int? num = other.End;
				if ((num2.GetValueOrDefault() >= num.GetValueOrDefault()) & ((num2 != null) & (num != null)))
				{
					return FwColumnFormat.ColumnPosition.Contain;
				}
			}
			if (this.Start >= other.Start)
			{
				int? num = this.End;
				num2 = other.End;
				if ((num.GetValueOrDefault() <= num2.GetValueOrDefault()) & ((num != null) & (num2 != null)))
				{
					return FwColumnFormat.ColumnPosition.Within;
				}
			}
			num2 = this.End;
			int start = other.Start;
			if ((num2.GetValueOrDefault() < start) & (num2 != null))
			{
				return FwColumnFormat.ColumnPosition.Before;
			}
			int start2 = this.Start;
			num2 = other.End;
			if ((start2 > num2.GetValueOrDefault()) & (num2 != null))
			{
				return FwColumnFormat.ColumnPosition.After;
			}
			return FwColumnFormat.ColumnPosition.Overlap;
		}

		// Token: 0x06003B2B RID: 15147 RVA: 0x000B6968 File Offset: 0x000B4B68
		public bool Equals(FwColumnFormat other)
		{
			if (other == null)
			{
				return false;
			}
			if (this == other)
			{
				return true;
			}
			if (this.Start == other.Start)
			{
				int? end = this.End;
				int? end2 = other.End;
				if (((end.GetValueOrDefault() == end2.GetValueOrDefault()) & (end != null == (end2 != null))) && string.Equals(this.Name, other.Name) && string.Equals(this.Type, other.Type))
				{
					return string.Equals(this.Description, other.Description);
				}
			}
			return false;
		}

		// Token: 0x06003B2C RID: 15148 RVA: 0x000B69F8 File Offset: 0x000B4BF8
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((FwColumnFormat)obj)));
		}

		// Token: 0x06003B2D RID: 15149 RVA: 0x000B6A28 File Offset: 0x000B4C28
		public override int GetHashCode()
		{
			return (((((((this.Start * 307) ^ ((this.End != null) ? this.End.GetHashCode() : 0)) * 307) ^ ((this.Name != null) ? this.Name.GetHashCode() : 0)) * 307) ^ ((this.Type != null) ? this.Type.GetHashCode() : 0)) * 307) ^ ((this.Description != null) ? this.Description.GetHashCode() : 0);
		}

		// Token: 0x06003B2E RID: 15150 RVA: 0x000B6AC0 File Offset: 0x000B4CC0
		public override string ToString()
		{
			return FormattableString.Invariant(FormattableStringFactory.Create("<{0}, {1}, {2}, {3}, {4}", new object[]
			{
				this.Start,
				this.End,
				this.Name ?? "null",
				this.Type ?? "null",
				this.Description ?? "null"
			}));
		}

		// Token: 0x0200099F RID: 2463
		internal enum ColumnPosition
		{
			// Token: 0x04001B3B RID: 6971
			Equal,
			// Token: 0x04001B3C RID: 6972
			Contain,
			// Token: 0x04001B3D RID: 6973
			Within,
			// Token: 0x04001B3E RID: 6974
			Before,
			// Token: 0x04001B3F RID: 6975
			After,
			// Token: 0x04001B40 RID: 6976
			Overlap
		}
	}
}
