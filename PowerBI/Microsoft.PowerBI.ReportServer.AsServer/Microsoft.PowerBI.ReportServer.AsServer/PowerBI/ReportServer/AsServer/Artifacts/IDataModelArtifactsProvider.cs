using System;
using System.IO;
using System.Threading.Tasks;

namespace Microsoft.PowerBI.ReportServer.AsServer.Artifacts
{
	// Token: 0x02000035 RID: 53
	public interface IDataModelArtifactsProvider
	{
		// Token: 0x06000121 RID: 289
		Task<DataModelArtifacts> RetrieveArtifactsAsync(Stream dataModel, string requestId, string sessionId);
	}
}
