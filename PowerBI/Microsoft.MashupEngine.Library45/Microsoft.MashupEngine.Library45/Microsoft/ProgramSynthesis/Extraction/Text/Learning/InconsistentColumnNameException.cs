using System;

namespace Microsoft.ProgramSynthesis.Extraction.Text.Learning
{
	// Token: 0x02000F53 RID: 3923
	public class InconsistentColumnNameException : ExampleException
	{
		// Token: 0x06006D34 RID: 27956 RVA: 0x00164708 File Offset: 0x00162908
		public InconsistentColumnNameException(int columnIndex, string column, string message)
			: base(-1, columnIndex, column, message)
		{
		}
	}
}
