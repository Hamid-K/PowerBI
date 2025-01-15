using System;
using Microsoft.PowerBI.ReportServer.AsServer.Mashup;
using Microsoft.PowerBI.ReportServer.PbixLib.Parsing;

namespace Microsoft.PowerBI.ReportServer.AsServer
{
	// Token: 0x02000009 RID: 9
	public class CheckConnectionServices : ICheckConnectionServices
	{
		// Token: 0x06000048 RID: 72 RVA: 0x00003B04 File Offset: 0x00001D04
		public PbixDataSourceCheckResult TestCredentials(PbixDataSource pbixDataSource)
		{
			if (pbixDataSource == null)
			{
				throw new ArgumentNullException("pbixDataSource");
			}
			return new MashupProviderManager().TestCredentials(pbixDataSource);
		}
	}
}
