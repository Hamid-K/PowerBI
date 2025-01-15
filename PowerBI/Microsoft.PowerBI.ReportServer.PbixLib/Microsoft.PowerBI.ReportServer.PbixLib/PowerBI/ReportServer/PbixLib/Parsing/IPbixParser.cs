using System;
using System.IO;

namespace Microsoft.PowerBI.ReportServer.PbixLib.Parsing
{
	// Token: 0x02000004 RID: 4
	public interface IPbixParser
	{
		// Token: 0x06000007 RID: 7
		PbixReportElements ParsePbixFileIntoParts(Stream pbixFileAsStream, string requestId, string sessionId);
	}
}
