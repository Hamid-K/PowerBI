using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Explore.ServiceContracts.Internal
{
	// Token: 0x02000007 RID: 7
	public class ServiceErrorExtractor
	{
		// Token: 0x06000007 RID: 7 RVA: 0x0000207C File Offset: 0x0000027C
		public ServiceErrorExtractor(IEnumerable<IServiceErrorExtractor> customExtractors, IServiceErrorExtractor defaultExtractor = null)
		{
			this.MessageExtractors = new List<IServiceErrorExtractor>();
			foreach (IServiceErrorExtractor serviceErrorExtractor in customExtractors)
			{
				this.MessageExtractors.Add(serviceErrorExtractor);
			}
			this.DefaultExtractor = defaultExtractor ?? new DefaultServiceErrorExtraction();
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000008 RID: 8 RVA: 0x000020EC File Offset: 0x000002EC
		// (set) Token: 0x06000009 RID: 9 RVA: 0x000020F4 File Offset: 0x000002F4
		public IList<IServiceErrorExtractor> MessageExtractors { get; private set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000020FD File Offset: 0x000002FD
		// (set) Token: 0x0600000B RID: 11 RVA: 0x00002105 File Offset: 0x00000305
		public IServiceErrorExtractor DefaultExtractor { get; private set; }

		// Token: 0x0600000C RID: 12 RVA: 0x00002110 File Offset: 0x00000310
		public bool TryExtractServiceError(Exception ex, out ServiceError error)
		{
			if (ex == null)
			{
				error = new ServiceError();
				return true;
			}
			IServiceErrorExtractor serviceErrorExtractor = this.DefaultExtractor;
			foreach (IServiceErrorExtractor serviceErrorExtractor2 in this.MessageExtractors)
			{
				if (serviceErrorExtractor2.CanExtractFromException(ex))
				{
					serviceErrorExtractor = serviceErrorExtractor2;
					break;
				}
			}
			return serviceErrorExtractor.TryExtractServiceError(ex, this, out error);
		}
	}
}
