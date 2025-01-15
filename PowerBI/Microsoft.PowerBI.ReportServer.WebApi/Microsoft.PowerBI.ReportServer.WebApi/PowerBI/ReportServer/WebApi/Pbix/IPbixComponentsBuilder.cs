using System;
using System.Threading.Tasks;
using Microsoft.PowerBI.ReportServer.PbixLib.Parsing;

namespace Microsoft.PowerBI.ReportServer.WebApi.Pbix
{
	// Token: 0x02000016 RID: 22
	public interface IPbixComponentsBuilder
	{
		// Token: 0x06000048 RID: 72
		Task<PbixComponents> BuildPbixComponentsFromPbixReportElements(PbixReportElements pbixReportElements, string requestId, string sessionId, bool getDataModelArtifacts = true);
	}
}
