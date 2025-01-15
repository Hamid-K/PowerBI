using System;
using System.Diagnostics;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x020000B7 RID: 183
	internal sealed class RSEventProvider
	{
		// Token: 0x06000600 RID: 1536 RVA: 0x00011916 File Offset: 0x0000FB16
		private RSEventProvider()
		{
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000601 RID: 1537 RVA: 0x0001191E File Offset: 0x0000FB1E
		public static IRSEventProvider Current
		{
			get
			{
				if (RSEventProvider.m_provider == null)
				{
					RSEventProvider.m_provider = new TraceEventProvider();
					RSEventProvider.m_initialized = true;
				}
				return RSEventProvider.m_provider;
			}
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x0001193C File Offset: 0x0000FB3C
		internal static void Init(IRSEventProvider provider)
		{
			if (RSEventProvider.m_initialized)
			{
				RSTrace.CatalogTrace.Trace(TraceLevel.Warning, "RSEventProvider initialized multiple times");
			}
			RSEventProvider.m_provider = provider;
			RSEventProvider.m_initialized = true;
		}

		// Token: 0x0400033E RID: 830
		private static bool m_initialized;

		// Token: 0x0400033F RID: 831
		private static IRSEventProvider m_provider;
	}
}
