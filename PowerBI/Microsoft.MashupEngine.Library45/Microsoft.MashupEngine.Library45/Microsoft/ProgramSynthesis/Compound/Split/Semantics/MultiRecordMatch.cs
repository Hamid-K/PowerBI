using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Compound.Split.Semantics
{
	// Token: 0x02000987 RID: 2439
	public struct MultiRecordMatch
	{
		// Token: 0x06003A6C RID: 14956 RVA: 0x000B37F6 File Offset: 0x000B19F6
		public MultiRecordMatch(uint startRecord, uint endRecord, uint startIndex, uint endIndex, StringRegion value)
		{
			if (startRecord > endRecord)
			{
				throw new ArgumentException("endRecord larger than startRecord");
			}
			if (startRecord == endRecord && startIndex > endIndex)
			{
				throw new ArgumentException("startIndex larger than endIndex in the same record");
			}
			this._match = Record.Create<uint, uint, uint, uint, StringRegion>(startRecord, endRecord, startIndex, endIndex, value);
		}

		// Token: 0x17000A6F RID: 2671
		// (get) Token: 0x06003A6D RID: 14957 RVA: 0x000B382D File Offset: 0x000B1A2D
		public uint StartRecord
		{
			get
			{
				return this._match.Item1;
			}
		}

		// Token: 0x17000A70 RID: 2672
		// (get) Token: 0x06003A6E RID: 14958 RVA: 0x000B383A File Offset: 0x000B1A3A
		public uint EndRecord
		{
			get
			{
				return this._match.Item2;
			}
		}

		// Token: 0x17000A71 RID: 2673
		// (get) Token: 0x06003A6F RID: 14959 RVA: 0x000B3847 File Offset: 0x000B1A47
		public uint StartIndex
		{
			get
			{
				return this._match.Item3;
			}
		}

		// Token: 0x17000A72 RID: 2674
		// (get) Token: 0x06003A70 RID: 14960 RVA: 0x000B3854 File Offset: 0x000B1A54
		public uint EndIndex
		{
			get
			{
				return this._match.Item4;
			}
		}

		// Token: 0x17000A73 RID: 2675
		// (get) Token: 0x06003A71 RID: 14961 RVA: 0x000B3861 File Offset: 0x000B1A61
		public StringRegion Value
		{
			get
			{
				return this._match.Item5;
			}
		}

		// Token: 0x06003A72 RID: 14962 RVA: 0x000B3870 File Offset: 0x000B1A70
		public bool OverlapsWith(MultiRecordMatch? other)
		{
			if (other == null || other.Value.EndRecord < this.StartRecord || other.Value.StartRecord > this.EndRecord)
			{
				return false;
			}
			Record<uint, uint> record = Record.Create<uint, uint>(this.StartRecord, this.StartIndex);
			Record<uint, uint> record2 = Record.Create<uint, uint>(other.Value.StartRecord, other.Value.StartIndex);
			Record<uint, uint> record3 = (MultiRecordMatch.<OverlapsWith>g__Less|12_0(record, record2) ? record2 : record);
			Record<uint, uint> record4 = Record.Create<uint, uint>(this.EndRecord, this.EndIndex);
			Record<uint, uint> record5 = Record.Create<uint, uint>(other.Value.EndRecord, other.Value.EndIndex);
			Record<uint, uint> record6 = (MultiRecordMatch.<OverlapsWith>g__Less|12_0(record4, record5) ? record4 : record5);
			return MultiRecordMatch.<OverlapsWith>g__Less|12_0(record3, record6);
		}

		// Token: 0x06003A73 RID: 14963 RVA: 0x000B394B File Offset: 0x000B1B4B
		public bool OverlapsWithAny(IReadOnlyList<MultiRecordMatch?> otherMatches)
		{
			return otherMatches.Any(new Func<MultiRecordMatch?, bool>(this.OverlapsWith));
		}

		// Token: 0x06003A74 RID: 14964 RVA: 0x000B396C File Offset: 0x000B1B6C
		public override bool Equals(object obj)
		{
			if (obj is MultiRecordMatch)
			{
				MultiRecordMatch multiRecordMatch = (MultiRecordMatch)obj;
				return this.StartRecord == multiRecordMatch.StartRecord && this.EndRecord == multiRecordMatch.EndRecord && this.StartIndex == multiRecordMatch.StartIndex && this.EndIndex == multiRecordMatch.EndIndex && this.Value.Equals(multiRecordMatch.Value);
			}
			return false;
		}

		// Token: 0x06003A75 RID: 14965 RVA: 0x000B39DC File Offset: 0x000B1BDC
		public override int GetHashCode()
		{
			return this._match.GetHashCode();
		}

		// Token: 0x06003A76 RID: 14966 RVA: 0x000B3A00 File Offset: 0x000B1C00
		public override string ToString()
		{
			return this._match.ToString();
		}

		// Token: 0x06003A77 RID: 14967 RVA: 0x000B3A21 File Offset: 0x000B1C21
		[CompilerGenerated]
		internal static bool <OverlapsWith>g__Less|12_0(Record<uint, uint> a, Record<uint, uint> b)
		{
			return a.Item1 < b.Item1 || (a.Item1 == b.Item1 && a.Item2 < b.Item2);
		}

		// Token: 0x04001AA7 RID: 6823
		private readonly Record<uint, uint, uint, uint, StringRegion> _match;
	}
}
