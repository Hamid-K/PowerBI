using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Microsoft.ProgramSynthesis.Compound.Split.Learning
{
	// Token: 0x020009A2 RID: 2466
	internal abstract class PositionFormatExtractor : FwFormatExtractor
	{
		// Token: 0x06003B3D RID: 15165 RVA: 0x000B6D81 File Offset: 0x000B4F81
		public PositionFormatExtractor(int startIndex, int nameIndex, int typeIndex, int descriptionIndex)
			: base(nameIndex, typeIndex, descriptionIndex)
		{
			if (this.StartIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex");
			}
			this.StartIndex = startIndex;
		}

		// Token: 0x17000A92 RID: 2706
		// (get) Token: 0x06003B3E RID: 15166 RVA: 0x000B6DA8 File Offset: 0x000B4FA8
		public int StartIndex { get; }

		// Token: 0x06003B3F RID: 15167 RVA: 0x000B6DB0 File Offset: 0x000B4FB0
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

		// Token: 0x06003B40 RID: 15168 RVA: 0x000B6DE8 File Offset: 0x000B4FE8
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

		// Token: 0x04001B47 RID: 6983
		public static readonly Regex NumberRegex = new Regex("^\\D*(?<number>\\d+)\\D*$", RegexOptions.Compiled);
	}
}
