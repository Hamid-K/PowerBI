using System;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200061B RID: 1563
	internal interface IParameterDataSource
	{
		// Token: 0x17001F60 RID: 8032
		// (get) Token: 0x060055CC RID: 21964
		int DataSourceIndex { get; }

		// Token: 0x17001F61 RID: 8033
		// (get) Token: 0x060055CD RID: 21965
		int DataSetIndex { get; }

		// Token: 0x17001F62 RID: 8034
		// (get) Token: 0x060055CE RID: 21966
		int ValueFieldIndex { get; }

		// Token: 0x17001F63 RID: 8035
		// (get) Token: 0x060055CF RID: 21967
		int LabelFieldIndex { get; }
	}
}
