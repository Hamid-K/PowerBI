using System;
using System.Data;

namespace Microsoft.Mashup.Engine1.Library.AnalysisServices
{
	// Token: 0x02000F3D RID: 3901
	internal interface IAnalysisServicesCommand : IDisposable
	{
		// Token: 0x17001DDC RID: 7644
		// (set) Token: 0x0600672B RID: 26411
		string CommandText { set; }

		// Token: 0x17001DDD RID: 7645
		// (set) Token: 0x0600672C RID: 26412
		int CommandTimeout { set; }

		// Token: 0x0600672D RID: 26413
		IDataReader ExecuteReader();

		// Token: 0x0600672E RID: 26414
		void AddParameter(string name, object value);

		// Token: 0x0600672F RID: 26415
		void Cancel();
	}
}
