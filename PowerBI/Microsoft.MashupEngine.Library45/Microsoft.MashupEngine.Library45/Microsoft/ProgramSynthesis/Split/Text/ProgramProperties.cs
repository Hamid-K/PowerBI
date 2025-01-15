using System;
using Microsoft.ProgramSynthesis.Split.Text.Semantics;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling;

namespace Microsoft.ProgramSynthesis.Split.Text
{
	// Token: 0x020012F7 RID: 4855
	public class ProgramProperties : IProgramProperties
	{
		// Token: 0x06009251 RID: 37457 RVA: 0x001EC72D File Offset: 0x001EA92D
		public ProgramProperties(string delimiter, int columnCount, int splitCount, Record<int, int?>[] fieldPositions, QuotingConfiguration quotingConf)
		{
			this.Delimiter = delimiter;
			this.ColumnCount = columnCount;
			this.SplitCount = splitCount;
			this.FieldPositions = fieldPositions;
			this.QuotingConfiguration = quotingConf;
		}

		// Token: 0x17001925 RID: 6437
		// (get) Token: 0x06009252 RID: 37458 RVA: 0x001EC75A File Offset: 0x001EA95A
		public string Delimiter { get; }

		// Token: 0x17001926 RID: 6438
		// (get) Token: 0x06009253 RID: 37459 RVA: 0x001EC762 File Offset: 0x001EA962
		public QuotingConfiguration QuotingConfiguration { get; }

		// Token: 0x17001927 RID: 6439
		// (get) Token: 0x06009254 RID: 37460 RVA: 0x001EC76A File Offset: 0x001EA96A
		public Record<int, int?>[] FieldPositions { get; }

		// Token: 0x17001928 RID: 6440
		// (get) Token: 0x06009255 RID: 37461 RVA: 0x001EC772 File Offset: 0x001EA972
		public int ColumnCount { get; }

		// Token: 0x17001929 RID: 6441
		// (get) Token: 0x06009256 RID: 37462 RVA: 0x001EC77A File Offset: 0x001EA97A
		public int SplitCount { get; }
	}
}
