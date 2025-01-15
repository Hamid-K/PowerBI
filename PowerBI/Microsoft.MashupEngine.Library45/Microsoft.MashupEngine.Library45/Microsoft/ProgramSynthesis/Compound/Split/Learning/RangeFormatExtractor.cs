using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Microsoft.ProgramSynthesis.Compound.Split.Learning
{
	// Token: 0x020009A1 RID: 2465
	internal class RangeFormatExtractor : FwFormatExtractor
	{
		// Token: 0x06003B39 RID: 15161 RVA: 0x000B6C6B File Offset: 0x000B4E6B
		public RangeFormatExtractor(int rangeIndex, int nameIndex, int typeIndex, int descriptionIndex)
			: base(nameIndex, typeIndex, descriptionIndex)
		{
			if (this.RangeIndex < 0)
			{
				throw new ArgumentOutOfRangeException("rangeIndex");
			}
			this.RangeIndex = rangeIndex;
		}

		// Token: 0x17000A91 RID: 2705
		// (get) Token: 0x06003B3A RID: 15162 RVA: 0x000B6C92 File Offset: 0x000B4E92
		public int RangeIndex { get; }

		// Token: 0x06003B3B RID: 15163 RVA: 0x000B6C9C File Offset: 0x000B4E9C
		protected override Tuple<int, int> ExtractPositions(IReadOnlyList<string> row)
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
					return new Tuple<int, int>(num, num2);
				}
				return null;
			}
			else
			{
				match = RangeFormatExtractor.SingleRangeRegex.Match(text);
				if (match.Success && int.TryParse(match.Groups["start"].Value, out num))
				{
					return new Tuple<int, int>(num, num);
				}
				return null;
			}
		}

		// Token: 0x04001B44 RID: 6980
		public static readonly Regex RangeRegex = new Regex("^\\D*(?<start>\\d+)\\W+(?<end>\\d+)\\D*$", RegexOptions.Compiled);

		// Token: 0x04001B45 RID: 6981
		public static readonly Regex SingleRangeRegex = new Regex("^\\s*(?<start>\\d+)\\s*$", RegexOptions.Compiled);
	}
}
