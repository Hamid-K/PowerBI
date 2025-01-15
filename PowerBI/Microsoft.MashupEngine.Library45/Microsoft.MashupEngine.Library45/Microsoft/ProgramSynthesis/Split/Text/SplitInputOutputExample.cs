using System;
using System.Linq;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Split.Text.Semantics;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Split.Text
{
	// Token: 0x020012FB RID: 4859
	public class SplitInputOutputExample : Example<StringRegion, SplitCell[]>
	{
		// Token: 0x06009270 RID: 37488 RVA: 0x001EC94E File Offset: 0x001EAB4E
		public SplitInputOutputExample(StringRegion input, SplitCell[] output)
			: base(input, output, false)
		{
		}

		// Token: 0x06009271 RID: 37489 RVA: 0x001EC95C File Offset: 0x001EAB5C
		public override bool Valid(Program<StringRegion, SplitCell[]> program)
		{
			SplitCell[] array = program.Run(base.Input);
			return base.Output.Select(delegate(SplitCell c)
			{
				StringRegion cellValue = c.CellValue;
				if (cellValue == null)
				{
					return null;
				}
				return cellValue.Value;
			}).SequenceEqual(array.Select(delegate(SplitCell c)
			{
				StringRegion cellValue2 = c.CellValue;
				if (cellValue2 == null)
				{
					return null;
				}
				return cellValue2.Value;
			}));
		}
	}
}
