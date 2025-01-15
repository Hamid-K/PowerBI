using System;

namespace Microsoft.Mashup.Engine1.Library.AnalysisServices
{
	// Token: 0x02000F3B RID: 3899
	internal interface IAnalysisServicesConnectionFactory
	{
		// Token: 0x06006724 RID: 26404
		IAnalysisServicesConnection CreateConnection(string connectionString);
	}
}
