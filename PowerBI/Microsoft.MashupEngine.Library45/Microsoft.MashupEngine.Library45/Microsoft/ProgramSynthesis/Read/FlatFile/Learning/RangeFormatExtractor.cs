using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Read.FlatFile.Learning
{
	// Token: 0x0200129C RID: 4764
	internal class RangeFormatExtractor : FwFormatExtractor
	{
		// Token: 0x06009015 RID: 36885 RVA: 0x001E4598 File Offset: 0x001E2798
		public RangeFormatExtractor(int rangeIndex, int nameIndex, int typeIndex, int descriptionIndex)
			: base(nameIndex, typeIndex, descriptionIndex)
		{
			if (this.RangeIndex < 0)
			{
				throw new ArgumentOutOfRangeException("rangeIndex");
			}
			this.RangeIndex = rangeIndex;
		}

		// Token: 0x170018CC RID: 6348
		// (get) Token: 0x06009016 RID: 36886 RVA: 0x001E45BF File Offset: 0x001E27BF
		public int RangeIndex { get; }

		// Token: 0x06009017 RID: 36887 RVA: 0x001E45C8 File Offset: 0x001E27C8
		protected override Record<int, int>? ExtractPositions(IReadOnlyList<string> row)
		{
			if (row.Count <= this.RangeIndex)
			{
				return null;
			}
			string text = row[this.RangeIndex];
			Match match = RangeFormatExtractor.RangeRegex.Match(text);
			int num;
			int num2;
			if (match.Success && int.TryParse(match.Groups["start"].Value, out num) && int.TryParse(match.Groups["end"].Value, out num2))
			{
				if (num <= num2)
				{
					return new Record<int, int>?(Record.Create<int, int>(num, num2));
				}
				return null;
			}
			else
			{
				match = RangeFormatExtractor.SingleRangeRegex.Match(text);
				if (match.Success && int.TryParse(match.Groups["start"].Value, out num))
				{
					return new Record<int, int>?(Record.Create<int, int>(num, num));
				}
				return null;
			}
		}

		// Token: 0x04003AC6 RID: 15046
		public static readonly Regex RangeRegex = new Regex("^\\D*(?<start>\\d+)\\W+(?<end>\\d+)\\D*$", RegexOptions.Compiled);

		// Token: 0x04003AC7 RID: 15047
		public static readonly Regex SingleRangeRegex = new Regex("^\\s*(?<start>\\d+)\\s*$", RegexOptions.Compiled);
	}
}
