using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Compound.Split.Learning
{
	// Token: 0x020009A0 RID: 2464
	internal abstract class FwFormatExtractor
	{
		// Token: 0x06003B2F RID: 15151 RVA: 0x000B6B34 File Offset: 0x000B4D34
		public FwFormatExtractor(int nameIndex, int typeIndex, int descriptionIndex)
		{
			this.NameIndex = nameIndex;
			this.TypeIndex = typeIndex;
			this.DescriptionIndex = descriptionIndex;
		}

		// Token: 0x17000A8E RID: 2702
		// (get) Token: 0x06003B30 RID: 15152 RVA: 0x000B6B51 File Offset: 0x000B4D51
		// (set) Token: 0x06003B31 RID: 15153 RVA: 0x000B6B59 File Offset: 0x000B4D59
		public int DescriptionIndex { get; internal set; }

		// Token: 0x17000A8F RID: 2703
		// (get) Token: 0x06003B32 RID: 15154 RVA: 0x000B6B62 File Offset: 0x000B4D62
		// (set) Token: 0x06003B33 RID: 15155 RVA: 0x000B6B6A File Offset: 0x000B4D6A
		public int NameIndex { get; internal set; }

		// Token: 0x17000A90 RID: 2704
		// (get) Token: 0x06003B34 RID: 15156 RVA: 0x000B6B73 File Offset: 0x000B4D73
		// (set) Token: 0x06003B35 RID: 15157 RVA: 0x000B6B7B File Offset: 0x000B4D7B
		public int TypeIndex { get; internal set; }

		// Token: 0x06003B36 RID: 15158 RVA: 0x000B6B84 File Offset: 0x000B4D84
		protected Tuple<string, string, string> ExtractNameTypeDescription(IReadOnlyList<string> row)
		{
			string text = ((this.NameIndex >= 0 && this.NameIndex < row.Count) ? row[this.NameIndex].Trim() : null);
			string text2 = ((this.TypeIndex >= 0 && this.TypeIndex < row.Count) ? row[this.TypeIndex].Trim() : null);
			string text3 = ((this.DescriptionIndex >= 0 && this.DescriptionIndex < row.Count) ? row[this.DescriptionIndex].Trim() : null);
			return new Tuple<string, string, string>(text, text2, text3);
		}

		// Token: 0x06003B37 RID: 15159
		protected abstract Tuple<int, int> ExtractPositions(IReadOnlyList<string> row);

		// Token: 0x06003B38 RID: 15160 RVA: 0x000B6C1C File Offset: 0x000B4E1C
		public FwColumnFormat Extract(IReadOnlyList<string> row)
		{
			Tuple<int, int> tuple = this.ExtractPositions(row);
			if (tuple == null)
			{
				return null;
			}
			Tuple<string, string, string> tuple2 = this.ExtractNameTypeDescription(row);
			if (tuple2 == null)
			{
				return null;
			}
			return new FwColumnFormat(tuple.Item1, new int?(tuple.Item2), tuple2.Item1, tuple2.Item2, tuple2.Item3);
		}
	}
}
