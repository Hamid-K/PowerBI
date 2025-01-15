using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.DslLibrary.Dates
{
	// Token: 0x0200084B RID: 2123
	public struct DateTimePartSet : IEquatable<DateTimePartSet>
	{
		// Token: 0x06002DF6 RID: 11766 RVA: 0x00083942 File Offset: 0x00081B42
		public DateTimePartSet(params DateTimePart[] parts)
		{
			this.Data = DateTimePartSet.Create(parts);
		}

		// Token: 0x06002DF7 RID: 11767 RVA: 0x00083942 File Offset: 0x00081B42
		public DateTimePartSet(IEnumerable<DateTimePart> parts)
		{
			this.Data = DateTimePartSet.Create(parts);
		}

		// Token: 0x06002DF8 RID: 11768 RVA: 0x00083950 File Offset: 0x00081B50
		private static ushort Create(IEnumerable<DateTimePart> parts)
		{
			ushort num = 0;
			foreach (DateTimePart dateTimePart in parts)
			{
				num |= (ushort)(1 << (int)dateTimePart);
			}
			return num;
		}

		// Token: 0x06002DF9 RID: 11769 RVA: 0x000839A0 File Offset: 0x00081BA0
		private DateTimePartSet(ushort data)
		{
			this.Data = data;
		}

		// Token: 0x170007F8 RID: 2040
		// (get) Token: 0x06002DFA RID: 11770 RVA: 0x000839A9 File Offset: 0x00081BA9
		private readonly ushort Data { get; }

		// Token: 0x170007F9 RID: 2041
		// (get) Token: 0x06002DFB RID: 11771 RVA: 0x000839B1 File Offset: 0x00081BB1
		public static DateTimePartSet All { get; } = new DateTimePartSet(DateTimePartList.AllDateTime);

		// Token: 0x170007FA RID: 2042
		// (get) Token: 0x06002DFC RID: 11772 RVA: 0x000839B8 File Offset: 0x00081BB8
		public static DateTimePartSet Empty { get; } = new DateTimePartSet(0);

		// Token: 0x170007FB RID: 2043
		// (get) Token: 0x06002DFD RID: 11773 RVA: 0x000839BF File Offset: 0x00081BBF
		public static DateTimePartSet TimeParts { get; } = new DateTimePartSet(from p in DateTimePartSet.All.AsEnumerable()
			where p.GetKind() == DateTimePartKind.Time
			select p);

		// Token: 0x170007FC RID: 2044
		// (get) Token: 0x06002DFE RID: 11774 RVA: 0x000839C6 File Offset: 0x00081BC6
		public static DateTimePartSet DateParts { get; } = new DateTimePartSet(from p in DateTimePartSet.All.AsEnumerable()
			where p.GetKind() == DateTimePartKind.Date
			select p);

		// Token: 0x170007FD RID: 2045
		// (get) Token: 0x06002DFF RID: 11775 RVA: 0x000839CD File Offset: 0x00081BCD
		public static DateTimePartSet StandardTimeParts { get; } = new DateTimePartSet(DateTimePartList.StandardTimeDescending);

		// Token: 0x06002E00 RID: 11776 RVA: 0x000839D4 File Offset: 0x00081BD4
		private static bool IsSet(ushort parts, int bit)
		{
			return ((int)parts | (1 << bit)) == (int)parts;
		}

		// Token: 0x06002E01 RID: 11777 RVA: 0x000839E1 File Offset: 0x00081BE1
		public bool ContainsBit(int i)
		{
			return DateTimePartSet.IsSet(this.Data, i);
		}

		// Token: 0x06002E02 RID: 11778 RVA: 0x000839E1 File Offset: 0x00081BE1
		public bool Contains(DateTimePart part)
		{
			return DateTimePartSet.IsSet(this.Data, (int)part);
		}

		// Token: 0x06002E03 RID: 11779 RVA: 0x000839EF File Offset: 0x00081BEF
		public bool Contains(DateTimePart part1, DateTimePart part2)
		{
			return DateTimePartSet.IsSet(this.Data, (int)part1) && DateTimePartSet.IsSet(this.Data, (int)part2);
		}

		// Token: 0x06002E04 RID: 11780 RVA: 0x00083A0D File Offset: 0x00081C0D
		public bool Contains(DateTimePart part1, DateTimePart part2, DateTimePart part3)
		{
			return DateTimePartSet.IsSet(this.Data, (int)part1) && DateTimePartSet.IsSet(this.Data, (int)part2) && DateTimePartSet.IsSet(this.Data, (int)part3);
		}

		// Token: 0x06002E05 RID: 11781 RVA: 0x00083A3C File Offset: 0x00081C3C
		public bool Contains(params DateTimePart[] parts)
		{
			return new DateTimePartSet(parts).Contains(this);
		}

		// Token: 0x06002E06 RID: 11782 RVA: 0x00083A5D File Offset: 0x00081C5D
		public bool Contains(DateTimePartSet other)
		{
			return other.Union(this) == this;
		}

		// Token: 0x06002E07 RID: 11783 RVA: 0x00083A77 File Offset: 0x00081C77
		public DateTimePartSet Intersect(DateTimePartSet otherSet)
		{
			return new DateTimePartSet(this.Data & otherSet.Data);
		}

		// Token: 0x06002E08 RID: 11784 RVA: 0x00083A8D File Offset: 0x00081C8D
		public DateTimePartSet Union(DateTimePartSet otherSet)
		{
			return new DateTimePartSet(this.Data | otherSet.Data);
		}

		// Token: 0x06002E09 RID: 11785 RVA: 0x00083AA3 File Offset: 0x00081CA3
		public DateTimePartSet SetDifference(DateTimePartSet otherSet)
		{
			return new DateTimePartSet(this.Data & ~otherSet.Data);
		}

		// Token: 0x06002E0A RID: 11786 RVA: 0x00083ABA File Offset: 0x00081CBA
		public DateTimePartSet Except(IEnumerable<DateTimePart> parts)
		{
			return this.SetDifference(new DateTimePartSet(parts));
		}

		// Token: 0x06002E0B RID: 11787 RVA: 0x00083AC8 File Offset: 0x00081CC8
		public DateTimePartSet Except(params DateTimePart[] parts)
		{
			return this.SetDifference(new DateTimePartSet(parts));
		}

		// Token: 0x06002E0C RID: 11788 RVA: 0x00083AD8 File Offset: 0x00081CD8
		public DateTimePart? OnlyOrDefault()
		{
			if (!this.IsSingleton())
			{
				return null;
			}
			uint data = (uint)this.Data;
			return new DateTimePart?((DateTimePart)DateTimePartSet.MultiplyDeBruijnBitPosition[(int)((uint)(((ulong)data & -(ulong)data) * 125613361UL) >> 27)]);
		}

		// Token: 0x06002E0D RID: 11789 RVA: 0x00083B1C File Offset: 0x00081D1C
		public bool IsSingleton()
		{
			ushort data = this.Data;
			return data != 0 && (data & (data - 1)) == 0;
		}

		// Token: 0x06002E0E RID: 11790 RVA: 0x00083B3D File Offset: 0x00081D3D
		public bool Any()
		{
			return this.Data > 0;
		}

		// Token: 0x06002E0F RID: 11791 RVA: 0x00083B48 File Offset: 0x00081D48
		public uint Count()
		{
			return MathUtils.CountBits((uint)this.Data);
		}

		// Token: 0x06002E10 RID: 11792 RVA: 0x00083B55 File Offset: 0x00081D55
		public DateTimePartSet Set(DateTimePart part)
		{
			return new DateTimePartSet((ushort)((int)this.Data | (1 << (int)part)));
		}

		// Token: 0x06002E11 RID: 11793 RVA: 0x00083B6A File Offset: 0x00081D6A
		public DateTimePartSet Set(params DateTimePart[] parts)
		{
			return this.Union(new DateTimePartSet(parts));
		}

		// Token: 0x06002E12 RID: 11794 RVA: 0x00083B78 File Offset: 0x00081D78
		public DateTimePartSet Clear(DateTimePart part)
		{
			return new DateTimePartSet((ushort)((int)this.Data & ~(1 << (int)part)));
		}

		// Token: 0x06002E13 RID: 11795 RVA: 0x00083B8E File Offset: 0x00081D8E
		public IEnumerable<DateTimePart> AsEnumerable()
		{
			return MathUtils.GetBits(this.Data, 16).Collect(delegate(int bit, int idx)
			{
				if (bit != 1)
				{
					return null;
				}
				return new DateTimePart?((DateTimePart)idx);
			});
		}

		// Token: 0x06002E14 RID: 11796 RVA: 0x00083BC1 File Offset: 0x00081DC1
		public bool Equals(DateTimePartSet other)
		{
			return this.Data == other.Data;
		}

		// Token: 0x06002E15 RID: 11797 RVA: 0x00083BD2 File Offset: 0x00081DD2
		public override bool Equals(object obj)
		{
			return obj is DateTimePartSet && this.Equals((DateTimePartSet)obj);
		}

		// Token: 0x06002E16 RID: 11798 RVA: 0x00083BEC File Offset: 0x00081DEC
		public override int GetHashCode()
		{
			return this.Data.GetHashCode();
		}

		// Token: 0x06002E17 RID: 11799 RVA: 0x00083C07 File Offset: 0x00081E07
		public static bool operator ==(DateTimePartSet left, DateTimePartSet right)
		{
			return left.Equals(right);
		}

		// Token: 0x06002E18 RID: 11800 RVA: 0x00083C11 File Offset: 0x00081E11
		public static bool operator !=(DateTimePartSet left, DateTimePartSet right)
		{
			return !left.Equals(right);
		}

		// Token: 0x06002E19 RID: 11801 RVA: 0x00083C20 File Offset: 0x00081E20
		public override string ToString()
		{
			return "{" + string.Join<DateTimePart>(", ", DateTimePartList.AllDateTime.Where((DateTimePart part) => this.Contains(part))) + "}";
		}

		// Token: 0x04001656 RID: 5718
		private static readonly int[] MultiplyDeBruijnBitPosition = new int[]
		{
			0, 1, 28, 2, 29, 14, 24, 3, 30, 22,
			20, 15, 25, 17, 4, 8, 31, 27, 13, 23,
			21, 19, 16, 7, 26, 12, 18, 6, 11, 5,
			10, 9
		};
	}
}
