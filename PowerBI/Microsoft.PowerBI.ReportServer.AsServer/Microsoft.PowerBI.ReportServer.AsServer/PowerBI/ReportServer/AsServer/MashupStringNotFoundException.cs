using System;

namespace Microsoft.PowerBI.ReportServer.AsServer
{
	// Token: 0x02000021 RID: 33
	public class MashupStringNotFoundException : AnalysisServicesException
	{
		// Token: 0x060000B3 RID: 179 RVA: 0x00004391 File Offset: 0x00002591
		public MashupStringNotFoundException(string connectionString)
			: base(string.Format("Specified connection string does not contain Mashup string: {0}", connectionString))
		{
		}
	}
}
