using System;

namespace Microsoft.ProgramSynthesis.Extraction.Text.Learning
{
	// Token: 0x02000F51 RID: 3921
	public class ExampleNotFoundException : ExampleException
	{
		// Token: 0x06006D32 RID: 27954 RVA: 0x001646FB File Offset: 0x001628FB
		public ExampleNotFoundException(int rowIndex, int columnIndex, string example, string message)
			: base(rowIndex, columnIndex, example, message)
		{
		}
	}
}
