using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Read.FlatFile.Learning
{
	// Token: 0x0200129E RID: 4766
	internal class StartEndFormatExtractor : PositionFormatExtractor
	{
		// Token: 0x0600901E RID: 36894 RVA: 0x001E4795 File Offset: 0x001E2995
		public StartEndFormatExtractor(int startIndex, int endIndex, int nameIndex, int typeIndex, int descriptionIndex)
			: base(startIndex, nameIndex, typeIndex, descriptionIndex)
		{
			if (this.EndIndex < 0)
			{
				throw new ArgumentOutOfRangeException("endIndex");
			}
			this.EndIndex = endIndex;
		}

		// Token: 0x170018CE RID: 6350
		// (get) Token: 0x0600901F RID: 36895 RVA: 0x001E47BE File Offset: 0x001E29BE
		public int EndIndex { get; }

		// Token: 0x06009020 RID: 36896 RVA: 0x001E47C8 File Offset: 0x001E29C8
		protected override Record<int, int>? ExtractPositions(IReadOnlyList<string> row)
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
			return new Record<int, int>?(Record.Create<int, int>(num, num2));
		}
	}
}
