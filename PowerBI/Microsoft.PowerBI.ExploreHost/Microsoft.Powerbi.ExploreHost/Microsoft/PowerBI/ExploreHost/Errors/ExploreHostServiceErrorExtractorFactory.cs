using System;
using Microsoft.InfoNav.Explore.ServiceContracts.Internal;

namespace Microsoft.PowerBI.ExploreHost.Errors
{
	// Token: 0x0200008B RID: 139
	public class ExploreHostServiceErrorExtractorFactory : IServiceErrorExtractorFactory
	{
		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060003A8 RID: 936 RVA: 0x0000B9DE File Offset: 0x00009BDE
		public static ExploreHostServiceErrorExtractorFactory Instance { get; } = new ExploreHostServiceErrorExtractorFactory();

		// Token: 0x060003A9 RID: 937 RVA: 0x0000B9E5 File Offset: 0x00009BE5
		protected ExploreHostServiceErrorExtractorFactory()
		{
		}

		// Token: 0x060003AA RID: 938 RVA: 0x0000BA14 File Offset: 0x00009C14
		public ServiceErrorExtractor Create()
		{
			return new ServiceErrorExtractor(this.Extractors, this.DefaultExtractor);
		}

		// Token: 0x040001B0 RID: 432
		public readonly IServiceErrorExtractor[] Extractors = new IServiceErrorExtractor[]
		{
			new ScriptHandlerErrorExtraction(),
			new PowerBIExploreExceptionExtractor()
		};

		// Token: 0x040001B1 RID: 433
		public readonly IServiceErrorExtractor DefaultExtractor = new DefaultServiceErrorExtraction();
	}
}
