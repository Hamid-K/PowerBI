using System;

namespace Microsoft.AnalysisServices.Tabular.Tmdl
{
	// Token: 0x02000149 RID: 329
	internal struct TmdlSourceLocation
	{
		// Token: 0x0600154F RID: 5455 RVA: 0x0008F61F File Offset: 0x0008D81F
		public TmdlSourceLocation(TmdlDocument document, int lineNumber)
		{
			this.Document = document;
			this.LineNumber = lineNumber;
		}

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x06001550 RID: 5456 RVA: 0x0008F62F File Offset: 0x0008D82F
		internal bool IsValid
		{
			get
			{
				return this.Document != null && this.LineNumber > 0;
			}
		}

		// Token: 0x040003A9 RID: 937
		public readonly TmdlDocument Document;

		// Token: 0x040003AA RID: 938
		public readonly int LineNumber;
	}
}
