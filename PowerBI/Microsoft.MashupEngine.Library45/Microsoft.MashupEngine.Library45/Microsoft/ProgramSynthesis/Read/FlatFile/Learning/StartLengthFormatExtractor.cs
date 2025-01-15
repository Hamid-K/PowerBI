using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Read.FlatFile.Learning
{
	// Token: 0x0200129F RID: 4767
	internal class StartLengthFormatExtractor : PositionFormatExtractor
	{
		// Token: 0x06009021 RID: 36897 RVA: 0x001E481C File Offset: 0x001E2A1C
		public StartLengthFormatExtractor(int startIndex, int lengthIndex, int nameIndex, int typeIndex, int descriptionIndex)
			: base(startIndex, nameIndex, typeIndex, descriptionIndex)
		{
			if (lengthIndex < 0)
			{
				throw new ArgumentOutOfRangeException("lengthIndex");
			}
			this.LengthIndex = lengthIndex;
		}

		// Token: 0x170018CF RID: 6351
		// (get) Token: 0x06009022 RID: 36898 RVA: 0x001E4840 File Offset: 0x001E2A40
		public int LengthIndex { get; }

		// Token: 0x06009023 RID: 36899 RVA: 0x001E4848 File Offset: 0x001E2A48
		protected override Record<int, int>? ExtractPositions(IReadOnlyList<string> row)
		{
			int num = base.ExtractStart(row);
			if (num < 0)
			{
				return null;
			}
			int num2 = base.ExtractNumber(row[this.LengthIndex]);
			if (num2 < 0)
			{
				return null;
			}
			return new Record<int, int>?(Record.Create<int, int>(num, num + num2 - 1));
		}
	}
}
