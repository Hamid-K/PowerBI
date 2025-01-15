using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Read.FlatFile.Learning
{
	// Token: 0x0200129B RID: 4763
	internal abstract class FwFormatExtractor
	{
		// Token: 0x0600900B RID: 36875 RVA: 0x001E4454 File Offset: 0x001E2654
		public FwFormatExtractor(int nameIndex, int typeIndex, int descriptionIndex)
		{
			this.NameIndex = nameIndex;
			this.TypeIndex = typeIndex;
			this.DescriptionIndex = descriptionIndex;
		}

		// Token: 0x170018C9 RID: 6345
		// (get) Token: 0x0600900C RID: 36876 RVA: 0x001E4471 File Offset: 0x001E2671
		// (set) Token: 0x0600900D RID: 36877 RVA: 0x001E4479 File Offset: 0x001E2679
		public int DescriptionIndex { get; internal set; }

		// Token: 0x170018CA RID: 6346
		// (get) Token: 0x0600900E RID: 36878 RVA: 0x001E4482 File Offset: 0x001E2682
		// (set) Token: 0x0600900F RID: 36879 RVA: 0x001E448A File Offset: 0x001E268A
		public int NameIndex { get; internal set; }

		// Token: 0x170018CB RID: 6347
		// (get) Token: 0x06009010 RID: 36880 RVA: 0x001E4493 File Offset: 0x001E2693
		// (set) Token: 0x06009011 RID: 36881 RVA: 0x001E449B File Offset: 0x001E269B
		public int TypeIndex { get; internal set; }

		// Token: 0x06009012 RID: 36882 RVA: 0x001E44A4 File Offset: 0x001E26A4
		protected Record<string, string, string> ExtractNameTypeDescription(IReadOnlyList<string> row)
		{
			string text = ((this.NameIndex >= 0 && this.NameIndex < row.Count) ? row[this.NameIndex].Trim() : null);
			string text2 = ((this.TypeIndex >= 0 && this.TypeIndex < row.Count) ? row[this.TypeIndex].Trim() : null);
			string text3 = ((this.DescriptionIndex >= 0 && this.DescriptionIndex < row.Count) ? row[this.DescriptionIndex].Trim() : null);
			return Record.Create<string, string, string>(text, text2, text3);
		}

		// Token: 0x06009013 RID: 36883
		protected abstract Record<int, int>? ExtractPositions(IReadOnlyList<string> row);

		// Token: 0x06009014 RID: 36884 RVA: 0x001E453C File Offset: 0x001E273C
		public FwColumnFormat Extract(IReadOnlyList<string> row)
		{
			Record<int, int>? record = this.ExtractPositions(row);
			if (record == null)
			{
				return null;
			}
			Record<string, string, string> record2 = this.ExtractNameTypeDescription(row);
			return new FwColumnFormat(record.Value.Item1, new int?(record.Value.Item2), record2.Item1, record2.Item2, record2.Item3);
		}
	}
}
