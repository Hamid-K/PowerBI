using System;
using System.Data;
using Microsoft.AnalysisServices.AdomdClient;

namespace Microsoft.Mashup.Engine1.Library.AnalysisServices
{
	// Token: 0x02000F3C RID: 3900
	internal interface IAnalysisServicesConnection : IDisposable
	{
		// Token: 0x17001DD9 RID: 7641
		// (get) Token: 0x06006725 RID: 26405
		string ProviderVersion { get; }

		// Token: 0x17001DDA RID: 7642
		// (get) Token: 0x06006726 RID: 26406
		string ServerVersion { get; }

		// Token: 0x17001DDB RID: 7643
		// (get) Token: 0x06006727 RID: 26407
		ConnectionState State { get; }

		// Token: 0x06006728 RID: 26408
		void Open();

		// Token: 0x06006729 RID: 26409
		IAnalysisServicesCommand CreateCommand();

		// Token: 0x0600672A RID: 26410
		DataSet GetSchemaDataSet(string name, AdomdRestrictionCollection restrictions);
	}
}
