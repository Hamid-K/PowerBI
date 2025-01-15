using System;

namespace Microsoft.ProgramSynthesis.Extraction.Text.Learning
{
	// Token: 0x02000F50 RID: 3920
	public abstract class ExampleException : LearningException
	{
		// Token: 0x06006D2E RID: 27950 RVA: 0x001646C4 File Offset: 0x001628C4
		public ExampleException(int rowIndex, int columnIndex, string example, string message)
			: base(message)
		{
			this.RowIndex = rowIndex;
			this.ColumnIndex = columnIndex;
			this.Example = example;
		}

		// Token: 0x17001377 RID: 4983
		// (get) Token: 0x06006D2F RID: 27951 RVA: 0x001646E3 File Offset: 0x001628E3
		public int RowIndex { get; }

		// Token: 0x17001378 RID: 4984
		// (get) Token: 0x06006D30 RID: 27952 RVA: 0x001646EB File Offset: 0x001628EB
		public int ColumnIndex { get; }

		// Token: 0x17001379 RID: 4985
		// (get) Token: 0x06006D31 RID: 27953 RVA: 0x001646F3 File Offset: 0x001628F3
		public string Example { get; }
	}
}
