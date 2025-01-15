using System;
using Microsoft.ReportingServices.OData.Json;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200007F RID: 127
	internal interface IJsonStreamWriter : IDisposable
	{
		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x0600077F RID: 1919
		JsonWriter JsonWriter { get; }

		// Token: 0x06000780 RID: 1920
		IJsonStreamWriter CreateJsonStreamWriter(string objectId);

		// Token: 0x06000781 RID: 1921
		void WriteException(string objectId, Exception exception);
	}
}
