using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Compound.Split.Learning
{
	// Token: 0x020009A4 RID: 2468
	internal class StartLengthFormatExtractor : PositionFormatExtractor
	{
		// Token: 0x06003B45 RID: 15173 RVA: 0x000B6EB3 File Offset: 0x000B50B3
		public StartLengthFormatExtractor(int startIndex, int lengthIndex, int nameIndex, int typeIndex, int descriptionIndex)
			: base(startIndex, nameIndex, typeIndex, descriptionIndex)
		{
			if (lengthIndex < 0)
			{
				throw new ArgumentOutOfRangeException("lengthIndex");
			}
			this.LengthIndex = lengthIndex;
		}

		// Token: 0x17000A94 RID: 2708
		// (get) Token: 0x06003B46 RID: 15174 RVA: 0x000B6ED7 File Offset: 0x000B50D7
		public int LengthIndex { get; }

		// Token: 0x06003B47 RID: 15175 RVA: 0x000B6EE0 File Offset: 0x000B50E0
		protected override Tuple<int, int> ExtractPositions(IReadOnlyList<string> row)
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
			return new Tuple<int, int>(num, num + num2 - 1);
		}
	}
}
