using System;

namespace Microsoft.ProgramSynthesis.Extraction.Text.Learning
{
	// Token: 0x02000F54 RID: 3924
	public class InconsistentExampleException : ExampleException
	{
		// Token: 0x06006D35 RID: 27957 RVA: 0x001646FB File Offset: 0x001628FB
		public InconsistentExampleException(int rowIndex, int columnIndex, string example, string message)
			: base(rowIndex, columnIndex, example, message)
		{
		}
	}
}
