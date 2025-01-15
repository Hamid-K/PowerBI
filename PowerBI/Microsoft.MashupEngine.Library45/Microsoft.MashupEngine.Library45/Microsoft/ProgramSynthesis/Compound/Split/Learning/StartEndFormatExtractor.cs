using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Compound.Split.Learning
{
	// Token: 0x020009A3 RID: 2467
	internal class StartEndFormatExtractor : PositionFormatExtractor
	{
		// Token: 0x06003B42 RID: 15170 RVA: 0x000B6E41 File Offset: 0x000B5041
		public StartEndFormatExtractor(int startIndex, int endIndex, int nameIndex, int typeIndex, int descriptionIndex)
			: base(startIndex, nameIndex, typeIndex, descriptionIndex)
		{
			if (this.EndIndex < 0)
			{
				throw new ArgumentOutOfRangeException("endIndex");
			}
			this.EndIndex = endIndex;
		}

		// Token: 0x17000A93 RID: 2707
		// (get) Token: 0x06003B43 RID: 15171 RVA: 0x000B6E6A File Offset: 0x000B506A
		public int EndIndex { get; }

		// Token: 0x06003B44 RID: 15172 RVA: 0x000B6E74 File Offset: 0x000B5074
		protected override Tuple<int, int> ExtractPositions(IReadOnlyList<string> row)
		{
			int num = base.ExtractStart(row);
			if (num < 0)
			{
				return null;
			}
			int num2 = base.ExtractNumber(row[this.EndIndex]);
			if (num2 < 0 || num2 < num)
			{
				return null;
			}
			return new Tuple<int, int>(num, num2);
		}
	}
}
