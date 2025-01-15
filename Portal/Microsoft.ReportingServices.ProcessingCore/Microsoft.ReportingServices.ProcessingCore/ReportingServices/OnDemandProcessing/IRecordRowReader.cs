using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandProcessing
{
	// Token: 0x020007F2 RID: 2034
	internal interface IRecordRowReader : IDisposable
	{
		// Token: 0x060071A3 RID: 29091
		bool GetNextRow();

		// Token: 0x1700269D RID: 9885
		// (get) Token: 0x060071A4 RID: 29092
		RecordRow RecordRow { get; }

		// Token: 0x060071A5 RID: 29093
		bool MoveToFirstRow();

		// Token: 0x060071A6 RID: 29094
		void Close();
	}
}
