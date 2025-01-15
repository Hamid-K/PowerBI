using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.ProgramSynthesis.Detection.RichDataTypes
{
	// Token: 0x02000AAF RID: 2735
	public class SyntacticTypeOptionSet<TSyntacticType> : IEnumerable<TSyntacticType>, IEnumerable where TSyntacticType : SyntacticType
	{
		// Token: 0x17000C41 RID: 3137
		// (get) Token: 0x060044C4 RID: 17604 RVA: 0x000D773C File Offset: 0x000D593C
		// (set) Token: 0x060044C5 RID: 17605 RVA: 0x000D7744 File Offset: 0x000D5944
		public IReadOnlyList<TSyntacticType> Options { get; private set; }

		// Token: 0x060044C6 RID: 17606 RVA: 0x000D774D File Offset: 0x000D594D
		public SyntacticTypeOptionSet(IEnumerable<TSyntacticType> options)
		{
			this.Options = options.ToList<TSyntacticType>();
		}

		// Token: 0x060044C7 RID: 17607 RVA: 0x000D7761 File Offset: 0x000D5961
		public IEnumerator<TSyntacticType> GetEnumerator()
		{
			return this.Options.GetEnumerator();
		}

		// Token: 0x060044C8 RID: 17608 RVA: 0x000D776E File Offset: 0x000D596E
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060044C9 RID: 17609 RVA: 0x000D7778 File Offset: 0x000D5978
		public bool Refine(string value)
		{
			if (this.Options.Count == 1)
			{
				return false;
			}
			List<TSyntacticType> list = this.Options.Where((TSyntacticType opt) => opt.IsValid(value)).ToList<TSyntacticType>();
			if (list.Count <= 0 || list.Count >= this.Options.Count)
			{
				return false;
			}
			this.Options = list;
			return true;
		}

		// Token: 0x060044CA RID: 17610 RVA: 0x000D77E8 File Offset: 0x000D59E8
		public bool MatchesAny(string value)
		{
			return this.Options.Any((TSyntacticType opt) => opt.IsValid(value));
		}

		// Token: 0x060044CB RID: 17611 RVA: 0x000D781C File Offset: 0x000D5A1C
		public bool MatchesAll(string value)
		{
			return this.Options.All((TSyntacticType opt) => opt.IsValid(value));
		}

		// Token: 0x060044CC RID: 17612 RVA: 0x000D7850 File Offset: 0x000D5A50
		public int GetMatchCount(string value)
		{
			return this.Options.Count((TSyntacticType opt) => opt.IsValid(value));
		}

		// Token: 0x17000C42 RID: 3138
		// (get) Token: 0x060044CD RID: 17613 RVA: 0x000D7881 File Offset: 0x000D5A81
		public int Count
		{
			get
			{
				return this.Options.Count;
			}
		}

		// Token: 0x060044CE RID: 17614 RVA: 0x000D7890 File Offset: 0x000D5A90
		internal void AddExample(string example)
		{
			TSyntacticType tsyntacticType = this.Options.FirstOrDefault((TSyntacticType option) => option.IsValid(example));
			if (tsyntacticType == null)
			{
				return;
			}
			tsyntacticType.AddExample(example);
		}

		// Token: 0x060044CF RID: 17615 RVA: 0x000D78D6 File Offset: 0x000D5AD6
		internal void DropOptionsWithNoExamples()
		{
			this.Options = this.Options.Where((TSyntacticType option) => option.Examples.Any<string>()).ToList<TSyntacticType>();
		}

		// Token: 0x060044D0 RID: 17616 RVA: 0x000D7910 File Offset: 0x000D5B10
		internal void Remove(Func<TSyntacticType, bool> filter)
		{
			this.Options = this.Options.Where((TSyntacticType option) => !filter(option)).ToList<TSyntacticType>();
		}
	}
}
