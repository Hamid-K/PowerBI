using System;

namespace Microsoft.ProgramSynthesis.Extraction.Text.Learning
{
	// Token: 0x02000F52 RID: 3922
	public class TableNotRectangleException : ExampleException
	{
		// Token: 0x06006D33 RID: 27955 RVA: 0x001646FB File Offset: 0x001628FB
		public TableNotRectangleException(int rowIndex, int columnIndex, string example, string message)
			: base(rowIndex, columnIndex, example, message)
		{
		}
	}
}
