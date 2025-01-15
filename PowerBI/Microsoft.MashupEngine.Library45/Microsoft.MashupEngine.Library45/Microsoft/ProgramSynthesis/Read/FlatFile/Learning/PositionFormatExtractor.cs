using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Microsoft.ProgramSynthesis.Read.FlatFile.Learning
{
	// Token: 0x0200129D RID: 4765
	internal abstract class PositionFormatExtractor : FwFormatExtractor
	{
		// Token: 0x06009019 RID: 36889 RVA: 0x001E46D2 File Offset: 0x001E28D2
		public PositionFormatExtractor(int startIndex, int nameIndex, int typeIndex, int descriptionIndex)
			: base(nameIndex, typeIndex, descriptionIndex)
		{
			if (this.StartIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex");
			}
			this.StartIndex = startIndex;
		}

		// Token: 0x170018CD RID: 6349
		// (get) Token: 0x0600901A RID: 36890 RVA: 0x001E46F9 File Offset: 0x001E28F9
		public int StartIndex { get; }

		// Token: 0x0600901B RID: 36891 RVA: 0x001E4704 File Offset: 0x001E2904
		protected int ExtractStart(IReadOnlyList<string> row)
		{
			if (row.Count <= this.StartIndex)
			{
				return -1;
			}
			int num = this.ExtractNumber(row[this.StartIndex]);
			if (num < 0)
			{
				return -1;
			}
			return num;
		}

		// Token: 0x0600901C RID: 36892 RVA: 0x001E473C File Offset: 0x001E293C
		protected int ExtractNumber(string cell)
		{
			if (cell == null)
			{
				return -1;
			}
			Match match = PositionFormatExtractor.NumberRegex.Match(cell);
			int num;
			if (!match.Success || !int.TryParse(match.Groups["number"].Value, out num))
			{
				return -1;
			}
			return num;
		}

		// Token: 0x04003AC9 RID: 15049
		public static readonly Regex NumberRegex = new Regex("^\\D*(?<number>\\d+)\\D*$", RegexOptions.Compiled);
	}
}
