using System;
using System.Runtime.CompilerServices;

namespace Microsoft.ProgramSynthesis.Read.FlatFile.Learning
{
	// Token: 0x02001299 RID: 4761
	public class FwColumnFormat : IEquatable<FwColumnFormat>
	{
		// Token: 0x06009000 RID: 36864 RVA: 0x001E4117 File Offset: 0x001E2317
		public FwColumnFormat(int start, int? end, string name = null, string type = null, string description = null)
		{
			this.Start = start;
			this.End = end;
			this.Name = name;
			this.Type = type;
			this.Description = description;
		}

		// Token: 0x170018C4 RID: 6340
		// (get) Token: 0x06009001 RID: 36865 RVA: 0x001E4144 File Offset: 0x001E2344
		public int Start { get; }

		// Token: 0x170018C5 RID: 6341
		// (get) Token: 0x06009002 RID: 36866 RVA: 0x001E414C File Offset: 0x001E234C
		public int? End { get; }

		// Token: 0x170018C6 RID: 6342
		// (get) Token: 0x06009003 RID: 36867 RVA: 0x001E4154 File Offset: 0x001E2354
		public string Name { get; }

		// Token: 0x170018C7 RID: 6343
		// (get) Token: 0x06009004 RID: 36868 RVA: 0x001E415C File Offset: 0x001E235C
		public string Type { get; }

		// Token: 0x170018C8 RID: 6344
		// (get) Token: 0x06009005 RID: 36869 RVA: 0x001E4164 File Offset: 0x001E2364
		public string Description { get; }

		// Token: 0x06009006 RID: 36870 RVA: 0x001E416C File Offset: 0x001E236C
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

		// Token: 0x06009007 RID: 36871 RVA: 0x001E4288 File Offset: 0x001E2488
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

		// Token: 0x06009008 RID: 36872 RVA: 0x001E4318 File Offset: 0x001E2518
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((FwColumnFormat)obj)));
		}

		// Token: 0x06009009 RID: 36873 RVA: 0x001E4348 File Offset: 0x001E2548
		public override int GetHashCode()
		{
			return (((((((this.Start * 307) ^ ((this.End != null) ? this.End.GetHashCode() : 0)) * 307) ^ ((this.Name != null) ? this.Name.GetHashCode() : 0)) * 307) ^ ((this.Type != null) ? this.Type.GetHashCode() : 0)) * 307) ^ ((this.Description != null) ? this.Description.GetHashCode() : 0);
		}

		// Token: 0x0600900A RID: 36874 RVA: 0x001E43E0 File Offset: 0x001E25E0
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

		// Token: 0x0200129A RID: 4762
		internal enum ColumnPosition
		{
			// Token: 0x04003ABD RID: 15037
			Equal,
			// Token: 0x04003ABE RID: 15038
			Contain,
			// Token: 0x04003ABF RID: 15039
			Within,
			// Token: 0x04003AC0 RID: 15040
			Before,
			// Token: 0x04003AC1 RID: 15041
			After,
			// Token: 0x04003AC2 RID: 15042
			Overlap
		}
	}
}
