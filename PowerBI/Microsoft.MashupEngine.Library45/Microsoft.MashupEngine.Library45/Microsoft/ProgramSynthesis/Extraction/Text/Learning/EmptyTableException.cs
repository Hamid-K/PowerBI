using System;

namespace Microsoft.ProgramSynthesis.Extraction.Text.Learning
{
	// Token: 0x02000F55 RID: 3925
	public class EmptyTableException : LearningException
	{
		// Token: 0x06006D36 RID: 27958 RVA: 0x00164714 File Offset: 0x00162914
		public EmptyTableException()
			: base("Example table is empty.")
		{
		}
	}
}
