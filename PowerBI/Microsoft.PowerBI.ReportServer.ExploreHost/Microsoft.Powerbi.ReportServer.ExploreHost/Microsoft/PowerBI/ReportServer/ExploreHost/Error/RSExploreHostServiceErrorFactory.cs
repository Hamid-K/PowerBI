using System;
using Microsoft.InfoNav.Explore.ServiceContracts.Internal;

namespace Microsoft.PowerBI.ReportServer.ExploreHost.Error
{
	// Token: 0x02000021 RID: 33
	public sealed class RSExploreHostServiceErrorFactory : IServiceErrorExtractorFactory
	{
		// Token: 0x060000E6 RID: 230 RVA: 0x00003FAA File Offset: 0x000021AA
		public RSExploreHostServiceErrorFactory(IServiceErrorExtractor serviceErrorExtractor)
		{
			this.Extractors = new IServiceErrorExtractor[] { serviceErrorExtractor };
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00003FCD File Offset: 0x000021CD
		public ServiceErrorExtractor Create()
		{
			return new ServiceErrorExtractor(this.Extractors, this.DefaultExtractor);
		}

		// Token: 0x04000073 RID: 115
		public readonly IServiceErrorExtractor[] Extractors;

		// Token: 0x04000074 RID: 116
		public readonly IServiceErrorExtractor DefaultExtractor = new DefaultServiceErrorExtraction();
	}
}
